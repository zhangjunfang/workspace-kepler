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
using ServiceStationClient.ComponentUI;

namespace HXCPcClient.UCForm.RepairBusiness.RepairRescue
{
    /// <summary>
    /// 维修管理-救援单单预览
    /// Author：JC
    /// AddTime：2014.10.27
    /// </summary>
    public partial class UCRepairRescueView : UCBase
    {
        #region 属性设置
        /// <summary>
        /// 救援单的Id值
        /// </summary>
        string strRescueId = string.Empty;
        /// <summary>
        /// 父窗体
        /// </summary>
        public UCRepairRescueManager uc;
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
        public UCRepairRescueView(string strId)
        {
            InitializeComponent();
            strRescueId = strId;
            SetDgvAnchor();
            SetTopbuttonShow();
            base.BalanceEvent += new ClickHandler(UCRepairRescueView_BalanceEvent);
            base.VerifyEvent += new ClickHandler(UCRepairRescueView_VerifyEvent);
            base.DeleteEvent += new ClickHandler(UCRepairRescueView_DeleteEvent);
            base.EditEvent += new ClickHandler(UCRepairRescueView_EditEvent);
            base.CopyEvent += new ClickHandler(UCRepairRescueView_CopyEvent);
            base.AddEvent += new ClickHandler(UCRepairRescueView_AddEvent);
            base.InvalidOrActivationEvent += new ClickHandler(UCRepairRescueView_InvalidOrActivationEvent);
        }       
        #endregion

        #region 作废激活事件
        void UCRepairRescueView_InvalidOrActivationEvent(object sender, EventArgs e)
        {

            string strmsg = string.Empty;
            List<SQLObj> listSql = new List<SQLObj>();
            SQLObj obj = new SQLObj();
            obj.cmdType = CommandType.Text;
            Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
            dicParam.Add("rescue_id", new ParamObj("rescue_id", strRescueId, SysDbType.VarChar, 40));//单据ID
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
                DataTable dvt = DBHelper.GetTable("获得前一个状态", "tb_maintain_rescue_info_BackUp", "document_status", "rescue_id='" + strRescueId + "'", "", "order by update_time desc");
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
            obj.sqlString = "update tb_maintain_rescue_info set document_status=@document_status,update_by=@update_by,update_name=@update_name,update_time=@update_time where rescue_id=@rescue_id";
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
            base.btnBalance.Visible = true;
            base.btnSubmit.Visible = false;
            base.btnStatus.Visible = false;
            base.btnAdd.Visible = false;
        }
        #endregion

        #region 新增事件
        void UCRepairRescueView_AddEvent(object sender, EventArgs e)
        {
            UCRepairRescueAddOrEdit RescueAdd = new UCRepairRescueAddOrEdit();
            RescueAdd.wStatus = WindowStatus.Add;
            RescueAdd.uc = uc;
            base.addUserControl(RescueAdd, "救援单-新增", "RescueAdd" + RescueAdd.strId, this.Tag.ToString(), this.Name);
            deleteMenuByTag(this.Tag.ToString(), RescueAdd.Name);
        }
        #endregion

        #region 复制事件
        void UCRepairRescueView_CopyEvent(object sender, EventArgs e)
        {
            UCRepairRescueAddOrEdit RescueCopy = new UCRepairRescueAddOrEdit();
            RescueCopy.wStatus = WindowStatus.Copy;
            RescueCopy.uc = uc;
            RescueCopy.strId = strRescueId;  //编辑单据的Id值
            base.addUserControl(RescueCopy, "救援单-复制", "RescueCopy" + RescueCopy.strId, this.Tag.ToString(), this.Name);
            deleteMenuByTag(this.Tag.ToString(), RescueCopy.Name);
        }
        #endregion

        #region 编辑事件
        void UCRepairRescueView_EditEvent(object sender, EventArgs e)
        {
            UCRepairRescueAddOrEdit RescueEdit = new UCRepairRescueAddOrEdit();
            RescueEdit.wStatus = WindowStatus.Edit;
            RescueEdit.uc = uc;
            RescueEdit.strId = strRescueId;  //编辑单据的Id值
            base.addUserControl(RescueEdit, "救援单-编辑", "RescueEdit" + RescueEdit.strId, this.Tag.ToString(), this.Name);
            deleteMenuByTag(this.Tag.ToString(), RescueEdit.Name);
        }
        #endregion

