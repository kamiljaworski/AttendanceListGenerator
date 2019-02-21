using MigraDoc.DocumentObjectModel;

namespace AttendanceListGenerator.Core.Pdf
{
    public interface IAttendanceListDocumentGenerator
    {
        bool EnableColors { get; set; }
        bool EnableHolidaysTexts { get; set; }
        bool EnableSundaysTexts { get; set; }
        bool EnableTableStretching { get; set; }

        Color FullnamesBackgroundColor { get; set; }
        Color DayOffBackgroundColor { get; set; }
        Color SaturdayBackgroundColor { get; set; }

        Document GenerateDocument();
    }
}
