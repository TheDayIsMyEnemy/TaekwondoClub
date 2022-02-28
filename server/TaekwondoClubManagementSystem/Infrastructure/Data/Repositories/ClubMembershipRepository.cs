using Infrastructure.Data.Models;
using Infrastructure.Data.Repositories.Interfaces;
using Infrastructure.Repositories;

namespace Infrastructure.Data.Repositories
{
    public class ClubMembershipRepository : AsyncRepository<ClubMembership>, IClubMembershipRepository
    {
        public ClubMembershipRepository(TaekwondoClubContext dbContext) : base(dbContext)
        {
        }
    }
}
