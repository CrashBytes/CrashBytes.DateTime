using SystemDateTime = System.DateTime;

namespace CrashBytes.DateTime.Tests;

public class StartOfDayTests
{
    [Fact]
    public void StartOfDay_ReturnsDateWithMidnightTime()
    {
        var dt = new SystemDateTime(2024, 6, 15, 14, 30, 45, DateTimeKind.Local);
        var result = dt.StartOfDay();
        Assert.Equal(new SystemDateTime(2024, 6, 15, 0, 0, 0, DateTimeKind.Local), result);
    }

    [Fact]
    public void StartOfDay_AlreadyMidnight_ReturnsSame()
    {
        var dt = new SystemDateTime(2024, 6, 15, 0, 0, 0, DateTimeKind.Utc);
        Assert.Equal(dt, dt.StartOfDay());
    }
}

public class EndOfDayTests
{
    [Fact]
    public void EndOfDay_ReturnsLastTickOfDay()
    {
        var dt = new SystemDateTime(2024, 6, 15, 10, 0, 0, DateTimeKind.Local);
        var result = dt.EndOfDay();
        Assert.Equal(new SystemDateTime(2024, 6, 15, 23, 59, 59, DateTimeKind.Local).AddTicks(9999999), result);
    }

    [Fact]
    public void EndOfDay_AlreadyEndOfDay_ReturnsSame()
    {
        var dt = new SystemDateTime(2024, 6, 15, 0, 0, 0, DateTimeKind.Utc).AddDays(1).AddTicks(-1);
        Assert.Equal(dt, dt.EndOfDay());
    }
}

public class StartOfWeekTests
{
    [Fact]
    public void StartOfWeek_DefaultMonday_ReturnsMonday()
    {
        // Wednesday June 19, 2024
        var dt = new SystemDateTime(2024, 6, 19, 14, 30, 0, DateTimeKind.Local);
        var result = dt.StartOfWeek();
        Assert.Equal(new SystemDateTime(2024, 6, 17, 0, 0, 0, DateTimeKind.Local), result);
        Assert.Equal(DayOfWeek.Monday, result.DayOfWeek);
    }

    [Fact]
    public void StartOfWeek_Sunday_ReturnsSunday()
    {
        // Wednesday June 19, 2024
        var dt = new SystemDateTime(2024, 6, 19, 14, 30, 0, DateTimeKind.Local);
        var result = dt.StartOfWeek(DayOfWeek.Sunday);
        Assert.Equal(new SystemDateTime(2024, 6, 16, 0, 0, 0, DateTimeKind.Local), result);
        Assert.Equal(DayOfWeek.Sunday, result.DayOfWeek);
    }

    [Fact]
    public void StartOfWeek_OnStartDay_ReturnsSameDate()
    {
        // Monday June 17, 2024
        var dt = new SystemDateTime(2024, 6, 17, 10, 0, 0, DateTimeKind.Local);
        var result = dt.StartOfWeek(DayOfWeek.Monday);
        Assert.Equal(new SystemDateTime(2024, 6, 17, 0, 0, 0, DateTimeKind.Local), result);
    }
}

public class EndOfWeekTests
{
    [Fact]
    public void EndOfWeek_DefaultMonday_ReturnsSundayEndOfDay()
    {
        // Wednesday June 19, 2024
        var dt = new SystemDateTime(2024, 6, 19, 14, 30, 0, DateTimeKind.Local);
        var result = dt.EndOfWeek();
        Assert.Equal(DayOfWeek.Sunday, result.DayOfWeek);
        Assert.Equal(23, result.Hour);
        Assert.Equal(59, result.Minute);
        Assert.Equal(59, result.Second);
    }

    [Fact]
    public void EndOfWeek_SundayStart_ReturnsSaturdayEndOfDay()
    {
        // Wednesday June 19, 2024
        var dt = new SystemDateTime(2024, 6, 19, 14, 30, 0, DateTimeKind.Local);
        var result = dt.EndOfWeek(DayOfWeek.Sunday);
        Assert.Equal(DayOfWeek.Saturday, result.DayOfWeek);
    }
}

