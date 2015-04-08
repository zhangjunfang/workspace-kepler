using System.Windows.Forms;
namespace ServiceStationClient.ComponentUI
{
    partial class FormPrint
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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.printView1 = new YTS.Print.PrintView();
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.printView1);
            this.pnlContainer.Size = new System.Drawing.Size(649, 620);
            // 
            // printView1
            // 
            this.printView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printView1.HasFooter = false;
            this.printView1.HasTitle = false;
            this.printView1.Location = new System.Drawing.Point(0, 0);
            this.printView1.Name = "printView1";
            this.printView1.Size = new System.Drawing.Size(649, 620);
            this.printView1.TabIndex = 0;
            this.printView1.TitleFont = null;
            // 
            // FormPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 650);
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPrint";
            this.Opacity = 0.8D;
            this.Text = "打印预览";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ToolTip toolTip1;
        private YTS.Print.PrintView printView1;
    }
}