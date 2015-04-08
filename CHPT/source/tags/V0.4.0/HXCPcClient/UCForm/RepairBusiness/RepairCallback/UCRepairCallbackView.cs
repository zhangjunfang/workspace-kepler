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

namespace HXCPcClient.UCForm.RepairBusiness.RepairCallback
{
    /// <summary>
    /// 维修管理-维修返修单预览
    /// Author：JC
    /// AddTime：2014.10.24
    /// </summary>
    public partial class UCRepairCallbackView : UCBase
    {
        #region 属性设置
        /// <summary>
        /// 维修结算单的Id值
        /// </summary>
        string strCallbackId = string.Empty;
        /// <summary>
        /// 父窗体
        /// </summary>
        public UCRepairCallbackManager uc;
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
        public UCRepairCallbackView(string strCallId)
        {
            InitializeComponent();
            strCallbackId = strCallId;
            SetDgvAnchor();
            SetTopbuttonShow();
            base.AddEvent += new ClickHandler(UCRepairCallbackView_AddEvent);
            base.CopyEvent += new ClickHandler(UCRepairCallbackView_CopyEvent);
            base.EditEvent += new ClickHandler(UCRepairCallbackView_EditEvent);
            base.DeleteEvent += new ClickHandler(UCRepairCallbackView_DeleteEvent);
            base.VerifyEvent += new ClickHandler(UCRepairCallbackView_VerifyEvent);
            base.SubmitEvent += new ClickHandler(UCRepairCallbackView_SubmitEvent);
            base.InvalidOrActivationEvent += new ClickHandler(UCRepairCallbackView_InvalidOrActivationEvent);
        }
        #endregion

        #region 作废激活状态
        void UCRepairCallbackView_InvalidOrActivationEvent(object sender, EventArgs e)
        {
            string strmsg = string.Empty;
            List<SQLObj> listSql = new List<SQLObj>();
            SQLObj obj = new SQLObj();
            obj.cmdType = CommandType.Text;
            Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
            dicParam.Add("repair_id", new ParamObj("repair_id", strCallbackId, SysDbType.VarChar, 40));//单据ID
            dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
            dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
            dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
            if (strStatus != Convert.ToInt32(DataSources.EnumAuditStatus.Invalid).ToString())
            {
                strmsg = "作废";
                dicParam.Add("document_status", new ParamObj("document_status", DataSources.EnumAuditStatus.Invalid, SysDbType.VarChar, 40));//单据状态
            }
            else
            {
                strmsg = "激活";
                string OnStatus = "";
                DataTable dvt = DBHelper.GetTable("获得前一个状态", "tb_maintain_back_repair_BackUp", "document_status", "repair_id='" + strCallbackId + "'", "", "order by update_time desc");
                if (dvt.Rows.Count > 0)
                {
                    DataRow dr = dvt.Rows[0];
                    OnStatus = CommonCtrl.IsNullToString(dr["document_status"]);
                    if (OnStatus == Convert.ToInt32(DataSources.EnumAuditStatus.Invalid).ToString())
                    {
                        DataRow dr1 = dvt.Rows[1];
                        OnStatus = CommonCtrl.IsNullToString(dr1["document_status"]);
                    }

                }
                OnStatus = !string.IsNullOrEmpty(OnStatus) ? OnStatus : Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString();
                dicParam.Add("document_status", new ParamObj("document_status", OnStatus, SysDbType.VarChar, 40));//单据状态
            }
            obj.sqlString = "update tb_maintain_back_repair set document_status=@document_status,update_by=@update_by,update_name=@update_name,update_time=@update_time where repair_id=@repair_id";
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
                deleteMenuByTag(this.Tag.ToString(), "UCReceiveView");
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

        #region 提交事件
        void UCRepairCallbackView_SubmitEvent(object sender, EventArgs e)
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
                labMaintain_noS.Text = CommonUtility.GetNewNo(SYSModel.DataSources.EnumProjectType.RepairCallback);
            }
            dicParam.Add("repair_no", new ParamObj("repair_no", labMaintain_noS.Text, SysDbType.VarChar, 40));//单据编号                   
            dicParam.Add("repair_id", new ParamObj("repair_id", strCallbackId, SysDbType.VarChar, 40));//单据ID
            dicParam.Add("document_status", new ParamObj("document_status", DataSources.EnumAuditStatus.SUBMIT, SysDbType.VarChar, 40));//单据状态
            dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
            dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
            dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
            obj.sqlString = "update tb_maintain_back_repair set document_status=@document_status,repair_no=@repair_no,update_by=@update_by,update_name=@update_name,update_time=@update_time where repair_id=@repair_id";
            obj.Param = dicParam;
            listSql.Add(obj);
            if (DBHelper.BatchExeSQLMultiByTrans("更新单据状态为提交", listSql))
            {
                MessageBoxEx.Show("提交成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                uc.BindPageData();
                deleteMenuByTag(this.Tag.ToString(), "UCRepairBalanceView");
            }
        }
        #endregion

