using TaekwondoClub.Models;

namespace TaekwondoClub.Interfaces
{
    public interface IStudentRepository : IAsyncRepository<Student>
    {
        public Task<Student?> GetStudentAndClubMembershipByStudentId(int studentId);

        public Task<Student?> GetStudentByFirstNameAndLastName(string firstName, string lastName);
    }
}
