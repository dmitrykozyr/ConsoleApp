using System.Collections.Generic;
using System;

public interface IEventStore
{
    void SaveEvents(Guid aggregateId, int expectedVersion, IEnumerable<object> events);
    List<object> GetEventsForAggregate(Guid aggregateId);
}