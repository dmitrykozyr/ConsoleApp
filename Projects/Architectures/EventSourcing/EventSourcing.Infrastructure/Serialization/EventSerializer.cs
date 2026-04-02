using EventSourcing.Domain.Events;
using EventSourcing.Domain.Interfaces;
using System.Text.Json;

namespace EventSourcing.Infrastructure.Serialization;

// JSON-сериализация доменных событий для store
public sealed class EventSerializer
{
    // JsonOptions — camelCase, без отступов
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    // TypeByName — карта имя типа (MoneyDeposited, MoneyWithdrawn)
    private static readonly Dictionary<string, Type> TypeByName = new()
    {
        [nameof(MoneyDeposited)] = typeof(MoneyDeposited),
        [nameof(MoneyWithdrawn)] = typeof(MoneyWithdrawn)
    };

    public PersistedEvent Serialize(object @event)
    {
        var typeName = @event.GetType().Name;

        var json = JsonSerializer.Serialize(@event, @event.GetType(), JsonOptions);

        return new PersistedEvent(typeName, json);
    }

    public object Deserialize(string typeName, string json)
    {
        if (!TypeByName.TryGetValue(typeName, out var type))
        {
            throw new NotSupportedException($"Неизвестный тип события: {typeName}");
        }

        var result = JsonSerializer.Deserialize(json, type, JsonOptions)
            ?? throw new InvalidOperationException("Ошибка десериализации события");

        return result;
    }
}
