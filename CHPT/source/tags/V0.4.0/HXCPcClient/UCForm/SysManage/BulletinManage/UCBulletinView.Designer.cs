namespace HXCPcClient.UCForm.SysManage.BulletinManage
{
    partial class UCBulletinView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCBulletinView));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labPerson = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labTime = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labType = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.webrowsContent = new System.Windows.Forms.WebBrowser();
            this.tabControlEx1 = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ucAttr = new HXCPcClient.UCForm.UCAttachment();
            this.dgvAttachment = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.dataGridViewEx1 = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.panelTitle = new System.Windows.Forms.Panel();
            this.tabControlEx1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.ucAttr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttachment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).BeginInit();
            this.panelTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // labPerson
            // 
            this.labPerson.AutoSize = true;
            this.labPerson.Font = new System.Drawing.Font("宋体", 9F);
            this.labPerson.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(156)))));
            this.labPerson.Location = new System.Drawing.Point(346, 8);
            this.labPerson.Name = "labPerson";
            this.labPerson.Size = new System.Drawing.Size(95, 12);
            this.labPerson.TabIndex = 68;
            this.labPerson.Text = "综合管理部-王静";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(156)))));
            this.label6.Location = new System.Drawing.Point(288, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 67;
            this.label6.Text = "发布人：";
            // 
            // labTime
            // 
            this.labTime.AutoSize = true;
            this.labTime.BackColor = System.Drawing.Color.Transparent;
            this.labTime.Font = new System.Drawing.Font("宋体", 9F);
            this.labTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(156)))));
            this.labTime.Location = new System.Drawing.Point(199, 8);
            this.labTime.Name = "labTime";
            this.labTime.Size = new System.Drawing.Size(65, 12);
            this.labTime.TabIndex = 4;
            this.labTime.Text = "2014-11-12";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("宋体", 9F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(156)))));
            this.label2.Location = new System.Drawing.Point(155, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "时间：";
            // 
            // labType
            // 
            this.labType.AutoSize = true;
            this.labType.Font = new System.Drawing.Font("宋体", 9F);
            this.labType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(158)))), ((int)(((byte)(0)))));
            this.labType.Location = new System.Drawing.Point(77, 8);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(53, 12);
            this.labType.TabIndex = 2;
            this.labType.Text = "公司公告";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(156)))));
            this.labelTitle.Location = new System.Drawing.Point(18, 37);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(57, 12);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "公告测试";
            // 
            // webrowsContent
            // 
            this.webrowsContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webrowsContent.Location = new System.Drawing.Point(2, 62);
            this.webrowsContent.MinimumSize = new System.Drawing.Size(20, 20);
            this.webrowsContent.Name = "webrowsContent";
            this.webrowsContent.Size = new System.Drawing.Size(1026, 296);
            this.webrowsContent.TabIndex = 0;
            // 
            // tabControlEx1
            // 
            this.tabControlEx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlEx1.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tabControlEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(249)))), ((int)(((byte)(254)))));
            this.tabControlEx1.Controls.Add(this.tabPage1);
            this.tabControlEx1.Location = new System.Drawing.Point(0, 364);
            this.tabControlEx1.Name = "tabControlEx1";
            this.tabControlEx1.SelectedIndex = 0;
            this.tabControlEx1.Size = new System.Drawing.Size(1024, 235);
            this.tabControlEx1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ucAttr);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1016, 205);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "附件信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ucAttr
            // 
            this.ucAttr.Controls.Add(this.dgvAttachment);
            this.ucAttr.Controls.Add(this.dataGridViewEx1);
            this.ucAttr.Location = new System.Drawing.Point(3, 3);
            this.ucAttr.Name = "ucAttr";
            this.ucAttr.Size = new System.Drawing.Size(1007, 199);
            this.ucAttr.TabIndex = 1;
            this.ucAttr.TableName = "";
            this.ucAttr.TableNameKeyValue = "";
            // 
            // dgvAttachment
            // 
            this.dgvAttachment.AllowUserToAddRows = false;
            this.dgvAttachment.AllowUserToDeleteRows = false;
            this.dgvAttachment.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvAttachment.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAttachment.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAttachment.BackgroundColor = System.Drawing.Color.White;
            this.dgvAttachment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAttachment.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAttachment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAttachment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAttachment.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvAttachment.EnableHeadersVisualStyles = false;
            this.dgvAttachment.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgvAttachment.Location = new System.Drawing.Point(0, 0);
            this.dgvAttachment.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvAttachment.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvAttachment.MergeColumnNames")));
            this.dgvAttachment.MultiSelect = false;
            this.dgvAttachment.Name = "dgvAttachment";
            this.dgvAttachment.ReadOnly = true;
            this.dgvAttachment.RowHeadersVisible = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvAttachment.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAttachment.RowTemplate.Height = 23;
            this.dgvAttachment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAttachment.ShowCheckBox = true;
            this.dgvAttachment.Size = new System.Drawing.Size(1007, 199);
            this.dgvAttachment.TabIndex = 1;
            // 
            // dataGridViewEx1
            // 
            this.dataGridViewEx1.AllowUserToAddRows = false;
            this.dataGridViewEx1.AllowUserToDeleteRows = false;
            this.dataGridViewEx1.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dataGridViewEx1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewEx1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewEx1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewEx1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewEx1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewEx1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewEx1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewEx1.EnableHeadersVisualStyles = false;
            this.dataGridViewEx1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dataGridViewEx1.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewEx1.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dataGridViewEx1.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dataGridViewEx1.MergeColumnNames")));
            this.dataGridViewEx1.MultiSelect = false;
            this.dataGridViewEx1.Name = "dataGridViewEx1";
            this.dataGridViewEx1.ReadOnly = true;
            this.dataGridViewEx1.RowHeadersVisible = false;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dataGridViewEx1.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewEx1.RowTemplate.Height = 23;
            this.dataGridViewEx1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEx1.ShowCheckBox = true;
            this.dataGridViewEx1.Size = new System.Drawing.Size(1007, 199);
            this.dataGridViewEx1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(156)))));
            this.label1.Location = new System.Drawing.Point(15, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "所属分类：";
            // 
            // panelTitle
            // 
            this.panelTitle.BackColor = System.Drawing.Color.Transparent;
            this.panelTitle.Controls.Add(this.label1);
            this.panelTitle.Controls.Add(this.labType);
            this.panelTitle.Controls.Add(this.labPerson);
            this.panelTitle.Controls.Add(this.label2);
            this.panelTitle.Controls.Add(this.labTime);
            this.panelTitle.Controls.Add(this.label6);
            this.panelTitle.Location = new System.Drawing.Point(107, 29);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(576, 26);
            this.panelTitle.TabIndex = 69;
            // 
            // UCBulletinView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(249)))), ((int)(((byte)(254)))));
            this.Controls.Add(this.webrowsContent);
            this.Controls.Add(this.panelTitle);
            this.Controls.Add(this.tabControlEx1);
            this.Controls.Add(this.labelTitle);
            this.Name = "UCBulletinView";
            this.Size = new System.Drawing.Size(1030, 600);
            this.Controls.SetChildIndex(this.labelTitle, 0);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.tabControlEx1, 0);
            this.Controls.SetChildIndex(this.panelTitle, 0);
            this.Controls.SetChildIndex(this.webrowsContent, 0);
            this.tabControlEx1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ucAttr.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttachment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).EndInit();
            this.panelTitle.ResumeLayout(false);
            this.panelTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labType;
        private System.Windows.Forms.Label labelTitle;
        private ServiceStationClient.ComponentUI.TabControlEx tabControlEx1;
        private System.Windows.Forms.TabPage tabPage1;
        private UCAttachment ucAttr;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvAttachment;
        private ServiceStationClient.ComponentUI.DataGridViewEx dataGridViewEx1;
        private System.Windows.Forms.Label labPerson;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.WebBrowser webrowsContent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelTitle;
    }
}
