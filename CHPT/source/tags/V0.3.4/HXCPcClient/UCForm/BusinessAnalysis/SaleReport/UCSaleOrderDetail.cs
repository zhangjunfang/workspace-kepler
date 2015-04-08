using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using HXCPcClient.CommonClass;
using HXCPcClient.UCForm.AccessoriesBusiness.SaleManagement.SaleOrder;
using SYSModel;

namespace HXCPcClient.UCForm.BusinessAnalysis.SaleReport
{
    public partial class UCSaleOrderDetail : UCReport
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
        public UCSaleOrderDetail()
            : base("v_sale_order_detail_report", "销售订单明细表")
        {
            InitializeComponent();
            dgvReport.ColumnHeadersHeight = 40;
            dgvReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        }
        //窗体加载
        private void UCSaleOrderDetail_Load(object sender, EventArgs e)
        {
            //公司
            CommonFuncCall.BindCompany(cboCompany, "全部");
            //客户类别
            CommonFuncCall.BindComBoxDataSource(cboCustType, "sys_customer_category", "全部");
            #region 初始化查询
            txtcCustCode.Text = supCode;
            txtCustName.Caption = supName;
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
            dgvReport.AddSpanHeader(16, 3, "订货");
            dgvReport.MergeColumnNames.Add("已收");
            dgvReport.AddSpanHeader(19, 3, "已收");
            dgvReport.MergeColumnNames.Add("中止");
            dgvReport.AddSpanHeader(22, 3, "中止");
            dgvReport.MergeColumnNames.Add("未收");
            dgvReport.AddSpanHeader(25, 3, "未收");
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
        }
        //客户选择
        private void txtcCustCode_ChooserClick(object sender, EventArgs e)
        {
            frmCustomerInfo frmCust = new frmCustomerInfo();
            if (frmCust.ShowDialog() == DialogResult.OK)
            {
                txtcCustCode.Text = frmCust.strCustomerNo;
                txtCustName.Caption = frmCust.strCustomerName;
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
            //查询字段
            string files = @"公司,单据编号,单据日期,合同号,发货日期,配件编码,配件名称,图号,配件品牌,配件类别,厂商编码,业务单位,辅助单位,赠品,
                            业务单价,订货数量,'' 订货辅助数量,订货数量*业务单价 订货金额,已收数量,'' 已收辅助数量,已收数量*业务单价 已收金额,
                            中止数量,'' 中止辅助数量,中止数量*业务单价 中止金额,未完成数量,'' 未完成辅助数量,未完成数量*业务单价 未完成金额,备注,cust_code,cust_name,sale_order_id";
            dt = DBHelper.GetTable("", "v_sale_order_detail_report", files, GetWhere(), "", "order by cust_code");
            dt.DataTableToDate("单据日期");
            dt.DataTableToDate("发货日期");
            //按客户分组
            dt.DataTableGroup("cust_name", "公司", "客户名称：", "cust_code", "单据编号", "客户编码：", null);
            dgvReport.DataSource = dt;
        }
        /// <summary>
        /// 打开销售订单
        /// </summary>
        void OpenDocument()
        {
            if (dgvReport.CurrentRow == null)
            {
                return;
            }
            string order_id = dgvReport.CurrentRow.Cells[colID.Name].Value.ToString();
            if (order_id.Length == 0)
            {
                return;
            }
            UCSaleOrderView view = new UCSaleOrderView(order_id, ((int)DataSources.EnumAuditStatus.SUBMIT).ToString(), null);
            base.addUserControl(view, "销售订单-查看", "UCPurchaseOrderView" + order_id + "", this.Tag.ToString(), this.Name);
        }

        private void dgvReport_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenDocument();
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
    }
}
