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
    public partial class UCPaperDetail : UCReport
    {
        public UCPaperDetail()
            : base("v_paper_detail", "账面库存明细表")
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

        private void UCPaperDetail_Load(object sender, EventArgs e)
        {
            CommonFuncCall.BindCompany(cboCompany, "全部");//绑定公司
            CommonFuncCall.BindWarehouse(cboWarehouse, "全部");//绑定仓库

            #region 显示合并表头
            dgvReport.MergeColumnNames.Add("收入");
            dgvReport.AddSpanHeader(5, 3, "收入");
            dgvReport.MergeColumnNames.Add("发出");
            dgvReport.AddSpanHeader(8, 3, "发出");
            dgvReport.MergeColumnNames.Add("结存");
            dgvReport.AddSpanHeader(11, 3, "结存");
            #endregion

            #region 报表合并表头
            List<string> listRevenues = new List<string>();
            listRevenues.Add("单价");
            listRevenues.Add("数量");
            listRevenues.Add("金额");
            AddSpanRows("收入", listRevenues);

            List<string> listExpend = new List<string>();
            listExpend.Add("单价");
            listExpend.Add("数量");
            listExpend.Add("金额");
            AddSpanRows("支出", listExpend);

            List<string> listBalance = new List<string>();
            listBalance.Add("单价");
            listBalance.Add("数量");
            listBalance.Add("金额");
            AddSpanRows("结存", listBalance);
            #endregion

            BindData();

            //双击查看明细,要手动调用权限
            if (this.Name != "CL_BusinessAnalysis_Stock_PaperDet")
            {
                base.RoleButtonStstus("CL_BusinessAnalysis_Stock_PaperDet");
            }
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
