using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApiNetCore.Classes;
using WebApiNetCore.DatabaseContext;
using WebApiNetCore.Models;

// Создание контроллера: на папке Controllers ПКМ -> Add Controller -> Api Controller - Empty
namespace WebApiNetCore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopContext _context;

        public ProductsController(ShopContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        [HttpGet]
        // Пагинация    https://localhost:44345/Products?size=10&page=2,
        // Фильтры      https://localhost:44345/Products?size=10&page=2&minPrice=1&maxPrice=20
        // Сортировка   https://localhost:44345/Products?sortBy=Price&sortOrder=desc
        // [FromQuery] QueryParameters позволяет получать параметры из URL
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductQueryParameters queryParameters)
        {
            IQueryable<Product> products = _context.Products;

            // Фильтры
            if(queryParameters.MinPrice != null && queryParameters.MaxPrice != null)
            {
                products = products.Where(p => p.Price >= queryParameters.MinPrice.Value && p.Price <= queryParameters.MaxPrice.Value);
            }

            if (!string.IsNullOrEmpty(queryParameters.Sku))
            {
                products = products.Where(p => p.Sku == queryParameters.Sku);
            }

            if (!string.IsNullOrEmpty(queryParameters.Name))
            {
                products = products.Where(p => p.Name.ToLower().Contains(queryParameters.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(queryParameters.SortBy))
            {
                if (typeof(Product).GetProperty(queryParameters.SortBy) != null)
                {
                    products = products.OrderByCustom(queryParameters.SortBy, queryParameters.SortOrder);
                }
            }

            // Пагинация
            products = products.Skip(queryParameters.Size * (queryParameters.Page - 1))
                               .Take(queryParameters.Size);

            return Ok(await products.ToArrayAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(z => z.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
