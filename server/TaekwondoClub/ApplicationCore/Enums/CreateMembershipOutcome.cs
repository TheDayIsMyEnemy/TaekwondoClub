namespace ApplicationCore.Enums
{
    public enum CreateMembershipOutcome
    {
        Success,
        StudentDoesNotExist,
        StudentMembershipAlreadyExists,
        InvalidMembershipPeriod,
        InsertFailed
    }
}
