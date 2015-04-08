namespace HXCServerWinForm.UCForm.RunLog
{
    partial class UCServiceRunLog
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCServiceRunLog));
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.txtname = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.cmbcom = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cmbrole = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cmborg = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cmbacc = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.dtploginend = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.dtpregend = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.dtploginstart = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.dtpregstart = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.label5 = new System.Windows.Forms.Label();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControlEx1 = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tabPageTextBox = new System.Windows.Forms.TabPage();
            this.txtServiceLog = new System.Windows.Forms.TextBox();
            this.tabPageGrid = new System.Windows.Forms.TabPage();
            this.panelEx1.SuspendLayout();
            this.tabControlEx1.SuspendLayout();
            this.tabPageTextBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOpt
            // 
            this.pnlOpt.Size = new System.Drawing.Size(1150, 25);
            // 
            // panelEx1
            // 
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.Controls.Add(this.txtname);
            this.panelEx1.Controls.Add(this.cmbcom);
            this.panelEx1.Controls.Add(this.cmbrole);
            this.panelEx1.Controls.Add(this.cmborg);
            this.panelEx1.Controls.Add(this.cmbacc);
            this.panelEx1.Controls.Add(this.dtploginend);
            this.panelEx1.Controls.Add(this.dtpregend);
            this.panelEx1.Controls.Add(this.dtploginstart);
            this.panelEx1.Controls.Add(this.dtpregstart);
            this.panelEx1.Controls.Add(this.btnSearch);
            this.panelEx1.Controls.Add(this.label5);
            this.panelEx1.Controls.Add(this.btnClear);
            this.panelEx1.Controls.Add(this.label12);
            this.panelEx1.Controls.Add(this.label4);
            this.panelEx1.Controls.Add(this.label7);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.label11);
            this.panelEx1.Controls.Add(this.label6);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 28);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1150, 99);
            this.panelEx1.TabIndex = 11;
            // 
            // txtname
            // 
            this.txtname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtname.BackColor = System.Drawing.Color.Transparent;
            this.txtname.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtname.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtname.ForeImage = null;
            this.txtname.Location = new System.Drawing.Point(724, 49);
            this.txtname.MaxLengh = 32767;
            this.txtname.Multiline = false;
            this.txtname.Name = "txtname";
            this.txtname.Radius = 3;
            this.txtname.ReadOnly = false;
            this.txtname.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtname.Size = new System.Drawing.Size(120, 23);
            this.txtname.TabIndex = 29;
            this.txtname.UseSystemPasswordChar = false;
            this.txtname.WaterMark = null;
            this.txtname.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // cmbcom
            // 
            this.cmbcom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbcom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbcom.FormattingEnabled = true;
            this.cmbcom.Location = new System.Drawing.Point(258, 15);
            this.cmbcom.Name = "cmbcom";
            this.cmbcom.Size = new System.Drawing.Size(183, 22);
            this.cmbcom.TabIndex = 28;
            // 
            // cmbrole
            // 
            this.cmbrole.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbrole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbrole.FormattingEnabled = true;
            this.cmbrole.Location = new System.Drawing.Point(723, 15);
            this.cmbrole.Name = "cmbrole";
            this.cmbrole.Size = new System.Drawing.Size(121, 22);
            this.cmbrole.TabIndex = 28;
            // 
            // cmborg
            // 
            this.cmborg.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmborg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmborg.FormattingEnabled = true;
            this.cmborg.Location = new System.Drawing.Point(530, 15);
            this.cmborg.Name = "cmborg";
            this.cmborg.Size = new System.Drawing.Size(121, 22);
            this.cmborg.TabIndex = 28;
            // 
            // cmbacc
            // 
            this.cmbacc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbacc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbacc.FormattingEnabled = true;
            this.cmbacc.Items.AddRange(new object[] {
            "不限"});
            this.cmbacc.Location = new System.Drawing.Point(78, 15);
            this.cmbacc.Name = "cmbacc";
            this.cmbacc.Size = new System.Drawing.Size(121, 22);
            this.cmbacc.TabIndex = 28;
            // 
            // dtploginend
            // 
            this.dtploginend.Location = new System.Drawing.Point(200, 49);
            this.dtploginend.Name = "dtploginend";
            this.dtploginend.ShowFormat = "yyyy-MM-dd";
            this.dtploginend.Size = new System.Drawing.Size(116, 21);
            this.dtploginend.TabIndex = 27;
            // 
            // dtpregend
            // 
            this.dtpregend.Location = new System.Drawing.Point(520, 49);
            this.dtpregend.Name = "dtpregend";
            this.dtpregend.ShowFormat = "yyyy-MM-dd";
            this.dtpregend.Size = new System.Drawing.Size(116, 21);
            this.dtpregend.TabIndex = 27;
            // 
            // dtploginstart
            // 
            this.dtploginstart.Location = new System.Drawing.Point(78, 49);
            this.dtploginstart.Name = "dtploginstart";
            this.dtploginstart.ShowFormat = "yyyy-MM-dd";
            this.dtploginstart.Size = new System.Drawing.Size(99, 21);
            this.dtploginstart.TabIndex = 27;
            // 
            // dtpregstart
            // 
            this.dtpregstart.Location = new System.Drawing.Point(398, 49);
            this.dtpregstart.Name = "dtpregstart";
            this.dtpregstart.ShowFormat = "yyyy-MM-dd";
            this.dtpregstart.Size = new System.Drawing.Size(99, 21);
            this.dtpregstart.TabIndex = 27;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.DownImage")));
            this.btnSearch.Location = new System.Drawing.Point(856, 49);
            this.btnSearch.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.MoveImage")));
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.NormalImage")));
            this.btnSearch.Size = new System.Drawing.Size(76, 26);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(181, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "至 ";
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(856, 13);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(76, 26);
            this.btnClear.TabIndex = 12;
            this.btnClear.Tag = "1";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(501, 53);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(23, 12);
            this.label12.TabIndex = 11;
            this.label12.Text = "至 ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "登录时间：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(676, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "用户名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(676, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "角色：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(335, 53);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 10;
            this.label11.Text = "注册时间：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(483, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "组织：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(217, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "公司：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "帐套：";
            // 
            // tabControlEx1
            // 
            this.tabControlEx1.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tabControlEx1.Controls.Add(this.tabPageTextBox);
            this.tabControlEx1.Controls.Add(this.tabPageGrid);
            this.tabControlEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlEx1.Location = new System.Drawing.Point(0, 127);
            this.tabControlEx1.Name = "tabControlEx1";
            this.tabControlEx1.SelectedIndex = 0;
            this.tabControlEx1.Size = new System.Drawing.Size(1150, 483);
            this.tabControlEx1.TabIndex = 12;
            // 
            // tabPageTextBox
            // 
            this.tabPageTextBox.Controls.Add(this.txtServiceLog);
            this.tabPageTextBox.Location = new System.Drawing.Point(4, 26);
            this.tabPageTextBox.Name = "tabPageTextBox";
            this.tabPageTextBox.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTextBox.Size = new System.Drawing.Size(1142, 453);
            this.tabPageTextBox.TabIndex = 0;
            this.tabPageTextBox.Text = "文本展示";
            this.tabPageTextBox.UseVisualStyleBackColor = true;
            // 
            // txtServiceLog
            // 
            this.txtServiceLog.BackColor = System.Drawing.Color.White;
            this.txtServiceLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtServiceLog.Location = new System.Drawing.Point(3, 3);
            this.txtServiceLog.Multiline = true;
            this.txtServiceLog.Name = "txtServiceLog";
            this.txtServiceLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtServiceLog.Size = new System.Drawing.Size(1136, 447);
            this.txtServiceLog.TabIndex = 4;
            // 
            // tabPageGrid
            // 
            this.tabPageGrid.Location = new System.Drawing.Point(4, 26);
            this.tabPageGrid.Name = "tabPageGrid";
            this.tabPageGrid.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGrid.Size = new System.Drawing.Size(1142, 453);
            this.tabPageGrid.TabIndex = 1;
            this.tabPageGrid.Text = "表格展示";
            this.tabPageGrid.UseVisualStyleBackColor = true;
            // 
            // UCServiceRunLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlEx1);
            this.Controls.Add(this.panelEx1);
            this.Name = "UCServiceRunLog";
            this.Size = new System.Drawing.Size(1150, 610);
            this.Load += new System.EventHandler(this.UCServiceRunLog_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.panelEx1, 0);
            this.Controls.SetChildIndex(this.tabControlEx1, 0);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.tabControlEx1.ResumeLayout(false);
            this.tabPageTextBox.ResumeLayout(false);
            this.tabPageTextBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private ServiceStationClient.ComponentUI.TextBoxEx txtname;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbcom;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbrole;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmborg;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbacc;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dtploginend;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dtpregend;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dtploginstart;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dtpregstart;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private System.Windows.Forms.Label label5;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.TabControlEx tabControlEx1;
        private System.Windows.Forms.TabPage tabPageTextBox;
        private System.Windows.Forms.TextBox txtServiceLog;
        private System.Windows.Forms.TabPage tabPageGrid;


    }
}
