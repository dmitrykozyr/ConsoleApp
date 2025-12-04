using Domain.Interfaces.DB.DbContext;
using Domain.Interfaces.Repositories.DB;
using Domain.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.DB;

public class CustomerRepository : ICustomerRepository
{
    private readonly IApplicationDbContext _dbContext;

    public CustomerRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Customer?> GetById(long id)
    {
        var result = await _dbContext.Customers.FirstOrDefaultAsync(o => o.Id == id);

        return result;
    }

    public async Task<int> Add(Customer request)
    {
        var result = await _dbContext.Customers.AddAsync(request);

        return result is null ? 0 : 1;

    }
}
