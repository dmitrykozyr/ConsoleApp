namespace Education.General.Db.Transactions.SagaPattern.Records.Events;

// СОБЫТИЕ: Кто-то нажал кнопку "Купить"
public record OrderSubmitted(Guid CorrelationId, int OrderId, decimal Amount);
