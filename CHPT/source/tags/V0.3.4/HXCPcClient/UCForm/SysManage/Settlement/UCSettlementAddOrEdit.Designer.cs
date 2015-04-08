namespace HXCPcClient.UCForm.SysManage.Settlement
{
    partial class UCSettlementAddOrEdit
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboDefaultAccount = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.rboEnable = new System.Windows.Forms.RadioButton();
            this.rboDisable = new System.Windows.Forms.RadioButton();
            this.cmbJSFS = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(69, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "结算方式：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "默认结算账户：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(93, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "状态：";
            // 
            // cboDefaultAccount
            // 
            this.cboDefaultAccount.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDefaultAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDefaultAccount.FormattingEnabled = true;
            this.cboDefaultAccount.Location = new System.Drawing.Point(140, 123);
            this.cboDefaultAccount.Name = "cboDefaultAccount";
            this.cboDefaultAccount.Size = new System.Drawing.Size(205, 22);
            this.cboDefaultAccount.TabIndex = 7;
            // 
            // rboEnable
            // 
            this.rboEnable.AutoSize = true;
            this.rboEnable.Checked = true;
            this.rboEnable.Location = new System.Drawing.Point(149, 175);
            this.rboEnable.Name = "rboEnable";
            this.rboEnable.Size = new System.Drawing.Size(47, 16);
            this.rboEnable.TabIndex = 8;
            this.rboEnable.TabStop = true;
            this.rboEnable.Text = "启用";
            this.rboEnable.UseVisualStyleBackColor = true;
            // 
            // rboDisable
            // 
            this.rboDisable.AutoSize = true;
            this.rboDisable.Location = new System.Drawing.Point(233, 175);
            this.rboDisable.Name = "rboDisable";
            this.rboDisable.Size = new System.Drawing.Size(47, 16);
            this.rboDisable.TabIndex = 9;
            this.rboDisable.Text = "停用";
            this.rboDisable.UseVisualStyleBackColor = true;
            // 
            // cmbJSFS
            // 
            this.cmbJSFS.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbJSFS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJSFS.FormattingEnabled = true;
            this.cmbJSFS.Location = new System.Drawing.Point(140, 76);
            this.cmbJSFS.Name = "cmbJSFS";
            this.cmbJSFS.Size = new System.Drawing.Size(205, 22);
            this.cmbJSFS.TabIndex = 10;
            // 
            // UCSettlementAddOrEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.cmbJSFS);
            this.Controls.Add(this.rboDisable);
            this.Controls.Add(this.rboEnable);
            this.Controls.Add(this.cboDefaultAccount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "UCSettlementAddOrEdit";
            this.Load += new System.EventHandler(this.UCSettlementAddOrEdit_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.cboDefaultAccount, 0);
            this.Controls.SetChildIndex(this.rboEnable, 0);
            this.Controls.SetChildIndex(this.rboDisable, 0);
            this.Controls.SetChildIndex(this.cmbJSFS, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboDefaultAccount;
        private System.Windows.Forms.RadioButton rboEnable;
        private System.Windows.Forms.RadioButton rboDisable;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbJSFS;
    }
}
