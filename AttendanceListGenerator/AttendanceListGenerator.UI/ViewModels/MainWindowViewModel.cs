using AttendanceListGenerator.Core.Data;
using AttendanceListGenerator.Core.Helpers;
using System.ComponentModel;
using System.Windows.Input;

namespace AttendanceListGenerator.UI.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public Month Month { get; private set; }
        public int Year { get; private set; }

        public ICommand NextMonthCommand { get; private set; }
        public ICommand PreviousMonthCommand { get; private set; }

        public MainWindowViewModel()
        {
            Month = Month.February;
            Year = 2019;

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
