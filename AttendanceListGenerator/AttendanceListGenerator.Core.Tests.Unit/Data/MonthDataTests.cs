using AttendanceListGenerator.Core.Data;
using NUnit.Framework;
using System;

namespace AttendanceListGenerator.Core.Tests.Unit.Data
{
    class MonthDataTests
    {
        [TestCase(Month.February)]
        [TestCase(Month.March)]
        [TestCase(Month.July)]
        [TestCase(Month.December)]
        public void Constructor_Month_MonthIsEqualToGivenMonth(Month month)
        {
            MonthData monthData = new MonthData(month, 2019);

            Assert.That(monthData.Month, Is.EqualTo(month));
        }

        [TestCase(2019)]
        [TestCase(2018)]
        [TestCase(2022)]
        [TestCase(2010)]
        public void Constructor_Year_YearIsEqualToGivenYear(int year)
        {
            MonthData monthData = new MonthData(Month.January, year);

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
            TestDelegate constructor = () => new MonthData(Month.January, year);

            Assert.That(constructor, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [TestCase(1900)]
        [TestCase(2100)]
        public void Constructor_Year1900And2100_DoNotThrowArgumentOutOfRangeException(int year)
        {
            TestDelegate constructor = () => new MonthData(Month.January, year);

            Assert.That(constructor, Throws.Nothing);
        }

        [Test]
        public void Constructor_February2019_CreateListOf28Days()
        {
            MonthData monthData = new MonthData(Month.February, 2019);

            Assert.That(monthData.Days.Count, Is.EqualTo(28));
        }
    }
}
