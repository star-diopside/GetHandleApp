using System;
using System.Windows.Input;

namespace GetHandleApp.Command
{
    class DelegateCommand : DelegateCommand<object>
    {
    }

    class DelegateCommand<T> : ICommand
    {
        public Func<T, bool> CanExecuteHandler { get; set; }
        public Action<T> ExecuteHandler { get; set; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (CanExecuteHandler == null)
            {
                return this.ExecuteHandler != null;
            }
            else
            {
                return this.CanExecuteHandler((T)parameter);
            }
        }

        public void Execute(object parameter)
        {
            if (this.ExecuteHandler != null)
            {
                this.ExecuteHandler((T)parameter);
            }
        }

        public void RaiseCanExecuteChanged()
        {
            this.OnCanExecuteChanged(new EventArgs());
        }

        protected virtual void OnCanExecuteChanged(EventArgs e)
        {
            if (this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, e);
            }
        }
    }
}
