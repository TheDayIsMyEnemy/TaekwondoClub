using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Interfaces;

namespace WebApi.Controllers
{
    public class UploadStudentsController : ApiControllerBase
    {
        private readonly IUploadStudentsCsvService _studentsCsvUploadService;

        public UploadStudentsController(IUploadStudentsCsvService studentsCsvUploadService)
        {
            _studentsCsvUploadService = studentsCsvUploadService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] IFormFile studentsCsv)
        {
            var newStudentsCount = await _studentsCsvUploadService
                .CreateStudentsFromCsvFile(studentsCsv.OpenReadStream());

            return Ok(newStudentsCount);
        }
    }
}
