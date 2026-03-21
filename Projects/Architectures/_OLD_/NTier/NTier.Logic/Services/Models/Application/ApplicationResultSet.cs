namespace NTier.Logic.Services.Models.Application;

public class ApplicationResultSet
{
    public long Id { get; init; }

    public long ApplicantId { get; init; }

    public long GradeId { get; init; }

    public long StatusId { get; init; }

    public DateTime SubmissionDate { get; init; }

    public long SchoolYear { get; init; }
}
