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

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public Month Month { get; private set; }
        public int Year { get; private set; }
        public string FirstPersonFullname { get; set; }
        public string SecondPersonFullname { get; set; }
        public string ThirdPersonFullname { get; set; }
        public string FourthPersonFullname { get; set; }
        public string FifthPersonFullname { get; set; }
        public string SixthPersonFullname { get; set; }
        public string SeventhPersonFullname { get; set; }

        public ICommand NextYearCommand { get; private set; }
        public ICommand PreviousYearCommand { get; private set; }
        public ICommand NextMonthCommand { get; private set; }
        public ICommand PreviousMonthCommand { get; private set; }
        public ICommand GenerateCommand { get; private set; }

        public MainWindowViewModel(IDateTimeProvider dateTimeProvider)
        {
            if (dateTimeProvider == null)
                throw new ArgumentNullException("DateTimeProvider cannot be null");

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
                IList<IPerson> people = new List<IPerson>();

                if (!string.IsNullOrEmpty(FirstPersonFullname))
                    people.Add(new Person(FirstPersonFullname));

                if (!string.IsNullOrEmpty(SecondPersonFullname))
                    people.Add(new Person(SecondPersonFullname));

                if (!string.IsNullOrEmpty(ThirdPersonFullname))
                    people.Add(new Person(ThirdPersonFullname));

                if (!string.IsNullOrEmpty(FourthPersonFullname))
                    people.Add(new Person(FourthPersonFullname));

                if (!string.IsNullOrEmpty(FifthPersonFullname))
                    people.Add(new Person(FifthPersonFullname));

                if (!string.IsNullOrEmpty(SixthPersonFullname))
                    people.Add(new Person(SixthPersonFullname));

                if (!string.IsNullOrEmpty(SeventhPersonFullname))
                    people.Add(new Person(SeventhPersonFullname));

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
    }
}
