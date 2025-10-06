using Infrastructure.Quartz;
using Quartz;

namespace Presentation.Extensions;

public static class QuartzExtensions
{
    public static void AddQuartzExtension(this IServiceCollection serviceCollection, WebApplicationBuilder builder)
    {
        builder.Services.AddQuartz(options =>
        {
            options.UseMicrosoftDependencyInjectionJobFactory();

            var jobKey = JobKey.Create(nameof(LoggingBackgroundJob));

            options.AddJob<LoggingBackgroundJob>(jobKey)
            .AddTrigger(trigger =>
                trigger
                    .ForJob(jobKey)                     // Триггер, после которого запустится Jon
                    //.WithCronSchedule("*/1 * * * *")) // Cron-выражение, которое будет вызывать этот Job каждую минуту
                    .WithSimpleSchedule(schedule =>
                        schedule
                            .WithIntervalInSeconds(5)
                            .RepeatForever()));
        });

        builder.Services.AddQuartzHostedService(options =>
        {
            // Перед завершеним работы программа должна дождаться завершения джобов
            // В данном случае джоба простая - логирование выполняется быстро
            options.WaitForJobsToComplete = true;
        });
    }
}
