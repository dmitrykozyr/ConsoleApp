using System;

// Определение событий и состояний системы

public class OrderCancelledEvent
{
    public Guid OrderId { get; set; }

    public DateTime CancelledDate { get; set; }
}