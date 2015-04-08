using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using HXCPcClient.CommonClass;
using Utility.Common;
using HXCPcClient.Chooser;
using SYSModel;
using System.IO;
using Utility.CommonForm;

namespace HXCPcClient.UCForm.DataManage.Dic
{
    public partial class UCDictionaryAddOrEdit : UCBase
    {
        #region 字段属性
        /// <summary> 查询条件
        /// </summary>
        string where = string.Empty;

        #region 外部属性
        /// <summary> 父编码表id
        /// </summary>
        public string ParentId { get; set; }
        /// <summary> 父编码表编码
        /// </summary>
        public string ParentCode { get; set; }
        /// <summary> 父编码表名称
        /// </summary>
        public string ParentName { get; set; }

        #endregion

        /// <summary> 操作行索引
        /// </summary>
        private int editRowIndex = -1;

        #endregion

        #region 构造和载入函数

        public UCDictionaryAddOrEdit()
        {
            InitializeComponent();
        }

        private void UCDictionaryAddOrEdit_Load(object sender, EventArgs e)
        {
            SetDefaultBtn();
            DataGridViewEx.SetDataGridViewStyle(dgvDicList);
            DataSources.BindComBoxDataEnum(cobDataSources, typeof(DataSources.EnumDataSources), true);//数据来源
            tpDicList.Text = string.Format("  {0}({1})字典内容  ", ParentName, ParentCode);
            base.AddEvent += new ClickHandler(UCDictionaryAddOrEdit_AddEvent);
            base.EditEvent += new ClickHandler(UCDictionaryAddOrEdit_EditEvent);
            base.SaveEvent += new ClickHandler(UCDictionaryAddOrEdit_SaveEvent);
            base.DeleteEvent += new ClickHandler(UCDictionaryAddOrEdit_DeleteEvent);
            base.CancelEvent += new ClickHandler(UCDictionaryAddOrEdit_CancelEvent);
            base.ExportEvent += new ClickHandler(UCDictionaryAddOrEdit_ExportEvent);
            BindCmb();
            dgvDicList_CellClick(null, null);
            SetQuick();
        }

        #endregion

        #region 设置按钮状态
        private void SetDefaultBtn()
        {
            base.RoleButtonStstus("CL_DataManagement_DictionaryTable");
            this.dgvDicList.ReadOnly = false;
            base.btnCopy.Visible = false;
            base.btnStatus.Visible = false;
            base.SetBtnStatus(WindowStatus.View);

        }
        #endregion

        #region 设置速查功能

        /// <summary>
        /// 设置速查功能
        /// </summary>
        private void SetQuick()
        {

            txtcreate_by.SetBindTable("sys_user", "user_name");
            txtcreate_by.GetDataSourced += new ServiceStationClient.ComponentUI.TextBox.TextChooser.GetDataSourceHandler(txtcreate_by_GetDataSourced);
            txtcreate_by.DataBacked += new ServiceStationClient.ComponentUI.TextBox.TextChooser.DataBackHandler(txtcreate_by_DataBacked);

        }
        void txtcreate_by_GetDataSourced(ServiceStationClient.ComponentUI.TextBox.TextChooser tc, string sqlString)
        {
            sqlString = string.Format("select * from sys_user where user_name like '%{0}%' and enable_flag=1 and status=1", txtcreate_by.Text);
            DataTable dvt = CommonFuncCall.GetDataSource(sqlString);
            tc.SetDataSource(dvt);
            if (dvt != null)
            {
                tc.Search();
            }
        }

        void txtcreate_by_DataBacked(DataRow dr)
        {
            txtcreate_by.Text = dr["user_name"].ToString();
            txtcreate_by.Tag = dr["user_id"].ToString();
        }

