using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models.Requests;

namespace WebApi.Controllers
{
    public class StudentsController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentService _studentService;

        public StudentsController(IMapper mapper, IStudentRepository studentRepository, IStudentService studentService)
        {
            _mapper = mapper;
            _studentService = studentService;
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAll()
        {
            var students = await _studentRepository.GetAllStudentsWithClubMembership();

            var mappedStudents = _mapper
                .Map<IEnumerable<Student>, IEnumerable<StudentDto>>(students)
                .ToList(); ;

            return mappedStudents;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> Get(int id)
        {
            var student = await _studentRepository.GetStudentAndClubMembershipByStudentId(id);

            if (student == null)
                return NotFound();

            var studentDto = _mapper.Map<Student, StudentDto>(student);

            return studentDto;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentRequest request)
        {
            var result = await _studentService.CreateNewStudent(request.FirstName,
                request.LastName,
                request.Gender,
                request.BirthDate,
                request.PhoneNumber);

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
