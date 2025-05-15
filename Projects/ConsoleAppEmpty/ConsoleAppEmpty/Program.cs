public class Program
{
    //!
    /*
        gRPC
    
        SOAP
    
        MediatR
    
        Quartz

        JSONb, blob

        DTO должны быть immutable
        public Guid ImageLinkId { get; init; }

	    // Так можно не делать метод асинхронным, чтобы получить асинхронный результат
	    {
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
