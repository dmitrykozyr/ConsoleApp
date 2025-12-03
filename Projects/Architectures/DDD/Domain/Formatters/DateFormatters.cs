using CommunityToolkit.Diagnostics;
using Domain.Enums;
using System.Globalization;

namespace Domain.Formatters;

public static class DateFormatters
{
    private static readonly CultureInfo SpecificCultureInfo = CultureInfo.CreateSpecificCulture("ru-RU");

    private const string MM_dd_yyyy         = "MM/dd/yyyy";
    private const string yyyy_MM_dd         = "yyyyMMdd";
    private const string dd_MM_yyyy_H_mm_ss = "dd.MM.yyyy H:mm:ss";
    private const string dd_MM_yyyy         = "dd.MM.yyyy";
    private const string ddMMyyyy           = "dd.MM.yyyy";

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
                case var str when DateTime.TryParseExact(str, MM_dd_yyyy,           SpecificCultureInfo,            DateTimeStyles.None, out result):   break;
                case var str when DateTime.TryParseExact(str, yyyy_MM_dd,           SpecificCultureInfo,            DateTimeStyles.None, out result):   break;
                case var str when DateTime.TryParseExact(str, dd_MM_yyyy_H_mm_ss,   CultureInfo.InvariantCulture,   DateTimeStyles.None, out result):   break;
                default: throw new FormatException();
            }

            return result;
        }
    }

    public static DateTime GetDateTimeByFormatOrDefault(string dataString, DateFormats dateFormat = DateFormats.ISO8601)
    {
        if (string.IsNullOrEmpty(dataString))
        {
            return DateTime.MinValue;
        }
        else
        {
            DateTime result;

            switch (dateFormat)
            {
                case DateFormats.ISO8601:                   DateTime.TryParseExact(dataString, yyyy_MM_dd,          SpecificCultureInfo, DateTimeStyles.None, out result); break;
                case DateFormats.MMddyyyy:                  DateTime.TryParseExact(dataString, MM_dd_yyyy,          SpecificCultureInfo, DateTimeStyles.None, out result); break;
                case DateFormats.ddMMyyyyHmmss:             DateTime.TryParseExact(dataString, dd_MM_yyyy_H_mm_ss,  SpecificCultureInfo, DateTimeStyles.None, out result); break;
                case DateFormats.ddMMyyyy:                  DateTime.TryParseExact(dataString, dd_MM_yyyy,          SpecificCultureInfo, DateTimeStyles.None, out result); break;
                case DateFormats.ddMMyyyy_withoutSymbols:   DateTime.TryParseExact(dataString, ddMMyyyy,            SpecificCultureInfo, DateTimeStyles.None, out result); break;
                default:                                    throw new FormatException();
            }

            if (result == DateTime.MinValue)
            {
                throw new FormatException();
            }

            return result;
        }
    }

    public static DateTime LongToDateTime(long dateTimeSeconds)
    {
        DateTime dateTime   = DateTimeOffset.FromUnixTimeSeconds(dateTimeSeconds).DateTime;
        DateTime result     = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
        return result;
    }

    public static DateTime LongToDateTime(long dateTimeSeconds, DateTimeKind dateTimeKind)
    {
        DateTime dateTime   = DateTimeOffset.FromUnixTimeSeconds(dateTimeSeconds).DateTime;
        DateTime result     = DateTime.SpecifyKind(dateTime, dateTimeKind);
        return result;
    }

    public static long DateTimeToLong(DateTime dateTime)
    {
        Guard.IsTrue(dateTime.Kind != DateTimeKind.Unspecified);

        long result = new DateTimeOffset(dateTime).ToUnixTimeSeconds();

        return result;
    }

    public static long DateTimeToLong(DateTime dateTime, DateTimeKind dateTimeKind)
    {
        DateTime dateTimeWithKind   = DateTime.SpecifyKind(dateTime, dateTimeKind);
        long result                 = new DateTimeOffset(dateTimeWithKind).ToUnixTimeSeconds();

        return result;
    }

    public static string DateTimeToString(DateTime dateTime, string format)
    {
        string result = dateTime.ToString(format, SpecificCultureInfo);

        return result;
    }

    //!
    //public static string DateTimeToString(DateTime dateTime, DateFormats dateFormat = DateFormats.ISO8601)
    //{
    //    string result = dateTime.ToString(dateFormat.GetDesription(), SpecificCultureInfo);

    //    return result;
    //}
}
