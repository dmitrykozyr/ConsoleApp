using MediatR;

namespace CQRS.Application.Queries.GetCustomers;

// В CQRS запрос — это класс, реализующий интерфейс IRequest<TResponse>
public sealed record GetCustomersQuery() : IRequest<IReadOnlyList<CustomerListItemDto>>;
