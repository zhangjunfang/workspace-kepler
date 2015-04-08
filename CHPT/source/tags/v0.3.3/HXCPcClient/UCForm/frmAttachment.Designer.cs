namespace HXCPcClient.UCForm
{
    partial class frmAttachment
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
            this.ucAttachment1 = new HXCPcClient.UCForm.UCAttachment();
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.ucAttachment1);
            // 
            // ucAttachment1
            // 
            this.ucAttachment1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucAttachment1.Location = new System.Drawing.Point(0, 3);
            this.ucAttachment1.Name = "ucAttachment1";
            this.ucAttachment1.Size = new System.Drawing.Size(679, 365);
            this.ucAttachment1.TabIndex = 0;
            this.ucAttachment1.TableName = "";
            this.ucAttachment1.TableNameKeyValue = "";
            // 
            // frmAttachment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 402);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmAttachment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "单据附件";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAttachment_FormClosing);
            this.pnlContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UCAttachment ucAttachment1;
    }
}