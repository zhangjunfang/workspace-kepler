namespace AutoUpdate
{
    partial class frmDownload
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDownload));
            this.hstprogressbar1 = new hstwintoolbox.hstprogressbar();
            this.SuspendLayout();
            // 
            // hstprogressbar1
            // 
            this.hstprogressbar1.BorderColor = System.Drawing.Color.DarkGreen;
            this.hstprogressbar1.Caption = "正在升级文件  ";
            this.hstprogressbar1.Location = new System.Drawing.Point(0, 0);
            this.hstprogressbar1.Name = "hstprogressbar1";
            this.hstprogressbar1.RealValue = 0;
            this.hstprogressbar1.Size = new System.Drawing.Size(700, 40);
            this.hstprogressbar1.TabIndex = 13;
            this.hstprogressbar1.ValueColor = System.Drawing.Color.YellowGreen;
            this.hstprogressbar1.ValueFont = new System.Drawing.Font("Arial", 9F);
            this.hstprogressbar1.ValueShineColor = System.Drawing.Color.YellowGreen;
            this.hstprogressbar1.ValueShowText = true;
            this.hstprogressbar1.ValueStyle = hstwintoolbox.hstprogressbar.ProgressBarStyle.Gradual;
            // 
            // frmDownload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 40);
            this.ControlBox = false;
            this.Controls.Add(this.hstprogressbar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDownload";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDownload";
            this.Load += new System.EventHandler(this.frmDownload_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private hstwintoolbox.hstprogressbar hstprogressbar1;
    }
}