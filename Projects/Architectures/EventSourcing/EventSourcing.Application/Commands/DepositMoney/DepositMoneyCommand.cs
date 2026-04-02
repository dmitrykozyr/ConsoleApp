namespace EventSourcing.Application.Commands.DepositMoney;

// Входные параметры команды
public sealed record DepositMoneyCommand(Guid accountId, decimal amount);
