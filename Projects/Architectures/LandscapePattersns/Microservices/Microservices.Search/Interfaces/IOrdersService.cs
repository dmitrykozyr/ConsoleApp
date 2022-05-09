using Microservices.Search.Models;

namespace Microservices.Search.Interfaces
{
    // Интерфейс для взаимодействия с сервисом Orders
    public interface IOrdersService
    {
        Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId);
    }
}
