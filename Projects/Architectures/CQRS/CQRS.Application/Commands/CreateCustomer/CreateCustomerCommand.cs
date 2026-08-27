using MediatR;

namespace CQRS.Application.Commands.CreateCustomer;

public sealed record CreateCustomerCommand(string Name, string Address) : IRequest<long>;
