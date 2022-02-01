using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SecuringRestApiAspNetCore.Models;
using System;

namespace SecuringRestApiAspNetCore.DatabaseContext
{
    // Authentication and authorization - вместо DbContext используем IdentityDbContext
    public class HotelApiDbContext : IdentityDbContext<UserEntity, UserRoleEntity, Guid>
    {
        public HotelApiDbContext(DbContextOptions options): base(options) { }

        public DbSet<RoomEntity> Rooms { get; set; }
    }
}
