using Newtonsoft.Json;

namespace SecuringRestApiAspNetCore.Models
{
    public abstract class Resource : Link
    {
        //[JsonProperty(Order = -2)]
        //public string Href { get; set; }

        [JsonIgnore]
        public Link Self { get; set; }
    }
}
