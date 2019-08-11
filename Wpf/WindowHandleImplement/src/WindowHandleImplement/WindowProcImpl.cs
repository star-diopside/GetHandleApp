using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using GetHandle.Function;
using Library.WindowsAPI;

namespace WindowHandleImplement
{
    /// <summary>
    /// ウィンドウ操作を行うクラス
    /// </summary>
    public class WindowProcImpl : IWindowProc
    {
        /// <summary>
        /// ウィンドウを示すハンドル値
        /// </summary>
        private IntPtr _handle;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="handle">ウィンドウを示すハンドル値</param>
        internal WindowProcImpl(IntPtr handle)
        {
            this._handle = handle;
        }

        /// <summary>
        /// ウィンドウにレイヤードウィンドウ属性が設定されているかどうかを取得する。
        /// </summary>
        public bool IsLayeredWindow
        {
            get
            {
                int windowLong = WindowsAPI.GetWindowLong(this._handle, (int)WindowsAPI.WindowLongConstants.GWL_EXSTYLE);

                if (windowLong == 0)
                {
                    throw new Win32Exception();
                }

                return (((WindowsAPI.WindowStyles)windowLong) & WindowsAPI.WindowStyles.WS_EX_LAYERED) == WindowsAPI.WindowStyles.WS_EX_LAYERED;
            }
        }

        /// <summary>
        /// ウィンドウが属するクラス名を取得する。
        /// </summary>
        /// <returns>取得したクラス名</returns>
        public string GetClassName()
        {
            StringBuilder className = new StringBuilder(1024);

            if (WindowsAPI.GetClassName(this._handle, className, className.Capacity) == 0)
            {
                throw new Win32Exception();
            }

            return className.ToString();
        }

        /// <summary>
        /// ウィンドウのテキストを取得する。
        /// </summary>
        /// <returns>取得したウィンドウテキスト</returns>
        public string GetWindowText()
        {
            int windowTextLength = WindowsAPI.GetWindowTextLength(this._handle);

            if (windowTextLength == 0)
            {
                return string.Empty;
            }
            else
            {
                StringBuilder windowText = new StringBuilder(windowTextLength + 1);

                if (WindowsAPI.GetWindowText(this._handle, windowText, windowText.Capacity) == 0)
                {
                    throw new Win32Exception();
                }

                return windowText.ToString();
            }
        }

        /// <summary>
        /// ウィンドウのテキストを設定する。
        /// </summary>
        /// <param name="text">設定するウィンドウテキスト</param>
        public void SetWindowText(string text)
        {
            if (!WindowsAPI.SetWindowText(this._handle, text))
            {
                throw new Win32Exception();
            }
        }

        /// <summary>
        /// ウィンドウの属性をレイヤードウィンドウにする。
        /// </summary>
        public void SetLayeredWindow()
        {
            if (WindowsAPI.SetWindowLong(this._handle, (int)WindowsAPI.WindowLongConstants.GWL_EXSTYLE,
                                         WindowsAPI.GetWindowLong(this._handle, (int)WindowsAPI.WindowLongConstants.GWL_EXSTYLE) | (int)WindowsAPI.WindowStyles.WS_EX_LAYERED) == 0)
            {
                throw new Win32Exception();
            }
        }

        /// <summary>
        /// ウィンドウのレイヤードウィンドウ属性を解除する。
        /// </summary>
        public void UnsetLayeredWindow()
        {
            if (WindowsAPI.SetWindowLong(this._handle, (int)WindowsAPI.WindowLongConstants.GWL_EXSTYLE,
                                         WindowsAPI.GetWindowLong(this._handle, (int)WindowsAPI.WindowLongConstants.GWL_EXSTYLE) & ~(int)WindowsAPI.WindowStyles.WS_EX_LAYERED) == 0)
            {
                throw new Win32Exception();
            }
        }

