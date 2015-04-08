using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using HXCPcClient.CommonClass;
using Utility.Common;
using HXCPcClient.Chooser;
using SYSModel;
using System.Collections;
using System.Threading;
using Model;

namespace HXCPcClient.UCForm.SysManage.MemberPara
{
    /// <summary>
    /// 会员参数设置 添加 编辑 
    /// 孙明生
    /// </summary>
    public partial class UCMemberParaAddOrEdit : UCBase
    {
        #region --回调更新事件
        public delegate void RefreshData();
        public RefreshData RefreshDataStart;
        #endregion

        #region --成员变量
        /// <summary> 父窗体名称 
        /// </summary>
        private string parentName = string.Empty;
        /// <summary>
        /// 会员参数id
        /// </summary>
        public string id;
        public WindowStatus wStatus;
        /// <summary>
        /// 特殊维修项目折扣表ID
        /// </summary>
        private string SetInfoprojrctid
        {
            get
            {
                if (dgvprojrct.CurrentRow == null)
                {
                    return string.Empty;
                }
                object setInfoprojrctid = dgvprojrct.CurrentRow.Cells["setInfo_projrct_id"].Value;
                if (setInfoprojrctid == null)
                {
                    return string.Empty;
                }
                else
                {
                    return setInfoprojrctid.ToString();
                }
            }
        }
        /// <summary>
        /// 会员参数设置特殊配件折扣表ID
        /// </summary>
        private string SetInfopartsid
        {
            get
            {
                if (dgvparts.CurrentRow == null)
                {
                    return string.Empty;
                }
                object setInfopartsid = dgvparts.CurrentRow.Cells["setInfo_parts_id"].Value;
                if (setInfopartsid == null)
                {
                    return string.Empty;
                }
                else
                {
                    return setInfopartsid.ToString();
                }
            }
        }
        /// <summary>
        /// 删除的特殊维修项目ID集合
        /// </summary>
        ArrayList delProjrctId = new ArrayList();
        /// <summary>
        /// 删除的特殊配件ID集合
        /// </summary>
        ArrayList delPartsId = new ArrayList();
        #endregion

        #region 初始化
        public UCMemberParaAddOrEdit(string parentName)
        {
            InitializeComponent();

            DataGridViewEx.SetDataGridViewStyle(this.dgvprojrct);
            DataGridViewEx.SetDataGridViewStyle(this.dgvparts); 

            this.parentName = parentName;

            this.uiHandler -= new UiHandler(this.ShowData);
            this.uiHandler += new UiHandler(this.ShowData);
            
            base.SaveEvent += new ClickHandler(this.UCMemberParaAddOrEdit_SaveEvent);
            base.AddEvent += new ClickHandler(this.tsmadd_Click);
            base.CancelEvent += new ClickHandler(UC_CancelEvent);
        }
        #endregion

        #region --窗体初始化
        private void UCMemberParaAddOrEdit_Load(object sender, EventArgs e)
        {
            base.SetBtnStatus(wStatus);
            this.SetBtnStatus();
            dgvprojrct.ReadOnly = false;
            dgvparts.ReadOnly = false;

            CommonFuncCall.BindComBoxDataSource(cbomember_grade_id, "sys_member_grade", "请选择");
            if (wStatus == WindowStatus.Edit || wStatus == WindowStatus.Copy)
            {
                DataTable dt = DBHelper.GetTable("查询会员参数设置信息", "tb_CustomerSer_member_setInfo", "*", "setInfo_id='" + id + "'", "", "");
                if (dt != null && dt.Rows.Count > 0)
                {
                    cbomember_grade_id.SelectedValue = dt.Rows[0]["member_grade_id"].ToString();
                    txtservice_project_discount.Caption = dt.Rows[0]["service_project_discount"].ToString();
                    txtparts_discount.Caption = dt.Rows[0]["parts_discount"].ToString();
                    txtSubscription_Ratio.Caption = dt.Rows[0]["Subscription_Ratio"].ToString();
                }
                ThreadPool.QueueUserWorkItem(new WaitCallback(this._LoadData));              
            }       
        }
        #endregion

