using AttendanceListGenerator.Core.Data;
using AttendanceListGenerator.Core.Helpers;
using NUnit.Framework;
using System;

namespace AttendanceListGenerator.Core.Tests.Unit.Helpers
{
    public class EnumNavigatorTests
    {
        [TestCase(DayOfWeek.Monday, DayOfWeek.Tuesday)]
        [TestCase(DayOfWeek.Tuesday, DayOfWeek.Wednesday)]
        [TestCase(DayOfWeek.Wednesday, DayOfWeek.Thursday)]
        [TestCase(DayOfWeek.Thursday, DayOfWeek.Friday)]
        [TestCase(DayOfWeek.Friday, DayOfWeek.Saturday)]
        [TestCase(DayOfWeek.Saturday, DayOfWeek.Sunday)]
        [TestCase(DayOfWeek.Sunday, DayOfWeek.Monday)]
        public void Next_DayOfWeek_ReturnsProperNextDay(DayOfWeek current, DayOfWeek expected)
        {
            DayOfWeek nextDayOfWeek = EnumNavigator<DayOfWeek>.Next(current);

            Assert.That(nextDayOfWeek, Is.EqualTo(expected));
        }

        [TestCase(DayOfWeek.Monday, DayOfWeek.Sunday)]
        [TestCase(DayOfWeek.Tuesday, DayOfWeek.Monday)]
        [TestCase(DayOfWeek.Wednesday, DayOfWeek.Tuesday)]
        [TestCase(DayOfWeek.Thursday, DayOfWeek.Wednesday)]
        [TestCase(DayOfWeek.Friday, DayOfWeek.Thursday)]
        [TestCase(DayOfWeek.Saturday, DayOfWeek.Friday)]
        [TestCase(DayOfWeek.Sunday, DayOfWeek.Saturday)]
        public void Previous_DayOfWeek_ReturnsProperNextDay(DayOfWeek current, DayOfWeek expected)
        {
            DayOfWeek previousDayOfWeek = EnumNavigator<DayOfWeek>.Previous(current);

            Assert.That(previousDayOfWeek, Is.EqualTo(expected));
        }

        [Test]
        public void Next_TypeThatIsNotAnEnum_ThrowsTypeInitializationException()
        {
            TestDelegate executeNextMethod = () => EnumNavigator<DateTime>.Next(DateTime.Now);

            Assert.That(executeNextMethod, Throws.InstanceOf<TypeInitializationException>());
        }

        [Test]
        public void Previous_TypeThatIsNotAnEnum_ThrowsTypeInitializationException()
        {
            TestDelegate executePreviousMethod = () => EnumNavigator<DateTime>.Previous(DateTime.Now);

            Assert.That(executePreviousMethod, Throws.InstanceOf<TypeInitializationException>());
        }
    }
}
