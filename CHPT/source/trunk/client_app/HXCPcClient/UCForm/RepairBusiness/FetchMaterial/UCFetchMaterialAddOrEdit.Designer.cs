namespace HXCPcClient.UCForm.RepairBusiness.FetchMaterial
{
    partial class UCFetchMaterialAddOrEdit
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCFetchMaterialAddOrEdit));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.palTop = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpMakeOrderTime = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.labMakeOrderTime = new System.Windows.Forms.Label();
            this.labReserveNoS = new System.Windows.Forms.Label();
            this.labReserveNo = new System.Windows.Forms.Label();
            this.palInfo = new System.Windows.Forms.Panel();
            this.txtFetchOpid = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.labFetchOpid = new System.Windows.Forms.Label();
            this.txtOrder = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labOrder = new System.Windows.Forms.Label();
            this.txtRemark = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labFaultDesc = new System.Windows.Forms.Label();
            this.txtCustomNO = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.txtCarType = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.txtCarNO = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtContactPhone = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtContact = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtCustomName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labContactPhone = new System.Windows.Forms.Label();
            this.labContact = new System.Windows.Forms.Label();
            this.labCustomName = new System.Windows.Forms.Label();
            this.labCustomNO = new System.Windows.Forms.Label();
            this.labCarType = new System.Windows.Forms.Label();
            this.labCarNO = new System.Windows.Forms.Label();
            this.labReserveInfo = new System.Windows.Forms.Label();
            this.labCustomerInfo = new System.Windows.Forms.Label();
            this.labCarInfo = new System.Windows.Forms.Label();
            this.palBottom = new System.Windows.Forms.Panel();
            this.dgvMaterials = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.Mcheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.parts_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parts_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.three_warranty = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inventory_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.picking_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.received_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.notFetchNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.warehouse = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.whether_imported = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drawn_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vehicle_brand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.material_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fetch_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mdata_source = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctmParts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addParts = new System.Windows.Forms.ToolStripMenuItem();
            this.editParts = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteParts = new System.Windows.Forms.ToolStripMenuItem();
            this.palbottom1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.labCreatePersonS = new System.Windows.Forms.Label();
            this.labCreatePerson = new System.Windows.Forms.Label();
            this.cboOrgId = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cobYHandle = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.labDepart = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.contextMenuM = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.palTop.SuspendLayout();
            this.palInfo.SuspendLayout();
            this.palBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterials)).BeginInit();
            this.ctmParts.SuspendLayout();
            this.palbottom1.SuspendLayout();
            this.contextMenuM.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // palTop
            // 
            this.palTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.palTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.palTop.Controls.Add(this.label4);
            this.palTop.Controls.Add(this.dtpMakeOrderTime);
            this.palTop.Controls.Add(this.labMakeOrderTime);
            this.palTop.Controls.Add(this.labReserveNoS);
            this.palTop.Controls.Add(this.labReserveNo);
            this.palTop.Location = new System.Drawing.Point(0, 34);
            this.palTop.Name = "palTop";
            this.palTop.Size = new System.Drawing.Size(1030, 40);
            this.palTop.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 32;
            this.label4.Text = "单据信息";
            // 
            // dtpMakeOrderTime
            // 
            this.dtpMakeOrderTime.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpMakeOrderTime.Location = new System.Drawing.Point(677, 17);
            this.dtpMakeOrderTime.Name = "dtpMakeOrderTime";
            this.dtpMakeOrderTime.ShowFormat = "yyyy-MM-dd HH:mm";
            this.dtpMakeOrderTime.Size = new System.Drawing.Size(121, 21);
            this.dtpMakeOrderTime.TabIndex = 31;
            this.dtpMakeOrderTime.Value = new System.DateTime(2014, 9, 24, 0, 0, 0, 0);
            // 
            // labMakeOrderTime
            // 
            this.labMakeOrderTime.AutoSize = true;
            this.labMakeOrderTime.Location = new System.Drawing.Point(608, 21);
            this.labMakeOrderTime.Name = "labMakeOrderTime";
            this.labMakeOrderTime.Size = new System.Drawing.Size(65, 12);
            this.labMakeOrderTime.TabIndex = 30;
            this.labMakeOrderTime.Text = "领料时间：";
            // 
            // labReserveNoS
            // 
            this.labReserveNoS.AutoSize = true;
            this.labReserveNoS.Location = new System.Drawing.Point(230, 21);
            this.labReserveNoS.Name = "labReserveNoS";
            this.labReserveNoS.Size = new System.Drawing.Size(0, 12);
            this.labReserveNoS.TabIndex = 1;
            // 
            // labReserveNo
            // 
            this.labReserveNo.AutoSize = true;
            this.labReserveNo.Location = new System.Drawing.Point(163, 21);
            this.labReserveNo.Name = "labReserveNo";
            this.labReserveNo.Size = new System.Drawing.Size(65, 12);
            this.labReserveNo.TabIndex = 0;
            this.labReserveNo.Text = "领料单号：";
            // 
            // palInfo
            // 
            this.palInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.palInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.palInfo.Controls.Add(this.txtFetchOpid);
            this.palInfo.Controls.Add(this.labFetchOpid);
            this.palInfo.Controls.Add(this.txtOrder);
            this.palInfo.Controls.Add(this.labOrder);
            this.palInfo.Controls.Add(this.txtRemark);
            this.palInfo.Controls.Add(this.labFaultDesc);
            this.palInfo.Controls.Add(this.txtCustomNO);
            this.palInfo.Controls.Add(this.txtCarType);
            this.palInfo.Controls.Add(this.txtCarNO);
            this.palInfo.Controls.Add(this.label3);
            this.palInfo.Controls.Add(this.label2);
            this.palInfo.Controls.Add(this.label1);
            this.palInfo.Controls.Add(this.txtContactPhone);
            this.palInfo.Controls.Add(this.txtContact);
            this.palInfo.Controls.Add(this.txtCustomName);
            this.palInfo.Controls.Add(this.labContactPhone);
            this.palInfo.Controls.Add(this.labContact);
            this.palInfo.Controls.Add(this.labCustomName);
            this.palInfo.Controls.Add(this.labCustomNO);
            this.palInfo.Controls.Add(this.labCarType);
            this.palInfo.Controls.Add(this.labCarNO);
            this.palInfo.Controls.Add(this.labReserveInfo);
            this.palInfo.Controls.Add(this.labCustomerInfo);
            this.palInfo.Controls.Add(this.labCarInfo);
            this.palInfo.Location = new System.Drawing.Point(3, 77);
            this.palInfo.Name = "palInfo";
            this.palInfo.Size = new System.Drawing.Size(1024, 147);
            this.palInfo.TabIndex = 7;
            // 
            // txtFetchOpid
            // 
            this.txtFetchOpid.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtFetchOpid.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFetchOpid.Location = new System.Drawing.Point(91, 113);
            this.txtFetchOpid.Name = "txtFetchOpid";
            this.txtFetchOpid.ReadOnly = false;
            this.txtFetchOpid.Size = new System.Drawing.Size(117, 24);
            this.txtFetchOpid.TabIndex = 159;
            this.txtFetchOpid.ToolTipTitle = "";
            this.txtFetchOpid.ChooserClick += new System.EventHandler(this.txtFetchOpid_ChooserClick);
            // 
            // labFetchOpid
            // 
            this.labFetchOpid.AutoSize = true;
            this.labFetchOpid.Location = new System.Drawing.Point(34, 118);
            this.labFetchOpid.Name = "labFetchOpid";
            this.labFetchOpid.Size = new System.Drawing.Size(53, 12);
            this.labFetchOpid.TabIndex = 158;
            this.labFetchOpid.Text = "领料人：";
            // 
            // txtOrder
            // 
            this.txtOrder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtOrder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtOrder.BackColor = System.Drawing.Color.Transparent;
            this.txtOrder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtOrder.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtOrder.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOrder.ForeImage = null;
            this.txtOrder.InputtingVerifyCondition = null;
            this.txtOrder.Location = new System.Drawing.Point(309, 113);
            this.txtOrder.MaxLengh = 40;
            this.txtOrder.Multiline = false;
            this.txtOrder.Name = "txtOrder";
            this.txtOrder.Radius = 3;
            this.txtOrder.ReadOnly = false;
            this.txtOrder.SelectStart = 0;
            this.txtOrder.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtOrder.ShowError = false;
            this.txtOrder.Size = new System.Drawing.Size(120, 23);
            this.txtOrder.TabIndex = 157;
            this.txtOrder.UseSystemPasswordChar = false;
            this.txtOrder.Value = "";
            this.txtOrder.VerifyCondition = null;
            this.txtOrder.VerifyType = null;
            this.txtOrder.VerifyTypeName = null;
            this.txtOrder.WaterMark = null;
            this.txtOrder.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labOrder
            // 
            this.labOrder.AutoSize = true;
            this.labOrder.Location = new System.Drawing.Point(238, 119);
            this.labOrder.Name = "labOrder";
            this.labOrder.Size = new System.Drawing.Size(65, 12);
            this.labOrder.TabIndex = 156;
            this.labOrder.Text = "维修单号：";
            // 
            // txtRemark
            // 
            this.txtRemark.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtRemark.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtRemark.BackColor = System.Drawing.Color.Transparent;
            this.txtRemark.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtRemark.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtRemark.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRemark.ForeImage = null;
            this.txtRemark.InputtingVerifyCondition = null;
            this.txtRemark.Location = new System.Drawing.Point(532, 113);
            this.txtRemark.MaxLengh = 200;
            this.txtRemark.Multiline = false;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Radius = 3;
            this.txtRemark.ReadOnly = false;
            this.txtRemark.SelectStart = 0;
            this.txtRemark.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtRemark.ShowError = false;
            this.txtRemark.Size = new System.Drawing.Size(358, 23);
            this.txtRemark.TabIndex = 138;
            this.txtRemark.UseSystemPasswordChar = false;
            this.txtRemark.Value = "";
            this.txtRemark.VerifyCondition = null;
            this.txtRemark.VerifyType = null;
            this.txtRemark.VerifyTypeName = null;
            this.txtRemark.WaterMark = null;
            this.txtRemark.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labFaultDesc
            // 
            this.labFaultDesc.AutoSize = true;
            this.labFaultDesc.Location = new System.Drawing.Point(488, 119);
            this.labFaultDesc.Name = "labFaultDesc";
            this.labFaultDesc.Size = new System.Drawing.Size(41, 12);
            this.labFaultDesc.TabIndex = 137;
            this.labFaultDesc.Text = "备注：";
            // 
            // txtCustomNO
            // 
            this.txtCustomNO.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtCustomNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCustomNO.Location = new System.Drawing.Point(91, 64);
            this.txtCustomNO.Name = "txtCustomNO";
            this.txtCustomNO.ReadOnly = false;
            this.txtCustomNO.Size = new System.Drawing.Size(117, 24);
            this.txtCustomNO.TabIndex = 65;
            this.txtCustomNO.ToolTipTitle = "";
            this.txtCustomNO.ChooserClick += new System.EventHandler(this.txtCustomNO_ChooserClick);
            // 
            // txtCarType
            // 
            this.txtCarType.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtCarType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCarType.Location = new System.Drawing.Point(309, 20);
            this.txtCarType.Name = "txtCarType";
            this.txtCarType.ReadOnly = true;
            this.txtCarType.Size = new System.Drawing.Size(120, 24);
            this.txtCarType.TabIndex = 64;
            this.txtCarType.ToolTipTitle = "";
            this.txtCarType.ChooserClick += new System.EventHandler(this.txtCarType_ChooserClick);
            // 
            // txtCarNO
            // 
            this.txtCarNO.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtCarNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCarNO.Location = new System.Drawing.Point(91, 20);
            this.txtCarNO.Name = "txtCarNO";
            this.txtCarNO.ReadOnly = false;
            this.txtCarNO.Size = new System.Drawing.Size(117, 24);
            this.txtCarNO.TabIndex = 63;
            this.txtCarNO.ToolTipTitle = "";
            this.txtCarNO.ChooserClick += new System.EventHandler(this.txtCarNO_ChooserClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(292, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 62;
            this.label3.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(77, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 61;
            this.label2.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(77, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 60;
            this.label1.Text = "*";
            // 
            // txtContactPhone
            // 
            this.txtContactPhone.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtContactPhone.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtContactPhone.BackColor = System.Drawing.Color.Transparent;
            this.txtContactPhone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtContactPhone.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtContactPhone.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtContactPhone.ForeImage = null;
            this.txtContactPhone.InputtingVerifyCondition = null;
            this.txtContactPhone.Location = new System.Drawing.Point(770, 66);
            this.txtContactPhone.MaxLengh = 11;
            this.txtContactPhone.Multiline = false;
            this.txtContactPhone.Name = "txtContactPhone";
            this.txtContactPhone.Radius = 3;
            this.txtContactPhone.ReadOnly = false;
            this.txtContactPhone.SelectStart = 0;
            this.txtContactPhone.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtContactPhone.ShowError = false;
            this.txtContactPhone.Size = new System.Drawing.Size(120, 23);
            this.txtContactPhone.TabIndex = 39;
            this.txtContactPhone.UseSystemPasswordChar = false;
            this.txtContactPhone.Value = "";
            this.txtContactPhone.VerifyCondition = null;
            this.txtContactPhone.VerifyType = null;
            this.txtContactPhone.VerifyTypeName = null;
            this.txtContactPhone.WaterMark = null;
            this.txtContactPhone.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtContact
            // 
            this.txtContact.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtContact.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtContact.BackColor = System.Drawing.Color.Transparent;
            this.txtContact.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtContact.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtContact.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtContact.ForeImage = null;
            this.txtContact.InputtingVerifyCondition = null;
            this.txtContact.Location = new System.Drawing.Point(532, 65);
            this.txtContact.MaxLengh = 20;
            this.txtContact.Multiline = false;
            this.txtContact.Name = "txtContact";
            this.txtContact.Radius = 3;
            this.txtContact.ReadOnly = false;
            this.txtContact.SelectStart = 0;
            this.txtContact.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtContact.ShowError = false;
            this.txtContact.Size = new System.Drawing.Size(120, 23);
            this.txtContact.TabIndex = 38;
            this.txtContact.UseSystemPasswordChar = false;
            this.txtContact.Value = "";
            this.txtContact.VerifyCondition = null;
            this.txtContact.VerifyType = null;
            this.txtContact.VerifyTypeName = null;
            this.txtContact.WaterMark = null;
            this.txtContact.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtCustomName
            // 
            this.txtCustomName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtCustomName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtCustomName.BackColor = System.Drawing.Color.Transparent;
            this.txtCustomName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtCustomName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtCustomName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCustomName.ForeImage = null;
            this.txtCustomName.InputtingVerifyCondition = null;
            this.txtCustomName.Location = new System.Drawing.Point(309, 65);
            this.txtCustomName.MaxLengh = 100;
            this.txtCustomName.Multiline = false;
            this.txtCustomName.Name = "txtCustomName";
            this.txtCustomName.Radius = 3;
            this.txtCustomName.ReadOnly = false;
            this.txtCustomName.SelectStart = 0;
            this.txtCustomName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtCustomName.ShowError = false;
            this.txtCustomName.Size = new System.Drawing.Size(120, 23);
            this.txtCustomName.TabIndex = 37;
            this.txtCustomName.UseSystemPasswordChar = false;
            this.txtCustomName.Value = "";
            this.txtCustomName.VerifyCondition = null;
            this.txtCustomName.VerifyType = null;
            this.txtCustomName.VerifyTypeName = null;
            this.txtCustomName.WaterMark = null;
            this.txtCustomName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labContactPhone
            // 
            this.labContactPhone.AutoSize = true;
            this.labContactPhone.Location = new System.Drawing.Point(687, 71);
            this.labContactPhone.Name = "labContactPhone";
            this.labContactPhone.Size = new System.Drawing.Size(77, 12);
            this.labContactPhone.TabIndex = 35;
            this.labContactPhone.Text = "联系人手机：";
            // 
            // labContact
            // 
            this.labContact.AutoSize = true;
            this.labContact.Location = new System.Drawing.Point(476, 71);
            this.labContact.Name = "labContact";
            this.labContact.Size = new System.Drawing.Size(53, 12);
            this.labContact.TabIndex = 34;
            this.labContact.Text = "联系人：";
            // 
            // labCustomName
            // 
            this.labCustomName.AutoSize = true;
            this.labCustomName.Location = new System.Drawing.Point(236, 71);
            this.labCustomName.Name = "labCustomName";
            this.labCustomName.Size = new System.Drawing.Size(65, 12);
            this.labCustomName.TabIndex = 33;
            this.labCustomName.Text = "客户名称：";
            // 
            // labCustomNO
            // 
            this.labCustomNO.AutoSize = true;
            this.labCustomNO.Location = new System.Drawing.Point(22, 71);
            this.labCustomNO.Name = "labCustomNO";
            this.labCustomNO.Size = new System.Drawing.Size(65, 12);
            this.labCustomNO.TabIndex = 32;
            this.labCustomNO.Text = "客户编码：";
            // 
            // labCarType
            // 
            this.labCarType.AutoSize = true;
            this.labCarType.Location = new System.Drawing.Point(260, 27);
            this.labCarType.Name = "labCarType";
            this.labCarType.Size = new System.Drawing.Size(41, 12);
            this.labCarType.TabIndex = 22;
            this.labCarType.Text = "车型：";
            // 
            // labCarNO
            // 
            this.labCarNO.AutoSize = true;
            this.labCarNO.Location = new System.Drawing.Point(35, 27);
            this.labCarNO.Name = "labCarNO";
            this.labCarNO.Size = new System.Drawing.Size(53, 12);
            this.labCarNO.TabIndex = 5;
            this.labCarNO.Text = "车牌号：";
            // 
            // labReserveInfo
            // 
            this.labReserveInfo.AutoSize = true;
            this.labReserveInfo.Location = new System.Drawing.Point(6, 95);
            this.labReserveInfo.Name = "labReserveInfo";
            this.labReserveInfo.Size = new System.Drawing.Size(53, 12);
            this.labReserveInfo.TabIndex = 2;
            this.labReserveInfo.Text = "领料信息";
            // 
            // labCustomerInfo
            // 
            this.labCustomerInfo.AutoSize = true;
            this.labCustomerInfo.Location = new System.Drawing.Point(5, 49);
            this.labCustomerInfo.Name = "labCustomerInfo";
            this.labCustomerInfo.Size = new System.Drawing.Size(53, 12);
            this.labCustomerInfo.TabIndex = 1;
            this.labCustomerInfo.Text = "客户信息";
            // 
            // labCarInfo
            // 
            this.labCarInfo.AutoSize = true;
            this.labCarInfo.Location = new System.Drawing.Point(5, 5);
            this.labCarInfo.Name = "labCarInfo";
            this.labCarInfo.Size = new System.Drawing.Size(53, 12);
            this.labCarInfo.TabIndex = 0;
            this.labCarInfo.Text = "车辆信息";
            // 
            // palBottom
            // 
            this.palBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.palBottom.Controls.Add(this.dgvMaterials);
            this.palBottom.Location = new System.Drawing.Point(3, 227);
            this.palBottom.Name = "palBottom";
            this.palBottom.Size = new System.Drawing.Size(1024, 278);
            this.palBottom.TabIndex = 8;
            // 
            // dgvMaterials
            // 
            this.dgvMaterials.AllowUserToAddRows = false;
            this.dgvMaterials.AllowUserToDeleteRows = false;
            this.dgvMaterials.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvMaterials.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMaterials.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMaterials.BackgroundColor = System.Drawing.Color.White;
            this.dgvMaterials.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMaterials.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMaterials.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaterials.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Mcheck,
            this.parts_code,
            this.parts_name,
            this.three_warranty,
            this.unit,
            this.quantity,
            this.inventory_num,
            this.picking_num,
            this.received_num,
            this.notFetchNum,
            this.warehouse,
            this.whether_imported,
            this.drawn_no,
            this.vehicle_brand,
            this.remarks,
            this.material_id,
            this.fetch_id,
            this.Mdata_source});
            this.dgvMaterials.ContextMenuStrip = this.ctmParts;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMaterials.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvMaterials.EnableHeadersVisualStyles = false;
            this.dgvMaterials.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvMaterials.IsCheck = true;
            this.dgvMaterials.Location = new System.Drawing.Point(3, 3);
            this.dgvMaterials.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvMaterials.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvMaterials.MergeColumnNames")));
            this.dgvMaterials.MultiSelect = false;
            this.dgvMaterials.Name = "dgvMaterials";
            this.dgvMaterials.ReadOnly = true;
            this.dgvMaterials.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.dgvMaterials.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvMaterials.RowHeadersVisible = false;
            this.dgvMaterials.RowHeadersWidth = 30;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgvMaterials.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvMaterials.RowTemplate.Height = 23;
            this.dgvMaterials.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMaterials.ShowCheckBox = true;
            this.dgvMaterials.Size = new System.Drawing.Size(1018, 272);
            this.dgvMaterials.TabIndex = 15;
            this.dgvMaterials.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMaterials_CellDoubleClick);
            this.dgvMaterials.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMaterials_CellValueChanged);
            // 
            // Mcheck
            // 
            this.Mcheck.HeaderText = "";
            this.Mcheck.MinimumWidth = 30;
            this.Mcheck.Name = "Mcheck";
            this.Mcheck.ReadOnly = true;
            this.Mcheck.Width = 30;
            // 
            // parts_code
            // 
            this.parts_code.DataPropertyName = "parts_code";
            this.parts_code.HeaderText = "配件编码";
            this.parts_code.Name = "parts_code";
            this.parts_code.ReadOnly = true;
            this.parts_code.Width = 90;
            // 
            // parts_name
            // 
            this.parts_name.DataPropertyName = "parts_name";
            this.parts_name.HeaderText = "配件名称";
            this.parts_name.Name = "parts_name";
            this.parts_name.ReadOnly = true;
            this.parts_name.Width = 90;
            // 
            // three_warranty
            // 
            this.three_warranty.DataPropertyName = "three_warranty";
            this.three_warranty.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.three_warranty.DisplayStyleForCurrentCellOnly = true;
            this.three_warranty.HeaderText = "是否三包";
            this.three_warranty.Items.AddRange(new object[] {
            "是",
            "否"});
            this.three_warranty.Name = "three_warranty";
            this.three_warranty.ReadOnly = true;
            this.three_warranty.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.three_warranty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // unit
            // 
            this.unit.DataPropertyName = "unit";
            this.unit.HeaderText = "单位";
            this.unit.Name = "unit";
            this.unit.ReadOnly = true;
            this.unit.Width = 60;
            // 
            // quantity
            // 
            this.quantity.DataPropertyName = "quantity";
            dataGridViewCellStyle3.NullValue = null;
            this.quantity.DefaultCellStyle = dataGridViewCellStyle3;
            this.quantity.HeaderText = "数量";
            this.quantity.Name = "quantity";
            this.quantity.ReadOnly = true;
            this.quantity.Width = 60;
            // 
            // inventory_num
            // 
            this.inventory_num.DataPropertyName = "inventory_num";
            this.inventory_num.HeaderText = "库存数量";
            this.inventory_num.Name = "inventory_num";
            this.inventory_num.ReadOnly = true;
            // 
            // picking_num
            // 
            this.picking_num.DataPropertyName = "picking_num";
            this.picking_num.HeaderText = "应领数量";
            this.picking_num.Name = "picking_num";
            this.picking_num.ReadOnly = true;
            this.picking_num.Width = 90;
            // 
            // received_num
            // 
            this.received_num.DataPropertyName = "received_num";
            this.received_num.HeaderText = "历史已领数量";
            this.received_num.MinimumWidth = 120;
            this.received_num.Name = "received_num";
            this.received_num.ReadOnly = true;
            this.received_num.Width = 120;
            // 
            // notFetchNum
            // 
            this.notFetchNum.HeaderText = "未领数量";
            this.notFetchNum.Name = "notFetchNum";
            this.notFetchNum.ReadOnly = true;
            // 
            // warehouse
            // 
            this.warehouse.DataPropertyName = "warehouse";
            this.warehouse.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.warehouse.DisplayStyleForCurrentCellOnly = true;
            this.warehouse.HeaderText = "仓库";
            this.warehouse.MinimumWidth = 105;
            this.warehouse.Name = "warehouse";
            this.warehouse.ReadOnly = true;
            this.warehouse.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.warehouse.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.warehouse.Width = 105;
            // 
            // whether_imported
            // 
            this.whether_imported.DataPropertyName = "whether_imported";
            this.whether_imported.HeaderText = "进口";
            this.whether_imported.Name = "whether_imported";
            this.whether_imported.ReadOnly = true;
            // 
            // drawn_no
            // 
            this.drawn_no.DataPropertyName = "drawn_no";
            this.drawn_no.HeaderText = "图号";
            this.drawn_no.Name = "drawn_no";
            this.drawn_no.ReadOnly = true;
            this.drawn_no.Width = 80;
            // 
            // vehicle_brand
            // 
            this.vehicle_brand.DataPropertyName = "vehicle_brand";
            this.vehicle_brand.HeaderText = "品牌";
            this.vehicle_brand.Name = "vehicle_brand";
            this.vehicle_brand.ReadOnly = true;
            this.vehicle_brand.Width = 80;
            // 
            // remarks
            // 
            this.remarks.DataPropertyName = "remarks";
            this.remarks.HeaderText = "备注";
            this.remarks.Name = "remarks";
            this.remarks.ReadOnly = true;
            this.remarks.Width = 90;
            // 
            // material_id
            // 
            this.material_id.DataPropertyName = "material_id";
            this.material_id.HeaderText = "material_id";
            this.material_id.Name = "material_id";
            this.material_id.ReadOnly = true;
            this.material_id.Visible = false;
            this.material_id.Width = 10;
            // 
            // fetch_id
            // 
            this.fetch_id.DataPropertyName = "fetch_id";
            this.fetch_id.HeaderText = "fetch_id";
            this.fetch_id.Name = "fetch_id";
            this.fetch_id.ReadOnly = true;
            this.fetch_id.Visible = false;
            // 
            // Mdata_source
            // 
            this.Mdata_source.DataPropertyName = "data_source";
            this.Mdata_source.HeaderText = "data_source";
            this.Mdata_source.Name = "Mdata_source";
            this.Mdata_source.ReadOnly = true;
            this.Mdata_source.Visible = false;
            // 
            // ctmParts
            // 
            this.ctmParts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addParts,
            this.editParts,
            this.deleteParts});
            this.ctmParts.Name = "ctmParts";
            this.ctmParts.Size = new System.Drawing.Size(125, 70);
            // 
            // addParts
            // 
            this.addParts.Image = global::HXCPcClient.Properties.Resources.Add_E;
            this.addParts.Name = "addParts";
            this.addParts.Size = new System.Drawing.Size(124, 22);
            this.addParts.Text = "添加配件";
            this.addParts.Click += new System.EventHandler(this.addParts_Click);
            // 
            // editParts
            // 
            this.editParts.Image = global::HXCPcClient.Properties.Resources.Edit_E;
            this.editParts.Name = "editParts";
            this.editParts.Size = new System.Drawing.Size(124, 22);
            this.editParts.Text = "编辑配件";
            this.editParts.Click += new System.EventHandler(this.editParts_Click);
            // 
            // deleteParts
            // 
            this.deleteParts.Image = global::HXCPcClient.Properties.Resources.Delete_E;
            this.deleteParts.Name = "deleteParts";
            this.deleteParts.Size = new System.Drawing.Size(124, 22);
            this.deleteParts.Text = "删除配件";
            this.deleteParts.Click += new System.EventHandler(this.deleteParts_Click);
            // 
            // palbottom1
            // 
            this.palbottom1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.palbottom1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.palbottom1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.palbottom1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.palbottom1.BorderWidth = 1;
            this.palbottom1.Controls.Add(this.labCreatePersonS);
            this.palbottom1.Controls.Add(this.labCreatePerson);
            this.palbottom1.Controls.Add(this.cboOrgId);
            this.palbottom1.Controls.Add(this.cobYHandle);
            this.palbottom1.Controls.Add(this.labDepart);
            this.palbottom1.Controls.Add(this.label31);
            this.palbottom1.Curvature = 0;
            this.palbottom1.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.palbottom1.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.palbottom1.Location = new System.Drawing.Point(3, 511);
            this.palbottom1.Name = "palbottom1";
            this.palbottom1.Size = new System.Drawing.Size(1027, 30);
            this.palbottom1.TabIndex = 13;
            // 
            // labCreatePersonS
            // 
            this.labCreatePersonS.AutoSize = true;
            this.labCreatePersonS.Location = new System.Drawing.Point(546, 9);
            this.labCreatePersonS.Name = "labCreatePersonS";
            this.labCreatePersonS.Size = new System.Drawing.Size(17, 12);
            this.labCreatePersonS.TabIndex = 162;
            this.labCreatePersonS.Text = "aa";
            // 
            // labCreatePerson
            // 
            this.labCreatePerson.AutoSize = true;
            this.labCreatePerson.Location = new System.Drawing.Point(487, 9);
            this.labCreatePerson.Name = "labCreatePerson";
            this.labCreatePerson.Size = new System.Drawing.Size(53, 12);
            this.labCreatePerson.TabIndex = 161;
            this.labCreatePerson.Text = "操作员：";
            // 
            // cboOrgId
            // 
            this.cboOrgId.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboOrgId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOrgId.FormattingEnabled = true;
            this.cboOrgId.Location = new System.Drawing.Point(678, 5);
            this.cboOrgId.Name = "cboOrgId";
            this.cboOrgId.Size = new System.Drawing.Size(121, 22);
            this.cboOrgId.TabIndex = 160;
            this.cboOrgId.SelectedIndexChanged += new System.EventHandler(this.cboOrgId_SelectedIndexChanged);
            // 
            // cobYHandle
            // 
            this.cobYHandle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cobYHandle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobYHandle.FormattingEnabled = true;
            this.cobYHandle.Location = new System.Drawing.Point(858, 5);
            this.cobYHandle.Name = "cobYHandle";
            this.cobYHandle.Size = new System.Drawing.Size(118, 22);
            this.cobYHandle.TabIndex = 159;
            // 
            // labDepart
            // 
            this.labDepart.AutoSize = true;
            this.labDepart.Location = new System.Drawing.Point(631, 9);
            this.labDepart.Name = "labDepart";
            this.labDepart.Size = new System.Drawing.Size(41, 12);
            this.labDepart.TabIndex = 158;
            this.labDepart.Text = "部门：";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(805, 9);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(53, 12);
            this.label31.TabIndex = 157;
            this.label31.Text = "经办人：";
            // 
            // contextMenuM
            // 
            this.contextMenuM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.contextMenuM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuM.Name = "contextMenuM";
            this.contextMenuM.Size = new System.Drawing.Size(153, 48);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem1.Text = "维修单导入";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // UCFetchMaterialAddOrEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.Controls.Add(this.palbottom1);
            this.Controls.Add(this.palBottom);
            this.Controls.Add(this.palInfo);
            this.Controls.Add(this.palTop);
            this.Name = "UCFetchMaterialAddOrEdit";
            this.Load += new System.EventHandler(this.UCFetchMaterialAddOrEdit_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.palTop, 0);
            this.Controls.SetChildIndex(this.palInfo, 0);
            this.Controls.SetChildIndex(this.palBottom, 0);
            this.Controls.SetChildIndex(this.palbottom1, 0);
            this.palTop.ResumeLayout(false);
            this.palTop.PerformLayout();
            this.palInfo.ResumeLayout(false);
            this.palInfo.PerformLayout();
            this.palBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterials)).EndInit();
            this.ctmParts.ResumeLayout(false);
            this.palbottom1.ResumeLayout(false);
            this.palbottom1.PerformLayout();
            this.contextMenuM.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel palTop;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpMakeOrderTime;
        private System.Windows.Forms.Label labMakeOrderTime;
        private System.Windows.Forms.Label labReserveNoS;
        private System.Windows.Forms.Label labReserveNo;
        private System.Windows.Forms.Panel palInfo;
        private ServiceStationClient.ComponentUI.TextBoxEx txtRemark;
        private System.Windows.Forms.Label labFaultDesc;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtCustomNO;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtCarType;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtCarNO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.TextBoxEx txtContactPhone;
        private ServiceStationClient.ComponentUI.TextBoxEx txtContact;
        private ServiceStationClient.ComponentUI.TextBoxEx txtCustomName;
        private System.Windows.Forms.Label labContactPhone;
        private System.Windows.Forms.Label labContact;
        private System.Windows.Forms.Label labCustomName;
        private System.Windows.Forms.Label labCustomNO;
        private System.Windows.Forms.Label labCarType;
        private System.Windows.Forms.Label labCarNO;
        private System.Windows.Forms.Label labReserveInfo;
        private System.Windows.Forms.Label labCustomerInfo;
        private System.Windows.Forms.Label labCarInfo;
        private ServiceStationClient.ComponentUI.TextBoxEx txtOrder;
        private System.Windows.Forms.Label labOrder;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtFetchOpid;
        private System.Windows.Forms.Label labFetchOpid;
        private System.Windows.Forms.Panel palBottom;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvMaterials;
        private ServiceStationClient.ComponentUI.PanelEx palbottom1;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboOrgId;
        private ServiceStationClient.ComponentUI.ComboBoxEx cobYHandle;
        private System.Windows.Forms.Label labDepart;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label labCreatePersonS;
        private System.Windows.Forms.Label labCreatePerson;
        private System.Windows.Forms.ContextMenuStrip contextMenuM;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip ctmParts;
        private System.Windows.Forms.ToolStripMenuItem addParts;
        private System.Windows.Forms.ToolStripMenuItem deleteParts;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolStripMenuItem editParts;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Mcheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn parts_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn parts_name;
        private System.Windows.Forms.DataGridViewComboBoxColumn three_warranty;
        private System.Windows.Forms.DataGridViewTextBoxColumn unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn inventory_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn picking_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn received_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn notFetchNum;
        private System.Windows.Forms.DataGridViewComboBoxColumn warehouse;
        private System.Windows.Forms.DataGridViewTextBoxColumn whether_imported;
        private System.Windows.Forms.DataGridViewTextBoxColumn drawn_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn vehicle_brand;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn material_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn fetch_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mdata_source;
    }
}