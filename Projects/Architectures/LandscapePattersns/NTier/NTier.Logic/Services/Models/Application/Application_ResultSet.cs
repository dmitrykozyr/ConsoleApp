namespace NTier.Logic.Services.Models.Application;

public class Application_ResultSet
{
    public long id { get; set; }

    public long applicant_id { get; set; }

    public long grade_id { get; set; }

    public long status_id { get; set; }

    public DateTime submission_date { get; set; }

    public long school_year { get; set; }
}
