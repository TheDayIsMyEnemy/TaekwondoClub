using ApplicationCore.Models;

namespace ApplicationCore.Interfaces
{
    public interface IStudentRepository : IAsyncRepository<Student>
    {
        public Task<IEnumerable<Student>> GetAllStudentsWithMembership();

        public Task<Student?> GetStudentAndMembershipByStudentId(int studentId);

        public Task<Student?> GetStudentByFirstNameAndLastName(string firstName, string lastName);
    }
}
