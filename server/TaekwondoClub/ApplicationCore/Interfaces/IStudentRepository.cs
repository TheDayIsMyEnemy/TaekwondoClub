using ApplicationCore.Models;

namespace ApplicationCore.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<IEnumerable<Student>> GetAllStudentsAndMembership();

        Task<Student?> GetStudentAndMembershipByStudentId(int studentId);

        Task<Student?> GetStudentByFirstNameAndLastName(string firstName, string lastName);
    }
}
