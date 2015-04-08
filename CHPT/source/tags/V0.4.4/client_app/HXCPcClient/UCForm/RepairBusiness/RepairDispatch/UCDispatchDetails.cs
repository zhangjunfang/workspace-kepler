using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using Utility.Common;
using SYSModel;
using HXCPcClient.Chooser;
using ServiceStationClient.ComponentUI;
using HXCPcClient.UCForm.RepairBusiness.FetchMaterial;

namespace HXCPcClient.UCForm.RepairBusiness.RepairDispatch
{
    /// <summary>
    /// 维修管理-维修调度详情
    /// Author：JC
    /// AddTime：2014.11.07
    /// </summary>
    public partial class UCDispatchDetails : UCDispatchBase
    {
        #region 属性设置
        /// <summary>
        /// 父窗体
        /// </summary>
        public UCDispatchManager uc;
        /// <summary>
        /// 维修单的Id值
        /// </summary>
        string strReceiveId = string.Empty;
        /// <summary>
        /// 会员工时费
        /// </summary>
        string strMHourMoney = string.Empty;
        /// <summary>
        /// 维修项目Id
        /// </summary>
        string strMaintainId = string.Empty;
        /// <summary>
        /// 会员项目折扣
        /// </summary>
        string strMemberPZk = string.Empty;
        /// <summary>
        /// 会员用料折扣
        /// </summary>
        string strMemberLZk = "0";
        /// <summary>
        /// 会员参数设置Id
        /// </summary>
        string strSetInfoid = string.Empty;
        /// <summary>
        /// 操作名称
        /// </summary>
        string opName = string.Empty;
        /// <summary>
        /// 维修进度状态
        /// </summary>
        string strProjectSattus = string.Empty;
        /// <summary>
        /// 单据状态
        /// </summary>
        DataSources.EnumDispatchStatus DStatus;
        /// <summary>
        /// 项目状态
        /// </summary>
        DataSources.EnumProjectDisStatus PStatus;
        /// <summary>
        /// 质检窗体
        /// </summary>
        UCInspection Inspection;
        /// <summary>
        /// 质检意见
        /// </summary>
        string strInspection = string.Empty;
        /// <summary>
        /// 停工窗体
        /// </summary>
        UCStopReason StopReason;
        /// <summary>
        /// 停工原因
        /// </summary>
        string strStopReason = string.Empty;
        /// <summary>
        /// 开工时间
        /// </summary>
        string strStarTime = string.Empty;
        /// <summary>
        /// 停工时间
        /// </summary>
        string strStopTime = string.Empty;
        /// <summary>
        /// 完工时间
        /// </summary>
        string strCTime = string.Empty;
        /// <summary>
        /// 继续时间
        /// </summary>
        string strContinueTime = string.Empty;
        /// <summary>
        /// 项目工时总费用
        /// </summary>
        decimal strHMoney = 0;
        /// <summary>
        /// 用料配件总费用
        /// </summary>
        decimal strPMoney = 0;
        /// <summary>
        /// 其他项目总费用
        /// </summary>
        decimal strOMoney = 0;

        /// <summary>
        /// 用于判断用料是否重复添加
        /// </summary>
        List<string> listMater = new List<string>();
        /// <summary>
        ///  用于判断项目是否重复添加
        /// </summary>
        List<string> listProject = new List<string>();
        /// <summary>
        /// 用于判断人员信息是否重复添加
        /// </summary>
        List<string> listUser = new List<string>();
        /// <summary>
        /// 是否自动关闭
        /// </summary>
        bool isAutoClose = false;
        /// <summary>
        /// 维修结算单Id
        /// </summary>
        string strBlanceId = string.Empty;
        /// <summary>
        /// 维修用料(配件总数量)
        /// </summary>
        decimal decPartsCount = 0;
        /// <summary>
        /// 相关领料情况总数量
        /// </summary>
        decimal decFPartsCount = 0;
        #endregion

        #region 初始化窗体
        public UCDispatchDetails(string ReceiveId)
        {
            InitializeComponent();
            strReceiveId = ReceiveId;
            SetDgvAnchor();
            base.CancelEvent += new ClickHandler(UCDispatchDetails_CancelEvent);
            base.AddSaveEvent += new ClickHandler(UCDispatchDetails_AddSaveEvent);
            base.DtaloEvent += new ClickHandler(UCDispatchDetails_DtaloEvent);
            //base.OverallEvent += new ClickHandler(UCDispatchDetails_OverallEvent);
            base.AffirmEvent += new ClickHandler(UCDispatchDetails_AffirmEvent);
            base.StartEvent += new ClickHandler(UCDispatchDetails_StartEvent);
            base.StopEvent += new ClickHandler(UCDispatchDetails_StopEvent);
            base.CompleteEvent += new ClickHandler(UCDispatchDetails_CompleteEvent);
            base.BalanceEvent += new ClickHandler(UCDispatchDetails_BalanceEvent);
            base.QCEvent += new ClickHandler(UCDispatchDetails_QCEvent);
        }
        #endregion

