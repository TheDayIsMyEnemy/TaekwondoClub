namespace ApplicationCore.Interfaces
{
    public interface IMembershipService
    {
        Task<bool> CreateNewMembership(int studentId, DateTime startDate, DateTime endDate);
        Task<bool> UpdateMembership(int clubMembershipId, DateTime startDate, DateTime endDate);
    }
}
