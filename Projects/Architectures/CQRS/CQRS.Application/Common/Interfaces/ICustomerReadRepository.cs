using CQRS.Application.Queries.GetCustomerById;
using CQRS.Application.Queries.GetCustomers;

namespace CQRS.Application.Common.Interfaces;

public interface ICustomerReadRepository
{
    Task<CustomerDetailsDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<CustomerListItemDto>> GetAllAsync(CancellationToken cancellationToken = default);
}
