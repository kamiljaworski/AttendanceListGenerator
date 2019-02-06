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
            string applicationName = _localizedNames.ApplicationName;

            string applicationCatalogPath = myDocumentsPath;

            if(!string.IsNullOrEmpty(applicationName))
                applicationCatalogPath += "\\" + applicationName;

            return applicationCatalogPath;
        }
    }
}
