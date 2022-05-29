using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;

namespace ApplicationCore.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMembershipValidationService _membershipValidationService;

        public StudentService(
            IStudentRepository studentRepository,
            IMembershipValidationService membershipValidationService)
        {
            _studentRepository = studentRepository;
            _membershipValidationService = membershipValidationService;
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

        public async Task<CreateStudentWithMembershipOutcome> CreateStudentWithMembership(
            string firstName,
            string lastName,
            Gender gender,
            DateTime? birthDate,
            string? phoneNumber,
            DateTime[]? membershipPeriod)
        {
            var student = await _studentRepository
                .GetStudentByFirstNameAndLastName(firstName, lastName);
            if (student != null)
                return CreateStudentWithMembershipOutcome.StudentAlreadyExists;

            student = new Student(firstName, lastName, gender, birthDate, phoneNumber);

            if (membershipPeriod != null)
            {
                if (!_membershipValidationService.Validate(membershipPeriod[0], membershipPeriod[1]))
                    return CreateStudentWithMembershipOutcome.MembershipPeriodValidationFailed;

                student.Membership = new Membership
                {
                    StartDate = membershipPeriod[0],
                    EndDate = membershipPeriod[1],
                    CreatedDate = DateTime.Now
                };
            }

            try
            {
                student = await _studentRepository.AddAsync(student);
            }
            catch (Exception)
            {
                return CreateStudentWithMembershipOutcome.InsertFailed;
            }

            return CreateStudentWithMembershipOutcome.Success;
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
