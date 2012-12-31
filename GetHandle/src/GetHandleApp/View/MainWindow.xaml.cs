using System.Windows;
using System.Windows.Input;
using GetHandleApp.ViewModel;
using Spring.Context;
using Spring.Context.Support;

namespace GetHandleApp.View
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            IApplicationContext ctx = ContextRegistry.GetContext();
            this.DataContext = (MainWindowViewModel)ctx.GetObject("MainWindowViewModel");
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            if (MessageBox.Show(this, "アプリケーションを終了してもよろしいですか？", "確認",
                                MessageBoxButton.OKCancel, MessageBoxImage.Question) != MessageBoxResult.OK)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// アプリケーション終了イベント
        /// </summary>
        private void ApplicationExit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
    }
}
