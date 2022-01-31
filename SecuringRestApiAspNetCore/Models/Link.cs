using Newtonsoft.Json;
using System.ComponentModel;

namespace SecuringRestApiAspNetCore.Models
{
    public class Link
    {
        public const string GetMethod = "GET";

        public static Link To(string routeName, object routeValues = null) => new Link
        {
            RouteName = routeName,
            RouteValues = routeValues,
            Method = GetMethod,
            Relations = null
        };

        public static Link ToCollection(string routeName, object routeValues = null) => new Link
        {
            RouteName = routeName,
            RouteValues = routeValues,
            Method = GetMethod,
            Relations = new[] { "collection" }
        };

        [JsonProperty(Order = -4)]
        public string Href { get; set; }

        [JsonProperty(Order = -3,
                      PropertyName = "rel",
                      NullValueHandling = NullValueHandling.Ignore)]
        public string[] Relations { get; set; }

        [JsonProperty(Order = -2,
                      DefaultValueHandling = DefaultValueHandling.Ignore,
                      NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(GetMethod)]
        public string Method { get; set; }


        [JsonIgnore]
        // Stores route name before being rewritten by LinkRewrittingFilter
        public string RouteName { get; set; }

        [JsonIgnore]
        // Stores route parameters before being rewritten by LinkRewrittingFilter
        public object RouteValues { get; set; }
    }
}
