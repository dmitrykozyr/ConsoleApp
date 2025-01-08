using Microservices.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Products.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    public IProductsProvider _productsProvider { get; }

    public ProductsController(IProductsProvider productsProvider)
    {
        _productsProvider = productsProvider;
    }

    [HttpGet]
    public async Task<IActionResult> GetProductsAsync()
    {
        var result = await _productsProvider.GetProductsAsync();
        if (result.IsSuccess)
        {
            return Ok(result.Products);
        }
        return NotFound();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductAsync(int id)
    {
        var result = await _productsProvider.GetProductAsync(id);
        if (result.IsSuccess)
        {
            return Ok(result.Product);
        }
        return NotFound();
    }
}
