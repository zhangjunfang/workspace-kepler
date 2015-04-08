namespace HXCPcClient
{
    partial class UCPageTurning
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
            this.picPageFirst = new System.Windows.Forms.PictureBox();
            this.picPageUp = new System.Windows.Forms.PictureBox();
            this.pnlLineOne = new System.Windows.Forms.Panel();
            this.txtCurrentPage = new System.Windows.Forms.TextBox();
            this.lblPageCount = new System.Windows.Forms.Label();
            this.pnlLineTwo = new System.Windows.Forms.Panel();
            this.picPageLast = new System.Windows.Forms.PictureBox();
            this.picPageDown = new System.Windows.Forms.PictureBox();
            this.picRefresh = new System.Windows.Forms.PictureBox();
            this.pnlPageTurning = new System.Windows.Forms.Panel();
            this.cmbRowsCount = new System.Windows.Forms.ComboBox();
            this.lblPageInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picPageFirst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPageUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPageLast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPageDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRefresh)).BeginInit();
            this.pnlPageTurning.SuspendLayout();
            this.SuspendLayout();
            // 
            // picPageFirst
            // 
            this.picPageFirst.Location = new System.Drawing.Point(5, 7);
            this.picPageFirst.Name = "picPageFirst";
            this.picPageFirst.Size = new System.Drawing.Size(16, 16);
            this.picPageFirst.TabIndex = 0;
            this.picPageFirst.TabStop = false;
            this.picPageFirst.Click += new System.EventHandler(this.picPageFirst_Click);
            // 
            // picPageUp
            // 
            this.picPageUp.Location = new System.Drawing.Point(32, 7);
            this.picPageUp.Name = "picPageUp";
            this.picPageUp.Size = new System.Drawing.Size(16, 16);
            this.picPageUp.TabIndex = 1;
            this.picPageUp.TabStop = false;
            this.picPageUp.Click += new System.EventHandler(this.picPageUp_Click);
            // 
            // pnlLineOne
            // 
            this.pnlLineOne.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnlLineOne.Location = new System.Drawing.Point(53, 4);
            this.pnlLineOne.Name = "pnlLineOne";
            this.pnlLineOne.Size = new System.Drawing.Size(1, 22);
            this.pnlLineOne.TabIndex = 2;
            // 
            // txtCurrentPage
            // 
            this.txtCurrentPage.Location = new System.Drawing.Point(66, 5);
            this.txtCurrentPage.Name = "txtCurrentPage";
            this.txtCurrentPage.Size = new System.Drawing.Size(38, 21);
            this.txtCurrentPage.TabIndex = 3;
            this.txtCurrentPage.Text = "1";
            this.txtCurrentPage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCurrentPage_KeyPress);
            // 
            // lblPageCount
            // 
            this.lblPageCount.AutoSize = true;
            this.lblPageCount.Location = new System.Drawing.Point(104, 9);
            this.lblPageCount.Name = "lblPageCount";
            this.lblPageCount.Size = new System.Drawing.Size(41, 12);
            this.lblPageCount.TabIndex = 4;
            this.lblPageCount.Text = "/10000";
            // 
            // pnlLineTwo
            // 
            this.pnlLineTwo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnlLineTwo.Location = new System.Drawing.Point(144, 4);
            this.pnlLineTwo.Name = "pnlLineTwo";
            this.pnlLineTwo.Size = new System.Drawing.Size(1, 22);
            this.pnlLineTwo.TabIndex = 3;
            // 
            // picPageLast
            // 
            this.picPageLast.Location = new System.Drawing.Point(180, 7);
            this.picPageLast.Name = "picPageLast";
            this.picPageLast.Size = new System.Drawing.Size(16, 16);
            this.picPageLast.TabIndex = 6;
            this.picPageLast.TabStop = false;
            this.picPageLast.Click += new System.EventHandler(this.picPageLast_Click);
            // 
            // picPageDown
            // 
            this.picPageDown.Location = new System.Drawing.Point(153, 7);
            this.picPageDown.Name = "picPageDown";
            this.picPageDown.Size = new System.Drawing.Size(16, 16);
            this.picPageDown.TabIndex = 5;
            this.picPageDown.TabStop = false;
            this.picPageDown.Click += new System.EventHandler(this.picPageDown_Click);
            // 
            // picRefresh
            // 
            this.picRefresh.Location = new System.Drawing.Point(208, 7);
            this.picRefresh.Name = "picRefresh";
            this.picRefresh.Size = new System.Drawing.Size(16, 16);
            this.picRefresh.TabIndex = 7;
            this.picRefresh.TabStop = false;
            this.picRefresh.Click += new System.EventHandler(this.picRefresh_Click);
            // 
            // pnlPageTurning
            // 
            this.pnlPageTurning.Controls.Add(this.picRefresh);
            this.pnlPageTurning.Controls.Add(this.picPageFirst);
            this.pnlPageTurning.Controls.Add(this.picPageLast);
            this.pnlPageTurning.Controls.Add(this.picPageUp);
            this.pnlPageTurning.Controls.Add(this.picPageDown);
            this.pnlPageTurning.Controls.Add(this.pnlLineOne);
            this.pnlPageTurning.Controls.Add(this.pnlLineTwo);
            this.pnlPageTurning.Controls.Add(this.txtCurrentPage);
            this.pnlPageTurning.Controls.Add(this.lblPageCount);
            this.pnlPageTurning.Location = new System.Drawing.Point(48, 0);
            this.pnlPageTurning.Name = "pnlPageTurning";
            this.pnlPageTurning.Size = new System.Drawing.Size(230, 31);
            this.pnlPageTurning.TabIndex = 8;
            // 
            // cmbRowsCount
            // 
            this.cmbRowsCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRowsCount.FormattingEnabled = true;
            this.cmbRowsCount.Items.AddRange(new object[] {
            "30",
            "50",
            "100"});
            this.cmbRowsCount.Location = new System.Drawing.Point(0, 5);
            this.cmbRowsCount.Name = "cmbRowsCount";
            this.cmbRowsCount.Size = new System.Drawing.Size(47, 20);
            this.cmbRowsCount.TabIndex = 9;
            this.cmbRowsCount.SelectedIndexChanged += new System.EventHandler(this.cmbRowsCount_SelectedIndexChanged);
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPageInfo.Location = new System.Drawing.Point(297, 5);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(165, 21);
            this.lblPageInfo.TabIndex = 12;
            this.lblPageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UCPageTurning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblPageInfo);
            this.Controls.Add(this.cmbRowsCount);
            this.Controls.Add(this.pnlPageTurning);
            this.DoubleBuffered = true;
            this.Name = "UCPageTurning";
            this.Size = new System.Drawing.Size(465, 31);
            ((System.ComponentModel.ISupportInitialize)(this.picPageFirst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPageUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPageLast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPageDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRefresh)).EndInit();
            this.pnlPageTurning.ResumeLayout(false);
            this.pnlPageTurning.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picPageFirst;
        private System.Windows.Forms.PictureBox picPageUp;
        private System.Windows.Forms.Panel pnlLineOne;
        private System.Windows.Forms.TextBox txtCurrentPage;
        private System.Windows.Forms.Label lblPageCount;
        private System.Windows.Forms.Panel pnlLineTwo;
        private System.Windows.Forms.PictureBox picPageLast;
        private System.Windows.Forms.PictureBox picPageDown;
        private System.Windows.Forms.PictureBox picRefresh;
        private System.Windows.Forms.Panel pnlPageTurning;
        private System.Windows.Forms.ComboBox cmbRowsCount;
        private System.Windows.Forms.Label lblPageInfo;
    }
}
