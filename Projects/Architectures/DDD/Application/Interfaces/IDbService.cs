using Application.Models.RequestModels;

namespace Application.Interfaces;

public interface IDbService
{
    Task<int> Handle(CreateCustomerRequest request, CancellationToken cancellationToken);
}
