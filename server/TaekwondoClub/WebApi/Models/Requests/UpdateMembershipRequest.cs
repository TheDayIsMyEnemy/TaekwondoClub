namespace WebApi.Models.Requests
{
    public class UpdateMembershipRequest
    {
        public int MembershipId { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }
    }
}
