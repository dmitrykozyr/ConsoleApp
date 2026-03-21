using System;

public class OrderCancelledEvent
{
    public Guid OrderId { get; init; }

    public DateTime CancelledDate { get; init; }
}