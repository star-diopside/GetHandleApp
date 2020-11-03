using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WindowHandle.Library.WindowsAPI
{
    internal static class WindowsAPI
    {
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(POINT point);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowText(IntPtr hWnd, String lpString);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetLayeredWindowAttributes(IntPtr hWnd, out uint crKey, out byte bAlpha, out uint dwFlags);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetLayeredWindowAttributes(IntPtr hWnd, uint crKey, byte bAlpha, uint dwFlags);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// 取得または設定するウィンドウ属性
        /// </summary>
        public enum WindowLongConstants : int
        {
            GWL_EXSTYLE = -20
        }

        /// <summary>
        /// ウィンドウスタイル
        /// </summary>
        [Flags]
        public enum WindowStyles : int
        {
            WS_EX_LAYERED = 0x00080000
        }

        /// <summary>
        /// レイヤードウィンドウ属性
        /// </summary>
        [Flags]
        public enum LayeredWindowAttributes : uint
        {
            LWA_COLORKEY = 0x1,
            LWA_ALPHA = 0x2
        }

        /// <summary>
        /// ウィンドウメッセージ
        /// </summary>
        public enum WindowMessages : uint
        {
            WM_CLOSE = 0x0010
        }
    }
}
