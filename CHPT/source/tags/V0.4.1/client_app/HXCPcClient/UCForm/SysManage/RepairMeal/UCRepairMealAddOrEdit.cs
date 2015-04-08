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
using Utility.Common;
using SYSModel;
using ServiceStationClient.ComponentUI;

namespace HXCPcClient.UCForm.SysManage.RepairMeal
{
    /// <summary>
    /// 系统管理—维修套餐设置新增、编辑
    /// Author：JC
    /// AddTime：2014.12.01
    /// </summary>
    public partial class UCRepairMealAddOrEdit : UCBase
    {
        #region 属性设置
        /// <summary>
        /// 父窗体
        /// </summary>
        public UCRepairMealManage uc;
        /// <summary>
        /// 窗体状态
        /// </summary>
        public WindowStatus wStatus;
        /// <summary>
        /// 维修套餐ID
        /// </summary>
        public string strId = string.Empty;       
        /// <summary>
        /// 单据状态
        /// </summary>
        string strStatus = string.Empty;
        /// <summary>
        /// 操作名称
        /// </summary>
        string opName = string.Empty;
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
        public UCRepairMealAddOrEdit()
        {
            InitializeComponent();
            SetTopbuttonShow();
            base.CancelEvent += new ClickHandler(UCRepairMealAddOrEdit_CancelEvent);
            base.SaveEvent += new ClickHandler(UCRepairMealAddOrEdit_SaveEvent);
            base.ViewEvent += new ClickHandler(UCRepairMealAddOrEdit_ViewEvent);
            base.StatusEvent += new ClickHandler(UCRepairMealAddOrEdit_StatusEvent);
            this.txtMealName.InnerTextBox.TextChanged+=new EventHandler(InnerTextBox_TextChanged);
        }
        #endregion

