using CQRS_.Models;
using MediatR;

namespace CQRS_.Queries
{
    internal class GetBookQuery : IRequest<Book>
    {
        public string Name { get; set; }

        public GetBookQuery(string name)
        {
            Name = name;
        }
    }
}
