using MigraDoc.DocumentObjectModel;

namespace AttendanceListGenerator.Core.Pdf
{
    public interface IAttendanceListDocumentGenerator
    {
        Document GenerateDocument();
    }
}
