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
    public partial class UCWarehouseDifferentiate : UCReport
    {
        public UCWarehouseDifferentiate()
            : base("v_warehouse_differentiate", "分仓库数量金额表")
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

        private void UCWarehouseDifferentiate_Load(object sender, EventArgs e)
        {
            CommonFuncCall.BindCompany(cboCompany, "全部");//绑定公司

            #region 显示合并表头
            dgvReport.MergeColumnNames.Add("合计");
            dgvReport.AddSpanHeader(7, 2, "合计");
            dgvReport.MergeColumnNames.Add("仓库1");
            dgvReport.AddSpanHeader(9, 2, "仓库1");
            #endregion

            #region 报表合并表头
            List<string> listRevenues = new List<string>();
            listRevenues.Add("数量");
            listRevenues.Add("金额");
            AddSpanRows("合计", listRevenues);

            List<string> listExpend = new List<string>();
            listExpend.Add("数量");
            listExpend.Add("金额");
            AddSpanRows("仓库1", listExpend);
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
