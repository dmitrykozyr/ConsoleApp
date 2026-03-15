using Microsoft.EntityFrameworkCore;

namespace Education.General.Db;

public class Transaction_
{
    /*
     public async Task<bool> F1Async(CancellationToken cancellationToken = default)
     {
      await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

      try
      {
       // .. логика

       var result = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

       await transaction.CommitAsync(cancellationToken);

       return result;
      }
      catch
      {
       // Rollback произойдет автоматически при выходе из try (dispose транзакции)
       throw;
      }
     }
    */


    #region Распределенные транзакции

        /*
            Outbox Pattern
            Позволяет гарантированно связать два действия внутри одного сервиса,
            например, сохранить заказ в БД и отправить сообщение об этом в RabbitMQ/Kafka

            Saga Pattern
            Используется, когда транзакция разносится на несколько независимых микросервисов, у каждого из которых своя БД:
            - сервис заказов бронирует товар
            - сервис оплаты списывает деньги
            - сервис доставки назначает курьера
            Если на шаге 3 произошла ошибка, Saga должна запустить компенсирующие транзакции (вернуть деньги в шаге 2 и разбронировать товар в шаге 1)
        */

    #region Outbox Pattern

        /*
             Паттерн обеспечивает надежную доставку сообщений в распределенных системах (микросервисах)
             Принцип работы как у транзакций - выполнятся все действия или ни одно
             Вместо немедленной отправки сообщений (например, через очередь) приложение сначала сохраняет их в таблице (Outbox)
             Это гарантирует сохранность сообщений при сбое
             При сбое система повторит отправку
             При успешной отправке сообщение удаляется из таблицы Outbox
        */

        public class OutboxMessage
        {
            public Guid Id { get; init; }

            public string? Message { get; init; }

            public bool IsSent { get; set; }
        }

        public class MyDbContext : DbContext
        {
            public DbSet<OutboxMessage>? OutboxMessages { get; init; }
        }

        public class MyService
        {
            private readonly MyDbContext _dbContext;

            public MyService(MyDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task PerformActionAndSendMessage(string actionData)
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync();

                try
                {
                    // ..
                    // Бизнес-логика, например, обновление данных
                    // ..

                    // Сохранение сообщения в Outbox
                    var outboxMessage = new OutboxMessage
                    {
                        Id = Guid.NewGuid(),
                        Message = actionData,
                        IsSent = false
                    };

                    _dbContext.OutboxMessages?.Add(outboxMessage);

                    await _dbContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    throw new Exception(ex.Message);
                }
            }

            // Данный метод не нужно вызывать вручную, а через BackgroundService или библиотеку Quartz,
            // которые раз в 5–10 секунд забирают пачку сообщений и отправляют их
            public async Task SendOutboxMessages()
            {
                List<OutboxMessage>? messages = await _dbContext.OutboxMessages
                 .Where(m => !m.IsSent)
                 .ToListAsync();

                foreach (var message in messages)
                {
                    // ..
                    // Логика отправки сообщения в очередь
                    // ..

                    // Если отправка успешна
                    message.IsSent = true;

                    // Здесь отправка в очередь и пометка IsSent = true происходят раздельно
                    // Если сообщение ушло в очередь, но произошел сбой и не выполнилось SaveChangesAsync()  при следующем запуске отправим то же сообщение повторно
                    // На стороне Consumer должна быть идемпотентность
                }

                await _dbContext.SaveChangesAsync();
            }
        }

    #endregion

    #region Saga Pattern

        //!

    #endregion

    #region Компенсирующие транзакции

    /*
         Это противоположное действие, отменяющее результат предыдущего успешного шага, если вся цепочка (Saga) развалилась на полпути
         Если данные уже зафиксированы в БД, нельзя просто сделать Rollback
         Для каждого шага Вперед должен быть прописан шаг Назад:
         - забронировать товар / разбронировать товар
         - списать деньги / вернуть деньги
         Пример логики на C# (Оркестрация)
         Допустим, у нас есть сервис-координатор, который по очереди дергает микросервисы:

         public async Task CreateOrderSaga(Order order)
         {
          // Бронируем товар
          var stockResult = await _stockService.Reserve(order.ProductId);

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
    */

    #endregion

    #endregion
}
