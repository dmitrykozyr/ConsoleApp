using EventSourcing.Domain.Interfaces;

namespace EventSourcing.Application.Commands.WithdrawMoney;

public sealed class WithdrawMoneyHandler(IAggregateRepository accounts)
{
    public async Task HandleAsync(WithdrawMoneyCommand command, CancellationToken cancellationToken = default)
    {
        var account = await accounts.GetAsync(command.accountId, cancellationToken)
            ?? throw new InvalidOperationException("Account not found.");

        account.Withdraw(command.amount);

        await accounts.SaveAsync(account, cancellationToken);
    }
}
