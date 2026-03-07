using Timer.Infrastructure.Interfaces;
using Timer.Infrastructure.Repositories;

namespace Timer.Presentation.Extensions;

public static class RepositoriesExtensions
{
    public static void AddRepositoriesExtensions(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ITimelineInfoRepository, TimelineInfoRepository>();
    }
}
