using Application.Services.Quartz_;
using Quartz;

namespace Presentation.Extensions;

public static class QuartzExtensions
{
    public static void AddQuartzExtension(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddQuartz();

        serviceCollection.AddQuartzHostedService(options =>
        {
            // Перед завершеним работы программа должна дождаться завершения джобов
            // В данном случае джоба простая - логирование выполняется быстро
            options.WaitForJobsToComplete = true;
        });

        serviceCollection.ConfigureOptions<LoggingBackgroundJobSetup>();
    }
}
