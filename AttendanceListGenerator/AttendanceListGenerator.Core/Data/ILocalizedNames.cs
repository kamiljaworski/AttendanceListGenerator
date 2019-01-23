using System;

namespace AttendanceListGenerator.Core.Data
{
    public interface ILocalizedNames
    {
        string DocumentAuthor { get; }
        string DocumentTitle { get; }
        string DocumentComment { get; }
        string GetDayOfWeekName(DayOfWeek dayOfWeek);
    }
}
