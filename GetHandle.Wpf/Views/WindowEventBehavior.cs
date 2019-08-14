using System.ComponentModel;
using System.Windows;
using System.Windows.Interactivity;

namespace GetHandle.Wpf.Views
{
    public class WindowEventBehavior : Behavior<Window>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Closing += WindowClosing;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Closing -= WindowClosing;
        }

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show(AssociatedObject, "アプリケーションを終了してもよろしいですか？", "確認",
                                MessageBoxButton.OKCancel, MessageBoxImage.Question) != MessageBoxResult.OK)
            {
                e.Cancel = true;
            }
        }
    }
}
