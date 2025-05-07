using System;

public class OrderCreatedEvent
{
    public Guid OrderId { get; set; }

    public DateTime CreatedDate { get; set; }

    public decimal TotalAmount { get; set; }
}
