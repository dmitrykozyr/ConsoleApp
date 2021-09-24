using Microsoft.EntityFrameworkCore;

namespace EmptyApi.Models
{
    public sealed class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
