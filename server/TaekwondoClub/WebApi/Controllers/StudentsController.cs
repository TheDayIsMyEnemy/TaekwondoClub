using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;

namespace WebApi.Controllers
{
    public class StudentsController : ApiControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository)
            => _studentRepository = studentRepository;

        [HttpGet]
        public async Task<IEnumerable<Student>> GetAll()
            => await _studentRepository.ListAllAsync();
    }
}
