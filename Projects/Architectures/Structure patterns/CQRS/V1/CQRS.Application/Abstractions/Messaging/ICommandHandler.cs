using CQRS.Domain.Shared;
using MediatR;

namespace CQRS.Application.Abstractions.Messaging;

// Result - ожидаемый возвращаемый тип
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
}
