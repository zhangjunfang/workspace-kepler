namespace HXCPcClient.UCForm.SysManage.BulletinManage
{
    partial class UCBulletinAddOrEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCBulletinAddOrEdit));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.palTop = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnPerson = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnOrganization = new ServiceStationClient.ComponentUI.ButtonEx();
            this.rtbPerson = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rtbOrganization = new System.Windows.Forms.RichTextBox();
            this.labContent = new System.Windows.Forms.Label();
            this.txtTitle = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.dtpSTime = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.label4 = new System.Windows.Forms.Label();
            this.labTitle = new System.Windows.Forms.Label();
            this.webContent = new System.Windows.Forms.WebBrowser();
            this.tcUsers = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tpUsers = new System.Windows.Forms.TabPage();
            this.ucAttr = new HXCPcClient.UCForm.UCAttachment();
            this.dgvAttachment = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.dataGridViewEx1 = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.cmbUser = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.cmbOrg = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.palTop.SuspendLayout();
            this.tcUsers.SuspendLayout();
            this.tpUsers.SuspendLayout();
            this.ucAttr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttachment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).BeginInit();
            this.SuspendLayout();
            // 
            // palTop
            // 
            this.palTop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.palTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.palTop.Controls.Add(this.label10);
            this.palTop.Controls.Add(this.label3);
            this.palTop.Controls.Add(this.btnPerson);
            this.palTop.Controls.Add(this.btnOrganization);
            this.palTop.Controls.Add(this.rtbPerson);
            this.palTop.Controls.Add(this.label2);
            this.palTop.Controls.Add(this.label1);
            this.palTop.Controls.Add(this.rtbOrganization);
            this.palTop.Controls.Add(this.labContent);
            this.palTop.Controls.Add(this.txtTitle);
            this.palTop.Controls.Add(this.dtpSTime);
            this.palTop.Controls.Add(this.label4);
            this.palTop.Controls.Add(this.labTitle);
            this.palTop.Controls.Add(this.webContent);
            this.palTop.Location = new System.Drawing.Point(0, 25);
            this.palTop.Name = "palTop";
            this.palTop.Size = new System.Drawing.Size(1030, 382);
            this.palTop.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(77, 211);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 12);
            this.label10.TabIndex = 85;
            this.label10.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(77, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 82;
            this.label3.Text = "*";
            // 
            // btnPerson
            // 
            this.btnPerson.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPerson.BackgroundImage")));
            this.btnPerson.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPerson.Caption = "选择";
            this.btnPerson.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnPerson.DownImage = ((System.Drawing.Image)(resources.GetObject("btnPerson.DownImage")));
            this.btnPerson.Location = new System.Drawing.Point(413, 155);
            this.btnPerson.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnPerson.MoveImage")));
            this.btnPerson.Name = "btnPerson";
            this.btnPerson.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnPerson.NormalImage")));
            this.btnPerson.Size = new System.Drawing.Size(60, 26);
            this.btnPerson.TabIndex = 80;
            this.btnPerson.Click += new System.EventHandler(this.btnPerson_Click);
            // 
            // btnOrganization
            // 
            this.btnOrganization.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOrganization.BackgroundImage")));
            this.btnOrganization.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOrganization.Caption = "选择";
            this.btnOrganization.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOrganization.DownImage = ((System.Drawing.Image)(resources.GetObject("btnOrganization.DownImage")));
            this.btnOrganization.Location = new System.Drawing.Point(413, 94);
            this.btnOrganization.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnOrganization.MoveImage")));
            this.btnOrganization.Name = "btnOrganization";
            this.btnOrganization.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnOrganization.NormalImage")));
            this.btnOrganization.Size = new System.Drawing.Size(60, 26);
            this.btnOrganization.TabIndex = 79;
            this.btnOrganization.Click += new System.EventHandler(this.btnOrganization_Click);
            // 
            // rtbPerson
            // 
            this.rtbPerson.BackColor = System.Drawing.Color.White;
            this.rtbPerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbPerson.Location = new System.Drawing.Point(92, 138);
            this.rtbPerson.Name = "rtbPerson";
            this.rtbPerson.ReadOnly = true;
            this.rtbPerson.Size = new System.Drawing.Size(311, 55);
            this.rtbPerson.TabIndex = 78;
            this.rtbPerson.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 77;
            this.label2.Text = "接收人员：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 76;
            this.label1.Text = "接收组织：";
            // 
            // rtbOrganization
            // 
            this.rtbOrganization.BackColor = System.Drawing.Color.White;
            this.rtbOrganization.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbOrganization.Location = new System.Drawing.Point(92, 77);
            this.rtbOrganization.Name = "rtbOrganization";
            this.rtbOrganization.ReadOnly = true;
            this.rtbOrganization.Size = new System.Drawing.Size(311, 55);
            this.rtbOrganization.TabIndex = 73;
            this.rtbOrganization.Text = "";
            // 
            // labContent
            // 
            this.labContent.AutoSize = true;
            this.labContent.Location = new System.Drawing.Point(40, 211);
            this.labContent.Name = "labContent";
            this.labContent.Size = new System.Drawing.Size(41, 12);
            this.labContent.TabIndex = 71;
            this.labContent.Text = "内容：";
            // 
            // txtTitle
            // 
            this.txtTitle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtTitle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtTitle.BackColor = System.Drawing.Color.Transparent;
            this.txtTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtTitle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTitle.ForeImage = null;
            this.txtTitle.InputtingVerifyCondition = null;
            this.txtTitle.Location = new System.Drawing.Point(91, 17);
            this.txtTitle.MaxLengh = 40;
            this.txtTitle.Multiline = false;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Radius = 3;
            this.txtTitle.ReadOnly = false;
            this.txtTitle.SelectStart = 0;
            this.txtTitle.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtTitle.ShowError = false;
            this.txtTitle.Size = new System.Drawing.Size(375, 23);
            this.txtTitle.TabIndex = 63;
            this.txtTitle.UseSystemPasswordChar = false;
            this.txtTitle.Value = "";
            this.txtTitle.VerifyCondition = null;
            this.txtTitle.VerifyType = null;
            this.txtTitle.VerifyTypeName = null;
            this.txtTitle.WaterMark = null;
            this.txtTitle.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // dtpSTime
            // 
            this.dtpSTime.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpSTime.Location = new System.Drawing.Point(91, 50);
            this.dtpSTime.Name = "dtpSTime";
            this.dtpSTime.ShowFormat = "yyyy-MM-dd";
            this.dtpSTime.Size = new System.Drawing.Size(121, 21);
            this.dtpSTime.TabIndex = 62;
            this.dtpSTime.Value = new System.DateTime(2014, 9, 24, 0, 0, 0, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 61;
            this.label4.Text = "日期：";
            // 
            // labTitle
            // 
            this.labTitle.AutoSize = true;
            this.labTitle.Location = new System.Drawing.Point(16, 22);
            this.labTitle.Name = "labTitle";
            this.labTitle.Size = new System.Drawing.Size(65, 12);
            this.labTitle.TabIndex = 0;
            this.labTitle.Text = "公告标题：";
            // 
            // webContent
            // 
            this.webContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webContent.Location = new System.Drawing.Point(93, 201);
            this.webContent.MinimumSize = new System.Drawing.Size(20, 20);
            this.webContent.Name = "webContent";
            this.webContent.Size = new System.Drawing.Size(921, 165);
            this.webContent.TabIndex = 72;
            this.webContent.Resize += new System.EventHandler(this.webContent_Resize);
            // 
            // tcUsers
            // 
            this.tcUsers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcUsers.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tcUsers.Controls.Add(this.tpUsers);
            this.tcUsers.Location = new System.Drawing.Point(0, 413);
            this.tcUsers.Name = "tcUsers";
            this.tcUsers.SelectedIndex = 0;
            this.tcUsers.Size = new System.Drawing.Size(1030, 148);
            this.tcUsers.TabIndex = 12;
            // 
            // tpUsers
            // 
            this.tpUsers.Controls.Add(this.ucAttr);
            this.tpUsers.Location = new System.Drawing.Point(4, 26);
            this.tpUsers.Name = "tpUsers";
            this.tpUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tpUsers.Size = new System.Drawing.Size(1022, 118);
            this.tpUsers.TabIndex = 0;
            this.tpUsers.Text = "附件信息";
            this.tpUsers.UseVisualStyleBackColor = true;
            // 
            // ucAttr
            // 
            this.ucAttr.Controls.Add(this.dgvAttachment);
            this.ucAttr.Controls.Add(this.dataGridViewEx1);
            this.ucAttr.Location = new System.Drawing.Point(3, 3);
            this.ucAttr.Name = "ucAttr";
            this.ucAttr.Size = new System.Drawing.Size(1007, 118);
            this.ucAttr.TabIndex = 1;
            this.ucAttr.TableName = "";
            this.ucAttr.TableNameKeyValue = "";
            // 
            // dgvAttachment
            // 
            this.dgvAttachment.AllowUserToAddRows = false;
            this.dgvAttachment.AllowUserToDeleteRows = false;
            this.dgvAttachment.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvAttachment.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAttachment.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAttachment.BackgroundColor = System.Drawing.Color.White;
            this.dgvAttachment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAttachment.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAttachment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAttachment.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAttachment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAttachment.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvAttachment.EnableHeadersVisualStyles = false;
            this.dgvAttachment.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvAttachment.IsCheck = true;
            this.dgvAttachment.Location = new System.Drawing.Point(0, 0);
            this.dgvAttachment.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvAttachment.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvAttachment.MergeColumnNames")));
            this.dgvAttachment.MultiSelect = false;
            this.dgvAttachment.Name = "dgvAttachment";
            this.dgvAttachment.ReadOnly = true;
            this.dgvAttachment.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.dgvAttachment.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvAttachment.RowHeadersVisible = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgvAttachment.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvAttachment.RowTemplate.Height = 23;
            this.dgvAttachment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAttachment.ShowCheckBox = true;
            this.dgvAttachment.Size = new System.Drawing.Size(1007, 118);
            this.dgvAttachment.TabIndex = 1;
            // 
            // dataGridViewEx1
            // 
            this.dataGridViewEx1.AllowUserToAddRows = false;
            this.dataGridViewEx1.AllowUserToDeleteRows = false;
            this.dataGridViewEx1.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridViewEx1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewEx1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewEx1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewEx1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewEx1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewEx1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewEx1.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewEx1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewEx1.EnableHeadersVisualStyles = false;
            this.dataGridViewEx1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dataGridViewEx1.IsCheck = true;
            this.dataGridViewEx1.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewEx1.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dataGridViewEx1.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dataGridViewEx1.MergeColumnNames")));
            this.dataGridViewEx1.MultiSelect = false;
            this.dataGridViewEx1.Name = "dataGridViewEx1";
            this.dataGridViewEx1.ReadOnly = true;
            this.dataGridViewEx1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.dataGridViewEx1.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewEx1.RowHeadersVisible = false;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dataGridViewEx1.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewEx1.RowTemplate.Height = 23;
            this.dataGridViewEx1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEx1.ShowCheckBox = true;
            this.dataGridViewEx1.Size = new System.Drawing.Size(1007, 118);
            this.dataGridViewEx1.TabIndex = 1;
            // 
            // cmbUser
            // 
            this.cmbUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbUser.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUser.FormattingEnabled = true;
            this.cmbUser.Location = new System.Drawing.Point(352, 567);
            this.cmbUser.Name = "cmbUser";
            this.cmbUser.Size = new System.Drawing.Size(121, 22);
            this.cmbUser.TabIndex = 75;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(293, 572);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 74;
            this.label6.Text = "发布人：";
            // 
            // cmbOrg
            // 
            this.cmbOrg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbOrg.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbOrg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrg.FormattingEnabled = true;
            this.cmbOrg.Location = new System.Drawing.Point(93, 567);
            this.cmbOrg.Name = "cmbOrg";
            this.cmbOrg.Size = new System.Drawing.Size(149, 22);
            this.cmbOrg.TabIndex = 73;
            this.cmbOrg.SelectionChangeCommitted += new System.EventHandler(this.cmbOrg_SelectionChangeCommitted);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 572);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 72;
            this.label5.Text = "发布部门：";
            // 
            // UCBulletinAddOrEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.tcUsers);
            this.Controls.Add(this.cmbUser);
            this.Controls.Add(this.palTop);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbOrg);
            this.Name = "UCBulletinAddOrEdit";
            this.Size = new System.Drawing.Size(1030, 600);
            this.Load += new System.EventHandler(this.UCBulletinAddOrEdit_Load);
            this.Controls.SetChildIndex(this.cmbOrg, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.palTop, 0);
            this.Controls.SetChildIndex(this.cmbUser, 0);
            this.Controls.SetChildIndex(this.tcUsers, 0);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.palTop.ResumeLayout(false);
            this.palTop.PerformLayout();
            this.tcUsers.ResumeLayout(false);
            this.tpUsers.ResumeLayout(false);
            this.ucAttr.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttachment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel palTop;
        private System.Windows.Forms.Label labTitle;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpSTime;
        private System.Windows.Forms.Label label4;
        private ServiceStationClient.ComponentUI.TextBoxEx txtTitle;
        private System.Windows.Forms.Label labContent;
        private System.Windows.Forms.WebBrowser webContent;
        private System.Windows.Forms.RichTextBox rtbOrganization;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbUser;
        private System.Windows.Forms.Label label6;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbOrg;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox rtbPerson;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.ButtonEx btnPerson;
        private ServiceStationClient.ComponentUI.ButtonEx btnOrganization;
        private ServiceStationClient.ComponentUI.TabControlEx tcUsers;
        private System.Windows.Forms.TabPage tpUsers;
        private UCAttachment ucAttr;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvAttachment;
        private ServiceStationClient.ComponentUI.DataGridViewEx dataGridViewEx1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label3;
    }
}
