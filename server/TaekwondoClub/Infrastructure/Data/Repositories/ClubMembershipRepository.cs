using TaekwondoClub.Interfaces;
using TaekwondoClub.Models;

namespace Infrastructure.Data.Repositories
{
    public class ClubMembershipRepository : AsyncRepository<ClubMembership>, IClubMembershipRepository
    {
        public ClubMembershipRepository(TaekwondoClubContext context) : base(context) { }
    }
}
