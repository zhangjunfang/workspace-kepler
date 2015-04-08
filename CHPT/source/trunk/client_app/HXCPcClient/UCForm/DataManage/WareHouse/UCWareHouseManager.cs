using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using Utility.Common;
using SYSModel;
using HXCPcClient.Chooser;
using System.Collections.ObjectModel;
using System.Drawing.Printing;

namespace HXCPcClient.UCForm.DataManage.WareHouse
{
    public partial class UCWareHouseManager : UCBase
    {

        #region 字段属性
        DataTable dt_CargoSpace_Num = null;
        List<string> listIDs = new List<string>();//已选择项的ID列表
        List<string> listStart = new List<string>();//启用状态
        List<string> listStop = new List<string>();//停用状态
        BusinessPrint businessPrint;//业务打印功能
        /// <summary>
        /// 启用停用状态
        /// </summary>
        DataSources.EnumStatus enumStatus;
        #endregion

        #region 初始化窗体
        /// <summary>
        /// 初始化窗体方法
        /// </summary>
        public UCWareHouseManager()
        {
            InitializeComponent();
            //禁止列表自增列
            dgvWareHouseList.AutoGenerateColumns = false;
            base.AddEvent += new ClickHandler(UCWareHouseManager_AddEvent);
            base.CopyEvent += new ClickHandler(UCWareHouseManager_CopyEvent);
            base.EditEvent += new ClickHandler(UCWareHouseManager_EditEvent);
            base.DeleteEvent += new ClickHandler(UCWareHouseManager_DeleteEvent);
            base.StatusEvent += new ClickHandler(UCWareHouseManager_StatusEvent);
            base.ViewEvent += new ClickHandler(UCWareHouseManager_ViewEvent);
            dateTimeStart.Value = DateTime.Now.AddMonths(-1);
            dateTimeEnd.Value = DateTime.Now;
            PaperSize paperSize = new PaperSize();
            paperSize.Width = 280;
            paperSize.Height = 210;
            businessPrint = new BusinessPrint(dgvWareHouseList, "tb_warehouse", "仓库管理", paperSize, new List<string>());
            BindDllInfo();
            BindgvWareHouseList();
            SetDgvMenu();
        }
        /// <summary>
        /// 设置表单的右键菜单
        /// </summary>
        private void SetDgvMenu()
        {
            this.SetContentMenuScrip(this.dgvWareHouseList);
            tsmiBalance.Visible = false;
            tsmiSubmit.Visible = false;
            tsmiVerify.Visible = false;
            tsmiStatus.Visible = false;
            tsmiSync.Visible = false;
            tsmiConfirm.Visible = false;
            tsmiActivation.Visible = false;
            tsmiCommit.Visible = false;
            tsmiRevoke.Visible = false;
        }
        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCWareHouseManager_Load(object sender, EventArgs e)
        {
            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);  //设置查询按钮和清除按钮样式
            btnSync.Visible = false;
            DataGridViewEx.SetDataGridViewStyle(dgvWareHouseList);
            base.SetContentMenuScrip(dgvWareHouseList);
            SetQuick();
        }
        #endregion

