using Microsoft.EntityFrameworkCore;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;

namespace Infrastructure.Data.Repositories
{
    public class StudentRepository : AsyncRepository<Student>, IStudentRepository
    {
        public StudentRepository(TaekwondoClubContext context) : base(context) { }

        public async Task<Student?> GetStudentAndClubMembershipByStudentId(int studentId)
        {
            return await _dbSet
                .Include(s => s.ClubMembership)
                .FirstOrDefaultAsync(s => s.Id == studentId);
        }

        public async Task<Student?> GetStudentByFirstNameAndLastName(string firstName, string lastName)
        {
            return await _dbSet
                .Where(s => s.FirstName == firstName && s.LastName == lastName)
                .FirstOrDefaultAsync();
        }
    }
}
