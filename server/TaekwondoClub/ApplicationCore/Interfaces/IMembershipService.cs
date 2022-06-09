using ApplicationCore.Enums;
using ApplicationCore.Models;

namespace ApplicationCore.Interfaces
{
    public interface IMembershipService
    {
        Task<IEnumerable<Membership>> GetAllMembershipsAndHistory();

        Task<CreateMembershipOutcome> CreateMembership(
            int studentId,
            DateTime startDate,
            DateTime endDate,
            double subscriptionFee);

        Task<UpdateMembershipOutcome> UpdateMembership(
            int membershipId,
            DateTime startDate,
            DateTime endDate,
            double subscriptionFee);
    }
}
