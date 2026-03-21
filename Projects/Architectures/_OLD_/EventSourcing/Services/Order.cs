using System.Collections.Generic;

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
