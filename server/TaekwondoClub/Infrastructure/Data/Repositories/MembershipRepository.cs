using ApplicationCore.Interfaces;
using ApplicationCore.Models;

namespace Infrastructure.Data.Repositories
{
    public class MembershipRepository : Repository<Membership>, IMembershipRepository
    {
        public MembershipRepository(TaekwondoClubContext context) : base(context) { }
    }
}
