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

        private const int _numberOfAdditionalColumns = 2;

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

            // Change margins
            setup.TopMargin = 30.0;
            setup.BottomMargin = 30.0;
            setup.LeftMargin = 30.0;
            setup.RightMargin = 30.0;

            // Add a section to the document
            Section section = document.AddSection();

            // Change section setup
            section.PageSetup = setup;

            // Add a paragraph and title text
            Paragraph documentHeading = section.AddParagraph();
            documentHeading.AddText(_names.GetDocumentTitle(_data.Month, _data.Year));

            // Add a table with black borders
            Table table = section.AddTable();
            table.Borders.Color = Colors.Black;

            // Count number of columns in the table
            int numberOfTableColumns = _data.MaxNumberOfFullnames + _numberOfAdditionalColumns;

            // Add columns to the table
            for (int i = 0; i < numberOfTableColumns; ++i)
                table.AddColumn();

            // Add fullnames list to the table
            Row row = table.AddRow();
            for (int i = 1; i < numberOfTableColumns; ++i)
            {
                if (i <= _data.Fullnames.Count)
                    row.Cells[i + 1].AddParagraph(_data.Fullnames[i - 1]);
            }

            // Add a row for each day with day number and day of week abbreviation
            foreach (IDay day in _data.Days)
            {
                row = table.AddRow();
                row.Cells[0].AddParagraph(day.FormattedDayOfMonth);
                row.Cells[1].AddParagraph(_names.GetDayOfWeekAbbreviation(day.DayOfWeek));
            }


            // Change document heading format
            documentHeading.Format.Font.Name = "Times New Roman";
            documentHeading.Format.Alignment = ParagraphAlignment.Center;
            documentHeading.Format.Font.Size = 18.0;
            documentHeading.Format.Font.Bold = true;
            documentHeading.Format.SpaceAfter = 15.0;

            // Change first two columns width
            table.Columns[0].Width = 25;
            table.Columns[1].Width = 30;

            // Change width of fullnames columns
            for (int i = 0; i < _data.MaxNumberOfFullnames; i++)
                table.Columns[i + 2].Width = 100;

            // Remove first two columns borders
            table.Rows[0].Cells[0].Borders.Top.Visible = false;
            table.Rows[0].Cells[1].Borders.Top.Visible = false;
            table.Rows[0].Cells[0].Borders.Left.Visible = false;
            table.Rows[0].Cells[1].Borders.Left.Visible = false;
            table.Rows[0].Cells[0].Borders.Right.Visible = false;
            table.Rows[0].Cells[1].Borders.Right.Visible = false;

            // Change table default font name and size
            table.Format.Font.Name = "Times New Roman";
            table.Format.Font.Size = 12.0;

            // Change table headings font size and weight
            table.Rows[0].Format.Font.Size = 14.0;
            table.Rows[0].Format.Font.Bold = true;


            return document;
        }
    }
}
