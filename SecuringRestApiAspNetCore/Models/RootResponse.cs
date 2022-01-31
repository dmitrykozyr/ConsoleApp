namespace SecuringRestApiAspNetCore.Models
{
    public class RootResponse : Resource
    {
        public Link Rooms { get; internal set; }
        public Link Info { get; internal set; }
    }
}
