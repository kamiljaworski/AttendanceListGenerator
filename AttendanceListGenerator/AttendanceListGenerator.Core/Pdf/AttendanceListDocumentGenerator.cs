using AttendanceListGenerator.Core.Data;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using System;

namespace AttendanceListGenerator.Core.Pdf
{
    public class AttendanceListDocumentGenerator : IAttendanceListDocumentGenerator
    {
        private readonly IAttendanceListData _data;
        private readonly ILocalizedNames _names;

        private const int _numberOfTableColumns = 8;

        public AttendanceListDocumentGenerator(IAttendanceListData data, ILocalizedNames names)
        {
            if (data == null || names == null)
                throw new ArgumentNullException();

            _data = data;
            _names = names;
        }

        public Document GenerateDocument()
        {
            // Create a document
            Document document = new Document();

            // Document information
            document.Info.Author = _names.DocumentAuthor;
            document.Info.Title = _names.GetDocumentTitle(_data.Month, _data.Year);
            document.Info.Comment = _names.DocumentComment;

            // Get the default page setup and change its orientation to Landscape
            PageSetup setup = document.DefaultPageSetup.Clone();
            setup.Orientation = Orientation.Landscape;

            // Add a section to the document
            Section section = document.AddSection();

            // Change section setup
            section.PageSetup = setup;

            // Add a paragraph and title text
            Paragraph paragraph = section.AddParagraph();
            paragraph.AddText(_names.GetDocumentTitle(_data.Month, _data.Year));

            // Add a table with black borders
            Table table = section.AddTable();
            table.Borders.Color = Colors.Black;

            // Add columns
            for (int i = 0; i < _numberOfTableColumns; ++i)
                table.AddColumn();

            // Add fullnames list to the table
            Row row = table.AddRow();
            for (int i = 1; i < _numberOfTableColumns; ++i)
            {
                if (i <= _data.Fullnames.Count)
                    row.Cells[i].AddParagraph(_data.Fullnames[i - 1]);
            }

            // Add a row for each day
            foreach (IDay day in _data.Days)
            {
                // Create day number formatted with dot at the end of number like: '1.', '2.', '21.'
                string dayNumber = $"{day.DayOfMonth}.";
                row = table.AddRow();
                row.Cells[0].AddParagraph(dayNumber);
            }




            return document;
        }
    }
}
