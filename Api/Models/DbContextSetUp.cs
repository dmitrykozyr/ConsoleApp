using Microsoft.EntityFrameworkCore;

namespace Api.Models
{
    public class DbContextSetUp : DbContext
    {
        public DbContextSetUp(DbContextOptions<DbContextSetUp> options)
            : base(options) { }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
