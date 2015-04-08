namespace HXCServerWinForm.LoginForms
{
    partial class UCCmbItem
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
            this.labelDisplay = new System.Windows.Forms.Label();
            this.pbDelete = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbDelete)).BeginInit();
            this.SuspendLayout();
            // 
            // labelDisplay
            // 
            this.labelDisplay.AutoSize = true;
            this.labelDisplay.BackColor = System.Drawing.Color.Transparent;
            this.labelDisplay.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelDisplay.Location = new System.Drawing.Point(10, 3);
            this.labelDisplay.Name = "labelDisplay";
            this.labelDisplay.Size = new System.Drawing.Size(59, 21);
            this.labelDisplay.TabIndex = 4;
            this.labelDisplay.Text = "用户ID";
            this.labelDisplay.Click += new System.EventHandler(this.UCCmbItem_Click);
            // 
            // pbDelete
            // 
            this.pbDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDelete.BackColor = System.Drawing.Color.Transparent;
            this.pbDelete.BackgroundImage = global::HXCServerWinForm.Properties.Resources.clear;
            this.pbDelete.Location = new System.Drawing.Point(148, 8);
            this.pbDelete.Name = "pbDelete";
            this.pbDelete.Size = new System.Drawing.Size(15, 15);
            this.pbDelete.TabIndex = 5;
            this.pbDelete.TabStop = false;
            this.pbDelete.Click += new System.EventHandler(this.pbDelete_Click);
            // 
            // UCCmbItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbDelete);
            this.Controls.Add(this.labelDisplay);
            this.DoubleBuffered = true;
            this.Name = "UCCmbItem";
            this.Size = new System.Drawing.Size(171, 29);
            this.Click += new System.EventHandler(this.UCCmbItem_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UCCmbItem_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.UCCmbItem_KeyUp);
            this.MouseEnter += new System.EventHandler(this.UCCmbItem_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.UCCmbItem_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this.pbDelete)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbDelete;
        private System.Windows.Forms.Label labelDisplay;
    }
}
