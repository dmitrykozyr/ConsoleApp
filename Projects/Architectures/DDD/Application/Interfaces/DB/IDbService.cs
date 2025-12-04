using Domain.Models.RequestModels.Db;

namespace Application.Interfaces.DB;

public interface IDbService
{
    Task<int> Handle(CreateCustomerRequest request, CancellationToken cancellationToken);
}
