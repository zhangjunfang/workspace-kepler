namespace HXCPcClient
{
    partial class LogonMemory
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
            this.pbImg = new System.Windows.Forms.PictureBox();
            this.labelUserId = new System.Windows.Forms.Label();
            this.pbDelete = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDelete)).BeginInit();
            this.SuspendLayout();
            // 
            // pbImg
            // 
            this.pbImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbImg.Location = new System.Drawing.Point(8, 3);
            this.pbImg.Name = "pbImg";
            this.pbImg.Size = new System.Drawing.Size(27, 23);
            this.pbImg.TabIndex = 0;
            this.pbImg.TabStop = false;
            this.pbImg.Click += new System.EventHandler(this.LogonMemory_Click);
            // 
            // labelUserId
            // 
            this.labelUserId.AutoSize = true;
            this.labelUserId.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelUserId.Location = new System.Drawing.Point(60, 3);
            this.labelUserId.Name = "labelUserId";
            this.labelUserId.Size = new System.Drawing.Size(59, 21);
            this.labelUserId.TabIndex = 1;
            this.labelUserId.Text = "用户ID";
            this.labelUserId.Click += new System.EventHandler(this.LogonMemory_Click);
            // 
            // pbDelete
            // 
            this.pbDelete.Location = new System.Drawing.Point(159, 8);
            this.pbDelete.Name = "pbDelete";
            this.pbDelete.Size = new System.Drawing.Size(15, 15);
            this.pbDelete.TabIndex = 2;
            this.pbDelete.TabStop = false;
            this.pbDelete.Click += new System.EventHandler(this.pbDelete_Click);
            // 
            // LogonMemory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pbDelete);
            this.Controls.Add(this.labelUserId);
            this.Controls.Add(this.pbImg);
            this.Name = "LogonMemory";
            this.Size = new System.Drawing.Size(182, 29);
            this.Click += new System.EventHandler(this.LogonMemory_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LogonMemory_KeyDown);
            this.MouseEnter += new System.EventHandler(this.LogonMemory_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.LogonMemory_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this.pbImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDelete)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbImg;
        private System.Windows.Forms.Label labelUserId;
        private System.Windows.Forms.PictureBox pbDelete;
    }
}
