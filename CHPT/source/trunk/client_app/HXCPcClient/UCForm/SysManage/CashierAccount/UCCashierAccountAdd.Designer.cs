namespace HXCPcClient.UCForm.SysManage.CashierAccount
{
    partial class UCCashierAccountAdd
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
            this.rboDisable = new System.Windows.Forms.RadioButton();
            this.rboEnable = new System.Windows.Forms.RadioButton();
            this.cboBank = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.txtAccountName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboAccountType = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.txtBankAccount = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.SuspendLayout();
            // 
            // rboDisable
            // 
            this.rboDisable.AutoSize = true;
            this.rboDisable.Location = new System.Drawing.Point(232, 176);
            this.rboDisable.Name = "rboDisable";
            this.rboDisable.Size = new System.Drawing.Size(47, 16);
            this.rboDisable.TabIndex = 16;
            this.rboDisable.Text = "停用";
            this.rboDisable.UseVisualStyleBackColor = true;
            // 
            // rboEnable
            // 
            this.rboEnable.AutoSize = true;
            this.rboEnable.Checked = true;
            this.rboEnable.Location = new System.Drawing.Point(148, 176);
            this.rboEnable.Name = "rboEnable";
            this.rboEnable.Size = new System.Drawing.Size(47, 16);
            this.rboEnable.TabIndex = 15;
            this.rboEnable.TabStop = true;
            this.rboEnable.Text = "启用";
            this.rboEnable.UseVisualStyleBackColor = true;
            // 
            // cboBank
            // 
            this.cboBank.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBank.FormattingEnabled = true;
            this.cboBank.Location = new System.Drawing.Point(139, 124);
            this.cboBank.Name = "cboBank";
            this.cboBank.Size = new System.Drawing.Size(171, 22);
            this.cboBank.TabIndex = 14;
            this.cboBank.SelectedIndexChanged += new System.EventHandler(this.cboBank_SelectedIndexChanged);
            // 
            // txtAccountName
            // 
            this.txtAccountName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtAccountName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtAccountName.BackColor = System.Drawing.Color.Transparent;
            this.txtAccountName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtAccountName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtAccountName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAccountName.ForeImage = null;
            this.txtAccountName.InputtingVerifyCondition = null;
            this.txtAccountName.Location = new System.Drawing.Point(139, 75);
            this.txtAccountName.MaxLengh = 25;
            this.txtAccountName.Multiline = false;
            this.txtAccountName.Name = "txtAccountName";
            this.txtAccountName.Radius = 3;
            this.txtAccountName.ReadOnly = false;
            this.txtAccountName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtAccountName.ShowError = false;
            this.txtAccountName.Size = new System.Drawing.Size(171, 23);
            this.txtAccountName.TabIndex = 13;
            this.txtAccountName.UseSystemPasswordChar = false;
            this.txtAccountName.Value = "";
            this.txtAccountName.VerifyCondition = null;
            this.txtAccountName.VerifyType = null;
            this.txtAccountName.VerifyTypeName = null;
            this.txtAccountName.WaterMark = null;
            this.txtAccountName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(92, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "状态：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(68, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "银行名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "账户名称：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(467, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "账户类型：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(467, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 18;
            this.label5.Text = "银行账号：";
            // 
            // cboAccountType
            // 
            this.cboAccountType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboAccountType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAccountType.FormattingEnabled = true;
            this.cboAccountType.Location = new System.Drawing.Point(538, 75);
            this.cboAccountType.Name = "cboAccountType";
            this.cboAccountType.Size = new System.Drawing.Size(202, 22);
            this.cboAccountType.TabIndex = 19;
            // 
            // txtBankAccount
            // 
            this.txtBankAccount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtBankAccount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtBankAccount.BackColor = System.Drawing.Color.Transparent;
            this.txtBankAccount.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtBankAccount.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtBankAccount.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBankAccount.ForeImage = null;
            this.txtBankAccount.InputtingVerifyCondition = null;
            this.txtBankAccount.Location = new System.Drawing.Point(538, 124);
            this.txtBankAccount.MaxLengh = 32767;
            this.txtBankAccount.Multiline = false;
            this.txtBankAccount.Name = "txtBankAccount";
            this.txtBankAccount.Radius = 3;
            this.txtBankAccount.ReadOnly = true;
            this.txtBankAccount.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtBankAccount.ShowError = false;
            this.txtBankAccount.Size = new System.Drawing.Size(202, 23);
            this.txtBankAccount.TabIndex = 20;
            this.txtBankAccount.UseSystemPasswordChar = false;
            this.txtBankAccount.Value = "";
            this.txtBankAccount.VerifyCondition = null;
            this.txtBankAccount.VerifyType = null;
            this.txtBankAccount.VerifyTypeName = null;
            this.txtBankAccount.WaterMark = null;
            this.txtBankAccount.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // UCCashierAccountAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtBankAccount);
            this.Controls.Add(this.cboAccountType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rboDisable);
            this.Controls.Add(this.rboEnable);
            this.Controls.Add(this.cboBank);
            this.Controls.Add(this.txtAccountName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "UCCashierAccountAdd";
            this.Load += new System.EventHandler(this.UCCashierAccountAdd_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtAccountName, 0);
            this.Controls.SetChildIndex(this.cboBank, 0);
            this.Controls.SetChildIndex(this.rboEnable, 0);
            this.Controls.SetChildIndex(this.rboDisable, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.cboAccountType, 0);
            this.Controls.SetChildIndex(this.txtBankAccount, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rboDisable;
        private System.Windows.Forms.RadioButton rboEnable;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboBank;
        private ServiceStationClient.ComponentUI.TextBoxEx txtAccountName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboAccountType;
        private ServiceStationClient.ComponentUI.TextBoxEx txtBankAccount;
    }
}
