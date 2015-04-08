using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using Model;
using Utility.Common;

namespace HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.YuTongPurchaseOrder
{
    public partial class UserControlShengJiView : UserControl
    {
        #region 变量、类声明
        
        #endregion

        #region 初始化窗体
        /// <summary> 初始化窗体
        /// </summary>
        public UserControlShengJiView()
        {
            InitializeComponent();
        }
        /// <summary> 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControlShengJi_Load(object sender, EventArgs e)
        {
            
        } 
        #endregion

        #region 方法、函数
        public void LoadControlInfo(tb_parts_purchase_order_2 yt_purchaseorder_model)
        {
            CommonFuncCall.SetShowControlValue(this, yt_purchaseorder_model, "View");
            if (!string.IsNullOrEmpty(lblreq_delivery_time.Text))
            {
                long ticks = Convert.ToInt64(lblreq_delivery_time.Text);
                lblreq_delivery_time.Text = Convert.ToDateTime(Common.UtcLongToLocalDateTime(ticks).ToString()).ToString("yyyy年MM月dd日 HH时");
            }
        }
        #endregion
    }
}
