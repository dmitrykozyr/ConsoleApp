using CQRS.Application.Commands.CreateCustomer;
using CQRS.Application.Queries.GetCustomerById;
using CQRS.Application.Queries.GetCustomers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CustomersController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<long>> Create([FromBody] CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        var id = await sender.Send(command, cancellationToken);

        var result = CreatedAtAction(
            nameof(GetById),
            new { id },
            id);

        return result;
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<CustomerDetailsDto>> GetById(long id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetCustomerByIdQuery(id), cancellationToken);

        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CustomerListItemDto>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetCustomersQuery(), cancellationToken);

        return Ok(result);
    }
}
