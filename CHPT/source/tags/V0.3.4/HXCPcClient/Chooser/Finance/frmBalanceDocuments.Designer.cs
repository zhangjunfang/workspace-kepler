namespace HXCPcClient.Chooser
{
    partial class frmBalanceDocuments
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBalanceDocuments));
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.txtCheckNumber = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtOrderNum = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.dtpBillsEndDate = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.dtpBillsStartDate = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.dtpEndDate = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.dtpStartDate = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvData = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.btnCheckAll = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClearAll = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnOK = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnCancel = new ServiceStationClient.ComponentUI.ButtonEx();
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBillsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBillsType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBillsCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBalanceMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWaitMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReceivablesDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReceiptNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlContainer.SuspendLayout();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.btnCancel);
            this.pnlContainer.Controls.Add(this.btnOK);
            this.pnlContainer.Controls.Add(this.btnClearAll);
            this.pnlContainer.Controls.Add(this.btnCheckAll);
            this.pnlContainer.Controls.Add(this.dgvData);
            this.pnlContainer.Controls.Add(this.panelEx1);
            this.pnlContainer.Size = new System.Drawing.Size(743, 371);
            // 
            // panelEx1
            // 
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx1.BorderWidth = 1;
            this.panelEx1.Controls.Add(this.txtCheckNumber);
            this.panelEx1.Controls.Add(this.txtOrderNum);
            this.panelEx1.Controls.Add(this.dtpBillsEndDate);
            this.panelEx1.Controls.Add(this.dtpBillsStartDate);
            this.panelEx1.Controls.Add(this.dtpEndDate);
            this.panelEx1.Controls.Add(this.dtpStartDate);
            this.panelEx1.Controls.Add(this.btnClear);
            this.panelEx1.Controls.Add(this.btnSearch);
            this.panelEx1.Controls.Add(this.label6);
            this.panelEx1.Controls.Add(this.label5);
            this.panelEx1.Controls.Add(this.label4);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.lblDate);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Curvature = 0;
            this.panelEx1.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx1.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx1.Location = new System.Drawing.Point(3, 3);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(729, 97);
            this.panelEx1.TabIndex = 0;
            // 
            // txtCheckNumber
            // 
            this.txtCheckNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtCheckNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtCheckNumber.BackColor = System.Drawing.Color.Transparent;
            this.txtCheckNumber.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtCheckNumber.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtCheckNumber.ForeImage = null;
            this.txtCheckNumber.Location = new System.Drawing.Point(462, 56);
            this.txtCheckNumber.MaxLengh = 32767;
            this.txtCheckNumber.Multiline = false;
            this.txtCheckNumber.Name = "txtCheckNumber";
            this.txtCheckNumber.Radius = 3;
            this.txtCheckNumber.ReadOnly = false;
            this.txtCheckNumber.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtCheckNumber.ShowError = false;
            this.txtCheckNumber.Size = new System.Drawing.Size(139, 23);
            this.txtCheckNumber.TabIndex = 13;
            this.txtCheckNumber.UseSystemPasswordChar = false;
            this.txtCheckNumber.Value = "";
            this.txtCheckNumber.VerifyCondition = null;
            this.txtCheckNumber.VerifyType = null;
            this.txtCheckNumber.WaterMark = null;
            this.txtCheckNumber.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtOrderNum
            // 
            this.txtOrderNum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtOrderNum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtOrderNum.BackColor = System.Drawing.Color.Transparent;
            this.txtOrderNum.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtOrderNum.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtOrderNum.ForeImage = null;
            this.txtOrderNum.Location = new System.Drawing.Point(462, 22);
            this.txtOrderNum.MaxLengh = 32767;
            this.txtOrderNum.Multiline = false;
            this.txtOrderNum.Name = "txtOrderNum";
            this.txtOrderNum.Radius = 3;
            this.txtOrderNum.ReadOnly = false;
            this.txtOrderNum.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtOrderNum.ShowError = false;
            this.txtOrderNum.Size = new System.Drawing.Size(139, 23);
            this.txtOrderNum.TabIndex = 12;
            this.txtOrderNum.UseSystemPasswordChar = false;
            this.txtOrderNum.Value = "";
            this.txtOrderNum.VerifyCondition = null;
            this.txtOrderNum.VerifyType = null;
            this.txtOrderNum.WaterMark = null;
            this.txtOrderNum.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // dtpBillsEndDate
            // 
            this.dtpBillsEndDate.Location = new System.Drawing.Point(238, 57);
            this.dtpBillsEndDate.Name = "dtpBillsEndDate";
            this.dtpBillsEndDate.ShowFormat = "yyyy年MM月dd日";
            this.dtpBillsEndDate.Size = new System.Drawing.Size(120, 21);
            this.dtpBillsEndDate.TabIndex = 11;
            this.dtpBillsEndDate.Value = new System.DateTime(2014, 10, 10, 15, 52, 0, 527);
            // 
            // dtpBillsStartDate
            // 
            this.dtpBillsStartDate.Location = new System.Drawing.Point(91, 57);
            this.dtpBillsStartDate.Name = "dtpBillsStartDate";
            this.dtpBillsStartDate.ShowFormat = "yyyy年MM月dd日";
            this.dtpBillsStartDate.Size = new System.Drawing.Size(124, 21);
            this.dtpBillsStartDate.TabIndex = 10;
            this.dtpBillsStartDate.Value = new System.DateTime(2014, 10, 10, 15, 51, 55, 992);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(238, 23);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.ShowFormat = "yyyy年MM月dd日";
            this.dtpEndDate.Size = new System.Drawing.Size(120, 21);
            this.dtpEndDate.TabIndex = 9;
            this.dtpEndDate.Value = new System.DateTime(2014, 10, 10, 15, 51, 49, 784);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(91, 23);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.ShowFormat = "yyyy年MM月dd日";
            this.dtpStartDate.Size = new System.Drawing.Size(124, 21);
            this.dtpStartDate.TabIndex = 8;
            this.dtpStartDate.Value = new System.DateTime(2014, 10, 10, 15, 51, 38, 47);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(652, 19);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 26);
            this.btnClear.TabIndex = 7;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.DownImage")));
            this.btnSearch.Location = new System.Drawing.Point(652, 56);
            this.btnSearch.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.MoveImage")));
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.NormalImage")));
            this.btnSearch.Size = new System.Drawing.Size(60, 26);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(403, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "发票号：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(415, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "单号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(221, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(221, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "-";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(20, 61);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(65, 12);
            this.lblDate.TabIndex = 1;
            this.lblDate.Text = "收款日期：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "日期：";
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvData.BackgroundColor = System.Drawing.Color.White;
            this.dgvData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.colID,
            this.colBillsName,
            this.colBillsType,
            this.colBillsCode,
            this.colTotalMoney,
            this.colBalanceMoney,
            this.colWaitMoney,
            this.colReceivablesDate,
            this.colReceiptNO});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvData.EnableHeadersVisualStyles = false;
            this.dgvData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgvData.IsCheck = true;
            this.dgvData.Location = new System.Drawing.Point(11, 106);
            this.dgvData.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvData.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvData.MergeColumnNames")));
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvData.RowHeadersVisible = false;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvData.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.ShowCheckBox = true;
            this.dgvData.Size = new System.Drawing.Size(721, 222);
            this.dgvData.TabIndex = 1;
            // 
            // btnCheckAll
            // 
            this.btnCheckAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCheckAll.BackgroundImage")));
            this.btnCheckAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCheckAll.Caption = "全选";
            this.btnCheckAll.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCheckAll.DownImage = ((System.Drawing.Image)(resources.GetObject("btnCheckAll.DownImage")));
            this.btnCheckAll.Location = new System.Drawing.Point(413, 334);
            this.btnCheckAll.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnCheckAll.MoveImage")));
            this.btnCheckAll.Name = "btnCheckAll";
            this.btnCheckAll.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnCheckAll.NormalImage")));
            this.btnCheckAll.Size = new System.Drawing.Size(60, 26);
            this.btnCheckAll.TabIndex = 2;
            this.btnCheckAll.Click += new System.EventHandler(this.btnCheckAll_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearAll.BackgroundImage")));
            this.btnClearAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearAll.Caption = "全清";
            this.btnClearAll.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClearAll.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClearAll.DownImage")));
            this.btnClearAll.Location = new System.Drawing.Point(495, 334);
            this.btnClearAll.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClearAll.MoveImage")));
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClearAll.NormalImage")));
            this.btnClearAll.Size = new System.Drawing.Size(60, 26);
            this.btnClearAll.TabIndex = 3;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.Caption = "确定";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOK.DownImage = ((System.Drawing.Image)(resources.GetObject("btnOK.DownImage")));
            this.btnOK.Location = new System.Drawing.Point(573, 334);
            this.btnOK.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnOK.MoveImage")));
            this.btnOK.Name = "btnOK";
            this.btnOK.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnOK.NormalImage")));
            this.btnOK.Size = new System.Drawing.Size(60, 26);
            this.btnOK.TabIndex = 4;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Caption = "取消";
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.DownImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.DownImage")));
            this.btnCancel.Location = new System.Drawing.Point(655, 334);
            this.btnCancel.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.MoveImage")));
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.NormalImage")));
            this.btnCancel.Size = new System.Drawing.Size(60, 26);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 18;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Width = 18;
            // 
            // colID
            // 
            this.colID.HeaderText = "ID";
            this.colID.Name = "colID";
            this.colID.ReadOnly = true;
            this.colID.Visible = false;
            // 
            // colBillsName
            // 
            this.colBillsName.HeaderText = "单据名称";
            this.colBillsName.Name = "colBillsName";
            this.colBillsName.ReadOnly = true;
            // 
            // colBillsType
            // 
            this.colBillsType.HeaderText = "单据类型";
            this.colBillsType.Name = "colBillsType";
            this.colBillsType.ReadOnly = true;
            // 
            // colBillsCode
            // 
            this.colBillsCode.HeaderText = "单据编号";
            this.colBillsCode.Name = "colBillsCode";
            this.colBillsCode.ReadOnly = true;
            // 
            // colTotalMoney
            // 
            this.colTotalMoney.HeaderText = "总金额";
            this.colTotalMoney.Name = "colTotalMoney";
            this.colTotalMoney.ReadOnly = true;
            // 
            // colBalanceMoney
            // 
            this.colBalanceMoney.HeaderText = "已结算金额";
            this.colBalanceMoney.Name = "colBalanceMoney";
            this.colBalanceMoney.ReadOnly = true;
            // 
            // colWaitMoney
            // 
            this.colWaitMoney.HeaderText = "未结算金额";
            this.colWaitMoney.Name = "colWaitMoney";
            this.colWaitMoney.ReadOnly = true;
            // 
            // colReceivablesDate
            // 
            this.colReceivablesDate.HeaderText = "收款日期";
            this.colReceivablesDate.Name = "colReceivablesDate";
            this.colReceivablesDate.ReadOnly = true;
            // 
            // colReceiptNO
            // 
            this.colReceiptNO.HeaderText = "发票号";
            this.colReceiptNO.Name = "colReceiptNO";
            this.colReceiptNO.ReadOnly = true;
            // 
            // frmBalanceDocuments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 402);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmBalanceDocuments";
            this.Text = "请选择单据";
            this.Load += new System.EventHandler(this.frmBalanceDocuments_Load);
            this.pnlContainer.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private ServiceStationClient.ComponentUI.ButtonEx btnCancel;
        private ServiceStationClient.ComponentUI.ButtonEx btnOK;
        private ServiceStationClient.ComponentUI.ButtonEx btnClearAll;
        private ServiceStationClient.ComponentUI.ButtonEx btnCheckAll;
        private ServiceStationClient.ComponentUI.TextBoxEx txtCheckNumber;
        private ServiceStationClient.ComponentUI.TextBoxEx txtOrderNum;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpBillsEndDate;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpBillsStartDate;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpEndDate;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpStartDate;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label label1;
        protected ServiceStationClient.ComponentUI.DataGridViewEx dgvData;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        protected System.Windows.Forms.DataGridViewTextBoxColumn colID;
        protected System.Windows.Forms.DataGridViewTextBoxColumn colBillsName;
        protected System.Windows.Forms.DataGridViewTextBoxColumn colBillsType;
        protected System.Windows.Forms.DataGridViewTextBoxColumn colBillsCode;
        protected System.Windows.Forms.DataGridViewTextBoxColumn colTotalMoney;
        protected System.Windows.Forms.DataGridViewTextBoxColumn colBalanceMoney;
        protected System.Windows.Forms.DataGridViewTextBoxColumn colWaitMoney;
        protected System.Windows.Forms.DataGridViewTextBoxColumn colReceivablesDate;
        protected System.Windows.Forms.DataGridViewTextBoxColumn colReceiptNO;
    }
}