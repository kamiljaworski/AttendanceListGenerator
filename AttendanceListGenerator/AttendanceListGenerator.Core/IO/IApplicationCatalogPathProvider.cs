namespace AttendanceListGenerator.Core.IO
{
    public interface IApplicationCatalogPathProvider
    {
        string GetApplicationCatalogPath();
        string GetDocumentsCatalogPath();
    }
}
