using DDD.Infrastructure.Services.Quartz_.Job;
using Microsoft.Extensions.Options;
using Quartz;

namespace DDD.Infrastructure.Services.Quartz_.Setup;

public class LoggingBackgroundJobSetup : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        var jobKey = JobKey.Create(nameof(LoggingBackgroundJob));

        options.AddJob<LoggingBackgroundJob>(jobBuilder => jobBuilder.WithIdentity(jobKey))
        .AddTrigger(trigger =>
            trigger
                .ForJob(jobKey) // Триггер, после которого запустится Job
                                //.WithCronSchedule("*/1 * * * *")) // Cron-выражение, которое будет вызывать этот Job каждую минуту
                .WithSimpleSchedule(schedule =>
                    schedule
                        .WithIntervalInSeconds(5)
                        .RepeatForever()));
    }
}
