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
        try
        {
            var id = await sender.Send(command, cancellationToken);

            var result = CreatedAtAction(
                nameof(GetById),
                new { id },
                id);

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<CustomerDetailsDto>> GetById(long id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await sender.Send(new GetCustomerByIdQuery(id), cancellationToken);

            return result is null ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CustomerListItemDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var result = await sender.Send(new GetCustomersQuery(), cancellationToken);

            return Ok(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
