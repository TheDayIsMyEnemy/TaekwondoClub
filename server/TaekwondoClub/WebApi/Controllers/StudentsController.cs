using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class StudentsController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IMapper mapper, IStudentRepository studentRepository)
        {
            _mapper = mapper;
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
