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
    public partial class UCStockAging : UCReport
    {
        public UCStockAging()
            : base("v_stock_aging", "库龄分析报表")
        {
            InitializeComponent();
            cboWarehouse.Tag = "wh_id";
            dgvReport.ColumnHeadersHeight = 40;
            dgvReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            colPrice.DefaultCellStyle = styleMoney;
            colNum.DefaultCellStyle = styleMoney;
            colMoney.DefaultCellStyle = styleMoney;
            colNum1.DefaultCellStyle = styleMoney;
            colMoney1.DefaultCellStyle = styleMoney;
            colNum2.DefaultCellStyle = styleMoney;
            colMoney2.DefaultCellStyle = styleMoney;
            colNum3.DefaultCellStyle = styleMoney;
            colMoney3.DefaultCellStyle = styleMoney;

            Quick quick = new Quick();
            quick.BindParts(txtcPartsCode);
            txtcPartsCode.DataBacked += new ServiceStationClient.ComponentUI.TextBox.TextChooser.DataBackHandler(txtcPartsCode_DataBacked);
        }

        void txtcPartsCode_DataBacked(DataRow dr)
        {
            txtcPartsCode.Text = CommonCtrl.IsNullToString(dr["ser_parts_code"]);
            txtPartsName.Caption = CommonCtrl.IsNullToString(dr["parts_name"]);
        }

        private void UCStockAging_Load(object sender, EventArgs e)
        {
            colNum2.Visible = false;
            colMoney2.Visible = false;
            colNum3.Visible = false;
            colMoney3.Visible = false;
            CommonFuncCall.BindCompany(cboCompany, "全部");//绑定公司
            CommonFuncCall.BindWarehouse(cboWarehouse, "全部");//绑定仓库

            Merage();
            BindData();
        }
        /// <summary>
        /// 合并表头
        /// </summary>
        void Merage()
        {
            dgvReport.ClearSpanInfo();
            dgvReport.MergeColumnNames.Clear();
            string header1 = string.Format("0-{0}天", nudTiming1.Value);
            string header2 = string.Format("{0}-{1}天", nudTiming1.Value, nudTiming2.Value);
            string header3 = string.Format("{0}-{1}天", nudTiming2.Value, nudTiming3.Value);
            #region 显示合并表头
            dgvReport.MergeColumnNames.Add(header1);
            dgvReport.AddSpanHeader(10, 2, header1);
            dgvReport.MergeColumnNames.Add(header2);
            dgvReport.AddSpanHeader(12, 2, header2);
            dgvReport.MergeColumnNames.Add(header3);
            dgvReport.AddSpanHeader(14, 2, header3);
            #endregion
            dicSpanRows.Clear();
            #region 报表合并表头
            List<string> list1 = new List<string>();
            list1.Add("ThirtyPhyCount");
            list1.Add("ThirtyAmount");
            AddSpanRows(header1, list1);

            List<string> list2 = new List<string>();
            list2.Add("SixtyPhyCount");
            list2.Add("SixtyAmount");
            AddSpanRows(header2, list2);

            List<string> list3 = new List<string>();
            list3.Add("MoreSixtyPhyCount");
            list3.Add("MoreSixtyAmount");
            AddSpanRows(header3, list3);
            #endregion
        }

        private void cbTiming2_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbTiming2.Checked)
            {
                cbTiming3.Checked = false;
            }
            nudTiming2.Enabled = cbTiming2.Checked;
            colNum2.Visible = cbTiming2.Checked;
            colMoney2.Visible = cbTiming2.Checked;
        }

        private void cbTiming3_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTiming3.Checked)
            {
                cbTiming2.Checked = true;
            }
            nudTiming3.Enabled = cbTiming3.Checked;
            colMoney3.Visible = cbTiming3.Checked;
            colNum3.Visible = cbTiming3.Checked;
        }

        private void txtcPartsCode_ChooserClick(object sender, EventArgs e)
        {
            frmParts frmPartsChooser = new frmParts();
            if (frmPartsChooser.ShowDialog() == DialogResult.OK)
            {
                txtcPartsCode.Text = frmPartsChooser.PartsCode;
                txtPartsName.Caption = frmPartsChooser.PartsName;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        void BindData()
        {
            string strWhere = GetWhere();
            dt = CommonFuncCall.GetStockAge(strWhere, Convert.ToInt32(nudTiming1.Value), Convert.ToInt32(nudTiming2.Value), Convert.ToInt32(nudTiming3.Value));
            dgvReport.DataSource = dt;
        }

        private void nudTiming1_ValueChanged(object sender, EventArgs e)
        {
            Merage();
        }

        private void nudTiming2_ValueChanged(object sender, EventArgs e)
        {
            Merage();
        }

        private void nudTiming3_ValueChanged(object sender, EventArgs e)
        {
            Merage();
        }
    }
}
