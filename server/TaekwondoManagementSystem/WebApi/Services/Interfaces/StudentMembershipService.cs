namespace WebApi.Services
{
    public interface IStudentMembershipService
    {
        public Task<bool> CreateStudentMembership(int studentId, int days);
    }
}
