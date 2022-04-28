using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Requests;

namespace WebApi.Controllers
{
    public class StudentsController : ApiControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAll()
        {
            var students = await _studentService.GetAllStudentsAndMembership();

            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            var (outcome, student) = await _studentService.GetStudentAndMembershipByStudentId(id);

            switch (outcome)
            {
                case GetStudentOutcome.NotFound:
                    return NotFound();
                case GetStudentOutcome.Success:
                    return Ok(student);
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentRequest req)
        {
            var outcome = await _studentService.CreateStudentWithMembership(
                req.FirstName,
                req.LastName,
                req.Gender,
                req.BirthDate,
                req.PhoneNumber,
                req.MembershipPeriod);

            switch (outcome)
            {
                case CreateStudentWithMembershipOutcome.StudentAlreadyExists:
                case CreateStudentWithMembershipOutcome.MembershipPeriodValidationFailed:      
                case CreateStudentWithMembershipOutcome.InsertFailed:
                    return UnprocessableEntity();
                case CreateStudentWithMembershipOutcome.Success:
                    return Ok();
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var outcome = await _studentService.DeleteStudent(id);

            switch (outcome)
            {
                case DeleteStudentOutcome.NotFound:
                    return NotFound();
                case DeleteStudentOutcome.DeleteFailed:
                    return UnprocessableEntity();
                case DeleteStudentOutcome.Success:
                    return NoContent();
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
