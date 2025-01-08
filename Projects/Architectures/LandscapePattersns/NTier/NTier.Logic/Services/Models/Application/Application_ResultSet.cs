namespace NTier.Logic.Services.Models.Application;

public class Application_ResultSet
{
    public Int64 id { get; set; }
    public Int64 applicant_id { get; set; }
    public Int64 grade_id { get; set; }
    public Int64 status_id { get; set; }
    public DateTime submission_date { get; set; }
    public Int64 school_year { get; set; }
}
