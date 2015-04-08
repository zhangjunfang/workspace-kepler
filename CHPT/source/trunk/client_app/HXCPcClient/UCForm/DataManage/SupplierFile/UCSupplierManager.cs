using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SYSModel;
using Newtonsoft.Json;
using System.IO;
using System.Drawing.Printing;
using HXCPcClient.CommonClass;
using System.Collections;
using ServiceStationClient.ComponentUI;
using Utility.Common;
using System.Threading.Tasks;

namespace HXCPcClient.UCForm.DataManage.SupplierFile
{
    public partial class UCSupplierManager : UCBase
    {

        #region 字段属性
        DataTable dt_bill;
        List<string> listIDs = new List<string>();//已选择项的ID列表
        List<string> listStart = new List<string>();//启用状态
        List<string> listStop = new List<string>();//停用状态
        BusinessPrint businessPrint;//业务打印功能
        /// <summary>
        /// 启用停用状态
        /// </summary>
        DataSources.EnumStatus enumStatus;

        private DataGrid masterDataGridView = new DataGrid();
        private BindingSource masterBindingSource = new BindingSource();
        private DataGrid detailsDataGridView = new DataGrid();
        private BindingSource detailsBindingSource = new BindingSource();

        #endregion

        #region 初始化窗体
        /// <summary>
        /// 初始化窗体方法
        /// </summary>
        public UCSupplierManager()
        {
            InitializeComponent();
            //禁止列表自增列
            dgvSupplierList.AutoGenerateColumns = false;


            base.AddEvent += new ClickHandler(UCSupplierManager_AddEvent);
            base.CopyEvent += new ClickHandler(UCSupplierManager_CopyEvent);
            base.EditEvent += new ClickHandler(UCSupplierManager_EditEvent);
            base.DeleteEvent += new ClickHandler(UCSupplierManager_DeleteEvent);
            base.StatusEvent += new ClickHandler(UCSupplierManager_StatusEvent);
            base.ExportEvent += new ClickHandler(UCSupplierManager_ExportEvent);
            base.ViewEvent += new ClickHandler(UCSupplierManager_ViewEvent);
            dateTimeStart.Value = DateTime.Now.AddMonths(-1);
            dateTimeEnd.Value = DateTime.Now;
            List<string> listNotPrint = new List<string>();
            listNotPrint.Add(create_time.Name);
            PaperSize printsize = new PaperSize("printsize", dgvSupplierList.Width / 4 + 10, dgvSupplierList.Height);
            businessPrint = new BusinessPrint(dgvSupplierList, "tb_supplier", "供应商档案", printsize, listNotPrint);
            BindDllInfo();
            BindgvSupplierList();
        }

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCSupplierManager_Load(object sender, EventArgs e)
        {
            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);  //设置查询按钮和清除按钮样式
            dt_bill = CommonFuncCall.GetDataTable();
            base.SetContentMenuScrip(dgvSupplierList);
        }
        #endregion

