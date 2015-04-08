using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using FastReport;
using FastReport.MSChart;
using FastReport.Data;

namespace HXCPcClient.UCForm.BusinessAnalysis.RepairReport
{
    public partial class UCRepairVehicleNumber : UCReport
    {
        public UCRepairVehicleNumber()
            : base("v_repair_vehicle_number_report", "维修车辆台次统计")
        {
            InitializeComponent();
        }

        private void UCRepairVehicleNumber_Load(object sender, EventArgs e)
        {
            CommonFuncCall.BindCompany(cboCompany, "全部");
            BindData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        void BindData()
        {
            string fileds = "(DATEADD(s,b.create_time/1000,'1970-01-01 08:00:00')) date,COUNT(1) num";
            string groupBy = "group by {0}(DATEADD(s,b.create_time/1000,'1970-01-01 08:00:00')) ";
            if (rbYear.Checked)
            {
                fileds = "year" + fileds;
                groupBy = string.Format(groupBy, "year");
            }
            else if (rbMonth.Checked)
            {
                fileds = "month" + fileds;
                groupBy = string.Format(groupBy, "month");
            }
            else if (rbDay.Checked)
            {
                fileds = "day" + fileds;
                groupBy = string.Format(groupBy, "day");
            }
            string strWhere = GetWhere();
            strWhere += " and a.info_status='2'";//查询已审核后的
            string strTables = @"tb_maintain_info a inner join tb_maintain_settlement_info b on a.maintain_id=b.maintain_id
                                Left join  tb_organization c on a.org_id=c.org_id ";
            dt = DBHelper.GetTable("获取维修车辆台次统计", strTables, fileds, strWhere, "", groupBy);
            dgvReport.DataSource = dt;
            BindReport(dt);
        }

        void BindReport(DataTable dt)
        {
            FastReport.Report report = new FastReport.Report();
            //判断有报表数据，则注册数据
            if (dt != null && dt.Rows.Count > 0)
            {
                report.RegisterData(dt, "v_repair_vehicle_number_report");
            }
            else
            {
                return;
            }

            report.GetDataSource("v_repair_vehicle_number_report").Enabled = true;
            report.Load("Report/repair_vehicle_number_report.frx");
            report.Preview = pcReport;
            report.Prepare();
            report.ShowPrepared();
        }
    }
}
