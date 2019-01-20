using AttendanceListGenerator.Core.Helpers;
using System;

namespace AttendanceListGenerator.Core.Data
{
    public static class DayOfWeekNavigator
    {
        public static DayOfWeek Next(this DayOfWeek dayOfWeek) => EnumNavigator<DayOfWeek>.Next(dayOfWeek);
        public static DayOfWeek Previous(this DayOfWeek dayOfWeek) => EnumNavigator<DayOfWeek>.Previous(dayOfWeek);
    }
}
