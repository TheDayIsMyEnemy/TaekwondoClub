using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class UploadStudentsController : ApiControllerBase
    {
        private readonly IUploadStudentsCsvFileService _uploadStudentsCsvFileService;

        public UploadStudentsController(IUploadStudentsCsvFileService uploadStudentsCsvFileService)
        {
            _uploadStudentsCsvFileService = uploadStudentsCsvFileService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] IFormFile students)
        {
            var (outcome, newStudentsCount) = await _uploadStudentsCsvFileService
                .UploadStudentsCsvFile(students.OpenReadStream());

            switch (outcome)
            {
                case UploadStudentsCsvFileOutcome.Success:
                    return Ok(newStudentsCount);
                case UploadStudentsCsvFileOutcome.FileNotFound:
                case UploadStudentsCsvFileOutcome.EmptyFile:
                    return BadRequest(outcome.ToString());
                case UploadStudentsCsvFileOutcome.MissingRequiredColumns:
                case UploadStudentsCsvFileOutcome.InvalidFile:
                    return UnprocessableEntity(outcome.ToString());
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
