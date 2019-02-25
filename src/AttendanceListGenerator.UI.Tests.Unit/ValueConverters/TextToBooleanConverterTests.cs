using AttendanceListGenerator.UI.ValueConverters;
using NUnit.Framework;
using System;

namespace AttendanceListGenerator.UI.Tests.Unit.ValueConverters
{
    class TextToBooleanConverterTests
    {
        [TestCase("TEST")]
        [TestCase("test")]
        [TestCase("asdasd")]
        [TestCase("123123")]
        public void Convert_NotEmptyStrings_ReturnsTrue(string str)
        {
            var converter = new TextToBooleanConverter();

            bool result = (bool)converter.Convert(str, null, null, null);

            Assert.True(result);
        }

        [TestCase("")]
        [TestCase(null)]
        public void Convert_EmptyStringAndNull_ReturnsFalse(string str)
        {
            var converter = new TextToBooleanConverter();

            bool result = (bool)converter.Convert(str, null, null, null);

            Assert.False(result);
        }

        [Test]
        public void ConvertBack_ThrowsNotImplementedException()
        {
            var converter = new TextToBooleanConverter();

            TestDelegate convertBack = () => converter.ConvertBack(null, null, null, null);

            Assert.That(convertBack, Throws.InstanceOf<NotImplementedException>());
        }
    }
}
