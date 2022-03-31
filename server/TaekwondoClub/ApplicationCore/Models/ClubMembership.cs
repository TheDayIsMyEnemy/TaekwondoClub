namespace ApplicationCore.Models
{
    public class ClubMembership
    {     
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string? Note { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public bool IsActive => DateTime.Now.Date <= EndDate.Date;
    }
}
