using EventSourcing.Domain.Aggregates;

namespace EventSourcing.Domain.Interfaces;

public interface IAggregateRepository
{
    Task<BankAccount?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task SaveAsync(BankAccount account, CancellationToken cancellationToken = default);
}
