using Data;
using Data.Models;
using System.Text;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class StudentsCsvUploadService : IStudentsCsvUploadService
    {
        private readonly AppDbContext dbContext;

        public StudentsCsvUploadService(AppDbContext db)
        {
            dbContext = db;
        }

        public async Task CreateNewStudentsFromCsvFile(MemoryStream memoryStream)
        {
            var studentList = Encoding.
           UTF8.
           GetString(memoryStream.ToArray())
           .Split(Environment.NewLine)
           .Select(s => s.Replace("\\", String.Empty))
           .Select(s => s.Replace("\"", String.Empty))
           .ToList();

            var columns = studentList[0].Split(",").ToArray();
            var dic = new Dictionary<string, string>();
            dic.Add("Preferred First Name", "FirstName");
            dic.Add("Preferred Last Name", "LastName");
            dic.Add("Gender", "Gender");
            dic.Add("Date of Birth", "BirthDate");

            var newStudents = new List<Student>();
            for (int i = 1; i < studentList.Count; i++)
            {
                var studentRow = studentList[i].Split(",");
                var student = new Student();

                for (int j = 0; j < studentRow.Length; j++)
                {
                    var studentProp = columns[j];
                    var studentPropValue = studentRow[j];

                    if (dic.ContainsKey(studentProp))
                    {
                        var studentDbProp = dic[studentProp];

                        if (studentDbProp == "BirthDate")
                        {
                            var prop = student
                            .GetType()
                            .GetProperty(studentDbProp);
                            DateTime? dateTime = DateTime
                                .ParseExact(studentPropValue
                                , "dd/MM/yyyy"
                                , null);
                            prop.SetValue(student, dateTime);
                        }
                        else
                        {
                            student
                            .GetType()
                            .GetProperty(studentDbProp)
                            .SetValue(student, studentPropValue);
                            newStudents.Add(student);
                        }
                    }
                }
            }

            await dbContext.AddRangeAsync(newStudents);
            await dbContext.SaveChangesAsync();
        }
    }
}
