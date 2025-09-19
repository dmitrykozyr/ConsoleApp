using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Transactions;

namespace Education;

public class EF_
{
    #region Transient Scoped Singleton

    /*
        - Singleton  Объект сервиса создается один раз при первом обращении к нему

        - Scoped     Для каждого запроса создается объект сервиса
                        Если в течение одного запроса есть несколько обращений к сервису - будет использован один и тот же объект

        - Transient  При каждом обращении к сервису создается новый объект сервиса
                        Подходит для сервисов, которые не хранят данные об состоянии
    */

    #endregion

    #region AsNoTracking

    /*
        Загружаемые сущности не будут отслеживаться контекстом БД

        Полезно в случаях:
        - улучшение производительности, когда загружаем данные, которые не собираемся изменять
        - снижение использования памяти - EF не будет отслеживать изменения этих сущностей
        - чесли хотим читать данные и не изменять
    
        using (var context = new MyDbContext())
        {
            var products = context.Products.AsNoTracking().ToList();
        }
    */

    #endregion

    #region Middleware

    /*
        Конвейер обработки Middleware конфигурируется методами Use, Run, Map, важен порядок:
        - Use добавляет компоненты middleware в конвейер
        - Run - замыкающий метод
        - Map применяется для сопоставления пути запроса с делегатом, который будет обрабатывать запрос по этому пути

        Компоненты Middleware по умолчанию:
        - Authentication
        - Cookie Policy          отслеживает согласие пользователя на хранение информации в куках
        - CORS                   поддержка кроссдоменных запросов
        - Diagnostics            предоставляет страницы статус кодов, функционал обработки исключений
        - Forwarded Headers      перенаправляет зголовки запроса
        - Health Check           проверяет работоспособность приложения
        - HTTP Method Override   позволяет входящему POST - запросу переопределить метод
        - HTTPS Redirection      перенаправляет все запросы HTTP на HTTPS
        - HTTP Strict Transport Security     для безопасности добавляет специальный заголовок ответа
        - MVC                    обеспечивает функционал MVC
        - Request Localization   обеспечивает поддержку локализации
        - Response Caching       позволяет кэшировать результаты запросов
        - Response Compression   обеспечивает сжатие ответа клиенту
        - URL Rewrite            предоставляет функциональность URL Rewriting
        - Endpoint Routing       предоставляет механизм маршрутизации
        - Session                предоставляет поддержку сессий
        - Static Files           предоставляет поддержку обработки статических файлов
        - WebSockets             добавляет поддержку протокола WebSockets

        Метод Configure выполняется один раз при создании объекта класса Startup,
        а компоненты middleware создаются один раз и живут в течение жизнни приложения,
        вызываются после каждого HTTP - запроса
    */

    #endregion

    #region Безопасное хранение паролей в БД

    /*
        Хеширование паролей

        Добавление соли

        Если просто захешировать пароль 123456,
        то для некоторых алгоримов хеширования есть список хешей, которым соответстуют простые пароли

        Поэтому к паролю добавляется соль и пароль хешируется
        123456 + salt -> vrt$%VTBW$

        Соль можно хранить в открытом виде в БД
        Когда пользователь авторизуется, он вводит пароль,
        нажимает ввод и к парою прибавляется соль, а затем хешируется
        и полученный хеш сравнивается с тем, что находится в БД
    */

    #endregion

    #region Атрибуты свойств модели

