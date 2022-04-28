using ApplicationCore.Enums;

namespace ApplicationCore.Models
{
    public class Student
    {
        public Student(
            string firstName,
            string lastName,
            Gender gender,
            DateTime? birthDate,
            string? phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? PhoneNumber { get; set; }

        public Membership? Membership { get; set; }

        public ICollection<Group> Groups { get; set; } = null!;
    }
}
