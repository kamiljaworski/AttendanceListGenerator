using MigraDoc.DocumentObjectModel;

namespace AttendanceListGenerator.Core.IO
{
    public interface IFileSaver
    {
        bool SavePdfDocument(Document document, string path, string filename);
    }
}
