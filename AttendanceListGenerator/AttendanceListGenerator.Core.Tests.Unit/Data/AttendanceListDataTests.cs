using AttendanceListGenerator.Core.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AttendanceListGenerator.Core.Tests.Unit.Data
{
    class AttendanceListDataTests
    {
        [TestCase(Month.February)]
        [TestCase(Month.March)]
        [TestCase(Month.July)]
        [TestCase(Month.December)]
        public void Constructor_PassValidMonth_MonthIsEqualToGivenMonth(Month month)
        {
            AttendanceListData monthData = new AttendanceListData(GetListOfFullnames(), month, 2019);

            Assert.That(monthData.Month, Is.EqualTo(month));
        }

        [TestCase(2019)]
        [TestCase(2018)]
        [TestCase(2022)]
        [TestCase(2010)]
        public void Constructor_PassValidYear_YearIsEqualToGivenYear(int year)
        {
            AttendanceListData monthData = new AttendanceListData(GetListOfFullnames(), Month.January, year);

            Assert.That(monthData.Year, Is.EqualTo(year));
        }

        [Test]
        public void Constructor_PassValidFullnamesList_FullnamesListIsEqualToGivenList()
        {
            IList<string> fullnames = GetListOfFullnames();

            AttendanceListData monthData = new AttendanceListData(fullnames, Month.January, 2019);

            Assert.That(monthData.Fullnames, Is.EqualTo(fullnames));
        }

        [Test]
        public void Constructor_PassEmptyFullnamesList_ThrowsArgumentException()
        {
            IList<string> fullnames = new List<string>();

            TestDelegate constructor = () => new AttendanceListData(fullnames, Month.January, 2019);

            Assert.That(constructor, Throws.ArgumentException);
        }

        [Test]
        public void Constructor_PassListOf8Fullnames_ThrowsArgumentException()
        {
            IList<string> fullnames = new List<string> { "aaa", "bbb", "ccc", "ddd", "eee", "fff", "ggg", "hhh" };

            TestDelegate constructor = () => new AttendanceListData(fullnames, Month.January, 2019);

            Assert.That(constructor, Throws.ArgumentException);
        }

        [Test]
        public void Constructor_PassMonthNone_ThrowsArgumentException()
        {
            TestDelegate constructor = () => new AttendanceListData(GetListOfFullnames(), Month.None, 2019);

            Assert.That(constructor, Throws.ArgumentException);
        }

        [Test]
        public void Constructor_PassNullFullnamesList_ThrowsArgumentException()
        {
            IList<string> fullnames = null;

            TestDelegate constructor = () => new AttendanceListData(fullnames, Month.January, 2019);

            Assert.That(constructor, Throws.ArgumentException);
        }

        [TestCase(1899)]
        [TestCase(1800)]
        [TestCase(1600)]
        [TestCase(2101)]
        [TestCase(2110)]
        [TestCase(2500)]
        public void Constructor_PassYearLessThan1900OrAbove2100_ThrowArgumentOutOfRangeException(int year)
        {
            TestDelegate constructor = () => new AttendanceListData(GetListOfFullnames(), Month.January, year);

            Assert.That(constructor, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [TestCase(1900)]
        [TestCase(2100)]
        public void Constructor_PassYear1900And2100_DoNotThrowArgumentOutOfRangeException(int year)
        {
            TestDelegate constructor = () => new AttendanceListData(GetListOfFullnames(), Month.January, year);

            Assert.That(constructor, Throws.Nothing);
        }

        [Test]
        public void Constructor_PassFebruary2019_CreateListOf28Days()
        {
            AttendanceListData monthData = new AttendanceListData(GetListOfFullnames(), Month.February, 2019);

            Assert.That(monthData.Days.Count, Is.EqualTo(28));
        }

        [Test]
        public void Constructor_PassFebruary2019_ListOfDaysDoNotContainAnyNull()
        {
            AttendanceListData monthData = new AttendanceListData(GetListOfFullnames(), Month.February, 2019);

            CollectionAssert.AllItemsAreNotNull(monthData.Days);
        }

        [Test]
        public void Constructor_PassFebruary2019_FirstDayInFebruaryIsFriday()
        {
            AttendanceListData monthData = new AttendanceListData(GetListOfFullnames(), Month.February, 2019);
            IDay firstDay = monthData.Days.FirstOrDefault();

            Assert.That(firstDay.DayOfWeek, Is.Not.Null.And.EqualTo(DayOfWeek.Friday));
        }

        [Test]
        public void Constructor_PassFebruary2019_LastDayInFebruaryIsThursday()
        {
            AttendanceListData monthData = new AttendanceListData(GetListOfFullnames(), Month.February, 2019);
            IDay lastDay = monthData.Days.LastOrDefault();

            Assert.That(lastDay.DayOfWeek, Is.Not.Null.And.EqualTo(DayOfWeek.Thursday));
        }

        [Test]
        public void Constructor_PassFebruary2020LeapYear_CreateListOf29Days()
        {
            AttendanceListData monthData = new AttendanceListData(GetListOfFullnames(), Month.February, 2020);

            Assert.That(monthData.Days.Count, Is.EqualTo(29));
        }

        [Test]
        public void Constructor_PassFebruary2020LeapYear_FirstDayIsSaturday()
        {
            AttendanceListData monthData = new AttendanceListData(GetListOfFullnames(), Month.February, 2020);
            IDay firstDay = monthData.Days.FirstOrDefault();

            Assert.That(firstDay.DayOfWeek, Is.Not.Null.And.EqualTo(DayOfWeek.Saturday));
        }

        [Test]
        public void Constructor_PassFebruary2020LeapYear_LastDayIsSaturday()
        {
            AttendanceListData monthData = new AttendanceListData(GetListOfFullnames(), Month.February, 2020);
            IDay lastDay = monthData.Days.LastOrDefault();

            Assert.That(lastDay.DayOfWeek, Is.Not.Null.And.EqualTo(DayOfWeek.Saturday));
        }

        [Test]
        public void Constructor_PassAugust2024_CreateListOf31Days()
        {
            AttendanceListData monthData = new AttendanceListData(GetListOfFullnames(), Month.August, 2024);

            Assert.That(monthData.Days.Count, Is.EqualTo(31));
        }

        [Test]
        public void Constructor_PassAugust2024_FirstDayIsThursday()
        {
            AttendanceListData monthData = new AttendanceListData(GetListOfFullnames(), Month.August, 2024);
            IDay firstDay = monthData.Days.FirstOrDefault();

            Assert.That(firstDay.DayOfWeek, Is.Not.Null.And.EqualTo(DayOfWeek.Thursday));
        }

        [Test]
        public void Constructor_PassAugust2024_LastDayIsSaturday()
        {
            AttendanceListData monthData = new AttendanceListData(GetListOfFullnames(), Month.August, 2024);
            IDay lastDay = monthData.Days.LastOrDefault();

            Assert.That(lastDay.DayOfWeek, Is.Not.Null.And.EqualTo(DayOfWeek.Saturday));

        }

        private IList<string> GetListOfFullnames()
        {
            return new List<string> { "James Hunt", "William Jefferson", "Ryan Carroll" };
        }
    }
}
