namespace HXCPcClient.UCForm.DataManage.VehicleFiles
{
    partial class BigImage
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
            this.pibBigImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pibBigImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pibBigImage
            // 
            this.pibBigImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.pibBigImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pibBigImage.Location = new System.Drawing.Point(0, 0);
            this.pibBigImage.Name = "pibBigImage";
            this.pibBigImage.Size = new System.Drawing.Size(1026, 662);
            this.pibBigImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pibBigImage.TabIndex = 0;
            this.pibBigImage.TabStop = false;
            // 
            // BigImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 662);
            this.Controls.Add(this.pibBigImage);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BigImage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "放大图片";
            ((System.ComponentModel.ISupportInitialize)(this.pibBigImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pibBigImage;
    }
}