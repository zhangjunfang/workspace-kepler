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
using HXCPcClient.UCForm.RepairBusiness.OldPartsManagement.YTOldPartsPalautusQuery;
using Model;

namespace HXCPcClient.UCForm.RepairBusiness.OldPartsManagement.YTOldPartsPalautus
{
    public partial class UCOldPartsPalautusView : UCBase
    {
        #region 属性设置
        /// <summary>
        /// 宇通旧件返厂单的Id值
        /// </summary>
        string strPalautusId = string.Empty;
        /// <summary>
        /// 父窗体
        /// </summary>
        public UCOldPartsPalautusManager uc;
        /// <summary>
        /// 窗体-宇通旧件返厂查询父
        /// </summary>
        public UCOldPartsPalautusQueryManage Quc;
        /// <summary>
        /// 审核窗体
        /// </summary>
        UCVerify verify;
        /// <summary>
        /// 单据状态
        /// </summary>
        string strStatus = string.Empty;
        /// <summary>
        /// 返厂详细信息总条数
        /// </summary>
        int intDetailCount = 0;
        /// <summary>
        /// 回收周期开始时间
        /// </summary>
        string strHSTime = string.Empty;
        /// <summary>
        /// 回收周期结束时间
        /// </summary>
        string strHETime = string.Empty;
        #endregion

        #region 初始化窗体
        public UCOldPartsPalautusView(string strId)
        {
            InitializeComponent();
            strPalautusId = strId;
            SetTopbuttonShow();
            base.AddEvent += new ClickHandler(UCOldPartsPalautusView_AddEvent);
            base.EditEvent += new ClickHandler(UCOldPartsPalautusView_EditEvent);
            base.DeleteEvent += new ClickHandler(UCOldPartsPalautusView_DeleteEvent);
            base.SubmitEvent += new ClickHandler(UCOldPartsPalautusView_SubmitEvent);
            base.VerifyEvent += new ClickHandler(UCOldPartsPalautusView_VerifyEvent);
            base.InvalidOrActivationEvent += new ClickHandler(UCOldPartsPalautusView_InvalidOrActivationEvent);
            base.ConfirmEvent += new ClickHandler(UCOldPartsPalautusView_ConfirmEvent);
        }
        #endregion

