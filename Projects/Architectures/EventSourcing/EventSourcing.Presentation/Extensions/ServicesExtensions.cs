using EventSourcing.Application.Commands.DepositMoney;
using EventSourcing.Application.Commands.WithdrawMoney;
using EventSourcing.Application.Queries.GetBalance;
using EventSourcing.Domain.Interfaces;
using EventSourcing.Infrastructure.EventStore;
using EventSourcing.Infrastructure.Repositories;
using EventSourcing.Infrastructure.Serialization;

namespace EventSourcing.Presentation.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddServicesExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Стркоа подключения 'DefaultConnection' не найдена");

        services.AddSingleton<EventSerializer>();
        services.AddSingleton<IEventStore>(_ => new PostgresEventStore(connectionString));
        services.AddScoped(sp => (PostgresEventStore)sp.GetRequiredService<IEventStore>());
        services.AddScoped<IAggregateRepository, EventSourcedRepository>();
        services.AddScoped<DepositMoneyHandler>();
        services.AddScoped<WithdrawMoneyHandler>();
        services.AddScoped<GetBalanceHandler>();

        return services;
    }
}
