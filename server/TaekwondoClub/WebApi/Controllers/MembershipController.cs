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
            var result =
                await _membershipService
                .CreateNewMembership(req.StudentId, req.StartDate, req.EndDate);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateMembershipRequest req)
        {
            var result =
                await _membershipService
                .UpdateMembership(req.MembershipId, req.StartDate, req.EndDate);

            return Ok(result);
        }
    }
}
