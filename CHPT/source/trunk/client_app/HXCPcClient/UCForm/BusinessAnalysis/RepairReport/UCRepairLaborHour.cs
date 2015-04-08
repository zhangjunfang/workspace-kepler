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
using SYSModel;

namespace HXCPcClient.UCForm.BusinessAnalysis.RepairReport
{
    public partial class UCRepairLaborHour : UCReport
    {
        public UCRepairLaborHour()
            : base("v_repair_labor_hour_report", "维修人员工时统计")
        {
            InitializeComponent();
            colDispatchingNum.DefaultCellStyle = styleMoney;
            colManHour.DefaultCellStyle = styleMoney;
            colSumMoney.DefaultCellStyle = styleMoney;
            colPushMoney.DefaultCellStyle = styleMoney;
        }

        private void UCRepairLaborHour_Load(object sender, EventArgs e)
        {
            CommonFuncCall.BindCompany(cboCompany, "全部");//绑定公司
            BindData();
        }
        //维修人员
        private void txtcServiceman_ChooserClick(object sender, EventArgs e)
        {
            frmPersonnelSelector frmSelector = new frmPersonnelSelector();
            frmSelector.isMoreCheck = false;
            DialogResult result = frmSelector.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtcServiceman.Text = frmSelector.strPersonName;
            }
        }
        //查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        void BindData()
        {
            string push = "100";
            if (txtPushMoney.Caption.Trim().Length > 0)
            {
                push = txtPushMoney.Caption.Trim();
            }
            string strWhere = string.Format(" c.info_status in ('{0}','{1}') and ", (int)DataSources.EnumAuditStatus.AUDIT, (int)DataSources.EnumAuditStatus.Balance);
            strWhere += GetWhere();
            string fileds = string.Format("dispatch_name,COUNT(1) dispatching_num,sum(man_hour) man_hour,sum(sum_money) sum_money,sum(sum_money)*({0}/100.0) push_money", push);
            string table = @"tb_maintain_dispatch_worker a inner join tb_maintain_settlement_info b on a.maintain_id=b.maintain_id
inner join tb_maintain_info c on a.maintain_id=b.maintain_id
inner join sys_user d on a.dispatch_no=d.user_code
inner join tb_organization e on d.org_id=e.org_id";
            dt = DBHelper.GetTable("", table, fileds, strWhere, "", "group by dispatch_name");
            dgvReport.DataSource = dt;
        }
    }
}
