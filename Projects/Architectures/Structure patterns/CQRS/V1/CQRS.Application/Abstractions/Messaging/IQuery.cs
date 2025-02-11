using CQRS.Domain.Shared;
using MediatR;

namespace CQRS.Application.Abstractions.Messaging;

public interface IQuery<TResponce> : IRequest<Result<TResponce>>
{
}
