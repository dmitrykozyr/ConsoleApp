using EventSourcing.Domain.Events;

namespace EventSourcing.Domain.Aggregates;

public class BankAccount
{
    private readonly List<object> _uncommitted = new();

    public Guid Id { get; private set; }

    public decimal Balance { get; private set; }

    public IReadOnlyList<object> UncommittedEvents => _uncommitted;

    public static BankAccount Empty(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Account id required.", nameof(id));
        }

        return new BankAccount { Id = id };
    }

    public void LoadFromHistory(IEnumerable<object> history)
    {
        foreach (var e in history)
        {
            Apply(e);
        }
    }

    public void Apply(object @event)
    {
        switch (@event)
        {
            case MoneyDeposited d:
                if (Id == Guid.Empty) Id = d.accountId;
                Balance += d.amount;
                break;
            case MoneyWithdrawn w:
                if (Id == Guid.Empty) Id = w.accountId;
                Balance -= w.amount;
                break;
        }
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        var e = new MoneyDeposited(Id, amount);
        Apply(e);
        _uncommitted.Add(e);
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        if (amount > Balance)
        {
            throw new InvalidOperationException("Insufficient funds.");
        }

        var e = new MoneyWithdrawn(Id, amount);

        Apply(e);

        _uncommitted.Add(e);
    }

    public void MarkCommitted() => _uncommitted.Clear();
}
