namespace WebApi.Services.Interfaces
{
    public interface IStudentsCsvUploadService
    {
        public Task CreateNewStudentsFromCsvFile(MemoryStream memStream);
    }
}
