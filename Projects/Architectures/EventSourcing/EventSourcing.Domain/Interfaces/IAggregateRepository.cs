using EventSourcing.Domain.Aggregates;

namespace EventSourcing.Domain.Interfaces;

// Загрузить/сохранить банковский агрегат поверх event store
public interface IAggregateRepository
{
    // Вернуть восстановленный BankAccount по id или null, если потока нет
    Task<BankAccount?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    // Сохранить новые (незакоммиченные) события агрегата
    Task SaveAsync(BankAccount account, CancellationToken cancellationToken = default);
}
