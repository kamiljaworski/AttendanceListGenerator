using AttendanceListGenerator.Core.Data;
using AttendanceListGenerator.Core.IO;
using Moq;
using NUnit.Framework;
using System;

namespace AttendanceListGenerator.Core.Tests.Unit.IO
{
    class FileNameProviderTests
    {
        [Test]
        public void Constructor_NullIAttendanceListData_ThrowsArgumentNullException()
        {
            TestDelegate constructor = () => new FileNameProvider(null, Mock.Of<ILocalizedNames>(), Mock.Of<IDateTimeProvider>());

            Assert.That(constructor, Throws.ArgumentNullException);
        }

        [Test]
        public void Constructor_NullILocalizedNames_ThrowsArgumentNullException()
        {
            TestDelegate constructor = () => new FileNameProvider(Mock.Of<IAttendanceListData>(), null, Mock.Of<IDateTimeProvider>());

            Assert.That(constructor, Throws.ArgumentNullException);
        }

        [Test]
        public void Constructor_NullIDateTimeProvider_ThrowsArgumentNullException()
        {
            TestDelegate constructor = () => new FileNameProvider(Mock.Of<IAttendanceListData>(), Mock.Of<ILocalizedNames>(), null);

            Assert.That(constructor, Throws.ArgumentNullException);
        }

        [Test]
        public void GetPdfFileName_January2019_ReturnsCorrectFileName()
        {
            IAttendanceListData attendanceListData = Mock.Of<IAttendanceListData>(a => a.Month == Month.January && a.Year == 2019);
            ILocalizedNames localizedNames = Mock.Of<ILocalizedNames>(l => l.GetMonthName(Month.January) == "January");
            IDateTimeProvider dateTimeProvider = Mock.Of<IDateTimeProvider>(d => d.Now == new DateTime(2019, 2, 6, 19, 25, 32));
            FileNameProvider fileNameProvider = new FileNameProvider(attendanceListData, localizedNames, dateTimeProvider);

            string fileName = fileNameProvider.GetPdfFileName();

            string expectedFileName = "January_2019_06022019192532.pdf";
            Assert.That(fileName, Is.EqualTo(expectedFileName));
        }
    }
}
