namespace NTier.DataAccess.Entities;

public class ApplicationStatus
{
    public long ApplicationStatusId { get; init; }

    public string? ApplicationStatusName { get; init; }

    public DateTime ApplicationStatusCreationDate { get; init; }

    public DateTime ApplicationStatusModifiedDate { get; init; }

    public ICollection<Application>? Applications { get; init; }
}
