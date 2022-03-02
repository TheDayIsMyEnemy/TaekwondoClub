namespace TaekwondoClub.Interfaces
{
    public interface IUploadStudentsCsvService
    {
        public Task<int> CreateNewStudentsFromCsvFile(MemoryStream csvStream);
    }
}
