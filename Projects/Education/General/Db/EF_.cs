using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.General.Db;

public class EF_
{
    #region Transient Scoped Singleton

    /*
        Singleton
        Объект сервиса создается один раз при первом обращении к нему и живет всё время, пока запущено приложение
        Нужно следить за потокобезопасностью, т.к. к одному объекту могут одновременно обращаться разные потоки

        Scoped
        Чаще всего scope — это один HTTP-запрос
        Для каждого запроса создается объект сервиса
        Если в течение одного запроса есть несколько обращений к сервису - будет использован один и тот же объект

        Transient
        При каждом обращении к сервису создается новый объект сервиса
        Подходит для сервисов, которые не хранят данные об состоянии
        Самый безопасный с точки зрения многопоточности, так как у каждого потребителя свой экземпляр
        Большой расход памяти, т.к. если сервис внедряется в 10 разных мест внутри одного запроса, будет создано 10 разных объектов

        Нельзя внедрять сервис с коротким временем жизни в сервис с более длинным
        Нельзя инжектить Scoped-сервис в Singleton, т.к. Scoped-объект застрянет в синглтоне и не удалится после завершения запроса,
        что приведет к утечкам памяти или ошибкам доступа к БД
    */

    #endregion

    #region AsNoTracking

    /*
         По умолчанию EF Core отслеживает все загруженные сущности,
         сохраняя их снимки в памяти для последующего сравнения при вызове SaveChanges()
         AsNoTracking() указывает EF пропустить этот шаг, что:
         - улучшает производительность, когда загружаем данные, которые только читаем и не собираемся изменять
         - снижение использования памяти - EF не будет отслеживать изменения этих сущностей

         Использовать при:
         — везде, где только читаем и не меняем свойства объекта, и не вызываем context.SaveChanges()
         - в больших объемах данных

         Если загрузить объект через .AsNoTracking(), изменить его и вызвать SaveChanges(),
         изменения не сохранятся в базе, т.к. контекст не видит этих правок

         using (var context = new DbContext())
         {
          var products = context.Products.AsNoTracking().ToList();
         }
    */

    #endregion

    #region Middleware

    /*
         Конвейер обработки Middleware конфигурируется методами Use, Map, Run, важен порядок:
         - Use добавляет компоненты middleware в конвейер
         - Map применяется для сопоставления пути запроса с делегатом, который будет обрабатывать запрос по этому пути
         - Run - замыкающий метод

         Компоненты Middleware по умолчанию:
         - Authentication
         - Cookie Policy                     отслеживает согласие пользователя на хранение информации в куках
         - CORS                              поддержка кроссдоменных запросов
         - Diagnostics                       предоставляет страницы статус кодов, функционал обработки исключений
         - Forwarded Headers                 перенаправляет зголовки запроса
         - Health Check                      проверяет работоспособность приложения
         - HTTP Method Override              позволяет входящему POST - запросу переопределить метод
         - HTTPS Redirection                 перенаправляет все запросы HTTP на HTTPS
         - HTTP Strict Transport Security    для безопасности добавляет специальный заголовок ответа
         - MVC                               обеспечивает функционал MVC
         - Request Localization              обеспечивает поддержку локализации
         - Response Caching                  позволяет кэшировать результаты запросов
         - Response Compression              обеспечивает сжатие ответа клиенту
         - URL Rewrite                       предоставляет функциональность URL Rewriting
         - Endpoint Routing                  предоставляет механизм маршрутизации
         - Session                           предоставляет поддержку сессий
         - Static Files                      предоставляет поддержку обработки статических файлов
         - WebSockets                        добавляет поддержку протокола WebSockets

         Компоненты middleware создаются один раз и живут в течение жизнни приложения
         Вызываются дважды после каждого HTTP - запроса - когда запрос идет вниз по конвейеру и потом обратно вверх
    */

    #endregion

    #region Безопасное хранение паролей в БД

