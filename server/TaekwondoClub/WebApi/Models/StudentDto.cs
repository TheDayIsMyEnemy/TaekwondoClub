namespace WebApi.Models
{
    public class StudentDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Gender { get; set; }

        public string? PhoneNumber { get; set; }

        public ClubMembershipDto ClubMembership { get; set; }
    }
}
