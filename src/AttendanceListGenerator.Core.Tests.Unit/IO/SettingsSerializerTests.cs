using NUnit.Framework;
using System.Collections.Generic;

namespace AttendanceListGenerator.Core.IO
{
    class SettingsSerializerTests
    {
        [Test]
        public void Serialize_CorrectSettings_CreatesCorrectJson()
        {
            Settings settings = new Settings
            {
                EnableColors = true,
                EnableHolidaysTexts = false,
                EnableSundaysTexts = true,
                EnableTableStretching = true,
                Fullnames = new List<string> { "Adam Adams", "Tom Cruise", "Robert Kubica" }
            };
            SettingsSerializer serializer = new SettingsSerializer();

            string output = serializer.Serialize(settings);

            string expectedJson = "{\"EnableColors\":true,\"EnableHolidaysTexts\":false,\"EnableSundaysTexts\":true,\"EnableTableStretching\":true,"
                                + "\"Fullnames\":[\"Adam Adams\",\"Tom Cruise\",\"Robert Kubica\"]}";
            Assert.That(output, Is.EqualTo(expectedJson));
        }

        [Test]
        public void Deserialize_CorrectJson_CreatesCorrectSettings()
        {
            string json = "{\"EnableColors\":true,\"EnableHolidaysTexts\":false,\"EnableSundaysTexts\":true,\"EnableTableStretching\":true,"
                        + "\"Fullnames\":[\"Adam Adams\",\"Tom Cruise\",\"Robert Kubica\"]}";
            SettingsSerializer serializer = new SettingsSerializer();

            Settings settings = serializer.Deserialize(json);

            Assert.That(settings.EnableColors, Is.EqualTo(true));
            Assert.That(settings.EnableHolidaysTexts, Is.EqualTo(false));
            Assert.That(settings.EnableSundaysTexts, Is.EqualTo(true));
            Assert.That(settings.EnableTableStretching, Is.EqualTo(true));
            Assert.That(settings.Fullnames, Is.EqualTo(new List<string> { "Adam Adams", "Tom Cruise", "Robert Kubica" }));
        }

    }
}
