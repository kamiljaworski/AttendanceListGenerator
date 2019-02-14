using AttendanceListGenerator.Core.Data;
using NUnit.Framework;

namespace AttendanceListGenerator.Core.Tests.Unit.Data
{
    class MonthNavigatorTests
    {
        [TestCase(Month.January, Month.February)]
        [TestCase(Month.February, Month.March)]
        [TestCase(Month.March, Month.April)]
        [TestCase(Month.April, Month.May)]
        [TestCase(Month.May, Month.June)]
        [TestCase(Month.June, Month.July)]
        [TestCase(Month.July, Month.August)]
        [TestCase(Month.August, Month.September)]
        [TestCase(Month.September, Month.October)]
        [TestCase(Month.October, Month.November)]
        [TestCase(Month.November, Month.December)]
        [TestCase(Month.December, Month.January)]
        public void Next_ReturnsCorrectNextMonth(Month current, Month expected)
        {
            Month nextMonth = current.Next();

            Assert.That(nextMonth, Is.EqualTo(expected));
        }

        [TestCase(Month.January, Month.December)]
        [TestCase(Month.February, Month.January)]
        [TestCase(Month.March, Month.February)]
        [TestCase(Month.April, Month.March)]
        [TestCase(Month.May, Month.April)]
        [TestCase(Month.June, Month.May)]
        [TestCase(Month.July, Month.June)]
        [TestCase(Month.August, Month.July)]
        [TestCase(Month.September, Month.August)]
        [TestCase(Month.October, Month.September)]
        [TestCase(Month.November, Month.October)]
        [TestCase(Month.December, Month.November)]
        public void Previous_ReturnsCorrectNextMonth(Month current, Month expected)
        {
            Month previousMonth = current.Previous();

            Assert.That(previousMonth, Is.EqualTo(expected));
        }

    }
}
