using Domain.Interfaces.Db;
using Domain.Interfaces.Db.DbContext;
using Infrastructure.Repositories;

namespace Presentation.Extensions;

public static class DbContextExtensions
{
    public static IServiceCollection AddDbExtensions(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        //!
        //serviceCollection.AddDbContext<ApplicationDbContext>(options =>
        //    options
        //        .UseNpgsql(configuration.GetConnectionString("Database"))
        //        .UseSnakeCaseNamingConvention());

        serviceCollection.AddScoped<IApplicationDbContext>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());

        serviceCollection.AddScoped<IUnitOfWork>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());

        return serviceCollection;
    }
}
