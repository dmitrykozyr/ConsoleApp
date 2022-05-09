using NTier.UI.Models.Applicant;

namespace NTier.UI.Models.Application
{
    public class Application_Pass_Object
    {
        public int applicant_id { get; set; }
        public int grade_id { get; set; }
        public int school_year { get; set; }
        public Applicant_Pass_Object applicant { get; set; }
    }
}
