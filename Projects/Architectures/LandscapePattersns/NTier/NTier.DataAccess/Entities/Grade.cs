namespace NTier.DataAccess.Entities
{
    public class Grade
    {
        public Int64 Grade_ID { get; set; }
        public string Grade_Name { get; set; }
        public Int32 Grade_Number { get; set; }
        public Int32 Grade_Capacity { get; set; }
        public DateTime Grade_CreationDate { get; set; }
        public DateTime Grade_ModifiedDate { get; set; }

        public ICollection<Application> Applications { get; set; }
    }
}
