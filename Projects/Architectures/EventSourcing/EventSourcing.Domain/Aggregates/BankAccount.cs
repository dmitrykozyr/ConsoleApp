using EventSourcing.Domain.Events;

namespace EventSourcing.Domain.Aggregates;

//  Агрегат счета - состояние меняется через Apply и события
public class BankAccount
{
    // Список доменных событий, еще не записанных в БД
    private readonly List<object> _uncommitted = new();

    // Id, Balance - текущее состояние после применения событий
    public Guid Id { get; private set; }

    public decimal Balance { get; private set; }

    // Только чтение списка незакоммиченных событий
    public IReadOnlyList<object> UncommittedEvents => _uncommitted;

    // Создать пустой счет с заданным id (без истории)
    public static BankAccount Empty(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Введите Id аккаунта", nameof(id));
        }

        return new BankAccount { Id = id };
    }

    // Восстановление: по очереди применяет каждое событие из истории (без добавления в uncommitted)
    public void LoadFromHistory(IEnumerable<object> history)
    {
        foreach (var e in history)
        {
            Apply(e);
        }
    }

    // Меняет Balance (и при необходимости Id) по типу события
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

    // Создает MoneyDeposited, применяет к состоянию, кладет в uncommitted
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

    // Создает MoneyWithdrawn, применяет, кладет в uncommitted
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

    // Очищает uncommitted после успешной записи в store
    public void MarkCommitted() => _uncommitted.Clear();
}