        #region 界面按钮事件
        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCSupplierManager_AddEvent(object sender, EventArgs e)
        {
            UCSupplierAddOrEdit UCSupplierAdd = new UCSupplierAddOrEdit(WindowStatus.Add, null, this);
            base.addUserControl(UCSupplierAdd, "供应商档案-添加", "UCSupplierAdd", this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 复制事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCSupplierManager_CopyEvent(object sender, EventArgs e)
        {
            string suppID = string.Empty;
            List<string> listField = GetSelectedRecord();
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择要复制的数据!");
                return;
            }
            if (listField.Count > 1)
            {
                MessageBoxEx.Show("一次只可以复制一条数据!");
                return;
            }
            suppID = listField[0].ToString();
            UCSupplierAddOrEdit UCSupplierucCopy = new UCSupplierAddOrEdit(WindowStatus.Copy, suppID, this);
            base.addUserControl(UCSupplierucCopy, "供应商档案-复制", "UCSupplierucCopy" + suppID + "", this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCSupplierManager_EditEvent(object sender, EventArgs e)
        {
            string suppID = string.Empty;
            List<string> listField = GetSelectedRecord();
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择要编辑的数据!");
                return;
            }
            if (listField.Count > 1)
            {
                MessageBoxEx.Show("一次只可以编辑一条数据!");
                return;
            }
            suppID = listField[0].ToString();
            UCSupplierAddOrEdit UCSupplierEdit = new UCSupplierAddOrEdit(WindowStatus.Edit, suppID, this);
            base.addUserControl(UCSupplierEdit, "供应商档案-编辑", "UCSupplierEdit" + suppID + "", this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCSupplierManager_DeleteEvent(object sender, EventArgs e)
        {
            List<string> listField = GetSelectedRecord();
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择要删除的数据!");
                return;
            }
            if (MessageBoxEx.Show("确认要删除选中的数据吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                return;
            }
            Dictionary<string, string> suppField = new Dictionary<string, string>();
            suppField.Add("enable_flag", "0");
            bool flag = DBHelper.BatchUpdateDataByIn("批量删除供应商档案表", "tb_supplier", suppField, "sup_id", listField.ToArray());
            if (flag)
            {
                BindgvSupplierList();
                MessageBoxEx.Show("操作成功！");
            }
            else
            {
                MessageBoxEx.Show("操作失败！");
            }
        }
        //启用/停用
        void UCSupplierManager_StatusEvent(object sender, EventArgs e)
        {
            if (listStart.Count == 0 && listStop.Count == 0)
            {
                return;
            }
            if (!MessageBoxEx.ShowQuestion(string.Format("确定要{0}吗？", btnStatus.Caption)))
            {
                if (enumStatus == DataSources.EnumStatus.Start)
                {
                    btnStatus.Caption = "启用";

                }
                else
                {
                    btnStatus.Caption = "停用";
                }
                return;
            }
            List<SysSQLString> listSql = new List<SysSQLString>();
            Dictionary<string, string> dicStatus = new Dictionary<string, string>();
            SysSQLString sql = new SysSQLString();
            sql.cmdType = CommandType.Text;
            sql.Param = new Dictionary<string, string>();
            string strSql = "update tb_supplier set status=@status where sup_id in ({0})";
            string ids = string.Empty;
            if (enumStatus == DataSources.EnumStatus.Start)
            {
                sql.Param.Add("status", ((int)DataSources.EnumStatus.Stop).ToString());
                foreach (string id in listStart)
                {
                    ids += string.Format("'{0}',", id);
                }
                ids = ids.TrimEnd(',');

            }
            else if (enumStatus == DataSources.EnumStatus.Stop)
            {
                sql.Param.Add("status", ((int)DataSources.EnumStatus.Start).ToString());
                foreach (string id in listStop)
                {
                    ids += string.Format("'{0}',", id);
                }
                ids = ids.TrimEnd(',');
            }
            sql.sqlString = string.Format(strSql, ids);
            listSql.Add(sql);
            if (DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup(btnStatus.Caption + "联系人", listSql))
            {
                MessageBoxEx.ShowInformation(btnStatus.Caption + "成功！");
                BindgvSupplierList();
            }
            else
            {
                MessageBoxEx.ShowError(btnStatus.Caption + "失败！");
                if (enumStatus == DataSources.EnumStatus.Start)
                {
                    btnStatus.Caption = "启用";

                }
                else
                {
                    btnStatus.Caption = "停用";
                }

            }
        }
        /// <summary>
        /// 导出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCSupplierManager_ExportEvent(object sender, EventArgs e)
        {
            if (this.dgvSupplierList.Rows.Count == 0)
            {
                return;
            }
            try
            {
                string fileName = "供应商档案" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
                ExcelHandler.ExportExcel(fileName, ExcelHandler.HandleDataTableForExcel(dgvSupplierList.GetBoundData(), dgvSupplierList));
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog("供应商档案" + ex.Message, "client");
                MessageBoxEx.ShowWarning("导出失败！");
            }
        }
        /// <summary>
        /// 预览事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCSupplierManager_ViewEvent(object sender, EventArgs e)
        {
            //ViewData();
            DataTable dtData = dgvSupplierList.GetBoundData();
            if (dtData != null)
            {
                businessPrint.Preview(dtData);
            }

        }
        /// <summary>
        /// 查询按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnSearch_Click(object sender, EventArgs e)
        {
            BindgvSupplierList();
        }
        /// <summary>
        /// 清除按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnClear_Click(object sender, EventArgs e)
        {
            ClearControlInfo();
        }
        /// <summary>
        /// 分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void winFormPager1_PageIndexChanged(object sender, EventArgs e)
        {
            BindgvSupplierList();
        }
        /// <summary>
        /// 双击列表单元格事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvSupplierList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
            {
                string suppId = Convert.ToString(this.dgvSupplierList.CurrentRow.Cells[suppID.Name].Value.ToString());
                UCSupplierView UCSupplierView = new UCSupplierView(suppId, this);
                base.addUserControl(UCSupplierView, "供应商档案-查看", "UCSupplierView" + suppId + "", this.Tag.ToString(), this.Name);
            }
        }
        #endregion

        #region 方法、函数
        /// <summary>
        /// 清除查询条件控件中的内容
        /// </summary>
        /// <param name="ControlTypeName"></param>
        void ClearControlInfo()
        {
            txtSupplierNo.Caption = string.Empty;
            txtSupplierName.Caption = string.Empty;
            txtSupplierBoss.Caption = string.Empty;
            txtSupplierUser.Caption = string.Empty;
            txtAddress.Caption = string.Empty;

            ddlSupplierType.SelectedIndex = 0;
            ddlCompanyNature.SelectedIndex = 0;
            ddlProvince.SelectedIndex = 0;
            ddlArea.SelectedIndex = 0;
            ddlCity.SelectedIndex = 0;
            ddlState.SelectedIndex = 0;
            dateTimeStart.Value = DateTime.Now.AddMonths(-1);
            dateTimeEnd.Value = DateTime.Now;
        }
        /// <summary>
        /// 绑定下拉框信息
        /// </summary>
        void BindDllInfo()
        {
            ddlSupplierType.Items.Clear();
            ddlCompanyNature.Items.Clear();
            ddlState.Items.Clear();

            //绑定供应商分类
            CommonFuncCall.BindComBoxDataSource(ddlSupplierType, "sys_supplier_category", "全部");
            //绑定企业性质类型
            CommonFuncCall.BindComBoxDataSource(ddlCompanyNature, "sys_enterprise_property", "全部");

            CommonFuncCall.BindProviceComBox(ddlProvince, "全部");
            CommonFuncCall.BindCityComBox(ddlCity, "", "全部");
            CommonFuncCall.BindCountryComBox(ddlArea, "", "全部");

            ddlState.DataSource = DataSources.EnumToList(typeof(DataSources.EnumStatus), true);
            ddlState.ValueMember = "Value";
            ddlState.DisplayMember = "Text";

        }
        /// <summary>
        /// 组合查询条件
        /// </summary>
        /// <returns></returns>
        string BuildString()
        {
            string Str_Where = " enable_flag != 0 ";
            if (!string.IsNullOrEmpty(txtSupplierNo.Caption.Trim()))
            {
                //Str_Where += " and sup_code='" + txtSupplierNo.Caption.Trim() + "'";
                Str_Where += " and sup_code like '%" + txtSupplierNo.Caption.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(txtSupplierName.Caption.Trim()))
            {
                Str_Where += " and sup_full_name like '%" + txtSupplierName.Caption.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(txtSupplierBoss.Caption.Trim()))
            {
                Str_Where += " and legal_person like '%" + txtSupplierBoss.Caption.Trim() + "%'";
            }

            ////联系人
            if (!string.IsNullOrEmpty(txtSupplierUser.Caption.Trim()))
            {
                //Str_Where += "and sup_id in(select relation_object_id from tr_base_contacts  where cont_id='" + txtSupplierUser.Caption.Trim() + "'";
                Str_Where += " and sup_id in(select b.relation_object_id from dbo.tr_base_contacts b where b.cont_id in ( select c.cont_id from tb_contacts c where c.cont_name like '%" + txtSupplierUser.Caption.Trim() + "%'))";
            }
            if (!string.IsNullOrEmpty(ddlSupplierType.SelectedValue.ToString()))
            {
                Str_Where += " and sup_type='" + ddlSupplierType.SelectedValue.ToString() + "'";
            }
            if (!string.IsNullOrEmpty(ddlState.SelectedValue.ToString()))
            {
                Str_Where += " and status='" + ddlState.SelectedValue.ToString() + "'";
            }
            if (!string.IsNullOrEmpty(ddlProvince.SelectedValue.ToString()))
            {
                Str_Where += " and province='" + ddlProvince.SelectedValue.ToString() + "'";
            }
            if (!string.IsNullOrEmpty(ddlCity.SelectedValue.ToString()))
            {
                Str_Where += " and city='" + ddlCity.SelectedValue.ToString() + "'";
            }
            if (!string.IsNullOrEmpty(ddlArea.SelectedValue.ToString()))
            {
                Str_Where += " and county='" + ddlArea.SelectedValue.ToString() + "'";
            }
            if (!string.IsNullOrEmpty(txtAddress.Caption.Trim()))
            {
                Str_Where += " and sup_address like '%" + txtAddress.Caption.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(ddlCompanyNature.SelectedValue.ToString()))
            {
                Str_Where += " and unit_properties='" + ddlCompanyNature.SelectedValue.ToString() + "'";
            }
            if (dateTimeStart.Value != null)
            {
                DateTime dtime = Convert.ToDateTime(dateTimeStart.Value.ToShortDateString() + " 00:00:00");
                Str_Where += " and create_time>=" + Common.LocalDateTimeToUtcLong(dtime);
            }
            if (dateTimeEnd.Value != null)
            {
                DateTime dtime = Convert.ToDateTime(dateTimeEnd.Value.ToShortDateString() + " 23:59:59");
                Str_Where += " and create_time<=" + Common.LocalDateTimeToUtcLong(dtime);
            }

            return Str_Where;
        }
        /// <summary>
        /// 加载供应商列表信息
        /// </summary>
        public void BindgvSupplierList()
        {
            try
            {
                int RecordCount = 0;
                DataTable gvSupplier_dt = DBHelper.GetTableByPage("查询供应商列表信息", "v_supplier_user", "*", BuildString(), "", " order by create_time desc ", winFormPager1.PageIndex, winFormPager1.PageSize, out RecordCount);
                if (gvSupplier_dt != null && gvSupplier_dt.Rows.Count > 0)
                {
                    string relationid = " a.relation_object_id in (";
                    for (int i = 0; i < gvSupplier_dt.Rows.Count; i++)
                    {

                        if (i == gvSupplier_dt.Rows.Count - 1)
                        {
                            relationid += "'" + gvSupplier_dt.Rows[i]["sup_id"].ToString() + "') ";
                        }
                        else
                        {
                            relationid += "'" + gvSupplier_dt.Rows[i]["sup_id"].ToString() + "',";
                        }
                    }
                    DataTable contacts = DBHelper.GetTable("查询供应商联系人", " tr_base_contacts a left join tb_contacts b on a.cont_id=b.cont_id ", "b.cont_name,b.cont_phone,b.cont_email,b.cont_tel,a.relation_object_id ", relationid, "", "");
                    BindData(gvSupplier_dt, contacts);
                }
                else
                {
                    dgvSupplierList.DataSource = gvSupplier_dt;
                    winFormPager1.RecordCount = RecordCount;
                }
            }
            catch (Exception ex)
            {
                //异常日志
            }
        }
        /// <summary>
        /// 获取gvSupplierList列表选中的记录条数
        /// </summary>
        /// <returns></returns>
        private List<string> GetSelectedRecord()
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in dgvSupplierList.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells["suppID"].Value.ToString());
                }
            }
            return listField;
        }

        /// <summary>
        /// 设置选择项后状态
        /// </summary>
        //void SetSelectedStatus()
        //{
        //    //已选择状态列表
        //    List<string> listFiles = new List<string>();
        //    listIDs.Clear();
        //    listStart.Clear();
        //    listStop.Clear();

        //    foreach (DataGridViewRow dgvr in dgvSupplierList.Rows)
        //    {
        //        if (Convert.ToBoolean(dgvr.Cells[colCheck.Name].EditedFormattedValue))
        //        {
        //            string cont_id = dgvr.Cells[suppID.Name].Value.ToString();
        //            listIDs.Add(cont_id);
        //            //if (dgvr.Cells[status.Name].Tag == null)
        //            //{
        //            //    continue;
        //            //}
        //            //enumStatus = (DataSources.EnumStatus)Convert.ToInt32(dgvr.Cells[status.Name].Value);//状态

        //            enumStatus = (DataSources.EnumStatus)Convert.ToInt32(dgvr.Cells[status.Name].Value);//状态

        //            if (enumStatus == DataSources.EnumStatus.Start)
        //            {
        //                listStart.Add(cont_id);
        //            }
        //            else if (enumStatus == DataSources.EnumStatus.Stop)
        //            {
        //                listStop.Add(cont_id);
        //            }
        //        }
        //    }
        //    #region 设置启用/停用
        //    if (listStart.Count > 0 && listStop.Count > 0)
        //    {
        //        btnStatus.Enabled = false;
        //    }
        //    else if (listStart.Count == 0 && listStop.Count == 0)
        //    {
        //        btnStatus.Enabled = false;
        //    }
        //    else if (listStart.Count > 0 && listStop.Count == 0)
        //    {
        //        btnStatus.Enabled = true;
        //        btnStatus.Caption = "停用";
        //        enumStatus = DataSources.EnumStatus.Start;
        //    }
        //    else if (listStart.Count == 0 && listStop.Count > 0)
        //    {
        //        btnStatus.Enabled = true;
        //        btnStatus.Caption = "启用";
        //        enumStatus = DataSources.EnumStatus.Stop;
        //    }
        //    #endregion


        //}
        #endregion

        #region dgv事件

        private void gvSupplierList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null || e.Value.ToString() == string.Empty)
            {
                return;
            }
            string fieldNmae = dgvSupplierList.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("create_time") || fieldNmae.Equals("update_time"))
            {
                long ticks = (long)e.Value;
                e.Value = Common.UtcLongToLocalDateTime(ticks);
            }
            if (fieldNmae.Equals("status"))
            {
                DataSources.EnumStatus enumDataSources = (DataSources.EnumStatus)Convert.ToInt16(e.Value.ToString());
                //e.Value = enumDataSources.ToString();
                e.Value = DataSources.GetDescription(enumDataSources, true);
            }
            if (fieldNmae.Equals("unit_properties") || fieldNmae.Equals("sup_type") || fieldNmae.Equals("credit_class"))
            {
                e.Value = CommonFuncCall.GetBillNameByBillCode(dt_bill, e.Value.ToString());
            }
        }

        private void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlProvince.SelectedValue.ToString()))
            {
                CommonFuncCall.BindCityComBox(ddlCity, ddlProvince.SelectedValue.ToString(), "全部");
                CommonFuncCall.BindCountryComBox(ddlArea, ddlCity.SelectedValue.ToString(), "全部");
            }
            else
            {
                CommonFuncCall.BindCityComBox(ddlCity, "", "全部");
                CommonFuncCall.BindCountryComBox(ddlArea, "", "全部");
            }
        }

        private void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlCity.SelectedValue.ToString()))
            {
                CommonFuncCall.BindCountryComBox(ddlArea, ddlCity.SelectedValue.ToString(), "全部");
            }
            else
            {
                CommonFuncCall.BindCountryComBox(ddlArea, "", "全部");
            }
        }
        //选择
        private void dgvSupplierList_HeadCheckChanged()
        {
            SetSelectedStatus();
        }

        //点击单元格
        private void dgvSupplierList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvSupplierList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

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
            //SetDataSourceBtnStatus(listFiles);
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
            foreach (DataGridViewRow dgvr in dgvSupplierList.Rows)
            {

                if (Convert.ToBoolean(dgvr.Cells[colCheck.Name].Value))
                {

                    //listFiles.Add(dgvr.Cells[colDataSources.Name].Tag.ToString());
                    string id = dgvr.Cells[suppID.Name].Value.ToString();
                    listIDs.Add(id);
                    listFiles.Add(dgvr.Cells[status.Name].Value.ToString());
                    if (dgvr.Cells[status.Name].Value == null)
                    {

                        continue;
                    }
                    enumStatus = (DataSources.EnumStatus)Convert.ToInt32(dgvr.Cells[status.Name].Value);//状态

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

        /// <summary>
        /// 根据数据来源设置按钮状态
        /// </summary>
        /// <param name="listFiles">选中的数据的数据来源</param>
        private void SetDataSourceBtnStatus(List<string> listFiles)
        {
            string dataSource = ((int)DataSources.EnumDataSources.SELFBUILD).ToString();
            if (listFiles.Where(a => a != dataSource).Count() > 0)
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnStatus.Enabled = false;
            }
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

        #endregion

        #region 数据绑定

        public void BindData(DataTable MasterTable, DataTable DetilsTable)
        {

            dgvSupplierList.DataSource = masterBindingSource;
            masterDataGridView.DataSource = masterBindingSource;
            detailsDataGridView.DataSource = detailsBindingSource;

            DataSet data = new DataSet();
            data.Locale = System.Globalization.CultureInfo.InvariantCulture;
            MasterTable.TableName = "Master";
            DetilsTable.TableName = "Detils";
            data.Tables.Add(CloneTable(MasterTable));
            data.Tables.Add(CloneTable(DetilsTable));

            // Establish a relationship between the two tables.
            DataRelation relation = new DataRelation("SupCont",
                data.Tables["Master"].Columns["sup_id"],
                data.Tables["Detils"].Columns["relation_object_id"]);
            data.Relations.Add(relation);

            masterBindingSource.DataSource = data;
            masterBindingSource.DataMember = "Master";

            detailsBindingSource.DataSource = masterBindingSource;
            detailsBindingSource.DataMember = "SupCont";

            masterDataGridView.Size = new Size(800, 600);
            //this.tabPage2.Controls.Add(masterDataGridView);


        }

        DataTable CloneTable(DataTable SourceTable)
        {
            DataTable CloneTable = SourceTable.Copy();
            CloneTable.TableName = SourceTable.TableName;
            return CloneTable;
        }
        #endregion
    }


}
