namespace Timer.Infrastructure.Models.Db;

public class TimelineInfo
{
    public long Id { get; set; }

    public DateTime BirthDate { get; set; }

    public DateTime TimelineEndDate { get; set; }

    //! Вынести в объект
    List<DateTime>? TimelineEvents { get; set; }

    //! Вынести в объект
    List<TimelinePeriod_>? TimelinePeriods { get; set; }
}
