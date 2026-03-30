namespace EventSourcing.Domain.Events;

public record MoneyWithdrawn(Guid accountId, decimal amount);
