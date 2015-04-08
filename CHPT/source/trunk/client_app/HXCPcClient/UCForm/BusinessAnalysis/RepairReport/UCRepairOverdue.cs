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
using Utility.Common;

namespace HXCPcClient.UCForm.BusinessAnalysis.RepairReport
{
    public partial class UCRepairOverdue : UCReport
    {
        public UCRepairOverdue()
            : base("v_repair_overdue_report", "超期无服务车辆统计")
        {
            InitializeComponent();
        }

        private void UCRepairOverdue_Load(object sender, EventArgs e)
        {
            CommonFuncCall.BindCompany(cboCompany, "全部");
            BindData();
        }
        //车型选择器
        private void txtvehicle_models_ChooserClick(object sender, EventArgs e)
        {
            frmVehicleModels frmVM = new frmVehicleModels();
            if (frmVM.ShowDialog() == DialogResult.OK)
            {
                txtvehicle_models.Text = frmVM.VMName;
            }
        }
        //车牌号选择器
        private void txtclicense_plate_ChooserClick(object sender, EventArgs e)
        {
            frmVehicleGrade frmVG = new frmVehicleGrade();
            if (frmVG.ShowDialog() == DialogResult.OK)
            {
                txtclicense_plate.Text = frmVG.strLicensePlate;
            }
        }
        //查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        //绑定数据
        void BindData()
        {
            //dt = DBHelper.GetTable("", "v_repair_overdue_report", "*,datediff(d,最后日期,getdate()) 无服务天数", GetWhere(), "", "");
            StringBuilder sbWhere = new StringBuilder();
            sbWhere.AppendFormat(" and b.create_time>{0}", Common.LocalDateTimeToUtcLong(dtpStartDate.Value.Date));
            //sbWhere.AppendFormat(" and datediff(d,DATEADD(s,c.create_time/1000,''1970-01-01 08:00:00''),getdate())>={0}", nudDay.Value);

            StringBuilder sbWhere2 = new StringBuilder();
            sbWhere2.AppendFormat(" and make_time>{0}", Common.LocalDateTimeToUtcLong(dtpStartDate.Value.Date));
            //sbWhere2.AppendFormat(" and datediff(d,DATEADD(s,make_time/1000,''1970-01-01 08:00:00''),getdate())>={0}", nudDay.Value);
            //公司
            string comID = CommonCtrl.IsNullToString(cboCompany.SelectedValue);
            if (comID.Length > 0)
            {
                sbWhere.AppendFormat(" and com_id='{0}'", comID);
                sbWhere2.AppendFormat(" and com_id='{0}'", comID);
            }
            //车牌号
            string vehicle_no = txtclicense_plate.Text.Trim();
            if (vehicle_no.Length > 0)
            {
                sbWhere.AppendFormat(" and vehicle_no like '%{0}%'", vehicle_no);
                sbWhere2.AppendFormat(" and vehicle_no like '%{0}%'", vehicle_no);
            }
            //车型
            string vehicle_model = txtvehicle_models.Text.Trim();
            if (vehicle_model.Length > 0)
            {
                sbWhere.AppendFormat(" and vm_name like '%{0}%'", vehicle_model);
                sbWhere2.AppendFormat(" and vm_name like '%{0}%'", vehicle_model);
            }
            SYSModel.SQLObj sql = new SYSModel.SQLObj();
            sql.cmdType = CommandType.StoredProcedure;
            sql.sqlString = "p_repair_overdue_report";
            sql.Param = new Dictionary<string, SYSModel.ParamObj>();
            sql.Param.Add("where", new SYSModel.ParamObj("where", sbWhere.ToString(), SYSModel.SysDbType.NVarChar, 200));
            sql.Param.Add("where2", new SYSModel.ParamObj("where2", sbWhere2.ToString(), SYSModel.SysDbType.NVarChar, 200));
            sql.Param.Add("day", new SYSModel.ParamObj("day", nudDay.Value.ToString(), SYSModel.SysDbType.VarChar, 10));
            DataSet ds = DBHelper.GetDataSet("获取超期无服务车辆统计", sql);
            if (ds != null && ds.Tables.Count == 1)
            {
                dt = ds.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    dt.DefaultView.Sort = "无服务天数";
                }
            }
            dgvReport.DataSource = dt;
        }
    }
}
