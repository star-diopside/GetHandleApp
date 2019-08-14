using GetHandle.Wpf.Module.Models;
using GetHandle.Wpf.Module.Utilities.Enum;
using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Drawing;
using System.Reactive.Disposables;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace GetHandle.Wpf.Module.ViewModels
{
    public class MainViewModel : BindableBase, IDisposable
    {
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        private readonly IFindWindowModel _model;

        public MainViewModel(IFindWindowModel model)
        {
            _model = model;

            //------------------------------
            // プロパティの設定を行う。
            //------------------------------
            FindWindowPointX = _model.FindWindowPoint
                                     .ToReactivePropertyAsSynchronized(x => x.Value,
                                                                       p => p.X.ToString(),
                                                                       s => new Point(int.Parse(s), _model.FindWindowPoint.Value.Y),
                                                                       ignoreValidationErrorValue: true)
                                     .SetValidateNotifyError(s => int.TryParse(s, out _) ? null : "整数を入力してください。")
                                     .AddTo(_disposable);

            FindWindowPointY = _model.FindWindowPoint
                                     .ToReactivePropertyAsSynchronized(x => x.Value,
                                                                       p => p.Y.ToString(),
                                                                       s => new Point(_model.FindWindowPoint.Value.X, int.Parse(s)),
                                                                       ignoreValidationErrorValue: true)
                                     .SetValidateNotifyError(s => int.TryParse(s, out _) ? null : "整数を入力してください。")
                                     .AddTo(_disposable);

            //------------------------------
            // コマンドの設定を行う。
            //------------------------------
            ReactiveCommand ToReactiveCommand(Action executeMethod, IObservable<bool> canExecute)
            {
                var command = canExecute.ToReactiveCommand();
                command.Subscribe(executeMethod).AddTo(_disposable);
                return command;
            }

            UpdateCursorPositionCommand = new DelegateCommand(UpdateCursorPosition);
            GetHandleCommand = new DelegateCommand(GetHandle);
            GetOwnHandleCommand = new DelegateCommand<Visual>(GetOwnHandle);
            GetTaskBarHandleCommand = new DelegateCommand(GetTaskBarHandle);
            SetWindowNameCommand = ToReactiveCommand(SetWindowName, IsFoundWindow);
            WindowCloseCommand = new DelegateCommand(WindowClose);
            UpdateLayeredWindowAttributesCommand = new DelegateCommand(UpdateLayeredWindowAttributes);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        #region Command の定義

        /// <summary>
        /// カーソル位置取得コマンド
        /// </summary>
        public ICommand UpdateCursorPositionCommand { get; }

        /// <summary>
        /// ハンドル取得コマンド
        /// </summary>
        public ICommand GetHandleCommand { get; }

        /// <summary>
        /// 自分自身のハンドル取得コマンド
        /// </summary>
        public ICommand GetOwnHandleCommand { get; }

        /// <summary>
        /// タスクバーのハンドル取得コマンド
        /// </summary>
        public ICommand GetTaskBarHandleCommand { get; }

        /// <summary>
        /// 取得したハンドルが示すウィンドウ名の変更コマンド
        /// </summary>
        public ICommand SetWindowNameCommand { get; }

        /// <summary>
        /// 取得したハンドルが示すウィンドウのクローズコマンド
        /// </summary>
        public ICommand WindowCloseCommand { get; }

        /// <summary>
        /// 取得したハンドルが示すウィンドウのレイヤード設定変更コマンド
        /// </summary>
        public ICommand UpdateLayeredWindowAttributesCommand { get; }

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
        public ReadOnlyReactivePropertySlim<string> FindWindowResultClassName
            => _model.FindWindowResultClassName.ToReadOnlyReactivePropertySlim();

        /// <summary>
        /// 取得したウィンドウのウィンドウ名を取得または設定する。
        /// </summary>
        public ReactivePropertySlim<string> FindWindowResultTextName => _model.FindWindowResultTextName;

        #endregion

        /// <summary>
        /// カーソル位置取得イベント
        /// </summary>
        private void UpdateCursorPosition()
        {
            // 現在のカーソル位置を取得し、モデルオブジェクトに設定する。
            _model.FindWindowPoint.Value = System.Windows.Forms.Cursor.Position;
        }

        /// <summary>
        /// ハンドル取得イベント
        /// </summary>
        private void GetHandle()
        {
            _model.FindWindow();
        }

        /// <summary>
        /// 自分自身のハンドル取得イベント
        /// </summary>
        private void GetOwnHandle(Visual parameter)
        {
            HwndSource source = (HwndSource)System.Windows.PresentationSource.FromVisual(parameter);
            _model.FindWindowFromHwnd(source.Handle);
        }

        /// <summary>
        /// タスクバーのハンドル取得イベント
        /// </summary>
        private void GetTaskBarHandle()
        {
            _model.FindTaskBarHandle();
        }

        /// <summary>
        /// 取得したハンドルが示すウィンドウ名の変更イベント
        /// </summary>
        private void SetWindowName()
        {
            _model.SetWindowText();
        }

        /// <summary>
        /// 取得したハンドルが示すウィンドウのクローズイベント
        /// </summary>
        private void WindowClose()
        {
            _model.WindowClose();
        }

        /// <summary>
        /// 取得したハンドルが示すウィンドウのレイヤード設定変更イベント
        /// </summary>
        private void UpdateLayeredWindowAttributes()
        {
            throw new NotImplementedException();
        }
    }
}
