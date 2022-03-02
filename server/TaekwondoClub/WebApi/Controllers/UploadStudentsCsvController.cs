using Microsoft.AspNetCore.Mvc;
using TaekwondoClub.Interfaces;

namespace WebApi.Controllers
{
    public class UploadStudentsCsvController : ApiControllerBase
    {
        private readonly IUploadStudentsCsvService _studentsCsvUploadService;

        public UploadStudentsCsvController(IUploadStudentsCsvService studentsCsvUploadService)
        {
            _studentsCsvUploadService = studentsCsvUploadService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] IFormFile studentsCsv)
        {
            var csvStream = new MemoryStream();
            await studentsCsv.CopyToAsync(csvStream);

            var newStudentsCount = await _studentsCsvUploadService
                .CreateNewStudentsFromCsvFile(csvStream);

            return Ok(newStudentsCount);
        }
    }
}
