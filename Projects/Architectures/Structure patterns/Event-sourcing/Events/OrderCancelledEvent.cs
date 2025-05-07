using System;

public class OrderCancelledEvent
{
    public Guid OrderId { get; set; }

    public DateTime CancelledDate { get; set; }
}