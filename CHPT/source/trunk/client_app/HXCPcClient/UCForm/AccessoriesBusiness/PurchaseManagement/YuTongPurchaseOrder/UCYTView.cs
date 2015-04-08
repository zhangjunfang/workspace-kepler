using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;
using HXCPcClient.CommonClass;
using Utility.Common;
using ServiceStationClient.ComponentUI;
using SYSModel;

namespace HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.YuTongPurchaseOrder
{
    public partial class UCYTView : UCBase
    {
        #region 变量
        UCYTManager uc;
        string orderstatus = string.Empty;
        string purchase_order_yt_id = string.Empty;
        tb_parts_purchase_order_2 yt_purchaseorder_model = new tb_parts_purchase_order_2();
        #endregion

        #region 初始化窗体
        /// <summary> 初始化窗体
        /// </summary>
        /// <param name="purchase_order_yt_id"></param>
        /// <param name="uc"></param>
        public UCYTView(string purchase_order_yt_id, UCYTManager uc)
        {
            InitializeComponent();
            this.uc = uc;
            this.purchase_order_yt_id = purchase_order_yt_id;
            base.SetBaseButtonStatus();
            base.SetButtonVisiableView();
            base.btnSync.Visible = true;
            if (uc == null)
            {
                base.SetBaeButtonEnable();
            }

            base.SyncEvent += new ClickHandler(UCYTView_SyncEvent);
            base.InvalidOrActivationEvent += new ClickHandler(UCPurchasePlanOrderView_InvalidOrActivationEvent);
            string[] NotReadOnlyColumnsName = new string[] { "colCheck" };
            CommonFuncCall.SetColumnReadOnly(gvPurchaseList, NotReadOnlyColumnsName);

            //CommonFuncCall.BindUnit(unit_id);
            LoadInfo(purchase_order_yt_id);
            GetAccessories(purchase_order_yt_id);
            //动态创建三种订单的panel区域
            LoadAllOrderTypeInfo();
        }
        #endregion