    /*
        EF при работе с Code First требует определения ключа для создания первичного ключа в таблице в БД
        По умолчанию EF в качестве первичных ключей рассматривает св-ва с именами Id или [Имя_класса]Id
        
        [Key]                                                       первичный ключ
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]  установить ключ в качестве идентификатора
        [Required]                                                  обязательное св-во, в БД будет помечено как NOT NULL
        [Required(ErrorMessage = "Не указан электронный адрес")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [Range(1, 110, ErrorMessage = "Недопустимый возраст")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [MinLength(10)]
        [MaxLength(20)]
        [NotMapped]                                                 не добавлять столбец в БД
        [Table("Mobiles")]                                          если имя модели и таблицы разные, можно указать, с какой таблицей сопоставить модель
        [Column("ModelName")]                                       аналогично для свойств
        [ForeignKey("CompId")]                                      установить внешний ключ для связи с другой сущностью
        [Index]                                                     установить индекс для столбца
        [ConcurrencyCheck]                                          решаем проблему параллелизма, когда с одной же записью могут работать одновременно несколько пользователей
    */

    #endregion


    #region Один к одному

    public class User
    {
        public int Id { get; init; }

        public string? Name { get; init; }

        public int Age { get; init; }

        public decimal MoneyAmount { get; init; }

        public UserProfile? UserProfile { get; init; }
    }

    public class UserProfile
    {
        [Key]
        [ForeignKey("User")]
        public int Id { get; init; }

        public string? Login { get; init; }

        public string? Password { get; init; }

        public User? User { get; init; }
    }

    #endregion

    #region Один ко многим

    public class Player
    {
        public int Id { get; init; }

        public string? Name { get; init; }

        public int TeamId { get; init; }

        public Team? Team { get; init; }
    }

    public class Team
    {
        public int Id { get; init; }

        public string TeamName { get; init; }

        public ICollection<Player> Players { get; init; }

        public Team()
        {
            Players = new List<Player>();
        }
    }

    #endregion

    #region Многие ко многим

    public class Team_
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public ICollection<Player_> Players { get; init; }

