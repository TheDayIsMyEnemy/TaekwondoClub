using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers
{
    public class ClubMembershipController : ApiControllerBase
    {
        private readonly IClubMembershipService _clubMembershipService;

        public ClubMembershipController(IClubMembershipService clubMembershipService)
        {
            _clubMembershipService = clubMembershipService;
        }

        public class MembershipRequest
        {
            public int StudentId { get; set; }

            public int MembershipExpirationDays { get; set; }
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MembershipRequest request)
        {
            var result =
                await _clubMembershipService
                .CreateClubMembership(request.StudentId, request.MembershipExpirationDays);

            return Ok(result);
        }

        //public void Update(MembershipRequest request)
        //{

        //}
    }
}
