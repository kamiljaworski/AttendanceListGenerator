using System;
using System.Collections.Generic;

namespace AttendanceListGenerator.Core.Data
{
    public class MonthData : IMonthData
    {
        public IList<IDay> Days { get; private set; }
        public Month Month { get; private set; }
        public int Year { get; private set; }

        public MonthData(Month month, int year)
        {
            if (year < 1900 || year > 2100)
                throw new ArgumentOutOfRangeException();

            Days = new List<IDay>();
            Month = month;
            Year = year;

            CreateDaysList();
        }

        private void CreateDaysList()
        {
            int daysInMonth = DateTime.DaysInMonth(Year, (int)Month);
            DayOfWeek dayOfWeekOfFirstDayInMonth = new DateTime(1, (int)Month, Year).DayOfWeek;

            for (int i = 0; i < daysInMonth; ++i)
                Days.Add(null);
        }
    }
}
