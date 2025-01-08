using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NTier.DataAccess.DataContext;

// Класс нужен для создания миграций
public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        var settings = new AppConfiguration();
        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        optionsBuilder.UseSqlServer(settings.SqlConnectionString);
        return new DatabaseContext(optionsBuilder.Options);
    }
}
