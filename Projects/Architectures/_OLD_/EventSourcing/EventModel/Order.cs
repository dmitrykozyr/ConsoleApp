using System.Collections.Generic;
using System;

public class Order
{
    private var _events = new List<object>();
    private OrderStatus _status;

    public void CreateOrder(decimal totalAmount)
    {
        var orderCreatedEvent = new OrderCreatedEvent
        {
            OrderId     = Guid.NewGuid(),
            CreatedDate = DateTime.UtcNow,
            TotalAmount = totalAmount
        };

        Apply(orderCreatedEvent);
    }

    public void CancelOrder()
    {
        if (_status == OrderStatus.Processing || _status == OrderStatus.Shipped)
        {
            var orderCancelledEvent = new OrderCancelledEvent
            {
                OrderId = Id,
                CancelledDate = DateTime.UtcNow
            };

            Apply(orderCancelledEvent);
        }
    }

    private void Apply(object _event)
    {
        switch (_event)
        {
            case OrderCreatedEvent orderCreated:
                Id          = orderCreated.OrderId;
                CreatedDate = orderCreated.CreatedDate;
                TotalAmount = orderCreated.TotalAmount;
                _status     = OrderStatus.New;
                break;
            case OrderCancelledEvent orderCancelled:
                _status = OrderStatus.Cancelled;
                break;
            default:
                throw new NotSupportedException($"Event type '{_event.GetType().Name}' is not supported.");
        }

        _events.Add(_event);
    }
}
