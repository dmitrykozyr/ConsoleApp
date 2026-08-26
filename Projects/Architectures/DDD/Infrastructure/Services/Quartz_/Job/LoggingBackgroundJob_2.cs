using Microsoft.Extensions.Logging;
using Quartz;

namespace DDD.Infrastructure.Services.Quartz_.Job;

// Аттрибут говорит Quartz создавать только 1 экземпляр данной джобы
[DisallowConcurrentExecution]
public class LoggingBackgroundJob_2 : IJob
{
    private readonly ILogger<LoggingBackgroundJob_2> _logger;

    public LoggingBackgroundJob_2(ILogger<LoggingBackgroundJob_2> logger)
    {
        _logger = logger;
    }

    // Этот метод будет вызываться по расписанию
    public Task Execute(IJobExecutionContext context)
    {
        //_logger.LogInformation("{UtcNow}", DateTime.UtcNow);

        return Task.CompletedTask;
    }
}
