using AutoMapper;
using Microservices.Products.DB;
using Microservices.Products.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Products.Providers;

public class ProductsProvider : IProductsProvider
{
    private readonly ProductsDbContext _dbContext;
    private readonly ILogger<ProductsProvider> _logger;
    private readonly IMapper _mapper;

    public ProductsProvider(
        ProductsDbContext dbContext,
        ILogger<ProductsProvider> logger,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;

        SeedData();
    }

    private void SeedData()
    {
        // Если нет продуктов - создадим их
        if (!_dbContext.Products.Any())
        {
            _dbContext.Products.Add(new DB.Product() { Id = 1, Name = "Keyboard", Price = 20, Inventory = 100 });
            _dbContext.Products.Add(new DB.Product() { Id = 2, Name = "Mouse", Price = 50, Inventory = 200 });
            _dbContext.Products.Add(new DB.Product() { Id = 3, Name = "Monitor", Price = 150, Inventory = 100 });
            _dbContext.Products.Add(new DB.Product() { Id = 4, Name = "CPU", Price = 200, Inventory = 2000 });
            _dbContext.SaveChanges();
        }
    }

    public async Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductsAsync()
    {
        try
        {
            // Обращаемся к DbContext, поэтому используем try catch
            var products = await _dbContext.Products.ToListAsync();
            if (products is not null && products.Any())
            {
                // Смаппим тип IEnumerable <Db.Product> в IEnumerable <Models.Product>
                var result = _mapper.Map<IEnumerable<DB.Product>, IEnumerable<Models.Product>>(products);
                return (true, result, null);
            }
            return (false, null, "Not Found");
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());
            return (false, null, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> GetProductAsync(int id)
    {
        try
        {
            // Обращаемся к DbContext, поэтому используем try catch
            var product = await _dbContext.Products.FirstOrDefaultAsync(z => z.Id == id);
            if (product is not null)
            {
                // Смаппим тип IEnumerable <Db.Product> в IEnumerable <Models.Product>
                var result = _mapper.Map<DB.Product, Models.Product>(product);
                return (true, result, null);
            }
            return (false, null, "Not Found");
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());
            return (false, null, ex.Message);
        }
    }
}
