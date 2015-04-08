using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HXCPcClient.UCForm.BusinessAnalysis.RepairReport
{
    public partial class UCRepairManage : UCReport
    {
        public UCRepairManage()
            : base("v_repair_manage_report", "维修经营情况统计")
        {
            InitializeComponent();
            dgvReport.ColumnHeadersHeight = 40;
            dgvReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        }

        private void UCRepairManage_Load(object sender, EventArgs e)
        {
            CommonFuncCall.BindCompany(cboCompany, "全部");
            #region 显示合并表头
            dgvReport.MergeColumnNames.Add("价税合计费用");
            dgvReport.AddSpanHeader(3, 3, "价税合计费用");
            dgvReport.MergeColumnNames.Add("班组一");
            dgvReport.AddSpanHeader(6, 4, "班组一");
            dgvReport.MergeColumnNames.Add("服务顾问接车台次");
            dgvReport.AddSpanHeader(10, 4, "服务顾问接车台次");
            #endregion
            #region 报表合并表头
            List<string> listDing = new List<string>();
            listDing.Add("工时金额");
            listDing.Add("配件金额");
            listDing.Add("其他项目金额");
            AddSpanRows("价税合计费用", listDing);

            List<string> listYiShou = new List<string>();
            listYiShou.Add("台次");
            listYiShou.Add("工时金额");
            listYiShou.Add("配件金额");
            listYiShou.Add("其他项目金额");
            AddSpanRows("班组一", listYiShou);

            List<string> listZhongZhi = new List<string>();
            listZhongZhi.Add("小小");
            listZhongZhi.Add("大大");
            listZhongZhi.Add("多多");
            listZhongZhi.Add("少少");
            AddSpanRows("服务顾问接车台次", listZhongZhi);
            #endregion
        }
    }
}
