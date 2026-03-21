namespace NTier.DataAccess.Entities;

public class Applicant
{
    public long ApplicantId { get; init; }

    public string? ApplicantName { get; init; }

    public string? ApplicantSurname { get; init; }

    public DateTime ApplicantBirthDate { get; init; }

    public string? ContactEmail { get; init; }

    public string? ContactNumber { get; init; }

    public DateTime ApplicantCreationDate { get; init; }

    public DateTime ApplicantModifiedDate { get; init; }

    public ICollection<Application>? Applications { get; init; }
}
