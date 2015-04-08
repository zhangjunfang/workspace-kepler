using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using SYSModel;
using HXCPcClient.CommonClass;

namespace HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchasePlan
{
    public partial class frmStockCount : FormChooser
    {
        public frmStockCount(string parts_id)
        {
            InitializeComponent();
            BindStock(parts_id);
        }


        void BindStock(string parts_id)
        {
            StringBuilder sbWhere = new StringBuilder();
            sbWhere.AppendFormat("stock_part_id='{0}'", parts_id);
            string stockSql = string.Format(@"select parts_name,wh_name,
            sum(case when statistic_Type=0 then statistic_count else 0 end) paper_count,
            sum(case when statistic_Type=1 then statistic_count else 0 end) actual_count 
            from tb_parts_stock_p where stock_part_id='{0}' 
            group by parts_name,wh_name", parts_id);

            SQLObj partsStock = new SQLObj();
            partsStock.cmdType = CommandType.Text;
            partsStock.Param = new Dictionary<string, ParamObj>();
            partsStock.sqlString = stockSql;
            DataTable dt = DBHelper.GetDataSet("查询配件库存", partsStock).Tables[0];
            dgvDetail.DataSource = dt;
        }
    }
}
