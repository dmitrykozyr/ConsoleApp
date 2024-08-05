using AutoMapper;
using Microservices.Orders.DB;
using Microservices.Orders.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext _DBContext;
        private readonly ILogger<OrdersProvider> _logger;
        private readonly IMapper _mapper;

        public OrdersProvider(
            OrdersDbContext DBContext,
            ILogger<OrdersProvider> logger,
            IMapper mapper)
        {
            _DBContext = DBContext;
            _logger = logger;
            _mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!_DBContext.Orders.Any())
            {
                _DBContext.Orders.Add(new DB.Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    Total = 10,
                    OrderDate = DateTime.Now,
                    Items = new List<DB.OrderItem>()
                    {
                        new DB.OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new DB.OrderItem() { OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new DB.OrderItem() { OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice = 10 },
                        new DB.OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new DB.OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    }
                });
                _DBContext.Orders.Add(new DB.Order()
                {
                    Id = 2,
                    CustomerId = 2,
                    Total = 20,
                    OrderDate = DateTime.Now,
                    Items = new List<DB.OrderItem>()
                    {
                        new DB.OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new DB.OrderItem() { OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new DB.OrderItem() { OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice = 10 },
                        new DB.OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new DB.OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    }
                });
                _DBContext.Orders.Add(new DB.Order()
                {
                    Id = 3,
                    CustomerId = 3,
                    Total = 30,
                    OrderDate = DateTime.Now,
                    Items = new List<DB.OrderItem>()
                    {
                        new DB.OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new DB.OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new DB.OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    }
                });
                _DBContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                _logger?.LogInformation("Queryng customers");
                var orders = await _DBContext.Orders.Where(z => z.CustomerId == customerId).ToListAsync();
                if (orders is not null && orders.Any())
                {
                    _logger?.LogInformation($"{orders.Count} order(s) found");
                    var result = _mapper.Map<IEnumerable<DB.Order>, IEnumerable<Models.Order>>(orders);
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
}
