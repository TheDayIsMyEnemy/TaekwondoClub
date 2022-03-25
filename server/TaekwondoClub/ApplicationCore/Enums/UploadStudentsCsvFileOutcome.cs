namespace ApplicationCore.Enums
{
    public enum UploadStudentsCsvFileOutcome
    {
        Success,
        FileNotFound,
        EmptyFile,
        MissingRequiredColumns,
        InvalidFile
    }
}
