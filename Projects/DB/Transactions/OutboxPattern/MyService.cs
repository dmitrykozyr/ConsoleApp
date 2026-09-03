using Microsoft.EntityFrameworkCore;

namespace Education.General.Db.Transactions.OutboxPattern;

/*
    Паттерн обеспечивает надежную доставку сообщений в распределенных системах (микросервисах)
    Принцип работы как у транзакций - выполнятся все действия или ни одно
    Вместо немедленной отправки сообщений (например, через очередь) приложение сначала сохраняет их в таблице (Outbox)
    Это гарантирует сохранность сообщений при сбое
    При сбое система повторит отправку
    При успешной отправке сообщение удаляется из таблицы Outbox
*/
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
