namespace ServiceStationClient.ComponentUI
{
    partial class OpButton
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpButton));
            this.btnCollection = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAudit = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDetail = new System.Windows.Forms.Button();
            this.btnAdjust = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnCollection.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCollection
            // 
            this.btnCollection.ColumnCount = 9;
            this.btnCollection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.btnCollection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.btnCollection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.btnCollection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.btnCollection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.btnCollection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.btnCollection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.btnCollection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.btnCollection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.btnCollection.Controls.Add(this.btnAdd, 0, 0);
            this.btnCollection.Controls.Add(this.btnEdit, 1, 0);
            this.btnCollection.Controls.Add(this.btnAudit, 5, 0);
            this.btnCollection.Controls.Add(this.btnClose, 8, 0);
            this.btnCollection.Controls.Add(this.btnDel, 2, 0);
            this.btnCollection.Controls.Add(this.btnCancel, 4, 0);
            this.btnCollection.Controls.Add(this.btnSave, 3, 0);
            this.btnCollection.Controls.Add(this.btnDetail, 7, 0);
            this.btnCollection.Controls.Add(this.btnAdjust, 6, 0);
            this.btnCollection.Location = new System.Drawing.Point(3, -2);
            this.btnCollection.Name = "btnCollection";
            this.btnCollection.RowCount = 1;
            this.btnCollection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.btnCollection.Size = new System.Drawing.Size(732, 29);
            this.btnCollection.TabIndex = 10;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(3, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "新 增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(84, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 22);
            this.btnEdit.TabIndex = 6;
            this.btnEdit.Text = "修 改";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAudit
            // 
            this.btnAudit.Location = new System.Drawing.Point(408, 3);
            this.btnAudit.Name = "btnAudit";
            this.btnAudit.Size = new System.Drawing.Size(75, 22);
            this.btnAudit.TabIndex = 11;
            this.btnAudit.Text = "审 核";
            this.btnAudit.UseVisualStyleBackColor = true;
            this.btnAudit.Click += new System.EventHandler(this.btnAudit_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(651, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 22);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "关 闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(165, 3);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 22);
            this.btnDel.TabIndex = 8;
            this.btnDel.Text = "删 除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(327, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 22);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "取 消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(246, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 22);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "保 存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDetail
            // 
            this.btnDetail.Location = new System.Drawing.Point(570, 3);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(75, 22);
            this.btnDetail.TabIndex = 9;
            this.btnDetail.Text = "详细信息";
            this.btnDetail.UseVisualStyleBackColor = true;
            this.btnDetail.Click += new System.EventHandler(this.btnDetail_Click);
            // 
            // btnAdjust
            // 
            this.btnAdjust.Location = new System.Drawing.Point(489, 3);
            this.btnAdjust.Name = "btnAdjust";
            this.btnAdjust.Size = new System.Drawing.Size(75, 22);
            this.btnAdjust.TabIndex = 12;
            this.btnAdjust.Text = "调 整";
            this.btnAdjust.UseVisualStyleBackColor = true;
            this.btnAdjust.Click += new System.EventHandler(this.btnAdjust_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "a.jpg");
            this.imageList1.Images.SetKeyName(1, "l.jpg");
            this.imageList1.Images.SetKeyName(2, "lb.jpg");
            this.imageList1.Images.SetKeyName(3, "lr.jpg");
            this.imageList1.Images.SetKeyName(4, "r.jpg");
            this.imageList1.Images.SetKeyName(5, "rb.jpg");
            // 
            // OpButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor =System.Drawing.Color.Transparent;
            this.Controls.Add(this.btnCollection);
            this.Name = "OpButton";
            this.Size = new System.Drawing.Size(737, 25);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OpButton_Paint);
            this.btnCollection.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel btnCollection;
        public System.Windows.Forms.Button btnDel;
        public System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.Button btnEdit;
        public System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnAdjust;
        public System.Windows.Forms.Button btnAudit;
        public System.Windows.Forms.Button btnDetail;
        public System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ImageList imageList1;
    }
}
