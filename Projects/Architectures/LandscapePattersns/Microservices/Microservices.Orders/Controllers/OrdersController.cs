using Microservices.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Orders.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    public readonly IOrdersProvider _ordersProvider;

    public OrdersController(IOrdersProvider ordersProvider)
    {
        _ordersProvider = ordersProvider;
    }

    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetOrdersAsync(int customerId)
    {
        var result = await _ordersProvider.GetOrdersAsync(customerId);
        if (result.IsSuccess)
        {
            return Ok(result.Orders);
        }
        return NotFound();
    }
}
