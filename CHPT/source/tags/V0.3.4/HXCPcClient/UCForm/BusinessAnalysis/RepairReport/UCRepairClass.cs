using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;

namespace HXCPcClient.UCForm.BusinessAnalysis.RepairReport
{
    public partial class UCRepairClass : UCReport
    {
        public UCRepairClass()
            : base("v_repair_class_report", "维修业务类别统计")
        {
            InitializeComponent();
        }

        private void UCRepairClass_Load(object sender, EventArgs e)
        {
            CommonFuncCall.BindCompany(cboCompany, "全部");
            BindData();
        }
        void BindData()
        {
            dt = DBHelper.GetTable("", "v_repair_class_report", "*", GetWhere(), "", "");
            dgvReport.DataSource = dt;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
    }
}
