namespace WebApi.Services
{
    public interface IClubMembershipService
    {
        public Task<bool> CreateClubMembership(int studentId, int days);
    }
}
