using ApplicationCore.Enums;

namespace ApplicationCore.Models
{
    public class Student : AuditableEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public Gender Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? PhoneNumber { get; set; }

        public Membership? Membership { get; set; }

        public ICollection<Group> Groups { get; set; } = null!;
    }
}
