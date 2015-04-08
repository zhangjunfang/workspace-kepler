namespace ServiceStationClient.ComponentUI.TextBox
{
    partial class TextChooser
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnChooser = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip();
            this.txtText = new ServiceStationClient.ComponentUI.TextBoxEx();
            ((System.ComponentModel.ISupportInitialize)(this.BtnChooser)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnChooser
            // 
            this.BtnChooser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnChooser.BackColor = System.Drawing.Color.Transparent;
            this.BtnChooser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnChooser.Location = new System.Drawing.Point(124, 3);
            this.BtnChooser.Margin = new System.Windows.Forms.Padding(0);
            this.BtnChooser.Name = "BtnChooser";
            this.BtnChooser.Size = new System.Drawing.Size(20, 21);
            this.BtnChooser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.BtnChooser.TabIndex = 6;
            this.BtnChooser.TabStop = false;
            this.BtnChooser.Click += new System.EventHandler(this.BtnChooser_Click);
            this.BtnChooser.MouseHover += new System.EventHandler(this.BtnChooser_MouseHover);
            // 
            // txtText
            // 
            this.txtText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtText.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtText.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtText.BackColor = System.Drawing.Color.Transparent;
            this.txtText.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtText.ForeImage = null;
            this.txtText.Location = new System.Drawing.Point(0, 1);
            this.txtText.Margin = new System.Windows.Forms.Padding(0);
            this.txtText.MaxLengh = 32767;
            this.txtText.Multiline = false;
            this.txtText.Name = "txtText";
            this.txtText.Radius = 3;
            this.txtText.ReadOnly = false;
            this.txtText.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtText.ShowError = false;
            this.txtText.Size = new System.Drawing.Size(124, 23);
            this.txtText.TabIndex = 4;
            this.txtText.UseSystemPasswordChar = false;
            this.txtText.Value = "";
            this.txtText.VerifyCondition = null;
            this.txtText.VerifyType = null;
            this.txtText.WaterMark = null;
            this.txtText.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // TextChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtText);
            this.Controls.Add(this.BtnChooser);
            this.Name = "TextChooser";
            this.Size = new System.Drawing.Size(145, 25);
            ((System.ComponentModel.ISupportInitialize)(this.BtnChooser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TextBoxEx txtText;
        private System.Windows.Forms.PictureBox BtnChooser;
        private System.Windows.Forms.ToolTip toolTip1;


    }
}
