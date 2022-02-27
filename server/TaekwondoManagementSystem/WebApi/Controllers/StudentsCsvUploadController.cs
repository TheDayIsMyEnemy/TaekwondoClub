using Microsoft.AspNetCore.Mvc;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsCsvUploadController : ControllerBase
    {
        private readonly IStudentsCsvUploadService _studentsCsvUploadService;

        public StudentsCsvUploadController(IStudentsCsvUploadService studentsCsvUploadService)
        {
            _studentsCsvUploadService = studentsCsvUploadService;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromForm] IFormFile studentsFile)
        {
            // read file
            var memoryStream = new MemoryStream();
            await studentsFile.CopyToAsync(memoryStream);

            await _studentsCsvUploadService.CreateNewStudentsFromCsvFile(memoryStream);

            // extract students
            // check students if exists
            // save students to db

            return Ok();
        }
    }
}
