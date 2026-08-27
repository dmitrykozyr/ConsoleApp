using CQRS.Domain.Interfaces;
using CQRS.Domain.Models.Db;
using CQRS.Infrastructure.Persistence;

namespace CQRS.Infrastructure.Repositories;

public sealed class CustomerWriteRepository(ApplicationDbContext dbContext)
    : ICustomerWriteRepository
{
    public async Task AddAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        await dbContext.Customers.AddAsync(customer, cancellationToken);
    }
}
