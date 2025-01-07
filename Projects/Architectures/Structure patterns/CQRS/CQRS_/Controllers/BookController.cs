using CQRS_.Commands;
using CQRS_.Models;
using CQRS_.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS_.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<Book> Get(string name)
        {
            var query = new GetBookQuery(name);

            return _mediator.Send(query);
        }

        [HttpPost]
        public Task Add(Book book)
        {
            var command = new AddBookCommand(book);

            return _mediator.Send(command);
        }
    }
}
