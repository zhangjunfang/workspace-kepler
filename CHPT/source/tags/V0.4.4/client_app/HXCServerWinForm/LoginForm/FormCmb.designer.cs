namespace HXCServerWinForm.LoginForms
{
    partial class FormCmb
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
            this.peContent = new ServiceStationClient.ComponentUI.Panel.PanelExtend();
            this.SuspendLayout();
            // 
            // peContent
            // 
            this.peContent.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.peContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.peContent.Location = new System.Drawing.Point(0, 0);
            this.peContent.Name = "peContent";
            this.peContent.Padding = new System.Windows.Forms.Padding(1);
            this.peContent.ShowBorder = true;
            this.peContent.Size = new System.Drawing.Size(239, 40);
            this.peContent.TabIndex = 0;
            // 
            // FormCmb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 40);
            this.Controls.Add(this.peContent);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormCmb";
            this.Text = "FormCmb";
            this.Deactivate += new System.EventHandler(this.FormCmb_Deactivate);
            this.Load += new System.EventHandler(this.FormCmb_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.Panel.PanelExtend peContent;
    }
}