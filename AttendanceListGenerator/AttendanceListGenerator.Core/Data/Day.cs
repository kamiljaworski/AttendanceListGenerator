using System;

namespace AttendanceListGenerator.Core.Data
{
    public class Day : IDay
    {
        public int DayOfMonth { get; private set; }
        public string FormattedDayOfMonth => $"{DayOfMonth}.";
        public DayOfWeek DayOfWeek { get; private set; }
        public Holiday Holiday { get; private set; }

        public Day(int dayOfMonth, DayOfWeek dayOfWeek)
        {
            DayOfMonth = dayOfMonth;
            DayOfWeek = dayOfWeek;
            Holiday = Holiday.None;
        }

        public Day(int dayOfMonth, DayOfWeek dayOfWeek, Holiday holiday)
        {
            DayOfMonth = dayOfMonth;
            DayOfWeek = dayOfWeek;
            Holiday = holiday;
        }
    }
}
