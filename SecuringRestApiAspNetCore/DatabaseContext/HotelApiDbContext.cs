using Microsoft.EntityFrameworkCore;
using SecuringRestApiAspNetCore.Models;

namespace SecuringRestApiAspNetCore.DatabaseContext
{
    public class HotelApiDbContext : DbContext
    {
        public HotelApiDbContext(DbContextOptions options): base(options) { }

        public DbSet<RoomEntity> Rooms { get; set; }
    }
}
