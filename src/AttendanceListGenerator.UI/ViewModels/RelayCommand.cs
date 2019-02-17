using System;
using System.Windows.Input;

namespace AttendanceListGenerator.UI.ViewModels
{
    public class RelayCommand : ICommand
    {
        private Action _action;

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public RelayCommand(Action action)
        {
            if (action == null)
                throw new ArgumentNullException("Action cannot be null");

            _action = action;
        }

        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter) => _action();
    }
}
