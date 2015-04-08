namespace testWinForm
{
    partial class frmSocketTest
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
            this.btnLink = new System.Windows.Forms.Button();
            this.tbIp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.tbSendMsg = new System.Windows.Forms.TextBox();
            this.tbReceiveMsg = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelTime = new System.Windows.Forms.Label();
            this.labelCount = new System.Windows.Forms.Label();
            this.tbSend = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbFilePort = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbFileSend = new System.Windows.Forms.TextBox();
            this.tbFileReceive = new System.Windows.Forms.TextBox();
            this.lableFileCount = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.labelTicks = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnLink
            // 
            this.btnLink.Location = new System.Drawing.Point(718, 27);
            this.btnLink.Name = "btnLink";
            this.btnLink.Size = new System.Drawing.Size(75, 23);
            this.btnLink.TabIndex = 0;
            this.btnLink.Text = "连接";
            this.btnLink.UseVisualStyleBackColor = true;
            this.btnLink.Click += new System.EventHandler(this.btnLink_Click);
            // 
            // tbIp
            // 
            this.tbIp.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbIp.Location = new System.Drawing.Point(81, 26);
            this.tbIp.Name = "tbIp";
            this.tbIp.Size = new System.Drawing.Size(169, 26);
            this.tbIp.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "服务器Ip:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(310, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "主端口:";
            // 
            // tbPort
            // 
            this.tbPort.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPort.Location = new System.Drawing.Point(359, 26);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(66, 26);
            this.tbPort.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(-2, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1246, 1);
            this.label3.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "登录消息：";
            // 
            // btnClose
            // 
            this.btnClose.Enabled = false;
            this.btnClose.Location = new System.Drawing.Point(839, 27);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "断开";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tbSendMsg
            // 
            this.tbSendMsg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbSendMsg.ForeColor = System.Drawing.Color.Green;
            this.tbSendMsg.Location = new System.Drawing.Point(84, 99);
            this.tbSendMsg.Multiline = true;
            this.tbSendMsg.Name = "tbSendMsg";
            this.tbSendMsg.Size = new System.Drawing.Size(767, 57);
            this.tbSendMsg.TabIndex = 8;
            // 
            // tbReceiveMsg
            // 
            this.tbReceiveMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tbReceiveMsg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbReceiveMsg.ForeColor = System.Drawing.Color.Navy;
            this.tbReceiveMsg.Location = new System.Drawing.Point(22, 487);
            this.tbReceiveMsg.Multiline = true;
            this.tbReceiveMsg.Name = "tbReceiveMsg";
            this.tbReceiveMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbReceiveMsg.Size = new System.Drawing.Size(713, 221);
            this.tbReceiveMsg.TabIndex = 9;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(922, 99);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(122, 57);
            this.btnSend.TabIndex = 10;
            this.btnSend.Text = "手动登录";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTime.ForeColor = System.Drawing.Color.Red;
            this.labelTime.Location = new System.Drawing.Point(22, 252);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(114, 20);
            this.labelTime.TabIndex = 11;
            this.labelTime.Text = "发送消息：";
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCount.ForeColor = System.Drawing.Color.Red;
            this.labelCount.Location = new System.Drawing.Point(640, 252);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(63, 20);
            this.labelCount.TabIndex = 12;
            this.labelCount.Text = "0/0条";
            // 
            // tbSend
            // 
            this.tbSend.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbSend.ForeColor = System.Drawing.Color.Navy;
            this.tbSend.Location = new System.Drawing.Point(22, 286);
            this.tbSend.Multiline = true;
            this.tbSend.Name = "tbSend";
            this.tbSend.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbSend.Size = new System.Drawing.Size(713, 174);
            this.tbSend.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BackColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(-8, 472);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1246, 1);
            this.label5.TabIndex = 14;
            // 
            // tbFilePort
            // 
            this.tbFilePort.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbFilePort.Location = new System.Drawing.Point(544, 26);
            this.tbFilePort.Name = "tbFilePort";
            this.tbFilePort.Size = new System.Drawing.Size(66, 26);
            this.tbFilePort.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(481, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "附件端口:";
            // 
            // tbFileSend
            // 
            this.tbFileSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFileSend.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbFileSend.ForeColor = System.Drawing.Color.Navy;
            this.tbFileSend.Location = new System.Drawing.Point(780, 286);
            this.tbFileSend.Multiline = true;
            this.tbFileSend.Name = "tbFileSend";
            this.tbFileSend.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbFileSend.Size = new System.Drawing.Size(431, 174);
            this.tbFileSend.TabIndex = 17;
            // 
            // tbFileReceive
            // 
            this.tbFileReceive.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFileReceive.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbFileReceive.ForeColor = System.Drawing.Color.Navy;
            this.tbFileReceive.Location = new System.Drawing.Point(780, 487);
            this.tbFileReceive.Multiline = true;
            this.tbFileReceive.Name = "tbFileReceive";
            this.tbFileReceive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbFileReceive.Size = new System.Drawing.Size(431, 221);
            this.tbFileReceive.TabIndex = 18;
            // 
            // lableFileCount
            // 
            this.lableFileCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lableFileCount.AutoSize = true;
            this.lableFileCount.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lableFileCount.ForeColor = System.Drawing.Color.Red;
            this.lableFileCount.Location = new System.Drawing.Point(1131, 252);
            this.lableFileCount.Name = "lableFileCount";
            this.lableFileCount.Size = new System.Drawing.Size(63, 20);
            this.lableFileCount.TabIndex = 19;
            this.lableFileCount.Text = "0/0条";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.BackColor = System.Drawing.Color.Gray;
            this.label8.Location = new System.Drawing.Point(756, 283);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 430);
            this.label8.TabIndex = 20;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(800, 181);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(184, 35);
            this.button1.TabIndex = 23;
            this.button1.Text = "附件表-attachment_info";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(543, 181);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(156, 35);
            this.button2.TabIndex = 24;
            this.button2.Text = "数据-sys_user";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePicker1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(227, 190);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(198, 26);
            this.dateTimePicker1.TabIndex = 25;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // labelTicks
            // 
            this.labelTicks.AutoSize = true;
            this.labelTicks.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTicks.Location = new System.Drawing.Point(19, 197);
            this.labelTicks.Name = "labelTicks";
            this.labelTicks.Size = new System.Drawing.Size(0, 14);
            this.labelTicks.TabIndex = 27;
            // 
            // frmSocketTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 720);
            this.Controls.Add(this.labelTicks);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lableFileCount);
            this.Controls.Add(this.tbFileReceive);
            this.Controls.Add(this.tbFileSend);
            this.Controls.Add(this.tbFilePort);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbSend);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.tbReceiveMsg);
            this.Controls.Add(this.tbSendMsg);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbIp);
            this.Controls.Add(this.btnLink);
            this.Name = "frmSocketTest";
            this.Text = "frmSocketTest";
            this.Load += new System.EventHandler(this.frmSocketTest_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLink;
        private System.Windows.Forms.TextBox tbIp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox tbSendMsg;
        private System.Windows.Forms.TextBox tbReceiveMsg;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.TextBox tbSend;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbFilePort;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbFileSend;
        private System.Windows.Forms.TextBox tbFileReceive;
        private System.Windows.Forms.Label lableFileCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label labelTicks;
    }
}