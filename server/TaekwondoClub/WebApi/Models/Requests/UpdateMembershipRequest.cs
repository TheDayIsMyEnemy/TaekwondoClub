namespace WebApi.Models.Requests
{
    public class UpdateMembershipRequest
    {
        public int ClubMembershipId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
