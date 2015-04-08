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
    /// 采购业务参数
    /// 孙明生
    /// 修改人：杨天帅
    /// </summary>
    public partial class UCPurcBPara : UCBase
    {
        #region --成员变量
        /// <summary>
        /// 采购业务参数id
        /// </summary>
        string strPurchase_param_id = string.Empty;
        #endregion

        #region --构造函数
        public UCPurcBPara()
        {
            InitializeComponent();            
        }
        #endregion

        #region --窗体初始化
        private void UCPurcBPara_Load(object sender, EventArgs e)
        {
            base.RoleButtonStstus(this.Name);//角色按钮权限-是否隐藏   

            foreach (Control ctl in this.panelEx1.Controls)
            {
                if (ctl is CheckBox)
                {
                    ctl.Enabled = false;
                }
            }

            this.uiHandler -= new UiHandler(this.ShowData);
            this.uiHandler += new UiHandler(this.ShowData);

            base.EditEvent += new ClickHandler(UCPurcBPara_EditEvent);
            base.SaveEvent += new ClickHandler(UCPurcBPara_SaveEvent);
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
            DataTable dt = DBHelper.GetTable("查询采购业务参数", GlobalStaticObj.CommAccCode, "sys_purchase_param", "*", "book_id='" + GlobalStaticObj.CurrAccID + "'", "", "");
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
                strPurchase_param_id = dt.Rows[0]["purchase_param_id"].ToString();

                chkpurchase_plan_audit.Checked = dt.Rows[0]["purchase_plan_audit"].ToString() == "1" ? true : false;
                chkpurchase_order_audit.Checked = dt.Rows[0]["purchase_order_audit"].ToString() == "1" ? true : false;
                chkpurchase_open_audit.Checked = dt.Rows[0]["purchase_open_audit"].ToString() == "1" ? true : false;
                chkpurchase_order_audit_yt.Checked = dt.Rows[0]["purchase_order_audit_yt"].ToString() == "1" ? true : false;
                chkpurchase_open_outin.Checked = dt.Rows[0]["purchase_open_outin"].ToString() == "1" ? true : false;

                chksingle_editors_same_person.Checked = dt.Rows[0]["single_editors_same_person"].ToString() == "1" ? true : false;
                chksingle_audit_same_person.Checked = dt.Rows[0]["single_audit_same_person"].ToString() == "1" ? true : false;
                chksingle_disabled_same_person.Checked = dt.Rows[0]["single_disabled_same_person"].ToString() == "1" ? true : false;
                chksingle_delete_same_person.Checked = dt.Rows[0]["single_delete_same_person"].ToString() == "1" ? true : false;

                chkpurchase_order_import_pre.Checked = dt.Rows[0]["purchase_order_import_pre"].ToString() == "1" ? true : false;
                chkpurchase_open_import_pre.Checked = dt.Rows[0]["purchase_open_import_pre"].ToString() == "1" ? true : false;
            }
        }

        #endregion

        #region --按钮事件

        #region 编辑
        void UCPurcBPara_EditEvent(object sender, EventArgs e)
        {
            foreach (Control ctl in this.panelEx1.Controls)
            {
                if (ctl is CheckBox)
                {
                    ctl.Enabled = true;
                }
            }
        }
        #endregion

        /// <summary> 取消
        /// </summary>
        void UC_CancelEvent(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(this._ShowData));
        }

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        void UCPurcBPara_SaveEvent(object sender, EventArgs e)
        {
            if (!chkpurchase_plan_audit.Checked)
            {
                return;
            }

            string keyName = string.Empty;
            string keyValue = string.Empty;
            string opName = "新增采购业务参数";

            Dictionary<string, string> Fileds = new Dictionary<string, string>();

            Fileds.Add("purchase_plan_audit", chkpurchase_plan_audit.Checked == true ? "1" : "0");
            Fileds.Add("purchase_order_audit", chkpurchase_order_audit.Checked == true ? "1" : "0");
            Fileds.Add("purchase_open_audit", chkpurchase_open_audit.Checked == true ? "1" : "0");
            Fileds.Add("purchase_order_audit_yt", chkpurchase_order_audit_yt.Checked == true ? "1" : "0");
            Fileds.Add("purchase_open_outin", chkpurchase_open_outin.Checked == true ? "1" : "0");

            Fileds.Add("single_editors_same_person", chksingle_editors_same_person.Checked == true ? "1" : "0");
            Fileds.Add("single_audit_same_person", chksingle_audit_same_person.Checked == true ? "1" : "0");
            Fileds.Add("single_disabled_same_person", chksingle_disabled_same_person.Checked == true ? "1" : "0");
            Fileds.Add("single_delete_same_person", chksingle_delete_same_person.Checked == true ? "1" : "0");

            Fileds.Add("purchase_order_import_pre", chkpurchase_order_import_pre.Checked == true ? "1" : "0");
            Fileds.Add("purchase_open_import_pre", chkpurchase_open_import_pre.Checked == true ? "1" : "0");

            string nowUtcTicks = Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString();
            Fileds.Add("update_by", HXCPcClient.GlobalStaticObj.UserID);
            Fileds.Add("update_time", nowUtcTicks);

            if (strPurchase_param_id.Length == 0)
            {
                Fileds.Add("purchase_param_id", Guid.NewGuid().ToString());//新ID                   
                Fileds.Add("create_by", HXCPcClient.GlobalStaticObj.UserID);
                Fileds.Add("create_time", nowUtcTicks);
            }
            else
            {
                keyName = "purchase_param_id";
                keyValue = strPurchase_param_id;
                opName = "更新采购业务参数";
            }
            bool bln = DBHelper.Submit_AddOrEdit(opName, GlobalStaticObj.CommAccCode, "sys_purchase_param", keyName, keyValue, Fileds);
            if (bln)
            {
                MessageBoxEx.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                foreach (Control ctl in this.panelEx1.Controls)
                {
                    if (ctl is CheckBox)
                    {
                        ctl.Enabled = false;
                    }
                }
            }
            else
            {
                MessageBoxEx.Show("保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #endregion
    }
}
