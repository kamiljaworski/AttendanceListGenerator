using System;
using System.IO;

namespace AttendanceListGenerator.Core.IO
{
    public class FileReader : IFileReader
    {
        public string ReadJsonFile(string path, string filename)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("Path cannot be null or empty");

            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException("Filename cannot be null or empty");

            try
            {
                string fullpath = GetFullPath(path, filename);
                return File.ReadAllText(fullpath);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private string GetFullPath(string path, string filename) => path + "\\" + filename;
    }
}
