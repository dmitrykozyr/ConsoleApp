using Quartz;

namespace Domain.Services.Quartz_;

public class QuartzService
{
    private readonly ISchedulerFactory _schedulerFactory;

    public QuartzService(ISchedulerFactory schedulerFactory)
    {
        _schedulerFactory = schedulerFactory;
    }

    public async Task StartQuartzJob()
    {
        // Создаем планировщик
        IScheduler scheduler = await _schedulerFactory.GetScheduler();

        //await scheduler.Start();

        // Создаем задачу
        IJobDetail jobDetail = JobBuilder.Create<LoggingBackgroundJob>()
            .WithIdentity("myJob", "group1")
            .Build();

        // Создаем триггер, который будет запускать задачу каждую минуту
        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("myTrigger", "group1")
            .StartNow()
            .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(5) // Интервал в 1 минуту
                .RepeatForever()) // Повторять бесконечно
            .Build();

        // Запускаем задачу с триггером
        await scheduler.ScheduleJob(jobDetail, trigger);

        // Останавливаем планировщик перед выходом
        //await scheduler.Shutdown();
    }
}
