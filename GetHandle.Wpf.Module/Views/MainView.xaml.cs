using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GetHandle.Wpf.Module.Views
{
    /// <summary>
    /// MainView.xaml の相互作用ロジック
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// アプリケーション終了イベント
        /// </summary>
        private void ApplicationExit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }
    }
}
