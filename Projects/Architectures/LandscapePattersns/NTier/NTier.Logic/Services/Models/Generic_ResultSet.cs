namespace NTier.Logic.Services.Models
{
    public class Generic_ResultSet<T> : StandardResultObject
    {
        public T result_set { get; set; }
    }
}
