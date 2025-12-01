using System.Globalization;

namespace Domain.Formatters;

public static class DateFormatters
{
    private static readonly CultureInfo SpecificCultureInfo = CultureInfo.CreateSpecificCulture("ru-RU");

    public static DateTime DateTimeNow()
    {
        //return DateTime.UtcNow;
        return DateTime.Now;
    }

    public static DateTime GetDateTimeOrDefault(string dataString)
    {
        if (string.IsNullOrEmpty(dataString))
        {
            return DateTime.MinValue;
        }
        else
        {
            DateTime result;

            switch (dataString)
            {
                case var str when DateTime.TryParse(str, out result):                                                                                   break;
                case var str when DateTime.TryParseExact(str, "MM/dd/yyyy",         SpecificCultureInfo,            DateTimeStyles.None, out result):   break;
                case var str when DateTime.TryParseExact(str, "yyyyMMdd",           SpecificCultureInfo,            DateTimeStyles.None, out result):   break;
                case var str when DateTime.TryParseExact(str, "dd.MM.yyyy H:mm:ss", CultureInfo.InvariantCulture,   DateTimeStyles.None, out result):   break;
                default: throw new FormatException();
            }

            return result;
        }
    }
}