    /*
         Если просто захешировать пароль 123456,
         то для некоторых алгоримов хеширования есть список хешей, которым соответстуют простые пароли

         Поэтому к паролю добавляется соль и пароль хешируется
         123456 + salt -> vrt$%VTBW$

         Соль можно хранить в открытом виде в БД, должна быть уникальна для каждого пользователя

         Когда пользователь авторизуется, он:
         - вводит пароль
         - нажимает ввод и к парою прибавляется соль
         - полученная строка хешируется
         - полученный хеш сравнивается с тем, что находится в БД

         Сейчас для этих целей не используют быстрые хеши - они быстро перебираются видеокартами
         Используют медленные алгоритмы

         Соль не хранится в отдельной колонке БД, а записывается в одну строку вместе с хешем через разделитель
         При проверке алгоритм сам отрезает соль и использует её


         Быстрые хеши
         Позволяют быстро обработать большие объемы данных
         Современный компьютер может вычислять сотни миллионов таких хешей в секунду
         Если злоумышленник украдет базу данных с быстрыми хешами,
         он сможет перебрать типичные пароли за секунды

         Медленные алгоритмы
         Спроектированы так, чтобы быть ресурсоемкими
         Можно настроить алгоритм, чтобы проверка одного пароля занимала, например, 200–500 миллисекунд,
         чтобы защититься от алгоритмов перебора пароля
    */

    #endregion

    #region Атрибуты свойств и таблиц

        // Первичный ключ
        // В EF Core, если свойство называется Id или EntityNameId, оно станет первичным ключом и Identity (автоинкрементом) автоматически
        // Атрибут [Key] нужен, если имя свойства нестандартное (например, Prop_1)
        [Key]
        public int Prop_1 { get; set; }

        // Установить ключ в качестве идентификатора
        // По умолчанию [Key] для типа int уже подразумевает DatabaseGeneratedOption.Identity
        // Явное указание не ошибка, но избыточно
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Prop_2 { get; set; }

        // Обязательное св-во int, в БД будет помечено как NOT NULL
        [Range(1, 110, ErrorMessage = "Недопустимый возраст")]
        public int Prop_3 { get; set; }

        // Обязательное св-во string
        [Required(ErrorMessage = "Не указан электронный адрес")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        [EmailAddress] // Специальный атрибут для почты, чтобы не писать регулярное выражение
        [MinLength(10)]
        [MaxLength(20)]
        public string? Prop_4 { get; set; }

        // Не добавлять столбец в БД
        [NotMapped]
        public int Prop_5 { get; set; }

        // Установить внешний ключ для связи с другой сущностью
        [ForeignKey("CompId")]
        public int Prop_6 { get; set; }

        // Решение проблемы параллелизма, когда с одной же записью могут работать одновременно несколько пользователей
        [ConcurrencyCheck]
        public int Prop_7 { get; set; }

        // Если имя свойства и столбца разные, можно указать, с каким столбцом сопоставить свойство
        [Column("ModelName")]
        public int Prop_8 { get; set; }

        // Если имя модели и таблицы разные, можно указать, с какой таблицей сопоставить модель
        [Table("Mobiles")]
        public class Table_1
        {
        }

    #endregion

    #region InMemoryDatabase

        // Сейчас чаще используют SQLite In-Memory вместо InMemoryDatabase

        // Microsoft.EntityFrameworkCore
        // Microsoft.EntityFrameworkCore.InMemory

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
             : base(options)
            {
            }
        }

        private static void SaveData(ServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetService<AppDbContext>())
            {
                if (context is not null)
                {
                    context.Products.Add(new Product { Name = "Product1", Price = 10.0m });
                    context.Products.Add(new Product { Name = "Product2", Price = 20.0m });

                    context.SaveChanges();
                }
            }
        }

        private static void GetData(ServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetService<AppDbContext>())
            {
                if (context is not null)
                {
                    var products = context.Products.ToList();

                    foreach (var product in products)
                    {
                        Console.WriteLine($"{product.Id}, {product.Name}, {product.Price}");
                    }
                }
            }
        }

        public class A
        {
            public static void Main_()
            {
                var serviceProvider = new ServiceCollection()
                 .AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TestDatabase"))
                 .BuildServiceProvider();

                SaveData(serviceProvider);

                GetData(serviceProvider);
            }
        }

    #endregion

    #region Когда использовать реляционные, а когда нереляционные БД

    /*
         Реляционные SQL, если:

          Нужны транзакции
          Данные имеют четкую структуру и связи между сущностями
          Приложение требует сложных запросов с использованием SQL, таких как JOIN, агрегации и подзапросы
          Необходимо обеспечить атомарность и согласованность операций (ACID)
          Нужно создавать отчеты и проводить анализ данных

         Нереляционные NoSQL, если:

          Данные не имеют фиксированной схемы или могут изменяться со временем (документы, JSON)
          Приложение требует высокой доступности и масштабируемости (для больших объемов данных или большого числа пользователей)
          Приложение требует высокой производительности при выполнении операций записи и чтения
          Работаем с большими объемами неструктурированных данных, такими как изображения или логи
          Требуется быстро вносить изменения в структуру данных
    */

    #endregion
}
