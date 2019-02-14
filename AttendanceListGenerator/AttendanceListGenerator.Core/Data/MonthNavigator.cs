using AttendanceListGenerator.Core.Helpers;

namespace AttendanceListGenerator.Core.Data
{
    public static class MonthNavigator
    {
        public static Month Next(this Month month)
        {
            if (month == Month.December)
                return Month.January;

            return EnumNavigator<Month>.Next(month);
        }

        public static Month Previous(this Month month)
        {
            if (month == Month.January)
                return Month.December;

            return EnumNavigator<Month>.Previous(month);
        }
    }
}
