using System;

namespace AttendanceListGenerator.Core.Data
{
    public interface IDay
    {
        int DayOfMonth { get; }
        DayOfWeek DayOfWeek { get; }
        // TODO: days off
    }
}
