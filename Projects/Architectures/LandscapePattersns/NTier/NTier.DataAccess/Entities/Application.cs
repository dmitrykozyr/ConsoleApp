namespace NTier.DataAccess.Entities;

public class Application
{
    public Int64 Application_ID { get; set; }

    public Int64 Applicant_ID { get; set; }

    public Int64 Grade_ID { get; set; }

    public Int64 ApplicationStatus_ID { get; set; }

    public DateTime Application_CreationDate { get; set; }

    public DateTime Application_ModifiedDate { get; set; }

    public Int32 SchoolYear { get; set; }

    public Applicant? Applicant { get; set; }

    public Grade? Grade { get; set; }

    public ApplicationStatus? ApplicationStatus { get; set; }
}
