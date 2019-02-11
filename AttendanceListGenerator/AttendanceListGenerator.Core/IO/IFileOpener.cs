namespace AttendanceListGenerator.Core.IO
{
    public interface IFileOpener
    {
        void OpenFile(string path, string filename);
    }
}
