using GetHandle.Wpf.Module.Models;
using GetHandle.Wpf.Module.Utility.Enum;
using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Reactive.Disposables;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace GetHandle.Wpf.Module.ViewModels
{
    public class MainViewModel : BindableBase, IDisposable
    {
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        private readonly GetHandleModel _model;

        public MainViewModel(GetHandleModel model)
        {
            _model = model;

            FindWindowPointX = _model.FindWindowPointX
                                     .ToReactivePropertyAsSynchronized(x => x.Value,
                                                                       n => n.ToString(),
                                                                       s => int.Parse(s),
                                                                       ignoreValidationErrorValue: true)
                                     .SetValidateNotifyError(s => int.TryParse(s, out _) ? null : "整数を入力してください。")
                                     .AddTo(_disposable);

            FindWindowPointY = _model.FindWindowPointY
                                     .ToReactivePropertyAsSynchronized(x => x.Value,
                                                                       n => n.ToString(),
                                                                       s => int.Parse(s),
                                                                       ignoreValidationErrorValue: true)
                                     .SetValidateNotifyError(s => int.TryParse(s, out _) ? null : "整数を入力してください。")
                                     .AddTo(_disposable);

            UpdateCursorPosition = new DelegateCommand(UpdateCursorPosition_Execute);

            GetHandle = new DelegateCommand(() => _model.FindWindow());

            GetOwnHandle = new DelegateCommand<Visual>(GetOwnHandle_Execute);

            GetTaskBarHandle = new DelegateCommand(() => _model.FindTaskBarHandle());

            SetWindowName = new DelegateCommand(() => _model.SetWindowText());

            WindowClose = new DelegateCommand(() => _model.WindowClose());

            UpdateLayeredWindowAttributes = new DelegateCommand(() => { });
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

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

        #region Command の定義

        /// <summary>
        /// カーソル位置取得コマンド
        /// </summary>
        public ICommand UpdateCursorPosition { get; }

        /// <summary>
        /// ハンドル取得コマンド
        /// </summary>
        public ICommand GetHandle { get; }

        /// <summary>
        /// 自分自身のハンドル取得コマンド
        /// </summary>
        public ICommand GetOwnHandle { get; }

        /// <summary>
        /// タスクバーのハンドル取得コマンド
        /// </summary>
        public ICommand GetTaskBarHandle { get; }

        /// <summary>
        /// 取得したハンドルが示すウィンドウ名の変更コマンド
        /// </summary>
        public ICommand SetWindowName { get; }

        /// <summary>
        /// 取得したハンドルが示すウィンドウのクローズコマンド
        /// </summary>
        public ICommand WindowClose { get; }

        /// <summary>
        /// 取得したハンドルが示すウィンドウのレイヤード設定変更コマンド
        /// </summary>
        public ICommand UpdateLayeredWindowAttributes { get; }

        #endregion

        #region プロパティ

        /// <summary>
        /// ウィンドウ検索済みであるかどうかを示す値のプロパティを取得する。
        /// </summary>
        public ReadOnlyReactivePropertySlim<bool> IsFoundWindow => _model.IsFoundWindow;

        /// <summary>
        /// ウィンドウ検索方法のプロパティを取得する。
        /// </summary>
        public ReactivePropertySlim<FindWindowSpecifying> Specifying => _model.Specifying;

        /// <summary>
        /// 位置でウィンドウを指定する場合の X 座標のプロパティを取得する。
        /// </summary>
        public ReactiveProperty<string> FindWindowPointX { get; }

        /// <summary>
        /// 位置でウィンドウを指定する場合の Y 座標のプロパティを取得する。
        /// </summary>
        public ReactiveProperty<string> FindWindowPointY { get; }

        /// <summary>
        /// 名前でウィンドウを指定する場合のクラス名のプロパティを取得する。
        /// </summary>
        public ReactivePropertySlim<string> FindWindowClassName => _model.FindWindowClassName;

        /// <summary>
        /// 名前でウィンドウを指定する場合のウィンドウ名のプロパティを取得する。
        /// </summary>
        public ReactivePropertySlim<string> FindWindowTextName => _model.FindWindowTextName;

        /// <summary>
        /// 取得したウィンドウのクラス名のプロパティを取得する。
        /// </summary>
        public ReadOnlyReactivePropertySlim<string> FindWindowResultClassName => _model.FindWindowResultClassName.ToReadOnlyReactivePropertySlim();

        /// <summary>
        /// 取得したウィンドウのウィンドウ名を取得または設定する。
        /// </summary>
        public ReactivePropertySlim<string> FindWindowResultTextName => _model.FindWindowResultTextName;

        #endregion

        /// <summary>
        /// カーソル位置取得イベント
        /// </summary>
        private void UpdateCursorPosition_Execute()
        {
            // 現在のカーソル位置を取得し、モデルオブジェクトに設定する。
            GetCursorPos(out POINT cursorPos);

            _model.FindWindowPointX.Value = cursorPos.X;
            _model.FindWindowPointY.Value = cursorPos.Y;
        }

        /// <summary>
        /// 自分自身のハンドル取得イベント
        /// </summary>
        private void GetOwnHandle_Execute(Visual parameter)
        {
            HwndSource source = (HwndSource)HwndSource.FromVisual(parameter);
            _model.FindWindowFromHwnd(source.Handle);
        }
    }
}
