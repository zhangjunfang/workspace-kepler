using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using Utility.Common;
using HXCPcClient.CommonClass;
using HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchaseOrder;
using SYSModel;
using HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.YuTongPurchaseOrder;

namespace HXCPcClient.UCForm.BusinessAnalysis.PurchaseReport
{
    public partial class UCPurchaseOrderDetail : UCReport
    {
        #region 属性，用来设置查询条件的默认值
        public string supCode = string.Empty;
        public string supName = string.Empty;
        public string partsName = string.Empty;
        public string partsCode = string.Empty;
        public string partsType = string.Empty;
        public string company = string.Empty;
        public string orgId = string.Empty;
        public string startDate = DateTime.Now.ToString("yyyy-MM-01");
        public string endDate = DateTime.Now.ToString("yyyy-MM-dd");
        public string drawNum = string.Empty;
        public string partsBrand = string.Empty;
        #endregion
        public UCPurchaseOrderDetail()
            : base("v_parts_purchase_order_detail_report", "采购订单明细表")
        {
            InitializeComponent();
            //dgvReport.ColumnTreeView=new TreeView
            dgvReport.ColumnHeadersHeight = 40;
            dgvReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            colPrice.DefaultCellStyle = styleMoney;
            colOrderNum.DefaultCellStyle = styleNum;
            colOrderAssist.DefaultCellStyle = styleNum;
            colOrderMoney.DefaultCellStyle = styleMoney;
            colDoneNum.DefaultCellStyle = styleNum;
            colDoneAssist.DefaultCellStyle = styleNum;
            colDoneMoney.DefaultCellStyle = styleMoney;
            colSuspendNum.DefaultCellStyle = styleNum;
            colSuspendAssist.DefaultCellStyle = styleNum;
            colSuspendMoney.DefaultCellStyle = styleMoney;
            colUncollectedNum.DefaultCellStyle = styleNum;
            colUncollectedAssist.DefaultCellStyle = styleNum;
            colUncollectedMoney.DefaultCellStyle = styleMoney;

            Quick qSupp = new Quick();

            qSupp.BindSupplier(txtcSupCode);
            txtcSupCode.DataBacked += new ServiceStationClient.ComponentUI.TextBox.TextChooser.DataBackHandler(txtcSup_code_DataBacked);

            qSupp.BindParts(txtcPartsCode);
            txtcPartsCode.DataBacked += new ServiceStationClient.ComponentUI.TextBox.TextChooser.DataBackHandler(txtcparts_code_DataBacked);
        }


        void txtcparts_code_DataBacked(DataRow dr)
        {
            txtcPartsCode.Text = CommonCtrl.IsNullToString(dr["ser_parts_code"]);
            txtPartsName.Caption = CommonCtrl.IsNullToString(dr["parts_name"]);
        }

