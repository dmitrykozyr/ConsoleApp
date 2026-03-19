namespace Education.General.Db.Transactions.SagaPattern.Records.Commands;

// КОМАНДА: Сага просит платежный сервис списать деньги
public record ProcessPayment(int OrderId);

