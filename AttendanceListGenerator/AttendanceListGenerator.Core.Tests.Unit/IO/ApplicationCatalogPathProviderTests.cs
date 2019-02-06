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
            string applicationName = "Attendance List Generator";
            ILocalizedNames localizedNames = Mock.Of<ILocalizedNames>(l => l.ApplicationName == applicationName);
            ApplicationCatalogPathProvider pathProvider = new ApplicationCatalogPathProvider(localizedNames);

            string applicationCatalogPath = pathProvider.GetApplicationCatalogPath();

            string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string expectedResult = myDocumentsPath + "\\" + applicationName;
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
