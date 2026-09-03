namespace Education.General.Db.Transactions.SagaPattern.Records.Events;

// СОБЫТИЕ: Ответ от платежного сервиса (Успех)
public record PaymentConfirmed(Guid CorrelationId);
