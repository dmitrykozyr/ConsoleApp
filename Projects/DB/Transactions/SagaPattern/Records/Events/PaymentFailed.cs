namespace Education.General.Db.Transactions.SagaPattern.Records.Events;

// СОБЫТИЕ: Ответ от платежного сервиса (Ошибка)
public record PaymentFailed(Guid CorrelationId, string Reason);
