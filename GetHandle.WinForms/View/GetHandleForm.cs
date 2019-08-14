using GetHandle.WinForms.Function;
using GetHandle.WinForms.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WindowHandleInterface.Function;

namespace GetHandle.WinForms.View
{
    public partial class GetHandleForm : Form
    {
        /// <summary>
        /// マウスドラッグ判定用フラグ
        /// </summary>
        private bool _mouseDragging = false;

        /// <summary>
        /// ウィンドウ操作を行うクラスのインスタンス
        /// </summary>
        private IWindowProc _windowProc = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GetHandleForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// カーソル画像表示用ピクチャボックスの再描画イベント
        /// </summary>
        private void CursorPicturePaintEvent(object sender, PaintEventArgs e)
        {
            // 描画イメージを取得する
            Bitmap drawImage;

            if (this._mouseDragging)
            {
                drawImage = Resources.Empty;
            }
            else
            {
                drawImage = Resources.CursorImage;
            }

            // 透明色を指定する
            drawImage.MakeTransparent(drawImage.GetPixel(0, 0));

            if (((Control)sender).Enabled)
            {
                // 画像を描画する
                e.Graphics.DrawImage(drawImage, 0, 0, drawImage.Width, drawImage.Height);
            }
            else
            {
                // 画像を無効状態で描画する
                ControlPaint.DrawImageDisabled(e.Graphics, drawImage, 0, 0, ((Control)sender).BackColor);
            }
        }

        /// <summary>
        /// カーソル画像表示用ピクチャボックスのマウスダウンイベント
        /// </summary>
        private void CursorPictureMouseDownEvent(object sender, MouseEventArgs e)
        {
            // マウスドラッグ判定用フラグを設定し、ピクチャボックスを再描画する
            this._mouseDragging = true;
            pictureCursor.Invalidate();

            // マウスカーソルを変更する
            Cursor.Current = Resources.TargetCursor;
        }

        /// <summary>
        /// カーゾル画像表示用ピクチャボックスのマウスアップイベント
        /// </summary>
        private void CursorPictureMouseUpEvent(object sender, MouseEventArgs e)
        {
            // マウスドラッグ判定用フラグを設定し、ピクチャボックスを再描画する
            this._mouseDragging = false;
            pictureCursor.Invalidate();
        }

        /// <summary>
        /// カーソル画像表示用ピクチャボックスのマウス移動イベント
        /// </summary>
        private void CursorPictureMouseMoveEvent(object sender, MouseEventArgs e)
        {
            if (this._mouseDragging)
            {
                UpdateCursorPosition();
            }
        }

        /// <summary>
        /// カーソル位置取得ボタンのクリックイベント
        /// </summary>
        private void CursorPositionButtonClickEvent(object sender, EventArgs e)
        {
            UpdateCursorPosition();
        }

        /// <summary>
        /// カーソル位置を取得し、コントロールの値を変更する
        /// </summary>
        private void UpdateCursorPosition()
        {
            Point cursorPos = Cursor.Position;

            textPosX.Text = cursorPos.X.ToString();
            textPosY.Text = cursorPos.Y.ToString();
        }

        /// <summary>
        /// 位置指定ラジオボタンのチェック状態変更イベント
        /// </summary>
        private void RadioPositionCheckedChangedEvent(object sender, EventArgs e)
        {
            groupPosition.Enabled = radioPosition.Checked;
        }

        /// <summary>
        /// クラス名指定ラジオボタンのチェック状態変更イベント
        /// </summary>
        private void RadioWindowClassCheckedChangedEvent(object sender, EventArgs e)
        {
            groupWindowClass.Enabled = radioWindowClass.Checked;
        }

        /// <summary>
        /// 透明色指定チェックボックスのチェック状態変更イベント
        /// </summary>
        private void CheckTransparentColorCheckedChangedEvent(object sender, EventArgs e)
        {
            buttonSelectTransparentColor.Enabled = checkTransparentColor.Checked;
        }

        /// <summary>
        /// レイヤードウィンドウの透明色選択イベント
        /// </summary>
        private void SelectTransparentColorEvent(object sender, EventArgs e)
        {
            selectTransparentColorDialog.Color = buttonSelectTransparentColor.BackColor;

            if (selectTransparentColorDialog.ShowDialog(this) == DialogResult.OK)
            {
                buttonSelectTransparentColor.BackColor = selectTransparentColorDialog.Color;
            }
        }

