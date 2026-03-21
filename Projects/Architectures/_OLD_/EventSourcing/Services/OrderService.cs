using System;

public class OrderService
{
    private readonly IEventStore _eventStore;

    public OrderService(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public void CreateOrder(decimal totalAmount)
    {
        var order = new Order();

        order.CreateOrder(totalAmount);

        _eventStore.SaveEvents(order.Id, -1, order.GetUncommittedEvents());
    }

    public void CancelOrder(Guid orderId)
    {
        var order = LoadOrder(orderId);

        order.CancelOrder();

        _eventStore.SaveEvents(order.Id, order.Version, order.GetUncommittedEvents());
    }

    private Order LoadOrder(Guid orderId)
    {
        var events = _eventStore.GetEventsForAggregate(orderId);

        if (events.Count == 0)
        {
            throw new Exception($"Order with id {orderId} not found.");
        }

        var order = new Order();

        order.LoadFromHistory(events);

        return order;
    }
}
