using System;

namespace AttendanceListGenerator.Core.Data
{
    public class DayOff : IDayOff
    {
        public Holiday Holiday { get; set; }
        public DateTime Date { get; set; }

        public DayOff(Holiday holiday, DateTime date)
        {
            Holiday = holiday;
            Date = date;
        }
    }
}
