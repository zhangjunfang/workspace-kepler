using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;

namespace HXCPcClient.UCForm.BusinessAnalysis.StockReport
{
    public partial class UCPaperSummariz : UCReport
    {
        public UCPaperSummariz()
            : base("v_paper_summariz", "账面库存汇总表")
        {
            InitializeComponent();
            dgvReport.ColumnHeadersHeight = 40;
            dgvReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        }

        private void UCPaperSummariz_Load(object sender, EventArgs e)
        {
            CommonFuncCall.BindCompany(cboCompany, "全部");//绑定公司
            CommonFuncCall.BindWarehouse(cboWarehouse, "全部");//绑定仓库

            #region 显示合并表头
            dgvReport.MergeColumnNames.Add("上期结存");
            dgvReport.AddSpanHeader(6, 3, "上期结存");
            dgvReport.MergeColumnNames.Add("本期收入");
            dgvReport.AddSpanHeader(9, 3, "本期收入");
            dgvReport.MergeColumnNames.Add("本期发出");
            dgvReport.AddSpanHeader(12, 3, "本期发出");
            dgvReport.MergeColumnNames.Add("本期结存");
            dgvReport.AddSpanHeader(15, 3, "本期结存");
            #endregion

            #region 报表合并表头
            List<string> listInitial = new List<string>();
            listInitial.Add("单价");
            listInitial.Add("数量");
            listInitial.Add("金额");
            AddSpanRows("上期结存", listInitial);

            List<string> listRevenues = new List<string>();
            listRevenues.Add("单价");
            listRevenues.Add("数量");
            listRevenues.Add("金额");
            AddSpanRows("本期收入", listRevenues);

            List<string> listExpend = new List<string>();
            listExpend.Add("单价");
            listExpend.Add("数量");
            listExpend.Add("金额");
            AddSpanRows("本期发出", listExpend);

            List<string> listBalance = new List<string>();
            listBalance.Add("单价");
            listBalance.Add("数量");
            listBalance.Add("金额");
            AddSpanRows("本期结存", listBalance);
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
