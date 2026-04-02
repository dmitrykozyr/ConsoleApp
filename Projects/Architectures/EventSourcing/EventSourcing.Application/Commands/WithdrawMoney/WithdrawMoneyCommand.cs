namespace EventSourcing.Application.Commands.WithdrawMoney;

// Входные параметры команды
public sealed record WithdrawMoneyCommand(Guid accountId, decimal amount);
