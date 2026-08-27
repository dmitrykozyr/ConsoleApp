using MediatR;

namespace CQRS.Application.Queries.GetCustomers;

public sealed record GetCustomersQuery()
    : IRequest<IReadOnlyList<CustomerListItemDto>>;
