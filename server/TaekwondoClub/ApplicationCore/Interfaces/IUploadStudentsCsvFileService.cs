using ApplicationCore.Enums;

namespace ApplicationCore.Interfaces
{
    public interface IUploadStudentsCsvFileService
    {
        public Task<(UploadStudentsCsvFile, int?)> UploadStudentsCsvFile(Stream csvStream);
    }
}
