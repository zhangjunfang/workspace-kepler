using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using HXCPcClient.Chooser;
using SYSModel;
using Utility.Common;
using ServiceStationClient.ComponentUI;
using System.Text.RegularExpressions;
using HXCPcClient.UCForm.RepairBusiness.RepairCallback;
using ServiceStationClient.ComponentUI.TextBox;

namespace HXCPcClient.UCForm.RepairBusiness.RepairBalance
{
    /// <summary>
    /// 维修管理-维修结算单新增与修改
    /// Author：JC
    /// AddTime：2014.10.21
    /// </summary>
    public partial class UCRepairBalanceAddOrEdit : UCBase
    {
        #region 属性设置
        /// <summary>
        /// 父窗体
        /// </summary>
        public UCRepairBalanceManager uc;
        /// <summary>
        /// 窗体状态
        /// </summary>
        public WindowStatus wStatus;
        /// <summary>
        /// 结算单ID
        /// </summary>
        public string strId = string.Empty;
        /// <summary>
        /// 操作名称
        /// </summary>
        string opName = string.Empty;
        /// <summary>
        /// 附件操作Id值
        /// </summary>
        string strValue = string.Empty;
        /// <summary>
        /// 工时货款
        /// </summary>
        decimal dcHmoney = 0;
        /// <summary>
        /// 配件货款
        /// </summary>
        decimal dcPmoney = 0;
        /// <summary>
        /// 单据状态
        /// </summary>
        string strStatus = string.Empty;
        /// <summary>
        /// 前置单据Id
        /// </summary>
        public string strBefore_orderId = string.Empty;
        /// <summary>
        /// 应收总额
        /// </summary>
        string strShouldSum = string.Empty;
        /// <summary>
        /// 用于判断用料是否重复添加
        /// </summary>
        List<string> listMater = new List<string>();
        /// <summary>
        ///  用于判断项目是否重复添加
        /// </summary>
        List<string> listProject = new List<string>();
        /// <summary>
        /// 是否自动关闭
        /// </summary>
        bool isAutoClose = false;
        #endregion

        #region 初始化窗体
        public UCRepairBalanceAddOrEdit()
        {
            InitializeComponent();
            GetRepairNo();
            initializeData();
            SetDgvAnchor();
            CommonFuncCall.BindDepartment(cboOrgId, GlobalStaticObj.CurrUserCom_Id, "请选择");
            BindBalanceWay();//结算方式
            BindAccount();//结算账户
            SetTopbuttonShow();
            CommonFuncCall.BindComBoxDataSource(cobMInvoiceType, "sys_receipt_type", "全部",false);//发票类型   
            base.CancelEvent += new ClickHandler(UCRepairBalanceAddOrEdit_CancelEvent);
            base.SaveEvent += new ClickHandler(UCRepairBalanceAddOrEdit_SaveEvent);
            base.BalanceEvent += new ClickHandler(UCRepairBalanceAddOrEdit_BalanceEvent);
            base.ImportEvent += new ClickHandler(UCRepairBalanceAddOrEdit_ImportEvent);
            SetQuick();
        }
        #endregion