        #region 质检事件
        void UCDispatchDetails_QCEvent(object sender, EventArgs e)
        {
            try
            {
                //if (MessageBoxEx.Show("确认要质检吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                //{
                //    return;
                //}
                Inspection = new UCInspection();
                if (Inspection.ShowDialog() == DialogResult.OK)
                {
                    strInspection = Inspection.Content;
                    DStatus = Inspection.DStatus;
                    PStatus = Inspection.PStatus;
                    AlterOrdersStatus(DStatus, PStatus);
                    //质检状体直接入库
                    List<SQLObj> listSql = new List<SQLObj>();
                    UpdateRepairOrderInfo(listSql, DStatus);
                    AttachmentInfo(listSql);
                    SaveProjectData(listSql, strReceiveId, PStatus);
                    SaveMaterialsData(listSql, strReceiveId);
                    SaveOtherData(listSql, strReceiveId);
                    DBHelper.BatchExeSQLMultiByTrans(opName, listSql);
                    if (DStatus == DataSources.EnumDispatchStatus.NotPassed)
                    {
                        MessageBoxEx.Show("质检未通过!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (DStatus == DataSources.EnumDispatchStatus.HasPassed)
                    {
                        MessageBoxEx.Show("质检成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                        #region 自动生成领料单（草稿状体）
                        strBlanceId = strReceiveId;
                        if (string.IsNullOrEmpty(DBHelper.GetSingleValue("查询单据是否结算", "tb_maintain_settlement_info", "settlement_id", "maintain_id='" + strBlanceId + "'", "")))
                        {
                            List<SQLObj> listblanceSql = new List<SQLObj>();
                            strBlanceId = strReceiveId;
                            SaveOrderInfo(listblanceSql);
                            SaveBalanceInfo(listblanceSql);
                            SaveProjectData(listblanceSql);
                            SaveMaterialsData(listblanceSql);
                            SaveOtherData(listblanceSql);
                            ucAttr.TableName = "tb_maintain_info";
                            ucAttr.TableNameKeyValue = strBlanceId;
                            listblanceSql.AddRange(ucAttr.AttachmentSql);
                            DBHelper.BatchExeSQLMultiByTrans(opName, listblanceSql);
                        }
                        #endregion
                    }
                    isAutoClose = true;
                    uc.BindPageData();
                    base.deleteMenuByTag(this.Tag.ToString(), this.uc.Name);
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 自动生成结算单信息
        #region 维修结算单基本信息保存
        /// <summary>
        /// 维修结算单基本信息保存
        /// </summary>
        private void SaveOrderInfo(List<SQLObj> listSql)
        {
            try
            {
                SQLObj obj = new SQLObj();
                obj.cmdType = CommandType.Text;
                Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();

                #region 基本信息
                dicParam.Add("vehicle_no", new ParamObj("vehicle_no", labCarNOS.Text.Trim(), SysDbType.VarChar, 20));//车牌号
                dicParam.Add("vehicle_vin", new ParamObj("vehicle_vin", labVINS.Text.Trim(), SysDbType.VarChar, 40));//VIN
                dicParam.Add("engine_no", new ParamObj("engine_no", labEngineNoS.Text.Trim(), SysDbType.VarChar, 40));//发动机号
                dicParam.Add("vehicle_model", new ParamObj("vehicle_model", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(labCarTypeS.Tag)) ? labCarTypeS.Tag.ToString() : null, SysDbType.VarChar, 40));//车型
                dicParam.Add("vehicle_brand", new ParamObj("vehicle_brand", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(labCarBrandS.Tag)) ? labCarBrandS.Tag.ToString() : null, SysDbType.VarChar, 40));//车辆品牌
                dicParam.Add("driver_name", new ParamObj("driver_name", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtDriver.Caption.Trim())) ? txtDriver.Caption.Trim() : null, SysDbType.VarChar, 20));//报修人
                dicParam.Add("driver_mobile", new ParamObj("driver_mobile", txtDriverPhone.Caption.Trim(), SysDbType.VarChar, 15));//报修人               
                dicParam.Add("turner", new ParamObj("turner", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(labturnerS.Text)) ? labturnerS.Text : null, SysDbType.VarChar, 40));//颜色
                dicParam.Add("travel_mileage", new ParamObj("travel_mileage", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtMil.Caption.Trim())) ? txtMil.Caption.Trim() : null, SysDbType.Decimal, 15));//行驶里程
                dicParam.Add("oil_into_factory", new ParamObj("oil_into_factory", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(labMlS.Text.Trim())) ? labMlS.Text.Trim().Replace("%", "") : null, SysDbType.Decimal, 15));//进场油量               
                dicParam.Add("completion_time", new ParamObj("completion_time", !string.IsNullOrEmpty(labSuTimeS.Text.Trim()) ? Common.LocalDateTimeToUtcLong(Convert.ToDateTime(labSuTimeS.Text.Trim())).ToString() : null, SysDbType.BigInt));//预计完工时间
                dicParam.Add("maintain_payment", new ParamObj("maintain_payment", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(labPayTypeS.Tag)) ? labPayTypeS.Tag.ToString() : null, SysDbType.VarChar, 40));//维修付费方式
                dicParam.Add("maintain_type", new ParamObj("maintain_type", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(labRepTypeS.Tag)) ? labRepTypeS.Tag.ToString() : null, SysDbType.VarChar, 40));//维修类别
                dicParam.Add("remark", new ParamObj("remark", labRemarkS.Text.Trim(), SysDbType.VarChar, 40));//备注
                dicParam.Add("favorable_reason", new ParamObj("favorable_reason", null, SysDbType.VarChar, 40));//优惠原因
                dicParam.Add("customer_code", new ParamObj("customer_code", labCustomNOS.Text, SysDbType.VarChar, 40));//客户编码
                dicParam.Add("customer_id", new ParamObj("customer_id", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(labCustomNOS.Tag)) ? labCustomNOS.Tag.ToString() : null, SysDbType.VarChar, 40));//客户Id
                dicParam.Add("customer_name", new ParamObj("customer_name", labCustomNameS.Text.Trim(), SysDbType.VarChar, 20));//客户名称           
                dicParam.Add("linkman", new ParamObj("linkman", labContactS.Text.Trim(), SysDbType.VarChar, 20));//联系人
                dicParam.Add("link_man_mobile", new ParamObj("link_man_mobile", labContactPhoneS.Text.Trim(), SysDbType.VarChar, 15));//联系人手机               
                //经办人id
                dicParam.Add("responsible_opid", new ParamObj("responsible_opid", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(labAttnS.Text)) ? labAttnS.Text : null, SysDbType.VarChar, 40));
                //经办人
                dicParam.Add("responsible_name", new ParamObj("responsible_name", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(labAttnS.Tag)) ? labAttnS.Tag : null, SysDbType.VarChar, 40));
                //部门
                dicParam.Add("org_name", new ParamObj("org_name", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(labDepartS.Tag)) ? labDepartS.Tag : null, SysDbType.VarChar, 40));
                dicParam.Add("enable_flag", new ParamObj("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString(), SysDbType.VarChar, 1));//信息状态（1|激活；2|作废；0|删除）           
                //单据来源,0结算单、1预约单、2返修单、3结算单，默认0
                dicParam.Add("orders_source", new ParamObj("orders_source", "3", SysDbType.VarChar, 1));
                //单据状态(草稿)
                dicParam.Add("info_status", new ParamObj("info_status", Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString(), SysDbType.VarChar, 40));
                dicParam.Add("maintain_no", new ParamObj("maintain_no", labMaintain_noS.Text.Trim(), SysDbType.VarChar, 40));//维修单号
                dicParam.Add("before_orderId", new ParamObj("before_orderId", strReceiveId, SysDbType.VarChar, 40));
                #endregion
                dicParam.Add("maintain_id", new ParamObj("maintain_id", strBlanceId, SysDbType.VarChar, 40));//Id
                dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
                dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
                dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                obj.sqlString = @"update tb_maintain_info set vehicle_no=@vehicle_no,vehicle_vin=@vehicle_vin,engine_no=@engine_no,vehicle_model=@vehicle_model 
                 ,vehicle_brand=@vehicle_brand,driver_name=@driver_name,driver_mobile=@driver_mobile,turner=@turner,travel_mileage=@travel_mileage 
                 ,oil_into_factory=@oil_into_factory,completion_time=@completion_time,maintain_payment=@maintain_payment,maintain_type=@maintain_type,remark=@remark 
                 ,favorable_reason=@favorable_reason,customer_code=@customer_code,customer_name=@customer_name,linkman=@linkman,link_man_mobile=@link_man_mobile,responsible_opid=@responsible_opid
                 ,responsible_name=@responsible_name,org_name=@org_name,enable_flag=@enable_flag,info_status=@info_status,maintain_no=@maintain_no,update_by=@update_by,update_name=@update_name,update_time=@update_time
                 ,orders_source=@orders_source,before_orderId=@before_orderId  where maintain_id=@maintain_id";
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
        private void SaveBalanceInfo(List<SQLObj> listSql)
        {
            try
            {
                SQLObj obj = new SQLObj();
                obj.cmdType = CommandType.Text;
                Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                #region 基本信息
                decimal dcHmoney = 0;//工时货款
                foreach (DataGridViewRow dgvr in dgvproject.Rows)
                {
                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["sum_money_goods"].Value)))
                    {
                        dcHmoney += Convert.ToDecimal(dgvr.Cells["sum_money_goods"].Value);
                    }
                }

                dicParam.Add("man_hour_sum_money", new ParamObj("man_hour_sum_money", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dcHmoney)) ? dcHmoney.ToString() : null, SysDbType.Decimal, 15));//工时货款
                dicParam.Add("man_hour_sum", new ParamObj("man_hour_sum", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dcHmoney)) ? dcHmoney.ToString() : null, SysDbType.Decimal, 15));//工时税价合计
                decimal dcPmoney = 0;//配件货款
                foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
                {
                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["sum_money"].Value)))
                    {
                        dcPmoney += Convert.ToDecimal(dgvr.Cells["sum_money"].Value);
                    }
                }
                dicParam.Add("fitting_sum_money", new ParamObj("fitting_sum_money", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dcPmoney)) ? dcPmoney.ToString() : null, SysDbType.Decimal, 15));//配件货款
                dicParam.Add("fitting_sum", new ParamObj("fitting_sum", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dcPmoney)) ? dcPmoney.ToString() : null, SysDbType.Decimal, 15));//配件价税合计
                decimal dcOmoney = 0;//其他项目费用
                foreach (DataGridViewRow dgvr in dgvOther.Rows)
                {
                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["Osum_money"].Value)))
                    {
                        dcOmoney += Convert.ToDecimal(dgvr.Cells["Osum_money"].Value);
                    }
                }
                dicParam.Add("other_item_sum_money", new ParamObj("other_item_sum_money", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dcOmoney)) ? dcOmoney.ToString() : null, SysDbType.Decimal, 15));//其他项目费用
                dicParam.Add("other_item_sum", new ParamObj("other_item_sum", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dcOmoney)) ? dcOmoney.ToString() : null, SysDbType.Decimal, 15));//其他项目税价合计
                dicParam.Add("should_sum", new ParamObj("should_sum", Convert.ToString((dcHmoney + dcPmoney + dcOmoney)), SysDbType.Decimal, 15));//应收总额位            
                dicParam.Add("maintain_id", new ParamObj("maintain_id", strBlanceId, SysDbType.VarChar, 40));//关联维修结算单主数据Id
                dicParam.Add("privilege_cost", new ParamObj("privilege_cost", "0", SysDbType.Decimal, 15));//优惠费用
                dicParam.Add("received_sum", new ParamObj("received_sum", Convert.ToString((dcHmoney + dcPmoney + dcOmoney)), SysDbType.Decimal, 15));//实收总额
                #endregion
                dicParam.Add("settlement_id", new ParamObj("settlement_id", Guid.NewGuid().ToString(), SysDbType.VarChar, 40));//Id
                dicParam.Add("create_by", new ParamObj("create_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//创建人id（制单人） 
                dicParam.Add("create_name", new ParamObj("create_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//创建人
                dicParam.Add("create_time", new ParamObj("create_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));
                obj.sqlString = @"insert into tb_maintain_settlement_info (man_hour_sum_money,man_hour_sum,fitting_sum_money,fitting_sum,other_item_sum_money,other_item_sum,should_sum
                ,maintain_id,privilege_cost,received_sum,settlement_id,create_by,create_name,create_time)
                values (@man_hour_sum_money,@man_hour_sum,@fitting_sum_money,@fitting_sum,@other_item_sum_money,@other_item_sum,@should_sum
                ,@maintain_id,@privilege_cost,@received_sum,@settlement_id,@create_by,@create_name,@create_time);";

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
        private void SaveProjectData(List<SQLObj> listSql)
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
                        dicParam.Add("maintain_id", new ParamObj("maintain_id", strBlanceId, SysDbType.VarChar, 40));
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
                            opName = "新增结算单维修项目";
                            strPID = Guid.NewGuid().ToString();
                            dicParam.Add("item_id", new ParamObj("item_id", strPID, SysDbType.VarChar, 40));
                            obj.sqlString = "insert into [tb_maintain_item] (item_id,maintain_id,item_no,item_type,item_name,man_hour_type,man_hour_quantity,man_hour_norm_unitprice,member_discount,member_price,member_sum_money,sum_money_goods,three_warranty,remarks,enable_flag,whours_id) ";
                            obj.sqlString += " values (@item_id,@maintain_id,@item_no,@item_type,@item_name,@man_hour_type,@man_hour_quantity,@man_hour_norm_unitprice,@member_discount,@member_price,@member_sum_money,@sum_money_goods,@three_warranty,@remarks,@enable_flag,@whours_id);";
                        }
                        else
                        {
                            dicParam.Add("item_id", new ParamObj("item_id", dgvr.Cells["item_id"].Value, SysDbType.VarChar, 40));
                            opName = "更新结算单维修项目";
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
        private void SaveMaterialsData(List<SQLObj> listSql)
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
                        dicParam.Add("maintain_id", new ParamObj("maintain_id", strBlanceId, SysDbType.VarChar, 40));
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
                        dicParam.Add("parts_id", new ParamObj("parts_id", dgvr.Cells["parts_id"].Value, SysDbType.VarChar, 40));
                        string strPID = CommonCtrl.IsNullToString(dgvr.Cells["material_id"].Value);
                        if (strPID.Length == 0)
                        {
                            opName = "新增结算单维修用料";
                            strPID = Guid.NewGuid().ToString();
                            dicParam.Add("material_id", new ParamObj("material_id", strPID, SysDbType.VarChar, 40));
                            obj.sqlString = "insert into [tb_maintain_material_detail] (material_id,parts_code,parts_name,norms,unit,whether_imported,quantity,unit_price,sum_money,drawn_no,vehicle_brand,three_warranty,remarks,enable_flag,maintain_id,parts_id) ";
                            obj.sqlString += " values (@material_id,@parts_code,@parts_name,@norms,@unit,@whether_imported,@quantity,@unit_price,@sum_money,@drawn_no,@vehicle_brand,@three_warranty,@remarks,@enable_flag,@maintain_id,@parts_id);";
                        }
                        else
                        {
                            dicParam.Add("material_id", new ParamObj("material_id", dgvr.Cells["material_id"].Value, SysDbType.VarChar, 40));
                            opName = "更新结算单维修用料";
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
        private void SaveOtherData(List<SQLObj> listSql)
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
                        dicParam.Add("maintain_id", new ParamObj("maintain_id", strBlanceId, SysDbType.VarChar, 40));
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
        #endregion

        #region 试结算事件
        void UCDispatchDetails_BalanceEvent(object sender, EventArgs e)
        {
            try
            {
                #region 工时费用总额
                foreach (DataGridViewRow dr in dgvproject.Rows)
                {
                    string strPname = CommonCtrl.IsNullToString(dr.Cells["item_name"].Value);
                    if (strPname.Length > 0)
                    {
                        string strHSumMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr.Cells["sum_money_goods"].Value)) ? dr.Cells["sum_money_goods"].Value.ToString() : "0";
                        strHMoney += Convert.ToDecimal(strHSumMoney);
                    }
                }
                #endregion

                #region 配件费用总额
                foreach (DataGridViewRow dr in dgvMaterials.Rows)
                {
                    string strPname = CommonCtrl.IsNullToString(dr.Cells["parts_name"].Value);
                    if (strPname.Length > 0)
                    {
                        string strPSumMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr.Cells["sum_money"].Value)) ? dr.Cells["sum_money"].Value.ToString() : "0";
                        strPMoney += Convert.ToDecimal(strPSumMoney);
                    }
                }
                #endregion

                #region 其他费用收费总额
                foreach (DataGridViewRow dr in dgvOther.Rows)
                {
                    string strPname = CommonCtrl.IsNullToString(dr.Cells["Osum_money"].Value);
                    if (strPname.Length > 0)
                    {
                        string strOSumMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr.Cells["Osum_money"].Value)) ? dr.Cells["Osum_money"].Value.ToString() : "0";
                        strOMoney += Convert.ToDecimal(strOSumMoney);
                    }
                }
                #endregion

                UCTrialSettlement settle = new UCTrialSettlement();
                settle.strHMoney = strHMoney.ToString();
                settle.strPMoney = strPMoney.ToString();
                settle.strOMoney = strOMoney.ToString();
                settle.ShowDialog();
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 完工事件
        void UCDispatchDetails_CompleteEvent(object sender, EventArgs e)
        {
            try
            {
                //if (!JudgeProjectOK("完工"))
                //{
                //    return;
                //}
                if (MessageBoxEx.Show("确认要完工吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
                if (decPartsCount > decFPartsCount)
                {
                    MessageBoxEx.Show("用料数量与领料数量不符,用料数量(" + decPartsCount + ")大于领料数量(" + ControlsConfig.SetNewValue(decFPartsCount,1) + ")请完善领料信息!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (decPartsCount < decFPartsCount)
                {
                    MessageBoxEx.Show("用料数量与领料数量不符,用料数量(" + decPartsCount + ")小于领料数量(" + ControlsConfig.SetNewValue(decFPartsCount,1) + ")请完善领料信息!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DStatus = DataSources.EnumDispatchStatus.FinishWork;
                PStatus = DataSources.EnumProjectDisStatus.RepairsCompleted;
                strStarTime = string.Empty;
                strStopTime = string.Empty;
                strCTime = DateTime.Now.ToString();
                strContinueTime = string.Empty;
                AlterOrdersStatus(DStatus, PStatus);
                base.btnSave.Enabled = true;
                List<SQLObj> listSql = new List<SQLObj>();
                UpdateRepairOrderInfo(listSql, DStatus);
                SaveProjectData(listSql, strReceiveId, PStatus);
                if (DBHelper.BatchExeSQLMultiByTrans(opName, listSql))
                {
                    MessageBoxEx.Show("已完工!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    uc.BindPageData();
                    isAutoClose = true;
                    base.deleteMenuByTag(this.Tag.ToString(), this.uc.Name);
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 停工事件
        void UCDispatchDetails_StopEvent(object sender, EventArgs e)
        {
            try
            {
                //if (!JudgeProjectOK("停工"))
                //{
                //    return;
                //}
                if (MessageBoxEx.Show("确认要停工吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
                StopReason = new UCStopReason();
                if (StopReason.ShowDialog() == DialogResult.OK)
                {
                    strStopReason = StopReason.Content;
                    DStatus = StopReason.DStatus;
                    PStatus = StopReason.PStatus;
                    strStarTime = string.Empty;
                    strStopTime = DateTime.Now.ToString();
                    strCTime = string.Empty;
                    strContinueTime = string.Empty;
                    AlterOrdersStatus(DStatus, PStatus);
                    base.btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 开工事件
        void UCDispatchDetails_StartEvent(object sender, EventArgs e)
        {
            try
            {
                //if (!JudgeProjectOK())
                //{
                //    return;
                //}

                if (MessageBoxEx.Show("确认要开工吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
                bool iscontinue = true;
                #region 判断维修配件库存是否够用
                foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
                {
                    string strPCode = CommonCtrl.IsNullToString(dgvr.Cells["parts_code"].Value);
                    string strPId = CommonCtrl.IsNullToString(dgvr.Cells["parts_id"].Value);
                    if (!string.IsNullOrEmpty(strPCode) && !string.IsNullOrEmpty(strPId))
                    {
                        //配件数量
                        decimal intPNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["quantity"].Value)) ? Convert.ToDecimal(CommonCtrl.IsNullToString(dgvr.Cells["quantity"].Value)) : 0;
                        //配件账面库存数量   
                        decimal intPCount = Convert.ToDecimal(CommonFuncCall.CheckPartStockCount(strPId, DataSources.EnumStatisticType.PaperCount));
                        if (intPNum > intPCount)
                        {
                            if (MessageBoxEx.Show("您选择的配件【" + strPCode + "】库存不足，是否继续开工?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                            {
                                iscontinue = false;
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }
                #endregion
                if (iscontinue)
                {
                    DStatus = DataSources.EnumDispatchStatus.StartWork;
                    PStatus = DataSources.EnumProjectDisStatus.Maintenance;
                    strStarTime = DateTime.Now.ToString();
                    strStopTime = string.Empty;
                    strCTime = string.Empty;
                    base.btnSave.Enabled = true;
                    base.btnComplete.Enabled = true;
                    base.btnStop.Enabled = true;
                    createFetch.Enabled = true;//自动生成领料单
                    //if (strProjectSattus == "4")//停工在开工
                    //{
                    //strStarTime = string.Empty;
                    strContinueTime = DateTime.Now.ToString();
                    //}
                    AlterOrdersStatus(DStatus, PStatus);
                    //List<SQLObj> listSql = new List<SQLObj>();
                    //UpdateRepairOrderInfo(listSql, DStatus);
                    //if (DBHelper.BatchExeSQLMultiByTrans(opName, listSql))
                    //{
                    //    MessageBoxEx.Show("已开工!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    //    uc.BindPageData();
                    //    base.deleteMenuByTag(this.Tag.ToString(), this.uc.Name);
                    //}
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 判断是否选择了开工、停工、完工所需的项目
        /// <summary>
        /// 判断是否选择了开工、停工、完工所需的项目
        /// </summary>
        /// <param name="strMessage">提示信息</param>
        /// <returns></returns>
        private bool JudgeProjectOK()
        {
            bool isboolCheck = true;
            try
            {
                List<string> listField = new List<string>();
                foreach (DataGridViewRow dr in dgvproject.Rows)
                {
                    //object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                    string strProgress = CommonCtrl.IsNullToString(dr.Cells["repair_progress"].Value);
                    if (strProgress == "未分配工时")
                    {
                        //if (isCheck != null && (bool)isCheck)
                        //{
                        listField.Add(dr.Cells["item_id"].Value.ToString());
                        // }
                    }
                }
                if (listField.Count > 0)
                {
                    isboolCheck = false;
                    MessageBoxEx.Show("存在未分配工时项目,请分配工时后在开工！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                isboolCheck = false;
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
            return isboolCheck;
        }

        #endregion

        #region 确认分配工时事件
        void UCDispatchDetails_AffirmEvent(object sender, EventArgs e)
        {

            //if (IsAllOverall)
            //{
            //    foreach (DataGridViewRow dr in dgvproject.Rows)
            //    {

            //        string strPname = CommonCtrl.IsNullToString(dr.Cells["item_name"].Value);
            //        string strProgress = CommonCtrl.IsNullToString(dr.Cells["repair_progress"].Value);
            //        if (strPname.Length > 0)
            //        {
            //            if (strProgress=="未分配工时")
            //            {
            //                //if (MessageBoxEx.Show("您有未分配工时项目，请分配工时！", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            //                //{
            //                //    return;
            //                //}
            //                MessageBoxEx.Show("您有未分配工时项目，请分配工时！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                return;
            //            }
            //        }
            //    }
            //}
            //else if (!IsChooserProject())
            //{
            //    return;
            //}
            if (MessageBoxEx.Show("确认要分配工时吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            base.btnSave.Enabled = true;//保存  
            base.btnSatart.Enabled = true;//开工
            DStatus = DataSources.EnumDispatchStatus.NotStartWork;
            PStatus = DataSources.EnumProjectDisStatus.NotStartWork;
            AlterOrdersStatus(DStatus, PStatus);
        }
        #endregion

        #region 整体分配工时时判断是否选中所有项目
        private bool IsCheckAll()
        {

            bool IsAll = false;
            foreach (DataGridViewRow dr in dgvproject.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                string strPname = CommonCtrl.IsNullToString(dr.Cells["item_name"].Value);
                if (strPname.Length > 0)
                {
                    if (isCheck != null && !(bool)isCheck)
                    {
                        dr.Cells["colCheck"].Value = true;
                        IsAll = true;
                    }
                    else
                    {
                        IsAll = true;

                    }
                }
            }
            return IsAll;
        }
        #endregion

        #region 分配工时事件
        void UCDispatchDetails_DtaloEvent(object sender, EventArgs e)
        {
            try
            {
                if (!IsChooserProject())
                {
                    return;
                }

                //frmDispatching persons = new frmDispatching();
                //DialogResult result = persons.ShowDialog();
                //if (result == DialogResult.OK)
                //{
                if (!IsChooserUser())
                {
                    return;
                }
                if (MessageBoxEx.Show("确认要分配工时吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }

                #region 保存调度人员的信息
                List<SQLObj> listSqlUser = new List<SQLObj>();
                string strProjectID = string.Empty;
                for (int i = 0; i < dgvproject.RowCount; i++)
                {

                    DataGridViewRow dgvr = dgvproject.Rows[i];
                    object isCheck = dgvr.Cells["colCheck"].Value;
                    if (isCheck != null && (bool)isCheck)
                    {
                        strProjectID = CommonCtrl.IsNullToString(dgvr.Cells["item_id"].Value);
                        DispatchUserInfo(listSqlUser, strReceiveId, strProjectID);
                    }
                }
                DBHelper.BatchExeSQLMultiByTrans("添加分配工时人员信息", listSqlUser);
                //BindUserInfo(persons.strUserId);
                //base.btnAffirm.Enabled = true;//确认分配工时
                base.btnSave.Enabled = true;//保存  
                base.btnSatart.Enabled = true;//开工
                DStatus = DataSources.EnumDispatchStatus.NotStartWork;
                PStatus = DataSources.EnumProjectDisStatus.NotStartWork;
                AlterOrdersStatus(DStatus, PStatus);
                #endregion
                //    IsAllOverall = true;
                //}
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 检测是否选择了分配工时人员
        /// <summary>
        /// 检测是否选择了分配工时人员
        /// </summary>
        private bool IsChooserUser()
        {
            bool isboolCheck = false;
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in dgvUserData.Rows)
            {
                object isCheck = dr.Cells["UcolCheck"].EditedFormattedValue;
                string strPname = CommonCtrl.IsNullToString(dr.Cells["dispatch_no"].Value);
                if (strPname.Length > 0)
                {
                    if (isCheck != null && (bool)isCheck)
                    {
                        listField.Add(dr.Cells["dispatch_no"].Value.ToString());
                    }
                }
            }
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择分配工时人员信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                isboolCheck = true;
            }
            return isboolCheck;
        }
        #endregion

        #region 分配工时时修改单据与项目的状态,开工、停工、完工、继续时间，停工原因，停工时长
        /// <summary>
        ///分配工时时修改单据与项目的状态
        /// </summary>
        /// <param name="DStatus">单据状态</param>
        /// <param name="PStatus">项目状态</param>       
        private void AlterOrdersStatus(DataSources.EnumDispatchStatus DStatus, DataSources.EnumProjectDisStatus PStatus)
        {
            try
            {
                labStatusS.Text = labStatusS.Text = DataSources.GetDescription(typeof(DataSources.EnumDispatchStatus), int.Parse(CommonCtrl.IsNullToString(Convert.ToInt32(DStatus).ToString())));//单据状态
                foreach (DataGridViewRow dr in dgvproject.Rows)
                {
                    //    object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                    string strPname = CommonCtrl.IsNullToString(dr.Cells["item_name"].Value);
                    if (strPname.Length > 0)
                    {
                        //if (PStatus == DataSources.EnumProjectDisStatus.NotStartWork)
                        //{
                        //if (isCheck != null && (bool)isCheck)
                        //{
                        dr.Cells["repair_progress"].Value = DataSources.GetDescription(typeof(DataSources.EnumProjectDisStatus), int.Parse(CommonCtrl.IsNullToString(Convert.ToInt32(PStatus).ToString())));//项目状态 
                        //}
                        //}
                        if (!string.IsNullOrEmpty(strStarTime))
                        {
                            dr.Cells["start_work_time"].Value = strStarTime;
                        }
                        if (!string.IsNullOrEmpty(strStopTime))
                        {
                            dr.Cells["shut_down_time"].Value = strStopTime;
                        }
                        if (!string.IsNullOrEmpty(strCTime))
                        {
                            dr.Cells["complete_work_time"].Value = strCTime;
                        }
                        if (!string.IsNullOrEmpty(strStopReason))
                        {
                            dr.Cells["shut_down_reason"].Value = strStopReason;
                        }
                        if (!string.IsNullOrEmpty(strContinueTime) && !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr.Cells["shut_down_time"].Value)))
                        {
                            dr.Cells["continue_time"].Value = strContinueTime;
                            TimeSpan nd = Convert.ToDateTime(strContinueTime) - Convert.ToDateTime(dr.Cells["shut_down_time"].Value.ToString());
                            dr.Cells["shut_down_duration"].Value = nd.TotalMinutes.ToString();
                        }
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 根据分配工时人员Id获取人员信息并绑定到datagridview中
        /// <summary>
        /// 根据分配工时人员Id获取人员信息并绑定到datagridview中
        /// </summary>
        /// <param name="strUId">用户Id字符串</param>
        private void BindUserInfo(string strUId, int introws)
        {
            try
            {
                DataTable dut = DBHelper.GetTable("查询人员信息", " v_user", "*", "user_id in ('" + strUId + "')", "", " order by user_id desc");
                if (dut.Rows.Count > 0)
                {
                    //if (dut.Rows.Count > dgvUserData.Rows.Count)
                    //{
                    //    dgvproject.Rows.Add(dut.Rows.Count - dgvUserData.Rows.Count + 1);
                    //}
                    //for (int i = 0; i < dut.Rows.Count; i++)
                    //{
                    DataRow dur = dut.Rows[0];
                    if (listUser.Contains(CommonCtrl.IsNullToString(dur["user_code"])))
                    {
                        MessageBoxEx.Show("此人员信息已存在，请选择其他人员", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    dgvUserData.Rows[introws].Cells["UcolCheck"].Value = true;
                    dgvUserData.Rows[introws].Cells["dispatch_no"].Value = CommonCtrl.IsNullToString(dur["user_code"]);
                    dgvUserData.Rows[introws].Cells["dispatch_name"].Value = CommonCtrl.IsNullToString(dur["user_name"]);
                    dgvUserData.Rows[introws].Cells["org_name"].Value = CommonCtrl.IsNullToString(dur["create_Username"]);
                    dgvUserData.Rows[introws].Cells["team_name"].Value = "";
                    dgvUserData.Rows[introws].Cells["create_time"].Value = DateTime.Now.ToString();
                    dgvUserData.Rows[introws].Cells["Usum_money"].Value = "0";
                    //}

                }
                foreach (DataGridViewRow dgvr in dgvUserData.Rows)
                {
                    string strPCode = CommonCtrl.IsNullToString(dgvr.Cells["dispatch_no"].Value);
                    if (!string.IsNullOrEmpty(strPCode) && !listUser.Contains(strPCode))
                    {
                        listUser.Add(strPCode);
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 分配工时时绑定人员信息
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strUId">用户Id字符串</param>
        private void BindUserInfo(string strUId)
        {
            try
            {

                DataTable dut = DBHelper.GetTable("查询人员信息", " v_user", "*", "user_id in (" + strUId.Substring(0, strUId.Length - 1) + ")", "", " order by user_id desc");
                if (dut.Rows.Count > 0)
                {
                    if (dut.Rows.Count > dgvUserData.Rows.Count)
                    {
                        dgvproject.Rows.Add(dut.Rows.Count - dgvUserData.Rows.Count + 1);
                    }
                    for (int i = 0; i < dut.Rows.Count; i++)
                    {
                        DataRow dur = dut.Rows[i];
                        if (listUser.Contains(CommonCtrl.IsNullToString(dur["user_code"])))
                        {
                            MessageBoxEx.Show("此人员信息已存在，请选择其他人员", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        dgvUserData.Rows[i].Cells["UcolCheck"].Value = true;
                        dgvUserData.Rows[i].Cells["dispatch_no"].Value = CommonCtrl.IsNullToString(dur["user_code"]);
                        dgvUserData.Rows[i].Cells["dispatch_name"].Value = CommonCtrl.IsNullToString(dur["user_name"]);
                        dgvUserData.Rows[i].Cells["org_name"].Value = CommonCtrl.IsNullToString(dur["org_name"]);
                        dgvUserData.Rows[i].Cells["team_name"].Value = "";
                        dgvUserData.Rows[i].Cells["create_time"].Value = DateTime.Now.ToString();
                        dgvUserData.Rows[i].Cells["Usum_money"].Value = strMHourMoney;
                    }

                }
                foreach (DataGridViewRow dgvr in dgvUserData.Rows)
                {
                    string strPCode = CommonCtrl.IsNullToString(dgvr.Cells["dispatch_no"].Value);
                    if (!string.IsNullOrEmpty(strPCode) && !listUser.Contains(strPCode))
                    {
                        listUser.Add(strPCode);
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 验证必填项
        private Boolean CheckControlValue()
        {
            try
            {
                errorProvider1.Clear();               
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
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
                return false;
            }
            return true;
        }
        #endregion

        #region 保存事件
        void UCDispatchDetails_AddSaveEvent(object sender, EventArgs e)
        {
            try
            {
                if (!CheckControlValue()) return;
                if (MessageBoxEx.Show("确认要保存吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
                List<SQLObj> listSql = new List<SQLObj>();
                UpdateRepairOrderInfo(listSql, DStatus);
                AttachmentInfo(listSql);
                SaveProjectData(listSql, strReceiveId, PStatus);
                SaveMaterialsData(listSql, strReceiveId);
                SaveOtherData(listSql, strReceiveId);
                #region 保存调度人员的信息
                List<SQLObj> listSqlUser = new List<SQLObj>();
                string strProjectID = string.Empty;
                for (int i = 0; i < dgvproject.RowCount; i++)
                {

                    DataGridViewRow dgvr = dgvproject.Rows[i];
                    object isCheck = dgvr.Cells["colCheck"].Value;
                    if (isCheck != null && (bool)isCheck)
                    {
                        strProjectID = CommonCtrl.IsNullToString(dgvr.Cells["item_id"].Value);
                        DispatchUserInfo(listSqlUser, strReceiveId, strProjectID);
                    }
                }
                DBHelper.BatchExeSQLMultiByTrans("添加分配工时人员信息", listSqlUser);
                #endregion
                if (DBHelper.BatchExeSQLMultiByTrans(opName, listSql))
                {
                    MessageBoxEx.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    uc.BindPageData();
                    isAutoClose = true;
                    base.deleteMenuByTag(this.Tag.ToString(), this.uc.Name);
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
                MessageBoxEx.Show("保存失败！" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 更新维修单基本信息
        /// <summary>
        /// 更新维修单基本信息
        /// </summary>
        /// <param name="listSql">SQLObj list</param>
        /// <param name="DStatus">调度状态枚举</param>
        private void UpdateRepairOrderInfo(List<SQLObj> listSql, DataSources.EnumDispatchStatus DStatus)
        {
            try
            {
                SQLObj obj = new SQLObj();
                obj.cmdType = CommandType.Text;
                Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                dicParam.Add("dispatch_status", new ParamObj("dispatch_status", Convert.ToInt32(DStatus).ToString(), SysDbType.VarChar, 1));//调度状态
                if (DStatus == DataSources.EnumDispatchStatus.FinishWork)
                {
                    dicParam.Add("complete_work_time", new ParamObj("complete_work_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//完工时间               
                }
                else
                {
                    dicParam.Add("complete_work_time", new ParamObj("complete_work_time", null, SysDbType.BigInt));//完工时间               
                }
                dicParam.Add("set_meal", new ParamObj("set_meal", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobSetMeal.SelectedValue)) ? cobSetMeal.SelectedValue.ToString() : null, SysDbType.VarChar, 40));//维修套餐
                dicParam.Add("driver_name", new ParamObj("driver_name", txtDriver.Caption.Trim(), SysDbType.VarChar, 20));//报修人
                if (!string.IsNullOrEmpty(txtDriverPhone.Caption.Trim()))//报修人手机
                {
                    dicParam.Add("driver_mobile", new ParamObj("driver_mobile", txtDriverPhone.Caption.Trim(), SysDbType.VarChar, 15));//报修人手机
                }
                else
                {
                    dicParam.Add("driver_mobile", new ParamObj("driver_mobile", null, SysDbType.VarChar, 15));//报修人手机
                }
                dicParam.Add("travel_mileage", new ParamObj("travel_mileage", !string.IsNullOrEmpty(txtMil.Caption.Trim()) ? txtMil.Caption.Trim() : null, SysDbType.Decimal, 15));//行驶里程
                dicParam.Add("fault_describe", new ParamObj("fault_describe", txtDesc.Caption.Trim(), SysDbType.VarChar, 200));//故障描述
                dicParam.Add("maintain_id", new ParamObj("maintain_id", strReceiveId, SysDbType.VarChar, 40));//Id
                dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
                dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
                dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                if (!string.IsNullOrEmpty(strInspection))
                {
                    dicParam.Add("Verify_advice", new ParamObj("Verify_advice", strInspection, SysDbType.VarChar, 200));//质检意见 
                    obj.sqlString = @"update tb_maintain_info set dispatch_status=@dispatch_status,set_meal=@set_meal
                   ,driver_name=@driver_name,driver_mobile=@driver_mobile,travel_mileage=@travel_mileage,fault_describe=@fault_describe
                   ,update_by=@update_by,update_name=@update_name,update_time=@update_time,Verify_advice=@Verify_advice,complete_work_time=@complete_work_time
                   where maintain_id=@maintain_id";
                }
                else
                {
                    obj.sqlString = @"update tb_maintain_info set dispatch_status=@dispatch_status,complete_work_time=@complete_work_time,set_meal=@set_meal
                    ,driver_name=@driver_name,driver_mobile=@driver_mobile,travel_mileage=@travel_mileage,fault_describe=@fault_describe
                    ,update_by=@update_by,update_name=@update_name,update_time=@update_time
                    where maintain_id=@maintain_id";
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

        #region 更新附件信息
        private void AttachmentInfo(List<SQLObj> listSql)
        {
            ucAttr.TableName = "tb_maintain_info";
            ucAttr.TableNameKeyValue = strReceiveId;
            listSql.AddRange(ucAttr.AttachmentSql);
        }
        #endregion

        #region 维修项目信息保存
        private void SaveProjectData(List<SQLObj> listSql, string partID, DataSources.EnumProjectDisStatus PStatus)
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
                        dicParam.Add("item_no", new ParamObj("item_no", dgvr.Cells["item_no"].Value, SysDbType.VarChar, 40));//项目编码
                        dicParam.Add("item_type", new ParamObj("item_type", dgvr.Cells["item_type"].Value, SysDbType.VarChar, 40));//项目维修类别
                        dicParam.Add("item_name", new ParamObj("item_name", dgvr.Cells["item_name"].Value, SysDbType.VarChar, 40));//项目名称
                        dicParam.Add("man_hour_type", new ParamObj("man_hour_type", dgvr.Cells["man_hour_type"].Value, SysDbType.VarChar, 40));//工时类别
                        string strHourQuantity = CommonCtrl.IsNullToString(dgvr.Cells["man_hour_quantity"].Value);//工时数量
                        if (!string.IsNullOrEmpty(strHourQuantity))//工时单价
                        {
                            dicParam.Add("man_hour_quantity", new ParamObj("man_hour_quantity", strHourQuantity, SysDbType.Decimal, 15));
                        }
                        else
                        {
                            dicParam.Add("man_hour_quantity", new ParamObj("man_hour_quantity", null, SysDbType.Decimal, 15));
                        }
                        //会员工时价
                        dicParam.Add("man_hour_norm_unitprice", new ParamObj("man_hour_norm_unitprice", dgvr.Cells["man_hour_norm_unitprice"].Value, SysDbType.Decimal, 15));
                        dicParam.Add("member_discount", new ParamObj("member_discount", dgvr.Cells["member_discount"].Value, SysDbType.Decimal, 5));//会员折扣                   
                        dicParam.Add("member_price", new ParamObj("member_price", dgvr.Cells["member_price"].Value, SysDbType.Decimal, 15));//会员工时费
                        dicParam.Add("member_sum_money", new ParamObj("member_sum_money", dgvr.Cells["member_sum_money"].Value, SysDbType.Decimal, 15));//折扣额
                        dicParam.Add("sum_money_goods", new ParamObj("sum_money_goods", dgvr.Cells["sum_money_goods"].Value, SysDbType.Decimal, 15));//货款
                        dicParam.Add("repair_progress", new ParamObj("repair_progress", Convert.ToInt32(PStatus), SysDbType.VarChar, 40));//维修进度

                        string strIsThree = CommonCtrl.IsNullToString(dgvr.Cells["three_warranty"].Value);
                        if (!string.IsNullOrEmpty(strIsThree))
                        {
                            dicParam.Add("three_warranty", new ParamObj("three_warranty", strIsThree == "是" ? "1" : "0", SysDbType.VarChar, 2));
                        }
                        else
                        {
                            dicParam.Add("three_warranty", new ParamObj("three_warranty", null, SysDbType.VarChar, 2));
                        }
                        //开工时间
                        dicParam.Add("start_work_time", new ParamObj("start_work_time", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["start_work_time"].Value)) ? Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dgvr.Cells["start_work_time"].Value)).ToString() : null, SysDbType.BigInt));
                        //完工时间
                        dicParam.Add("complete_work_time", new ParamObj("complete_work_time", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["complete_work_time"].Value)) ? Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dgvr.Cells["complete_work_time"].Value)).ToString() : null, SysDbType.BigInt));
                        //停工时间
                        dicParam.Add("shut_down_time", new ParamObj("shut_down_time", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["shut_down_time"].Value)) ? Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dgvr.Cells["shut_down_time"].Value)).ToString() : null, SysDbType.BigInt));
                        //停工原因
                        dicParam.Add("shut_down_reason", new ParamObj("shut_down_reason", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["shut_down_reason"].Value)) ? dgvr.Cells["shut_down_reason"].Value.ToString() : null, SysDbType.VarChar, 200));
                        //停工累计时
                        dicParam.Add("shut_down_duration", new ParamObj("shut_down_duration", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["shut_down_duration"].Value)) ? dgvr.Cells["shut_down_duration"].Value.ToString() : null, SysDbType.Decimal, 15));
                        //继续开工时间
                        dicParam.Add("continue_time", new ParamObj("continue_time", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["continue_time"].Value)) ? Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dgvr.Cells["continue_time"].Value)).ToString() : null, SysDbType.BigInt));
                        dicParam.Add("remarks", new ParamObj("remarks", dgvr.Cells["remarks"].Value, SysDbType.VarChar, 200));
                        dicParam.Add("enable_flag", new ParamObj("enable_flag", "1", SysDbType.VarChar, 1));
                        dicParam.Add("whours_id", new ParamObj("whours_id", dgvr.Cells["whours_id"].Value, SysDbType.VarChar, 40));
                        dicParam.Add("data_source", new ParamObj("data_source", dgvr.Cells["data_source"].Value, SysDbType.VarChar, 5));                        
                        string strPID = CommonCtrl.IsNullToString(dgvr.Cells["item_id"].Value);
                        if (strPID == "NewId")
                        {
                            opName = "新增调度维修项目";
                            strPID = Guid.NewGuid().ToString();
                            dicParam.Add("item_id", new ParamObj("item_id", strPID, SysDbType.VarChar, 40));
                            obj.sqlString = @"insert into [tb_maintain_item] (item_id,maintain_id,item_no,item_type,item_name,man_hour_type,man_hour_quantity,man_hour_norm_unitprice,member_discount,member_price,member_sum_money,sum_money_goods
                        ,repair_progress,three_warranty,start_work_time,complete_work_time,shut_down_time,shut_down_reason,shut_down_duration,continue_time,remarks,enable_flag,whours_id,data_source)
                        values (@item_id,@maintain_id,@item_no,@item_type,@item_name,@man_hour_type,@man_hour_quantity,@man_hour_norm_unitprice,@member_discount,@member_price,@member_sum_money,@sum_money_goods
                        ,@repair_progress,@three_warranty,@start_work_time,@complete_work_time,@shut_down_time,@shut_down_reason,@shut_down_duration,@continue_time,@remarks,@enable_flag,@whours_id,@data_source);";
                        }
                        else
                        {
                            dicParam.Add("item_id", new ParamObj("item_id", dgvr.Cells["item_id"].Value, SysDbType.VarChar, 40));
                            opName = "更新调度维修项目";
                            obj.sqlString = @"update tb_maintain_item set item_no=@item_no,item_type=@item_type,item_name=@item_name,man_hour_type=@man_hour_type,man_hour_quantity=@man_hour_quantity,man_hour_norm_unitprice=@man_hour_norm_unitprice,member_discount=@member_discount,member_price=@member_price,
                        member_sum_money=@member_sum_money,sum_money_goods=@sum_money_goods,repair_progress=@repair_progress,three_warranty=@three_warranty,start_work_time=@start_work_time,complete_work_time=@complete_work_time,shut_down_time=@shut_down_time
                        ,shut_down_reason=@shut_down_reason,shut_down_duration=@shut_down_duration,continue_time=@continue_time,remarks=@remarks,enable_flag=@enable_flag,whours_id=@whours_id,data_source=@data_source where item_id=@item_id";
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
                        dicParam.Add("parts_code", new ParamObj("parts_code", dgvr.Cells["parts_code"].Value, SysDbType.VarChar, 40));//配件编码
                        dicParam.Add("parts_name", new ParamObj("parts_name", dgvr.Cells["parts_name"].Value, SysDbType.VarChar, 40));//配件名称
                        dicParam.Add("norms", new ParamObj("norms", dgvr.Cells["norms"].Value, SysDbType.VarChar, 40));//规格
                        dicParam.Add("unit", new ParamObj("unit", dgvr.Cells["unit"].Value, SysDbType.VarChar, 20));//单位
                        string strIsimport = CommonCtrl.IsNullToString(dgvr.Cells["whether_imported"].Value);//进口
                        if (!string.IsNullOrEmpty(strIsimport))
                        {
                            dicParam.Add("whether_imported", new ParamObj("whether_imported", strIsimport == "是" ? "1" : "0", SysDbType.VarChar, 1));
                        }
                        else
                        {
                            dicParam.Add("whether_imported", new ParamObj("whether_imported", null, SysDbType.VarChar, 1));
                        }
                        //数量
                        dicParam.Add("quantity", new ParamObj("quantity", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["quantity"].Value)) ? dgvr.Cells["quantity"].Value : null, SysDbType.Decimal, 15));
                        //原始单价
                        dicParam.Add("unit_price", new ParamObj("unit_price", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["unit_price"].Value)) ? dgvr.Cells["unit_price"].Value : null, SysDbType.Decimal, 15));
                        //会员折扣
                        dicParam.Add("member_discount", new ParamObj("member_discount", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["Mmember_discount"].Value)) ? dgvr.Cells["Mmember_discount"].Value : null, SysDbType.Decimal, 15));
                        //会员单价
                        dicParam.Add("member_price", new ParamObj("member_price", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["Mmember_price"].Value)) ? dgvr.Cells["Mmember_price"].Value : null, SysDbType.Decimal, 15));
                        //折扣额
                        dicParam.Add("member_sum_money", new ParamObj("member_sum_money", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["Mmember_sum_money"].Value)) ? dgvr.Cells["Mmember_sum_money"].Value : null, SysDbType.Decimal, 15));
                        //货款
                        dicParam.Add("sum_money", new ParamObj("sum_money", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["sum_money"].Value)) ? dgvr.Cells["sum_money"].Value : null, SysDbType.Decimal, 15));
                        //图号
                        dicParam.Add("drawn_no", new ParamObj("drawn_no", dgvr.Cells["drawn_no"].Value, SysDbType.VarChar, 40));
                        //使用车型
                        dicParam.Add("vehicle_brand", new ParamObj("vehicle_brand", dgvr.Cells["vehicle_brand"].Value, SysDbType.VarChar, 2000));
                        string strIsThree = CommonCtrl.IsNullToString(dgvr.Cells["Mthree_warranty"].Value);
                        if (!string.IsNullOrEmpty(strIsThree))//是否三包
                        {
                            dicParam.Add("three_warranty", new ParamObj("three_warranty", strIsThree == "是" ? "1" : "0", SysDbType.VarChar, 2));
                        }
                        else
                        {
                            dicParam.Add("three_warranty", new ParamObj("three_warranty", null, SysDbType.VarChar, 2));
                        }
                        //备注
                        dicParam.Add("remarks", new ParamObj("remarks", dgvr.Cells["Mremarks"].Value, SysDbType.VarChar, 200));
                        dicParam.Add("enable_flag", new ParamObj("enable_flag", "1", SysDbType.VarChar, 1));
                        dicParam.Add("parts_id", new ParamObj("parts_id", dgvr.Cells["parts_id"].Value, SysDbType.VarChar, 40));
                        dicParam.Add("data_source", new ParamObj("data_source", dgvr.Cells["Mdata_source"].Value, SysDbType.VarChar, 5));  
                        string strPID = CommonCtrl.IsNullToString(dgvr.Cells["material_id"].Value);
                        if (strPID.Length == 0)
                        {
                            opName = "新增调度维修用料";
                            strPID = Guid.NewGuid().ToString();
                            dicParam.Add("material_id", new ParamObj("material_id", strPID, SysDbType.VarChar, 40));
                            obj.sqlString = "insert into [tb_maintain_material_detail] (material_id,parts_code,parts_name,norms,unit,whether_imported,quantity,unit_price,member_discount,member_price,member_sum_money,sum_money,drawn_no,vehicle_brand,three_warranty,remarks,enable_flag,maintain_id,parts_id,data_source) ";
                            obj.sqlString += " values (@material_id,@parts_code,@parts_name,@norms,@unit,@whether_imported,@quantity,@unit_price,@member_discount,@member_price,@member_sum_money,@sum_money,@drawn_no,@vehicle_brand,@three_warranty,@remarks,@enable_flag,@maintain_id,@parts_id,@data_source);";
                        }
                        else
                        {
                            dicParam.Add("material_id", new ParamObj("material_id", dgvr.Cells["material_id"].Value, SysDbType.VarChar, 40));
                            opName = "更新调度维修用料";
                            obj.sqlString = "update tb_maintain_material_detail set parts_code=@parts_code,parts_name=@parts_name,norms=@norms,unit=@unit,whether_imported=@whether_imported,quantity=@quantity,unit_price=@unit_price,member_discount=@member_discount,member_price=@member_price,sum_money=@sum_money,";
                            obj.sqlString += "drawn_no=@drawn_no,vehicle_brand=@vehicle_brand,member_sum_money=@member_sum_money, three_warranty=@three_warranty,remarks=@remarks,enable_flag=@enable_flag,parts_id=@parts_id,data_source=@data_source where material_id=@material_id";
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
                            opName = "新增调度其他项目收费";
                            strTollID = Guid.NewGuid().ToString();
                            dicParam.Add("toll_id", new ParamObj("toll_id", strTollID, SysDbType.VarChar, 40));
                            obj.sqlString = "insert into [tb_maintain_other_toll] (toll_id,cost_types,sum_money,remarks,maintain_id,enable_flag) values (@toll_id,@cost_types,@sum_money,@remarks,@maintain_id,@enable_flag);";
                        }
                        else
                        {
                            dicParam.Add("toll_id", new ParamObj("toll_id", dgvr.Cells["toll_id"].Value, SysDbType.VarChar, 40));
                            opName = "更新调度其他项目收费";
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

        #region 人员分配工时信息保存
        /// <summary>
        /// 人员分配工时信息保存
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="strMaintainId">关联的维修单ID</param>
        /// <param name="strProjectId">关联的维修项目Id</param>
        private void DispatchUserInfo(List<SQLObj> listSql, string strMaintainId, string strProjectId)
        {
            try
            {
                for (int i = 0; i < dgvUserData.RowCount; i++)
                {
                    DataGridViewRow dgvr = dgvUserData.Rows[i];
                    object isCheck = dgvr.Cells["UcolCheck"].Value;
                    if (isCheck != null && (bool)isCheck)
                    {
                        SQLObj obj = new SQLObj();
                        obj.cmdType = CommandType.Text;
                        Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                        string strUNo = CommonCtrl.IsNullToString(dgvr.Cells["dispatch_no"].Value);
                        if (strUNo.Length > 0)
                        {
                            dicParam.Add("maintain_id", new ParamObj("maintain_id", strMaintainId, SysDbType.VarChar, 40));//Id
                            dicParam.Add("dispatch_no", new ParamObj("dispatch_no", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["dispatch_no"].Value)) ? dgvr.Cells["dispatch_no"].Value.ToString() : null, SysDbType.VarChar, 40));//人员编码
                            dicParam.Add("dispatch_name", new ParamObj("dispatch_name", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["dispatch_name"].Value)) ? dgvr.Cells["dispatch_name"].Value.ToString() : null, SysDbType.VarChar, 40));//人员名称
                            dicParam.Add("org_name", new ParamObj("org_name", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["org_name"].Value)) ? dgvr.Cells["org_name"].Value.ToString() : null, SysDbType.VarChar, 40));//部门
                            dicParam.Add("team_name", new ParamObj("team_name", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["team_name"].Value)) ? dgvr.Cells["team_name"].Value : null, SysDbType.VarChar, 40));//班组
                            //分配工时
                            dicParam.Add("man_hour", new ParamObj("man_hour", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["man_hour"].Value)) ? dgvr.Cells["man_hour"].Value.ToString() : null, SysDbType.Decimal, 15));
                            //分配金额
                            dicParam.Add("sum_money", new ParamObj("sum_money", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["Usum_money"].Value)) ? dgvr.Cells["Usum_money"].Value.ToString() : null, SysDbType.Decimal, 15));
                            //分配工时时间
                            dicParam.Add("create_time", new ParamObj("create_time", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["create_time"].Value)) ? Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dgvr.Cells["create_time"].Value)).ToString() : null, SysDbType.BigInt));
                            dicParam.Add("item_id", new ParamObj("item_id", strProjectId, SysDbType.VarChar, 40));//Id

                            opName = "新增分配工时人员信息";
                            string strDUserId = Guid.NewGuid().ToString();
                            dicParam.Add("dispatch_id", new ParamObj("dispatch_id", strDUserId, SysDbType.VarChar, 40));
                            obj.sqlString = @"delete from tb_maintain_dispatch_worker where maintain_id=@maintain_id and item_id=@item_id and dispatch_no=@dispatch_no;
                        insert into tb_maintain_dispatch_worker(maintain_id,dispatch_no,dispatch_name,org_name,team_name,man_hour,sum_money,create_time,dispatch_id,item_id)
                        values (@maintain_id,@dispatch_no,@dispatch_name,@org_name,@team_name,@man_hour,@sum_money,@create_time,@dispatch_id,@item_id);";

                            obj.Param = dicParam;
                            listSql.Add(obj);
                            // 
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

        #region 取消事件
        void UCDispatchDetails_CancelEvent(object sender, EventArgs e)
        {
            try
            {
                if (MessageBoxEx.Show("确认要取消吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
                isAutoClose = true;
                base.deleteMenuByTag(this.Tag.ToString(), uc.Name);
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 根据维修单Id获取信息
        private void GetReservData(string strRId)
        {
            try
            {
                DataTable dt = DBHelper.GetTable("维修调度单详情", "tb_maintain_info", "*", string.Format(" maintain_id='{0}'", strRId), "", "");
                if (dt.Rows.Count > 0)
                {
                    #region 基本信息
                    DataRow dr = dt.Rows[0];
                    labMaintain_noS.Text = CommonCtrl.IsNullToString(dr["maintain_no"]);//维修单号
                    string strReTime = CommonCtrl.IsNullToString(dr["reception_time"]);//接待时间
                    if (!string.IsNullOrEmpty(strReTime))
                    {
                        labRTimeS.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(strReTime)).ToString("yyyy-MM-dd HH:mm");
                    }
                    else
                    {
                        labRTimeS.Text = string.Empty;
                    }
                    labCustomNOS.Text = CommonCtrl.IsNullToString(dr["customer_code"]);//客户编码
                    labCustomNameS.Text = CommonCtrl.IsNullToString(dr["customer_name"]);//客户名称
                    labCustomNOS.Tag = CommonCtrl.IsNullToString(dr["customer_id"]);//客户Id
                    GetMemberInfo(CommonCtrl.IsNullToString(dr["customer_id"]));
                    labContactS.Text = CommonCtrl.IsNullToString(dr["linkman"]);//联系人
                    labContactPhoneS.Text = CommonCtrl.IsNullToString(dr["link_man_mobile"]);//联系人电话
                    labCarNOS.Text = CommonCtrl.IsNullToString(dr["vehicle_no"]);//车牌号
                    labCarTypeS.Text = GetVehicleModels(CommonCtrl.IsNullToString(dr["vehicle_model"]));//车型
                    labCarTypeS.Tag = CommonCtrl.IsNullToString(dr["vehicle_model"]);
                    labCarBrandS.Text = GetDicName(CommonCtrl.IsNullToString(dr["vehicle_brand"]));//车辆品牌
                    labCarBrandS.Tag = CommonCtrl.IsNullToString(dr["vehicle_brand"]);
                    labVINS.Text = CommonCtrl.IsNullToString(dr["vehicle_vin"]);//VIN
                    labEngineNoS.Text = CommonCtrl.IsNullToString(dr["engine_no"]);//发动机号
                    labturnerS.Text = CommonCtrl.IsNullToString(dr["turner"]);//车厂编码
                    txtDriver.Caption = CommonCtrl.IsNullToString(dr["driver_name"]);//报修人
                    txtDriverPhone.Caption = CommonCtrl.IsNullToString(dr["driver_mobile"]);//报修人手机
                    labRepTypeS.Text = GetDicName(CommonCtrl.IsNullToString(dr["maintain_type"]));//维修类别
                    labRepTypeS.Tag = CommonCtrl.IsNullToString(dr["maintain_type"]);
                    labPayTypeS.Text = GetDicName(CommonCtrl.IsNullToString(dr["maintain_payment"]));//维修付费方式
                    labPayTypeS.Tag = CommonCtrl.IsNullToString(dr["maintain_payment"]);
                    string strInTime = CommonCtrl.IsNullToString(dr["completion_time"]);//预计完工时间
                    if (!string.IsNullOrEmpty(strInTime))
                    {
                        labSuTimeS.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(strInTime)).ToString("yyyy-MM-dd HH:mm");
                    }
                    else
                    {
                        labSuTimeS.Text = string.Empty;
                    }
                    labMlS.Text = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["oil_into_factory"])) ? CommonCtrl.IsNullToString(dr["oil_into_factory"]) + "%" : "";//进场油量
                    txtMil.Caption = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["travel_mileage"])) ? CommonCtrl.IsNullToString(dr["travel_mileage"]): "";//行驶里程
                    txtDesc.Caption = CommonCtrl.IsNullToString(dr["fault_describe"]);//故障描述 
                    labRemarkS.Text = CommonCtrl.IsNullToString(dr["remark"]);//备注
                    labStatusS.Text = DataSources.GetDescription(typeof(DataSources.EnumDispatchStatus), int.Parse(CommonCtrl.IsNullToString(dr["dispatch_status"])));//单据状态
                    strProjectSattus = CommonCtrl.IsNullToString(dr["dispatch_status"]);
                    //labMoney.Text = CommonCtrl.IsNullToString(dr["maintain_payment"]);//欠款余额
                    labDepartS.Text = GetDepartmentName(CommonCtrl.IsNullToString(dr["org_name"]));//部门
                    labDepartS.Tag = CommonCtrl.IsNullToString(dr["org_name"]);
                    labAttnS.Text = GetUserSetName(CommonCtrl.IsNullToString(dr["responsible_name"]));//经办人
                    labAttnS.Tag = CommonCtrl.IsNullToString(dr["responsible_name"]);
                    labCreatePersonS.Text = CommonCtrl.IsNullToString(dr["create_name"]);//创建人
                    string strCreateTime = CommonCtrl.IsNullToString(dr["create_time"]); //创建时间
                    if (!string.IsNullOrEmpty(strCreateTime))
                    {
                        labCreateTimeS.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(strCreateTime)).ToString("yyyy-MM-dd HH:mm");
                    }
                    else
                    {
                        labCreateTimeS.Text = string.Empty;
                    }
                    labFinallyPerS.Text = CommonCtrl.IsNullToString(dr["update_name"]);//最后编辑人
                    string strFinallyTime = CommonCtrl.IsNullToString(dr["update_time"]); //最后编辑时间
                    if (!string.IsNullOrEmpty(strFinallyTime))
                    {
                        labFinallyTimeS.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(strFinallyTime)).ToString("yyyy-MM-dd HH:mm");
                    }
                    else
                    {
                        labFinallyTimeS.Text = string.Empty;
                    }
                    cobSetMeal.SelectedValue = CommonCtrl.IsNullToString(dr["set_meal"]);//套餐

                    #endregion

                    #region 底部datagridview数据

                    #region 维修项目数据
                    //维修项目数据  
                    DataTable dpt = DBHelper.GetTable("维修项目数据", "tb_maintain_item", "*", string.Format(" maintain_id='{0}'and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", strRId), "", ""); ;
                    if (dpt.Rows.Count > 0)
                    {
                        if (dpt.Rows.Count >= dgvproject.Rows.Count)
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
                            dgvproject.Rows[0].Cells["colCheck"].Value = true;
                            dgvproject.Rows[i].Cells["item_id"].Value = CommonCtrl.IsNullToString(dpr["item_id"]);
                            dgvproject.Rows[i].Cells["whours_id"].Value = CommonCtrl.IsNullToString(dpr["item_id"]);
                            dgvproject.Rows[i].Cells["three_warranty"].Value = CommonCtrl.IsNullToString(dpr["three_warranty"]) == "1" ? "是" : "否";
                            dgvproject.Rows[i].Cells["man_hour_type"].Value = CommonCtrl.IsNullToString(dpr["man_hour_type"]);
                            dgvproject.Rows[i].Cells["item_no"].Value = CommonCtrl.IsNullToString(dpr["item_no"]);
                            dgvproject.Rows[i].Cells["item_name"].Value = CommonCtrl.IsNullToString(dpr["item_name"]);
                            dgvproject.Rows[i].Cells["item_type"].Value = CommonCtrl.IsNullToString(dpr["item_type"]);
                            dgvproject.Rows[i].Cells["man_hour_quantity"].Value = CommonCtrl.IsNullToString(dpr["man_hour_quantity"]);
                            dgvproject.Rows[i].Cells["man_hour_norm_unitprice"].ReadOnly = CommonCtrl.IsNullToString(dpr["man_hour_type"]) == "工时" ? true : false;
                            dgvproject.Rows[i].Cells["man_hour_norm_unitprice"].Value = CommonCtrl.IsNullToString(dpr["man_hour_norm_unitprice"]);
                            dgvproject.Rows[i].Cells["remarks"].Value = CommonCtrl.IsNullToString(dpr["remarks"]);
                            dgvproject.Rows[i].Cells["sum_money_goods"].Value = CommonCtrl.IsNullToString(dpr["sum_money_goods"]);
                            dgvproject.Rows[i].Cells["member_discount"].Value = CommonCtrl.IsNullToString(dpr["member_discount"]);
                            dgvproject.Rows[i].Cells["member_price"].Value = CommonCtrl.IsNullToString(dpr["member_price"]);
                            dgvproject.Rows[i].Cells["member_sum_money"].Value = CommonCtrl.IsNullToString(dpr["member_sum_money"]);
                            dgvproject.Rows[i].Cells["whours_id"].Value = CommonCtrl.IsNullToString(dpr["whours_id"]);
                            dgvproject.Rows[i].Cells["repair_progress"].Value = DataSources.GetDescription(typeof(DataSources.EnumProjectDisStatus), int.Parse(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["repair_progress"])) ? CommonCtrl.IsNullToString(dpr["repair_progress"]) : "0"));//项目状态 CommonCtrl.IsNullToString(dpr["repair_progress"]);
                            string StartTime = CommonCtrl.IsNullToString(dpr["start_work_time"]);//开工时间
                            dgvproject.Rows[i].Cells["start_work_time"].Value = !string.IsNullOrEmpty(StartTime) ? Common.UtcLongToLocalDateTime(Convert.ToInt64(StartTime)).ToString() : "";
                            string StopTime = CommonCtrl.IsNullToString(dpr["shut_down_time"]);//停工时间
                            dgvproject.Rows[i].Cells["shut_down_time"].Value = !string.IsNullOrEmpty(StopTime) ? Common.UtcLongToLocalDateTime(Convert.ToInt64(StopTime)).ToString() : "";
                            string ComplateTime = CommonCtrl.IsNullToString(dpr["complete_work_time"]);//完工时间
                            dgvproject.Rows[i].Cells["complete_work_time"].Value = !string.IsNullOrEmpty(ComplateTime) ? Common.UtcLongToLocalDateTime(Convert.ToInt64(ComplateTime)).ToString() : "";
                            dgvproject.Rows[i].Cells["shut_down_reason"].Value = CommonCtrl.IsNullToString(dpr["shut_down_reason"]);//停工原因
                            string ContinueTime = CommonCtrl.IsNullToString(dpr["continue_time"]);//继续时间
                            dgvproject.Rows[i].Cells["continue_time"].Value = !string.IsNullOrEmpty(ContinueTime) ? Common.UtcLongToLocalDateTime(Convert.ToInt64(ContinueTime)).ToString() : "";
                            string DurationTime = CommonCtrl.IsNullToString(dpr["shut_down_duration"]);//停工累计时长
                            dgvproject.Rows[i].Cells["shut_down_duration"].Value = !string.IsNullOrEmpty(DurationTime) ? DurationTime : "";
                            dgvproject.Rows[i].Cells["data_source"].Value = CommonCtrl.IsNullToString(dpr["data_source"]);
                            dgvproject.Rows[i].Cells["three_warranty"].ReadOnly = CommonCtrl.IsNullToString(dpr["data_source"]) == Convert.ToInt32(DataSources.EnumDataSources.SELFBUILD).ToString() ? true : false;
                            listProject.Add(CommonCtrl.IsNullToString(dpr["item_no"]));
                        }
                    }
                    #endregion

                    #region 维修用料数据
                    //维修用料数据   
                    DataTable dmt = DBHelper.GetTable("维修用料数据", "tb_maintain_material_detail", "*", string.Format(" maintain_id='{0}' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", strRId), "", "");
                    if (dmt.Rows.Count > 0)
                    {

                        if (dmt.Rows.Count >= dgvMaterials.Rows.Count)
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
                            dgvMaterials.Rows[i].Cells["quantity"].Value = CommonCtrl.IsNullToString(dmr["quantity"]);
                            dgvMaterials.Rows[i].Cells["unit_price"].Value = CommonCtrl.IsNullToString(dmr["unit_price"]);
                            dgvMaterials.Rows[i].Cells["Mmember_discount"].Value =!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dmr["member_discount"]))?CommonCtrl.IsNullToString(dmr["member_discount"]):"100";
                            dgvMaterials.Rows[i].Cells["Mmember_price"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dmr["member_price"])) ? CommonCtrl.IsNullToString(dmr["member_price"]) : CommonCtrl.IsNullToString(dmr["unit_price"]);
                            dgvMaterials.Rows[i].Cells["Mmember_sum_money"].Value = Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dmr["unit_price"])) ? CommonCtrl.IsNullToString(dmr["unit_price"]) : "0") - Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dmr["member_price"])) ? CommonCtrl.IsNullToString(dmr["member_price"]) : "0");
                            dgvMaterials.Rows[i].Cells["sum_money"].Value = CommonCtrl.IsNullToString(dmr["sum_money"]);
                            dgvMaterials.Rows[i].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dmr["drawn_no"]);
                            dgvMaterials.Rows[i].Cells["vehicle_brand"].Value = CommonCtrl.IsNullToString(dmr["vehicle_brand"]);
                            dgvMaterials.Rows[i].Cells["Mthree_warranty"].Value = CommonCtrl.IsNullToString(dmr["three_warranty"]) == "1" ? "是" : "否";
                            dgvMaterials.Rows[i].Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dmr["remarks"]);
                            dgvMaterials.Rows[i].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dmr["whether_imported"]) == "1" ? "是" : "否";
                            dgvMaterials.Rows[i].Cells["parts_id"].Value = CommonCtrl.IsNullToString(dmr["parts_id"]);
                            string strMaterialNo = DBHelper.GetSingleValue("获得领料单编号", "tb_maintain_fetch_material", "material_num", "maintain_id='" + labMaintain_noS.Text.Trim() + "'", "");
                            dgvMaterials.Rows[i].Cells["Mmaterial_no"].Value = strMaterialNo;
                            dgvMaterials.Rows[i].Cells["Mdata_source"].Value = CommonCtrl.IsNullToString(dmr["data_source"]);
                            dgvMaterials.Rows[i].Cells["Mthree_warranty"].ReadOnly = CommonCtrl.IsNullToString(dmr["data_source"]) == Convert.ToInt32(DataSources.EnumDataSources.SELFBUILD).ToString() ? true : false;
                            listMater.Add(CommonCtrl.IsNullToString(dmr["parts_code"]));
                        }

                    }
                    #endregion

                    #region 其他项目收费数据
                    //其他项目收费数据             
                    DataTable dot = DBHelper.GetTable("其他项目收费数据", "tb_maintain_other_toll", "*", string.Format(" maintain_id='{0}'", strRId), "", "");
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
                    #endregion

                    #region 附件信息数据
                    //附件信息数据
                    ucAttr.TableName = "tb_maintain_info";
                    ucAttr.TableNameKeyValue = strRId;
                    ucAttr.BindAttachment();
                    #endregion

                    #region 相关领料情况
                    string strFildes = @"a.material_num,a.fetch_time,b.parts_code,b.parts_name,b.unit,b.whether_imported, 
                                    b.quantity,b.drawn_no,b.vehicle_model,(select user_name from v_user where user_id=a.fetch_opid) as fetch_opid,b.remarks";
                    string strTable = @"tb_maintain_fetch_material a 
                                    left join tb_maintain_fetch_material_detai b on a. fetch_id=b.fetch_id";
                    //string strWhere = " a.maintain_id='" + labMaintain_noS.Text.Trim() + "' and (a.info_status='" + Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString() + "' or a.info_status='" + Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString() + "')";
                    string strWhere = " a.maintain_id='" + labMaintain_noS.Text.Trim() + "' and b.enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'";
                    DataTable dft = DBHelper.GetTable("获取领料单情况", strTable, strFildes, strWhere, "", "order by a.fetch_time desc ");
                    dgvFData.DataSource = dft;
                    decFPartsCount = 0;
                    foreach (DataGridViewRow dgvr in dgvFData.Rows)
                    {
                        decFPartsCount += Convert.ToDecimal(CommonCtrl.IsNullToString(dgvr.Cells["Fquantity"].Value));
                    }
                    #endregion

                    #region 分配工时人员信息
                    //其他项目收费数据  
                    string strProD = string.Empty;
                    if (dpt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dpt.Rows.Count; i++)
                        {
                            DataRow dpr = dpt.Rows[0];
                            strProD = CommonCtrl.IsNullToString(dpr["item_id"]);
                            break;
                        }
                    }
                    DataTable dut = DBHelper.GetTable("分配工时人员信息", "tb_maintain_dispatch_worker a left join tb_maintain_item b on a.item_id=b.item_id", "*", string.Format(" a.maintain_id='{0}' and a.item_id='" + strProD + "'", strRId), "", "");
                    if (dut.Rows.Count > 0)
                    {
                        if (dut.Rows.Count >= dgvUserData.Rows.Count)
                        {
                            dgvUserData.Rows.Add(dut.Rows.Count - dgvUserData.Rows.Count + 2);
                        }
                        else if ((dgvUserData.Rows.Count - dut.Rows.Count) == 1)
                        {
                            dgvUserData.Rows.Add(1);
                        }
                        for (int i = 0; i < dut.Rows.Count; i++)
                        {
                            DataRow dur = dut.Rows[i];
                            dgvUserData.Rows[i].Cells["UcolCheck"].Value = true;
                            dgvUserData.Rows[i].Cells["dispatch_id"].Value = CommonCtrl.IsNullToString(dur["dispatch_id"]);
                            dgvUserData.Rows[i].Cells["dispatch_no"].Value = CommonCtrl.IsNullToString(dur["dispatch_no"]);
                            dgvUserData.Rows[i].Cells["dispatch_name"].Value = CommonCtrl.IsNullToString(dur["dispatch_name"]);
                            dgvUserData.Rows[i].Cells["org_name"].Value = CommonCtrl.IsNullToString(dur["org_name"]);
                            dgvUserData.Rows[i].Cells["team_name"].Value = CommonCtrl.IsNullToString(dur["team_name"]);
                            dgvUserData.Rows[i].Cells["man_hour"].Value = CommonCtrl.IsNullToString(dur["man_hour"]);
                            dgvUserData.Rows[i].Cells["Usum_money"].Value = CommonCtrl.IsNullToString(dur["sum_money"]);
                            dgvUserData.Rows[i].Cells["Uitem_name"].Value = CommonCtrl.IsNullToString(dur["item_name"]);
                            dgvUserData.Rows[i].Cells["create_time"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dur["create_time"])) ? Common.UtcLongToLocalDateTime(dur["create_time"]) : "";
                            if (!listUser.Contains(CommonCtrl.IsNullToString(dur["dispatch_no"])))
                            {
                                listUser.Add(CommonCtrl.IsNullToString(dur["dispatch_no"]));
                            }
                        }
                    }
                    #endregion
                    #endregion
                }
                else
                {
                    #region 没有数据时全部显示为空
                    labMaintain_noS.Text = string.Empty;
                    labRTimeS.Text = string.Empty;
                    labAttnS.Text = string.Empty;
                    labCarBrandS.Text = string.Empty;
                    labCarNOS.Text = string.Empty;
                    labCarTypeS.Text = string.Empty;
                    labturnerS.Text = string.Empty;
                    labContactPhoneS.Text = string.Empty;
                    labContactS.Text = string.Empty;
                    labCreatePersonS.Text = string.Empty;
                    labCreateTimeS.Text = string.Empty;
                    labCustomNameS.Text = string.Empty;
                    labCustomNOS.Text = string.Empty;
                    labDepartS.Text = string.Empty;
                    txtDesc.Caption = string.Empty;
                    txtDriverPhone.Caption = string.Empty;
                    txtDriver.Caption = string.Empty;
                    labEngineNoS.Text = string.Empty;
                    labFinallyPerS.Text = string.Empty;
                    labFinallyTimeS.Text = string.Empty;
                    labPayTypeS.Text = string.Empty;
                    labRemarkS.Text = string.Empty;
                    labRepTypeS.Text = string.Empty;
                    labStatusS.Text = string.Empty;
                    labVINS.Text = string.Empty;
                    labMlS.Text = string.Empty;
                    txtMil.Caption = string.Empty;
                    cobSetMeal.SelectedValue = string.Empty;
                    #endregion
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 设置底部DataGridView的Anchor属性
        /// <summary>
        /// 设置底部DataGridView的Anchor属性
        /// </summary>
        private void SetDgvAnchor()
        {
            #region 维修项目设置
            dgvproject.Dock = DockStyle.Fill;
            dgvproject.ReadOnly = false;
            dgvproject.Rows.Add(3);
            dgvproject.AllowUserToAddRows = true;
            dgvproject.Columns["item_no"].ReadOnly = true;
            dgvproject.Columns["item_type"].ReadOnly = true;
            dgvproject.Columns["item_name"].ReadOnly = true;
            dgvproject.Columns["man_hour_type"].ReadOnly = true;
            dgvproject.Columns["man_hour_norm_unitprice"].ReadOnly = true;
            dgvproject.Columns["member_discount"].ReadOnly = true;
            dgvproject.Columns["member_price"].ReadOnly = true;
            dgvproject.Columns["sum_money_goods"].ReadOnly = true;
            dgvproject.Columns["repair_progress"].ReadOnly = true;
            dgvproject.Columns["start_work_time"].ReadOnly = true;
            dgvproject.Columns["complete_work_time"].ReadOnly = true;
            dgvproject.Columns["shut_down_reason"].ReadOnly = true;
            dgvproject.Columns["shut_down_time"].ReadOnly = true;
            dgvproject.Columns["continue_time"].ReadOnly = true;
            dgvproject.Columns["repair_progress"].ReadOnly = true;
            dgvproject.Columns["shut_down_duration"].ReadOnly = true;
            ControlsConfig.NumberLimitdgv(dgvproject, new List<string>() { "man_hour_quantity", "man_hour_norm_unitprice" });
            #endregion

            #region 维修用料设置
            dgvMaterials.Dock = DockStyle.Fill;
            dgvMaterials.ReadOnly = false;
            dgvMaterials.Rows.Add(3);
            dgvMaterials.AllowUserToAddRows = true;
            dgvMaterials.Columns["parts_code"].ReadOnly = true;
            dgvMaterials.Columns["parts_name"].ReadOnly = true;
            dgvMaterials.Columns["norms"].ReadOnly = true;
            dgvMaterials.Columns["unit"].ReadOnly = true;
            dgvMaterials.Columns["whether_imported"].ReadOnly = true;
            //dgvMaterials.Columns["unit_price"].ReadOnly = true;
            dgvMaterials.Columns["Mmember_discount"].ReadOnly = true;
            dgvMaterials.Columns["Mmember_price"].ReadOnly = true;
            dgvMaterials.Columns["Mmember_sum_money"].ReadOnly = true;
            dgvMaterials.Columns["sum_money"].ReadOnly = true;
            dgvMaterials.Columns["drawn_no"].ReadOnly = true;
            dgvMaterials.Columns["vehicle_brand"].ReadOnly = true;
            dgvMaterials.Columns["Mmaterial_no"].ReadOnly = true;
            dgvMaterials.Columns["material_id"].ReadOnly = true;
            ControlsConfig.NumberLimitdgv(dgvMaterials, new List<string>() { "quantity", "unit_price" });
            #endregion
            dgvFData.Dock = DockStyle.Fill;
            dgvFData.Rows.Add(3);
            #region 人员设置
            dgvUserData.Dock = DockStyle.Fill;
            dgvUserData.ReadOnly = false;
            dgvUserData.Rows.Add(3);
            dgvUserData.AllowUserToAddRows = true;
            dgvUserData.Columns["dispatch_no"].ReadOnly = true;
            dgvUserData.Columns["dispatch_name"].ReadOnly = true;
            dgvUserData.Columns["org_name"].ReadOnly = true;
            dgvUserData.Columns["create_time"].ReadOnly = true;
            //dgvUserData.Columns["Usum_money"].ReadOnly = true;
            #endregion
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

        #region 绑定维修套餐
        /// <summary>
        /// 绑定维修套餐
        /// </summary>
        private void BindRepairPackage()
        {
            string strWhere = "enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "' and valid_until>='" + Common.LocalDateTimeToUtcLong(DateTime.Now) + "' and period_validity <='" + Common.LocalDateTimeToUtcLong(DateTime.Now) + "' ";
            DataTable dt = DBHelper.GetTable("获取维修套餐信息", "sys_b_set_repair_package_set ", "repair_package_set_id,package_name", strWhere, "", "order by repair_package_set_id desc");
            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem("", "请选择"));
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new ListItem(dr["repair_package_set_id"], dr["package_name"].ToString()));
            }
            cobSetMeal.DataSource = list;
            cobSetMeal.ValueMember = "Value";
            cobSetMeal.DisplayMember = "Text";
        }
        #endregion

        #region 绑定工位信息
        /// <summary>
        /// 绑定工位信息
        /// </summary>
        private void BindStationInfo()
        {
            try
            {
                List<ListItem> list = new List<ListItem>();
                list.Add(new ListItem("", "请选择"));
                string sqlWhere = " parent_id in (select dic_id from sys_dictionaries where dic_code='sys_station_information' and enable_flag=1) ";
                DataTable dt_dic = DBHelper.GetTable("查询字典码表信息", "sys_dictionaries", "dic_id,dic_name", sqlWhere, "", " order by create_time ");
                if (dt_dic != null && dt_dic.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt_dic.Rows)
                    {
                        list.Add(new ListItem(dr["dic_id"], dr["dic_name"].ToString()));
                    }
                }
                team_name.DisplayMember = "Text";
                team_name.ValueMember = "Value";
                team_name.DataSource = list;
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 绑定其他费用类别
        /// <summary>
        /// 绑定其他费用类别
        /// </summary>
        private void BindOMoney()
        {
            try
            {
                dgvOther.Dock = DockStyle.Fill;
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
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 窗体Load事件
        private void UCDispatchDetails_Load(object sender, EventArgs e)
        {
            ControlsConfig.TextToDecimal(txtMil, false);
            BindRepairPackage();//维修套餐信息
            BindStationInfo();//工位信息 
            BindOMoney();//绑定其他费用类别
            GetReservData(strReceiveId);
            SetTopbuttonShow();
        }
        #endregion

        #region 顶部button按钮显示设置
        /// <summary>
        /// 顶部button按钮显示设置
        /// </summary>
        private void SetTopbuttonShow()
        {
            base.btnDelete.Visible = false;
            switch (strProjectSattus)
            {

                case "0"://未开工
                    //base.btnSave.Enabled = false;//保存 
                    base.btnStop.Enabled = false;//停工
                    base.btnComplete.Enabled = false;//完工
                    base.btnQC.Enabled = false;//质检    
                    createFetch.Enabled = false;//自动生成领料单
                    break;
                case "1"://已开工
                    //base.btnSave.Enabled = false;//保存 
                    base.btnDtalo.Enabled = true;//分配工时                   
                    base.btnQC.Enabled = false;//质检 
                    createFetch.Enabled = true;//自动生成领料单
                    break;
                case "2"://已完工
                    base.btnSave.Enabled = false;//保存 
                    base.btnDtalo.Enabled = false;//分配工时 
                    base.btnSatart.Enabled = false;//开工
                    base.btnStop.Enabled = false;//停工
                    base.btnComplete.Enabled = false;//完工   
                    createFetch.Enabled = true;//自动生成领料单
                    ucAttr.ReadOnly=true;
                    dgvAttachment.ReadOnly = true;
                    dgvFData.ReadOnly = true;
                    dgvMaterials.ReadOnly = true;
                    dgvOther.ReadOnly = true;
                    dgvproject.ReadOnly=true;
                    dgvUserData.ReadOnly = true;
                    cobSetMeal.Enabled = false;
                    ctmFdata.Enabled = false;
                    ctmParts.Enabled = false;
                    ctmProject.Enabled = false;
                    ctmUser.Enabled = false;
                    break;
                case "3"://已停工
                    //base.btnSave.Enabled = false;//保存
                    base.btnDtalo.Enabled = false;//分配工时 
                    base.btnStop.Enabled = false;//停工
                    base.btnComplete.Enabled = false;//完工
                    base.btnQC.Enabled = false;//质检 
                    createFetch.Enabled = true;//自动生成领料单
                    break;
                case "4"://已质检未通过
                    base.btnQC.Enabled = false;//质检 
                    createFetch.Enabled = true;//自动生成领料单
                    break;
                case "5"://已质检通过
                    base.btnSave.Enabled = false;//保存
                    base.btnDtalo.Enabled = false;//分配工时
                    base.btnSatart.Enabled = false;//开工
                    base.btnStop.Enabled = false;//停工
                    base.btnComplete.Enabled = false;//完工
                    base.btnQC.Enabled = false;//质检 
                    createFetch.Enabled = true;//自动生成领料单
                    ucAttr.ReadOnly=true;
                    dgvAttachment.ReadOnly = true;
                    dgvFData.ReadOnly = true;
                    dgvMaterials.ReadOnly = true;
                    dgvOther.ReadOnly = true;
                    dgvproject.ReadOnly=true;
                    dgvUserData.ReadOnly = true;
                    cobSetMeal.Enabled = false;
                    ctmFdata.Enabled = false;
                    ctmParts.Enabled = false;
                    ctmProject.Enabled = false;
                    ctmUser.Enabled = false;
                    break;
            }
        }
        #endregion

        #region 获取部门名称
        private string GetDepartmentName(string strDId)
        {
            return DBHelper.GetSingleValue("获得部门名称", "tb_organization", "org_name", "org_id='" + strDId + "'", "");
        }
        #endregion

        #region 获得人员名称
        private string GetUserSetName(string strPid)
        {
            return DBHelper.GetSingleValue("获得人员名称", "sys_user", "user_name", "user_id='" + strPid + "'", "");
        }
        #endregion

        #region 双击人员单元格获取分配工时人员信息
        private void dgvUserData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!IsChooserProject())
                {
                    return;
                }
                int intUrows = dgvUserData.CurrentRow.Index;//当前行索引
                frmPersonnelSelector person = new frmPersonnelSelector();
                DialogResult result = person.ShowDialog();
                if (result == DialogResult.OK)
                {
                    for (int i = 0; i <= intUrows; i++)
                    {
                        if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvUserData.Rows[i].Cells["dispatch_no"].Value)))
                        {
                            BindUserInfo(person.strUserId, i);
                            dgvUserData.Rows.Add(1);
                            break;
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

        #region 检测是否选择了分配工时项目
        /// <summary>
        /// 检测是否选择了分配工时项目
        /// </summary>
        private bool IsChooserProject()
        {
            bool isboolCheck = false;
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in dgvproject.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                string strPname = CommonCtrl.IsNullToString(dr.Cells["item_name"].Value);
                if (strPname.Length > 0)
                {
                    if (isCheck != null && (bool)isCheck)
                    {
                        listField.Add(dr.Cells["item_id"].Value.ToString());
                        strMaintainId = dr.Cells["item_id"].Value.ToString();
                        strMHourMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr.Cells["member_price"].Value)) ? dr.Cells["member_price"].Value.ToString() : "0";
                    }
                }
            }
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择需要分配工时项目！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                isboolCheck = true;
            }
            return isboolCheck;
        }
        #endregion

        #region 维修项目信息读取-双击维修项目单元格
        private void dgvproject_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (strProjectSattus != "3" && strProjectSattus != "5")
                {
                    int intProws = e.RowIndex;//当前行的索引
                    frmWorkHours frmHours = new frmWorkHours();
                    DialogResult result = frmHours.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        if (listProject.Contains(frmHours.strProjectNum) && frmHours.strWhoursType != "1")
                        {
                            MessageBoxEx.Show("此维修项目已存在，请选择其他维修项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        for (int i = 0; i <= intProws; i++)
                        {
                            if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["item_no"].Value)))
                            {
                                dgvproject.Rows[i].Cells["colCheck"].Value = true;
                                dgvproject.Rows[i].Cells["item_no"].Value = frmHours.strProjectNum;
                                dgvproject.Rows[i].Cells["item_type"].Value = GetDicName(frmHours.strRepairType);
                                dgvproject.Rows[i].Cells["item_name"].Value = frmHours.strProjectName;
                                dgvproject.Rows[i].Cells["remarks"].Value = frmHours.strRemark;
                                dgvproject.Rows[i].Cells["man_hour_type"].Value = frmHours.strWhoursType == "1" ? "工时" : "定额";
                                dgvproject.Rows[i].Cells["man_hour_norm_unitprice"].ReadOnly = CommonCtrl.IsNullToString(frmHours.strWhoursType) == "1" ? true : false;
                                dgvproject.Rows[i].Cells["item_id"].Value = "NewId";
                                string strPzk = !string.IsNullOrEmpty(strMemberPZk) ? strMemberPZk : "100";
                                #region 验证是否存在特殊项目
                                string strPdic = DBHelper.GetSingleValue("获取特殊项目折扣", "tb_CustomerSer_member_setInfo_projrct", "service_project_discount", "setInfo_id='" + strSetInfoid + "' and service_project_id='" + frmHours.strWhours_id + "' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", "");
                                if (!string.IsNullOrEmpty(strPdic))
                                {
                                    strPzk = strPdic;
                                }
                                #endregion
                                //工时单价
                                dgvproject.Rows[i].Cells["man_hour_norm_unitprice"].Value = frmHours.strQuotaPrice;
                                //会员折扣
                                dgvproject.Rows[i].Cells["member_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk));
                                //会员工时费
                                dgvproject.Rows[i].Cells["member_price"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strQuotaPrice) ? frmHours.strQuotaPrice : "0") * (Convert.ToDecimal(strPzk) / 100));
                                if (CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value) != "0")
                                {
                                    //折扣额
                                    dgvproject.Rows[i].Cells["member_sum_money"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strQuotaPrice) ? frmHours.strQuotaPrice : "0") - Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value) : "0"));
                                }
                                else
                                {
                                    dgvproject.Rows[i].Cells["member_sum_money"].Value = "0";
                                }
                                dgvproject.Rows[i].Cells["whours_id"].Value = frmHours.strWhours_id;
                                dgvproject.Rows[i].Cells["man_hour_quantity"].Value = frmHours.strWhoursNum;
                                dgvproject.Rows[i].Cells["sum_money_goods"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strWhoursNum) ? frmHours.strWhoursNum : "0") * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value) : "0"));
                                //新添加数据维修进度设置为未分配工时
                                dgvproject.Rows[i].Cells["repair_progress"].Value = DataSources.GetDescription(typeof(DataSources.EnumProjectDisStatus), Convert.ToInt64(DataSources.EnumProjectDisStatus.NotStartWork));//项目状态 CommonCtrl.IsNullToString(dpr["repair_progress"]);                       
                                dgvproject.Rows[i].Cells["three_warranty"].Value = "否";
                                dgvproject.Rows[i].Cells["data_source"].Value = frmHours.strData_source;
                                dgvproject.Rows[i].Cells["three_warranty"].ReadOnly = frmHours.strData_source == Convert.ToInt32(DataSources.EnumDataSources.SELFBUILD).ToString() ? true : false;
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
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        /// <summary>
        /// 工时数量发生改变时金额也发生改变
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
                    string strUnitprice = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value) : "0";

                    if (e.ColumnIndex == 6 || e.ColumnIndex == 7)
                    {
                        //会员工时费
                        dgvproject.Rows[e.RowIndex].Cells["member_price"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["member_discount"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["member_discount"].Value) : "0") * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value) : "0") / 100);
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
                            dgvproject.Rows[e.RowIndex].Cells["sum_money_goods"].Value = Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value) : "0");
                        }
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
                                    dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value = CommonCtrl.IsNullToString(dpr["quota_price"]);
                                    dgvproject.Rows[e.RowIndex].Cells["sum_money_goods"].Value = Convert.ToString(Convert.ToDecimal(strNum) * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["quota_price"])) ? CommonCtrl.IsNullToString(dpr["quota_price"]) : "0"));
                                }
                            }
                        }
                    }

                    dgvproject.Rows[e.RowIndex].Cells["man_hour_quantity"].Value = ControlsConfig.SetNewValue(dgvproject.Rows[e.RowIndex].Cells["man_hour_quantity"].Value, 1);
                    dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value = ControlsConfig.SetNewValue(dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value, 2);
                    dgvproject.Rows[e.RowIndex].Cells["sum_money_goods"].Value = ControlsConfig.SetNewValue(dgvproject.Rows[e.RowIndex].Cells["sum_money_goods"].Value, 2);
                    dgvproject.Rows[e.RowIndex].Cells["member_price"].Value = ControlsConfig.SetNewValue(dgvproject.Rows[e.RowIndex].Cells["member_price"].Value, 2);
                    dgvproject.Rows[e.RowIndex].Cells["member_sum_money"].Value = ControlsConfig.SetNewValue(dgvproject.Rows[e.RowIndex].Cells["member_sum_money"].Value, 2);
                }
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
                if (strProjectSattus != "3" && strProjectSattus != "5")
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
                                if (MessageBoxEx.Show("已添加此用料,是否继续添加?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                                {
                                    return;
                                }
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
                                    dgvMaterials.Rows[i].Cells["quantity"].Value = "1";
                                    dgvMaterials.Rows[i].Cells["unit_price"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["ref_out_price"])) ? CommonCtrl.IsNullToString(dpr["ref_out_price"]) : "0";
                                    string strPzk = !string.IsNullOrEmpty(strMemberLZk) ? strMemberLZk : "100";
                                    #region 验证是否存在特殊配件
                                    string strMdic = DBHelper.GetSingleValue("获取特殊配件折扣", "tb_CustomerSer_member_setInfo_parts", "parts_discount", "setInfo_id='" + strSetInfoid + "' and parts_id='" + strPId + "' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", "");
                                    if (!string.IsNullOrEmpty(strMdic))
                                    {
                                        strPzk = strMdic;
                                    }
                                    #endregion
                                    //会员折扣
                                    dgvMaterials.Rows[i].Cells["Mmember_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk));
                                    string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["quantity"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["quantity"].Value) : "0";
                                    string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["unit_price"].Value)) ? dgvMaterials.Rows[i].Cells["unit_price"].Value.ToString() : "0";
                                    //会员单价
                                    dgvMaterials.Rows[i].Cells["Mmember_price"].Value = Convert.ToString(Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["Mmember_discount"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["Mmember_discount"].Value) : "0") / 100);

                                    if (CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["Mmember_price"].Value) != "0")
                                    {
                                        //折扣额
                                        dgvMaterials.Rows[i].Cells["Mmember_sum_money"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["unit_price"].Value)) ? dgvMaterials.Rows[i].Cells["unit_price"].Value : "0") - Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["Mmember_price"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["Mmember_price"].Value) : "0"));
                                    }
                                    else
                                    {
                                        dgvMaterials.Rows[i].Cells["Mmember_sum_money"].Value = "0";
                                    }
                                    dgvMaterials.Rows[i].Cells["sum_money"].Value = Convert.ToString(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["Mmember_discount"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["Mmember_discount"].Value) : "0") / 100);
                                    dgvMaterials.Rows[i].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dpr["drawing_num"]);
                                    dgvMaterials.Rows[i].Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dpr["remark"]);
                                    dgvMaterials.Rows[i].Cells["vehicle_brand"].Value = CommonFuncCall.GetCarTypeForMa((CommonCtrl.IsNullToString(dpr["ser_parts_code"]))); ;
                                    dgvMaterials.Rows[i].Cells["parts_id"].Value = frmPart.PartsID;
                                    dgvMaterials.Rows[i].Cells["Mthree_warranty"].Value = "否";
                                    dgvMaterials.Rows[i].Cells["Mdata_source"].Value = CommonCtrl.IsNullToString(dpr["data_source"]);
                                    dgvMaterials.Rows[i].Cells["Mthree_warranty"].ReadOnly = CommonCtrl.IsNullToString(dpr["data_source"]) == Convert.ToInt32(DataSources.EnumDataSources.SELFBUILD).ToString() ? true : false;
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
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }

        bool isHasreturn = false;
        /// <summary>
        /// 工时数量发生改变时金额也发生改变
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
                isHasreturn = true;
                string strIsThree = CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mthree_warranty"].Value);
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["parts_name"].Value)))
                {
                    #region 修改数量和金额
                    if (e.ColumnIndex == 7 || e.ColumnIndex == 8)
                    {
                        //if (strIsThree == "否")
                        //{
                        ControlsConfig.SetCellsValue(dgvMaterials, e.RowIndex, "quantity,unit_price");
                        string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value)) ? dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value.ToString() : "0";
                        string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value)) ? dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value.ToString() : "0";
                        string strzk = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value)) ? dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value.ToString() : "0";
                        dgvMaterials.Rows[e.RowIndex].Cells["Mmember_price"].Value = Convert.ToString(Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(strzk.Length > 0 ? strzk : "0") / 100);
                        if (CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_price"].Value) != "0")
                        {
                            strzk = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value) : "0";
                            dgvMaterials.Rows[e.RowIndex].Cells["sum_money"].Value = Convert.ToString(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(strzk) / 100);
                        }
                        else
                        {
                            dgvMaterials.Rows[e.RowIndex].Cells["sum_money"].Value = Convert.ToString(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney));
                        }
                        //}
                    }
                    #endregion

                    #region 选择是三包时把数据恢复到原始数据
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
                                dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value = CommonCtrl.IsNullToString(dpr["ref_out_price"]);
                                string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value) : "0";
                                string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value) : "0";
                                dgvMaterials.Rows[e.RowIndex].Cells["sum_money"].Value = Convert.ToString(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney));
                            }
                        }
                    }
                    #endregion
                    dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value = ControlsConfig.SetNewValue(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value, 1);
                    dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value = ControlsConfig.SetNewValue(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value, 2);
                    dgvMaterials.Rows[e.RowIndex].Cells["sum_money"].Value = ControlsConfig.SetNewValue(dgvMaterials.Rows[e.RowIndex].Cells["sum_money"].Value, 2);
                    dgvMaterials.Rows[e.RowIndex].Cells["Mmember_price"].Value = ControlsConfig.SetNewValue(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_price"].Value, 2);
                    dgvMaterials.Rows[e.RowIndex].Cells["Mmember_sum_money"].Value = ControlsConfig.SetNewValue(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_sum_money"].Value, 2);
                    #region 修改库存时判断库存数量是否足够
                    if (e.ColumnIndex == 7 && isHasreturn)
                    {
                        string strPCode = CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["parts_code"].Value);
                        string strPId = CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["parts_id"].Value);
                        if (!string.IsNullOrEmpty(strPId))
                        {
                            //配件数量
                            decimal intPNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value)) ? Convert.ToDecimal(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value)) : 0;
                            //配件账面库存数量   
                            decimal intPCount = Convert.ToDecimal(CommonFuncCall.CheckPartStockCount(strPId, DataSources.EnumStatisticType.PaperCount));
                            if (intPNum > intPCount)
                            {
                                if (MessageBoxEx.Show("您选择的配件【" + strPCode + "】库存不足，是否继续添加?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                                {
                                    dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value = "0";
                                    return;
                                }
                                isHasreturn = false;
                            }
                        }
                    }
                    #endregion
                }
                decPartsCount = 0;
                foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
                {
                    string strPCode = CommonCtrl.IsNullToString(dgvr.Cells["parts_code"].Value);
                    if (!string.IsNullOrEmpty(strPCode))
                    {
                        decPartsCount += Convert.ToDecimal(CommonCtrl.IsNullToString(dgvr.Cells["quantity"].Value));
                    }
                }

            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 根据车型编号获取车型名称
        private string GetVehicleModels(string strMId)
        {
            return DBHelper.GetSingleValue("获取车型名称", "tb_vehicle_models", "vm_name", "vm_id='" + strMId + "'", "");
        }
        #endregion

        #region 其他收费项目数据设置
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
                    }
                    dgvOther.Rows[e.RowIndex].Cells["Osum_money"].Value = ControlsConfig.SetNewValue(dgvOther.Rows[e.RowIndex].Cells["Osum_money"].Value, 2);
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 根据分配工时获取分配金额值
        private void dgvUserData_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                string strUName = CommonCtrl.IsNullToString(dgvUserData.Rows[e.RowIndex].Cells["dispatch_no"].Value);
                if (!string.IsNullOrEmpty(strUName))
                {
                    //if (e.ColumnIndex == 5)
                    //{
                    //    //分配工时
                    //    string strHourNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvUserData.Rows[e.RowIndex].Cells["man_hour"].Value)) ? dgvUserData.Rows[e.RowIndex].Cells["man_hour"].Value.ToString() : "0";
                    //    //分配金额
                    //    string strUSumMoney = (Convert.ToDecimal(!string.IsNullOrEmpty(strMHourMoney) ? strMHourMoney : "0") * Convert.ToDecimal(strHourNum)).ToString();
                    //    dgvUserData.Rows[e.RowIndex].Cells["Usum_money"].Value = strUSumMoney;
                    //}
                    dgvUserData.Rows[e.RowIndex].Cells["man_hour"].Value = ControlsConfig.SetNewValue(dgvUserData.Rows[e.RowIndex].Cells["man_hour"].Value, 2);
                    dgvUserData.Rows[e.RowIndex].Cells["Usum_money"].Value = ControlsConfig.SetNewValue(dgvUserData.Rows[e.RowIndex].Cells["Usum_money"].Value, 2);
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 右键新增维修用料
        private void addParts_Click(object sender, EventArgs e)
        {
            try
            {
                if (strProjectSattus != "3" && strProjectSattus != "5")
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
                                if (MessageBoxEx.Show("已添加此用料,是否继续添加?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                                {
                                    return;
                                }
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
                                    dgvMaterials.Rows[i].Cells["quantity"].Value = "1";
                                    dgvMaterials.Rows[i].Cells["unit_price"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["ref_out_price"])) ? CommonCtrl.IsNullToString(dpr["ref_out_price"]) : "0";
                                    string strPzk = !string.IsNullOrEmpty(strMemberLZk) ? strMemberLZk : "100";
                                    #region 验证是否存在特殊配件
                                    string strMdic = DBHelper.GetSingleValue("获取特殊配件折扣", "tb_CustomerSer_member_setInfo_parts", "parts_discount", "setInfo_id='" + strSetInfoid + "' and parts_id='" + strPId + "' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", "");
                                    if (!string.IsNullOrEmpty(strMdic))
                                    {
                                        strPzk = strMdic;
                                    }
                                    #endregion
                                    //会员折扣
                                    dgvMaterials.Rows[i].Cells["Mmember_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk));
                                    string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["quantity"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["quantity"].Value) : "0";
                                    string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["unit_price"].Value)) ? dgvMaterials.Rows[i].Cells["unit_price"].Value.ToString() : "0";
                                    //会员单价
                                    dgvMaterials.Rows[i].Cells["Mmember_price"].Value = Convert.ToString(Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(dgvMaterials.Rows[i].Cells["Mmember_discount"].Value) / 100);
                                    if (CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["Mmember_price"].Value) != "0")
                                    {
                                        //折扣额
                                        dgvMaterials.Rows[i].Cells["Mmember_sum_money"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["unit_price"].Value)) ? dgvMaterials.Rows[i].Cells["unit_price"].Value : "0") - Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["Mmember_price"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["Mmember_price"].Value) : "0"));
                                    }
                                    else
                                    {
                                        dgvMaterials.Rows[i].Cells["Mmember_sum_money"].Value = "0";
                                    }
                                    dgvMaterials.Rows[i].Cells["sum_money"].Value = Convert.ToString(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["Mmember_discount"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["Mmember_discount"].Value) : "0") / 100);
                                    dgvMaterials.Rows[i].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dpr["drawing_num"]);
                                    dgvMaterials.Rows[i].Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dpr["remark"]);
                                    dgvMaterials.Rows[i].Cells["vehicle_brand"].Value = CommonFuncCall.GetCarTypeForMa((CommonCtrl.IsNullToString(dpr["ser_parts_code"]))); ;
                                    dgvMaterials.Rows[i].Cells["parts_id"].Value = frmPart.PartsID;
                                    dgvMaterials.Rows[i].Cells["Mthree_warranty"].Value = "否";
                                    dgvMaterials.Rows[i].Cells["Mdata_source"].Value = CommonCtrl.IsNullToString(dpr["data_source"]);
                                    dgvMaterials.Rows[i].Cells["Mthree_warranty"].ReadOnly = CommonCtrl.IsNullToString(dpr["data_source"]) == Convert.ToInt32(DataSources.EnumDataSources.SELFBUILD).ToString() ? true : false;
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

        #region 右键添加维修项目
        private void addProject_Click(object sender, EventArgs e)
        {
            try
            {
                if (strProjectSattus != "3" && strProjectSattus != "5")
                {
                    int intProws = dgvproject.CurrentRow.Index;//当前行的索引
                    frmWorkHours frmHours = new frmWorkHours();
                    DialogResult result = frmHours.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        if (listProject.Contains(frmHours.strProjectNum) && frmHours.strWhoursType != "1")
                        {
                            MessageBoxEx.Show("此维修项目已存在，请选择其他维修项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        for (int i = 0; i <= intProws; i++)
                        {
                            if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["item_no"].Value)))
                            {
                                dgvproject.Rows[i].Cells["colCheck"].Value = true;
                                dgvproject.Rows[i].Cells["item_no"].Value = frmHours.strProjectNum;
                                dgvproject.Rows[i].Cells["item_type"].Value = GetDicName(frmHours.strRepairType);
                                dgvproject.Rows[i].Cells["item_name"].Value = frmHours.strProjectName;
                                dgvproject.Rows[i].Cells["remarks"].Value = frmHours.strRemark;
                                dgvproject.Rows[i].Cells["man_hour_type"].Value = frmHours.strWhoursType == "1" ? "工时" : "定额";
                                dgvproject.Rows[i].Cells["man_hour_norm_unitprice"].ReadOnly = CommonCtrl.IsNullToString(frmHours.strWhoursType) == "1" ? true : false;
                                dgvproject.Rows[i].Cells["item_id"].Value = "NewId";
                                string strPzk = !string.IsNullOrEmpty(strMemberPZk) ? strMemberPZk : "100";
                                #region 验证是否存在特殊项目
                                string strPdic = DBHelper.GetSingleValue("获取特殊项目折扣", "tb_CustomerSer_member_setInfo_projrct", "service_project_discount", "setInfo_id='" + strSetInfoid + "' and service_project_id='" + frmHours.strWhours_id + "' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", "");
                                if (!string.IsNullOrEmpty(strPdic))
                                {
                                    strPzk = strPdic;
                                }
                                #endregion
                                //工时单价
                                dgvproject.Rows[i].Cells["man_hour_norm_unitprice"].Value = frmHours.strQuotaPrice;
                                //会员折扣
                                dgvproject.Rows[i].Cells["member_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk));
                                //会员工时费
                                dgvproject.Rows[i].Cells["member_price"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strQuotaPrice) ? frmHours.strQuotaPrice : "0") * (Convert.ToDecimal(strPzk) / 100));
                                if (dgvproject.Rows[i].Cells["member_price"].Value.ToString() != "0")
                                {
                                    //折扣额
                                    dgvproject.Rows[i].Cells["member_sum_money"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strQuotaPrice) ? frmHours.strQuotaPrice : "0") - Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value) : "0"));
                                }
                                else
                                {
                                    dgvproject.Rows[i].Cells["member_sum_money"].Value = "0";
                                }
                                dgvproject.Rows[i].Cells["whours_id"].Value = frmHours.strWhours_id;
                                dgvproject.Rows[i].Cells["man_hour_quantity"].Value = frmHours.strWhoursNum;
                                dgvproject.Rows[i].Cells["sum_money_goods"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strWhoursNum) ? frmHours.strWhoursNum : "0") * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value) : "0"));                                
                               //新添加数据维修进度设置为未分配工时
                                dgvproject.Rows[i].Cells["repair_progress"].Value = DataSources.GetDescription(typeof(DataSources.EnumProjectDisStatus), Convert.ToInt64(DataSources.EnumProjectDisStatus.NotStartWork));//项目状态 CommonCtrl.IsNullToString(dpr["repair_progress"]);                       
                                dgvproject.Rows[i].Cells["three_warranty"].Value = "否";
                                dgvproject.Rows[i].Cells["data_source"].Value = frmHours.strData_source;
                                dgvproject.Rows[i].Cells["three_warranty"].ReadOnly = frmHours.strData_source == Convert.ToInt32(DataSources.EnumDataSources.SELFBUILD).ToString() ? true : false;
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
                        if (!string.IsNullOrEmpty(strItemId))
                        {
                            #region 删除维修项目
                            List<string> listField = new List<string>();
                            Dictionary<string, string> comField = new Dictionary<string, string>();
                            listField.Add(strItemId);
                            comField.Add("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.DELETED).ToString());
                            DBHelper.BatchUpdateDataByIn("删除维修项目信息", "tb_maintain_item", comField, "item_id", listField.ToArray());
                            #endregion
                            #region 删除救援人员
                            List<SQLObj> listUserSql = new List<SQLObj>();
                            SQLObj objUser = new SQLObj();
                            objUser.cmdType = CommandType.Text;
                            Dictionary<string, ParamObj> dicUserParam = new Dictionary<string, ParamObj>();
                            dicUserParam.Add("item_id", new ParamObj("item_id", strItemId, SysDbType.VarChar, 40));
                            dicUserParam.Add("maintain_id", new ParamObj("maintain_id", strReceiveId, SysDbType.VarChar, 40));
                            objUser.sqlString = @"delete from tb_maintain_dispatch_worker where  maintain_id=@maintain_id and item_id=@item_id";
                            objUser.Param = dicUserParam;
                            listUserSql.Add(objUser);
                            DBHelper.BatchExeSQLMultiByTrans("删除分配工时人员", listUserSql);
                            #endregion
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
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 维修项目选择行
        private void dgvproject_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                {
                    return;
                }
                if (e.ColumnIndex == colCheck.Index)
                {
                    return;
                }
                //选择当前行
                dgvproject.Rows[e.RowIndex].Cells["colCheck"].Value = true;
                string strProjectID = CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["item_id"].Value);
                #region 分配工时人员信息
                string strdispatchNo = string.Empty;
                //其他项目收费数据             
                DataTable dut = DBHelper.GetTable("分配工时人员信息", "tb_maintain_dispatch_worker a left join tb_maintain_item b on a.item_id=b.item_id ", "*", string.Format("a.maintain_id='{0}' and a.item_id='" + strProjectID + "'", strReceiveId), "", "");
                if (dut.Rows.Count > 0)
                {
                    //清空已选择框
                    //foreach (DataGridViewRow dgvr in dgvproject.Rows)
                    //{
                    //    object check = dgvr.Cells[colCheck.Name].EditedFormattedValue;
                    //    if (check != null && (bool)check)
                    //    {
                    //        dgvr.Cells[colCheck.Name].Value = false;
                    //    }
                    //}
                    if (dut.Rows.Count > dgvUserData.Rows.Count)
                    {
                        dgvUserData.Rows.Add(dut.Rows.Count - dgvUserData.Rows.Count + 2);
                    }
                    else if ((dgvUserData.Rows.Count - dut.Rows.Count) == 1)
                    {
                        dgvUserData.Rows.Add(1);
                    }
                    for (int i = 0; i < dgvUserData.Rows.Count; i++)
                    {
                        strdispatchNo = CommonCtrl.IsNullToString(dgvUserData.Rows[i].Cells["dispatch_no"].Value);
                        listUser.Remove(strdispatchNo);
                        dgvUserData.Rows[i].Cells["UcolCheck"].Value = false;
                        dgvUserData.Rows[i].Cells["dispatch_id"].Value = "";
                        dgvUserData.Rows[i].Cells["dispatch_no"].Value = "";
                        dgvUserData.Rows[i].Cells["dispatch_name"].Value = "";
                        dgvUserData.Rows[i].Cells["org_name"].Value = "";
                        dgvUserData.Rows[i].Cells["man_hour"].Value = "";
                        dgvUserData.Rows[i].Cells["Usum_money"].Value = "";
                        dgvUserData.Rows[i].Cells["Uitem_name"].Value = "";
                        dgvUserData.Rows[i].Cells["create_time"].Value = "";

                    }
                    for (int i = 0; i < dut.Rows.Count; i++)
                    {
                        DataRow dur = dut.Rows[i];
                        dgvUserData.Rows[i].Cells["UcolCheck"].Value = true;
                        dgvUserData.Rows[i].Cells["dispatch_id"].Value = CommonCtrl.IsNullToString(dur["dispatch_id"]);
                        dgvUserData.Rows[i].Cells["dispatch_no"].Value = CommonCtrl.IsNullToString(dur["dispatch_no"]);
                        dgvUserData.Rows[i].Cells["dispatch_name"].Value = CommonCtrl.IsNullToString(dur["dispatch_name"]);
                        dgvUserData.Rows[i].Cells["org_name"].Value = CommonCtrl.IsNullToString(dur["org_name"]);
                        dgvUserData.Rows[i].Cells["team_name"].Value = CommonCtrl.IsNullToString(dur["team_name"]);
                        dgvUserData.Rows[i].Cells["man_hour"].Value = CommonCtrl.IsNullToString(dur["man_hour"]);
                        dgvUserData.Rows[i].Cells["Usum_money"].Value = CommonCtrl.IsNullToString(dur["sum_money"]);
                        dgvUserData.Rows[i].Cells["Uitem_name"].Value = CommonCtrl.IsNullToString(dur["item_name"]);
                        dgvUserData.Rows[i].Cells["create_time"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dur["create_time"])) ? Common.UtcLongToLocalDateTime(dur["create_time"]) : "";
                        strdispatchNo = CommonCtrl.IsNullToString(dgvUserData.Rows[i].Cells["dispatch_no"].Value);
                        if (!listUser.Contains(strdispatchNo))
                        {
                            listUser.Add(strdispatchNo);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < dgvUserData.Rows.Count; i++)
                    {
                        strdispatchNo = CommonCtrl.IsNullToString(dgvUserData.Rows[i].Cells["dispatch_no"].Value);
                        listUser.Remove(strdispatchNo);
                        dgvUserData.Rows[i].Cells["UcolCheck"].Value = false;
                        dgvUserData.Rows[i].Cells["dispatch_id"].Value = "";
                        dgvUserData.Rows[i].Cells["dispatch_no"].Value = "";
                        dgvUserData.Rows[i].Cells["dispatch_name"].Value = "";
                        dgvUserData.Rows[i].Cells["org_name"].Value = "";
                        dgvUserData.Rows[i].Cells["man_hour"].Value = "";
                        dgvUserData.Rows[i].Cells["Usum_money"].Value = "";
                        dgvUserData.Rows[i].Cells["Uitem_name"].Value = "";
                        dgvUserData.Rows[i].Cells["create_time"].Value = "";

                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 右键添加分配工时人员
        private void addUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (strProjectSattus != "3" && strProjectSattus != "5")
                {
                    if (!IsChooserProject())
                    {
                        return;
                    }
                    int intUrows = dgvUserData.CurrentRow.Index;//当前行索引
                    frmPersonnelSelector person = new frmPersonnelSelector();
                    DialogResult result = person.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        for (int i = 0; i <= intUrows; i++)
                        {
                            if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvUserData.Rows[i].Cells["dispatch_no"].Value)))
                            {
                                BindUserInfo(person.strUserId, i);
                                dgvUserData.Rows.Add(1);
                                break;
                            }
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

        #region 右键删除分配工时人员
        private void deleteUser_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvUserData.RowCount; i++)
                {
                    DataGridViewRow dgvr = dgvUserData.Rows[i];
                    object isCheck = dgvr.Cells["UcolCheck"].Value;
                    //将选中的删除
                    if (isCheck != null && (bool)isCheck)
                    {
                        string strItemId = CommonCtrl.IsNullToString(dgvUserData.Rows[i].Cells["dispatch_id"].Value);
                        string strdispatchNo = CommonCtrl.IsNullToString(dgvUserData.Rows[i].Cells["dispatch_no"].Value);
                        if (!string.IsNullOrEmpty(strItemId))
                        {
                            List<SQLObj> listSql = new List<SQLObj>();
                            SQLObj obj = new SQLObj();
                            obj.cmdType = CommandType.Text;
                            Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                            dicParam.Add("dispatch_id", new ParamObj("dispatch_id", strItemId, SysDbType.VarChar, 40));
                            obj.sqlString = @"delete from tb_maintain_dispatch_worker where dispatch_id=@dispatch_id";
                            obj.Param = dicParam;
                            listSql.Add(obj);
                            DBHelper.BatchExeSQLMultiByTrans("删除分配工时人员", listSql);
                            dgvUserData.Rows.RemoveAt(i--);
                        }
                        else
                        {
                            dgvUserData.Rows.RemoveAt(i--);
                        }
                        listUser.Remove(strdispatchNo);
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
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
            return true;
        }
        #endregion

        #region 自动生成领料单
        private void createFetch_Click(object sender, EventArgs e)
        {
            #region 保存现有的用料(配件)信息
            List<SQLObj> listSql = new List<SQLObj>();
            SaveMaterialsData(listSql, strReceiveId);
            bool isSecess = DBHelper.BatchExeSQLMultiByTrans(opName, listSql);
            #endregion
            UCFetchMaterialAddOrEdit MaterialAdd = new UCFetchMaterialAddOrEdit();
            UCFetchMaterialManager ucManager = new UCFetchMaterialManager();
            MaterialAdd.wStatus = WindowStatus.Add;
            MaterialAdd.uc = ucManager;
            MaterialAdd.strBefore_orderId = strReceiveId;
            MaterialAdd.CreateFetch();
            base.addUserControl(MaterialAdd, "领料单-新增", "MaterialEdit" + MaterialAdd.strId, this.Tag.ToString(), this.Name);
            isAutoClose = true;

        }
        #endregion

        #region 刷新领料情况
        private void tsmRefresh_Click(object sender, EventArgs e)
        {
            string strFildes = @"a.material_num,a.fetch_time,b.parts_code,b.parts_name,b.unit,b.whether_imported, 
                                    b.quantity,b.drawn_no,b.vehicle_model,(select user_name from v_user where user_id=a.fetch_opid) as fetch_opid,b.remarks";
            string strTable = @"tb_maintain_fetch_material a 
                                    left join tb_maintain_fetch_material_detai b on a. fetch_id=b.fetch_id";
            //string strWhere = " a.maintain_id='" + labMaintain_noS.Text.Trim() + "' and (a.info_status='" + Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString() + "' or a.info_status='" + Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString() + "')";
            string strWhere = " a.maintain_id='" + labMaintain_noS.Text.Trim() + "' and b.enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'";
            DataTable dft = DBHelper.GetTable("获取领料单情况", strTable, strFildes, strWhere, "", "order by a.fetch_time desc ");
            dgvFData.DataSource = dft;
            decFPartsCount = 0;
            foreach (DataGridViewRow dgvr in dgvFData.Rows)
            {
                decFPartsCount += Convert.ToDecimal(CommonCtrl.IsNullToString(dgvr.Cells["Fquantity"].Value));
            }           
        }
        #endregion

        #region 限制手机号码只能数据数字
        private void txtDriverPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationRegex.KeyPressInt(sender, e);
        }
        #endregion

        #region 右键编辑用料（配件）
        private void editParts_Click(object sender, EventArgs e)
        {
            try
            {
                if (strProjectSattus != "3" && strProjectSattus != "5")
                {
                    int intMrows = dgvMaterials.CurrentRow.Index;
                    if (intMrows >= 0)
                    {
                        string strPcode = CommonCtrl.IsNullToString(dgvMaterials.Rows[intMrows].Cells["parts_code"].Value);
                        if (!string.IsNullOrEmpty(strPcode))
                        {
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
                                        if (MessageBoxEx.Show("已添加此用料,是否继续添加?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                                        {
                                            return;
                                        }
                                    }
                                    dgvMaterials.Rows[intMrows].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dpr["ser_parts_code"]);
                                    dgvMaterials.Rows[intMrows].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dpr["parts_name"]);
                                    dgvMaterials.Rows[intMrows].Cells["norms"].Value = CommonCtrl.IsNullToString(dpr["model"]);
                                    dgvMaterials.Rows[intMrows].Cells["unit"].Value = CommonCtrl.IsNullToString(dpr["unit"]);
                                    dgvMaterials.Rows[intMrows].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dpr["is_import"]) == "1" ? "是" : "否";
                                    dgvMaterials.Rows[intMrows].Cells["quantity"].Value = "1";
                                    dgvMaterials.Rows[intMrows].Cells["unit_price"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["ref_out_price"])) ? CommonCtrl.IsNullToString(dpr["ref_out_price"]) : "0";
                                    string strPzk = !string.IsNullOrEmpty(strMemberLZk) ? strMemberLZk : "100";
                                    #region 验证是否存在特殊配件
                                    string strMdic = DBHelper.GetSingleValue("获取特殊配件折扣", "tb_CustomerSer_member_setInfo_parts", "parts_discount", "setInfo_id='" + strSetInfoid + "' and parts_id='" + strPId + "' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", "");
                                    if (!string.IsNullOrEmpty(strMdic))
                                    {
                                        strPzk = strMdic;
                                    }
                                    #endregion
                                    //会员折扣
                                    dgvMaterials.Rows[intMrows].Cells["Mmember_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk));
                                    string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[intMrows].Cells["quantity"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[intMrows].Cells["quantity"].Value) : "0";
                                    string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[intMrows].Cells["unit_price"].Value)) ? dgvMaterials.Rows[intMrows].Cells["unit_price"].Value.ToString() : "0";
                                    //会员单价
                                    dgvMaterials.Rows[intMrows].Cells["Mmember_price"].Value = Convert.ToString(Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(dgvMaterials.Rows[intMrows].Cells["Mmember_discount"].Value) / 100);
                                    if (CommonCtrl.IsNullToString(dgvMaterials.Rows[intMrows].Cells["Mmember_price"].Value) != "0")
                                    {
                                        //折扣额
                                        dgvMaterials.Rows[intMrows].Cells["Mmember_sum_money"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[intMrows].Cells["unit_price"].Value)) ? dgvMaterials.Rows[intMrows].Cells["unit_price"].Value : "0") - Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[intMrows].Cells["Mmember_price"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[intMrows].Cells["Mmember_price"].Value) : "0"));
                                    }
                                    else
                                    {
                                        dgvMaterials.Rows[intMrows].Cells["Mmember_sum_money"].Value = "0";
                                    }
                                    dgvMaterials.Rows[intMrows].Cells["sum_money"].Value = Convert.ToString(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[intMrows].Cells["Mmember_discount"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[intMrows].Cells["Mmember_discount"].Value) : "0") / 100);
                                    dgvMaterials.Rows[intMrows].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dpr["drawing_num"]);
                                    dgvMaterials.Rows[intMrows].Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dpr["remark"]);
                                    dgvMaterials.Rows[intMrows].Cells["vehicle_brand"].Value = CommonFuncCall.GetCarTypeForMa((CommonCtrl.IsNullToString(dpr["ser_parts_code"]))); ;
                                    dgvMaterials.Rows[intMrows].Cells["parts_id"].Value = frmPart.PartsID;
                                    dgvMaterials.Rows[intMrows].Cells["Mthree_warranty"].Value = "否";
                                    dgvMaterials.Rows[intMrows].Cells["Mdata_source"].Value = CommonCtrl.IsNullToString(dpr["data_source"]);
                                    dgvMaterials.Rows[intMrows].Cells["Mthree_warranty"].ReadOnly = CommonCtrl.IsNullToString(dpr["data_source"]) == Convert.ToInt32(DataSources.EnumDataSources.SELFBUILD).ToString() ? true : false;
                                    listMater.Remove(strPcode);
                                    foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
                                    {
                                        string strPCode = CommonCtrl.IsNullToString(dgvr.Cells["parts_code"].Value);
                                        if (!string.IsNullOrEmpty(strPCode) && !listMater.Contains(strPCode))
                                        {
                                            listMater.Add(strPCode);
                                        }
                                    }
                                }
                            }
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

        #region 右键编辑维修项目
        private void editProject_Click(object sender, EventArgs e)
        {
             try
            {
                if (strProjectSattus != "3" && strProjectSattus != "5")
                {
                    int intCurrentIndex = dgvproject.CurrentRow.Index;
                    if (intCurrentIndex >= 0)
                    {
                        string strItemNo = CommonCtrl.IsNullToString(dgvproject.Rows[intCurrentIndex].Cells["item_no"].Value);
                        if (!string.IsNullOrEmpty(strItemNo))
                        {
                            frmWorkHours frmHours = new frmWorkHours();
                            DialogResult result = frmHours.ShowDialog();
                            if (result == DialogResult.OK)
                            {
                                if (listProject.Contains(frmHours.strProjectNum) && frmHours.strWhoursType != "1")
                                {
                                    MessageBoxEx.Show("此维修项目已存在，请选择其他维修项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                dgvproject.Rows[intCurrentIndex].Cells["colCheck"].Value = true;
                                dgvproject.Rows[intCurrentIndex].Cells["item_no"].Value = frmHours.strProjectNum;
                                dgvproject.Rows[intCurrentIndex].Cells["item_type"].Value = GetDicName(frmHours.strRepairType);
                                dgvproject.Rows[intCurrentIndex].Cells["item_name"].Value = frmHours.strProjectName;
                                dgvproject.Rows[intCurrentIndex].Cells["remarks"].Value = frmHours.strRemark;
                                dgvproject.Rows[intCurrentIndex].Cells["man_hour_type"].Value = frmHours.strWhoursType == "1" ? "工时" : "定额";
                                dgvproject.Rows[intCurrentIndex].Cells["man_hour_norm_unitprice"].ReadOnly = CommonCtrl.IsNullToString(frmHours.strWhoursType) == "1" ? true : false;
                                dgvproject.Rows[intCurrentIndex].Cells["item_id"].Value = "NewId";
                                string strPzk = !string.IsNullOrEmpty(strMemberPZk) ? strMemberPZk : "100";
                                #region 验证是否存在特殊项目
                                string strPdic = DBHelper.GetSingleValue("获取特殊项目折扣", "tb_CustomerSer_member_setInfo_projrct", "service_project_discount", "setInfo_id='" + strSetInfoid + "' and service_project_id='" + frmHours.strWhours_id + "' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", "");
                                if (!string.IsNullOrEmpty(strPdic))
                                {
                                    strPzk = strPdic;
                                }
                                #endregion
                                //工时单价
                                dgvproject.Rows[intCurrentIndex].Cells["man_hour_norm_unitprice"].Value = frmHours.strQuotaPrice;
                                //会员折扣
                                dgvproject.Rows[intCurrentIndex].Cells["member_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk));
                                //会员工时费
                                dgvproject.Rows[intCurrentIndex].Cells["member_price"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strQuotaPrice) ? frmHours.strQuotaPrice : "0") * (Convert.ToDecimal(strPzk) / 100));
                                if (dgvproject.Rows[intCurrentIndex].Cells["member_price"].Value.ToString() != "0")
                                {
                                    //折扣额
                                    dgvproject.Rows[intCurrentIndex].Cells["member_sum_money"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strQuotaPrice) ? frmHours.strQuotaPrice : "0") - Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[intCurrentIndex].Cells["member_price"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[intCurrentIndex].Cells["member_price"].Value) : "0"));
                                }
                                else
                                {
                                    dgvproject.Rows[intCurrentIndex].Cells["member_sum_money"].Value = "0";
                                }
                                dgvproject.Rows[intCurrentIndex].Cells["whours_id"].Value = frmHours.strWhours_id;
                                dgvproject.Rows[intCurrentIndex].Cells["man_hour_quantity"].Value = frmHours.strWhoursNum;
                                dgvproject.Rows[intCurrentIndex].Cells["sum_money_goods"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strWhoursNum) ? frmHours.strWhoursNum : "0") * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[intCurrentIndex].Cells["member_price"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[intCurrentIndex].Cells["member_price"].Value) : "0"));
                                //新添加数据维修进度设置为未分配工时
                                dgvproject.Rows[intCurrentIndex].Cells["repair_progress"].Value = DataSources.GetDescription(typeof(DataSources.EnumProjectDisStatus), Convert.ToInt64(DataSources.EnumProjectDisStatus.NotStartWork));//项目状态 CommonCtrl.IsNullToString(dpr["repair_progress"]);                       
                                dgvproject.Rows[intCurrentIndex].Cells["three_warranty"].Value = "否";
                                dgvproject.Rows[intCurrentIndex].Cells["data_source"].Value = frmHours.strData_source;
                                dgvproject.Rows[intCurrentIndex].Cells["three_warranty"].ReadOnly = frmHours.strData_source == Convert.ToInt32(DataSources.EnumDataSources.SELFBUILD).ToString() ? true : false;
                                listProject.Remove(strItemNo);
                                foreach (DataGridViewRow dgvr in dgvproject.Rows)
                                {
                                    string strPCode = CommonCtrl.IsNullToString(dgvr.Cells["item_no"].Value);
                                    if (!string.IsNullOrEmpty(strPCode) && !listProject.Contains(strPCode))
                                    {
                                        listProject.Add(strPCode);
                                    }
                                }
                            }
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

        #region 右键编辑派工人员
        private void editUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (strProjectSattus != "3" && strProjectSattus != "5")
                {
                    if (!IsChooserProject())
                    {
                        return;
                    }
                    int intUserIndex = dgvUserData.CurrentRow.Index;//当前行索引
                    if (intUserIndex >= 0)
                    {
                        string strRescuerNo = CommonCtrl.IsNullToString(dgvUserData.Rows[intUserIndex].Cells["dispatch_no"].Value);
                        string strItemId = CommonCtrl.IsNullToString(dgvUserData.Rows[intUserIndex].Cells["dispatch_id"].Value);
                        if (!string.IsNullOrEmpty(strRescuerNo))
                        {
                            frmPersonnelSelector person = new frmPersonnelSelector();
                            DialogResult result = person.ShowDialog();
                            if (result == DialogResult.OK)
                            {
                                if (!string.IsNullOrEmpty(strItemId))
                                {
                                    List<SQLObj> listSql = new List<SQLObj>();
                                    SQLObj obj = new SQLObj();
                                    obj.cmdType = CommandType.Text;
                                    Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                                    dicParam.Add("dispatch_id", new ParamObj("dispatch_id", strItemId, SysDbType.VarChar, 40));
                                    obj.sqlString = @"delete from tb_maintain_dispatch_worker where dispatch_id=@dispatch_id";
                                    obj.Param = dicParam;
                                    listSql.Add(obj);
                                    DBHelper.BatchExeSQLMultiByTrans("删除分配工时人员", listSql);
                                }
                                listUser.Remove(strRescuerNo);
                                BindUserInfo(person.strUserId, intUserIndex);                               
                            }
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

        #region 获取客户会员信息
        /// <summary>
        /// 获取客户会员信息
        /// </summary>
        /// <param name="strCid">客户Id</param>
        private void GetMemberInfo(string strCid)
        {
            MemberInfo mInfo = new MemberInfo();
            CommonFuncCall.GetMemberDiscount(mInfo, strCid);
            strMemberPZk = mInfo.strMemberPZk;
            strMemberLZk = mInfo.strMemberLZk;
            strSetInfoid = mInfo.strSetInfoId;
        }
        #endregion
    }
}
