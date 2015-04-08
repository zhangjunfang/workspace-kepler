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
using Utility.Common;

namespace HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchasePlan
{
    public partial class frmRelationOrder : FormChooser
    {
        public frmRelationOrder(string parts_name, string plan_count, string finish_count, string relation_order, string parts_code)
        {
            InitializeComponent();
            gvPurchseList.CellFormatting += new DataGridViewCellFormattingEventHandler(gvPurchseList_CellFormatting);
            lblparts_name.Text = parts_name;
            lblplan_count.Text = plan_count;
            lblfinish_count.Text = finish_count;
            BindDataInfo(relation_order, parts_code);
        }

        void gvPurchseList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null || e.Value.ToString().Length == 0)
            {
                return;
            }
            string fieldNmae = gvPurchseList.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("order_date"))
            {
                long ticks = (long)e.Value;
                e.Value = Common.UtcLongToLocalDateTime(ticks).ToShortDateString();
            }
        }

        /// <summary> 绑定计划完成详情
        /// </summary>
        /// <param name="relation_order"></param>
        /// <param name="parts_code"></param>
        void BindDataInfo(string relation_order, string parts_code)
        {
            try
            {
                if (!string.IsNullOrEmpty(relation_order) && !string.IsNullOrEmpty(parts_code))
                {
                    string TableName = string.Format(@"
                    (
                        select order_num,'采购订单' as OrderType,order_date,sup_name from tb_parts_purchase_order_p as tb_order_p 
                        left join tb_parts_purchase_order as tb_order
                        on tb_order_p.order_id=tb_order.order_id 
                        where tb_order_p.relation_order='{0}' and parts_code='{1}' and len(order_num)>0
                        union 
                        select order_num,'宇通采购订单' as OrderType,order_date,'' from tb_parts_purchase_order_p_2 as tb_order_p_2 
                        left join tb_parts_purchase_order_2 as tb_order_2
                        on tb_order_p_2.purchase_order_yt_id=tb_order_2.purchase_order_yt_id 
                        where tb_order_p_2.relation_order='{0}' and parts_code='{1}' and len(order_num)>0
                    ) a", relation_order, parts_code);
                    DataTable dt = DBHelper.GetTable("查询采购计划单配件关联信息", TableName, "*", "", "", "");
                    gvPurchseList.DataSource = dt;
                }
            }
            catch (Exception ex)
            { }
        }
    }
}
