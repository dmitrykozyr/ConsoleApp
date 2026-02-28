using Application.Models.RequestModels;

namespace Application.Services.API;

public interface IDbService
{
    Task<int> Handle(CreateCustomerRequest request, CancellationToken cancellationToken);
}
