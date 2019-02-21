namespace AttendanceListGenerator.Core.IO
{
    public interface IFileReader
    {
        string ReadJsonFile(string path, string filename);
    }
}
