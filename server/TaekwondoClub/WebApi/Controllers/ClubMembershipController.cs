using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Requests;

namespace WebApi.Controllers
{
    public class ClubMembershipController : ApiControllerBase
    {
        private readonly IClubMembershipService _clubMembershipService;

        public ClubMembershipController(IClubMembershipService clubMembershipService)
        {
            _clubMembershipService = clubMembershipService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClubMembershipRequest request)
        {
            var result =
                await _clubMembershipService
                .CreateNewClubMembership(request.StudentId, request.StartDate, request.EndDate);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateClubMembershipRequest request)
        {
            var result =
                await _clubMembershipService
                .UpdateClubMembership(request.ClubMembershipId, request.StartDate, request.EndDate);

            return Ok(result);
        }
    }
}