        #region 更新宇通审核状态
        /// <summary> 更新宇通审核状态 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCYTView_SyncEvent(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lblcrm_bill_id.Text.Trim()) && lblcrm_bill_id.Text.Trim() != ".")
            {
                DBHelper.WebServHandler("", SYSModel.EnumWebServFunName.LoadPartPurchaseStauts, lblcrm_bill_id.Text.Trim());//crm_bill_id
            }
            else
            { MessageBoxEx.Show("该订单未生成宇通单号,不能查询宇通审核状态!"); }
        }
        /// <summary> 激活/作废
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPurchasePlanOrderView_InvalidOrActivationEvent(object sender, EventArgs e)
        {
            string strmsg = string.Empty;
            List<SysSQLString> listSql = new List<SysSQLString>();
            SysSQLString sysStringSql = new SysSQLString();
            sysStringSql.cmdType = CommandType.Text;
            Dictionary<string, string> dic = new Dictionary<string, string>();//参数
            dic.Add("purchase_order_yt_id", purchase_order_yt_id);//单据ID
            dic.Add("update_by", GlobalStaticObj.UserID);//修改人Id
            dic.Add("update_name", GlobalStaticObj.UserName);//修改人姓名
            dic.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());//修改时间               
            if (orderstatus != Convert.ToInt32(DataSources.EnumAuditStatus.Invalid).ToString())
            {
                strmsg = "作废";
                dic.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.Invalid).ToString());//单据状态编号
                dic.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.Invalid, true));//单据状态名称
            }
            else
            {
                strmsg = "激活";
                string order_status = string.Empty;
                string order_status_name = string.Empty;
                DataTable dvt = DBHelper.GetTable("获得宇通采购订单的前一个状态", "tb_parts_purchase_order_2_BackUp", "order_status,order_status_name", "purchase_order_yt_id='" + purchase_order_yt_id + "'", "", "order by update_time desc");
                if (dvt != null && dvt.Rows.Count > 0)
                {
                    DataRow dr = dvt.Rows[0];
                    order_status = CommonCtrl.IsNullToString(dr["order_status"]);
                    if (order_status == Convert.ToInt32(DataSources.EnumAuditStatus.Invalid).ToString())
                    {
                        DataRow dr1 = dvt.Rows[1];
                        order_status = CommonCtrl.IsNullToString(dr1["order_status"]);
                        order_status_name = CommonCtrl.IsNullToString(dr1["order_status_name"]);
                    }
                }
                order_status = !string.IsNullOrEmpty(order_status) ? order_status : Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString();
                if (order_status == Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString())
                { order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.NOTAUDIT, true); }
                else if (order_status == Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString())
                { order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true); }
                dic.Add("order_status", order_status);//单据状态
                dic.Add("order_status_name", order_status_name);//单据状态名称
            }
            sysStringSql.sqlString = "update tb_parts_purchase_order_2 set order_status=@order_status,order_status_name=@order_status_name,update_by=@update_by,update_name=@update_name,update_time=@update_time where purchase_order_yt_id=@purchase_order_yt_id";
            sysStringSql.Param = dic;
            listSql.Add(sysStringSql);
            if (MessageBoxEx.Show("确认要" + strmsg + "吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            if (DBHelper.BatchExeSQLStringMultiByTrans("更新单据状态为" + strmsg + "", listSql))
            {
                MessageBoxEx.Show("" + strmsg + "成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                uc.BindgvYTPurchaseOrderList();
                deleteMenuByTag(this.Tag.ToString(), uc.Name);
            }
            else
            {
                MessageBoxEx.Show("" + strmsg + "失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 方法、函数
        /// <summary> 加载采购计划信息和配件信息
        /// </summary>
        /// <param name="purchase_order_yt_id"></param>
        private void LoadInfo(string purchase_order_yt_id)
        {
            if (!string.IsNullOrEmpty(purchase_order_yt_id))
            {
                //1.查看一条宇通采购订单信息
                DataTable dt = DBHelper.GetTable("查看一条宇通采购订单信息", "tb_parts_purchase_order_2", "*", " purchase_order_yt_id='" + purchase_order_yt_id + "'", "", "");
                if (dt != null && dt.Rows.Count > 0)
                {
                    CommonFuncCall.SetModlByDataTable(yt_purchaseorder_model, dt);
                    CommonFuncCall.SetShowControlValue(this, yt_purchaseorder_model, "View");
                    orderstatus = yt_purchaseorder_model.order_status;
                    if (!string.IsNullOrEmpty(lblorder_date.Text))
                    {
                        long ticks = Convert.ToInt64(lblorder_date.Text);
                        lblorder_date.Text = Common.UtcLongToLocalDateTime(ticks).ToString();
                    }
                    lblcreate_time1.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(yt_purchaseorder_model.create_time.ToString())).ToString();
                    if (yt_purchaseorder_model.update_time > 0)
                    { lblupdate_time1.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(yt_purchaseorder_model.update_time.ToString())).ToString(); }

                    if (orderstatus == Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString())
                    {
                        //已提交状态屏蔽提交、编辑、删除按钮
                        base.btnSubmit.Enabled = false;
                        base.btnEdit.Enabled = false;
                        base.btnDelete.Enabled = false;
                        base.btnActivation.Enabled = false;
                    }
                    else if (orderstatus == Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString())
                    {
                        //已审核时屏蔽提交、审核、编辑、删除按钮
                        base.btnSubmit.Enabled = false;
                        base.btnVerify.Enabled = false;
                        base.btnEdit.Enabled = false;
                        base.btnDelete.Enabled = false;
                        base.btnActivation.Enabled = false;
                    }
                    else if (orderstatus == Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString() || orderstatus == Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString())
                    {
                        //审核没通过时屏蔽审核按钮
                        base.btnVerify.Enabled = false;
                    }
                    else if (orderstatus == Convert.ToInt32(DataSources.EnumAuditStatus.Invalid).ToString())
                    {
                        base.btnActivation.Caption = "激活";
                        base.btnSubmit.Enabled = false;
                        base.btnVerify.Enabled = false;
                        base.btnEdit.Enabled = false;
                    }
                }
            }
        }
        /// <summary> 根据采购订单号获取采购配件信息
        /// </summary>
        /// <param name="purchase_order_yt_id"></param>
        private void GetAccessories(string purchase_order_yt_id)
        {
            string conId = string.Empty;
            DataTable dt_parts_purchase = DBHelper.GetTable("查询宇通采购订单配件表信息", "tb_parts_purchase_order_p_2", "*", " purchase_order_yt_id='" + purchase_order_yt_id + "'", "", "");
            if (dt_parts_purchase != null && dt_parts_purchase.Rows.Count > 0)
            {
                foreach (DataRow dr in dt_parts_purchase.Rows)
                {
                    DataGridViewRow dgvr = gvPurchaseList.Rows[gvPurchaseList.Rows.Add()];
                    dgvr.Cells["parts_code"].Value = dr["parts_code"];//配件编码
                    dgvr.Cells["parts_name"].Value = dr["parts_name"];//配件名称
                    dgvr.Cells["car_factory_code"].Value = dr["car_factory_code"];//厂商编号
                    dgvr.Cells["drawing_num"].Value = dr["drawing_num"];//图号
                    dgvr.Cells["model"].Value = dr["model"];//规格型号
                    dgvr.Cells["application_count"].Value = dr["application_count"];//申请数量
                    dgvr.Cells["price"].Value = dr["price"];//单价
                    dgvr.Cells["money"].Value = dr["money"];//金额
                    dgvr.Cells["conf_count"].Value = dr["conf_count"];//确认数量
                    dgvr.Cells["parts_explain"].Value = dr["parts_explain"];//配件说明
                    dgvr.Cells["replaces"].Value = dr["replaces"];//代替
                    dgvr.Cells["unit_name"].Value = dr["unit_name"];//单位
                    dgvr.Cells["center_library_explain"].Value = dr["center_library_explain"];//中心站/库处理说明
                    //dgvr.Cells["total_library_explain"].Value = dr["total_library_explain"];//总库处理说明
                    dgvr.Cells["cancel_reasons"].Value = dr["cancel_reasons"];//取消原因
                    dgvr.Cells["relation_order"].Value = dr["relation_order"];//引用单号
                    dgvr.Cells["create_by"].Value = dr["create_by"];
                    dgvr.Cells["create_name"].Value = dr["create_name"];
                    dgvr.Cells["create_time"].Value = Common.UtcLongToLocalDateTime(Convert.ToInt64(dr["create_time"].ToString()));
                }
            }
        }
        /// <summary> 加载三种类型订单的panel区域信息
        /// </summary>
        void LoadAllOrderTypeInfo()
        {
            try
            {
                if (yt_purchaseorder_model != null)
                {
                    if (!string.IsNullOrEmpty(yt_purchaseorder_model.order_type))
                    {
                        switch (yt_purchaseorder_model.order_type)
                        {
                            //配件需求订单
                            case "order_type_100000001":
                                LoadUCXuQiu();
                                break;
                            //产品升级订单
                            case "order_type_100000005":
                                LoadUCShengJi();
                                break;
                            //新三包调件订单
                            case "order_type_100000004":
                                LoadUCSanBao();
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        /// <summary> 加载 配件需求订单 用户控件
        /// </summary>
        void LoadUCXuQiu()
        {
            this.panelArea.Controls.Clear();
            UserControlXuQiuView uc = new UserControlXuQiuView();
            this.panelArea.Controls.Add(uc);
            uc.Dock = System.Windows.Forms.DockStyle.Fill;
            uc.LoadControlInfo(yt_purchaseorder_model);
        }
        /// <summary> 加载 产品升级订单 用户控件
        /// </summary>
        void LoadUCShengJi()
        {
            this.panelArea.Controls.Clear();
            UserControlShengJiView uc = new UserControlShengJiView();
            this.panelArea.Controls.Add(uc);
            uc.Dock = System.Windows.Forms.DockStyle.Fill;
            uc.LoadControlInfo(yt_purchaseorder_model);
        }
        /// <summary> 加载 新三包调件订单 用户控件
        /// </summary>
        void LoadUCSanBao()
        {
            this.panelArea.Controls.Clear();
            UserControlSanBaoView uc = new UserControlSanBaoView();
            this.panelArea.Controls.Add(uc);
            uc.Dock = System.Windows.Forms.DockStyle.Fill;
            uc.LoadControlInfo(yt_purchaseorder_model);
        } 
        #endregion
    }
}
