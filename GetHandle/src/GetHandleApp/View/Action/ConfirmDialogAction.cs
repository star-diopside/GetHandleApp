using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace GetHandleApp.View.Action
{
    class ConfirmDialogAction : TriggerAction<FrameworkElement>
    {
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(ConfirmDialogAction), new UIPropertyMetadata());
        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register("Caption", typeof(string), typeof(ConfirmDialogAction), new UIPropertyMetadata());
        public static readonly DependencyProperty ButtonProperty =
            DependencyProperty.Register("Button", typeof(MessageBoxButton), typeof(ConfirmDialogAction), new UIPropertyMetadata());
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(MessageBoxImage), typeof(ConfirmDialogAction), new UIPropertyMetadata());
        public static readonly DependencyProperty ConfirmResultProperty =
            DependencyProperty.Register("ConfirmResult", typeof(MessageBoxResult), typeof(ConfirmDialogAction), new UIPropertyMetadata());
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(ConfirmDialogAction), new UIPropertyMetadata());
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(ConfirmDialogAction), new UIPropertyMetadata());

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        public MessageBoxButton Button
        {
            get { return (MessageBoxButton)GetValue(ButtonProperty); }
            set { SetValue(ButtonProperty, value); }
        }

        public MessageBoxImage Icon
        {
            get { return (MessageBoxImage)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public MessageBoxResult ConfirmResult
        {
            get { return (MessageBoxResult)GetValue(ConfirmResultProperty); }
            set { SetValue(ConfirmResultProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        protected override void Invoke(object parameter)
        {
            if (MessageBox.Show(this.Message, this.Caption, this.Button, this.Icon) == this.ConfirmResult)
            {
                if (this.Command != null)
                {
                    this.Command.Execute(this.CommandParameter);
                }
            }
        }
    }
}
