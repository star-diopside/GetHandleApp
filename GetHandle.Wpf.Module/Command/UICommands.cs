using System.Windows.Input;

namespace GetHandle.Wpf.Module.Command
{
    static class UICommands
    {
        /// <summary>
        /// アプリケーション終了コマンド
        /// </summary>
        private static RoutedUICommand _applicationExit = new RoutedUICommand("アプリケーションの終了(_X)", "ApplicationExit",
                typeof(UICommands), new InputGestureCollection() { new KeyGesture(Key.F4, ModifierKeys.Alt) });

        /// <summary>
        /// アプリケーション終了コマンドを取得する。
        /// </summary>
        public static RoutedUICommand ApplicationExit
        {
            get
            {
                return _applicationExit;
            }
        }
    }
}
