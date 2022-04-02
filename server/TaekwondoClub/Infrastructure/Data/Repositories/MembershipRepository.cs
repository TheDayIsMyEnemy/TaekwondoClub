using ApplicationCore.Interfaces;
using ApplicationCore.Models;

namespace Infrastructure.Data.Repositories
{
    public class MembershipRepository : AsyncRepository<Membership>, IMembershipRepository
    {
        public MembershipRepository(TaekwondoClubContext context) : base(context) { }
    }
}
