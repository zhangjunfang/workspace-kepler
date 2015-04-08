namespace HXCPcClient.UCForm.RepairBusiness.OldPartsManagement.YTOldPartsTagPrint
{
    partial class UCYTOldPartsTagPrintManager
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuM = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucOldPartsTagBR = new HXCPcClient.UCForm.RepairBusiness.OldPartsManagement.YTOldPartsTagPrint.UCOldPartsTagControl();
            this.ucOldPartsTagBL = new HXCPcClient.UCForm.RepairBusiness.OldPartsManagement.YTOldPartsTagPrint.UCOldPartsTagControl();
            this.ucOldPartsTagTR = new HXCPcClient.UCForm.RepairBusiness.OldPartsManagement.YTOldPartsTagPrint.UCOldPartsTagControl();
            this.ucOldPartsTagTL = new HXCPcClient.UCForm.RepairBusiness.OldPartsManagement.YTOldPartsTagPrint.UCOldPartsTagControl();
            this.contextMenuM.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuM
            // 
            this.contextMenuM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.contextMenuM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuM.Name = "contextMenuM";
            this.contextMenuM.Size = new System.Drawing.Size(185, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(184, 22);
            this.toolStripMenuItem1.Text = "宇通旧件返厂单导入";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.ucOldPartsTagBR);
            this.panel1.Controls.Add(this.ucOldPartsTagBL);
            this.panel1.Controls.Add(this.ucOldPartsTagTR);
            this.panel1.Controls.Add(this.ucOldPartsTagTL);
            this.panel1.Location = new System.Drawing.Point(5, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1022, 687);
            this.panel1.TabIndex = 4;
            // 
            // ucOldPartsTagBR
            // 
            this.ucOldPartsTagBR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.ucOldPartsTagBR.Location = new System.Drawing.Point(481, 304);
            this.ucOldPartsTagBR.Name = "ucOldPartsTagBR";
            this.ucOldPartsTagBR.Size = new System.Drawing.Size(470, 295);
            this.ucOldPartsTagBR.TabIndex = 18;
            // 
            // ucOldPartsTagBL
            // 
            this.ucOldPartsTagBL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.ucOldPartsTagBL.Location = new System.Drawing.Point(5, 304);
            this.ucOldPartsTagBL.Name = "ucOldPartsTagBL";
            this.ucOldPartsTagBL.Size = new System.Drawing.Size(470, 295);
            this.ucOldPartsTagBL.TabIndex = 17;
            // 
            // ucOldPartsTagTR
            // 
            this.ucOldPartsTagTR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.ucOldPartsTagTR.Location = new System.Drawing.Point(481, 3);
            this.ucOldPartsTagTR.Name = "ucOldPartsTagTR";
            this.ucOldPartsTagTR.Size = new System.Drawing.Size(470, 295);
            this.ucOldPartsTagTR.TabIndex = 16;
            // 
            // ucOldPartsTagTL
            // 
            this.ucOldPartsTagTL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.ucOldPartsTagTL.Location = new System.Drawing.Point(5, 3);
            this.ucOldPartsTagTL.Name = "ucOldPartsTagTL";
            this.ucOldPartsTagTL.Size = new System.Drawing.Size(470, 295);
            this.ucOldPartsTagTL.TabIndex = 15;
            // 
            // UCYTOldPartsTagPrintManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.Controls.Add(this.panel1);
            this.Name = "UCYTOldPartsTagPrintManager";
            this.Size = new System.Drawing.Size(1030, 724);
            this.Load += new System.EventHandler(this.UCYTOldPartsTagPrintManager_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.contextMenuM.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuM;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Panel panel1;
        private UCOldPartsTagControl ucOldPartsTagBR;
        private UCOldPartsTagControl ucOldPartsTagBL;
        private UCOldPartsTagControl ucOldPartsTagTR;
        private UCOldPartsTagControl ucOldPartsTagTL;

    }
}