public class StartOfMonthTests
{
    [Fact]
    public void StartOfMonth_ReturnsFirstDayOfMonth()
    {
        var dt = new SystemDateTime(2024, 6, 15, 14, 30, 0, DateTimeKind.Utc);
        var result = dt.StartOfMonth();
        Assert.Equal(new SystemDateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc), result);
    }

    [Fact]
    public void StartOfMonth_AlreadyFirstDay_ReturnsSameDate()
    {
        var dt = new SystemDateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc);
        Assert.Equal(dt, dt.StartOfMonth());
    }

    [Fact]
    public void StartOfMonth_PreservesKind()
    {
        var dt = new SystemDateTime(2024, 6, 15, 0, 0, 0, DateTimeKind.Utc);
        Assert.Equal(DateTimeKind.Utc, dt.StartOfMonth().Kind);
    }
}

public class EndOfMonthTests
{
    [Fact]
    public void EndOfMonth_ReturnsLastDayEndOfDay()
    {
        var dt = new SystemDateTime(2024, 6, 15, 14, 30, 0, DateTimeKind.Utc);
        var result = dt.EndOfMonth();
        Assert.Equal(30, result.Day);
        Assert.Equal(23, result.Hour);
        Assert.Equal(59, result.Minute);
    }

    [Fact]
    public void EndOfMonth_February_LeapYear_Returns29()
    {
        var dt = new SystemDateTime(2024, 2, 10, 0, 0, 0, DateTimeKind.Utc);
        Assert.Equal(29, dt.EndOfMonth().Day);
    }

    [Fact]
    public void EndOfMonth_February_NonLeapYear_Returns28()
    {
        var dt = new SystemDateTime(2023, 2, 10, 0, 0, 0, DateTimeKind.Utc);
        Assert.Equal(28, dt.EndOfMonth().Day);
    }

    [Fact]
    public void EndOfMonth_December_Returns31()
    {
        var dt = new SystemDateTime(2024, 12, 5, 0, 0, 0, DateTimeKind.Utc);
        Assert.Equal(31, dt.EndOfMonth().Day);
    }
}

public class StartOfYearTests
{
    [Fact]
    public void StartOfYear_ReturnsJanuary1()
    {
        var dt = new SystemDateTime(2024, 6, 15, 14, 30, 0, DateTimeKind.Utc);
        var result = dt.StartOfYear();
        Assert.Equal(new SystemDateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), result);
    }
}

public class EndOfYearTests
{
    [Fact]
    public void EndOfYear_ReturnsDecember31EndOfDay()
    {
        var dt = new SystemDateTime(2024, 6, 15, 14, 30, 0, DateTimeKind.Utc);
        var result = dt.EndOfYear();
        Assert.Equal(12, result.Month);
        Assert.Equal(31, result.Day);
        Assert.Equal(23, result.Hour);
        Assert.Equal(59, result.Minute);
    }
}

public class StartOfQuarterTests
{
    [Theory]
    [InlineData(1, 1)]   // Q1 -> January
    [InlineData(2, 1)]
    [InlineData(3, 1)]
    [InlineData(4, 4)]   // Q2 -> April
    [InlineData(5, 4)]
    [InlineData(6, 4)]
    [InlineData(7, 7)]   // Q3 -> July
    [InlineData(8, 7)]
    [InlineData(9, 7)]
    [InlineData(10, 10)] // Q4 -> October
    [InlineData(11, 10)]
    [InlineData(12, 10)]
    public void StartOfQuarter_ReturnsFirstMonthOfQuarter(int month, int expectedMonth)
    {
        var dt = new SystemDateTime(2024, month, 15, 0, 0, 0, DateTimeKind.Utc);
        var result = dt.StartOfQuarter();
        Assert.Equal(expectedMonth, result.Month);
        Assert.Equal(1, result.Day);
    }
}

