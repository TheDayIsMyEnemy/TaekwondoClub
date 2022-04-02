namespace WebApi.Models.Requests
{
    public class UpdateMembershipRequest
    {
        public int MembershipId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
