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

namespace HXCPcClient.UCForm.BusinessAnalysis.StockReport
{
    public partial class UCStockTypeSummariz : UCReport
    {
        public UCStockTypeSummariz()
            : base("v_stock_type_summariz", "库存分类汇总表")
        {
            InitializeComponent();
            dgvReport.ColumnHeadersHeight = 40;
            dgvReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            Quick quick = new Quick();
            quick.BindParts(txtcPartsCode);
            txtcPartsCode.DataBacked += new ServiceStationClient.ComponentUI.TextBox.TextChooser.DataBackHandler(txtcPartsCode_DataBacked);
        }

        void txtcPartsCode_DataBacked(DataRow dr)
        {
            txtcPartsCode.Text = CommonCtrl.IsNullToString(dr["ser_parts_code"]);
            txtPartsName.Caption = CommonCtrl.IsNullToString(dr["parts_name"]);
        }

        private void UCStockTypeSummariz_Load(object sender, EventArgs e)
        {
            CommonFuncCall.BindCompany(cboCompany, "全部");//绑定公司
            CommonFuncCall.BindWarehouse(cboWarehouse, "全部");//绑定仓库

            #region 显示合并表头
            dgvReport.MergeColumnNames.Add("本期收入数量");
            dgvReport.AddSpanHeader(8, 5, "本期收入数量");
            dgvReport.MergeColumnNames.Add("本期发出数量");
            dgvReport.AddSpanHeader(13, 6, "本期发出数量");
            #endregion

            #region 报表合并表头
            List<string> listRevenues = new List<string>();
            listRevenues.Add("采购开单");
            listRevenues.Add("盘点");
            listRevenues.Add("调拨");
            listRevenues.Add("其它收货");
            listRevenues.Add("合计");
            AddSpanRows("本期收入数量", listRevenues);

            List<string> listExpend = new List<string>();
            listExpend.Add("销售开单");
            listExpend.Add("调拨");
            listExpend.Add("报损");
            listExpend.Add("领料");
            listExpend.Add("其它发货");
            listExpend.Add("合计");
            AddSpanRows("本期发出数量", listExpend);
            #endregion

            BindData();
        }
        //配件选择器
        private void txtcPartsCode_ChooserClick(object sender, EventArgs e)
        {
            frmParts frmPartsChooser = new frmParts();
            if (frmPartsChooser.ShowDialog() == DialogResult.OK)
            {
                txtcPartsCode.Text = frmPartsChooser.PartsCode;
                txtPartsName.Caption = frmPartsChooser.PartsName;
            }
        }
        //配件类别选择器
        private void txtcPartsType_ChooserClick(object sender, EventArgs e)
        {
            frmPartsType frmType = new frmPartsType();
            if (frmType.ShowDialog() == DialogResult.OK)
            {
                txtcPartsType.Text = frmType.TypeName;
            }
        }
        //配件车型选择器
        private void txtcPartsVehicle_ChooserClick(object sender, EventArgs e)
        {
            frmVehicleModels frmVM = new frmVehicleModels();
            if (frmVM.ShowDialog() == DialogResult.OK)
            {
                txtcPartsVehicle.Text = frmVM.VMName;
            }
        }
        //查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 绑定报表数据
        /// </summary>
        void BindData()
        {

        }
    }
}
