using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using SYSModel;
using Utility.Common;
using ServiceStationClient.ComponentUI;
using Model;
using System.Threading;
using Utility.CommonForm;

namespace HXCPcClient.UCForm.RepairBusiness.OldPartsManagement.YTOldPartsPalautus
{
    /// <summary>
    /// 旧件管理-宇通旧件返厂单新增编辑
    /// Author：JC
    /// AddTime：2014.11.03
    /// </summary>
    public partial class UCOldPartsPalautusAddOrEdit : UCBase
    {
        #region 属性设置
        /// <summary>
        /// 父窗体
        /// </summary>
        public UCOldPartsPalautusManager uc;
        /// <summary>
        /// 窗体状态
        /// </summary>
        public WindowStatus wStatus;
        /// <summary>
        /// 宇通旧件返厂单ID
        /// </summary>
        public string strId = string.Empty;
        /// <summary>
        /// 操作名称
        /// </summary>
        string opName = string.Empty;
        /// <summary>
        /// 返厂详细信息总条数
        /// </summary>
        int intDetailCount = 0;
        /// <summary>
        /// 宇通单据状态
        /// </summary>
        string info_status_yt = string.Empty;
        /// <summary>
        /// 单据状态
        /// </summary>
        string strStatus = string.Empty;

        string strWhere = string.Empty;
        #endregion

        #region 初始化窗体
        public UCOldPartsPalautusAddOrEdit()
        {
            InitializeComponent();
            CommonFuncCall.BindDepartment(cboOrgId, GlobalStaticObj.CurrUserCom_Id, "请选择");
            SetDgvAnchor();
            GetRepairNo();
            BindModeYT();
            base.SyncEvent += new ClickHandler(UCOldPartsPalautusAddOrEdit_SyncEvent);
            base.SaveEvent += new ClickHandler(UCOldPartsPalautusAddOrEdit_SaveEvent);
            base.SubmitEvent += new ClickHandler(UCOldPartsPalautusAddOrEdit_SubmitEvent);
            base.CancelEvent += new ClickHandler(UCOldPartsPalautusAddOrEdit_CancelEvent);
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
            base.btnDelete.Visible = false;
            base.btnStatus.Visible = false;
            base.btnVerify.Visible = false;
            base.btnView.Visible = false;
            base.btnEdit.Visible = false;
            base.btnConfirm.Visible = false;
            base.btnImport.Visible = false;
            base.btnActivation.Visible = false;
            base.btnExport.Visible = false;
        }
        #endregion

