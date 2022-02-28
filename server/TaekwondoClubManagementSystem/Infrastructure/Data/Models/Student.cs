using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }

        public string Gender { get; set; }

        public string? PhoneNumber { get; set; }

        public int? ClubMembershipId { get; set; }
        public ClubMembership? ClubMembership { get; set; }

        public ICollection<Group> Groups { get; set; } = null!;

        public Student(string firstName, string lastName, string gender)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
        }
    }
}
