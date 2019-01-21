using AttendanceListGenerator.Core.Data;
using MigraDoc.DocumentObjectModel;

namespace AttendanceListGenerator.Core.Pdf
{
    public class AttendanceListDocumentGenerator : IAttendanceListDocumentGenerator
    {
        private readonly IAttendanceListData _attendanceListData;

        public AttendanceListDocumentGenerator(IAttendanceListData attendanceListData)
        {
            _attendanceListData = attendanceListData;
        }

        public Document GenerateDocument()
        {
            // Create a document
            Document document = new Document();
            // TODO: localized document info strings
            document.Info.Author = "Kamil Jaworski";
            document.Info.Title = "Test";
            document.Info.Comment = "Test";

            // Get the default page setup and change its orientation to Landscape
            PageSetup setup = document.DefaultPageSetup.Clone();
            setup.Orientation = Orientation.Landscape;

            // Add a section to the document
            Section section = document.AddSection();

            // Change section setup
            section.PageSetup = setup;

            return document;
        }
    }
}