        #region --设置按钮隐显
        private void SetBtnStatus()
        {
            this.btnCopy.Visible = false;
            this.btnStatus.Visible = false;
            this.btnBalance.Visible = false;
            this.btnImport.Visible = false;
            this.btnExport.Visible = false;
            this.btnPrint.Visible = false;
            this.btnSet.Visible = false;
            this.btnSubmit.Visible = false;
            this.btnConfirm.Visible = false;
            this.btnCommit.Visible = false;
            this.btnActivation.Visible = false;
            this.btnSync.Visible = false;
            this.btnSave.Visible = true;
            this.btnCancel.Visible = true;

            this.btnAdd.Visible = false;
            this.btnEdit.Visible = false;
            this.btnDelete.Visible = false;
            this.btnVerify.Visible = false;
            this.btnRevoke.Visible = false;
            this.btnView.Visible = false;
            this.btnSet.Visible = false;
            this.btnPrint.Visible = false;           
        }
        #endregion

        private void _LoadData(object obj)
        {
            SQLObj sqlobj = new SQLObj();
            sqlobj.cmdType = CommandType.Text;
            sqlobj.Param = new Dictionary<string, ParamObj>();

           sqlobj.sqlString = "SELECT p.*,w.project_name,w.project_num,w.quota_price,'' as discount_price  FROM tb_CustomerSer_member_setInfo_projrct p left join tb_workhours w on w.whours_id=p.service_project_id  "
                                + " where p.enable_flag='1'  and p.setInfo_id='" + id + "'";

           sqlobj.sqlString += "; SELECT p2.*,w2.ser_parts_code,w2.parts_name,w3.ref_out_price,'' as discount_price  FROM tb_CustomerSer_member_setInfo_parts p2 left join tb_parts w2 on w2.parts_id = p2.parts_id left join tb_parts_price w3 on w3.parts_id=p2.parts_id "
                             + " where p2.enable_flag='1'  and p2.setInfo_id='" + id + "'";

            DataSet ds = DBHelper.GetDataSet("", sqlobj);

            this.Invoke(this.uiHandler, ds);
        }

        private void ShowData(object obj)
        {
            if (obj == null)
            {
                return;
            }
            DataSet ds = obj as DataSet;
            if (ds.Tables.Count > 0)
            {
                this.BindDgvProjrct(ds.Tables[0]);

                if (ds.Tables.Count > 1)
                {
                    this.BindDgvParts(ds.Tables[1]);
                }
            }
        }

        #region 查询绑定会员参数设置特殊配件折扣表
        /// <summary>
        /// 查询绑定会员参数设置特殊配件折扣表
        /// </summary>
        private void BindDgvParts(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                DataGridViewRow row = dgvparts.Rows[dgvparts.Rows.Add()];

                row.Cells["ser_parts_code"].Value = dr["ser_parts_code"].ToString();
                row.Cells["parts_name"].Value = dr["parts_name"].ToString();
                row.Cells["ref_out_price"].Value = dr["ref_out_price"].ToString();
                row.Cells["parts_discount"].Value = dr["parts_discount"].ToString();
                row.Cells["setInfo_parts_id"].Value = dr["id"].ToString();
                row.Cells["remark"].Value = dr["remark"].ToString();
                row.Cells["parts_id"].Value = dr["parts_id"].ToString();
                row.Cells["setInfo_id"].Value = dr["setInfo_id"].ToString();
                if (dr["ref_out_price"].ToString() != "")
                {
                    int iValue = Convert.ToInt32(dr["parts_discount"].ToString());
                    bool bln = false;
                    decimal quota_price = 0;
                    string strquota_price = Utility.Common.Validator.IsDecimal(dr["ref_out_price"].ToString(), 10, 2, ref bln);
                    if (bln)
                    {
                        quota_price = Convert.ToDecimal(strquota_price);
                        decimal discount_price = Math.Abs(Math.Round((quota_price * iValue) / 100, 2));
                        row.Cells["discount_price"].Value = discount_price.ToString();
                    }
                    else
                    {
                        row.Cells["discount_price"].Value = "";
                    }
                }
            }
        }
        #endregion