        #region 设置速查功能
        /// <summary>
        /// 设置速查功能
        /// </summary>
        private void SetQuick()
        {

            //设置车牌号速查
            txtCarNO.SetBindTable("v_vehicle", "license_plate");
            txtCarNO.GetDataSourced += new ServiceStationClient.ComponentUI.TextBox.TextChooser.GetDataSourceHandler(tc_GetDataSourced);
            txtCarNO.DataBacked += new TextChooser.DataBackHandler(txtCarNO_DataBacked);
            //设置客户编码速查
            txtCustomNO.SetBindTable("tb_customer", "cust_code");
            txtCustomNO.GetDataSourced += new ServiceStationClient.ComponentUI.TextBox.TextChooser.GetDataSourceHandler(tc_GetDataSourced);
            txtCustomNO.DataBacked += new TextChooser.DataBackHandler(txtCustomNO_DataBacked);

        }
        /// <summary>
        /// 客户编码速查连带信息
        /// </summary>
        /// <param name="dr"></param>
        void txtCustomNO_DataBacked(DataRow dr)
        {
            try
            {
                txtCustomNO.Text = CommonCtrl.IsNullToString(dr["cust_code"]);
                txtCustomNO.Tag = CommonCtrl.IsNullToString(dr["cust_id"]);
                txtCustomName.Caption = CommonCtrl.IsNullToString(dr["cust_name"]);
                txtBalanceCompany.Text = CommonCtrl.IsNullToString(dr["cust_name"]);
                txtBalanceCompany.Tag = CommonCtrl.IsNullToString(dr["cust_id"]);
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        /// <summary>
        /// 车牌号速查连带信息
        /// </summary>
        /// <param name="dr"></param>
        void txtCarNO_DataBacked(DataRow dr)
        {
            try
            {
                txtVIN.Caption = CommonCtrl.IsNullToString(dr["vin"]);
                txtEngineNo.Caption = CommonCtrl.IsNullToString(dr["engine_num"]);
                txtCarType.Text = GetVmodel(CommonCtrl.IsNullToString(dr["v_model"]));
                txtCarType.Tag = CommonCtrl.IsNullToString(dr["v_model"]);
                cobCarBrand.SelectedValue = CommonCtrl.IsNullToString(dr["v_brand"]);
                cobColor.SelectedValue = CommonCtrl.IsNullToString(dr["v_color"]);
                txtCustomNO.Text = CommonCtrl.IsNullToString(dr["cust_code"]);
                txtCustomNO.Tag = CommonCtrl.IsNullToString(dr["cust_id"]);
                txtCustomName.Caption = CommonCtrl.IsNullToString(dr["cust_name"]);
                txtContact.Caption = CommonCtrl.IsNullToString(dr["cont_name"]);
                txtContactPhone.Caption = CommonCtrl.IsNullToString(dr["cont_phone"]);
                txtBalanceCompany.Text = CommonCtrl.IsNullToString(dr["cust_name"]);
                txtBalanceCompany.Tag = CommonCtrl.IsNullToString(dr["cust_id"]);
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        /// <summary>
        /// 设置速查
        /// </summary>
        /// <param name="sqlString"></param>
        void tc_GetDataSourced(TextChooser tc, string sqlString)
        {
            try
            {
                DataTable dvt = CommonFuncCall.GetDataSource(sqlString);
                tc.SetDataSource(dvt);
                if (dvt != null)
                {
                    tc.Search();
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }

        #endregion

        #region 导入功能
        void UCRepairBalanceAddOrEdit_ImportEvent(object sender, EventArgs e)
        {
            int x = Cursor.Position.X;
            int y = Cursor.Position.Y;
            ShowContextMenuStrip(x, y);
        }
        /// <summary> 点击导入按钮，显示导入方式的下拉选项
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        private void ShowContextMenuStrip(int X, int Y)
        {
            contextMenuM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            contextMenuM.Show();
            contextMenuM.Location = new System.Drawing.Point(X, Y);
        }
        #endregion

        #region 顶部button显示设置
        /// <summary>
        ///  顶部button显示设置
        /// </summary>
        private void SetTopbuttonShow()
        {
            base.btnAdd.Visible = false;
            base.btnCopy.Visible = false;
            base.btnEdit.Visible = false;
            base.btnDelete.Visible = false;
            base.btnStatus.Visible = false;
            base.btnVerify.Visible = false;
            base.btnSync.Visible = false;
            base.btnConfirm.Visible = false;
            base.btnActivation.Visible = false;
            base.btnBalance.Visible = true;
            base.btnSubmit.Visible = false;
        }
        #endregion

        #region 绑定结算方式
        /// <summary>
        /// 绑定结算方式
        /// </summary>
        private void BindBalanceWay()
        {
            try
            {
                DataTable dt = DBHelper.GetTable("", "tb_balance_way", "balance_way_id,balance_way_name,default_account", "", "", "");
                List<ListItem> list = new List<ListItem>();
                //list.Add(new ListItem("", "全部"));
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new ListItem(dr["balance_way_id"], dr["balance_way_name"].ToString()));
                }
                cobBalanceType.DataSource = list;
                cobBalanceType.ValueMember = "Value";
                cobBalanceType.DisplayMember = "Text";
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 绑定付款账户
        /// <summary>
        /// 绑定付款账户
        /// </summary>
        private void BindAccount()
        {
            try
            {
                DataTable dat = DBHelper.GetTable("", "v_cashier_account", "cashier_account,account_name,bank_name,bank_account", "", "", "");

                List<ListItem> list = new List<ListItem>();
                //list.Add(new ListItem("", "全部"));
                foreach (DataRow dr in dat.Rows)
                {
                    list.Add(new ListItem(dr["cashier_account"], dr["account_name"].ToString()));
                }
                cobSetAccount.DataSource = list;
                cobSetAccount.ValueMember = "Value";
                cobSetAccount.DisplayMember = "Text";
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 结算事件
        void UCRepairBalanceAddOrEdit_BalanceEvent(object sender, EventArgs e)
        {
            opName = "结算结算单信息";
            SaveOrSubmitMethod("结算", DataSources.EnumAuditStatus.Balance);
        }
        #endregion

        #region 保存事件
        void UCRepairBalanceAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            opName = "保存结算单信息";
            SaveOrSubmitMethod("保存", DataSources.EnumAuditStatus.DRAFT);
        }
        #endregion

        #region 验证必填项
        private Boolean CheckControlValue()
        {
            try
            {
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(txtCarNO.Text.Trim()))
                {
                    Validator.SetError(errorProvider1, txtCarNO, "车牌号不能为空!");
                    return false;
                }
                if (string.IsNullOrEmpty(txtCustomNO.Text.Trim()))
                {
                    Validator.SetError(errorProvider1, txtCustomNO, "客户编码不能为空!");
                    return false;
                }
                if (string.IsNullOrEmpty(txtCustomName.Caption.Trim()))
                {
                    Validator.SetError(errorProvider1, txtCustomName, "客户名称不能为空!");
                    return false;
                }
                if (string.IsNullOrEmpty(txtDriver.Caption.Trim()))
                {
                    Validator.SetError(errorProvider1, txtDriver, "报修人不能为空!");
                    return false;
                }
                if (string.IsNullOrEmpty(txtDriverPhone.Caption.Trim()))//报修人手机
                {
                    Validator.SetError(errorProvider1, txtDriverPhone, "报修人手机不能为空!");
                    return false;
                }
                if (string.IsNullOrEmpty(txtReceivedSum.Caption.Trim()))
                {
                    Validator.SetError(errorProvider1, txtReceivedSum, "实收总额不能为空!");
                    return false;
                }
                if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobMInvoiceType.SelectedValue)))
                {
                    Validator.SetError(errorProvider1, cobMInvoiceType, "开票方式不能为空!");
                    return false;
                }
                if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobBalanceType.SelectedValue)))
                {
                    Validator.SetError(errorProvider1, cobBalanceType, "结算方式不能为空!");
                    return false;
                }
                if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobSetAccount.SelectedValue)))
                {
                    Validator.SetError(errorProvider1, cobSetAccount, "结算账户不能为空!");
                    return false;
                }
                if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtBalanceCompany.Tag)))
                {
                    Validator.SetError(errorProvider1, txtBalanceCompany, "结算单位不能为空!");
                    return false;
                }
                if (!ucAttr.CheckAttachment())
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
                return false;
            }
            return true;
        }
        #endregion

        #region 保存、结算方法
        /// <summary>
        /// 保存、结算方法
        /// </summary>
        /// <param name="strMessage">提示信息</param>
        /// <param name="Estatus">单据操作状态</param>
        private void SaveOrSubmitMethod(string strMessage, DataSources.EnumAuditStatus Estatus)
        {
            try
            {
                if (!CheckControlValue()) return;
                if (MessageBoxEx.Show("确认要" + strMessage + "吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
                List<SQLObj> listSql = new List<SQLObj>();
                SaveOrderInfo(listSql, Estatus);
                SaveBalanceInfo(listSql, strId);
                SaveProjectData(listSql, strId);
                SaveMaterialsData(listSql, strId);
                SaveOtherData(listSql, strId);
                ucAttr.TableName = "tb_maintain_info";
                ucAttr.TableNameKeyValue = strId;
                listSql.AddRange(ucAttr.AttachmentSql);
                if (DBHelper.BatchExeSQLMultiByTrans(opName, listSql))
                {
                    MessageBoxEx.Show("" + strMessage + "成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    uc.BindPageData();
                    isAutoClose = true;
                    deleteMenuByTag(this.Tag.ToString(), this.uc.Name);
                }
                else
                {
                    MessageBoxEx.Show("" + strMessage + "失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
                MessageBoxEx.Show("" + strMessage + "失败！" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 取消事件
        void UCRepairBalanceAddOrEdit_CancelEvent(object sender, EventArgs e)
        {
            try
            {
                if (MessageBoxEx.Show("确认要取消吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
                if (!string.IsNullOrEmpty(strBefore_orderId))
                {
                    List<SQLObj> listSql = new List<SQLObj>();
                    UpdateMaintainInfo(listSql, strBefore_orderId, "", "0");
                    DBHelper.BatchExeSQLMultiByTrans("更新前置单据状体", listSql);
                }
                isAutoClose = true;
                deleteMenuByTag(this.Tag.ToString(), uc.Name);
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 维修结算单基本信息保存
        /// <summary>
        /// 维修结算单基本信息保存
        /// </summary>
        private void SaveOrderInfo(List<SQLObj> listSql, DataSources.EnumAuditStatus status)
        {
            try
            {
                SQLObj obj = new SQLObj();
                obj.cmdType = CommandType.Text;
                Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();

                #region 基本信息
                dicParam.Add("vehicle_no", new ParamObj("vehicle_no", txtCarNO.Text.Trim(), SysDbType.VarChar, 20));//车牌号
                dicParam.Add("vehicle_vin", new ParamObj("vehicle_vin", txtVIN.Caption.Trim(), SysDbType.VarChar, 40));//VIN
                dicParam.Add("engine_no", new ParamObj("engine_no", txtEngineNo.Caption.Trim(), SysDbType.VarChar, 40));//发动机号
                dicParam.Add("vehicle_model", new ParamObj("vehicle_model", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtCarType.Tag)) ? txtCarType.Tag : null, SysDbType.VarChar, 40));//车型
                dicParam.Add("vehicle_brand", new ParamObj("vehicle_brand", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobCarBrand.SelectedValue)) ? cobCarBrand.SelectedValue.ToString() : null, SysDbType.VarChar, 40));//车辆品牌
                dicParam.Add("driver_name", new ParamObj("driver_name", txtDriver.Caption.Trim(), SysDbType.VarChar, 20));//司机
                if (!string.IsNullOrEmpty(txtDriverPhone.Caption.Trim()))//司机手机
                {
                    dicParam.Add("driver_mobile", new ParamObj("driver_mobile", txtDriverPhone.Caption.Trim(), SysDbType.VarChar, 15));//司机手机
                }
                else
                {
                    dicParam.Add("driver_mobile", new ParamObj("driver_mobile", null, SysDbType.VarChar, 15));//司机手机
                }
                dicParam.Add("vehicle_color", new ParamObj("vehicle_color", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobColor.SelectedValue)) ? cobColor.SelectedValue.ToString() : null, SysDbType.VarChar, 40));//颜色
                dicParam.Add("travel_mileage", new ParamObj("travel_mileage", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtMil.Caption.Trim())) ? txtMil.Caption.Trim() : null, SysDbType.Decimal, 15));//行驶里程
                dicParam.Add("oil_into_factory", new ParamObj("oil_into_factory", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtMl.Caption.Trim())) ? txtMl.Caption.Trim() : null, SysDbType.Decimal, 15));//进场油量
                dicParam.Add("maintain_mileage", new ParamObj("maintain_mileage", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtMl.Caption.Trim())) ? txtSuggestMil.Caption.Trim() : null, SysDbType.Decimal, 15));//建议保养里程
                dicParam.Add("completion_time", new ParamObj("completion_time", Common.LocalDateTimeToUtcLong(dtpSuggestTime.Value).ToString(), SysDbType.BigInt));//预计完工时间
                dicParam.Add("maintain_payment", new ParamObj("maintain_payment", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobPayType.SelectedValue)) ? cobPayType.SelectedValue.ToString() : null, SysDbType.VarChar, 40));//维修付费方式
                dicParam.Add("maintain_type", new ParamObj("maintain_type", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobRepType.SelectedValue)) ? cobRepType.SelectedValue.ToString() : null, SysDbType.VarChar, 40));//维修类别
                dicParam.Add("remark", new ParamObj("remark", txtRemark.Caption.Trim(), SysDbType.VarChar, 40));//备注
                dicParam.Add("favorable_reason", new ParamObj("favorable_reason", txtPreferReason.Caption.Trim(), SysDbType.VarChar, 40));//优惠原因
                dicParam.Add("customer_code", new ParamObj("customer_code", txtCustomNO.Text, SysDbType.VarChar, 40));//客户编码
                dicParam.Add("customer_id", new ParamObj("customer_id", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtCustomNO.Tag)) ? txtCustomNO.Tag.ToString() : null, SysDbType.VarChar, 40));//客户Id
                dicParam.Add("customer_name", new ParamObj("customer_name", txtCustomName.Caption.Trim(), SysDbType.VarChar, 20));//客户名称           
                dicParam.Add("linkman", new ParamObj("linkman", txtContact.Caption.Trim(), SysDbType.VarChar, 20));//联系人
                if (!string.IsNullOrEmpty(txtContactPhone.Caption.Trim()))//联系人手机
                {
                    dicParam.Add("link_man_mobile", new ParamObj("link_man_mobile", txtContactPhone.Caption.Trim(), SysDbType.VarChar, 15));//联系人手机 
                }
                else
                {
                    dicParam.Add("link_man_mobile", new ParamObj("link_man_mobile", null, SysDbType.VarChar, 15));//联系人手机 
                }
                //经办人id
                dicParam.Add("responsible_opid", new ParamObj("responsible_opid", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobYHandle.SelectedValue)) ? cobYHandle.SelectedValue.ToString() : null, SysDbType.VarChar, 40));
                //经办人
                dicParam.Add("responsible_name", new ParamObj("responsible_name", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobYHandle.SelectedValue)) ? cobYHandle.SelectedText : null, SysDbType.VarChar, 40));
                //部门
                dicParam.Add("org_name", new ParamObj("org_name", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cboOrgId.SelectedValue)) ? cboOrgId.SelectedValue : null, SysDbType.VarChar, 40));
                dicParam.Add("enable_flag", new ParamObj("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString(), SysDbType.VarChar, 1));//信息状态（1|激活；2|作废；0|删除）           
                //单据来源,0接待单、1预约单、2返修单、3结算单，默认0
                dicParam.Add("orders_source", new ParamObj("orders_source", "3", SysDbType.VarChar, 1));
                if (status == DataSources.EnumAuditStatus.Balance)//结算操作时生成单号
                {
                    dicParam.Add("maintain_no", new ParamObj("maintain_no", labReserveNoS.Text.Trim(), SysDbType.VarChar, 40));//维修单号               
                    //单据状态
                    dicParam.Add("info_status", new ParamObj("info_status", Convert.ToInt32(status).ToString(), SysDbType.VarChar, 40));
                    if (!string.IsNullOrEmpty(strBefore_orderId))
                    {
                        dicParam.Add("before_orderId", new ParamObj("before_orderId", strBefore_orderId, SysDbType.VarChar, 40));
                        UpdateMaintainInfo(listSql, strBefore_orderId, labReserveInfo.Text.Trim(), "2");
                    }
                    else
                    {
                        dicParam.Add("before_orderId", new ParamObj("before_orderId", null, SysDbType.VarChar, 40));
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(strStatus) && strStatus == Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString())
                    {
                        dicParam.Add("maintain_no", new ParamObj("maintain_no", labReserveNoS.Text.Trim(), SysDbType.VarChar, 40));//发货单号 
                        //单据状态
                        dicParam.Add("info_status", new ParamObj("info_status", Convert.ToInt32(status).ToString(), SysDbType.VarChar, 40));
                    }
                    else
                    {
                        //单据状态
                        dicParam.Add("info_status", new ParamObj("info_status", Convert.ToInt32(status).ToString(), SysDbType.VarChar, 40));
                        if (!string.IsNullOrEmpty(strBefore_orderId))
                        {
                            dicParam.Add("maintain_no", new ParamObj("maintain_no", labReserveNoS.Text.Trim(), SysDbType.VarChar, 40));//维修单号
                        }
                        else
                        {
                            dicParam.Add("maintain_no", new ParamObj("maintain_no", null, SysDbType.VarChar, 40));//维修单号
                        }
                    }
                    if (!string.IsNullOrEmpty(strBefore_orderId))
                    {
                        opName = "更新前置单据导";
                        UpdateMaintainInfo(listSql, strBefore_orderId, "", "0");
                        dicParam.Add("before_orderId", new ParamObj("before_orderId", strBefore_orderId, SysDbType.VarChar, 40));
                    }
                    else
                    {
                        dicParam.Add("before_orderId", new ParamObj("before_orderId", null, SysDbType.VarChar, 40));
                    }
                }
                #endregion
                if ((wStatus == WindowStatus.Add || wStatus == WindowStatus.Copy) && string.IsNullOrEmpty(strBefore_orderId))
                {
                    strId = Guid.NewGuid().ToString();
                    dicParam.Add("maintain_id", new ParamObj("maintain_id", strId, SysDbType.VarChar, 40));//Id
                    dicParam.Add("create_by", new ParamObj("create_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//创建人id（制单人） 
                    dicParam.Add("create_name", new ParamObj("create_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//创建人
                    dicParam.Add("create_time", new ParamObj("create_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));
                    obj.sqlString = @"insert into tb_maintain_info (vehicle_no,vehicle_vin,engine_no,vehicle_model,vehicle_brand,driver_name,driver_mobile,vehicle_color,travel_mileage
                 ,oil_into_factory,maintain_mileage,completion_time,maintain_payment,maintain_type,remark,favorable_reason,customer_code,customer_name,linkman,link_man_mobile,responsible_opid
                 ,responsible_name,org_name,enable_flag,info_status,maintain_no,maintain_id,create_by,create_name,create_time,orders_source,before_orderId)
                 values (@vehicle_no,@vehicle_vin,@engine_no,@vehicle_model,@vehicle_brand,@driver_name,@driver_mobile,@vehicle_color,@travel_mileage
                 ,@oil_into_factory,@maintain_mileage,@completion_time,@maintain_payment,@maintain_type,@remark,@favorable_reason,@customer_code,@customer_name,@linkman,@link_man_mobile,@responsible_opid
                 ,@responsible_name,@org_name,@enable_flag,@info_status,@maintain_no,@maintain_id,@create_by,@create_name,@create_time,@orders_source,@before_orderId);";
                }
                else if (wStatus == WindowStatus.Edit || !string.IsNullOrEmpty(strBefore_orderId))
                {
                    if (!string.IsNullOrEmpty(strBefore_orderId))
                    {
                        strId = strBefore_orderId;
                    }
                    dicParam.Add("maintain_id", new ParamObj("maintain_id", strId, SysDbType.VarChar, 40));//Id
                    dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
                    dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
                    dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                    obj.sqlString = @"update tb_maintain_info set vehicle_no=@vehicle_no,vehicle_vin=@vehicle_vin,engine_no=@engine_no,vehicle_model=@vehicle_model,vehicle_brand=@vehicle_brand,driver_name=@driver_name,driver_mobile=@driver_mobile,vehicle_color=@vehicle_color,travel_mileage=@travel_mileage
                 ,oil_into_factory=@oil_into_factory,maintain_mileage=@maintain_mileage,completion_time=@completion_time,maintain_payment=@maintain_payment,maintain_type=@maintain_type,remark=@remark,favorable_reason=@favorable_reason,customer_code=@customer_code,customer_name=@customer_name,linkman=@linkman,link_man_mobile=@link_man_mobile,responsible_opid=@responsible_opid
                 ,responsible_name=@responsible_name,org_name=@org_name,enable_flag=@enable_flag,info_status=@info_status,maintain_no=@maintain_no,update_by=@update_by,update_name=@update_name,update_time=@update_time,orders_source=@orders_source,before_orderId=@before_orderId where maintain_id=@maintain_id";
                }
                obj.Param = dicParam;
                listSql.Add(obj);
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 维修结算单结算信息保存
        /// <summary>
        /// 维修结算单结算信息保存
        /// </summary>
        /// <param name="strOrderId">跟维修结算单基本信息关联的Id</param>
        private void SaveBalanceInfo(List<SQLObj> listSql, string strOrderId)
        {
            try
            {
                SQLObj obj = new SQLObj();
                obj.cmdType = CommandType.Text;
                Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                #region 基本信息
                dicParam.Add("man_hour_sum_money", new ParamObj("man_hour_sum_money", !string.IsNullOrEmpty(txtHSumMoney.Caption.Trim()) ? txtHSumMoney.Caption.Trim() : null, SysDbType.Decimal, 15));//工时货款
                dicParam.Add("man_hour_tax_rate", new ParamObj("man_hour_tax_rate", !string.IsNullOrEmpty(txtHTaxRate.Caption.Trim()) ? txtHTaxRate.Caption.Trim() : null, SysDbType.Decimal, 15));//工时税率
                dicParam.Add("man_hour_tax_cost", new ParamObj("man_hour_tax_cost", !string.IsNullOrEmpty(txtHTaxCost.Caption.Trim()) ? txtHTaxCost.Caption.Trim() : null, SysDbType.Decimal, 15));//工时税额
                dicParam.Add("man_hour_sum", new ParamObj("man_hour_sum", !string.IsNullOrEmpty(txtHSum.Caption.Trim()) ? txtHSum.Caption.Trim() : null, SysDbType.Decimal, 15));//工时税价合计
                dicParam.Add("fitting_sum_money", new ParamObj("fitting_sum_money", !string.IsNullOrEmpty(txtFSumMoney.Caption.Trim()) ? txtFSumMoney.Caption.Trim() : null, SysDbType.Decimal, 15));//配件货款
                dicParam.Add("fitting_tax_rate", new ParamObj("fitting_tax_rate", !string.IsNullOrEmpty(txtFTaxRate.Caption.Trim()) ? txtFTaxRate.Caption.Trim() : null, SysDbType.Decimal, 15));//配件税率
                dicParam.Add("fitting_tax_cost", new ParamObj("fitting_tax_cost", !string.IsNullOrEmpty(txtFTaxCost.Caption.Trim()) ? txtFTaxCost.Caption.Trim() : null, SysDbType.Decimal, 15));//配件税额
                dicParam.Add("fitting_sum", new ParamObj("fitting_sum", !string.IsNullOrEmpty(txtFSum.Caption.Trim()) ? txtFSum.Caption.Trim() : null, SysDbType.Decimal, 15));//配件价税合计
                dicParam.Add("other_item_sum_money", new ParamObj("other_item_sum_money", !string.IsNullOrEmpty(txtOSumMoney.Caption.Trim()) ? txtOSumMoney.Caption.Trim() : null, SysDbType.Decimal, 15));//其他项目费用
                dicParam.Add("other_item_tax_rate", new ParamObj("other_item_tax_rate", !string.IsNullOrEmpty(txtOTaxRate.Caption.Trim()) ? txtOTaxRate.Caption.Trim() : null, SysDbType.Decimal, 15));//其他项目税率
                dicParam.Add("other_item_tax_cost", new ParamObj("other_item_tax_cost", !string.IsNullOrEmpty(txtOTaxCost.Caption.Trim()) ? txtOTaxCost.Caption.Trim() : null, SysDbType.Decimal, 15));//其他项目税额
                dicParam.Add("other_item_sum", new ParamObj("other_item_sum", !string.IsNullOrEmpty(txtOSum.Caption.Trim()) ? txtOSum.Caption.Trim() : null, SysDbType.Decimal, 15));//其他项目税价合计
                dicParam.Add("privilege_cost", new ParamObj("privilege_cost", !string.IsNullOrEmpty(txtPrivilegeCost.Caption.Trim()) ? txtPrivilegeCost.Caption.Trim() : null, SysDbType.Decimal, 15));//优惠费用
                dicParam.Add("should_sum", new ParamObj("should_sum", !string.IsNullOrEmpty(txtShouldSum.Caption.Trim()) ? txtShouldSum.Caption.Trim() : null, SysDbType.Decimal, 15));//应收总额
                dicParam.Add("received_sum", new ParamObj("received_sum", !string.IsNullOrEmpty(txtReceivedSum.Caption.Trim()) ? txtReceivedSum.Caption.Trim() : null, SysDbType.Decimal, 15));//实收总额
                dicParam.Add("debt_cost", new ParamObj("debt_cost", !string.IsNullOrEmpty(txtDebtCost.Caption.Trim()) ? txtDebtCost.Caption.Trim() : null, SysDbType.Decimal, 15));//本子欠款金额
                dicParam.Add("make_invoice_type", new ParamObj("make_invoice_type", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobMInvoiceType.SelectedValue)) ? cobMInvoiceType.SelectedValue.ToString() : null, SysDbType.VarChar, 40));//发票类型
                dicParam.Add("payment_terms", new ParamObj("payment_terms", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobMInvoiceType.SelectedValue)) ? cobBalanceType.SelectedValue.ToString() : null, SysDbType.VarChar, 40));//结算方式
                dicParam.Add("settlement_account", new ParamObj("settlement_account", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobSetAccount.SelectedValue)) ? cobSetAccount.SelectedValue.ToString() : null, SysDbType.VarChar, 40));//结算账户
                dicParam.Add("settle_company", new ParamObj("settle_company", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtBalanceCompany.Tag)) ? txtBalanceCompany.Tag.ToString() : null, SysDbType.VarChar, 50));//结算单位            
                dicParam.Add("maintain_id", new ParamObj("maintain_id", strOrderId, SysDbType.VarChar, 40));//关联维修结算单主数据Id
                #endregion
                if (wStatus == WindowStatus.Add || wStatus == WindowStatus.Copy)
                {
                    dicParam.Add("settlement_id", new ParamObj("settlement_id", Guid.NewGuid().ToString(), SysDbType.VarChar, 40));//Id
                    dicParam.Add("create_by", new ParamObj("create_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//创建人id（制单人） 
                    dicParam.Add("create_name", new ParamObj("create_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//创建人
                    dicParam.Add("create_time", new ParamObj("create_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));
                    obj.sqlString = @"insert into tb_maintain_settlement_info (man_hour_sum_money,man_hour_tax_rate,man_hour_tax_cost,man_hour_sum,fitting_sum_money,fitting_tax_rate
                ,fitting_tax_cost,fitting_sum,other_item_sum_money,other_item_tax_rate,other_item_tax_cost,other_item_sum,privilege_cost,should_sum,received_sum
                ,debt_cost,make_invoice_type,payment_terms,settlement_account,settle_company,maintain_id,settlement_id,create_by,create_name,create_time)
                values (@man_hour_sum_money,@man_hour_tax_rate,@man_hour_tax_cost,@man_hour_sum,@fitting_sum_money,@fitting_tax_rate
                ,@fitting_tax_cost,@fitting_sum,@other_item_sum_money,@other_item_tax_rate,@other_item_tax_cost,@other_item_sum,@privilege_cost,@should_sum,@received_sum
                ,@debt_cost,@make_invoice_type,@payment_terms,@settlement_account,@settle_company,@maintain_id,@settlement_id,@create_by,@create_name,@create_time);";
                }
                else if (wStatus == WindowStatus.Edit)
                {
                    dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
                    dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
                    dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                    obj.sqlString = @"update tb_maintain_settlement_info set man_hour_sum_money=@man_hour_sum_money,man_hour_tax_rate=@man_hour_tax_rate,man_hour_tax_cost=@man_hour_tax_cost,man_hour_sum=@man_hour_sum,fitting_sum_money=@fitting_sum_money,fitting_tax_rate=@fitting_tax_rate
                ,fitting_tax_cost=@fitting_tax_cost,fitting_sum=@fitting_sum,other_item_sum_money=@other_item_sum_money,other_item_tax_rate=@other_item_tax_rate,other_item_tax_cost=@other_item_tax_cost,other_item_sum=@other_item_sum,privilege_cost=@privilege_cost,should_sum=@should_sum,received_sum=@received_sum
                ,debt_cost=@debt_cost,make_invoice_type=@make_invoice_type,payment_terms=@payment_terms,settlement_account=@settlement_account,settle_company=@settle_company,update_by=@update_by,update_name=@update_name,update_time=@update_time where maintain_id=@maintain_id";
                }
                obj.Param = dicParam;
                listSql.Add(obj);
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 维修项目信息保存
        private void SaveProjectData(List<SQLObj> listSql, string partID)
        {
            try
            {
                foreach (DataGridViewRow dgvr in dgvproject.Rows)
                {
                    string strPname = CommonCtrl.IsNullToString(dgvr.Cells["item_name"].Value);
                    if (strPname.Length > 0)
                    {
                        SQLObj obj = new SQLObj();
                        obj.cmdType = CommandType.Text;
                        Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                        dicParam.Add("maintain_id", new ParamObj("maintain_id", partID, SysDbType.VarChar, 40));
                        dicParam.Add("item_no", new ParamObj("item_no", dgvr.Cells["item_no"].Value, SysDbType.VarChar, 40));
                        dicParam.Add("item_type", new ParamObj("item_type", dgvr.Cells["item_type"].Value, SysDbType.VarChar, 40));
                        dicParam.Add("item_name", new ParamObj("item_name", dgvr.Cells["item_name"].Value, SysDbType.VarChar, 40));
                        dicParam.Add("man_hour_type", new ParamObj("man_hour_type", dgvr.Cells["man_hour_type"].Value, SysDbType.VarChar, 40));
                        string strHourQuantity = CommonCtrl.IsNullToString(dgvr.Cells["man_hour_quantity"].Value);
                        if (!string.IsNullOrEmpty(strHourQuantity))
                        {
                            dicParam.Add("man_hour_quantity", new ParamObj("man_hour_quantity", strHourQuantity, SysDbType.Decimal, 15));
                        }
                        else
                        {
                            dicParam.Add("man_hour_quantity", new ParamObj("man_hour_quantity", null, SysDbType.Decimal, 15));
                        }
                        dicParam.Add("man_hour_norm_unitprice", new ParamObj("man_hour_norm_unitprice", dgvr.Cells["man_hour_norm_unitprice"].Value, SysDbType.Decimal, 15));
                        dicParam.Add("member_discount", new ParamObj("member_discount", dgvr.Cells["member_discount"].Value, SysDbType.Decimal, 5));
                        dicParam.Add("member_price", new ParamObj("member_price", dgvr.Cells["member_price"].Value, SysDbType.Decimal, 15));
                        dicParam.Add("member_sum_money", new ParamObj("member_sum_money", dgvr.Cells["member_sum_money"].Value, SysDbType.Decimal, 15));
                        dicParam.Add("sum_money_goods", new ParamObj("sum_money_goods", dgvr.Cells["sum_money_goods"].Value, SysDbType.Decimal, 15));
                        string strIsThree = CommonCtrl.IsNullToString(dgvr.Cells["three_warranty"].Value);
                        if (!string.IsNullOrEmpty(strIsThree))
                        {
                            dicParam.Add("three_warranty", new ParamObj("three_warranty", strIsThree == "是" ? "1" : "0", SysDbType.VarChar, 2));
                        }
                        else
                        {
                            dicParam.Add("three_warranty", new ParamObj("three_warranty", null, SysDbType.VarChar, 2));
                        }
                        dicParam.Add("remarks", new ParamObj("remarks", dgvr.Cells["remarks"].Value, SysDbType.VarChar, 200));
                        dicParam.Add("enable_flag", new ParamObj("enable_flag", "1", SysDbType.VarChar, 1));
                        dicParam.Add("whours_id", new ParamObj("whours_id", dgvr.Cells["whours_id"].Value, SysDbType.VarChar, 40));
                        string strPID = CommonCtrl.IsNullToString(dgvr.Cells["item_id"].Value);
                        if (strPID.Length == 0)
                        {
                            opName = "新增接待单维修项目";
                            strPID = Guid.NewGuid().ToString();
                            dicParam.Add("item_id", new ParamObj("item_id", strPID, SysDbType.VarChar, 40));
                            obj.sqlString = "insert into [tb_maintain_item] (item_id,maintain_id,item_no,item_type,item_name,man_hour_type,man_hour_quantity,man_hour_norm_unitprice,member_discount,member_price,member_sum_money,sum_money_goods,three_warranty,remarks,enable_flag,whours_id) ";
                            obj.sqlString += " values (@item_id,@maintain_id,@item_no,@item_type,@item_name,@man_hour_type,@man_hour_quantity,@man_hour_norm_unitprice,@member_discount,@member_price,@member_sum_money,@sum_money_goods,@three_warranty,@remarks,@enable_flag,@whours_id);";
                        }
                        else
                        {
                            dicParam.Add("item_id", new ParamObj("item_id", dgvr.Cells["item_id"].Value, SysDbType.VarChar, 40));
                            opName = "更新接待单维修项目";
                            obj.sqlString = "update tb_maintain_item set item_no=@item_no,item_type=@item_type,item_name=@item_name,man_hour_type=@man_hour_type,man_hour_quantity=@man_hour_quantity,man_hour_norm_unitprice=@man_hour_norm_unitprice,member_discount=@member_discount,member_price=@member_price,";
                            obj.sqlString += " member_sum_money=@member_sum_money,sum_money_goods=@sum_money_goods,three_warranty=@three_warranty,remarks=@remarks,enable_flag=@enable_flag,whours_id=@whours_id where item_id=@item_id";
                        }
                        obj.Param = dicParam;
                        listSql.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 维修用料信息保存
        private void SaveMaterialsData(List<SQLObj> listSql, string partID)
        {
            try
            {
                foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
                {
                    string strPname = CommonCtrl.IsNullToString(dgvr.Cells["parts_name"].Value);
                    string strPCmoney = CommonCtrl.IsNullToString(dgvr.Cells["sum_money"].Value);
                    if (strPname.Length > 0 && strPCmoney.Length > 0)
                    {
                        SQLObj obj = new SQLObj();
                        obj.cmdType = CommandType.Text;
                        Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                        dicParam.Add("maintain_id", new ParamObj("maintain_id", partID, SysDbType.VarChar, 40));
                        dicParam.Add("parts_code", new ParamObj("parts_code", dgvr.Cells["parts_code"].Value, SysDbType.VarChar, 40));
                        dicParam.Add("parts_name", new ParamObj("parts_name", dgvr.Cells["parts_name"].Value, SysDbType.VarChar, 40));
                        dicParam.Add("norms", new ParamObj("norms", dgvr.Cells["norms"].Value, SysDbType.VarChar, 40));
                        dicParam.Add("unit", new ParamObj("unit", dgvr.Cells["unit"].Value, SysDbType.VarChar, 20));
                        string strIsimport = CommonCtrl.IsNullToString(dgvr.Cells["whether_imported"].Value);
                        if (!string.IsNullOrEmpty(strIsimport))
                        {
                            dicParam.Add("whether_imported", new ParamObj("whether_imported", strIsimport == "是" ? "1" : "0", SysDbType.VarChar, 1));
                        }
                        else
                        {
                            dicParam.Add("whether_imported", new ParamObj("whether_imported", null, SysDbType.VarChar, 1));
                        }
                        dicParam.Add("quantity", new ParamObj("quantity", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["quantity"].Value)) ? dgvr.Cells["quantity"].Value : null, SysDbType.Decimal, 15));
                        dicParam.Add("unit_price", new ParamObj("unit_price", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["unit_price"].Value)) ? dgvr.Cells["unit_price"].Value : null, SysDbType.Decimal, 15));
                        dicParam.Add("member_discount", new ParamObj("member_discount", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["Mmember_discount"].Value)) ? dgvr.Cells["Mmember_discount"].Value : null, SysDbType.Decimal, 15));
                        dicParam.Add("member_price", new ParamObj("member_price", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["Mmember_price"].Value)) ? dgvr.Cells["Mmember_price"].Value : null, SysDbType.Decimal, 15));
                        dicParam.Add("sum_money", new ParamObj("sum_money", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["sum_money"].Value)) ? dgvr.Cells["sum_money"].Value : null, SysDbType.Decimal, 15));
                        dicParam.Add("drawn_no", new ParamObj("drawn_no", dgvr.Cells["drawn_no"].Value, SysDbType.VarChar, 40));
                        dicParam.Add("vehicle_brand", new ParamObj("vehicle_brand", dgvr.Cells["vehicle_brand"].Value, SysDbType.VarChar, 200));
                        string strIsThree = CommonCtrl.IsNullToString(dgvr.Cells["Mthree_warranty"].Value);
                        if (!string.IsNullOrEmpty(strIsThree))
                        {
                            dicParam.Add("three_warranty", new ParamObj("three_warranty", strIsThree == "是" ? "1" : "0", SysDbType.VarChar, 2));
                        }
                        else
                        {
                            dicParam.Add("three_warranty", new ParamObj("three_warranty", null, SysDbType.VarChar, 2));
                        }
                        dicParam.Add("remarks", new ParamObj("remarks", dgvr.Cells["Mremarks"].Value, SysDbType.VarChar, 200));
                        dicParam.Add("enable_flag", new ParamObj("enable_flag", "1", SysDbType.VarChar, 1));
                        dicParam.Add("parts_id", new ParamObj("parts_id", dgvr.Cells["parts_id"].Value, SysDbType.VarChar,40));                        
                        string strPID = CommonCtrl.IsNullToString(dgvr.Cells["material_id"].Value);
                        if (strPID.Length == 0)
                        {
                            opName = "新增接待单维修用料";
                            strPID = Guid.NewGuid().ToString();
                            dicParam.Add("material_id", new ParamObj("material_id", strPID, SysDbType.VarChar, 40));
                            obj.sqlString = "insert into [tb_maintain_material_detail] (material_id,parts_code,parts_name,norms,unit,whether_imported,quantity,unit_price,sum_money,drawn_no,vehicle_brand,three_warranty,remarks,enable_flag,maintain_id,parts_id) ";
                            obj.sqlString += " values (@material_id,@parts_code,@parts_name,@norms,@unit,@whether_imported,@quantity,@unit_price,@sum_money,@drawn_no,@vehicle_brand,@three_warranty,@remarks,@enable_flag,@maintain_id,@parts_id);";
                        }
                        else
                        {
                            dicParam.Add("material_id", new ParamObj("material_id", dgvr.Cells["material_id"].Value, SysDbType.VarChar, 40));
                            opName = "更新接待单维修用料";
                            obj.sqlString = "update tb_maintain_material_detail set parts_code=@parts_code,parts_name=@parts_name,norms=@norms,unit=@unit,whether_imported=@whether_imported,quantity=@quantity,unit_price=@unit_price,sum_money=@sum_money,";
                            obj.sqlString += "drawn_no=@drawn_no,vehicle_brand=@vehicle_brand, three_warranty=@three_warranty,remarks=@remarks,enable_flag=@enable_flag,parts_id=@parts_id where material_id=@material_id";
                        }
                        obj.Param = dicParam;
                        listSql.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 其他收费项目信息保存
        //其他收费项目信息保存sql语句
        private void SaveOtherData(List<SQLObj> listSql, string partID)
        {
            try
            {
                foreach (DataGridViewRow dgvr in dgvOther.Rows)
                {
                    string strCotype = CommonCtrl.IsNullToString(dgvr.Cells["cost_types"].Value);
                    if (strCotype.Length > 0)
                    {
                        SQLObj obj = new SQLObj();
                        obj.cmdType = CommandType.Text;
                        Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                        dicParam.Add("maintain_id", new ParamObj("maintain_id", partID, SysDbType.VarChar, 40));
                        dicParam.Add("cost_types", new ParamObj("cost_types", dgvr.Cells["cost_types"].Value, SysDbType.VarChar, 40));
                        if (CommonCtrl.IsNullToString(dgvr.Cells["Osum_money"].Value).Length > 0)
                        {
                            dicParam.Add("sum_money", new ParamObj("sum_money", dgvr.Cells["Osum_money"].Value, SysDbType.Decimal, 18));
                        }
                        else
                        {
                            dicParam.Add("sum_money", new ParamObj("sum_money", null, SysDbType.Decimal, 18));
                        }
                        dicParam.Add("remarks", new ParamObj("remarks", dgvr.Cells["Oremarks"].Value, SysDbType.VarChar, 200));
                        dicParam.Add("enable_flag", new ParamObj("enable_flag", "1", SysDbType.Char, 10));
                        string strTollID = CommonCtrl.IsNullToString(dgvr.Cells["toll_id"].Value);
                        if (strTollID.Length == 0)
                        {
                            opName = "新增结算单其他项目收费";
                            strTollID = Guid.NewGuid().ToString();
                            dicParam.Add("toll_id", new ParamObj("toll_id", strTollID, SysDbType.VarChar, 40));
                            obj.sqlString = "insert into [tb_maintain_other_toll] (toll_id,cost_types,sum_money,remarks,maintain_id,enable_flag) values (@toll_id,@cost_types,@sum_money,@remarks,@maintain_id,@enable_flag);";
                        }
                        else
                        {
                            dicParam.Add("toll_id", new ParamObj("toll_id", dgvr.Cells["toll_id"].Value, SysDbType.VarChar, 40));
                            opName = "更新结算单其他项目收费";
                            obj.sqlString = "update tb_maintain_other_toll set cost_types=@cost_types,sum_money=@sum_money,remarks=@remarks where toll_id=@toll_id";
                        }
                        obj.Param = dicParam;
                        listSql.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 初始化车辆品牌、颜色、维修付费方式、维修类别信息
        /// <summary>
        /// 初始化车型、车辆品牌、颜色、维修付费方式、维修类别信息
        /// </summary>
        private void initializeData()
        {
            try
            {
                CommonCtrl.BindComboBoxByDictionarr(cobCarBrand, "sys_vehicle_brand", true);//绑定车型品牌
                CommonCtrl.BindComboBoxByDictionarr(cobColor, "sys_vehicle_color", true);//绑定颜色
                CommonCtrl.BindComboBoxByDictionarr(cobPayType, "sys_repair_pay_methods", true);//绑定维修付费方式
                CommonCtrl.BindComboBoxByDictionarr(cobRepType, "sys_repair_category", true);//绑定维修类别   
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 生成维修单号
        /// <summary>
        /// 生成预约单号&创建人姓名
        /// </summary>
        private void GetRepairNo()
        {
            labReserveNoS.Text = CommonUtility.GetNewNo(SYSModel.DataSources.EnumProjectType.Repair);
            labCreatePersonS.Text = HXCPcClient.GlobalStaticObj.UserName;
        }
        #endregion

        #region 设置底部DataGridView的Anchor属性
        /// <summary>
        /// 设置底部DataGridView的Anchor属性
        /// </summary>
        private void SetDgvAnchor()
        {
            //dgvAccess.Dock = DockStyle.Fill;
            //dgvAccess.Columns["ACheck"].HeaderText = "选择";           
            //dgvAccess.Rows.Add(4);

            #region 其他收费项目
            dgvOther.Dock = DockStyle.Fill;
            //dgvOther.Columns["OCheck"].HeaderText = "选择";
            dgvOther.ReadOnly = false;
            dgvOther.Rows.Add(3);
            dgvOther.AllowUserToAddRows = true;
            dgvOther.Columns["Osum_money"].ReadOnly = true;
            dgvOther.Columns["Oremarks"].ReadOnly = true;
            ControlsConfig.NumberLimitdgv(dgvOther, new List<string>() { "Osum_money" });
            DataTable dot = CommonCtrl.GetDictByCode("sys_other_expense_type", true);
            cost_types.DataSource = dot;
            cost_types.ValueMember = "dic_id";
            cost_types.DisplayMember = "dic_name";
            #endregion

            #region 维修项目信息设置
            dgvproject.Dock = DockStyle.Fill;
            //dgvproject.Columns["colCheck"].HeaderText = "选择";
            dgvproject.ReadOnly = false;
            dgvproject.Rows.Add(3);
            dgvproject.AllowUserToAddRows = true;
            dgvproject.Columns["item_no"].ReadOnly = true;
            dgvproject.Columns["item_type"].ReadOnly = true;
            dgvproject.Columns["item_name"].ReadOnly = true;
            dgvproject.Columns["man_hour_norm_unitprice"].ReadOnly = true;
            dgvproject.Columns["sum_money_goods"].ReadOnly = true;
            dgvproject.Columns["member_discount"].ReadOnly = true;
            dgvproject.Columns["member_price"].ReadOnly = true;
            dgvproject.Columns["member_sum_money"].ReadOnly = true;
            dgvproject.Columns["man_hour_type"].ReadOnly = true;
            //dgvproject.Columns["man_hour_quantity"].ReadOnly = true;
            ControlsConfig.NumberLimitdgv(dgvproject, new List<string>() { "man_hour_quantity", "man_hour_norm_unitprice" });
            #endregion

            #region 维修用料设置
            dgvMaterials.Dock = DockStyle.Fill;
            //dgvMaterials.Columns["MCheck"].HeaderText = "选择";
            dgvMaterials.ReadOnly = false;
            dgvMaterials.Rows.Add(3);
            dgvMaterials.AllowUserToAddRows = true;
            dgvMaterials.Columns["parts_code"].ReadOnly = true;
            dgvMaterials.Columns["parts_name"].ReadOnly = true;
            dgvMaterials.Columns["norms"].ReadOnly = true;
            dgvMaterials.Columns["unit"].ReadOnly = true;
            dgvMaterials.Columns["whether_imported"].ReadOnly = true;
            dgvMaterials.Columns["sum_money"].ReadOnly = true;
            dgvMaterials.Columns["drawn_no"].ReadOnly = true;
            dgvMaterials.Columns["vehicle_brand"].ReadOnly = true;
            dgvMaterials.Columns["Mmember_discount"].ReadOnly = true;
            dgvMaterials.Columns["Mmember_price"].ReadOnly = true;
            ControlsConfig.NumberLimitdgv(dgvMaterials, new List<string>() { "quantity", "unit_price" });
            #endregion
        }
        #endregion


        #region 车牌号选择器事件
        /// <summary>
        /// 车牌号选择器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCarNO_ChooserClick(object sender, EventArgs e)
        {
            try
            {
                frmVehicleGrade frmVehicle = new frmVehicleGrade();
                DialogResult result = frmVehicle.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtCarNO.Text = frmVehicle.strLicensePlate;
                    txtVIN.Caption = frmVehicle.strVIN;
                    txtEngineNo.Caption = frmVehicle.strEngineNum;
                    txtCarType.Text = GetVmodel(frmVehicle.strModel);
                    txtCarType.Tag = frmVehicle.strModel;
                    cobCarBrand.SelectedValue = frmVehicle.strBrand;
                    cobColor.SelectedValue = frmVehicle.strColor;
                    txtCustomNO.Text = frmVehicle.strCustCode;
                    txtCustomNO.Tag = frmVehicle.strCustId;
                    txtCustomName.Caption = frmVehicle.strCustName;
                    txtContact.Caption = frmVehicle.strContactName;
                    txtContactPhone.Caption = frmVehicle.strContactPhone;
                    txtBalanceCompany.Text = frmVehicle.strCustName;
                    txtBalanceCompany.Tag = frmVehicle.strCustId;
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 车型选择器事件
        /// <summary>
        /// 车型选择器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCarType_ChooserClick(object sender, EventArgs e)
        {
            try
            {
                frmVehicleModels frmModels = new frmVehicleModels();
                DialogResult result = frmModels.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtCarType.Text = frmModels.VMName;
                    txtCarType.Tag = frmModels.VMID;
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 客户编码选择器事件
        /// <summary>
        /// 客户编码选择器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCustomNO_ChooserClick(object sender, EventArgs e)
        {
            try
            {
                frmCustomerInfo frmCInfo = new frmCustomerInfo();
                DialogResult result = frmCInfo.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtCustomNO.Text = frmCInfo.strCustomerNo;
                    txtCustomNO.Tag = frmCInfo.strCustomerId;
                    txtCustomName.Caption = frmCInfo.strCustomerName;
                    txtContact.Caption = frmCInfo.strLegalPerson;
                    txtBalanceCompany.Text = frmCInfo.strCustomerName;
                    txtBalanceCompany.Tag = frmCInfo.strCustomerId;

                    #region 会员信息
                    DataTable dct = DBHelper.GetTable("获取会员信息", "tb_customer", "member_number,member_class,accessories_discount,workhours_discount", " is_member='1' and cust_id='" + txtCustomNO.Tag + "'", "", "");
                    if (dct.Rows.Count > 0)
                    {
                        DataRow dcr = dct.Rows[0];
                        labMemberNoS.Text = CommonCtrl.IsNullToString(dcr["member_number"]);
                        labMemberGradeS.Text = GetDicName(CommonCtrl.IsNullToString(dcr["member_class"]));
                        labMemberPZkS.Text = CommonCtrl.IsNullToString(dcr["workhours_discount"]);
                        labMemberLZkS.Text = CommonCtrl.IsNullToString(dcr["accessories_discount"]);
                    }
                    else
                    {
                        labMemberNoS.Text = string.Empty;
                        labMemberGradeS.Text = string.Empty;
                        labMemberPZkS.Text = string.Empty;
                        labMemberLZkS.Text = string.Empty;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 根据码表ID获取其对应的名称
        /// <summary>
        /// 根据码表ID获取其对应的名称
        /// </summary>
        /// <param name="strId">码表Id值</param>
        private string GetDicName(string strId)
        {
            return DBHelper.GetSingleValue("获取码表值", "sys_dictionaries", "dic_name", "dic_id='" + strId + "'", "");
        }
        #endregion

        #region 窗体Load事件
        private void UCRepairBalanceAddOrEdit_Load(object sender, EventArgs e)
        {
            dtpComplateTime.Value = DateTime.Now;
            dtpBalanceTime.Value = DateTime.Now;
            dtpSuggestTime.Value = DateTime.Now;
            ControlsConfig.TextToDecimal(txtHTaxRate,false);
            ControlsConfig.TextToDecimal(txtFTaxRate,false);
            ControlsConfig.TextToDecimal(txtOTaxRate,false);
            ControlsConfig.TextToDecimal(txtMil,false);
            ControlsConfig.TextToDecimal(txtMl,false);
            ControlsConfig.TextToDecimal(txtSuggestMil,false);
            ControlsConfig.TextToDecimal(txtPrivilegeCost);
            ControlsConfig.TextToDecimal(txtReceivedSum);
            //base.SetBtnStatus(wStatus);
            if (wStatus == WindowStatus.Edit || wStatus == WindowStatus.Copy)
            {
                BindData();
            }
            else if (wStatus == WindowStatus.Add)
            {
                labReserveNoS.Visible = false;
            }
        }
        #endregion

        #region 根据维修单ID获取信息，复制和编辑用
        /// <summary>
        /// 根据维修单ID获取信息，复制和编辑用
        /// </summary>
        private void BindData()
        {
            try
            {
                #region 基础信息
                string strWhere = string.Format("a.maintain_id='{0}'", strId);
                strValue = strId;
                DataTable dt = DBHelper.GetTable("查询维修单", "tb_maintain_settlement_info a left join tb_maintain_info b on a.maintain_id=b.maintain_id ", "*", strWhere, "", "");
                if (dt == null || dt.Rows.Count == 0)
                {
                    return;
                }
                DataRow dr = dt.Rows[0];
                #region 车辆信息

                string strCtime = CommonCtrl.IsNullToString(dr["create_time"]);
                if (!string.IsNullOrEmpty(strCtime))
                {
                    long Rticks = Convert.ToInt64(strCtime);
                    dtpBalanceTime.Value = Common.UtcLongToLocalDateTime(Rticks);//结算时间
                }
                string strCwTime = CommonCtrl.IsNullToString(dr["complete_work_time"]);
                if (!string.IsNullOrEmpty(strCwTime))
                {
                    long ComplateTime = Convert.ToInt64(strCwTime);
                    dtpComplateTime.Value = Common.UtcLongToLocalDateTime(ComplateTime);//完工时间
                }
                if (wStatus == WindowStatus.Edit)
                {
                    txtCarNO.Text = CommonCtrl.IsNullToString(dr["vehicle_no"]);//车牌号
                    txtVIN.Caption = CommonCtrl.IsNullToString(dr["vehicle_vin"]);//VIN
                    txtEngineNo.Caption = CommonCtrl.IsNullToString(dr["engine_no"]);//发动机号
                    txtCarType.Text = GetVmodel(CommonCtrl.IsNullToString(dr["vehicle_model"]));//车型          
                    cobCarBrand.SelectedValue = CommonCtrl.IsNullToString(dr["vehicle_brand"]);//车辆品牌
                    txtDriver.Caption = CommonCtrl.IsNullToString(dr["driver_name"]);//司机
                    txtDriverPhone.Caption = CommonCtrl.IsNullToString(dr["driver_mobile"]);//司机手机
                    cobColor.SelectedValue = CommonCtrl.IsNullToString(dr["vehicle_color"]);//颜色 
                }
                cobPayType.SelectedValue = CommonCtrl.IsNullToString(dr["maintain_payment"]);//维修付费方式
                cobRepType.SelectedValue = CommonCtrl.IsNullToString(dr["maintain_type"]);//维修类别           
                txtMl.Caption = CommonCtrl.IsNullToString(dr["oil_into_factory"]);//进场油量
                txtMil.Caption = CommonCtrl.IsNullToString(dr["travel_mileage"]);//行驶里程
                txtSuggestMil.Caption = CommonCtrl.IsNullToString(dr["maintain_mileage"]);//建议保养里程
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["completion_time"])))
                {
                    long ticks = Convert.ToInt64(CommonCtrl.IsNullToString(dr["completion_time"]));
                    dtpSuggestTime.Value = Common.UtcLongToLocalDateTime(ticks); //预计完工时间      
                }
                else
                {
                    dtpSuggestTime.Value = DateTime.Now;
                }

                txtRemark.Caption = CommonCtrl.IsNullToString(dr["remark"]);//备注
                txtPreferReason.Caption = CommonCtrl.IsNullToString(dr["favorable_reason"]);//优惠原因
                #endregion

                #region 客户信息
                txtCustomNO.Text = CommonCtrl.IsNullToString(dr["customer_code"]);//客户编码
                txtCustomName.Caption = CommonCtrl.IsNullToString(dr["customer_name"]);//客户名称  
                txtCustomNO.Tag = CommonCtrl.IsNullToString(dr["customer_id"]);//客户关联id           
                DataTable dct = DBHelper.GetTable("获取会员信息", "tb_customer", "member_number,member_class,accessories_discount,workhours_discount", " is_member='1' and cust_id='" + txtCustomNO.Tag + "'", "", "");
                if (dct.Rows.Count > 0)
                {
                    DataRow dcr = dct.Rows[0];
                    labMemberNoS.Text = CommonCtrl.IsNullToString(dcr["member_number"]);
                    labMemberGradeS.Text = GetDicName(CommonCtrl.IsNullToString(dcr["member_class"]));
                    labMemberPZkS.Text = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dcr["workhours_discount"])) ? (Convert.ToInt32(CommonCtrl.IsNullToString(dcr["workhours_discount"])) * 10).ToString() + "%" : "";
                    labMemberLZkS.Text = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dcr["accessories_discount"])) ? (Convert.ToInt32(CommonCtrl.IsNullToString(dcr["accessories_discount"])) * 10).ToString() + "%" : "";
                }
                else
                {
                    labMemberNoS.Text = string.Empty;
                    labMemberGradeS.Text = string.Empty;
                    labMemberPZkS.Text = string.Empty;
                    labMemberLZkS.Text = string.Empty;
                }
                txtContact.Caption = CommonCtrl.IsNullToString(dr["linkman"]);//联系人
                txtContactPhone.Caption = CommonCtrl.IsNullToString(dr["link_man_mobile"]);//联系人手机
                #endregion

                #region 结算信息
                txtHSumMoney.Caption = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["man_hour_sum_money"]))? Math.Round((Convert.ToDecimal(CommonCtrl.IsNullToString(dr["man_hour_sum_money"]))),2).ToString("0.00"):"0.00";//工时货款
                txtHTaxRate.Caption = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["man_hour_tax_rate"])) ? CommonCtrl.IsNullToString(dr["man_hour_tax_rate"]) : "0";//工时税率
                txtHTaxCost.Caption = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["man_hour_tax_cost"]))? Math.Round((Convert.ToDecimal(CommonCtrl.IsNullToString(dr["man_hour_tax_cost"]))),2).ToString("0.00"):"0.00";//工时税额
                txtHSum.Caption = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["man_hour_sum"]))? Math.Round((Convert.ToDecimal(CommonCtrl.IsNullToString(dr["man_hour_sum"]))),2).ToString("0.00"):"0";//工时价税合计
                txtFSumMoney.Caption = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["fitting_sum_money"]))? Math.Round((Convert.ToDecimal(CommonCtrl.IsNullToString(dr["fitting_sum_money"]))),2).ToString("0.00"):"0.00";//配件货款
                txtFTaxRate.Caption = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["fitting_tax_rate"]))? CommonCtrl.IsNullToString(dr["fitting_tax_rate"]):"0";//配件税率
                txtFTaxCost.Caption = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["fitting_tax_cost"]))? Math.Round((Convert.ToDecimal(CommonCtrl.IsNullToString(dr["fitting_tax_cost"]))),2).ToString("0.00"):"0.00";//配件税额
                txtFSum.Caption = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["fitting_sum"]))? Math.Round((Convert.ToDecimal(CommonCtrl.IsNullToString(dr["fitting_sum"]))),2).ToString("0.00"):"0.00";//配件价税合计
                txtOSumMoney.Caption = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["other_item_sum_money"]))? Math.Round((Convert.ToDecimal(CommonCtrl.IsNullToString(dr["other_item_sum_money"]))),2).ToString("0.00"):"0.00";//其他项目费用
                txtOTaxRate.Caption = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["other_item_tax_rate"]))? CommonCtrl.IsNullToString(dr["other_item_tax_rate"]):"0";//其他项目税率
                txtOTaxCost.Caption = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["other_item_tax_cost"]))? Math.Round((Convert.ToDecimal(CommonCtrl.IsNullToString(dr["other_item_tax_cost"]))),2).ToString("0.00"):"0.00";//其他项目税额
                txtOSum.Caption = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["other_item_sum"]))? Math.Round((Convert.ToDecimal(CommonCtrl.IsNullToString(dr["other_item_sum"]))),2).ToString("0.00"):"0.00";//其他项目价税合计
                txtPrivilegeCost.Caption = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["privilege_cost"]))? Math.Round((Convert.ToDecimal(CommonCtrl.IsNullToString(dr["privilege_cost"]))),2).ToString("0.00"):"0.00";//优惠费用
                txtShouldSum.Caption = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["should_sum"]))? Math.Round((Convert.ToDecimal(CommonCtrl.IsNullToString(dr["should_sum"]))),2).ToString("0.00"):"0.00";//应收总额
                strShouldSum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["should_sum"]))? Math.Round((Convert.ToDecimal(CommonCtrl.IsNullToString(dr["should_sum"]))),2).ToString("0.00"):"0.00";//应收总额
                txtReceivedSum.Caption = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["received_sum"]))? Math.Round((Convert.ToDecimal(CommonCtrl.IsNullToString(dr["received_sum"]))),2).ToString("0.00"):"0.00";//实收总额
                txtDebtCost.Caption = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["debt_cost"]))? Math.Round((Convert.ToDecimal(CommonCtrl.IsNullToString(dr["debt_cost"]))),2).ToString("0.00"):"0.00";//本次欠款金额
                cobMInvoiceType.SelectedValue = CommonCtrl.IsNullToString(dr["make_invoice_type"]);//开票类型
                cobBalanceType.SelectedValue = CommonCtrl.IsNullToString(dr["payment_terms"]);//结算方式
                cobSetAccount.SelectedValue = CommonCtrl.IsNullToString(dr["settlement_account"]);//结算账户
                txtBalanceCompany.Tag = CommonCtrl.IsNullToString(dr["settle_company"]);//结算单位
                txtBalanceCompany.Text = GetCompanyName(CommonCtrl.IsNullToString(dr["settle_company"]));//结算单位
                #endregion

                #region 顶部按钮设置
                if (wStatus == WindowStatus.Edit)
                {
                    //labReserveNoS.Text = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["maintain_no"])) ? CommonCtrl.IsNullToString(dr["maintain_no"]) : labReserveNoS.Text;//维修单号
                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["maintain_no"])))
                    {
                        labReserveNoS.Text = CommonCtrl.IsNullToString(dr["maintain_no"]);//维修单号
                    }
                    else
                    {
                        labReserveNoS.Visible = false;
                    }
                    strStatus = CommonCtrl.IsNullToString(dr["info_status"]);//单据状态
                    if (strStatus == Convert.ToInt32(DataSources.EnumAuditStatus.Balance).ToString())
                    {
                        //已结算状态屏蔽结算按钮
                        base.btnBalance.Enabled = false;
                    }
                    else if (strStatus == Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString())
                    {
                        //已审核时屏蔽结算、审核按钮
                        base.btnBalance.Enabled = false;
                        base.btnVerify.Enabled = false;
                    }
                    //else if (strStatus == Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString())
                    //{
                    //    //审核没通过时屏蔽审核按钮
                    //    base.btnVerify.Enabled = false;
                    //}
                }
                else
                {
                    labReserveNoS.Visible = false;
                }
                #endregion
                #endregion

                #region 底部datagridview数据
                BindPData(strId);
                BindMData(strId);
                BindOData(strId);
                BindAData("tb_maintain_info", strId);
                #endregion
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 获取结算单位名称
        private string GetCompanyName(string comId)
        {
            return DBHelper.GetSingleValue("获得单位名", "tb_customer", "cust_name", "cust_id='" + comId + "'", "");
        }
        #endregion

        #region 底部datagridview数据信息绑定

        #region 维修项目数据
        private void BindPData(string strOrderId)
        {
            try
            {
                //维修项目数据                
                DataTable dpt = DBHelper.GetTable("维修项目数据", "tb_maintain_item", "*", string.Format(" maintain_id='{0}' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", strOrderId), "", "");
                if (dpt.Rows.Count > 0)
                {
                    if (dpt.Rows.Count > dgvproject.Rows.Count)
                    {
                        dgvproject.Rows.Add(dpt.Rows.Count - dgvproject.Rows.Count + 2);
                    }
                    else if ((dgvproject.Rows.Count - dpt.Rows.Count) == 1)
                    {
                        dgvproject.Rows.Add(1);
                    }
                    for (int i = 0; i < dpt.Rows.Count; i++)
                    {
                        DataRow dpr = dpt.Rows[i];
                        dgvproject.Rows[i].Cells["item_id"].Value = CommonCtrl.IsNullToString(dpr["item_id"]);
                        dgvproject.Rows[i].Cells["three_warranty"].Value = CommonCtrl.IsNullToString(dpr["three_warranty"]) == "1" ? "是" : "否";
                        dgvproject.Rows[i].Cells["man_hour_type"].Value = CommonCtrl.IsNullToString(dpr["man_hour_type"]);
                        dgvproject.Rows[i].Cells["item_no"].Value = CommonCtrl.IsNullToString(dpr["item_no"]);
                        dgvproject.Rows[i].Cells["item_name"].Value = CommonCtrl.IsNullToString(dpr["item_name"]);
                        dgvproject.Rows[i].Cells["item_type"].Value = CommonCtrl.IsNullToString(dpr["item_type"]);
                        dgvproject.Rows[i].Cells["man_hour_quantity"].Value =!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["man_hour_quantity"]))? Math.Round((Convert.ToDecimal(CommonCtrl.IsNullToString(dpr["man_hour_quantity"]))),1).ToString("0.0"):"0.0";
                        dgvproject.Rows[i].Cells["man_hour_norm_unitprice"].ReadOnly = CommonCtrl.IsNullToString(dpr["man_hour_type"]) == "工时" ? true : false;
                        dgvproject.Rows[i].Cells["man_hour_norm_unitprice"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["man_hour_norm_unitprice"])) ? Math.Round((Convert.ToDecimal(CommonCtrl.IsNullToString(dpr["man_hour_norm_unitprice"]))), 2).ToString("0.00") : "0.00";
                        dgvproject.Rows[i].Cells["remarks"].Value = CommonCtrl.IsNullToString(dpr["remarks"]);
                        dgvproject.Rows[i].Cells["sum_money_goods"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["sum_money_goods"])) ? Math.Round((Convert.ToDecimal(CommonCtrl.IsNullToString(dpr["sum_money_goods"]))), 2).ToString("0.00") : "0.00";
                        dgvproject.Rows[i].Cells["member_discount"].Value = CommonCtrl.IsNullToString(dpr["member_discount"]);
                        dgvproject.Rows[i].Cells["member_price"].Value =  !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["member_price"]))?Math.Round((Convert.ToDecimal(CommonCtrl.IsNullToString(dpr["member_price"]))), 2).ToString("0.00") : "0.00";
                        dgvproject.Rows[i].Cells["member_sum_money"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["member_sum_money"])) ? Math.Round((Convert.ToDecimal(CommonCtrl.IsNullToString(dpr["member_sum_money"]))), 2).ToString("0.00") : "0.00";
                        dgvproject.Rows[i].Cells["whours_id"].Value = CommonCtrl.IsNullToString(dpr["whours_id"]);
                        listProject.Add(CommonCtrl.IsNullToString(dpr["item_no"]));
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 维修用料数据
        private void BindMData(string strOrderId)
        {
            try
            {
                //维修用料数据
                DataTable dmt = DBHelper.GetTable("维修用料数据", "tb_maintain_material_detail", "*", string.Format(" maintain_id='{0}' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", strOrderId), "", "");
                if (dmt.Rows.Count > 0)
                {
                    if (dmt.Rows.Count > dgvMaterials.Rows.Count)
                    {
                        dgvMaterials.Rows.Add(dmt.Rows.Count - dgvMaterials.Rows.Count + 2);
                    }
                    else if ((dgvMaterials.Rows.Count - dmt.Rows.Count) == 1)
                    {
                        dgvMaterials.Rows.Add(1);
                    }
                    for (int i = 0; i < dmt.Rows.Count; i++)
                    {
                        DataRow dmr = dmt.Rows[i];
                        dgvMaterials.Rows[i].Cells["material_id"].Value = CommonCtrl.IsNullToString(dmr["material_id"]);
                        dgvMaterials.Rows[i].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dmr["parts_code"]);
                        dgvMaterials.Rows[i].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dmr["parts_name"]);
                        dgvMaterials.Rows[i].Cells["norms"].Value = CommonCtrl.IsNullToString(dmr["norms"]);
                        dgvMaterials.Rows[i].Cells["unit"].Value = CommonCtrl.IsNullToString(dmr["unit"]);
                        dgvMaterials.Rows[i].Cells["quantity"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dmr["quantity"]))?Math.Round(Convert.ToDecimal(CommonCtrl.IsNullToString(dmr["quantity"])),1).ToString("0.0"):"0.0";
                        dgvMaterials.Rows[i].Cells["unit_price"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dmr["unit_price"])) ? Math.Round(Convert.ToDecimal(CommonCtrl.IsNullToString(dmr["unit_price"])), 2).ToString("0.00") : "0.00";
                        dgvMaterials.Rows[i].Cells["Mmember_discount"].Value = CommonCtrl.IsNullToString(dmr["member_discount"]);
                        dgvMaterials.Rows[i].Cells["Mmember_price"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dmr["member_price"])) ? Math.Round(Convert.ToDecimal(CommonCtrl.IsNullToString(dmr["member_price"])), 2).ToString("0.00") : "0.00";
                        dgvMaterials.Rows[i].Cells["sum_money"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dmr["sum_money"])) ? Math.Round(Convert.ToDecimal(CommonCtrl.IsNullToString(dmr["sum_money"])), 2).ToString("0.00") : "0.00";
                        dgvMaterials.Rows[i].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dmr["drawn_no"]);
                        dgvMaterials.Rows[i].Cells["vehicle_brand"].Value = CommonCtrl.IsNullToString(dmr["vehicle_brand"]);
                        dgvMaterials.Rows[i].Cells["Mthree_warranty"].Value = CommonCtrl.IsNullToString(dmr["three_warranty"]) == "1" ? "是" : "否";
                        dgvMaterials.Rows[i].Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dmr["remarks"]);
                        dgvMaterials.Rows[i].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dmr["whether_imported"]) == "1" ? "是" : "否";
                        listMater.Add(CommonCtrl.IsNullToString(dmr["parts_code"]));
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 其他项目收费数据
        private void BindOData(string strOrderId)
        {
            try
            {
                ////其他项目收费数据
                DataTable dot = DBHelper.GetTable("其他项目收费数据", "tb_maintain_other_toll", "*", string.Format(" maintain_id='{0}'", strOrderId), "", "");
                if (dot.Rows.Count > 0)
                {
                    if (dot.Rows.Count > dgvOther.Rows.Count)
                    {
                        dgvOther.Rows.Add(dot.Rows.Count - dgvOther.Rows.Count + 1);
                    }
                    for (int i = 0; i < dot.Rows.Count; i++)
                    {
                        DataRow dor = dot.Rows[i];
                        dgvOther.Rows[i].Cells["toll_id"].Value = CommonCtrl.IsNullToString(dor["toll_id"]);
                        dgvOther.Rows[i].Cells["Osum_money"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dor["sum_money"])) ? Math.Round(Convert.ToDecimal(CommonCtrl.IsNullToString(dor["sum_money"])), 2).ToString("0.00") : "0.00";
                        dgvOther.Rows[i].Cells["Oremarks"].Value = CommonCtrl.IsNullToString(dor["remarks"]);
                        dgvOther.Rows[i].Cells["cost_types"].Value = CommonCtrl.IsNullToString(dor["cost_types"]);
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 附件信息数据
        private void BindAData(string strTableName, string strOrderId)
        {
            try
            {
                //附件信息数据
                ucAttr.TableName = strTableName;
                ucAttr.TableNameKeyValue = strOrderId;
                ucAttr.BindAttachment();
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #endregion       

        #region 根据部门的选择绑定经办人
        /// <summary>
        /// 根据部门的选择绑定经办人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboOrgId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboOrgId.SelectedValue == null)
                {
                    return;
                }
                CommonFuncCall.BindHandle(cobYHandle, cboOrgId.SelectedValue.ToString(), "请选择");
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 维修用料信息读取
        private void dgvMaterials_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3)
                {
                    dcPmoney = 0;
                    frmParts frmPart = new frmParts();
                    DialogResult result = frmPart.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        string strPId = frmPart.PartsID;
                        DataTable dpt = DBHelper.GetTable("", "v_parts_chooser", "*", " parts_id='" + strPId + "'", "", "");
                        if (dpt.Rows.Count > 0)
                        {
                            DataRow dpr = dpt.Rows[0];
                            dgvMaterials.Rows[e.RowIndex].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dpr["ser_parts_code"]);
                            dgvMaterials.Rows[e.RowIndex].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dpr["parts_name"]);
                            dgvMaterials.Rows[e.RowIndex].Cells["norms"].Value = CommonCtrl.IsNullToString(dpr["model"]);
                            dgvMaterials.Rows[e.RowIndex].Cells["unit"].Value = CommonCtrl.IsNullToString(dpr["unit"]);
                            dgvMaterials.Rows[e.RowIndex].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dpr["is_import"]) == "1" ? "是" : "否";
                            dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value = "1";
                            dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value = CommonCtrl.IsNullToString(dpr["ref_out_price"]);
                            string strPzk = !string.IsNullOrEmpty(labMemberLZkS.Text) ? labMemberLZkS.Text : "10";
                            //会员折扣
                            dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk) * 10);
                            string strNum = dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value.ToString();
                            string strUMoney = dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value != null ? dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value.ToString() : "0";
                            //会员单价
                            dgvMaterials.Rows[e.RowIndex].Cells["Mmember_price"].Value = Convert.ToString(Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value) / 10);
                            dgvMaterials.Rows[e.RowIndex].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dpr["drawing_num"]);
                            dgvMaterials.Rows[e.RowIndex].Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dpr["remark"]);
                            dgvMaterials.Rows[e.RowIndex].Cells["vehicle_brand"].Value = CommonFuncCall.GetCarTypeForMa((CommonCtrl.IsNullToString(dpr["ser_parts_code"])));
                        }
                        foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
                        {
                            if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["sum_money"].Value)))
                            {
                                dcPmoney += Convert.ToDecimal(dgvr.Cells["sum_money"].Value);
                            }
                        }
                    }
                    txtFSumMoney.Caption = dcPmoney.ToString();
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        /// <summary>
        /// 数量、原始单价发生改变时金额也跟着改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvMaterials_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dcPmoney = 0;
                string strIsThree = CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mthree_warranty"].Value);
                if (dgvMaterials.Rows[e.RowIndex].Cells["parts_name"].Value != null)
                {
                    if (e.ColumnIndex == 6 || e.ColumnIndex == 7)
                    {
                        if (strIsThree == "否")
                        {
                            string strNum = dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value != null ? dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value.ToString() : "0";
                            string strUMoney = dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value != null ? dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value.ToString() : "0";
                            string strzk = dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value != null ? dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value.ToString() : "0";
                            dgvMaterials.Rows[e.RowIndex].Cells["Mmember_price"].Value = Convert.ToString(Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(strzk.Length > 0 ? strzk : "0") / 100);
                            if (dgvMaterials.Rows[e.RowIndex].Cells["Mmember_price"].Value.ToString() != "0")
                            {
                                strzk = dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value.ToString();
                                dgvMaterials.Rows[e.RowIndex].Cells["sum_money"].Value = Convert.ToString(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(strzk) / 100);
                            }
                            else
                            {
                                dgvMaterials.Rows[e.RowIndex].Cells["sum_money"].Value = Convert.ToString(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney));
                            }
                        }
                    }
                    if (e.ColumnIndex == 13)
                    {
                        dgvMaterials.Columns["quantity"].ReadOnly = strIsThree == "否" ? false : true;
                    }
                }
                foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
                {
                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["sum_money"].Value)))
                    {
                        dcPmoney += Convert.ToDecimal(dgvr.Cells["sum_money"].Value);
                    }
                }
                txtFSumMoney.Caption = dcPmoney.ToString();
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 结算单位选择器事件
        private void txtBalanceCompany_ChooserClick(object sender, EventArgs e)
        {
            try
            {
                frmCustomerInfo frmCInfo = new frmCustomerInfo();
                DialogResult result = frmCInfo.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtBalanceCompany.Text = frmCInfo.strCustomerName;
                    txtBalanceCompany.Tag = frmCInfo.strCustomerId;
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 计算税额、价税合计
        /// <summary>
        /// 计算税额、价税合计
        /// </summary>
        /// <param name="txtM">货款</param>
        /// <param name="txtR">税率</param>
        /// <param name="txtC">税额</param>
        /// <param name="txtS">价税合计</param>
        private void RateMoney(TextBoxEx txtM, TextBoxEx txtR, TextBoxEx txtC, TextBoxEx txtS)
        {
            try
            {
                string strHMoney = !string.IsNullOrEmpty(txtM.Caption.Trim()) ? txtM.Caption.Trim() : "0.00";
                string strHRate = !string.IsNullOrEmpty(txtR.Caption.Trim()) ? txtR.Caption.Trim() : "0.00";
                txtC.Caption = Math.Round((Convert.ToDecimal(strHMoney) * Convert.ToDecimal(strHRate) / 100),2).ToString("0.00");
                txtS.Caption = Math.Round((Convert.ToDecimal(strHMoney) + Convert.ToDecimal(txtC.Caption.Trim())),2).ToString("0.00");
                txtShouldSum.Caption = Math.Round(Convert.ToDecimal(!string.IsNullOrEmpty(txtHSum.Caption.Trim()) ? txtHSum.Caption.Trim() : "0.00") + Convert.ToDecimal(!string.IsNullOrEmpty(txtFSum.Caption.Trim()) ? txtFSum.Caption.Trim() : "0.00") + Convert.ToDecimal(!string.IsNullOrEmpty(txtOSum.Caption.Trim()) ? txtOSum.Caption.Trim() : "0.00") - Convert.ToDecimal(!string.IsNullOrEmpty(txtPrivilegeCost.Caption.Trim()) ? txtPrivilegeCost.Caption.Trim() : "0.00"),2).ToString("0.00");//应收总额
                strShouldSum = txtShouldSum.Caption = Math.Round(Convert.ToDecimal(!string.IsNullOrEmpty(txtHSum.Caption.Trim()) ? txtHSum.Caption.Trim() : "0.00") + Convert.ToDecimal(!string.IsNullOrEmpty(txtFSum.Caption.Trim()) ? txtFSum.Caption.Trim() : "0.00") + Convert.ToDecimal(!string.IsNullOrEmpty(txtOSum.Caption.Trim()) ? txtOSum.Caption.Trim() : "0.00") - Convert.ToDecimal(!string.IsNullOrEmpty(txtPrivilegeCost.Caption.Trim()) ? txtPrivilegeCost.Caption.Trim() : "0.00"),2).ToString("0.00");//应收总额
                //本次欠款金额
                string Smoney = !string.IsNullOrEmpty(txtReceivedSum.Caption) ? txtReceivedSum.Caption : "0.00";//实收总额
                string Ymoney = !string.IsNullOrEmpty(txtShouldSum.Caption) ? txtShouldSum.Caption : "0.00";//应收总额
                if (Convert.ToDecimal(Smoney) > Convert.ToDecimal(Ymoney))
                {
                    MessageBoxEx.Show("实收总额不能大于应收总额!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    txtReceivedSum.Caption = "0.00";
                    return;
                }
                txtDebtCost.Caption = (Convert.ToDecimal(Ymoney) - Convert.ToDecimal(Smoney)).ToString();
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 维修单导入功能
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(strBefore_orderId))
                {
                    List<SQLObj> listSql = new List<SQLObj>();
                    UpdateMaintainInfo(listSql, strBefore_orderId, "", "0");
                    DBHelper.BatchExeSQLMultiByTrans("更新前置单据状体", listSql);
                }
                UCRepairBalanceImport import = new UCRepairBalanceImport();
                import.uc = this;
                import.ShowDialog();
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 维修结算单导入-根据前置单据绑定维修信息
        /// <summary>
        /// 维修结算单导入-根据前置单据绑定维修信息
        /// </summary>
        public void BindSetmentData()
        {
            try
            {
                #region 基本信息
                DataTable dt = DBHelper.GetTable("查询维修单单", "tb_maintain_info", "*", " maintain_id ='" + strBefore_orderId + "'", "", "");
                if (dt == null || dt.Rows.Count == 0)
                {
                    return;
                }
                DataRow dr = dt.Rows[0];
                labReserveNoS.Text = CommonCtrl.IsNullToString(dr["maintain_no"]);//维修单号
                txtCarNO.Text = CommonCtrl.IsNullToString(dr["vehicle_no"]);//车牌号
                txtCarNO.Enabled = false;
                txtVIN.Caption = CommonCtrl.IsNullToString(dr["vehicle_vin"]);//VIN
                txtVIN.Enabled = false;
                txtEngineNo.Caption = CommonCtrl.IsNullToString(dr["engine_no"]);//发动机号
                txtEngineNo.Enabled = false;
                txtCarType.Text = GetVmodel(CommonCtrl.IsNullToString(dr["vehicle_model"]));//车型     
                txtCarType.Tag = CommonCtrl.IsNullToString(dr["vehicle_model"]);
                txtCarType.Enabled = false; ;
                cobCarBrand.SelectedValue = CommonCtrl.IsNullToString(dr["vehicle_brand"]);//车辆品牌
                cobCarBrand.Enabled = false;
                cobPayType.SelectedValue = CommonCtrl.IsNullToString(dr["maintain_payment"]);//维修付费方式
                cobPayType.Enabled = false;
                cobRepType.SelectedValue = CommonCtrl.IsNullToString(dr["maintain_type"]);//维修类别
                cobRepType.Enabled = false;
                txtDriver.Caption = CommonCtrl.IsNullToString(dr["driver_name"]);//司机
                txtDriver.Enabled = false;
                txtDriverPhone.Caption = CommonCtrl.IsNullToString(dr["driver_mobile"]);//司机手机
                txtDriverPhone.Enabled = false;
                cobColor.SelectedValue = CommonCtrl.IsNullToString(dr["vehicle_color"]);//颜色  
                cobColor.Enabled = false;
                txtMil.Caption = CommonCtrl.IsNullToString(dr["travel_mileage"]);//行驶里程
                txtMil.Enabled = false;
                txtMl.Caption = CommonCtrl.IsNullToString(dr["oil_into_factory"]);//进场油量
                txtMl.Enabled = false;
                txtSuggestMil.Caption = CommonCtrl.IsNullToString(dr["maintain_mileage"]);//建议保养里程
                txtSuggestMil.Enabled = false;
                string strCreateTime = CommonCtrl.IsNullToString(dr["completion_time"]); //预计完工时间
                if (!string.IsNullOrEmpty(strCreateTime))
                {
                    dtpSuggestTime.Value = Common.UtcLongToLocalDateTime(Convert.ToInt64(strCreateTime));
                }
                dtpSuggestTime.Enabled = false;
                txtRemark.Caption = CommonCtrl.IsNullToString(dr["remark"]);//备注
                txtRemark.Enabled = false;
                txtPreferReason.Caption = CommonCtrl.IsNullToString(dr["favorable_reason"]);//优惠原因
                txtPreferReason.Enabled = false;
                txtCustomNO.Text = CommonCtrl.IsNullToString(dr["customer_code"]);//客户编码
                txtCustomNO.Enabled = false;
                txtCustomName.Caption = CommonCtrl.IsNullToString(dr["customer_name"]);//客户名称  
                txtCustomName.Enabled = false;
                txtCustomNO.Tag = CommonCtrl.IsNullToString(dr["customer_id"]);//客户关联id
                #region 会员信息
                DataTable dct = DBHelper.GetTable("获取会员信息", "tb_customer", "member_number,member_class,accessories_discount,workhours_discount", " is_member='1' and cust_id='" + txtCustomNO.Tag + "'", "", "");
                if (dct.Rows.Count > 0)
                {
                    DataRow dcr = dct.Rows[0];
                    labMemberPZkS.Text = CommonCtrl.IsNullToString(dcr["workhours_discount"]);
                    labMemberLZkS.Text = CommonCtrl.IsNullToString(dcr["accessories_discount"]);
                }
                else
                {
                    labMemberPZkS.Text = "0";
                    labMemberLZkS.Text = "0";
                }
                #endregion
                txtContact.Caption = CommonCtrl.IsNullToString(dr["linkman"]);//联系人
                txtContact.Enabled = false;
                txtContactPhone.Caption = CommonCtrl.IsNullToString(dr["link_man_mobile"]);//联系人手机  
                txtContactPhone.Enabled = false;

                #endregion

                #region 底部datagridview数据
                ucAttr.Enabled = false;
                dgvMaterials.Enabled = false;
                dgvOther.Enabled = false;
                dgvproject.Enabled = false;
                BindPData(strBefore_orderId);
                BindMData(strBefore_orderId);
                BindOData(strBefore_orderId);
                BindAData("tb_maintain_info", strBefore_orderId);
                List<SQLObj> listSql = new List<SQLObj>();
                UpdateMaintainInfo(listSql, strBefore_orderId, "", "1");
                DBHelper.BatchExeSQLMultiByTrans("更新前置单据状体", listSql);
                #endregion
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 获取车型名称
        private string GetVmodel(string strVId)
        {
            return DBHelper.GetSingleValue("获得车型名称", "tb_vehicle_models", "vm_name", "vm_id='" + strVId + "'", "");
        }
        #endregion

        #region 导入后更新预约单-在预约单中添加维修单号
        /// <summary>
        /// 导入后更新预约单-在预约单中添加维修单号
        /// </summary>
        /// <param name="strReservId">维修单Id</param>
        /// <param name="strMaintainNo">维修单号</param>
        /// <param name="status">操作状体，0保存开放、1导入占用、2结算审核锁定</param>
        private void UpdateMaintainInfo(List<SQLObj> listSql, string strReservId, string strMaintainNo, string status)
        {
            try
            {
                SQLObj obj = new SQLObj();
                obj.cmdType = CommandType.Text;
                Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();

                dicParam.Add("maintain_id", new ParamObj("maintain_id", strReservId, SysDbType.VarChar, 40));
                if (status == "0")
                {
                    //保存时，前置单据被释放               
                    dicParam.Add("Import_status", new ParamObj("Import_status", "0", SysDbType.VarChar, 40));//开放
                    obj.sqlString = "update tb_maintain_info set Import_status=@Import_status where maintain_id=@maintain_id";

                }
                else if (status == "1")
                {
                    //导入时，前置单据被占用 
                    dicParam.Add("Import_status", new ParamObj("Import_status", "1", SysDbType.VarChar, 40));//占用
                    obj.sqlString = "update tb_maintain_info set Import_status=@Import_status where maintain_id=@maintain_id";
                }
                else if (status == "2")
                {
                    //审核结算时，前置单据被锁定
                    dicParam.Add("Import_status", new ParamObj("Import_status", "2", SysDbType.VarChar, 40));//锁定
                    obj.sqlString = "update tb_maintain_info set Import_status=@Import_status where maintain_id=@maintain_id";

                }
                obj.Param = dicParam;
                listSql.Add(obj);
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 工时税率值发生改变时更改工时税额与工时价税合计值
        private void txtHTaxRate_UserControlValueChanged(object sender, EventArgs e)
        {
            RateMoney(txtHSumMoney, txtHTaxRate, txtHTaxCost, txtHSum);
        }
        #endregion

        #region 配件税率值发生改变时更改配件税额与配件价税合计值
        private void txtFTaxRate_UserControlValueChanged(object sender, EventArgs e)
        {
            RateMoney(txtFSumMoney, txtFTaxRate, txtFTaxCost, txtFSum);
        }
        #endregion

        #region 其他税率值发生改变时更改其他税额与其他价税合计值
        private void txtOTaxRate_UserControlValueChanged(object sender, EventArgs e)
        {
            RateMoney(txtOSumMoney, txtOTaxRate, txtOTaxCost, txtOSum);
        }
        #endregion

        #region 优惠费用值添加后应收总额发生变化
        private void txtPrivilegeCost_UserControlValueChanged(object sender, EventArgs e)
        {
            try
            {
                string strYmoney = !string.IsNullOrEmpty(txtPrivilegeCost.Caption.Trim()) ? txtPrivilegeCost.Caption.Trim() : "0.00";//优惠金额
                string strSmoney = !string.IsNullOrEmpty(txtShouldSum.Caption.Trim()) ? txtShouldSum.Caption.Trim() : "0.00";//应收金额
                if (Convert.ToDecimal(strYmoney) > Convert.ToDecimal(strShouldSum))
                {
                    MessageBoxEx.Show("优惠费用不能大于应收总额!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    txtPrivilegeCost.Caption = "0.00";
                    return;
                }
                txtShouldSum.Caption = Math.Round(Convert.ToDecimal(!string.IsNullOrEmpty(strShouldSum) ? strShouldSum : "0.00") - Convert.ToDecimal(!string.IsNullOrEmpty(txtPrivilegeCost.Caption) ? txtPrivilegeCost.Caption : "0.00"),2).ToString("0.00");
                //本次欠款金额
                string Smoney = !string.IsNullOrEmpty(txtReceivedSum.Caption) ? txtReceivedSum.Caption : "0.00";//实收总额
                string Ymoney = !string.IsNullOrEmpty(txtShouldSum.Caption) ? txtShouldSum.Caption : "0.00";//应收总额
                if (Convert.ToDecimal(Smoney) > Convert.ToDecimal(strShouldSum))
                {
                    MessageBoxEx.Show("实收总额不能大于应收总额!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    txtReceivedSum.Caption = "0.00";
                    return;
                }
                txtDebtCost.Caption = Math.Round((Convert.ToDecimal(Ymoney) - Convert.ToDecimal(Smoney)),2).ToString("0.00");
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 实收总额添加后本次欠款金额发生变化
        private void txtReceivedSum_UserControlValueChanged(object sender, EventArgs e)
        {
            try
            {
                string Smoney = !string.IsNullOrEmpty(txtReceivedSum.Caption) ? txtReceivedSum.Caption : "0.00";//实收总额
                string Ymoney = !string.IsNullOrEmpty(txtShouldSum.Caption) ? txtShouldSum.Caption : "0.00";//应收总额
                if (Convert.ToDecimal(Smoney) > Convert.ToDecimal(strShouldSum))
                {
                    MessageBoxEx.Show("实收总额不能大于应收总额!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    txtReceivedSum.Caption ="0.00";
                    return;
                }
                txtDebtCost.Caption = Math.Round((Convert.ToDecimal(Ymoney) - Convert.ToDecimal(Smoney)),2).ToString("0.00");
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 维修项目信息读取
        private void dgvproject_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int intProws = e.RowIndex;//当前行的索引
                frmWorkHours frmHours = new frmWorkHours();
                DialogResult result = frmHours.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (listProject.Contains(frmHours.strProjectNum))
                    {
                        MessageBoxEx.Show("此维修项目已存在，请选择其他维修项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    for (int i = 0; i <= intProws; i++)
                    {
                        if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["item_no"].Value)))
                        {
                            dgvproject.Rows[i].Cells["item_no"].Value = frmHours.strProjectNum;
                            dgvproject.Rows[i].Cells["item_type"].Value = GetDicName(frmHours.strRepairType);
                            dgvproject.Rows[i].Cells["item_name"].Value = frmHours.strProjectName;
                            dgvproject.Rows[i].Cells["man_hour_norm_unitprice"].Value =  Math.Round((Convert.ToDecimal(frmHours.strQuotaPrice)),2).ToString("0.00");
                            dgvproject.Rows[i].Cells["remarks"].Value = frmHours.strRemark;
                            dgvproject.Rows[i].Cells["man_hour_type"].Value = frmHours.strWhoursType == "1" ? "工时" : "定额";
                            dgvproject.Rows[i].Cells["man_hour_norm_unitprice"].ReadOnly = CommonCtrl.IsNullToString(frmHours.strWhoursType) == "1" ? true : false;
                            string strPzk = !string.IsNullOrEmpty(labMemberPZkS.Text) ? labMemberPZkS.Text : "10";
                            //会员折扣
                            dgvproject.Rows[i].Cells["member_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk) * 10);
                            //会员工时费
                            dgvproject.Rows[i].Cells["member_price"].Value = Math.Round((Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strQuotaPrice) ? frmHours.strQuotaPrice : "0.00") * (Convert.ToDecimal(strPzk) / 10)),2).ToString("0.00");
                            if (CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value) != "0.00")
                            {
                                //折扣额
                                dgvproject.Rows[i].Cells["member_sum_money"].Value = Math.Round((Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strQuotaPrice) ? frmHours.strQuotaPrice : "0.00") - Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value) : "0.00")),2).ToString("0.00");
                            }
                            else
                            {
                                dgvproject.Rows[i].Cells["member_sum_money"].Value = "0.00";
                            }
                            dgvproject.Rows[i].Cells["whours_id"].Value = frmHours.strWhours_id;
                            dgvproject.Rows[i].Cells["man_hour_quantity"].Value = Math.Round((Convert.ToDecimal(frmHours.strWhoursNum)),1).ToString("0.0");
                            dgvproject.Rows[i].Cells["sum_money_goods"].Value = Math.Round((Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strWhoursNum) ? frmHours.strWhoursNum : "0.00") * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value) : "0.00")),2).ToString("0.00");
                            dgvproject.Rows[i].Cells["three_warranty"].Value = "否";
                            dcHmoney = 0;
                            foreach (DataGridViewRow dgvr in dgvproject.Rows)
                            {
                                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["sum_money_goods"].Value)))
                                {
                                    dcHmoney += Convert.ToDecimal(dgvr.Cells["sum_money_goods"].Value);
                                }
                            }
                            txtHSumMoney.Caption =  Math.Round(dcHmoney,2).ToString("0.00");//工时货款
                            txtHTaxCost.Caption = Math.Round((Convert.ToDecimal(!string.IsNullOrEmpty(txtHSumMoney.Caption.Trim()) ? txtHSumMoney.Caption.Trim() : "0.00") * Convert.ToDecimal(!string.IsNullOrEmpty(txtHTaxRate.Caption.Trim()) ? txtHTaxRate.Caption.Trim() : "0.00") / 100),2).ToString("0.00");//工时税额
                            txtHSum.Caption =Math.Round((Convert.ToDecimal(!string.IsNullOrEmpty(txtHSumMoney.Caption.Trim()) ? txtHSumMoney.Caption.Trim() : "0.00") + Convert.ToDecimal(!string.IsNullOrEmpty(txtHTaxCost.Caption.Trim()) ? txtHTaxCost.Caption.Trim() : "0.00")),2).ToString("0.00");//工时价税合计
                            txtShouldSum.Caption = Math.Round((Convert.ToDecimal(!string.IsNullOrEmpty(txtHSum.Caption.Trim()) ? txtHSum.Caption.Trim() : "0.00") + Convert.ToDecimal(!string.IsNullOrEmpty(txtFSum.Caption.Trim()) ? txtFSum.Caption.Trim() : "0.00") + Convert.ToDecimal(!string.IsNullOrEmpty(txtOSum.Caption.Trim()) ? txtOSum.Caption.Trim() : "0.00") - Convert.ToDecimal(!string.IsNullOrEmpty(txtPrivilegeCost.Caption.Trim()) ? txtPrivilegeCost.Caption.Trim() : "0.00")),2).ToString("0.00");//应收总额
                            strShouldSum = txtShouldSum.Caption;
                            dgvproject.Rows.Add(1);
                            break;
                        }
                    }
                }
                foreach (DataGridViewRow dgvr in dgvproject.Rows)
                {
                    string strPCode = CommonCtrl.IsNullToString(dgvr.Cells["item_no"].Value);
                    if (!string.IsNullOrEmpty(strPCode)&&!listProject.Contains(strPCode))
                    {
                        listProject.Add(strPCode);
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        /// <summary>
        /// 工时数量发生改变时货款也跟着改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>      
        private void dgvproject_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                //是否三包值
                string strIsThree = CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["three_warranty"].Value);
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["item_name"].Value)))
                {
                    ControlsConfig.SetCellsValue(dgvproject, e.RowIndex, "man_hour_quantity,man_hour_norm_unitprice");
                    //工时数量
                    string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_quantity"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_quantity"].Value) : "0";
                    //原工时单价
                    string strUnitprice = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value) : "0.00";

                    if (e.ColumnIndex == 6 || e.ColumnIndex == 7)
                    {
                        //会员工时费
                        dgvproject.Rows[e.RowIndex].Cells["member_price"].Value = Math.Round((Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["member_discount"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["member_discount"].Value) : "0.00") * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value) : "0.00") / 100),2).ToString("0.00");
                        string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["member_price"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["member_price"].Value) : "0.00";
                        if (CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["member_price"].Value) != "0.00")
                        {
                            //折扣额
                            dgvproject.Rows[e.RowIndex].Cells["member_sum_money"].Value =  Math.Round((Convert.ToDecimal(strUnitprice) - Convert.ToDecimal(strUMoney)),2).ToString("0.00");
                            //货款
                            dgvproject.Rows[e.RowIndex].Cells["sum_money_goods"].Value = Math.Round((Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0.00" : strUMoney)),2).ToString("0.00");
                        }
                        else
                        {
                            dgvproject.Rows[e.RowIndex].Cells["sum_money_goods"].Value =  Math.Round(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value) : "0.00"),2).ToString("0.00");
                        }
                        dgvproject.Rows[e.RowIndex].Cells["man_hour_quantity"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_quantity"].Value)) ?  Math.Round((Convert.ToDecimal(dgvproject.Rows[e.RowIndex].Cells["man_hour_quantity"].Value)),1).ToString("0.0") : "0.0";
                    }
                    if (e.ColumnIndex == 4)
                    {
                        //if (CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_type"].Value) == "工时")
                        //{
                        //    //工时时设置数量和金额均可修改
                        //    //dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].ReadOnly = strIsThree == "否" ? false : true;
                        //    dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].ReadOnly = true;
                        //}
                        dgvproject.Rows[e.RowIndex].Cells["man_hour_quantity"].ReadOnly = strIsThree == "否" ? false : true;
                        if (strIsThree == "是")
                        {
                            string strOId = CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["whours_id"].Value);
                            if (!string.IsNullOrEmpty(strOId))
                            {
                                DataTable dot = DBHelper.GetTable("", "v_workhours_users", "*", "whours_id='" + strOId + "'", "", "");
                                if (dot.Rows.Count > 0)
                                {
                                    DataRow dpr = dot.Rows[0];
                                    dgvproject.Rows[e.RowIndex].Cells["man_hour_quantity"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["whours_num_a"]))?Math.Round((Convert.ToDecimal(CommonCtrl.IsNullToString(dpr["whours_num_a"]))),1).ToString("0.0"):"0.0";
                                    string strONum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["whours_num_a"])) ? CommonCtrl.IsNullToString(dpr["whours_num_a"]) : "0";
                                    dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["quota_price"]))?Math.Round((Convert.ToDecimal(CommonCtrl.IsNullToString(dpr["quota_price"]))),2).ToString("0.00"):"0.00";
                                    dgvproject.Rows[e.RowIndex].Cells["sum_money_goods"].Value = Math.Round((Convert.ToDecimal(strONum) * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["quota_price"])) ? CommonCtrl.IsNullToString(dpr["quota_price"]) : "0.00")),2).ToString("0.00");
                                }
                            }
                        }
                    }
                }
                dcHmoney = 0;
                foreach (DataGridViewRow dgvr in dgvproject.Rows)
                {
                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["sum_money_goods"].Value)))
                    {
                        dcHmoney += Convert.ToDecimal(dgvr.Cells["sum_money_goods"].Value);
                    }
                }
                txtHSumMoney.Caption = Math.Round(dcHmoney, 2).ToString("0.00");//工时货款
                txtHTaxCost.Caption = Math.Round((Convert.ToDecimal(!string.IsNullOrEmpty(txtHSumMoney.Caption.Trim()) ? txtHSumMoney.Caption.Trim() : "0.00") * Convert.ToDecimal(!string.IsNullOrEmpty(txtHTaxRate.Caption.Trim()) ? txtHTaxRate.Caption.Trim() : "0.00") / 100), 2).ToString("0.00");//工时税额
                txtHSum.Caption = Math.Round((Convert.ToDecimal(!string.IsNullOrEmpty(txtHSumMoney.Caption.Trim()) ? txtHSumMoney.Caption.Trim() : "0.00") + Convert.ToDecimal(!string.IsNullOrEmpty(txtHTaxCost.Caption.Trim()) ? txtHTaxCost.Caption.Trim() : "0.00")), 2).ToString("0.00");//工时价税合计
                txtShouldSum.Caption = Math.Round((Convert.ToDecimal(!string.IsNullOrEmpty(txtHSum.Caption.Trim()) ? txtHSum.Caption.Trim() : "0.00") + Convert.ToDecimal(!string.IsNullOrEmpty(txtFSum.Caption.Trim()) ? txtFSum.Caption.Trim() : "0.00") + Convert.ToDecimal(!string.IsNullOrEmpty(txtOSum.Caption.Trim()) ? txtOSum.Caption.Trim() : "0.00") - Convert.ToDecimal(!string.IsNullOrEmpty(txtPrivilegeCost.Caption.Trim()) ? txtPrivilegeCost.Caption.Trim() : "0.00")), 2).ToString("0.00");//应收总额
                strShouldSum =  Math.Round(Convert.ToDecimal(txtShouldSum.Caption),2).ToString("0.00");
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 维修用料信息读取
        private void dgvMaterials_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int intMrows = e.RowIndex;//当前行的索引
                frmParts frmPart = new frmParts();
                DialogResult result = frmPart.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string strPId = frmPart.PartsID;
                    DataTable dpt = DBHelper.GetTable("", "v_parts_chooser", "*", " parts_id='" + strPId + "'", "", "");
                    if (dpt.Rows.Count > 0)
                    {
                        DataRow dpr = dpt.Rows[0];
                        if (listMater.Contains(CommonCtrl.IsNullToString(dpr["ser_parts_code"])))
                        {
                            MessageBoxEx.Show("此维修用料已存在，请选择其他维修用料", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        for (int i = 0; i <= intMrows; i++)
                        {
                            if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["parts_code"].Value)))
                            {
                                dgvMaterials.Rows[i].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dpr["ser_parts_code"]);
                                dgvMaterials.Rows[i].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dpr["parts_name"]);
                                dgvMaterials.Rows[i].Cells["norms"].Value = CommonCtrl.IsNullToString(dpr["model"]);
                                dgvMaterials.Rows[i].Cells["unit"].Value = CommonCtrl.IsNullToString(dpr["unit"]);
                                dgvMaterials.Rows[i].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dpr["is_import"]) == "1" ? "是" : "否";
                                dgvMaterials.Rows[i].Cells["quantity"].Value = "1.0";
                                dgvMaterials.Rows[i].Cells["unit_price"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["ref_out_price"])) ? Math.Round(Convert.ToDecimal(CommonCtrl.IsNullToString(dpr["ref_out_price"])), 2).ToString("0.00") : "0.00";
                                string strPzk = !string.IsNullOrEmpty(labMemberLZkS.Text) ? labMemberLZkS.Text : "10";
                                //会员折扣
                                dgvMaterials.Rows[i].Cells["Mmember_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk) * 10);
                                string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["quantity"].Value)) ? Math.Round(Convert.ToDecimal(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["quantity"].Value)), 2).ToString("0.0") : "0.0";
                                string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["unit_price"].Value)) ? dgvMaterials.Rows[i].Cells["unit_price"].Value.ToString() : "0.00";
                                //会员单价
                                dgvMaterials.Rows[i].Cells["Mmember_price"].Value = Math.Round((Convert.ToDecimal(strUMoney = strUMoney == "" ? "0.00" : strUMoney) * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["Mmember_discount"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["Mmember_discount"].Value) : "0.00") / 10), 2).ToString("0.00");
                                dgvMaterials.Rows[i].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dpr["drawing_num"]);
                                dgvMaterials.Rows[i].Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dpr["remark"]);
                                dgvMaterials.Rows[i].Cells["vehicle_brand"].Value = CommonFuncCall.GetCarTypeForMa((CommonCtrl.IsNullToString(dpr["ser_parts_code"])));
                                dgvMaterials.Rows[i].Cells["parts_id"].Value = frmPart.PartsID;
                                dgvMaterials.Rows[i].Cells["Mthree_warranty"].Value = "否";
                                dcPmoney = 0;
                                foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
                                {
                                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["sum_money"].Value)))
                                    {
                                        dcPmoney += Convert.ToDecimal(dgvr.Cells["sum_money"].Value);
                                    }
                                }
                                txtFSumMoney.Caption = dcPmoney.ToString();
                                txtFTaxCost.Caption = Math.Round((Convert.ToDecimal(!string.IsNullOrEmpty(txtFSumMoney.Caption.Trim()) ? txtFSumMoney.Caption.Trim() : "0.00") * Convert.ToDecimal(!string.IsNullOrEmpty(txtFTaxRate.Caption.Trim()) ? txtFTaxRate.Caption.Trim() : "0.00") / 100), 2).ToString("0.00");//配件税额
                                txtFSum.Caption = Math.Round((Convert.ToDecimal(!string.IsNullOrEmpty(txtFSumMoney.Caption.Trim()) ? txtFSumMoney.Caption.Trim() : "0.00") + Convert.ToDecimal(!string.IsNullOrEmpty(txtFTaxCost.Caption.Trim()) ? txtFTaxCost.Caption.Trim() : "0.00")), 2).ToString("0.00");//配件价税合计
                                txtShouldSum.Caption = Math.Round((Convert.ToDecimal(!string.IsNullOrEmpty(txtHSum.Caption.Trim()) ? txtHSum.Caption.Trim() : "0.00") + Convert.ToDecimal(!string.IsNullOrEmpty(txtFSum.Caption.Trim()) ? txtFSum.Caption.Trim() : "0.00") + Convert.ToDecimal(!string.IsNullOrEmpty(txtOSum.Caption.Trim()) ? txtOSum.Caption.Trim() : "0") - Convert.ToDecimal(!string.IsNullOrEmpty(txtPrivilegeCost.Caption.Trim()) ? txtPrivilegeCost.Caption.Trim() : "0.00")), 2).ToString("0.00");//应收总额
                                strShouldSum = Math.Round(Convert.ToDecimal(txtShouldSum.Caption), 2).ToString("0.00");
                                dgvMaterials.Rows.Add(1);
                                break;
                            }
                        }
                    }
                }
                foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
                {
                    string strPCode = CommonCtrl.IsNullToString(dgvr.Cells["parts_code"].Value);
                    if (!string.IsNullOrEmpty(strPCode) && !listMater.Contains(strPCode))
                    {
                        listMater.Add(strPCode);
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        /// <summary>
        /// 数量、原始单价发生改变时金额也跟着改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvMaterials_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                string strIsThree = CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mthree_warranty"].Value);
                if (dgvMaterials.Rows[e.RowIndex].Cells["parts_name"].Value != null)
                {
                    if (e.ColumnIndex == 7 || e.ColumnIndex == 8)
                    {
                        //if (strIsThree == "否")
                        //{
                        ControlsConfig.SetCellsValue(dgvMaterials, e.RowIndex, "quantity,unit_price");
                        string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value)) ? dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value.ToString() : "0";
                        string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value)) ? dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value.ToString() : "0";
                        string strzk = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value)) ? dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value.ToString() : "0";
                        dgvMaterials.Rows[e.RowIndex].Cells["Mmember_price"].Value = Math.Round((Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(strzk.Length > 0 ? strzk : "0") / 100),2).ToString("0.00");
                        if (CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_price"].Value) != "0")
                        {
                            strzk = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value) : "0";
                            dgvMaterials.Rows[e.RowIndex].Cells["sum_money"].Value = Math.Round((Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(strzk) / 100),2).ToString("0.00");
                        }
                        else
                        {
                            dgvMaterials.Rows[e.RowIndex].Cells["sum_money"].Value = Math.Round((Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney)),2).ToString("0.00");
                        }
                        dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value)) ? Math.Round((Convert.ToDecimal(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value)), 1).ToString("0.0") : "0.0";
                        dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value)) ? Math.Round((Convert.ToDecimal(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value)), 2).ToString("0.00") : "0.00";
                        //}
                    }
                    if (e.ColumnIndex == 3)
                    {
                        if (strIsThree == "否")
                        {
                            dgvMaterials.Rows[e.RowIndex].Cells["quantity"].ReadOnly = false;
                            dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].ReadOnly = false;
                        }
                        else if (strIsThree == "是")
                        {
                            dgvMaterials.Columns["quantity"].ReadOnly = false;
                            dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].ReadOnly = true;
                            string strMId = CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["parts_id"].Value);
                            DataTable dpt = DBHelper.GetTable("", "v_parts_chooser", "*", " parts_id='" + strMId + "'", "", "");
                            if (dpt.Rows.Count > 0)
                            {
                                DataRow dpr = dpt.Rows[0];
                                dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value =!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["ref_out_price"]))?Math.Round((Convert.ToDecimal(CommonCtrl.IsNullToString(dpr["ref_out_price"]))),2).ToString("0.00"):"0.00";
                                string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value)) ? Math.Round((Convert.ToDecimal(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value))),2).ToString("0.00") : "0.00";
                                string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value)) ? Math.Round((Convert.ToDecimal(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value))),1).ToString("0.0") : "0.0";
                                dgvMaterials.Rows[e.RowIndex].Cells["sum_money"].Value = Math.Round((Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney)),2).ToString("0.00");
                            }
                        }
                    }
                }
                dcPmoney = 0;
                foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
                {
                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["sum_money"].Value)))
                    {
                        dcPmoney += Convert.ToDecimal(dgvr.Cells["sum_money"].Value);
                    }
                }
                txtFSumMoney.Caption = Math.Round((Convert.ToDecimal(dcPmoney)),2).ToString("0.00");
                txtFTaxCost.Caption = Math.Round((Convert.ToDecimal(!string.IsNullOrEmpty(txtFSumMoney.Caption.Trim()) ? txtFSumMoney.Caption.Trim() : "0.00") * Convert.ToDecimal(!string.IsNullOrEmpty(txtFTaxRate.Caption.Trim()) ? txtFTaxRate.Caption.Trim() : "0.00") / 100), 2).ToString("0.00");//配件税额
                txtFSum.Caption = Math.Round((Convert.ToDecimal(!string.IsNullOrEmpty(txtFSumMoney.Caption.Trim()) ? txtFSumMoney.Caption.Trim() : "0.00") + Convert.ToDecimal(!string.IsNullOrEmpty(txtFTaxCost.Caption.Trim()) ? txtFTaxCost.Caption.Trim() : "0.00")), 2).ToString("0.00");//配件价税合计
                txtShouldSum.Caption = Math.Round((Convert.ToDecimal(!string.IsNullOrEmpty(txtHSum.Caption.Trim()) ? txtHSum.Caption.Trim() : "0.00") + Convert.ToDecimal(!string.IsNullOrEmpty(txtFSum.Caption.Trim()) ? txtFSum.Caption.Trim() : "0.00") + Convert.ToDecimal(!string.IsNullOrEmpty(txtOSum.Caption.Trim()) ? txtOSum.Caption.Trim() : "0") - Convert.ToDecimal(!string.IsNullOrEmpty(txtPrivilegeCost.Caption.Trim()) ? txtPrivilegeCost.Caption.Trim() : "0.00")), 2).ToString("0.00");//应收总额
                strShouldSum = Math.Round(Convert.ToDecimal(txtShouldSum.Caption), 2).ToString("0.00");
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 其他项目收费-获取其他项目费用
        private void dgvOther_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvOther.Rows[e.RowIndex].Cells["cost_types"].Value)))
                {
                    dgvOther.Rows[e.RowIndex].Cells["Osum_money"].ReadOnly = false;
                    dgvOther.Rows[e.RowIndex].Cells["Oremarks"].ReadOnly = false;
                    if (e.ColumnIndex == 2)
                    {
                        ControlsConfig.SetCellsValue(dgvOther, e.RowIndex, "Osum_money");
                        dgvOther.Rows[e.RowIndex].Cells["Osum_money"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvOther.Rows[e.RowIndex].Cells["Osum_money"].Value)) ? Math.Round((Convert.ToDecimal(dgvOther.Rows[e.RowIndex].Cells["Osum_money"].Value)),2).ToString("0.00") : "0.00";
                    }
                }
                decimal dcOmoney = 0;
                foreach (DataGridViewRow dgvr in dgvOther.Rows)
                {
                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["Osum_money"].Value)))
                    {
                        dcOmoney += Convert.ToDecimal(dgvr.Cells["Osum_money"].Value);
                    }
                }
                txtOSumMoney.Caption =  Math.Round((Convert.ToDecimal(dcOmoney)),2).ToString("0.00");
                txtOTaxCost.Caption = Math.Round((Convert.ToDecimal(!string.IsNullOrEmpty(txtOSumMoney.Caption.Trim()) ? txtOSumMoney.Caption.Trim() : "0.00") * Convert.ToDecimal(!string.IsNullOrEmpty(txtOTaxRate.Caption.Trim()) ? txtOTaxRate.Caption.Trim() : "0.00") / 100),2).ToString("0.00");//配件税额
                txtOSum.Caption = Math.Round((Convert.ToDecimal(!string.IsNullOrEmpty(txtOSumMoney.Caption.Trim()) ? txtOSumMoney.Caption.Trim() : "0.00") + Convert.ToDecimal(!string.IsNullOrEmpty(txtOTaxCost.Caption.Trim()) ? txtOTaxCost.Caption.Trim() : "0.00")),2).ToString("0.00");//配件价税合计
                txtShouldSum.Caption =Math.Round((Convert.ToDecimal(!string.IsNullOrEmpty(txtHSum.Caption.Trim()) ? txtHSum.Caption.Trim() : "0.00") + Convert.ToDecimal(!string.IsNullOrEmpty(txtFSum.Caption.Trim()) ? txtFSum.Caption.Trim() : "0.00") + Convert.ToDecimal(!string.IsNullOrEmpty(txtOSum.Caption.Trim()) ? txtOSum.Caption.Trim() : "0.00") - Convert.ToDecimal(!string.IsNullOrEmpty(txtPrivilegeCost.Caption.Trim()) ? txtPrivilegeCost.Caption.Trim() : "0.00")),2).ToString("0.00");//应收总额
                strShouldSum = Math.Round(Convert.ToDecimal(txtShouldSum.Caption), 2).ToString("0.00");
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 右键添加维修项目
        private void addProject_Click(object sender, EventArgs e)
        {
            try
            {
                int intProws = dgvproject.CurrentRow.Index;//当前行的索引
                frmWorkHours frmHours = new frmWorkHours();
                DialogResult result = frmHours.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (listProject.Contains(frmHours.strProjectNum))
                    {
                        MessageBoxEx.Show("此维修项目已存在，请选择其他维修项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    for (int i = 0; i <= intProws; i++)
                    {
                        if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["item_no"].Value)))
                        {
                            dgvproject.Rows[i].Cells["item_no"].Value = frmHours.strProjectNum;
                            dgvproject.Rows[i].Cells["item_type"].Value = GetDicName(frmHours.strRepairType);
                            dgvproject.Rows[i].Cells["item_name"].Value = frmHours.strProjectName;
                            dgvproject.Rows[i].Cells["man_hour_norm_unitprice"].Value =  Math.Round((Convert.ToDecimal(frmHours.strQuotaPrice)),2).ToString("0.00");
                            dgvproject.Rows[i].Cells["remarks"].Value = frmHours.strRemark;
                            dgvproject.Rows[i].Cells["man_hour_type"].Value = frmHours.strWhoursType == "1" ? "工时" : "定额";
                            dgvproject.Rows[i].Cells["man_hour_norm_unitprice"].ReadOnly = CommonCtrl.IsNullToString(frmHours.strWhoursType) == "1" ? true : false;
                            string strPzk = !string.IsNullOrEmpty(labMemberPZkS.Text) ? labMemberPZkS.Text : "10";
                            //会员折扣
                            dgvproject.Rows[i].Cells["member_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk) * 10);
                            //会员工时费
                            dgvproject.Rows[i].Cells["member_price"].Value = Math.Round((Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strQuotaPrice) ? frmHours.strQuotaPrice : "0.00") * (Convert.ToDecimal(strPzk) / 10)),2).ToString("0.00");
                            if (CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value) != "0.00")
                            {
                                //折扣额
                                dgvproject.Rows[i].Cells["member_sum_money"].Value =  Math.Round((Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strQuotaPrice) ? frmHours.strQuotaPrice : "0.00") - Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value) : "0.00")),2).ToString("0.00");
                            }
                            else
                            {
                                dgvproject.Rows[i].Cells["member_sum_money"].Value = "0.00";
                            }
                            dgvproject.Rows[i].Cells["whours_id"].Value = frmHours.strWhours_id;
                            dgvproject.Rows[i].Cells["man_hour_quantity"].Value = Math.Round((Convert.ToDecimal(frmHours.strWhoursNum)), 1).ToString("0.0");
                            dgvproject.Rows[i].Cells["sum_money_goods"].Value = Math.Round((Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strWhoursNum) ? frmHours.strWhoursNum : "0.00") * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value) : "0.00")),2).ToString("0.00");
                            dgvproject.Rows[i].Cells["three_warranty"].Value = "否";
                            dcHmoney = 0;
                            foreach (DataGridViewRow dgvr in dgvproject.Rows)
                            {
                                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["sum_money_goods"].Value)))
                                {
                                    dcHmoney += Convert.ToDecimal(dgvr.Cells["sum_money_goods"].Value);
                                }
                            }
                            txtHSumMoney.Caption = Math.Round(dcHmoney, 2).ToString("0.00");//工时货款
                            txtHTaxCost.Caption = Math.Round((Convert.ToDecimal(!string.IsNullOrEmpty(txtHSumMoney.Caption.Trim()) ? txtHSumMoney.Caption.Trim() : "0.00") * Convert.ToDecimal(!string.IsNullOrEmpty(txtHTaxRate.Caption.Trim()) ? txtHTaxRate.Caption.Trim() : "0.00") / 100), 2).ToString("0.00");//工时税额
                            txtHSum.Caption = Math.Round((Convert.ToDecimal(!string.IsNullOrEmpty(txtHSumMoney.Caption.Trim()) ? txtHSumMoney.Caption.Trim() : "0.00") + Convert.ToDecimal(!string.IsNullOrEmpty(txtHTaxCost.Caption.Trim()) ? txtHTaxCost.Caption.Trim() : "0.00")), 2).ToString("0.00");//工时价税合计
                            txtShouldSum.Caption = Math.Round((Convert.ToDecimal(!string.IsNullOrEmpty(txtHSum.Caption.Trim()) ? txtHSum.Caption.Trim() : "0.00") + Convert.ToDecimal(!string.IsNullOrEmpty(txtFSum.Caption.Trim()) ? txtFSum.Caption.Trim() : "0.00") + Convert.ToDecimal(!string.IsNullOrEmpty(txtOSum.Caption.Trim()) ? txtOSum.Caption.Trim() : "0") - Convert.ToDecimal(!string.IsNullOrEmpty(txtPrivilegeCost.Caption.Trim()) ? txtPrivilegeCost.Caption.Trim() : "0.00")), 2).ToString("0.00");//应收总额
                            strShouldSum = Math.Round(Convert.ToDecimal(txtShouldSum.Caption),2).ToString("0.00");
                            dgvproject.Rows.Add(1);
                            break;
                        }
                    }
                }
                foreach (DataGridViewRow dgvr in dgvproject.Rows)
                {
                    string strPCode = CommonCtrl.IsNullToString(dgvr.Cells["item_no"].Value);
                    if (!string.IsNullOrEmpty(strPCode) && !listProject.Contains(strPCode))
                    {
                        listProject.Add(strPCode);
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 右键删除维修项目
        private void deleteProject_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvproject.RowCount; i++)
                {
                    DataGridViewRow dgvr = dgvproject.Rows[i];
                    object isCheck = dgvr.Cells["colCheck"].Value;
                    //将选中的删除
                    if (isCheck != null && (bool)isCheck)
                    {
                        string strItemId = CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["item_id"].Value);
                        string strItemNo = CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["item_no"].Value);
                        if (!string.IsNullOrEmpty(strItemNo))
                        {
                            if (!string.IsNullOrEmpty(strItemId))
                            {
                                List<string> listField = new List<string>();
                                Dictionary<string, string> comField = new Dictionary<string, string>();
                                listField.Add(strItemId);
                                comField.Add("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.DELETED).ToString());
                                DBHelper.BatchUpdateDataByIn("删除维修项目信息", "tb_maintain_item", comField, "item_id", listField.ToArray());
                                dgvproject.Rows.RemoveAt(i--);
                            }
                            else
                            {
                                dgvproject.Rows.RemoveAt(i--);
                            }
                            listProject.Remove(strItemNo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 右键添加维修用料
        private void addParts_Click(object sender, EventArgs e)
        {
            try
            {
                int intMrows = dgvMaterials.CurrentRow.Index;//当前行的索引
                frmParts frmPart = new frmParts();
                DialogResult result = frmPart.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string strPId = frmPart.PartsID;
                    DataTable dpt = DBHelper.GetTable("", "v_parts_chooser", "*", " parts_id='" + strPId + "'", "", "");
                    if (dpt.Rows.Count > 0)
                    {
                        DataRow dpr = dpt.Rows[0];
                        if (listMater.Contains(CommonCtrl.IsNullToString(dpr["ser_parts_code"])))
                        {
                            MessageBoxEx.Show("此维修用料已存在，请选择其他维修用料", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        for (int i = 0; i <= intMrows; i++)
                        {
                            if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["parts_code"].Value)))
                            {
                                dgvMaterials.Rows[i].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dpr["ser_parts_code"]);
                                dgvMaterials.Rows[i].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dpr["parts_name"]);
                                dgvMaterials.Rows[i].Cells["norms"].Value = CommonCtrl.IsNullToString(dpr["model"]);
                                dgvMaterials.Rows[i].Cells["unit"].Value = CommonCtrl.IsNullToString(dpr["unit"]);
                                dgvMaterials.Rows[i].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dpr["is_import"]) == "1" ? "是" : "否";
                                dgvMaterials.Rows[i].Cells["quantity"].Value = "1.0";
                                dgvMaterials.Rows[i].Cells["unit_price"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["ref_out_price"])) ? Math.Round(Convert.ToDecimal(CommonCtrl.IsNullToString(dpr["ref_out_price"])), 2).ToString("0.00") : "0.00";
                                string strPzk = !string.IsNullOrEmpty(labMemberLZkS.Text) ? labMemberLZkS.Text : "10";
                                //会员折扣
                                dgvMaterials.Rows[i].Cells["Mmember_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk) * 10);
                                string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["quantity"].Value)) ? Math.Round(Convert.ToDecimal(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["quantity"].Value)), 2).ToString("0.0") : "0.0";
                                string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["unit_price"].Value)) ? dgvMaterials.Rows[i].Cells["unit_price"].Value.ToString() : "0.00";
                                //会员单价
                                dgvMaterials.Rows[i].Cells["Mmember_price"].Value = Math.Round((Convert.ToDecimal(strUMoney = strUMoney == "" ? "0.00" : strUMoney) * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["Mmember_discount"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["Mmember_discount"].Value) : "0.00") / 10), 2).ToString("0.00");
                                dgvMaterials.Rows[i].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dpr["drawing_num"]);
                                dgvMaterials.Rows[i].Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dpr["remark"]);
                                dgvMaterials.Rows[i].Cells["vehicle_brand"].Value = CommonFuncCall.GetCarTypeForMa((CommonCtrl.IsNullToString(dpr["ser_parts_code"])));
                                dgvMaterials.Rows[i].Cells["parts_id"].Value = frmPart.PartsID;
                                dgvMaterials.Rows[i].Cells["Mthree_warranty"].Value = "否";
                                dcPmoney = 0;
                                foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
                                {
                                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["sum_money"].Value)))
                                    {
                                        dcPmoney += Convert.ToDecimal(dgvr.Cells["sum_money"].Value);
                                    }
                                }
                                txtFSumMoney.Caption = dcPmoney.ToString();
                                txtFTaxCost.Caption = Math.Round((Convert.ToDecimal(!string.IsNullOrEmpty(txtFSumMoney.Caption.Trim()) ? txtFSumMoney.Caption.Trim() : "0.00") * Convert.ToDecimal(!string.IsNullOrEmpty(txtFTaxRate.Caption.Trim()) ? txtFTaxRate.Caption.Trim() : "0.00") / 100), 2).ToString("0.00");//配件税额
                                txtFSum.Caption = Math.Round((Convert.ToDecimal(!string.IsNullOrEmpty(txtFSumMoney.Caption.Trim()) ? txtFSumMoney.Caption.Trim() : "0.00") + Convert.ToDecimal(!string.IsNullOrEmpty(txtFTaxCost.Caption.Trim()) ? txtFTaxCost.Caption.Trim() : "0.00")), 2).ToString("0.00");//配件价税合计
                                txtShouldSum.Caption = Math.Round((Convert.ToDecimal(!string.IsNullOrEmpty(txtHSum.Caption.Trim()) ? txtHSum.Caption.Trim() : "0.00") + Convert.ToDecimal(!string.IsNullOrEmpty(txtFSum.Caption.Trim()) ? txtFSum.Caption.Trim() : "0.00") + Convert.ToDecimal(!string.IsNullOrEmpty(txtOSum.Caption.Trim()) ? txtOSum.Caption.Trim() : "0") - Convert.ToDecimal(!string.IsNullOrEmpty(txtPrivilegeCost.Caption.Trim()) ? txtPrivilegeCost.Caption.Trim() : "0.00")), 2).ToString("0.00");//应收总额
                                strShouldSum = Math.Round(Convert.ToDecimal(txtShouldSum.Caption), 2).ToString("0.00");
                                dgvMaterials.Rows.Add(1);
                                break;
                            }
                        }
                    }
                }
                foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
                {
                    string strPCode = CommonCtrl.IsNullToString(dgvr.Cells["parts_code"].Value);
                    if (!string.IsNullOrEmpty(strPCode) && !listMater.Contains(strPCode))
                    {
                        listMater.Add(strPCode);
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 右键删除维修用料
        private void deleteParts_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvMaterials.RowCount; i++)
                {
                    DataGridViewRow dgvr = dgvMaterials.Rows[i];
                    object isCheck = dgvr.Cells["Mcheck"].Value;
                    //将选中的删除
                    if (isCheck != null && (bool)isCheck)
                    {
                        string strItemId = CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["material_id"].Value);
                        string strPartCode = CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["parts_code"].Value);
                        if (!string.IsNullOrEmpty(strItemId))
                        {
                            List<string> listField = new List<string>();
                            Dictionary<string, string> comField = new Dictionary<string, string>();
                            listField.Add(strItemId);
                            comField.Add("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.DELETED).ToString());
                            DBHelper.BatchUpdateDataByIn("删除维修用料", "tb_maintain_material_detail", comField, "material_id", listField.ToArray());
                            dgvMaterials.Rows.RemoveAt(i--);
                        }
                        else
                        {
                            dgvMaterials.Rows.RemoveAt(i--);
                        }
                        listMater.Remove(strPartCode);
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 限制手机号码只能数据数字
        private void txtContactPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationRegex.KeyPressInt(sender, e);
        }

        private void txtDriverPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationRegex.KeyPressInt(sender, e);
        }
        #endregion

        #region 点击页签上的叉号提示是否关闭当前窗体
        /// <summary>
        /// 点击页签上的叉号提示是否关闭当前窗体
        /// </summary>
        /// <returns></returns>
        public override bool CloseMenu()
        {
            if (isAutoClose)
            {
                return true;
            }
            if (MessageBoxEx.Show("确定要关闭当前窗体吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                return false;
            }
            else
            {
                if (!string.IsNullOrEmpty(strBefore_orderId))
                {
                    List<SQLObj> listSql = new List<SQLObj>();
                    UpdateMaintainInfo(listSql, strBefore_orderId, "", "0");
                    DBHelper.BatchExeSQLMultiByTrans("更新前置单据状体", listSql);
                }
            }
            return true;
        }
        #endregion
    }
}
