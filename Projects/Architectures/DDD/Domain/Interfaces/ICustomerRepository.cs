using Domain.Models.DB;

namespace Domain.Interfaces;

public interface ICustomerRepository
{
    Task<Customer?> GetById(long id);

    Task<int> Add(Customer request);
}
