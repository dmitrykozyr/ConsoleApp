namespace NTier.Logic.Services.Models.Applicant;

public class ApplicantResultSet
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public DateTime Birthday { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime SubmissionDate { get; set; }
}
