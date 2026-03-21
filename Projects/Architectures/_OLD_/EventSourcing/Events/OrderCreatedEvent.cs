using System;

public class OrderCreatedEvent
{
    public Guid OrderId { get; init; }

    public DateTime CreatedDate { get; init; }

    public decimal TotalAmount { get; init; }
}
