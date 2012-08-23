using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GetHandleApp.Util
{
    /// <summary>
    /// IDataErrorInfo の実装を提供するヘルパークラス
    /// </summary>
    public class DataErrorInfoHelper : IDataErrorInfo
    {
        /// <summary>
        /// プロパティに関するエラーメッセージを格納するコレクション
        /// </summary>
        private readonly Dictionary<string, string> _errors = new Dictionary<string, string>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DataErrorInfoHelper()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="handler">オブジェクト全体に関するエラーチェック処理</param>
        public DataErrorInfoHelper(Func<string> handler)
        {
            this.CheckErrorHandler = handler;
        }

        /// <summary>
        /// プロパティに関するエラーメッセージを格納するコレクションを取得する。
        /// </summary>
        public Dictionary<string, string> ErrorMap
        {
            get
            {
                return this._errors;
            }
        }

        /// <summary>
        /// オブジェクト全体に関するエラーチェック処理を取得または設定する。
        /// </summary>
        public Func<string> CheckErrorHandler { get; set; }

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
                return this.CheckErrorHandler == null ? null : this.CheckErrorHandler();
            }
        }

        /// <summary>
        /// 指定した名前のプロパティに関するエラーメッセージを取得する。
        /// </summary>
        /// <param name="columnName">エラーメッセージを取得する対象のプロパティの名前</param>
        /// <returns>プロパティに関するエラーメッセージ</returns>
        public string this[string columnName]
        {
            get
            {
                return this._errors.ContainsKey(columnName) ? this._errors[columnName] : null;
            }
        }
    }
}
