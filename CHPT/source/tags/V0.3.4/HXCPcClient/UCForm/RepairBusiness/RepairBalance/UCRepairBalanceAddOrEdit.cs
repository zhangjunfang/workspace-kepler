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
        #endregion

        #region 初始化窗体
        public UCRepairBalanceAddOrEdit()
        {
            InitializeComponent();
            GetRepairNo();
            initializeData();
            SetDgvAnchor();
            BindDepartment();
            BindBalanceWay();//结算方式
            BindAccount();//结算账户
            SetTopbuttonShow();
            CommonFuncCall.BindComBoxDataSource(cobMInvoiceType, "sys_receipt_type", "全部");//发票类型            
            base.CancelEvent += new ClickHandler(UCRepairBalanceAddOrEdit_CancelEvent);
            base.SaveEvent += new ClickHandler(UCRepairBalanceAddOrEdit_SaveEvent);
            base.BalanceEvent += new ClickHandler(UCRepairBalanceAddOrEdit_BalanceEvent);
            base.ImportEvent += new ClickHandler(UCRepairBalanceAddOrEdit_ImportEvent);

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
            DataTable dt = DBHelper.GetTable("", "tb_balance_way", "balance_way_id,balance_way_name,default_account", "", "", "");
            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem("", "全部"));
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new ListItem(dr["balance_way_id"], dr["balance_way_name"].ToString()));
            }
            cobBalanceType.DataSource = list;
            cobBalanceType.ValueMember = "Value";
            cobBalanceType.DisplayMember = "Text";
        }
        #endregion

        #region 绑定付款账户
        /// <summary>
        /// 绑定付款账户
        /// </summary>
        private void BindAccount()
        {
            DataTable dat = DBHelper.GetTable("", "v_cashier_account", "cashier_account,account_name,bank_name,bank_account", "", "", "");

            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem("", "全部"));
            foreach (DataRow dr in dat.Rows)
            {
                list.Add(new ListItem(dr["cashier_account"], dr["account_name"].ToString()));
            }
            cobSetAccount.DataSource = list;
            cobSetAccount.ValueMember = "Value";
            cobSetAccount.DisplayMember = "Text";
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
                #region 必要的判断
                if (string.IsNullOrEmpty(txtCarNO.Text.Trim()))
                {
                    MessageBoxEx.Show("车牌号不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (string.IsNullOrEmpty(txtCustomNO.Text.Trim()))
                {
                    MessageBoxEx.Show("客户编码不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (string.IsNullOrEmpty(txtCustomName.Caption.Trim()))
                {
                    MessageBoxEx.Show("客户名称不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (!ucAttr.CheckAttachment())
                {
                    return;
                }
                if (!string.IsNullOrEmpty(txtContactPhone.Caption.Trim()))//联系人手机
                {
                    if (!Validator.IsMobile(txtContactPhone.Caption.Trim()))
                    {
                        MessageBoxEx.Show("联系人手机号码格式错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (string.IsNullOrEmpty(txtDriver.Caption.Trim()))
                {
                    MessageBoxEx.Show("报修人不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (!string.IsNullOrEmpty(txtDriverPhone.Caption.Trim()))//报修人手机
                {
                    if (!Validator.IsMobile(txtDriverPhone.Caption.Trim()))
                    {
                        MessageBoxEx.Show("报修人手机号码格式错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    MessageBoxEx.Show("报修人手机不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobMInvoiceType.SelectedValue)))
                {
                    MessageBoxEx.Show("开票方式不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobBalanceType.SelectedValue)))
                {
                    MessageBoxEx.Show("结算方式不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobSetAccount.SelectedValue)))
                {
                    MessageBoxEx.Show("结算账户不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtBalanceCompany.Tag)))
                {
                    MessageBoxEx.Show("结算单位不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }

                if (string.IsNullOrEmpty(txtHSumMoney.Caption.Trim()))
                {
                    MessageBoxEx.Show("工时货款不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (string.IsNullOrEmpty(txtHTaxCost.Caption.Trim()))
                {
                    MessageBoxEx.Show("工时税额不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (string.IsNullOrEmpty(txtHSum.Caption.Trim()))
                {
                    MessageBoxEx.Show("工时价税合计不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (string.IsNullOrEmpty(txtFSumMoney.Caption.Trim()))
                {
                    MessageBoxEx.Show("配件货款不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (string.IsNullOrEmpty(txtFTaxCost.Caption.Trim()))
                {
                    MessageBoxEx.Show("配件税额不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (string.IsNullOrEmpty(txtFSum.Caption.Trim()))
                {
                    MessageBoxEx.Show("配件价税合计不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (string.IsNullOrEmpty(txtOSumMoney.Caption.Trim()))
                {
                    MessageBoxEx.Show("其他项目费用不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (string.IsNullOrEmpty(txtOTaxCost.Caption.Trim()))
                {
                    MessageBoxEx.Show("其他项目税额不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (string.IsNullOrEmpty(txtOSum.Caption.Trim()))
                {
                    MessageBoxEx.Show("其他项目价税合计不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                #endregion
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
                ucAttr.TableNameKeyValue = strValue;
                listSql.AddRange(ucAttr.AttachmentSql);
                if (DBHelper.BatchExeSQLMultiByTrans(opName, listSql))
                {
                    MessageBoxEx.Show("" + strMessage + "成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    uc.BindPageData();
                    deleteMenuByTag(this.Tag.ToString(), this.uc.Name);
                }
                else
                {
                    MessageBoxEx.Show("" + strMessage + "失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("" + strMessage + "失败！" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 取消事件
        void UCRepairBalanceAddOrEdit_CancelEvent(object sender, EventArgs e)
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
            deleteMenuByTag(this.Tag.ToString(), uc.Name);
        }
        #endregion

        #region 维修结算单基本信息保存
        /// <summary>
        /// 维修结算单基本信息保存
        /// </summary>
        private void SaveOrderInfo(List<SQLObj> listSql, DataSources.EnumAuditStatus status)
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
                    UpdateMaintainInfo(listSql, strBefore_orderId, labReserveInfo.Text.Trim(), "2");
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
                 ,responsible_name,org_name,enable_flag,info_status,maintain_no,maintain_id,create_by,create_name,create_time,orders_source)
                 values (@vehicle_no,@vehicle_vin,@engine_no,@vehicle_model,@vehicle_brand,@driver_name,@driver_mobile,@vehicle_color,@travel_mileage
                 ,@oil_into_factory,@maintain_mileage,@completion_time,@maintain_payment,@maintain_type,@remark,@favorable_reason,@customer_code,@customer_name,@linkman,@link_man_mobile,@responsible_opid
                 ,@responsible_name,@org_name,@enable_flag,@info_status,@maintain_no,@maintain_id,@create_by,@create_name,@create_time,@orders_source);";
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
                 ,responsible_name=@responsible_name,org_name=@org_name,enable_flag=@enable_flag,info_status=@info_status,maintain_no=@maintain_no,update_by=@update_by,update_name=@update_name,update_time=@update_time,orders_source=@orders_source where maintain_id=@maintain_id";
            }
            obj.Param = dicParam;
            listSql.Add(obj);

        }
        #endregion

        #region 维修结算单结算信息保存
        /// <summary>
        /// 维修结算单结算信息保存
        /// </summary>
        /// <param name="strOrderId">跟维修结算单基本信息关联的Id</param>
        private void SaveBalanceInfo(List<SQLObj> listSql, string strOrderId)
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
        #endregion

        #region 维修项目信息保存
        private void SaveProjectData(List<SQLObj> listSql, string partID)
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
                    string strPID = CommonCtrl.IsNullToString(dgvr.Cells["item_id"].Value);
                    if (strPID.Length == 0)
                    {
                        opName = "新增接待单维修项目";
                        strPID = Guid.NewGuid().ToString();
                        dicParam.Add("item_id", new ParamObj("item_id", strPID, SysDbType.VarChar, 40));
                        obj.sqlString = "insert into [tb_maintain_item] (item_id,maintain_id,item_no,item_type,item_name,man_hour_type,man_hour_quantity,man_hour_norm_unitprice,member_discount,member_price,member_sum_money,sum_money_goods,three_warranty,remarks,enable_flag) ";
                        obj.sqlString += " values (@item_id,@maintain_id,@item_no,@item_type,@item_name,@man_hour_type,@man_hour_quantity,@man_hour_norm_unitprice,@member_discount,@member_price,@member_sum_money,@sum_money_goods,@three_warranty,@remarks,@enable_flag);";
                    }
                    else
                    {
                        dicParam.Add("item_id", new ParamObj("item_id", dgvr.Cells["item_id"].Value, SysDbType.VarChar, 40));
                        opName = "更新接待单维修项目";
                        obj.sqlString = "update tb_maintain_item set item_no=@item_no,item_type=@item_type,item_name=@item_name,man_hour_type=@man_hour_type,man_hour_quantity=@man_hour_quantity,man_hour_norm_unitprice=@man_hour_norm_unitprice,member_discount=@member_discount,member_price=@member_price,";
                        obj.sqlString += " member_sum_money=@member_sum_money,sum_money_goods=@sum_money_goods,three_warranty=@three_warranty,remarks=@remarks,enable_flag=@enable_flag where item_id=@item_id";
                    }
                    obj.Param = dicParam;
                    listSql.Add(obj);
                }
            }
        }
        #endregion

        #region 维修用料信息保存
        private void SaveMaterialsData(List<SQLObj> listSql, string partID)
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
                    dicParam.Add("vehicle_brand", new ParamObj("vehicle_brand", dgvr.Cells["vehicle_brand"].Value, SysDbType.VarChar, 40));
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
                    string strPID = CommonCtrl.IsNullToString(dgvr.Cells["material_id"].Value);
                    if (strPID.Length == 0)
                    {
                        opName = "新增接待单维修用料";
                        strPID = Guid.NewGuid().ToString();
                        dicParam.Add("material_id", new ParamObj("material_id", strPID, SysDbType.VarChar, 40));
                        obj.sqlString = "insert into [tb_maintain_material_detail] (material_id,parts_code,parts_name,norms,unit,whether_imported,quantity,unit_price,sum_money,drawn_no,vehicle_brand,three_warranty,remarks,enable_flag,maintain_id) ";
                        obj.sqlString += " values (@material_id,@parts_code,@parts_name,@norms,@unit,@whether_imported,@quantity,@unit_price,@sum_money,@drawn_no,@vehicle_brand,@three_warranty,@remarks,@enable_flag,@maintain_id);";
                    }
                    else
                    {
                        dicParam.Add("material_id", new ParamObj("material_id", dgvr.Cells["material_id"].Value, SysDbType.VarChar, 40));
                        opName = "更新接待单维修用料";
                        obj.sqlString = "update tb_maintain_material_detail set parts_code=@parts_code,parts_name=@parts_name,norms=@norms,unit=@unit,whether_imported=@whether_imported,quantity=@quantity,unit_price=@unit_price,sum_money=@sum_money,";
                        obj.sqlString += "drawn_no=@drawn_no,vehicle_brand=@vehicle_brand, three_warranty=@three_warranty,remarks=@remarks,enable_flag=@enable_flag where material_id=@material_id";
                    }
                    obj.Param = dicParam;
                    listSql.Add(obj);
                }
            }
        }
        #endregion

        #region 其他收费项目信息保存
        //其他收费项目信息保存sql语句
        private void SaveOtherData(List<SQLObj> listSql, string partID)
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
                        opName = "新增接待单其他项目收费";
                        strTollID = Guid.NewGuid().ToString();
                        dicParam.Add("toll_id", new ParamObj("toll_id", strTollID, SysDbType.VarChar, 40));
                        obj.sqlString = "insert into [tb_maintain_other_toll] (toll_id,cost_types,sum_money,remarks,maintain_id,enable_flag) values (@toll_id,@cost_types,@sum_money,@remarks,@maintain_id,@enable_flag);";
                    }
                    else
                    {
                        dicParam.Add("toll_id", new ParamObj("toll_id", dgvr.Cells["toll_id"].Value, SysDbType.VarChar, 40));
                        opName = "更新接待单其他项目收费";
                        obj.sqlString = "update tb_maintain_other_toll set cost_types=@cost_types,sum_money=@sum_money,remarks=@remarks where toll_id=@toll_id";
                    }
                    obj.Param = dicParam;
                    listSql.Add(obj);
                }
            }
        }
        #endregion

        #region 初始化车辆品牌、颜色、维修付费方式、维修类别信息
        /// <summary>
        /// 初始化车型、车辆品牌、颜色、维修付费方式、维修类别信息
        /// </summary>
        private void initializeData()
        {
            CommonCtrl.BindComboBoxByDictionarr(cobCarBrand, "sys_vehicle_brand", true);//绑定车型品牌
            CommonCtrl.BindComboBoxByDictionarr(cobColor, "sys_vehicle_color", true);//绑定颜色
            CommonCtrl.BindComboBoxByDictionarr(cobPayType, "sys_repair_pay_methods", true);//绑定维修付费方式
            CommonCtrl.BindComboBoxByDictionarr(cobRepType, "sys_repair_category", true);//绑定维修类别            
        }
        #endregion

        #region 生成维修单号
        /// <summary>
        /// 生成预约单号&创建人姓名
        /// </summary>
        private void GetRepairNo()
        {
            labReserveNoS.Text = CommonUtility.GetNewNo(SYSModel.DataSources.EnumProjectType.Repair);
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
            ControlsConfig.NumberLimitdgv(dgvproject, new List<string>() { "Osum_money" });
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
            //dgvproject.Columns["man_hour_norm_unitprice"].ReadOnly = true;
            dgvproject.Columns["sum_money_goods"].ReadOnly = true;
            dgvproject.Columns["member_discount"].ReadOnly = true;
            dgvproject.Columns["member_price"].ReadOnly = true;
            dgvproject.Columns["member_sum_money"].ReadOnly = true;
            dgvproject.Columns["man_hour_type"].ReadOnly = true;
            //dgvproject.Columns["man_hour_quantity"].ReadOnly = true;
            ControlsConfig.NumberLimitdgv(dgvproject, new List<string>() { "man_hour_quantity" });
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
            ControlsConfig.NumberLimitdgv(dgvMaterials, new List<string>() { "quantity" });
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
            frmVehicleGrade frmVehicle = new frmVehicleGrade();
            DialogResult result = frmVehicle.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtCarNO.Text = frmVehicle.strLicensePlate;
                txtVIN.Caption = frmVehicle.strVIN;
                txtEngineNo.Caption = frmVehicle.strEngineNum;
                txtCarType.Text = GetDicName(frmVehicle.strModel);
                txtCarType.Tag = frmVehicle.strModel;
                cobCarBrand.SelectedValue = frmVehicle.strBrand;
                cobColor.SelectedValue = frmVehicle.strColor;
                txtCustomNO.Text = frmVehicle.strCustCode;
                txtCustomNO.Tag = frmVehicle.strCustId;
                txtCustomName.Caption = frmVehicle.strCustName;
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
            frmVehicleModels frmModels = new frmVehicleModels();
            DialogResult result = frmModels.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtCarType.Text = frmModels.VMName;
                txtCarType.Tag = frmModels.VMID;
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
            frmCustomerInfo frmCInfo = new frmCustomerInfo();
            DialogResult result = frmCInfo.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtCustomNO.Text = frmCInfo.strCustomerNo;
                txtCustomNO.Tag = frmCInfo.strCustomerId;
                txtCustomName.Caption = frmCInfo.strCustomerName;
                txtContact.Caption = frmCInfo.strLegalPerson;

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
            ControlsConfig.TextToDecimal(txtHTaxRate);
            ControlsConfig.TextToDecimal(txtFTaxRate);
            ControlsConfig.TextToDecimal(txtOTaxRate);
            ControlsConfig.TextToDecimal(txtMil);
            ControlsConfig.TextToDecimal(txtMl);
            ControlsConfig.TextToDecimal(txtSuggestMil);
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
                txtCarType.Text = GetDicName(CommonCtrl.IsNullToString(dr["vehicle_model"]));//车型          
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
            txtHSumMoney.Caption = CommonCtrl.IsNullToString(dr["man_hour_sum_money"]);//工时货款
            txtHTaxRate.Caption = CommonCtrl.IsNullToString(dr["man_hour_tax_rate"]);//工时税率
            txtHTaxCost.Caption = CommonCtrl.IsNullToString(dr["man_hour_tax_cost"]);//工时税额
            txtHSum.Caption = CommonCtrl.IsNullToString(dr["man_hour_sum"]);//工时价税合计
            txtFSumMoney.Caption = CommonCtrl.IsNullToString(dr["fitting_sum_money"]);//配件货款
            txtFTaxRate.Caption = CommonCtrl.IsNullToString(dr["fitting_tax_rate"]);//配件税率
            txtFTaxCost.Caption = CommonCtrl.IsNullToString(dr["fitting_tax_cost"]);//配件税额
            txtFSum.Caption = CommonCtrl.IsNullToString(dr["fitting_sum"]);//配件价税合计
            txtOSumMoney.Caption = CommonCtrl.IsNullToString(dr["other_item_sum_money"]);//其他项目费用
            txtOTaxRate.Caption = CommonCtrl.IsNullToString(dr["other_item_tax_rate"]);//其他项目税率
            txtOTaxCost.Caption = CommonCtrl.IsNullToString(dr["other_item_tax_cost"]);//其他项目税额
            txtOSum.Caption = CommonCtrl.IsNullToString(dr["other_item_sum"]);//其他项目价税合计
            txtPrivilegeCost.Caption = CommonCtrl.IsNullToString(dr["privilege_cost"]);//优惠费用
            txtShouldSum.Caption = CommonCtrl.IsNullToString(dr["should_sum"]);//应收总额
            strShouldSum = CommonCtrl.IsNullToString(dr["should_sum"]);//应收总额
            txtReceivedSum.Caption = CommonCtrl.IsNullToString(dr["received_sum"]);//实收总额
            txtDebtCost.Caption = CommonCtrl.IsNullToString(dr["debt_cost"]);//本次欠款金额
            cobMInvoiceType.SelectedValue = CommonCtrl.IsNullToString(dr["make_invoice_type"]);//开票类型
            cobBalanceType.SelectedValue = CommonCtrl.IsNullToString(dr["payment_terms"]);//结算方式
            cobSetAccount.SelectedValue = CommonCtrl.IsNullToString(dr["settlement_account"]);//结算账户
            txtBalanceCompany.Text = CommonCtrl.IsNullToString(dr["settle_company"]);//结算单位
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
            strBefore_orderId = strId;
            #endregion

            #region 底部datagridview数据
            BindPData(strId);
            BindMData(strId);
            BindOData(strId);
            BindAData("tb_maintain_info", strId);
            #endregion
        }
        #endregion

        #region 底部datagridview数据信息绑定

        #region 维修项目数据
        private void BindPData(string strOrderId)
        {

            //维修项目数据                
            DataTable dpt = DBHelper.GetTable("维修项目数据", "tb_maintain_item", "*", string.Format(" maintain_id='{0}' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", strOrderId), "", "");
            if (dpt.Rows.Count > 0)
            {
                if (dpt.Rows.Count > dgvproject.Rows.Count)
                {
                    dgvproject.Rows.Add(dpt.Rows.Count - dgvproject.Rows.Count + 1);
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
                    dgvproject.Rows[i].Cells["man_hour_quantity"].Value = CommonCtrl.IsNullToString(dpr["man_hour_quantity"]);
                    dgvproject.Rows[i].Cells["man_hour_norm_unitprice"].Value = CommonCtrl.IsNullToString(dpr["man_hour_norm_unitprice"]);
                    dgvproject.Rows[i].Cells["remarks"].Value = CommonCtrl.IsNullToString(dpr["remarks"]);
                    dgvproject.Rows[i].Cells["sum_money_goods"].Value = CommonCtrl.IsNullToString(dpr["sum_money_goods"]);
                    dgvproject.Rows[i].Cells["member_discount"].Value = CommonCtrl.IsNullToString(dpr["member_discount"]);
                    dgvproject.Rows[i].Cells["member_price"].Value = CommonCtrl.IsNullToString(dpr["member_price"]);
                    dgvproject.Rows[i].Cells["member_sum_money"].Value = CommonCtrl.IsNullToString(dpr["member_sum_money"]);
                    listProject.Add(CommonCtrl.IsNullToString(dpr["item_no"]));
                }
            }
        }
        #endregion

        #region 维修用料数据
        private void BindMData(string strOrderId)
        {

            //维修用料数据
            DataTable dmt = DBHelper.GetTable("维修用料数据", "tb_maintain_material_detail", "*", string.Format(" maintain_id='{0}' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", strOrderId), "", "");
            if (dmt.Rows.Count > 0)
            {
                if (dmt.Rows.Count > dgvMaterials.Rows.Count)
                {
                    dgvMaterials.Rows.Add(dmt.Rows.Count - dgvMaterials.Rows.Count + 1);
                }
                for (int i = 0; i < dmt.Rows.Count; i++)
                {
                    DataRow dmr = dmt.Rows[i];
                    dgvMaterials.Rows[i].Cells["material_id"].Value = CommonCtrl.IsNullToString(dmr["material_id"]);
                    dgvMaterials.Rows[i].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dmr["parts_code"]);
                    dgvMaterials.Rows[i].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dmr["parts_name"]);
                    dgvMaterials.Rows[i].Cells["norms"].Value = CommonCtrl.IsNullToString(dmr["norms"]);
                    dgvMaterials.Rows[i].Cells["unit"].Value = CommonCtrl.IsNullToString(dmr["unit"]);
                    dgvMaterials.Rows[i].Cells["quantity"].Value = CommonCtrl.IsNullToString(dmr["quantity"]);
                    dgvMaterials.Rows[i].Cells["unit_price"].Value = CommonCtrl.IsNullToString(dmr["unit_price"]);
                    dgvMaterials.Rows[i].Cells["Mmember_discount"].Value = CommonCtrl.IsNullToString(dmr["member_discount"]);
                    dgvMaterials.Rows[i].Cells["Mmember_price"].Value = CommonCtrl.IsNullToString(dmr["member_price"]);
                    dgvMaterials.Rows[i].Cells["sum_money"].Value = CommonCtrl.IsNullToString(dmr["sum_money"]);
                    dgvMaterials.Rows[i].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dmr["drawn_no"]);
                    dgvMaterials.Rows[i].Cells["vehicle_brand"].Value = CommonCtrl.IsNullToString(dmr["vehicle_brand"]);
                    dgvMaterials.Rows[i].Cells["Mthree_warranty"].Value = CommonCtrl.IsNullToString(dmr["three_warranty"]) == "1" ? "是" : "否";
                    dgvMaterials.Rows[i].Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dmr["remarks"]);
                    dgvMaterials.Rows[i].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dmr["whether_imported"]) == "1" ? "是" : "否";
                    listMater.Add(CommonCtrl.IsNullToString(dmr["parts_code"]));
                }
            }
        }
        #endregion

        #region 其他项目收费数据
        private void BindOData(string strOrderId)
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
                    dgvOther.Rows[i].Cells["Osum_money"].Value = CommonCtrl.IsNullToString(dor["sum_money"]);
                    dgvOther.Rows[i].Cells["Oremarks"].Value = CommonCtrl.IsNullToString(dor["remarks"]);
                    dgvOther.Rows[i].Cells["cost_types"].Value = CommonCtrl.IsNullToString(dor["cost_types"]);
                }
            }
        }
        #endregion

        #region 附件信息数据
        private void BindAData(string strTableName, string strOrderId)
        {
            //附件信息数据
            ucAttr.TableName = strTableName;
            ucAttr.TableNameKeyValue = strOrderId;
            ucAttr.BindAttachment();
        }
        #endregion

        #endregion

        #region 绑定部门
        /// <summary>
        /// 绑定部门
        /// </summary>
        private void BindDepartment()
        {
            DataTable dt = DBHelper.GetTable("", "tb_organization", "org_id,org_name", "", "", "");
            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem("", "全部"));
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new ListItem(dr["org_id"], dr["org_name"].ToString()));
            }
            cboOrgId.DataSource = list;
            cboOrgId.ValueMember = "Value";
            cboOrgId.DisplayMember = "Text";
        }
        #endregion

        #region 绑定经办人
        /// <summary>
        /// 绑定经办人
        /// </summary>
        /// <param name="strDepartId">部门Id</param>
        private void BindHandle(ComboBox cobox, string strDepartId = null)
        {
            string strWhere = !string.IsNullOrEmpty(strDepartId) ? string.Format("org_id='{0}'", strDepartId) : "";
            DataTable dt = DBHelper.GetTable("", "sys_user", "user_id,user_name", strWhere, "", "");
            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem("", "全部"));
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new ListItem(dr["user_id"], dr["user_name"].ToString()));
            }
            cobox.DataSource = list;
            cobox.ValueMember = "Value";
            cobox.DisplayMember = "Text";
        }
        #endregion

        #region 根据部门的选择绑定经办人
        /// <summary>
        /// 根据部门的选择绑定经办人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboOrgId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboOrgId.SelectedValue == null)
            {
                return;
            }
            BindHandle(cobYHandle, cboOrgId.SelectedValue.ToString());
        }
        #endregion

        #region 维修用料信息读取
        private void dgvMaterials_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3)
            {
                dcPmoney = 0;
                frmParts frmPart = new frmParts();
                DialogResult result = frmPart.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string strPId = frmPart.PartsID;
                    DataTable dpt = DBHelper.GetTable("", "v_parts", "*", " parts_id='" + strPId + "'", "", "");
                    if (dpt.Rows.Count > 0)
                    {
                        DataRow dpr = dpt.Rows[0];
                        dgvMaterials.Rows[e.RowIndex].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dpr["ser_parts_code"]);
                        dgvMaterials.Rows[e.RowIndex].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dpr["parts_name"]);
                        dgvMaterials.Rows[e.RowIndex].Cells["norms"].Value = CommonCtrl.IsNullToString(dpr["model"]);
                        dgvMaterials.Rows[e.RowIndex].Cells["unit"].Value = CommonCtrl.IsNullToString(dpr["default_unit_name"]);
                        dgvMaterials.Rows[e.RowIndex].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dpr["is_import"]) == "1" ? "是" : "否";
                        dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value = "1";
                        dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value = CommonCtrl.IsNullToString(dpr["highest_out_price"]);
                        string strPzk = !string.IsNullOrEmpty(labMemberLZkS.Text) ? labMemberLZkS.Text : "10";
                        //会员折扣
                        dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk) * 10);
                        string strNum = dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value.ToString();
                        string strUMoney = dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value != null ? dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value.ToString() : "0";
                        //会员单价
                        dgvMaterials.Rows[e.RowIndex].Cells["Mmember_price"].Value = Convert.ToString(Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value) / 10);
                        dgvMaterials.Rows[e.RowIndex].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dpr["drawing_num"]);
                        dgvMaterials.Rows[e.RowIndex].Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dpr["remark"]);
                        dgvMaterials.Rows[e.RowIndex].Cells["vehicle_brand"].Value = CommonCtrl.IsNullToString(dpr["v_brand_name"]);
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
        /// <summary>
        /// 数量、原始单价发生改变时金额也跟着改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvMaterials_CellEndEdit(object sender, DataGridViewCellEventArgs e)
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
        #endregion

        #region 结算单位选择器事件
        private void txtBalanceCompany_ChooserClick(object sender, EventArgs e)
        {
            frmCustomerInfo frmCInfo = new frmCustomerInfo();
            DialogResult result = frmCInfo.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtBalanceCompany.Text = frmCInfo.strCustomerName;
                txtBalanceCompany.Tag = frmCInfo.strCustomerId;
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
            string strHMoney = !string.IsNullOrEmpty(txtM.Caption.Trim()) ? txtM.Caption.Trim() : "0";
            string strHRate = !string.IsNullOrEmpty(txtR.Caption.Trim()) ? txtR.Caption.Trim() : "0";
            txtC.Caption = (Convert.ToDecimal(strHMoney) * Convert.ToDecimal(strHRate)/100).ToString();
            txtS.Caption = (Convert.ToDecimal(strHMoney) + Convert.ToDecimal(txtC.Caption.Trim())).ToString();
            txtShouldSum.Caption = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(txtHSum.Caption.Trim()) ? txtHSum.Caption.Trim() : "0") + Convert.ToDecimal(!string.IsNullOrEmpty(txtFSum.Caption.Trim()) ? txtFSum.Caption.Trim() : "0") + Convert.ToDecimal(!string.IsNullOrEmpty(txtOSum.Caption.Trim()) ? txtOSum.Caption.Trim() : "0") - Convert.ToDecimal(!string.IsNullOrEmpty(txtPrivilegeCost.Caption.Trim()) ? txtPrivilegeCost.Caption.Trim() : "0"));//应收总额
            strShouldSum = txtShouldSum.Caption = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(txtHSum.Caption.Trim()) ? txtHSum.Caption.Trim() : "0") + Convert.ToDecimal(!string.IsNullOrEmpty(txtFSum.Caption.Trim()) ? txtFSum.Caption.Trim() : "0") + Convert.ToDecimal(!string.IsNullOrEmpty(txtOSum.Caption.Trim()) ? txtOSum.Caption.Trim() : "0") - Convert.ToDecimal(!string.IsNullOrEmpty(txtPrivilegeCost.Caption.Trim()) ? txtPrivilegeCost.Caption.Trim() : "0"));//应收总额
            //本次欠款金额
            string Smoney = !string.IsNullOrEmpty(txtReceivedSum.Caption) ? txtReceivedSum.Caption : "0";//实收总额
            string Ymoney = !string.IsNullOrEmpty(txtShouldSum.Caption) ? txtShouldSum.Caption : "0";//应收总额
            if (Convert.ToDecimal(Smoney) > Convert.ToDecimal(Ymoney))
            {
                MessageBoxEx.Show("实收总额不能大于应收总额!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                txtReceivedSum.Caption = string.Empty;
                return;
            }
            txtDebtCost.Caption = (Convert.ToDecimal(Ymoney) - Convert.ToDecimal(Smoney)).ToString();
        }
        #endregion

        #region 维修单导入功能
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
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
        #endregion

        #region 维修结算单导入-根据前置单据绑定维修信息
        /// <summary>
        /// 维修结算单导入-根据前置单据绑定维修信息
        /// </summary>
        public void BindSetmentData()
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
            txtVIN.Caption = CommonCtrl.IsNullToString(dr["vehicle_vin"]);//VIN
            txtEngineNo.Caption = CommonCtrl.IsNullToString(dr["engine_no"]);//发动机号
            txtCarType.Text = CommonCtrl.IsNullToString(dr["vehicle_model"]);//车型          
            cobCarBrand.SelectedValue = CommonCtrl.IsNullToString(dr["vehicle_brand"]);//车辆品牌
            txtDriver.Caption = CommonCtrl.IsNullToString(dr["driver_name"]);//司机
            txtDriverPhone.Caption = CommonCtrl.IsNullToString(dr["driver_mobile"]);//司机手机
            cobColor.SelectedValue = CommonCtrl.IsNullToString(dr["vehicle_color"]);//颜色           
            txtCustomNO.Text = CommonCtrl.IsNullToString(dr["customer_code"]);//客户编码
            txtCustomName.Caption = CommonCtrl.IsNullToString(dr["customer_name"]);//客户名称  
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
            txtContactPhone.Caption = CommonCtrl.IsNullToString(dr["link_man_mobile"]);//联系人手机  
            #endregion

            #region 底部datagridview数据
            BindPData(strBefore_orderId);
            BindMData(strBefore_orderId);
            BindOData(strBefore_orderId);
            BindAData("tb_maintain_reservation", strBefore_orderId);
            List<SQLObj> listSql = new List<SQLObj>();
            UpdateMaintainInfo(listSql, strBefore_orderId, "", "1");
            DBHelper.BatchExeSQLMultiByTrans("更新前置单据状体", listSql);
            #endregion
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
            string strYmoney = !string.IsNullOrEmpty(txtPrivilegeCost.Caption.Trim()) ? txtPrivilegeCost.Caption.Trim() : "0";//优惠金额
            string strSmoney = !string.IsNullOrEmpty(txtShouldSum.Caption.Trim()) ? txtShouldSum.Caption.Trim() : "0";//应收金额
            if (Convert.ToDecimal(strYmoney) > Convert.ToDecimal(strYmoney))
            {
                MessageBoxEx.Show("优惠费用不能大于应收总额!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            txtShouldSum.Caption = (Convert.ToDecimal(!string.IsNullOrEmpty(strShouldSum) ? strShouldSum : "0") - Convert.ToDecimal(!string.IsNullOrEmpty(txtPrivilegeCost.Caption) ? txtPrivilegeCost.Caption : "0")).ToString();
            //本次欠款金额
            string Smoney = !string.IsNullOrEmpty(txtReceivedSum.Caption) ? txtReceivedSum.Caption : "0";//实收总额
            string Ymoney = !string.IsNullOrEmpty(txtShouldSum.Caption) ? txtShouldSum.Caption : "0";//应收总额
            if (Convert.ToDecimal(Smoney) > Convert.ToDecimal(Ymoney))
            {
                MessageBoxEx.Show("实收总额不能大于应收总额!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                txtReceivedSum.Caption = string.Empty;
                return;
            }
            txtDebtCost.Caption = (Convert.ToDecimal(Ymoney) - Convert.ToDecimal(Smoney)).ToString();

        }
        #endregion

         #region 实收总额添加后本次欠款金额发生变化
        private void txtReceivedSum_UserControlValueChanged(object sender, EventArgs e)
        {
            string Smoney = !string.IsNullOrEmpty(txtReceivedSum.Caption) ? txtReceivedSum.Caption : "0";//实收总额
            string Ymoney = !string.IsNullOrEmpty(txtShouldSum.Caption) ? txtShouldSum.Caption : "0";//应收总额
            if (Convert.ToDecimal(Smoney) > Convert.ToDecimal(Ymoney))
            {
                MessageBoxEx.Show("实收总额不能大于应收总额!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                txtReceivedSum.Caption = string.Empty;
                return;
            }
            txtDebtCost.Caption = (Convert.ToDecimal(Ymoney) - Convert.ToDecimal(Smoney)).ToString();
        }
         #endregion

        #region 维修项目信息读取
        private void dgvproject_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow dgvr in dgvproject.Rows)
            {
                string strPCode = CommonCtrl.IsNullToString(dgvr.Cells["item_no"].Value);
                if (!string.IsNullOrEmpty(strPCode))
                {
                    listProject.Add(strPCode);
                }
            }
            frmWorkHours frmHours = new frmWorkHours();
            DialogResult result = frmHours.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (listProject.Contains(frmHours.strProjectNum))
                {
                    MessageBoxEx.Show("此维修项目已存在，请选择其他维修项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }     
                dgvproject.Rows[e.RowIndex].Cells["item_no"].Value = frmHours.strProjectNum;
                dgvproject.Rows[e.RowIndex].Cells["item_type"].Value = GetDicName(frmHours.strRepairType);
                dgvproject.Rows[e.RowIndex].Cells["item_name"].Value = frmHours.strProjectName;
                dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value = frmHours.strQuotaPrice;
                dgvproject.Rows[e.RowIndex].Cells["remarks"].Value = frmHours.strRemark;
                dgvproject.Rows[e.RowIndex].Cells["man_hour_type"].Value = frmHours.strWhoursType == "1" ? "工时" : "定额";
                string strPzk = !string.IsNullOrEmpty(labMemberPZkS.Text) ? labMemberPZkS.Text : "10";
                //会员折扣
                dgvproject.Rows[e.RowIndex].Cells["member_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk) * 10);
                //会员工时费
                dgvproject.Rows[e.RowIndex].Cells["member_price"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strQuotaPrice)?frmHours.strQuotaPrice:"0") * (Convert.ToDecimal(strPzk) / 10));
                if (CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["member_price"].Value) != "0")
                {
                    //折扣额
                    dgvproject.Rows[e.RowIndex].Cells["member_sum_money"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strQuotaPrice) ? frmHours.strQuotaPrice : "0") - Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["member_price"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["member_price"].Value) : "0"));
                }
                else
                {
                    dgvproject.Rows[e.RowIndex].Cells["member_sum_money"].Value = "0";
                }
                dgvproject.Rows[e.RowIndex].Cells["OldItem_id"].Value = frmHours.strWhours_id;
                dgvproject.Rows[e.RowIndex].Cells["man_hour_quantity"].Value = frmHours.strWhoursNum;
                dgvproject.Rows[e.RowIndex].Cells["sum_money_goods"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strWhoursNum)?frmHours.strWhoursNum:"0") * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["member_price"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["member_price"].Value) : "0"));
                dcHmoney = 0;
                foreach (DataGridViewRow dgvr in dgvproject.Rows)
                {
                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["sum_money_goods"].Value)))
                    {
                        dcHmoney += Convert.ToDecimal(dgvr.Cells["sum_money_goods"].Value);
                    }
                }
                txtHSumMoney.Caption = dcHmoney.ToString();//工时货款
                txtHTaxCost.Caption =Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(txtHSumMoney.Caption.Trim()) ? txtHSumMoney.Caption.Trim() : "0") * Convert.ToDecimal(!string.IsNullOrEmpty(txtHTaxRate.Caption.Trim())?txtHTaxRate.Caption.Trim():"0"));//工时税额
                txtHSum.Caption = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(txtHSumMoney.Caption.Trim()) ? txtHSumMoney.Caption.Trim() : "0") + Convert.ToDecimal(!string.IsNullOrEmpty(txtHTaxCost.Caption.Trim()) ? txtHTaxCost.Caption.Trim() : "0"));//工时价税合计
                txtShouldSum.Caption = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(txtHSum.Caption.Trim()) ? txtHSum.Caption.Trim() : "0") + Convert.ToDecimal(!string.IsNullOrEmpty(txtFSum.Caption.Trim()) ? txtFSum.Caption.Trim() : "0") + Convert.ToDecimal(!string.IsNullOrEmpty(txtOSum.Caption.Trim()) ? txtOSum.Caption.Trim() : "0") - Convert.ToDecimal(!string.IsNullOrEmpty(txtPrivilegeCost.Caption.Trim()) ? txtPrivilegeCost.Caption.Trim() : "0"));//应收总额
                dgvproject.Rows.Add(1);
            }
        }
        /// <summary>
        /// 工时数量发生改变时货款也跟着改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>      
        private void dgvproject_CellValueChanged(object sender, DataGridViewCellEventArgs e)
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
                string strUnitprice = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value) : "0";

                if (e.ColumnIndex == 5 || e.ColumnIndex == 6)
                {
                    //会员工时费
                    dgvproject.Rows[e.RowIndex].Cells["member_price"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["member_discount"].Value))?CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["member_discount"].Value):"0") * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value))?CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value):"0") / 100);
                    string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["member_price"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["member_price"].Value) : "0";
                    if (CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["member_price"].Value) != "0")
                    {
                        //折扣额
                        dgvproject.Rows[e.RowIndex].Cells["member_sum_money"].Value = Convert.ToString(Convert.ToDecimal(strUnitprice) - Convert.ToDecimal(strUMoney));
                        //货款
                        dgvproject.Rows[e.RowIndex].Cells["sum_money_goods"].Value = Convert.ToString(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney));
                    }
                    else
                    {
                        dgvproject.Rows[e.RowIndex].Cells["sum_money_goods"].Value = Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value))?CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value):"0");
                    }

                }
                if (e.ColumnIndex == 11)
                {
                    if (CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_type"].Value) == "工时")
                    {
                        //工时时设置数量和金额均可修改
                        dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].ReadOnly = strIsThree == "否" ? false : true;
                    }
                    dgvproject.Rows[e.RowIndex].Cells["man_hour_quantity"].ReadOnly = strIsThree == "否" ? false : true;
                    if (strIsThree == "是")
                    {
                        string strOId = CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["OldItem_id"].Value);
                        if (!string.IsNullOrEmpty(strOId))
                        {
                            DataTable dot = DBHelper.GetTable("", "v_workhours_users", "*", "whours_id='" + strOId + "'", "", "");
                            if (dot.Rows.Count > 0)
                            {
                                DataRow dpr = dot.Rows[0];
                                dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value = CommonCtrl.IsNullToString(dpr["quota_price"]);
                                dgvproject.Rows[e.RowIndex].Cells["sum_money_goods"].Value = Convert.ToString(Convert.ToDecimal(strNum) * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["quota_price"])) ? CommonCtrl.IsNullToString(dpr["quota_price"]) : "0"));
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
            txtHSumMoney.Caption = dcHmoney.ToString();//工时货款
            txtHTaxCost.Caption = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(txtHSumMoney.Caption.Trim()) ? txtHSumMoney.Caption.Trim() : "0") * Convert.ToDecimal(!string.IsNullOrEmpty(txtHTaxRate.Caption.Trim()) ? txtHTaxRate.Caption.Trim() : "0"));//工时税额
            txtHSum.Caption = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(txtHSumMoney.Caption.Trim()) ? txtHSumMoney.Caption.Trim() : "0") + Convert.ToDecimal(!string.IsNullOrEmpty(txtHTaxCost.Caption.Trim()) ? txtHTaxCost.Caption.Trim() : "0"));//工时价税合计
            txtShouldSum.Caption = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(txtHSum.Caption.Trim()) ? txtHSum.Caption.Trim() : "0") + Convert.ToDecimal(!string.IsNullOrEmpty(txtFSum.Caption.Trim()) ? txtFSum.Caption.Trim() : "0") + Convert.ToDecimal(!string.IsNullOrEmpty(txtOSum.Caption.Trim()) ? txtOSum.Caption.Trim() : "0") - Convert.ToDecimal(!string.IsNullOrEmpty(txtPrivilegeCost.Caption.Trim()) ? txtPrivilegeCost.Caption.Trim() : "0"));//应收总额
        }
        #endregion

        #region 维修用料信息读取
        private void dgvMaterials_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
            {
                string strPCode = CommonCtrl.IsNullToString(dgvr.Cells["parts_code"].Value);
                if (!string.IsNullOrEmpty(strPCode))
                {
                    listMater.Add(strPCode);
                }
            }
            frmParts frmPart = new frmParts();
            DialogResult result = frmPart.ShowDialog();
            if (result == DialogResult.OK)
            {
                string strPId = frmPart.PartsID;
                DataTable dpt = DBHelper.GetTable("", "v_parts", "*", " parts_id='" + strPId + "'", "", "");
                if (dpt.Rows.Count > 0)
                {
                    DataRow dpr = dpt.Rows[0];
                    if (listMater.Contains(CommonCtrl.IsNullToString(dpr["ser_parts_code"])))
                    {
                        MessageBoxEx.Show("此维修用料已存在，请选择其他维修用料", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }   
                    dgvMaterials.Rows[e.RowIndex].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dpr["ser_parts_code"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dpr["parts_name"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["norms"].Value = CommonCtrl.IsNullToString(dpr["model"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["unit"].Value = CommonCtrl.IsNullToString(dpr["default_unit_name"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dpr["is_import"]) == "1" ? "是" : "否";
                    dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value = "1";
                    dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value = CommonCtrl.IsNullToString(dpr["highest_out_price"]);
                    string strPzk = !string.IsNullOrEmpty(labMemberLZkS.Text) ? labMemberLZkS.Text : "10";
                    //会员折扣
                    dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk) * 10);
                    string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value))?CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value):"0";
                    string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value)) ? dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value.ToString() : "0";
                    //会员单价
                    dgvMaterials.Rows[e.RowIndex].Cells["Mmember_price"].Value = Convert.ToString(Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value))?CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value):"0") / 10);
                    dgvMaterials.Rows[e.RowIndex].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dpr["drawing_num"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dpr["remark"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["vehicle_brand"].Value = CommonCtrl.IsNullToString(dpr["v_brand_name"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["M_Id"].Value = frmPart.PartsID;
                }
                dcPmoney = 0;
                foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
                {
                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["sum_money"].Value)))
                    {
                        dcPmoney += Convert.ToDecimal(dgvr.Cells["sum_money"].Value);
                    }
                }
                txtFSumMoney.Caption = dcPmoney.ToString();
                txtFTaxCost.Caption = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(txtFSumMoney.Caption.Trim()) ? txtFSumMoney.Caption.Trim() : "0") * Convert.ToDecimal(!string.IsNullOrEmpty(txtFTaxRate.Caption.Trim()) ? txtFTaxRate.Caption.Trim() : "0"));//配件税额
                txtFSum.Caption = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(txtFSumMoney.Caption.Trim()) ? txtFSumMoney.Caption.Trim() : "0") + Convert.ToDecimal(!string.IsNullOrEmpty(txtFTaxCost.Caption.Trim()) ? txtFTaxCost.Caption.Trim() : "0"));//配件价税合计
                txtShouldSum.Caption = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(txtHSum.Caption.Trim()) ? txtHSum.Caption.Trim() : "0") + Convert.ToDecimal(!string.IsNullOrEmpty(txtFSum.Caption.Trim()) ? txtFSum.Caption.Trim() : "0") + Convert.ToDecimal(!string.IsNullOrEmpty(txtOSum.Caption.Trim()) ? txtOSum.Caption.Trim() : "0") - Convert.ToDecimal(!string.IsNullOrEmpty(txtPrivilegeCost.Caption.Trim()) ? txtPrivilegeCost.Caption.Trim() : "0"));//应收总额
                dgvMaterials.Rows.Add(1);
            }
        }
        /// <summary>
        /// 数量、原始单价发生改变时金额也跟着改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvMaterials_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            string strIsThree = CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mthree_warranty"].Value);
            if (dgvMaterials.Rows[e.RowIndex].Cells["parts_name"].Value != null)
            {
                if (e.ColumnIndex == 6 || e.ColumnIndex == 7)
                {
                    //if (strIsThree == "否")
                    //{
                    ControlsConfig.SetCellsValue(dgvMaterials, e.RowIndex, "quantity,unit_price");
                    string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value))? dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value.ToString() : "0";
                    string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value))? dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value.ToString() : "0";
                    string strzk = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value))? dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value.ToString() : "0";
                    dgvMaterials.Rows[e.RowIndex].Cells["Mmember_price"].Value = Convert.ToString(Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(strzk.Length > 0 ? strzk : "0") / 100);
                    if (CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_price"].Value) != "0")
                    {
                        strzk =  !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value))?CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value):"0";
                        dgvMaterials.Rows[e.RowIndex].Cells["sum_money"].Value = Convert.ToString(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(strzk) / 100);
                    }
                    else
                    {
                        dgvMaterials.Rows[e.RowIndex].Cells["sum_money"].Value = Convert.ToString(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney));
                    }
                    //}
                }
                if (e.ColumnIndex == 13)
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
                        string strMId = CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["M_Id"].Value);
                        DataTable dpt = DBHelper.GetTable("", "v_parts", "*", " parts_id='" + strMId + "'", "", "");
                        if (dpt.Rows.Count > 0)
                        {
                            DataRow dpr = dpt.Rows[0];
                            dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value = CommonCtrl.IsNullToString(dpr["highest_out_price"]);
                            string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value) : "0";
                            string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value) : "0";
                            dgvMaterials.Rows[e.RowIndex].Cells["sum_money"].Value = Convert.ToString(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney));
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
            txtFSumMoney.Caption = dcPmoney.ToString();
            txtFTaxCost.Caption = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(txtFSumMoney.Caption.Trim()) ? txtFSumMoney.Caption.Trim() : "0") * Convert.ToDecimal(!string.IsNullOrEmpty(txtFTaxRate.Caption.Trim()) ? txtFTaxRate.Caption.Trim() : "0"));//配件税额
            txtFSum.Caption = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(txtFSumMoney.Caption.Trim()) ? txtFSumMoney.Caption.Trim() : "0") + Convert.ToDecimal(!string.IsNullOrEmpty(txtFTaxCost.Caption.Trim()) ? txtFTaxCost.Caption.Trim() : "0"));//配件价税合计
            txtShouldSum.Caption = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(txtHSum.Caption.Trim()) ? txtHSum.Caption.Trim() : "0") + Convert.ToDecimal(!string.IsNullOrEmpty(txtFSum.Caption.Trim()) ? txtFSum.Caption.Trim() : "0") + Convert.ToDecimal(!string.IsNullOrEmpty(txtOSum.Caption.Trim()) ? txtOSum.Caption.Trim() : "0") - Convert.ToDecimal(!string.IsNullOrEmpty(txtPrivilegeCost.Caption.Trim()) ? txtPrivilegeCost.Caption.Trim() : "0"));//应收总额
        }
        #endregion

         #region 其他项目收费-获取其他项目费用
        private void dgvOther_CellValueChanged(object sender, DataGridViewCellEventArgs e)
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
            txtOSumMoney.Caption = dcOmoney.ToString();
            txtOTaxCost.Caption = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(txtOSumMoney.Caption.Trim()) ? txtOSumMoney.Caption.Trim() : "0") * Convert.ToDecimal(!string.IsNullOrEmpty(txtOTaxRate.Caption.Trim()) ? txtOTaxRate.Caption.Trim() : "0"));//配件税额
            txtOSum.Caption = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(txtOSumMoney.Caption.Trim()) ? txtOSumMoney.Caption.Trim() : "0") + Convert.ToDecimal(!string.IsNullOrEmpty(txtOTaxCost.Caption.Trim()) ? txtOTaxCost.Caption.Trim() : "0"));//配件价税合计
            txtShouldSum.Caption = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(txtHSum.Caption.Trim()) ? txtHSum.Caption.Trim() : "0") + Convert.ToDecimal(!string.IsNullOrEmpty(txtFSum.Caption.Trim()) ? txtFSum.Caption.Trim() : "0") + Convert.ToDecimal(!string.IsNullOrEmpty(txtOSum.Caption.Trim()) ? txtOSum.Caption.Trim() : "0") - Convert.ToDecimal(!string.IsNullOrEmpty(txtPrivilegeCost.Caption.Trim()) ? txtPrivilegeCost.Caption.Trim() : "0"));//应收总额
        }
         #endregion

        #region 右键添加维修项目
        private void addProject_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dgvproject.Rows)
            {
                string strPCode = CommonCtrl.IsNullToString(dgvr.Cells["item_no"].Value);
                if (!string.IsNullOrEmpty(strPCode))
                {
                    listProject.Add(strPCode);
                }
            }
            frmWorkHours frmHours = new frmWorkHours();
            DialogResult result = frmHours.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (listProject.Contains(frmHours.strProjectNum))
                {
                    MessageBoxEx.Show("此维修项目已存在，请选择其他维修项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                dgvproject.CurrentRow.Cells["item_no"].Value = frmHours.strProjectNum;
                dgvproject.CurrentRow.Cells["item_type"].Value = GetDicName(frmHours.strRepairType);
                dgvproject.CurrentRow.Cells["item_name"].Value = frmHours.strProjectName;
                dgvproject.CurrentRow.Cells["man_hour_norm_unitprice"].Value = frmHours.strQuotaPrice;
                dgvproject.CurrentRow.Cells["remarks"].Value = frmHours.strRemark;
                dgvproject.CurrentRow.Cells["man_hour_type"].Value = frmHours.strWhoursType == "1" ? "工时" : "定额";
                string strPzk = !string.IsNullOrEmpty(labMemberPZkS.Text) ? labMemberPZkS.Text : "10";
                //会员折扣
                dgvproject.CurrentRow.Cells["member_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk) * 10);
                //会员工时费
                dgvproject.CurrentRow.Cells["member_price"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strQuotaPrice) ? frmHours.strQuotaPrice : "0") * (Convert.ToDecimal(strPzk) / 10));
                if (CommonCtrl.IsNullToString(dgvproject.CurrentRow.Cells["member_price"].Value) != "0")
                {
                    //折扣额
                    dgvproject.CurrentRow.Cells["member_sum_money"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strQuotaPrice) ? frmHours.strQuotaPrice : "0") - Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.CurrentRow.Cells["member_price"].Value)) ? CommonCtrl.IsNullToString(dgvproject.CurrentRow.Cells["member_price"].Value) : "0"));
                }
                else
                {
                    dgvproject.CurrentRow.Cells["member_sum_money"].Value = "0";
                }
                dgvproject.CurrentRow.Cells["OldItem_id"].Value = frmHours.strWhours_id;
                dgvproject.CurrentRow.Cells["man_hour_quantity"].Value = frmHours.strWhoursNum;
                dgvproject.CurrentRow.Cells["sum_money_goods"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strWhoursNum) ? frmHours.strWhoursNum : "0") * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.CurrentRow.Cells["member_price"].Value)) ? CommonCtrl.IsNullToString(dgvproject.CurrentRow.Cells["member_price"].Value) : "0"));
                dcHmoney = 0;
                foreach (DataGridViewRow dgvr in dgvproject.Rows)
                {
                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["sum_money_goods"].Value)))
                    {
                        dcHmoney += Convert.ToDecimal(dgvr.Cells["sum_money_goods"].Value);
                    }
                }
                txtHSumMoney.Caption = dcHmoney.ToString();//工时货款
                txtHTaxCost.Caption = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(txtHSumMoney.Caption.Trim()) ? txtHSumMoney.Caption.Trim() : "0") * Convert.ToDecimal(!string.IsNullOrEmpty(txtHTaxRate.Caption.Trim()) ? txtHTaxRate.Caption.Trim() : "0"));//工时税额
                txtHSum.Caption = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(txtHSumMoney.Caption.Trim()) ? txtHSumMoney.Caption.Trim() : "0") + Convert.ToDecimal(!string.IsNullOrEmpty(txtHTaxCost.Caption.Trim()) ? txtHTaxCost.Caption.Trim() : "0"));//工时价税合计
                txtShouldSum.Caption = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(txtHSum.Caption.Trim()) ? txtHSum.Caption.Trim() : "0") + Convert.ToDecimal(!string.IsNullOrEmpty(txtFSum.Caption.Trim()) ? txtFSum.Caption.Trim() : "0") + Convert.ToDecimal(!string.IsNullOrEmpty(txtOSum.Caption.Trim()) ? txtOSum.Caption.Trim() : "0") - Convert.ToDecimal(!string.IsNullOrEmpty(txtPrivilegeCost.Caption.Trim()) ? txtPrivilegeCost.Caption.Trim() : "0"));//应收总额
                dgvproject.Rows.Add(1);
            }
        }
        #endregion

        #region 右键删除维修项目
        private void deleteProject_Click(object sender, EventArgs e)
        {
            int introw = dgvproject.CurrentRow.Index;
            if (introw < 0)
            {
                return;
            }
            if (dgvproject.Rows.Count <= 1)
            {
                MessageBoxEx.Show("至少保留一条维修项目信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string strItemId = CommonCtrl.IsNullToString(dgvproject.CurrentRow.Cells["item_id"].Value);
            if (!string.IsNullOrEmpty(strItemId))
            {
                List<string> listField = new List<string>();
                Dictionary<string, string> comField = new Dictionary<string, string>();
                listField.Add(strItemId);
                comField.Add("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.DELETED).ToString());
                DBHelper.BatchUpdateDataByIn("删除维修项目信息", "tb_maintain_item", comField, "item_id", listField.ToArray());
                dgvproject.Rows.RemoveAt(introw);
            }
            else
            {
                if (introw != dgvproject.Rows.Count - 1)
                {
                    dgvproject.Rows.RemoveAt(introw);
                }
            }
        }
        #endregion

        #region 右键添加维修用料
        private void addParts_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
            {
                string strPCode = CommonCtrl.IsNullToString(dgvr.Cells["parts_code"].Value);
                if (!string.IsNullOrEmpty(strPCode))
                {
                    listMater.Add(strPCode);
                }
            }
            frmParts frmPart = new frmParts();
            DialogResult result = frmPart.ShowDialog();
            if (result == DialogResult.OK)
            {
                string strPId = frmPart.PartsID;
                DataTable dpt = DBHelper.GetTable("", "v_parts", "*", " parts_id='" + strPId + "'", "", "");
                if (dpt.Rows.Count > 0)
                {
                    DataRow dpr = dpt.Rows[0];
                    if (listMater.Contains(CommonCtrl.IsNullToString(dpr["ser_parts_code"])))
                    {
                        MessageBoxEx.Show("此维修用料已存在，请选择其他维修用料", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    dgvMaterials.CurrentRow.Cells["parts_code"].Value = CommonCtrl.IsNullToString(dpr["ser_parts_code"]);
                    dgvMaterials.CurrentRow.Cells["parts_name"].Value = CommonCtrl.IsNullToString(dpr["parts_name"]);
                    dgvMaterials.CurrentRow.Cells["norms"].Value = CommonCtrl.IsNullToString(dpr["model"]);
                    dgvMaterials.CurrentRow.Cells["unit"].Value = CommonCtrl.IsNullToString(dpr["default_unit_name"]);
                    dgvMaterials.CurrentRow.Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dpr["is_import"]) == "1" ? "是" : "否";
                    dgvMaterials.CurrentRow.Cells["quantity"].Value = "1";
                    dgvMaterials.CurrentRow.Cells["unit_price"].Value = CommonCtrl.IsNullToString(dpr["highest_out_price"]);
                    string strPzk = !string.IsNullOrEmpty(labMemberLZkS.Text) ? labMemberLZkS.Text : "10";
                    //会员折扣
                    dgvMaterials.CurrentRow.Cells["Mmember_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk) * 10);
                    string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.CurrentRow.Cells["quantity"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.CurrentRow.Cells["quantity"].Value) : "0";
                    string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.CurrentRow.Cells["unit_price"].Value)) ? dgvMaterials.CurrentRow.Cells["unit_price"].Value.ToString() : "0";
                    //会员单价
                    dgvMaterials.CurrentRow.Cells["Mmember_price"].Value = Convert.ToString(Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.CurrentRow.Cells["Mmember_discount"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.CurrentRow.Cells["Mmember_discount"].Value) : "0") / 10);
                    dgvMaterials.CurrentRow.Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dpr["drawing_num"]);
                    dgvMaterials.CurrentRow.Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dpr["remark"]);
                    dgvMaterials.CurrentRow.Cells["vehicle_brand"].Value = CommonCtrl.IsNullToString(dpr["v_brand_name"]);
                    dgvMaterials.CurrentRow.Cells["M_Id"].Value = frmPart.PartsID;
                }
                dcPmoney = 0;
                foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
                {
                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["sum_money"].Value)))
                    {
                        dcPmoney += Convert.ToDecimal(dgvr.Cells["sum_money"].Value);
                    }
                }
                txtFSumMoney.Caption = dcPmoney.ToString();
                txtFTaxCost.Caption = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(txtFSumMoney.Caption.Trim()) ? txtFSumMoney.Caption.Trim() : "0") * Convert.ToDecimal(!string.IsNullOrEmpty(txtFTaxRate.Caption.Trim()) ? txtFTaxRate.Caption.Trim() : "0"));//配件税额
                txtFSum.Caption = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(txtFSumMoney.Caption.Trim()) ? txtFSumMoney.Caption.Trim() : "0") + Convert.ToDecimal(!string.IsNullOrEmpty(txtFTaxCost.Caption.Trim()) ? txtFTaxCost.Caption.Trim() : "0"));//配件价税合计
                txtShouldSum.Caption = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(txtHSum.Caption.Trim()) ? txtHSum.Caption.Trim() : "0") + Convert.ToDecimal(!string.IsNullOrEmpty(txtFSum.Caption.Trim()) ? txtFSum.Caption.Trim() : "0") + Convert.ToDecimal(!string.IsNullOrEmpty(txtOSum.Caption.Trim()) ? txtOSum.Caption.Trim() : "0") - Convert.ToDecimal(!string.IsNullOrEmpty(txtPrivilegeCost.Caption.Trim()) ? txtPrivilegeCost.Caption.Trim() : "0"));//应收总额
                dgvMaterials.Rows.Add(1);
            }
        }
        #endregion

        #region 右键删除维修用料
        private void deleteParts_Click(object sender, EventArgs e)
        {
            int introw = dgvMaterials.CurrentRow.Index;
            if (introw < 0)
            {
                return;
            }
            if (dgvMaterials.Rows.Count <= 1)
            {
                MessageBoxEx.Show("至少保留一条维修用料信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string strItemId = CommonCtrl.IsNullToString(dgvMaterials.CurrentRow.Cells["material_id"].Value);
            if (!string.IsNullOrEmpty(strItemId))
            {
                List<string> listField = new List<string>();
                Dictionary<string, string> comField = new Dictionary<string, string>();
                listField.Add(strItemId);
                comField.Add("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.DELETED).ToString());
                DBHelper.BatchUpdateDataByIn("删除维修用料", "tb_maintain_material_detail", comField, "material_id", listField.ToArray());
                dgvMaterials.Rows.RemoveAt(introw);
            }
            else
            {
                if (introw != dgvMaterials.Rows.Count - 1)
                {
                    dgvMaterials.Rows.RemoveAt(introw);
                }
            }
        }
        #endregion
    }
}
