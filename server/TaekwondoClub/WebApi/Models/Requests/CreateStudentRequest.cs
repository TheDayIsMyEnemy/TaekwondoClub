using ApplicationCore.Enums;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Requests
{
    public class CreateStudentRequest
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        public Gender Gender { get; set; }

        public DateTimeOffset? BirthDate { get; set; }

        [RegularExpression(@"^0\d{9}$|^359\d{9}$")]
        public string? PhoneNumber { get; set; }

        public DateTimeOffset[]? MembershipPeriod { get; set; }
    }
}
