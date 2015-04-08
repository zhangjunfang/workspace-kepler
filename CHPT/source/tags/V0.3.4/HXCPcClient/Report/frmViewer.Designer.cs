namespace HXCPcClient.Report
{
    partial class frmViewer
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
            this.previewReport = new FastReport.Preview.PreviewControl();
            this.label1 = new System.Windows.Forms.Label();
            this.cboPrintStyle = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.cboPrintStyle);
            this.pnlContainer.Controls.Add(this.label1);
            this.pnlContainer.Controls.Add(this.previewReport);
            // 
            // previewReport
            // 
            this.previewReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.previewReport.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.previewReport.Font = new System.Drawing.Font("宋体", 9F);
            this.previewReport.Location = new System.Drawing.Point(3, 36);
            this.previewReport.Name = "previewReport";
            this.previewReport.PageOffset = new System.Drawing.Point(10, 10);
            this.previewReport.Size = new System.Drawing.Size(676, 332);
            this.previewReport.TabIndex = 0;
            this.previewReport.UIStyle = FastReport.Utils.UIStyle.Office2003;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(479, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "打印样式：";
            // 
            // cboPrintStyle
            // 
            this.cboPrintStyle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPrintStyle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboPrintStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPrintStyle.FormattingEnabled = true;
            this.cboPrintStyle.Location = new System.Drawing.Point(550, 7);
            this.cboPrintStyle.Name = "cboPrintStyle";
            this.cboPrintStyle.Size = new System.Drawing.Size(121, 22);
            this.cboPrintStyle.TabIndex = 2;
            this.cboPrintStyle.SelectedIndexChanged += new System.EventHandler(this.cboPrintStyle_SelectedIndexChanged);
            // 
            // frmViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 402);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmViewer";
            this.Text = "报表预览";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmViewer_Load);
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FastReport.Preview.PreviewControl previewReport;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboPrintStyle;
        private System.Windows.Forms.Label label1;
    }
}