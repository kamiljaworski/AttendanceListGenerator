using AttendanceListGenerator.Core.Pdf;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PdfGenerator generator = new PdfGenerator();
            generator.Generate();
        }
    }
}
