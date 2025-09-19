namespace NTier.Logic.Services.Models.Applicant;

public class ApplicantResultSet
{
    public long Id { get; init; }

    public string? Name { get; init; }

    public string? Surname { get; init; }

    public DateTime Birthday { get; init; }

    public string? Email { get; init; }

    public string? PhoneNumber { get; init; }

    public DateTime SubmissionDate { get; init; }
}
