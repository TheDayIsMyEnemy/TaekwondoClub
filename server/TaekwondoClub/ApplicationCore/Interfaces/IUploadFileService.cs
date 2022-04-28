using ApplicationCore.Enums;

namespace ApplicationCore.Interfaces
{
    public interface IUploadFileService
    {
        Task<(UploadStudentsCsvFileOutcome, int?)> UploadStudentsCsvFile(Stream csvStream);
    }
}
