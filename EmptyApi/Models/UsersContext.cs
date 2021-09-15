using Microsoft.EntityFrameworkCore;

namespace EmptyApi.Models
{
    public sealed class UsersContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
