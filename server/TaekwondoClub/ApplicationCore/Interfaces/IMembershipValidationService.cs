namespace ApplicationCore.Interfaces
{
    public interface IMembershipValidationService
    {
        bool Validate(DateTimeOffset startDate, DateTimeOffset endDate);
    }
}