        #region 审核事件
        void UCRepairCallbackView_VerifyEvent(object sender, EventArgs e)
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
                dicParam.Add("repair_id", new ParamObj("repair_id", strCallbackId, SysDbType.VarChar, 40));//单据ID
                dicParam.Add("document_status", new ParamObj("document_status", verify.auditStatus, SysDbType.VarChar, 40));//单据状态
                dicParam.Add("Verify_advice", new ParamObj("Verify_advice", verify.Content, SysDbType.VarChar, 200));//审核意见
                dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
                dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
                dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                obj.sqlString = "update tb_maintain_back_repair set document_status=@document_status,Verify_advice=@Verify_advice,update_by=@update_by,update_name=@update_name,update_time=@update_time where repair_id=@repair_id";
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
                    deleteMenuByTag(this.Tag.ToString(), "UCRepairBalanceView");
                }
            }
        }
        #endregion

        #region 删除事件
        void UCRepairCallbackView_DeleteEvent(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确认要删除吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            List<SQLObj> listSql = new List<SQLObj>();
            List<string> listField = new List<string>();
            listField.Add(strCallbackId);
            Dictionary<string, string> comField = new Dictionary<string, string>();
            comField.Add("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.DELETED).ToString());
            comField.Add("update_by", HXCPcClient.GlobalStaticObj.UserID);//修改人Id
            comField.Add("update_name", HXCPcClient.GlobalStaticObj.UserName);//修改人姓名
            comField.Add("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString());//修改时间  
            bool flag = DBHelper.BatchUpdateDataByIn("删除维修返修单", "tb_maintain_back_repair", comField, "repair_id", listField.ToArray());
            if (flag)
            {
                MessageBoxEx.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                uc.BindPageData();
                deleteMenuByTag(this.Tag.ToString(), "UCRepairCallbackView");
            }
            else
            {
                MessageBoxEx.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 编辑事件
        void UCRepairCallbackView_EditEvent(object sender, EventArgs e)
        {
            UCRepairCallbackAddOrEdit CallbackEdit = new UCRepairCallbackAddOrEdit();
            CallbackEdit.wStatus = WindowStatus.Edit;
            CallbackEdit.uc = uc;
            CallbackEdit.strId = strCallbackId;  //编辑单据的Id值
            base.addUserControl(CallbackEdit, "维修返修单-编辑", "CallbackEdit" + CallbackEdit.strId, this.Tag.ToString(), this.Name);
            deleteMenuByTag(this.Tag.ToString(), "UCRepairCallbackView");
        }
        #endregion

        #region 复制事件
        void UCRepairCallbackView_CopyEvent(object sender, EventArgs e)
        {
            UCRepairCallbackAddOrEdit CallbackCopy = new UCRepairCallbackAddOrEdit();
            CallbackCopy.wStatus = WindowStatus.Copy;
            CallbackCopy.uc = uc;
            CallbackCopy.strId = strCallbackId;  //复制单据的Id值
            base.addUserControl(CallbackCopy, "维修返修单-复制", "CallbackCopy" + CallbackCopy.strId, this.Tag.ToString(), this.Name);
            deleteMenuByTag(this.Tag.ToString(), "UCRepairCallbackView");
        }
        #endregion

        #region 新增事件
        void UCRepairCallbackView_AddEvent(object sender, EventArgs e)
        {
            UCRepairCallbackAddOrEdit CallbackAdd = new UCRepairCallbackAddOrEdit();
            CallbackAdd.wStatus = WindowStatus.Add;
            CallbackAdd.uc = uc;
            base.addUserControl(CallbackAdd, "维修返修单-新增", "CallbackAdd" + CallbackAdd.strId, this.Tag.ToString(), this.Name);
            deleteMenuByTag(this.Tag.ToString(), "UCRepairCallbackView");
        }
        #endregion

        #region 获取车型名称
        private string GetVmodel(string strVId)
        {
            return DBHelper.GetSingleValue("获得车型名称", "tb_vehicle_models", "vm_name", "vm_id='" + strVId + "'", "");
        }
        #endregion

        #region 根据预约单Id获取相应的详细信息
        /// <summary>
        /// 根据预约单Id获取相应的详细信息
        /// </summary>
        /// <param name="strRId">预约单reserv_id值</param>
        private void GetReservData(string strRId)
        {
            #region 基本信息
            //SetBtnStatus(WindowStatus.View);
            DataTable dt = DBHelper.GetTable("维修返修单预览", "tb_maintain_back_repair", "*", string.Format(" repair_id='{0}'", strRId), "", "");
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["repair_no"])))
                {
                    labMaintain_noS.Text = CommonCtrl.IsNullToString(dr["repair_no"]);//返修单号
                }
                else
                {
                    labMaintain_noS.Text = string.Empty;
                }
                string strReTime = CommonCtrl.IsNullToString(dr["reception_time"]);//接待日期
                if (!string.IsNullOrEmpty(strReTime))
                {
                    labReceptionTimeS.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(strReTime)).ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    labReceptionTimeS.Text = string.Empty;
                }     
                labCustomNOS.Text = CommonCtrl.IsNullToString(dr["customer_code"]);//客户编码
                labCustomNameS.Text = CommonCtrl.IsNullToString(dr["customer_name"]);//客户名称
                labCarNOS.Text = CommonCtrl.IsNullToString(dr["vehicle_no"]);//车牌号
                labCarTypeS.Text = GetVmodel(CommonCtrl.IsNullToString(dr["vehicle_model"]));//车型
                labCarBrandS.Text = GetDicName(CommonCtrl.IsNullToString(dr["vehicle_brand"]));//车辆品牌
                labVINS.Text = CommonCtrl.IsNullToString(dr["vehicle_vin"]);//VIN
                labEngineNoS.Text = CommonCtrl.IsNullToString(dr["engine_type"]);//发动机号
                labColorS.Text = GetDicName(CommonCtrl.IsNullToString(dr["vehicle_color"]));//颜色              
                labContactS.Text = CommonCtrl.IsNullToString(dr["linkman"]);//联系人
                labContactPhoneS.Text = CommonCtrl.IsNullToString(dr["linkman_mobile"]);//联系人手机               
                labOrderStatusS.Text = DataSources.GetDescription(typeof(DataSources.EnumAuditStatus), int.Parse(CommonCtrl.IsNullToString(dr["document_status"])));//单据状态               
                labBlamePersonS.Text = CommonCtrl.IsNullToString(dr["repairer_name"]);//返修负责人               
                labMilS.Text =!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["mileage"]))?CommonCtrl.IsNullToString(dr["mileage"])+"Km":"";//行驶里程               
                labSendPersonS.Text = CommonCtrl.IsNullToString(dr["driver_name"]);//报修人
                labSendPhoneS.Text = CommonCtrl.IsNullToString(dr["driver_mobile"]);//报修人手机
                labRepairDescS.Text = CommonCtrl.IsNullToString(dr["repair_describe"]);//故障原因
                labOpinionS.Text = CommonCtrl.IsNullToString(dr["dispose_opinion"]);//处理意见
                labResultS.Text = CommonCtrl.IsNullToString(dr["dispose_result"]);//处理结果
                labDepartS.Text = GetDepartmentName(CommonCtrl.IsNullToString(dr["org_name"]));//部门
                labAttnS.Text = GetUserSetName(CommonCtrl.IsNullToString(dr["responsible_opid"]));//经办人
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


                string strStatus = CommonCtrl.IsNullToString(dr["document_status"]);//单据状态
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

            #region 底部datagridview数据

                #region 维修项目数据
                //维修项目数据                
                DataTable dpt = DBHelper.GetTable("维修项目数据", "tb_maintain_item", "*", string.Format(" maintain_id='{0}' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", strRId), "", "");
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
                        dgvproject.Rows[i].Cells["man_hour_quantity"].Value = ControlsConfig.SetNewValue(CommonCtrl.IsNullToString(dpr["man_hour_quantity"]),1);
                        dgvproject.Rows[i].Cells["man_hour_norm_unitprice"].Value = ControlsConfig.SetNewValue(CommonCtrl.IsNullToString(dpr["man_hour_norm_unitprice"]),2);
                        dgvproject.Rows[i].Cells["remarks"].Value = CommonCtrl.IsNullToString(dpr["remarks"]);
                        dgvproject.Rows[i].Cells["sum_money_goods"].Value = ControlsConfig.SetNewValue(CommonCtrl.IsNullToString(dpr["sum_money_goods"]),2);

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
                        dgvMaterials.Rows[i].Cells["quantity"].Value = ControlsConfig.SetNewValue(CommonCtrl.IsNullToString(dmr["quantity"]),1);
                        dgvMaterials.Rows[i].Cells["unit_price"].Value =  ControlsConfig.SetNewValue(CommonCtrl.IsNullToString(dmr["unit_price"]),2);
                        dgvMaterials.Rows[i].Cells["sum_money"].Value =  ControlsConfig.SetNewValue(CommonCtrl.IsNullToString(dmr["sum_money"]),2);
                        dgvMaterials.Rows[i].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dmr["drawn_no"]);
                        //dgvMaterials.Rows[i].Cells["vehicle_model"].Value = CommonCtrl.IsNullToString(dmr["vehicle_model"]);
                        dgvMaterials.Rows[i].Cells["Mthree_warranty"].Value = CommonCtrl.IsNullToString(dmr["three_warranty"]) == "1" ? "是" : "否";
                        dgvMaterials.Rows[i].Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dmr["remarks"]);
                        dgvMaterials.Rows[i].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dmr["whether_imported"]) == "1" ? "是" : "否";

                    }
                }
                #endregion

                #region 其他项目收费数据
                ////其他项目收费数据
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
                        dgvOther.Rows[i].Cells["Osum_money"].Value =  ControlsConfig.SetNewValue(CommonCtrl.IsNullToString(dor["sum_money"]),2);
                        dgvOther.Rows[i].Cells["Oremarks"].Value = CommonCtrl.IsNullToString(dor["remarks"]);
                        dgvOther.Rows[i].Cells["cost_types"].Value = GetDicName(CommonCtrl.IsNullToString(dor["cost_types"]));
                    }
                }
                #endregion
                //附件信息数据
                ucAttr.TableName = "tb_maintain_back_repair";
                ucAttr.TableNameKeyValue = strRId; 
                ucAttr.BindAttachment();
                #endregion
            }
            else
            {
                #region
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
                labEngineNoS.Text = string.Empty;
                labFinallyPerS.Text = string.Empty;
                labFinallyTimeS.Text = string.Empty;               
                labVINS.Text = string.Empty;
                labBlamePersonS.Text = string.Empty;//返修负责人               
                labMilS.Text = string.Empty;//进站里程               
                labSendPersonS.Text = string.Empty;//送车人
                labSendPhoneS.Text = string.Empty;//送车人手机
                labRepairDescS.Text = string.Empty;//故障原因
                labOpinionS.Text = string.Empty;//处理意见
                labResultS.Text = string.Empty;//处理结果
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
            dgvMaterials.Dock = DockStyle.Fill;
            //dgvMaterials.Columns["MCheck"].HeaderText = "选择";
            dgvOther.Dock = DockStyle.Fill;
            //dgvOther.Columns["OCheck"].HeaderText = "选择";
            dgvproject.Dock = DockStyle.Fill;
            //dgvproject.Columns["colCheck"].HeaderText = "选择";
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
        private void UCRepairCallbackView_Load(object sender, EventArgs e)
        {
            GetReservData(strCallbackId);
            ucAttr.ReadOnly = true;
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
    }
}
