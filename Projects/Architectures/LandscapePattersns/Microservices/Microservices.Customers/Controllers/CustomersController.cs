using Microservices.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Customers.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController
{
    private readonly ICustomersProvider _customersProvider;

    public CustomersController(ICustomersProvider customersProvider)
    {
        _customersProvider = customersProvider;
    }

    [HttpGet]
    public async Task<IResult> GetCustomersAsync()
    {
        var result = await _customersProvider.GetCustomersAsync();

        if (result.IsSuccess)
        {
            return Results.Ok(result.Customers);
        }

        return Results.NotFound();
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetCustomerAsync(int id)
    {
        var result = await _customersProvider.GetCustomerAsync(id);

        if (result.IsSuccess)
        {
            return Results.Ok(result.Customer);
        }

        return Results.NotFound();
    }
}
