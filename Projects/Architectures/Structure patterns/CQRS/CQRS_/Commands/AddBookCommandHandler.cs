using MediatR;

namespace CQRS_.Commands
{
    internal class AddBookCommandHandler : IRequestHandler<AddBookCommand>
    {
        public Task Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            // Добавление книги в БД

            return Task.CompletedTask;
        }
    }
}
