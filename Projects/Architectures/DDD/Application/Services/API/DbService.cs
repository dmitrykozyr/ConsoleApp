using Application.Interfaces.DB;
using Domain.Interfaces.DB;
using Domain.Interfaces.Repositories.DB;
using Domain.Models.DB;
using Domain.Models.RequestModels.Db;

namespace Application.Services.API;

public class DbService : IDbService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DbService(
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = Customer.Create(request.Name, request.Address);

        int result = await _customerRepository.Add(customer);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return result;
    }
}
