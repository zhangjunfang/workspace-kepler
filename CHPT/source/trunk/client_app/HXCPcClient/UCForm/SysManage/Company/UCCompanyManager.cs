using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using SYSModel;
using System.Threading;
using System.Collections;
using Utility.Common;
using System.IO;
using Utility.CommonForm;
using System.Drawing.Printing;

namespace HXCPcClient.UCForm.SysManage.Company
{
    /// <summary>
    /// 公司档案
    /// 孙明生
    /// 修改人：杨天帅
    /// </summary>
    public partial class UCCompanyManager : UCBase
    {
        #region --成员变量
        private bool myLock = true;
        private int recordCount = 0;
        /// <summary>
        /// 列表中选中的是启用的 id集合
        /// </summary>
        ArrayList qi_list = new ArrayList();
        /// <summary>
        /// 列表中选中的是停用的 id集合
        /// </summary>
        ArrayList ti_list = new ArrayList();

        List<string> file = new List<string>();
        private string status = string.Empty;
        List<string> listIDs = new List<string>();//已选择项的ID列表
        List<string> listStart = new List<string>();//启用状态
        List<string> listStop = new List<string>();//停用状态
        /// <summary>
        /// 启用停用状态
        /// </summary>
        DataSources.EnumStatus enumStatus;
        BusinessPrint businessPrint;//业务打印功能
        #endregion

        #region --构造函数
        public UCCompanyManager()
        {
            InitializeComponent();
            UIAssistants.SetButtonStyle4QueryAndClear(btnQuery, btnClear);  //设置查询按钮和清除按钮样式
            DataGridViewEx.SetDataGridViewStyle(this.dgvRecord);   //设置数据表格样式,并将最后一列填充其余空白
        }
        #endregion

        #region --窗体初始化
        private void UCCompanyManager_Load(object sender, EventArgs e)
        {
            base.RoleButtonStstus(this.Name);//角色按钮权限-是否隐藏
            //dgvRecord.ReadOnly = false;

            this.InitEvent();

            CommonCtrl.DgCmbBindDict(this.colWXZZ, "sys_repair_qualification");
            CommonCtrl.DgCmbBindDict(this.colGSXZ, "sys_enterprise_property");
            DataSources.BindComDataGridViewBoxColumnDataEnum(this.colStatus, typeof(DataSources.EnumStatus));

            this.InitData();

            this.BindPageData();

            this.page.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.page_PageIndexChanged);

            base.SetContentMenuScrip(dgvRecord);
            List<string> listNotPrint = new List<string>();
            PaperSize printsize = new PaperSize("printsize", dgvRecord.Width / 4 + 40, dgvRecord.Height);
            businessPrint = new BusinessPrint(dgvRecord, "tb_company", "公司档案", printsize, listNotPrint);

        }
        #endregion

        #region --初始化事件和数据
        private void InitEvent()
        {
            this.uiHandler -= new UiHandler(this.ShowBindData);
            this.uiHandler += new UiHandler(this.ShowBindData);

            base.AddEvent -= new ClickHandler(UC_AddEvent);
            base.AddEvent += new ClickHandler(UC_AddEvent);

            base.CopyEvent -= new ClickHandler(UC_CopyEvent);
            base.CopyEvent += new ClickHandler(UC_CopyEvent);

            base.EditEvent -= new ClickHandler(UC_EditEvent);
            base.EditEvent += new ClickHandler(UC_EditEvent);

            base.DeleteEvent -= new ClickHandler(UC_DeleteEvent);
            base.DeleteEvent += new ClickHandler(UC_DeleteEvent);

            base.ViewEvent -= new ClickHandler(UC_ViewEvent);
            base.ViewEvent += new ClickHandler(UC_ViewEvent);

            base.StatusEvent -= new ClickHandler(UC_StatusEvent);
            base.StatusEvent += new ClickHandler(UC_StatusEvent);

            base.ExportEvent -= new ClickHandler(UC_ExportEvent);
            base.ExportEvent += new ClickHandler(UC_ExportEvent);

            base.PrintEvent += new ClickHandler(UCCompanyManager_PrintEvent);
        }


