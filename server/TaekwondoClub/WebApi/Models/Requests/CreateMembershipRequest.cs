namespace WebApi.Models.Requests
{
    public class CreateMembershipRequest
    {
        public int StudentId { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }
    }
}
