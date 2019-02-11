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
            string filename = new FilenameGenerator(localizedNames, new DateTimeProvider()).GeneratePdfDocumentFilename(attendanceListData);

            
            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);


            FileSaver fileSaver = new FileSaver();
            bool fileExist = fileSaver.SavePdfDocument(document, path, filename);

            if (fileExist)
                new FileOpener().OpenFile(path, filename);
        }
    }
}
