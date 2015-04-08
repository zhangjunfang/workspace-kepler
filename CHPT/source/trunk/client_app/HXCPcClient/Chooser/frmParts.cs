using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using HXCPcClient.CommonClass;
using SYSModel;

namespace HXCPcClient.Chooser
{
    public partial class frmParts : FormChooser
    {
        #region 属性
        /// <summary>
        /// 配件id
        /// </summary>
        public string PartsID = string.Empty;
        /// <summary>
        /// 配件编码
        /// </summary>
        public string PartsCode = string.Empty;
        /// <summary>
        /// 配件名称
        /// </summary>
        public string PartsName = string.Empty;
        /// <summary>
        /// 参考单价
        /// </summary>
        public string ref_out_price = string.Empty;
        /// <summary>
        /// 车厂编码
        /// </summary>
        public string CarPartsCode { get; set; }
        /// <summary>
        /// 配件描述
        /// </summary>
        public String Remark { get; set; }
        public delegate void SelectedRowDelegate(frmParts parts);
        /// <summary>
        /// 选择行事件
        /// </summary>
        public event SelectedRowDelegate SelectedRowEvent;
        #endregion
        public frmParts()
        {
            InitializeComponent();
        }
        //页面加载
        private void frmParts_Load(object sender, EventArgs e)
        {
            BindTree();
            DataSources.BindComBoxDataEnum(cboDataSource, typeof(DataSources.EnumDataSources), true);//绑定数据源
            CommonFuncCall.BindComBoxDataSource(cboPartsBrand, "sys_parts_brand", "全部");
            CommonFuncCall.BindWarehouse(cboWarehouse, "全部");
            cboPartsBrand.DropDownStyle = ComboBoxStyle.DropDown;
            PartsLoad();
            //分页控件会调用绑定事件
            //BindParts(tvPartsType.SelectedNode);
        }
        /// <summary>
        /// 页面加载完后
        /// </summary>
        public virtual void PartsLoad()
        {

        }
        #region 事件
        //关闭
        private void btnColse_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //确定
        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedRow();
            DialogResult = DialogResult.OK;
            this.Close();
        }
        //保存当前页
        private void btnSave_Click(object sender, EventArgs e)
        {
            SelectedRow();
        }
        //清空
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPartsName.Caption = string.Empty;
            txtDrawingNum.Caption = string.Empty;
            txtCarPartsCode.Caption = string.Empty;
            txtPartsCode.Caption = string.Empty;
            txtSupperOne.Caption = string.Empty;
            cboDataSource.SelectedIndex = 0;
            cboPartsBrand.SelectedIndex = 0;
            cboWarehouse.SelectedIndex = 0;
        }
        //查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindParts(tvPartsType.SelectedNode);
        }
        //选择仓库，查询配件的当前仓库
        private void cboWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgvParts.CurrentRow == null)
            {
                return;
            }
            string parts_id = dgvParts.CurrentRow.Cells[colPartsID.Name].Value.ToString();
            BindStock(parts_id, CommonCtrl.IsNullToString(cboWarehouse.SelectedValue));
        }
        //选择配件，查询配件的当前仓库
        private void dgvParts_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            string parts_id = dgvParts.Rows[e.RowIndex].Cells[colPartsID.Name].Value.ToString();
            BindStock(parts_id, CommonCtrl.IsNullToString(cboWarehouse.SelectedValue));
        }
        //新增配件
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!MessageBoxEx.ShowQuestion("新增配件会关闭当前页面，确认要新增吗？"))
            {
                return;
            }
            if (GlobalStaticObj.AppMainForm is HXCMainForm)
            {
                HXCPcClient.UCForm.DataManage.Parts.UCPartsAddOrEdit addParts = new UCForm.DataManage.Parts.UCPartsAddOrEdit(UCForm.WindowStatus.Add, null, null);
                string tag = "UCPartsAdd|CL_DataManagement_BasicData|CL_DataManagement_BasicData_Accessories";
                ((HXCMainForm)GlobalStaticObj.AppMainForm).addUserControls(addParts, "配件档案-新增", "UCPartsAdd", tag, "");
            }
            this.Close();
        }
        //翻页
        void page_PageIndexChanged(object sender, EventArgs e)
        {
            BindParts(tvPartsType.SelectedNode);
        }
        //选择配件类别节点时
        private void tvPartsType_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            BindParts(e.Node);
        }
        #endregion

        #region 方法
        /// <summary>
        /// 选择当前行
        /// </summary>
        void SelectedRow()
        {
            if (dgvParts.CurrentRow == null)
            {
                return;
            }
            PartsID = CommonCtrl.IsNullToString(dgvParts.CurrentRow.Cells[colPartsID.Name].Value);
            PartsCode = CommonCtrl.IsNullToString(dgvParts.CurrentRow.Cells[colSerPartsCode.Name].Value);
            CarPartsCode = CommonCtrl.IsNullToString(dgvParts.CurrentRow.Cells[colCarPartsCode.Name].Value);
            PartsName = CommonCtrl.IsNullToString(dgvParts.CurrentRow.Cells[colPartsName.Name].Value);
            ref_out_price = CommonCtrl.IsNullToString(dgvParts.CurrentRow.Cells["colref_out_price"].Value);
            if (SelectedRowEvent != null)
            {
                SelectedRowEvent(this);
            }
        }
        /// <summary>
        /// 绑定树
        /// </summary>
        void BindTree()
        {
            tvPartsType.Nodes.Clear();
            TreeNode root = new TreeNode();
            root.Text = "全部配件";
            root.Name = "";
            tvPartsType.Nodes.Add(root);
            CreateTreeViewChildNode(root, "sys_parts_category");
            tvPartsType.ExpandAll();
        }
        /// <summary>
        /// 创建绑定树节点
        /// </summary>
        /// <param name="parentNode">父节点</param>
        /// <param name="parentID">父编码</param>
        void CreateTreeViewChildNode(TreeNode parentNode, string parentID)
        {
            if (parentID.Length == 0)
            {
                return;
            }
            DataTable dt = CommonCtrl.GetDictByCode(parentID, false);
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode node = new TreeNode(CommonCtrl.IsNullToString(dr["dic_name"]));
                node.Name = CommonCtrl.IsNullToString(dr["dic_id"]);
                parentNode.Nodes.Add(node);
                string dicCode = CommonCtrl.IsNullToString(dr["dic_Code"]);
                CreateTreeViewChildNode(node, dicCode);
            }
        }


        BackgroundWorker Slience = null;
        DataTable dt = null;
        /// <summary>
        /// 绑定配件
        /// </summary>
        void BindParts(TreeNode node)
        {
            Slience = new BackgroundWorker();
            Slience.WorkerReportsProgress = false;
            Slience.WorkerSupportsCancellation = true;
            Slience.DoWork += Slience_DoWork;
            Slience.RunWorkerCompleted += Slience_RunWorkerCompleted;
            Slience.RunWorkerAsync(node);
            
        }

        void Slience_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvParts.DataSource = dt;
            page.RecordCount = int.Parse(e.Result.ToString());
            page.SetBtnState();
        }

        void Slience_DoWork(object sender, DoWorkEventArgs e)
        {
            TreeNode node = e.Argument as TreeNode;
            StringBuilder sbWhere = new StringBuilder();
            sbWhere.Append(" 1=1");
            if (node != null)
            {
                if (node.Name.Length > 0)
                {
                    sbWhere.AppendFormat(" and parts_type='{0}'", node.Name);
                }
            }


            SlienceWorker.CrossThreadInvoke(txtPartsName, new MethodInvoker(() =>
                {
                    string partName = txtPartsName.Caption.Trim();//配件名称
                    if (partName.Length > 0)
                    {
                        sbWhere.AppendFormat(" and parts_name like '%{0}%'", partName);
                    }
                }));


            SlienceWorker.CrossThreadInvoke(cboDataSource, new MethodInvoker(() =>
            {
                string datasource = CommonCtrl.IsNullToString(cboDataSource.SelectedValue);//数据来源
                if (datasource.Length > 0)
                {
                    sbWhere.AppendFormat(" and data_source='{0}'", datasource);
                }
            }));




            SlienceWorker.CrossThreadInvoke(txtSupperOne, new MethodInvoker(() =>
            {
                string supper = txtSupperOne.Caption.Trim();//供应商
                if (supper.Length > 0)
                {
                    sbWhere.AppendFormat(" and sup_full_name like '%{0}%'", supper);
                }
            }));



            SlienceWorker.CrossThreadInvoke(txtPartsCode, new MethodInvoker(() =>
            {
                string partsCode = txtPartsCode.Caption.Trim();//配件编码
                if (partsCode.Length > 0)
                {
                    sbWhere.AppendFormat(" and ser_parts_code like '%{0}%'", partsCode);
                }
            }));



            SlienceWorker.CrossThreadInvoke(txtCarPartsCode, new MethodInvoker(() =>
            {
                string carPartCode = txtCarPartsCode.Caption.Trim();//车厂编码
                if (carPartCode.Length > 0)
                {
                    sbWhere.AppendFormat(" and car_parts_code like '%{0}%'", carPartCode);
                }
            }));



            SlienceWorker.CrossThreadInvoke(txtDrawingNum, new MethodInvoker(() =>
            {
                string drawing_num = txtDrawingNum.Caption.Trim();//图号
                if (drawing_num.Length > 0)
                {
                    sbWhere.AppendFormat(" and drawing_num like '%{0}%'", drawing_num);
                }
            }));



            SlienceWorker.CrossThreadInvoke(cboPartsBrand, new MethodInvoker(() =>
            {
                string partsBrand = CommonCtrl.IsNullToString(cboPartsBrand.Text).Trim();//配件品牌
                if (partsBrand.Length > 0 && partsBrand != "全部")
                {
                    sbWhere.AppendFormat(" and brand_name like '%{0}%'", partsBrand);
                }
            }));

            int recordCount = 0;
            dt = DBHelper.GetTableByPage("", "v_parts_chooser", "*", sbWhere.ToString(), "", "parts_name", page.PageIndex, page.PageSize, out recordCount);



            e.Result = recordCount;
        }
        /// <summary>
        /// 绑定库存
        /// </summary>
        /// <param name="parts_id">配件ID</param>
        /// <param name="wh_id">仓库ID</param>
        void BindStock(string parts_id, string wh_id)
        {
            StringBuilder sbWhere = new StringBuilder();
            sbWhere.AppendFormat("stock_part_id='{0}'", parts_id);
            if (wh_id.Length > 0)
            {
                sbWhere.AppendFormat(" and wh_id='{0}'", wh_id);
            }
            //            select parts_name,wh_name,
            //sum(case when statistic_Type=0 then statistic_count else 0 end) paper_count,
            //sum(case when statistic_Type=1 then statistic_count else 0 end) actual_count 
            //from tb_parts_stock_p where stock_part_id='99baa9dd-feab-45fe-a1be-1f2d295a6ba4' 
            //group by parts_name,wh_name
            //            string fileds = string.Format(@"parts_name,wh_name,case when statistic_Type={0} then statistic_count else 0 end paper_count,
            //case when statistic_Type={1} then statistic_count else 0 end actual_count", (int)DataSources.EnumStatisticType.PaperCount, (int)DataSources.EnumStatisticType.ActualCount);

            string stockSql = string.Format(@"select parts_name,wh_name,
            sum(case when statistic_Type=0 then statistic_count else 0 end) paper_count,
            sum(case when statistic_Type=1 then statistic_count else 0 end) actual_count 
            from tb_parts_stock_p where stock_part_id='{0}' 
            group by parts_name,wh_name", parts_id);

            SQLObj partsStock = new SQLObj();
            partsStock.cmdType = CommandType.Text;
            partsStock.Param = new Dictionary<string, ParamObj>();
            partsStock.sqlString = stockSql;
            DataTable dt = DBHelper.GetDataSet("查询配件库存", partsStock).Tables[0];
            dgvDetail.DataSource = dt;
        }
        #endregion

        private void dgvParts_DoubleClick(object sender, EventArgs e)
        {
            btnOK_Click(sender, e);
        }
    }
}
