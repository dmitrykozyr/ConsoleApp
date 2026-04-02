namespace EventSourcing.Domain.Events;

// Событие «деньги зачислены»
public record MoneyDeposited(Guid accountId, decimal amount);
