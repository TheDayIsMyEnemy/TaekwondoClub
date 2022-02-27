using Data.Models;
using Data.Repositories.Interfaces;
using Infrastructure.Repositories;

namespace Data.Repositories
{
    public class StudentMembershipRepository : AsyncRepository<StudentMembership>, IStudentMembershipRepository
    {
        public StudentMembershipRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
