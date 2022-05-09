namespace NTier.Logic.Services.Models.Applicant
{
    public class Applicant_ResultSet
    {
        public Int64 id { get; set; }
        public String name { get; set; }
        public String surname { get; set; }
        public DateTime birthday { get; set; }
        public String email { get; set; }
        public String phone_number { get; set; }
        public DateTime submission_date { get; set; }
    }
}