        public Team_()
        {
            Players = new List<Player_>();
        }
    }

    public class Player_
    {
        public int Id { get; init; }

        public string TeamName { get; init; }

        public ICollection<Team_> Teams { get; init; }

        public Player_()
        {
            Teams = new List<Team_>();
        }
    }

    #endregion


    #region Паттерн Saga

    /*
        Используется для управления долгоживущими транзакциями и координации распределенных процессов
        Разбивает транзакции на серию шагов, каждый из которых может быть отменен при необходимости

        Пример:

        Есть процесс заказа товара, который включает шаги:
        - резервирование товара
        - обработка платежа
        - отправка уведомления о заказе

        Каждый шаг может завершиться успехом или неудачей
        Если что-то пойдет не так, нужно выполнить компенсационные действия

        Каждый шаг обрабатывается последовательно
        В случае ошибки выполняются компенсационные действия   
        В реальных приложениях можно использовать более сложные механизмы управления состоянием и обработки событий
    */

    public interface IProductService
    {
        Task<bool> ReserveProduct(int productId);

        Task<bool> ReleaseProduct(int productId);
    }

    public interface IPaymentService
    {
        Task<bool> ProcessPayment(decimal amount);
    }

    public interface INotificationService
    {
        Task SendOrderConfirmation(Order order);
    }

    public class Order
    {
        public int ProductId { get; init; }

        public decimal Amount { get; init; }
    }

    public class OrderSaga
    {
        private readonly IProductService _productService;
        private readonly IPaymentService _paymentService;
        private readonly INotificationService _notificationService;

        public OrderSaga(
            IProductService productService,
            IPaymentService paymentService,
            INotificationService notificationService)
        {
            _productService = productService;
            _paymentService = paymentService;
            _notificationService = notificationService;
        }

        public async Task PlaceOrder(Order order)
        {
            try
            {
                // Резервируем товар
                var productReserved = await _productService.ReserveProduct(order.ProductId);
                if (!productReserved)
                {
                    throw new Exception("Не удалось зарезервировать товар");
                }

                // Обрабатываем платеж
                var paymentProcessed = await _paymentService.ProcessPayment(order.Amount);
                if (!paymentProcessed)
                {
                    // Если не удалось обработать платеж, откатываем резервирование товара
                    await _productService.ReleaseProduct(order.ProductId);
                    throw new Exception("Не удалось обработать платеж");
                }

                // Отправляем уведомление
                await _notificationService.SendOrderConfirmation(order);
            }
            catch (Exception ex)
            {
                // Логируем ошибку
                Console.WriteLine($"Ошибка в процессе заказа: {ex.Message}");
            }
        }
    }

    public class OrderController : ControllerBase
    {
        private readonly OrderSaga _orderSaga;

        public OrderController(OrderSaga orderSaga)
        {
            _orderSaga = orderSaga;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            await _orderSaga.PlaceOrder(order);
            return Ok();
        }
    }

    #endregion

    #region Outbox

    /*
        Паттерн обеспечивает надежную доставку сообщений в распределенных системах, особенно в микросервисах
        Помогает избежать проблем с потерей сообщений и обеспечивает согласованность между действиями, выполняемыми в приложении и отправкой сообщений

        Вместо немедленной отправки сообщений (например, через очередь) приложение сначала сохраняет их в специальной таблице или хранилище данных (outbox)
        Это гарантирует, что сообщения не будут потеряны, даже если система выйдет из строя
        Outbox работает в контексте транзакции БД, когда приложение обновляет данне и так-же добавляет сообщение в outbox в той же транзакции
        Это гарантирует, что либо оба действия (обновление и запись сообщения) будут выполнены, либо ни одно не будет выполнено
        После того как транзакция завершена, отдельный процесс (или фоновая задача) периодически проверяет таблицу outbox и отправляет сообщения в очередь или другой сервис
        После успешной отправки сообщение удаляется из outbox
        Если отправка не удалась, система может повторить попытку отправки, что обеспечивает надежность и устойчивость к сбоям

        Уменьшает вероятность потери сообщений, поскольку они сохраняются в БД, пока не будут отправлены
        Гарантирует, что операции над данными и отправка сообщений происходят в одной транзакции
        Позволяет системе восстанавливаться после сбоев без потери данных или сообщений.
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
            // Начало транзакции
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                // Выполнение бизнес-логики, например, обновление данных

                // Сохранение сообщения в outbox
                var outboxMessage = new OutboxMessage
                {
                    Id = Guid.NewGuid(),
                    Message = actionData,
                    IsSent = false
                };

                _dbContext.OutboxMessages.Add(outboxMessage);

                await _dbContext.SaveChangesAsync();

                // Подтверждение транзакции
                await transaction.CommitAsync();
            }
            catch
            {
                // Обработка ошибок и откат транзакции
                await transaction.RollbackAsync();

                throw;
            }
        }

        public async Task SendOutboxMessages()
        {
            var messages = await _dbContext.OutboxMessages
                .Where(m => !m.IsSent)
                .ToListAsync();

            foreach (var message in messages)
            {
                // Логика отправки сообщения
                // Например, отправка в очередь сообщений

                // Если отправка успешна:
                message.IsSent = true;
            }

            await _dbContext.SaveChangesAsync();
        }
    }

    #endregion

    #region Распределенные транзакции

    /*    
        Осуществляется с использованием технологий:
        - Microsoft Distributed Transaction Coordinator (MSDTC)
        - ADO.NET

        Эти технологии позволяют управлять транзакциями, которые охватывают несколько БД

        Пример ADO.NET:
        - используем TransactionScope для создания распределенной транзакции
        - запустить на компьютере MSDTC
        - нужны 2 БД SQL Server

        -- В DatabaseA

            CREATE TABLE Users
            (
                Id INT
                    PRIMARY KEY IDENTITY,
                Name NVARCHAR(100)
            );

        -- В DatabaseB

            CREATE TABLE Logs
            (
                Id INT
                    PRIMARY KEY IDENTITY,
                UserId INT,
                Action NVARCHAR(100)
            );
    */

    class Program
    {
        static void Main_()
        {
            string connectionStringA = "Data Source=server;Initial Catalog=DatabaseA;Integrated Security=True;";
            string connectionStringB = "Data Source=server;Initial Catalog=DatabaseB;Integrated Security=True;";

            // Создаем область транзакции - все операции внутри будут частью одной транзакции
            using (var scope = new TransactionScope())
            {
                try
                {
                    // Вставка в первую БД
                    using (var connectionA = new SqlConnection(connectionStringA))
                    {
                        connectionA.Open();

                        using (var commandA = new SqlCommand("INSERT INTO Users (Name) VALUES (@Name);", connectionA))
                        {
                            commandA.Parameters.AddWithValue("@Name", "User1");
                            commandA.ExecuteNonQuery();
                        }
                    }

                    // Вставка во вторую БД
                    using (var connectionB = new SqlConnection(connectionStringB))
                    {
                        connectionB.Open();

                        using (var commandB = new SqlCommand("INSERT INTO Logs (UserId, Action) VALUES (@UserId, @Action);", connectionB))
                        {
                            // Предполагается, что пользователь с ID 1 был создан
                            commandB.Parameters.AddWithValue("@UserId", 1);
                            commandB.Parameters.AddWithValue("@Action", "Created User1");
                            commandB.ExecuteNonQuery();
                        }
                    }

                // Если все операции прошли успешно, вызывается scope.Complete(), чтобы зафиксировать транзакцию
                // Если возникает ошибка, транзакция будет отменена при выходе из блока using
                scope.Complete();
                    Console.WriteLine("Transaction completed successfully.");
                }
                catch (Exception ex)
                {
                    // Обработка ошибок
                    Console.WriteLine($"Transaction failed: {ex.Message}");
                }
            }
        }
    }

    #endregion

    #region InMemoryDb

    /*
    
        Используется в Entity Framework Core, когда нужно выполнять операции CRUD без необходимости настраивать полноценную БД

        Пакеты:
        - Microsoft.EntityFrameworkCore
        - Microsoft.EntityFrameworkCore.InMemory
     
    */

    public class Product
    {
        public int Id { get; init; }

        public string? Name { get; init; }

        public decimal Price { get; init; }
    }

    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; init; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
    }

    class A
    {
        static void Main_()
        {
            var serviceProvider = new ServiceCollection()
                //.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TestDatabase"))
                .BuildServiceProvider();

            using (var context = serviceProvider.GetService<AppDbContext>())
            {
                if (context is not null)
                {
                    // Сохранение данных
                    context.Products.Add(new Product { Name = "Product1", Price = 10.0m });
                    context.Products.Add(new Product { Name = "Product2", Price = 20.0m });

                    context.SaveChanges();
                }
            }

            using (var context = serviceProvider.GetService<AppDbContext>())
            {
                if (context is not null)
                {
                    // Получение данных
                    var products = context.Products.ToList();

                    foreach (var product in products)
                    {
                        Console.WriteLine($"{product.Id}, {product.Name}, {product.Price}");
                    }
                }
            }
        }
    }

    #endregion

    #region Когда использовать реляционные, а когда нереляционные БД

    /*
        Реляционные, если:
            
            Данные имеют четкую структуру и связи между сущностями
            Приложение требует сложных запросов с использованием SQL, таких как JOIN, агрегации и подзапросы
            Необходимо обеспечить атомарность и согласованность операций (ACID)
            У нас традиционное бизнес-приложение (CRM или ERP)
            Нужно создавать отчеты и проводить анализ данных

        Нереляционные, если:

            Данные не имеют фиксированной схемы или могут изменяться со временем (документы, JSON)
            Приложение требует высокой доступности и масштабируемости (для больших объемов данных или большого числа пользователей)
            Приложение требует высокой производительности при выполнении операций записи и чтения
            Работаем с большими объемами неструктурированных данных, такими как изображения или логи
            Команде требуется быстро вносить изменения в структуру данных
    */

    #endregion
}
