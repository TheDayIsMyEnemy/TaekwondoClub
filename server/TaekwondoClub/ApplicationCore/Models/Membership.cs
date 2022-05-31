namespace ApplicationCore.Models
{
    public class Membership
    {     
        public int Id { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; } = null!;

        public bool IsActive => DateTime.UtcNow.Date <= EndDate.Date;
    }
}
