﻿using System.Windows;
using System.Windows.Input;
using GetHandleApp.ViewModel;

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
            this.DataContext = new MainWindowViewModel();
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
