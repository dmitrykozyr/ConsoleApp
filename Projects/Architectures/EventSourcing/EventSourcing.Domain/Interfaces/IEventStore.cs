namespace EventSourcing.Domain.Interfaces;

// Уже сериализованное событие для записи в хранилище
public readonly record struct PersistedEvent(string EventType, string PayloadJson);

public interface IEventStore
{
    // Дописать список уже сериализованных событий в поток агрегата aggregateId
    Task AppendAsync(Guid aggregateId, IReadOnlyList<PersistedEvent> events, CancellationToken cancellationToken = default);

    // Почитать все события потока aggregateId в порядке записи
    Task<IReadOnlyList<PersistedEvent>> ReadStreamAsync(Guid aggregateId, CancellationToken cancellationToken = default);
}
