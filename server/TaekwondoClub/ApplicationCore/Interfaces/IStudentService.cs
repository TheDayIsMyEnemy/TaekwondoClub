namespace ApplicationCore.Interfaces
{
    public interface IStudentService
    {
        public Task<bool> CreateNewStudentWithMembership(
            string firstName,
            string lastName,
            string gender,
            DateTime? birthDate,
            string? phoneNumber,
            DateTime[]? membershipPeriod);
    }
}
