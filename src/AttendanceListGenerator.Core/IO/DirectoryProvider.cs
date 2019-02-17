using AttendanceListGenerator.Core.Data;
using System;

namespace AttendanceListGenerator.Core.IO
{
    public class DirectoryProvider : IDirectoryProvider
    {
        private readonly ILocalizedNames _localizedNames;

        public DirectoryProvider(ILocalizedNames localizedNames)
        {
            if (localizedNames == null)
                throw new ArgumentNullException("Localized names cannot be null");

            _localizedNames = localizedNames;
        }

        public string GetMyDocumentsDirectoryPath() => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public string GetApplicationDirectoryPath()
        {
            string myDocumentsPath = GetMyDocumentsDirectoryPath();
            string applicationDirectoryName = _localizedNames.ApplicationDirectoryName;

            string applicationDirectoryPath = myDocumentsPath;

            // Add application catalog name to the path
            if (!string.IsNullOrEmpty(applicationDirectoryName))
                applicationDirectoryPath += "\\" + applicationDirectoryName;

            return applicationDirectoryPath;
        }

        public string GetDocumentsDirectoryPath()
        {
            string documentsPath = GetApplicationDirectoryPath();
            string documentsDirectoryName = _localizedNames.DocumentsDirectoryName;

            // Add Documents Catalog Path
            if (!string.IsNullOrEmpty(documentsDirectoryName))
                documentsPath += "\\" + documentsDirectoryName;

            return documentsPath;
        }
    }
}
