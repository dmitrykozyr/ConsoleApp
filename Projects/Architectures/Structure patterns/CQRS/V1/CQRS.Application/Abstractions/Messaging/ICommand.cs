using CQRS.Domain.Shared;
using MediatR;

namespace CQRS.Application.Abstractions.Messaging;

// Для использования MediatR нужно унаследовать интерфейс от IRequest

// Команда ничего не возвращает
public interface ICommand : IRequest<Result>
{
}

// Команда возвращает данные
public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
