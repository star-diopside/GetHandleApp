using System.Drawing;
using System.Windows.Forms;

namespace GetHandle.Function
{
    /// <summary>
    /// ウィンドウ操作を行う IWindowProc を実装するクラスを生成するクラスが実装するインタフェース
    /// </summary>
    public interface IWindowProcFactory
    {
        /// <summary>
        /// 指定された座標を含むウィンドウのハンドルを取得する。
        /// </summary>
        /// <param name="point">座標</param>
        /// <returns>取得したウィンドウ操作用の IWindowProc インスタンス</returns>
        IWindowProc FindWindow(Point point);

        /// <summary>
        /// 指定されたクラス名とウィンドウ名からウィンドウのハンドルを取得する。
        /// </summary>
        /// <param name="className">クラス名</param>
        /// <param name="windowName">ウィンドウ名</param>
        /// <returns>取得したウィンドウ操作用の IWindowProc インスタンス</returns>
        IWindowProc FindWindow(string className, string windowName);

        /// <summary>
        /// 指定されたコントロールのウィンドウハンドルを取得する。
        /// </summary>
        /// <param name="control">コントロール</param>
        /// <returns>取得したウィンドウ操作用の IWindowProc インスタンス</returns>
        IWindowProc GetControlWindow(Control control);

        /// <summary>
        /// タスクバーのウィンドウハンドルを取得する。
        /// </summary>
        /// <returns>取得したウィンドウ操作用の IWindowProc インスタンス</returns>
        IWindowProc GetTaskBarWindow();
    }
}
