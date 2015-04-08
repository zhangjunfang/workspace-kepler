using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using Utility.Common;
using System.Threading;

namespace HXCPcClient.UCForm.SysManage.AcountManage
{
    /// <summary>
    /// 销售业务参数
    /// 孙明生
    /// </summary>
    public partial class UCSaleBPara : UCBase
    {
        /// <summary>
        /// 销售业务参数ID
        /// </summary>
        string strSale_param_id = "";

        public UCSaleBPara()
        {
            InitializeComponent();
            
        }

        #region Load
        private void UCSaleBPara_Load(object sender, EventArgs e)
        {
            base.RoleButtonStstus(this.Name);//角色按钮权限-是否隐藏          
            this.btnDelete.Visible = false;
            this.btnStatus.Visible = false;
            this.btnSave.Visible = true;
            this.btnCancel.Visible = true;

            this.uiHandler -= new UiHandler(this.ShowData);
            this.uiHandler += new UiHandler(this.ShowData);

            base.EditEvent += new ClickHandler(UCSaleBPara_EditEvent);
            base.SaveEvent += new ClickHandler(UCSaleBPara_SaveEvent);
            base.CancelEvent += new ClickHandler(UC_CancelEvent);

            ThreadPool.QueueUserWorkItem(new WaitCallback(this._ShowData));
        }
        #endregion

        #region --显示数据

        /// <summary> 异步数据查询 
        /// </summary>
        /// <param name="obj"></param>
        private void _ShowData(object obj)
        {
            DataTable dt = DBHelper.GetTable("查询销售业务参数", GlobalStaticObj.CommAccCode, 
                "sys_sale_param", "*", "book_id='" + GlobalStaticObj.CurrAccID + "'", "", "");
            this.Invoke(this.uiHandler, dt);
        }
        /// <summary> 异步绑定数据 
        /// </summary>
        /// <param name="obj"></param>
        private void ShowData(object obj)
        {
            DataTable dt = obj as DataTable;

            if (dt != null && dt.Rows.Count > 0)
            {
                strSale_param_id = dt.Rows[0]["sale_param_id"].ToString();

                chksales_plan_audit.Checked = dt.Rows[0]["sales_plan_audit"].ToString() == "1" ? true : false;
                chksales_order_audit.Checked = dt.Rows[0]["sales_order_audit"].ToString() == "1" ? true : false;
                chksales_open_audit.Checked = dt.Rows[0]["sales_open_audit"].ToString() == "1" ? true : false;
                chksales_open_outin.Checked = dt.Rows[0]["sales_open_outin"].ToString() == "1" ? true : false;

                chksingle_editors_same_person.Checked = dt.Rows[0]["single_editors_same_person"].ToString() == "1" ? true : false;
                chksingle_audit_same_person.Checked = dt.Rows[0]["single_audit_same_person"].ToString() == "1" ? true : false;
                chksingle_disabled_same_person.Checked = dt.Rows[0]["single_disabled_same_person"].ToString() == "1" ? true : false;
                chksingle_delete_same_person.Checked = dt.Rows[0]["single_delete_same_person"].ToString() == "1" ? true : false;

                rdbsales_open_line_credit.Checked = dt.Rows[0]["sales_open_line_credit"].ToString() == "1" ? true : false;
                rdbsales_order_line_credit.Checked = dt.Rows[0]["sales_order_line_credit"].ToString() == "1" ? true : false;
            }
        }

        #endregion

        #region 编辑
        void UCSaleBPara_EditEvent(object sender, EventArgs e)
        {
            chksales_plan_audit.Enabled = true;
            chksales_order_audit.Enabled = true;
            chksales_open_audit.Enabled = true;
            chksales_open_outin.Enabled = true;

            chksingle_editors_same_person.Enabled = true;
            chksingle_audit_same_person.Enabled = true;
            chksingle_disabled_same_person.Enabled = true;
            chksingle_delete_same_person.Enabled = true;

            rdbsales_open_line_credit.Enabled = true;
            rdbsales_order_line_credit.Enabled = true;
        }
         #endregion

        #region 取消
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UC_CancelEvent(object sender, EventArgs e)
        {
            if (chksales_plan_audit.Enabled)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(this._ShowData));
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        void UCSaleBPara_SaveEvent(object sender, EventArgs e)
        {
            if (!chksales_plan_audit.Enabled)
            {
                return;
            }

            string keyName = string.Empty;
            string keyValue = string.Empty;
            string opName = "新增销售业务参数";
            Dictionary<string, string> Fileds = new Dictionary<string, string>();
            Fileds.Add("sales_plan_audit", chksales_plan_audit.Checked == true ? "1" : "0");
            Fileds.Add("sales_order_audit", chksales_order_audit.Checked == true ? "1" : "0");
            Fileds.Add("sales_open_audit", chksales_open_audit.Checked == true ? "1" : "0");
            Fileds.Add("sales_open_outin", chksales_open_outin.Checked == true ? "1" : "0");

            Fileds.Add("single_editors_same_person", chksingle_editors_same_person.Checked == true ? "1" : "0");
            Fileds.Add("single_audit_same_person", chksingle_audit_same_person.Checked == true ? "1" : "0");
            Fileds.Add("single_disabled_same_person", chksingle_disabled_same_person.Checked == true ? "1" : "0");
            Fileds.Add("single_delete_same_person", chksingle_delete_same_person.Checked == true ? "1" : "0");

            Fileds.Add("sales_open_line_credit", rdbsales_open_line_credit.Checked == true ? "1" : "0");
            Fileds.Add("sales_order_line_credit", rdbsales_order_line_credit.Checked == true ? "1" : "0");

            string nowUtcTicks = Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString();
            Fileds.Add("update_by", HXCPcClient.GlobalStaticObj.UserID);
            Fileds.Add("update_time", nowUtcTicks);

            if (this.strSale_param_id.Length == 0)
            {
                Fileds.Add("book_id", GlobalStaticObj.CurrAccID);
                Fileds.Add("sale_param_id", Guid.NewGuid().ToString());//新ID                   
                Fileds.Add("create_by", HXCPcClient.GlobalStaticObj.UserID);
                Fileds.Add("create_time", nowUtcTicks);
            }
            else
            {
                keyName = "sale_param_id";
                keyValue = strSale_param_id;
                opName = "更新销售业务参数";
            }
            bool bln = DBHelper.Submit_AddOrEdit(opName, GlobalStaticObj.CommAccCode, "sys_sale_param", keyName, keyValue, Fileds);
            if (bln)
            {
                MessageBoxEx.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);

                chksales_plan_audit.Enabled = false;
                chksales_order_audit.Enabled = false;
                chksales_open_audit.Enabled = false;
                chksales_open_outin.Enabled = false;

                chksingle_editors_same_person.Enabled = false;
                chksingle_audit_same_person.Enabled = false;
                chksingle_disabled_same_person.Enabled = false;
                chksingle_delete_same_person.Enabled = false;

                rdbsales_open_line_credit.Enabled = false;
                rdbsales_order_line_credit.Enabled = false;
            }
            else
            {
                MessageBoxEx.Show("保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

    }
}
