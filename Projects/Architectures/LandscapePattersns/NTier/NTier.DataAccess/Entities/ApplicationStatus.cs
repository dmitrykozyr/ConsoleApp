namespace NTier.DataAccess.Entities;

public class ApplicationStatus
{
    public long ApplicationStatusId { get; set; }

    public string? ApplicationStatusName { get; set; }

    public DateTime ApplicationStatusCreationDate { get; set; }

    public DateTime ApplicationStatusModifiedDate { get; set; }

    public ICollection<Application>? Applications { get; set; }
}
