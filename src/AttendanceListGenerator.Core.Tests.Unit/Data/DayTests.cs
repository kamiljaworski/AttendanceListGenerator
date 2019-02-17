using AttendanceListGenerator.Core.Data;
using NUnit.Framework;
using System;

namespace AttendanceListGenerator.Core.Tests.Unit.Data
{
    class DayTests
    {
        [Test]
        public void ConstructorWithTwoParameters_CorrectData_PropertiesAreCorrect()
        {
            Day day = new Day(20, DayOfWeek.Saturday);

            Assert.That(day.DayOfMonth, Is.EqualTo(20));
            Assert.That(day.DayOfWeek, Is.EqualTo(DayOfWeek.Saturday));
        }

        [Test]
        public void ConstructorWithTwoParameters_CorrectData_HolidayIsEqualToNone()
        {
            Day day = new Day(20, DayOfWeek.Saturday);

            Assert.That(day.Holiday, Is.EqualTo(Holiday.None));
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-10)]
        [TestCase(32)]
        [TestCase(33)]
        [TestCase(100)]
        public void ConstructorWithTwoParameters_DayOfMonthLessThan1OrAbove31_ThrowsArgumentException(int dayOfMonth)
        {
            TestDelegate constructor = () => new Day(dayOfMonth, DayOfWeek.Saturday);

            Assert.That(constructor, Throws.ArgumentException);
        }

        [Test]
        public void ConstructorWithThreeParameters_CorrectData_PropertiesAreCorrect()
        {
            Day day = new Day(20, DayOfWeek.Saturday, Holiday.LabourDay);

            Assert.That(day.DayOfMonth, Is.EqualTo(20));
            Assert.That(day.DayOfWeek, Is.EqualTo(DayOfWeek.Saturday));
            Assert.That(day.Holiday, Is.EqualTo(Holiday.LabourDay));
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-10)]
        [TestCase(32)]
        [TestCase(33)]
        [TestCase(100)]
        public void ConstructorWithThreeParameters_DayOfMonthLessThan1OrAbove31_ThrowsArgumentException(int dayOfMonth)
        {
            TestDelegate constructor = () => new Day(dayOfMonth, DayOfWeek.Saturday);

            Assert.That(constructor, Throws.ArgumentException);
        }

        [TestCase(1, "1.")]
        [TestCase(5, "5.")]
        [TestCase(10, "10.")]
        [TestCase(31, "31.")]
        public void FormattedDayOfMonth_CorrectData_ReturnsDayOfMonthWithADot(int dayOfMonth, string expectedResult)
        {
            Day day = new Day(dayOfMonth, DayOfWeek.Saturday, Holiday.LabourDay);

            string result = day.FormattedDayOfMonth;

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
