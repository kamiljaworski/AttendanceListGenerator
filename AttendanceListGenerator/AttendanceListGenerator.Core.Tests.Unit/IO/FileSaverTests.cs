using AttendanceListGenerator.Core.IO;
using MigraDoc.DocumentObjectModel;
using NUnit.Framework;
using System.IO;

namespace AttendanceListGenerator.Core.Tests.Unit.IO
{
    class FileSaverTests
    {
        private string _pdfFilename = "test.pdf";
        private string _directoryPath = "C:";
        private string _pdfFullPath => _directoryPath + "\\" + _pdfFilename;

        [SetUp]
        [TearDown]
        public void DeleteTestFiles()
        {
            if (File.Exists(_pdfFullPath))
                File.Delete(_pdfFullPath);
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

        private Document GetExampleDocument()
        {
            Document document = new Document();

            Section sec = document.AddSection();
            sec.AddParagraph("test");

            return document;
        }
    }
}
