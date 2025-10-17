using Domain.Models.Db;

namespace Domain.Interfaces.Repositories.Db;

public interface ICustomerRepository
{
    Customer? GetById(long id);

    public int Add(Customer request);
}