        #region 方法、函数
        /// <summary>
        /// 清除查询条件控件中的内容
        /// </summary>
        /// <param name="ControlTypeName"></param>
        void ClearControlInfo()
        {
            txtBelongCompany.Text = string.Empty;
            txtBelongCompany.Tag = string.Empty;
            txtWareHouseName.Caption = string.Empty;
            txtWareHouseNo.Caption = string.Empty;

            ddlDataSource.SelectedIndex = 0;
            ddlState.SelectedIndex = 0;

            dateTimeStart.Value = DateTime.Now.AddMonths(-1);
            dateTimeEnd.Value = DateTime.Now;
        }
        /// <summary>
        /// 组合查询条件
        /// </summary>
        /// <returns></returns>
        string BuildString()
        {
            string Str_Where = " enable_flag != 0 ";
            if (!string.IsNullOrEmpty(ddlDataSource.SelectedValue.ToString()))
            {
                Str_Where += " and data_source='" + ddlDataSource.SelectedValue.ToString() + "'";
            }
            if (!string.IsNullOrEmpty(txtBelongCompany.Text.ToString()))
            {
                ////Str_Where += " and com_id='" + txtBelongCompany.Tag.ToString() + "'";
                Str_Where += " and com_name like '%" + txtBelongCompany.Text + "%'";
            }
            if (!string.IsNullOrEmpty(txtWareHouseNo.Caption.Trim()))
            {
                //Str_Where += " and wh_code='" + txtWareHouseNo.Caption.Trim() + "'";
                Str_Where += " and wh_code like '%" + txtWareHouseNo.Caption.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(txtWareHouseName.Caption.Trim()))
            {
                Str_Where += " and wh_name like '%" + txtWareHouseName.Caption.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(ddlState.SelectedValue.ToString()))
            {
                Str_Where += " and status='" + ddlState.SelectedValue.ToString() + "'";
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
        /// 绑定下拉框信息
        /// </summary>
        void BindDllInfo()
        {
            ddlDataSource.Items.Clear();
            ddlState.Items.Clear();

            ddlDataSource.DataSource = DataSources.EnumToList(typeof(DataSources.EnumDataSources), true);
            ddlDataSource.ValueMember = "Value";
            ddlDataSource.DisplayMember = "Text";
            ddlState.DataSource = DataSources.EnumToList(typeof(DataSources.EnumStatus), true);
            ddlState.ValueMember = "Value";
            ddlState.DisplayMember = "Text";
        }
        /// <summary>
        /// 绑定下拉框的第一项默认值
        /// </summary>
        /// <param name="control"></param>
        void BindDllFristInfo(ComboBoxEx control)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("dic_code");
            dt.Columns.Add("dic_name");
            DataRow dr = dt.NewRow();
            dr["dic_code"] = "";
            dr["dic_name"] = "--全部--";
            dt.Rows.Add(dr);

            control.DataSource = dt;
            control.ValueMember = "dic_code";
            control.DisplayMember = "dic_name";
            control.SelectedIndex = 0;
        }
        /// <summary>
        /// 获取列表选中的记录条数
        /// </summary>
        /// <returns></returns>
        private List<string> GetSelectedRecord()
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in dgvWareHouseList.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells["wh_id"].Value.ToString());
                }
            }
            return listField;
        }
        /// <summary>
        /// 加载仓库列表信息
        /// </summary>
        public void BindgvWareHouseList()
        {
            try
            {
                int RecordCount = 0;
                //DataTable gvSupplier_dt = DBHelper.GetTableByPage("查询仓库列表信息", "tb_warehouse", "*", BuildString(), "", "wh_id ", winFormPager1.PageIndex, winFormPager1.PageSize, out RecordCount);
                DataTable gvWareHouse_dt = DBHelper.GetTableByPage("查询仓库列表信息", "v_warehouse_companyname_username", "*", BuildString(), "", " order by create_time desc ", winFormPager1.PageIndex, winFormPager1.PageSize, out RecordCount);
                dgvWareHouseList.DataSource = gvWareHouse_dt;
                winFormPager1.RecordCount = RecordCount;

                string wh_id = string.Empty;
                if (gvWareHouse_dt != null && gvWareHouse_dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in gvWareHouse_dt.Rows)
                    {
                        wh_id += "'" + dr["wh_id"] + "',";
                    }
                    wh_id = wh_id.Trim(',');
                    dt_CargoSpace_Num = DBHelper.GetTable("计算仓库货位数", "tb_cargo_space", "COUNT(1) Num,wh_id", " enable_flag=1 and wh_id in(" + wh_id + ") group by wh_id", "", "");
                }
            }
            catch (Exception ex)
            {
                //异常日志
            }
        }
        #endregion

        #region 设置速查功能

        /// <summary>
        /// 设置速查功能
        /// </summary>
        private void SetQuick()
        {
            //设置所属公司速查
            txtBelongCompany.SetBindTable("tb_company", "com_name");
            txtBelongCompany.GetDataSourced += new ServiceStationClient.ComponentUI.TextBox.TextChooser.GetDataSourceHandler(txtBelongCompany_GetDataSourced);
            txtBelongCompany.DataBacked += new ServiceStationClient.ComponentUI.TextBox.TextChooser.DataBackHandler(txtBelongCompany_DataBacked);
            
        }

        void txtBelongCompany_GetDataSourced(ServiceStationClient.ComponentUI.TextBox.TextChooser tc, string sqlString)
        {
            sqlString = string.Format("select * from tb_company where com_name like '%{0}%' and enable_flag=1 and status=1", txtBelongCompany.Text);
            DataTable dvt = CommonFuncCall.GetDataSource(sqlString);
            tc.SetDataSource(dvt);
            if (dvt != null)
            {
                tc.Search();
            }
        }

        void txtBelongCompany_DataBacked(DataRow dr)
        {
            txtBelongCompany.Text = dr["com_name"].ToString();
        }

        #endregion

        #region 界面按钮事件
        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCWareHouseManager_AddEvent(object sender, EventArgs e)
        {
            UCWareHouseAddOrEdit ucAddOrEdit = new UCWareHouseAddOrEdit(WindowStatus.Add, null, this);
            base.addUserControl(ucAddOrEdit, "仓库档案-添加", "UCWareHouseAdd", this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 复制事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCWareHouseManager_CopyEvent(object sender, EventArgs e)
        {
            string wareHouseId = string.Empty;
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
            wareHouseId = listField[0].ToString();
            UCWareHouseAddOrEdit UCWareHouseCopy = new UCWareHouseAddOrEdit(WindowStatus.Copy, wareHouseId, this);
            base.addUserControl(UCWareHouseCopy, "仓库档案-复制", "UCWareHouseCopy" + wareHouseId + "", this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCWareHouseManager_EditEvent(object sender, EventArgs e)
        {
            string wareHouseId = string.Empty;
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
            wareHouseId = listField[0].ToString();
            UCWareHouseAddOrEdit UCWareHouseEdit = new UCWareHouseAddOrEdit(WindowStatus.Edit, wareHouseId, this);
            base.addUserControl(UCWareHouseEdit, "仓库档案-编辑", "UCWareHouseEdit" + wareHouseId + "", this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 启用/停用事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCWareHouseManager_StatusEvent(object sender, EventArgs e)
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
            string strSql = "update tb_warehouse set status=@status where wh_id in ({0})";
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
            if (DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup(btnStatus.Caption + "仓库", listSql))
            {
                MessageBoxEx.ShowInformation(btnStatus.Caption + "成功！");
                BindgvWareHouseList();
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
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCWareHouseManager_DeleteEvent(object sender, EventArgs e)
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
            Dictionary<string, string> warehouseField = new Dictionary<string, string>();
            warehouseField.Add("enable_flag", "0");
            bool flag = DBHelper.BatchUpdateDataByIn("批量删除仓库档案表", "tb_warehouse", warehouseField, "wh_id", listField.ToArray());
            if (flag)
            {
                BindgvWareHouseList();
                MessageBoxEx.Show("操作成功！");
            }
            else
            {
                MessageBoxEx.Show("操作失败！");
            }
        }
        /// <summary>
        /// 导出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCWareHouseManager_ExportEvent(object sender, EventArgs e)
        {
            if (this.dgvWareHouseList.Rows.Count == 0)
            {
                return;
            }
            try
            {
                string fileName = "仓库档案" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
                ExcelHandler.ExportExcel(fileName, ExcelHandler.HandleDataTableForExcel(dgvWareHouseList.GetBoundData(), dgvWareHouseList));
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog("仓库档案" + ex.Message, "client");
                MessageBoxEx.ShowWarning("导出失败！");
            }
        }
        /// <summary>
        /// 预览事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCWareHouseManager_ViewEvent(object sender, EventArgs e)
        {
            DataTable dtData = dgvWareHouseList.DataSource as DataTable;
            if (dtData != null)
            {
                businessPrint.Preview(dtData);
            }
        }
        /// <summary>
        /// 清除按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearControlInfo();
        }
        /// <summary>
        /// 查询按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindgvWareHouseList();
        }
        /// <summary>
        /// 分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winFormPager1_PageIndexChanged(object sender, EventArgs e)
        {
            BindgvWareHouseList();
        }


        private void txtBelongCompany_ChooserClick(object sender, EventArgs e)
        {
            frmChooseCompany frm = new frmChooseCompany();
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtBelongCompany.Text = frm.Company_Name;
                txtBelongCompany.Tag = frm.Company_ID;
            }
        }

        #endregion

        #region dgv事件
        private void gvWareHouseList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null || e.Value.ToString().Length == 0)
            {
                return;
            }
            string fieldNmae = dgvWareHouseList.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("create_time") || fieldNmae.Equals("update_time"))
            {
                long ticks = (long)e.Value;
                e.Value = Common.UtcLongToLocalDateTime(ticks);
            }
            if (fieldNmae.Equals("data_source"))
            {
                DataSources.EnumDataSources enumDataSources = (DataSources.EnumDataSources)Convert.ToInt16(e.Value.ToString());
                e.Value = DataSources.GetDescription(enumDataSources, true);
            }
            if (fieldNmae.Equals("status"))
            {
                DataSources.EnumStatus enumDataSources = (DataSources.EnumStatus)Convert.ToInt16(e.Value.ToString());
                e.Value = DataSources.GetDescription(enumDataSources, true);
            }
            if (fieldNmae.Equals("cargospacenum"))
            {
                if (dt_CargoSpace_Num != null && dt_CargoSpace_Num.Rows.Count > 0)
                {
                    string wh_id = dgvWareHouseList.Rows[e.RowIndex].Cells["wh_id"].Value == null ? "0" : dgvWareHouseList.Rows[e.RowIndex].Cells["wh_id"].Value.ToString();
                    for (int i = 0; i < dt_CargoSpace_Num.Rows.Count; i++)
                    {
                        if (dt_CargoSpace_Num.Rows[i]["wh_id"].ToString() == wh_id)
                        {
                            e.Value = dt_CargoSpace_Num.Rows[i]["Num"];
                            break;
                        }
                    }
                }
                //string wh_id = gvWareHouseList.Rows[e.RowIndex].Cells["wh_id"].Value == null ? "0" : gvWareHouseList.Rows[e.RowIndex].Cells["wh_id"].Value.ToString();
                //DataTable dt_cargo_space = DBHelper.GetTable("查询关联的仓库库位信息", "tb_cargo_space", "*", " wh_id='" + wh_id + "' and enable_flag=1", "", "");
                //e.Value = dt_cargo_space.Rows.Count;
            }
        }

        /// <summary>
        /// 双击列表单元格事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvWareHouseList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
            {
                string wareHouseId = Convert.ToString(this.dgvWareHouseList.CurrentRow.Cells["wh_id"].Value.ToString());
                UCWareHouseView UCWareHouseView = new UCWareHouseView(wareHouseId, this);
                base.addUserControl(UCWareHouseView, "仓库档案-查看", "UCWareHouseView" + wareHouseId + "", this.Tag.ToString(), this.Name);
            }
        }
        private void dgvWareHouseList_HeadCheckChanged()
        {
            SetSelectedStatus();
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
            #region 设置启用/停用
            SetStatus();
            #endregion
            SetMultiBtnStatus(listFiles);
            SetDataSourceBtnStatus(listFiles);
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
            foreach (DataGridViewRow dgvr in dgvWareHouseList.Rows)
            {

                if (Convert.ToBoolean(dgvr.Cells[colCheck.Name].Value))
                {

                    //listFiles.Add(dgvr.Cells[colDataSources.Name].Tag.ToString());
                    string id = dgvr.Cells[wh_id.Name].Value.ToString();
                    listIDs.Add(id);
                    listFiles.Add(dgvr.Cells[data_source.Name].Value.ToString());
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

    }
}
