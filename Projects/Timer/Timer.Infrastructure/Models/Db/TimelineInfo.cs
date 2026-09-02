namespace Timer.Infrastructure.Models.Db;

public class TimelineInfo
{
    public long Id { get; set; }

    public DateTime BirthDate { get; set; }

    public DateTime TimelineEndDate { get; set; }

    List<DateTime>? TimelineEvents { get; set; }

    List<TimelinePeriod_>? TimelinePeriods { get; set; }
}
