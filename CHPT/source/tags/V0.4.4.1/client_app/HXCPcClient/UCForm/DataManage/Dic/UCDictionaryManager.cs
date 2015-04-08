using System;
using System.Data;
using System.Windows.Forms;
using SYSModel;
using HXCPcClient.CommonClass;
using Utility.Common;
using HXCPcClient.Chooser;
using ServiceStationClient.ComponentUI;

namespace HXCPcClient.UCForm.DataManage.Dic
{
    public partial class UCDictionaryManager : UCBase
    {
        public UCDictionaryManager()
        {
            InitializeComponent();
        }

        private void UCDictionaryManager_Load(object sender, EventArgs e)
        {
            base.RoleButtonStstus(this.Name);
            DataGridViewEx.SetDataGridViewStyle(dgvDicList);
            DataSources.BindComBoxDataEnum(cobDataSources, typeof(DataSources.EnumDataSources), true);//数据来源
            base.btnCopy.Visible = false;
            base.btnStatus.Visible = false;
            base.btnAdd.Visible = false;
            base.btnEdit.Visible = false;
            base.btnSave.Visible = false;
            base.btnCancel.Visible = false;
            base.btnDelete.Visible = false;
            base.SetBtnStatus(WindowStatus.View);
            base.ExportEvent += new ClickHandler(UCDictionaryManager_ExportEvent);
            dtpStart.Value = "";
            dtpEnd.Value = "";
            btnSearch_Click(null, null);
        }
        private string where = string.Empty;

        #region 事件
        /// <summary> 选择码表
        /// </summary>
        private void txtparent_id_ChooserClick(object sender, EventArgs e)
        {
            try
            {
                frmDictionaries frm = new frmDictionaries();
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtparent_id.Text = frm.Dic_Name;
                    txtparent_id.Tag = frm.Dic_ID;
                }
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog("【字典码表】" + ex.Message, "client");
                MessageBoxEx.ShowWarning("操作失败！");
            }
        }

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

        /// <summary> 查询
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                where = string.Format("is_class='1' and enable_flag='1'");
                if (!string.IsNullOrEmpty(txtdic_code.Caption.Trim()))
                {
                    where += string.Format(" and  dic_code like '%{0}%'", txtdic_code.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtdic_name.Caption.Trim()))
                {
                    where += string.Format(" and  dic_name like '%{0}%'", txtdic_name.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtparent_id.Text))
                {
                    where += string.Format(" and  (dic_id = '{0}' or parent_id='{0}')", txtparent_id.Tag.ToString());
                }
                if (!string.IsNullOrEmpty(txtcreate_by.Text))
                {
                    where += string.Format(" and  create_by = '{0}'", txtcreate_by.Tag.ToString());
                }
                if (cobDataSources.SelectedIndex > 0)//数据来源
                {
                    where += string.Format(" and  data_source = '{0}'", cobDataSources.SelectedValue);
                }
                if (!string.IsNullOrEmpty(dtpStart.Value))
                {
                    long startTicks = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dtpStart.Value));
                    where += " and create_time>=" + startTicks.ToString();
                }
                if (!string.IsNullOrEmpty(dtpEnd.Value))
                {
                    long endTicks = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dtpEnd.Value).AddDays(1));
                    where += " and create_time<" + endTicks.ToString();
                }
                page.PageIndex = 1;
                BindPageData(where);
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog("【字典码表】" + ex.Message, "client");
                MessageBoxEx.ShowWarning("操作失败！");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtdic_code.Caption = string.Empty;
                txtdic_name.Caption = string.Empty;
                txtparent_id.Text = string.Empty;
                txtparent_id.Tag = null;
                txtcreate_by.Text = string.Empty;
                txtcreate_by.Tag = null;
                dtpStart.Value = "";
                dtpEnd.Value = "";
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog("【字典码表】" + ex.Message, "client");
                MessageBoxEx.ShowWarning("操作失败！");
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

        /// <summary> 双击新增/复制/编辑字典内容
        /// </summary>
        private void dgvDicList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
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
                catch (Exception ex)
                {
                    Utility.Log.Log.writeLineToLog("【字典码表】" + ex.Message, "client");
                    MessageBoxEx.ShowWarning("操作失败！");
                }
            }
        }

        /// <summary> 格式化
        /// </summary>
        private void dgvDicList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.Value == null || string.IsNullOrEmpty(e.Value.ToString()))
                {
                    return;
                }
                string fieldNmae = dgvDicList.Columns[e.ColumnIndex].DataPropertyName;
                if (fieldNmae.Equals("create_time") || fieldNmae.Equals("update_time"))
                {
                    long ticks = (long)e.Value;
                    e.Value = Common.UtcLongToLocalDateTime(ticks);
                }
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

        /// <summary> 导出
        /// </summary>
        void UCDictionaryManager_ExportEvent(object sender, EventArgs e)
        {
            try
            {
                //获取数据表
                DataTable dt = DBHelper.GetTable("查询码表", "v_dictionaries", "*", where, "", "order by dic_code");
                //根据datagridview列制定datatable导出列及列名称
                dt = ExcelHandler.HandleDataTableForExcel(dt, dgvDicList);
                string fileName = "字典码表" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
                ExcelHandler.ExportExcel(fileName, dt);
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog("【字典码表】" + ex.Message, "client");
                MessageBoxEx.ShowWarning("导出失败！");
            }
        }
        #endregion

        #region 方法
        /// <summary> 绑定数据
        /// </summary>
        private void BindPageData(string where)
        {
            int recordCount;
            DataTable dt = DBHelper.GetTableByPage("分页查询码表", "v_dictionaries", "*", where, "", "order by dic_code", page.PageIndex, page.PageSize, out recordCount);
            dgvDicList.DataSource = dt;
            page.RecordCount = recordCount;
            page.SetBtnState();
        }
        #endregion
    }
}