        private void InitData()
        {
            this.tbCode.Caption = string.Empty;
            this.tbName.Caption = string.Empty;
            this.tbPhone.Caption = string.Empty;

            //维修资质
            CommonCtrl.CmbBindDict(this.cmbWXZZ, "sys_repair_qualification");

            //单位性质
            CommonCtrl.CmbBindDict(this.cmbGSXZ, "sys_enterprise_property");

            CommonCtrl.CmbBindProvice(this.cmbProvince, "省");
            CommonCtrl.CmbBindCity(this.cmbCity, string.Empty, "市");
            CommonCtrl.CmbBindCountry(this.cmbCountry, string.Empty, "县");
        }
        #endregion

        #region --按钮事件
        //复制操作
        void UC_CopyEvent(object sender, EventArgs e)
        {
            if (dgvRecord.CurrentRow == null)
            {
                MessageBoxEx.Show("请选择复制记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataRow dr = (this.dgvRecord.CurrentRow.DataBoundItem as DataRowView).Row;
            string id = dr["com_id"].ToString();
            UCCompanyAddOrEdit CompanyEdit = new UCCompanyAddOrEdit(WindowStatus.Copy, dr, this.Name, this);
            base.addUserControl(CompanyEdit, "公司档案-复制", "CompanyCopy" + id, this.Tag.ToString(), this.Name);
        }
        //删除操作
        void UC_DeleteEvent(object sender, EventArgs e)
        {
            List<string> listField = this.dgvRecord.GetCheckedIds(this.columnCheck, this.columnId.Name);
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择删除记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            for (int i = 0; i < listField.Count; i++)
            {
                ErrInfo err = CanDelete(listField[i]);
                if (err.IsSuccess == false)
                {
                    MessageBoxEx.Show(err.Info, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            if (MessageBoxEx.ShowQuestion("是否确认删除?"))
            {
                Dictionary<string, string> dicFileds = new Dictionary<string, string>();
                dicFileds.Add("enable_flag", DataSources.EnumEnableFlag.DELETED.ToString("d"));
                dicFileds.Add("update_by", GlobalStaticObj.UserID);
                dicFileds.Add("update_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj.CurrentDateTime).ToString());

                bool flag = DBHelper.BatchUpdateDataByIn("批量删除公司档案", "tb_company", dicFileds, "com_id", listField.ToArray());
                if (flag)
                {
                    MessageBoxEx.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //dicFileds = new Dictionary<string, string>();
                    //dicFileds.Add("enable_flag", DataSources.EnumEnableFlag.DELETED.ToString("d"));
                    //dicFileds.Add("update_by", GlobalStaticObj.UserID);
                    //dicFileds.Add("update_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj.CurrentDateTime).ToString());
                    //DBHelper.BatchUpdateDataByIn("删除公司下属组织", "tb_organization", dicFileds, "com_id", listField.ToArray());
                    if (dgvRecord.Rows.Count > 0)
                    {
                        dgvRecord.CurrentCell = dgvRecord.Rows[0].Cells[0];
                    }
                    this.BindPageData();
                    MessageProcessor.UpdateComOrgInfo();
                }
                else
                {
                    MessageBoxEx.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        //编辑
        void UC_EditEvent(object sender, EventArgs e)
        {
            if (dgvRecord.CurrentRow == null)
            {
                MessageBoxEx.Show("请选择编辑记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataRow dr = (this.dgvRecord.CurrentRow.DataBoundItem as DataRowView).Row;
            string id = dr["com_id"].ToString();
            UCCompanyAddOrEdit uc = new UCCompanyAddOrEdit(WindowStatus.Edit, dr, this.Name, this);
            uc.RefreshDataStart -= new UCCompanyAddOrEdit.RefreshData(this.BindPageData);
            uc.RefreshDataStart += new UCCompanyAddOrEdit.RefreshData(this.BindPageData);
            base.addUserControl(uc, "公司档案-编辑", "CompanyEdit" + id, this.Tag.ToString(), this.Name);
        }
        // 添加      
        void UC_AddEvent(object sender, EventArgs e)
        {
            UCCompanyAddOrEdit uc = new UCCompanyAddOrEdit(WindowStatus.Add, null, this.Name, this);
            uc.RefreshDataStart -= new UCCompanyAddOrEdit.RefreshData(this.BindPageData);
            uc.RefreshDataStart += new UCCompanyAddOrEdit.RefreshData(this.BindPageData);
            base.addUserControl(uc, "公司档案-新增", "CompanyAdd", this.Tag.ToString(), this.Name);
        }
        // 预览       
        void UC_ViewEvent(object sender, EventArgs e)
        {
            businessPrint.Preview(dgvRecord.GetBoundData());
        }

        //浏览
        private void View()
        {
            if (dgvRecord.CurrentRow == null)
            {
                MessageBoxEx.Show("请选择预览记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string com_id = dgvRecord.CurrentRow.Cells["columnId"].Value.ToString();
            UCCompanyView uc = new UCCompanyView(com_id, this, WindowStatus.View);
            base.addUserControl(uc, "公司档案-浏览", "CompanyView" + com_id, this.Tag.ToString(), this.Name);
        }

        //打印
        void UCCompanyManager_PrintEvent(object sender, EventArgs e)
        {
            businessPrint.Print(dgvRecord.GetBoundData());
        }
        //导出
        void UC_ExportEvent(object sender, EventArgs e)
        {
            if (this.dgvRecord.Rows.Count == 0)
            {
                return;
            }
            try
            {
                string fileName = "公司档案" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
                ExcelHandler.ExportExcel(fileName, ExcelHandler.HandleDataTableForExcel(dgvRecord.GetBoundData(), dgvRecord));
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog("公司档案" + ex.Message, "client");
                MessageBoxEx.ShowWarning("导出失败！");
            }

        }


        #endregion

        #region --数据查询
        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.BindPageData();
        }
        /// <summary>
        /// 查询绑定数据
        /// </summary>
        public void BindPageData()
        {
            if (this.myLock)
            {
                this.myLock = false;

                string where = string.Format(" enable_flag='{0}' ", DataSources.EnumEnableFlag.USING.ToString("d"));

                if (!string.IsNullOrEmpty(tbCode.Caption.Trim()))//公司编码
                {
                    where += string.Format(" and  Com_code like '%{0}%'", tbCode.Caption.Trim());
                }
                if (this.cmbWXZZ.SelectedValue != null
                    && this.cmbWXZZ.SelectedValue.ToString() != "")  //维修资质 
                {
                    where += string.Format(" and  repair_qualification like '%{0}%'", this.cmbWXZZ.SelectedValue);
                }
                if (!string.IsNullOrEmpty(this.tbPhone.Caption.Trim()))//联系电话
                {
                    where += string.Format(" and  com_tel like '%{0}%'", tbPhone.Caption.Trim());
                }
                if (this.cmbGSXZ.SelectedValue != null
                    && this.cmbGSXZ.SelectedValue.ToString() != "")//公司性质
                {
                    where += string.Format(" and  unit_properties = '{0}'", cmbGSXZ.SelectedValue);
                }

                if (!string.IsNullOrEmpty(this.tbName.Caption.Trim()))//公司全称
                {
                    where += string.Format(" and  com_name like '%{0}%'", this.tbName.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(this.cmbProvince.SelectedValue.ToString()))//省
                {
                    where += string.Format(" and  province  = '{0}'", this.cmbProvince.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(this.cmbCity.SelectedValue.ToString()))//市
                {
                    where += string.Format(" and  city  = '{0}'", this.cmbCity.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(this.cmbCountry.SelectedValue.ToString()))//县
                {
                    where += string.Format(" and  county  = '{0}'", this.cmbCountry.SelectedValue.ToString());
                }

                ThreadPool.QueueUserWorkItem(new WaitCallback(this._BindPageData), where);
            }
        }
        /// <summary> 异步数据查询 
        /// </summary>
        /// <param name="obj"></param>
        private void _BindPageData(object obj)
        {
            DataTable dt = DBHelper.GetTableByPage("分页查询公司", "tb_company", "*", obj.ToString(), "", "order by create_time desc",
                           page.PageIndex, page.PageSize, out this.recordCount);

            this.Invoke(this.uiHandler, dt);
        }
        /// <summary> 异步绑定数据 
        /// </summary>
        /// <param name="obj"></param>
        private void ShowBindData(object obj)
        {
            this.dgvRecord.DataSource = obj;
            this.page.RecordCount = this.recordCount;

            this.myLock = true;
        }
        /// <summary>
        /// 页码改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page_PageIndexChanged(object sender, EventArgs e)
        {
            this.BindPageData();
        }
        #endregion

        #region --dgv事件
        private void dgvRecord_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0)
            {
                e.Cancel = true;
            }
        }
        /// <summary>
        /// 双击单元格 进浏览页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRecord_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string com_id = dgvRecord.Rows[e.RowIndex].Cells[this.columnId.Name].Value.ToString();
                UCCompanyView uc = new UCCompanyView(com_id, this, WindowStatus.View);
                uc.Name = this.Name;
                //uc.RoleButtonStstus(Name);
                base.addUserControl(uc, "公司档案-浏览", "CompanyView" + com_id, this.Tag.ToString(), this.Name);
            }
        }
        #endregion

        #region --清除查询条件
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.InitData();
        }
        #endregion

        #region --地区联动
        private void cmbProvince_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CommonCtrl.CmbBindCity(this.cmbCity, this.cmbProvince.SelectedValue.ToString(), "市");
            CommonCtrl.CmbBindCountry(this.cmbCountry, string.Empty, "县");
        }

        private void cmbCity_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CommonCtrl.CmbBindCountry(this.cmbCountry, this.cmbCity.SelectedValue.ToString(), "县");
        }
        #endregion

        #region --启用停用
        //启动或者停止
        private void UC_StatusEvent(object sender, EventArgs e)
        {
            if (!MessageBoxEx.ShowQuestion(string.Format("确定要{0}吗？", btnStatus.Caption)))
            {
                if (this.status == DataSources.EnumStatus.Start.ToString("d"))
                {
                    btnStatus.Caption = "启用";

                }
                else
                {
                    btnStatus.Caption = "停用";
                }
                return;
            }
            List<SQLObj> listSql = new List<SQLObj>();
            string opName = "修改公司档案状态";
            string msg = "";
            if (this.status == DataSources.EnumStatus.Start.ToString("d"))//启用
            {
                StatusSql(listSql, ti_list, this.status);//停用的ti_list 改为启用
                msg = "启用";
            }
            else//停用
            {
                StatusSql(listSql, qi_list, this.status);//启用的qi_list 改为停用
                msg = "停用";
            }
            if (DBHelper.BatchExeSQLMultiByTrans(opName, listSql))
            {
                foreach (string id in this.qi_list)
                {
                    foreach (DataGridViewRow row in this.dgvRecord.Rows)
                    {
                        if (row.Cells[this.columnId.Name].Value.ToString() == id)
                        {
                            DataRow dr = (row.DataBoundItem as DataRowView).Row;
                            dr["status"] = DataSources.EnumStatus.Stop.ToString("d");
                        }
                    }
                }
                foreach (string id in this.ti_list)
                {
                    foreach (DataGridViewRow row in this.dgvRecord.Rows)
                    {
                        if (row.Cells[this.columnId.Name].Value.ToString() == id)
                        {
                            DataRow dr = (row.DataBoundItem as DataRowView).Row;
                            dr["status"] = DataSources.EnumStatus.Start.ToString("d");
                        }
                    }
                }
                base.btnStatus.Enabled = false;
                MessageBoxEx.Show(msg + "成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageProcessor.UpdateComOrgInfo();
                base.btnStatus.Enabled = true;
                GetStatus();
                btnStatus.Caption = msg;
            }
            else
            {
                MessageBoxEx.Show(msg + "失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetStatus();
                if (msg == "启用")
                    btnStatus.Caption = "停用";
                else
                    btnStatus.Caption = "启用";
            }
        }

        private void StatusSql(List<SQLObj> listSql, ArrayList idList, string status)
        {
            if (idList.Count > 0)
            {
                foreach (string id in idList)
                {
                    SQLObj sqlObj = new SQLObj();
                    sqlObj.cmdType = CommandType.Text;
                    Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();//参数
                    dicParam.Add("com_id", new ParamObj("com_id", id, SysDbType.VarChar, 40));//ID
                    dicParam.Add("status", new ParamObj("status", status, SysDbType.VarChar, 40));
                    dicParam.Add("update_by", new ParamObj("update_by", GlobalStaticObj.UserID, SysDbType.NVarChar, 40));
                    dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString(), SysDbType.BigInt));
                    sqlObj.sqlString = @"update [tb_company] set status=@status,update_by=@update_by,update_time=@update_time where com_id=@com_id;";
                    sqlObj.Param = dicParam;
                    listSql.Add(sqlObj);
                }
            }
        }
        /// <summary>
        /// 全选复选框事件 
        /// </summary>
        private void dgvRecord_HeadCheckChanged()
        {
            GetStatus();
        }

        private void GetStatus()
        {
            btnEdit.Enabled = true;
            btnCopy.Enabled = true;
            this.qi_list.Clear();
            this.ti_list.Clear();
            this.file.Clear();
            this.status = string.Empty;

            foreach (DataGridViewRow row in this.dgvRecord.Rows)
            {

                if (Convert.ToBoolean(row.Cells[columnCheck.Name].Value))
                {

                    file.Add(row.Cells[columnId.Name].Value.ToString());
                    if (row.Cells[this.colStatus.Name].Value.ToString() == DataSources.EnumStatus.Start.ToString("d")) //表格中是启用
                    {
                        this.qi_list.Add(row.Cells[this.columnId.Name].Value.ToString());
                    }
                    else//表格中是停用
                    {
                        this.ti_list.Add(row.Cells[this.columnId.Name].Value.ToString());
                    }
                }
            }
            if (file.Count > 1)
            {
                btnEdit.Enabled = false;
                btnCopy.Enabled = false;
            }
            this.SetBtnStatus(this.qi_list, this.ti_list);
        }
        /// <summary>
        /// 启用按钮状态设置
        /// </summary>
        /// <param name="qi_list">列表中选中的是启用的 id集合</param>
        /// <param name="ti_list">列表中选中的是停用的 id集合</param>
        private void SetBtnStatus(ArrayList qi_list, ArrayList ti_list)
        {
            if (qi_list.Count > 0 && ti_list.Count > 0)
            {
                base.btnStatus.Enabled = false;
                base.btnStatus.Caption = DataSources.GetDescription(DataSources.EnumStatus.Stop);
            }
            else if (qi_list.Count > 0 && ti_list.Count == 0)
            {
                base.btnStatus.Enabled = true;
                base.btnStatus.Caption = DataSources.GetDescription(DataSources.EnumStatus.Stop);
                this.status = DataSources.EnumStatus.Stop.ToString("d");
            }
            else if (qi_list.Count == 0 && ti_list.Count > 0)
            {
                base.btnStatus.Enabled = true;
                base.btnStatus.Caption = DataSources.GetDescription(DataSources.EnumStatus.Start);
                this.status = DataSources.EnumStatus.Start.ToString("d");
            }
            else if (qi_list.Count == 0 && ti_list.Count == 0)
            {
                base.btnStatus.Enabled = false;
                base.btnStatus.Caption = DataSources.GetDescription(DataSources.EnumStatus.Start);
            }
        }
        #endregion

        #region 按钮状态
        /// <summary>
        /// 设置选择项后状态
        /// </summary>
        void SetSelectedStatus()
        {
            btnCopy.Enabled = true;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            btnStatus.Enabled = false;
            //已选择状态列表
            List<string> listFiles = new List<string>();
            //记录选中数据状态
            RecordData(listFiles);

            SetStatus();

            SetMultiBtnStatus(listFiles);


        }

        /// <summary>
        /// 设置多选时按钮状态
        /// </summary>
        /// <param name="listFiles">选中的记录</param>
        private void SetMultiBtnStatus(List<string> listFiles)
        {
            if (listFiles.Count > 1)
            {
                btnCopy.Enabled = false;
                btnEdit.Enabled = false;
            }
        }

        /// <summary>
        /// 记录选中数据
        /// </summary>
        /// <param name="listFiles">数据状态的表</param>
        private void RecordData(List<string> listFiles)
        {
            listIDs.Clear();
            listStart.Clear();
            listStop.Clear();

            foreach (DataGridViewRow dgvr in dgvRecord.Rows)
            {

                if (Convert.ToBoolean(dgvr.Cells[columnCheck.Name].Value))
                {

                    //listFiles.Add(dgvr.Cells[colDataSources.Name].Tag.ToString());
                    string id = dgvr.Cells[columnId.Name].Value.ToString();
                    listIDs.Add(id);
                    listFiles.Add(dgvr.Cells[colStatus.Name].Value.ToString());
                    if (dgvr.Cells[colStatus.Name].Value == null)
                    {

                        continue;
                    }
                    enumStatus = (DataSources.EnumStatus)Convert.ToInt32(dgvr.Cells[colStatus.Name].Value);//状态

                    if (enumStatus == DataSources.EnumStatus.Start)
                    {
                        listStart.Add(id);
                    }
                    else if (enumStatus == DataSources.EnumStatus.Stop)
                    {
                        listStop.Add(id);
                    }
                }
            }
        }

        /// <summary>
        /// 设置启用停用
        /// </summary>
        private void SetStatus()
        {
            if (listStart.Count > 0 && listStop.Count > 0)
            {
                btnStatus.Enabled = false;
            }
            else if (listStart.Count == 0 && listStop.Count == 0)
            {
                btnStatus.Enabled = false;
            }
            else if (listStart.Count > 0 && listStop.Count == 0)
            {
                btnStatus.Enabled = true;
                btnStatus.Caption = "停用";
                enumStatus = DataSources.EnumStatus.Start;
            }
            else if (listStart.Count == 0 && listStop.Count > 0)
            {
                btnStatus.Enabled = true;
                btnStatus.Caption = "启用";
                enumStatus = DataSources.EnumStatus.Stop;
            }
        }


        #endregion

        #region 判断公司可否删除

        public ErrInfo CanDelete(string ComID)
        {
            ErrInfo err = new ErrInfo(true, "成功");
            if (HasOrg(ComID))
            {
                err = new ErrInfo(false, "要删除的公司下还有组织,无法删除");
            }
            if (HasWarehouse(ComID))
            {
                err = new ErrInfo(false, "要删除的公司下还有仓库,无法删除");
            }
            return err;
        }

        public bool HasOrg(string ComID)
        {
            return DBHelper.IsExist("查询公司下是否存在组织", "tb_organization", " enable_flag=1 and com_id='" + ComID + "'");
        }

        public bool HasWarehouse(string ComID)
        {
            return DBHelper.IsExist("查询公司下是否存在仓库", "tb_warehouse", " enable_flag=1 and com_id='" + ComID + "'");
        }

        #endregion

    }

    public class ErrInfo
    {
        bool isSuccess;

        public bool IsSuccess
        {
            get { return isSuccess; }
        }

        string info;

        public string Info
        {
            get { return info; }
        }

        public ErrInfo(bool SuccessFlag, string Meg)
        {
            isSuccess = SuccessFlag;
            info = Meg;
        }
    }
}