public class EndOfQuarterTests
{
    [Theory]
    [InlineData(1, 3, 31)]   // Q1 -> March 31
    [InlineData(4, 6, 30)]   // Q2 -> June 30
    [InlineData(7, 9, 30)]   // Q3 -> September 30
    [InlineData(10, 12, 31)] // Q4 -> December 31
    public void EndOfQuarter_ReturnsLastDayOfQuarter(int month, int expectedMonth, int expectedDay)
    {
        var dt = new SystemDateTime(2024, month, 15, 0, 0, 0, DateTimeKind.Utc);
        var result = dt.EndOfQuarter();
        Assert.Equal(expectedMonth, result.Month);
        Assert.Equal(expectedDay, result.Day);
        Assert.Equal(23, result.Hour);
    }
}

public class QuarterTests
{
    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 1)]
    [InlineData(3, 1)]
    [InlineData(4, 2)]
    [InlineData(5, 2)]
    [InlineData(6, 2)]
    [InlineData(7, 3)]
    [InlineData(8, 3)]
    [InlineData(9, 3)]
    [InlineData(10, 4)]
    [InlineData(11, 4)]
    [InlineData(12, 4)]
    public void Quarter_ReturnsCorrectQuarter(int month, int expectedQuarter)
    {
        var dt = new SystemDateTime(2024, month, 1, 0, 0, 0, DateTimeKind.Utc);
        Assert.Equal(expectedQuarter, dt.Quarter());
    }
}

public class IsWeekendTests
{
    [Theory]
    [InlineData(2024, 6, 15, true)]  // Saturday
    [InlineData(2024, 6, 16, true)]  // Sunday
    [InlineData(2024, 6, 17, false)] // Monday
    [InlineData(2024, 6, 18, false)] // Tuesday
    [InlineData(2024, 6, 19, false)] // Wednesday
    [InlineData(2024, 6, 20, false)] // Thursday
    [InlineData(2024, 6, 21, false)] // Friday
    public void IsWeekend_ReturnsExpected(int year, int month, int day, bool expected)
    {
        var dt = new SystemDateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
        Assert.Equal(expected, dt.IsWeekend());
    }
}

public class IsWeekdayTests
{
    [Theory]
    [InlineData(2024, 6, 15, false)] // Saturday
    [InlineData(2024, 6, 16, false)] // Sunday
    [InlineData(2024, 6, 17, true)]  // Monday
    [InlineData(2024, 6, 21, true)]  // Friday
    public void IsWeekday_ReturnsExpected(int year, int month, int day, bool expected)
    {
        var dt = new SystemDateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
        Assert.Equal(expected, dt.IsWeekday());
    }
}

public class IsLeapYearTests
{
    [Theory]
    [InlineData(2024, true)]
    [InlineData(2023, false)]
    [InlineData(2000, true)]
    [InlineData(1900, false)]
    [InlineData(2100, false)]
    public void IsLeapYear_ReturnsExpected(int year, bool expected)
    {
        var dt = new SystemDateTime(year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        Assert.Equal(expected, dt.IsLeapYear());
    }
}

public class AddBusinessDaysTests
{
    [Fact]
    public void AddBusinessDays_PositiveDays_SkipsWeekends()
    {
        // Friday June 14, 2024
        var friday = new SystemDateTime(2024, 6, 14, 10, 0, 0, DateTimeKind.Utc);
        var result = friday.AddBusinessDays(1);
        // Should be Monday June 17
        Assert.Equal(new SystemDateTime(2024, 6, 17, 10, 0, 0, DateTimeKind.Utc), result);
    }

    [Fact]
    public void AddBusinessDays_PositiveDays_AcrossWeekend()
    {
        // Friday June 14, 2024
        var friday = new SystemDateTime(2024, 6, 14, 10, 0, 0, DateTimeKind.Utc);
        var result = friday.AddBusinessDays(5);
        // 5 business days later: Mon+Tue+Wed+Thu+Fri = June 21
        Assert.Equal(new SystemDateTime(2024, 6, 21, 10, 0, 0, DateTimeKind.Utc), result);
    }

