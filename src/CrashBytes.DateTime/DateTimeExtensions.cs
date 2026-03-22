using System.Globalization;
using SystemDateTime = System.DateTime;

namespace CrashBytes.DateTime;

/// <summary>
/// Extension methods for <see cref="SystemDateTime"/>.
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// Returns a new <see cref="SystemDateTime"/> with the time set to 00:00:00.0000000 (start of day).
    /// </summary>
    /// <param name="dateTime">The source date/time.</param>
    /// <returns>A <see cref="SystemDateTime"/> representing the start of the day.</returns>
    public static SystemDateTime StartOfDay(this SystemDateTime dateTime) =>
        dateTime.Date;

    /// <summary>
    /// Returns a new <see cref="SystemDateTime"/> with the time set to 23:59:59.9999999 (end of day).
    /// </summary>
    /// <param name="dateTime">The source date/time.</param>
    /// <returns>A <see cref="SystemDateTime"/> representing the end of the day.</returns>
    public static SystemDateTime EndOfDay(this SystemDateTime dateTime) =>
        dateTime.Date.AddDays(1).AddTicks(-1);

    /// <summary>
    /// Returns the first day of the week containing this date.
    /// </summary>
    /// <param name="dateTime">The source date/time.</param>
    /// <param name="startOfWeek">The day considered the start of the week (default: Monday).</param>
    /// <returns>A <see cref="SystemDateTime"/> representing the start of the week.</returns>
    public static SystemDateTime StartOfWeek(this SystemDateTime dateTime, DayOfWeek startOfWeek = DayOfWeek.Monday)
    {
        int diff = ((int)dateTime.DayOfWeek - (int)startOfWeek + 7) % 7;
        return dateTime.Date.AddDays(-diff);
    }

    /// <summary>
    /// Returns the last day of the week containing this date.
    /// </summary>
    /// <param name="dateTime">The source date/time.</param>
    /// <param name="startOfWeek">The day considered the start of the week (default: Monday).</param>
    /// <returns>A <see cref="SystemDateTime"/> representing the end of the week (end of the last day).</returns>
    public static SystemDateTime EndOfWeek(this SystemDateTime dateTime, DayOfWeek startOfWeek = DayOfWeek.Monday) =>
        dateTime.StartOfWeek(startOfWeek).AddDays(6).EndOfDay();

    /// <summary>
    /// Returns the first day of the month.
    /// </summary>
    /// <param name="dateTime">The source date/time.</param>
    /// <returns>A <see cref="SystemDateTime"/> representing the first day of the month.</returns>
    public static SystemDateTime StartOfMonth(this SystemDateTime dateTime) =>
        new(dateTime.Year, dateTime.Month, 1, 0, 0, 0, dateTime.Kind);

    /// <summary>
    /// Returns the last day of the month with time set to end of day.
    /// </summary>
    /// <param name="dateTime">The source date/time.</param>
    /// <returns>A <see cref="SystemDateTime"/> representing the last moment of the month.</returns>
    public static SystemDateTime EndOfMonth(this SystemDateTime dateTime) =>
        new SystemDateTime(dateTime.Year, dateTime.Month, SystemDateTime.DaysInMonth(dateTime.Year, dateTime.Month), 0, 0, 0, dateTime.Kind).EndOfDay();

    /// <summary>
    /// Returns January 1 of the year.
    /// </summary>
    /// <param name="dateTime">The source date/time.</param>
    /// <returns>A <see cref="SystemDateTime"/> representing the first day of the year.</returns>
    public static SystemDateTime StartOfYear(this SystemDateTime dateTime) =>
        new(dateTime.Year, 1, 1, 0, 0, 0, dateTime.Kind);

    /// <summary>
    /// Returns December 31 of the year with time set to end of day.
    /// </summary>
    /// <param name="dateTime">The source date/time.</param>
    /// <returns>A <see cref="SystemDateTime"/> representing the last moment of the year.</returns>
    public static SystemDateTime EndOfYear(this SystemDateTime dateTime) =>
        new SystemDateTime(dateTime.Year, 12, 31, 0, 0, 0, dateTime.Kind).EndOfDay();

    /// <summary>
    /// Returns the first day of the calendar quarter (Q1=Jan, Q2=Apr, Q3=Jul, Q4=Oct).
    /// </summary>
    /// <param name="dateTime">The source date/time.</param>
    /// <returns>A <see cref="SystemDateTime"/> representing the first day of the quarter.</returns>
    public static SystemDateTime StartOfQuarter(this SystemDateTime dateTime)
    {
        int quarterMonth = ((dateTime.Month - 1) / 3) * 3 + 1;
        return new SystemDateTime(dateTime.Year, quarterMonth, 1, 0, 0, 0, dateTime.Kind);
    }

    /// <summary>
    /// Returns the last day of the calendar quarter with time set to end of day.
    /// </summary>
    /// <param name="dateTime">The source date/time.</param>
    /// <returns>A <see cref="SystemDateTime"/> representing the last moment of the quarter.</returns>
    public static SystemDateTime EndOfQuarter(this SystemDateTime dateTime)
    {
        int quarterMonth = ((dateTime.Month - 1) / 3) * 3 + 3;
        return new SystemDateTime(dateTime.Year, quarterMonth, SystemDateTime.DaysInMonth(dateTime.Year, quarterMonth), 0, 0, 0, dateTime.Kind).EndOfDay();
    }

    /// <summary>
    /// Returns the calendar quarter (1-4) for this date.
    /// </summary>
    /// <param name="dateTime">The source date/time.</param>
    /// <returns>An integer from 1 to 4 representing the calendar quarter.</returns>
    public static int Quarter(this SystemDateTime dateTime) =>
        (dateTime.Month - 1) / 3 + 1;

    /// <summary>
    /// Determines whether the date falls on a weekend (Saturday or Sunday).
    /// </summary>
    /// <param name="dateTime">The source date/time.</param>
    /// <returns><c>true</c> if the date is Saturday or Sunday; otherwise, <c>false</c>.</returns>
    public static bool IsWeekend(this SystemDateTime dateTime) =>
        dateTime.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

    /// <summary>
    /// Determines whether the date falls on a weekday (Monday through Friday).
    /// </summary>
    /// <param name="dateTime">The source date/time.</param>
    /// <returns><c>true</c> if the date is Monday through Friday; otherwise, <c>false</c>.</returns>
    public static bool IsWeekday(this SystemDateTime dateTime) =>
        !dateTime.IsWeekend();

    /// <summary>
    /// Determines whether the year of this date is a leap year.
    /// </summary>
    /// <param name="dateTime">The source date/time.</param>
    /// <returns><c>true</c> if the year is a leap year; otherwise, <c>false</c>.</returns>
    public static bool IsLeapYear(this SystemDateTime dateTime) =>
        SystemDateTime.IsLeapYear(dateTime.Year);

    /// <summary>
    /// Adds the specified number of business days (skipping weekends) to this date.
    /// Negative values subtract business days.
    /// </summary>
    /// <param name="dateTime">The source date/time.</param>
    /// <param name="days">The number of business days to add (can be negative).</param>
    /// <returns>A <see cref="SystemDateTime"/> offset by the specified number of business days.</returns>
    public static SystemDateTime AddBusinessDays(this SystemDateTime dateTime, int days)
    {
        int direction = days < 0 ? -1 : 1;
        int remaining = Math.Abs(days);
        var result = dateTime;

        while (remaining > 0)
        {
            result = result.AddDays(direction);
            if (result.IsWeekday())
            {
                remaining--;
            }
        }

        return result;
    }

    /// <summary>
    /// Determines whether this date is a business day (weekday). No holiday awareness.
    /// </summary>
    /// <param name="dateTime">The source date/time.</param>
    /// <returns><c>true</c> if the date is a weekday; otherwise, <c>false</c>.</returns>
    public static bool IsBusinessDay(this SystemDateTime dateTime) =>
        dateTime.IsWeekday();

    /// <summary>
    /// Determines whether this date is between <paramref name="start"/> and <paramref name="end"/> (inclusive).
    /// </summary>
    /// <param name="dateTime">The source date/time.</param>
    /// <param name="start">The start of the range (inclusive).</param>
    /// <param name="end">The end of the range (inclusive).</param>
    /// <returns><c>true</c> if this date is within the range; otherwise, <c>false</c>.</returns>
    public static bool IsBetween(this SystemDateTime dateTime, SystemDateTime start, SystemDateTime end) =>
        dateTime >= start && dateTime <= end;

    /// <summary>
    /// Calculates the age in whole years from this date to <see cref="SystemDateTime.Today"/>.
    /// </summary>
    /// <param name="dateTime">The birth date.</param>
    /// <returns>The age in whole years.</returns>
    public static int Age(this SystemDateTime dateTime)
    {
        var today = SystemDateTime.Today;
        int age = today.Year - dateTime.Year;
        if (dateTime.Date > today.AddYears(-age))
        {
            age--;
        }
        return age;
    }

    /// <summary>
    /// Returns the ISO 8601 week number for this date.
    /// </summary>
    /// <param name="dateTime">The source date/time.</param>
    /// <returns>The ISO 8601 week number (1-53).</returns>
    public static int WeekOfYear(this SystemDateTime dateTime) =>
        ISOWeek.GetWeekOfYear(dateTime);

    /// <summary>
    /// Converts this <see cref="SystemDateTime"/> to a Unix timestamp (seconds since 1970-01-01T00:00:00Z).
    /// </summary>
    /// <param name="dateTime">The source date/time.</param>
    /// <returns>The number of seconds since the Unix epoch.</returns>
    public static long ToUnixTimestamp(this SystemDateTime dateTime) =>
        new DateTimeOffset(dateTime.ToUniversalTime()).ToUnixTimeSeconds();

    /// <summary>
    /// Returns a human-readable relative time string such as "just now", "5 minutes ago", or "in 3 hours".
    /// </summary>
    /// <param name="dateTime">The source date/time.</param>
    /// <returns>A relative time string.</returns>
    public static string ToRelativeString(this SystemDateTime dateTime) =>
        ToRelativeString(dateTime, SystemDateTime.Now);

    /// <summary>
    /// Returns a human-readable relative time string relative to <paramref name="relativeTo"/>.
    /// </summary>
    /// <param name="dateTime">The source date/time.</param>
    /// <param name="relativeTo">The reference point in time.</param>
    /// <returns>A relative time string.</returns>
    public static string ToRelativeString(this SystemDateTime dateTime, SystemDateTime relativeTo)
    {
        var span = relativeTo - dateTime;
        bool isFuture = span.TotalSeconds < 0;
        var absSpan = isFuture ? -span : span;

        string text;

        if (absSpan.TotalSeconds < 60)
        {
            text = "just now";
            return text;
        }
        else if (absSpan.TotalMinutes < 60)
        {
            int minutes = (int)absSpan.TotalMinutes;
            text = minutes == 1 ? "1 minute" : $"{minutes} minutes";
        }
        else if (absSpan.TotalHours < 24)
        {
            int hours = (int)absSpan.TotalHours;
            text = hours == 1 ? "1 hour" : $"{hours} hours";
        }
        else if (absSpan.TotalDays < 30)
        {
            int days = (int)absSpan.TotalDays;
            text = days == 1 ? "1 day" : $"{days} days";
        }
        else if (absSpan.TotalDays < 365)
        {
            int months = (int)(absSpan.TotalDays / 30);
            text = months == 1 ? "1 month" : $"{months} months";
        }
        else
        {
            int years = (int)(absSpan.TotalDays / 365);
            text = years == 1 ? "1 year" : $"{years} years";
        }

        return isFuture ? $"in {text}" : $"{text} ago";
    }
}
