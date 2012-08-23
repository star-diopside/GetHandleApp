using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using GetHandle.Function;

namespace GetHandle.View
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// ウィンドウで保持するデータ
        /// </summary>
        private readonly MainWindowData _data = new MainWindowData();

        #region P/Invoke

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public static implicit operator System.Drawing.Point(POINT p)
            {
                return new System.Drawing.Point(p.X, p.Y);
            }

            public static implicit operator POINT(System.Drawing.Point p)
            {
                return new POINT(p.X, p.Y);
            }
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out POINT lpPoint);

        #endregion

        #region Command

        /// <summary>
        /// アプリケーション終了コマンド
        /// </summary>
        public static readonly ICommand ApplicationExit = new RoutedUICommand("アプリケーションの終了(_X)", "ApplicationExit",
                typeof(MainWindow), new InputGestureCollection() { new KeyGesture(Key.F4, ModifierKeys.Alt) });

        /// <summary>
        /// カーソル位置取得コマンド
        /// </summary>
        public static readonly ICommand UpdateCursorPosition = new RoutedCommand("UpdateCursorPosition", typeof(MainWindow));

        /// <summary>
        /// ハンドル取得コマンド
        /// </summary>
        public static readonly ICommand GetHandle = new RoutedCommand("GetHandle", typeof(MainWindow));

        /// <summary>
        /// 自分自身のハンドル取得コマンド
        /// </summary>
        public static readonly ICommand GetOwnHandle = new RoutedCommand("GetOwnHandle", typeof(MainWindow));

        /// <summary>
        /// タスクバーのハンドル取得コマンド
        /// </summary>
        public static readonly ICommand GetTaskBarHandle = new RoutedCommand("GetTaskBarHandle", typeof(MainWindow));

        /// <summary>
        /// 取得したハンドルが示すウィンドウ名の変更コマンド
        /// </summary>
        public static readonly ICommand SetWindowName = new RoutedCommand("SetWindowName", typeof(MainWindow));

        /// <summary>
        /// 取得したハンドルが示すウィンドウのクローズコマンド
        /// </summary>
        public static readonly ICommand WindowClose = new RoutedCommand("WindowClose", typeof(MainWindow));

        /// <summary>
        /// 取得したハンドルが示すウィンドウのレイヤード設定変更コマンド
        /// </summary>
        public static readonly ICommand UpdateLayeredWindowAttributes = new RoutedCommand("UpdateLayeredWindowAttributes", typeof(MainWindow));

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this._data;
        }

        protected override void OnClosing(CancelEventArgs e)
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

        /// <summary>
        /// カーソル画像表示用ピクチャボックスのマウスダウンイベント
        /// </summary>
        private void pictureCursor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image senderControl = (Image)sender;

            // マウスイベントをキャプチャする。
            senderControl.CaptureMouse();

            // コントロール表示画像を変更する。
            senderControl.Source = (ImageSource)this.Resources["EmptyImage"];

            // マウスカーソルを変更する。
            Mouse.OverrideCursor = new Cursor(new MemoryStream(Properties.Resources.TargetCursor, false));
        }

        /// <summary>
        /// カーゾル画像表示用ピクチャボックスのマウスアップイベント
        /// </summary>
        private void pictureCursor_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Image senderControl = (Image)sender;

            // マウスイベントのキャプチャを解放する。
            senderControl.ReleaseMouseCapture();

            // コントロール表示画像を変更する。
            senderControl.Source = (ImageSource)this.Resources["CursorImage"];

            // マウスカーソルを元に戻す。
            Mouse.OverrideCursor = null;
        }

        /// <summary>
        /// カーソル画像表示用ピクチャボックスのマウス移動イベント
        /// </summary>
        private void pictureCursor_MouseMove(object sender, MouseEventArgs e)
        {
            Image senderControl = (Image)sender;

            // マウスキャプチャされている場合、カーソル位置をコントロールの値に反映する。
            if (senderControl.IsMouseCaptured)
            {
                UpdateCursorPositionProc();
            }
        }

        /// <summary>
        /// カーソル位置取得イベント
        /// </summary>
        private void UpdateCursorPosition_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // カーソル位置をコントロールの値に反映する。
            UpdateCursorPositionProc();
        }

        /// <summary>
        /// カーソル位置を取得し、コントロールの値を変更する。
        /// </summary>
        private void UpdateCursorPositionProc()
        {
            POINT cursorPos;
            GetCursorPos(out cursorPos);

            this._data.FindWindowPointX = cursorPos.X.ToString();
            this._data.FindWindowPointY = cursorPos.Y.ToString();
        }

        /// <summary>
        /// ハンドル取得イベント
        /// </summary>
        private void GetHandle_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // 現在の取得しているウィンドウ操作オブジェクトを無効にする。
            this._data.WindowProc = null;

            // ウィンドウ操作オブジェクトの取得処理を行う。
            IWindowProc windowProc;

            if (radioPosition.IsChecked == true)
            {
                System.Drawing.Point p = new System.Drawing.Point(int.Parse(this._data.FindWindowPointX), int.Parse(this._data.FindWindowPointY));
                windowProc = WindowProcFactoryMethod.Instance.FindWindow(p);
            }
            else if (radioWindowClass.IsChecked == true)
            {
                string className = null;
                string windowName = null;

                if (!string.IsNullOrEmpty(this._data.FindWindowClassName))
                {
                    className = this._data.FindWindowClassName;
                }
                if (!string.IsNullOrEmpty(this._data.FindWindowTextName))
                {
                    windowName = this._data.FindWindowTextName;
                }

                windowProc = WindowProcFactoryMethod.Instance.FindWindow(className, windowName);
            }
            else
            {
                throw new InvalidOperationException();
            }

            FindWindow(windowProc);
        }

        /// <summary>
        /// 自分自身のハンドル取得イベント
        /// </summary>
        private void GetOwnHandle_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // 現在の取得しているウィンドウ操作オブジェクトを無効にする。
            this._data.WindowProc = null;

            // 自分自身のウィンドウハンドルを取得する。
            HwndSource source = (HwndSource)HwndSource.FromVisual(this);
            FindWindow(WindowProcFactoryMethod.Instance.GetControlWindow(source.Handle));
        }

        /// <summary>
        /// タスクバーのハンドル取得イベント
        /// </summary>
        private void GetTaskBarHandle_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // 現在の取得しているウィンドウ操作オブジェクトを無効にする。
            this._data.WindowProc = null;

            // タスクバーのウィンドウハンドルを取得する。
            FindWindow(WindowProcFactoryMethod.Instance.GetTaskBarWindow());
        }

        /// <summary>
        /// ウィンドウ取得処理を行う。
        /// </summary>
        /// <param name="windowProc">ウィンドウ操作を行うオブジェクト</param>
        private void FindWindow(IWindowProc windowProc)
        {
            // クラス名とウィンドウ名を取得する。
            this._data.FindWindowResultClassName = windowProc.GetClassName();
            this._data.FindWindowResultTextName = windowProc.GetWindowText();

            // レイヤードウィンドウ属性を取得する。
            this.checkLayeredWindow.IsChecked = windowProc.IsLayeredWindow;

            // ウィンドウ操作を行うオブジェクトをプライベートフィールドに保持する。
            this._data.WindowProc = windowProc;
        }

        /// <summary>
        /// 取得したハンドルが示すウィンドウ名の変更イベント
        /// </summary>
        private void SetWindowName_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._data.WindowProc.SetWindowText(this._data.FindWindowResultTextName);
        }

        /// <summary>
        /// 取得したハンドルが示すウィンドウのクローズイベント
        /// </summary>
        private void WindowClose_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show(this, "この操作による動作保障はできません。実行しますか？", "警告",
                                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                this._data.WindowProc.CloseWindow();
            }
        }

        /// <summary>
        /// 取得したハンドルが示すウィンドウのレイヤード設定変更イベント
        /// </summary>
        private void UpdateLayeredWindowAttributes_Executed(object sender, ExecutedRoutedEventArgs e)
        {
        }
    }
}
