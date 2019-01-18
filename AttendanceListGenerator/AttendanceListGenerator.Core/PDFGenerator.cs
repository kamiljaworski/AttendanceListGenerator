using PdfSharp1_32.Drawing;
using PdfSharp1_32.Pdf;
using System.Diagnostics;

namespace AttendanceListGenerator.Core
{
    public class PdfGenerator
    {
        public void Generate()
        {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();

            // Create an empty page
            PdfPage page = document.AddPage();
            page.Orientation = PdfSharp1_32.PageOrientation.Landscape;
            page.Size = PdfSharp1_32.PageSize.A4;

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            XFont font = new XFont("Verdana", 20, XFontStyle.Bold);

            // Draw the text
            gfx.DrawString("Hello, World!", font, XBrushes.Black,
              new XRect(0, 0, page.Width, page.Height),
              XStringFormats.Center);

            // Save the document...
            string filename = "HelloWorld.pdf";
            document.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);
        }
    }
}
