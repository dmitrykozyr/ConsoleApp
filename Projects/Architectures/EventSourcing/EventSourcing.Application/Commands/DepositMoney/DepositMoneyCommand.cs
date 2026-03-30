namespace EventSourcing.Application.Commands.DepositMoney;

public sealed record DepositMoneyCommand(Guid accountId, decimal amount);
