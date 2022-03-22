using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Interfaces;
using ApplicationCore.Enums;

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
            var (outcome, newStudentsCount) = await _studentsCsvUploadService
                .CreateStudentsFromCsvFile(studentsCsv.OpenReadStream());

            switch (outcome)
            {
                case CreateStudentsFromCsvOutcome.Success:
                    return Ok(newStudentsCount);
                case CreateStudentsFromCsvOutcome.FileNotFound:
                case CreateStudentsFromCsvOutcome.EmptyFile:
                    return BadRequest();
                case CreateStudentsFromCsvOutcome.MissingRequiredColumns:
                case CreateStudentsFromCsvOutcome.InvalidFile:            
                default:
                    return UnprocessableEntity();
            }
        }
    }
}
