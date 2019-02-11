using AttendanceListGenerator.Core.Data;
using AttendanceListGenerator.Core.IO;
using AttendanceListGenerator.Core.Pdf;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace AttendanceListGenerator.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public object PdfFontEmbedding { get; private set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IList<IPerson> people = new List<IPerson>
            {
                new Person ("James", "Hunt"),
                new Person ("William", "Jefferson")
            };

            IAttendanceListData attendanceListData = new AttendanceListData(new DaysOffData(2019), people, Month.January, 2019);
            ILocalizedNames localizedNames = new TempLocalizedNames();
            IAttendanceListDocumentGenerator documentGenerator = new AttendanceListDocumentGenerator(attendanceListData, localizedNames);
            Document document = documentGenerator.GenerateDocument();

            string path = new DirectoryProvider(localizedNames).GetDocumentsDirectoryPath();
            string filename = new FilenameGenerator(attendanceListData, localizedNames, new DateTimeProvider()).GeneratePdfDocumentFilename();

            
            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);


            SaveDocument(document, path + "\\" + filename);
        }

        private void SaveDocument(Document document, string filename)
        {
            MigraDoc.DocumentObjectModel.IO.DdlWriter.WriteToFile(document, "MigraDoc.mdddl");

            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true, PdfSharp1_32.Pdf.PdfFontEmbedding.Always);
            renderer.Document = document;

            renderer.RenderDocument();

            // Save the document...
            //string filename = "SimpleTable.pdf";
            renderer.PdfDocument.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);
        }
    }
}
