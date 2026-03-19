using Microsoft.EntityFrameworkCore;

namespace Education.General.Db.Transactions.OutboxPattern;

public class MyDbContext : DbContext
{
    public DbSet<OutboxMessage>? OutboxMessages { get; init; }
}
