using AttendanceListGenerator.Core.Data;
using AttendanceListGenerator.Core.Helpers;
using System;
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

        public ICommand NextYearCommand { get; private set; }
        public ICommand PreviousYearCommand { get; private set; }
        public ICommand NextMonthCommand { get; private set; }
        public ICommand PreviousMonthCommand { get; private set; }

        public MainWindowViewModel(IDateTimeProvider dateTimeProvider)
        {
            if (dateTimeProvider == null)
                throw new ArgumentNullException("Date Time Provider cannot be null");

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
        }
    }
}
