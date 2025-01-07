using CQRS_.Models;
using MediatR;

namespace CQRS_.Commands
{
    internal class AddBookCommand : IRequest
    {
        public Book Book { get; set; }

        public AddBookCommand(Book book)
        {
            Book = book;
        }
    }
}
