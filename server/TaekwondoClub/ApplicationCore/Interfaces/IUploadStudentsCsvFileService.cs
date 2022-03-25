using ApplicationCore.Enums;

namespace ApplicationCore.Interfaces
{
    public interface IUploadStudentsCsvFileService
    {
        public Task<(UploadStudentsCsvFileOutcome, int?)> UploadStudentsCsvFile(Stream csvStream);
    }
}
