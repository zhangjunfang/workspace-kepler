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
    /// 库存业务参数
    /// 孙明生
    /// </summary>
    public partial class UCStockBPara : UCBase
    {
        /// <summary>
        /// 库存业务参数id
        /// </summary>
        string strStock_param_id = "";

        #region 初始化
        public UCStockBPara()
        {
            InitializeComponent();
           
        }
        #endregion

        #region --窗体初始化
        private void UCStockBPara_Load(object sender, EventArgs e)
        {
            base.RoleButtonStstus(this.Name);//角色按钮权限-是否隐藏         

            this.uiHandler -= new UiHandler(this.ShowData);
            this.uiHandler += new UiHandler(this.ShowData);

            base.SaveEvent += new ClickHandler(UCStockBPara_SaveEvent);
            base.EditEvent += new ClickHandler(UCStockBPara_EditEvent);
            base.CancelEvent += new ClickHandler(UCStockBPara_CancelEvent);

            ThreadPool.QueueUserWorkItem(new WaitCallback(this._ShowData));
        }
        #endregion

        #region --显示数据

        /// <summary> 异步数据查询 
        /// </summary>
        /// <param name="obj"></param>
        private void _ShowData(object obj)
        {
            DataTable dt = DBHelper.GetTable("查询库存业务参数", GlobalStaticObj.CommAccCode, 
                "sys_stock_param", "*", "book_id='" + GlobalStaticObj.CurrAccID + "'", "", "");
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
                strStock_param_id = dt.Rows[0]["stock_param_id"].ToString();

                chkstorage_manage.Checked = dt.Rows[0]["storage_manage"].ToString() == "1" ? true : false;
                chkmaking_audit_one_person.Checked = dt.Rows[0]["making_audit_one_person"].ToString() == "1" ? true : false;
                chkallow_zero_lib_stock.Checked = dt.Rows[0]["allow_zero_lib_stock"].ToString() == "1" ? true : false;
                chkallow_zero_lib_junction.Checked = dt.Rows[0]["allow_zero_lib_junction"].ToString() == "1" ? true : false;

                chksingle_editors_same_person.Checked = dt.Rows[0]["single_editors_one_person"].ToString() == "1" ? true : false;
                chksingle_audit_same_person.Checked = dt.Rows[0]["single_audit_one_person"].ToString() == "1" ? true : false;
                chksingle_disabled_same_person.Checked = dt.Rows[0]["single_disabled_one_person"].ToString() == "1" ? true : false;
                chksingle_delete_same_person.Checked = dt.Rows[0]["single_delete_one_person"].ToString() == "1" ? true : false;

                rdbmonthly_average_method.Checked = dt.Rows[0]["monthly_average_method"].ToString() == "1" ? true : false;
                rdbmoving_average_method.Checked = dt.Rows[0]["moving_average_method"].ToString() == "1" ? true : false;
                rdbfifo_method.Checked = dt.Rows[0]["fifo_method"].ToString() == "1" ? true : false;

                nudcounts.Value = Convert.ToDecimal(dt.Rows[0]["counts"].ToString());
                chkcounts_zero.Checked = dt.Rows[0]["counts_zero"].ToString() == "1" ? true : false;
                nudprice.Value = Convert.ToDecimal(dt.Rows[0]["price"].ToString());
                chkprice_zero.Checked = dt.Rows[0]["price_zero"].ToString() == "1" ? true : false;

                chkwarehous_single_reference.Checked = dt.Rows[0]["warehous_single_reference"].ToString() == "1" ? true : false;
            }
        }

        #endregion

        #region 编辑
        void UCStockBPara_EditEvent(object sender, EventArgs e)
        {
            this.SetEnable(true);           
        }
        #endregion

        private void SetEnable(bool flag)
        {
            chkstorage_manage.Enabled = flag;
            chkmaking_audit_one_person.Enabled = flag;
            chkallow_zero_lib_stock.Enabled = flag;
            chkallow_zero_lib_junction.Enabled = flag;

            chksingle_editors_same_person.Enabled = flag;
            chksingle_audit_same_person.Enabled = flag;
            chksingle_disabled_same_person.Enabled = flag;
            chksingle_delete_same_person.Enabled = flag;

            rdbmonthly_average_method.Enabled = flag;
            rdbmoving_average_method.Enabled = flag;
            rdbfifo_method.Enabled = flag;


            chkcounts_zero.Enabled = flag;
            chkprice_zero.Enabled = flag;
            chkwarehous_single_reference.Enabled = flag;

            nudcounts.ReadOnly = !flag;
            nudprice.ReadOnly = !flag;
        }

        #region 取消
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCStockBPara_CancelEvent(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(this._ShowData));
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCStockBPara_SaveEvent(object sender, EventArgs e)
        {
            if (!chkstorage_manage.Enabled)
            {
                return;
            }

            string keyName = string.Empty;
            string keyValue = string.Empty;
            string opName = "新增库存业务参数";
            Dictionary<string, string> Fileds = new Dictionary<string, string>();
            Fileds.Add("storage_manage", chkstorage_manage.Checked == true ? "1" : "0");
            Fileds.Add("making_audit_one_person", chkmaking_audit_one_person.Checked == true ? "1" : "0");
            Fileds.Add("allow_zero_lib_stock", chkallow_zero_lib_stock.Checked == true ? "1" : "0");
            Fileds.Add("allow_zero_lib_junction", chkallow_zero_lib_junction.Checked == true ? "1" : "0");

            Fileds.Add("single_editors_one_person", chksingle_editors_same_person.Checked == true ? "1" : "0");
            Fileds.Add("single_audit_one_person", chksingle_audit_same_person.Checked == true ? "1" : "0");
            Fileds.Add("single_disabled_one_person", chksingle_disabled_same_person.Checked == true ? "1" : "0");
            Fileds.Add("single_delete_one_person", chksingle_delete_same_person.Checked == true ? "1" : "0");

            Fileds.Add("monthly_average_method", rdbmonthly_average_method.Checked == true ? "1" : "0");
            Fileds.Add("moving_average_method", rdbmoving_average_method.Checked == true ? "1" : "0");
            Fileds.Add("fifo_method", rdbfifo_method.Checked == true ? "1" : "0");

            Fileds.Add("counts", nudcounts.Value.ToString());
            Fileds.Add("counts_zero", chkcounts_zero.Checked == true ? "1" : "0");
            Fileds.Add("price", nudprice.Value.ToString());
            Fileds.Add("price_zero", chkprice_zero.Checked == true ? "1" : "0");

            Fileds.Add("warehous_single_reference", chkwarehous_single_reference.Checked == true ? "1" : "0");

            string nowUtcTicks = Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString();
            Fileds.Add("update_by", HXCPcClient.GlobalStaticObj.UserID);
            Fileds.Add("update_time", nowUtcTicks);

            if (strStock_param_id.Length == 0)
            {
                Fileds.Add("stock_param_id", Guid.NewGuid().ToString());//新ID                   
                Fileds.Add("create_by", HXCPcClient.GlobalStaticObj.UserID);
                Fileds.Add("create_time", nowUtcTicks);
            }
            else
            {
                keyName = "stock_param_id";
                keyValue = strStock_param_id;
                opName = "更新库存业务参数";
            }
            bool bln = DBHelper.Submit_AddOrEdit(opName, GlobalStaticObj.CommAccCode, "sys_stock_param", keyName, keyValue, Fileds);
            if (bln)
            {
                MessageBoxEx.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                this.SetEnable(false);
            }
            else
            {
                MessageBoxEx.Show("保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}
