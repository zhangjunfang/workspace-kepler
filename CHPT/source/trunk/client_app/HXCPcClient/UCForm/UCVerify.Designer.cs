namespace HXCPcClient.UCForm
{
    partial class UCVerify
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCVerify));
            this.label1 = new System.Windows.Forms.Label();
            this.rdbOK = new System.Windows.Forms.RadioButton();
            this.rdbReturn = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.rtbContent = new System.Windows.Forms.RichTextBox();
            this.btnOK = new HXC.UI.Library.Controls.ExtButton();
            this.btnCancel = new HXC.UI.Library.Controls.ExtButton();
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlContainer.Controls.Add(this.btnCancel);
            this.pnlContainer.Controls.Add(this.btnOK);
            this.pnlContainer.Controls.Add(this.rtbContent);
            this.pnlContainer.Controls.Add(this.label2);
            this.pnlContainer.Controls.Add(this.rdbReturn);
            this.pnlContainer.Controls.Add(this.rdbOK);
            this.pnlContainer.Controls.Add(this.label1);
            this.pnlContainer.Size = new System.Drawing.Size(423, 201);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "您确认要";
            // 
            // rdbOK
            // 
            this.rdbOK.AutoSize = true;
            this.rdbOK.Checked = true;
            this.rdbOK.Location = new System.Drawing.Point(72, 19);
            this.rdbOK.Name = "rdbOK";
            this.rdbOK.Size = new System.Drawing.Size(47, 16);
            this.rdbOK.TabIndex = 1;
            this.rdbOK.TabStop = true;
            this.rdbOK.Text = "通过";
            this.rdbOK.UseVisualStyleBackColor = true;
            // 
            // rdbReturn
            // 
            this.rdbReturn.AutoSize = true;
            this.rdbReturn.Location = new System.Drawing.Point(145, 19);
            this.rdbReturn.Name = "rdbReturn";
            this.rdbReturn.Size = new System.Drawing.Size(47, 16);
            this.rdbReturn.TabIndex = 2;
            this.rdbReturn.Text = "驳回";
            this.rdbReturn.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(226, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "此条信息吗？";
            // 
            // rtbContent
            // 
            this.rtbContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbContent.Location = new System.Drawing.Point(6, 41);
            this.rtbContent.MaxLength = 200;
            this.rtbContent.Name = "rtbContent";
            this.rtbContent.Size = new System.Drawing.Size(408, 120);
            this.rtbContent.TabIndex = 4;
            this.rtbContent.Text = "审核意见：";
            this.rtbContent.TextChanged += new System.EventHandler(this.rtbContent_TextChanged);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.btnOK.BorderWidth = 0;
            this.btnOK.Content = null;
            this.btnOK.ContentTypeName = null;
            this.btnOK.ContentTypeParameter = null;
            this.btnOK.CornerRadiu = 0;
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.DisplayValue = "确定";
            this.btnOK.InputtingVerifyCondition = null;
            this.btnOK.LightBackgroudImage = ((System.Drawing.Image)(resources.GetObject("btnOK.LightBackgroudImage")));
            this.btnOK.Location = new System.Drawing.Point(216, 167);
            this.btnOK.Name = "btnOK";
            this.btnOK.NormalBackgroudImage = ((System.Drawing.Image)(resources.GetObject("btnOK.NormalBackgroudImage")));
            this.btnOK.ShowError = false;
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 24;
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnOK.Value = "确定";
            this.btnOK.VerifyCondition = null;
            this.btnOK.VerifyType = null;
            this.btnOK.VerifyTypeName = null;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.btnCancel.BorderWidth = 0;
            this.btnCancel.Content = null;
            this.btnCancel.ContentTypeName = null;
            this.btnCancel.ContentTypeParameter = null;
            this.btnCancel.CornerRadiu = 0;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DisplayValue = "取消";
            this.btnCancel.InputtingVerifyCondition = null;
            this.btnCancel.LightBackgroudImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.LightBackgroudImage")));
            this.btnCancel.Location = new System.Drawing.Point(337, 167);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NormalBackgroudImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.NormalBackgroudImage")));
            this.btnCancel.ShowError = false;
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCancel.Value = "取消";
            this.btnCancel.VerifyCondition = null;
            this.btnCancel.VerifyType = null;
            this.btnCancel.VerifyTypeName = null;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // UCVerify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(425, 232);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "UCVerify";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "审核";
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.RadioButton rdbReturn;
        public System.Windows.Forms.RadioButton rdbOK;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.RichTextBox rtbContent;
        private HXC.UI.Library.Controls.ExtButton btnOK;
        private HXC.UI.Library.Controls.ExtButton btnCancel;
    }
}