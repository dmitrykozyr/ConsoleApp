namespace NTier.DataAccess.Entities;

public class Applicant
{
    public long ApplicantId { get; set; }

    public string? ApplicantName { get; set; }

    public string? ApplicantSurname { get; set; }

    public DateTime ApplicantBirthDate { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactNumber { get; set; }

    public DateTime ApplicantCreationDate { get; set; }

    public DateTime ApplicantModifiedDate { get; set; }

    public ICollection<Application>? Applications { get; set; }
}
