using CQRS.Domain.Models.Db;

namespace CQRS.Domain.Interfaces;

public interface ICustomerWriteRepository
{
    Task AddAsync(Customer customer, CancellationToken cancellationToken = default);
}
