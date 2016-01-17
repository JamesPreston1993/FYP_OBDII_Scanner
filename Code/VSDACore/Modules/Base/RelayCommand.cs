using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VSDACore.Modules.Base
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action action;
        private Func<bool> canExecute;

        public RelayCommand(Action action)
        {
            this.action = action;
            this.canExecute = null;
        }

        public RelayCommand(Action action, Func<bool> canExecute)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (this.canExecute != null)
                return canExecute();
            return true;
        }

        public void Execute(object parameter)
        {
            action.Invoke();
        }
    }
}
