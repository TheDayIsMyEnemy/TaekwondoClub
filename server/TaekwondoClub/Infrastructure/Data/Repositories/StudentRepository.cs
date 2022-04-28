using Microsoft.EntityFrameworkCore;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;

namespace Infrastructure.Data.Repositories
{
    public class StudentRepository : AsyncRepository<Student>, IStudentRepository
    {
        public StudentRepository(TaekwondoClubContext context) : base(context) { }

        public async Task<IEnumerable<Student>> GetAllStudentsAndMembership()
        {
            return await _dbSet
                .Include(s => s.Membership)
                .ToListAsync();
        }

        public async Task<Student?> GetStudentAndMembershipByStudentId(int studentId)
        {
            return await _dbSet
                .Include(s => s.Membership)
                .FirstOrDefaultAsync(s => s.Id == studentId);
        }

        public async Task<Student?> GetStudentByFirstNameAndLastName(
            string firstName,
            string lastName)
        {
            return await _dbSet.FirstOrDefaultAsync(s =>
                s.FirstName.ToLower().Equals(firstName.ToLower()) &&
                s.LastName.ToLower().Equals(lastName.ToLower()));
        }
    }
}
