using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentMembershipController : ControllerBase
    {
        private readonly IStudentMembershipService _studentMembershipService;

        public StudentMembershipController(IStudentMembershipService studentMembershipService)
        {
            _studentMembershipService = studentMembershipService;
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
                await _studentMembershipService
                .CreateStudentMembership(request.StudentId, request.MembershipExpirationDays);

            return Ok(result);
        }

        public void Update(MembershipRequest request)
        {

        }
    }
}
