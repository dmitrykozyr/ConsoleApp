namespace SecuringRestApiAspNetCore.Models
{
    public class Collection<T> : Resource
    {
        public T[] value { get; set; }
    }
}
