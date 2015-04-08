namespace HXCServerWinForm.Chooser
{
    partial class frmBackupRecord
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBackupRecord));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.cmmethod = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.dtpend = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.dtpstart = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtaccname = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtacccode = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.tcbakup = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tpbackup = new System.Windows.Forms.TabPage();
            this.dgvBakList = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.bak_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bak_acccode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bak_accname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bak_filename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bak_method = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_success = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bak_failmsg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bak_by = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlBottom = new ServiceStationClient.ComponentUI.PanelEx();
            this.page = new ServiceStationClient.ComponentUI.WinFormPager();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFileDir = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.btnTo = new ServiceStationClient.ComponentUI.ButtonEx();
            this.pnlContainer.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.tcbakup.SuspendLayout();
            this.tpbackup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBakList)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.tcbakup);
            this.pnlContainer.Controls.Add(this.pnlBottom);
            this.pnlContainer.Controls.Add(this.panelEx1);
            this.pnlContainer.Size = new System.Drawing.Size(755, 475);
            // 
            // panelEx1
            // 
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx1.BorderWidth = 1;
            this.panelEx1.Controls.Add(this.txtFileDir);
            this.panelEx1.Controls.Add(this.cmmethod);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.dtpend);
            this.panelEx1.Controls.Add(this.dtpstart);
            this.panelEx1.Controls.Add(this.label5);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.label4);
            this.panelEx1.Controls.Add(this.txtaccname);
            this.panelEx1.Controls.Add(this.txtacccode);
            this.panelEx1.Controls.Add(this.label6);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Controls.Add(this.btnTo);
            this.panelEx1.Controls.Add(this.btnSearch);
            this.panelEx1.Controls.Add(this.btnClear);
            this.panelEx1.Curvature = 0;
            this.panelEx1.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(755, 108);
            this.panelEx1.TabIndex = 3;
            // 
            // cmmethod
            // 
            this.cmmethod.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmmethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmmethod.FormattingEnabled = true;
            this.cmmethod.Location = new System.Drawing.Point(422, 42);
            this.cmmethod.Name = "cmmethod";
            this.cmmethod.Size = new System.Drawing.Size(166, 22);
            this.cmmethod.TabIndex = 33;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(360, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 32;
            this.label2.Text = "备份方式：";
            // 
            // dtpend
            // 
            this.dtpend.Location = new System.Drawing.Point(209, 41);
            this.dtpend.Name = "dtpend";
            this.dtpend.ShowFormat = "yyyy-MM-dd";
            this.dtpend.Size = new System.Drawing.Size(116, 21);
            this.dtpend.TabIndex = 30;
            // 
            // dtpstart
            // 
            this.dtpstart.Location = new System.Drawing.Point(87, 41);
            this.dtpstart.Name = "dtpstart";
            this.dtpstart.ShowFormat = "yyyy-MM-dd";
            this.dtpstart.Size = new System.Drawing.Size(99, 21);
            this.dtpstart.TabIndex = 31;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(190, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 12);
            this.label5.TabIndex = 29;
            this.label5.Text = "至 ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 28;
            this.label4.Text = "备份时间：";
            // 
            // txtaccname
            // 
            this.txtaccname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtaccname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtaccname.BackColor = System.Drawing.Color.Transparent;
            this.txtaccname.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtaccname.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtaccname.ForeImage = null;
            this.txtaccname.InputtingVerifyCondition = null;
            this.txtaccname.Location = new System.Drawing.Point(422, 12);
            this.txtaccname.MaxLengh = 32767;
            this.txtaccname.Multiline = false;
            this.txtaccname.Name = "txtaccname";
            this.txtaccname.Radius = 3;
            this.txtaccname.ReadOnly = false;
            this.txtaccname.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtaccname.ShowError = false;
            this.txtaccname.Size = new System.Drawing.Size(166, 23);
            this.txtaccname.TabIndex = 24;
            this.txtaccname.UseSystemPasswordChar = false;
            this.txtaccname.Value = "";
            this.txtaccname.VerifyCondition = null;
            this.txtaccname.VerifyType = null;
            this.txtaccname.VerifyTypeName = null;
            this.txtaccname.WaterMark = null;
            this.txtaccname.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtacccode
            // 
            this.txtacccode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtacccode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtacccode.BackColor = System.Drawing.Color.Transparent;
            this.txtacccode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtacccode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtacccode.ForeImage = null;
            this.txtacccode.InputtingVerifyCondition = null;
            this.txtacccode.Location = new System.Drawing.Point(87, 12);
            this.txtacccode.MaxLengh = 32767;
            this.txtacccode.Multiline = false;
            this.txtacccode.Name = "txtacccode";
            this.txtacccode.Radius = 3;
            this.txtacccode.ReadOnly = false;
            this.txtacccode.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtacccode.ShowError = false;
            this.txtacccode.Size = new System.Drawing.Size(121, 23);
            this.txtacccode.TabIndex = 23;
            this.txtacccode.UseSystemPasswordChar = false;
            this.txtacccode.Value = "";
            this.txtacccode.VerifyCondition = null;
            this.txtacccode.VerifyType = null;
            this.txtacccode.VerifyTypeName = null;
            this.txtacccode.WaterMark = null;
            this.txtacccode.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(360, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 22;
            this.label6.Text = "帐套名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "帐套编码：";
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.DownImage")));
            this.btnSearch.Location = new System.Drawing.Point(641, 40);
            this.btnSearch.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.MoveImage")));
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.NormalImage")));
            this.btnSearch.Size = new System.Drawing.Size(60, 26);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(641, 9);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 26);
            this.btnClear.TabIndex = 8;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // tcbakup
            // 
            this.tcbakup.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tcbakup.Controls.Add(this.tpbackup);
            this.tcbakup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcbakup.Location = new System.Drawing.Point(0, 108);
            this.tcbakup.Name = "tcbakup";
            this.tcbakup.SelectedIndex = 0;
            this.tcbakup.Size = new System.Drawing.Size(755, 332);
            this.tcbakup.TabIndex = 5;
            // 
            // tpbackup
            // 
            this.tpbackup.Controls.Add(this.dgvBakList);
            this.tpbackup.Location = new System.Drawing.Point(4, 26);
            this.tpbackup.Name = "tpbackup";
            this.tpbackup.Padding = new System.Windows.Forms.Padding(3);
            this.tpbackup.Size = new System.Drawing.Size(747, 302);
            this.tpbackup.TabIndex = 0;
            this.tpbackup.Text = "备份记录列表  ";
            this.tpbackup.UseVisualStyleBackColor = true;
            // 
            // dgvBakList
            // 
            this.dgvBakList.AllowUserToAddRows = false;
            this.dgvBakList.AllowUserToDeleteRows = false;
            this.dgvBakList.AllowUserToOrderColumns = true;
            this.dgvBakList.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvBakList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvBakList.BackgroundColor = System.Drawing.Color.White;
            this.dgvBakList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBakList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvBakList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBakList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.bak_time,
            this.bak_acccode,
            this.bak_accname,
            this.bak_filename,
            this.bak_method,
            this.is_success,
            this.bak_failmsg,
            this.bak_by});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBakList.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvBakList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBakList.EnableHeadersVisualStyles = false;
            this.dgvBakList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvBakList.IsCheck = true;
            this.dgvBakList.Location = new System.Drawing.Point(3, 3);
            this.dgvBakList.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvBakList.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvBakList.MergeColumnNames")));
            this.dgvBakList.MultiSelect = false;
            this.dgvBakList.Name = "dgvBakList";
            this.dgvBakList.ReadOnly = true;
            this.dgvBakList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.dgvBakList.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvBakList.RowHeadersVisible = false;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgvBakList.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvBakList.RowTemplate.Height = 23;
            this.dgvBakList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBakList.ShowCheckBox = true;
            this.dgvBakList.Size = new System.Drawing.Size(741, 296);
            this.dgvBakList.TabIndex = 1;
            this.ToolTip.SetToolTip(this.dgvBakList, "请双击选择");
            this.dgvBakList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBakList_CellDoubleClick);
            this.dgvBakList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvBakList_CellFormatting);
            // 
            // bak_time
            // 
            this.bak_time.DataPropertyName = "bak_time";
            this.bak_time.HeaderText = "备份时间";
            this.bak_time.Name = "bak_time";
            this.bak_time.ReadOnly = true;
            this.bak_time.Width = 120;
            // 
            // bak_acccode
            // 
            this.bak_acccode.DataPropertyName = "bak_acccode";
            this.bak_acccode.HeaderText = "帐套编码";
            this.bak_acccode.Name = "bak_acccode";
            this.bak_acccode.ReadOnly = true;
            this.bak_acccode.Width = 120;
            // 
            // bak_accname
            // 
            this.bak_accname.DataPropertyName = "bak_accname";
            this.bak_accname.HeaderText = "帐套名称";
            this.bak_accname.Name = "bak_accname";
            this.bak_accname.ReadOnly = true;
            // 
            // bak_filename
            // 
            this.bak_filename.DataPropertyName = "bak_filename";
            this.bak_filename.HeaderText = "备份文件名";
            this.bak_filename.Name = "bak_filename";
            this.bak_filename.ReadOnly = true;
            this.bak_filename.Width = 120;
            // 
            // bak_method
            // 
            this.bak_method.DataPropertyName = "bak_method";
            this.bak_method.HeaderText = "备份方式";
            this.bak_method.Name = "bak_method";
            this.bak_method.ReadOnly = true;
            this.bak_method.Width = 120;
            // 
            // is_success
            // 
            this.is_success.DataPropertyName = "is_success";
            this.is_success.HeaderText = "是否成功";
            this.is_success.Name = "is_success";
            this.is_success.ReadOnly = true;
            this.is_success.Width = 120;
            // 
            // bak_failmsg
            // 
            this.bak_failmsg.DataPropertyName = "bak_failmsg";
            this.bak_failmsg.HeaderText = "错误信息";
            this.bak_failmsg.Name = "bak_failmsg";
            this.bak_failmsg.ReadOnly = true;
            // 
            // bak_by
            // 
            this.bak_by.DataPropertyName = "bak_by";
            this.bak_by.HeaderText = "操作人";
            this.bak_by.Name = "bak_by";
            this.bak_by.ReadOnly = true;
            this.bak_by.Width = 120;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlBottom.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlBottom.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.pnlBottom.BorderWidth = 1;
            this.pnlBottom.Controls.Add(this.page);
            this.pnlBottom.Curvature = 0;
            this.pnlBottom.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.pnlBottom.Location = new System.Drawing.Point(0, 440);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(755, 35);
            this.pnlBottom.TabIndex = 6;
            // 
            // page
            // 
            this.page.BackColor = System.Drawing.Color.Transparent;
            this.page.BtnTextNext = "下页";
            this.page.BtnTextPrevious = "上页";
            this.page.Location = new System.Drawing.Point(277, 0);
            this.page.Name = "page";
            this.page.PageCount = 0;
            this.page.PageSize = 15;
            this.page.RecordCount = 0;
            this.page.Size = new System.Drawing.Size(453, 31);
            this.page.TabIndex = 0;
            this.page.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.page_PageIndexChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 12);
            this.label3.TabIndex = 28;
            this.label3.Text = "备份文件存放路径：";
            // 
            // txtFileDir
            // 
            this.txtFileDir.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtFileDir.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtFileDir.BackColor = System.Drawing.Color.Transparent;
            this.txtFileDir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtFileDir.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtFileDir.ForeImage = null;
            this.txtFileDir.InputtingVerifyCondition = null;
            this.txtFileDir.Location = new System.Drawing.Point(132, 71);
            this.txtFileDir.MaxLengh = 32767;
            this.txtFileDir.Multiline = false;
            this.txtFileDir.Name = "txtFileDir";
            this.txtFileDir.Radius = 3;
            this.txtFileDir.ReadOnly = true;
            this.txtFileDir.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtFileDir.ShowError = false;
            this.txtFileDir.Size = new System.Drawing.Size(453, 23);
            this.txtFileDir.TabIndex = 24;
            this.txtFileDir.UseSystemPasswordChar = false;
            this.txtFileDir.Value = "";
            this.txtFileDir.VerifyCondition = null;
            this.txtFileDir.VerifyType = null;
            this.txtFileDir.VerifyTypeName = null;
            this.txtFileDir.WaterMark = null;
            this.txtFileDir.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // btnTo
            // 
            this.btnTo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTo.BackgroundImage")));
            this.btnTo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTo.Caption = "打开";
            this.btnTo.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnTo.DownImage = ((System.Drawing.Image)(resources.GetObject("btnTo.DownImage")));
            this.btnTo.Location = new System.Drawing.Point(591, 72);
            this.btnTo.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnTo.MoveImage")));
            this.btnTo.Name = "btnTo";
            this.btnTo.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnTo.NormalImage")));
            this.btnTo.Size = new System.Drawing.Size(37, 22);
            this.btnTo.TabIndex = 9;
            this.btnTo.Click += new System.EventHandler(this.btnTo_Click);
            // 
            // frmBackupRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 506);
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBackupRecord";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "帐套备份";
            this.Load += new System.EventHandler(this.frmBackupRecord_Load);
            this.pnlContainer.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.tcbakup.ResumeLayout(false);
            this.tpbackup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBakList)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private ServiceStationClient.ComponentUI.TextBoxEx txtaccname;
        private ServiceStationClient.ComponentUI.TextBoxEx txtacccode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.TabControlEx tcbakup;
        private System.Windows.Forms.TabPage tpbackup;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvBakList;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dtpend;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dtpstart;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmmethod;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.PanelEx pnlBottom;
        private ServiceStationClient.ComponentUI.WinFormPager page;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn bak_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn bak_acccode;
        private System.Windows.Forms.DataGridViewTextBoxColumn bak_accname;
        private System.Windows.Forms.DataGridViewTextBoxColumn bak_filename;
        private System.Windows.Forms.DataGridViewTextBoxColumn bak_method;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_success;
        private System.Windows.Forms.DataGridViewTextBoxColumn bak_failmsg;
        private System.Windows.Forms.DataGridViewTextBoxColumn bak_by;
        private ServiceStationClient.ComponentUI.TextBoxEx txtFileDir;
        private System.Windows.Forms.Label label3;
        private ServiceStationClient.ComponentUI.ButtonEx btnTo;
    }
}