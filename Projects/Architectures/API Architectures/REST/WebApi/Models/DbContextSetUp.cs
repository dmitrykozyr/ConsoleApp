using Microsoft.EntityFrameworkCore;

namespace WebApi.Models;

public class DbContextSetUp : DbContext
{
    public DbContextSetUp(DbContextOptions<DbContextSetUp> options) : base(options) { }
    public DbSet<TodoItem> TodoItems { get; set; }
}
