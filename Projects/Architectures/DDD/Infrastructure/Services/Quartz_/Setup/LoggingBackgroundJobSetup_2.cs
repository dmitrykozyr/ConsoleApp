using DDD.Infrastructure.Services.Quartz_.Job;
using Microsoft.Extensions.Options;
using Quartz;

namespace DDD.Infrastructure.Services.Quartz_.Setup;

public class LoggingBackgroundJobSetup_2 : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        var jobKey = JobKey.Create(nameof(LoggingBackgroundJob_2));

        options.AddJob<LoggingBackgroundJob_2>(jobBuilder => jobBuilder.WithIdentity(jobKey))
        .AddTrigger(trigger =>
            trigger
                .ForJob(jobKey)
                // Запуск шедулера с задержкой 5 секунд после старта программы, чтобы он точно запустился один раз, а не два
                .StartAt(DateBuilder.FutureDate(5, IntervalUnit.Second))
                .WithSimpleSchedule(schedule =>
                    schedule
                        .WithIntervalInSeconds(5)
                        .RepeatForever()));        
    }
}
