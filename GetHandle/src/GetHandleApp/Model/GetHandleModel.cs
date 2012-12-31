using System;
using System.ComponentModel;
using GetHandle.Function;
using GetHandleApp.Util;
using GetHandleApp.Util.Enum;

namespace GetHandleApp.Model
{
    public class GetHandleModel : INotifyPropertyChanged
    {
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

        /// <summary>
        /// ウィンドウ操作オブジェクトファクトリ
        /// </summary>
        public IWindowProcFactory WindowProcFactory { get; set; }

        #region プロパティ

        /// <summary>
        /// ウィンドウ操作を行うクラスのインスタンス
        /// </summary>
        private IWindowProc _windowProc = null;

        /// <summary>
        /// ウィンドウ検索方法
        /// </summary>
        private FindWindowSpecifying _specifying = FindWindowSpecifying.Position;

        /// <summary>
        /// 位置でウィンドウを指定する場合の X 座標
        /// </summary>
        private int _findWindowPointX = 0;

        /// <summary>
        /// 位置でウィンドウを指定する場合の Y 座標
        /// </summary>
        private int _findWindowPointY = 0;

        /// <summary>
        /// 名前でウィンドウを指定する場合のクラス名
        /// </summary>
        private string _findWindowClassName;

        /// <summary>
        /// 名前でウィンドウを指定する場合のウィンドウ名
        /// </summary>
        private string _findWindowTextName;

        /// <summary>
        /// 取得したウィンドウのクラス名
        /// </summary>
        private string _findWindowResultClassName;

        /// <summary>
        /// 取得したウィンドウのウィンドウ名
        /// </summary>
        private string _findWindowResultTextName;

        /// <summary>
        /// ウィンドウ操作を行うクラスのインスタンスを取得または設定する。
        /// </summary>
        private IWindowProc WindowProc
        {
            get
            {
                return this._windowProc;
            }
            set
            {
                this._windowProc = value;
                this.OnPropertyChanged("IsFoundWindow");
                this.OnPropertyChanged("IsLayeredWindow");
            }
        }

        /// <summary>
        /// ウィンドウ検索済みであるかどうかを示す値を取得する。
        /// </summary>
        public bool IsFoundWindow
        {
            get
            {
                return this._windowProc != null;
            }
        }

        /// <summary>
        /// ウィンドウ検索方法を取得または設定する。
        /// </summary>
        public FindWindowSpecifying Specifying
        {
            get
            {
                return this._specifying;
            }
            set
            {
                this._specifying = value;
                this.OnPropertyChanged("Specifying");
            }
        }

        /// <summary>
        /// 位置でウィンドウを指定する場合の X 座標を取得または設定する。
        /// </summary>
        public int FindWindowPointX
        {
            get
            {
                return this._findWindowPointX;
            }
            set
            {
                this._findWindowPointX = value;
                OnPropertyChanged("FindWindowPointX");
            }
        }

        /// <summary>
        /// 位置でウィンドウを指定する場合の Y 座標を取得または設定する。
        /// </summary>
        public int FindWindowPointY
        {
            get
            {
                return this._findWindowPointY;
            }
            set
            {
                this._findWindowPointY = value;
                OnPropertyChanged("FindWindowPointY");
            }
        }

        /// <summary>
        /// 名前でウィンドウを指定する場合のクラス名を取得または設定する。
        /// </summary>
        public string FindWindowClassName
        {
            get
            {
                return this._findWindowClassName;
            }
            set
            {
                this._findWindowClassName = value;
                OnPropertyChanged("FindWindowClassName");
            }
        }

        /// <summary>
        /// 名前でウィンドウを指定する場合のウィンドウ名を取得または設定する。
        /// </summary>
        public string FindWindowTextName
        {
            get
            {
                return this._findWindowTextName;
            }
            set
            {
                this._findWindowTextName = value;
                OnPropertyChanged("FindWindowTextName");
            }
        }

