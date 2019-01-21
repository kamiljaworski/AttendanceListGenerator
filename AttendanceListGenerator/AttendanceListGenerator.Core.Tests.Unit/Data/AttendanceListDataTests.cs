using AttendanceListGenerator.Core.Data;
using NUnit.Framework;
using System;
using System.Linq;

namespace AttendanceListGenerator.Core.Tests.Unit.Data
{
    class AttendanceListDataTests
    {
        [TestCase(Month.February)]
        [TestCase(Month.March)]
        [TestCase(Month.July)]
        [TestCase(Month.December)]
        public void Constructor_Month_MonthIsEqualToGivenMonth(Month month)
        {
            AttendanceListData monthData = new AttendanceListData(month, 2019);

            Assert.That(monthData.Month, Is.EqualTo(month));
        }

        [TestCase(2019)]
        [TestCase(2018)]
        [TestCase(2022)]
        [TestCase(2010)]
        public void Constructor_Year_YearIsEqualToGivenYear(int year)
        {
            AttendanceListData monthData = new AttendanceListData(Month.January, year);

            Assert.That(monthData.Year, Is.EqualTo(year));
        }

        [TestCase(1899)]
        [TestCase(1800)]
        [TestCase(1600)]
        [TestCase(2101)]
        [TestCase(2110)]
        [TestCase(2500)]
        public void Constructor_YearLessThan1900OrAbove2100_ThrowArgumentOutOfRangeException(int year)
        {
            TestDelegate constructor = () => new AttendanceListData(Month.January, year);

            Assert.That(constructor, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [TestCase(1900)]
        [TestCase(2100)]
        public void Constructor_Year1900And2100_DoNotThrowArgumentOutOfRangeException(int year)
        {
            TestDelegate constructor = () => new AttendanceListData(Month.January, year);

            Assert.That(constructor, Throws.Nothing);
        }

        [Test]
        public void Constructor_February2019_CreateListOf28Days()
        {
            AttendanceListData monthData = new AttendanceListData(Month.February, 2019);

            Assert.That(monthData.Days.Count, Is.EqualTo(28));
        }

        [Test]
        public void Constructor_February2019_ListOfDaysDoNotContainAnyNull()
        {
            AttendanceListData monthData = new AttendanceListData(Month.February, 2019);

            CollectionAssert.AllItemsAreNotNull(monthData.Days);
        }

        [Test]
        public void Constructor_February2019_FirstDayInFebruaryIsFriday()
        {
            AttendanceListData monthData = new AttendanceListData(Month.February, 2019);
            IDay firstDay = monthData.Days.FirstOrDefault();

            Assert.That(firstDay.DayOfWeek, Is.Not.Null.And.EqualTo(DayOfWeek.Friday));
        }

        [Test]
        public void Constructor_February2019_LastDayInFebruaryIsThursday()
        {
            AttendanceListData monthData = new AttendanceListData(Month.February, 2019);
            IDay lastDay = monthData.Days.LastOrDefault();

            Assert.That(lastDay.DayOfWeek, Is.Not.Null.And.EqualTo(DayOfWeek.Thursday));
        }
    }
}
