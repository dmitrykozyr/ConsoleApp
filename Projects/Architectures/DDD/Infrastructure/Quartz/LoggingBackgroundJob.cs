using Microsoft.Extensions.Logging;
using Quartz;

namespace Infrastructure.Quartz;

// Аттрибут говорит Quartz создавать только 1 экземпляр данной джобы
[DisallowConcurrentExecution]
public class LoggingBackgroundJob : IJob
{
    private readonly ILogger<LoggingBackgroundJob> _logger;

    public LoggingBackgroundJob(ILogger<LoggingBackgroundJob> logger)
    {
        _logger = logger;
    }

    // Этот метод будет вызываться по расписанию
    public Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("{UtcNow}", DateTime.UtcNow);

        return Task.CompletedTask;
    }
}