        void txtcSup_code_DataBacked(DataRow dr)
        {
            txtSupName.Caption = CommonCtrl.IsNullToString(dr["sup_full_name"]);
            txtcSupCode.Text = CommonCtrl.IsNullToString(dr["sup_code"]);
        }
        //窗体加载
        private void UCPurchaseOrderDetail_Load(object sender, EventArgs e)
        {
            //公司
            CommonFuncCall.BindCompany(cboCompany, "全部");
            //供应商类别
            CommonFuncCall.BindComBoxDataSource(cboSupType, "sys_supplier_category", "全部");
            #region 初始化查询
            txtcSupCode.Text = supCode;
            txtSupName.Caption = supName;
            cboCompany.SelectedValue = company;
            cboOrg.SelectedValue = orgId;
            txtcPartsCode.Text = partsCode;
            txtPartsName.Caption = partsName;
            txtcPartsType.Text = partsType;
            txtDrawingNum.Caption = drawNum;
            txtPartsBrand.Caption = partsBrand;
            diCreateTime.StartDate = startDate;
            diCreateTime.EndDate = endDate;
            #endregion
            #region 显示合并表头
            dgvReport.MergeColumnNames.Add("订货");
            dgvReport.AddSpanHeader(15, 3, "订货");
            dgvReport.MergeColumnNames.Add("已收");
            dgvReport.AddSpanHeader(18, 3, "已收");
            dgvReport.MergeColumnNames.Add("中止");
            dgvReport.AddSpanHeader(21, 3, "中止");
            dgvReport.MergeColumnNames.Add("未收");
            dgvReport.AddSpanHeader(24, 3, "未收");
            #endregion
            #region 报表合并表头
            List<string> listDing = new List<string>();
            listDing.Add("订货数量");
            listDing.Add("订货辅助数量");
            listDing.Add("订货金额");
            AddSpanRows("订货", listDing);

            List<string> listYiShou = new List<string>();
            listYiShou.Add("已收数量");
            listYiShou.Add("已收辅助数量");
            listYiShou.Add("已收金额");
            AddSpanRows("已收", listYiShou);

            List<string> listZhongZhi = new List<string>();
            listZhongZhi.Add("中止数量");
            listZhongZhi.Add("中止辅助数量");
            listZhongZhi.Add("中止金额");
            AddSpanRows("中止", listZhongZhi);

            List<string> listWeiShou = new List<string>();
            listWeiShou.Add("未收数量");
            listWeiShou.Add("未收辅助数量");
            listWeiShou.Add("未收金额");
            AddSpanRows("未收", listWeiShou);
            #endregion
            BindData();

            //双击查看明细,要手动调用权限
            if (this.Name != "CL_BusinessAnalysis_Purchase_OrderDet")
            {
                base.RoleButtonStstus("CL_BusinessAnalysis_Purchase_OrderDet");
            }
        }
        //选择公司，绑定公司所有部门
        private void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCompany.SelectedValue == null)
            {
                return;
            }
            CommonFuncCall.BindDepartment(cboOrg, cboCompany.SelectedValue.ToString(), "全部");
        }
        //供应商选择
        private void txtcSupCode_ChooserClick(object sender, EventArgs e)
        {
            frmSupplier frmSup = new frmSupplier();
            if (frmSup.ShowDialog() == DialogResult.OK)
            {
                txtcSupCode.Text = frmSup.supperCode;
                txtSupName.Caption = frmSup.supperName;
            }
        }
        //配件选择
        private void txtcPartsCode_ChooserClick(object sender, EventArgs e)
        {
            frmParts parts = new frmParts();
            if (parts.ShowDialog() == DialogResult.OK)
            {
                txtcPartsCode.Text = parts.PartsCode;
                txtPartsName.Caption = parts.PartsName;
            }
        }
        //配件类型选择
        private void txtcPartsType_ChooserClick(object sender, EventArgs e)
        {
            frmPartsType frmType = new frmPartsType();
            if (frmType.ShowDialog() == DialogResult.OK)
            {
                txtcPartsType.Text = frmType.TypeName;
            }
        }
        //查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        void BindData()
        {
            string files = @"公司,单据编号,单据日期,合同号,到货日期,配件编码,配件名称,图号,配件品牌,配件类别,厂商编码,业务单位,辅助单位,赠品,
                            业务单价,订货数量,'' 订货辅助数量,订货数量*业务单价 订货金额,已收数量,'' 已收辅助数量,已收数量*业务单价 已收金额,
                            中止数量,'' 中止辅助数量,中止数量*业务单价 中止金额,未完成数量,'' 未完成辅助数量,未完成数量*业务单价 未完成金额,备注,sup_code,sup_name,order_id,order_type";
            dt = DBHelper.GetTable("", "v_parts_purchase_order_detail_report", files, GetWhere(), "", "order by sup_name");
            dt.DataTableToDate("单据日期");
            dt.DataTableToDate("到货日期");
            //按供应商分组
            dt.DataTableGroup("sup_name", "公司", "供应商名称：", "sup_code", "单据编号", "供应商编码：", null);
            dgvReport.DataSource = dt;
        }

        private void dgvReport_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenDocument();
        }
        /// <summary>
        /// 打开采购订单
        /// </summary>
        void OpenDocument()
        {
            if (dgvReport.CurrentRow == null)
            {
                return;
            }
            string order_id = dgvReport.CurrentRow.Cells[colID.Name].Value.ToString();
            string type = CommonCtrl.IsNullToString(dgvReport.CurrentRow.Cells[colType.Name].Value);
            if (order_id.Length == 0)
            {
                return;
            }
            if (type == "1")
            {
                UCPurchaseOrderView UCPurchaseOrderView = new UCPurchaseOrderView(order_id, ((int)DataSources.EnumAuditStatus.SUBMIT).ToString(), null);
                base.addUserControl(UCPurchaseOrderView, "采购订单-查看", "UCPurchaseOrderView" + order_id + "", this.Tag.ToString(), this.Name);
            }
            else if (type == "2")
            {
                UCYTView ucYtView = new UCYTView(order_id, null);
                base.addUserControl(ucYtView, "宇通采购订单-查看", "UCPurchaseOrderView" + order_id, this.Tag.ToString(), this.Name);
            }
        }
    }
}
