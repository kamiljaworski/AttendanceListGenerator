using AttendanceListGenerator.Core.Data;
using AttendanceListGenerator.Core.IO;
using Moq;
using NUnit.Framework;
using System;

namespace AttendanceListGenerator.Core.Tests.Unit.IO
{
    class ApplicationCatalogPathProviderTests
    {
        [Test]
        public void Constructor_NullILocalizedNames_ThrowsArgumentNullException()
        {
            TestDelegate constructor = () => new ApplicationCatalogPathProvider(null);

            Assert.That(constructor, Throws.ArgumentNullException);
        }

        [Test]
        public void GetApplicationCatalogPath_CorrectILocalizedNames_ReturnsCorrectPath()
        {
            string applicationCatalogName = "Attendance List Generator";
            ILocalizedNames localizedNames = Mock.Of<ILocalizedNames>(l => l.ApplicationCatalogName == applicationCatalogName);
            ApplicationCatalogPathProvider pathProvider = new ApplicationCatalogPathProvider(localizedNames);

            string applicationCatalogPath = pathProvider.GetApplicationCatalogPath();

            string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string expectedResult = myDocumentsPath + "\\" + applicationCatalogName;
            Assert.That(applicationCatalogPath, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetDocumentsCatalogPath_CorrectILocalizedNames_ReturnsCorrectPath()
        {
            string applicationName = "Attendance List Generator";
            string documentsCatalogName = "Documents";
            ILocalizedNames localizedNames = Mock.Of<ILocalizedNames>(l => l.ApplicationCatalogName == applicationName &&
                                                                           l.DocumentsCatalogName == documentsCatalogName);
            ApplicationCatalogPathProvider pathProvider = new ApplicationCatalogPathProvider(localizedNames);

            string applicationCatalogPath = pathProvider.GetDocumentsCatalogPath();

            string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string expectedResult = myDocumentsPath + "\\" + applicationName + "\\" + documentsCatalogName;
            Assert.That(applicationCatalogPath, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetApplicationCatalogPath_NullApplicationName_ReturnsMyDocumentsPath()
        {
            ApplicationCatalogPathProvider pathProvider = new ApplicationCatalogPathProvider(Mock.Of<ILocalizedNames>());

            string applicationCatalogPath = pathProvider.GetApplicationCatalogPath();

            string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            Assert.That(applicationCatalogPath, Is.EqualTo(myDocumentsPath));
        }
    }
}
