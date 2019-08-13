using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace GetHandle.Wpf.Module.Views
{
    public class ApplicationCommandsBehavior : Behavior<UIElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, CloseCommandExecuted));
        }

        private void CloseCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Window.GetWindow(AssociatedObject).Close();
        }
    }
}
