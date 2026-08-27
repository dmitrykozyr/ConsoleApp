using CQRS.Application.Common.Interfaces;
using MediatR;

namespace CQRS.Application.Queries.GetCustomers;

public sealed class GetCustomersQueryHandler(ICustomerReadRepository readRepository)
    : IRequestHandler<GetCustomersQuery, IReadOnlyList<CustomerListItemDto>>
{
    public Task<IReadOnlyList<CustomerListItemDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        => readRepository.GetAllAsync(cancellationToken);
}
