using Domain.Interfaces.Repositories;
using Domain.Interfaces.Repositories.Db;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Db;

namespace Presentation.Extensions;

public static class RepositoriesExtensions
{
    public static void AddRepositoriesExtensions(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
        serviceCollection.AddScoped<ISqlProceduresRepository, SqlProceduresRepository>();
    }
}
