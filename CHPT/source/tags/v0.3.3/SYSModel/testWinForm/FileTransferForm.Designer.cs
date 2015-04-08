namespace testWinForm
{
    partial class FileTransferForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.btnPicTransfer = new System.Windows.Forms.Button();
            this.btnFileTransfer = new System.Windows.Forms.Button();
            this.btnPicDownLoad = new System.Windows.Forms.Button();
            this.btnFileDownLoad = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(339, 354);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // txtMsg
            // 
            this.txtMsg.Location = new System.Drawing.Point(467, 13);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMsg.Size = new System.Drawing.Size(393, 416);
            this.txtMsg.TabIndex = 1;
            // 
            // btnPicTransfer
            // 
            this.btnPicTransfer.Location = new System.Drawing.Point(13, 386);
            this.btnPicTransfer.Name = "btnPicTransfer";
            this.btnPicTransfer.Size = new System.Drawing.Size(75, 23);
            this.btnPicTransfer.TabIndex = 2;
            this.btnPicTransfer.Text = "上传图片";
            this.btnPicTransfer.UseVisualStyleBackColor = true;
            this.btnPicTransfer.Click += new System.EventHandler(this.btnPicTransfer_Click);
            // 
            // btnFileTransfer
            // 
            this.btnFileTransfer.Location = new System.Drawing.Point(12, 415);
            this.btnFileTransfer.Name = "btnFileTransfer";
            this.btnFileTransfer.Size = new System.Drawing.Size(75, 23);
            this.btnFileTransfer.TabIndex = 3;
            this.btnFileTransfer.Text = "上传文件";
            this.btnFileTransfer.UseVisualStyleBackColor = true;
            // 
            // btnPicDownLoad
            // 
            this.btnPicDownLoad.Location = new System.Drawing.Point(13, 444);
            this.btnPicDownLoad.Name = "btnPicDownLoad";
            this.btnPicDownLoad.Size = new System.Drawing.Size(75, 23);
            this.btnPicDownLoad.TabIndex = 4;
            this.btnPicDownLoad.Text = "下载图片";
            this.btnPicDownLoad.UseVisualStyleBackColor = true;
            // 
            // btnFileDownLoad
            // 
            this.btnFileDownLoad.Location = new System.Drawing.Point(13, 474);
            this.btnFileDownLoad.Name = "btnFileDownLoad";
            this.btnFileDownLoad.Size = new System.Drawing.Size(75, 23);
            this.btnFileDownLoad.TabIndex = 5;
            this.btnFileDownLoad.Text = "下载文件";
            this.btnFileDownLoad.UseVisualStyleBackColor = true;
            // 
            // FileTransferForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 550);
            this.Controls.Add(this.btnFileDownLoad);
            this.Controls.Add(this.btnPicDownLoad);
            this.Controls.Add(this.btnFileTransfer);
            this.Controls.Add(this.btnPicTransfer);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.pictureBox1);
            this.Name = "FileTransferForm";
            this.Text = "FileTransferForm";
            this.Load += new System.EventHandler(this.FileTransferForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.Button btnPicTransfer;
        private System.Windows.Forms.Button btnFileTransfer;
        private System.Windows.Forms.Button btnPicDownLoad;
        private System.Windows.Forms.Button btnFileDownLoad;
    }
}