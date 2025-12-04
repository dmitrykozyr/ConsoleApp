using Domain.Interfaces.Repositories;
using Domain.Interfaces.Repositories.DB;
using Infrastructure.Repositories;
using Infrastructure.Repositories.DB;

namespace Presentation.Extensions;

public static class RepositoriesExtensions
{
    public static void AddRepositoriesExtensions(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
        serviceCollection.AddScoped<ISqlProceduresRepository, SqlProceduresRepository>();
    }
}
