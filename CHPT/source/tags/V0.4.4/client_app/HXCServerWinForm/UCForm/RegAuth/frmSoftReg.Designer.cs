namespace HXCServerWinForm.UCForm
{
    partial class frmSoftReg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSoftReg));
            this.errProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlStep1 = new System.Windows.Forms.Panel();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.ddlcounty = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.ddlcity = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.ddlprovince = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.txtcom_tel = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtzip_code = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtlegal_person = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtcom_fax = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtaccess_code = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtservice_station_sap = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txthotline = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtcom_email = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtunit_properties = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtrepair_qualification = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtcom_contact = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label12 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtcom_address = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtcom_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.tlpOpBtn = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnCancel = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnReg = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnPrevStep = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnNextStep = new ServiceStationClient.ComponentUI.ButtonEx();
            this.pnlStep2 = new System.Windows.Forms.Panel();
            this.txtsign_id = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.txtgrant_authorization = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txtmachine_code_sequence = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label24 = new System.Windows.Forms.Label();
            this.txtcom_name2 = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.pnlStep3 = new System.Windows.Forms.Panel();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.txts_pwd = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label29 = new System.Windows.Forms.Label();
            this.txts_user = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label28 = new System.Windows.Forms.Label();
            this.txtauthentication = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label23 = new System.Windows.Forms.Label();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).BeginInit();
            this.pnlStep1.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.tlpOpBtn.SuspendLayout();
            this.pnlStep2.SuspendLayout();
            this.pnlStep3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.pnlStep2);
            this.pnlContainer.Controls.Add(this.pnlStep1);
            this.pnlContainer.Controls.Add(this.pnlStep3);
            this.pnlContainer.Controls.Add(this.pnlBottom);
            this.pnlContainer.Size = new System.Drawing.Size(589, 400);
            // 
            // errProvider
            // 
            this.errProvider.ContainerControl = this;
            // 
            // pnlStep1
            // 
            this.pnlStep1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlStep1.Controls.Add(this.label35);
            this.pnlStep1.Controls.Add(this.label34);
            this.pnlStep1.Controls.Add(this.label33);
            this.pnlStep1.Controls.Add(this.label32);
            this.pnlStep1.Controls.Add(this.label31);
            this.pnlStep1.Controls.Add(this.label14);
            this.pnlStep1.Controls.Add(this.ddlcounty);
            this.pnlStep1.Controls.Add(this.ddlcity);
            this.pnlStep1.Controls.Add(this.ddlprovince);
            this.pnlStep1.Controls.Add(this.txtcom_tel);
            this.pnlStep1.Controls.Add(this.txtzip_code);
            this.pnlStep1.Controls.Add(this.txtlegal_person);
            this.pnlStep1.Controls.Add(this.txtcom_fax);
            this.pnlStep1.Controls.Add(this.txtaccess_code);
            this.pnlStep1.Controls.Add(this.txtservice_station_sap);
            this.pnlStep1.Controls.Add(this.txthotline);
            this.pnlStep1.Controls.Add(this.txtcom_email);
            this.pnlStep1.Controls.Add(this.txtunit_properties);
            this.pnlStep1.Controls.Add(this.txtrepair_qualification);
            this.pnlStep1.Controls.Add(this.label16);
            this.pnlStep1.Controls.Add(this.label13);
            this.pnlStep1.Controls.Add(this.label15);
            this.pnlStep1.Controls.Add(this.txtcom_contact);
            this.pnlStep1.Controls.Add(this.label12);
            this.pnlStep1.Controls.Add(this.label7);
            this.pnlStep1.Controls.Add(this.label11);
            this.pnlStep1.Controls.Add(this.txtcom_address);
            this.pnlStep1.Controls.Add(this.label10);
            this.pnlStep1.Controls.Add(this.label8);
            this.pnlStep1.Controls.Add(this.label9);
            this.pnlStep1.Controls.Add(this.label5);
            this.pnlStep1.Controls.Add(this.label6);
            this.pnlStep1.Controls.Add(this.txtcom_name);
            this.pnlStep1.Controls.Add(this.label4);
            this.pnlStep1.Controls.Add(this.label3);
            this.pnlStep1.Controls.Add(this.label2);
            this.pnlStep1.Controls.Add(this.label1);
            this.pnlStep1.Location = new System.Drawing.Point(0, 0);
            this.pnlStep1.Name = "pnlStep1";
            this.pnlStep1.Size = new System.Drawing.Size(589, 363);
            this.pnlStep1.TabIndex = 6;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.ForeColor = System.Drawing.Color.Red;
            this.label35.Location = new System.Drawing.Point(564, 147);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(11, 12);
            this.label35.TabIndex = 38;
            this.label35.Text = "*";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.ForeColor = System.Drawing.Color.Red;
            this.label34.Location = new System.Drawing.Point(271, 147);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(11, 12);
            this.label34.TabIndex = 38;
            this.label34.Text = "*";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.ForeColor = System.Drawing.Color.Red;
            this.label33.Location = new System.Drawing.Point(361, 114);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(11, 12);
            this.label33.TabIndex = 38;
            this.label33.Text = "*";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.ForeColor = System.Drawing.Color.Red;
            this.label32.Location = new System.Drawing.Point(363, 80);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(11, 12);
            this.label32.TabIndex = 38;
            this.label32.Text = "*";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.ForeColor = System.Drawing.Color.Red;
            this.label31.Location = new System.Drawing.Point(430, 46);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(11, 12);
            this.label31.TabIndex = 38;
            this.label31.Text = "*";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(38, 284);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(341, 12);
            this.label14.TabIndex = 37;
            this.label14.Text = "如果是宇通服务站，请填写宇通相关信息，便于建立关联关系。";
            // 
            // ddlcounty
            // 
            this.ddlcounty.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlcounty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlcounty.FormattingEnabled = true;
            this.ddlcounty.Location = new System.Drawing.Point(276, 75);
            this.ddlcounty.Name = "ddlcounty";
            this.ddlcounty.Size = new System.Drawing.Size(84, 22);
            this.ddlcounty.TabIndex = 2;
            // 
            // ddlcity
            // 
            this.ddlcity.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlcity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlcity.FormattingEnabled = true;
            this.ddlcity.Location = new System.Drawing.Point(186, 75);
            this.ddlcity.Name = "ddlcity";
            this.ddlcity.Size = new System.Drawing.Size(84, 22);
            this.ddlcity.TabIndex = 1;
            this.ddlcity.SelectedIndexChanged += new System.EventHandler(this.ddlcity_SelectedIndexChanged);
            // 
            // ddlprovince
            // 
            this.ddlprovince.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlprovince.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlprovince.FormattingEnabled = true;
            this.ddlprovince.Location = new System.Drawing.Point(96, 75);
            this.ddlprovince.Name = "ddlprovince";
            this.ddlprovince.Size = new System.Drawing.Size(84, 22);
            this.ddlprovince.TabIndex = 34;
            this.ddlprovince.SelectedIndexChanged += new System.EventHandler(this.ddlprovince_SelectedIndexChanged);
            // 
            // txtcom_tel
            // 
            this.txtcom_tel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtcom_tel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtcom_tel.BackColor = System.Drawing.Color.Transparent;
            this.txtcom_tel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtcom_tel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtcom_tel.ForeImage = null;
            this.txtcom_tel.InputtingVerifyCondition = null;
            this.txtcom_tel.Location = new System.Drawing.Point(412, 142);
            this.txtcom_tel.MaxLengh = 40;
            this.txtcom_tel.Multiline = false;
            this.txtcom_tel.Name = "txtcom_tel";
            this.txtcom_tel.Radius = 3;
            this.txtcom_tel.ReadOnly = false;
            this.txtcom_tel.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtcom_tel.ShowError = false;
            this.txtcom_tel.Size = new System.Drawing.Size(151, 23);
            this.txtcom_tel.TabIndex = 6;
            this.txtcom_tel.UseSystemPasswordChar = false;
            this.txtcom_tel.Value = "";
            this.txtcom_tel.VerifyCondition = null;
            this.txtcom_tel.VerifyType = null;
            this.txtcom_tel.VerifyTypeName = null;
            this.txtcom_tel.WaterMark = null;
            this.txtcom_tel.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtzip_code
            // 
            this.txtzip_code.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtzip_code.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtzip_code.BackColor = System.Drawing.Color.Transparent;
            this.txtzip_code.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtzip_code.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtzip_code.ForeImage = null;
            this.txtzip_code.InputtingVerifyCondition = null;
            this.txtzip_code.Location = new System.Drawing.Point(412, 109);
            this.txtzip_code.MaxLengh = 32767;
            this.txtzip_code.Multiline = false;
            this.txtzip_code.Name = "txtzip_code";
            this.txtzip_code.Radius = 3;
            this.txtzip_code.ReadOnly = false;
            this.txtzip_code.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtzip_code.ShowError = false;
            this.txtzip_code.Size = new System.Drawing.Size(151, 23);
            this.txtzip_code.TabIndex = 4;
            this.txtzip_code.UseSystemPasswordChar = false;
            this.txtzip_code.Value = "";
            this.txtzip_code.VerifyCondition = null;
            this.txtzip_code.VerifyType = null;
            this.txtzip_code.VerifyTypeName = null;
            this.txtzip_code.WaterMark = null;
            this.txtzip_code.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtlegal_person
            // 
            this.txtlegal_person.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtlegal_person.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtlegal_person.BackColor = System.Drawing.Color.Transparent;
            this.txtlegal_person.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtlegal_person.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtlegal_person.ForeImage = null;
            this.txtlegal_person.InputtingVerifyCondition = null;
            this.txtlegal_person.Location = new System.Drawing.Point(96, 175);
            this.txtlegal_person.MaxLengh = 32767;
            this.txtlegal_person.Multiline = false;
            this.txtlegal_person.Name = "txtlegal_person";
            this.txtlegal_person.Radius = 3;
            this.txtlegal_person.ReadOnly = false;
            this.txtlegal_person.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtlegal_person.ShowError = false;
            this.txtlegal_person.Size = new System.Drawing.Size(174, 23);
            this.txtlegal_person.TabIndex = 7;
            this.txtlegal_person.UseSystemPasswordChar = false;
            this.txtlegal_person.Value = "";
            this.txtlegal_person.VerifyCondition = null;
            this.txtlegal_person.VerifyType = null;
            this.txtlegal_person.VerifyTypeName = null;
            this.txtlegal_person.WaterMark = null;
            this.txtlegal_person.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtcom_fax
            // 
            this.txtcom_fax.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtcom_fax.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtcom_fax.BackColor = System.Drawing.Color.Transparent;
            this.txtcom_fax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtcom_fax.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtcom_fax.ForeImage = null;
            this.txtcom_fax.InputtingVerifyCondition = null;
            this.txtcom_fax.Location = new System.Drawing.Point(412, 241);
            this.txtcom_fax.MaxLengh = 32767;
            this.txtcom_fax.Multiline = false;
            this.txtcom_fax.Name = "txtcom_fax";
            this.txtcom_fax.Radius = 3;
            this.txtcom_fax.ReadOnly = false;
            this.txtcom_fax.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtcom_fax.ShowError = false;
            this.txtcom_fax.Size = new System.Drawing.Size(151, 23);
            this.txtcom_fax.TabIndex = 12;
            this.txtcom_fax.UseSystemPasswordChar = false;
            this.txtcom_fax.Value = "";
            this.txtcom_fax.VerifyCondition = null;
            this.txtcom_fax.VerifyType = null;
            this.txtcom_fax.VerifyTypeName = null;
            this.txtcom_fax.WaterMark = null;
            this.txtcom_fax.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtaccess_code
            // 
            this.txtaccess_code.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtaccess_code.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtaccess_code.BackColor = System.Drawing.Color.Transparent;
            this.txtaccess_code.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtaccess_code.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtaccess_code.ForeImage = null;
            this.txtaccess_code.InputtingVerifyCondition = null;
            this.txtaccess_code.Location = new System.Drawing.Point(412, 310);
            this.txtaccess_code.MaxLengh = 32767;
            this.txtaccess_code.Multiline = false;
            this.txtaccess_code.Name = "txtaccess_code";
            this.txtaccess_code.Radius = 3;
            this.txtaccess_code.ReadOnly = false;
            this.txtaccess_code.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtaccess_code.ShowError = false;
            this.txtaccess_code.Size = new System.Drawing.Size(151, 23);
            this.txtaccess_code.TabIndex = 14;
            this.txtaccess_code.UseSystemPasswordChar = false;
            this.txtaccess_code.Value = "";
            this.txtaccess_code.VerifyCondition = null;
            this.txtaccess_code.VerifyType = null;
            this.txtaccess_code.VerifyTypeName = null;
            this.txtaccess_code.WaterMark = null;
            this.txtaccess_code.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtservice_station_sap
            // 
            this.txtservice_station_sap.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtservice_station_sap.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtservice_station_sap.BackColor = System.Drawing.Color.Transparent;
            this.txtservice_station_sap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtservice_station_sap.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtservice_station_sap.ForeImage = null;
            this.txtservice_station_sap.InputtingVerifyCondition = null;
            this.txtservice_station_sap.Location = new System.Drawing.Point(95, 310);
            this.txtservice_station_sap.MaxLengh = 32767;
            this.txtservice_station_sap.Multiline = false;
            this.txtservice_station_sap.Name = "txtservice_station_sap";
            this.txtservice_station_sap.Radius = 3;
            this.txtservice_station_sap.ReadOnly = false;
            this.txtservice_station_sap.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtservice_station_sap.ShowError = false;
            this.txtservice_station_sap.Size = new System.Drawing.Size(175, 23);
            this.txtservice_station_sap.TabIndex = 13;
            this.txtservice_station_sap.UseSystemPasswordChar = false;
            this.txtservice_station_sap.Value = "";
            this.txtservice_station_sap.VerifyCondition = null;
            this.txtservice_station_sap.VerifyType = null;
            this.txtservice_station_sap.VerifyTypeName = null;
            this.txtservice_station_sap.WaterMark = null;
            this.txtservice_station_sap.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txthotline
            // 
            this.txthotline.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txthotline.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txthotline.BackColor = System.Drawing.Color.Transparent;
            this.txthotline.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txthotline.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txthotline.ForeImage = null;
            this.txthotline.InputtingVerifyCondition = null;
            this.txthotline.Location = new System.Drawing.Point(95, 241);
            this.txthotline.MaxLengh = 40;
            this.txthotline.Multiline = false;
            this.txthotline.Name = "txthotline";
            this.txthotline.Radius = 3;
            this.txthotline.ReadOnly = false;
            this.txthotline.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txthotline.ShowError = false;
            this.txthotline.Size = new System.Drawing.Size(175, 23);
            this.txthotline.TabIndex = 11;
            this.txthotline.UseSystemPasswordChar = false;
            this.txthotline.Value = "";
            this.txthotline.VerifyCondition = null;
            this.txthotline.VerifyType = null;
            this.txthotline.VerifyTypeName = null;
            this.txthotline.WaterMark = null;
            this.txthotline.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtcom_email
            // 
            this.txtcom_email.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtcom_email.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtcom_email.BackColor = System.Drawing.Color.Transparent;
            this.txtcom_email.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtcom_email.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtcom_email.ForeImage = null;
            this.txtcom_email.InputtingVerifyCondition = null;
            this.txtcom_email.Location = new System.Drawing.Point(412, 208);
            this.txtcom_email.MaxLengh = 32767;
            this.txtcom_email.Multiline = false;
            this.txtcom_email.Name = "txtcom_email";
            this.txtcom_email.Radius = 3;
            this.txtcom_email.ReadOnly = false;
            this.txtcom_email.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtcom_email.ShowError = false;
            this.txtcom_email.Size = new System.Drawing.Size(151, 23);
            this.txtcom_email.TabIndex = 10;
            this.txtcom_email.UseSystemPasswordChar = false;
            this.txtcom_email.Value = "";
            this.txtcom_email.VerifyCondition = null;
            this.txtcom_email.VerifyType = null;
            this.txtcom_email.VerifyTypeName = null;
            this.txtcom_email.WaterMark = null;
            this.txtcom_email.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtunit_properties
            // 
            this.txtunit_properties.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtunit_properties.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtunit_properties.BackColor = System.Drawing.Color.Transparent;
            this.txtunit_properties.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtunit_properties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtunit_properties.ForeImage = null;
            this.txtunit_properties.InputtingVerifyCondition = null;
            this.txtunit_properties.Location = new System.Drawing.Point(96, 208);
            this.txtunit_properties.MaxLengh = 32767;
            this.txtunit_properties.Multiline = false;
            this.txtunit_properties.Name = "txtunit_properties";
            this.txtunit_properties.Radius = 3;
            this.txtunit_properties.ReadOnly = false;
            this.txtunit_properties.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtunit_properties.ShowError = false;
            this.txtunit_properties.Size = new System.Drawing.Size(175, 23);
            this.txtunit_properties.TabIndex = 9;
            this.txtunit_properties.UseSystemPasswordChar = false;
            this.txtunit_properties.Value = "";
            this.txtunit_properties.VerifyCondition = null;
            this.txtunit_properties.VerifyType = null;
            this.txtunit_properties.VerifyTypeName = null;
            this.txtunit_properties.WaterMark = null;
            this.txtunit_properties.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtrepair_qualification
            // 
            this.txtrepair_qualification.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtrepair_qualification.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtrepair_qualification.BackColor = System.Drawing.Color.Transparent;
            this.txtrepair_qualification.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtrepair_qualification.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtrepair_qualification.ForeImage = null;
            this.txtrepair_qualification.InputtingVerifyCondition = null;
            this.txtrepair_qualification.Location = new System.Drawing.Point(412, 175);
            this.txtrepair_qualification.MaxLengh = 32767;
            this.txtrepair_qualification.Multiline = false;
            this.txtrepair_qualification.Name = "txtrepair_qualification";
            this.txtrepair_qualification.Radius = 3;
            this.txtrepair_qualification.ReadOnly = false;
            this.txtrepair_qualification.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtrepair_qualification.ShowError = false;
            this.txtrepair_qualification.Size = new System.Drawing.Size(151, 23);
            this.txtrepair_qualification.TabIndex = 8;
            this.txtrepair_qualification.UseSystemPasswordChar = false;
            this.txtrepair_qualification.Value = "";
            this.txtrepair_qualification.VerifyCondition = null;
            this.txtrepair_qualification.VerifyType = null;
            this.txtrepair_qualification.VerifyTypeName = null;
            this.txtrepair_qualification.WaterMark = null;
            this.txtrepair_qualification.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(338, 315);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 12);
            this.label16.TabIndex = 19;
            this.label16.Text = "宇通接入码：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(374, 246);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 20;
            this.label13.Text = "传真：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(39, 315);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 12);
            this.label15.TabIndex = 17;
            this.label15.Text = "宇通sap：";
            // 
            // txtcom_contact
            // 
            this.txtcom_contact.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtcom_contact.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtcom_contact.BackColor = System.Drawing.Color.Transparent;
            this.txtcom_contact.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtcom_contact.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtcom_contact.ForeImage = null;
            this.txtcom_contact.InputtingVerifyCondition = null;
            this.txtcom_contact.Location = new System.Drawing.Point(95, 142);
            this.txtcom_contact.MaxLengh = 32767;
            this.txtcom_contact.Multiline = false;
            this.txtcom_contact.Name = "txtcom_contact";
            this.txtcom_contact.Radius = 3;
            this.txtcom_contact.ReadOnly = false;
            this.txtcom_contact.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtcom_contact.ShowError = false;
            this.txtcom_contact.Size = new System.Drawing.Size(175, 23);
            this.txtcom_contact.TabIndex = 5;
            this.txtcom_contact.UseSystemPasswordChar = false;
            this.txtcom_contact.Value = "";
            this.txtcom_contact.VerifyCondition = null;
            this.txtcom_contact.VerifyType = null;
            this.txtcom_contact.VerifyTypeName = null;
            this.txtcom_contact.WaterMark = null;
            this.txtcom_contact.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(33, 246);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 16;
            this.label12.Text = "热线电话：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(350, 147);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "联系电话：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(350, 213);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 14;
            this.label11.Text = "电子邮件：";
            // 
            // txtcom_address
            // 
            this.txtcom_address.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtcom_address.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtcom_address.BackColor = System.Drawing.Color.Transparent;
            this.txtcom_address.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtcom_address.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtcom_address.ForeImage = null;
            this.txtcom_address.InputtingVerifyCondition = null;
            this.txtcom_address.Location = new System.Drawing.Point(95, 109);
            this.txtcom_address.MaxLengh = 32767;
            this.txtcom_address.Multiline = false;
            this.txtcom_address.Name = "txtcom_address";
            this.txtcom_address.Radius = 3;
            this.txtcom_address.ReadOnly = false;
            this.txtcom_address.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtcom_address.ShowError = false;
            this.txtcom_address.Size = new System.Drawing.Size(265, 23);
            this.txtcom_address.TabIndex = 3;
            this.txtcom_address.UseSystemPasswordChar = false;
            this.txtcom_address.Value = "";
            this.txtcom_address.VerifyCondition = null;
            this.txtcom_address.VerifyType = null;
            this.txtcom_address.VerifyTypeName = null;
            this.txtcom_address.WaterMark = null;
            this.txtcom_address.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(33, 213);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 13;
            this.label10.Text = "单位性质：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 180);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 12);
            this.label8.TabIndex = 12;
            this.label8.Text = "法人|负责人：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(350, 180);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 11;
            this.label9.Text = "维修资质：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(374, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "邮编：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(44, 147);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "联系人：";
            // 
            // txtcom_name
            // 
            this.txtcom_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtcom_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtcom_name.BackColor = System.Drawing.Color.Transparent;
            this.txtcom_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtcom_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtcom_name.ForeImage = null;
            this.txtcom_name.InputtingVerifyCondition = null;
            this.txtcom_name.Location = new System.Drawing.Point(96, 41);
            this.txtcom_name.MaxLengh = 32767;
            this.txtcom_name.Multiline = false;
            this.txtcom_name.Name = "txtcom_name";
            this.txtcom_name.Radius = 3;
            this.txtcom_name.ReadOnly = false;
            this.txtcom_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtcom_name.ShowError = false;
            this.txtcom_name.Size = new System.Drawing.Size(333, 23);
            this.txtcom_name.TabIndex = 0;
            this.txtcom_name.UseSystemPasswordChar = false;
            this.txtcom_name.Value = "";
            this.txtcom_name.VerifyCondition = null;
            this.txtcom_name.VerifyType = null;
            this.txtcom_name.VerifyTypeName = null;
            this.txtcom_name.WaterMark = null;
            this.txtcom_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "联系地址：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "所在地：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "公司名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(44, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(377, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "为了能够及时获得授权并使用本软件。请认真、准确填写下列公司信息";
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlBottom.Controls.Add(this.tlpOpBtn);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 362);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(589, 38);
            this.pnlBottom.TabIndex = 7;
            // 
            // tlpOpBtn
            // 
            this.tlpOpBtn.ColumnCount = 5;
            this.tlpOpBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpOpBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpOpBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpOpBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpOpBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpOpBtn.Controls.Add(this.btnSave, 4, 0);
            this.tlpOpBtn.Controls.Add(this.btnCancel, 0, 0);
            this.tlpOpBtn.Controls.Add(this.btnReg, 1, 0);
            this.tlpOpBtn.Controls.Add(this.btnPrevStep, 3, 0);
            this.tlpOpBtn.Controls.Add(this.btnNextStep, 2, 0);
            this.tlpOpBtn.Location = new System.Drawing.Point(233, 4);
            this.tlpOpBtn.Name = "tlpOpBtn";
            this.tlpOpBtn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tlpOpBtn.RowCount = 1;
            this.tlpOpBtn.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOpBtn.Size = new System.Drawing.Size(330, 32);
            this.tlpOpBtn.TabIndex = 25;
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.Caption = "保存";
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSave.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSave.DownImage")));
            this.btnSave.Location = new System.Drawing.Point(3, 3);
            this.btnSave.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSave.MoveImage")));
            this.btnSave.Name = "btnSave";
            this.btnSave.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSave.NormalImage")));
            this.btnSave.Size = new System.Drawing.Size(60, 24);
            this.btnSave.TabIndex = 0;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Caption = "取消";
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.DownImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.DownImage")));
            this.btnCancel.Location = new System.Drawing.Point(267, 3);
            this.btnCancel.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.MoveImage")));
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.NormalImage")));
            this.btnCancel.Size = new System.Drawing.Size(60, 24);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnReg
            // 
            this.btnReg.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReg.BackgroundImage")));
            this.btnReg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReg.Caption = "注册";
            this.btnReg.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnReg.DownImage = ((System.Drawing.Image)(resources.GetObject("btnReg.DownImage")));
            this.btnReg.Location = new System.Drawing.Point(201, 3);
            this.btnReg.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnReg.MoveImage")));
            this.btnReg.Name = "btnReg";
            this.btnReg.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnReg.NormalImage")));
            this.btnReg.Size = new System.Drawing.Size(60, 24);
            this.btnReg.TabIndex = 3;
            this.btnReg.Click += new System.EventHandler(this.btnReg_Click);
            // 
            // btnPrevStep
            // 
            this.btnPrevStep.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrevStep.BackgroundImage")));
            this.btnPrevStep.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrevStep.Caption = "上一步";
            this.btnPrevStep.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnPrevStep.DownImage = ((System.Drawing.Image)(resources.GetObject("btnPrevStep.DownImage")));
            this.btnPrevStep.Location = new System.Drawing.Point(69, 3);
            this.btnPrevStep.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnPrevStep.MoveImage")));
            this.btnPrevStep.Name = "btnPrevStep";
            this.btnPrevStep.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnPrevStep.NormalImage")));
            this.btnPrevStep.Size = new System.Drawing.Size(60, 24);
            this.btnPrevStep.TabIndex = 1;
            this.btnPrevStep.Click += new System.EventHandler(this.btnPrevStep_Click);
            // 
            // btnNextStep
            // 
            this.btnNextStep.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNextStep.BackgroundImage")));
            this.btnNextStep.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNextStep.Caption = "下一步";
            this.btnNextStep.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnNextStep.DownImage = ((System.Drawing.Image)(resources.GetObject("btnNextStep.DownImage")));
            this.btnNextStep.Location = new System.Drawing.Point(135, 3);
            this.btnNextStep.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnNextStep.MoveImage")));
            this.btnNextStep.Name = "btnNextStep";
            this.btnNextStep.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnNextStep.NormalImage")));
            this.btnNextStep.Size = new System.Drawing.Size(60, 24);
            this.btnNextStep.TabIndex = 2;
            this.btnNextStep.Click += new System.EventHandler(this.btnNextStep_Click);
            // 
            // pnlStep2
            // 
            this.pnlStep2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlStep2.Controls.Add(this.txtsign_id);
            this.pnlStep2.Controls.Add(this.label39);
            this.pnlStep2.Controls.Add(this.label40);
            this.pnlStep2.Controls.Add(this.label30);
            this.pnlStep2.Controls.Add(this.txtgrant_authorization);
            this.pnlStep2.Controls.Add(this.label17);
            this.pnlStep2.Controls.Add(this.label18);
            this.pnlStep2.Controls.Add(this.label19);
            this.pnlStep2.Controls.Add(this.label20);
            this.pnlStep2.Controls.Add(this.label21);
            this.pnlStep2.Controls.Add(this.label22);
            this.pnlStep2.Controls.Add(this.txtmachine_code_sequence);
            this.pnlStep2.Controls.Add(this.label24);
            this.pnlStep2.Controls.Add(this.txtcom_name2);
            this.pnlStep2.Controls.Add(this.label25);
            this.pnlStep2.Controls.Add(this.label26);
            this.pnlStep2.Location = new System.Drawing.Point(0, 0);
            this.pnlStep2.Name = "pnlStep2";
            this.pnlStep2.Size = new System.Drawing.Size(589, 363);
            this.pnlStep2.TabIndex = 38;
            // 
            // txtsign_id
            // 
            this.txtsign_id.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtsign_id.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtsign_id.BackColor = System.Drawing.Color.Transparent;
            this.txtsign_id.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtsign_id.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtsign_id.ForeImage = null;
            this.txtsign_id.InputtingVerifyCondition = null;
            this.txtsign_id.Location = new System.Drawing.Point(166, 146);
            this.txtsign_id.MaxLengh = 32767;
            this.txtsign_id.Multiline = false;
            this.txtsign_id.Name = "txtsign_id";
            this.txtsign_id.Radius = 3;
            this.txtsign_id.ReadOnly = false;
            this.txtsign_id.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtsign_id.ShowError = false;
            this.txtsign_id.Size = new System.Drawing.Size(333, 23);
            this.txtsign_id.TabIndex = 2;
            this.txtsign_id.UseSystemPasswordChar = false;
            this.txtsign_id.Value = "";
            this.txtsign_id.VerifyCondition = null;
            this.txtsign_id.VerifyType = null;
            this.txtsign_id.VerifyTypeName = null;
            this.txtsign_id.WaterMark = null;
            this.txtsign_id.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(103, 151);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(65, 12);
            this.label39.TabIndex = 20;
            this.label39.Text = "服务站ID：";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.ForeColor = System.Drawing.Color.Red;
            this.label40.Location = new System.Drawing.Point(500, 151);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(11, 12);
            this.label40.TabIndex = 19;
            this.label40.Text = "*";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.ForeColor = System.Drawing.Color.Red;
            this.label30.Location = new System.Drawing.Point(500, 180);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(11, 12);
            this.label30.TabIndex = 19;
            this.label30.Text = "*";
            // 
            // txtgrant_authorization
            // 
            this.txtgrant_authorization.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtgrant_authorization.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtgrant_authorization.BackColor = System.Drawing.Color.Transparent;
            this.txtgrant_authorization.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtgrant_authorization.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtgrant_authorization.ForeImage = null;
            this.txtgrant_authorization.InputtingVerifyCondition = null;
            this.txtgrant_authorization.Location = new System.Drawing.Point(166, 175);
            this.txtgrant_authorization.MaxLengh = 32767;
            this.txtgrant_authorization.Multiline = false;
            this.txtgrant_authorization.Name = "txtgrant_authorization";
            this.txtgrant_authorization.Radius = 3;
            this.txtgrant_authorization.ReadOnly = false;
            this.txtgrant_authorization.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtgrant_authorization.ShowError = false;
            this.txtgrant_authorization.Size = new System.Drawing.Size(333, 23);
            this.txtgrant_authorization.TabIndex = 3;
            this.txtgrant_authorization.UseSystemPasswordChar = false;
            this.txtgrant_authorization.Value = "";
            this.txtgrant_authorization.VerifyCondition = null;
            this.txtgrant_authorization.VerifyType = null;
            this.txtgrant_authorization.VerifyTypeName = null;
            this.txtgrant_authorization.WaterMark = null;
            this.txtgrant_authorization.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(119, 245);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(380, 26);
            this.label17.TabIndex = 6;
            this.label17.Text = "2、如果系统需要授权使用，请向服务商申请授权码，报上您的机器序列号，运营质层管理员将手动为您生成授权码。";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(118, 229);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(359, 12);
            this.label18.TabIndex = 7;
            this.label18.Text = "1、如果系统为你自动生成了授权码，请点击“下一步”直接跳过。";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(115, 211);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(41, 12);
            this.label19.TabIndex = 8;
            this.label19.Text = "注意：";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(149, 283);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(107, 12);
            this.label20.TabIndex = 9;
            this.label20.Text = "邮件：123@163.com";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(149, 271);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(119, 12);
            this.label21.TabIndex = 10;
            this.label21.Text = "电话：0371-62313956";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(115, 180);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(53, 12);
            this.label22.TabIndex = 11;
            this.label22.Text = "授权码：";
            // 
            // txtmachine_code_sequence
            // 
            this.txtmachine_code_sequence.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtmachine_code_sequence.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtmachine_code_sequence.BackColor = System.Drawing.Color.Transparent;
            this.txtmachine_code_sequence.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtmachine_code_sequence.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtmachine_code_sequence.ForeImage = null;
            this.txtmachine_code_sequence.InputtingVerifyCondition = null;
            this.txtmachine_code_sequence.Location = new System.Drawing.Point(166, 117);
            this.txtmachine_code_sequence.MaxLengh = 32767;
            this.txtmachine_code_sequence.Multiline = false;
            this.txtmachine_code_sequence.Name = "txtmachine_code_sequence";
            this.txtmachine_code_sequence.Radius = 3;
            this.txtmachine_code_sequence.ReadOnly = true;
            this.txtmachine_code_sequence.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtmachine_code_sequence.ShowError = false;
            this.txtmachine_code_sequence.Size = new System.Drawing.Size(333, 23);
            this.txtmachine_code_sequence.TabIndex = 1;
            this.txtmachine_code_sequence.UseSystemPasswordChar = false;
            this.txtmachine_code_sequence.Value = "";
            this.txtmachine_code_sequence.VerifyCondition = null;
            this.txtmachine_code_sequence.VerifyType = null;
            this.txtmachine_code_sequence.VerifyTypeName = null;
            this.txtmachine_code_sequence.WaterMark = null;
            this.txtmachine_code_sequence.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(91, 122);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(77, 12);
            this.label24.TabIndex = 13;
            this.label24.Text = "机器序列号：";
            // 
            // txtcom_name2
            // 
            this.txtcom_name2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtcom_name2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtcom_name2.BackColor = System.Drawing.Color.Transparent;
            this.txtcom_name2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtcom_name2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtcom_name2.ForeImage = null;
            this.txtcom_name2.InputtingVerifyCondition = null;
            this.txtcom_name2.Location = new System.Drawing.Point(166, 88);
            this.txtcom_name2.MaxLengh = 32767;
            this.txtcom_name2.Multiline = false;
            this.txtcom_name2.Name = "txtcom_name2";
            this.txtcom_name2.Radius = 3;
            this.txtcom_name2.ReadOnly = true;
            this.txtcom_name2.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtcom_name2.ShowError = false;
            this.txtcom_name2.Size = new System.Drawing.Size(333, 23);
            this.txtcom_name2.TabIndex = 0;
            this.txtcom_name2.UseSystemPasswordChar = false;
            this.txtcom_name2.Value = "";
            this.txtcom_name2.VerifyCondition = null;
            this.txtcom_name2.VerifyType = null;
            this.txtcom_name2.VerifyTypeName = null;
            this.txtcom_name2.WaterMark = null;
            this.txtcom_name2.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(103, 93);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(65, 12);
            this.label25.TabIndex = 14;
            this.label25.Text = "公司名称：";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.ForeColor = System.Drawing.Color.Red;
            this.label26.Location = new System.Drawing.Point(120, 61);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(341, 12);
            this.label26.TabIndex = 0;
            this.label26.Text = "请牢记您的机器序列号，将该号码提交给服务商，申领授权码！";
            // 
            // pnlStep3
            // 
            this.pnlStep3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlStep3.Controls.Add(this.label38);
            this.pnlStep3.Controls.Add(this.label37);
            this.pnlStep3.Controls.Add(this.label36);
            this.pnlStep3.Controls.Add(this.label27);
            this.pnlStep3.Controls.Add(this.txts_pwd);
            this.pnlStep3.Controls.Add(this.label29);
            this.pnlStep3.Controls.Add(this.txts_user);
            this.pnlStep3.Controls.Add(this.label28);
            this.pnlStep3.Controls.Add(this.txtauthentication);
            this.pnlStep3.Controls.Add(this.label23);
            this.pnlStep3.Location = new System.Drawing.Point(0, 0);
            this.pnlStep3.Name = "pnlStep3";
            this.pnlStep3.Size = new System.Drawing.Size(589, 363);
            this.pnlStep3.TabIndex = 38;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.ForeColor = System.Drawing.Color.Red;
            this.label38.Location = new System.Drawing.Point(488, 214);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(11, 12);
            this.label38.TabIndex = 20;
            this.label38.Text = "*";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.ForeColor = System.Drawing.Color.Red;
            this.label37.Location = new System.Drawing.Point(488, 174);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(11, 12);
            this.label37.TabIndex = 20;
            this.label37.Text = "*";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.ForeColor = System.Drawing.Color.Red;
            this.label36.Location = new System.Drawing.Point(488, 134);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(11, 12);
            this.label36.TabIndex = 20;
            this.label36.Text = "*";
            // 
            // label27
            // 
            this.label27.ForeColor = System.Drawing.Color.Red;
            this.label27.Location = new System.Drawing.Point(118, 85);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(341, 29);
            this.label27.TabIndex = 19;
            this.label27.Text = "为了保证正常的数据传输,请填写慧修车系统与慧联支撑系统的通讯鉴权码信息。该信息可向客户经理索取。";
            // 
            // txts_pwd
            // 
            this.txts_pwd.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txts_pwd.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txts_pwd.BackColor = System.Drawing.Color.Transparent;
            this.txts_pwd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txts_pwd.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txts_pwd.ForeImage = null;
            this.txts_pwd.InputtingVerifyCondition = null;
            this.txts_pwd.Location = new System.Drawing.Point(154, 209);
            this.txts_pwd.MaxLengh = 32767;
            this.txts_pwd.Multiline = false;
            this.txts_pwd.Name = "txts_pwd";
            this.txts_pwd.Radius = 3;
            this.txts_pwd.ReadOnly = false;
            this.txts_pwd.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txts_pwd.ShowError = false;
            this.txts_pwd.Size = new System.Drawing.Size(333, 23);
            this.txts_pwd.TabIndex = 18;
            this.txts_pwd.UseSystemPasswordChar = false;
            this.txts_pwd.Value = "";
            this.txts_pwd.VerifyCondition = null;
            this.txts_pwd.VerifyType = null;
            this.txts_pwd.VerifyTypeName = null;
            this.txts_pwd.WaterMark = null;
            this.txts_pwd.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(115, 214);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(41, 12);
            this.label29.TabIndex = 17;
            this.label29.Text = "密码：";
            // 
            // txts_user
            // 
            this.txts_user.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txts_user.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txts_user.BackColor = System.Drawing.Color.Transparent;
            this.txts_user.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txts_user.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txts_user.ForeImage = null;
            this.txts_user.InputtingVerifyCondition = null;
            this.txts_user.Location = new System.Drawing.Point(154, 169);
            this.txts_user.MaxLengh = 32767;
            this.txts_user.Multiline = false;
            this.txts_user.Name = "txts_user";
            this.txts_user.Radius = 3;
            this.txts_user.ReadOnly = false;
            this.txts_user.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txts_user.ShowError = false;
            this.txts_user.Size = new System.Drawing.Size(333, 23);
            this.txts_user.TabIndex = 18;
            this.txts_user.UseSystemPasswordChar = false;
            this.txts_user.Value = "";
            this.txts_user.VerifyCondition = null;
            this.txts_user.VerifyType = null;
            this.txts_user.VerifyTypeName = null;
            this.txts_user.WaterMark = null;
            this.txts_user.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(103, 174);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(53, 12);
            this.label28.TabIndex = 17;
            this.label28.Text = "用户名：";
            // 
            // txtauthentication
            // 
            this.txtauthentication.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtauthentication.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtauthentication.BackColor = System.Drawing.Color.Transparent;
            this.txtauthentication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtauthentication.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtauthentication.ForeImage = null;
            this.txtauthentication.InputtingVerifyCondition = null;
            this.txtauthentication.Location = new System.Drawing.Point(154, 130);
            this.txtauthentication.MaxLengh = 32767;
            this.txtauthentication.Multiline = false;
            this.txtauthentication.Name = "txtauthentication";
            this.txtauthentication.Radius = 3;
            this.txtauthentication.ReadOnly = false;
            this.txtauthentication.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtauthentication.ShowError = false;
            this.txtauthentication.Size = new System.Drawing.Size(333, 23);
            this.txtauthentication.TabIndex = 18;
            this.txtauthentication.UseSystemPasswordChar = false;
            this.txtauthentication.Value = "";
            this.txtauthentication.VerifyCondition = null;
            this.txtauthentication.VerifyType = null;
            this.txtauthentication.VerifyTypeName = null;
            this.txtauthentication.WaterMark = null;
            this.txtauthentication.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(103, 135);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(53, 12);
            this.label23.TabIndex = 17;
            this.label23.Text = "鉴权码：";
            // 
            // frmSoftReg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CanResize = false;
            this.ClientSize = new System.Drawing.Size(591, 431);
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSoftReg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户注册";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSoftReg_FormClosing);
            this.Load += new System.EventHandler(this.frmSoftReg_Load);
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).EndInit();
            this.pnlStep1.ResumeLayout(false);
            this.pnlStep1.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.tlpOpBtn.ResumeLayout(false);
            this.pnlStep2.ResumeLayout(false);
            this.pnlStep2.PerformLayout();
            this.pnlStep3.ResumeLayout(false);
            this.pnlStep3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errProvider;
        private System.Windows.Forms.Panel pnlStep1;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label label14;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlcounty;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlcity;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlprovince;
        private ServiceStationClient.ComponentUI.TextBoxEx txtcom_tel;
        private ServiceStationClient.ComponentUI.TextBoxEx txtzip_code;
        private ServiceStationClient.ComponentUI.TextBoxEx txtlegal_person;
        private ServiceStationClient.ComponentUI.TextBoxEx txtcom_fax;
        private ServiceStationClient.ComponentUI.TextBoxEx txtaccess_code;
        private ServiceStationClient.ComponentUI.TextBoxEx txtservice_station_sap;
        private ServiceStationClient.ComponentUI.TextBoxEx txthotline;
        private ServiceStationClient.ComponentUI.TextBoxEx txtcom_email;
        private ServiceStationClient.ComponentUI.TextBoxEx txtunit_properties;
        private ServiceStationClient.ComponentUI.TextBoxEx txtrepair_qualification;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private ServiceStationClient.ComponentUI.TextBoxEx txtcom_contact;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private ServiceStationClient.ComponentUI.TextBoxEx txtcom_address;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private ServiceStationClient.ComponentUI.TextBoxEx txtcom_name;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tlpOpBtn;
        private ServiceStationClient.ComponentUI.ButtonEx btnSave;
        private ServiceStationClient.ComponentUI.ButtonEx btnCancel;
        private ServiceStationClient.ComponentUI.ButtonEx btnReg;
        private ServiceStationClient.ComponentUI.ButtonEx btnPrevStep;
        private ServiceStationClient.ComponentUI.ButtonEx btnNextStep;
        private System.Windows.Forms.Panel pnlStep2;
        private System.Windows.Forms.Panel pnlStep3;
        private System.Windows.Forms.Label label27;
        private ServiceStationClient.ComponentUI.TextBoxEx txts_pwd;
        private System.Windows.Forms.Label label29;
        private ServiceStationClient.ComponentUI.TextBoxEx txts_user;
        private System.Windows.Forms.Label label28;
        private ServiceStationClient.ComponentUI.TextBoxEx txtauthentication;
        private System.Windows.Forms.Label label23;
        private ServiceStationClient.ComponentUI.TextBoxEx txtgrant_authorization;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private ServiceStationClient.ComponentUI.TextBoxEx txtmachine_code_sequence;
        private System.Windows.Forms.Label label24;
        private ServiceStationClient.ComponentUI.TextBoxEx txtcom_name2;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private ServiceStationClient.ComponentUI.TextBoxEx txtsign_id;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label40;
    }
}