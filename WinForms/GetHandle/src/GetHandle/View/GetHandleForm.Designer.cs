namespace GetHandle.View
{
    partial class GetHandleForm
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureCursor = new System.Windows.Forms.PictureBox();
            this.groupPosition = new System.Windows.Forms.GroupBox();
            this.buttonCursorPosition = new System.Windows.Forms.Button();
            this.textPosY = new System.Windows.Forms.TextBox();
            this.labelPoxY = new System.Windows.Forms.Label();
            this.textPosX = new System.Windows.Forms.TextBox();
            this.labelPosX = new System.Windows.Forms.Label();
            this.groupWindowClass = new System.Windows.Forms.GroupBox();
            this.textWindow = new System.Windows.Forms.TextBox();
            this.labelWindow = new System.Windows.Forms.Label();
            this.textClass = new System.Windows.Forms.TextBox();
            this.labelClass = new System.Windows.Forms.Label();
            this.radioPosition = new System.Windows.Forms.RadioButton();
            this.radioWindowClass = new System.Windows.Forms.RadioButton();
            this.buttonHandle = new System.Windows.Forms.Button();
            this.buttonOwnHandle = new System.Windows.Forms.Button();
            this.buttonTaskBarHandle = new System.Windows.Forms.Button();
            this.labelGetClassName = new System.Windows.Forms.Label();
            this.labelGetWindowName = new System.Windows.Forms.Label();
            this.textGetClassName = new System.Windows.Forms.TextBox();
            this.textGetWindowName = new System.Windows.Forms.TextBox();
            this.buttonSetWindowName = new System.Windows.Forms.Button();
            this.buttonWindowClose = new System.Windows.Forms.Button();
            this.groupLayeredWindow = new System.Windows.Forms.GroupBox();
            this.checkLayeredWindow = new System.Windows.Forms.CheckBox();
            this.groupLayeredSetting = new System.Windows.Forms.GroupBox();
            this.buttonApplyLayeredSetting = new System.Windows.Forms.Button();
            this.buttonSelectTransparentColor = new System.Windows.Forms.Button();
            this.checkTransparentColor = new System.Windows.Forms.CheckBox();
            this.numericTransparency = new System.Windows.Forms.NumericUpDown();
            this.labelTransparency = new System.Windows.Forms.Label();
            this.selectTransparentColorDialog = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCursor)).BeginInit();
            this.groupPosition.SuspendLayout();
            this.groupWindowClass.SuspendLayout();
            this.groupLayeredWindow.SuspendLayout();
            this.groupLayeredSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericTransparency)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureCursor
            // 
            this.pictureCursor.Location = new System.Drawing.Point(206, 61);
            this.pictureCursor.Name = "pictureCursor";
            this.pictureCursor.Size = new System.Drawing.Size(17, 17);
            this.pictureCursor.TabIndex = 0;
            this.pictureCursor.TabStop = false;
            this.pictureCursor.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CursorPictureMouseMoveEvent);
            this.pictureCursor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CursorPictureMouseDownEvent);
            this.pictureCursor.Paint += new System.Windows.Forms.PaintEventHandler(this.CursorPicturePaintEvent);
            this.pictureCursor.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CursorPictureMouseUpEvent);
            // 
            // groupPosition
            // 
            this.groupPosition.Controls.Add(this.pictureCursor);
            this.groupPosition.Controls.Add(this.buttonCursorPosition);
            this.groupPosition.Controls.Add(this.textPosY);
            this.groupPosition.Controls.Add(this.labelPoxY);
            this.groupPosition.Controls.Add(this.textPosX);
            this.groupPosition.Controls.Add(this.labelPosX);
            this.groupPosition.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupPosition.Location = new System.Drawing.Point(12, 35);
            this.groupPosition.Name = "groupPosition";
            this.groupPosition.Padding = new System.Windows.Forms.Padding(24, 12, 24, 16);
            this.groupPosition.Size = new System.Drawing.Size(250, 100);
            this.groupPosition.TabIndex = 2;
            this.groupPosition.TabStop = false;
            this.groupPosition.Text = "位置の指定";
            // 
            // buttonCursorPosition
            // 
            this.buttonCursorPosition.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonCursorPosition.Location = new System.Drawing.Point(27, 58);
            this.buttonCursorPosition.Name = "buttonCursorPosition";
            this.buttonCursorPosition.Size = new System.Drawing.Size(173, 23);
            this.buttonCursorPosition.TabIndex = 4;
            this.buttonCursorPosition.Text = "現在のカーソル位置の取得";
            this.buttonCursorPosition.UseVisualStyleBackColor = true;
            this.buttonCursorPosition.Click += new System.EventHandler(this.CursorPositionButtonClickEvent);
            // 
            // textPosY
            // 
            this.textPosY.Location = new System.Drawing.Point(173, 27);
            this.textPosY.Name = "textPosY";
            this.textPosY.Size = new System.Drawing.Size(50, 19);
            this.textPosY.TabIndex = 3;
            this.textPosY.Text = "0";
            // 
            // labelPoxY
            // 
            this.labelPoxY.AutoSize = true;
            this.labelPoxY.Location = new System.Drawing.Point(131, 30);
            this.labelPoxY.Name = "labelPoxY";
            this.labelPoxY.Size = new System.Drawing.Size(36, 12);
            this.labelPoxY.TabIndex = 2;
            this.labelPoxY.Text = "Y座標";
            // 
            // textPosX
            // 
            this.textPosX.Location = new System.Drawing.Point(69, 27);
            this.textPosX.Name = "textPosX";
            this.textPosX.Size = new System.Drawing.Size(50, 19);
            this.textPosX.TabIndex = 1;
            this.textPosX.Text = "0";
            // 
            // labelPosX
            // 
            this.labelPosX.AutoSize = true;
            this.labelPosX.Location = new System.Drawing.Point(27, 30);
            this.labelPosX.Name = "labelPosX";
            this.labelPosX.Size = new System.Drawing.Size(36, 12);
            this.labelPosX.TabIndex = 0;
            this.labelPosX.Text = "X座標";
            // 
            // groupWindowClass
            // 
            this.groupWindowClass.Controls.Add(this.textWindow);
            this.groupWindowClass.Controls.Add(this.labelWindow);
            this.groupWindowClass.Controls.Add(this.textClass);
            this.groupWindowClass.Controls.Add(this.labelClass);
            this.groupWindowClass.Enabled = false;
            this.groupWindowClass.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupWindowClass.Location = new System.Drawing.Point(274, 35);
            this.groupWindowClass.Name = "groupWindowClass";
            this.groupWindowClass.Padding = new System.Windows.Forms.Padding(12, 12, 24, 16);
            this.groupWindowClass.Size = new System.Drawing.Size(368, 100);
            this.groupWindowClass.TabIndex = 3;
            this.groupWindowClass.TabStop = false;
            this.groupWindowClass.Text = "クラス名とウィンドウ名の指定";
            // 
            // textWindow
            // 
            this.textWindow.Location = new System.Drawing.Point(101, 60);
            this.textWindow.Name = "textWindow";
            this.textWindow.Size = new System.Drawing.Size(240, 19);
            this.textWindow.TabIndex = 3;
            // 
            // labelWindow
            // 
            this.labelWindow.Location = new System.Drawing.Point(15, 63);
            this.labelWindow.Name = "labelWindow";
            this.labelWindow.Size = new System.Drawing.Size(80, 12);
            this.labelWindow.TabIndex = 2;
            this.labelWindow.Text = "ウィンドウ名";
            this.labelWindow.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textClass
            // 
            this.textClass.Location = new System.Drawing.Point(101, 27);
            this.textClass.Name = "textClass";
            this.textClass.Size = new System.Drawing.Size(240, 19);
            this.textClass.TabIndex = 1;
            // 
            // labelClass
            // 
            this.labelClass.Location = new System.Drawing.Point(15, 30);
            this.labelClass.Name = "labelClass";
            this.labelClass.Size = new System.Drawing.Size(80, 12);
            this.labelClass.TabIndex = 0;
            this.labelClass.Text = "クラス名";
            this.labelClass.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // radioPosition
            // 
            this.radioPosition.Checked = true;
            this.radioPosition.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.radioPosition.Location = new System.Drawing.Point(12, 12);
            this.radioPosition.Name = "radioPosition";
            this.radioPosition.Size = new System.Drawing.Size(250, 17);
            this.radioPosition.TabIndex = 0;
            this.radioPosition.TabStop = true;
            this.radioPosition.Text = "位置で指定する";
            this.radioPosition.UseVisualStyleBackColor = true;
            this.radioPosition.CheckedChanged += new System.EventHandler(this.RadioPositionCheckedChangedEvent);
            // 
            // radioWindowClass
            // 
            this.radioWindowClass.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.radioWindowClass.Location = new System.Drawing.Point(274, 12);
            this.radioWindowClass.Name = "radioWindowClass";
            this.radioWindowClass.Size = new System.Drawing.Size(368, 17);
            this.radioWindowClass.TabIndex = 1;
            this.radioWindowClass.Text = "クラス名とウィンドウ名で指定する";
            this.radioWindowClass.UseVisualStyleBackColor = true;
            this.radioWindowClass.CheckedChanged += new System.EventHandler(this.RadioWindowClassCheckedChangedEvent);
            // 
            // buttonHandle
            // 
            this.buttonHandle.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonHandle.Location = new System.Drawing.Point(12, 141);
            this.buttonHandle.Name = "buttonHandle";
            this.buttonHandle.Size = new System.Drawing.Size(200, 23);
            this.buttonHandle.TabIndex = 4;
            this.buttonHandle.Text = "上記の設定で取得する";
            this.buttonHandle.UseVisualStyleBackColor = true;
            this.buttonHandle.Click += new System.EventHandler(this.HandleButtonClickEvent);
            // 
            // buttonOwnHandle
            // 
            this.buttonOwnHandle.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonOwnHandle.Location = new System.Drawing.Point(218, 141);
            this.buttonOwnHandle.Name = "buttonOwnHandle";
            this.buttonOwnHandle.Size = new System.Drawing.Size(210, 23);
            this.buttonOwnHandle.TabIndex = 5;
            this.buttonOwnHandle.Text = "自分自身のハンドルを取得する";
            this.buttonOwnHandle.UseVisualStyleBackColor = true;
            this.buttonOwnHandle.Click += new System.EventHandler(this.OwnHandleButtonClickEvent);
            // 
            // buttonTaskBarHandle
            // 
            this.buttonTaskBarHandle.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonTaskBarHandle.Location = new System.Drawing.Point(434, 141);
            this.buttonTaskBarHandle.Name = "buttonTaskBarHandle";
            this.buttonTaskBarHandle.Size = new System.Drawing.Size(208, 23);
            this.buttonTaskBarHandle.TabIndex = 6;
            this.buttonTaskBarHandle.Text = "タスクバーのハンドルを取得する";
            this.buttonTaskBarHandle.UseVisualStyleBackColor = true;
            this.buttonTaskBarHandle.Click += new System.EventHandler(this.TaskBarHandleButtonClickEvent);
            // 
            // labelGetClassName
            // 
            this.labelGetClassName.Location = new System.Drawing.Point(12, 186);
            this.labelGetClassName.Name = "labelGetClassName";
            this.labelGetClassName.Size = new System.Drawing.Size(80, 12);
            this.labelGetClassName.TabIndex = 7;
            this.labelGetClassName.Text = "クラス名";
            this.labelGetClassName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelGetWindowName
            // 
            this.labelGetWindowName.Location = new System.Drawing.Point(12, 213);
            this.labelGetWindowName.Name = "labelGetWindowName";
            this.labelGetWindowName.Size = new System.Drawing.Size(80, 12);
            this.labelGetWindowName.TabIndex = 9;
            this.labelGetWindowName.Text = "ウィンドウ名";
            this.labelGetWindowName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textGetClassName
            // 
            this.textGetClassName.BackColor = System.Drawing.SystemColors.Window;
            this.textGetClassName.Location = new System.Drawing.Point(98, 183);
            this.textGetClassName.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.textGetClassName.Name = "textGetClassName";
            this.textGetClassName.ReadOnly = true;
            this.textGetClassName.Size = new System.Drawing.Size(410, 19);
            this.textGetClassName.TabIndex = 8;
            // 
            // textGetWindowName
            // 
            this.textGetWindowName.Location = new System.Drawing.Point(98, 210);
            this.textGetWindowName.Name = "textGetWindowName";
            this.textGetWindowName.Size = new System.Drawing.Size(410, 19);
            this.textGetWindowName.TabIndex = 10;
            // 
            // buttonSetWindowName
            // 
            this.buttonSetWindowName.Enabled = false;
            this.buttonSetWindowName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonSetWindowName.Location = new System.Drawing.Point(514, 208);
            this.buttonSetWindowName.Name = "buttonSetWindowName";
            this.buttonSetWindowName.Size = new System.Drawing.Size(128, 23);
            this.buttonSetWindowName.TabIndex = 11;
            this.buttonSetWindowName.Text = "変更を適用する";
            this.buttonSetWindowName.UseVisualStyleBackColor = true;
            this.buttonSetWindowName.Click += new System.EventHandler(this.SetWindowNameButtonClickEvent);
            // 
            // buttonWindowClose
            // 
            this.buttonWindowClose.Enabled = false;
            this.buttonWindowClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonWindowClose.Location = new System.Drawing.Point(514, 237);
            this.buttonWindowClose.Name = "buttonWindowClose";
            this.buttonWindowClose.Size = new System.Drawing.Size(128, 23);
            this.buttonWindowClose.TabIndex = 12;
            this.buttonWindowClose.Text = "ウィンドウを閉じる";
            this.buttonWindowClose.UseVisualStyleBackColor = true;
            this.buttonWindowClose.Click += new System.EventHandler(this.WindowCloseButtonClickEvent);
            // 
            // groupLayeredWindow
            // 
            this.groupLayeredWindow.Controls.Add(this.groupLayeredSetting);
            this.groupLayeredWindow.Controls.Add(this.checkLayeredWindow);
            this.groupLayeredWindow.Enabled = false;
            this.groupLayeredWindow.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupLayeredWindow.Location = new System.Drawing.Point(12, 275);
            this.groupLayeredWindow.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
            this.groupLayeredWindow.Name = "groupLayeredWindow";
            this.groupLayeredWindow.Padding = new System.Windows.Forms.Padding(12, 8, 12, 12);
            this.groupLayeredWindow.Size = new System.Drawing.Size(630, 137);
            this.groupLayeredWindow.TabIndex = 13;
            this.groupLayeredWindow.TabStop = false;
            this.groupLayeredWindow.Text = "レイヤードウィンドウ設定";
            // 
            // checkLayeredWindow
            // 
            this.checkLayeredWindow.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkLayeredWindow.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkLayeredWindow.Location = new System.Drawing.Point(15, 23);
            this.checkLayeredWindow.Name = "checkLayeredWindow";
            this.checkLayeredWindow.Size = new System.Drawing.Size(180, 23);
            this.checkLayeredWindow.TabIndex = 0;
            this.checkLayeredWindow.Text = "レイヤードウィンドウ";
            this.checkLayeredWindow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkLayeredWindow.UseVisualStyleBackColor = true;
            this.checkLayeredWindow.Click += new System.EventHandler(this.CheckLayeredWindowClickEvent);
            // 
            // groupLayeredSetting
            // 
            this.groupLayeredSetting.Controls.Add(this.buttonApplyLayeredSetting);
            this.groupLayeredSetting.Controls.Add(this.buttonSelectTransparentColor);
            this.groupLayeredSetting.Controls.Add(this.checkTransparentColor);
            this.groupLayeredSetting.Controls.Add(this.numericTransparency);
            this.groupLayeredSetting.Controls.Add(this.labelTransparency);
            this.groupLayeredSetting.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupLayeredSetting.Location = new System.Drawing.Point(214, 23);
            this.groupLayeredSetting.Margin = new System.Windows.Forms.Padding(16, 3, 3, 3);
            this.groupLayeredSetting.Name = "groupLayeredSetting";
            this.groupLayeredSetting.Padding = new System.Windows.Forms.Padding(24, 12, 16, 12);
            this.groupLayeredSetting.Size = new System.Drawing.Size(401, 99);
            this.groupLayeredSetting.TabIndex = 1;
            this.groupLayeredSetting.TabStop = false;
            this.groupLayeredSetting.Text = "透明度設定";
            // 
            // buttonApplyLayeredSetting
            // 
            this.buttonApplyLayeredSetting.Location = new System.Drawing.Point(247, 61);
            this.buttonApplyLayeredSetting.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.buttonApplyLayeredSetting.Name = "buttonApplyLayeredSetting";
            this.buttonApplyLayeredSetting.Size = new System.Drawing.Size(140, 23);
            this.buttonApplyLayeredSetting.TabIndex = 4;
            this.buttonApplyLayeredSetting.Text = "設定を適用する";
            this.buttonApplyLayeredSetting.UseVisualStyleBackColor = true;
            this.buttonApplyLayeredSetting.Click += new System.EventHandler(this.ApplyLayeredSettingEvent);
            // 
            // buttonSelectTransparentColor
            // 
            this.buttonSelectTransparentColor.Enabled = false;
            this.buttonSelectTransparentColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSelectTransparentColor.Location = new System.Drawing.Point(291, 27);
            this.buttonSelectTransparentColor.Name = "buttonSelectTransparentColor";
            this.buttonSelectTransparentColor.Size = new System.Drawing.Size(96, 23);
            this.buttonSelectTransparentColor.TabIndex = 3;
            this.buttonSelectTransparentColor.Text = "透明色の選択";
            this.buttonSelectTransparentColor.UseVisualStyleBackColor = false;
            this.buttonSelectTransparentColor.Click += new System.EventHandler(this.SelectTransparentColorEvent);
            // 
            // checkTransparentColor
            // 
            this.checkTransparentColor.AutoSize = true;
            this.checkTransparentColor.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkTransparentColor.Location = new System.Drawing.Point(167, 31);
            this.checkTransparentColor.Name = "checkTransparentColor";
            this.checkTransparentColor.Size = new System.Drawing.Size(118, 17);
            this.checkTransparentColor.TabIndex = 2;
            this.checkTransparentColor.Text = "透明色を指定する";
            this.checkTransparentColor.UseVisualStyleBackColor = true;
            this.checkTransparentColor.CheckedChanged += new System.EventHandler(this.CheckTransparentColorCheckedChangedEvent);
            // 
            // numericTransparency
            // 
            this.numericTransparency.Location = new System.Drawing.Point(74, 30);
            this.numericTransparency.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericTransparency.Name = "numericTransparency";
            this.numericTransparency.Size = new System.Drawing.Size(60, 19);
            this.numericTransparency.TabIndex = 1;
            this.numericTransparency.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // labelTransparency
            // 
            this.labelTransparency.AutoSize = true;
            this.labelTransparency.Location = new System.Drawing.Point(27, 32);
            this.labelTransparency.Name = "labelTransparency";
            this.labelTransparency.Size = new System.Drawing.Size(41, 12);
            this.labelTransparency.TabIndex = 0;
            this.labelTransparency.Text = "透明度";
            // 
            // GetHandleForm
            // 
            this.AcceptButton = this.buttonCursorPosition;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 424);
            this.Controls.Add(this.groupLayeredWindow);
            this.Controls.Add(this.buttonWindowClose);
            this.Controls.Add(this.buttonSetWindowName);
            this.Controls.Add(this.textGetWindowName);
            this.Controls.Add(this.labelGetWindowName);
            this.Controls.Add(this.textGetClassName);
            this.Controls.Add(this.labelGetClassName);
            this.Controls.Add(this.buttonTaskBarHandle);
            this.Controls.Add(this.buttonOwnHandle);
            this.Controls.Add(this.buttonHandle);
            this.Controls.Add(this.groupWindowClass);
            this.Controls.Add(this.groupPosition);
            this.Controls.Add(this.radioWindowClass);
            this.Controls.Add(this.radioPosition);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GetHandleForm";
            this.Text = "ハンドルの取得";
            ((System.ComponentModel.ISupportInitialize)(this.pictureCursor)).EndInit();
            this.groupPosition.ResumeLayout(false);
            this.groupPosition.PerformLayout();
            this.groupWindowClass.ResumeLayout(false);
            this.groupWindowClass.PerformLayout();
            this.groupLayeredWindow.ResumeLayout(false);
            this.groupLayeredSetting.ResumeLayout(false);
            this.groupLayeredSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericTransparency)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureCursor;
        private System.Windows.Forms.GroupBox groupPosition;
        private System.Windows.Forms.TextBox textPosY;
        private System.Windows.Forms.Label labelPoxY;
        private System.Windows.Forms.TextBox textPosX;
        private System.Windows.Forms.Label labelPosX;
        private System.Windows.Forms.Button buttonCursorPosition;
        private System.Windows.Forms.GroupBox groupWindowClass;
        private System.Windows.Forms.TextBox textWindow;
        private System.Windows.Forms.TextBox textClass;
        private System.Windows.Forms.Label labelWindow;
        private System.Windows.Forms.Label labelClass;
        private System.Windows.Forms.RadioButton radioPosition;
        private System.Windows.Forms.RadioButton radioWindowClass;
        private System.Windows.Forms.Button buttonHandle;
        private System.Windows.Forms.Button buttonOwnHandle;
        private System.Windows.Forms.Button buttonTaskBarHandle;
        private System.Windows.Forms.Label labelGetClassName;
        private System.Windows.Forms.Label labelGetWindowName;
        private System.Windows.Forms.TextBox textGetClassName;
        private System.Windows.Forms.TextBox textGetWindowName;
        private System.Windows.Forms.Button buttonSetWindowName;
        private System.Windows.Forms.Button buttonWindowClose;
        private System.Windows.Forms.GroupBox groupLayeredWindow;
        private System.Windows.Forms.GroupBox groupLayeredSetting;
        private System.Windows.Forms.Button buttonSelectTransparentColor;
        private System.Windows.Forms.CheckBox checkTransparentColor;
        private System.Windows.Forms.NumericUpDown numericTransparency;
        private System.Windows.Forms.Label labelTransparency;
        private System.Windows.Forms.Button buttonApplyLayeredSetting;
        private System.Windows.Forms.ColorDialog selectTransparentColorDialog;
        private System.Windows.Forms.CheckBox checkLayeredWindow;
    }
}

