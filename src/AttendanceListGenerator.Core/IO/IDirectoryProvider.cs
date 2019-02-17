namespace AttendanceListGenerator.Core.IO
{
    public interface IDirectoryProvider
    {
        string GetMyDocumentsDirectoryPath();
        string GetApplicationDirectoryPath();
        string GetDocumentsDirectoryPath();
    }
}
