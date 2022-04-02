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

        public async Task<bool> CreateNewStudentWithMembership(
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
                student.Membership = new Membership
                {
                    CreatedDate = DateTime.Now,
                    StartDate = membershipPeriod[0],
                    EndDate = membershipPeriod[1]
                };
            }

            try
            {
                await _studentRepository.AddAsync(student);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
