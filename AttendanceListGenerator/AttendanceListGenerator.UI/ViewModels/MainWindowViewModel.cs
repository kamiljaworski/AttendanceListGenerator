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
        public ICommand NextMonthCommand { get; private set; }
        public ICommand PreviousMonthCommand { get; private set; }

        public MainWindowViewModel()
        {
            Month = Month.February;

            NextMonthCommand = new RelayCommand(() =>
            {
                Month = EnumNavigator<Month>.Next(Month);
            });

            PreviousMonthCommand = new RelayCommand(() =>
            {
                Month = EnumNavigator<Month>.Previous(Month);
            });
        }
    }
}
