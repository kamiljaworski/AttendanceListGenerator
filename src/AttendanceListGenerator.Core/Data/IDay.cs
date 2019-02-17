using System;

namespace AttendanceListGenerator.Core.Data
{
    public interface IDay
    {
        int DayOfMonth { get; }
        string FormattedDayOfMonth { get; }
        DayOfWeek DayOfWeek { get; }
        Holiday Holiday { get; }
    }
}
