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
        /// 是否整体派工
        /// </summary>
        bool IsAllOverall = false;
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
        decimal strHMoney =0;
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
            if (MessageBoxEx.Show("确认要质检吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            Inspection = new UCInspection();
            if (Inspection.ShowDialog() == DialogResult.OK)
            {
                strInspection = Inspection.Content;
                DStatus = Inspection.DStatus;
                PStatus = Inspection.PStatus;
                AlterOrdersStatus(DStatus, PStatus);
            }
        }
        #endregion

        #region 试结算事件
        void UCDispatchDetails_BalanceEvent(object sender, EventArgs e)
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
        #endregion

        #region 完工事件
        void UCDispatchDetails_CompleteEvent(object sender, EventArgs e)
        {
            if (!JudgeProjectOK("完工"))
            {
                return;
            }
            if (MessageBoxEx.Show("确认要完工吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
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
            if (DBHelper.BatchExeSQLMultiByTrans(opName, listSql))
            {
                MessageBoxEx.Show("已完工!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                uc.BindPageData();
                base.deleteMenuByTag(this.Tag.ToString(), this.uc.Name);
            }
        }
        #endregion

        #region 停工事件
        void UCDispatchDetails_StopEvent(object sender, EventArgs e)
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
        #endregion

        #region 开工事件
        void UCDispatchDetails_StartEvent(object sender, EventArgs e)
        {
            //if (!JudgeProjectOK("开工"))
            //{
            //    return;
            //}
            if (MessageBoxEx.Show("确认要开工吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            DStatus = DataSources.EnumDispatchStatus.StartWork;
            PStatus = DataSources.EnumProjectDisStatus.Maintenance;           
            strStarTime = DateTime.Now.ToString();
            strStopTime = string.Empty;
            strCTime = string.Empty;
            base.btnSave.Enabled = true;
            base.btnComplete.Enabled = true;
            base.btnStop.Enabled = true;
            if (strProjectSattus == "4")//停工在开工
            {
                strStarTime = string.Empty;
                strContinueTime = DateTime.Now.ToString();               
            }
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
        #endregion

        #region 判断是否选择了开工、停工、完工所需的项目
        /// <summary>
        /// 判断是否选择了开工、停工、完工所需的项目
        /// </summary>
        /// <param name="strMessage">提示信息</param>
        /// <returns></returns>
        private bool JudgeProjectOK(string strMessage)
        {
            bool isboolCheck = true;
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
                    }
                }
            }
            if (listField.Count == 0)
            {
                isboolCheck = false;
                MessageBoxEx.Show("请选择需要" + strMessage + "项目！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }           
            return isboolCheck;
        }

        #endregion

        #region 确认派工事件
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
            //            if (strProgress=="未派工")
            //            {
            //                //if (MessageBoxEx.Show("您有未派工项目，请派工！", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            //                //{
            //                //    return;
            //                //}
            //                MessageBoxEx.Show("您有未派工项目，请派工！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                return;
            //            }
            //        }
            //    }
            //}
            //else if (!IsChooserProject())
            //{
            //    return;
            //}
            if (MessageBoxEx.Show("确认要派工吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
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

        //#region 整体派工事件
        //void UCDispatchDetails_OverallEvent(object sender, EventArgs e)
        //{
        //    if (!IsCheckAll())
        //    {
        //        return;
        //    }
        //    frmDispatching persons = new frmDispatching();
        //    DialogResult result = persons.ShowDialog();
        //    if (result == DialogResult.OK)
        //    {
        //        BindUserInfo(persons.strUserId);               
        //        base.btnAffirm.Enabled = true;//确认派工
        //        IsAllOverall = true;
        //    }
        //}
        //#endregion

        #region 整体派工时判断是否选中所有项目
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

        #region 派工事件
        void UCDispatchDetails_DtaloEvent(object sender, EventArgs e)
        {
            if (!IsChooserProject())
            {
                return;
            }
            if (MessageBoxEx.Show("确认要派工吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
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

            #region 保存调度人员的信息
            List<SQLObj> listSqlUser = new List<SQLObj>();
            foreach (DataGridViewRow dgvr in dgvproject.Rows)
            {
                object isCheck = dgvr.Cells["colCheck"].EditedFormattedValue;

                if (isCheck != null && (bool)isCheck)
                {
                    string strProjectID = CommonCtrl.IsNullToString(dgvr.Cells["OldItem_id"].Value);
                    DispatchUserInfo(listSqlUser, strReceiveId, strProjectID);
                    DBHelper.BatchExeSQLMultiByTrans("添加派工人员信息", listSqlUser);
                }
            }
            //BindUserInfo(persons.strUserId);
            //base.btnAffirm.Enabled = true;//确认派工
            base.btnSave.Enabled = true;//保存  
            base.btnSatart.Enabled = true;//开工
            DStatus = DataSources.EnumDispatchStatus.NotStartWork;
            PStatus = DataSources.EnumProjectDisStatus.NotStartWork;
            AlterOrdersStatus(DStatus, PStatus);
            #endregion
            //    IsAllOverall = true;
            //}

        }
        #endregion

        #region 检测是否选择了派工人员
        /// <summary>
        /// 检测是否选择了派工人员
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
                MessageBoxEx.Show("请选择派工人员信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                isboolCheck = true;
            }
            return isboolCheck;
        }
        #endregion

        #region 派工时修改单据与项目的状态,开工、停工、完工、继续时间，停工原因，停工时长
        /// <summary>
        ///派工时修改单据与项目的状态
        /// </summary>
        /// <param name="DStatus">单据状态</param>
        /// <param name="PStatus">项目状态</param>       
        private void AlterOrdersStatus(DataSources.EnumDispatchStatus DStatus, DataSources.EnumProjectDisStatus PStatus)
        {
            labStatusS.Text = labStatusS.Text = DataSources.GetDescription(typeof(DataSources.EnumDispatchStatus), int.Parse(CommonCtrl.IsNullToString(Convert.ToInt32(DStatus).ToString())));//单据状态
            foreach (DataGridViewRow dr in dgvproject.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                string strPname = CommonCtrl.IsNullToString(dr.Cells["item_name"].Value);
                if (strPname.Length > 0)
                {
                    if (PStatus == DataSources.EnumProjectDisStatus.NotStartWork)
                    {
                        if (isCheck != null && (bool)isCheck)
                        {
                            dr.Cells["repair_progress"].Value = DataSources.GetDescription(typeof(DataSources.EnumProjectDisStatus), int.Parse(CommonCtrl.IsNullToString(Convert.ToInt32(PStatus).ToString())));//项目状态 
                        }
                    }
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
                    if (!string.IsNullOrEmpty(strContinueTime))
                    {
                        dr.Cells["continue_time"].Value = strContinueTime;
                        TimeSpan nd = Convert.ToDateTime(strContinueTime) - Convert.ToDateTime(dr.Cells["shut_down_time"].Value.ToString());
                        dr.Cells["shut_down_duration"].Value = nd.TotalMinutes.ToString();
                    }
                    //}
                }
            }
        }
        #endregion

        #region 根据派工人员Id获取人员信息并绑定到datagridview中
        /// <summary>
        /// 根据派工人员Id获取人员信息并绑定到datagridview中
        /// </summary>
        /// <param name="strUId">用户Id字符串</param>
        private void BindUserInfo(string strUId,int introws)
        {
            foreach (DataGridViewRow dgvr in dgvUserData.Rows)
            {
                string strPCode = CommonCtrl.IsNullToString(dgvr.Cells["dispatch_no"].Value);
                if (!string.IsNullOrEmpty(strPCode))
                {
                    listUser.Add(strPCode);
                }
            }
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
                    dgvUserData.Rows[introws].Cells["org_name"].Value = CommonCtrl.IsNullToString(dur["org_name"]);
                    //dgvUserData.Rows[i].Cells["team_name"].Value = CommonCtrl.IsNullToString(dur["dic_name"]);
                    dgvUserData.Rows[introws].Cells["create_time"].Value = DateTime.Now.ToString();
                    dgvUserData.Rows[introws].Cells["Usum_money"].Value = strMHourMoney;                   
                //}

            }

        }
        #endregion

        #region 派工时绑定人员信息
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strUId">用户Id字符串</param>
        private void BindUserInfo(string strUId)
        {
            foreach (DataGridViewRow dgvr in dgvUserData.Rows)
            {
                string strPCode = CommonCtrl.IsNullToString(dgvr.Cells["dispatch_no"].Value);
                if (!string.IsNullOrEmpty(strPCode))
                {
                    listUser.Add(strPCode);
                }
            }
            DataTable dut = DBHelper.GetTable("查询人员信息", " v_user", "*", "user_id in (" + strUId.Substring(0,strUId.Length-1) + ")", "", " order by user_id desc");
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
                //dgvUserData.Rows[i].Cells["team_name"].Value = CommonCtrl.IsNullToString(dur["dic_name"]);
                dgvUserData.Rows[i].Cells["create_time"].Value = DateTime.Now.ToString();
                dgvUserData.Rows[i].Cells["Usum_money"].Value = strMHourMoney;
                }

            }

        }
        #endregion

        #region 保存事件
        void UCDispatchDetails_AddSaveEvent(object sender, EventArgs e)
        {
            List<SQLObj> listSql = new List<SQLObj>();
            UpdateRepairOrderInfo(listSql,DStatus);
            AttachmentInfo(listSql);
            SaveProjectData(listSql,strReceiveId,PStatus);
            SaveMaterialsData(listSql,strReceiveId);
            SaveOtherData(listSql,strReceiveId);
            //DispatchUserInfo(listSql,strReceiveId);
            if (DBHelper.BatchExeSQLMultiByTrans(opName, listSql))
            {
                MessageBoxEx.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                uc.BindPageData();
                base.deleteMenuByTag(this.Tag.ToString(), this.uc.Name);
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
                dicParam.Add("complete_work_time", new ParamObj("complete_work_time",null, SysDbType.BigInt));//完工时间               
            }
            dicParam.Add("set_meal", new ParamObj("set_meal", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobSetMeal.SelectedValue)) ? cobSetMeal.SelectedValue.ToString() : null, SysDbType.VarChar, 40));//维修套餐
            dicParam.Add("maintain_id", new ParamObj("maintain_id", strReceiveId, SysDbType.VarChar, 40));//Id
            dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
            dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
            dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
            if (!string.IsNullOrEmpty(strInspection))
            {
                dicParam.Add("Verify_advice", new ParamObj("Verify_advice", strInspection, SysDbType.VarChar, 200));//质检意见 
                obj.sqlString = @"update tb_maintain_info set dispatch_status=@dispatch_status,set_meal=@set_meal,update_by=@update_by,update_name=@update_name,update_time=@update_time,Verify_advice=@Verify_advice
            where maintain_id=@maintain_id";
            }
            else
            {
                obj.sqlString = @"update tb_maintain_info set dispatch_status=@dispatch_status,complete_work_time=@complete_work_time,set_meal=@set_meal,update_by=@update_by,update_name=@update_name,update_time=@update_time
            where maintain_id=@maintain_id";
            }
            obj.Param = dicParam;
            listSql.Add(obj);
        }
        #endregion

        #region 更新附件信息
        private void AttachmentInfo(List<SQLObj> listSql)
        {
            ucAttr.TableNameKeyValue = strReceiveId;
            listSql.AddRange(ucAttr.AttachmentSql);
        }
        #endregion

        #region 维修项目信息保存
        private void SaveProjectData(List<SQLObj> listSql, string partID, DataSources.EnumProjectDisStatus PStatus)
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
                    string strPostion = CommonCtrl.IsNullToString(dgvr.Cells["repair_station"].Value);//所在工位
                    dicParam.Add("repair_station", new ParamObj("repair_station", !string.IsNullOrEmpty(strPostion)?strPostion:null, SysDbType.VarChar, 40));
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
                    dicParam.Add("shut_down_reason", new ParamObj("shut_down_reason", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["shut_down_reason"].Value)) ? dgvr.Cells["shut_down_reason"].Value.ToString() : null, SysDbType.VarChar,200));
                    //停工累计时
                    dicParam.Add("shut_down_duration", new ParamObj("shut_down_duration", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["shut_down_duration"].Value)) ? dgvr.Cells["shut_down_duration"].Value.ToString() : null, SysDbType.Decimal,15));
                    //继续开工时间
                    dicParam.Add("continue_time", new ParamObj("continue_time", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["continue_time"].Value)) ? Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dgvr.Cells["continue_time"].Value)).ToString() : null, SysDbType.BigInt));
                    dicParam.Add("remarks", new ParamObj("remarks", dgvr.Cells["remarks"].Value, SysDbType.VarChar, 200));
                    dicParam.Add("enable_flag", new ParamObj("enable_flag", "1", SysDbType.VarChar, 1));
                    string strPID = CommonCtrl.IsNullToString(dgvr.Cells["item_id"].Value);
                    if (strPID == "NewId")
                    {
                        opName = "新增接待单维修项目";
                        strPID = Guid.NewGuid().ToString();
                        dicParam.Add("item_id", new ParamObj("item_id", strPID, SysDbType.VarChar, 40));
                        obj.sqlString = @"insert into [tb_maintain_item] (item_id,maintain_id,item_no,item_type,item_name,man_hour_type,man_hour_quantity,man_hour_norm_unitprice,member_discount,member_price,member_sum_money,sum_money_goods
                        ,repair_progress,three_warranty,repair_station,start_work_time,complete_work_time,shut_down_time,shut_down_reason,shut_down_duration,continue_time,remarks,enable_flag)
                        values (@item_id,@maintain_id,@item_no,@item_type,@item_name,@man_hour_type,@man_hour_quantity,@man_hour_norm_unitprice,@member_discount,@member_price,@member_sum_money,@sum_money_goods
                        ,@repair_progress,@three_warranty,@repair_station,@start_work_time,@complete_work_time,@shut_down_time,@shut_down_reason,@shut_down_duration,@continue_time,@remarks,@enable_flag);";
                    }
                    else
                    {
                        dicParam.Add("item_id", new ParamObj("item_id", dgvr.Cells["item_id"].Value, SysDbType.VarChar, 40));
                        opName = "更新接待单维修项目";
                        obj.sqlString = @"update tb_maintain_item set item_no=@item_no,item_type=@item_type,item_name=@item_name,man_hour_type=@man_hour_type,man_hour_quantity=@man_hour_quantity,man_hour_norm_unitprice=@man_hour_norm_unitprice,member_discount=@member_discount,member_price=@member_price,
                        member_sum_money=@member_sum_money,sum_money_goods=@sum_money_goods,repair_progress=@repair_progress,three_warranty=@three_warranty,repair_station=@repair_station,start_work_time=@start_work_time,complete_work_time=@complete_work_time,shut_down_time=@shut_down_time
                        ,shut_down_reason=@shut_down_reason,shut_down_duration=@shut_down_duration,continue_time=@continue_time,remarks=@remarks,enable_flag=@enable_flag where item_id=@item_id";
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
                    dicParam.Add("vehicle_brand", new ParamObj("vehicle_brand", dgvr.Cells["vehicle_brand"].Value, SysDbType.VarChar, 40));
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
                    string strPID = CommonCtrl.IsNullToString(dgvr.Cells["material_id"].Value);
                    if (strPID.Length == 0)
                    {
                        opName = "新增接待单维修用料";
                        strPID = Guid.NewGuid().ToString();
                        dicParam.Add("material_id", new ParamObj("material_id", strPID, SysDbType.VarChar, 40));
                        obj.sqlString = "insert into [tb_maintain_material_detail] (material_id,parts_code,parts_name,norms,unit,whether_imported,quantity,unit_price,member_sum_money,sum_money,drawn_no,vehicle_brand,three_warranty,remarks,enable_flag,maintain_id) ";
                        obj.sqlString += " values (@material_id,@parts_code,@parts_name,@norms,@unit,@whether_imported,@quantity,@unit_price,@sum_money,@drawn_no,@vehicle_brand,@member_sum_money,@three_warranty,@remarks,@enable_flag,@maintain_id);";
                    }
                    else
                    {
                        dicParam.Add("material_id", new ParamObj("material_id", dgvr.Cells["material_id"].Value, SysDbType.VarChar, 40));
                        opName = "更新接待单维修用料";
                        obj.sqlString = "update tb_maintain_material_detail set parts_code=@parts_code,parts_name=@parts_name,norms=@norms,unit=@unit,whether_imported=@whether_imported,quantity=@quantity,unit_price=@unit_price,sum_money=@sum_money,";
                        obj.sqlString += "drawn_no=@drawn_no,vehicle_brand=@vehicle_brand,member_sum_money=@member_sum_money, three_warranty=@three_warranty,remarks=@remarks,enable_flag=@enable_flag where material_id=@material_id";
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

        #region 人员派工信息保存
        /// <summary>
        /// 人员派工信息保存
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="strMaintainId">关联的维修单ID</param>
        /// <param name="strProjectId">关联的维修项目Id</param>
        private void DispatchUserInfo(List<SQLObj> listSql, string strMaintainId, string strProjectId)
        {

            foreach (DataGridViewRow dgvr in dgvUserData.Rows)
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
                    //派工时间
                    dicParam.Add("create_time", new ParamObj("create_time", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["create_time"].Value)) ? Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dgvr.Cells["create_time"].Value)).ToString() : null, SysDbType.BigInt));
                    dicParam.Add("item_id", new ParamObj("item_id", strProjectId, SysDbType.VarChar, 40));//Id

                    opName = "新增派工人员信息";
                    string strDId = Guid.NewGuid().ToString();
                    dicParam.Add("dispatch_id", new ParamObj("dispatch_id", strDId, SysDbType.VarChar, 40));
                    obj.sqlString = @"delete from tb_maintain_dispatch_worker where maintain_id=@maintain_id and item_id=@item_id; insert into tb_maintain_dispatch_worker(maintain_id,dispatch_no,dispatch_name,org_name,team_name,man_hour,sum_money,create_time,dispatch_id,item_id)
                        values (@maintain_id,@dispatch_no,@dispatch_name,@org_name,@team_name,@man_hour,@sum_money,@create_time,@create_time,@item_id);";

                    obj.Param = dicParam;
                    listSql.Add(obj);
                }
            }
        }
        #endregion

        #region 取消事件
        void UCDispatchDetails_CancelEvent(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确认要取消吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            base.deleteMenuByTag(this.Tag.ToString(), uc.Name); 
        }
        #endregion

        #region 根据维修单Id获取信息
        private void GetReservData(string strRId)
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
                #region 会员信息
                DataTable dct = DBHelper.GetTable("获取会员信息", "tb_customer", "member_number,member_class,accessories_discount,workhours_discount", " is_member='1' and cust_id='" + labCustomNOS.Tag + "'", "", "");
                if (dct.Rows.Count > 0)
                {
                    DataRow dcr = dct.Rows[0];                   
                    strMemberPZk = CommonCtrl.IsNullToString(dcr["workhours_discount"]);
                    strMemberLZk = CommonCtrl.IsNullToString(dcr["accessories_discount"]);
                }
                else
                {
                    strMemberPZk = string.Empty; ;
                    strMemberLZk = string.Empty; ;
                }
                #endregion
                labContactS.Text = CommonCtrl.IsNullToString(dr["linkman"]);//联系人
                labContactPhoneS.Text = CommonCtrl.IsNullToString(dr["link_man_mobile"]);//联系人电话
                labCarNOS.Text = CommonCtrl.IsNullToString(dr["vehicle_no"]);//车牌号
                labCarTypeS.Text = GetVehicleModels(CommonCtrl.IsNullToString(dr["vehicle_model"]));//车型
                labCarBrandS.Text = GetDicName(CommonCtrl.IsNullToString(dr["vehicle_brand"]));//车辆品牌
                labVINS.Text = CommonCtrl.IsNullToString(dr["vehicle_vin"]);//VIN
                labEngineNoS.Text = CommonCtrl.IsNullToString(dr["engine_no"]);//发动机号
                labColorS.Text = GetDicName(CommonCtrl.IsNullToString(dr["vehicle_color"]));//颜色
                labDriverS.Text = CommonCtrl.IsNullToString(dr["driver_name"]);//司机
                labDriverPhoneS.Text = CommonCtrl.IsNullToString(dr["driver_mobile"]);//司机手机
                labRepTypeS.Text = GetDicName(CommonCtrl.IsNullToString(dr["maintain_type"]));//维修类别
                labPayTypeS.Text = GetDicName(CommonCtrl.IsNullToString(dr["maintain_payment"]));//维修付费方式
                string strInTime = CommonCtrl.IsNullToString(dr["completion_time"]);//预计完工时间
                if (!string.IsNullOrEmpty(strInTime))
                {
                    labSuTimeS.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(strInTime)).ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    labSuTimeS.Text = string.Empty;
                }
                labMlS.Text = CommonCtrl.IsNullToString(dr["oil_into_factory"]);//进场油量
                labMilS.Text = CommonCtrl.IsNullToString(dr["travel_mileage"]);//行驶里程
                labDescS.Text = CommonCtrl.IsNullToString(dr["fault_describe"]);//故障描述 
                labRemarkS.Text = CommonCtrl.IsNullToString(dr["remark"]);//备注
                labStatusS.Text = DataSources.GetDescription(typeof(DataSources.EnumDispatchStatus), int.Parse(CommonCtrl.IsNullToString(dr["dispatch_status"])));//单据状态
                strProjectSattus = CommonCtrl.IsNullToString(dr["dispatch_status"]);
                //labMoney.Text = CommonCtrl.IsNullToString(dr["maintain_payment"]);//欠款余额
                labDepartS.Text =GetDepartmentName(CommonCtrl.IsNullToString(dr["org_name"]));//部门
                labAttnS.Text =GetUserSetName(CommonCtrl.IsNullToString(dr["responsible_name"]));//经办人
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
                    if (dpt.Rows.Count > dgvproject.Rows.Count)
                    {
                        dgvproject.Rows.Add(dpt.Rows.Count - dgvproject.Rows.Count + 1);
                    }
                    for (int i = 0; i < dpt.Rows.Count; i++)
                    {
                        DataRow dpr = dpt.Rows[i];
                        dgvproject.Rows[i].Cells["colCheck"].Value = true;
                        dgvproject.Rows[i].Cells["item_id"].Value = CommonCtrl.IsNullToString(dpr["item_id"]);
                        dgvproject.Rows[i].Cells["OldItem_id"].Value = CommonCtrl.IsNullToString(dpr["item_id"]);
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
                        dgvproject.Rows[i].Cells["repair_progress"].Value = DataSources.GetDescription(typeof(DataSources.EnumProjectDisStatus), int.Parse(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["repair_progress"]))?CommonCtrl.IsNullToString(dpr["repair_progress"]):"0"));//项目状态 CommonCtrl.IsNullToString(dpr["repair_progress"]);
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
                        dgvproject.Rows[i].Cells["shut_down_duration"].Value = !string.IsNullOrEmpty(DurationTime) ? DurationTime: "";
                        listProject.Add(CommonCtrl.IsNullToString(dpr["item_no"]));
                    }                   
                }
                #endregion

                #region 维修用料数据
                //维修用料数据   
                DataTable dmt = DBHelper.GetTable("维修用料数据", "tb_maintain_material_detail", "*", string.Format(" maintain_id='{0}' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", strRId), "", "");
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
                        string strMaterialNo = DBHelper.GetSingleValue("获得领料单编号", "tb_maintain_fetch_material", "material_no", "material_id='" + CommonCtrl.IsNullToString(dmr["material_id"]) + "'", "");
                        dgvMaterials.Rows[i].Cells["Mmaterial_no"].Value = strMaterialNo;
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
                        dgvOther.Rows[i].Cells["cost_types"].Value =CommonCtrl.IsNullToString(dor["cost_types"]);
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
                                    b.quantity,b.drawn_no,b.vehicle_model,a.fetch_opid,b.remarks";
                string strTable = @"tb_maintain_fetch_material a 
                                    left join tb_maintain_fetch_material_detai b on a. material_id=b.material_id";
                string strWhere = " a.maintain_id='" + labMaintain_noS.Text.Trim() + "'";
                DataTable dft = DBHelper.GetTable("获取领料单情况", strTable, strFildes, strWhere, "", "order by a.fetch_time desc ");
                dgvFData.DataSource = dft;
                #endregion

                #region 派工人员信息
                 //其他项目收费数据             
                DataTable dut = DBHelper.GetTable("派工人员信息", "tb_maintain_dispatch_worker", "*", string.Format(" maintain_id='{0}'", strRId), "", "");
                if (dut.Rows.Count > 0)
                {
                    if (dut.Rows.Count > dgvUserData.Rows.Count)
                    {
                        dgvOther.Rows.Add(dut.Rows.Count - dgvUserData.Rows.Count + 1);
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
                        dgvUserData.Rows[i].Cells["create_time"].Value =!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dur["create_time"]))? Common.UtcLongToLocalDateTime(dur["create_time"]):"";
                        listUser.Add(CommonCtrl.IsNullToString(dur["dispatch_no"]));
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
                labColorS.Text = string.Empty;
                labContactPhoneS.Text = string.Empty;
                labContactS.Text = string.Empty;
                labCreatePersonS.Text = string.Empty;
                labCreateTimeS.Text = string.Empty;
                labCustomNameS.Text = string.Empty;
                labCustomNOS.Text = string.Empty;
                labDepartS.Text = string.Empty;
                labDescS.Text = string.Empty;
                labDriverPhoneS.Text = string.Empty;
                labDriverS.Text = string.Empty;
                labEngineNoS.Text = string.Empty;
                labFinallyPerS.Text = string.Empty;
                labFinallyTimeS.Text = string.Empty;
                labPayTypeS.Text = string.Empty;
                labRemarkS.Text = string.Empty;
                labRepTypeS.Text = string.Empty;
                labStatusS.Text = string.Empty;
                labVINS.Text = string.Empty;
                labMlS.Text = string.Empty;
                labMilS.Text = string.Empty;
                cobSetMeal.SelectedValue = string.Empty;
                #endregion
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
            //dgvproject.Columns["man_hour_norm_unitprice"].ReadOnly = true;           
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
            ControlsConfig.NumberLimitdgv(dgvproject, new List<string>() { "man_hour_quantity" });  
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
            dgvUserData.Columns["team_name"].ReadOnly = true;
            dgvUserData.Columns["create_time"].ReadOnly = true;
            dgvUserData.Columns["Usum_money"].ReadOnly = true;
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
            string strWhere = "period_validity<='" + Common.LocalDateTimeToUtcLong(DateTime.Now).ToString() + "' and valid_until>='" + Common.LocalDateTimeToUtcLong(DateTime.Now).ToString() + "'";
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
            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem("", "全部"));
            string sqlWhere = " parent_id in (select dic_id from sys_dictionaries where dic_code='sys_station_information' and enable_flag=1) ";
            DataTable dt_dic = DBHelper.GetTable("查询字典码表信息", "sys_dictionaries", "dic_id,dic_name", sqlWhere, "", " order by create_time ");
            if (dt_dic != null && dt_dic.Rows.Count > 0)
            {
                foreach (DataRow dr in dt_dic.Rows)
                {
                    list.Add(new ListItem(dr["dic_id"], dr["dic_name"].ToString()));
                }
            }
            repair_station.DisplayMember = "Text";
            repair_station.ValueMember = "Value";
            repair_station.DataSource = list;   
        }
        #endregion

        #region 绑定其他费用类别
        /// <summary>
        /// 绑定其他费用类别
        /// </summary>
        private void BindOMoney()
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
        #endregion

        #region 窗体Load事件
        private void UCDispatchDetails_Load(object sender, EventArgs e)
        {
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
                case "0"://未派工
                    base.btnSave.Enabled = false;//保存 
                    base.btnAffirm.Visible = false;//确认派工
                    base.btnSatart.Enabled = false;//开工
                    base.btnStop.Enabled = false;//停工
                    base.btnComplete.Enabled = false;//完工
                    base.btnQC.Enabled = false;//质检                      
                    break;
                case "1"://已派工
                    base.btnSave.Enabled = false;//保存 
                    base.btnStop.Enabled = false;//停工
                    base.btnComplete.Enabled = false;//完工
                    base.btnQC.Enabled = false;//质检 
                    base.btnAffirm.Visible = false;//确认派工
                    break;
                case "2"://已开工
                    base.btnSave.Enabled = false;//保存 
                    base.btnDtalo.Enabled = false;//逐项派工                  
                    base.btnAffirm.Visible = false;//确认派工
                    base.btnSatart.Enabled = false;//开工
                    base.btnQC.Enabled = false;//质检 
                    break;
                case "3"://已完工
                    base.btnSave.Enabled = false;//保存 
                     base.btnDtalo.Enabled = false;//逐项派工                   
                    base.btnAffirm.Visible = false;//确认派工
                    base.btnSatart.Enabled = false;//开工
                    base.btnStop.Enabled = false;//停工
                    base.btnComplete.Enabled = false;//完工    
                    ucAttr.ReadOnly = true;
                    dgvAttachment.ReadOnly = true;
                     dgvFData.ReadOnly = true;
                    dgvMaterials.ReadOnly = true;
                    dgvOther.ReadOnly = true;
                    dgvproject.ReadOnly = true;
                    dgvUserData.ReadOnly = true;
                    cobSetMeal.Enabled = false;
                    break;
                case "4"://已停工
                    base.btnSave.Enabled = false;//保存
                    base.btnDtalo.Enabled = false;//逐项派工                  
                    base.btnAffirm.Visible = false;//确认派工                   
                    base.btnStop.Enabled = false;//停工
                    base.btnComplete.Enabled = false;//完工
                    base.btnQC.Enabled = false;//质检 
                    break;
                case "5"://已质检未通过
                    base.btnQC.Enabled = false;//质检 
                    break;
                case "6"://已质检通过
                    base.btnSave.Enabled = false;//保存
                    base.btnDtalo.Enabled = false;//逐项派工                  
                    base.btnAffirm.Visible = false;//确认派工
                    base.btnSatart.Enabled = false;//开工
                    base.btnStop.Enabled = false;//停工
                    base.btnComplete.Enabled = false;//完工
                    base.btnQC.Enabled = false;//质检 
                    ucAttr.ReadOnly = true;
                    dgvAttachment.ReadOnly = true;
                    dgvFData.ReadOnly = true;
                    dgvMaterials.ReadOnly = true;
                    dgvOther.ReadOnly = true;
                    dgvproject.ReadOnly = true;
                    dgvUserData.ReadOnly = true;
                    cobSetMeal.Enabled = false;
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

        #region 双击人员单元格获取派工人员信息
        private void dgvUserData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (strProjectSattus != "3" && strProjectSattus != "6")
            {
                if (!IsChooserProject())
                {
                    return;
                }              
                frmPersonnelSelector person = new frmPersonnelSelector();
                DialogResult result = person.ShowDialog();
                if (result == DialogResult.OK)
                {
                    BindUserInfo(person.strUserId, e.RowIndex);
                    dgvUserData.Rows.Add(1);
                }
            }
        }
        #endregion

        #region 检测是否选择了派工项目
        /// <summary>
        /// 检测是否选择了派工项目
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
                MessageBoxEx.Show("请选择需要派工项目！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
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
            if (strProjectSattus != "3" && strProjectSattus != "6")
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
                    dgvproject.Rows[e.RowIndex].Cells["colCheck"].Value = true;
                    dgvproject.Rows[e.RowIndex].Cells["item_no"].Value = frmHours.strProjectNum;
                    dgvproject.Rows[e.RowIndex].Cells["item_type"].Value = GetDicName(frmHours.strRepairType);
                    dgvproject.Rows[e.RowIndex].Cells["item_name"].Value = frmHours.strProjectName;
                    dgvproject.Rows[e.RowIndex].Cells["remarks"].Value = frmHours.strRemark;
                    dgvproject.Rows[e.RowIndex].Cells["man_hour_type"].Value = frmHours.strWhoursType == "1" ? "工时" : "定额";
                    dgvproject.Rows[e.RowIndex].Cells["item_id"].Value = "NewId";
                    string strPzk = !string.IsNullOrEmpty(strMemberPZk) ? strMemberPZk : "10";
                    //工时单价
                    dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value = frmHours.strQuotaPrice;
                    //会员折扣
                    dgvproject.Rows[e.RowIndex].Cells["member_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk) * 10);
                    //会员工时费
                    dgvproject.Rows[e.RowIndex].Cells["member_price"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strQuotaPrice)?frmHours.strQuotaPrice:"0") * (Convert.ToDecimal(strPzk) / 10));
                    if (CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["member_price"].Value) != "0")
                    {
                        //折扣额
                        dgvproject.Rows[e.RowIndex].Cells["member_sum_money"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strQuotaPrice)?frmHours.strQuotaPrice:"0") - Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["member_price"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["member_price"].Value) : "0"));
                    }
                    else
                    {
                        dgvproject.Rows[e.RowIndex].Cells["member_sum_money"].Value = "0";
                    }
                    dgvproject.Rows[e.RowIndex].Cells["OldItem_id"].Value = frmHours.strWhours_id;
                    dgvproject.Rows[e.RowIndex].Cells["man_hour_quantity"].Value = frmHours.strWhoursNum;
                    dgvproject.Rows[e.RowIndex].Cells["sum_money_goods"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strWhoursNum)?frmHours.strWhoursNum:"0") * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["member_price"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["member_price"].Value) : "0"));
                    //新添加数据维修进度设置为未派工
                    dgvproject.Rows[e.RowIndex].Cells["repair_progress"].Value = DataSources.GetDescription(typeof(DataSources.EnumProjectDisStatus), Convert.ToInt64(DataSources.EnumProjectDisStatus.NotWork));//项目状态 CommonCtrl.IsNullToString(dpr["repair_progress"]);                       
                    dgvproject.Rows.Add(1);
                }
            }
        }  
        /// <summary>
        /// 工时数量发生改变时金额也发生改变
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
                if (e.ColumnIndex == 13)
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
        }
        #endregion

        #region 维修用料信息读取
        private void dgvMaterials_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (strProjectSattus != "3" && strProjectSattus != "6")
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
                        string strPzk = !string.IsNullOrEmpty(strMemberPZk) ? strMemberPZk : "10";
                        //会员折扣
                        dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk) * 10);
                        string strNum =!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value))?CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value):"0";
                        string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value)) ? dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value.ToString() : "0";
                        //会员单价
                        dgvMaterials.Rows[e.RowIndex].Cells["Mmember_price"].Value = Convert.ToString(Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal( !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value))?CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value):"0") / 10);
                        dgvMaterials.Rows[e.RowIndex].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dpr["drawing_num"]);
                        dgvMaterials.Rows[e.RowIndex].Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dpr["remark"]);
                        dgvMaterials.Rows[e.RowIndex].Cells["vehicle_brand"].Value = CommonCtrl.IsNullToString(dpr["v_brand_name"]);
                        dgvMaterials.Rows[e.RowIndex].Cells["M_Id"].Value = frmPart.PartsID;
                        dgvMaterials.Rows.Add(1);
                    }
                }
            }
        }

        /// <summary>
        /// 工时数量发生改变时金额也发生改变
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
            if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["parts_name"].Value)))
            {
                if (e.ColumnIndex == 6 || e.ColumnIndex == 7)
                {
                    //if (strIsThree == "否")
                    //{
                    ControlsConfig.SetCellsValue(dgvMaterials, e.RowIndex, "quantity,unit_price");
                    string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value))? dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value.ToString() : "0";
                    string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value))? dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value.ToString() : "0";
                    string strzk = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value)) ? dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value.ToString() : "0";
                    dgvMaterials.Rows[e.RowIndex].Cells["Mmember_price"].Value = Convert.ToString(Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(strzk.Length > 0 ? strzk : "0") / 100);
                    if (CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_price"].Value) != "0")
                    {
                        strzk = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value))?CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value):"0";
                        dgvMaterials.Rows[e.RowIndex].Cells["sum_money"].Value = Convert.ToString(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(strzk) / 100);
                    }
                    else
                    {
                        dgvMaterials.Rows[e.RowIndex].Cells["sum_money"].Value = Convert.ToString(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney));
                    }
                    //}
                }
                if (e.ColumnIndex == 14)
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
        }
        #endregion

        #region 根据分配工时获取分配金额值
        private void dgvUserData_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            string strUName = CommonCtrl.IsNullToString(dgvUserData.Rows[e.RowIndex].Cells["dispatch_no"].Value);
            if (!string.IsNullOrEmpty(strUName))
            {
                if (e.ColumnIndex == 5)
                {
                    //分配工时
                    string strHourNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvUserData.Rows[e.RowIndex].Cells["man_hour"].Value)) ? dgvUserData.Rows[e.RowIndex].Cells["man_hour"].Value.ToString() : "0";
                    //分配金额
                    string strUSumMoney = (Convert.ToDecimal(!string.IsNullOrEmpty(strMHourMoney)?strMHourMoney:"0") * Convert.ToDecimal(strHourNum)).ToString();
                    dgvUserData.Rows[e.RowIndex].Cells["Usum_money"].Value = strUSumMoney;
                }
            }
        }
         #endregion

        #region 右键新增维修用料
        private void addParts_Click(object sender, EventArgs e)
        {
            if (strProjectSattus != "3" && strProjectSattus != "6")
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
                        string strPzk = !string.IsNullOrEmpty(strMemberPZk) ? strMemberPZk : "10";
                        //会员折扣
                        dgvMaterials.CurrentRow.Cells["Mmember_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk) * 10);
                        string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.CurrentRow.Cells["quantity"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.CurrentRow.Cells["quantity"].Value) : "0";
                        string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.CurrentRow.Cells["unit_price"].Value)) ? dgvMaterials.CurrentRow.Cells["unit_price"].Value.ToString() : "0";
                        //会员单价
                        dgvMaterials.CurrentRow.Cells["Mmember_price"].Value = Convert.ToString(Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(dgvMaterials.CurrentRow.Cells["Mmember_discount"].Value) / 10);
                        dgvMaterials.CurrentRow.Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dpr["drawing_num"]);
                        dgvMaterials.CurrentRow.Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dpr["remark"]);
                        dgvMaterials.CurrentRow.Cells["vehicle_brand"].Value = CommonCtrl.IsNullToString(dpr["v_brand_name"]);
                        dgvMaterials.CurrentRow.Cells["M_Id"].Value = frmPart.PartsID;
                        dgvMaterials.Rows.Add(1);
                    }
                }
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

        #region 右键添加维修项目
        private void addProject_Click(object sender, EventArgs e)
        {
            if (strProjectSattus != "3" && strProjectSattus != "6")
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
                    dgvproject.CurrentRow.Cells["colCheck"].Value = true;
                    dgvproject.CurrentRow.Cells["item_no"].Value = frmHours.strProjectNum;
                    dgvproject.CurrentRow.Cells["item_type"].Value = GetDicName(frmHours.strRepairType);
                    dgvproject.CurrentRow.Cells["item_name"].Value = frmHours.strProjectName;
                    dgvproject.CurrentRow.Cells["remarks"].Value = frmHours.strRemark;
                    dgvproject.CurrentRow.Cells["man_hour_type"].Value = frmHours.strWhoursType == "1" ? "工时" : "定额";
                    dgvproject.CurrentRow.Cells["item_id"].Value = "NewId";
                    string strPzk = !string.IsNullOrEmpty(strMemberPZk) ? strMemberPZk : "10";
                    //工时单价
                    dgvproject.CurrentRow.Cells["man_hour_norm_unitprice"].Value = frmHours.strQuotaPrice;
                    //会员折扣
                    dgvproject.CurrentRow.Cells["member_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk) * 10);
                    //会员工时费
                    dgvproject.CurrentRow.Cells["member_price"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strQuotaPrice) ? frmHours.strQuotaPrice : "0") * (Convert.ToDecimal(strPzk) / 10));
                    if (dgvproject.CurrentRow.Cells["member_price"].Value.ToString() != "0")
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
                    //新添加数据维修进度设置为未派工
                    dgvproject.CurrentRow.Cells["repair_progress"].Value = DataSources.GetDescription(typeof(DataSources.EnumProjectDisStatus), Convert.ToInt64(DataSources.EnumProjectDisStatus.NotWork));//项目状态 CommonCtrl.IsNullToString(dpr["repair_progress"]);                       
                    dgvproject.Rows.Add(1);
                }
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

        #region 维修项目选择行
        private void dgvproject_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == colCheck.Index)
            {
                return;
            }
            //清空已选择框
            foreach (DataGridViewRow dgvr in dgvproject.Rows)
            {
                object check = dgvr.Cells[colCheck.Name].EditedFormattedValue;
                if (check != null && (bool)check)
                {
                    dgvr.Cells[colCheck.Name].Value = false;
                }
            }
            //选择当前行
            dgvproject.Rows[e.RowIndex].Cells[colCheck.Name].Value = true;
            string strProjectID = CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["OldItem_id"].Value);
            #region 派工人员信息
            //其他项目收费数据             
            DataTable dut = DBHelper.GetTable("派工人员信息", "tb_maintain_dispatch_worker", "*", string.Format(" maintain_id='{0}' and item_id='{1}'", strReceiveId, strProjectID), "", "");
            if (dut.Rows.Count > 0)
            {
                if (dut.Rows.Count > dgvUserData.Rows.Count)
                {
                    dgvUserData.Rows.Add(dut.Rows.Count - dgvUserData.Rows.Count + 1);
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
                    dgvUserData.Rows[i].Cells["create_time"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dur["create_time"])) ? Common.UtcLongToLocalDateTime(dur["create_time"]) : "";
                    listUser.Add(CommonCtrl.IsNullToString(dur["dispatch_no"]));
                }
            }
            #endregion
        }
        #endregion
    }
}
