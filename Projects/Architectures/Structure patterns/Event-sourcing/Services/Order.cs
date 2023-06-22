using System.Collections.Generic;

// Механизм восстановления состояния

public class Order
{
    public void LoadFromHistory(IEnumerable<object> events)
    {
        foreach (var @event in events)
        {
            Apply(@event);
        }
    }

    public IEnumerable<object> GetUncommittedEvents()
    {
        return _events;
    }
}
