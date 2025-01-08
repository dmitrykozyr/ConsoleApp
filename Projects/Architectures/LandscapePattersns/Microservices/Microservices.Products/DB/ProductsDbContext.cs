using Microsoft.EntityFrameworkCore;

namespace Microservices.Products.DB;

public class ProductsDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public ProductsDbContext(DbContextOptions options) : base(options)
    {
    }
}
