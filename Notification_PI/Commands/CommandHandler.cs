using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Notification_PI.Commands
{
    public class CommandHandler : ICommand
    {


        private readonly Action<object> _action;
        private readonly Func<object, bool> _canExecute;
        public CommandHandler(Action<object> action, Func<object, bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute ?? (x => true);
        }

        public CommandHandler(Action<object> action) : this(action,null)
        {
            
        }

        public event EventHandler CanExecuteChanged {

            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }
    }
}
