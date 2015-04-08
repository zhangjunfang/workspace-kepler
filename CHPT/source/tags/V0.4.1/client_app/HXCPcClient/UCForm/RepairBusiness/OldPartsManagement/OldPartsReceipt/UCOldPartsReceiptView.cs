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

namespace HXCPcClient.UCForm.RepairBusiness.OldPartsManagement.OldPartsReceipt
{
    /// <summary>
    /// 旧件管理-旧件收货单预览
    /// Author：JC
    /// AddTime：2014.10.31
    /// </summary>
    public partial class UCOldPartsReceiptView : UCBase
    {
        #region 属性设置
        /// <summary>
        /// 旧件收货单的Id值
        /// </summary>
        string strPartsId = string.Empty;
        /// <summary>
        /// 父窗体
        /// </summary>
        public UCOldPartsReceiptManager uc;
        /// <summary>
        /// 审核窗体
        /// </summary>
        UCVerify verify;
        /// <summary>
        /// 单据状态
        /// </summary>
        string strStatus = string.Empty;
        #endregion

        #region 初始化窗体
        public UCOldPartsReceiptView(string strPId)
        {
            InitializeComponent();
            strPartsId = strPId;
            SetTopbuttonShow();
            base.AddEvent += new ClickHandler(UCOldPartsReceiptView_AddEvent);
            base.CopyEvent += new ClickHandler(UCOldPartsReceiptView_CopyEvent);
            base.DeleteEvent += new ClickHandler(UCOldPartsReceiptView_DeleteEvent);
            base.VerifyEvent += new ClickHandler(UCOldPartsReceiptView_VerifyEvent);
            base.SubmitEvent += new ClickHandler(UCOldPartsReceiptView_SubmitEvent);
            base.EditEvent += new ClickHandler(UCOldPartsReceiptView_EditEvent);
            base.InvalidOrActivationEvent += new ClickHandler(UCOldPartsReceiptView_InvalidOrActivationEvent);
        }       
        #endregion

        #region 作废激活事件
        void UCOldPartsReceiptView_InvalidOrActivationEvent(object sender, EventArgs e)
        {
            string strmsg = string.Empty;
            List<SQLObj> listSql = new List<SQLObj>();
            SQLObj obj = new SQLObj();
            obj.cmdType = CommandType.Text;
            Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
            dicParam.Add("oldpart_id", new ParamObj("oldpart_id", strPartsId, SysDbType.VarChar, 40));//单据ID
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
                DataTable dvt = DBHelper.GetTable("获得前一个状态", "tb_maintain_oldpart_receiv_send_BackUp", "info_status", "oldpart_id='" + strPartsId + "'", "", "order by update_time desc");
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
            obj.sqlString = "update tb_maintain_oldpart_receiv_send set info_status=@info_status,update_by=@update_by,update_name=@update_name,update_time=@update_time where oldpart_id=@oldpart_id";
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
                deleteMenuByTag(this.Tag.ToString(), uc.Name);
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
            base.btnSave.Visible = false;
            base.btnCancel.Visible = false;
            base.btnImport.Visible = false;
            base.btnSync.Visible = false;
            base.btnConfirm.Visible = false;
            base.btnStatus.Visible = false;
            base.btnAdd.Visible = false;
        }
        #endregion

        #region 编辑事件
        void UCOldPartsReceiptView_EditEvent(object sender, EventArgs e)
        {
            UCOldPartsReceiptAddOrEdit PartsEdit = new UCOldPartsReceiptAddOrEdit();
            PartsEdit.wStatus = WindowStatus.Edit;
            PartsEdit.uc = uc;
            PartsEdit.strId = strPartsId;  //编辑单据的Id值
            base.addUserControl(PartsEdit, "救援单-编辑", "PartsEdit" + PartsEdit.strId, this.Tag.ToString(), this.Name);
            deleteMenuByTag(this.Tag.ToString(), PartsEdit.Name);
        }
        #endregion

