using ApplicationCore.Interfaces;
using ApplicationCore.Models;

namespace ApplicationCore.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<bool> CreateNewStudent(
            string firstName,
            string lastName,
            string gender,
            DateTime? birthDate,
            string? phoneNumber,
            DateTime[]? membershipPeriod)
        {
            // add validations
            var student = new Student(firstName, lastName, gender, birthDate, phoneNumber);
            if (membershipPeriod != null)
            {
                //var membership = new ClubMembership { S}
            }

            try
            {
               await  _studentRepository.AddAsync(student);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
