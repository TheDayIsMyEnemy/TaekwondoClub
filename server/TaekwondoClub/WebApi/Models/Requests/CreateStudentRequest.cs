namespace WebApi.Models.Requests
{
    public class CreateStudentRequest
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Gender { get; set; } = null!;

        public DateTime? BirthDate { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
