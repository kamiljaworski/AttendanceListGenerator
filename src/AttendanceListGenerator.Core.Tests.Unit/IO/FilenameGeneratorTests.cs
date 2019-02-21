using AttendanceListGenerator.Core.Data;
using AttendanceListGenerator.Core.IO;
using Moq;
using NUnit.Framework;
using System;

namespace AttendanceListGenerator.Core.Tests.Unit.IO
{
    class FilenameGeneratorTests
    {
        [Test]
        public void Constructor_NullILocalizedNames_ThrowsArgumentNullException()
        {
            TestDelegate constructor = () => new FilenameGenerator(null, Mock.Of<IDateTimeProvider>());

            Assert.That(constructor, Throws.ArgumentNullException);
        }

        [Test]
        public void Constructor_NullIDateTimeProvider_ThrowsArgumentNullException()
        {
            TestDelegate constructor = () => new FilenameGenerator(Mock.Of<ILocalizedNames>(), null);

            Assert.That(constructor, Throws.ArgumentNullException);
        }

        [Test]
        public void GeneratePdfDocumentFilename_NullIAttendanceListData_ThrowsArgumentNullException()
        {
            FilenameGenerator fileNameProvider = new FilenameGenerator(Mock.Of<ILocalizedNames>(), Mock.Of<IDateTimeProvider>());

            TestDelegate executeGeneratePdfDocumentFilename = () => fileNameProvider.GeneratePdfDocumentFilename(null);

            Assert.That(executeGeneratePdfDocumentFilename, Throws.ArgumentNullException);
        }

        [Test]
        public void GenerateJsonSettingsFilename_ReturnsCorrectFilename()
        {
            FilenameGenerator fileNameGenerator = new FilenameGenerator(Mock.Of<ILocalizedNames>(), Mock.Of<IDateTimeProvider>());

            string filename = fileNameGenerator.GenerateJsonSettingsFilename();

            Assert.That(filename, Is.EqualTo("settings.json"));
        }

        [TestCase(Month.January, 2019, "2019-02-06 19:25:32", "January_2019_06022019192532.pdf")]
        [TestCase(Month.May, 2016, "2018-06-01 11:01:02", "May_2016_01062018110102.pdf")]
        [TestCase(Month.December, 2001, "2006-12-01 05:32:16", "December_2001_01122006053216.pdf")]
        public void GeneratePdfDocumentFilename_GivenData_ReturnsCorrectFileName(Month month, int year, DateTime dateTime, string expectedResult)
        {
            ILocalizedNames localizedNames = Mock.Of<ILocalizedNames>(l => l.GetMonthName(Month.January) == "January" &&
                                                                           l.GetMonthName(Month.February) == "February" &&
                                                                           l.GetMonthName(Month.March) == "March" &&
                                                                           l.GetMonthName(Month.April) == "April" &&
                                                                           l.GetMonthName(Month.May) == "May" &&
                                                                           l.GetMonthName(Month.June) == "June" &&
                                                                           l.GetMonthName(Month.July) == "July" &&
                                                                           l.GetMonthName(Month.September) == "September" &&
                                                                           l.GetMonthName(Month.October) == "October" &&
                                                                           l.GetMonthName(Month.November) == "November" &&
                                                                           l.GetMonthName(Month.December) == "December");
            IAttendanceListData attendanceListData = Mock.Of<IAttendanceListData>(a => a.Month == month && a.Year == year);
            IDateTimeProvider dateTimeProvider = Mock.Of<IDateTimeProvider>(d => d.Now == dateTime);
            FilenameGenerator fileNameGenerator = new FilenameGenerator(localizedNames, dateTimeProvider);

            string fileName = fileNameGenerator.GeneratePdfDocumentFilename(attendanceListData);

            Assert.That(fileName, Is.EqualTo(expectedResult));
        }
    }
}
