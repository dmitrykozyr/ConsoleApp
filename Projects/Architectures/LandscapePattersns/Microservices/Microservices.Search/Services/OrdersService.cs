using Microservices.Search.Interfaces;
using Microservices.Search.Models;
using System.Text.Json;

namespace Microservices.Search.Services
{
    // Класс для взаимодействия с сервисом Orders
    public class OrdersService : IOrdersService
    {
        IHttpClientFactory _httpClientFactory;
        ILogger<OrdersService> _logger;

        public OrdersService(
            IHttpClientFactory httpClientFactory,
            ILogger<OrdersService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("OrdersService");
                var response = await client.GetAsync($"api/orders/{customerId}");
                if (response.IsSuccessStatusCode)
                {
                    // Десериализация
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Order>>(content, options);
                    return (true, result, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
