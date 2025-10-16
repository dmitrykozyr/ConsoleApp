using Domain.Models.RequentModels.Db;

namespace Domain.Interfaces.Repositories.Db;

public interface ICustomerRepository
{
    public Task<int> Add(CreateCustomerRequest request);
}
