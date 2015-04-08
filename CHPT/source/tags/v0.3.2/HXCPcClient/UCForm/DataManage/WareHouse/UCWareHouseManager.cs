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

namespace HXCPcClient.UCForm.DataManage.WareHouse
{
    public partial class UCWareHouseManager : UCBase
    {
        DataTable dt_CargoSpace_Num = null;

        #region 初始化窗体
        /// <summary>
        /// 初始化窗体方法
        /// </summary>
        public UCWareHouseManager()
        {
            InitializeComponent();
            //禁止列表自增列
            gvWareHouseList.AutoGenerateColumns = false;
            base.AddEvent += new ClickHandler(UCWareHouseManager_AddEvent);
            base.CopyEvent += new ClickHandler(UCWareHouseManager_CopyEvent);
            base.EditEvent += new ClickHandler(UCWareHouseManager_EditEvent);
            base.DeleteEvent += new ClickHandler(UCWareHouseManager_DeleteEvent);

            dateTimeStart.Value = DateTime.Now;
            dateTimeEnd.Value = DateTime.Now;

            BindDllInfo();
            BindgvWareHouseList();
        }
        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCWareHouseManager_Load(object sender, EventArgs e)
        {
            this.gvWareHouseList.ReadOnly = false;
            base.SetBtnStatus(WindowStatus.View);
            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);  //设置查询按钮和清除按钮样式
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
            foreach (DataGridViewRow dr in gvWareHouseList.Rows)
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
                gvWareHouseList.DataSource = gvWareHouse_dt;
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
        { }
        /// <summary>
        /// 预览事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCWareHouseManager_ViewEvent(object sender, EventArgs e)
        { }
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
        /// <summary>
        /// 双击列表单元格事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvWareHouseList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
            {
                string wareHouseId = Convert.ToString(this.gvWareHouseList.CurrentRow.Cells[0].Value.ToString());
                UCWareHouseView UCWareHouseView = new UCWareHouseView(wareHouseId);
                base.addUserControl(UCWareHouseView, "仓库档案-查看", "UCWareHouseView" + wareHouseId + "", this.Tag.ToString(), this.Name);
            }
        }
        #endregion

        private void gvWareHouseList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null || e.Value == string.Empty)
            {
                return;
            }
            string fieldNmae = gvWareHouseList.Columns[e.ColumnIndex].DataPropertyName;
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
                    string wh_id = gvWareHouseList.Rows[e.RowIndex].Cells["wh_id"].Value == null ? "0" : gvWareHouseList.Rows[e.RowIndex].Cells["wh_id"].Value.ToString();
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
    }
}
