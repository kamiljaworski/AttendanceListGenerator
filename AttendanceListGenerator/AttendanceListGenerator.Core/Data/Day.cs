using System;

namespace AttendanceListGenerator.Core.Data
{
    public class Day : IDay
    {
        public int DayOfMonth { get; private set; }
        public DayOfWeek DayOfWeek { get; private set; }
        public string FormattedDayOfMonth => $"{DayOfMonth}.";

        public Day(int dayOfMonth, DayOfWeek dayOfWeek)
        {
            DayOfMonth = dayOfMonth;
            DayOfWeek = dayOfWeek;
        }
    }
}
