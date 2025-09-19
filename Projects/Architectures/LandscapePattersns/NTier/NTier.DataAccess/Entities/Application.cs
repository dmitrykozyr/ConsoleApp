namespace NTier.DataAccess.Entities;

public class Application
{
    public long ApplicationId { get; init; }

    public long ApplicantId { get; init; }

    public long GradeId { get; init; }

    public long ApplicationStatusId { get; init; }

    public DateTime ApplicationCreationDate { get; init; }

    public DateTime ApplicationModifiedDate { get; init; }

    public int SchoolYear { get; init; }

    public Applicant? Applicant { get; set; }

    public Grade? Grade { get; init; }

    public ApplicationStatus? ApplicationStatus { get; init; }
}
