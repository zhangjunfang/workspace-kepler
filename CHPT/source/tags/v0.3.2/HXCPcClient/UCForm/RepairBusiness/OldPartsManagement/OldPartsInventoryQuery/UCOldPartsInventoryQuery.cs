using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using HXCPcClient.CommonClass;
using HXCPcClient.UCForm.BusinessAnalysis;
using ServiceStationClient.ComponentUI;
using SYSModel;

namespace HXCPcClient.UCForm.RepairBusiness.OldPartsManagement.OldPartsInventoryQuery
{
    /// <summary>
    /// 旧件管理-旧件库存查询管理
    /// Author：JC
    /// AddTime：2014.11.06
    /// </summary>
    public partial class UCOldPartsInventoryQuery : UCBase
    {
        #region 属性设置
        /// <summary>
        /// 查询条件
        /// </summary>
        string strWhere = string.Empty;
        #endregion
        public UCOldPartsInventoryQuery()
        {
            InitializeComponent();
            SetTopbuttonShow();
            UIAssistants.SetButtonStyle4QueryAndClear(btnQuery, btnClear);  //设置查询按钮和清除按钮样式
            CommonFuncCall.BindCompany(cobCompany, "全部");//公司信息           
            #region 显示合并表头
            dgvRData.ColumnHeadersHeight = 40;
            dgvRData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvRData.MergeColumnNames.Add("基本单位");
            dgvRData.AddSpanHeader(7, 5, "基本单位");
            #endregion
        }


        #region 顶部button显示设置
        /// <summary>
        ///  顶部button显示设置
        /// </summary>
        private void SetTopbuttonShow()
        {
            base.btnAdd.Visible = false;
            base.btnCopy.Visible = false;
            base.btnEdit.Visible = false;
            base.btnDelete.Visible = false;
            base.btnStatus.Visible = false;
            base.btnVerify.Visible = false;
            base.btnSave.Visible = false;
            base.btnImport.Visible = false;
            base.btnSync.Visible = false;
            base.btnConfirm.Visible = false;
            base.btnSubmit.Visible = false;
            base.btnCancel.Visible = false;         
            base.btnActivation.Visible = false;
        }
        #endregion

        #region 配件编码选择器事件
        /// <summary>
        /// 配件编码选择器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPartsCode_ChooserClick(object sender, EventArgs e)
        {
            frmParts frmPart = new frmParts();
            DialogResult result = frmPart.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtPartsCode.Text = frmPart.PartsCode;
                txtPartsCode.Tag = frmPart.PartsID;
                txtPartsName.Caption = frmPart.PartsName;
            }
        }
        #endregion