        /// <summary>
        /// レイヤードウィンドウの透明度を設定する。透明のカラーキーを設定している場合は解除する。
        /// </summary>
        /// <param name="alpha">レイヤードウィンドウの不透明度を示すアルファ値を指定する。0を指定すると、ウィンドウは完全に透明になり、255を指定すると、ウィンドウは不透明になる。</param>
        public void SetLayeredWindowAttributes(byte alpha)
        {
            SetLayeredWindowAttributes(new LayeredWindowAttribute() { Alpha = alpha });
        }

        /// <summary>
        /// レイヤードウィンドウの透明度と透明のカラーキーを設定する。
        /// </summary>
        /// <param name="alpha">レイヤードウィンドウの不透明度を示すアルファ値を指定する。0を指定すると、ウィンドウは完全に透明になり、255を指定すると、ウィンドウは不透明になる。</param>
        /// <param name="colorKey">透明のカラーキー。ウィンドウによって描画されるこの色のピクセルはすべて透明になる。</param>
        public void SetLayeredWindowAttributes(byte alpha, Color colorKey)
        {
            SetLayeredWindowAttributes(new LayeredWindowAttribute() { Alpha = alpha, ColorKey = colorKey });
        }

        /// <summary>
        /// レイヤードウィンドウの透明度と透明のカラーキーを設定する。
        /// </summary>
        /// <param name="attribute">レイヤードウィンドウの属性値</param>
        public void SetLayeredWindowAttributes(LayeredWindowAttribute attribute)
        {
            byte alpha;
            Color colorKey;
            WindowsAPI.LayeredWindowAttributes flags = 0;

            if (attribute.Alpha == null)
            {
                alpha = 0;
            }
            else
            {
                alpha = attribute.Alpha.Value;
                flags |= WindowsAPI.LayeredWindowAttributes.LWA_ALPHA;
            }

            if (attribute.ColorKey == null)
            {
                colorKey = Color.Empty;
            }
            else
            {
                colorKey = attribute.ColorKey.Value;
                flags |= WindowsAPI.LayeredWindowAttributes.LWA_COLORKEY;
            }

            if (!WindowsAPI.SetLayeredWindowAttributes(this._handle, (uint)ColorTranslator.ToWin32(colorKey), alpha, (uint)flags))
            {
                throw new Win32Exception();
            }
        }

        /// <summary>
        /// レイヤードウィンドウの透明度と透明のカラーキーを取得する。
        /// </summary>
        /// <returns>レイヤードウィンドウの属性値</returns>
        public LayeredWindowAttribute GetLayeredWindowAttributes()
        {
            LayeredWindowAttribute attribute = new LayeredWindowAttribute();

            if (this.IsLayeredWindow)
            {
                uint colorKey;
                byte alpha;
                uint flags;

                if (!WindowsAPI.GetLayeredWindowAttributes(this._handle, out colorKey, out alpha, out flags))
                {
                    throw new Win32Exception();
                }

                if ((((WindowsAPI.LayeredWindowAttributes)flags) & WindowsAPI.LayeredWindowAttributes.LWA_ALPHA) == WindowsAPI.LayeredWindowAttributes.LWA_ALPHA)
                {
                    attribute.Alpha = alpha;
                }
                else
                {
                    attribute.Alpha = null;
                }

                if ((((WindowsAPI.LayeredWindowAttributes)flags) & WindowsAPI.LayeredWindowAttributes.LWA_COLORKEY) == WindowsAPI.LayeredWindowAttributes.LWA_COLORKEY)
                {
                    attribute.ColorKey = ColorTranslator.FromWin32((int)colorKey);
                }
                else
                {
                    attribute.ColorKey = null;
                }
            }
            else
            {
                attribute.Alpha = null;
                attribute.ColorKey = null;
            }

            return attribute;
        }

        /// <summary>
        /// ウィンドウにクローズ通知を行う。
        /// </summary>
        public void CloseWindow()
        {
            if (!WindowsAPI.PostMessage(this._handle, (uint)WindowsAPI.WindowMessages.WM_CLOSE, IntPtr.Zero, IntPtr.Zero))
            {
                throw new Win32Exception();
            }
        }
    }
}
