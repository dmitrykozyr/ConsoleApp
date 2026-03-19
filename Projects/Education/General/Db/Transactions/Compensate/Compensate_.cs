using Education.General.Db.Transactions.Compensate.Interfaces;
using Education.General.Db.Transactions.Compensate.Models;

namespace Education.General.Db.Transactions.Compensate;

public class Compensate_
{
    /*
         Это противоположное действие, отменяющее результат предыдущего успешного шага, если вся цепочка (Saga) развалилась на полпути
         Если данные уже зафиксированы в БД, нельзя просто сделать Rollback
         Для каждого шага Вперед должен быть прописан шаг Назад:
         - забронировать товар / разбронировать товар
         - списать деньги / вернуть деньги
         Пример логики на C# (Оркестрация)
         Допустим, у нас есть сервис-координатор, который по очереди дергает микросервисы:
    */

    private readonly IStockService _stockService;
    private readonly IPaymentService _paymentService;
    private readonly IDeliveryService _deliveryService;

    public Compensate_(IStockService stockService, IPaymentService paymentService, IDeliveryService deliveryService)
    {
        _stockService = stockService;
        _paymentService = paymentService;
        _deliveryService = deliveryService;
    }

    public async Task CreateOrderSaga(Order order)
    {
        // Бронируем товар
        Order stockResult = await _stockService.Reserve(order.ProductId);

        if (stockResult.Success)
        {
            // Списываем деньги
            var paymentResult = await _paymentService.Charge(order.UserId, order.Amount);

            if (paymentResult.Success)
            {
                // Доставка (тут ошибка)
                var deliveryResult = await _deliveryService.Schedule(order.Id);

                if (!deliveryResult.Success)
                {
                    // Запускаем цепочку откатов

                    // Откат Шага 2
                    await _paymentService.Refund(order.UserId, order.Amount);

                    // Откат Шага 1
                    await _stockService.Release(order.ProductId);

                    Console.WriteLine("Сага отменена, средства возвращены");
                }
            }
            else
            {
                // Если оплата не прошла, откатываем только Шаг 1
                await _stockService.Release(order.ProductId);
            }
        }
    }
}