        #region 取消事件
        void UCOldPartsPalautusAddOrEdit_CancelEvent(object sender, EventArgs e)
        {
            try
            {
                if (MessageBoxEx.Show("确认要取消吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
                deleteMenuByTag(this.Tag.ToString(), uc.Name);
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 提交事件
        void UCOldPartsPalautusAddOrEdit_SubmitEvent(object sender, EventArgs e)
        {
            opName = "提交宇通旧件返厂单信息";
            SaveOrSubmitMethod("提交", DataSources.EnumAuditStatus.SUBMIT);
        }
        #endregion

        #region 保存事件
        void UCOldPartsPalautusAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            opName = "保存宇通旧件返厂单信息";
            SaveOrSubmitMethod("保存", DataSources.EnumAuditStatus.DRAFT);
        }
        #endregion

        #region 保存、提交方法
        /// <summary>
        /// 保存、提交方法
        /// </summary>
        /// <param name="strMessage">提示信息</param>
        /// <param name="Estatus">单据操作状态</param>
        private void SaveOrSubmitMethod(string strMessage, DataSources.EnumAuditStatus Estatus)
        {
            try
            {
                #region 必要的判断
                string strSCurrentSMon = dtpSTime.Value.Month.ToString();//创建日期范围开始日期的月份
                string strSCurrentEMon = dtpETime.Value.Month.ToString();//创建日期范围结束日期的月份
                if (strSCurrentEMon != strSCurrentSMon)
                {
                    MessageBoxEx.Show("创建日期范围必须在同一个月!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                string strSCurrentSDay = dtpSTime.Value.Day.ToString();//创建日期范围开始日期的天
                if (strSCurrentSDay != "1")
                {
                    MessageBoxEx.Show("创建日期范围,开始日期必须为每月第1天!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (dtpSTime.Value.AddMonths(1).AddDays(-1).ToShortDateString() != dtpETime.Value.ToShortDateString())
                {
                    MessageBoxEx.Show("创建日期范围必须为整月份!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (string.IsNullOrEmpty(laboldpart_receipts_no.Text))
                {
                    MessageBoxEx.Show("请先执行同步操作!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                #endregion

                //if (!string.IsNullOrEmpty(laboldpart_receipts_no.Text.Trim()))
                //{
                //    string result = DBHelper.GetSingleValue("检测该单号是否存在", "tb_maintain_oldpart_recycle", "receipt_time", "oldpart_receipts_no='" + laboldpart_receipts_no.Text.Trim() + "'", "");
                //    if (!string.IsNullOrEmpty(result))
                //    {
                //        MessageBoxEx.Show("该日期范围内的旧件返厂单据已存在!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                //        return;
                //    }
                //}
                if (MessageBoxEx.Show("确认要" + strMessage + "吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
                List<SQLObj> listSql = new List<SQLObj>();
                SaveOrderInfo(listSql, Estatus);
                SaveMaterialsData(listSql);
                if (Estatus == DataSources.EnumAuditStatus.SUBMIT)
                {
                    ProcessModeToYT();//提交时把处理状态提交到宇通
                }
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
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
                MessageBoxEx.Show("" + strMessage + "失败！" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 宇通旧件返厂单基本信息保存
        private void SaveOrderInfo(List<SQLObj> listSql, DataSources.EnumAuditStatus status)
        {
            try
            {
                SQLObj obj = new SQLObj();
                obj.cmdType = CommandType.Text;
                Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                #region 基本信息
                dicParam.Add("receipt_time", new ParamObj("receipt_time", Common.LocalDateTimeToUtcLong(dtpMakeOrderTime.Value).ToString(), SysDbType.BigInt));//制单日期 
                dicParam.Add("remarks", new ParamObj("remarks", txtRemark.Caption.Trim(), SysDbType.NVarChar, 200));//备注           
                #endregion
                //经办人id
                dicParam.Add("responsible_opid", new ParamObj("responsible_opid", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobYHandle.SelectedValue)) ? cobYHandle.SelectedValue.ToString() : null, SysDbType.VarChar, 40));
                //经办人
                dicParam.Add("responsible_name", new ParamObj("responsible_name", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobYHandle.SelectedValue)) ? cobYHandle.SelectedText : null, SysDbType.VarChar, 40));
                //部门
                dicParam.Add("org_name", new ParamObj("org_name", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cboOrgId.SelectedValue)) ? cboOrgId.SelectedValue : null, SysDbType.VarChar, 40));
                dicParam.Add("enable_flag", new ParamObj("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString(), SysDbType.VarChar, 1));//信息状态（1|激活；2|作废；0|删除）

                //单据类型          
                if (status == DataSources.EnumAuditStatus.SUBMIT)//提交操作时生成单号
                {
                    dicParam.Add("receipts_no", new ParamObj("receipts_no",!string.IsNullOrEmpty(labPalNoS.Text.Trim())?labPalNoS.Text.Trim():CommonUtility.GetNewNo(SYSModel.DataSources.EnumProjectType.OldPartsPalautus), SysDbType.VarChar, 40));//返厂单号 
                    //单据状态
                    dicParam.Add("info_status", new ParamObj("info_status", Convert.ToInt32(status).ToString(), SysDbType.VarChar, 40));
                }
                else
                {
                    if (!string.IsNullOrEmpty(strStatus) && strStatus != Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString())
                    {
                        dicParam.Add("receipts_no", new ParamObj("receipts_no", labPalNoS.Text.Trim(), SysDbType.VarChar, 40));//返厂单号 
                        //单据状态
                        dicParam.Add("info_status", new ParamObj("info_status", Convert.ToInt32(strStatus).ToString(), SysDbType.VarChar, 40));
                    }
                    else
                    {
                        dicParam.Add("receipts_no", new ParamObj("receipts_no", labPalNoS.Text.Trim(), SysDbType.VarChar, 40));//返厂单号
                        //单据状态
                        dicParam.Add("info_status", new ParamObj("info_status", Convert.ToInt32(status).ToString(), SysDbType.VarChar, 40));
                    }
                }
                dicParam.Add("return_id", new ParamObj("return_id", strId, SysDbType.VarChar, 40));//Id
                if (wStatus == WindowStatus.Add)
                {
                    strId = Guid.NewGuid().ToString();
                    dicParam.Add("create_by", new ParamObj("create_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//创建人id（制单人） 
                    dicParam.Add("create_name", new ParamObj("create_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//创建人
                    dicParam.Add("create_time", new ParamObj("create_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));
                    obj.sqlString = @"update tb_maintain_oldpart_recycle set receipt_time=@receipt_time,remarks=@remarks,responsible_opid=@responsible_opid,responsible_name=@responsible_name
                  ,org_name=@org_name,enable_flag=@enable_flag,info_status=@info_status,receipts_no=@receipts_no,create_by=@create_by,create_name=@create_name,create_time=@create_time 
                  where return_id=@return_id ";
                }
                else if (wStatus == WindowStatus.Edit)
                {
                    dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
                    dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
                    dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                    obj.sqlString = @"update tb_maintain_oldpart_recycle set receipt_time=@receipt_time,remarks=@remarks,responsible_opid=@responsible_opid,responsible_name=@responsible_name
                  ,org_name=@org_name,enable_flag=@enable_flag,info_status=@info_status,receipts_no=@receipts_no,update_by=@update_by,update_name=@update_name,update_time=@update_time
                where return_id=@return_id";
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

        #region 宇通旧件返厂单信息信息保存
        private void SaveMaterialsData(List<SQLObj> listSql)
        {
            try
            {
                foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
                {
                    string strPNO = CommonCtrl.IsNullToString(dgvr.Cells["service_no"].Value);
                    string strPCode = CommonCtrl.IsNullToString(dgvr.Cells["parts_code"].Value);
                    if (strPNO.Length > 0 && strPCode.Length > 0)
                    {
                        SQLObj obj = new SQLObj();
                        obj.cmdType = CommandType.Text;
                        Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                        dicParam.Add("process_mode", new ParamObj("process_mode", dgvr.Cells["process_mode"].Value, SysDbType.VarChar, 40));//处理方式 
                        dicParam.Add("send_num", new ParamObj("send_num", dgvr.Cells["send_num"].Value, SysDbType.Decimal));//发送数量
                        dicParam.Add("parts_id", new ParamObj("parts_id", dgvr.Cells["parts_id"].Value, SysDbType.VarChar, 40));
                        opName = "更新旧件收货单";
                        obj.sqlString = @"update tb_maintain_oldpart_recycle_material_detail set process_mode=@process_mode,send_num=@send_num 
                        where parts_id=@parts_id";
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

        #region 旧件回收处理方式同步到宇通
        private void ProcessModeToYT()
        {
            try
            {
                partReturn model = new partReturn();
                model.PartDetails = new partReturnPartDetail[intDetailCount];
                model.crm_old_bill_num = laboldpart_receipts_no.Text;
                model.info_status_yt = info_status_yt;
                model.create_time_start = dtpSTime.Value.ToString("yyyy-MM-dd");
                model.create_time_end = dtpETime.Value.ToString("yyyy-MM-dd");
                for (int i = 0; i < dgvMaterials.Rows.Count; i++)
                {
                    string strPNO = CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["service_no"].Value);
                    string strPCode = CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["parts_code"].Value);
                    if (strPCode.Length > 0)
                    {
                        partReturnPartDetail detail = new partReturnPartDetail();
                        detail.parts_id = CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["parts_id"].Value);
                        detail.service_no = CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["service_no"].Value);
                        detail.car_parts_code = CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["parts_code"].Value);
                        detail.parts_remark = CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["receive_explain"].Value);
                        detail.change_num = CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["change_num"].Value);
                        detail.send_num = CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["send_num"].Value);
                        detail.process_mode = CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["process_mode"].Value);
                        detail.remark = "";
                        model.PartDetails[i] = detail;
                    }
                }
                DBHelper.WebServHandler("回收旧件-更新", EnumWebServFunName.UpPartRetureUpdate, model);
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 绑定宇通单据处理方式
        /// <summary>
        /// 绑定宇通单据处理方式
        /// </summary>
        private void BindModeYT()
        {
            try
            {
                DataTable dot = CommonCtrl.GetDictByCode("set_mode_yt", true);
                process_mode.DataSource = dot;
                process_mode.ValueMember = "dic_id";
                process_mode.DisplayMember = "dic_name";
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 同步事件
        void UCOldPartsPalautusAddOrEdit_SyncEvent(object sender, EventArgs e)
        {
            try
            {
                #region 必要的判断
                if (dtpSTime.Value > dtpETime.Value)
                {
                    MessageBoxEx.Show("创建日期范围,开始时间不能大于结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string strCurrentSMon = dtpSTime.Value.Month.ToString();//创建日期范围开始日期的月份
                string strCurrentEMon = dtpETime.Value.Month.ToString();//创建日期范围结束日期的月份
                if (strCurrentEMon != strCurrentSMon)
                {
                    MessageBoxEx.Show("创建日期范围必须在同一个月!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                string strCurrentSDay = dtpSTime.Value.Day.ToString();//创建日期范围开始日期的天
                if (strCurrentSDay != "1")
                {
                    MessageBoxEx.Show("创建日期范围,开始日期必须为每月第1天!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (dtpSTime.Value.AddMonths(1).AddDays(-1).ToShortDateString() != dtpETime.Value.ToShortDateString())
                {
                    MessageBoxEx.Show("创建日期范围必须为整月份!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }


                #endregion
                //宇通同步的数据没有状态,如果有保存,就有状态了
                strWhere = @"enable_flag is null and create_time_start='" + Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dtpSTime.Value.ToString("yyyy-MM-dd"))).ToString() + "'and create_time_end='" + Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dtpETime.Value.ToString("yyyy-MM-dd"))).ToString() + "' ";
                this.uiHandler -= new UiHandler(this.ShowData);
                this.uiHandler += new UiHandler(this.ShowData);
                //加载帐套信息
                ThreadPool.QueueUserWorkItem(new WaitCallback(this.GetWebServHandler), dtpSTime.Value.ToString("yyyy-MM-dd") + "," + dtpETime.Value.ToString("yyyy-MM-dd"));
               // FormLoading.StartLoading(dgvMaterials);


                //PercentProcessOperator process = new PercentProcessOperator();
                //#region 匿名方法，后台线程执行调用
                //process.BackgroundWork =
                //    delegate(Action<int> percent)
                //    {
                //        ThreadPool.QueueUserWorkItem(new WaitCallback(this.GetWebServHandler), dtpSTime.Value.ToString("yyyy-MM-dd") + "," + dtpETime.Value.ToString("yyyy-MM-dd"));
                //    };
                //#endregion
                //process.MessageInfo = "正在执行中";
                //process.Maximum = 1;
                //#region 匿名方法，后台线程执行完调用
                //process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(
                //        delegate(object osender, BackgroundWorkerEventArgs be)
                //        {
                //            if (be.BackGroundException == null)
                //            {
                //                //MessageBoxEx.ShowInformation("同步成功！");
                //            }
                //            else
                //            {
                //                throw be.BackGroundException;
                //            }
                //        }
                //    );
                //#endregion
                //process.Start();
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }

        }
        #endregion

        #region 新开线程处理同步的数据
        /// <summary>
        /// 同步宇通数据
        /// </summary>
        /// <param name="o"></param>
        private void GetWebServHandler(object o)
        {
            try
            {
                Model.partReturn part = new Model.partReturn();
                string strResult = DBHelper.WebServHandler("回收旧件-创建", EnumWebServFunName.UpPartReturnCreate, o);
                DataTable dt = DBHelper.GetTable("获取返厂单数据", "tb_maintain_oldpart_recycle", "*", strWhere, "", "order by return_id desc ");
                if (this.uiHandler != null)
                {
                    this.Invoke(this.uiHandler, dt);
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        /// <summary>
        /// 查询同步过的数据
        /// </summary>
        /// <param name="obj>"</param>
        private void ShowData(object obj)
        {
            try
            {
                DataTable dt = obj as DataTable;
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        intDetailCount = dt.Rows.Count;
                        DataRow dr = dt.Rows[0];

                        #region 基础信息
                        info_status_yt = CommonCtrl.IsNullToString(dr["info_status_yt"]);//宇通单据状态
                        laboldpart_receipts_no.Text = CommonCtrl.IsNullToString(dr["oldpart_receipts_no"]);//旧件回收单号
                        labservice_station_code.Text = CommonCtrl.IsNullToString(dr["service_station_code"]);//服务站编码
                        labservice_station_name.Text = CommonCtrl.IsNullToString(dr["service_station_name"]);//服务站名称
                        string CreateTime = CommonCtrl.IsNullToString(dr["create_time_yt"]);//创建时间
                        if (!string.IsNullOrEmpty(CreateTime))
                        {
                            labytcreate_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(CreateTime)).ToString();
                        }
                        string DepotTime = CommonCtrl.IsNullToString(dr["depot_time"]);//回厂时间
                        if (!string.IsNullOrEmpty(DepotTime))
                        {
                            labdepot_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(DepotTime)).ToString();
                        }
                        string NitarizeTime = CommonCtrl.IsNullToString(dr["notarize_time"]);//确认时间
                        if (!string.IsNullOrEmpty(NitarizeTime))
                        {
                            labnotarize_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(NitarizeTime)).ToString();
                        }
                        labsum_money.Text = CommonCtrl.IsNullToString(dr["sum_money"]);//旧件回收费用
                        strId = CommonCtrl.IsNullToString(dr["return_id"]);//旧件返厂单Id
                        #endregion

                        #region 详细信息
                        DataTable dmt = DBHelper.GetTable("宇通旧件返厂明细数据", "tb_maintain_oldpart_recycle_material_detail", "*", string.Format(" maintain_id='{0}'", strId), "", "");
                        if (dmt.Rows.Count > 0)
                        {
                            if (dmt.Rows.Count > dgvMaterials.Rows.Count)
                            {
                                dgvMaterials.Rows.Add(dmt.Rows.Count - dgvMaterials.Rows.Count + 1);
                            }
                            for (int i = 0; i < dmt.Rows.Count; i++)
                            {
                                DataRow dmr = dmt.Rows[i];
                                dgvMaterials.Rows[i].Cells["service_no"].Value = CommonCtrl.IsNullToString(dmr["service_no"]);
                                dgvMaterials.Rows[i].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dmr["parts_code"]);
                                dgvMaterials.Rows[i].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dmr["parts_name"]);
                                dgvMaterials.Rows[i].Cells["change_num"].Value = CommonCtrl.IsNullToString(dmr["change_num"]);
                                dgvMaterials.Rows[i].Cells["send_num"].Value = CommonCtrl.IsNullToString(dmr["send_num"]);
                                dgvMaterials.Rows[i].Cells["receive_num"].Value = CommonCtrl.IsNullToString(dmr["receive_num"]);
                                dgvMaterials.Rows[i].Cells["unit"].Value = CommonCtrl.IsNullToString(dmr["unit"]);
                                dgvMaterials.Rows[i].Cells["unit_price"].Value = CommonCtrl.IsNullToString(dmr["unit_price"]);
                                dgvMaterials.Rows[i].Cells["need_recycle_mark"].Value = CommonCtrl.IsNullToString(dmr["need_recycle_mark"]);
                                dgvMaterials.Rows[i].Cells["all_recycle_mark"].Value = CommonCtrl.IsNullToString(dmr["all_recycle_mark"]);
                                dgvMaterials.Rows[i].Cells["original"].Value = CommonCtrl.IsNullToString(dmr["original"]);
                                dgvMaterials.Rows[i].Cells["identity_state"].Value = CommonCtrl.IsNullToString(dmr["identity_state"]);
                                dgvMaterials.Rows[i].Cells["second_station_code"].Value = CommonCtrl.IsNullToString(dmr["second_station_code"]);
                                dgvMaterials.Rows[i].Cells["process_mode"].Value = CommonCtrl.IsNullToString(dmr["process_mode"]);
                                dgvMaterials.Rows[i].Cells["remarks"].Value = CommonCtrl.IsNullToString(dmr["remarks"]);
                                dgvMaterials.Rows[i].Cells["receive_explain"].Value = CommonCtrl.IsNullToString(dmr["receive_explain"]);
                                dgvMaterials.Rows[i].Cells["parts_id"].Value = CommonCtrl.IsNullToString(dmr["parts_id"]);
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        //FormLoading.EndLoading();
                        MessageBoxEx.Show("该日期范围内暂无数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                        return;
                    }
                }
                //FormLoading.EndLoading();
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 判断创建日期
        /// <summary>
        /// 判断创建日期
        /// </summary>
        private bool JudgeDatetime()
        {
            bool isOk = true;
            string strCurrentSMon = dtpSTime.Value.Month.ToString();//创建日期范围开始日期的月份
            string strCurrentEMon = dtpETime.Value.Month.ToString();//创建日期范围结束日期的月份
            if (strCurrentEMon != strCurrentSMon)
            {
                MessageBoxEx.Show("创建日期范围必须在同一个月!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                isOk = false;
            }
            string strCurrentSDay = dtpSTime.Value.Day.ToString();//创建日期范围开始日期的天
            if (strCurrentSDay != "1")
            {
                MessageBoxEx.Show("创建日期范围,开始日期必须为每月第1天!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                isOk = false;
            }
            if (dtpSTime.Value.AddMonths(1).AddDays(-1).ToShortDateString() != dtpETime.Value.ToShortDateString())
            {
                MessageBoxEx.Show("创建日期范围必须为整月份!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                isOk = false;
            }
            return isOk;
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

        #region 窗体Load事件
        private void UCOldPartsPalautusAddOrEdit_Load(object sender, EventArgs e)
        {

            SetTopbuttonShow();
            dtpMakeOrderTime.Value = DateTime.Now;
            dtpSTime.Value = DateTime.Now.AddDays(1 - DateTime.Now.Day);
            dtpETime.Value = dtpSTime.Value.AddMonths(1).AddDays(-1);
            //base.SetBtnStatus(wStatus);
            if (wStatus == WindowStatus.Edit || wStatus == WindowStatus.Copy)
            {
                BindData();
            }
            else if (wStatus == WindowStatus.Add)
            {
                labPalNoS.Visible = false;
            }
        }
        #endregion

        #region 根据宇通旧件返厂单ID获取信息编辑用
        /// <summary>
        /// 根据宇通旧件返厂单ID获取信息，编辑用
        /// </summary>
        private void BindData()
        {
            try
            {
                #region 基础信息
                string strWhere = string.Format("return_id='{0}'", strId);
                DataTable dt = DBHelper.GetTable("查询宇通旧件返厂单", "tb_maintain_oldpart_recycle ", "*", strWhere, "", "");
                if (dt == null || dt.Rows.Count == 0)
                {
                    return;
                }
                DataRow dr = dt.Rows[0];
                string strMtime = CommonCtrl.IsNullToString(dr["receipt_time"]);
                if (!string.IsNullOrEmpty(strMtime))
                {
                    long Rticks = Convert.ToInt64(strMtime);
                    dtpMakeOrderTime.Value = Common.UtcLongToLocalDateTime(Rticks);//制单日期
                }
                string strStime = CommonCtrl.IsNullToString(dr["create_time_start"]);
                if (!string.IsNullOrEmpty(strStime))
                {
                    long Rticks = Convert.ToInt64(strStime);
                    dtpSTime.Value = Common.UtcLongToLocalDateTime(Rticks);//创建开始日期
                }
                string strEtime = CommonCtrl.IsNullToString(dr["create_time_end"]);
                if (!string.IsNullOrEmpty(strEtime))
                {
                    long Rticks = Convert.ToInt64(strEtime);
                    dtpETime.Value = Common.UtcLongToLocalDateTime(Rticks);//创建结束日期
                }
                txtRemark.Caption = CommonCtrl.IsNullToString(dr["remarks"]);//备注
                info_status_yt = CommonCtrl.IsNullToString(dr["info_status_yt"]);//宇通单据状态
                laboldpart_receipts_no.Text = CommonCtrl.IsNullToString(dr["oldpart_receipts_no"]);//旧件回收单号
                labservice_station_code.Text = CommonCtrl.IsNullToString(dr["service_station_code"]);//服务站编码
                labservice_station_name.Text = CommonCtrl.IsNullToString(dr["service_station_name"]);//服务站名称           
                string strMTime = CommonCtrl.IsNullToString(dr["create_time"]); //创建时间
                if (!string.IsNullOrEmpty(strMTime))
                {
                    labytcreate_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(strMTime)).ToShortDateString();
                }
                else
                {
                    labytcreate_time.Text = string.Empty;
                }
                string strdepot_time = CommonCtrl.IsNullToString(dr["depot_time"]); //回厂时间
                if (!string.IsNullOrEmpty(strdepot_time))
                {
                    labdepot_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(strdepot_time)).ToShortDateString();
                }
                else
                {
                    labdepot_time.Text = string.Empty;
                }
                string strnotarize_time = CommonCtrl.IsNullToString(dr["notarize_time"]); //确认时间
                if (!string.IsNullOrEmpty(strnotarize_time))
                {
                    labnotarize_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(strnotarize_time)).ToShortDateString();
                }
                else
                {
                    labnotarize_time.Text = string.Empty;
                }
                labsum_money.Text = CommonCtrl.IsNullToString(dr["sum_money"]);//旧件回收费用

                #endregion

                #region 详细信息
                DataTable dmt = DBHelper.GetTable("宇通旧件返厂明细数据", "tb_maintain_oldpart_recycle_material_detail", "*", string.Format(" maintain_id='{0}'", strId), "", "");
                if (dmt.Rows.Count > 0)
                {
                    intDetailCount = dmt.Rows.Count;
                    if (dmt.Rows.Count > dgvMaterials.Rows.Count)
                    {
                        dgvMaterials.Rows.Add(dmt.Rows.Count - dgvMaterials.Rows.Count + 1);
                    }
                    for (int i = 0; i < dmt.Rows.Count; i++)
                    {
                        DataRow dmr = dmt.Rows[i];
                        dgvMaterials.Rows[i].Cells["service_no"].Value = CommonCtrl.IsNullToString(dmr["service_no"]);
                        dgvMaterials.Rows[i].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dmr["parts_code"]);
                        dgvMaterials.Rows[i].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dmr["parts_name"]);
                        dgvMaterials.Rows[i].Cells["change_num"].Value = CommonCtrl.IsNullToString(dmr["change_num"]);
                        dgvMaterials.Rows[i].Cells["send_num"].Value = CommonCtrl.IsNullToString(dmr["send_num"]);
                        dgvMaterials.Rows[i].Cells["receive_num"].Value = CommonCtrl.IsNullToString(dmr["receive_num"]);
                        dgvMaterials.Rows[i].Cells["unit"].Value = CommonCtrl.IsNullToString(dmr["unit"]);
                        dgvMaterials.Rows[i].Cells["unit_price"].Value = CommonCtrl.IsNullToString(dmr["unit_price"]);
                        dgvMaterials.Rows[i].Cells["need_recycle_mark"].Value = CommonCtrl.IsNullToString(dmr["need_recycle_mark"]);
                        dgvMaterials.Rows[i].Cells["all_recycle_mark"].Value = CommonCtrl.IsNullToString(dmr["all_recycle_mark"]);
                        dgvMaterials.Rows[i].Cells["original"].Value = CommonCtrl.IsNullToString(dmr["original"]);
                        dgvMaterials.Rows[i].Cells["identity_state"].Value = CommonCtrl.IsNullToString(dmr["identity_state"]);
                        dgvMaterials.Rows[i].Cells["second_station_code"].Value = CommonCtrl.IsNullToString(dmr["second_station_code"]);
                        dgvMaterials.Rows[i].Cells["process_mode"].Value = CommonCtrl.IsNullToString(dmr["process_mode"]);
                        dgvMaterials.Rows[i].Cells["remarks"].Value = CommonCtrl.IsNullToString(dmr["remarks"]);
                        dgvMaterials.Rows[i].Cells["receive_explain"].Value = CommonCtrl.IsNullToString(dmr["receive_explain"]);
                        dgvMaterials.Rows[i].Cells["parts_id"].Value = CommonCtrl.IsNullToString(dmr["parts_id"]);
                    }
                }
                #endregion

                if (wStatus == WindowStatus.Edit)
                {
                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["receipts_no"])))
                    {
                        labPalNoS.Text = CommonCtrl.IsNullToString(dr["receipts_no"]);//返厂单号
                    }
                    else
                    {
                        labPalNoS.Visible = false;
                    }
                }
                else
                {
                    labPalNoS.Visible = false;
                }

                #region 顶部按钮设置
                strStatus = CommonCtrl.IsNullToString(dr["info_status"]);//单据状态
                if (strStatus == Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString())
                {
                    //已提交状态屏蔽提交按钮
                    base.btnSubmit.Enabled = false;
                }
                else if (strStatus == Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString())
                {
                    //已审核时屏蔽提交、审核按钮
                    base.btnSubmit.Enabled = false;
                    base.btnVerify.Enabled = false;
                }
                //else if (strStatus == Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString())
                //{
                //    //审核没通过时屏蔽提交按钮
                //    base.btnSubmit.Enabled = false;
                //}
                #endregion
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
            #region 维修用料设置
            dgvMaterials.Dock = DockStyle.Fill;
            //dgvMaterials.Columns["MCheck"].HeaderText = "选择";
            dgvMaterials.ReadOnly = false;
            dgvMaterials.Rows.Add(3);
            dgvMaterials.AllowUserToAddRows = true;
            dgvMaterials.Columns["service_no"].ReadOnly = true;
            dgvMaterials.Columns["parts_code"].ReadOnly = true;
            dgvMaterials.Columns["parts_name"].ReadOnly = true;
            dgvMaterials.Columns["change_num"].ReadOnly = true;
            //dgvMaterials.Columns["send_num"].ReadOnly = true;
            dgvMaterials.Columns["receive_num"].ReadOnly = true;
            dgvMaterials.Columns["unit"].ReadOnly = true;
            dgvMaterials.Columns["unit_price"].ReadOnly = true;
            dgvMaterials.Columns["need_recycle_mark"].ReadOnly = true;
            dgvMaterials.Columns["all_recycle_mark"].ReadOnly = true;
            dgvMaterials.Columns["original"].ReadOnly = true;
            dgvMaterials.Columns["identity_state"].ReadOnly = true;
            dgvMaterials.Columns["second_station_code"].ReadOnly = true;
            dgvMaterials.Columns["remarks"].ReadOnly = true;
            dgvMaterials.Columns["receive_explain"].ReadOnly = true;
            dgvMaterials.Columns["parts_id"].ReadOnly = true;
            send_num.ReadOnly = false;
            #endregion
        }
        #endregion

        #region 生成宇通旧件返厂单单号创建人姓名
        /// <summary>
        /// 生成宇通旧件返厂单单号创建人姓名
        /// </summary>
        private void GetRepairNo()
        {
            //labPalNoS.Text = CommonUtility.GetNewNo(SYSModel.DataSources.EnumProjectType.OldPartsPalautus);
            labCreatePersonS.Text = HXCPcClient.GlobalStaticObj.UserName;
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
    }
}
