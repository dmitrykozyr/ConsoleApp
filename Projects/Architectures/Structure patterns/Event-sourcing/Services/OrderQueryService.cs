using System.Collections.Generic;

// Запросная часть

public class OrderQueryService
{
    private readonly IEventStore _eventStore;

    public OrderQueryService(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public List<OrderDto> GetOrders()
    {
        var orders = new List<OrderDto>();

        foreach (var events in _eventStore.GetAllEvents())
        {
            var order = new Order();
            order.LoadFromHistory(events);

            orders.Add(new OrderDto
            {
                Id = order.Id,
                CreatedDate = order.CreatedDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status
            });
        }

        return orders;
    }
}
