public class Program
{
    //! Что нужно выучить
    /*
        Фичефлаги
        Вместо
	        'bool shouldGetDataFromVault = _configuration.GetValue<bool>("GetDataFromVault");'
	    использовать фича флаги через
	        'FeatureManagerExtensions.IsFeatureEnabled' или 'IFeatureManager'
        https://learn.microsoft.com/en-us/azure/azure-app-configuration/feature-management-dotnet-reference

        Domain Driven Design
        
        gRPC
    
        SOAP
    
        MediatR
    
        Quartz

        Уровни изоляции транзакций в БД
        https://habr.com/ru/articles/845522/

        ConfigureAwait
        https://habr.com/ru/articles/482354/

        Async/await
        https://habr.com/ru/articles/470830/

        Saga Паттерн используется для управления распределенными транзакциями

        Monitor

        микросервисы сервисориентированная архитектура

        IEnumerable, IEnumerator отложенное выполнение
    
        yield return, yield break
    
        примитивы синхронизации(ReadWriteLock, отличия slim версий)
    
        httpclientfactory

        DTO должны быть immutable
        public Guid ImageLinkId { get; init; }

	    // Так можно не делать метод асинхронным, чтобы получить асинхронный результат
	    {
		    // Task.Run - создает новый поток из пула потоков
		    // ConfigureAwait(true) гарантирует продолжение выполнения в том-же контексте синхронизации
		    // GetAwaiter().GetResult() - блокирует выполнение до завершения задачи
		    _merchantsService.GetMerchantsListAsync("", context.Token)
		    .IfFailThrow()
		    .ConfigureAwait(true)
		    .GetAwaiter()
		    .GetResult();
        }

        // Когда есть IServiceScopeFactory, зависимости запрашиваем через него
        using (IServiceScope scope = _serviceScopeFactory.CreateScope())
        {
            var fileRepository = scope.ServiceProvider.GetRequiredService<IDbFileRepository>();
        }

        // Логировать ошибки так:
        _logger.LogError(e, "Загрузка Excel не было произведена");

        'DateTime.Now' не используем, вметсо него '_dateProvider.CurrentDateNow'

        unit of work
    */

    static void Main()
    {
        
    }
}
