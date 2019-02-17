using AttendanceListGenerator.Core.Data;
using AttendanceListGenerator.Core.IO;
using Moq;
using NUnit.Framework;
using System;

namespace AttendanceListGenerator.Core.Tests.Unit.IO
{
    class DirectoryProviderTests
    {
        [Test]
        public void Constructor_NullILocalizedNames_ThrowsArgumentNullException()
        {
            TestDelegate constructor = () => new DirectoryProvider(null);

            Assert.That(constructor, Throws.ArgumentNullException);
        }

        [Test]
        public void GetMyDocumentsDirectoryPath_CorrectILocalizedNames_ReturnsCorrectPath()
        {
            DirectoryProvider directoryProvider = new DirectoryProvider(Mock.Of<ILocalizedNames>());

            string myDocumentsPath = directoryProvider.GetMyDocumentsDirectoryPath();

            string expectedMyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            Assert.That(myDocumentsPath, Is.EqualTo(expectedMyDocumentsPath));
        }

        [Test]
        public void GetApplicationDirectoryPath_CorrectILocalizedNames_ReturnsCorrectPath()
        {
            string applicationDirectoryName = "Attendance List Generator";
            ILocalizedNames localizedNames = Mock.Of<ILocalizedNames>(l => l.ApplicationDirectoryName == applicationDirectoryName);
            DirectoryProvider directoryProvider = new DirectoryProvider(localizedNames);

            string applicationDirectoryPath = directoryProvider.GetApplicationDirectoryPath();

            string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string expectedResult = myDocumentsPath + "\\Attendance List Generator";
            Assert.That(applicationDirectoryPath, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetDocumentsDirectoryPath_CorrectILocalizedNames_ReturnsCorrectPath()
        {
            string applicationName = "Attendance List Generator";
            string documentsDirectoryName = "Documents";
            ILocalizedNames localizedNames = Mock.Of<ILocalizedNames>(l => l.ApplicationDirectoryName == applicationName &&
                                                                           l.DocumentsDirectoryName == documentsDirectoryName);
            DirectoryProvider directoryProvider = new DirectoryProvider(localizedNames);

            string applicationDirectoryPath = directoryProvider.GetDocumentsDirectoryPath();

            string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string expectedResult = myDocumentsPath + "\\Attendance List Generator\\Documents";
            Assert.That(applicationDirectoryPath, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetApplicationDirectoryPath_NullApplicationName_ReturnsMyDocumentsPath()
        {
            DirectoryProvider directoryProvider = new DirectoryProvider(Mock.Of<ILocalizedNames>());

            string applicationDirectoryPath = directoryProvider.GetApplicationDirectoryPath();

            string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            Assert.That(applicationDirectoryPath, Is.EqualTo(myDocumentsPath));
        }
    }
}
