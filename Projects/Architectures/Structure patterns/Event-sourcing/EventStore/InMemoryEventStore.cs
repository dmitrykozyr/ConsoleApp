using System.Collections.Generic;
using System;

//  EventStore является хранилищем событий, которые были сгенерированы при изменении состояния системы

public class InMemoryEventStore : IEventStore
{
    private readonly var _events = new Dictionary<Guid, List<object>>();

    public void SaveEvents(Guid aggregateId, int expectedVersion, IEnumerable<object> events)
    {
        if (!_events.ContainsKey(aggregateId))
        {
            _events.Add(aggregateId, new List<object>());
        }

        var currentVersion = _events[aggregateId].Count - 1;

        if (currentVersion != expectedVersion)
        {
            throw new Exception($"Concurrency exception: expected version {expectedVersion}, but actual version is {currentVersion}");
        }

        _events[aggregateId].AddRange(events);
    }

    public List<object> GetEventsForAggregate(Guid aggregateId)
    {
        if (!_events.ContainsKey(aggregateId))
        {
            return new List<object>();
        }

        return _events[aggregateId];
    }
}
