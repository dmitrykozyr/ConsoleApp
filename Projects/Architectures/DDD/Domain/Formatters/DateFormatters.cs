namespace Domain.Formatters;

public static class DateFormatters
{
    public static DateTime DateTimeNow()
    {
        //return DateTime.UtcNow;
        return DateTime.Now;
    }
}
