using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using AutoMapper;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class StudentsController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<StudentDto>> GetAll()
        {
            var students = await _studentRepository
                .GetAllStudentsWithClubMembership();

            var mappedStudents = _mapper
                .Map<IEnumerable<Student>, IEnumerable<StudentDto>>(students)
                .ToList(); ;

            return mappedStudents;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var student = await _studentRepository.GetStudentAndClubMembershipByStudentId(id);

            if (student == null)
                return NotFound();

            var mappedStudent = _mapper.Map<Student, StudentDto>(student);

            return Ok(mappedStudent);
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
