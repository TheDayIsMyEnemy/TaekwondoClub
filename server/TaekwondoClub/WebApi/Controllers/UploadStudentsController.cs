using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class UploadStudentsController : ApiControllerBase
    {
        private readonly IUploadFileService _uploadFileService;

        public UploadStudentsController(IUploadFileService uploadFileService)
        {
            _uploadFileService = uploadFileService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] IFormFile students)
        {
            var (outcome, newStudentsCount) = await _uploadFileService
                .UploadStudentsCsvFile(students.OpenReadStream());

            switch (outcome)
            {
                case UploadStudentsCsvFileOutcome.Success:
                    return Ok(newStudentsCount);
                case UploadStudentsCsvFileOutcome.FileNotFound:
                case UploadStudentsCsvFileOutcome.EmptyFile:
                case UploadStudentsCsvFileOutcome.MissingRequiredColumns:
                case UploadStudentsCsvFileOutcome.InvalidFile:
                    return UnprocessableEntity();
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
