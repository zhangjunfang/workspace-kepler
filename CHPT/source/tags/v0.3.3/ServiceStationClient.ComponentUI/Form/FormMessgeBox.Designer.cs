namespace ServiceStationClient.ComponentUI
{
    partial class FormMessgeBox
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pbClose = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelCancel = new System.Windows.Forms.Panel();
            this.labelCancal = new System.Windows.Forms.Label();
            this.panelYes = new System.Windows.Forms.Panel();
            this.labelYes = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelMsg = new System.Windows.Forms.Label();
            this.panelMsg = new System.Windows.Forms.Panel();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).BeginInit();
            this.panel2.SuspendLayout();
            this.panelCancel.SuspendLayout();
            this.panelYes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelMsg.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(129)))), ((int)(((byte)(194)))));
            this.panelTop.Controls.Add(this.pictureBox2);
            this.panelTop.Controls.Add(this.label2);
            this.panelTop.Controls.Add(this.pbClose);
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(440, 36);
            this.panelTop.TabIndex = 0;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = global::ServiceStationClient.ComponentUI.Properties.Resources.logo;
            this.pictureBox2.Location = new System.Drawing.Point(8, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(12, 12);
            this.pictureBox2.TabIndex = 48;
            this.pictureBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(23, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 49;
            this.label2.Text = "提示";
            // 
            // pbClose
            // 
            this.pbClose.BackColor = System.Drawing.Color.Transparent;
            this.pbClose.BackgroundImage = global::ServiceStationClient.ComponentUI.Properties.Resources.close_n;
            this.pbClose.Location = new System.Drawing.Point(417, 12);
            this.pbClose.Name = "pbClose";
            this.pbClose.Size = new System.Drawing.Size(11, 9);
            this.pbClose.TabIndex = 5;
            this.pbClose.TabStop = false;
            this.pbClose.Click += new System.EventHandler(this.pbClose_Click);
            this.pbClose.MouseEnter += new System.EventHandler(this.pbClose_MouseEnter);
            this.pbClose.MouseLeave += new System.EventHandler(this.pbClose_MouseLeave);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(225)))), ((int)(((byte)(226)))));
            this.panel2.Controls.Add(this.panelCancel);
            this.panel2.Controls.Add(this.panelYes);
            this.panel2.Location = new System.Drawing.Point(0, 132);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(440, 68);
            this.panel2.TabIndex = 1;
            // 
            // panelCancel
            // 
            this.panelCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(187)))), ((int)(((byte)(202)))));
            this.panelCancel.Controls.Add(this.labelCancal);
            this.panelCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelCancel.Location = new System.Drawing.Point(318, 18);
            this.panelCancel.Name = "panelCancel";
            this.panelCancel.Size = new System.Drawing.Size(102, 35);
            this.panelCancel.TabIndex = 7;
            this.panelCancel.Click += new System.EventHandler(this.panelCancel_Click);
            // 
            // labelCancal
            // 
            this.labelCancal.AutoSize = true;
            this.labelCancal.BackColor = System.Drawing.Color.Transparent;
            this.labelCancal.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.labelCancal.ForeColor = System.Drawing.Color.White;
            this.labelCancal.Location = new System.Drawing.Point(30, 8);
            this.labelCancal.Name = "labelCancal";
            this.labelCancal.Size = new System.Drawing.Size(47, 19);
            this.labelCancal.TabIndex = 0;
            this.labelCancal.Text = "取  消";
            this.labelCancal.Click += new System.EventHandler(this.panelCancel_Click);
            // 
            // panelYes
            // 
            this.panelYes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelYes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(129)))), ((int)(((byte)(194)))));
            this.panelYes.Controls.Add(this.labelYes);
            this.panelYes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelYes.Location = new System.Drawing.Point(202, 18);
            this.panelYes.Name = "panelYes";
            this.panelYes.Size = new System.Drawing.Size(102, 35);
            this.panelYes.TabIndex = 6;
            this.panelYes.Click += new System.EventHandler(this.panelYes_Click);
            // 
            // labelYes
            // 
            this.labelYes.AutoSize = true;
            this.labelYes.BackColor = System.Drawing.Color.Transparent;
            this.labelYes.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.labelYes.ForeColor = System.Drawing.Color.White;
            this.labelYes.Location = new System.Drawing.Point(30, 8);
            this.labelYes.Name = "labelYes";
            this.labelYes.Size = new System.Drawing.Size(47, 19);
            this.labelYes.TabIndex = 0;
            this.labelYes.Text = "确  认";
            this.labelYes.Click += new System.EventHandler(this.panelYes_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::ServiceStationClient.ComponentUI.Properties.Resources.info;
            this.pictureBox1.Location = new System.Drawing.Point(25, 60);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(52, 52);
            this.pictureBox1.TabIndex = 47;
            this.pictureBox1.TabStop = false;
            // 
            // labelMsg
            // 
            this.labelMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMsg.BackColor = System.Drawing.Color.Transparent;
            this.labelMsg.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.labelMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(129)))), ((int)(((byte)(194)))));
            this.labelMsg.Location = new System.Drawing.Point(104, 71);
            this.labelMsg.Name = "labelMsg";
            this.labelMsg.Size = new System.Drawing.Size(306, 24);
            this.labelMsg.TabIndex = 48;
            this.labelMsg.Text = "提示信息";
            // 
            // panelMsg
            // 
            this.panelMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.panelMsg.Controls.Add(this.labelMsg);
            this.panelMsg.Controls.Add(this.pictureBox1);
            this.panelMsg.Controls.Add(this.panel2);
            this.panelMsg.Controls.Add(this.panelTop);
            this.panelMsg.Location = new System.Drawing.Point(0, 0);
            this.panelMsg.Name = "panelMsg";
            this.panelMsg.Size = new System.Drawing.Size(440, 200);
            this.panelMsg.TabIndex = 0;
            // 
            // FormMessgeBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(440, 200);
            this.Controls.Add(this.panelMsg);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, -600);
            this.Name = "FormMessgeBox";
            this.ShowInTaskbar = false;
            this.Text = "FormMsg";
            this.TopMost = true;
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panelCancel.ResumeLayout(false);
            this.panelCancel.PerformLayout();
            this.panelYes.ResumeLayout(false);
            this.panelYes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelMsg.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pbClose;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelCancel;
        private System.Windows.Forms.Label labelCancal;
        private System.Windows.Forms.Panel panelYes;
        private System.Windows.Forms.Label labelYes;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelMsg;
        private System.Windows.Forms.Panel panelMsg;

    }
}