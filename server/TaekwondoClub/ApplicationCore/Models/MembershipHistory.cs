namespace ApplicationCore.Models
{
    public class MembershipHistory
    {
        public int Id { get; set; }

        public int MembershipId { get; set; }

        public Membership Membership { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double SubscriptionFee { get; set; }
    }
}
