using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using HXCPcClient.CommonClass;

namespace HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.YuTongPurchaseOrder
{
    public partial class frmDistributionView : FormChooser
    {
        public frmDistributionView(string YTOrder_num, string BusinessCount, string dsn_adjustable_parts)
        {
            InitializeComponent();
            lblYTOrder_num.Text = YTOrder_num;
            lblBusinessCount.Text = BusinessCount;
            BindDataInfo(dsn_adjustable_parts);
        }

        /// <summary> 绑定配送单信息
        /// </summary>
        /// <param name="dsn_adjustable_parts">配送需求单号</param>
        void BindDataInfo(string dsn_adjustable_parts)
        {
            try
            {
                string wherestr = string.Empty;
                if (!string.IsNullOrEmpty(dsn_adjustable_parts))
                {
                    wherestr = " dsn_adjustable_parts='" + dsn_adjustable_parts + "'";
                    string TableName = string.Format(@"(
                    select '宇通配送单' as OrderType,a.*,case a.distribution_status when 1 then '配送中' else '已收货' end dis_status_name,
                    b.order_num,sum(c.send_count) send_count from tb_distribution a left join tb_parts_purchase_billing b 
                    on a.ration_send_code=b.ration_send_code left join tb_distribution_parts c on a.distribution_id=c.distribution_id where LEN(b.order_num)>0
                    group by a.distribution_id,a.ration_send_code,a.dsn_adjustable_parts,a.distribution_status,b.order_num
                                               ) tb_dis");
                    DataTable dt = DBHelper.GetTable("查询配送单信息", TableName, "*", wherestr, "", "");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        gvDisList.DataSource = dt;
                        decimal counts = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string send_count = dt.Rows[i]["send_count"] == null ? "0" : dt.Rows[i]["send_count"].ToString();
                            send_count = string.IsNullOrEmpty(send_count) ? "0" : dt.Rows[i]["send_count"].ToString();
                            counts = counts + Convert.ToDecimal(send_count);
                        }
                        lblcount.Text = counts.ToString();
                    }
                }
            }
            catch (Exception ex)
            { }
        }
    }
}
