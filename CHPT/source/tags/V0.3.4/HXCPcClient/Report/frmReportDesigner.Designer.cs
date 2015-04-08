namespace HXCPcClient.Report
{
    partial class frmReportDesigner
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportDesigner));
            this.designerReport = new FastReport.Design.StandardDesigner.DesignerControl();
            this.cboPrintStyle = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.btnDelete = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnAdd = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnSave = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnImport = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnExport = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnDefault = new ServiceStationClient.ComponentUI.ButtonEx();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.designerReport)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.btnDefault);
            this.pnlContainer.Controls.Add(this.btnExport);
            this.pnlContainer.Controls.Add(this.btnImport);
            this.pnlContainer.Controls.Add(this.btnDelete);
            this.pnlContainer.Controls.Add(this.btnAdd);
            this.pnlContainer.Controls.Add(this.cboPrintStyle);
            this.pnlContainer.Controls.Add(this.label1);
            this.pnlContainer.Controls.Add(this.btnSave);
            this.pnlContainer.Controls.Add(this.designerReport);
            this.pnlContainer.Size = new System.Drawing.Size(1021, 452);
            // 
            // designerReport
            // 
            this.designerReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.designerReport.AskSave = true;
            this.designerReport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.designerReport.LayoutState = resources.GetString("designerReport.LayoutState");
            this.designerReport.Location = new System.Drawing.Point(3, 37);
            this.designerReport.Name = "designerReport";
            this.designerReport.ShowMainMenu = false;
            this.designerReport.Size = new System.Drawing.Size(1015, 412);
            this.designerReport.TabIndex = 0;
            this.designerReport.UIStyle = FastReport.Utils.UIStyle.Office2007Black;
            this.designerReport.UIStateChanged += new System.EventHandler(this.designerReport_UIStateChanged);
            // 
            // cboPrintStyle
            // 
            this.cboPrintStyle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPrintStyle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboPrintStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPrintStyle.FormattingEnabled = true;
            this.cboPrintStyle.Location = new System.Drawing.Point(889, 9);
            this.cboPrintStyle.Name = "cboPrintStyle";
            this.cboPrintStyle.Size = new System.Drawing.Size(121, 22);
            this.cboPrintStyle.TabIndex = 4;
            this.cboPrintStyle.SelectedIndexChanged += new System.EventHandler(this.cboPrintStyle_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(818, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "打印样式：";
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.BackgroundImage")));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.Caption = "删除";
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnDelete.DownImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.DownImage")));
            this.btnDelete.Location = new System.Drawing.Point(351, 5);
            this.btnDelete.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.MoveImage")));
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.NormalImage")));
            this.btnDelete.Size = new System.Drawing.Size(60, 26);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.Caption = "新增保存";
            this.btnAdd.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAdd.DownImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.DownImage")));
            this.btnAdd.Location = new System.Drawing.Point(256, 5);
            this.btnAdd.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.MoveImage")));
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.NormalImage")));
            this.btnAdd.Size = new System.Drawing.Size(71, 26);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.Caption = "保存";
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSave.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSave.DownImage")));
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(177, 5);
            this.btnSave.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSave.MoveImage")));
            this.btnSave.Name = "btnSave";
            this.btnSave.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSave.NormalImage")));
            this.btnSave.Size = new System.Drawing.Size(60, 26);
            this.btnSave.TabIndex = 1;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnImport
            // 
            this.btnImport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnImport.BackgroundImage")));
            this.btnImport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnImport.Caption = "导入";
            this.btnImport.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnImport.DownImage = ((System.Drawing.Image)(resources.GetObject("btnImport.DownImage")));
            this.btnImport.Location = new System.Drawing.Point(27, 5);
            this.btnImport.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnImport.MoveImage")));
            this.btnImport.Name = "btnImport";
            this.btnImport.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnImport.NormalImage")));
            this.btnImport.Size = new System.Drawing.Size(60, 26);
            this.btnImport.TabIndex = 7;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExport
            // 
            this.btnExport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExport.BackgroundImage")));
            this.btnExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExport.Caption = "导出";
            this.btnExport.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnExport.DownImage = ((System.Drawing.Image)(resources.GetObject("btnExport.DownImage")));
            this.btnExport.Location = new System.Drawing.Point(93, 5);
            this.btnExport.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnExport.MoveImage")));
            this.btnExport.Name = "btnExport";
            this.btnExport.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnExport.NormalImage")));
            this.btnExport.Size = new System.Drawing.Size(60, 26);
            this.btnExport.TabIndex = 8;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnDefault
            // 
            this.btnDefault.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDefault.BackgroundImage")));
            this.btnDefault.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDefault.Caption = "设为默认";
            this.btnDefault.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnDefault.DownImage = ((System.Drawing.Image)(resources.GetObject("btnDefault.DownImage")));
            this.btnDefault.Location = new System.Drawing.Point(430, 5);
            this.btnDefault.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnDefault.MoveImage")));
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnDefault.NormalImage")));
            this.btnDefault.Size = new System.Drawing.Size(60, 26);
            this.btnDefault.TabIndex = 9;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // frmReportDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 483);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmReportDesigner";
            this.Text = "报表设计器";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmReportDesigner_FormClosing);
            this.Load += new System.EventHandler(this.frmReportDesigner_Load);
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.designerReport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FastReport.Design.StandardDesigner.DesignerControl designerReport;
        private ServiceStationClient.ComponentUI.ButtonEx btnSave;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboPrintStyle;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.ButtonEx btnAdd;
        private ServiceStationClient.ComponentUI.ButtonEx btnDelete;
        private ServiceStationClient.ComponentUI.ButtonEx btnImport;
        private ServiceStationClient.ComponentUI.ButtonEx btnExport;
        private ServiceStationClient.ComponentUI.ButtonEx btnDefault;
    }
}