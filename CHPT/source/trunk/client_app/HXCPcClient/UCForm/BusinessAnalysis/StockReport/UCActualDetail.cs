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
    public partial class UCActualDetail : UCReport
    {
        public UCActualDetail()
            : base("v_actual_detail", "实际库存明细表")
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

        private void UCActualDetail_Load(object sender, EventArgs e)
        {
            CommonFuncCall.BindCompany(cboCompany, "全部");//绑定公司
            CommonFuncCall.BindWarehouse(cboWarehouse, "全部");//绑定仓库
            #region 显示合并表头
            dgvReport.MergeColumnNames.Add("收入数量");
            dgvReport.AddSpanHeader(6, 2, "收入数量");
            dgvReport.MergeColumnNames.Add("发出数量");
            dgvReport.AddSpanHeader(8, 2, "发出数量");
            dgvReport.MergeColumnNames.Add("结存数量");
            dgvReport.AddSpanHeader(10, 2, "结存数量");
            #endregion

            #region 报表合并表头
            List<string> listRevenues = new List<string>();
            listRevenues.Add("数量（基）");
            listRevenues.Add("数量（辅）");
            AddSpanRows("收入数量", listRevenues);

            List<string> listExpend = new List<string>();
            listExpend.Add("数量（基）");
            listExpend.Add("数量（辅）");
            AddSpanRows("支出数量", listExpend);

            List<string> listBalance = new List<string>();
            listBalance.Add("数量（基）");
            listBalance.Add("数量（辅）");
            AddSpanRows("结存数量", listBalance);
            #endregion

            BindData();

            //双击查看明细,要手动调用权限
            if (this.Name != "CL_BusinessAnalysis_Stock_ActualDet")
            {
                base.RoleButtonStstus("CL_BusinessAnalysis_Stock_ActualDet");
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
