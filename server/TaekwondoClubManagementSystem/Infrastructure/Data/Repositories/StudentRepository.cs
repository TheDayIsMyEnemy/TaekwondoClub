using Data.Models;
using Infrastructure.Data.Repositories.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class StudentRepository : AsyncRepository<Student>, IStudentRepository
    {
        public StudentRepository(TaekwondoClubContext dbContext) : base(dbContext)
        {
        }

        public async Task<Student?> GetStudentAndClubMembershipByStudentId(int studentId)
        {
            return await _entities
                .Include(s => s.ClubMembership)
                .FirstOrDefaultAsync(s => s.Id == studentId);
        }

        public async Task<Student?> GetStudentByFirstNameAndLastName(string firstName, string lastName)
        {
            return await _entities
                .Where(s => s.FirstName == firstName && s.LastName == lastName)
                .FirstOrDefaultAsync();
        }
    }
}
