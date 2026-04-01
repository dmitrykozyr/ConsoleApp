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
            throw new ArgumentException("Введите Id аккаунта", nameof(id));
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
            case MoneyDeposited deposited:
                {
                    if (Id == Guid.Empty)
                    {
                        Id = deposited.accountId;
                    }

                    Balance += deposited.amount;
                    break;
                }
            case MoneyWithdrawn withdrawn:
                {
                    if (Id == Guid.Empty)
                    {
                        Id = withdrawn.accountId;
                    }

                    Balance -= withdrawn.amount;
                    break;
                }
            default:
                {
                    string? eventName = @event?.GetType().FullName;

                    throw new InvalidOperationException($"Неизвестное событие: {eventName}");
                }
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
            throw new InvalidOperationException("Недостаток средств");
        }

        var e = new MoneyWithdrawn(Id, amount);

        Apply(e);

        _uncommitted.Add(e);
    }

    public void MarkCommitted() => _uncommitted.Clear();
}
