namespace NTier.Logic.Services.Models.Application;

public class ApplicationResultSet
{
    public long Id { get; set; }

    public long ApplicantId { get; set; }

    public long GradeId { get; set; }

    public long StatusId { get; set; }

    public DateTime SubmissionDate { get; set; }

    public long SchoolYear { get; set; }
}
