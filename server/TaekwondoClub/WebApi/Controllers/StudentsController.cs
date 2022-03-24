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

    }
}
