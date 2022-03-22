using ApplicationCore.Enums;

namespace ApplicationCore.Interfaces
{
    public interface IUploadStudentsCsvService
    {
        public Task<(CreateStudentsFromCsvOutcome, int?)> CreateStudentsFromCsvFile(Stream csvStream);
    }
}
