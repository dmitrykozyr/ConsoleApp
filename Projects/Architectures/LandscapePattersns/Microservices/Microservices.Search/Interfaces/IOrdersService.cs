using Microservices.Search.Models;

namespace Microservices.Search.Interfaces;

public interface IOrdersService
{
    Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId);
}
