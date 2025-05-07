using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Education;

public class EF_
{
    #region Transient Scoped Singleton
    /*
        - Transient  При каждом обращении к сервису создается новый объект сервиса
                     Подходит для сервисов, которые не хранят данные об состоянии

        - Scoped     Для каждого запроса создается объект сервиса
                     Если в течение одного запроса есть несколько обращений к сервису - будет использован один и тот же объект

        - Singleton  Объект сервиса создается один раз при первом обращении к нему
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

    #region Настройка конфигурации
    /*
        static IHostBuilder CreateHostBuilder(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((appConfiguration) =>
                {
                    // Подключение файла конфигурации JSON
                    appConfiguration.AddJsonFile("settings.json", false, true);

                    // Подключение файла конфигурации XML
                    appConfiguration.AddXmlFile("settings.xml", false, true);
                })
                .ConfigureDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                .UseNLog();
        }

        // Примеры, как получить данные из конфигурации в другом месте программы
        public string GetConfigJSON()
        {
            return _confuguration["settings:SomeConfiguration"];
        }

        public string GetConfigXML()
        {
            return _confuguration["SomeConfiguration1"];
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

    // Хеширование паролей

    // Добавление соли
    /*
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
            public int Id { get; set; }

            public string? Name { get; set; }

            public int Age { get; set; }

            public decimal MoneyAmount { get; set; }

            public UserProfile? UserProfile { get; set; }
        }

        public class UserProfile
        {
            [Key]
            [ForeignKey("User")]
            public int Id { get; set; }

            public string? Login { get; set; }

            public string? Password { get; set; }

            public User? User { get; set; }
        }

    #endregion

    #region Один ко многим

        public class Player
        {
            public int Id { get; set; }

            public string? Name { get; set; }

            public int TeamId { get; set; }

            public Team? Team { get; set; }
        }

        public class Team
        {
            public int Id { get; set; }

            public string TeamName { get; set; }

            public ICollection<Player> Players { get; set; }

            public Team()
            {
                Players = new List<Player>();
            }
        }

    #endregion

    #region Многие ко многим

        public class Team_
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public ICollection<Player_> Players { get; set; }

            public Team_()
            {
                Players = new List<Player_>();
            }
        }

        public class Player_
        {
            public int Id { get; set; }

            public string TeamName { get; set; }

            public ICollection<Team_> Teams { get; set; }

            public Player_()
            {
                Teams = new List<Team_>();
            }
        }

    #endregion

}
