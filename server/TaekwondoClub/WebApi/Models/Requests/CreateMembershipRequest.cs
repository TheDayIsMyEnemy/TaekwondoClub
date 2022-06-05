namespace WebApi.Models.Requests
{
    public class CreateMembershipRequest : MembershipRequest
    {
        public int StudentId { get; set; }
    }
}