        /// <summary>
        /// 取得したウィンドウのクラス名を取得または設定する。
        /// </summary>
        public string FindWindowResultClassName
        {
            get
            {
                return this._findWindowResultClassName;
            }
            set
            {
                this._findWindowResultClassName = value;
                OnPropertyChanged("FindWindowResultClassName");
            }
        }

        /// <summary>
        /// 取得したウィンドウのウィンドウ名を取得または設定する。
        /// </summary>
        public string FindWindowResultTextName
        {
            get
            {
                return this._findWindowResultTextName;
            }
            set
            {
                this._findWindowResultTextName = value;
                OnPropertyChanged("FindWindowResultTextName");
            }
        }

        /// <summary>
        /// 取得したウィンドウがレイヤードウィンドウかどうかを示す値を取得する。
        /// </summary>
        public bool IsLayeredWindow
        {
            get
            {
                if (this.WindowProc == null)
                {
                    return false;
                }
                else
                {
                    return this.WindowProc.IsLayeredWindow;
                }
            }
        }

        #endregion

        /// <summary>
        /// プロパティ情報をもとにハンドルを取得する。
        /// </summary>
        public void FindWindow()
        {
            // 現在の取得しているウィンドウ操作オブジェクトを無効にする。
            this.WindowProc = null;

            // ウィンドウ操作オブジェクトの取得処理を行う。
            IWindowProc windowProc;

            if (this.Specifying == FindWindowSpecifying.Position)
            {
                System.Drawing.Point p = new System.Drawing.Point(this.FindWindowPointX, this.FindWindowPointY);
                windowProc = this.WindowProcFactory.FindWindow(p);
            }
            else if (this.Specifying == FindWindowSpecifying.WindowClass)
            {
                string className = null;
                string windowName = null;

                if (!string.IsNullOrEmpty(this.FindWindowClassName))
                {
                    className = this.FindWindowClassName;
                }
                if (!string.IsNullOrEmpty(this.FindWindowTextName))
                {
                    windowName = this.FindWindowTextName;
                }

                windowProc = this.WindowProcFactory.FindWindow(className, windowName);
            }
            else
            {
                throw new InvalidOperationException();
            }

            FindWindow(windowProc);
        }

        /// <summary>
        /// ウィンドウハンドルをもとにハンドルを取得する。
        /// </summary>
        public void FindWindowFromHwnd(IntPtr hwnd)
        {
            // 現在の取得しているウィンドウ操作オブジェクトを無効にする。
            this.WindowProc = null;

            // ウィンドウハンドルを取得する。
            FindWindow(this.WindowProcFactory.GetControlWindow(hwnd));
        }

        /// <summary>
        /// タスクバーのハンドルを取得する。
        /// </summary>
        public void FindTaskBarHandle()
        {
            // 現在の取得しているウィンドウ操作オブジェクトを無効にする。
            this.WindowProc = null;

            // タスクバーのウィンドウハンドルを取得する。
            FindWindow(this.WindowProcFactory.GetTaskBarWindow());
        }

        /// <summary>
        /// ウィンドウ取得処理を行う。
        /// </summary>
        /// <param name="windowProc">ウィンドウ操作を行うオブジェクト</param>
        private void FindWindow(IWindowProc windowProc)
        {
            // クラス名とウィンドウ名を取得する。
            this.FindWindowResultClassName = windowProc.GetClassName();
            this.FindWindowResultTextName = windowProc.GetWindowText();

            // ウィンドウ操作を行うオブジェクトをプライベートフィールドに保持する。
            this.WindowProc = windowProc;
        }

        /// <summary>
        /// プロパティ情報をもとにウィンドウ名を設定する。
        /// </summary>
        public void SetWindowText()
        {
            this.WindowProc.SetWindowText(this.FindWindowResultTextName);
        }

        /// <summary>
        /// 取得したハンドルが示すウィンドウのクローズイベント
        /// </summary>
        public void WindowClose()
        {
            this.WindowProc.CloseWindow();
        }
    }
}
