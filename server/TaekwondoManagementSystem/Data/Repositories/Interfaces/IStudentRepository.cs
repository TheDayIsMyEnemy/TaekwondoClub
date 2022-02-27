using Data.Models;

namespace Data.Repositories.Interfaces
{
    public interface IStudentRepository : IAsyncRepository<Student>
    {
        public Task<Student> GetStudentByIdAndStudentMembership(int studentId);
    }
}
