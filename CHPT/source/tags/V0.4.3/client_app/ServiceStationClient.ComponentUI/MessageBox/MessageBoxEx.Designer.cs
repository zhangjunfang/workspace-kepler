namespace ServiceStationClient.ComponentUI
{
    partial class MessageBoxEx
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageBoxEx));
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.pnlButtom = new ServiceStationClient.ComponentUI.PanelEx();
            this.btnLeft = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnRight = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnMidlle = new ServiceStationClient.ComponentUI.ButtonEx();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.pnlButtom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.pnlButtom);
            this.pnlContainer.Controls.Add(this.lblMessage);
            this.pnlContainer.Controls.Add(this.picIcon);
            this.pnlContainer.Location = new System.Drawing.Point(0, 33);
            this.pnlContainer.Size = new System.Drawing.Size(379, 132);
            // 
            // picIcon
            // 
            this.picIcon.Location = new System.Drawing.Point(27, 15);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(51, 61);
            this.picIcon.TabIndex = 0;
            this.picIcon.TabStop = false;
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMessage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(122)))), ((int)(((byte)(185)))));
            this.lblMessage.Location = new System.Drawing.Point(83, 8);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(287, 76);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "Message";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlButtom
            // 
            this.pnlButtom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlButtom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.pnlButtom.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(214)))), ((int)(((byte)(214)))));
            this.pnlButtom.BorderColor = System.Drawing.Color.Silver;
            this.pnlButtom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlButtom.Controls.Add(this.btnLeft);
            this.pnlButtom.Controls.Add(this.btnRight);
            this.pnlButtom.Controls.Add(this.btnMidlle);
            this.pnlButtom.Curvature = 2;
            this.pnlButtom.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.pnlButtom.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.Vertical;
            this.pnlButtom.Location = new System.Drawing.Point(1, 98);
            this.pnlButtom.Name = "pnlButtom";
            this.pnlButtom.Size = new System.Drawing.Size(378, 35);
            this.pnlButtom.TabIndex = 5;
            // 
            // btnLeft
            // 
            this.btnLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLeft.BackgroundImage")));
            this.btnLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLeft.Caption = "Left";
            this.btnLeft.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnLeft.DownImage = ((System.Drawing.Image)(resources.GetObject("btnLeft.DownImage")));
            this.btnLeft.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLeft.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnLeft.Location = new System.Drawing.Point(117, 4);
            this.btnLeft.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLeft.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnLeft.MoveImage")));
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnLeft.NormalImage")));
            this.btnLeft.Size = new System.Drawing.Size(80, 27);
            this.btnLeft.TabIndex = 2;
            this.btnLeft.Click += new System.EventHandler(this.ButtionClick);
            // 
            // btnRight
            // 
            this.btnRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRight.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRight.BackgroundImage")));
            this.btnRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRight.Caption = "Right";
            this.btnRight.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnRight.DownImage = ((System.Drawing.Image)(resources.GetObject("btnRight.DownImage")));
            this.btnRight.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRight.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRight.Location = new System.Drawing.Point(289, 4);
            this.btnRight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRight.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnRight.MoveImage")));
            this.btnRight.Name = "btnRight";
            this.btnRight.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnRight.NormalImage")));
            this.btnRight.Size = new System.Drawing.Size(80, 27);
            this.btnRight.TabIndex = 4;
            this.btnRight.Click += new System.EventHandler(this.ButtionClick);
            // 
            // btnMidlle
            // 
            this.btnMidlle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMidlle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMidlle.BackgroundImage")));
            this.btnMidlle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMidlle.Caption = "Middle";
            this.btnMidlle.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnMidlle.DownImage = ((System.Drawing.Image)(resources.GetObject("btnMidlle.DownImage")));
            this.btnMidlle.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMidlle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnMidlle.Location = new System.Drawing.Point(203, 4);
            this.btnMidlle.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnMidlle.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnMidlle.MoveImage")));
            this.btnMidlle.Name = "btnMidlle";
            this.btnMidlle.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnMidlle.NormalImage")));
            this.btnMidlle.Size = new System.Drawing.Size(80, 27);
            this.btnMidlle.TabIndex = 3;
            this.btnMidlle.Click += new System.EventHandler(this.ButtionClick);
            // 
            // MessageBoxEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CanResize = false;
            this.ClientSize = new System.Drawing.Size(380, 166);
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1920, 1127);
            this.MinimizeBox = false;
            this.Name = "MessageBoxEx";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统提示";
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.pnlButtom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.PictureBox picIcon;
        private ButtonEx btnRight;
        private ButtonEx btnMidlle;
        private ButtonEx btnLeft;
        private PanelEx pnlButtom;




    }
}
