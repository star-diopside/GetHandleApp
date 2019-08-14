using GetHandle.Wpf.Module.Utilities.Enum;
using Reactive.Bindings;
using System;
using System.Drawing;

namespace GetHandle.Wpf.Module.Models
{
    public interface IFindWindowModel
    {
        /// <summary>
        /// ウィンドウ検索方法のプロパティを取得する。
        /// </summary>
        ReactivePropertySlim<FindWindowSpecifying> Specifying { get; }

        /// <summary>
        /// 位置でウィンドウを指定する場合の座標値のプロパティを取得する。
        /// </summary>
        ReactivePropertySlim<Point> FindWindowPoint { get; }

        /// <summary>
        /// 名前でウィンドウを指定する場合のクラス名のプロパティを取得する。
        /// </summary>
        ReactivePropertySlim<string> FindWindowClassName { get; }

        /// <summary>
        /// 名前でウィンドウを指定する場合のウィンドウ名のプロパティを取得する。
        /// </summary>
        ReactivePropertySlim<string> FindWindowTextName { get; }

        /// <summary>
        /// 取得したウィンドウのクラス名のプロパティを取得する。
        /// </summary>
        ReactivePropertySlim<string> FindWindowResultClassName { get; }

        /// <summary>
        /// 取得したウィンドウのウィンドウ名のプロパティを取得する。
        /// </summary>
        ReactivePropertySlim<string> FindWindowResultTextName { get; }

        /// <summary>
        /// ウィンドウ検索済みであるかどうかを示す値のプロパティを取得する。
        /// </summary>
        ReadOnlyReactivePropertySlim<bool> IsFoundWindow { get; }

        /// <summary>
        /// 取得したウィンドウがレイヤードウィンドウかどうかを示す値のプロパティを取得する。
        /// </summary>
        ReadOnlyReactivePropertySlim<bool> IsLayeredWindow { get; }

        /// <summary>
        /// プロパティ情報をもとにハンドルを取得する。
        /// </summary>
        void FindWindow();

        /// <summary>
        /// ウィンドウハンドルをもとにハンドルを取得する。
        /// </summary>
        void FindWindowFromHwnd(IntPtr hwnd);

        /// <summary>
        /// タスクバーのハンドルを取得する。
        /// </summary>
        void FindTaskBarHandle();

        /// <summary>
        /// プロパティ情報をもとにウィンドウ名を設定する。
        /// </summary>
        void SetWindowText();

        /// <summary>
        /// 取得したハンドルが示すウィンドウを閉じる。
        /// </summary>
        void WindowClose();
    }
}
