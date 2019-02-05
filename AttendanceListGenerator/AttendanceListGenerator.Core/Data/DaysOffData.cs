using System;
using System.Collections.Generic;
using System.Linq;

namespace AttendanceListGenerator.Core.Data
{
    public class DaysOffData : IDaysOffData
    {
        public IList<IDayOff> DaysOff { get; private set; }

        public int Year { get; private set; }

        public IList<IDayOff> GetDaysOff(Month month)
        {
            if (month == Month.None)
                throw new ArgumentException("Month cannot be 'None'");

            return DaysOff.Where(d => d.Date.Month == (int)month).ToList();
        }

        public DaysOffData(int year)
        {
            if (year < 1900 || year > 2100)
                throw new ArgumentOutOfRangeException("Year should be between 1900 and 2100");

            Year = year;

            // Count Easter Sunday and Monday dates
            DateTime easterSundayDate = GetEasterSundayDate(Year);
            DateTime easterMondayDate = easterSundayDate + TimeSpan.FromDays(1);

            // Count DescendOfTheHolySpirit and CorpusChristiDay dates
            DateTime descendOfTheHolySpiritDate = easterSundayDate + TimeSpan.FromDays(49);
            DateTime corpusChristiDayDate = easterSundayDate + TimeSpan.FromDays(60);

            // Add all days off to the list
            DaysOff = new List<IDayOff>
            {
                new DayOff(Holiday.NewYearsDay, new DateTime(Year, 1, 1)),
                new DayOff(Holiday.Epiphany, new DateTime(Year, 1, 6)),
                new DayOff(Holiday.EasterSunday, easterSundayDate),
                new DayOff(Holiday.EasterMonday, easterMondayDate),
                new DayOff(Holiday.LabourDay, new DateTime(Year, 5, 1)),
                new DayOff(Holiday.ConstitutionDay, new DateTime(Year, 5, 3)),
                new DayOff(Holiday.DescendOfTheHolySpirit, descendOfTheHolySpiritDate),
                new DayOff(Holiday.CorpusChristiDay, corpusChristiDayDate),
                new DayOff(Holiday.ArmedForcesDay, new DateTime(Year, 8, 15)),
                new DayOff(Holiday.AllSaintsDay, new DateTime(Year, 11, 1)),
                new DayOff(Holiday.IndependenceDay, new DateTime(Year, 11, 11)),
                new DayOff(Holiday.Christmas, new DateTime(Year, 12, 25)),
                new DayOff(Holiday.Christmas, new DateTime(Year, 12, 26))
            };
        }

        private DateTime GetEasterSundayDate(int year)
        {
            int day = 0;
            int month = 0;

            int g = year % 19;
            int c = year / 100;
            int h = (c - (c / 4) - ((8 * c + 13) / 25) + 19 * g + 15) % 30;
            int i = h - (h / 28) * (1 - (h / 28) * (29 / (h + 1)) * ((21 - g) / 11));

            day = i - ((year + (year / 4) + i + 2 - c + (c / 4)) % 7) + 28;
            month = 3;

            if (day > 31)
            {
                month++;
                day -= 31;
            }

            return new DateTime(year, month, day);
        }
    }
}
