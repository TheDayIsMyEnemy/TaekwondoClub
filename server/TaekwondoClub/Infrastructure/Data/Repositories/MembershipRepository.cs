using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class MembershipRepository : Repository<Membership>, IMembershipRepository
    {
        public MembershipRepository(TaekwondoClubContext context) : base(context) { }

        public async Task<IEnumerable<Membership>> GetAllMembershipsAndHistory()
            => await _dbSet.Include(m => m.History).ToListAsync();
    }
}
