public class Program
{
    /*
        Фичефлаги
        Domain Driven Design
        gRPC
        SOAP
        MediatR
        Quartz
        Saga Паттерн используется для управления распределенными транзакциями
        Билдер
        Виды индексов
        первые 3 нормальные формы
        IEnumerable, IEnumerator отложенное выполнение
        yield return, yield break
        интернирование строк, интерполяция
        примитивы синхронизации(ReadWriteLock, отличия slim версий)
        httpclient vs httpclientfactory

        // DTO должны быть immutable
        public Guid ImageLinkId { get; init; }

        // Фича флаг вместо конфигурации
        Вместо
	    'bool shouldGetDataFromVault = _configuration.GetValue<bool>("GetDataFromVault");'
	    использовать фича флаги через
	    'FeatureManagerExtensions.IsFeatureEnabled' или 'IFeatureManager'

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
    */

    static void Main()
    {
        
    }
}
