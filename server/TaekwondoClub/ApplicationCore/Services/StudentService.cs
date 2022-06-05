using ApplicationCore.Enums;
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

        public async Task<IEnumerable<Student>> GetAllStudentsAndMembership()
            => await _studentRepository.GetAllStudentsAndMembership();

        public async Task<(GetStudentOutcome, Student?)> GetStudentAndMembershipByStudentId(
            int studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
                return (GetStudentOutcome.NotFound, null);

            return (GetStudentOutcome.Success, student);
        }

        public async Task<(CreateStudentOutcome, int?)> CreateStudent(
            string firstName,
            string lastName,
            Gender gender,
            DateTime? birthDate,
            string? phoneNumber)
        {
            var student = await _studentRepository
                .GetStudentByFirstNameAndLastName(firstName, lastName);
            if (student != null)
                return (CreateStudentOutcome.StudentAlreadyExists, null);

            student = new Student
            {
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                BirthDate = birthDate?.ToLocalTime(),
                PhoneNumber = phoneNumber
            };

            try
            {
                student = await _studentRepository.AddAsync(student);
            }
            catch (Exception)
            {
                return (CreateStudentOutcome.InsertFailed, null);
            }

            return (CreateStudentOutcome.Success, student.Id);
        }

        public async Task<DeleteStudentOutcome> DeleteStudent(int studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
                return DeleteStudentOutcome.NotFound;

            try
            {
                await _studentRepository.RemoveAsync(student);
            }
            catch (Exception)
            {
                return DeleteStudentOutcome.DeleteFailed;
            }

            return DeleteStudentOutcome.Success;
        }
    }
}
