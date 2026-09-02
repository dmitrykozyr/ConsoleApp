using MediatR;

namespace CQRS.Application.Queries.GetCustomerById;

public sealed record GetCustomerByIdQuery(long Id) : IRequest<CustomerDetailsDto?>;
