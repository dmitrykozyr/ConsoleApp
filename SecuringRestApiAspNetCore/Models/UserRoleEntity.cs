using Microsoft.AspNetCore.Identity;
using System;

namespace SecuringRestApiAspNetCore.Models
{
    // Authentication and authorization
    public class UserRoleEntity : IdentityRole<Guid>
    {
        public UserRoleEntity() : base() { }
        public UserRoleEntity(string roleName) : base(roleName) { }
    }
}
