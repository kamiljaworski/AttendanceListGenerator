using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp1_32.Pdf;
using System.Diagnostics;

namespace AttendanceListGenerator.Core.Pdf
{
    public class PdfGenerator
    {
        public void Generate()
        {
            Document document = new Document();
            document.Info.Title = "test pdf table";
            document.Info.Author = "Kamil Jaworski";
            document.Info.Comment = "Some test simple table";

            // Get page setup and change the orientation
            PageSetup setup = document.DefaultPageSetup.Clone();
            setup.Orientation = Orientation.Landscape;

            // Add new section and set its page setup (orientation) to landscape
            Section section = document.AddSection();
            section.PageSetup = setup;

            // Create a table
            Table table = section.AddTable();
            table.Borders.Color = Colors.Black;


            // Add columns
            table.AddColumn();
            table.AddColumn();
            table.AddColumn();

            // Add rows
            Row row = table.AddRow();
            row.Cells[0].AddParagraph("Test 1");
            row.Cells[1].AddParagraph("Test 2");
            row.Cells[2].AddParagraph("Test 3");

            row = table.AddRow();
            row.Shading.Color = Colors.LightGray;
            row.Cells[0].AddParagraph("abc");
            row.Cells[1].AddParagraph("def");
            row.Cells[2].AddParagraph("ghj");


            // Saving the document
            //string ddl = MigraDoc.DocumentObjectModel.IO.DdlWriter.WriteToString(document);
            MigraDoc.DocumentObjectModel.IO.DdlWriter.WriteToFile(document, "MigraDoc.mdddl");

            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true, PdfFontEmbedding.Always);
            renderer.Document = document;

            renderer.RenderDocument();

            // Save the document...
            string filename = "SimpleTable.pdf";
            renderer.PdfDocument.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);
        }
    }
}
