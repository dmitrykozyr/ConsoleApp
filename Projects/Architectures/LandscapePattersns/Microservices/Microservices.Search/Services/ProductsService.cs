using Microservices.Search.Interfaces;
using Microservices.Search.Models;
using System.Text.Json;

namespace Microservices.Search.Services
{
    public class ProductsService : IProductsService
    {
        IHttpClientFactory _httpClientFactory;
        ILogger<ProductsService> _logger;

        public ProductsService(
            IHttpClientFactory httpClientFactory,
            ILogger<ProductsService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ProductsService");
                var response = await client.GetAsync("api/products");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Product>>(content, options);
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
