using AttendanceListGenerator.Core.Data;
using AttendanceListGenerator.UI.ValueConverters;
using NUnit.Framework;
using System;
using System.Globalization;

namespace AttendanceListGenerator.UI.Tests.Unit.ValueConverters
{
    class MonthToStringConverterTests
    {
        [TestCase("pl-PL", Month.January, "Styczeń")]
        [TestCase("pl-PL", Month.February, "Luty")]
        [TestCase("pl-PL", Month.March, "Marzec")]
        [TestCase("pl-PL", Month.April, "Kwiecień")]
        [TestCase("pl-PL", Month.May, "Maj")]
        [TestCase("pl-PL", Month.June, "Czerwiec")]
        [TestCase("pl-PL", Month.July, "Lipiec")]
        [TestCase("pl-PL", Month.August, "Sierpień")]
        [TestCase("pl-PL", Month.September, "Wrzesień")]
        [TestCase("pl-PL", Month.October, "Październik")]
        [TestCase("pl-PL", Month.November, "Listopad")]
        [TestCase("pl-PL", Month.December, "Grudzień")]
        [TestCase("en-US", Month.January, "January")]
        [TestCase("en-US", Month.February, "February")]
        [TestCase("en-US", Month.March, "March")]
        [TestCase("en-US", Month.April, "April")]
        [TestCase("en-US", Month.May, "May")]
        [TestCase("en-US", Month.June, "June")]
        [TestCase("en-US", Month.July, "July")]
        [TestCase("en-US", Month.August, "August")]
        [TestCase("en-US", Month.September, "September")]
        [TestCase("en-US", Month.October, "October")]
        [TestCase("en-US", Month.November, "November")]
        [TestCase("en-US", Month.December, "December")]
        public void Convert_GivenCultureAndCorrectMonth_ReturnsCorrectMonthName(string cultureCode, object month, string expectedResult)
        {
            UseCulture(cultureCode);
            var converter = new MonthToStringConverter();

            string result = converter.Convert(month, null, null, null) as string;

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(DayOfWeek.Friday, "Friday")]
        [TestCase("Test", "Test")]
        [TestCase("", "")]
        [TestCase(null, "")]
        public void Convert_NotCorrectMonth_ReturnsValueAsString(object month, string expectedResult)
        {
            var converter = new MonthToStringConverter();

            string result = converter.Convert(month, null, null, null) as string;

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ConvertBack_ThrowsNotImplementedException()
        {
            var converter = new MonthToStringConverter();

            TestDelegate convertBack = () => converter.ConvertBack(null, null, null, null);

            Assert.That(convertBack, Throws.InstanceOf<NotImplementedException>());
        }

        private void UseCulture(string cultureCode)
        {
            CultureInfo.CurrentCulture = new CultureInfo(cultureCode, false);
            CultureInfo.CurrentUICulture = new CultureInfo(cultureCode, false);
        }
    }
}
