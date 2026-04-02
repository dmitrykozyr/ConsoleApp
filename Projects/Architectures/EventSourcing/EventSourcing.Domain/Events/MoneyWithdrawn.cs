namespace EventSourcing.Domain.Events;

// Событие «деньги списаны»
public record MoneyWithdrawn(Guid accountId, decimal amount);
