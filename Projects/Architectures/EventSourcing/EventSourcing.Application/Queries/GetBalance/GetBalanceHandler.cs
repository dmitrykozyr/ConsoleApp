using EventSourcing.Domain.Interfaces;

namespace EventSourcing.Application.Queries.GetBalance;

public sealed class GetBalanceHandler(IAggregateRepository accounts)
{
    public async Task<decimal?> HandleAsync(GetBalanceQuery query, CancellationToken cancellationToken = default)
    {
        var account = await accounts.GetAsync(query.accountId, cancellationToken);

        return account?.Balance;
    }
}
