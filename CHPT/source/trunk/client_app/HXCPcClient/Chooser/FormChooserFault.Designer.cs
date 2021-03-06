﻿namespace HXCPcClient.Chooser
{
    partial class FormChooserFault
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChooserFault));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.btnSubmit = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.txt_part_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_assembly_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_system_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlSearch = new ServiceStationClient.ComponentUI.PanelEx();
            this.tabControlEx1 = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvCompany = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.winFormPager1 = new ServiceStationClient.ComponentUI.WinFormPager();
            this.drchk_check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.drtxt_system_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_system_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_assembly_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_assembly_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_part_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_part_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlContainer.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.tabControlEx1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompany)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlContainer.Controls.Add(this.panelEx1);
            this.pnlContainer.Location = new System.Drawing.Point(1, 28);
            this.pnlContainer.Size = new System.Drawing.Size(663, 390);
            // 
            // panelEx1
            // 
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx1.BorderWidth = 1;
            this.panelEx1.Controls.Add(this.btnSubmit);
            this.panelEx1.Controls.Add(this.btnClear);
            this.panelEx1.Controls.Add(this.txt_part_name);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.txt_assembly_name);
            this.panelEx1.Controls.Add(this.label4);
            this.panelEx1.Controls.Add(this.txt_system_name);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Controls.Add(this.pnlSearch);
            this.panelEx1.Controls.Add(this.winFormPager1);
            this.panelEx1.Curvature = 0;
            this.panelEx1.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(663, 390);
            this.panelEx1.TabIndex = 10;
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSubmit.Caption = "查询";
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSubmit.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSubmit.Location = new System.Drawing.Point(574, 20);
            this.btnSubmit.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSubmit.Size = new System.Drawing.Size(80, 24);
            this.btnSubmit.TabIndex = 25;
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnClear.Location = new System.Drawing.Point(574, 56);
            this.btnClear.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnClear.Size = new System.Drawing.Size(80, 24);
            this.btnClear.TabIndex = 24;
            // 
            // txt_part_name
            // 
            this.txt_part_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txt_part_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txt_part_name.BackColor = System.Drawing.Color.Transparent;
            this.txt_part_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txt_part_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txt_part_name.ForeImage = null;
            this.txt_part_name.Location = new System.Drawing.Point(124, 56);
            this.txt_part_name.MaxLengh = 32767;
            this.txt_part_name.Multiline = false;
            this.txt_part_name.Name = "txt_part_name";
            this.txt_part_name.Radius = 3;
            this.txt_part_name.ReadOnly = false;
            this.txt_part_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txt_part_name.ShowError = false;
            this.txt_part_name.Size = new System.Drawing.Size(120, 23);
            this.txt_part_name.TabIndex = 23;
            this.txt_part_name.UseSystemPasswordChar = false;
            this.txt_part_name.Value = "";
            this.txt_part_name.VerifyCondition = null;
            this.txt_part_name.VerifyType = null;
            this.txt_part_name.WaterMark = null;
            this.txt_part_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 22;
            this.label2.Text = "故障部件名称：";
            // 
            // txt_assembly_name
            // 
            this.txt_assembly_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txt_assembly_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txt_assembly_name.BackColor = System.Drawing.Color.Transparent;
            this.txt_assembly_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txt_assembly_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txt_assembly_name.ForeImage = null;
            this.txt_assembly_name.Location = new System.Drawing.Point(380, 20);
            this.txt_assembly_name.MaxLengh = 32767;
            this.txt_assembly_name.Multiline = false;
            this.txt_assembly_name.Name = "txt_assembly_name";
            this.txt_assembly_name.Radius = 3;
            this.txt_assembly_name.ReadOnly = false;
            this.txt_assembly_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txt_assembly_name.ShowError = false;
            this.txt_assembly_name.Size = new System.Drawing.Size(120, 23);
            this.txt_assembly_name.TabIndex = 21;
            this.txt_assembly_name.UseSystemPasswordChar = false;
            this.txt_assembly_name.Value = "";
            this.txt_assembly_name.VerifyCondition = null;
            this.txt_assembly_name.VerifyType = null;
            this.txt_assembly_name.WaterMark = null;
            this.txt_assembly_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(283, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "故障总成名称：";
            // 
            // txt_system_name
            // 
            this.txt_system_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txt_system_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txt_system_name.BackColor = System.Drawing.Color.Transparent;
            this.txt_system_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txt_system_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txt_system_name.ForeImage = null;
            this.txt_system_name.Location = new System.Drawing.Point(124, 20);
            this.txt_system_name.MaxLengh = 32767;
            this.txt_system_name.Multiline = false;
            this.txt_system_name.Name = "txt_system_name";
            this.txt_system_name.Radius = 3;
            this.txt_system_name.ReadOnly = false;
            this.txt_system_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txt_system_name.ShowError = false;
            this.txt_system_name.Size = new System.Drawing.Size(120, 23);
            this.txt_system_name.TabIndex = 19;
            this.txt_system_name.UseSystemPasswordChar = false;
            this.txt_system_name.Value = "";
            this.txt_system_name.VerifyCondition = null;
            this.txt_system_name.VerifyType = null;
            this.txt_system_name.WaterMark = null;
            this.txt_system_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "故障类型名称：";
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlSearch.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSearch.BorderWidth = 1;
            this.pnlSearch.Controls.Add(this.tabControlEx1);
            this.pnlSearch.Curvature = 0;
            this.pnlSearch.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.pnlSearch.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.pnlSearch.Location = new System.Drawing.Point(5, 86);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(3);
            this.pnlSearch.Size = new System.Drawing.Size(654, 259);
            this.pnlSearch.TabIndex = 11;
            // 
            // tabControlEx1
            // 
            this.tabControlEx1.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tabControlEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.tabControlEx1.Controls.Add(this.tabPage1);
            this.tabControlEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlEx1.Location = new System.Drawing.Point(3, 3);
            this.tabControlEx1.Name = "tabControlEx1";
            this.tabControlEx1.SelectedIndex = 0;
            this.tabControlEx1.Size = new System.Drawing.Size(648, 253);
            this.tabControlEx1.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvCompany);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(640, 223);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "故障系统信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvCompany
            // 
            this.dgvCompany.AllowUserToAddRows = false;
            this.dgvCompany.AllowUserToDeleteRows = false;
            this.dgvCompany.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvCompany.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCompany.BackgroundColor = System.Drawing.Color.White;
            this.dgvCompany.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCompany.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCompany.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCompany.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.drchk_check,
            this.drtxt_system_code,
            this.drtxt_system_name,
            this.drtxt_assembly_code,
            this.drtxt_assembly_name,
            this.drtxt_part_code,
            this.drtxt_part_name});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCompany.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCompany.EnableHeadersVisualStyles = false;
            this.dgvCompany.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvCompany.Location = new System.Drawing.Point(3, 3);
            this.dgvCompany.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvCompany.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvCompany.MergeColumnNames")));
            this.dgvCompany.MultiSelect = false;
            this.dgvCompany.Name = "dgvCompany";
            this.dgvCompany.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCompany.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvCompany.RowHeadersVisible = false;
            this.dgvCompany.RowHeadersWidth = 30;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvCompany.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvCompany.RowTemplate.Height = 23;
            this.dgvCompany.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCompany.ShowCheckBox = true;
            this.dgvCompany.Size = new System.Drawing.Size(634, 217);
            this.dgvCompany.TabIndex = 0;
            // 
            // winFormPager1
            // 
            this.winFormPager1.BackColor = System.Drawing.Color.Transparent;
            this.winFormPager1.BtnTextNext = "下页";
            this.winFormPager1.BtnTextPrevious = "上页";
            this.winFormPager1.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.winFormPager1.Location = new System.Drawing.Point(187, 351);
            this.winFormPager1.Name = "winFormPager1";
            this.winFormPager1.PageCount = 0;
            this.winFormPager1.PageSize = 15;
            this.winFormPager1.RecordCount = 0;
            this.winFormPager1.Size = new System.Drawing.Size(469, 31);
            this.winFormPager1.TabIndex = 10;
            this.winFormPager1.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            // 
            // drchk_check
            // 
            this.drchk_check.HeaderText = "";
            this.drchk_check.MinimumWidth = 30;
            this.drchk_check.Name = "drchk_check";
            this.drchk_check.ReadOnly = true;
            this.drchk_check.Width = 30;
            // 
            // drtxt_system_code
            // 
            this.drtxt_system_code.DataPropertyName = "system_code";
            this.drtxt_system_code.HeaderText = "故障系统编码";
            this.drtxt_system_code.Name = "drtxt_system_code";
            this.drtxt_system_code.ReadOnly = true;
            this.drtxt_system_code.Width = 120;
            // 
            // drtxt_system_name
            // 
            this.drtxt_system_name.DataPropertyName = "system_name";
            this.drtxt_system_name.HeaderText = "故障系统名称";
            this.drtxt_system_name.Name = "drtxt_system_name";
            this.drtxt_system_name.ReadOnly = true;
            this.drtxt_system_name.Width = 120;
            // 
            // drtxt_assembly_code
            // 
            this.drtxt_assembly_code.DataPropertyName = "assembly_code";
            this.drtxt_assembly_code.HeaderText = "故障总成编码";
            this.drtxt_assembly_code.Name = "drtxt_assembly_code";
            this.drtxt_assembly_code.ReadOnly = true;
            this.drtxt_assembly_code.Width = 120;
            // 
            // drtxt_assembly_name
            // 
            this.drtxt_assembly_name.DataPropertyName = "assembly_name";
            this.drtxt_assembly_name.HeaderText = "故障总成名称";
            this.drtxt_assembly_name.Name = "drtxt_assembly_name";
            this.drtxt_assembly_name.ReadOnly = true;
            this.drtxt_assembly_name.Width = 120;
            // 
            // drtxt_part_code
            // 
            this.drtxt_part_code.DataPropertyName = "part_code";
            this.drtxt_part_code.HeaderText = "故障部件编码";
            this.drtxt_part_code.Name = "drtxt_part_code";
            this.drtxt_part_code.ReadOnly = true;
            this.drtxt_part_code.Width = 120;
            // 
            // drtxt_part_name
            // 
            this.drtxt_part_name.DataPropertyName = "part_name";
            this.drtxt_part_name.HeaderText = "故障部件名称";
            this.drtxt_part_name.Name = "drtxt_part_name";
            this.drtxt_part_name.ReadOnly = true;
            this.drtxt_part_name.Width = 120;
            // 
            // FormChooserFault
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 422);
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximumSize = new System.Drawing.Size(1600, 794);
            this.Name = "FormChooserFault";
            this.Text = "故障系统选择";
            this.pnlContainer.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            this.tabControlEx1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompany)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private ServiceStationClient.ComponentUI.ButtonEx btnSubmit;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.TextBoxEx txt_part_name;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.TextBoxEx txt_assembly_name;
        private System.Windows.Forms.Label label4;
        private ServiceStationClient.ComponentUI.TextBoxEx txt_system_name;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.PanelEx pnlSearch;
        private ServiceStationClient.ComponentUI.WinFormPager winFormPager1;
        private ServiceStationClient.ComponentUI.TabControlEx tabControlEx1;
        private System.Windows.Forms.TabPage tabPage1;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvCompany;
        private System.Windows.Forms.DataGridViewCheckBoxColumn drchk_check;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_system_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_system_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_assembly_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_assembly_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_part_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_part_name;


    }
}