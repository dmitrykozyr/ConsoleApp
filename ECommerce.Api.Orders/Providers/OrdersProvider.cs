using AutoMapper;
using ECommerce.Api.Orders.Db;
using ECommerce.Api.Orders.Interfaces;
using ECommerce.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext _dbContext;
        private readonly ILogger<OrdersProvider> _logger;
        private readonly IMapper _mapper;

        public OrdersProvider(
            OrdersDbContext dbContext,
            ILogger<OrdersProvider> logger,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!_dbContext.Orders.Any())
            {
                _dbContext.Orders.Add(new Db.Order() { Id = 1, CustomerId = 1, Total = 10, OrderDate = DateTime.Now });
                _dbContext.Orders.Add(new Db.Order() { Id = 2, CustomerId = 2, Total = 20, OrderDate = DateTime.Now });
                _dbContext.Orders.Add(new Db.Order() { Id = 3, CustomerId = 3, Total = 30, OrderDate = DateTime.Now });
                _dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                _logger?.LogInformation("Queryng customers");
                var orders = await _dbContext.Orders.Where(z => z.CustomerId == customerId).ToListAsync();
                if (orders != null && orders.Any())
                {
                    _logger?.LogInformation($"{orders.Count} order(s) found");
                    var result = _mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
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
