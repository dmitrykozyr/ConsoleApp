using Domain.Interfaces.Db.DbContext;
using Domain.Interfaces.Repositories.Db;
using Domain.Models.Db;

namespace Infrastructure.Repositories.Db;

public class CustomerRepository : ICustomerRepository
{
    private readonly IApplicationDbContext _dbContext;

    public CustomerRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Customer? GetById(long id)
    {
        var result = _dbContext.Customers.FirstOrDefault(o => o.Id == id);

        return result;
    }

    public int Add(Customer request)
    {
        var result = _dbContext.Customers.Add(request);

        return result is null ? 0 : 1;

    }
}
