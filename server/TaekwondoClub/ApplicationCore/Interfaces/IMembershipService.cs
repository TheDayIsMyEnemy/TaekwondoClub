using ApplicationCore.Enums;

namespace ApplicationCore.Interfaces
{
    public interface IMembershipService
    {
        Task<CreateMembershipOutcome> CreateMembership(int studentId, DateTime startDate, DateTime endDate);
        Task<UpdateMembershipOutcome> UpdateMembership(int membershipId, DateTime startDate, DateTime endDate);
    }
}
