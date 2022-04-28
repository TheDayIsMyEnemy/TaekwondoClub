using ApplicationCore.Interfaces;

namespace ApplicationCore.Services
{
    public class MembershipValidationService : IMembershipValidationService
    {
        public bool Validate(DateTime startDate, DateTime endDate)
        {
            if (startDate.Date < DateTime.Now.Date || startDate.Date >= endDate.Date)
                return false;

            return true;
        }
    }
}
