using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using GetHandleApp.Command;
using GetHandleApp.Model;
using GetHandleApp.Util;
using GetHandleApp.Util.Enum;

namespace GetHandleApp.ViewModel
{
    public class MainWindowViewModel : IDataErrorInfo, INotifyPropertyChanged
    {
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

        #region PropertyChanged イベントの実装

        /// <summary>
        /// プロパティが変更されたときに発生するイベント。
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// PropertyChanged イベントを発生させる。
        /// </summary>
        /// <param name="propertyName">変更されたプロパティの名前</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// PropertyChanged イベントを発生させる。
        /// </summary>
        /// <param name="e">PropertyChanged イベントデータ</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, e);
            }
        }

        #endregion

        #region IDataErrorInfo の実装

        // エラー情報ヘルパークラス
        private readonly DataErrorInfoHelper _errorHelper = new DataErrorInfoHelper();

        string IDataErrorInfo.Error
        {
            get
            {
                return this._errorHelper.Error;
            }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                return this._errorHelper[columnName];
            }
        }

        #endregion

        #region Command の定義

        /// <summary>
        /// カーソル位置取得コマンド
        /// </summary>
        public ICommand UpdateCursorPosition { get; private set; }

        /// <summary>
        /// ハンドル取得コマンド
        /// </summary>
        public ICommand GetHandle { get; private set; }

        /// <summary>
        /// 自分自身のハンドル取得コマンド
        /// </summary>
        public ICommand GetOwnHandle { get; private set; }

        /// <summary>
        /// タスクバーのハンドル取得コマンド
        /// </summary>
        public ICommand GetTaskBarHandle { get; private set; }

        /// <summary>
        /// 取得したハンドルが示すウィンドウ名の変更コマンド
        /// </summary>
        public ICommand SetWindowName { get; private set; }

        /// <summary>
        /// 取得したハンドルが示すウィンドウのクローズコマンド
        /// </summary>
        public ICommand WindowClose { get; private set; }

        /// <summary>
        /// 取得したハンドルが示すウィンドウのレイヤード設定変更コマンド
        /// </summary>
        public ICommand UpdateLayeredWindowAttributes { get; private set; }

        /// <summary>
        /// コマンドの初期化を行う。
        /// </summary>
        private void InitializeCommand()
        {
            // カーソル位置取得コマンドの設定
            this.UpdateCursorPosition = new DelegateCommand()
            {
                ExecuteHandler = this.UpdateCursorPosition_Execute
            };

            // ハンドル取得コマンドの設定
            this.GetHandle = new DelegateCommand()
            {
                ExecuteHandler = delegate { this.Model.FindWindow(); }
            };

            // 自分自身のハンドル取得コマンドの設定
            this.GetOwnHandle = new DelegateCommand<Visual>()
            {
                ExecuteHandler = this.GetOwnHandle_Execute
            };

            // タスクバーのハンドル取得コマンドの設定
            this.GetTaskBarHandle = new DelegateCommand()
            {
                ExecuteHandler = delegate { this.Model.FindTaskBarHandle(); }
            };

            // 取得したハンドルが示すウィンドウ名の変更コマンドの設定
            this.SetWindowName = new DelegateCommand()
            {
                ExecuteHandler = delegate { this.Model.SetWindowText(); }
            };

            // 取得したハンドルが示すウィンドウのクローズコマンドの設定
            this.WindowClose = new DelegateCommand()
            {
                ExecuteHandler = delegate { this.Model.WindowClose(); }
            };

            // 取得したハンドルが示すウィンドウのレイヤード設定変更コマンドの設定
            this.UpdateLayeredWindowAttributes = new DelegateCommand();
        }

        #endregion

        #region プロパティ

        private string _bufferFindWindowPointX;
        private string _bufferFindWindowPointY;

        /// <summary>
        /// ウィンドウ検索済みであるかどうかを示す値を取得する。
        /// </summary>
        public bool IsFoundWindow
        {
            get
            {
                return this.Model.IsFoundWindow;
            }
        }

        /// <summary>
        /// ウィンドウ検索方法を取得または設定する。
        /// </summary>
        public FindWindowSpecifying Specifying
        {
            get
            {
                return this.Model.Specifying;
            }
            set
            {
                this.Model.Specifying = value;
            }
        }

        /// <summary>
        /// 位置でウィンドウを指定する場合の X 座標を取得または設定する。
        /// </summary>
        public string FindWindowPointX
        {
            get
            {
                if (this._bufferFindWindowPointX == null)
                {
                    return this.Model.FindWindowPointX.ToString();
                }
                else
                {
                    return this._bufferFindWindowPointX;
                }
            }
            set
            {
                this._bufferFindWindowPointX = value;

                // 入力チェックを行う。
                const string propertyName = "FindWindowPointX";
                int posX;

                if (!int.TryParse(this._bufferFindWindowPointX, out posX))
                {
                    this._errorHelper.ErrorMap[propertyName] = "整数を入力してください。";
                }
                else
                {
                    this.Model.FindWindowPointX = posX;
                    this._errorHelper.ErrorMap.Remove(propertyName);
                }

                // プロパティの変更を通知する。
                OnPropertyChanged(propertyName);
            }
        }

        /// <summary>
        /// 位置でウィンドウを指定する場合の X 座標の画面入力をリセットする。
        /// </summary>
        private void ResetFindWindowPointX()
        {
            this._bufferFindWindowPointX = null;
            this._errorHelper.ErrorMap.Remove("FindWindowPointX");
        }

        /// <summary>
        /// 位置でウィンドウを指定する場合の Y 座標を取得または設定する。
        /// </summary>
        public string FindWindowPointY
        {
            get
            {
                if (this._bufferFindWindowPointY == null)
                {
                    return this.Model.FindWindowPointY.ToString();
                }
                else
                {
                    return this._bufferFindWindowPointY;
                }
            }
            set
            {
                this._bufferFindWindowPointY = value;

                // 入力チェックを行う。
                const string propertyName = "FindWindowPointY";
                int posY;

                if (!int.TryParse(this._bufferFindWindowPointY, out posY))
                {
                    this._errorHelper.ErrorMap[propertyName] = "整数を入力してください。";
                }
                else
                {
                    this.Model.FindWindowPointY = posY;
                    this._errorHelper.ErrorMap.Remove(propertyName);
                }

                // プロパティの変更を通知する。
                OnPropertyChanged(propertyName);
            }
        }

        /// <summary>
        /// 位置でウィンドウを指定する場合の Y 座標の画面入力をリセットする。
        /// </summary>
        private void ResetFindWindowPointY()
        {
            this._bufferFindWindowPointY = null;
            this._errorHelper.ErrorMap.Remove("FindWindowPointY");
        }

        /// <summary>
        /// 名前でウィンドウを指定する場合のクラス名を取得または設定する。
        /// </summary>
        public string FindWindowClassName
        {
            get
            {
                return this.Model.FindWindowClassName;
            }
            set
            {
                this.Model.FindWindowClassName = value;
            }
        }

        /// <summary>
        /// 名前でウィンドウを指定する場合のウィンドウ名を取得または設定する。
        /// </summary>
        public string FindWindowTextName
        {
            get
            {
                return this.Model.FindWindowTextName;
            }
            set
            {
                this.Model.FindWindowTextName = value;
            }
        }

        /// <summary>
        /// 取得したウィンドウのクラス名を取得または設定する。
        /// </summary>
        public string FindWindowResultClassName
        {
            get
            {
                return this.Model.FindWindowResultClassName;
            }
        }

        /// <summary>
        /// 取得したウィンドウのウィンドウ名を取得または設定する。
        /// </summary>
        public string FindWindowResultTextName
        {
            get
            {
                return this.Model.FindWindowResultTextName;
            }
            set
            {
                this.Model.FindWindowResultTextName = value;
            }
        }

        #endregion

        /// <summary>
        /// モデルオブジェクト
        /// </summary>
        public GetHandleModel Model { get; set; }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            InitializeCommand();

            // モデルオブジェクトの PropertyChanged イベントを設定する。
            this.Model.PropertyChanged += (sender, e) =>
            {
                this.OnPropertyChanged(e);
            };
        }

        /// <summary>
        /// カーソル位置取得イベント
        /// </summary>
        private void UpdateCursorPosition_Execute(object parameter)
        {
            // 画面入力バッファをリセットする。
            this.ResetFindWindowPointX();
            this.ResetFindWindowPointY();

            // 現在のカーソル位置を取得し、モデルオブジェクトに設定する。
            POINT cursorPos;
            GetCursorPos(out cursorPos);

            this.Model.FindWindowPointX = cursorPos.X;
            this.Model.FindWindowPointY = cursorPos.Y;
        }

        /// <summary>
        /// 自分自身のハンドル取得イベント
        /// </summary>
        private void GetOwnHandle_Execute(Visual parameter)
        {
            HwndSource source = (HwndSource)HwndSource.FromVisual(parameter);
            this.Model.FindWindowFromHwnd(source.Handle);
        }
    }
}
