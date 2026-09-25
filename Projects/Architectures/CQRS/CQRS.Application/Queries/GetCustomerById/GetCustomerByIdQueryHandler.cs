using CQRS.Application.Common.Interfaces;
using MediatR;

namespace CQRS.Application.Queries.GetCustomerById;

public sealed class GetCustomerByIdQueryHandler(ICustomerReadRepository readRepository)
    : IRequestHandler<GetCustomerByIdQuery, CustomerDetailsDto?>
{
    public Task<CustomerDetailsDto?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken) =>
        readRepository.GetByIdAsync(request.Id, cancellationToken);
}
