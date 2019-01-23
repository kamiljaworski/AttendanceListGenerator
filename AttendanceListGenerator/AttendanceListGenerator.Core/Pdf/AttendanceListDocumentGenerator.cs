using AttendanceListGenerator.Core.Data;
using MigraDoc.DocumentObjectModel;

namespace AttendanceListGenerator.Core.Pdf
{
    public class AttendanceListDocumentGenerator : IAttendanceListDocumentGenerator
    {
        private readonly IAttendanceListData _data;
        private readonly ILocalizedNames _names;

        public AttendanceListDocumentGenerator(IAttendanceListData data, ILocalizedNames names)
        {
            _data = data;
            _names = names;
        }

        public Document GenerateDocument()
        {
            // Create a document
            Document document = new Document();
            
            // Document information
            document.Info.Author = _names.DocumentAuthor;
            document.Info.Title = _names.DocumentTitle;
            document.Info.Comment = _names.DocumentComment;

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
