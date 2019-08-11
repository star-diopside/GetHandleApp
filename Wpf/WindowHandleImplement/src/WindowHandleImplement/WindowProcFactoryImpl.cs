using System;
using System.Drawing;
using GetHandle.Function;
using Library.WindowsAPI;

namespace WindowHandleImplement
{
    /// <summary>
    /// ウィンドウ操作を行う IWindowProc を実装するクラスを生成するクラス
    /// </summary>
    public class WindowProcFactoryImpl : IWindowProcFactory
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public WindowProcFactoryImpl()
        {
        }

        /// <summary>
        /// 指定された座標を含むウィンドウのハンドルを取得する。
        /// </summary>
        /// <param name="point">座標</param>
        /// <returns>取得したウィンドウ操作用の IWindowProc インスタンス</returns>
        public IWindowProc FindWindow(Point point)
        {
            return new WindowProcImpl(WindowsAPI.WindowFromPoint(point));
        }

        /// <summary>
        /// 指定されたクラス名とウィンドウ名からウィンドウのハンドルを取得する。
        /// </summary>
        /// <param name="className">クラス名</param>
        /// <param name="windowName">ウィンドウ名</param>
        /// <returns>取得したウィンドウ操作用の IWindowProc インスタンス</returns>
        public IWindowProc FindWindow(string className, string windowName)
        {
            return new WindowProcImpl(WindowsAPI.FindWindow(className, windowName));
        }

        /// <summary>
        /// 指定されたウィンドウハンドルを取得する。
        /// </summary>
        /// <param name="handle">コントロールのハンドル</param>
        /// <returns>取得したウィンドウ操作用の IWindowProc インスタンス</returns>
        public IWindowProc GetControlWindow(IntPtr handle)
        {
            return new WindowProcImpl(handle);
        }

        /// <summary>
        /// タスクバーのウィンドウハンドルを取得する。
        /// </summary>
        /// <returns>取得したウィンドウ操作用の IWindowProc インスタンス</returns>
        public IWindowProc GetTaskBarWindow()
        {
            return new WindowProcImpl(WindowsAPI.FindWindow("Shell_TrayWnd", null));
        }
    }
}