        /// <summary>
        /// 指定条件をもとにウィンドウハンドルの取得イベント
        /// </summary>
        private void HandleButtonClickEvent(object sender, EventArgs e)
        {
            try
            {
                if (radioPosition.Checked)
                {
                    Point p = new Point(int.Parse(textPosX.Text), int.Parse(textPosY.Text));
                    this._windowProc = WindowProcFactoryMethod.Instance.FindWindow(p);
                }
                else if (radioWindowClass.Checked)
                {
                    string className = null;
                    string windowName = null;

                    if (!string.IsNullOrEmpty(textClass.Text))
                    {
                        className = textClass.Text;
                    }
                    if (!string.IsNullOrEmpty(textWindow.Text))
                    {
                        windowName = textWindow.Text;
                    }

                    this._windowProc = WindowProcFactoryMethod.Instance.FindWindow(className, windowName);
                }
                else
                {
                    throw new InvalidOperationException();
                }

                AfterFindWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 自分自身のウィンドウハンドルの取得イベント
        /// </summary>
        private void OwnHandleButtonClickEvent(object sender, EventArgs e)
        {
            try
            {
                this._windowProc = WindowProcFactoryMethod.Instance.GetControlWindow(this.Handle);

                AfterFindWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// タスクバーのウィンドウハンドルの取得イベント
        /// </summary>
        private void TaskBarHandleButtonClickEvent(object sender, EventArgs e)
        {
            try
            {
                this._windowProc = WindowProcFactoryMethod.Instance.GetTaskBarWindow();

                AfterFindWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ウィンドウハンドル取得後の共通処理
        /// </summary>
        private void AfterFindWindow()
        {
            // クラス名とウィンドウ名を取得する
            textGetClassName.Text = this._windowProc.GetClassName();
            textGetWindowName.Text = this._windowProc.GetWindowText();

            // ウィンドウハンドル取得後に操作可能なコントロールを有効化する
            buttonSetWindowName.Enabled = true;
            buttonWindowClose.Enabled = true;

            // レイヤードウィンドウ機能が利用可能な場合
            if (OSFeature.Feature.IsPresent(OSFeature.LayeredWindows))
            {
                // レイヤードウィンドウ操作を行うためのコントロールを有効化する
                groupLayeredWindow.Enabled = true;

                // レイヤードウィンドウ属性を取得しコントロールに反映する
                bool layered = this._windowProc.IsLayeredWindow;
                checkLayeredWindow.Checked = layered;

                // レイヤードウィンドウ属性が設定されている場合
                if (layered)
                {
                    try
                    {
                        LayeredWindowAttribute attr = this._windowProc.GetLayeredWindowAttributes();

                        numericTransparency.Value = attr.Alpha ?? numericTransparency.Maximum;
                        checkTransparentColor.Checked = attr.ColorKey.HasValue;
                        buttonSelectTransparentColor.BackColor = attr.ColorKey ?? SystemColors.Control;
                    }
                    catch (EntryPointNotFoundException)
                    {
                        // エントリポイントが見つからない場合、既定値を設定する
                        numericTransparency.Value = numericTransparency.Maximum;
                        checkTransparentColor.Checked = false;
                        buttonSelectTransparentColor.BackColor = SystemColors.Control;
                    }
                    catch (Win32Exception)
                    {
                        MessageBox.Show(this, "レイヤードウィンドウ属性の取得に失敗しました。", "情報",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        /// <summary>
        /// ウィンドウ名変更イベント
        /// </summary>
        private void SetWindowNameButtonClickEvent(object sender, EventArgs e)
        {
            try
            {
                this._windowProc.SetWindowText(textGetWindowName.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ウィンドウのクローズイベント
        /// </summary>
        private void WindowCloseButtonClickEvent(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(this, "この操作による動作保障はできません。実行しますか？", "警告",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    this._windowProc.CloseWindow();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// レイヤードウィンドウ属性の設定/解除イベント
        /// </summary>
        private void CheckLayeredWindowClickEvent(object sender, EventArgs e)
        {
            try
            {
                if (checkLayeredWindow.Checked)
                {
                    this._windowProc.SetLayeredWindow();
                    UpdateLayeredWindowAttributes();
                }
                else
                {
                    this._windowProc.UnsetLayeredWindow();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// レイヤードウィンドウ属性の変更イベント
        /// </summary>
        private void ApplyLayeredSettingEvent(object sender, EventArgs e)
        {
            try
            {
                if (this._windowProc.IsLayeredWindow)
                {
                    UpdateLayeredWindowAttributes();
                }
                else
                {
                    MessageBox.Show(this, "対象のウィンドウはレイヤードウィンドウではありません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// レイヤードウィンドウ属性を変更する
        /// </summary>
        private void UpdateLayeredWindowAttributes()
        {
            if (checkTransparentColor.Checked)
            {
                this._windowProc.SetLayeredWindowAttributes(decimal.ToByte(numericTransparency.Value), buttonSelectTransparentColor.BackColor);
            }
            else
            {
                this._windowProc.SetLayeredWindowAttributes(decimal.ToByte(numericTransparency.Value));
            }
        }
    }
}
