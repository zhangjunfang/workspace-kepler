using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYSModel;
using System.Data;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
namespace HXCPcClient.Chooser
{
    /// <summary>
    /// 预付导入
    /// </summary>
    public class frmPaymentByAdvances:frmBalanceDocuments
    {
        public frmPaymentByAdvances(string custCode) :
            base( custCode)
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
            colBalanceMoney.HeaderText = "已预付金额";
            colWaitMoney.HeaderText = "未预付金额";
            sbWhere.AppendFormat(" sup_id='{0}'", custCode);
            sbWhere.AppendFormat(" and is_lock='{0}'", (int)DataSources.EnumImportStaus.OPEN);
            dt = DBHelper.GetTable("", "v_parts_purchase_order_payment", "*", sbWhere.ToString(), "", "");
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            foreach (DataRow dr in dt.Rows)
            {
                DataGridViewRow dgvr = dgvData.Rows[dgvData.Rows.Add()];
                dgvr.Cells[colID.Name].Value = dr["order_id"];//ID
                dgvr.Cells[colBillsName.Name].Value = "采购订单";//单据名称
                colBillsType.Visible = false;
                dgvr.Cells[colBillsCode.Name].Value = dr["order_num"];//单据编码
                dgvr.Cells[colTotalMoney.Name].Value = dr["prepaid_money"];//总金额
                dgvr.Cells[colBalanceMoney.Name].Value = dr["money"];//已结算
                dgvr.Cells[colWaitMoney.Name].Value = dr["wait_money"];//未结算
            }
        }

        protected override bool LockDocument(string ids)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("is_lock", ((int)DataSources.EnumImportStaus.LOCK).ToString());
            return DBHelper.BatchUpdateDataByIn("锁定采购订单", "tb_parts_purchase_order", dic, "order_id", ids.Split(','));
        }
    }
}
