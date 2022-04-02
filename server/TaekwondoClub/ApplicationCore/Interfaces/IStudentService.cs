namespace ApplicationCore.Interfaces
{
    public interface IStudentService
    {
        public Task<bool> CreateNewStudent(
            string firstName,
            string lastName,
            string gender,
            DateTime? birthDate,
            string? phoneNumber,
            DateTime[]? membershipPeriod);
    }
}
