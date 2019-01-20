namespace AttendanceListGenerator.Core.Data
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