        #region 删除事件
        void UCRepairRescueView_DeleteEvent(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确认要删除吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            List<SQLObj> listSql = new List<SQLObj>();
            List<string> listField = new List<string>();
            listField.Add(strRescueId);
            Dictionary<string, string> comField = new Dictionary<string, string>();
            comField.Add("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.DELETED).ToString());
            comField.Add("update_by", HXCPcClient.GlobalStaticObj.UserID);//修改人Id
            comField.Add("update_name", HXCPcClient.GlobalStaticObj.UserName);//修改人姓名
            comField.Add("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString());//修改时间  
            bool flag = DBHelper.BatchUpdateDataByIn("删除维修结算单", "tb_maintain_rescue_info", comField, "rescue_id", listField.ToArray());
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

        #region 审核事件
        void UCRepairRescueView_VerifyEvent(object sender, EventArgs e)
        {
            //if (MessageBoxEx.Show("确认要审核吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            //{
            //    return;
            //}
            verify = new UCVerify();
            if (verify.ShowDialog() == DialogResult.OK)
            {
                List<SQLObj> listSql = new List<SQLObj>();
                SQLObj obj = new SQLObj();
                obj.cmdType = CommandType.Text;
                Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                dicParam.Add("rescue_id", new ParamObj("rescue_id", strRescueId, SysDbType.VarChar, 40));//单据ID
                dicParam.Add("document_status", new ParamObj("document_status", verify.auditStatus, SysDbType.VarChar, 40));//单据状态
                dicParam.Add("Verify_advice", new ParamObj("Verify_advice", verify.Content, SysDbType.VarChar, 200));//审核意见
                dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
                dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
                dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                obj.sqlString = "update tb_maintain_rescue_info set document_status=@document_status,Verify_advice=@Verify_advice,update_by=@update_by,update_name=@update_name,update_time=@update_time where rescue_id=@rescue_id";
                obj.Param = dicParam;
                listSql.Add(obj);
                if (DBHelper.BatchExeSQLMultiByTrans("更新单据状态为审核", listSql))
                {
                    MessageBoxEx.Show("审核成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    uc.BindPageData();
                    deleteMenuByTag(this.Tag.ToString(), uc.Name);
                }
            }
        }
        #endregion

        #region 结算事件
        void UCRepairRescueView_BalanceEvent(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确认要结算吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            List<SQLObj> listSql = new List<SQLObj>();
            SQLObj obj = new SQLObj();
            obj.cmdType = CommandType.Text;
            Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
            if (strStatus == Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString())
            {
                labMaintain_noS.Text = CommonUtility.GetNewNo(SYSModel.DataSources.EnumProjectType.Rescue);
            }
            dicParam.Add("rescue_no", new ParamObj("rescue_no", labMaintain_noS.Text, SysDbType.VarChar, 40));//单据编号                   
            dicParam.Add("rescue_id", new ParamObj("rescue_id", strRescueId, SysDbType.VarChar, 40));//单据ID
            dicParam.Add("document_status", new ParamObj("document_status", DataSources.EnumAuditStatus.Balance, SysDbType.VarChar, 40));//单据状态
            dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
            dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
            dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
            obj.sqlString = "update tb_maintain_rescue_info set document_status=@document_status,rescue_no=@rescue_no,update_by=@update_by,update_name=@update_name,update_time=@update_time where rescue_id=@rescue_id";
            obj.Param = dicParam;
            listSql.Add(obj);
            if (DBHelper.BatchExeSQLMultiByTrans("更新单据状态为结算", listSql))
            {
                MessageBoxEx.Show("结算成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                uc.BindPageData();
                deleteMenuByTag(this.Tag.ToString(), uc.Name);
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
            dgvRescuePerson.Dock = DockStyle.Fill;
            //dgvRescuePerson.Columns["RPcolCheck"].HeaderText = "选择";
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
        private void UCRepairRescueView_Load(object sender, EventArgs e)
        {
            GetRescueData(strRescueId);
            ucAttr.ReadOnly = true;
        }
        #endregion

        #region 获取车型名称
        private string GetVmodel(string strVId)
        {
            return DBHelper.GetSingleValue("获得车型名称", "tb_vehicle_models", "vm_name", "vm_id='" + strVId + "'", "");
        }
        #endregion


        #region 根据救援单Id获取相应的详细信息
        /// <summary>
        /// 根据救援单Id获取相应的详细信息
        /// </summary>
        /// <param name="strRId">救援单Id值</param>
        private void GetRescueData(string strRId)
        {
            #region 基本信息
            //SetBtnStatus(WindowStatus.View);
            DataTable dt = DBHelper.GetTable("救援单预览", "tb_maintain_rescue_info", "*", string.Format(" rescue_id='{0}'", strRId), "", "");
            if (dt.Rows.Count > 0)
            {
                #region 维修表信息
                DataRow dr = dt.Rows[0];
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["rescue_no"])))
                {
                    labMaintain_noS.Text = CommonCtrl.IsNullToString(dr["rescue_no"]);//救援单号   
                }
                else
                {
                    labMaintain_noS.Text = string.Empty;
                }
                
                string strMakeTime = CommonCtrl.IsNullToString(dr["make_time"]); //制单时间
                if (!string.IsNullOrEmpty(strMakeTime))
                {
                    labMakeOrderTimeS.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(strMakeTime)).ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    labMakeOrderTimeS.Text = string.Empty;
                }

                labCustomNOS.Text = CommonCtrl.IsNullToString(dr["customer_code"]);//客户编码
                labCustomNameS.Text = CommonCtrl.IsNullToString(dr["customer_name"]);//客户名称
                labContactS.Text = CommonCtrl.IsNullToString(dr["linkman"]);//联系人
                labContactPhoneS.Text = CommonCtrl.IsNullToString(dr["linkman_mobile"]);//联系人电话
                labCarNOS.Text = CommonCtrl.IsNullToString(dr["vehicle_no"]);//车牌号
                labCarTypeS.Text = GetVmodel(CommonCtrl.IsNullToString(dr["vehicle_model"]));//车型
                labCarBrandS.Text = GetDicName(CommonCtrl.IsNullToString(dr["vehicle_brand"]));//车辆品牌
                labVINS.Text = CommonCtrl.IsNullToString(dr["vehicle_vin"]);//VIN
                labturnerS.Text = CommonCtrl.IsNullToString(dr["turner"]);//车厂编码     
                labEngineNoS.Text = CommonCtrl.IsNullToString(dr["engine_type"]);//发动机号
                labDriverS.Text = CommonCtrl.IsNullToString(dr["driver_name"]);//司机
                labDriverPhoneS.Text = CommonCtrl.IsNullToString(dr["driver_mobile"]);//司机手机  
                labMilsS.Text = CommonCtrl.IsNullToString(dr["rescue_mileage"]);//救援里程 
                labStatusS.Text = DataSources.GetDescription(typeof(DataSources.EnumAuditStatus), int.Parse(CommonCtrl.IsNullToString(dr["document_status"])));//单据状态              
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

                strStatus = CommonCtrl.IsNullToString(dr["document_status"]);//单据状态
                if (strStatus == Convert.ToInt32(DataSources.EnumAuditStatus.Balance).ToString())
                {
                    //已结算状态屏蔽结算、编辑、删除按钮
                    base.btnBalance.Enabled = false;
                    base.btnEdit.Enabled = false;
                    base.btnDelete.Enabled = false;
                    base.btnActivation.Enabled = false;
                }
                else if (strStatus == Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString())
                {
                    //已审核时屏蔽结算、审核、编辑、删除按钮
                    base.btnBalance.Enabled = false;
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
                    base.btnBalance.Enabled = false;
                    base.btnVerify.Enabled = false;
                    base.btnEdit.Enabled = false;
                }
                #endregion

                #region 救援信息
                labGshkS.Text = ControlsConfig.SetNewValue(CommonCtrl.IsNullToString(dr["man_hour_sum_money"]),2);//工时货款
                labGsslS.Text = CommonCtrl.IsNullToString(dr["man_hour_tax_rate"]);//工时税率
                labGsseS.Text = ControlsConfig.SetNewValue(CommonCtrl.IsNullToString(dr["man_hour_tax_cost"]),2);//工时税额
                labGssjhjS.Text = ControlsConfig.SetNewValue(CommonCtrl.IsNullToString(dr["man_hour_sum"]),2);//工时价税合计
                labPjhkS.Text = ControlsConfig.SetNewValue(CommonCtrl.IsNullToString(dr["fitting_sum_money"]),2);//配件货款
                labPjslS.Text = CommonCtrl.IsNullToString(dr["fitting_tax_rate"]);//配件税率
                labPjseS.Text = ControlsConfig.SetNewValue(CommonCtrl.IsNullToString(dr["fitting_tax_cost"]),2);//配件税额
                labPjsjhjS.Text = ControlsConfig.SetNewValue(CommonCtrl.IsNullToString(dr["fitting_sum"]),2);//配件价税合计
                labQtflS.Text = ControlsConfig.SetNewValue(CommonCtrl.IsNullToString(dr["other_item_sum_money"]),2);//其他项目费用
                labQtslS.Text = ControlsConfig.SetNewValue(CommonCtrl.IsNullToString(dr["other_item_tax_rate"]),2);//其他项目税率
                labQtseS.Text = ControlsConfig.SetNewValue(CommonCtrl.IsNullToString(dr["other_item_tax_cost"]),2);//其他项目税额
                labQthjS.Text = ControlsConfig.SetNewValue(CommonCtrl.IsNullToString(dr["other_item_sum"]),2);//其他项目价税合计
                labYH.Text = ControlsConfig.SetNewValue(CommonCtrl.IsNullToString(dr["privilege_cost"]),2);//优惠费用
                labShould.Text = ControlsConfig.SetNewValue(CommonCtrl.IsNullToString(dr["should_sum"]),2);//应收总额
                labReceive.Text = ControlsConfig.SetNewValue(CommonCtrl.IsNullToString(dr["received_sum"]),2);//实收总额
                labDebt.Text = ControlsConfig.SetNewValue(CommonCtrl.IsNullToString(dr["debt_cost"]),2);//欠款金额
                labInvoiceType.Text = GetDicName(CommonCtrl.IsNullToString(dr["make_invoice_type"]));//开票类型
                labPayment.Text = GetPaymentName(CommonCtrl.IsNullToString(dr["payment_terms"]));//结算方式
                labSetAccount.Text = GetSetName(CommonCtrl.IsNullToString(dr["settlement_account"]));//结算账户
                labSetCompany.Text = GetComName(CommonCtrl.IsNullToString(dr["settle_company"]));//结算单位
                string strStartTime = CommonCtrl.IsNullToString(dr["depart_time"]);//出发时间
                if (!string.IsNullOrEmpty(strStartTime))
                {
                    labStartTimeS.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(strStartTime)).ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    labStartTimeS.Text = string.Empty;
                }
                string strEndTime = CommonCtrl.IsNullToString(dr["arrive_time"]);//到达时间
                if (!string.IsNullOrEmpty(strEndTime))
                {
                    labEndTimeS.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(strEndTime)).ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    labEndTimeS.Text = string.Empty;
                }
                string strReturnTime = CommonCtrl.IsNullToString(dr["back_time"]);//返回时间
                if (!string.IsNullOrEmpty(strReturnTime))
                {
                    labReturnTimeS.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(strReturnTime)).ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    labReturnTimeS.Text = string.Empty;
                }

                labDefaultDescS.Text = CommonCtrl.IsNullToString(dr["fault_describe"]);//故障描述
                labRemarkS.Text = CommonCtrl.IsNullToString(dr["remarks"]);//备注
                #endregion

            #endregion

                #region 底部datagridview数据

                #region 维修项目数据
                //维修项目数据     
                decimal dcPmoney = 0;
                DataTable dpt = DBHelper.GetTable("维修项目数据", "tb_maintain_item", "*", string.Format(" maintain_id='{0}' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", strRId), "", ""); ;
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
                        dgvproject.Rows[i].Cells["member_discount"].Value = CommonCtrl.IsNullToString(dpr["member_discount"]);
                        dgvproject.Rows[i].Cells["member_price"].Value = ControlsConfig.SetNewValue(CommonCtrl.IsNullToString(dpr["member_price"]),2);
                        dgvproject.Rows[i].Cells["member_sum_money"].Value = ControlsConfig.SetNewValue(CommonCtrl.IsNullToString(dpr["member_sum_money"]),2);

                    }
                    foreach (DataGridViewRow dgvr in dgvproject.Rows)
                    {
                        dcPmoney += Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["sum_money_goods"].Value)) ? dgvr.Cells["sum_money_goods"].Value : 0);
                    }

                }
                #endregion

                #region 维修用料数据
                //维修用料数据   
                decimal dcMmoney = 0;
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
                        dgvMaterials.Rows[i].Cells["unit_price"].Value = ControlsConfig.SetNewValue(CommonCtrl.IsNullToString(dmr["unit_price"]),2);
                        dgvMaterials.Rows[i].Cells["Mmember_discount"].Value = CommonCtrl.IsNullToString(dmr["member_discount"]);
                        dgvMaterials.Rows[i].Cells["Mmember_price"].Value = ControlsConfig.SetNewValue(CommonCtrl.IsNullToString(dmr["member_price"]),2);
                        dgvMaterials.Rows[i].Cells["sum_money"].Value = ControlsConfig.SetNewValue(CommonCtrl.IsNullToString(dmr["sum_money"]),2);
                        dgvMaterials.Rows[i].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dmr["drawn_no"]);
                        dgvMaterials.Rows[i].Cells["vehicle_brand"].Value = CommonCtrl.IsNullToString(dmr["vehicle_brand"]);
                        dgvMaterials.Rows[i].Cells["Mthree_warranty"].Value = CommonCtrl.IsNullToString(dmr["three_warranty"]) == "1" ? "是" : "否";
                        dgvMaterials.Rows[i].Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dmr["remarks"]);
                        dgvMaterials.Rows[i].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dmr["whether_imported"]) == "1" ? "是" : "否";

                    }
                    foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
                    {
                        dcMmoney += Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["sum_money"].Value)) ? dgvr.Cells["sum_money"].Value : 0);
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
                        dgvOther.Rows[i].Cells["Osum_money"].Value = ControlsConfig.SetNewValue(CommonCtrl.IsNullToString(dor["sum_money"]),2);
                        dgvOther.Rows[i].Cells["Oremarks"].Value = CommonCtrl.IsNullToString(dor["remarks"]);
                        dgvOther.Rows[i].Cells["cost_types"].Value =GetDicName(CommonCtrl.IsNullToString(dor["cost_types"]));
                    }                    
                }

                #endregion

                #region 救援人员信息
                DataTable drpt = DBHelper.GetTable("救援人员数据", "tb_maintain_rescue_worker", "*", string.Format(" maintain_id='{0}'", strRId), "", "");
                if (drpt.Rows.Count > 0)
                {
                    dgvRescuePerson.DataSource = drpt;
                }
                #endregion

                #region 附件信息数据
                //附件信息数据
                ucAttr.TableName = "tb_maintain_rescue_info";
                ucAttr.TableNameKeyValue = strRId;
                ucAttr.BindAttachment();
                #endregion
                #endregion
            }
            else
            {
                #region 没有数据时全部显示为空
                labMaintain_noS.Text = string.Empty;
                labAttnS.Text = string.Empty;
                labCarBrandS.Text = string.Empty;
                labCarNOS.Text = string.Empty;
                labCarTypeS.Text = string.Empty;
                labContactPhoneS.Text = string.Empty;
                labContactS.Text = string.Empty;
                labCreatePersonS.Text = string.Empty;
                labCreateTimeS.Text = string.Empty;
                labCustomNameS.Text = string.Empty;
                labCustomNOS.Text = string.Empty;
                labDepartS.Text = string.Empty;
                labDriverPhoneS.Text = string.Empty;
                labDriverS.Text = string.Empty;
                labEngineNoS.Text = string.Empty;
                labFinallyPerS.Text = string.Empty;
                labFinallyTimeS.Text = string.Empty;               
                labStatusS.Text = string.Empty;
                labVINS.Text = string.Empty;              
                labRescueTypeS.Text = string.Empty;
                labServerCarNoS.Text = string.Empty;
                labStartTimeS.Text = string.Empty;
                labEndTimeS.Text = string.Empty;
                labReturnTimeS.Text = string.Empty;
                labMilsS.Text = string.Empty;
                labRescuPlaceS.Text = string.Empty;
                labInvoiceType.Text = string.Empty;
                labSetAccount.Text = string.Empty;
                labSetCompany.Text = string.Empty;
                labDefaultDescS.Text = string.Empty;
                labRemarkS.Text = string.Empty;
                #endregion
            }
        }
        #endregion

        #region 获得结算方式的值
        private string GetPaymentName(string strPid)
        {
            return DBHelper.GetSingleValue("获取结算方式的名称", "tb_balance_way", "balance_way_name", "balance_way_id='" + strPid + "'", "");
        }
        #endregion

        #region 获得结算账户的值
        private string GetSetName(string strPid)
        {
            return DBHelper.GetSingleValue("获取结算结算账户的名称", "v_cashier_account", "account_name", "cashier_account='" + strPid + "'", "");
        }
        #endregion

        #region 获得结算结算单位的值
        private string GetComName(string strPid)
        {
            return DBHelper.GetSingleValue("获取结算结结算单位的名称", "tb_customer", "cust_name", "cust_id='" + strPid + "'", "");
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
