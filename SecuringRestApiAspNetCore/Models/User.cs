using System;

namespace SecuringRestApiAspNetCore.Models
{
    // Authentication and authorization
    public class User : Resource
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
