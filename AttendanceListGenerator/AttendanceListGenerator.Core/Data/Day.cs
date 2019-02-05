using System;

namespace AttendanceListGenerator.Core.Data
{
    public class Day : IDay
    {
        public int DayOfMonth { get; private set; }
        public string FormattedDayOfMonth => $"{DayOfMonth}.";
        public DayOfWeek DayOfWeek { get; private set; }
        public Holiday Holiday { get; private set; }

        public Day(int dayOfMonth, DayOfWeek dayOfWeek) : this(dayOfMonth, dayOfWeek, Holiday.None) { }

        public Day(int dayOfMonth, DayOfWeek dayOfWeek, Holiday holiday)
        {
            if (dayOfMonth < 1 || dayOfMonth > 31)
                throw new ArgumentException("Day of month cannot be less than 1 or above 31");

            DayOfMonth = dayOfMonth;
            DayOfWeek = dayOfWeek;
            Holiday = holiday;
        }
    }
}
