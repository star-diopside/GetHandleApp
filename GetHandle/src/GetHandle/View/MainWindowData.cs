using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using GetHandle.Function;

namespace GetHandle.View
{
    class MainWindowData : IDataErrorInfo, INotifyPropertyChanged
    {
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

        /// <summary>
        /// プロパティに関するエラーメッセージを格納するコレクション
        /// </summary>
        private readonly Dictionary<string, string> _errors = new Dictionary<string, string>();

        /// <summary>
        /// オブジェクトに関するエラーが存在するかどうかを示す値を取得する。
        /// </summary>
        public bool HasError
        {
            get
            {
                return this._errors.Count != 0 || this.Error != null;
            }
        }

        /// <summary>
        /// オブジェクト全体に関するエラーメッセージを取得する。
        /// </summary>
        public string Error
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// 指定した名前のプロパティに関するエラーメッセージを取得する。
        /// </summary>
        /// <param name="columnName">エラーメッセージを取得する対象のプロパティの名前</param>
        /// <returns>プロパティに関するエラーメッセージ</returns>
        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                return this._errors.ContainsKey(columnName) ? this._errors[columnName] : null;
            }
        }

        /// <summary>
        /// ウィンドウ操作を行うクラスのインスタンス
        /// </summary>
        private IWindowProc _windowProc = null;

        /// <summary>
        /// 位置でウィンドウを指定する場合の X 座標
        /// </summary>
        private string _findWindowPointX = "0";

        /// <summary>
        /// 位置でウィンドウを指定する場合の Y 座標
        /// </summary>
        private string _findWindowPointY = "0";

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
        public IWindowProc WindowProc
        {
            get
            {
                return this._windowProc;
            }
            set
            {
                this._windowProc = value;
                this.OnPropertyChanged("WindowProc");
                this.OnPropertyChanged("IsFoundWindow");
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
        /// 位置でウィンドウを指定する場合の X 座標を取得または設定する。
        /// </summary>
        public string FindWindowPointX
        {
            get
            {
                return this._findWindowPointX;
            }
            set
            {
                this._findWindowPointX = value;

                // 入力チェックを行う。
                const string propertyName = "FindWindowPointX";
                int posX;

                if (!int.TryParse(this._findWindowPointX, out posX))
                {
                    this._errors[propertyName] = "整数を入力してください。";
                }
                else
                {
                    this._errors.Remove(propertyName);
                }

                // プロパティの変更を通知する。
                OnPropertyChanged(propertyName);
            }
        }

        /// <summary>
        /// 位置でウィンドウを指定する場合の Y 座標を取得または設定する。
        /// </summary>
        public string FindWindowPointY
        {
            get
            {
                return this._findWindowPointY;
            }
            set
            {
                this._findWindowPointY = value;

                // 入力チェックを行う。
                const string propertyName = "FindWindowPointY";
                int posY;

                if (!int.TryParse(this._findWindowPointY, out posY))
                {
                    this._errors[propertyName] = "整数を入力してください。";
                }
                else
                {
                    this._errors.Remove(propertyName);
                }

                // プロパティの変更を通知する。
                OnPropertyChanged(propertyName);
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
    }
}
