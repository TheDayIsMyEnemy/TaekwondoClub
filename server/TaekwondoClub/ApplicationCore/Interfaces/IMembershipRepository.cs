using ApplicationCore.Models;

namespace ApplicationCore.Interfaces
{
    public interface IMembershipRepository : IRepository<Membership> 
    {
        Task<IEnumerable<Membership>> GetAllMembershipsAndHistory();
    }
}
