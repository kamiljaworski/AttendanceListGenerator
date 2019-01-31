using System;
using System.Collections.Generic;

namespace AttendanceListGenerator.Core.Data
{
    public class AttendanceListData : IAttendanceListData
    {
        private const int _maxNumberOfDaysInAMonth = 31;
        private const int _minNumberOfFullnames = 1;
        private const int _maxNumberOfFullnames = 7;

        public IList<IDay> Days { get; private set; }
        public IList<IPerson> People { get; private set; }
        public int MaxNumberOfFullnames => _maxNumberOfFullnames;
        public Month Month { get; private set; }
        public int Year { get; private set; }

        public AttendanceListData(IList<IPerson> people, Month month, int year)
        {
            if (people == null || people.Count < _minNumberOfFullnames || people.Count > _maxNumberOfFullnames)
                throw new ArgumentException("People list should contain at least 1 person and at most 7");

            if(month == Month.None)
                throw new ArgumentException("Month cannot be 'None'");

            if (year < 1900 || year > 2100)
                throw new ArgumentOutOfRangeException("Year should be between 1900 and 2100");


            People = people;
            Days = new List<IDay>(_maxNumberOfDaysInAMonth);
            Month = month;
            Year = year;

            CreateListOfDays();
        }

        private void CreateListOfDays()
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
