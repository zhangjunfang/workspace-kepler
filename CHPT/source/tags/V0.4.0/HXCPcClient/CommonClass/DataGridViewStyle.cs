using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStationClient.ComponentUI;
using System.Windows.Forms;

namespace HXCPcClient.CommonClass
{
    /// <summary>
    /// DataGridViewEx 边框 背景色 选中色 隔行换色 设置
    /// </summary>
    public class DataGridViewStyle
    {
        /// <summary>
        /// DataGridViewEx 边框 背景色 选中色 隔行换色 设置
        /// </summary>
        /// <param name="dgv">DataGridViewEx</param>
        public static void DataGridViewBgColor(DataGridViewEx dgv)
        {
            var dataGridViewCellStyle1 = new DataGridViewCellStyle();
            var dataGridViewCellStyle2 = new DataGridViewCellStyle();
            var dataGridViewCellStyle3 = new DataGridViewCellStyle();
            var dataGridViewCellStyle4 = new DataGridViewCellStyle();
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;

            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(240, 241, 243);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(51, 51, 51);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;            
            dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;

            //选中行颜色
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(255, 246, 150);
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular);
            dgv.RowsDefaultCellStyle = dataGridViewCellStyle3;
            dgv.GridColor = System.Drawing.Color.FromArgb(221, 221, 221);

            //序号列样式-行标题背景色 选中色
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(224, 236, 255);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(224, 236, 255);          
            dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            //行标题边框样式
            dgv.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            
        }
    }
}
