﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kiosk.MVVMBase
{
    public class RelayCommand<T> : ICommand
    {
        readonly Action<T> _execute = null;
        readonly Predicate<T> _canExecute = null;


        public RelayCommand(Action<T> execute) : this(execute, null) { }
        

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            this._execute = execute;
            this._canExecute = canExecute;
        }


        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute((T)parameter);

        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);

        }
    }
}
