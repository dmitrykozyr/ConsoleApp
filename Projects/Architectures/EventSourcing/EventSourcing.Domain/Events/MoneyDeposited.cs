namespace EventSourcing.Domain.Events;

public record MoneyDeposited(Guid accountId, decimal amount);
