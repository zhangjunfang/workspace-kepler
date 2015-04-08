namespace ServiceStationClient.ComponentUI.TextBox
{
    partial class UCAutoComplete
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
            this.txtKeyWords = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.SuspendLayout();
            // 
            // txtKeyWords
            // 
            this.txtKeyWords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKeyWords.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtKeyWords.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtKeyWords.BackColor = System.Drawing.Color.Transparent;
            this.txtKeyWords.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtKeyWords.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtKeyWords.Caption = "请输入名称";
            this.txtKeyWords.ForeImage = null;
            this.txtKeyWords.Location = new System.Drawing.Point(0, 0);
            this.txtKeyWords.MaxLengh = 32767;
            this.txtKeyWords.Multiline = false;
            this.txtKeyWords.Name = "txtKeyWords";
            this.txtKeyWords.Radius = 3;
            this.txtKeyWords.ReadOnly = false;
            this.txtKeyWords.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtKeyWords.Size = new System.Drawing.Size(159, 23);
            this.txtKeyWords.TabIndex = 0;
            this.txtKeyWords.UseSystemPasswordChar = false;
            this.txtKeyWords.WaterMark = "请输入车牌号或组织名称";
            this.txtKeyWords.WaterMarkColor = System.Drawing.Color.Silver;
            this.txtKeyWords.UserControlValueChanged += new ServiceStationClient.ComponentUI.TextBoxEx.TextBoxChangedHandle(this.txtKeyWords_UserControlValueChanged);
            this.txtKeyWords.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKeyWords_KeyDown);
            // 
            // UCAutoComplete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtKeyWords);
            this.Name = "UCAutoComplete";
            this.Size = new System.Drawing.Size(159, 25);
            this.ResumeLayout(false);

        }

        #endregion

        public TextBoxEx txtKeyWords;

    }
}
