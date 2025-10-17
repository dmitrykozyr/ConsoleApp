using Domain.Interfaces.Db;
using Domain.Interfaces.Repositories.Db;
using Domain.Models.Db;
using Domain.Models.RequentModels.Db;

namespace Domain.Services.API;

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
        var customer = new Customer
        {
            Name = request.Name,
            Address = request.Address
        };

        int result = _customerRepository.Add(customer);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return result;
    }
}
