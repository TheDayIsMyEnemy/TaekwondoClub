using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Requests;

namespace WebApi.Controllers
{
    public class StudentsController : ApiControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentService _studentService;

        public StudentsController(IStudentRepository studentRepository, IStudentService studentService)
        {
            _studentService = studentService;
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAll()
        {
            var students = await _studentRepository.GetAllStudentsWithMembership();

            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            var student = await _studentRepository.GetStudentAndMembershipByStudentId(id);

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] CreateStudentRequest req)
        {
            var result = await _studentService.CreateNewStudentWithMembership(req.FirstName,
                req.LastName,
                req.Gender,
                req.BirthDate,
                req.PhoneNumber,
                req.MembershipPeriod);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);

            if (student == null)
                return NotFound();

            try
            {
                await _studentRepository.RemoveAsync(student);
            }
            catch (Exception)
            {
                return UnprocessableEntity();
            }

            return NoContent();
        }
    }
}