        #region 车型选择器事件
        /// <summary>
        /// 车型选择器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textChooser1_ChooserClick(object sender, EventArgs e)
        {
            frmVehicleModels frmModels = new frmVehicleModels();
            DialogResult result = frmModels.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtCarType.Text = frmModels.VMName;
                txtCarType.Tag = frmModels.VMID;
            }
        }
        #endregion    

        #region 查询事件
        private void btnQuery_Click(object sender, EventArgs e)
        {
            BindPageData();
        }
        #endregion

        #region 清除事件
        private void btnClear_Click(object sender, EventArgs e)
        {
            
            txtPartsName.Caption = string.Empty;
            txtPartsCode.Text = string.Empty;
            txtPartsCode.Tag = string.Empty;
            txtDrawingNo.Caption = string.Empty;
            txtCarType.Text = string.Empty;
            txtCarType.Tag = string.Empty;
            txtBrand.Caption = string.Empty;
            txtBar.Caption = string.Empty;
            txtAddress.Caption = string.Empty;
        }
        #endregion

        #region 分页查询绑定数据
        /// <summary>
        /// 分页查询绑定数据
        /// </summary>
        public void BindPageData()
        {
            try
            {
                strWhere = string.Format("enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'");//enable_flag 1未删除
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobCompany.SelectedValue)))//公司
                {
                    strWhere += string.Format(" and  org_id = '{0}'", cobCompany.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobWearhouse.SelectedValue)))//仓库
                {
                    strWhere += string.Format(" and  wh_id = '{0}'", cobWearhouse.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(txtAddress.Caption.Trim()))//产地
                {
                    strWhere += string.Format(" and  origin_place like '%{0}%'", txtAddress.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtBar.Caption.Trim())))//条码
                {
                    strWhere += string.Format(" and bar_code = '{0}'", txtBar.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtPartsCode.Text.Trim())))//配件编码
                {
                    strWhere += string.Format(" and parts_code = '{0}'", txtPartsCode.Text.Trim());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtPartsName.Caption.Trim())))//配件名称
                {
                    strWhere += string.Format(" and parts_name like '%{0}%'", txtPartsName.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtDrawingNo.Caption.Trim())))//配件图号
                {
                    strWhere += string.Format(" and drawn_no like '%{0}%'", txtDrawingNo.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtBrand.Caption.Trim())))//品牌
                {
                    strWhere += string.Format(" and trademark like '%{0}%'", txtBrand.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(tvData.SelectedNode.Name.ToString())))//配件类型
                {
                    strWhere += string.Format(" and parts_type_id = '{0}'", tvData.SelectedNode.Name.ToString());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtCarType.Tag)))//配件车型
                {
                    strWhere += string.Format(" and vehicle_model = '{0}'", txtCarType.Tag.ToString());
                }
                int recordCount;
                DataTable dt = DBHelper.GetTableByPage("分页查询旧件库存管理", "tb_maintain_oldpart_inventory", "*", strWhere, "", " order by create_time desc", page.PageIndex, page.PageSize, out recordCount);
                dgvRData.DataSource = dt;
                page.RecordCount = recordCount;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void page_PageIndexChanged(object sender, EventArgs e)
        {
            BindPageData();
        }

        #endregion

        #region 窗体Load事件
        private void UCOldPartsInventoryQuery_Load(object sender, EventArgs e)
        {
            #region 绑定左侧树信息
            tvData.Nodes.Clear();
            TreeNode root = new TreeNode();
            root.Text = "全部配件";
            root.Name = "";
            tvData.Nodes.Add(root);
            CreateTreeViewChildNode(root, "sys_parts_category");
            tvData.Nodes[0].Expand();
            tvData.SelectedNode = tvData.Nodes[0];
            #endregion
            BindWarehouseWay();
            cobWearhouse.SelectedValue="bc09db4d-3080-434c-bf3d-2404a9252920";//旧件库
        }
        #endregion

        #region 创建绑定树节点
        /// <summary>
        /// 创建绑定树节点
        /// </summary>
        /// <param name="parentNode">父节点</param>
        /// <param name="parentID">父编码</param>
        private void CreateTreeViewChildNode(TreeNode parentNode, string parentID)
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
        #endregion

        #region 绑定仓库信息
        /// <summary>
        /// 绑定仓库信息
        /// </summary>
        private void BindWarehouseWay()
        {
            DataTable dt = DBHelper.GetTable("", "tb_warehouse", "wh_id,wh_name", "", "", "");
            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem("", "全部"));
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new ListItem(dr["wh_id"], dr["wh_name"].ToString()));
            }
            cobWearhouse.DataSource = list;
            cobWearhouse.ValueMember = "Value";
            cobWearhouse.DisplayMember = "Text";
        }
        #endregion

        #region 选择左侧树时查询数据
        private void tvData_AfterSelect(object sender, TreeViewEventArgs e)
        {
            BindPageData();
        }
        #endregion

        #region 重写车型、品牌
        private void dgvRData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
            string fieldNmae = dgvRData.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("vehicle_model"))
            {
                e.Value = GetVehicleModels(e.Value.ToString());
            }
            if (fieldNmae.Equals("trademark"))
            {
                e.Value = GetDicName(e.Value.ToString());
            }
        }
        #endregion

        #region 根据码表ID获取其对应的名称
        /// <summary>
        /// 根据码表ID获取其对应的名称
        /// </summary>
        /// <param name="strId">码表Id值</param>
        private string GetDicName(string strId)
        {
            return DBHelper.GetSingleValue("获取码表值", "sys_dictionaries", "dic_name", "dic_id='" + strId + "'", "");
        }
        #endregion

        #region 根据车型编号获取车型名称
        /// <summary>
        /// 根据车型编号获取车型名称
        /// </summary>
        /// <param name="strMId"></param>
        /// <returns></returns>
        private string GetVehicleModels(string strMId)
        {
            return DBHelper.GetSingleValue("获取车型名称", "tb_vehicle_models", "vm_name", "vm_id='" + strMId + "'", "");
        }
        #endregion       

        #region 在单元格的任意位置单击鼠标时发生
        private void dgvRData_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == colCheck.Index)
            {
                return;
            }
            //清空已选择框
            foreach (DataGridViewRow dgvr in dgvRData.Rows)
            {
                object check = dgvr.Cells[colCheck.Name].EditedFormattedValue;
                if (check != null && (bool)check)
                {
                    dgvr.Cells[colCheck.Name].Value = false;
                }
            }
            //选择当前行
            dgvRData.Rows[e.RowIndex].Cells[colCheck.Name].Value = true;
        }
        #endregion
    }
}
