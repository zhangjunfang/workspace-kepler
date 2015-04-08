using System;
using System.Data;
using System.Windows.Forms;
using SYSModel;
using HXCPcClient.CommonClass;
using Utility.Common;
using HXCPcClient.Chooser;
using ServiceStationClient.ComponentUI;
using System.Drawing.Printing;
using System.Collections.Generic;

namespace HXCPcClient.UCForm.DataManage.Dic
{
    public partial class UCDictionaryManager : UCBase
    {
        #region 字段属性
        BusinessPrint businessPrint;//业务打印功能
        #endregion

        #region 构造和载入
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
            base.ViewEvent += new ClickHandler(UCDictionaryManager_ViewEvent);
            base.PrintEvent += new ClickHandler(UCDictionaryManager_PrintEvent);
            dtpStart.Value = "";
            dtpEnd.Value = "";
            btnSearch_Click(null, null);
            List<string> listNotPrint = new List<string>();
            PaperSize paperSize = new PaperSize();
            paperSize.Width = 360;
            paperSize.Height = 210;
            businessPrint = new BusinessPrint(dgvDicList, "v_dictionaries", "字典码表", paperSize, listNotPrint);
            SetQuick();

        }
        #endregion




        private string where = string.Empty;

        #region 设置速查功能

        /// <summary>
        /// 设置速查功能
        /// </summary>
        private void SetQuick()
        {
            //设置所属公司速查
            txtcreate_by.SetBindTable("sys_user", "user_name");
            txtcreate_by.GetDataSourced += new ServiceStationClient.ComponentUI.TextBox.TextChooser.GetDataSourceHandler(txtBelongCompany_GetDataSourced);
            txtcreate_by.DataBacked += new ServiceStationClient.ComponentUI.TextBox.TextChooser.DataBackHandler(txtBelongCompany_DataBacked);
            txtparent_id.SetBindTable("v_dictionaries", "dic_name");
            txtparent_id.GetDataSourced += new ServiceStationClient.ComponentUI.TextBox.TextChooser.GetDataSourceHandler(txtparent_id_GetDataSourced);
            txtparent_id.DataBacked += new ServiceStationClient.ComponentUI.TextBox.TextChooser.DataBackHandler(txtparent_id_DataBacked);
        }

        void txtparent_id_DataBacked(DataRow dr)
        {
            txtparent_id.Text = dr["dic_name"].ToString();
            txtparent_id.Tag = dr["dic_id"].ToString();
        }

        void txtparent_id_GetDataSourced(ServiceStationClient.ComponentUI.TextBox.TextChooser tc, string sqlString)
        {
            sqlString = string.Format("select * from v_dictionaries where dic_name like '%{0}%' and enable_flag=1 ", txtparent_id.Text);
            DataTable dvt = CommonFuncCall.GetDataSource(sqlString);
            tc.SetDataSource(dvt);
            if (dvt != null)
            {
                tc.Search();
            }
        }

        void txtBelongCompany_GetDataSourced(ServiceStationClient.ComponentUI.TextBox.TextChooser tc, string sqlString)
        {
            sqlString = string.Format("select * from sys_user where user_name like '%{0}%' and enable_flag=1 and status=1", txtcreate_by.Text);
            DataTable dvt = CommonFuncCall.GetDataSource(sqlString);
            tc.SetDataSource(dvt);
            if (dvt != null)
            {
                tc.Search();
            }
        }

        void txtBelongCompany_DataBacked(DataRow dr)
        {
            txtcreate_by.Text = dr["user_name"].ToString();
            txtcreate_by.Tag = dr["user_id"].ToString();
        }

        #endregion

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
            Search();
        }

        private void Search()
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
            if (this.dgvDicList.Rows.Count == 0)
            {
                return;
            }
            try
            {
                string fileName = "字典码表" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
                ExcelHandler.ExportExcel(fileName, ExcelHandler.HandleDataTableForExcel(dgvDicList.GetBoundData(), dgvDicList));
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog("字典码表" + ex.Message, "client");
                MessageBoxEx.ShowWarning("导出失败！");
            }

        }

        void UCDictionaryManager_PrintEvent(object sender, EventArgs e)
        {
            DataTable dtData = dgvDicList.GetBoundData();
            if (dtData != null)
            {
                businessPrint.Print(dtData);
            }
        }

        void UCDictionaryManager_ViewEvent(object sender, EventArgs e)
        {

            DataTable dtData = dgvDicList.GetBoundData();
            if (dtData != null)
            {
                businessPrint.Preview(dtData);
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
