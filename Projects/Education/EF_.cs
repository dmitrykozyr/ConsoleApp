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

}
