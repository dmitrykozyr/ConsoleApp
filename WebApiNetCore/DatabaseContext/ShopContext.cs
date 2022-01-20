using Microsoft.EntityFrameworkCore;
using WebApiNetCore.Models;

namespace WebApiNetCore.DatabaseContext
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasMany(z => z.Products).WithOne(c => c.Category).HasForeignKey(x => x.CategoryId);
            modelBuilder.Entity<Order>().HasMany(z => z.Products);
            modelBuilder.Entity<Order>().HasOne(z => z.User);
            modelBuilder.Entity<User>().HasMany(z => z.Orders).WithOne(c => c.User).HasForeignKey(x => x.UserId);

            modelBuilder.Seed();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
