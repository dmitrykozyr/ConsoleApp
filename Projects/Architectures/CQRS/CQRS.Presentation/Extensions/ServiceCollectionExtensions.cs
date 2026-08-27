using CQRS.Application;
using CQRS.Infrastructure;

namespace CQRS.Presentation.Extensions;

/*
    Что не нужно класть в ServiceCollectionExtensions:
    - регистрацию репозиториев и DbContext — это Infrastructure.DependencyInjection
    - MediatR и валидаторы — это Application.DependencyInjection
    - бизнес-логику и handlers — они регистрируются автоматически через AddMediatR(RegisterServicesFromAssembly)
*/

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddApplication()
            .AddInfrastructure(configuration);

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}
