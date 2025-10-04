public class Program
{
    //!
    /*
        Настроить в docker prod-версию vault
        GitLab build, tests, GitLab Runner
        AutoMapper
        Kubernetes
        Prometheus
        Grafana,
        PostgreSQL
        ClickHouse
        Oracle
        Kafka
        RabbitMQ
        NATS
        gRPC
        SOAP
        MediatR
        Шедулеры Quartz
        Виртуальная БД
        JSONb, blob
        Балансировщик
        Api Gateway
        Docker Hub
        Logger Elastic
        Архитектура мессенджера, выбор БД


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
    

        В контексте Entity Framework (EF Core),
        DbContext часто выступает как реализация паттернов "Репозиторий" и "Единица работы" (Unit of Work)
    */

    static void Main()
    {
    }
}
