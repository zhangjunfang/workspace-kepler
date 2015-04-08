namespace testWinForm
{
    partial class frmReport
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.hxcDataSet = new testWinForm.hxcDataSet();
            this.tb_bill_receivableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tb_bill_receivableTableAdapter = new testWinForm.hxcDataSetTableAdapters.tb_bill_receivableTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.hxcDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_bill_receivableBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.tb_bill_receivableBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "testWinForm.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(106, 71);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(396, 246);
            this.reportViewer1.TabIndex = 0;
            // 
            // hxcDataSet
            // 
            this.hxcDataSet.DataSetName = "hxcDataSet";
            this.hxcDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tb_bill_receivableBindingSource
            // 
            this.tb_bill_receivableBindingSource.DataMember = "tb_bill_receivable";
            this.tb_bill_receivableBindingSource.DataSource = this.hxcDataSet;
            // 
            // tb_bill_receivableTableAdapter
            // 
            this.tb_bill_receivableTableAdapter.ClearBeforeFill = true;
            // 
            // frmReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 403);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmReport";
            this.Text = "frmReport";
            this.Load += new System.EventHandler(this.frmReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.hxcDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_bill_receivableBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource tb_bill_receivableBindingSource;
        private hxcDataSet hxcDataSet;
        private hxcDataSetTableAdapters.tb_bill_receivableTableAdapter tb_bill_receivableTableAdapter;
    }
}