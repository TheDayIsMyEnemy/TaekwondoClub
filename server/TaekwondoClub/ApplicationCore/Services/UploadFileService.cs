using System.Text;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using ApplicationCore.Enums;
using System.Reflection;

namespace ApplicationCore.Services
{
    public class UploadFileService : IUploadFileService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly Dictionary<string, string> _csvColumnToStudentProp =
            new Dictionary<string, string>() {
                { "Preferred First Name", "FirstName" },
                { "Preferred Last Name", "LastName" },
                { "Gender", "Gender" },
                { "Date of Birth", "BirthDate"}
            };

        public UploadFileService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<(UploadStudentsCsvFileOutcome, int?)> UploadStudentsCsvFile(Stream csvStream)
        {
            if (csvStream == null)
                return (UploadStudentsCsvFileOutcome.FileNotFound, null);

            if (csvStream.Length == 0)
                return (UploadStudentsCsvFileOutcome.EmptyFile, null);

            var memoryStream = new MemoryStream();
            await csvStream.CopyToAsync(memoryStream);

            string[] csvColumns = null!;
            string[][] csvRows = null!;

            try
            {
                csvRows = GetCsvRows(memoryStream);
                csvColumns = csvRows[0];
            }
            catch (Exception)
            {
                return (UploadStudentsCsvFileOutcome.InvalidFile, null);
            }

            if (!ValidateColumns(csvColumns))
                return (UploadStudentsCsvFileOutcome.MissingRequiredColumns, null);          

            var newStudents = new List<Student>();

            for (int row = 1; row < csvRows.Length; row++)
            {
                var student = CreateNewStudentFromCsvRow(csvColumns, csvRows[row]);
                var studentExists = await _studentRepository
                    .GetStudentByFirstNameAndLastName(student.FirstName, student.LastName) != null;
                if (!studentExists)
                    newStudents.Add(student);
            }

            if (newStudents.Any())
                await _studentRepository.AddRangeAsync(newStudents);

            return (UploadStudentsCsvFileOutcome.Success, newStudents.Count);
        }

        private bool ValidateColumns(string[] csvColumns)
        {
            return true;
            // check if all required columns exist
        }

        private string[][] GetCsvRows(MemoryStream csvStream)
        {
            return Encoding.UTF8
               .GetString(csvStream.ToArray())
               .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
               .Select(row => row.Split(",")
                    .Select(rowValue => rowValue.Replace("\"", String.Empty))
                    .ToArray())
               .ToArray();
        }

        private Student CreateNewStudentFromCsvRow(string[] csvColumns, string[] csvRow)
        {
            var student = new Student("", "", Gender.Male, null, null);

            var studentType = student.GetType();

            for (int colNum = 0; colNum < csvRow.Length; colNum++)
            {
                var columnName = csvColumns[colNum];
                var cellValue = csvRow[colNum];

                if (_csvColumnToStudentProp.ContainsKey(columnName) &&
                    !string.IsNullOrWhiteSpace(cellValue))
                {
                    var studentPropName = _csvColumnToStudentProp[columnName];
                    var studentProp = studentType.GetProperty(studentPropName);
                    if (studentProp == null)
                        continue;

                    var propValue = GetPropertyValue(cellValue, studentProp);
                    studentProp.SetValue(student, propValue);
                }
            }
            return student;
        }

        private object GetPropertyValue(string cellValue, PropertyInfo prop)
        {
            if (prop.PropertyType == typeof(Gender))
                return Enum.Parse(prop.PropertyType, cellValue);
            if (prop.PropertyType == typeof(DateTime?))
                return DateTime.ParseExact(cellValue, "dd/MM/yyyy", null);

            return cellValue;
        }
    }
}
