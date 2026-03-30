namespace EventSourcing.Application.Commands.WithdrawMoney;

public sealed record WithdrawMoneyCommand(Guid accountId, decimal amount);
