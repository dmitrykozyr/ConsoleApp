using Domain.Interfaces.Repositories;
using Infrastructure.Repositories;

namespace Presentation.Extensions;

public static class RepositoriesExtensions
{
    public static void AddRepositoriesExtensions(this IServiceCollection serviceCollection, WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ISqlProceduresRepository, SqlProceduresRepository>();
    }
}
