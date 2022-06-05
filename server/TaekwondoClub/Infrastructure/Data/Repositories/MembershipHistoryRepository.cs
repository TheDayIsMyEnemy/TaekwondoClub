using ApplicationCore.Interfaces;
using ApplicationCore.Models;

namespace Infrastructure.Data.Repositories
{
    public class MembershipHistoryRepository 
        : Repository<MembershipHistory>, IMembershipHistoryRepository
    {
        public MembershipHistoryRepository(TaekwondoClubContext context) : base(context) { }
    }
}
