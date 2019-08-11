using System.Drawing;

namespace GetHandle.Function
{
    /// <summary>
    /// レイヤードウィンドウの属性を表すクラス
    /// </summary>
    public struct LayeredWindowAttribute
    {
        /// <summary>
        /// レイヤードウィンドウの不透明度を示すアルファ値を取得または設定する。属性が設定されていない場合はnullを返す。
        /// </summary>
        public byte? Alpha
        {
            get;
            set;
        }

        /// <summary>
        /// レイヤードウィンドウの透明のカラーキーを取得または設定する。属性が設定されていない場合はnullを返す。
        /// </summary>
        public Color? ColorKey
        {
            get;
            set;
        }
    }
}