    [Fact]
    public void AddBusinessDays_NegativeDays_GoesBackward()
    {
        // Monday June 17, 2024
        var monday = new SystemDateTime(2024, 6, 17, 10, 0, 0, DateTimeKind.Utc);
        var result = monday.AddBusinessDays(-1);
        // Should be Friday June 14
        Assert.Equal(new SystemDateTime(2024, 6, 14, 10, 0, 0, DateTimeKind.Utc), result);
    }

    [Fact]
    public void AddBusinessDays_ZeroDays_ReturnsSameDate()
    {
        var dt = new SystemDateTime(2024, 6, 17, 10, 0, 0, DateTimeKind.Utc);
        Assert.Equal(dt, dt.AddBusinessDays(0));
    }
}

public class IsBusinessDayTests
{
    [Fact]
    public void IsBusinessDay_Weekday_ReturnsTrue()
    {
        var monday = new SystemDateTime(2024, 6, 17, 0, 0, 0, DateTimeKind.Utc);
        Assert.True(monday.IsBusinessDay());
    }

    [Fact]
    public void IsBusinessDay_Weekend_ReturnsFalse()
    {
        var saturday = new SystemDateTime(2024, 6, 15, 0, 0, 0, DateTimeKind.Utc);
        Assert.False(saturday.IsBusinessDay());
    }
}

public class IsBetweenTests
{
    [Fact]
    public void IsBetween_InRange_ReturnsTrue()
    {
        var dt = new SystemDateTime(2024, 6, 15, 0, 0, 0, DateTimeKind.Utc);
        var start = new SystemDateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc);
        var end = new SystemDateTime(2024, 6, 30, 0, 0, 0, DateTimeKind.Utc);
        Assert.True(dt.IsBetween(start, end));
    }

    [Fact]
    public void IsBetween_OnStartBoundary_ReturnsTrue()
    {
        var dt = new SystemDateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc);
        var start = new SystemDateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc);
        var end = new SystemDateTime(2024, 6, 30, 0, 0, 0, DateTimeKind.Utc);
        Assert.True(dt.IsBetween(start, end));
    }

    [Fact]
    public void IsBetween_OnEndBoundary_ReturnsTrue()
    {
        var dt = new SystemDateTime(2024, 6, 30, 0, 0, 0, DateTimeKind.Utc);
        var start = new SystemDateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc);
        var end = new SystemDateTime(2024, 6, 30, 0, 0, 0, DateTimeKind.Utc);
        Assert.True(dt.IsBetween(start, end));
    }

    [Fact]
    public void IsBetween_OutOfRange_ReturnsFalse()
    {
        var dt = new SystemDateTime(2024, 7, 1, 0, 0, 0, DateTimeKind.Utc);
        var start = new SystemDateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc);
        var end = new SystemDateTime(2024, 6, 30, 0, 0, 0, DateTimeKind.Utc);
        Assert.False(dt.IsBetween(start, end));
    }
}

public class AgeTests
{
    [Fact]
    public void Age_BirthdayAlreadyPassed_ReturnsCorrectAge()
    {
        var birthDate = new SystemDateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Local);
        int age = birthDate.Age();
        int expectedAge = SystemDateTime.Today.Year - 2000;
        if (new SystemDateTime(2000, 1, 1).Date > SystemDateTime.Today.AddYears(-expectedAge))
            expectedAge--;
        Assert.Equal(expectedAge, age);
    }

    [Fact]
    public void Age_SameDay_ReturnsZeroOrCorrectAge()
    {
        var today = SystemDateTime.Today;
        Assert.Equal(0, today.Age());
    }
}

public class WeekOfYearTests
{
    [Theory]
    [InlineData(2024, 1, 1, 1)]    // Monday Jan 1, 2024 -> Week 1
    [InlineData(2024, 12, 30, 1)]  // Monday Dec 30, 2024 -> Week 1 of 2025
    public void WeekOfYear_ReturnsIsoWeekNumber(int year, int month, int day, int expectedWeek)
    {
        var dt = new SystemDateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
        Assert.Equal(expectedWeek, dt.WeekOfYear());
    }

