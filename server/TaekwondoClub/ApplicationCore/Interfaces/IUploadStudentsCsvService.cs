namespace ApplicationCore.Interfaces
{
    public interface IUploadStudentsCsvService
    {
        public Task<int> CreateStudentsFromCsvFile(Stream csvStream);
    }
}
