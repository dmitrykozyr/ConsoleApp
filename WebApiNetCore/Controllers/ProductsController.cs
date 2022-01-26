using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
    [Authorize]
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
            if (queryParameters.MinPrice != null && queryParameters.MaxPrice != null)
            {
                products = products.Where(p => p.Price >= queryParameters.MinPrice.Value && p.Price <= queryParameters.MaxPrice.Value);
            }

            if (!string.IsNullOrEmpty(queryParameters.SearchTerm))
            {
                products = products.Where(z => z.Sku.ToLower().Contains(queryParameters.SearchTerm.ToLower()) ||
                                               z.Name.ToLower().Contains(queryParameters.SearchTerm.ToLower()));
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

        // Можно возвращать IActionResult или ActionResult<>
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromBody] Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                "GetProcuct",
                new { id = product.Id },
                product);
        }

        // Тут используем [FromRoute] и [FromBody]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Products.Find(id) == null)
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        // HttpDelete удаляет один элемент, поэтому используем HttpPost для удаления нескольких элементов
        [HttpPost("{id}")]
        [Route("Delete")]
        public async Task<IActionResult> DeleteProducts([FromQuery] int[] ids)
        {
            var products = new List<Product>();
            foreach (var id in ids)
            {
                var product = await _context.Products.FindAsync(id);
                
                if (product == null)
                {
                    return NotFound();
                }

                products.Add(product);
            }

            _context.Products.RemoveRange(products);
            await _context.SaveChangesAsync();
            return Ok(products);
        }
    }
}
