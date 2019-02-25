using AttendanceListGenerator.Core.IO;
using MigraDoc.DocumentObjectModel;
using NUnit.Framework;
using System.IO;

namespace AttendanceListGenerator.Core.Tests.Unit.IO
{
    /// <summary>
    /// To pass these tests you have to run them as administrator
    /// </summary>
    class FileReaderTests
    {
        private string _jsonFilename = "test.json";
        private string _directoryPath = "C:";
        private string _jsonFullPath => _directoryPath + "\\" + _jsonFilename;

        private string _json = "{\"EnableColors\":true,\"EnableHolidaysTexts\":false,\"EnableSundaysTexts\":true,\"EnableTableStretching\":true,"
                             + "\"Fullnames\":[\"Adam Adams\",\"Tom Cruise\",\"Robert Kubica\"]}";

        [SetUp]
        public void DeleteTestFiles()
        {
            if (File.Exists(_jsonFullPath))
                File.Delete(_jsonFullPath);

            File.WriteAllText(_jsonFullPath, _json);
        }

        [TestCase(null, "test")]
        [TestCase("", "test")]
        [TestCase("test", null)]
        [TestCase("test", "")]
        [TestCase("", "")]
        [TestCase(null, null)]
        public void ReadJsonFile_NullOrEmptyStringArguments_ThrowsArgumentNullException(string path, string filename)
        {
            FileReader fileReader = new FileReader();

            TestDelegate executeReadJsonFile = () => fileReader.ReadJsonFile(path, filename);

            Assert.That(executeReadJsonFile, Throws.ArgumentNullException);
        }

        [Test]
        public void ReadJsonFile_CorrectData_ReturnsCorrectJson()
        {
            FileReader fileReader = new FileReader();

            string result = fileReader.ReadJsonFile(_directoryPath, _jsonFilename);

            Assert.That(result, Is.EqualTo(_json));
        }
        
    }
}
