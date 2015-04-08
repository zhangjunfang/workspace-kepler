namespace ServiceStationClient.ComponentUI.Panel
{
    partial class CardPanel
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
            this.tlp_container = new System.Windows.Forms.TableLayoutPanel();
            this.pnl_header = new ServiceStationClient.ComponentUI.PanelEx();
            this.lbl_header = new System.Windows.Forms.Label();
            this.pic_icon = new System.Windows.Forms.PictureBox();
            this.pnl_content = new ServiceStationClient.ComponentUI.PanelEx();
            this.tlp_container.SuspendLayout();
            this.pnl_header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_icon)).BeginInit();
            this.SuspendLayout();
            // 
            // tlp_container
            // 
            this.tlp_container.ColumnCount = 1;
            this.tlp_container.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_container.Controls.Add(this.pnl_header, 0, 0);
            this.tlp_container.Controls.Add(this.pnl_content, 0, 1);
            this.tlp_container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_container.Location = new System.Drawing.Point(0, 0);
            this.tlp_container.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_container.Name = "tlp_container";
            this.tlp_container.RowCount = 2;
            this.tlp_container.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.94658F));
            this.tlp_container.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.05341F));
            this.tlp_container.Size = new System.Drawing.Size(555, 286);
            this.tlp_container.TabIndex = 0;
            // 
            // pnl_header
            // 
            this.pnl_header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(215)))), ((int)(((byte)(254)))));
            this.pnl_header.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(215)))), ((int)(((byte)(254)))));
            this.pnl_header.BorderColor = System.Drawing.Color.Transparent;
            this.pnl_header.BorderWidth = 0;
            this.pnl_header.Controls.Add(this.lbl_header);
            this.pnl_header.Controls.Add(this.pic_icon);
            this.pnl_header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_header.Location = new System.Drawing.Point(0, 0);
            this.pnl_header.Margin = new System.Windows.Forms.Padding(0);
            this.pnl_header.Name = "pnl_header";
            this.pnl_header.Size = new System.Drawing.Size(555, 37);
            this.pnl_header.TabIndex = 1;
            // 
            // lbl_header
            // 
            this.lbl_header.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_header.AutoSize = true;
            this.lbl_header.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_header.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(129)))), ((int)(((byte)(194)))));
            this.lbl_header.Location = new System.Drawing.Point(29, 9);
            this.lbl_header.Name = "lbl_header";
            this.lbl_header.Size = new System.Drawing.Size(69, 19);
            this.lbl_header.TabIndex = 1;
            this.lbl_header.Text = "基本信息";
            // 
            // pic_icon
            // 
            this.pic_icon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pic_icon.Location = new System.Drawing.Point(9, 8);
            this.pic_icon.Name = "pic_icon";
            this.pic_icon.Size = new System.Drawing.Size(20, 20);
            this.pic_icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_icon.TabIndex = 0;
            this.pic_icon.TabStop = false;
            // 
            // pnl_content
            // 
            this.pnl_content.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.pnl_content.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.pnl_content.BorderColor = System.Drawing.Color.Transparent;
            this.pnl_content.BorderWidth = 0;
            this.pnl_content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_content.Location = new System.Drawing.Point(0, 37);
            this.pnl_content.Margin = new System.Windows.Forms.Padding(0);
            this.pnl_content.Name = "pnl_content";
            this.pnl_content.Size = new System.Drawing.Size(555, 249);
            this.pnl_content.TabIndex = 0;
            // 
            // CardPanel
            // 
            this.Controls.Add(this.tlp_container);
            this.Name = "CardPanel";
            this.Size = new System.Drawing.Size(555, 286);
            this.tlp_container.ResumeLayout(false);
            this.pnl_header.ResumeLayout(false);
            this.pnl_header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_icon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_container;
        private PanelEx pnl_header;
        private PanelEx pnl_content;
        private System.Windows.Forms.PictureBox pic_icon;
        private System.Windows.Forms.Label lbl_header;
    }
}
