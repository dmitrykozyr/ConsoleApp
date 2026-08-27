using CQRS.Application.Common.Interfaces;
using CQRS.Application.Queries.GetCustomerById;
using CQRS.Application.Queries.GetCustomers;
using CQRS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Infrastructure.Repositories;

public sealed class CustomerReadRepository(ApplicationDbContext dbContext)
    : ICustomerReadRepository
{
    public async Task<CustomerDetailsDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var result = await dbContext.Customers
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new CustomerDetailsDto(x.Id, x.Name, x.Address))
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }

    public async Task<IReadOnlyList<CustomerListItemDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await dbContext.Customers
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new CustomerListItemDto(x.Id, x.Name))
            .ToListAsync(cancellationToken);

        return result;
    }
}
