using CQRS.Domain.Shared;
using MediatR;

namespace CQRS.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
