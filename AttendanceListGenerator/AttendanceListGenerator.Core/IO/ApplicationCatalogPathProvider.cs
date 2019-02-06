using AttendanceListGenerator.Core.Data;
using System;

namespace AttendanceListGenerator.Core.IO
{
    public class ApplicationCatalogPathProvider : IApplicationCatalogPathProvider
    {
        private readonly ILocalizedNames _localizedNames;

        public ApplicationCatalogPathProvider(ILocalizedNames localizedNames)
        {
            if (localizedNames == null)
                throw new ArgumentNullException("Localized names cannot be null");

            _localizedNames = localizedNames;
        }

        public string GetApplicationCatalogPath()
        {
            string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string applicationCatalogName = _localizedNames.ApplicationCatalogName;

            string applicationCatalogPath = myDocumentsPath;

            // Add application catalog name to the path
            if (!string.IsNullOrEmpty(applicationCatalogName))
                applicationCatalogPath += "\\" + applicationCatalogName;

            return applicationCatalogPath;
        }

        public string GetDocumentsCatalogPath()
        {
            string applicationCatalogPath = GetApplicationCatalogPath();

            string documentsCatalogName = _localizedNames.DocumentsCatalogName;

            // Add Documents Catalog Path
            if (!string.IsNullOrEmpty(documentsCatalogName))
                applicationCatalogPath += "\\" + documentsCatalogName;

            return applicationCatalogPath;
        }
    }
}
