namespace WebApi.Models
{
    public class StudentDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateTime? BirthDate { get; set; }

        public string Gender { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public ClubMembershipDto? ClubMembership { get; set; }
    }
}
