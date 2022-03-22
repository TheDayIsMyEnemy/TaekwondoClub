using System.Text;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using ApplicationCore.Enums;

namespace ApplicationCore.Services
{
    public class UploadStudentsCsvService : IUploadStudentsCsvService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly Dictionary<string, string> _csvColumnToStudentProp =
            new Dictionary<string, string>() {
                { "Preferred First Name", "FirstName" },
                { "Preferred Last Name", "LastName" },
                { "Gender", "Gender" },
                { "Date of Birth", "BirthDate"}
            };

        public UploadStudentsCsvService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<(CreateStudentsFromCsvOutcome, int?)> CreateStudentsFromCsvFile(Stream csvStream)
        {
            if (csvStream == null)
                return (CreateStudentsFromCsvOutcome.FileNotFound, null);

            if (csvStream.Length == 0)
                return (CreateStudentsFromCsvOutcome.EmptyFile, null);

            var memoryStream = new MemoryStream();
            await csvStream.CopyToAsync(memoryStream);

            string[] csvColumns = null;
            string[][] csvRows = null;

            try
            {
                csvRows = GetCsvRows(memoryStream);
                csvColumns = csvRows[0];
            }
            catch (Exception)
            {
                return (CreateStudentsFromCsvOutcome.InvalidFile, null);
            }

            if (!ValidateColumns(csvColumns))
                return (CreateStudentsFromCsvOutcome.MissingRequiredColumns, null);          

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

            return (CreateStudentsFromCsvOutcome.Success, newStudents.Count);
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
            var student = new Student("", "", "");
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
                    var parsedCellValue = ParseCellValue(cellValue, studentPropName);
                    studentProp?.SetValue(student, parsedCellValue);
                }
            }
            return student;
        }

        private object ParseCellValue(string cellValue, string studentPropName)
        {
            if (studentPropName.Equals("BirthDate"))
                return DateTime.ParseExact(cellValue, "dd/MM/yyyy", null);

            return cellValue;
        }
    }
}
