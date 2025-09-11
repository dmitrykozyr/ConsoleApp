using Microservices.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Products.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController
{
    public IProductsProvider _productsProvider { get; }

    public ProductsController(IProductsProvider productsProvider)
    {
        _productsProvider = productsProvider;
    }

    [HttpGet]
    public async Task<IResult> GetProductsAsync()
    {
        var result = await _productsProvider.GetProductsAsync();

        if (result.IsSuccess)
        {
            return Results.Ok(result.Products);
        }

        return Results.NotFound();
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetProductAsync(int id)
    {
        var result = await _productsProvider.GetProductAsync(id);

        if (result.IsSuccess)
        {
            return Results.Ok(result.Product);
        }

        return Results.NotFound();
    }
}
