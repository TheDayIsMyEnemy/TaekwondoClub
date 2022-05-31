using ApplicationCore.Enums;

namespace ApplicationCore.Interfaces
{
    public interface IMembershipService
    {
        Task<CreateMembershipOutcome> CreateMembership(int studentId, DateTimeOffset startDate, DateTimeOffset endDate);
        Task<UpdateMembershipOutcome> UpdateMembership(int membershipId, DateTimeOffset startDate, DateTimeOffset endDate);
    }
}
