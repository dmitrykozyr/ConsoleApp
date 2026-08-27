using CQRS.Domain.Interfaces;
using CQRS.Domain.Models.Db;
using MediatR;

namespace CQRS.Application.Commands.CreateCustomer;

public sealed class CreateCustomerCommandHandler(ICustomerWriteRepository writeRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateCustomerCommand, long>
{
    public async Task<long> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = Customer.Create(request.Name, request.Address);

        await writeRepository.AddAsync(customer, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return customer.Id;
    }
}
