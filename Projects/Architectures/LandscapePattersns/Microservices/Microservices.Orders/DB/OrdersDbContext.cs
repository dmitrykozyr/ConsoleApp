using Microsoft.EntityFrameworkCore;

namespace Microservices.Orders.DB
{
    public class OrdersDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public OrdersDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
