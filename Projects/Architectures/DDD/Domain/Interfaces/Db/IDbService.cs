using Domain.Models.RequentModels.Db;

namespace Domain.Interfaces.Db;

public interface IDbService
{
    Task<int> Handle(CreateCustomerRequest request, CancellationToken cancellationToken);
}