    [Fact]
    public void WeekOfYear_MidYear_ReturnsValidWeek()
    {
        var dt = new SystemDateTime(2024, 6, 15, 0, 0, 0, DateTimeKind.Utc);
        int week = dt.WeekOfYear();
        Assert.InRange(week, 1, 53);
    }
}

public class ToUnixTimestampTests
{
    [Fact]
    public void ToUnixTimestamp_Epoch_ReturnsZero()
    {
        var epoch = new SystemDateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        Assert.Equal(0L, epoch.ToUnixTimestamp());
    }

    [Fact]
    public void ToUnixTimestamp_KnownDate_ReturnsExpected()
    {
        // 2024-01-01T00:00:00Z = 1704067200
        var dt = new SystemDateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        Assert.Equal(1704067200L, dt.ToUnixTimestamp());
    }
}

public class ToRelativeStringTests
{
    [Fact]
    public void ToRelativeString_JustNow()
    {
        var now = SystemDateTime.Now;
        var result = now.ToRelativeString(now.AddSeconds(30));
        Assert.Equal("just now", result);
    }

    [Fact]
    public void ToRelativeString_MinutesAgo()
    {
        var now = SystemDateTime.Now;
        var past = now.AddMinutes(-5);
        Assert.Equal("5 minutes ago", past.ToRelativeString(now));
    }

    [Fact]
    public void ToRelativeString_OneMinuteAgo()
    {
        var now = SystemDateTime.Now;
        var past = now.AddMinutes(-1);
        Assert.Equal("1 minute ago", past.ToRelativeString(now));
    }

    [Fact]
    public void ToRelativeString_HoursAgo()
    {
        var now = SystemDateTime.Now;
        var past = now.AddHours(-3);
        Assert.Equal("3 hours ago", past.ToRelativeString(now));
    }

    [Fact]
    public void ToRelativeString_OneHourAgo()
    {
        var now = SystemDateTime.Now;
        var past = now.AddHours(-1);
        Assert.Equal("1 hour ago", past.ToRelativeString(now));
    }

    [Fact]
    public void ToRelativeString_DaysAgo()
    {
        var now = SystemDateTime.Now;
        var past = now.AddDays(-5);
        Assert.Equal("5 days ago", past.ToRelativeString(now));
    }

    [Fact]
    public void ToRelativeString_OneDayAgo()
    {
        var now = SystemDateTime.Now;
        var past = now.AddDays(-1);
        Assert.Equal("1 day ago", past.ToRelativeString(now));
    }

    [Fact]
    public void ToRelativeString_MonthsAgo()
    {
        var now = SystemDateTime.Now;
        var past = now.AddDays(-90);
        Assert.Equal("3 months ago", past.ToRelativeString(now));
    }

    [Fact]
    public void ToRelativeString_YearsAgo()
    {
        var now = SystemDateTime.Now;
        var past = now.AddDays(-730);
        Assert.Equal("2 years ago", past.ToRelativeString(now));
    }

    [Fact]
    public void ToRelativeString_InFuture()
    {
        var now = SystemDateTime.Now;
        var future = now.AddHours(3);
        Assert.Equal("in 3 hours", future.ToRelativeString(now));
    }

    [Fact]
    public void ToRelativeString_InFutureMinutes()
    {
        var now = SystemDateTime.Now;
        var future = now.AddMinutes(10);
        Assert.Equal("in 10 minutes", future.ToRelativeString(now));
    }

    [Fact]
    public void ToRelativeString_OneYearAgo()
    {
        var now = SystemDateTime.Now;
        var past = now.AddDays(-365);
        Assert.Equal("1 year ago", past.ToRelativeString(now));
    }

    [Fact]
    public void ToRelativeString_OneMonthAgo()
    {
        var now = SystemDateTime.Now;
        var past = now.AddDays(-30);
        Assert.Equal("1 month ago", past.ToRelativeString(now));
    }
}
