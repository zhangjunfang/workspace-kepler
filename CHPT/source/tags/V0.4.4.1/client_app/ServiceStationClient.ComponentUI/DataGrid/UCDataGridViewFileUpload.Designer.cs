namespace ServiceStationClient.ComponentUI.DataGrid
{
    partial class UCDataGridViewFileUpload
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCDataGridViewFileUpload));
            this.txtUpload = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.btnUpload = new ServiceStationClient.ComponentUI.ButtonEx();
            this.SuspendLayout();
            // 
            // txtUpload
            // 
            this.txtUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUpload.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtUpload.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtUpload.BackColor = System.Drawing.Color.Transparent;
            this.txtUpload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtUpload.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtUpload.ForeImage = null;
            this.txtUpload.Location = new System.Drawing.Point(0, 0);
            this.txtUpload.MaxLengh = 32767;
            this.txtUpload.Multiline = false;
            this.txtUpload.Name = "txtUpload";
            this.txtUpload.Radius = 3;
            this.txtUpload.ReadOnly = true;
            this.txtUpload.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtUpload.Size = new System.Drawing.Size(128, 23);
            this.txtUpload.TabIndex = 2;
            this.txtUpload.UseSystemPasswordChar = false;
            this.txtUpload.WaterMark = null;
            this.txtUpload.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpload.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUpload.BackgroundImage")));
            this.btnUpload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUpload.Caption = "上传";
            this.btnUpload.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnUpload.DownImage = ((System.Drawing.Image)(resources.GetObject("btnUpload.DownImage")));
            this.btnUpload.Location = new System.Drawing.Point(129, 0);
            this.btnUpload.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnUpload.MoveImage")));
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnUpload.NormalImage")));
            this.btnUpload.Size = new System.Drawing.Size(60, 23);
            this.btnUpload.TabIndex = 1;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // UCDataGridViewFileUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtUpload);
            this.Controls.Add(this.btnUpload);
            this.Name = "UCDataGridViewFileUpload";
            this.Size = new System.Drawing.Size(191, 24);
            this.ResumeLayout(false);

        }

        #endregion

        private ButtonEx btnUpload;
        private TextBoxEx txtUpload;
    }
}
