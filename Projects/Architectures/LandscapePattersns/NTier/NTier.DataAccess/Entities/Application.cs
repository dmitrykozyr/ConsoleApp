namespace NTier.DataAccess.Entities;

public class Application
{
    public long ApplicationId { get; set; }

    public long ApplicantId { get; set; }

    public long GradeId { get; set; }

    public long ApplicationStatusId { get; set; }

    public DateTime ApplicationCreationDate { get; set; }

    public DateTime ApplicationModifiedDate { get; set; }

    public Int32 SchoolYear { get; set; }

    public Applicant? Applicant { get; set; }

    public Grade? Grade { get; set; }

    public ApplicationStatus? ApplicationStatus { get; set; }
}
