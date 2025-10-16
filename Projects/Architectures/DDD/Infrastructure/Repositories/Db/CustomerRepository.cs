using Domain.Interfaces.Repositories.Db;
using Domain.Models.RequentModels.Db;

namespace Infrastructure.Repositories.Db;

public class CustomerRepository : ICustomerRepository
{
    public Task<int> Add(CreateCustomerRequest request)
    {
        throw new NotImplementedException();
    }
}
