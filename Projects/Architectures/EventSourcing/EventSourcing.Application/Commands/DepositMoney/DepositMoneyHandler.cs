using EventSourcing.Domain.Aggregates;
using EventSourcing.Domain.Interfaces;

namespace EventSourcing.Application.Commands.DepositMoney;

public sealed class DepositMoneyHandler(IAggregateRepository accounts)
{
    public async Task HandleAsync(DepositMoneyCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            var account = await accounts.GetAsync(command.accountId, cancellationToken)
                ?? BankAccount.Empty(command.accountId);

            account.Deposit(command.amount);

            await accounts.SaveAsync(account, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }
}
