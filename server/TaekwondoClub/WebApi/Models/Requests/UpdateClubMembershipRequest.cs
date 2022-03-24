using System.Text.Json.Serialization;

namespace WebApi.Models.Requests
{
    public class UpdateClubMembershipRequest
    {
        [JsonPropertyName("clubMembershipId")]
        public int ClubMembershipId { get; set; }

        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }
    }
}
