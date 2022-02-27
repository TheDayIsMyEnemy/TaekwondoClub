using Data.Models;
using Data.Repositories.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class StudentRepository : AsyncRepository<Student>, IStudentRepository
    {
        public StudentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Student> GetStudentByIdAndStudentMembership(int studentId)
        {
            var student = await _dbContext
                .Students
                .Include(s => s.Membership)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            return student;
        }
    }
}
