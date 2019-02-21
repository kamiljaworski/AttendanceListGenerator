using AttendanceListGenerator.Core.Data;
using AttendanceListGenerator.Core.Helpers;
using AttendanceListGenerator.Core.IO;
using AttendanceListGenerator.Core.Pdf;
using AttendanceListGenerator.UI.Localization;
using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;

namespace AttendanceListGenerator.UI.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private const int _numberOfFullnames = 7;
        private const int _minYear = 1900;
        private const int _maxYear = 2100;

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public Month Month { get; private set; }
        public int Year { get; private set; }
        public IList<string> Fullnames { get; set; }

        public bool EnableColors { get; set; } = true;
        public bool EnableHolidaysTexts { get; set; } = true;
        public bool EnableSundaysTexts { get; set; } = true;
        public bool EnableTableStretching { get; set; } = true;

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

            CreateApplicationDirectories();

            Settings settings = DeserializeSettings();

            if (settings != null)
                UseSettings(settings);
            else
                CreateListOfEmptyFullnames();


            // Set current Month and Year
            Month = (Month)now.Month;
            Year = now.Year;

            // Create commands
            NextYearCommand = new RelayCommand(NextYear);
            PreviousYearCommand = new RelayCommand(PreviousYear);
            NextMonthCommand = new RelayCommand(NextMonth);
            PreviousMonthCommand = new RelayCommand(PreviousMonth);
            GenerateCommand = new RelayCommand(Generate);
        }

        private void CreateApplicationDirectories()
        {
            LocalizedNames localizedNames = new LocalizedNames();
            DirectoryProvider directoryProvider = new DirectoryProvider(localizedNames);

            if (!Directory.Exists(directoryProvider.GetApplicationDirectoryPath()))
                Directory.CreateDirectory(directoryProvider.GetApplicationDirectoryPath());

            if (!Directory.Exists(directoryProvider.GetDocumentsDirectoryPath()))
                Directory.CreateDirectory(directoryProvider.GetDocumentsDirectoryPath());
        }

        private Settings DeserializeSettings()
        {
            try
            {
                LocalizedNames localizedNames = new LocalizedNames();
                DirectoryProvider directoryProvider = new DirectoryProvider(localizedNames);
                FilenameGenerator filenameGenerator = new FilenameGenerator(localizedNames, _dateTimeProvider);

                string applicationDirectory = directoryProvider.GetApplicationDirectoryPath();
                string settingsFilename = filenameGenerator.GenerateJsonSettingsFilename();

                FileReader fileReader = new FileReader();
                string json = fileReader.ReadJsonFile(applicationDirectory, settingsFilename);

                SettingsSerializer serializer = new SettingsSerializer();
                return serializer.Deserialize(json);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void UseSettings(Settings settings)
        {
            EnableColors = settings.EnableColors;
            EnableHolidaysTexts = settings.EnableHolidaysTexts;
            EnableSundaysTexts = settings.EnableSundaysTexts;
            EnableTableStretching = settings.EnableTableStretching;

            Fullnames = new List<string>();
            for (int i = 0; i < _numberOfFullnames; i++)
                if (i < settings.Fullnames.Count)
                    Fullnames.Add(settings.Fullnames[i]);
                else
                    Fullnames.Add(string.Empty);
        }

        private void CreateListOfEmptyFullnames()
        {
            Fullnames = new List<string>();
            for (int i = 0; i < _numberOfFullnames; i++)
                Fullnames.Add(string.Empty);
        }

        private void NextYear() => Year = (Year < _maxYear) ? ++Year : Year;
        private void PreviousYear() => Year = (Year > _minYear) ? --Year : Year;
        private void NextMonth()
        {
            if (Month == Month.December)
                NextYear();

            Month = Month.Next();
        }

        private void PreviousMonth()
        {
            if (Month == Month.January)
                PreviousYear();

            Month = Month.Previous();
        }

        private void Generate()
        {
            IList<IPerson> people = GetPeopleList();

            // Generate data
            DaysOffData daysOff = new DaysOffData(Year);
            AttendanceListData listData = new AttendanceListData(daysOff, people, Month, Year);

            // Create document generator
            LocalizedNames localizedNames = new LocalizedNames();
            AttendanceListDocumentGenerator documentGenerator = new AttendanceListDocumentGenerator(listData, localizedNames);

            // Set document generator settings
            documentGenerator.EnableColors = EnableColors;
            documentGenerator.EnableHolidaysTexts = EnableHolidaysTexts;
            documentGenerator.EnableSundaysTexts = EnableSundaysTexts;
            documentGenerator.EnableTableStretching = EnableTableStretching;

            // Generate a document
            Document document = documentGenerator.GenerateDocument();

            // Get directory path and filename
            DirectoryProvider directoryProvider = new DirectoryProvider(localizedNames);
            FilenameGenerator filenameGenerator = new FilenameGenerator(localizedNames, _dateTimeProvider);
            string path = directoryProvider.GetDocumentsDirectoryPath();
            string filename = filenameGenerator.GeneratePdfDocumentFilename(listData);

            // Save document
            FileSaver fileSaver = new FileSaver();
            fileSaver.SavePdfDocument(document, path, filename);

            // And open it
            FileOpener fileOpener = new FileOpener();
            fileOpener.OpenFile(path, filename);

            SerializeSettings(directoryProvider, filenameGenerator, fileSaver);
        }

        private IList<IPerson> GetPeopleList()
        {
            IList<IPerson> people = new List<IPerson>();

            foreach (string fullname in Fullnames)
                if (!string.IsNullOrEmpty(fullname))
                    people.Add(new Person(fullname));

            return people;
        }

        private void SerializeSettings(IDirectoryProvider directoryProvider, IFilenameGenerator filenameGenerator, IFileSaver fileSaver)
        {
            // Create Settings object
            Settings settings = new Settings
            {
                EnableColors = EnableColors,
                EnableHolidaysTexts = EnableHolidaysTexts,
                EnableSundaysTexts = EnableSundaysTexts,
                EnableTableStretching = EnableTableStretching,
                Fullnames = Fullnames
            };

            // Serialize settings
            SettingsSerializer serializer = new SettingsSerializer();
            string json = serializer.Serialize(settings);

            // Get directory and filename
            string path = directoryProvider.GetApplicationDirectoryPath();
            string filename = filenameGenerator.GenerateJsonSettingsFilename();

            // Save serialized setting to file
            fileSaver.SaveJsonFile(json, path, filename);
        }
    }
}
