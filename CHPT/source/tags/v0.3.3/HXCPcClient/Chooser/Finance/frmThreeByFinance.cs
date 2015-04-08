using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HXCPcClient.CommonClass;
using System.Windows.Forms;
using Utility.Common;
using SYSModel;

namespace HXCPcClient.Chooser
{
    /// <summary>
    /// 三包服务单
    /// </summary>
    public class frmThreeByFinance:frmBalanceDocuments
    {
        public frmThreeByFinance(string custCode)
            : base(custCode)
        {
 
        }

        protected override void BindData()
        {
            if (custCode.Length == 0)
            {
                return;
            }
            dgvData.RowCount = 0;
            DataTable dt;//数据
            StringBuilder sbWhere = new StringBuilder();
            //sbWhere.AppendFormat("cust_id='{0}'", custCode);
            sbWhere.AppendFormat(" wait_money>0");
            sbWhere.AppendFormat(" and is_lock='{0}'", (int)DataSources.EnumImportStaus.OPEN);
            dt = DBHelper.GetTable("", "v_tb_maintain_three_guaranty_settlement_yt_receivable", "*", sbWhere.ToString(), "", "");
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            foreach (DataRow dr in dt.Rows)
            {
                DataGridViewRow dgvr = dgvData.Rows[dgvData.Rows.Add()];
                dgvr.Cells["colID"].Value = dr["st_id"];//ID
                dgvr.Cells["colBillsName"].Value = "三包结算单";//单据名称
                //dgvr.Cells["colBillsType"].Value = dr["price_type"];//单据类型
                dgvr.Cells["colBillsCode"].Value = dr["settlement_no"];//单据编码
                dgvr.Cells["colTotalMoney"].Value = dr["sum_cost"];//总金额
                dgvr.Cells["colBalanceMoney"].Value = dr["money"];//已结算
                dgvr.Cells["colWaitMoney"].Value = dr["wait_money"];//未结算
                string date = CommonCtrl.IsNullToString(dr["clearing_time"]);
                if (date.Length > 0)
                {
                    dgvr.Cells["colReceivablesDate"].Value = Common.UtcLongToLocalDateTime(Int64.Parse(date));//收款日期
                }
                //dgvr.Cells["colReceiptNO"].Value = dr["receipt_no"];//发票号
            }
        }

        protected override bool LockDocument(string ids)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("is_lock", ((int)DataSources.EnumImportStaus.LOCK).ToString());
            return DBHelper.BatchUpdateDataByIn("锁定三包单", "tb_maintain_three_guaranty_settlement_yt", dic, "st_id", ids.Split(','));
        }
    }
}
