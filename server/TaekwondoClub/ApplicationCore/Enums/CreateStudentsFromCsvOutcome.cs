namespace ApplicationCore.Enums
{
    public enum CreateStudentsFromCsvOutcome
    {
        Success,
        FileNotFound,
        EmptyFile,
        MissingRequiredColumns,
        InvalidFile
    }
}
