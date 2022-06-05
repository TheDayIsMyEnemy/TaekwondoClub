namespace ApplicationCore.Models
{
    public class Membership : AuditableEntity
    {     
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double SubscriptionFee { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; } = null!;

        public ICollection<MembershipHistory> History { get; set; } = null!;

        public bool IsActive => DateTime.Now.Date <= EndDate.Date;
    }
}
