using System;
using System.Diagnostics;

namespace AttendanceListGenerator.Core.IO
{
    public class FileOpener : IFileOpener
    {
        public void OpenFile(string path, string filename) => Process.Start(GetFullPath(path, filename));
        private string GetFullPath(string path, string filename) => path + "\\" + filename;
    }
}