        #region --套餐编码
        protected void InnerTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.txtMealName.Caption.Length == 0)
            {
                this.txtMealCode.Caption = string.Empty;
                return;
            }           
            this.txtMealCode.Caption = ConvertToPy.GetSpellStringSupper(this.txtMealName.Caption);
        }
        #endregion

        #region 顶部button显示设置
        /// <summary>
        ///  顶部button显示设置
        /// </summary>
        private void SetTopbuttonShow()
        {
            base.btnAdd.Visible = false;
            base.btnEdit.Visible = false;
            base.btnCopy.Visible = false;
            base.btnDelete.Visible = false;
            base.btnImport.Visible = false;
            base.btnSync.Visible = false;
            base.btnConfirm.Visible = false;
            base.btnActivation.Visible = false;
            base.btnBalance.Visible = false;
            base.btnSubmit.Visible = false;          
            base.btnVerify.Visible = false;
            base.btnSet.Visible = false;
            base.btnPrint.Visible = false;
            base.btnRevoke.Visible = false;
            base.btnExport.Visible = false;
            base.btnStatus.Visible = false;
            base.btnView.Visible = false;

        }
        #endregion

        #region 启用停用事件
        void UCRepairMealAddOrEdit_StatusEvent(object sender, EventArgs e)
        {
            List<SQLObj> listSql = new List<SQLObj>();
            SQLObj obj = new SQLObj();
            obj.cmdType = CommandType.Text;
            Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();     
            string strMsg = string.Empty;          
            if (strStatus == Convert.ToInt32(DataSources.EnumStatus.Start).ToString())
            {
                strMsg = "停用";
                dicParam.Add("status", new ParamObj("status", DataSources.EnumStatus.Stop, SysDbType.VarChar, 40));//状态
            }
            else if (strStatus == Convert.ToInt32(DataSources.EnumStatus.Stop).ToString())
            {
                strMsg = "启用";
                dicParam.Add("status", new ParamObj("status", DataSources.EnumStatus.Start, SysDbType.VarChar, 40));//状态
            }
            if (MessageBoxEx.Show("确认要" + strMsg + "吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            dicParam.Add("repair_package_set_id", new ParamObj("repair_package_set_id",strId, SysDbType.VarChar, 40));//维修套餐Id 
            dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id          
            dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
            obj.sqlString = "update sys_b_set_repair_package_set set status=@status,update_by=@update_by,update_time=@update_time where repair_package_set_id=@repair_package_set_id";
            obj.Param = dicParam;
            listSql.Add(obj);
            if (DBHelper.BatchExeSQLMultiByTrans("更新单据状态为启停用", listSql))
            {
                MessageBoxEx.Show("" + strMsg + "成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                uc.BindPageData();
                deleteMenuByTag(this.Tag.ToString(), this.uc.Name);
            }
        }
        #endregion

        #region 预览事件
        void UCRepairMealAddOrEdit_ViewEvent(object sender, EventArgs e)
        {
            UCRepairMealView view = new UCRepairMealView(strId);           
            base.addUserControl(view, "维修套餐设置-预览", "UCRepairMealView" + strId, this.Tag.ToString(), this.Name);
        }
        #endregion

        #region 保存事件
        void UCRepairMealAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确认要保存吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            if (string.IsNullOrEmpty(txtMealName.Caption.Trim()))
            {
                MessageBoxEx.Show("套餐名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!ucAttr.CheckAttachment())
            {
                return;
            }
            List<SQLObj> listSql = new List<SQLObj>();
            SaveOrderInfo(listSql);
            SaveProjectData(listSql, strId);
            SaveMaterialsData(listSql, strId);
            SaveOtherData(listSql, strId);
            ucAttr.TableNameKeyValue = strId;
            listSql.AddRange(ucAttr.AttachmentSql);
            if (DBHelper.BatchExeSQLMultiByTrans(opName, listSql))
            {
                MessageBoxEx.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                uc.BindPageData();
                deleteMenuByTag(this.Tag.ToString(), this.uc.Name);
            }
            else
            {
                MessageBoxEx.Show("保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 维修套餐设置基本信息保存
        /// <summary>
        /// 维修套餐设置基本信息保存
        /// </summary>
        /// <param name="listSql">sql语句集合</param>
        /// <param name="status">操作方式</param>
        private void SaveOrderInfo(List<SQLObj> listSql)
        {
            SQLObj obj = new SQLObj();
            obj.cmdType = CommandType.Text;
            Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
            #region  基本信息
            dicParam.Add("period_validity", new ParamObj("period_validity", Common.LocalDateTimeToUtcLong(dtpSTime.Value).ToString(), SysDbType.BigInt));//有效期起
            dicParam.Add("valid_until", new ParamObj("valid_until", Common.LocalDateTimeToUtcLong(dtpETime.Value).ToString(), SysDbType.BigInt));//有效期止
            dicParam.Add("package_code", new ParamObj("package_code", txtMealCode.Caption.Trim(), SysDbType.VarChar, 20));//套餐编码
            dicParam.Add("package_name", new ParamObj("package_name", txtMealName.Caption.Trim(), SysDbType.VarChar, 40));//套餐名称
            dicParam.Add("remarks", new ParamObj("remarks", txtRemark.Caption.Trim(), SysDbType.VarChar, 200));//备注
            dicParam.Add("status", new ParamObj("status",Convert.ToInt32(DataSources.EnumStatus.Start).ToString(), SysDbType.VarChar, 40));//状态-启用
            dicParam.Add("enable_flag", new ParamObj("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString(), SysDbType.VarChar, 1));//信息状态（1|激活；2|作废；0|删除）           
            #endregion
            if (wStatus == WindowStatus.Add)
            {
                strId = Guid.NewGuid().ToString();
                dicParam.Add("repair_package_set_id", new ParamObj("repair_package_set_id", strId, SysDbType.VarChar, 40));//Id
                dicParam.Add("create_by", new ParamObj("create_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//创建人id（制单人）
                dicParam.Add("create_time", new ParamObj("create_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));
                obj.sqlString = @"insert into sys_b_set_repair_package_set (period_validity,valid_until,package_code,package_name,remarks,status,enable_flag,repair_package_set_id,create_by,create_time)
                 values (@period_validity,@valid_until,@package_code,@package_name,@remarks,@status,@enable_flag,@repair_package_set_id,@create_by,@create_time);";
            }
            else if (wStatus == WindowStatus.Edit)
            {
                dicParam.Add("repair_package_set_id", new ParamObj("repair_package_set_id", strId, SysDbType.VarChar, 40));//Id
                dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id               
                dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                obj.sqlString = @"update sys_b_set_repair_package_set set period_validity=@period_validity,valid_until=@valid_until,package_code=@package_code,package_name=@package_name,remarks=@remarks,status=@status,enable_flag=@enable_flag
                                   ,update_by=@update_by,update_time=@update_time where repair_package_set_id=@repair_package_set_id";
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
                string strPname = CommonCtrl.IsNullToString(dgvr.Cells["project_num"].Value);
                if (strPname.Length > 0)
                {
                    SQLObj obj = new SQLObj();
                    obj.cmdType = CommandType.Text;
                    Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                    dicParam.Add("repair_package_set_id", new ParamObj("repair_package_set_id", partID, SysDbType.VarChar, 40));
                    dicParam.Add("project_num", new ParamObj("project_num", dgvr.Cells["project_num"].Value, SysDbType.VarChar, 40));
                    dicParam.Add("project_name", new ParamObj("project_name", dgvr.Cells["project_name"].Value, SysDbType.VarChar, 40));
                    dicParam.Add("repair_type", new ParamObj("repair_type", dgvr.Cells["repair_type"].Value, SysDbType.VarChar, 40));
                    dicParam.Add("whours_type", new ParamObj("whours_type", dgvr.Cells["whours_type"].Value, SysDbType.VarChar, 40));
                    dicParam.Add("whours_counts", new ParamObj("whours_counts", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["whours_counts"].Value)) ? dgvr.Cells["whours_counts"].Value : null, SysDbType.Int));
                    dicParam.Add("whours_price", new ParamObj("whours_price", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["whours_price"].Value)) ? dgvr.Cells["whours_price"].Value : null, SysDbType.Decimal, 15));
                    dicParam.Add("tax_amount", new ParamObj("tax_amount", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["tax_amount"].Value)) ? dgvr.Cells["tax_amount"].Value : null, SysDbType.Decimal, 15));                    
                    dicParam.Add("remark", new ParamObj("remark", dgvr.Cells["remark"].Value, SysDbType.VarChar, 200));
                    dicParam.Add("enable_flag", new ParamObj("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString(), SysDbType.VarChar, 1));                 
                    string strPID = CommonCtrl.IsNullToString(dgvr.Cells["repair_package_set_project_id"].Value);
                    if (strPID.Length == 0)
                    {
                        opName = "新增维修套餐设置项目";
                        strPID = Guid.NewGuid().ToString();
                        dicParam.Add("repair_package_set_project_id", new ParamObj("repair_package_set_project_id", strPID, SysDbType.VarChar, 40));
                        dicParam.Add("create_by", new ParamObj("create_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//创建人id（制单人）
                        dicParam.Add("create_time", new ParamObj("create_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));
                        obj.sqlString = @"insert into sys_b_set_repair_package_set_project (repair_package_set_id,project_num,project_name
                        ,repair_type,whours_type,whours_counts,whours_price,tax_amount,remark,repair_package_set_project_id,create_by,create_time,enable_flag)
                        values (@repair_package_set_id,@project_num,@project_name
                        ,@repair_type,@whours_type,@whours_counts,@whours_price,@tax_amount,@remark,@repair_package_set_project_id,@create_by,@create_time,@enable_flag);";
                    }
                    else
                    {
                        dicParam.Add("repair_package_set_project_id", new ParamObj("repair_package_set_project_id", dgvr.Cells["repair_package_set_project_id"].Value, SysDbType.VarChar, 40));
                        opName = "更新维修套餐设置项目";
                        dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id               
                        dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                        obj.sqlString = @"update sys_b_set_repair_package_set_project set repair_package_set_id=@repair_package_set_id,project_num=@project_num,project_name=@project_name
                        ,repair_type=@repair_type,whours_type=@whours_type,whours_counts=@whours_counts,whours_price=@whours_price,tax_amount=@tax_amount,remark=@remark
                        ,update_by=@update_by,update_time=@update_time,enable_flag=@enable_flag where repair_package_set_project_id=@repair_package_set_project_id";
                    }
                    obj.Param = dicParam;
                    listSql.Add(obj);
                }
            }
        }
        #endregion

        #region 维修套餐设置用料信息保存
        private void SaveMaterialsData(List<SQLObj> listSql, string partID)
        {
            foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
            {
                string strPname = CommonCtrl.IsNullToString(dgvr.Cells["parts_name"].Value);               
                if (strPname.Length > 0)
                {
                    SQLObj obj = new SQLObj();
                    obj.cmdType = CommandType.Text;
                    Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                    dicParam.Add("repair_package_set_id", new ParamObj("repair_package_set_id", partID, SysDbType.VarChar, 40));
                    dicParam.Add("parts_code", new ParamObj("parts_code", dgvr.Cells["parts_code"].Value, SysDbType.VarChar, 40));
                    dicParam.Add("parts_name", new ParamObj("parts_name", dgvr.Cells["parts_name"].Value, SysDbType.VarChar, 40));                    
                    dicParam.Add("unit", new ParamObj("unit", dgvr.Cells["unit"].Value, SysDbType.VarChar, 20));
                    dicParam.Add("number", new ParamObj("number", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["number"].Value)) ? dgvr.Cells["number"].Value : null, SysDbType.Int));
                    dicParam.Add("original_price", new ParamObj("original_price", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["original_price"].Value)) ? dgvr.Cells["original_price"].Value : null, SysDbType.Decimal, 15));
                    dicParam.Add("preferential_price", new ParamObj("preferential_price", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["preferential_price"].Value)) ? dgvr.Cells["preferential_price"].Value : null, SysDbType.Decimal, 15));                   
                    dicParam.Add("remarks", new ParamObj("remarks", dgvr.Cells["Mremarks"].Value, SysDbType.VarChar, 200));
                    dicParam.Add("enable_flag", new ParamObj("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString(), SysDbType.VarChar, 1));                 
                    string strPID = CommonCtrl.IsNullToString(dgvr.Cells["repair_package_set_materials_id"].Value);
                    if (strPID.Length == 0)
                    {
                        opName = "新增维修套餐设置用料";
                        strPID = Guid.NewGuid().ToString();
                        dicParam.Add("repair_package_set_materials_id", new ParamObj("repair_package_set_materials_id", strPID, SysDbType.VarChar, 40));
                        dicParam.Add("create_by", new ParamObj("create_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//创建人id（制单人）
                        dicParam.Add("create_time", new ParamObj("create_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));
                        obj.sqlString = @"insert into sys_b_set_repair_package_set_materials (repair_package_set_id,parts_code,parts_name,unit
                        ,number,original_price,preferential_price,remarks,repair_package_set_materials_id,create_by,create_time,enable_flag)
                        values (@repair_package_set_id,@parts_code,@parts_name,@unit,@number,@original_price,@preferential_price
                        ,@remarks,@repair_package_set_materials_id,@create_by,@create_time,@enable_flag);";
                    }
                    else
                    {
                        dicParam.Add("repair_package_set_materials_id", new ParamObj("repair_package_set_materials_id", dgvr.Cells["repair_package_set_materials_id"].Value, SysDbType.VarChar, 40));
                        dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id               
                        dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                        opName = "更新维修套餐设置用料";
                        obj.sqlString = @"update sys_b_set_repair_package_set_materials set repair_package_set_id=@repair_package_set_id,parts_code=@parts_code,parts_name=@parts_name,unit=@unit
                        ,number=@number,original_price=@original_price,preferential_price=@preferential_price,remarks=@remarks,update_by=@update_by,update_time=@update_time,enable_flag=@enable_flag 
                        where repair_package_set_materials_id=@repair_package_set_materials_id";
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
                string strCotype = CommonCtrl.IsNullToString(dgvr.Cells["other_expense_type"].Value);
                if (strCotype.Length > 0)
                {
                    SQLObj obj = new SQLObj();
                    obj.cmdType = CommandType.Text;
                    Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                    dicParam.Add("repair_package_set_id", new ParamObj("repair_package_set_id", partID, SysDbType.VarChar, 40));
                    dicParam.Add("other_expense_type", new ParamObj("other_expense_type", dgvr.Cells["other_expense_type"].Value, SysDbType.VarChar, 40));
                    if (CommonCtrl.IsNullToString(dgvr.Cells["other_expense_amount"].Value).Length > 0)
                    {
                        dicParam.Add("other_expense_amount", new ParamObj("other_expense_amount", dgvr.Cells["other_expense_amount"].Value, SysDbType.Decimal, 18));
                    }
                    else
                    {
                        dicParam.Add("other_expense_amount", new ParamObj("other_expense_amount", null, SysDbType.Decimal, 18));
                    }
                    dicParam.Add("remark", new ParamObj("remark", dgvr.Cells["Oremark"].Value, SysDbType.VarChar, 200));
                    string strTollID = CommonCtrl.IsNullToString(dgvr.Cells[this.repair_package_set_other_id.Name].Value);
                    if (strTollID.Length == 0)
                    {
                        opName = "新增接待单其他项目收费";
                        strTollID = Guid.NewGuid().ToString();
                        dicParam.Add("repair_package_set_other_id", new ParamObj("repair_package_set_other_id", strTollID, SysDbType.VarChar, 40));
                        dicParam.Add("create_by", new ParamObj("create_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//创建人id（制单人）
                        dicParam.Add("create_time", new ParamObj("create_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));
                        obj.sqlString = @"insert into sys_b_set_repair_package_set_other (repair_package_set_id,other_expense_type,other_expense_amount,remark,repair_package_set_other_id
                        ,create_by,create_time) 
                        values (@repair_package_set_id,@other_expense_type,@other_expense_amount,@remark,@repair_package_set_other_id
                        ,@create_by,@create_time);";
                    }
                    else
                    {
                        dicParam.Add("repair_package_set_other_id", new ParamObj("repair_package_set_other_id", dgvr.Cells["repair_package_set_other_id"].Value, SysDbType.VarChar, 40));
                        dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id               
                        dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                        opName = "更新接待单其他项目收费";
                        obj.sqlString = @"update sys_b_set_repair_package_set_other set repair_package_set_id=@repair_package_set_id,other_expense_type=@other_expense_type
                        ,other_expense_amount=@other_expense_amount,remark=@remark
                        where repair_package_set_other_id=@repair_package_set_other_id";
                    }
                    obj.Param = dicParam;
                    listSql.Add(obj);
                }
            }
        }
        #endregion      

        #region 取消事件
        void UCRepairMealAddOrEdit_CancelEvent(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确认要取消吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            deleteMenuByTag(this.Tag.ToString(), uc.Name);
        }
        #endregion

        #region 窗体Load事件
        private void UCRepairMealAddOrEdit_Load(object sender, EventArgs e)
        {
            SetDgvAnchor();
            dtpSTime.Value = DateTime.Now;
            dtpETime.Value = DateTime.Now.AddMonths(3);
            if (wStatus == WindowStatus.Edit)
            {
                BindData();
            }

        }
        #endregion

        #region 根据维修套餐ID获取信息，编辑用
        /// <summary>
        /// 根据维修单ID获取信息，复制和编辑用
        /// </summary>
        private void BindData()
        {
            #region 基础信息
            string strWhere = string.Format("repair_package_set_id='{0}'", strId);
            DataTable dt = DBHelper.GetTable("查询维修套餐", "sys_b_set_repair_package_set", "*", strWhere, "", "");
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            DataRow dr = dt.Rows[0];

            #region 套餐基本信息
            string strStime = CommonCtrl.IsNullToString(dr["period_validity"]);
            if (!string.IsNullOrEmpty(strStime))
            {
                long Rticks = Convert.ToInt64(strStime);
                dtpSTime.Value = Common.UtcLongToLocalDateTime(Rticks);//有效日期开始时间
            }
            string strEtime = CommonCtrl.IsNullToString(dr["valid_until"]);
            if (!string.IsNullOrEmpty(strEtime))
            {
                long Rticke = Convert.ToInt64(strEtime);
                dtpETime.Value = Common.UtcLongToLocalDateTime(Rticke);//有效日期结束时间
            }

            txtMealCode.Caption = CommonCtrl.IsNullToString(dr["package_code"]);//套餐编码
            txtMealName.Caption = CommonCtrl.IsNullToString(dr["package_name"]);//套餐名称
            txtRemark.Caption = CommonCtrl.IsNullToString(dr["remarks"]);//备注
                   
            #endregion

            #region 顶部按钮设置
            base.btnStatus.Visible = true;
            base.btnView.Visible = true;
            strStatus = CommonCtrl.IsNullToString(dr["status"]);//状态
            if (strStatus == Convert.ToInt32(DataSources.EnumStatus.Start).ToString())
            {
                base.btnStatus.Caption = "停用";
            }
            else if (strStatus == Convert.ToInt32(DataSources.EnumStatus.Stop).ToString())
            {
                base.btnStatus.Caption = "启用";
            }          

            #endregion

            #endregion

            #region 底部datagridview数据
            BindPData(strId);
            BindMData(strId);
            BindOData(strId);
            BindAData("sys_b_set_repair_package_set", strId);
            #endregion
        }
        #endregion

        #region 维修项目数据
        private void BindPData(string strOrderId)
        {

            //维修项目数据                
            DataTable dpt = DBHelper.GetTable("维修项目数据", "sys_b_set_repair_package_set_project", "*", string.Format(" repair_package_set_id='{0}' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", strOrderId), "", "");
            if (dpt.Rows.Count > 0)
            {
                if (dpt.Rows.Count > dgvproject.Rows.Count)
                {
                    dgvproject.Rows.Add(dpt.Rows.Count - dgvproject.Rows.Count + 1);
                }
                for (int i = 0; i < dpt.Rows.Count; i++)
                {
                    DataRow dpr = dpt.Rows[i];
                    dgvproject.Rows[i].Cells["repair_package_set_project_id"].Value = CommonCtrl.IsNullToString(dpr["repair_package_set_project_id"]);
                    dgvproject.Rows[i].Cells["project_num"].Value = CommonCtrl.IsNullToString(dpr["project_num"]);
                    dgvproject.Rows[i].Cells["project_name"].Value = CommonCtrl.IsNullToString(dpr["project_name"]);
                    dgvproject.Rows[i].Cells["repair_type"].Value = CommonCtrl.IsNullToString(dpr["repair_type"]);
                    dgvproject.Rows[i].Cells["whours_type"].Value = CommonCtrl.IsNullToString(dpr["whours_type"]);
                    dgvproject.Rows[i].Cells["whours_counts"].Value = CommonCtrl.IsNullToString(dpr["whours_counts"]);
                    dgvproject.Rows[i].Cells["whours_price"].Value = CommonCtrl.IsNullToString(dpr["whours_price"]);
                    dgvproject.Rows[i].Cells["tax_amount"].Value = CommonCtrl.IsNullToString(dpr["tax_amount"]);
                    dgvproject.Rows[i].Cells["remark"].Value = CommonCtrl.IsNullToString(dpr["remark"]);
                    listProject.Add(CommonCtrl.IsNullToString(dpr["project_num"]));
                }      
            }
        }
        #endregion

        #region 维修用料数据
        private void BindMData(string strOrderId)
        {

            //维修用料数据
            DataTable dmt = DBHelper.GetTable("维修用料数据", "sys_b_set_repair_package_set_materials", "*", string.Format(" repair_package_set_id='{0}' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", strOrderId), "", "");
            if (dmt.Rows.Count > 0)
            {
                if (dmt.Rows.Count > dgvMaterials.Rows.Count)
                {
                    dgvMaterials.Rows.Add(dmt.Rows.Count - dgvMaterials.Rows.Count + 1);
                }
                for (int i = 0; i < dmt.Rows.Count; i++)
                {
                    DataRow dmr = dmt.Rows[i];
                    dgvMaterials.Rows[i].Cells["repair_package_set_materials_id"].Value = CommonCtrl.IsNullToString(dmr["repair_package_set_materials_id"]);
                    dgvMaterials.Rows[i].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dmr["parts_code"]);
                    dgvMaterials.Rows[i].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dmr["parts_name"]);
                    dgvMaterials.Rows[i].Cells["unit"].Value = CommonCtrl.IsNullToString(dmr["unit"]);
                    dgvMaterials.Rows[i].Cells["number"].Value = CommonCtrl.IsNullToString(dmr["number"]);
                    dgvMaterials.Rows[i].Cells["original_price"].Value = CommonCtrl.IsNullToString(dmr["original_price"]);
                    dgvMaterials.Rows[i].Cells["preferential_price"].Value = CommonCtrl.IsNullToString(dmr["preferential_price"]);                   
                    dgvMaterials.Rows[i].Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dmr["remarks"]);
                    listMater.Add(CommonCtrl.IsNullToString(dmr["parts_code"]));
                }
            }
        }
        #endregion

        #region 其他项目收费数据
        private void BindOData(string strOrderId)
        {

            ////其他项目收费数据
            DataTable dot = DBHelper.GetTable("其他项目收费数据", "sys_b_set_repair_package_set_other", "*", string.Format(" repair_package_set_id='{0}'", strOrderId), "", "");
            if (dot.Rows.Count > 0)
            {
                if (dot.Rows.Count > dgvOther.Rows.Count)
                {
                    dgvOther.Rows.Add(dot.Rows.Count - dgvOther.Rows.Count + 1);
                }
                for (int i = 0; i < dot.Rows.Count; i++)
                {
                    DataRow dor = dot.Rows[i];
                    dgvOther.Rows[i].Cells["repair_package_set_other_id"].Value = CommonCtrl.IsNullToString(dor["repair_package_set_other_id"]);
                    dgvOther.Rows[i].Cells["other_expense_type"].Value = CommonCtrl.IsNullToString(dor["other_expense_type"]);
                    dgvOther.Rows[i].Cells["other_expense_amount"].Value = CommonCtrl.IsNullToString(dor["other_expense_amount"]);
                    dgvOther.Rows[i].Cells["Oremark"].Value = CommonCtrl.IsNullToString(dor["remark"]);
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

        #region 设置底部DataGridView的Anchor属性
        /// <summary>
        /// 设置底部DataGridView的Anchor属性
        /// </summary>
        private void SetDgvAnchor()
        {
            #region 其他收费项目
            dgvOther.Dock = DockStyle.Fill;           
            dgvOther.ReadOnly = false;
            dgvOther.Rows.Add(3);
            dgvOther.AllowUserToAddRows = true;
            ControlsConfig.NumberLimitdgv(dgvproject, new List<string>() { "other_expense_amount" });
            DataTable dot = CommonCtrl.GetDictByCode("sys_other_expense_type", true);
            other_expense_type.DataSource = dot;
            other_expense_type.ValueMember = "dic_id";
            other_expense_type.DisplayMember = "dic_name";
            #endregion

            #region 维修项目信息设置
            dgvproject.Dock = DockStyle.Fill;           
            dgvproject.ReadOnly = false;
            dgvproject.Rows.Add(3);
            dgvproject.AllowUserToAddRows = true;
            dgvproject.Columns["project_num"].ReadOnly = true;
            dgvproject.Columns["project_name"].ReadOnly = true;
            dgvproject.Columns["repair_type"].ReadOnly = true;
            dgvproject.Columns["whours_type"].ReadOnly = true;       
            dgvproject.Columns["tax_amount"].ReadOnly = true;
            ControlsConfig.NumberLimitdgv(dgvproject, new List<string>() { "whours_counts", "whours_price",});
            #endregion

            #region 维修用料设置
            dgvMaterials.Dock = DockStyle.Fill;           
            dgvMaterials.ReadOnly = false;
            dgvMaterials.Rows.Add(3);
            dgvMaterials.AllowUserToAddRows = true;
            dgvMaterials.Columns["parts_code"].ReadOnly = true;
            dgvMaterials.Columns["parts_name"].ReadOnly = true;
            dgvMaterials.Columns["unit"].ReadOnly = true;           
            dgvMaterials.Columns["original_price"].ReadOnly = true;
            ControlsConfig.NumberLimitdgv(dgvMaterials, new List<string>() { "number", "preferential_price" });
            #endregion
        }
        #endregion

        #region 维修项目信息读取
        private void dgvproject_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
            frmWorkHours frmHours = new frmWorkHours();
            DialogResult result = frmHours.ShowDialog();
            if (result == DialogResult.OK)
            {
                dgvproject.Rows[e.RowIndex].Cells["project_num"].Value = frmHours.strProjectNum;
                dgvproject.Rows[e.RowIndex].Cells["repair_type"].Value = GetDicName(frmHours.strRepairType);
                dgvproject.Rows[e.RowIndex].Cells["project_name"].Value = frmHours.strProjectName;
                dgvproject.Rows[e.RowIndex].Cells["whours_price"].Value = frmHours.strQuotaPrice;
                dgvproject.Rows[e.RowIndex].Cells["remark"].Value = frmHours.strRemark;
                dgvproject.Rows[e.RowIndex].Cells["whours_type"].Value = frmHours.strWhoursType == "1" ? "工时" : "定额";
                dgvproject.Rows.Add(1);
                listProject.Add(frmHours.strProjectNum);
            }

        }

        /// <summary>
        /// 工时数量，工时单价发生改变时金额也跟着改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>   
        private void dgvproject_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 5 || e.ColumnIndex == 6)
            {
                string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["whours_counts"].Value)) ? dgvproject.Rows[e.RowIndex].Cells["whours_counts"].Value.ToString() : "0";
                string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["whours_price"])) ? dgvproject.Rows[e.RowIndex].Cells["whours_price"].Value.ToString() : "0";
                dgvproject.Rows[e.RowIndex].Cells["tax_amount"].Value = Convert.ToString(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney));
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

        #region 维修用料信息读取
        private void dgvMaterials_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
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
                    dgvMaterials.Rows[e.RowIndex].Cells["unit"].Value = CommonCtrl.IsNullToString(dpr["default_unit_name"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["number"].Value = "1";
                    dgvMaterials.Rows[e.RowIndex].Cells["original_price"].Value = CommonCtrl.IsNullToString(dpr["highest_out_price"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dpr["remark"]);
                    dgvMaterials.Rows.Add(1);
                    listMater.Add(CommonCtrl.IsNullToString(dpr["ser_parts_code"]));
                }
            }
        }
        #endregion

        #region 右键添加维修项目
        private void addProject_Click(object sender, EventArgs e)
        {
            frmWorkHours frmHours = new frmWorkHours();
            DialogResult result = frmHours.ShowDialog();
            if (result == DialogResult.OK)
            {
                dgvproject.CurrentRow.Cells["project_num"].Value = frmHours.strProjectNum;
                dgvproject.CurrentRow.Cells["repair_type"].Value = GetDicName(frmHours.strRepairType);
                dgvproject.CurrentRow.Cells["project_name"].Value = frmHours.strProjectName;
                dgvproject.CurrentRow.Cells["whours_price"].Value = frmHours.strQuotaPrice;
                dgvproject.CurrentRow.Cells["remark"].Value = frmHours.strRemark;
                dgvproject.CurrentRow.Cells["whours_type"].Value = frmHours.strWhoursType == "1" ? "工时" : "定额";
                dgvproject.Rows.Add(1);
                listProject.Add(frmHours.strProjectNum);
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
            string strItemId = CommonCtrl.IsNullToString(dgvproject.CurrentRow.Cells["repair_package_set_project_id"].Value);
            if (!string.IsNullOrEmpty(strItemId))
            {
                List<string> listField = new List<string>();
                Dictionary<string, string> comField = new Dictionary<string, string>();
                listField.Add(strItemId);
                comField.Add("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.DELETED).ToString());
                DBHelper.BatchUpdateDataByIn("删除维修项目信息", "sys_b_set_repair_package_set_project", comField, "repair_package_set_project_id", listField.ToArray());
                dgvproject.Rows.RemoveAt(introw);
            }
            else
            {
                if (introw != dgvproject.Rows.Count - 1)
                {
                    dgvproject.Rows.RemoveAt(introw);
                }
            }
            string strItemNo = CommonCtrl.IsNullToString(dgvproject.CurrentRow.Cells["project_num"].Value);
            listProject.Remove(strItemNo);
        }
        #endregion

        #region 右键添加维修用料
        private void addParts_Click(object sender, EventArgs e)
        {
            frmParts frmPart = new frmParts();
            DialogResult result = frmPart.ShowDialog();
            if (result == DialogResult.OK)
            {
                string strPId = frmPart.PartsID;
                DataTable dpt = DBHelper.GetTable("", "v_parts", "*", " parts_id='" + strPId + "'", "", "");
                if (dpt.Rows.Count > 0)
                {
                    DataRow dpr = dpt.Rows[0];
                    dgvMaterials.CurrentRow.Cells["parts_code"].Value = CommonCtrl.IsNullToString(dpr["ser_parts_code"]);
                    dgvMaterials.CurrentRow.Cells["parts_name"].Value = CommonCtrl.IsNullToString(dpr["parts_name"]);
                    dgvMaterials.CurrentRow.Cells["unit"].Value = CommonCtrl.IsNullToString(dpr["default_unit_name"]);
                    dgvMaterials.CurrentRow.Cells["number"].Value = "1";
                    dgvMaterials.CurrentRow.Cells["original_price"].Value = CommonCtrl.IsNullToString(dpr["highest_out_price"]);
                    dgvMaterials.CurrentRow.Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dpr["remark"]);
                    dgvMaterials.Rows.Add(1);
                    listMater.Add(CommonCtrl.IsNullToString(dpr["ser_parts_code"]));
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
            string strItemId = CommonCtrl.IsNullToString(dgvMaterials.CurrentRow.Cells["repair_package_set_materials_id"].Value);
            if (!string.IsNullOrEmpty(strItemId))
            {
                List<string> listField = new List<string>();
                Dictionary<string, string> comField = new Dictionary<string, string>();
                listField.Add(strItemId);
                comField.Add("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.DELETED).ToString());
                DBHelper.BatchUpdateDataByIn("删除维修用料", "sys_b_set_repair_package_set_materials", comField, "repair_package_set_materials_id", listField.ToArray());
                dgvMaterials.Rows.RemoveAt(introw);
            }
            else
            {
                if (introw != dgvMaterials.Rows.Count - 1)
                {
                    dgvMaterials.Rows.RemoveAt(introw);
                }
            }
            string strPartCode = CommonCtrl.IsNullToString(dgvMaterials.CurrentRow.Cells["parts_code"].Value);
            listMater.Remove(strPartCode);
        }
        #endregion


    }
}
