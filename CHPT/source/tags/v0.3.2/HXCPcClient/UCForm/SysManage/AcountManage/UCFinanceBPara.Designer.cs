namespace HXCPcClient.UCForm.SysManage.AcountManage
{
    partial class UCFinanceBPara
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
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.label6 = new System.Windows.Forms.Label();
            this.cbocurrency = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.txttax_rate = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.nudprice = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chkprice_zero = new System.Windows.Forms.CheckBox();
            this.chkcounts_zero = new System.Windows.Forms.CheckBox();
            this.nudcounts = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label15 = new System.Windows.Forms.Label();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudprice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudcounts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.Controls.Add(this.label15);
            this.panelEx1.Controls.Add(this.label6);
            this.panelEx1.Controls.Add(this.cbocurrency);
            this.panelEx1.Controls.Add(this.txttax_rate);
            this.panelEx1.Controls.Add(this.nudprice);
            this.panelEx1.Controls.Add(this.label5);
            this.panelEx1.Controls.Add(this.label4);
            this.panelEx1.Controls.Add(this.chkprice_zero);
            this.panelEx1.Controls.Add(this.chkcounts_zero);
            this.panelEx1.Controls.Add(this.nudcounts);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Location = new System.Drawing.Point(0, 26);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1030, 518);
            this.panelEx1.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(185, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 25;
            this.label6.Text = "%";
            // 
            // cbocurrency
            // 
            this.cbocurrency.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbocurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbocurrency.Enabled = false;
            this.cbocurrency.FormattingEnabled = true;
            this.cbocurrency.Location = new System.Drawing.Point(369, 78);
            this.cbocurrency.Name = "cbocurrency";
            this.cbocurrency.Size = new System.Drawing.Size(121, 22);
            this.cbocurrency.TabIndex = 24;
            // 
            // txttax_rate
            // 
            this.txttax_rate.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txttax_rate.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txttax_rate.BackColor = System.Drawing.Color.Transparent;
            this.txttax_rate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txttax_rate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txttax_rate.Caption = "10";
            this.txttax_rate.ForeImage = null;
            this.txttax_rate.Location = new System.Drawing.Point(104, 77);
            this.txttax_rate.MaxLengh = 5;
            this.txttax_rate.Multiline = false;
            this.txttax_rate.Name = "txttax_rate";
            this.txttax_rate.Radius = 3;
            this.txttax_rate.ReadOnly = true;
            this.txttax_rate.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txttax_rate.Size = new System.Drawing.Size(79, 23);
            this.txttax_rate.TabIndex = 23;
            this.txttax_rate.UseSystemPasswordChar = false;
            this.txttax_rate.WaterMark = null;
            this.txttax_rate.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // nudprice
            // 
            this.nudprice.Location = new System.Drawing.Point(107, 164);
            this.nudprice.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudprice.Name = "nudprice";
            this.nudprice.ReadOnly = true;
            this.nudprice.Size = new System.Drawing.Size(75, 21);
            this.nudprice.TabIndex = 22;
            this.nudprice.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(44, 169);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "价格：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "数量：";
            // 
            // chkprice_zero
            // 
            this.chkprice_zero.AutoSize = true;
            this.chkprice_zero.Enabled = false;
            this.chkprice_zero.Location = new System.Drawing.Point(195, 166);
            this.chkprice_zero.Name = "chkprice_zero";
            this.chkprice_zero.Size = new System.Drawing.Size(48, 16);
            this.chkprice_zero.TabIndex = 19;
            this.chkprice_zero.Text = "补零";
            this.chkprice_zero.UseVisualStyleBackColor = true;
            // 
            // chkcounts_zero
            // 
            this.chkcounts_zero.AutoSize = true;
            this.chkcounts_zero.Enabled = false;
            this.chkcounts_zero.Location = new System.Drawing.Point(195, 135);
            this.chkcounts_zero.Name = "chkcounts_zero";
            this.chkcounts_zero.Size = new System.Drawing.Size(48, 16);
            this.chkcounts_zero.TabIndex = 18;
            this.chkcounts_zero.Text = "补零";
            this.chkcounts_zero.UseVisualStyleBackColor = true;
            // 
            // nudcounts
            // 
            this.nudcounts.Location = new System.Drawing.Point(107, 129);
            this.nudcounts.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudcounts.Name = "nudcounts";
            this.nudcounts.ReadOnly = true;
            this.nudcounts.Size = new System.Drawing.Size(75, 21);
            this.nudcounts.TabIndex = 17;
            this.nudcounts.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(286, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "本位币：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "税率：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(44, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "财务业务参数";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.label15.Location = new System.Drawing.Point(0, 52);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1030, 1);
            this.label15.TabIndex = 41;
            // 
            // UCFinanceBPara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.panelEx1);
            this.Name = "UCFinanceBPara";
            this.Load += new System.EventHandler(this.UCFinanceBPara_Load);
            this.Controls.SetChildIndex(this.panelEx1, 0);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudprice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudcounts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private System.Windows.Forms.NumericUpDown nudprice;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkprice_zero;
        private System.Windows.Forms.CheckBox chkcounts_zero;
        private System.Windows.Forms.NumericUpDown nudcounts;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.TextBoxEx txttax_rate;
        private ServiceStationClient.ComponentUI.ComboBoxEx cbocurrency;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label15;        
    }
}
