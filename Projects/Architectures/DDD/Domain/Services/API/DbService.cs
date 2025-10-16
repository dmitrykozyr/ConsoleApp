using Domain.Interfaces.Db;
using Domain.Interfaces.Db.DbContext;
using Domain.Interfaces.Repositories.Db;
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
        int result = await _customerRepository.Add(request);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return result;
    }
}
