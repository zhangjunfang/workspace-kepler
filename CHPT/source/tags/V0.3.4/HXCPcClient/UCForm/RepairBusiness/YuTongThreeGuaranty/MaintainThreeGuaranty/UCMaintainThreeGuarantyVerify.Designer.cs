namespace HXCPcClient.UCForm.RepairBusiness.YuTongThreeGuaranty.MaintainThreeGuaranty
{
    partial class UCMaintainThreeGuarantyVerify
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCMaintainThreeGuarantyVerify));
            this.label1 = new System.Windows.Forms.Label();
            this.rdo_ture = new System.Windows.Forms.RadioButton();
            this.rdo_false = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_information = new System.Windows.Forms.TextBox();
            this.btn_ok = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btn_cancel = new ServiceStationClient.ComponentUI.ButtonEx();
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.btn_cancel);
            this.pnlContainer.Controls.Add(this.btn_ok);
            this.pnlContainer.Controls.Add(this.txt_information);
            this.pnlContainer.Controls.Add(this.label2);
            this.pnlContainer.Controls.Add(this.rdo_false);
            this.pnlContainer.Controls.Add(this.rdo_ture);
            this.pnlContainer.Controls.Add(this.label3);
            this.pnlContainer.Controls.Add(this.label1);
            this.pnlContainer.Location = new System.Drawing.Point(3, 30);
            this.pnlContainer.Size = new System.Drawing.Size(437, 245);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "您确认要";
            // 
            // rdo_ture
            // 
            this.rdo_ture.AutoSize = true;
            this.rdo_ture.Checked = true;
            this.rdo_ture.Location = new System.Drawing.Point(89, 16);
            this.rdo_ture.Name = "rdo_ture";
            this.rdo_ture.Size = new System.Drawing.Size(49, 17);
            this.rdo_ture.TabIndex = 1;
            this.rdo_ture.TabStop = true;
            this.rdo_ture.Tag = "verify";
            this.rdo_ture.Text = "通过";
            this.rdo_ture.UseVisualStyleBackColor = true;
            // 
            // rdo_false
            // 
            this.rdo_false.AutoSize = true;
            this.rdo_false.Location = new System.Drawing.Point(144, 16);
            this.rdo_false.Name = "rdo_false";
            this.rdo_false.Size = new System.Drawing.Size(49, 17);
            this.rdo_false.TabIndex = 1;
            this.rdo_false.Tag = "verify";
            this.rdo_false.Text = "驳回";
            this.rdo_false.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(199, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "此条信息吗?";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "审核意见:";
            // 
            // txt_information
            // 
            this.txt_information.Location = new System.Drawing.Point(89, 50);
            this.txt_information.Multiline = true;
            this.txt_information.Name = "txt_information";
            this.txt_information.Size = new System.Drawing.Size(329, 146);
            this.txt_information.TabIndex = 3;
            // 
            // btn_ok
            // 
            this.btn_ok.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_ok.BackgroundImage")));
            this.btn_ok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_ok.Caption = "确定";
            this.btn_ok.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_ok.DownImage = ((System.Drawing.Image)(resources.GetObject("btn_ok.DownImage")));
            this.btn_ok.Location = new System.Drawing.Point(251, 212);
            this.btn_ok.MoveImage = ((System.Drawing.Image)(resources.GetObject("btn_ok.MoveImage")));
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.NormalImage = ((System.Drawing.Image)(resources.GetObject("btn_ok.NormalImage")));
            this.btn_ok.Size = new System.Drawing.Size(73, 25);
            this.btn_ok.TabIndex = 4;
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_cancel.BackgroundImage")));
            this.btn_cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_cancel.Caption = "取消";
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_cancel.DownImage = ((System.Drawing.Image)(resources.GetObject("btn_cancel.DownImage")));
            this.btn_cancel.Location = new System.Drawing.Point(345, 212);
            this.btn_cancel.MoveImage = ((System.Drawing.Image)(resources.GetObject("btn_cancel.MoveImage")));
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.NormalImage = ((System.Drawing.Image)(resources.GetObject("btn_cancel.NormalImage")));
            this.btn_cancel.Size = new System.Drawing.Size(73, 25);
            this.btn_cancel.TabIndex = 5;
            // 
            // UCMaintainThreeGuarantyVerify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(444, 279);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "UCMaintainThreeGuarantyVerify";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "审核";
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txt_information;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdo_false;
        private System.Windows.Forms.RadioButton rdo_ture;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.ButtonEx btn_cancel;
        private ServiceStationClient.ComponentUI.ButtonEx btn_ok;


    }
}