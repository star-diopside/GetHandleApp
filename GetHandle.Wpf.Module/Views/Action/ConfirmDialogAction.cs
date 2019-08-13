using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace GetHandle.Wpf.Module.Views.Action
{
    class ConfirmDialogAction : TriggerAction<FrameworkElement>
    {
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register(nameof(Message), typeof(string), typeof(ConfirmDialogAction), new UIPropertyMetadata());
        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register(nameof(Caption), typeof(string), typeof(ConfirmDialogAction), new UIPropertyMetadata());
        public static readonly DependencyProperty ButtonProperty =
            DependencyProperty.Register(nameof(Button), typeof(MessageBoxButton), typeof(ConfirmDialogAction), new UIPropertyMetadata());
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(nameof(Icon), typeof(MessageBoxImage), typeof(ConfirmDialogAction), new UIPropertyMetadata());
        public static readonly DependencyProperty ConfirmResultProperty =
            DependencyProperty.Register(nameof(ConfirmResult), typeof(MessageBoxResult), typeof(ConfirmDialogAction), new UIPropertyMetadata());
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(ConfirmDialogAction), new UIPropertyMetadata());
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(ConfirmDialogAction), new UIPropertyMetadata());

        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public string Caption
        {
            get => (string)GetValue(CaptionProperty);
            set => SetValue(CaptionProperty, value);
        }

        public MessageBoxButton Button
        {
            get => (MessageBoxButton)GetValue(ButtonProperty);
            set => SetValue(ButtonProperty, value);
        }

        public MessageBoxImage Icon
        {
            get => (MessageBoxImage)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public MessageBoxResult ConfirmResult
        {
            get => (MessageBoxResult)GetValue(ConfirmResultProperty);
            set => SetValue(ConfirmResultProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        protected override void Invoke(object parameter)
        {
            if (MessageBox.Show(Message, Caption, Button, Icon) == ConfirmResult)
            {
                Command?.Execute(CommandParameter);
            }
        }
    }
}
