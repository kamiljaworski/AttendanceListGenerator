using MigraDoc.DocumentObjectModel;

namespace AttendanceListGenerator.Core.Pdf
{
    public interface IAttendanceListDocumentGenerator
    {
        bool CanAddColorsToTheDocument { get; set; }
        Color FullnamesBackgroundColor { get; set; }
        Color SundayBackgroundColor { get; set; }
        Color SaturdayBackgroundColor { get; set; }

        Document GenerateDocument();
    }
}
