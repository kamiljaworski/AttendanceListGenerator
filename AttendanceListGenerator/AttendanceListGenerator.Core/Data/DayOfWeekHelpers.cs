using System;

namespace AttendanceListGenerator.Core.Data
{
    public static class DayOfWeekHelpers
    {
        private static readonly DayOfWeek[] _daysOfWeek = (DayOfWeek[])Enum.GetValues(typeof(DayOfWeek));

        public static DayOfWeek Next(this DayOfWeek dayOfWeek)
        {
            if (dayOfWeek == DayOfWeek.Saturday)
                return DayOfWeek.Sunday;

            int index = Array.IndexOf(_daysOfWeek, dayOfWeek);
            return _daysOfWeek[index + 1];
        }

        public static DayOfWeek Previous(this DayOfWeek dayOfWeek)
        {
            if (dayOfWeek == DayOfWeek.Sunday)
                return DayOfWeek.Saturday;

            int index = Array.IndexOf(_daysOfWeek, dayOfWeek);
            return _daysOfWeek[index - 1];
        }
    }
}
