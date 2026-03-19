using Education.General.Db.Transactions.Compensate.Models;

namespace Education.General.Db.Transactions.Compensate.Interfaces;

public interface IStockService
{
    Task<Order> Reserve(long productId);

    Task Release(long productId);
}
