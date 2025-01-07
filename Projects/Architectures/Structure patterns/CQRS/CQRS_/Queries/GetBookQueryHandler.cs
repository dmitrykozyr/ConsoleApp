using CQRS_.Models;
using MediatR;

namespace CQRS_.Queries
{
    internal class GetBookQueryHandler : IRequestHandler<GetBookQuery, Book>
    {
        public Task<Book> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            // Получение книги по имени из БД

            return Task.FromResult(new Book(request.Name));
        }
    }
}
