using AttendanceListGenerator.Core.IO;
using MigraDoc.DocumentObjectModel;
using NUnit.Framework;
using System.IO;

namespace AttendanceListGenerator.Core.Tests.Unit.IO
{
    class FileSaverTests
    {
        private string _pdfFilename = "test.pdf";
        private string _jsonFilename = "test.json";
        private string _directoryPath = "C:";
        private string _pdfFullPath => _directoryPath + "\\" + _pdfFilename;
        private string _jsonFullPath => _directoryPath + "\\" + _jsonFilename;

        [SetUp]
        [TearDown]
        public void DeleteTestFiles()
        {
            if (File.Exists(_pdfFullPath))
                File.Delete(_pdfFullPath);

            if (File.Exists(_jsonFullPath))
                File.Delete(_jsonFullPath);
        }

        [Test]
        public void SavePdfDocument_NullDocument_ThrowsArgumentNullException()
        {
            FileSaver documentSaver = new FileSaver();

            TestDelegate executeSavePdfDocument = () => documentSaver.SavePdfDocument(null, _directoryPath, _pdfFilename);

            Assert.That(executeSavePdfDocument, Throws.ArgumentNullException);
        }

        [TestCase(null, "test")]
        [TestCase("", "test")]
        [TestCase("test", null)]
        [TestCase("test", "")]
        [TestCase("", "")]
        [TestCase(null, null)]
        public void SavePdfDocument_NullOrEmptyStringArguments_ThrowsArgumentNullException(string path, string filename)
        {
            FileSaver documentSaver = new FileSaver();
            Document document = new Document();

            TestDelegate executeSavePdfDocument = () => documentSaver.SavePdfDocument(document, path, filename);

            Assert.That(executeSavePdfDocument, Throws.ArgumentNullException);
        }

        [Test]
        public void SavePdfDocument_CorrectData_ReturnsTrues()
        {
            FileSaver documentSaver = new FileSaver();
            Document document = GetExampleDocument();

            bool result = documentSaver.SavePdfDocument(document, _directoryPath, _pdfFilename);

            Assert.That(result, Is.True);
        }

        [Test]
        public void SavePdfDocument_CorrectData_CreatesFile()
        {
            FileSaver documentSaver = new FileSaver();
            Document document = GetExampleDocument();

            documentSaver.SavePdfDocument(document, _directoryPath, _pdfFilename);

            FileAssert.Exists(_pdfFullPath);
        }

        [TestCase(null, "test", "test")]
        [TestCase("", "test", "test")]
        [TestCase("test", null, "test")]
        [TestCase("test", "", "test")]
        [TestCase("", "", "test")]
        [TestCase("", "", "")]
        [TestCase(null, null, "test")]
        [TestCase(null, null, null)]
        public void SaveJsonFile_NullOrEmptyStringArguments_ThrowsArgumentNullException(string json, string path, string filename)
        {
            FileSaver fileSacver = new FileSaver();

            TestDelegate executeSaveJsonFile = () => fileSacver.SaveJsonFile(json, path, filename);

            Assert.That(executeSaveJsonFile, Throws.ArgumentNullException);
        }

        [Test]
        public void SaveJsonFile_CorrectData_CreatesFile()
        {
            FileSaver documentSaver = new FileSaver();
            string json = "{\"EnableColors\":true,\"EnableHolidaysTexts\":false,\"EnableSundaysTexts\":true,\"EnableTableStretching\":true,"
                        + "\"Fullnames\":[\"Adam Adams\",\"Tom Cruise\",\"Robert Kubica\"]}";

            documentSaver.SaveJsonFile(json, _directoryPath, _jsonFilename);

            FileAssert.Exists(_jsonFullPath);
        }

        [Test]
        public void SaveJsonFile_CorrectData_ReturnsTrues()
        {
            FileSaver documentSaver = new FileSaver();
            string json = "{\"EnableColors\":true,\"EnableHolidaysTexts\":false,\"EnableSundaysTexts\":true,\"EnableTableStretching\":true,"
                        + "\"Fullnames\":[\"Adam Adams\",\"Tom Cruise\",\"Robert Kubica\"]}";

            bool result = documentSaver.SaveJsonFile(json, _directoryPath, _jsonFilename);

            Assert.That(result, Is.True);
        }

        private Document GetExampleDocument()
        {
            Document document = new Document();

            Section sec = document.AddSection();
            sec.AddParagraph("test");

            return document;
        }
    }
}
