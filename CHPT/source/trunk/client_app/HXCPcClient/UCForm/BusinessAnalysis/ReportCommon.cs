using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SYSModel;

namespace HXCPcClient.UCForm.BusinessAnalysis
{
    /// <summary>
    /// 报表公共类
    /// </summary>
    public class ReportCommon
    {
        /// <summary>
        /// 绑定仓库开单单据类型
        /// </summary>
        /// <param name="cbo"></param>
        public static void BindWarehouseType(ComboBox cbo)
        {
            List<ListItem> listType = new List<ListItem>();
            listType.Add(new ListItem("", "全部"));
            listType.Add(new ListItem("1", "入库单"));
            listType.Add(new ListItem("2", "出库单"));
            listType.Add(new ListItem("3", "调拨单"));
            listType.Add(new ListItem("4", "领料单"));
            listType.Add(new ListItem("5", "调价单"));
            listType.Add(new ListItem("6", "盘点单"));
            listType.Add(new ListItem("7", "报损单"));
            listType.Add(new ListItem("8", "其他收货单"));
            listType.Add(new ListItem("9", "其他发货单"));
            cbo.DataSource = listType;
            cbo.DisplayMember = "Text";
        }
    }
}