        #region 提交事件
        void UCOldPartsReceiptView_SubmitEvent(object sender, EventArgs e)
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
                labMaterialNoS.Text = CommonUtility.GetNewNo(SYSModel.DataSources.EnumProjectType.PartsReceipt);
            }
            dicParam.Add("receipts_no", new ParamObj("receipts_no", labMaterialNoS.Text, SysDbType.VarChar, 40));//单据编号                   
            dicParam.Add("oldpart_id", new ParamObj("oldpart_id", strPartsId, SysDbType.VarChar, 40));//单据ID
            dicParam.Add("info_status", new ParamObj("info_status", DataSources.EnumAuditStatus.SUBMIT, SysDbType.VarChar, 40));//单据状态
            dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
            dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
            dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
            obj.sqlString = "update tb_maintain_oldpart_receiv_send set info_status=@info_status,receipts_no=@receipts_no,update_by=@update_by,update_name=@update_name,update_time=@update_time where oldpart_id=@oldpart_id";
            obj.Param = dicParam;
            listSql.Add(obj);
            if (DBHelper.BatchExeSQLMultiByTrans("更新单据状态为提交", listSql))
            {
                MessageBoxEx.Show("提交成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                uc.BindPageData();
                deleteMenuByTag(this.Tag.ToString(), uc.Name);
            }
        }
        #endregion

