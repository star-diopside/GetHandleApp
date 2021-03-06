﻿using System;
using System.Drawing;
using WindowHandle.Library.WindowsAPI;

namespace WindowHandle.Function
{
    /// <summary>
    /// ウィンドウ操作を行う IWindowProc を実装するクラスを生成するクラス
    /// </summary>
    public class WindowProcFactory : IWindowProcFactory
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public WindowProcFactory()
        {
        }

        /// <summary>
        /// 指定された座標を含むウィンドウのハンドルを取得する。
        /// </summary>
        /// <param name="point">座標</param>
        /// <returns>取得したウィンドウ操作用の IWindowProc インスタンス</returns>
        public IWindowProc FindWindow(Point point)
        {
            return new WindowProc(WindowsAPI.WindowFromPoint(point));
        }

        /// <summary>
        /// 指定されたクラス名とウィンドウ名からウィンドウのハンドルを取得する。
        /// </summary>
        /// <param name="className">クラス名</param>
        /// <param name="windowName">ウィンドウ名</param>
        /// <returns>取得したウィンドウ操作用の IWindowProc インスタンス</returns>
        public IWindowProc FindWindow(string className, string windowName)
        {
            return new WindowProc(WindowsAPI.FindWindow(className, windowName));
        }

        /// <summary>
        /// 指定されたウィンドウハンドルを取得する。
        /// </summary>
        /// <param name="handle">コントロールのハンドル</param>
        /// <returns>取得したウィンドウ操作用の IWindowProc インスタンス</returns>
        public IWindowProc GetControlWindow(IntPtr handle)
        {
            return new WindowProc(handle);
        }

        /// <summary>
        /// タスクバーのウィンドウハンドルを取得する。
        /// </summary>
        /// <returns>取得したウィンドウ操作用の IWindowProc インスタンス</returns>
        public IWindowProc GetTaskBarWindow()
        {
            return new WindowProc(WindowsAPI.FindWindow("Shell_TrayWnd", null));
        }
    }
}
