using ApplicationCore.Enums;
using ApplicationCore.Models;

namespace ApplicationCore.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllStudentsAndMembership();

        Task<(GetStudentOutcome, Student?)> GetStudentAndMembershipByStudentId(int studentId);

        Task<DeleteStudentOutcome> DeleteStudent(int studentId);

        Task<CreateStudentWithMembershipOutcome> CreateStudentWithMembership(
            string firstName,
            string lastName,
            Gender gender,
            DateTime? birthDate,
            string? phoneNumber,
            DateTime[]? membershipPeriod);
    }
}
