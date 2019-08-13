using GetHandle.Wpf.Module.Utility.Enum;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using WindowHandleInterface.Function;

namespace GetHandle.Wpf.Module.Models
{
    public class FindWindowModel : IFindWindowModel, IDisposable
    {
        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        /// <summary>
        /// ウィンドウ操作オブジェクトファクトリ
        /// </summary>
        private readonly IWindowProcFactory _windowProcFactory;

        public FindWindowModel(IWindowProcFactory windowProcFactory)
        {
            _windowProcFactory = windowProcFactory;

            // プロパティを設定する。
            IsFoundWindow = WindowProc.Select(x => x != null).ToReadOnlyReactivePropertySlim().AddTo(_disposable);
            IsLayeredWindow = WindowProc.Select(x => x != null && x.IsLayeredWindow).ToReadOnlyReactivePropertySlim().AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        #region プロパティ

        /// <summary>
        /// ウィンドウ操作を行うクラスのインスタンスのプロパティを取得する。
        /// </summary>
        private ReactivePropertySlim<IWindowProc> WindowProc { get; } = new ReactivePropertySlim<IWindowProc>();

        /// <summary>
        /// ウィンドウ検索方法のプロパティを取得する。
        /// </summary>
        public ReactivePropertySlim<FindWindowSpecifying> Specifying { get; } = new ReactivePropertySlim<FindWindowSpecifying>(FindWindowSpecifying.Position);

        /// <summary>
        /// 位置でウィンドウを指定する場合の X 座標のプロパティを取得する。
        /// </summary>
        public ReactivePropertySlim<int> FindWindowPointX { get; } = new ReactivePropertySlim<int>();

        /// <summary>
        /// 位置でウィンドウを指定する場合の Y 座標のプロパティを取得する。
        /// </summary>
        public ReactivePropertySlim<int> FindWindowPointY { get; } = new ReactivePropertySlim<int>();

        /// <summary>
        /// 名前でウィンドウを指定する場合のクラス名のプロパティを取得する。
        /// </summary>
        public ReactivePropertySlim<string> FindWindowClassName { get; } = new ReactivePropertySlim<string>();

        /// <summary>
        /// 名前でウィンドウを指定する場合のウィンドウ名のプロパティを取得する。
        /// </summary>
        public ReactivePropertySlim<string> FindWindowTextName { get; } = new ReactivePropertySlim<string>();

        /// <summary>
        /// 取得したウィンドウのクラス名のプロパティを取得する。
        /// </summary>
        public ReactivePropertySlim<string> FindWindowResultClassName { get; } = new ReactivePropertySlim<string>();

        /// <summary>
        /// 取得したウィンドウのウィンドウ名のプロパティを取得する。
        /// </summary>
        public ReactivePropertySlim<string> FindWindowResultTextName { get; } = new ReactivePropertySlim<string>();

        /// <summary>
        /// ウィンドウ検索済みであるかどうかを示す値のプロパティを取得する。
        /// </summary>
        public ReadOnlyReactivePropertySlim<bool> IsFoundWindow { get; }

        /// <summary>
        /// 取得したウィンドウがレイヤードウィンドウかどうかを示す値のプロパティを取得する。
        /// </summary>
        public ReadOnlyReactivePropertySlim<bool> IsLayeredWindow { get; }

        #endregion

        /// <summary>
        /// プロパティ情報をもとにハンドルを取得する。
        /// </summary>
        public void FindWindow()
        {
            // 現在の取得しているウィンドウ操作オブジェクトを無効にする。
            WindowProc.Value = null;

            // ウィンドウ操作オブジェクトの取得処理を行う。
            IWindowProc windowProc;

            if (Specifying.Value == FindWindowSpecifying.Position)
            {
                System.Drawing.Point p = new System.Drawing.Point(FindWindowPointX.Value, FindWindowPointY.Value);
                windowProc = _windowProcFactory.FindWindow(p);
            }
            else if (Specifying.Value == FindWindowSpecifying.WindowClass)
            {
                string className = null;
                string windowName = null;

                if (!string.IsNullOrEmpty(FindWindowClassName.Value))
                {
                    className = FindWindowClassName.Value;
                }
                if (!string.IsNullOrEmpty(FindWindowTextName.Value))
                {
                    windowName = FindWindowTextName.Value;
                }

                windowProc = _windowProcFactory.FindWindow(className, windowName);
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
            WindowProc.Value = null;

            // ウィンドウハンドルを取得する。
            FindWindow(_windowProcFactory.GetControlWindow(hwnd));
        }

        /// <summary>
        /// タスクバーのハンドルを取得する。
        /// </summary>
        public void FindTaskBarHandle()
        {
            // 現在の取得しているウィンドウ操作オブジェクトを無効にする。
            WindowProc.Value = null;

            // タスクバーのウィンドウハンドルを取得する。
            FindWindow(_windowProcFactory.GetTaskBarWindow());
        }

        /// <summary>
        /// ウィンドウ取得処理を行う。
        /// </summary>
        /// <param name="windowProc">ウィンドウ操作を行うオブジェクト</param>
        private void FindWindow(IWindowProc windowProc)
        {
            // クラス名とウィンドウ名を取得する。
            FindWindowResultClassName.Value = windowProc.GetClassName();
            FindWindowResultTextName.Value = windowProc.GetWindowText();

            // ウィンドウ操作を行うオブジェクトをプライベートフィールドに保持する。
            WindowProc.Value = windowProc;
        }

        /// <summary>
        /// プロパティ情報をもとにウィンドウ名を設定する。
        /// </summary>
        public void SetWindowText()
        {
            WindowProc.Value.SetWindowText(FindWindowResultTextName.Value);
        }

        /// <summary>
        /// 取得したハンドルが示すウィンドウのクローズイベント
        /// </summary>
        public void WindowClose()
        {
            WindowProc.Value.CloseWindow();
        }
    }
}
