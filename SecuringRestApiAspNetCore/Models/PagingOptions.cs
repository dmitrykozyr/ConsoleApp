using System.ComponentModel.DataAnnotations;

namespace SecuringRestApiAspNetCore.Models
{
    // Pagination
    public class PagingOptions
    {
        [Range(1, 99999, ErrorMessage = "Offset must be > 0")]
        public int? Offset { get; set; }

        [Range(1, 100, ErrorMessage = "Limit must be > 0 and < 100")]
        public int? Limit { get; set; }
    }
}
