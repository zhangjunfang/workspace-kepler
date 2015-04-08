namespace ProgramUpdate
{
    partial class frmUpMain
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpMain));
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.hstprogressbar1 = new hstwintoolbox.hstprogressbar();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 186);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 12);
            this.label7.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 241);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 12);
            this.label3.TabIndex = 11;
            // 
            // hstprogressbar1
            // 
            this.hstprogressbar1.BorderColor = System.Drawing.Color.DarkGreen;
            this.hstprogressbar1.Caption = "正在升级文件  ";
            this.hstprogressbar1.Location = new System.Drawing.Point(0, 0);
            this.hstprogressbar1.Name = "hstprogressbar1";
            this.hstprogressbar1.RealValue = 1;
            this.hstprogressbar1.Size = new System.Drawing.Size(700, 40);
            this.hstprogressbar1.TabIndex = 12;
            this.hstprogressbar1.Value = 1;
            this.hstprogressbar1.ValueColor = System.Drawing.Color.YellowGreen;
            this.hstprogressbar1.ValueFont = new System.Drawing.Font("Arial", 9F);
            this.hstprogressbar1.ValueShineColor = System.Drawing.Color.YellowGreen;
            this.hstprogressbar1.ValueShowText = true;
            this.hstprogressbar1.ValueStyle = hstwintoolbox.hstprogressbar.ProgressBarStyle.Gradual;
            // 
            // frmUpMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 40);
            this.Controls.Add(this.hstprogressbar1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmUpMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "平台在线升级";
            this.Load += new System.EventHandler(this.frmUpMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        
        private hstwintoolbox.hstprogressbar hstprogressbar1;
    }
}

