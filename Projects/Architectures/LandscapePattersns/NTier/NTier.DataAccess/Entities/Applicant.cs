namespace NTier.DataAccess.Entities
{
    public class Applicant
    {
        public Int64 Applicant_ID { get; set; }
        public string Applicant_Name { get; set; }
        public string Applicant_Surname { get; set; }
        public DateTime Applicant_BirthDate { get; set; }
        public string Contact_Email { get; set; }
        public string Contact_Number { get; set; }
        public DateTime Applicant_CreationDate { get; set; }
        public DateTime Applicant_ModifiedDate { get; set; }

        public ICollection<Application> Applications { get; set; }
    }
}
