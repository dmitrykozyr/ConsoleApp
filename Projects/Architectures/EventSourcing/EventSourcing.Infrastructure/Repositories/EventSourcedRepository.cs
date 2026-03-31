using EventSourcing.Domain.Aggregates;
using EventSourcing.Domain.Interfaces;
using EventSourcing.Infrastructure.EventStore;
using EventSourcing.Infrastructure.Serialization;

namespace EventSourcing.Infrastructure.Repositories;

public sealed class EventSourcedRepository(PostgresEventStore store, EventSerializer serializer) : IAggregateRepository
{
    public async Task<BankAccount?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var stream = await store.ReadStreamAsync(id, cancellationToken);

        if (stream.Count == 0)
        {
            return null;
        }

        var history = stream.Select(e => serializer.Deserialize(e.EventType, e.PayloadJson)).ToList();

        var account = BankAccount.Empty(id);

        account.LoadFromHistory(history);

        return account;
    }

    public async Task SaveAsync(BankAccount account, CancellationToken cancellationToken = default)
    {
        var pending = account.UncommittedEvents.ToList();

        if (pending.Count == 0)
        {
            return;
        }

        var envelopes = pending.Select(serializer.Serialize).ToList();

        await store.AppendAsync(account.Id, envelopes, cancellationToken);

        account.MarkCommitted();
    }
}
