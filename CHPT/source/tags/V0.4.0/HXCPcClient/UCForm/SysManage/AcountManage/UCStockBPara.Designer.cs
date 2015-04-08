namespace HXCPcClient.UCForm.SysManage.AcountManage
{
    partial class UCStockBPara
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
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.label15 = new System.Windows.Forms.Label();
            this.chkwarehous_single_reference = new System.Windows.Forms.CheckBox();
            this.nudprice = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chkprice_zero = new System.Windows.Forms.CheckBox();
            this.chkcounts_zero = new System.Windows.Forms.CheckBox();
            this.nudcounts = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.rdbfifo_method = new System.Windows.Forms.RadioButton();
            this.rdbmoving_average_method = new System.Windows.Forms.RadioButton();
            this.rdbmonthly_average_method = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.chksingle_delete_same_person = new System.Windows.Forms.CheckBox();
            this.chksingle_disabled_same_person = new System.Windows.Forms.CheckBox();
            this.chksingle_audit_same_person = new System.Windows.Forms.CheckBox();
            this.chksingle_editors_same_person = new System.Windows.Forms.CheckBox();
            this.chkallow_zero_lib_junction = new System.Windows.Forms.CheckBox();
            this.chkallow_zero_lib_stock = new System.Windows.Forms.CheckBox();
            this.chkmaking_audit_one_person = new System.Windows.Forms.CheckBox();
            this.chkstorage_manage = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudprice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudcounts)).BeginInit();
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
            this.panelEx1.Controls.Add(this.chkwarehous_single_reference);
            this.panelEx1.Controls.Add(this.nudprice);
            this.panelEx1.Controls.Add(this.label5);
            this.panelEx1.Controls.Add(this.label4);
            this.panelEx1.Controls.Add(this.chkprice_zero);
            this.panelEx1.Controls.Add(this.chkcounts_zero);
            this.panelEx1.Controls.Add(this.nudcounts);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.rdbfifo_method);
            this.panelEx1.Controls.Add(this.rdbmoving_average_method);
            this.panelEx1.Controls.Add(this.rdbmonthly_average_method);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.chksingle_delete_same_person);
            this.panelEx1.Controls.Add(this.chksingle_disabled_same_person);
            this.panelEx1.Controls.Add(this.chksingle_audit_same_person);
            this.panelEx1.Controls.Add(this.chksingle_editors_same_person);
            this.panelEx1.Controls.Add(this.chkallow_zero_lib_junction);
            this.panelEx1.Controls.Add(this.chkallow_zero_lib_stock);
            this.panelEx1.Controls.Add(this.chkmaking_audit_one_person);
            this.panelEx1.Controls.Add(this.chkstorage_manage);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Location = new System.Drawing.Point(0, 26);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1030, 518);
            this.panelEx1.TabIndex = 10;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.label15.Location = new System.Drawing.Point(0, 52);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1030, 1);
            this.label15.TabIndex = 42;
            // 
            // chkwarehous_single_reference
            // 
            this.chkwarehous_single_reference.AutoSize = true;
            this.chkwarehous_single_reference.Enabled = false;
            this.chkwarehous_single_reference.Location = new System.Drawing.Point(540, 234);
            this.chkwarehous_single_reference.Name = "chkwarehous_single_reference";
            this.chkwarehous_single_reference.Size = new System.Drawing.Size(156, 16);
            this.chkwarehous_single_reference.TabIndex = 23;
            this.chkwarehous_single_reference.Text = "启用出入库单无引用生成";
            this.chkwarehous_single_reference.UseVisualStyleBackColor = true;
            // 
            // nudprice
            // 
            this.nudprice.Location = new System.Drawing.Point(588, 134);
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
            this.label5.Location = new System.Drawing.Point(547, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "价格:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(547, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "数量:";
            // 
            // chkprice_zero
            // 
            this.chkprice_zero.AutoSize = true;
            this.chkprice_zero.Enabled = false;
            this.chkprice_zero.Location = new System.Drawing.Point(676, 136);
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
            this.chkcounts_zero.Location = new System.Drawing.Point(676, 105);
            this.chkcounts_zero.Name = "chkcounts_zero";
            this.chkcounts_zero.Size = new System.Drawing.Size(48, 16);
            this.chkcounts_zero.TabIndex = 18;
            this.chkcounts_zero.Text = "补零";
            this.chkcounts_zero.UseVisualStyleBackColor = true;
            // 
            // nudcounts
            // 
            this.nudcounts.Location = new System.Drawing.Point(588, 99);
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
            this.label3.Location = new System.Drawing.Point(538, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "小数位数设置";
            // 
            // rdbfifo_method
            // 
            this.rdbfifo_method.AutoSize = true;
            this.rdbfifo_method.Enabled = false;
            this.rdbfifo_method.Location = new System.Drawing.Point(355, 162);
            this.rdbfifo_method.Name = "rdbfifo_method";
            this.rdbfifo_method.Size = new System.Drawing.Size(83, 16);
            this.rdbfifo_method.TabIndex = 13;
            this.rdbfifo_method.Text = "先进先出法";
            this.rdbfifo_method.UseVisualStyleBackColor = true;
            // 
            // rdbmoving_average_method
            // 
            this.rdbmoving_average_method.AutoSize = true;
            this.rdbmoving_average_method.Checked = true;
            this.rdbmoving_average_method.Enabled = false;
            this.rdbmoving_average_method.Location = new System.Drawing.Point(355, 130);
            this.rdbmoving_average_method.Name = "rdbmoving_average_method";
            this.rdbmoving_average_method.Size = new System.Drawing.Size(107, 16);
            this.rdbmoving_average_method.TabIndex = 12;
            this.rdbmoving_average_method.TabStop = true;
            this.rdbmoving_average_method.Text = "移动加权平均法";
            this.rdbmoving_average_method.UseVisualStyleBackColor = true;
            // 
            // rdbmonthly_average_method
            // 
            this.rdbmonthly_average_method.AutoSize = true;
            this.rdbmonthly_average_method.Enabled = false;
            this.rdbmonthly_average_method.Location = new System.Drawing.Point(355, 100);
            this.rdbmonthly_average_method.Name = "rdbmonthly_average_method";
            this.rdbmonthly_average_method.Size = new System.Drawing.Size(95, 16);
            this.rdbmonthly_average_method.TabIndex = 11;
            this.rdbmonthly_average_method.Text = "月加权平均法";
            this.rdbmonthly_average_method.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(331, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "成本计算方式";
            // 
            // chksingle_delete_same_person
            // 
            this.chksingle_delete_same_person.AutoSize = true;
            this.chksingle_delete_same_person.Enabled = false;
            this.chksingle_delete_same_person.Location = new System.Drawing.Point(46, 327);
            this.chksingle_delete_same_person.Name = "chksingle_delete_same_person";
            this.chksingle_delete_same_person.Size = new System.Drawing.Size(180, 16);
            this.chksingle_delete_same_person.TabIndex = 9;
            this.chksingle_delete_same_person.Text = "制单人和删除人可以为同一人";
            this.chksingle_delete_same_person.UseVisualStyleBackColor = true;
            // 
            // chksingle_disabled_same_person
            // 
            this.chksingle_disabled_same_person.AutoSize = true;
            this.chksingle_disabled_same_person.Enabled = false;
            this.chksingle_disabled_same_person.Location = new System.Drawing.Point(46, 294);
            this.chksingle_disabled_same_person.Name = "chksingle_disabled_same_person";
            this.chksingle_disabled_same_person.Size = new System.Drawing.Size(180, 16);
            this.chksingle_disabled_same_person.TabIndex = 8;
            this.chksingle_disabled_same_person.Text = "制单人和作废人可以为同一人";
            this.chksingle_disabled_same_person.UseVisualStyleBackColor = true;
            // 
            // chksingle_audit_same_person
            // 
            this.chksingle_audit_same_person.AutoSize = true;
            this.chksingle_audit_same_person.Enabled = false;
            this.chksingle_audit_same_person.Location = new System.Drawing.Point(46, 263);
            this.chksingle_audit_same_person.Name = "chksingle_audit_same_person";
            this.chksingle_audit_same_person.Size = new System.Drawing.Size(180, 16);
            this.chksingle_audit_same_person.TabIndex = 7;
            this.chksingle_audit_same_person.Text = "制单人和审核人可以为同一人";
            this.chksingle_audit_same_person.UseVisualStyleBackColor = true;
            // 
            // chksingle_editors_same_person
            // 
            this.chksingle_editors_same_person.AutoSize = true;
            this.chksingle_editors_same_person.Enabled = false;
            this.chksingle_editors_same_person.Location = new System.Drawing.Point(46, 234);
            this.chksingle_editors_same_person.Name = "chksingle_editors_same_person";
            this.chksingle_editors_same_person.Size = new System.Drawing.Size(180, 16);
            this.chksingle_editors_same_person.TabIndex = 6;
            this.chksingle_editors_same_person.Text = "制单人和编辑人可以为同一人";
            this.chksingle_editors_same_person.UseVisualStyleBackColor = true;
            // 
            // chkallow_zero_lib_junction
            // 
            this.chkallow_zero_lib_junction.AutoSize = true;
            this.chkallow_zero_lib_junction.Enabled = false;
            this.chkallow_zero_lib_junction.Location = new System.Drawing.Point(46, 179);
            this.chkallow_zero_lib_junction.Name = "chkallow_zero_lib_junction";
            this.chkallow_zero_lib_junction.Size = new System.Drawing.Size(192, 16);
            this.chkallow_zero_lib_junction.TabIndex = 4;
            this.chkallow_zero_lib_junction.Text = "允许期末零（负）库存账面结账";
            this.chkallow_zero_lib_junction.UseVisualStyleBackColor = true;
            // 
            // chkallow_zero_lib_stock
            // 
            this.chkallow_zero_lib_stock.AutoSize = true;
            this.chkallow_zero_lib_stock.Enabled = false;
            this.chkallow_zero_lib_stock.Location = new System.Drawing.Point(46, 148);
            this.chkallow_zero_lib_stock.Name = "chkallow_zero_lib_stock";
            this.chkallow_zero_lib_stock.Size = new System.Drawing.Size(168, 16);
            this.chkallow_zero_lib_stock.TabIndex = 3;
            this.chkallow_zero_lib_stock.Text = "允许账面零（负）库存出库";
            this.chkallow_zero_lib_stock.UseVisualStyleBackColor = true;
            // 
            // chkmaking_audit_one_person
            // 
            this.chkmaking_audit_one_person.AutoSize = true;
            this.chkmaking_audit_one_person.Enabled = false;
            this.chkmaking_audit_one_person.Location = new System.Drawing.Point(46, 119);
            this.chkmaking_audit_one_person.Name = "chkmaking_audit_one_person";
            this.chkmaking_audit_one_person.Size = new System.Drawing.Size(132, 16);
            this.chkmaking_audit_one_person.TabIndex = 2;
            this.chkmaking_audit_one_person.Text = "制单和审核为同一人";
            this.chkmaking_audit_one_person.UseVisualStyleBackColor = true;
            // 
            // chkstorage_manage
            // 
            this.chkstorage_manage.AutoSize = true;
            this.chkstorage_manage.Enabled = false;
            this.chkstorage_manage.Location = new System.Drawing.Point(46, 70);
            this.chkstorage_manage.Name = "chkstorage_manage";
            this.chkstorage_manage.Size = new System.Drawing.Size(96, 16);
            this.chkstorage_manage.TabIndex = 1;
            this.chkstorage_manage.Text = "启用货位管理";
            this.chkstorage_manage.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(41, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "库存业务参数";
            // 
            // UCStockBPara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelEx1);
            this.Name = "UCStockBPara";
            this.Load += new System.EventHandler(this.UCStockBPara_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.panelEx1, 0);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudprice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudcounts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private System.Windows.Forms.RadioButton rdbmoving_average_method;
        private System.Windows.Forms.RadioButton rdbmonthly_average_method;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chksingle_delete_same_person;
        private System.Windows.Forms.CheckBox chksingle_disabled_same_person;
        private System.Windows.Forms.CheckBox chksingle_audit_same_person;
        private System.Windows.Forms.CheckBox chksingle_editors_same_person;
        private System.Windows.Forms.CheckBox chkallow_zero_lib_junction;
        private System.Windows.Forms.CheckBox chkallow_zero_lib_stock;
        private System.Windows.Forms.CheckBox chkmaking_audit_one_person;
        private System.Windows.Forms.CheckBox chkstorage_manage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdbfifo_method;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkprice_zero;
        private System.Windows.Forms.CheckBox chkcounts_zero;
        private System.Windows.Forms.NumericUpDown nudcounts;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudprice;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkwarehous_single_reference;
        private System.Windows.Forms.Label label15;
    }
}