        #region 确认事件
        void UCOldPartsPalautusView_ConfirmEvent(object sender, EventArgs e)
        {
            if (intDetailCount > 0)
            {
                if (MessageBoxEx.Show("确定要确认吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
                ProcessModeToYT();
                MessageBoxEx.Show("确认成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                uc.BindPageData();
                deleteMenuByTag(this.Tag.ToString(), "UCOldPartsSendView");
            }
        }
        #endregion

        #region 旧件回收确认到宇通
        /// <summary>
        /// 旧件回收确认到宇通
        /// </summary>
        private void ProcessModeToYT()
        {
            partReturn model = new partReturn();
            model.PartDetails = new partReturnPartDetail[intDetailCount];
            model.crm_old_bill_num = laboldpart_receipts_no.Text;
            model.info_status_yt = DBHelper.GetSingleValue("", "sys_dictionaries", "dic_id", "dic_code='oldpart_recycle_status_PCM_FIX_CALLBACK_ENTER'", ""); ;
            model.create_time_start = strHSTime;
            model.create_time_end = strHETime;
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
        #endregion

        #region 作废激活事件
        void UCOldPartsPalautusView_InvalidOrActivationEvent(object sender, EventArgs e)
        {
            string strmsg = string.Empty;
            List<SQLObj> listSql = new List<SQLObj>();
            SQLObj obj = new SQLObj();
            obj.cmdType = CommandType.Text;
            Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
            dicParam.Add("return_id", new ParamObj("return_id", strPalautusId, SysDbType.VarChar, 40));//单据ID
            dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
            dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
            dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
            if (strStatus != Convert.ToInt32(DataSources.EnumAuditStatus.Invalid).ToString())
            {
                strmsg = "作废";
                dicParam.Add("info_status", new ParamObj("info_status", DataSources.EnumAuditStatus.Invalid, SysDbType.VarChar, 40));//单据状态
            }
            else
            {
                strmsg = "激活";
                string OnStatus = "";
                DataTable dvt = DBHelper.GetTable("获得前一个状态", "tb_maintain_oldpart_recycle_BackUp", "info_status", "return_id='" + strPalautusId + "'", "", "order by update_time desc");
                if (dvt.Rows.Count > 0)
                {
                    DataRow dr = dvt.Rows[0];
                    OnStatus = CommonCtrl.IsNullToString(dr["info_status"]);
                    if (OnStatus == Convert.ToInt32(DataSources.EnumAuditStatus.Invalid).ToString())
                    {
                        DataRow dr1 = dvt.Rows[1];
                        OnStatus = CommonCtrl.IsNullToString(dr1["info_status"]);
                    }

                }
                OnStatus = !string.IsNullOrEmpty(OnStatus) ? OnStatus : Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString();
                dicParam.Add("info_status", new ParamObj("info_status", OnStatus, SysDbType.VarChar, 40));//单据状态
            }
            obj.sqlString = "update tb_maintain_oldpart_recycle set info_status=@info_status,update_by=@update_by,update_name=@update_name,update_time=@update_time where return_id=@return_id";
            obj.Param = dicParam;
            listSql.Add(obj);
            if (MessageBoxEx.Show("确认要" + strmsg + "吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            if (DBHelper.BatchExeSQLMultiByTrans("更新单据状态为" + strmsg + "", listSql))
            {
                MessageBoxEx.Show("" + strmsg + "成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                uc.BindPageData();
                deleteMenuByTag(this.Tag.ToString(), "UCOldPartsSendView");
            }
            else
            {
                MessageBoxEx.Show("" + strmsg + "失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 顶部button显示设置
        /// <summary>
        ///  顶部button显示设置
        /// </summary>
        private void SetTopbuttonShow()
        {
            base.btnCopy.Visible = false;
            base.btnSave.Visible = false;
            base.btnCancel.Visible = false;
            base.btnImport.Visible = false;
            base.btnStatus.Visible = false;
            base.btnSync.Visible = false;
            base.btnAdd.Visible = false;
        }
        #endregion

        #region 审核事件
        void UCOldPartsPalautusView_VerifyEvent(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确认要审核吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            verify = new UCVerify();
            if (verify.ShowDialog() == DialogResult.OK)
            {
                List<SQLObj> listSql = new List<SQLObj>();
                SQLObj obj = new SQLObj();
                obj.cmdType = CommandType.Text;
                Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                dicParam.Add("return_id", new ParamObj("return_id", strPalautusId, SysDbType.VarChar, 40));//单据ID
                dicParam.Add("info_status", new ParamObj("info_status", verify.auditStatus, SysDbType.VarChar, 40));//单据状态
                dicParam.Add("verify_advice", new ParamObj("verify_advice", verify.Content, SysDbType.VarChar, 200));//审核意见
                dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
                dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
                dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                obj.sqlString = "update tb_maintain_oldpart_recycle set info_status=@info_status,verify_advice=@verify_advice,update_by=@update_by,update_name=@update_name,update_time=@update_time where return_id=@return_id";
                obj.Param = dicParam;
                listSql.Add(obj);
                if (DBHelper.BatchExeSQLMultiByTrans("更新单据状态为审核", listSql))
                {
                    string strMsg = string.Empty;
                    if (verify.auditStatus == DataSources.EnumAuditStatus.AUDIT)
                    {
                        strMsg = "成功";
                    }
                    else
                    {
                        strMsg = "不通过";
                    }
                    MessageBoxEx.Show("审核" + strMsg + "！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    uc.BindPageData();
                    deleteMenuByTag(this.Tag.ToString(), "UCOldPartsReceiptView");
                }
            }
        }
        #endregion

        #region 提交事件
        void UCOldPartsPalautusView_SubmitEvent(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确认要提交吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            List<SQLObj> listSql = new List<SQLObj>();
            SQLObj obj = new SQLObj();
            obj.cmdType = CommandType.Text;
            Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
            if (strStatus == Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString())
            {
                labMaterialNoS.Text = CommonUtility.GetNewNo(SYSModel.DataSources.EnumProjectType.OldPartsPalautus);
            }
            dicParam.Add("receipts_no", new ParamObj("receipts_no", labMaterialNoS.Text, SysDbType.VarChar, 40));//单据编号                   
            dicParam.Add("return_id", new ParamObj("return_id", strPalautusId, SysDbType.VarChar, 40));//单据ID
            dicParam.Add("info_status", new ParamObj("info_status", DataSources.EnumAuditStatus.SUBMIT, SysDbType.VarChar, 40));//单据状态
            dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
            dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
            dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
            obj.sqlString = "update tb_maintain_oldpart_recycle set info_status=@info_status,receipts_no=@receipts_no,update_by=@update_by,update_name=@update_name,update_time=@update_time where return_id=@return_id";
            obj.Param = dicParam;
            listSql.Add(obj);
            if (DBHelper.BatchExeSQLMultiByTrans("更新单据状态为提交", listSql))
            {
                MessageBoxEx.Show("提交成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                uc.BindPageData();
                deleteMenuByTag(this.Tag.ToString(), "UCOldPartsPalautusView");
            }
        }
        #endregion

        #region 删除事件
        void UCOldPartsPalautusView_DeleteEvent(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确认要删除吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            List<SQLObj> listSql = new List<SQLObj>();
            List<string> listField = new List<string>();
            listField.Add(strPalautusId);
            Dictionary<string, string> comField = new Dictionary<string, string>();
            comField.Add("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.DELETED).ToString());
            comField.Add("update_by", HXCPcClient.GlobalStaticObj.UserID);//修改人Id
            comField.Add("update_name", HXCPcClient.GlobalStaticObj.UserName);//修改人姓名
            comField.Add("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString());//修改时间  
            bool flag = DBHelper.BatchUpdateDataByIn("删除宇通旧件返厂单", "tb_maintain_oldpart_recycle", comField, "return_id", listField.ToArray());
            if (flag)
            {
                MessageBoxEx.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                uc.BindPageData();
                deleteMenuByTag(this.Tag.ToString(), "UCOldPartsPalautusView");
            }
            else
            {
                MessageBoxEx.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 编辑事件
        void UCOldPartsPalautusView_EditEvent(object sender, EventArgs e)
        {
            UCOldPartsPalautusAddOrEdit PalautusEdit = new UCOldPartsPalautusAddOrEdit();
            PalautusEdit.wStatus = WindowStatus.Edit;
            PalautusEdit.uc = uc;
            PalautusEdit.strId = strPalautusId;  //编辑单据的Id值
            base.addUserControl(PalautusEdit, "宇通旧件返厂单-编辑", "PalautusEdit" + PalautusEdit.strId, this.Tag.ToString(), this.Name);
            deleteMenuByTag(this.Tag.ToString(), "UCOldPartsPalautusView");
        }
        #endregion

        #region 新增事件
        void UCOldPartsPalautusView_AddEvent(object sender, EventArgs e)
        {
            UCOldPartsPalautusAddOrEdit PalautuAdd = new UCOldPartsPalautusAddOrEdit();
            PalautuAdd.wStatus = WindowStatus.Add;
            PalautuAdd.uc = uc;
            base.addUserControl(PalautuAdd, "宇通旧件返厂单-新增", "PalautuAdd" + PalautuAdd.strId, this.Tag.ToString(), this.Name);
            deleteMenuByTag(this.Tag.ToString(), "UCOldPartsPalautusView");
        }
        #endregion        

        #region 根据宇通旧件返厂单Id获取相应的详细信息
        /// <summary>
        /// 根据宇通旧件返厂单Id获取相应的详细信息
        /// </summary>
        /// <param name="strRId">宇通旧件返厂单Id值</param>
        private void GetRescueData(string strRId)
        {
            #region 基本信息
            //SetBtnStatus(WindowStatus.View);
            DataTable dt = DBHelper.GetTable("宇通旧件返厂单预览", "tb_maintain_oldpart_recycle", "*", string.Format(" return_id='{0}'", strRId), "", "");
            if (dt.Rows.Count > 0)
            {
                #region 维修表信息
                DataRow dr = dt.Rows[0];
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["receipts_no"])))
                {
                    labMaterialNoS.Text = CommonCtrl.IsNullToString(dr["receipts_no"]);//返厂单号 
                }
                else
                {
                    labMaterialNoS.Text = string.Empty;
                }  
                string strCreateTime = CommonCtrl.IsNullToString(dr["receipt_time"]); //制单日期
                if (!string.IsNullOrEmpty(strCreateTime))
                {
                    labCreateTimeS.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(strCreateTime)).ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    labCreateTimeS.Text = string.Empty;
                }
                labStatusS.Text = DataSources.GetDescription(typeof(DataSources.EnumAuditStatus), int.Parse(CommonCtrl.IsNullToString(dr["info_status"])));//单据状态 
                labYTStatusS.Text = GetDicName(CommonCtrl.IsNullToString(dr["info_status_yt"]));//宇通旧件回收单状态
                //创建日期范围
                string strSTime = CommonCtrl.IsNullToString(dr["receipt_time"]); //创建开始时间
                if (!string.IsNullOrEmpty(strSTime))
                {
                    strSTime = Common.UtcLongToLocalDateTime(Convert.ToInt64(strSTime)).ToString("yyyy-MM-dd");
                }
                else
                {
                    strSTime = string.Empty;
                }
                string strETime = CommonCtrl.IsNullToString(dr["receipt_time"]); //创建结束时间
                if (!string.IsNullOrEmpty(strETime))
                {
                    strETime = Common.UtcLongToLocalDateTime(Convert.ToInt64(strETime)).ToString("yyyy-MM-dd");
                }
                else
                {
                    strETime = string.Empty;
                }
                labTimeS.Text = strSTime + "-" + strETime;
                labRemarkS.Text = CommonCtrl.IsNullToString(dr["remarks"]);//备注  
                laboldpart_receipts_no.Text = CommonCtrl.IsNullToString(dr["oldpart_receipts_no"]);//旧件回收单号 
                labservice_station_code.Text = CommonCtrl.IsNullToString(dr["service_station_code"]);//服务站编码 
                labservice_station_name.Text = CommonCtrl.IsNullToString(dr["service_station_name"]);//服务站名称 
                string strCTime = CommonCtrl.IsNullToString(dr["create_time_yt"]); //创建时间
                if (!string.IsNullOrEmpty(strCTime))
                {
                    labcreate_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(strCTime)).ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    labcreate_time.Text = string.Empty;
                }
                string strdepot_time = CommonCtrl.IsNullToString(dr["receipt_time"]); //回厂时间
                if (!string.IsNullOrEmpty(strdepot_time))
                {
                    labdepot_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(strdepot_time)).ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    labdepot_time.Text = string.Empty;
                }
                string strnotarize_time = CommonCtrl.IsNullToString(dr["notarize_time"]); //确认时间
                if (!string.IsNullOrEmpty(strnotarize_time))
                {
                    labnotarize_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(strnotarize_time)).ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    labnotarize_time.Text = string.Empty;
                }
                labsum_money.Text = CommonCtrl.IsNullToString(dr["sum_money"]); //旧件回收费用
                labDepartS.Text = GetDepartmentName(CommonCtrl.IsNullToString(dr["org_name"]));//部门
                labAttnS.Text = GetUserSetName(CommonCtrl.IsNullToString(dr["responsible_name"]));//经办人
                labCreatePersonS.Text = CommonCtrl.IsNullToString(dr["create_name"]);//创建人               
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
                long tickeS = Convert.ToInt64(CommonCtrl.IsNullToString(dr["create_time_start"]));
                long tickeE = Convert.ToInt64(CommonCtrl.IsNullToString(dr["create_time_end"]));
                strHSTime = Common.UtcLongToLocalDateTime(tickeS).ToString("yyyy-MM-dd");
                strHETime = Common.UtcLongToLocalDateTime(tickeE).ToString("yyyy-MM-dd");
                strStatus = CommonCtrl.IsNullToString(dr["info_status"]);//单据状态
                if (strStatus == Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString())
                {
                    //已提交状态屏蔽提交、编辑、删除按钮
                    base.btnSubmit.Enabled = false;
                    base.btnEdit.Enabled = false;
                    base.btnDelete.Enabled = false;
                    base.btnActivation.Enabled = false;
                }
                else if (strStatus == Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString())
                {
                    //已审核时屏蔽提交、审核、编辑、删除按钮
                    base.btnSubmit.Enabled = false;
                    base.btnVerify.Enabled = false;
                    base.btnEdit.Enabled = false;
                    base.btnDelete.Enabled = false;
                    base.btnActivation.Enabled = false;
                }
                else if (strStatus == Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString() || strStatus == Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString())
                {
                    //审核没通过时屏蔽审核按钮
                    base.btnVerify.Enabled = false;
                }
                else if (strStatus == Convert.ToInt32(DataSources.EnumAuditStatus.Invalid).ToString())
                {
                    base.btnActivation.Caption = "激活";
                    base.btnSubmit.Enabled = false;
                    base.btnVerify.Enabled = false;
                    base.btnEdit.Enabled = false;
                }
                #endregion
            #endregion

            #region 配件信息
                DataTable dpt = DBHelper.GetTable("宇通旧件返厂单详情", "tb_maintain_oldpart_recycle_material_detail", "*", string.Format(" maintain_id='{0}'", strRId), "", "");
                if (dpt.Rows.Count > 0)
                {
                    dgvMaterials.DataSource = dpt;
                    intDetailCount = dpt.Rows.Count;
                }
                #endregion
            }
            else
            {
                #region 没有数据时全部显示为空                
                labCreatePersonS.Text = string.Empty;
                labCreateTimeS.Text = string.Empty;               
                labDepartS.Text = string.Empty;
                labFinallyPerS.Text = string.Empty;
                labFinallyTimeS.Text = string.Empty;
                labStatusS.Text = string.Empty;
                labRemarkS.Text = string.Empty;
                #endregion
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
        private void UCOldPartsPalautusView_Load(object sender, EventArgs e)
        {
            GetRescueData(strPalautusId);
           
        }
        #endregion

        #region 重写详细数据部分数据
        private void dgvMaterials_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }
        #endregion
    }
}
