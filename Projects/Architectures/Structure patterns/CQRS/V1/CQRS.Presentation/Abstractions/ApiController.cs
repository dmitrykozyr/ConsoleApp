using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Presentation.Abstractions;

[ApiController]
public abstract class ApiController : ControllerBase
{
    // Этот интерфейс подходит для отправки Command, Query
    protected readonly ISender Sender;

    protected ApiController(ISender sender)
    {
        Sender = sender;
    }
}
