namespace EventSourcing.Domain.Interfaces;

// Уже сериализованное событие для записи в хранилище
public readonly record struct PersistedEvent(string EventType, string PayloadJson);

public interface IEventStore
{
    Task AppendAsync(Guid aggregateId, IReadOnlyList<PersistedEvent> events, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<PersistedEvent>> ReadStreamAsync(Guid aggregateId, CancellationToken cancellationToken = default);
}
