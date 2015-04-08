using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using SYSModel;
using Utility.Common;

namespace HXCPcClient.UCForm.DataManage.VehicleModels
{
    public partial class UCVehicleModelsView : UCBase
    {
        string vm_id;

        public UCVehicleModelsView(string vm_id)
        {
            InitializeComponent();
            this.vm_id = vm_id;
        }
        //加载
        private void UCVehicleModelsView_Load(object sender, EventArgs e)
        {
            //SetBtnStatus(WindowStatus.View);
            DataTable dt = DBHelper.GetTable("预览车型", "v_vehicle_models", "*", string.Format(" vm_id='{0}'", vm_id), "", "");
            //如果查不到车型，全部为空
            if (dt == null || dt.Rows.Count == 0)
            {
                lblBrand.Text = string.Empty;
                lblCode.Text = string.Empty;
                lblCreateDate.Text = string.Empty;
                lblCreateUser.Text = string.Empty;
                lblName.Text = string.Empty;
                lblRemark.Text = string.Empty;
                lblStatus.Text = string.Empty;
                lblType.Text = string.Empty;
                lblUpdateDate.Text = string.Empty;
                lblUpdateUser.Text = string.Empty;
            }
            else
            {
                DataRow dr = dt.Rows[0];
                lblBrand.Text = dr["v_brand_name"].ToString();
                lblCode.Text = dr["vm_code"].ToString();
                if (dr["create_time"] != null && dr["create_time"] != DBNull.Value)
                {
                    DateTime createDT =Common.UtcLongToLocalDateTime(Convert.ToInt64(dr["create_time"]));
                    lblCreateDate.Text = createDT.ToString();
                }
                else
                {
                    lblCreateDate.Text = "";
                }
                lblCreateUser.Text = dr["create_by_name"].ToString();
                lblName.Text = dr["vm_name"].ToString();
                lblRemark.Text = dr["remark"].ToString();
                lblStatus.Text = DataSources.GetDescription(typeof(DataSources.EnumStatus), dr["status"]);
                lblType.Text = dr["vm_type_name"].ToString();
                if (dr["update_time"] != null && dr["update_time"] != DBNull.Value)
                {
                    lblUpdateDate.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(dr["update_time"])).ToString();
                }
                else
                {
                    lblUpdateDate.Text = "";
                }
                lblUpdateUser.Text = dr["update_by_name"].ToString();
            }
            SetDataViewBtn();
        }
    }
}
