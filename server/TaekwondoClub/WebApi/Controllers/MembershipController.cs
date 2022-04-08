using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Requests;

namespace WebApi.Controllers
{
    public class MembershipController : ApiControllerBase
    {
        private readonly IMembershipService _membershipService;

        public MembershipController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMembershipRequest req)
        {
            var outcome = await _membershipService
                .CreateMembership(req.StudentId, req.StartDate, req.EndDate);

            switch (outcome)
            {
                case CreateMembershipOutcome.Success:
                    return Ok();
                case CreateMembershipOutcome.StudentNotFound:
                case CreateMembershipOutcome.StudentMembershipAlreadyExists:
                case CreateMembershipOutcome.InvalidMembershipPeriod:
                case CreateMembershipOutcome.InsertFailed:
                    return UnprocessableEntity();
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateMembershipRequest req)
        {
            var outcome = await _membershipService
                .UpdateMembership(req.MembershipId, req.StartDate, req.EndDate);

            switch (outcome)
            {
                case UpdateMembershipOutcome.Success:
                    return Ok();
                case UpdateMembershipOutcome.MembershipNotFound:
                case UpdateMembershipOutcome.InvalidMembershipPeriod:
                case UpdateMembershipOutcome.UpdateFailed:
                    return UnprocessableEntity();
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
