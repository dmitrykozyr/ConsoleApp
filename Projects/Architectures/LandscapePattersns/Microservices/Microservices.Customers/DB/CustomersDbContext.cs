using Microsoft.EntityFrameworkCore;

namespace Microservices.Customers.DB
{
    public class CustomersDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public CustomersDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
