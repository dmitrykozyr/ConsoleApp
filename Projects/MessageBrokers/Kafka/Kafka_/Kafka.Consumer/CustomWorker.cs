using Confluent.Kafka;

namespace Kafka.Consumer;

public class CustomWorker : BackgroundService
{
    private readonly ILogger<CustomWorker> _logger;
    private readonly IConsumer<Ignore, string> _consumer;

    public CustomWorker(ILogger<CustomWorker> logger)
    {
        _logger = logger;
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "my-consumer-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() =>
        {
            _consumer.Subscribe("test-topic");

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    // Ожидаем сообщение
                    var result = _consumer.Consume(stoppingToken);
                    _logger.LogInformation($"ПОЛУЧЕНО: {result.Message.Value}");
                }
            }
            catch (OperationCanceledException)
            {
                _consumer.Close();
            }
        }, stoppingToken);
    }
}
