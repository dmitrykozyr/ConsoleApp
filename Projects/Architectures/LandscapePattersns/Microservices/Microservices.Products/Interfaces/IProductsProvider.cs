using Microservices.Products.Models;

namespace Microservices.Products.Interfaces
{
    public interface IProductsProvider
    {
        // Кортеж возвращает три значения
        Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync();
        Task<(bool IsSuccess, Product Product, string ErrorMessage)> GetProductAsync(int id);
    }
}
