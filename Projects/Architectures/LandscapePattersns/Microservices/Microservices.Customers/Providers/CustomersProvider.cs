using AutoMapper;
using Microservices.Customers.DB;
using Microservices.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Customers.Providers;

public class CustomersProvider : ICustomersProvider
{
    private readonly CustomersDbContext _dbContext;
    private readonly ILogger<CustomersProvider> _logger;
    private readonly IMapper _mapper;

    public CustomersProvider(
        CustomersDbContext dbContext,
        ILogger<CustomersProvider> logger,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;

        SeedData();
    }

    private void SeedData()
    {
        if (!_dbContext.Customers.Any())
        {
            _dbContext.Customers.Add(new DB.Customer() { Id = 1, Name = "Jessica", Address = "Address 1" });
            _dbContext.Customers.Add(new DB.Customer() { Id = 2, Name = "John",    Address = "Address 2" });
            _dbContext.Customers.Add(new DB.Customer() { Id = 3, Name = "William", Address = "Address 3" });

            _dbContext.SaveChanges();
        }
    }

    public async Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers, string ErrorMessage)> GetCustomersAsync()
    {
        try
        {
            _logger?.LogInformation("Queryng customers");

            var customers = await _dbContext.Customers.ToListAsync();

            if (customers is not null && customers.Any())
            {
                _logger?.LogInformation($"{customers.Count} customer(s) found");

                var result = _mapper.Map<IEnumerable<DB.Customer>, IEnumerable<Models.Customer>>(customers);

                return (true, result, null);
            }

            return (false, null, "Not found");
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());

            return (false, null, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, Models.Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
    {
        try
        {
            _logger?.LogInformation("Queryng customers");

            var customer = await _dbContext.Customers.FirstOrDefaultAsync(z => z.Id == id);

            if (customer is not null)
            {
                _logger?.LogInformation("Customer found");

                var result = _mapper.Map<DB.Customer, Models.Customer>(customer);

                return (true, result, null);
            }

            return (false, null, "Not found");
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());

            return (false, null, ex.Message);
        }
    }
}
