namespace HXCPcClient.Chooser.CommonForm
{
    partial class ProgressBarFrm
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
            this.lblPercent = new System.Windows.Forms.Label();
            this.ProgBar = new System.Windows.Forms.ProgressBar();
            this.PBoxProgress = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PBoxProgress)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPercent
            // 
            this.lblPercent.AutoSize = true;
            this.lblPercent.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPercent.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblPercent.Location = new System.Drawing.Point(48, 57);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(49, 19);
            this.lblPercent.TabIndex = 1;
            this.lblPercent.Text = "100%";
            this.lblPercent.Visible = false;
            // 
            // ProgBar
            // 
            this.ProgBar.Location = new System.Drawing.Point(36, 60);
            this.ProgBar.Name = "ProgBar";
            this.ProgBar.Size = new System.Drawing.Size(13, 12);
            this.ProgBar.TabIndex = 3;
            this.ProgBar.Visible = false;
            // 
            // PBoxProgress
            // 
            this.PBoxProgress.Image = global::HXCPcClient.Properties.Resources.progres;
            this.PBoxProgress.Location = new System.Drawing.Point(38, 0);
            this.PBoxProgress.Name = "PBoxProgress";
            this.PBoxProgress.Size = new System.Drawing.Size(58, 58);
            this.PBoxProgress.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PBoxProgress.TabIndex = 2;
            this.PBoxProgress.TabStop = false;
            // 
            // ProgressBarFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(76, 76);
            this.Controls.Add(this.ProgBar);
            this.Controls.Add(this.PBoxProgress);
            this.Controls.Add(this.lblPercent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressBarFrm";
            this.Opacity = 0.75D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.ProgressBarFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PBoxProgress)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.PictureBox PBoxProgress;
        private System.Windows.Forms.ProgressBar ProgBar;
    }
}