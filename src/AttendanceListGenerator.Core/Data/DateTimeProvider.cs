using System;

namespace AttendanceListGenerator.Core.Data
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
