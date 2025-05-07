using NTier.UI.Models.Applicant;

namespace NTier.UI.Models.Application;

public class ApplicationPassObject
{
    public int ApplicantId { get; set; }

    public int GradeId { get; set; }

    public int SchoolYear { get; set; }

    public ApplicantPassObject? Applicant { get; set; }
}
