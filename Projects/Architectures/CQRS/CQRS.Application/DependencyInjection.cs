using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace CQRS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        // При старте приложения вызываем метод регистрации MediatR
        // В этот момент MediatR сканирует сборку и ищет классы,
        // реализующие интерфейс IRequestHandler<TRequest, TResponse>
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
