using System.Drawing;

namespace GetHandle.Function
{
    /// <summary>
    /// ウィンドウ操作を行うクラスが実装するインタフェース
    /// </summary>
    public interface IWindowProc
    {
        /// <summary>
        /// ウィンドウにレイヤードウィンドウ属性が設定されているかどうかを取得する。
        /// </summary>
        bool IsLayeredWindow { get; }

        /// <summary>
        /// ウィンドウが属するクラス名を取得する。
        /// </summary>
        /// <returns>取得したクラス名</returns>
        string GetClassName();

        /// <summary>
        /// ウィンドウのテキストを取得する。
        /// </summary>
        /// <returns>取得したウィンドウテキスト</returns>
        string GetWindowText();

        /// <summary>
        /// ウィンドウのテキストを設定する。
        /// </summary>
        /// <param name="text">設定するウィンドウテキスト</param>
        void SetWindowText(string text);

        /// <summary>
        /// ウィンドウの属性をレイヤードウィンドウにする。
        /// </summary>
        void SetLayeredWindow();

        /// <summary>
        /// ウィンドウのレイヤードウィンドウ属性を解除する。
        /// </summary>
        void UnsetLayeredWindow();

        /// <summary>
        /// レイヤードウィンドウの透明度を設定する。透明のカラーキーを設定している場合は解除する。
        /// </summary>
        /// <param name="alpha">レイヤードウィンドウの不透明度を示すアルファ値を指定する。0を指定すると、ウィンドウは完全に透明になり、255を指定すると、ウィンドウは不透明になる。</param>
        void SetLayeredWindowAttributes(byte alpha);

        /// <summary>
        /// レイヤードウィンドウの透明度と透明のカラーキーを設定する。
        /// </summary>
        /// <param name="alpha">レイヤードウィンドウの不透明度を示すアルファ値を指定する。0を指定すると、ウィンドウは完全に透明になり、255を指定すると、ウィンドウは不透明になる。</param>
        /// <param name="colorKey">透明のカラーキー。ウィンドウによって描画されるこの色のピクセルはすべて透明になる。</param>
        void SetLayeredWindowAttributes(byte alpha, Color colorKey);

        /// <summary>
        /// レイヤードウィンドウの透明度と透明のカラーキーを取得する。
        /// </summary>
        /// <returns>レイヤードウィンドウの属性値</returns>
        LayeredWindowAttribute GetLayeredWindowAttributes();

        /// <summary>
        /// ウィンドウにクローズ通知を行う。
        /// </summary>
        void CloseWindow();
    }
}