        #region 查询绑定会员参数设置特殊维修项目折扣
        /// <summary>
        /// 查询绑定会员参数设置特殊维修项目折扣
        /// </summary>
        private void BindDgvProjrct(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                DataGridViewRow row = dgvprojrct.Rows[dgvprojrct.Rows.Add()];
                row.Cells["project_num"].Value = dr["project_num"].ToString();
                row.Cells["project_name"].Value = dr["project_name"].ToString();
                row.Cells["quota_price"].Value = dr["quota_price"].ToString();
                row.Cells["project_service_project_discount"].Value = dr["service_project_discount"].ToString();
                row.Cells["project_remark"].Value = dr["remark"].ToString();
                row.Cells["service_project_id"].Value = dr["service_project_id"].ToString();
                row.Cells["p_setInfo_id"].Value = dr["setInfo_id"].ToString();
                row.Cells["setInfo_projrct_id"].Value = dr["id"].ToString();
                if (dr["service_project_discount"].ToString() != "")
                {
                    int iValue = Convert.ToInt32(dr["service_project_discount"].ToString());
                    bool bln = false;
                    decimal quota_price = 0;
                    string strquota_price = Utility.Common.Validator.IsDecimal(dr["quota_price"].ToString(), 10, 2, ref bln);
                    if (bln)
                    {
                        quota_price = Convert.ToDecimal(strquota_price);
                        decimal discount_price = Math.Abs(Math.Round((quota_price * iValue) / 100, 2));
                        row.Cells["project_discount_price"].Value = discount_price.ToString();
                    }
                    else
                    {
                        row.Cells["project_discount_price"].Value = "";
                    }
                }
            }
        }
        #endregion

        #region 保存
        void UCMemberParaAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                dgvprojrct.EndEdit();
                dgvparts.EndEdit();
                string msg = "";
                bool bln = false;
                if (string.IsNullOrEmpty(cbomember_grade_id.SelectedValue.ToString()))
                {
                    Utility.Common.Validator.SetError(errorProvider1, cbomember_grade_id, "请选择会员等级!");
                    return ;
                }
                string Service_project_discount = Utility.Common.Validator.IsDecimal(txtservice_project_discount.Caption.Trim(), 10, 2, ref bln);
                if (!bln)
                {
                    Utility.Common.Validator.SetError(errorProvider1, txtservice_project_discount, "服务项目折扣格式不正确!限定0-100");
                    return ;
                }
                string Parts_discount = Utility.Common.Validator.IsDecimal(txtparts_discount.Caption.Trim(), 10, 2, ref bln);
                if (!bln)
                {
                    Utility.Common.Validator.SetError(errorProvider1, txtparts_discount, "配件折扣格式不正确!限定0-100");
                    return;
                }
                
                string Subscription_Ratio = Utility.Common.Validator.IsDecimal(txtSubscription_Ratio.Caption.Trim(), 10, 2, ref bln);
                if (!bln)
                {
                    Utility.Common.Validator.SetError(errorProvider1, txtSubscription_Ratio, "消费金额兑换积分比例格式不正确!限定0-100");
                    return;
                }
                bln = ValidatorDgv(ref msg);
                if (!bln)
                {
                    MessageBoxEx.Show(msg, "保存", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                    return;
                }
                string newGuid;
                string currSetInfo_id = ""; ;

                string keyName = string.Empty;
                string keyValue = string.Empty;
                string opName = "新增会员参数设置";
                Dictionary<string, string> dicFileds = new Dictionary<string, string>();
                dicFileds.Add("member_grade_id", cbomember_grade_id.SelectedValue.ToString());//会员级别 
                dicFileds.Add("service_project_discount", Service_project_discount);//服务项目折扣
                dicFileds.Add("parts_discount", Parts_discount);//配件折扣
                dicFileds.Add("Subscription_Ratio", Subscription_Ratio);//消费金额兑换积分比例            
             
                string nowUtcTicks = Common.LocalDateTimeToUtcLong(GlobalStaticObj.CurrentDateTime).ToString();
                dicFileds.Add("update_by", HXCPcClient.GlobalStaticObj.UserID);
                dicFileds.Add("update_time", nowUtcTicks);

                if (wStatus == WindowStatus.Add || wStatus == WindowStatus.Copy)
                {
                    newGuid = Guid.NewGuid().ToString();
                    currSetInfo_id = newGuid;
                    dicFileds.Add("setInfo_id", newGuid);//新ID                   
                    dicFileds.Add("create_by", HXCPcClient.GlobalStaticObj.UserID);
                    dicFileds.Add("create_time", nowUtcTicks);
                    dicFileds.Add("enable_flag", DataSources.EnumEnableFlag.USING.ToString("d"));
                    bln = DBHelper.Submit_AddOrEdit(opName, "tb_CustomerSer_member_setInfo", keyName, keyValue, dicFileds);
                    bln = UpdateProjrctParts(newGuid);
                }
                else if (wStatus == WindowStatus.Edit)
                {
                    keyName = "setInfo_id";
                    keyValue = id;
                    currSetInfo_id = id;
                    opName = "更新会员参数设置";
                    bln = DBHelper.Submit_AddOrEdit(opName, "tb_CustomerSer_member_setInfo", keyName, keyValue, dicFileds);
                    bln = UpdateProjrctParts(id);
                }
                if (bln)
                {
                    if (this.RefreshDataStart != null)
                    {
                        this.RefreshDataStart();
                    }
                    MessageBoxEx.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    deleteMenuByTag(this.Tag.ToString(), this.parentName);

                }
                else
                {
                    MessageBoxEx.Show("保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("保存失败！" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        /// <summary>
        /// 取消事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UC_CancelEvent(object sender, EventArgs e)
        {
            deleteMenuByTag(this.Tag.ToString(), this.parentName);
        }

        #region 获取特殊维修项目折扣列表删除sql
        /// <summary>
        /// 获取特殊维修项目折扣列表删除sql
        /// </summary>
        /// <returns></returns>
        private string GetDelProjrctSql()
        {
            string delProjrctIds = "";
            string delProjrct = "";
            foreach (string strProjrctId in delProjrctId)
            {
                delProjrctIds += "'" + strProjrctId + "',";
            }
            if (delProjrctIds != "")
            {
                delProjrctIds = delProjrctIds.Substring(0, delProjrctIds.Length - 1);
                delProjrct = "update tb_CustomerSer_member_setInfo_projrct set enable_flag='" + 0 + "' where id in (" + delProjrctIds + ");";
            }
            return delProjrct;
        }
        #endregion

        #region 获取会员参数设置特殊配件折扣列表删除sql
        /// <summary>
        /// 获取会员参数设置特殊配件折扣列表删除sql
        /// </summary>
        /// <returns></returns>
        private string GetDelPartsSql()
        {
            string delPartsIds = "";
            string delParts = "";
            foreach (string strPartsId in delPartsId)
            {
                delPartsIds += "'" + strPartsId + "',";
            }
            if (delPartsIds != "")
            {
                delPartsIds = delPartsIds.Substring(0, delPartsIds.Length - 1);
                delParts = "update tb_CustomerSer_member_setInfo_parts set enable_flag='" + 0 + "' where id in (" + delPartsIds + ");";
            }
            return delParts;
        }
        #endregion

        #region 编辑页面列表信息 拼语句执行
        /// <summary>
        /// 编辑页面列表信息
        /// </summary>
        /// <param name="setInfo_id">会员参数设置ID</param>
        /// <returns></returns>
        private bool UpdateProjrctParts(string setInfo_id)
        {
            string delProjrctSql = GetDelProjrctSql();
            string delPartsSql = GetDelPartsSql();
            string UpdteInsertProjrctSql = GetUpdteInsertProjrctSql(setInfo_id);
            string UpdteInsertPartsSql = GetUpdteInsertPartsSql(setInfo_id);
            string strSql = delProjrctSql + delPartsSql + UpdteInsertProjrctSql + UpdteInsertPartsSql;
            if (strSql.Length > 0)
            {
                SysSQLString sysSql = new SysSQLString();
                sysSql.cmdType = CommandType.Text;
                sysSql.Param = new Dictionary<string, string>();
                sysSql.sqlString = strSql;
                List<SysSQLString> sqlList = new List<SysSQLString>();
                sqlList.Add(sysSql);
                return DBHelper.BatchExeSQLStringMultiByTrans("编辑会员参数设置特殊维修项目折扣", sqlList);
            }
            return true;
        }
        #endregion

        #region 获取特殊配件折扣列表编辑和添加语句
        /// <summary>
        /// 获取特殊配件折扣列表编辑和添加语句
        /// </summary>
        /// <param name="setInfo_id"></param>
        /// <returns></returns>
        private string GetUpdteInsertPartsSql(string setInfo_id)
        {
            string insertSql = "";
            string updateSql = "";
            string strInsert = "insert into tb_CustomerSer_member_setInfo_parts(id,setInfo_id,parts_id,parts_discount,remark,enable_flag,create_by, create_time,update_by,update_time)values";
            string nowUtcTicks = Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString();
            string strSql = "";
            foreach (DataGridViewRow dr in dgvparts.Rows)
            {
                if (dr.Cells[this.setInfo_parts_id.Name].Value.ToString().Length == 0)
                {
                    string guid = Guid.NewGuid().ToString();
                    strSql += "('" + guid + "',";
                    strSql += "'" + setInfo_id + "',";
                    strSql += "'" + dr.Cells[this.parts_id.Name].Value.ToString() + "',";                   
                    strSql += dr.Cells[this.parts_discount.Name].Value.ToString() + ",";//折扣
                    strSql += "'" + dr.Cells["remark"].Value.ToString() + "',";//备注
                    strSql += "'1',";//enable_flag
                    strSql += "'" + HXCPcClient.GlobalStaticObj.UserID + "',";//create_by
                    strSql += nowUtcTicks + ",";//create_time
                    strSql += "'" + HXCPcClient.GlobalStaticObj.UserID + "',";//update_by
                    strSql += nowUtcTicks + ");";//update_time
                }
                else
                {
                    updateSql += " update tb_CustomerSer_member_setInfo_parts set ";
                    updateSql += "parts_discount='" + dr.Cells[this.parts_discount.Name].Value.ToString() + "',";
                    updateSql += "remark='" + dr.Cells["remark"].Value.ToString() + "',";
                    updateSql += "update_by='" + HXCPcClient.GlobalStaticObj.UserID + "',";
                    updateSql += "update_time='" + nowUtcTicks + "' ";
                    updateSql += "where id='" + dr.Cells[this.setInfo_parts_id.Name].Value.ToString() + "'; ";
                }
            }
            if (strSql.Length > 0)
            {
                strSql = strSql.Substring(0, strSql.Length - 1);
                insertSql = strInsert + strSql + ";";
            }
            return insertSql + updateSql;
        }
        #endregion

        #region 获取特殊维修项目折扣列表编辑和添加语句
        /// <summary>
        /// 获取特殊维修项目折扣列表编辑和添加语句
        /// </summary>
        /// <param name="setInfo_id"></param>
        /// <returns></returns>
        private string GetUpdteInsertProjrctSql(string setInfo_id)
        {
            string insertSql = "";
            string updateSql = "";
            string strInsert = "insert into tb_CustomerSer_member_setInfo_projrct(id,setInfo_id,service_project_id,service_project_discount,remark,enable_flag,create_by,create_time,update_by,update_time) values";
            string nowUtcTicks = Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString();
            string strSql = "";
            foreach (DataGridViewRow dr in dgvprojrct.Rows)
            {
                if (dr.Cells[this.setInfo_projrct_id.Name].Value.ToString().Length == 0)
                {
                    string guid = Guid.NewGuid().ToString();
                    strSql += "('" + guid + "',";
                    strSql += "'" + setInfo_id + "',";
                    strSql += "'" + dr.Cells[this.service_project_id.Name].Value.ToString() + "',";                   
                    strSql += dr.Cells[this.project_service_project_discount.Name].Value.ToString() + ",";//折扣
                    strSql += "'" + dr.Cells["project_remark"].Value.ToString() + "',";//备注
                    strSql += "'1',";//enable_flag
                    strSql += "'" + HXCPcClient.GlobalStaticObj.UserID + "',";//create_by
                    strSql += nowUtcTicks + ",";//create_time
                    strSql += "'" + HXCPcClient.GlobalStaticObj.UserID + "',";//update_by
                    strSql += nowUtcTicks + ");";//update_time
                }
                else
                {
                    updateSql += " update tb_CustomerSer_member_setInfo_projrct set ";
                    updateSql += "service_project_discount='" + dr.Cells[this.project_service_project_discount.Name].Value.ToString() + "',";
                    updateSql += "service_project_id='" + dr.Cells[this.service_project_id.Name].Value.ToString() + "',";
                    updateSql += "remark='" + dr.Cells["project_remark"].Value.ToString() + "',";
                    updateSql += "update_by='" + HXCPcClient.GlobalStaticObj.UserID + "',";
                    updateSql += "update_time='" + nowUtcTicks + "' ";
                    updateSql += "where id='" + dr.Cells[this.setInfo_projrct_id.Name].Value.ToString() + "'; ";
                }
            }
            if (strSql.Length > 0)
            {
                strSql = strSql.Substring(0, strSql.Length - 1);
                insertSql = strInsert + strSql + ";";
            }
            return insertSql + updateSql;
        }
        #endregion
     
        #region 控件内容验证
        private bool ValidatorDgv(ref string msg)
        {
            foreach (DataGridViewRow dr in dgvprojrct.Rows)
            {
                bool bln = false;
                string strproject_service_project_discount = dr.Cells["project_service_project_discount"].Value.ToString();

                string currValue = Utility.Common.Validator.IsDecimal(strproject_service_project_discount, 10, 2, ref bln);
                if (!bln)
                {
                    msg = "特殊维修项目折扣" + strproject_service_project_discount + "格式不正确,请输入0-10数字，例8.5";
                    tabControlEx1.SelectedIndex = 0;
                    dgvprojrct.CurrentCell = dr.Cells["project_service_project_discount"];
                    dgvprojrct.BeginEdit(true);
                    return false;
                }
                decimal iValue = Convert.ToDecimal(currValue);
                if (iValue < 0 || iValue > 10)
                {
                    msg = "特殊维修项目折扣" + strproject_service_project_discount + "请输入0-10数字，例8.5";
                    tabControlEx1.SelectedIndex = 0;
                   dgvprojrct.CurrentCell = dr.Cells["project_service_project_discount"];
                   dgvprojrct.BeginEdit(true);
                    return false;
                }
                decimal quota_price = 0;
                string strquota_price = Utility.Common.Validator.IsDecimal(dr.Cells["quota_price"].Value.ToString(), 10, 2, ref bln);
                if (bln)
                {
                    quota_price = Convert.ToDecimal(strquota_price);
                    decimal discount_price = Math.Abs(Math.Round((quota_price * iValue) / 100, 2)); //(quota_price * iValue) / 100;
                    dr.Cells["project_discount_price"].Value = discount_price.ToString();
                }
                
            }
            foreach (DataGridViewRow dr in dgvparts.Rows)
            {
                bool bln = false;
                string strparts_discount = dr.Cells["parts_discount"].Value.ToString();
                string currValue = Utility.Common.Validator.IsDecimal(strparts_discount, 10, 2, ref bln);
                if (!bln)
                {
                    msg = "特殊配件折扣" + strparts_discount + "格式不正确,请输入0-10数字，例8.5";
                    tabControlEx1.SelectedIndex = 1;
                    dgvparts.CurrentCell = dr.Cells["parts_discount"];
                    dgvparts.BeginEdit(true);
                    return false;
                }

                decimal iValue = Convert.ToDecimal(currValue);
                if (iValue < 0 || iValue > 10)
                {
                    msg = "特殊配件折扣" + strparts_discount + "请输入0-10数字，例8.5";
                    tabControlEx1.SelectedIndex = 1;
                    dgvparts.CurrentCell = dr.Cells["parts_discount"];
                    dgvparts.BeginEdit(true);
                    return false;
                }

                decimal quota_price = 0;
                string strquota_price = Utility.Common.Validator.IsDecimal(dr.Cells["ref_out_price"].Value.ToString(), 10, 2, ref bln);
                if (bln)
                {
                    quota_price = Convert.ToDecimal(strquota_price);
                    decimal discount_price = Math.Abs(Math.Round((quota_price * iValue) / 100, 2));
                    dr.Cells["discount_price"].Value = discount_price.ToString();
                }                
            }
            return true;
        }
        #endregion

        #region 右键删除 事件
        private void tsmdel_Click(object sender, EventArgs e)
        {
            if (tabControlEx1.SelectedIndex == 0)
            {
                //特殊维修项目              
                string id = SetInfoprojrctid;
                dgvprojrct.Rows.Remove(dgvprojrct.CurrentRow);
                if(id!="")
                    delProjrctId.Add(id);
            }
            else
            { 
                //特殊配件
                string id = SetInfopartsid;
                dgvparts.Rows.Remove(dgvparts.CurrentRow);
                if (id != "")
                    delPartsId.Add(id);
            }
        }
        #endregion

        #region 右键添加 事件
        private void tsmadd_Click(object sender, EventArgs e)
        {
            if (tabControlEx1.SelectedIndex == 0)
            {
                //特殊维修项目
                frmWorkHours fm = new frmWorkHours();
                if (fm.ShowDialog() == DialogResult.OK)
                {
                    int rowindex = dgvprojrct.Rows.Add();
                    DataGridViewRow gvr = dgvprojrct.Rows[rowindex];
                    gvr.Cells["project_num"].Value = fm.strProjectNum;
                    gvr.Cells["project_name"].Value = fm.strProjectName;
                    gvr.Cells["quota_price"].Value = fm.strQuotaPrice;
                    gvr.Cells["service_project_id"].Value = fm.strWhours_id;
                    gvr.Cells["project_service_project_discount"].Value = "";
                    gvr.Cells["project_discount_price"].Value = "";
                    gvr.Cells["project_remark"].Value = "";
                    gvr.Cells["setInfo_projrct_id"].Value = "";                    
                }
            }
            else
            {
                //特殊配件
                frmParts fm = new frmParts();
                if (fm.ShowDialog() == DialogResult.OK)
                {
                    DataGridViewRow gvr = dgvparts.Rows[dgvparts.Rows.Add()];
                    gvr.Cells["ser_parts_code"].Value = fm.PartsCode;
                    gvr.Cells["parts_name"].Value = fm.PartsName;
                    gvr.Cells["ref_out_price"].Value = fm.ref_out_price;
                    gvr.Cells["parts_id"].Value = fm.PartsID;

                    gvr.Cells["parts_discount"].Value = "";
                    gvr.Cells["discount_price"].Value = "";
                    gvr.Cells["remark"].Value = "";
                    gvr.Cells["setInfo_parts_id"].Value = "";
                }
            }
        }
        #endregion

        #region 选定单元格的编辑模式启动时发生
        private void dgvprojrct_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex < 3 || e.ColumnIndex == 4 || e.ColumnIndex > 5)
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region 特殊维修项目dgv输入验证 并计算项目折后价
        /// <summary>
        /// 特殊维修项目dgv输入验证 并计算项目折后价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvprojrct_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 3)
                return;
            bool bln = false;
            string currValue = e.FormattedValue.ToString(); //dgvprojrct.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            string strValue = Utility.Common.Validator.IsDecimal(currValue, 10, 2, ref bln);

            if (!bln)
            {
                MessageBoxEx.Show("折扣" + currValue + "格式不正确,请输入0-10数字，例8.5", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                return;
            }
            decimal iValue = Convert.ToDecimal(strValue);
            if (iValue < 0 || iValue > 10)
            {
                MessageBoxEx.Show("折扣" + currValue + "请输入0-10数字，例8.5", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                return;
            }
            
            decimal quota_price = 0;
            string strquota_price = Utility.Common.Validator.IsDecimal(dgvprojrct.Rows[e.RowIndex].Cells["quota_price"].Value.ToString(), 10, 2, ref bln);
            if (!bln)
            {
                MessageBoxEx.Show("原价格式不正确!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            quota_price = Convert.ToDecimal(strquota_price);
            decimal discount_price = Math.Abs(Math.Round((quota_price * iValue) / 10, 2)); //(quota_price * iValue) / 100;
            dgvprojrct.Rows[e.RowIndex].Cells["project_discount_price"].Value = discount_price.ToString();
        }
         #endregion

        #region 选定单元格的编辑模式启动时发生
        private void dgvparts_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex < 3 || e.ColumnIndex == 4 || e.ColumnIndex > 5)
            {
                e.Cancel = true;
            }
        }
         #endregion

        #region 特殊配件dgv输入验证 并计算项目折后价
        /// <summary>
        /// 特殊配件dgv输入验证 并计算项目折后价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvparts_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 3)
                return;
            bool bln = false;
            string currValue = e.FormattedValue.ToString(); //dgvprojrct.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            string strValue = Utility.Common.Validator.IsDecimal(currValue, 10, 2, ref bln);

            if (!bln)
            {
                MessageBoxEx.Show("折扣" + currValue + "格式不正确,请输入0-10数字，例8.5", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                return;
            }
            decimal iValue = Convert.ToDecimal(strValue);
            if (iValue < 0 || iValue > 10)
            {
                MessageBoxEx.Show("折扣" + currValue + "请输入0-10数字，例8.5", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                return;
            }

            decimal quota_price = 0;
            string strquota_price = Utility.Common.Validator.IsDecimal(dgvparts.Rows[e.RowIndex].Cells["ref_out_price"].Value.ToString(), 10, 2, ref bln);
            if (!bln)
            {
                MessageBoxEx.Show("原价格式不正确!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            quota_price = Convert.ToDecimal(strquota_price);
            decimal discount_price = Math.Abs(Math.Round((quota_price * iValue) / 10, 2));
            dgvparts.Rows[e.RowIndex].Cells["discount_price"].Value = discount_price.ToString();
        }
        #endregion

        private void dgvprojrct_DoubleClick(object sender, EventArgs e)
        {
            this.tsmadd_Click(null, null);
        }

        private void dgvparts_DoubleClick(object sender, EventArgs e)
        {
            this.tsmadd_Click(null, null);
        }
    }
}