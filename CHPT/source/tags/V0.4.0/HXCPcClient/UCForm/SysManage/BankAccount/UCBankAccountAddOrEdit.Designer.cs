namespace HXCPcClient.UCForm.SysManage.BankAccount
{
    partial class UCBankAccountAddOrEdit
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBankName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtBankAccount = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.rboEnable = new System.Windows.Forms.RadioButton();
            this.rboDisable = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "银行名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "银行账户：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(82, 183);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "状态：";
            // 
            // txtBankName
            // 
            this.txtBankName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtBankName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtBankName.BackColor = System.Drawing.Color.Transparent;
            this.txtBankName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtBankName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtBankName.ForeImage = null;
            this.txtBankName.Location = new System.Drawing.Point(129, 70);
            this.txtBankName.MaxLengh = 32767;
            this.txtBankName.Multiline = false;
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Radius = 3;
            this.txtBankName.ReadOnly = false;
            this.txtBankName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtBankName.Size = new System.Drawing.Size(236, 23);
            this.txtBankName.TabIndex = 6;
            this.txtBankName.UseSystemPasswordChar = false;
            this.txtBankName.WaterMark = null;
            this.txtBankName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtBankAccount
            // 
            this.txtBankAccount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtBankAccount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtBankAccount.BackColor = System.Drawing.Color.Transparent;
            this.txtBankAccount.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtBankAccount.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtBankAccount.ForeImage = null;
            this.txtBankAccount.Location = new System.Drawing.Point(129, 124);
            this.txtBankAccount.MaxLengh = 32767;
            this.txtBankAccount.Multiline = false;
            this.txtBankAccount.Name = "txtBankAccount";
            this.txtBankAccount.Radius = 3;
            this.txtBankAccount.ReadOnly = false;
            this.txtBankAccount.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtBankAccount.Size = new System.Drawing.Size(236, 23);
            this.txtBankAccount.TabIndex = 7;
            this.txtBankAccount.UseSystemPasswordChar = false;
            this.txtBankAccount.WaterMark = null;
            this.txtBankAccount.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // rboEnable
            // 
            this.rboEnable.AutoSize = true;
            this.rboEnable.Checked = true;
            this.rboEnable.Location = new System.Drawing.Point(148, 181);
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
            this.rboDisable.Location = new System.Drawing.Point(240, 181);
            this.rboDisable.Name = "rboDisable";
            this.rboDisable.Size = new System.Drawing.Size(47, 16);
            this.rboDisable.TabIndex = 9;
            this.rboDisable.TabStop = true;
            this.rboDisable.Text = "停用";
            this.rboDisable.UseVisualStyleBackColor = true;
            // 
            // UCBankAccountAddOrEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rboDisable);
            this.Controls.Add(this.rboEnable);
            this.Controls.Add(this.txtBankAccount);
            this.Controls.Add(this.txtBankName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "UCBankAccountAddOrEdit";
            this.Load += new System.EventHandler(this.UCBankAccountAddOrEdit_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtBankName, 0);
            this.Controls.SetChildIndex(this.txtBankAccount, 0);
            this.Controls.SetChildIndex(this.rboEnable, 0);
            this.Controls.SetChildIndex(this.rboDisable, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private ServiceStationClient.ComponentUI.TextBoxEx txtBankName;
        private ServiceStationClient.ComponentUI.TextBoxEx txtBankAccount;
        private System.Windows.Forms.RadioButton rboEnable;
        private System.Windows.Forms.RadioButton rboDisable;
    }
}
