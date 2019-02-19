using AttendanceListGenerator.Core.Data;
using AttendanceListGenerator.Core.Helpers;
using AttendanceListGenerator.Core.IO;
using AttendanceListGenerator.Core.Pdf;
using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace AttendanceListGenerator.UI.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private const int _numberOfFullnames = 7;

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public Month Month { get; private set; }
        public int Year { get; private set; }
        public IList<string> Fullnames { get; set; }

        public ICommand NextYearCommand { get; private set; }
        public ICommand PreviousYearCommand { get; private set; }
        public ICommand NextMonthCommand { get; private set; }
        public ICommand PreviousMonthCommand { get; private set; }
        public ICommand GenerateCommand { get; private set; }

        public MainWindowViewModel(IDateTimeProvider dateTimeProvider)
        {
            if (dateTimeProvider == null)
                throw new ArgumentNullException("DateTimeProvider cannot be null");

            // Create Fullnames list and fill it
            Fullnames = new List<string>();
            for (int i = 0; i < _numberOfFullnames; i++)
                Fullnames.Add(string.Empty);

            _dateTimeProvider = dateTimeProvider;
            DateTime now = _dateTimeProvider.Now;

            Month = (Month)now.Month;
            Year = now.Year;

            NextYearCommand = new RelayCommand(() =>
            {
                Year++;
            });

            PreviousYearCommand = new RelayCommand(() =>
            {
                Year--;
            });

            NextMonthCommand = new RelayCommand(() =>
            {
                if (Month == Month.December)
                    Year++;

                Month = Month.Next();
            });

            PreviousMonthCommand = new RelayCommand(() =>
            {
                if (Month == Month.January)
                    Year--;

                Month = Month.Previous();
            });

            GenerateCommand = new RelayCommand(() =>
            {
                IList<IPerson> people = GetPeopleList();

                // Generate data
                IDaysOffData daysOff = new DaysOffData(Year);
                IAttendanceListData listData = new AttendanceListData(daysOff, people, Month, Year);

                // Create document generator
                ILocalizedNames localizedNames = new TempLocalizedNames();
                IAttendanceListDocumentGenerator documentGenerator = new AttendanceListDocumentGenerator(listData, localizedNames);

                // Generate a document
                Document document = documentGenerator.GenerateDocument();

                // Get directory path and filename
                IDirectoryProvider directoryProvider = new DirectoryProvider(localizedNames);
                IFilenameGenerator filenameGenerator = new FilenameGenerator(localizedNames, _dateTimeProvider);
                string path = directoryProvider.GetDocumentsDirectoryPath();
                string filename = filenameGenerator.GeneratePdfDocumentFilename(listData);

                // Save document
                IFileSaver fileSaver = new FileSaver();
                fileSaver.SavePdfDocument(document, path, filename);

                // And open it
                IFileOpener fileOpener = new FileOpener();
                fileOpener.OpenFile(path, filename);
            });
        }

        private IList<IPerson> GetPeopleList()
        {
            IList<IPerson> people = new List<IPerson>();

            foreach (string fullname in Fullnames)
                if (!string.IsNullOrEmpty(fullname))
                    people.Add(new Person(fullname));

            return people;
        }
    }
}
