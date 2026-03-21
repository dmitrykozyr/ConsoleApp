using NTier.UI.Models.Applicant;

namespace NTier.UI.Models.Application;

public class ApplicationPassObject
{
    public int ApplicantId { get; init; }

    public int GradeId { get; init; }

    public int SchoolYear { get; init; }

    public ApplicantPassObject? Applicant { get; init; }
}
