using System.Text.Json.Serialization;

namespace WebApi.Models.Requests
{
    public class CreateClubMembershipRequest
    {
        [JsonPropertyName("studentId")]
        public int StudentId { get; set; }

        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }
    }
}
