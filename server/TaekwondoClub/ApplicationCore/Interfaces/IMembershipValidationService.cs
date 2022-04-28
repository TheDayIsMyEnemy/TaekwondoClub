namespace ApplicationCore.Interfaces
{
    public interface IMembershipValidationService
    {
        bool Validate(DateTime startDate, DateTime endDate);
    }
}
