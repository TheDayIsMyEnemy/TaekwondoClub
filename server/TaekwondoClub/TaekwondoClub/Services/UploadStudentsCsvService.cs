using System.Text;
using TaekwondoClub.Interfaces;
using TaekwondoClub.Models;

namespace TaekwondoClub.Services
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

        public async Task<int> CreateNewStudentsFromCsvFile(MemoryStream csvStream)
        {
            // To do: Add validations 
            var csvRows = GetCsvRows(csvStream);
            var csvColumns = csvRows[0];
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

            return newStudents.Count;
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
