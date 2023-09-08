namespace Utilities;

using System;
using System.Linq;
using System.Globalization;

public static class CommonHelper
{
    public static string FormatDateTimeStrings(string date, string time)
    {
        CultureInfo.CurrentCulture = new CultureInfo("en-AU");
        if (DateTime.TryParse(date + " " + time, out var parsedDate))
            return parsedDate.ToString("d/M/yyyy, h:mm tt").ToLower();
            
        return "";
    }

    public static string FormatDateTimeStrings(string dateTime)
    {
        CultureInfo.CurrentCulture = new CultureInfo("en-AU");
        if (DateTime.TryParse(dateTime, out var parsedDate))
            return parsedDate.ToString("d/M/yyyy, h:mm tt");

        return "";
    }

    public static string FormatDateStringsWithFormat(string date, string format)
    {
        CultureInfo.CurrentCulture = new CultureInfo("en-AU");
        if (DateTime.TryParse(date, out var parsedDate))
            return parsedDate.ToString(format).ToLower();
            
        return "";
    }

    public static string AddDaysToDateString(string date, int days)
    {
        if (DateTime.TryParse(date, out var parsedDate))
        {
            parsedDate = parsedDate.AddDays(days);
            return parsedDate.ToString("d/M/yyyy").ToLower();
        }
            
        return "";
    }

    public static string GetRandomEmail(Random rnd)
    {
        return rnd.Next(1, 99999).ToString("00000") + "@health.vic.gov.au";
    }

    public static string GetRandomMobile(Random rnd)
    {
        return "04010" + rnd.Next(1, 9999).ToString("0000");
    }

    public static string GetRandomBirthDate(Random rnd)
    {
        return string.Format("{0}/{1}/19{2}", rnd.Next(1, 29).ToString("00"), rnd.Next(1, 13).ToString("00"), rnd.Next(50, 99));
    }

    public static string GetRandomName(int length, Random rnd)
    {
        const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
        var capital = new string(Enumerable.Repeat(upperChars, 1).Select(s => s[rnd.Next(s.Length)]).ToArray());
        return capital + new string(Enumerable.Repeat(lowerChars, length-1).Select(s => s[rnd.Next(s.Length)]).ToArray());
    }
}
