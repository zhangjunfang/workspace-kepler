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
    public partial class UCActualSummariz : UCReport
    {
        public UCActualSummariz()
            : base("v_actual_summariz", "实际库存汇总表")
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

        private void UCActualSummariz_Load(object sender, EventArgs e)
        {
            CommonFuncCall.BindCompany(cboCompany, "全部");//绑定公司
            CommonFuncCall.BindWarehouse(cboWarehouse, "全部");//绑定仓库

            #region 显示合并表头
            dgvReport.MergeColumnNames.Add("期初结存数量");
            dgvReport.AddSpanHeader(7, 3, "期初结存数量");
            dgvReport.MergeColumnNames.Add("本期收入数量");
            dgvReport.AddSpanHeader(10, 4, "本期收入数量");
            dgvReport.MergeColumnNames.Add("本期发出数量");
            dgvReport.AddSpanHeader(14, 4, "本期发出数量");
            dgvReport.MergeColumnNames.Add("期末结存数量");
            dgvReport.AddSpanHeader(18, 3, "期末结存数量");
            #endregion

            #region 报表合并表头
            List<string> listInitial = new List<string>();
            listInitial.Add("账面数量");
            listInitial.Add("实际数量");
            listInitial.Add("账实差");
            AddSpanRows("期初结存数量", listInitial);

            List<string> listRevenues = new List<string>();
            listRevenues.Add("实际数量（基）");
            listRevenues.Add("实际数量（辅）");
            listRevenues.Add("账面数量（基）");
            listRevenues.Add("账面数量（辅）");
            AddSpanRows("本期收入数量", listRevenues);

            List<string> listExpend = new List<string>();
            listExpend.Add("实际数量（基）");
            listExpend.Add("实际数量（辅）");
            listExpend.Add("账面数量（基）");
            listExpend.Add("账面数量（辅）");
            AddSpanRows("本期发出数量", listExpend);

            List<string> listBalance = new List<string>();
            listBalance.Add("账面数量");
            listBalance.Add("实际数量");
            listBalance.Add("帐实差");
            AddSpanRows("期末结存数量", listBalance);
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
