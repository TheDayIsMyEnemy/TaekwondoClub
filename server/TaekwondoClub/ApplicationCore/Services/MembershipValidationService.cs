using ApplicationCore.Interfaces;

namespace ApplicationCore.Services
{
    public class MembershipValidationService : IMembershipValidationService
    {
        public bool Validate(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            if (startDate.Date < DateTime.UtcNow.Date || startDate.Date >= endDate.Date)
                return false;

            return true;
        }
    }
}
