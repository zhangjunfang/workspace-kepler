using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using Model;
using Utility.Common;

namespace HXCPcClient.UCForm.DataManage.WorkingTimeFile
{
    public partial class UCWorkingTimeView : UCBase
    {
        public UCWorkingTimeView(string workingTimeId)
        {
            InitializeComponent();
            LoadInfo(workingTimeId);
        }

        private void UCWorkingTimeView_Load(object sender, EventArgs e)
        {
            //base.SetBtnStatus(WindowStatus.View);
            SetDataViewBtn();
        }

        /// <summary>
        /// 加载供应商档案信息
        /// </summary>
        private void LoadInfo(string workingTimeId)
        {
            if (!string.IsNullOrEmpty(workingTimeId))
            {
                //1.加载工时档案主信息
                DataTable dt = DBHelper.GetTable("查看一条工时档案信息", "v_workhours_users", "*", " whours_id='" + workingTimeId + "'", "", "");
                if (dt != null && dt.Rows.Count > 0)
                {
                    tb_workhours tb_workhours_Model = new tb_workhours();
                    CommonFuncCall.SetModlByDataTable(tb_workhours_Model, dt);
                    CommonFuncCall.SetShowControlValue(this, tb_workhours_Model, "");
                    if (tb_workhours_Model.whours_type == "1")
                    {
                        radIsWorkTime.Checked = true;
                    }
                    else if (tb_workhours_Model.whours_type == "2")
                    {
                        radIsQuota.Checked = true;
                    }
                    lblcreate_by.Text = dt.Rows[0]["create_username"].ToString();
                    lblupdate_by.Text = dt.Rows[0]["update_username"].ToString();

                    if (!string.IsNullOrEmpty(lblcreate_time.Text))
                    {
                        long ticks = (long)Convert.ToInt64(lblcreate_time.Text);
                        lblcreate_time.Text = Common.UtcLongToLocalDateTime(ticks).ToString();
                    }
                    if (!string.IsNullOrEmpty(lblupdate_time.Text))
                    {
                        long ticks = (long)Convert.ToInt64(lblupdate_time.Text);
                        lblupdate_time.Text = Common.UtcLongToLocalDateTime(ticks).ToString();
                    }

                    DataTable dt_bill = CommonFuncCall.GetDataTable();
                    if (!string.IsNullOrEmpty(tb_workhours_Model.repair_type))
                    {
                        lblrepair_type.Text = CommonFuncCall.GetBillNameByBillCode(dt_bill, tb_workhours_Model.repair_type);
                    }
                }
            }
        }
    }
}
