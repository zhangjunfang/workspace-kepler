namespace testWinForm
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnOPBackup = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonEx1 = new ServiceStationClient.ComponentUI.ButtonEx();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn
            // 
            this.btn.Location = new System.Drawing.Point(12, 12);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(121, 52);
            this.btn.TabIndex = 0;
            this.btn.Text = "默认皮肤";
            this.btn.UseVisualStyleBackColor = true;
            this.btn.Visible = false;
            this.btn.Click += new System.EventHandler(this.btn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(139, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 52);
            this.button1.TabIndex = 1;
            this.button1.Text = "皮肤1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnOPBackup
            // 
            this.btnOPBackup.Location = new System.Drawing.Point(669, 12);
            this.btnOPBackup.Name = "btnOPBackup";
            this.btnOPBackup.Size = new System.Drawing.Size(138, 48);
            this.btnOPBackup.TabIndex = 2;
            this.btnOPBackup.Text = "测试：操作记录备份";
            this.btnOPBackup.UseVisualStyleBackColor = true;
            this.btnOPBackup.Visible = false;
            this.btnOPBackup.Click += new System.EventHandler(this.btnOPBackup_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.buttonEx1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(938, 613);
            this.flowLayoutPanel1.TabIndex = 3;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // buttonEx1
            // 
            this.buttonEx1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonEx1.BackgroundImage")));
            this.buttonEx1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonEx1.Caption = "btn1";
            this.buttonEx1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonEx1.DownImage = ((System.Drawing.Image)(resources.GetObject("buttonEx1.DownImage")));
            this.buttonEx1.Location = new System.Drawing.Point(3, 3);
            this.buttonEx1.MoveImage = ((System.Drawing.Image)(resources.GetObject("buttonEx1.MoveImage")));
            this.buttonEx1.Name = "buttonEx1";
            this.buttonEx1.NormalImage = ((System.Drawing.Image)(resources.GetObject("buttonEx1.NormalImage")));
            this.buttonEx1.Size = new System.Drawing.Size(60, 26);
            this.buttonEx1.TabIndex = 6;
            this.buttonEx1.Click += new System.EventHandler(this.buttonEx1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 613);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.btnOPBackup);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnOPBackup;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private ServiceStationClient.ComponentUI.ButtonEx buttonEx1;
    }
}

