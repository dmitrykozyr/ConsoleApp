using Microsoft.Extensions.Options;
using Quartz;

namespace Domain.Services.Quartz_;

public class LoggingBackgroundJobSetup : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        var jobKey = JobKey.Create(nameof(LoggingBackgroundJob));

        options.AddJob<LoggingBackgroundJob>(jobBuilder => jobBuilder.WithIdentity(jobKey))
        .AddTrigger(trigger =>
            trigger
                .ForJob(jobKey) // Триггер, после которого запустится Jon
                                //.WithCronSchedule("*/1 * * * *")) // Cron-выражение, которое будет вызывать этот Job каждую минуту
                .WithSimpleSchedule(schedule =>
                    schedule
                        .WithIntervalInSeconds(5)
                        .RepeatForever()));
    }
}