        #region 审核事件
        void UCOldPartsReceiptView_VerifyEvent(object sender, EventArgs e)
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
                dicParam.Add("oldpart_id", new ParamObj("oldpart_id", strPartsId, SysDbType.VarChar, 40));//单据ID
                dicParam.Add("info_status", new ParamObj("info_status", verify.auditStatus, SysDbType.VarChar, 40));//单据状态
                dicParam.Add("Verify_advice", new ParamObj("Verify_advice", verify.Content, SysDbType.VarChar, 200));//审核意见
                dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
                dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
                dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                obj.sqlString = "update tb_maintain_oldpart_receiv_send set info_status=@info_status,Verify_advice=@Verify_advice,update_by=@update_by,update_name=@update_name,update_time=@update_time where oldpart_id=@oldpart_id";
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
                    deleteMenuByTag(this.Tag.ToString(), uc.Name);
                }
            }
        }
        #endregion

        #region 删除事件
        void UCOldPartsReceiptView_DeleteEvent(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确认要删除吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            List<SQLObj> listSql = new List<SQLObj>();
            List<string> listField = new List<string>();
            listField.Add(strPartsId);
            Dictionary<string, string> comField = new Dictionary<string, string>();
            comField.Add("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.DELETED).ToString());
            comField.Add("update_by", HXCPcClient.GlobalStaticObj.UserID);//修改人Id
            comField.Add("update_name", HXCPcClient.GlobalStaticObj.UserName);//修改人姓名
            comField.Add("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString());//修改时间  
            bool flag = DBHelper.BatchUpdateDataByIn("删除旧件收货单", "tb_maintain_oldpart_receiv_send", comField, "oldpart_id", listField.ToArray());
            if (flag)
            {
                MessageBoxEx.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                uc.BindPageData();
                deleteMenuByTag(this.Tag.ToString(), uc.Name);
            }
            else
            {
                MessageBoxEx.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 复制事件
        void UCOldPartsReceiptView_CopyEvent(object sender, EventArgs e)
        {
            UCOldPartsReceiptAddOrEdit PartsCopy = new UCOldPartsReceiptAddOrEdit();
            PartsCopy.wStatus = WindowStatus.Copy;
            PartsCopy.uc = uc;
            PartsCopy.strId = strPartsId;  //编辑单据的Id值
            base.addUserControl(PartsCopy, "旧件收货单-复制", "PartsCopy" + PartsCopy.strId, this.Tag.ToString(), this.Name);
            deleteMenuByTag(this.Tag.ToString(), PartsCopy.Name);
        }
        #endregion

        #region 新增事件
        void UCOldPartsReceiptView_AddEvent(object sender, EventArgs e)
        {
            UCOldPartsReceiptAddOrEdit PartsAdd = new UCOldPartsReceiptAddOrEdit();          
            PartsAdd.wStatus = WindowStatus.Add;
            PartsAdd.uc = uc;
            base.addUserControl(PartsAdd, "旧件收货单-新增", "PartsAdd" + PartsAdd.strId, this.Tag.ToString(), this.Name);
            deleteMenuByTag(this.Tag.ToString(), PartsAdd.Name);
        }
        #endregion

        #region 窗体Load事件
        private void UCOldPartsReceiptView_Load(object sender, EventArgs e)
        {
            GetRescueData(strPartsId);
        }
        #endregion

        #region 根据旧件收货单Id获取相应的详细信息
        /// <summary>
        /// 根据旧件收货单Id获取相应的详细信息
        /// </summary>
        /// <param name="strRId">旧件收货单Id值</param>
        private void GetRescueData(string strRId)
        {
            #region 基本信息
            //SetBtnStatus(WindowStatus.View);
            DataTable dt = DBHelper.GetTable("旧件收货单预览", "tb_maintain_oldpart_receiv_send", "*", string.Format(" oldpart_id='{0}'", strRId), "", "");
            if (dt.Rows.Count > 0)
            {
                #region 维修表信息
                DataRow dr = dt.Rows[0];
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["receipts_no"])))
                {
                    labMaterialNoS.Text = CommonCtrl.IsNullToString(dr["receipts_no"]);//收货单号 
                }
                else
                {
                    labMaterialNoS.Text = string.Empty;
                }           
                labCustomNOS.Text = CommonCtrl.IsNullToString(dr["customer_code"]);//客户编码
                labCustomNameS.Text = CommonCtrl.IsNullToString(dr["customer_name"]);//客户名称
                labContactS.Text = CommonCtrl.IsNullToString(dr["linkman"]);//联系人
                labContactPhoneS.Text = CommonCtrl.IsNullToString(dr["link_man_mobile"]);//联系人电话
                string strRTime = CommonCtrl.IsNullToString(dr["receipt_time"]); //收货日期
                if (!string.IsNullOrEmpty(strRTime))
                {
                    labReceiptTimeS.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(strRTime)).ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    labReceiptTimeS.Text = string.Empty;
                }
                labRemarkS.Text = CommonCtrl.IsNullToString(dr["remarks"]); //备注
                labStatusS.Text = DataSources.GetDescription(typeof(DataSources.EnumAuditStatus), int.Parse(CommonCtrl.IsNullToString(dr["info_status"])));//单据状态              
                labDepartS.Text = GetDepartmentName(CommonCtrl.IsNullToString(dr["org_name"]));//部门
                labAttnS.Text = GetUserSetName(CommonCtrl.IsNullToString(dr["responsible_name"]));//经办人
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
                DataTable dpt = DBHelper.GetTable("旧件收货单详情", "tb_maintain_oldpart_material_detail", "*", string.Format(" maintain_id='{0}' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", strRId), "", "");
                dgvMaterials.DataSource = dpt;
            #endregion
            }
            else
            {
                #region 没有数据时全部显示为空 
                labContactPhoneS.Text = string.Empty;
                labContactS.Text = string.Empty;
                labCreatePersonS.Text = string.Empty;
                labCreateTimeS.Text = string.Empty;
                labCustomNameS.Text = string.Empty;
                labCustomNOS.Text = string.Empty;
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

        #region 获得经办人名称
        private string GetUserSetName(string strPid)
        {
            return DBHelper.GetSingleValue("获得人员名称", "sys_user", "user_name", "user_id='" + strPid + "'", "");
        }
        #endregion

        #region 重写是否进口、车型的值
        private void dgvMaterials_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
            string fieldNmae = dgvMaterials.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("whether_imported"))
            {
                e.Value =!string.IsNullOrEmpty(CommonCtrl.IsNullToString(e.Value))?e.Value.ToString()=="1"?"是":"否":"";
            }
            if (fieldNmae.Equals("vehicle_brand"))
            {
                e.Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(e.Value))?GetDicName(e.Value.ToString()):"";
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

    }
}
