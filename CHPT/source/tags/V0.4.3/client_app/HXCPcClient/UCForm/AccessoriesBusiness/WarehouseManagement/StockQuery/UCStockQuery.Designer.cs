namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.StockQuery
{
    partial class UCStockQuery
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCStockQuery));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx4 = new ServiceStationClient.ComponentUI.PanelEx();
            this.winFormStockPager = new ServiceStationClient.ComponentUI.WinFormPager();
            this.gvStockList = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.PartNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrawingNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PapCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PhysicalCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OccupyCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AvailableCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UntName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReferInPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReferOutPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BarCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Weight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HighestInPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LowestOutPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VIPPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirstInPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SecondInPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThirdInPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirstOutPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SecondOutPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThirdOutPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VehicleType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductPlace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartBrand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FactoryCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ApplyFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TreeVPartCategory = new ServiceStationClient.ComponentUI.TreeViewEx();
            this.panelEx3 = new ServiceStationClient.ComponentUI.PanelEx();
            this.txtparts_cartype = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.label1 = new System.Windows.Forms.Label();
            this.CheckBGuarantyStock = new System.Windows.Forms.CheckBox();
            this.CombWarehouse = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label12 = new System.Windows.Forms.Label();
            this.txtBarCode = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtCarFactoryCode = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtparts_brand = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtdrawing_num = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtparts_type = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.txtparts_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label23 = new System.Windows.Forms.Label();
            this.txtparts_code = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.label19 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.CombCompany = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label16 = new System.Windows.Forms.Label();
            this.txtProductPlace = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label20 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.btnPartSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnPartClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.BtnExportMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ExcelExport = new System.Windows.Forms.ToolStripMenuItem();
            this.panelEx4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvStockList)).BeginInit();
            this.panelEx3.SuspendLayout();
            this.BtnExportMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOpt
            // 
            this.pnlOpt.Size = new System.Drawing.Size(1141, 25);
            // 
            // panelEx4
            // 
            this.panelEx4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx4.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx4.BorderWidth = 1;
            this.panelEx4.Controls.Add(this.winFormStockPager);
            this.panelEx4.Curvature = 0;
            this.panelEx4.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx4.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx4.Location = new System.Drawing.Point(2, 514);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(1134, 48);
            this.panelEx4.TabIndex = 52;
            // 
            // winFormStockPager
            // 
            this.winFormStockPager.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.winFormStockPager.BackColor = System.Drawing.Color.Transparent;
            this.winFormStockPager.BtnTextNext = "下页";
            this.winFormStockPager.BtnTextPrevious = "上页";
            this.winFormStockPager.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.winFormStockPager.Location = new System.Drawing.Point(628, 3);
            this.winFormStockPager.Name = "winFormStockPager";
            this.winFormStockPager.PageCount = 0;
            this.winFormStockPager.PageSize = 15;
            this.winFormStockPager.RecordCount = 0;
            this.winFormStockPager.Size = new System.Drawing.Size(505, 40);
            this.winFormStockPager.TabIndex = 5;
            this.winFormStockPager.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            this.winFormStockPager.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.winFormStockPager_PageIndexChanged);
            // 
            // gvStockList
            // 
            this.gvStockList.AllowUserToAddRows = false;
            this.gvStockList.AllowUserToDeleteRows = false;
            this.gvStockList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gvStockList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvStockList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvStockList.BackgroundColor = System.Drawing.Color.White;
            this.gvStockList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvStockList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvStockList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvStockList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PartNum,
            this.PartName,
            this.DrawingNum,
            this.PartModel,
            this.PartCategory,
            this.PapCount,
            this.PhysicalCount,
            this.OccupyCount,
            this.AvailableCount,
            this.UntName,
            this.ReferInPrice,
            this.ReferOutPrice,
            this.BarCode,
            this.Weight,
            this.HighestInPrice,
            this.LowestOutPrice,
            this.VIPPrice,
            this.FirstInPrice,
            this.SecondInPrice,
            this.ThirdInPrice,
            this.FirstOutPrice,
            this.SecondOutPrice,
            this.ThirdOutPrice,
            this.VehicleType,
            this.ProductPlace,
            this.PartBrand,
            this.FactoryCode,
            this.PartRemark,
            this.ApplyFlag});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvStockList.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvStockList.EnableHeadersVisualStyles = false;
            this.gvStockList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.gvStockList.IsCheck = true;
            this.gvStockList.Location = new System.Drawing.Point(199, 181);
            this.gvStockList.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.gvStockList.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("gvStockList.MergeColumnNames")));
            this.gvStockList.MultiSelect = false;
            this.gvStockList.Name = "gvStockList";
            this.gvStockList.ReadOnly = true;
            this.gvStockList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.gvStockList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvStockList.RowHeadersVisible = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.gvStockList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gvStockList.RowTemplate.Height = 23;
            this.gvStockList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvStockList.ShowCheckBox = true;
            this.gvStockList.Size = new System.Drawing.Size(936, 331);
            this.gvStockList.TabIndex = 51;
            // 
            // PartNum
            // 
            this.PartNum.HeaderText = "配件编码";
            this.PartNum.Name = "PartNum";
            this.PartNum.ReadOnly = true;
            // 
            // PartName
            // 
            this.PartName.HeaderText = "配件名称";
            this.PartName.Name = "PartName";
            this.PartName.ReadOnly = true;
            // 
            // DrawingNum
            // 
            this.DrawingNum.HeaderText = "图号";
            this.DrawingNum.Name = "DrawingNum";
            this.DrawingNum.ReadOnly = true;
            // 
            // PartModel
            // 
            this.PartModel.HeaderText = "规格";
            this.PartModel.Name = "PartModel";
            this.PartModel.ReadOnly = true;
            // 
            // PartCategory
            // 
            this.PartCategory.HeaderText = "配件类别";
            this.PartCategory.Name = "PartCategory";
            this.PartCategory.ReadOnly = true;
            // 
            // PapCount
            // 
            this.PapCount.HeaderText = "账面库存";
            this.PapCount.Name = "PapCount";
            this.PapCount.ReadOnly = true;
            // 
            // PhysicalCount
            // 
            this.PhysicalCount.HeaderText = "实际库存";
            this.PhysicalCount.Name = "PhysicalCount";
            this.PhysicalCount.ReadOnly = true;
            // 
            // OccupyCount
            // 
            this.OccupyCount.HeaderText = "占用库存";
            this.OccupyCount.Name = "OccupyCount";
            this.OccupyCount.ReadOnly = true;
            // 
            // AvailableCount
            // 
            this.AvailableCount.HeaderText = "可用库存";
            this.AvailableCount.Name = "AvailableCount";
            this.AvailableCount.ReadOnly = true;
            // 
            // UntName
            // 
            this.UntName.HeaderText = "单位名称";
            this.UntName.Name = "UntName";
            this.UntName.ReadOnly = true;
            // 
            // ReferInPrice
            // 
            this.ReferInPrice.HeaderText = "参考进价";
            this.ReferInPrice.Name = "ReferInPrice";
            this.ReferInPrice.ReadOnly = true;
            // 
            // ReferOutPrice
            // 
            this.ReferOutPrice.HeaderText = "参考售价";
            this.ReferOutPrice.Name = "ReferOutPrice";
            this.ReferOutPrice.ReadOnly = true;
            // 
            // BarCode
            // 
            this.BarCode.HeaderText = "条码";
            this.BarCode.Name = "BarCode";
            this.BarCode.ReadOnly = true;
            // 
            // Weight
            // 
            this.Weight.HeaderText = "重量";
            this.Weight.Name = "Weight";
            this.Weight.ReadOnly = true;
            // 
            // HighestInPrice
            // 
            this.HighestInPrice.HeaderText = "最高进价";
            this.HighestInPrice.Name = "HighestInPrice";
            this.HighestInPrice.ReadOnly = true;
            // 
            // LowestOutPrice
            // 
            this.LowestOutPrice.HeaderText = "最低售价";
            this.LowestOutPrice.Name = "LowestOutPrice";
            this.LowestOutPrice.ReadOnly = true;
            // 
            // VIPPrice
            // 
            this.VIPPrice.HeaderText = "会员价";
            this.VIPPrice.Name = "VIPPrice";
            this.VIPPrice.ReadOnly = true;
            // 
            // FirstInPrice
            // 
            this.FirstInPrice.HeaderText = "一级进价";
            this.FirstInPrice.Name = "FirstInPrice";
            this.FirstInPrice.ReadOnly = true;
            // 
            // SecondInPrice
            // 
            this.SecondInPrice.HeaderText = "二级进价";
            this.SecondInPrice.Name = "SecondInPrice";
            this.SecondInPrice.ReadOnly = true;
            // 
            // ThirdInPrice
            // 
            this.ThirdInPrice.HeaderText = "三级进价";
            this.ThirdInPrice.Name = "ThirdInPrice";
            this.ThirdInPrice.ReadOnly = true;
            // 
            // FirstOutPrice
            // 
            this.FirstOutPrice.HeaderText = "一级销价";
            this.FirstOutPrice.Name = "FirstOutPrice";
            this.FirstOutPrice.ReadOnly = true;
            // 
            // SecondOutPrice
            // 
            this.SecondOutPrice.HeaderText = "二级销价";
            this.SecondOutPrice.Name = "SecondOutPrice";
            this.SecondOutPrice.ReadOnly = true;
            // 
            // ThirdOutPrice
            // 
            this.ThirdOutPrice.HeaderText = "三级销价";
            this.ThirdOutPrice.Name = "ThirdOutPrice";
            this.ThirdOutPrice.ReadOnly = true;
            // 
            // VehicleType
            // 
            this.VehicleType.HeaderText = "车型";
            this.VehicleType.Name = "VehicleType";
            this.VehicleType.ReadOnly = true;
            // 
            // ProductPlace
            // 
            this.ProductPlace.HeaderText = "产地";
            this.ProductPlace.Name = "ProductPlace";
            this.ProductPlace.ReadOnly = true;
            // 
            // PartBrand
            // 
            this.PartBrand.HeaderText = "品牌";
            this.PartBrand.Name = "PartBrand";
            this.PartBrand.ReadOnly = true;
            // 
            // FactoryCode
            // 
            this.FactoryCode.HeaderText = "车厂编码";
            this.FactoryCode.Name = "FactoryCode";
            this.FactoryCode.ReadOnly = true;
            // 
            // PartRemark
            // 
            this.PartRemark.HeaderText = "备注";
            this.PartRemark.Name = "PartRemark";
            this.PartRemark.ReadOnly = true;
            // 
            // ApplyFlag
            // 
            this.ApplyFlag.HeaderText = "启停用标志";
            this.ApplyFlag.Name = "ApplyFlag";
            this.ApplyFlag.ReadOnly = true;
            // 
            // TreeVPartCategory
            // 
            this.TreeVPartCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.TreeVPartCategory.IgnoreAutoCheck = false;
            this.TreeVPartCategory.Location = new System.Drawing.Point(2, 180);
            this.TreeVPartCategory.Name = "TreeVPartCategory";
            this.TreeVPartCategory.Size = new System.Drawing.Size(195, 333);
            this.TreeVPartCategory.TabIndex = 50;
            // 
            // panelEx3
            // 
            this.panelEx3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx3.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx3.BorderWidth = 1;
            this.panelEx3.Controls.Add(this.txtparts_cartype);
            this.panelEx3.Controls.Add(this.label1);
            this.panelEx3.Controls.Add(this.CheckBGuarantyStock);
            this.panelEx3.Controls.Add(this.CombWarehouse);
            this.panelEx3.Controls.Add(this.label12);
            this.panelEx3.Controls.Add(this.txtBarCode);
            this.panelEx3.Controls.Add(this.txtCarFactoryCode);
            this.panelEx3.Controls.Add(this.txtparts_brand);
            this.panelEx3.Controls.Add(this.txtdrawing_num);
            this.panelEx3.Controls.Add(this.txtparts_type);
            this.panelEx3.Controls.Add(this.label26);
            this.panelEx3.Controls.Add(this.label25);
            this.panelEx3.Controls.Add(this.label24);
            this.panelEx3.Controls.Add(this.txtparts_name);
            this.panelEx3.Controls.Add(this.label23);
            this.panelEx3.Controls.Add(this.txtparts_code);
            this.panelEx3.Controls.Add(this.label19);
            this.panelEx3.Controls.Add(this.label15);
            this.panelEx3.Controls.Add(this.CombCompany);
            this.panelEx3.Controls.Add(this.label16);
            this.panelEx3.Controls.Add(this.txtProductPlace);
            this.panelEx3.Controls.Add(this.label20);
            this.panelEx3.Controls.Add(this.label31);
            this.panelEx3.Controls.Add(this.btnPartSearch);
            this.panelEx3.Controls.Add(this.btnPartClear);
            this.panelEx3.Curvature = 0;
            this.panelEx3.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx3.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx3.Location = new System.Drawing.Point(2, 30);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(1134, 149);
            this.panelEx3.TabIndex = 49;
            // 
            // txtparts_cartype
            // 
            this.txtparts_cartype.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtparts_cartype.BackgroundImage")));
            this.txtparts_cartype.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.txtparts_cartype.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Vehicle;
            this.txtparts_cartype.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtparts_cartype.Location = new System.Drawing.Point(576, 94);
            this.txtparts_cartype.Name = "txtparts_cartype";
            this.txtparts_cartype.ReadOnly = false;
            this.txtparts_cartype.Size = new System.Drawing.Size(145, 28);
            this.txtparts_cartype.TabIndex = 268;
            this.txtparts_cartype.ToolTipTitle = "请输入或者选择配件车型";
            this.txtparts_cartype.ChooserClick += new System.EventHandler(this.txtparts_cartype_ChooserClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(509, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 267;
            this.label1.Text = "配件车型：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CheckBGuarantyStock
            // 
            this.CheckBGuarantyStock.AutoSize = true;
            this.CheckBGuarantyStock.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CheckBGuarantyStock.Location = new System.Drawing.Point(761, 99);
            this.CheckBGuarantyStock.Name = "CheckBGuarantyStock";
            this.CheckBGuarantyStock.Size = new System.Drawing.Size(108, 16);
            this.CheckBGuarantyStock.TabIndex = 266;
            this.CheckBGuarantyStock.Text = "宇通三包件库存";
            this.CheckBGuarantyStock.UseVisualStyleBackColor = true;
            this.CheckBGuarantyStock.CheckedChanged += new System.EventHandler(this.CheckBGuarantyStock_CheckedChanged);
            // 
            // CombWarehouse
            // 
            this.CombWarehouse.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CombWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CombWarehouse.FormattingEnabled = true;
            this.CombWarehouse.Location = new System.Drawing.Point(325, 10);
            this.CombWarehouse.Name = "CombWarehouse";
            this.CombWarehouse.Size = new System.Drawing.Size(145, 22);
            this.CombWarehouse.TabIndex = 265;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(281, 14);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 264;
            this.label12.Text = "仓库：";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBarCode
            // 
            this.txtBarCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtBarCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtBarCode.BackColor = System.Drawing.Color.Transparent;
            this.txtBarCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtBarCode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtBarCode.ForeImage = null;
            this.txtBarCode.InputtingVerifyCondition = null;
            this.txtBarCode.Location = new System.Drawing.Point(803, 7);
            this.txtBarCode.MaxLengh = 32767;
            this.txtBarCode.Multiline = false;
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.Radius = 3;
            this.txtBarCode.ReadOnly = false;
            this.txtBarCode.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtBarCode.ShowError = false;
            this.txtBarCode.Size = new System.Drawing.Size(145, 23);
            this.txtBarCode.TabIndex = 167;
            this.txtBarCode.UseSystemPasswordChar = false;
            this.txtBarCode.Value = "";
            this.txtBarCode.VerifyCondition = null;
            this.txtBarCode.VerifyType = null;
            this.txtBarCode.VerifyTypeName = null;
            this.txtBarCode.WaterMark = null;
            this.txtBarCode.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtCarFactoryCode
            // 
            this.txtCarFactoryCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtCarFactoryCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtCarFactoryCode.BackColor = System.Drawing.Color.Transparent;
            this.txtCarFactoryCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtCarFactoryCode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtCarFactoryCode.ForeImage = null;
            this.txtCarFactoryCode.InputtingVerifyCondition = null;
            this.txtCarFactoryCode.Location = new System.Drawing.Point(79, 95);
            this.txtCarFactoryCode.MaxLengh = 32767;
            this.txtCarFactoryCode.Multiline = false;
            this.txtCarFactoryCode.Name = "txtCarFactoryCode";
            this.txtCarFactoryCode.Radius = 3;
            this.txtCarFactoryCode.ReadOnly = false;
            this.txtCarFactoryCode.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtCarFactoryCode.ShowError = false;
            this.txtCarFactoryCode.Size = new System.Drawing.Size(145, 23);
            this.txtCarFactoryCode.TabIndex = 166;
            this.txtCarFactoryCode.UseSystemPasswordChar = false;
            this.txtCarFactoryCode.Value = "";
            this.txtCarFactoryCode.VerifyCondition = null;
            this.txtCarFactoryCode.VerifyType = null;
            this.txtCarFactoryCode.VerifyTypeName = null;
            this.txtCarFactoryCode.WaterMark = null;
            this.txtCarFactoryCode.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtparts_brand
            // 
            this.txtparts_brand.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtparts_brand.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtparts_brand.BackColor = System.Drawing.Color.Transparent;
            this.txtparts_brand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtparts_brand.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtparts_brand.ForeImage = null;
            this.txtparts_brand.InputtingVerifyCondition = null;
            this.txtparts_brand.Location = new System.Drawing.Point(803, 52);
            this.txtparts_brand.MaxLengh = 32767;
            this.txtparts_brand.Multiline = false;
            this.txtparts_brand.Name = "txtparts_brand";
            this.txtparts_brand.Radius = 3;
            this.txtparts_brand.ReadOnly = false;
            this.txtparts_brand.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtparts_brand.ShowError = false;
            this.txtparts_brand.Size = new System.Drawing.Size(145, 23);
            this.txtparts_brand.TabIndex = 165;
            this.txtparts_brand.UseSystemPasswordChar = false;
            this.txtparts_brand.Value = "";
            this.txtparts_brand.VerifyCondition = null;
            this.txtparts_brand.VerifyType = null;
            this.txtparts_brand.VerifyTypeName = null;
            this.txtparts_brand.WaterMark = null;
            this.txtparts_brand.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtdrawing_num
            // 
            this.txtdrawing_num.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtdrawing_num.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtdrawing_num.BackColor = System.Drawing.Color.Transparent;
            this.txtdrawing_num.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtdrawing_num.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtdrawing_num.ForeImage = null;
            this.txtdrawing_num.InputtingVerifyCondition = null;
            this.txtdrawing_num.Location = new System.Drawing.Point(576, 52);
            this.txtdrawing_num.MaxLengh = 32767;
            this.txtdrawing_num.Multiline = false;
            this.txtdrawing_num.Name = "txtdrawing_num";
            this.txtdrawing_num.Radius = 3;
            this.txtdrawing_num.ReadOnly = false;
            this.txtdrawing_num.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtdrawing_num.ShowError = false;
            this.txtdrawing_num.Size = new System.Drawing.Size(145, 23);
            this.txtdrawing_num.TabIndex = 164;
            this.txtdrawing_num.UseSystemPasswordChar = false;
            this.txtdrawing_num.Value = "";
            this.txtdrawing_num.VerifyCondition = null;
            this.txtdrawing_num.VerifyType = null;
            this.txtdrawing_num.VerifyTypeName = null;
            this.txtdrawing_num.WaterMark = null;
            this.txtdrawing_num.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtparts_type
            // 
            this.txtparts_type.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtparts_type.BackgroundImage")));
            this.txtparts_type.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.txtparts_type.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.PartType;
            this.txtparts_type.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtparts_type.Location = new System.Drawing.Point(325, 94);
            this.txtparts_type.Name = "txtparts_type";
            this.txtparts_type.ReadOnly = false;
            this.txtparts_type.Size = new System.Drawing.Size(145, 28);
            this.txtparts_type.TabIndex = 163;
            this.txtparts_type.ToolTipTitle = "请输入或选择配件类型";
            this.txtparts_type.ChooserClick += new System.EventHandler(this.txtparts_type_ChooserClick);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(760, 57);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(41, 12);
            this.label26.TabIndex = 160;
            this.label26.Text = "品牌：";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(759, 12);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(41, 12);
            this.label25.TabIndex = 159;
            this.label25.Text = "条码：";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(12, 100);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(65, 12);
            this.label24.TabIndex = 158;
            this.label24.Text = "车厂编码：";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtparts_name
            // 
            this.txtparts_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtparts_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtparts_name.BackColor = System.Drawing.Color.Transparent;
            this.txtparts_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtparts_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtparts_name.ForeImage = null;
            this.txtparts_name.InputtingVerifyCondition = null;
            this.txtparts_name.Location = new System.Drawing.Point(325, 52);
            this.txtparts_name.MaxLengh = 32767;
            this.txtparts_name.Multiline = false;
            this.txtparts_name.Name = "txtparts_name";
            this.txtparts_name.Radius = 3;
            this.txtparts_name.ReadOnly = false;
            this.txtparts_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtparts_name.ShowError = false;
            this.txtparts_name.Size = new System.Drawing.Size(145, 23);
            this.txtparts_name.TabIndex = 157;
            this.txtparts_name.UseSystemPasswordChar = false;
            this.txtparts_name.Value = "";
            this.txtparts_name.VerifyCondition = null;
            this.txtparts_name.VerifyType = null;
            this.txtparts_name.VerifyTypeName = null;
            this.txtparts_name.WaterMark = null;
            this.txtparts_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(258, 57);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(65, 12);
            this.label23.TabIndex = 156;
            this.label23.Text = "配件名称：";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtparts_code
            // 
            this.txtparts_code.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtparts_code.BackgroundImage")));
            this.txtparts_code.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.txtparts_code.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.PartCode;
            this.txtparts_code.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtparts_code.Location = new System.Drawing.Point(79, 52);
            this.txtparts_code.Name = "txtparts_code";
            this.txtparts_code.ReadOnly = false;
            this.txtparts_code.Size = new System.Drawing.Size(145, 28);
            this.txtparts_code.TabIndex = 155;
            this.txtparts_code.ToolTipTitle = "请输入或选择配件编码";
            this.txtparts_code.ChooserClick += new System.EventHandler(this.txtparts_code_ChooserClick);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(11, 58);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 12);
            this.label19.TabIndex = 154;
            this.label19.Text = "配件编码：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(258, 99);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 12);
            this.label15.TabIndex = 151;
            this.label15.Text = "配件类型：";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CombCompany
            // 
            this.CombCompany.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CombCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CombCompany.FormattingEnabled = true;
            this.CombCompany.Location = new System.Drawing.Point(79, 10);
            this.CombCompany.Name = "CombCompany";
            this.CombCompany.Size = new System.Drawing.Size(145, 22);
            this.CombCompany.TabIndex = 150;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(33, 13);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(41, 12);
            this.label16.TabIndex = 149;
            this.label16.Text = "机构：";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProductPlace
            // 
            this.txtProductPlace.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtProductPlace.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtProductPlace.BackColor = System.Drawing.Color.Transparent;
            this.txtProductPlace.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtProductPlace.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtProductPlace.ForeImage = null;
            this.txtProductPlace.InputtingVerifyCondition = null;
            this.txtProductPlace.Location = new System.Drawing.Point(576, 8);
            this.txtProductPlace.MaxLengh = 32767;
            this.txtProductPlace.Multiline = false;
            this.txtProductPlace.Name = "txtProductPlace";
            this.txtProductPlace.Radius = 3;
            this.txtProductPlace.ReadOnly = false;
            this.txtProductPlace.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtProductPlace.ShowError = false;
            this.txtProductPlace.Size = new System.Drawing.Size(145, 23);
            this.txtProductPlace.TabIndex = 148;
            this.txtProductPlace.UseSystemPasswordChar = false;
            this.txtProductPlace.Value = "";
            this.txtProductPlace.VerifyCondition = null;
            this.txtProductPlace.VerifyType = null;
            this.txtProductPlace.VerifyTypeName = null;
            this.txtProductPlace.WaterMark = null;
            this.txtProductPlace.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(532, 14);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(41, 12);
            this.label20.TabIndex = 147;
            this.label20.Text = "产地：";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(508, 57);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(65, 12);
            this.label31.TabIndex = 142;
            this.label31.Text = "配件图号：";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnPartSearch
            // 
            this.btnPartSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPartSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPartSearch.BackgroundImage")));
            this.btnPartSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPartSearch.Caption = "查询";
            this.btnPartSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPartSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnPartSearch.DownImage = ((System.Drawing.Image)(resources.GetObject("btnPartSearch.DownImage")));
            this.btnPartSearch.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPartSearch.Location = new System.Drawing.Point(1003, 71);
            this.btnPartSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnPartSearch.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnPartSearch.MoveImage")));
            this.btnPartSearch.Name = "btnPartSearch";
            this.btnPartSearch.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnPartSearch.NormalImage")));
            this.btnPartSearch.Size = new System.Drawing.Size(60, 26);
            this.btnPartSearch.TabIndex = 31;
            this.btnPartSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnPartClear
            // 
            this.btnPartClear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPartClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPartClear.BackgroundImage")));
            this.btnPartClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPartClear.Caption = "清除";
            this.btnPartClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPartClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnPartClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnPartClear.DownImage")));
            this.btnPartClear.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPartClear.Location = new System.Drawing.Point(1003, 26);
            this.btnPartClear.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnPartClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnPartClear.MoveImage")));
            this.btnPartClear.Name = "btnPartClear";
            this.btnPartClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnPartClear.NormalImage")));
            this.btnPartClear.Size = new System.Drawing.Size(60, 26);
            this.btnPartClear.TabIndex = 30;
            this.btnPartClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // BtnExportMenu
            // 
            this.BtnExportMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExcelExport});
            this.BtnExportMenu.Name = "BtnImportMenu";
            this.BtnExportMenu.Size = new System.Drawing.Size(153, 50);
            // 
            // ExcelExport
            // 
            this.ExcelExport.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ExcelExport.Name = "ExcelExport";
            this.ExcelExport.Size = new System.Drawing.Size(152, 24);
            this.ExcelExport.Text = "● Excel导出";
            this.ExcelExport.Click += new System.EventHandler(this.ExcelExport_Click);
            // 
            // UCStockQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelEx4);
            this.Controls.Add(this.gvStockList);
            this.Controls.Add(this.TreeVPartCategory);
            this.Controls.Add(this.panelEx3);
            this.Name = "UCStockQuery";
            this.Size = new System.Drawing.Size(1141, 565);
            this.Load += new System.EventHandler(this.UCStockQuery_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.panelEx3, 0);
            this.Controls.SetChildIndex(this.TreeVPartCategory, 0);
            this.Controls.SetChildIndex(this.gvStockList, 0);
            this.Controls.SetChildIndex(this.panelEx4, 0);
            this.panelEx4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvStockList)).EndInit();
            this.panelEx3.ResumeLayout(false);
            this.panelEx3.PerformLayout();
            this.BtnExportMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx panelEx4;
        private ServiceStationClient.ComponentUI.WinFormPager winFormStockPager;
        private ServiceStationClient.ComponentUI.DataGridViewEx gvStockList;
        private ServiceStationClient.ComponentUI.TreeViewEx TreeVPartCategory;
        private ServiceStationClient.ComponentUI.PanelEx panelEx3;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtparts_cartype;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox CheckBGuarantyStock;
        private ServiceStationClient.ComponentUI.ComboBoxEx CombWarehouse;
        private System.Windows.Forms.Label label12;
        private ServiceStationClient.ComponentUI.TextBoxEx txtBarCode;
        private ServiceStationClient.ComponentUI.TextBoxEx txtCarFactoryCode;
        private ServiceStationClient.ComponentUI.TextBoxEx txtparts_brand;
        private ServiceStationClient.ComponentUI.TextBoxEx txtdrawing_num;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtparts_type;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private ServiceStationClient.ComponentUI.TextBoxEx txtparts_name;
        private System.Windows.Forms.Label label23;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtparts_code;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label15;
        private ServiceStationClient.ComponentUI.ComboBoxEx CombCompany;
        private System.Windows.Forms.Label label16;
        private ServiceStationClient.ComponentUI.TextBoxEx txtProductPlace;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label31;
        private ServiceStationClient.ComponentUI.ButtonEx btnPartSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnPartClear;
        private System.Windows.Forms.ContextMenuStrip BtnExportMenu;
        private System.Windows.Forms.ToolStripMenuItem ExcelExport;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrawingNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn PapCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn PhysicalCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn OccupyCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn AvailableCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn UntName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReferInPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReferOutPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn BarCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Weight;
        private System.Windows.Forms.DataGridViewTextBoxColumn HighestInPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn LowestOutPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn VIPPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirstInPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn SecondInPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThirdInPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirstOutPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn SecondOutPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThirdOutPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn VehicleType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductPlace;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartBrand;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactoryCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn ApplyFlag;
    }
}
