using DDD.Infrastructure.Interfaces.Db.Dapper;
using DDD.Infrastructure.Repositories.Dapper.Custom;
using DDD.Infrastructure.Repositories.EF.DB;
using Domain.Interfaces;
using Infrastructure.Interfaces.Db;
using Infrastructure.Repositories;

namespace Presentation.Extensions;

public static class RepositoriesExtensions
{
    public static void AddRepositoriesExtensions(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IDapperRepository, DapperRepository>();
        serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
        serviceCollection.AddScoped<ISqlProceduresRepository, SqlProceduresRepository>();        
    }
}