        #endregion
        #region 事件
        /// <summary> 查询
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (editRowIndex >= 0)
                {
                    MessageBoxEx.ShowWarning("编辑过程中不能进行查询操作");
                    return;
                }
                where = "";
                page.PageIndex = 1;
                BindPageData(where);
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog("【字典码表】" + ex.Message, "client");
                MessageBoxEx.ShowWarning("操作失败！");
            }
        }
        /// <summary> 清空查询
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtdic_code.Caption = string.Empty;
            txtdic_name.Caption = string.Empty;
            txtcreate_by.Text = string.Empty;
            txtcreate_by.Tag = null;
        }

        /// <summary> 选择创建人
        /// </summary>
        private void txtcreate_by_ChooserClick(object sender, EventArgs e)
        {
            try
            {
                frmUsers frm = new frmUsers();
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtcreate_by.Text = frm.User_Name;
                    txtcreate_by.Tag = frm.User_ID;
                }
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog("【字典码表】" + ex.Message, "client");
                MessageBoxEx.ShowWarning("操作失败！");
            }
        }

        /// <summary> 新增
        /// </summary>
        void UCDictionaryAddOrEdit_AddEvent(object sender, EventArgs e)
        {
            try
            {
                editRowIndex = 0;
                if (dgvDicList.CurrentRow != null)
                {
                    editRowIndex = dgvDicList.CurrentRow.Index;
                }
                dgvDicList.Rows.Insert(editRowIndex, 1);
                SetDefaultRowValue();
                CommonUtility.SetDgvEditCellBgColor(dgvDicList.Rows[editRowIndex], new string[] { "dic_name", "is_class", "remark" }, true);
                base.SetBtnStatus(WindowStatus.Add);
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog("【字典码表】" + ex.Message, "client");
                MessageBoxEx.ShowWarning("操作失败！");
            }
        }

        /// <summary> 编辑
        /// </summary>
        void UCDictionaryAddOrEdit_EditEvent(object sender, EventArgs e)
        {
            try
            {
                if (dgvDicList.CurrentRow == null)
                {
                    MessageBoxEx.ShowInformation("请选择编辑记录！");
                    return;
                }
                editRowIndex = dgvDicList.CurrentRow.Index;
                CommonUtility.SetDgvEditCellBgColor(dgvDicList.Rows[editRowIndex], new string[] { "dic_name", "is_class", "remark" }, true);
                base.SetBtnStatus(WindowStatus.Edit);
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog("【字典码表】" + ex.Message, "client");
                MessageBoxEx.ShowWarning("操作失败！");
            }
        }

        /// <summary> 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCDictionaryAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            try
            {
                dgvDicList.EndEdit();
                if (editRowIndex < 0)
                {
                    return;
                }
                if (!ValidateValue())
                {
                    return;
                }
                bool flag = SaveRecord();
                if (flag)
                {
                    CommonUtility.SetDgvEditCellBgColor(dgvDicList.Rows[editRowIndex], new string[] { "dic_name", "is_class", "remark" }, false);
                    dgvDicList.CurrentCell = dgvDicList.Rows[editRowIndex].Cells[0];
                    base.SetBtnStatus(WindowStatus.Save);
                    editRowIndex = -1;
                    MessageBoxEx.ShowInformation("操作成功！");
                    SetDefaultBtn();
                    DataGridViewEx.SetDataGridViewStyle(dgvDicList);
                }
                else
                {
                    MessageBoxEx.ShowWarning("操作失败！");
                }
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog("【字典码表】" + ex.Message, "client");
                MessageBoxEx.ShowWarning("操作失败！");
            }
        }

        /// <summary> 删除
        /// </summary>
        void UCDictionaryAddOrEdit_DeleteEvent(object sender, EventArgs e)
        {
            try
            {
                List<string> listField = new List<string>();
                foreach (DataGridViewRow dr in dgvDicList.Rows)
                {
                    object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                    if (isCheck != null && (bool)isCheck)
                    {
                        listField.Add(dr.Cells["dic_id"].Value.ToString());
                    }
                }
                if (listField.Count == 0)
                {
                    MessageBoxEx.ShowInformation("请选择删除记录！");
                    return;
                }
                if (!MessageBoxEx.ShowQuestion("将要删除选中字典码表，是否继续？"))
                {
                    return;
                }
                Dictionary<string, string> dicField = new Dictionary<string, string>();
                dicField.Add("enable_flag", "0");
                bool flag = DBHelper.BatchUpdateDataByIn("批量删除字典码表", "sys_dictionaries", dicField, "dic_id", listField.ToArray());
                if (flag)
                {
                    BindPageData(where);
                    if (dgvDicList.Rows.Count > 0)
                    {
                        dgvDicList.CurrentCell = dgvDicList.Rows[0].Cells[0];
                    }
                    MessageBoxEx.ShowInformation("操作成功！");
                }
                else
                {
                    MessageBoxEx.ShowWarning("操作失败！");
                }
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog("【字典码表】" + ex.Message, "client");
                MessageBoxEx.ShowWarning("操作失败！");
            }
        }

        /// <summary> 取消
        /// </summary>
        void UCDictionaryAddOrEdit_CancelEvent(object sender, EventArgs e)
        {
            try
            {
                if (editRowIndex < 0 || dgvDicList.Rows.Count == 0)
                {
                    return;
                }
                BindPageData(where);
                if (dgvDicList.Rows.Count > 0)
                {
                    dgvDicList.CurrentCell = dgvDicList.Rows[editRowIndex].Cells[0];
                }
                base.SetBtnStatus(WindowStatus.Cancel);
                editRowIndex = -1;
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog("【字典码表】" + ex.Message, "client");
                MessageBoxEx.ShowWarning("操作失败！");
            }
        }

        void UCDictionaryAddOrEdit_ExportEvent(object sender, EventArgs e)
        {
            try
            {
                string dir = Application.StartupPath + @"\ExportFile";
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.InitialDirectory = dir;
                sfd.Title = "导出文件";
                sfd.DefaultExt = "xls";
                sfd.Filter = "Microsoft Office Excel 文件(*.xls;*.xlsx)|*.xls;*.xlsx|Microsoft Office Excel 文件(*.xls)|*.xls|Microsoft Office Excel 文件(*.xlsx)|*.xlsx";
                sfd.FileName = dir + @"\字典码表" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
                DialogResult result = sfd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    int totalCount = 0;
                    PercentProcessOperator process = new PercentProcessOperator();
                    #region 匿名方法，后台线程执行完调用
                    process.BackgroundWork =
                        delegate(Action<int> percent)
                        {
                            DataTable dt = DBHelper.GetTable("查询码表", "v_dictionaries", "*", where, "", "order by dic_code");
                            totalCount = dt.Rows.Count;
                            dt = ExcelHandler.HandleDataTableForExcel(dt, dgvDicList);
                            ExcelHandler.ExportDTtoExcel(dt, "", sfd.FileName, percent);
                        };
                    #endregion
                    process.MessageInfo = "正在执行中";
                    process.Maximum = totalCount;
                    #region 匿名方法，后台线程执行完调用
                    process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(
                            delegate(object osender, BackgroundWorkerEventArgs be)
                            {
                                if (be.BackGroundException == null)
                                {
                                    MessageBoxEx.ShowInformation("导出成功！");
                                }
                                else
                                {
                                    Utility.Log.Log.writeLineToLog("【字典码表】" + be.BackGroundException.Message, "client");
                                    MessageBoxEx.ShowWarning("导出出现异常");
                                }
                            }
                        );
                    #endregion
                    process.Start();
                }
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog("【字典码表】" + ex.Message, "client");
                MessageBoxEx.ShowWarning("导出失败！");
            }
        }

        /// <summary> 页码改变事件
        /// </summary>
        private void page_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindPageData(where);
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog("【字典码表】" + ex.Message, "client");
                MessageBoxEx.ShowWarning("操作失败！");
            }
        }

        /// <summary> 单击事件
        /// </summary>
        private void dgvDicList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (editRowIndex < 0 && dgvDicList.CurrentRow != null)
                {
                    object obj = dgvDicList.CurrentRow.Cells["data_source"].Value;
                    if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                    {
                        return;
                    }

                    DataSources.EnumDataSources enumDataSources = (DataSources.EnumDataSources)Convert.ToInt32(obj.ToString());
                    bool flag = enumDataSources == DataSources.EnumDataSources.SELFBUILD;
                    Func<bool, bool> fc = delegate(bool status)
                    {
                        base.btnAdd.Enabled = status;
                        base.btnEdit.Enabled = status;
                        base.btnDelete.Enabled = status;
                        return true;
                    };
                    this.BeginInvoke(fc, new object[] { flag });
                }
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog("【字典码表】" + ex.Message, "client");
                MessageBoxEx.ShowWarning("操作失败！");
            }
        }

        /// <summary> 控制批号单元格编辑
        /// </summary>
        private void dgvDicList_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)//复选框列可编辑
                {
                    return;
                }
                //判断是否编辑列
                if (e.RowIndex != editRowIndex)
                {
                    e.Cancel = true;
                }
                string field = dgvDicList.Columns[e.ColumnIndex].DataPropertyName;
                if (field != "is_class" && field != "dic_name" && field != "remark")//可编辑字段
                {
                    // 取消编辑 
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog("【字典码表】" + ex.Message, "client");
                MessageBoxEx.ShowWarning("操作失败！");
            }
        }

        /// <summary> 格式化
        /// </summary>
        private void dgvDicList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
            try
            {
                string fieldNmae = dgvDicList.Columns[e.ColumnIndex].DataPropertyName;
                if (fieldNmae.Equals("enable_flag"))
                {
                    DataSources.EnumEnableFlag enumEnableFlag = (DataSources.EnumEnableFlag)Convert.ToInt16(e.Value.ToString());
                    e.Value = DataSources.GetDescription(enumEnableFlag, true);
                }
                if (fieldNmae.Equals("data_source"))
                {
                    DataSources.EnumDataSources enumDataSources = (DataSources.EnumDataSources)Convert.ToInt16(e.Value.ToString());
                    e.Value = DataSources.GetDescription(enumDataSources, true);
                }
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog("【字典码表】" + ex.Message, "client");
            }
        }

        private void dgvDicList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && editRowIndex < 0)
                {
                    object obj = dgvDicList.Rows[e.RowIndex].Cells["is_class"].Value;
                    if (obj != null && obj.ToString() == DataSources.EnumYesNo.Yes.ToString("d"))
                    {
                        string dic_id = dgvDicList.Rows[e.RowIndex].Cells["dic_id"].Value.ToString();
                        string dic_code = dgvDicList.Rows[e.RowIndex].Cells["dic_code"].Value.ToString();
                        string dic_name = dgvDicList.Rows[e.RowIndex].Cells["dic_name"].Value.ToString();
                        UCDictionaryAddOrEdit uc = new UCDictionaryAddOrEdit();
                        uc.ParentId = dic_id;
                        uc.ParentCode = dic_code;
                        uc.ParentName = dic_name;
                        base.addUserControl(uc, "字典码表-" + dic_name, "UCDictionaryManagerOperator" + dic_code, this.Tag.ToString(), this.Name);
                    }
                    else
                    {
                        UCDictionaryAddOrEdit_EditEvent(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog("【字典码表】" + ex.Message, "client");
                MessageBoxEx.ShowWarning("操作失败！");
            }
        }

        #endregion

        #region 方法
        /// <summary> 保存验证
        /// </summary>
        /// <returns></returns>
        public bool ValidateValue()
        {
            DataGridViewRow row = dgvDicList.Rows[editRowIndex];
            object objName = row.Cells["dic_name"].Value;
            if (objName == null || string.IsNullOrEmpty(objName.ToString()))
            {
                MessageBoxEx.Show("码表名称不能为空！");
                dgvDicList.CurrentCell = row.Cells["dic_name"];
                return false;
            }
            string isClass = row.Cells["is_class"].Value.ToString();
            if (string.IsNullOrEmpty(isClass))
            {
                MessageBoxEx.Show("请选择码表类别！");
                dgvDicList.CurrentCell = row.Cells["is_class"];
                return false;
            }
            return true;
        }

        /// <summary> 绑定Combox
        /// </summary>
        private void BindCmb()
        {
            List<ListItem> listYesNo = DataSources.EnumToList(typeof(DataSources.EnumYesNo), false);
            is_class.DataSource = listYesNo;
            is_class.DisplayMember = "Text";
            is_class.ValueMember = "Value";
        }

        /// <summary> 绑定数据
        /// </summary>
        private void BindPageData(string where)
        {
            int recordCount;
            where = string.Format("parent_id='{0}' and enable_flag='1'", this.ParentId);
            if (!string.IsNullOrEmpty(txtdic_code.Caption.Trim()))
            {
                where += string.Format(" and  dic_code like '%{0}%'", txtdic_code.Caption.Trim());
            }
            if (!string.IsNullOrEmpty(txtdic_name.Caption.Trim()))
            {
                where += string.Format(" and  dic_name like '%{0}%'", txtdic_name.Caption.Trim());
            }
            if (!string.IsNullOrEmpty(txtcreate_by.Text))
            {
                where += string.Format(" and  create_by = '{0}'", txtcreate_by.Tag.ToString());
            }
            if (!string.IsNullOrEmpty(dtpStart.Value))
            {
                long startTicks = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dtpStart.Value));
                where += " and create_time>=" + startTicks.ToString();
            }
            if (cobDataSources.SelectedIndex > 0)//数据来源
            {
                where += string.Format(" and  data_source = '{0}'", cobDataSources.SelectedValue.ToString());
            }
            if (!string.IsNullOrEmpty(dtpEnd.Value))
            {
                long endTicks = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dtpEnd.Value).AddDays(1));
                where += " and create_time<" + endTicks.ToString();
            }
            DataTable dt = DBHelper.GetTableByPage("分页查询码表", "v_dictionaries", "*", where, "", "order by dic_code", page.PageIndex, page.PageSize, out recordCount);
            BindDgv(dt);
            page.RecordCount = recordCount;
            page.SetBtnState();
            dgvDicList_CellClick(null, null);
        }

        /// <summary> 新增时设置默认数据
        /// </summary>
        private void SetDefaultRowValue()
        {
            DataGridViewRow row = dgvDicList.Rows[editRowIndex];
            row.Cells["dic_id"].Value = Guid.NewGuid().ToString();
            row.Cells["dic_code"].Value = CommonUtility.GetNewNo(DataSources.EnumProjectType.Dictionaries);
            //row.Cells["dic_name"].Value = "";
            row.Cells["parent_id"].Value = ParentId;
            row.Cells["parent_code"].Value = ParentCode;
            row.Cells["parent_name"].Value = ParentName;
            row.Cells["is_class"].Value = 0;
            row.Cells["data_source"].Value = (int)SYSModel.DataSources.EnumDataSources.SELFBUILD;
            row.Cells["enable_flag"].Value = "1";
            row.Cells["create_name"].Value = GlobalStaticObj.UserName;
            DateTime nowTime = GlobalStaticObj.CurrentDateTime;
            row.Cells["create_time"].Value = nowTime;
            row.Cells["update_name"].Value = GlobalStaticObj.UserName;
            row.Cells["update_time"].Value = nowTime;
        }

        /// <summary> 添加字典码
        /// </summary>
        private bool SaveRecord()
        {
            string keyName = string.Empty;
            string keyValue = string.Empty;
            string opName = "新增字典码";
            DataGridViewRow row = dgvDicList.Rows[editRowIndex];
            Dictionary<string, string> dicFileds = new Dictionary<string, string>();
            DateTime dt = Convert.ToDateTime(row.Cells["create_time"].Value.ToString());
            string nowTicks = Common.LocalDateTimeToUtcLong(dt).ToString();
            if (base.windowStatus == WindowStatus.Add)
            {
                dicFileds.Add("dic_id", row.Cells["dic_id"].Value.ToString());
                dicFileds.Add("dic_code", row.Cells["dic_code"].Value.ToString());
                dicFileds.Add("parent_id", this.ParentId);
                int datasorces = (int)SYSModel.DataSources.EnumDataSources.SELFBUILD;
                dicFileds.Add("data_source", datasorces.ToString());
                dicFileds.Add("enable_flag", row.Cells["enable_flag"].Value.ToString());
                dicFileds.Add("create_by", GlobalStaticObj.UserID);
                dicFileds.Add("create_time", nowTicks);
            }
            dicFileds.Add("dic_name", row.Cells["dic_name"].Value.ToString());
            dicFileds.Add("is_class", row.Cells["is_class"].Value.ToString());
            string remark = row.Cells["remark"].Value == null ? "" : row.Cells["remark"].Value.ToString();
            dicFileds.Add("remark", remark);
            dicFileds.Add("update_time", nowTicks);
            dicFileds.Add("update_by", GlobalStaticObj.UserID);
            if (base.windowStatus == WindowStatus.Edit)
            {
                keyName = "dic_id";
                keyValue = row.Cells["dic_id"].Value.ToString();
                opName = "更新字典码";
            }
            return DBHelper.Submit_AddOrEdit(opName, "sys_dictionaries", keyName, keyValue, dicFileds);
        }

        /// <summary> 绑定字典
        /// </summary>
        /// <param name="dt">字典数据表</param>
        private void BindDgv(DataTable dt)
        {
            dgvDicList.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                int rowIndex = dgvDicList.Rows.Add(1);
                dgvDicList.Rows[rowIndex].Cells["dic_id"].Value = dr["dic_id"].ToString();
                dgvDicList.Rows[rowIndex].Cells["dic_code"].Value = dr["dic_code"].ToString();
                dgvDicList.Rows[rowIndex].Cells["dic_name"].Value = dr["dic_name"].ToString();
                dgvDicList.Rows[rowIndex].Cells["parent_id"].Value = dr["parent_id"].ToString();
                dgvDicList.Rows[rowIndex].Cells["parent_code"].Value = ParentCode;
                dgvDicList.Rows[rowIndex].Cells["parent_name"].Value = ParentName;
                dgvDicList.Rows[rowIndex].Cells["is_class"].Value = Convert.ToInt32(dr["is_class"].ToString());
                dgvDicList.Rows[rowIndex].Cells["data_source"].Value = dr["data_source"].ToString();
                dgvDicList.Rows[rowIndex].Cells["remark"].Value = dr["remark"].ToString();
                dgvDicList.Rows[rowIndex].Cells["enable_flag"].Value = dr["enable_flag"].ToString();
                dgvDicList.Rows[rowIndex].Cells["create_name"].Value = dr["create_name"].ToString();
                dgvDicList.Rows[rowIndex].Cells["update_name"].Value = dr["update_name"].ToString();
                long ticksCreate = Convert.ToInt64(dr["create_time"].ToString());
                dgvDicList.Rows[rowIndex].Cells["create_time"].Value = Common.UtcLongToLocalDateTime(ticksCreate);
                long ticksUpdate = Convert.ToInt64(dr["create_time"].ToString());
                dgvDicList.Rows[rowIndex].Cells["update_time"].Value = Common.UtcLongToLocalDateTime(ticksUpdate);
            }
        }

        #endregion
    }
}