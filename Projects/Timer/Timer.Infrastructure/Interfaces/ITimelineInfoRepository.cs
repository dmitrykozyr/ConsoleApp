namespace Timer.Infrastructure.Interfaces;

public interface ITimelineInfoRepository
{
    Task<string> GetDatabaseVersionAsync();

    Task AddTimerEntry();
}
