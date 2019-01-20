using AttendanceListGenerator.Core.Data;
using NUnit.Framework;
using System;

namespace AttendanceListGenerator.Core.Tests.Unit.Data
{
    class DayOfWeekNavigatorTests
    {
        [TestCase(DayOfWeek.Monday, DayOfWeek.Tuesday)]
        [TestCase(DayOfWeek.Tuesday, DayOfWeek.Wednesday)]
        [TestCase(DayOfWeek.Wednesday, DayOfWeek.Thursday)]
        [TestCase(DayOfWeek.Thursday, DayOfWeek.Friday)]
        [TestCase(DayOfWeek.Friday, DayOfWeek.Saturday)]
        [TestCase(DayOfWeek.Saturday, DayOfWeek.Sunday)]
        [TestCase(DayOfWeek.Sunday, DayOfWeek.Monday)]
        public void Next_ReturnsProperNextDay(DayOfWeek current, DayOfWeek expected)
        {
            DayOfWeek nextDayOfWeek = current.Next();

            Assert.That(nextDayOfWeek, Is.EqualTo(expected));
        }

        [TestCase(DayOfWeek.Monday, DayOfWeek.Sunday)]
        [TestCase(DayOfWeek.Tuesday, DayOfWeek.Monday)]
        [TestCase(DayOfWeek.Wednesday, DayOfWeek.Tuesday)]
        [TestCase(DayOfWeek.Thursday, DayOfWeek.Wednesday)]
        [TestCase(DayOfWeek.Friday, DayOfWeek.Thursday)]
        [TestCase(DayOfWeek.Saturday, DayOfWeek.Friday)]
        [TestCase(DayOfWeek.Sunday, DayOfWeek.Saturday)]
        public void Previous_ReturnsProperNextDay(DayOfWeek current, DayOfWeek expected)
        {
            DayOfWeek previousDayOfWeek = current.Previous();

            Assert.That(previousDayOfWeek, Is.EqualTo(expected));
        }
    }
}
