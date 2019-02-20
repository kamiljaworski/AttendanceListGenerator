using System;
using System.Collections.Generic;
using System.Linq;

namespace AttendanceListGenerator.Core.Data
{
    public class AttendanceListData : IAttendanceListData
    {
        private const int _maxNumberOfDaysInAMonth = 31;
        private const int _minNumberOfFullnames = 1;
        private const int _maxNumberOfFullnames = 7;

        private readonly IDaysOffData _daysOffData;

        public IList<IDay> Days { get; private set; }
        public IList<IPerson> People { get; private set; }
        public int MaxNumberOfFullnames => _maxNumberOfFullnames;
        public Month Month { get; private set; }
        public int Year { get; private set; }

        public AttendanceListData(IDaysOffData daysOff, IList<IPerson> people, Month month, int year)
        {
            if (daysOff == null || daysOff.Year != year)
                throw new ArgumentException("Days off data cannot be null and its year must be the same as passed in this constructor");

            if (people == null || people.Count > _maxNumberOfFullnames)
                throw new ArgumentException("People list should contain and at most 7 people");

            if(month == Month.None)
                throw new ArgumentException("Month cannot be 'None'");

            if (year < 1900 || year > 2100)
                throw new ArgumentOutOfRangeException("Year should be between 1900 and 2100");

            _daysOffData = daysOff;

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
            IList<IDayOff> thisMonthDaysOff = _daysOffData.GetDaysOff(Month);

            for (int i = 1; i <= daysInMonth; ++i)
            {
                IDayOff dayOff = thisMonthDaysOff?.FirstOrDefault(d => d.Date.Day == i);
                IDay day = new Day(i, currentDayOfWeek);

                if (dayOff != null)
                    day = new Day(i, currentDayOfWeek, dayOff.Holiday);

                Days.Add(day);
                currentDayOfWeek = currentDayOfWeek.Next();
            }
        }
    }
}
