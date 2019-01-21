using System;
using System.Collections.Generic;

namespace AttendanceListGenerator.Core.Data
{
    public class AttendanceListData : IAttendanceListData
    {
        private const int _maxNumberOfDaysInAMonth = 31;

        public IList<IDay> Days { get; private set; }
        public IList<string> FullNames { get; private set; }
        public Month Month { get; private set; }
        public int Year { get; private set; }


        public AttendanceListData(Month month, int year)
        {
            if (year < 1900 || year > 2100)
                throw new ArgumentOutOfRangeException();

            Days = new List<IDay>(_maxNumberOfDaysInAMonth);
            Month = month;
            Year = year;

            CreateDaysList();
        }

        private void CreateDaysList()
        {
            int daysInMonth = DateTime.DaysInMonth(Year, (int)Month);
            DayOfWeek currentDayOfWeek = new DateTime(Year, (int)Month, 1).DayOfWeek;

            for (int i = 1; i <= daysInMonth; ++i)
            {
                IDay day = new Day(i, currentDayOfWeek);
                Days.Add(day);
                currentDayOfWeek = currentDayOfWeek.Next();
            }
        }
    }
}
