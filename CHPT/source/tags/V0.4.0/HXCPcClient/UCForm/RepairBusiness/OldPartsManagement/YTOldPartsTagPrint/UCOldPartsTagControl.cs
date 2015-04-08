using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;

namespace HXCPcClient.UCForm.RepairBusiness.OldPartsManagement.YTOldPartsTagPrint
{
    /// <summary>
    /// 维修管理-宇通旧件标签打印服务卡信息控件
    /// Author：JC
    /// AddTime：2014.11.05
    /// </summary>
    public partial class UCOldPartsTagControl : UserControl
    {
        #region 属性设置
        /// <summary>
        /// 总数据量
        /// </summary>
        public string strTotalS ="0";
        /// <summary>
        /// 总页数
        /// </summary>
        public string strTotalPageS = "0";
        /// <summary>
        /// 服务单号
        /// </summary>
        public string strServerNo = string.Empty;
        #endregion
        public UCOldPartsTagControl()
        {
            InitializeComponent();
            ClearContralValue();
           
        }

        #region 清空信息控件值
        /// <summary>
        /// 清空信息控件值
        /// </summary>
        public void ClearContralValue()
        {
            #region 服务卡信息
            labServiceName0.Text = string.Empty;
            labPartsCode0.Text = string.Empty;
            labFaultPart0.Text = string.Empty;
            label59.Text = string.Empty;
            label66.Text = string.Empty;
            labServiceNo0.Text = string.Empty;
            labMaintainTime0.Text = string.Empty;
            labDepotNo0.Text = string.Empty;
            labTravelMileage0.Text = string.Empty;
            labVehicleModel0.Text = string.Empty;
            labEngineType0.Text = string.Empty;
            labFaultDescribe0.Text = string.Empty;
            labDisposeResult0.Text = string.Empty;
            #endregion          

        }
        #endregion

        #region 绑定服务单信息
        /// <summary>
        /// 绑定服务单信息
        /// </summary>
        /// <param name="strNo">服务单号</param>
        public void BindServiceOrderInfo(string strNo)
        {
            DataTable dt = DBHelper.GetTable("服务单信息获取", "tb_maintain_three_guaranty", "*", "service_no =" + strNo + "", "", "order by repairs_time desc");
            if (dt.Rows.Count > 0)
            {
                 DataRow dr = dt.Rows[0];
                 labServiceName0.Text = CommonCtrl.IsNullToString(dr["service_station_name"]);
                 labPartsCode0.Text = CommonCtrl.IsNullToString(dr["parts_code"]);
                 labFaultPart0.Text = CommonCtrl.IsNullToString(dr["fault_part"]);
                 label59.Text = string.Empty;
                 label66.Text = string.Empty;
                 labServiceNo0.Text = CommonCtrl.IsNullToString(dr["service_no"]);
                 labMaintainTime0.Text = CommonCtrl.IsNullToString(dr["maintain_time"]);
                 labDepotNo0.Text = CommonCtrl.IsNullToString(dr["depot_no"]);
                 labTravelMileage0.Text = CommonCtrl.IsNullToString(dr["travel_mileage"]);
                 labVehicleModel0.Text = CommonCtrl.IsNullToString(dr["vehicle_model"]);
                 labEngineType0.Text = CommonCtrl.IsNullToString(dr["engine_type"]);
                 labFaultDescribe0.Text = CommonCtrl.IsNullToString(dr["fault_describe"]);
                 labDisposeResult0.Text = CommonCtrl.IsNullToString(dr["dispose_result"]);
               
            }
        }
        #endregion
    }
}
