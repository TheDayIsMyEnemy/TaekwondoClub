using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Requests
{
    public class MembershipRequest : IValidatableObject
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Range(20, 500)]
        public double SubscriptionFee { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate.ToLocalTime().Date < DateTime.Now.Date)
                yield return new ValidationResult("Start date cannot be in the past.");
            if (StartDate.ToLocalTime().Date >= EndDate.ToLocalTime().Date)
                yield return new ValidationResult("End date cannot be the same or before the Start date.");
        }
    }
}
