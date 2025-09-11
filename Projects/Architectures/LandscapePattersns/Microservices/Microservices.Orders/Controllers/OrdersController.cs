using Microservices.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Orders.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController
{
    public readonly IOrdersProvider _ordersProvider;

    public OrdersController(IOrdersProvider ordersProvider)
    {
        _ordersProvider = ordersProvider;
    }

    [HttpGet("{customerId}")]
    public async Task<IResult> GetOrdersAsync(int customerId)
    {
        var result = await _ordersProvider.GetOrdersAsync(customerId);

        if (result.IsSuccess)
        {
            return Results.Ok(result.Orders);
        }

        return Results.NotFound();
    }
}
