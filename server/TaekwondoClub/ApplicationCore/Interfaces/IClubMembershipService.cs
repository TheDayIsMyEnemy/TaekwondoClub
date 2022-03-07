namespace ApplicationCore.Interfaces
{
    public interface IClubMembershipService
    {
        Task<bool> CreateNewClubMembership(int studentId, DateTime startDate, DateTime endDate);
    }
}
