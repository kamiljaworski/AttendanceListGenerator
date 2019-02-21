namespace AttendanceListGenerator.Core.IO
{
    public interface ISerializer<T> where T : class
    {
        string Serialize(T value);
        T Deserialize(string value);
    }
}
