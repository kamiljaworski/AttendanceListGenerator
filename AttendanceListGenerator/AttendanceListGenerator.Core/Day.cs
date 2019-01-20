namespace AttendanceListGenerator.Core
{
    public class Day : IDay
    {
        public DayOfWeek DayOfWeek { get; }

        public Day(DayOfWeek dayOfWeek)
        {
            DayOfWeek = dayOfWeek;
        }
    }
}
