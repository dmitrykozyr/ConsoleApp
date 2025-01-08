using Microsoft.EntityFrameworkCore;
using MVVM.DTOs;

namespace MVVM.DbContexts;

public class ReservoomDbContext : DbContext
{
    public ReservoomDbContext(DbContextOptions options) : base(options) { }

    public DbSet<ReservationDTO> Reservations { get; set; }
}
