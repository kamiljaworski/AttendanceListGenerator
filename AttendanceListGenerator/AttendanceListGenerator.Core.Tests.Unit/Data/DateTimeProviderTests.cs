using AttendanceListGenerator.Core.Data;
using NUnit.Framework;
using System;

namespace AttendanceListGenerator.Core.Tests.Unit.Data
{
    class DateTimeProviderTests
    {
        [Test]
        public void Now_ReturnsDateTimeNow()
        {
            DateTimeProvider dateTimeProvider = new DateTimeProvider();

            DateTime now = dateTimeProvider.Now;

            Assert.That(now, Is.EqualTo(DateTime.Now).Within(TimeSpan.FromMilliseconds(2)));
        }
    }
}
