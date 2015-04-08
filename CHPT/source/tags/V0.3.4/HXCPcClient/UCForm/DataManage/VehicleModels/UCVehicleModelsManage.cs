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
using System.Collections.ObjectModel;

namespace HXCPcClient.UCForm.DataManage.VehicleModels
{
    public partial class UCVehicleModelsManage : UCBase
    {
        #region 属性
        List<string> listIDs = new List<string>();//已选择ID列表
        List<string> listStart = new List<string>();//启用状态
        List<string> listStop = new List<string>();//停用状态
        /// <summary>
        /// 启用停用状态
        /// </summary>
        DataSources.EnumStatus enumStatus;
        /// <summary>
        /// 车型ID
        /// </summary>
        private string VM_ID
        {
            get
            {
                //if (dgvVehicleModels.CurrentRow == null)
                //{
                //    return string.Empty;
                //}
                //object vm_id = dgvVehicleModels.CurrentRow.Cells["colVM_ID"].Value;
                //if (vm_id == null)
                //{
                //    return string.Empty;
                //}
                //else
                //{
                //    return vm_id.ToString();
                //}
                if (listIDs.Count == 1)
                {
                    return listIDs[0];
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        #endregion
        public UCVehicleModelsManage()
        {
            InitializeComponent();
            base.AddEvent += new ClickHandler(UCVehicleModelsManage_AddEvent);
            base.EditEvent += new ClickHandler(UCVehicleModelsManage_EditEvent);
            base.ViewEvent += new ClickHandler(UCVehicleModelsManage_ViewEvent);
            base.DeleteEvent += new ClickHandler(UCVehicleModelsManage_DeleteEvent);
            base.CopyEvent += new ClickHandler(UCVehicleModelsManage_CopyEvent);
            base.StatusEvent += new ClickHandler(UCVehicleModelsManage_StatusEvent);
            base.CancelEvent += new ClickHandler(UCVehicleModelsManage_CancelEvent);
            dgvVehicleModels.ReadOnly = false;
            dgvVehicleModels.HeadCheckChanged += new DataGridViewEx.DelegateOnClick(dgvVehicleModels_HeadCheckChanged);
            foreach (DataGridViewColumn dgvc in dgvVehicleModels.Columns)
            {
                if (dgvc == colCheck)
                {
                    continue;
                }
                dgvc.ReadOnly = true;
            }
        }
        //加载
        private void UCVehicleModelsManage_Load(object sender, EventArgs e)
        {
            BindSearch();
            #region 初始化右键的权限
            tmsiView.Visible = btnView.Visible;
            tsmiEdit.Visible = btnEdit.Visible;
            tsmiDelete.Visible = btnDelete.Visible;
            tsmiCopy.Visible = btnCopy.Visible;
            #endregion
            btnEdit.Enabled = false;
            btnCopy.Enabled = false;
            btnView.Enabled = false;
            btnDelete.Enabled = false;
            btnStatus.Enabled = false;
            tsmiCopy.Enabled = false;
            tsmiDelete.Enabled = false;
            tsmiEdit.Enabled = false;
            tmsiView.Enabled = false;
            //设置页面按钮可见性
            var btnCols = new ObservableCollection<ButtonEx_sms>
            {
                btnAdd, btnCopy, btnEdit, btnDelete, btnStatus, btnCancel, btnExport, btnImport, btnPrint,btnSet
            };
            UIAssistants.SetUCBaseFuncationVisible(this, btnCols);
            btnStatus.Enabled = false;
            diCreate.StartDate = DateTime.Now.AddYears(-1).ToString(diCreate.customFormat);
            diCreate.EndDate = DateTime.Now.ToString(diCreate.customFormat);
            BindData();
        }
        #region 工具栏事件
        //取消事件
        void UCVehicleModelsManage_CancelEvent(object sender, EventArgs e)
        {
            deleteMenuByTag(this.Tag.ToString(), this.Name);
        }
        //启停用事件
        void UCVehicleModelsManage_StatusEvent(object sender, EventArgs e)
        {
            List<SysSQLString> listSql = new List<SysSQLString>();
            Dictionary<string, string> dicStatus = new Dictionary<string, string>();
            if (enumStatus == DataSources.EnumStatus.Start)
            {
                if (!MessageBoxEx.ShowQuestion("确定要停用吗？"))
                {
                    btnStatus.Caption = "启用";               
                    return;
                }
                dicStatus.Add("status", ((int)DataSources.EnumStatus.Stop).ToString());
                if (DBHelper.BatchUpdateDataByIn("停用车型", "tb_vehicle_models", dicStatus, "vm_id", listStart.ToArray()))
                {
                    MessageBoxEx.ShowInformation("停用成功！");
                    BindData();
                }
                else
                {
                    btnStatus.Caption = "启用";     
                    MessageBoxEx.ShowError("停用失败！");
                }
            }
            else if (enumStatus == DataSources.EnumStatus.Stop)
            {
                if (!MessageBoxEx.ShowQuestion("确定要启用吗？"))
                {
                    btnStatus.Caption = "停用";
                    return;
                }
                dicStatus.Add("status", ((int)DataSources.EnumStatus.Start).ToString());
                if (DBHelper.BatchUpdateDataByIn("启用车型", "tb_vehicle_models", dicStatus, "vm_id", listStop.ToArray()))
                {
                    MessageBoxEx.ShowInformation("启用成功！");
                    BindData();
                }
                else
                {
                    btnStatus.Caption = "停用";
                    MessageBoxEx.ShowError("启用失败！");
                }
            }

        }
        
        //复制事件
        void UCVehicleModelsManage_CopyEvent(object sender, EventArgs e)
        {
            Action<WindowStatus> action = EditData;
            action.Invoke(WindowStatus.Copy);
        }
        
        //删除事件
        void UCVehicleModelsManage_DeleteEvent(object sender, EventArgs e)
        {
            Action action = DeleteData;
            action.Invoke();
        }
        //查看事件
        void UCVehicleModelsManage_ViewEvent(object sender, EventArgs e)
        {
            ViewData();
        }
        //编辑事件
        void UCVehicleModelsManage_EditEvent(object sender, EventArgs e)
        {
            EditData(WindowStatus.Edit);
        }
        //新增事件
        void UCVehicleModelsManage_AddEvent(object sender, EventArgs e)
        {
            AddData();
        }
        #endregion

        #region 方法

        // 查询数据
        public void BindData()
        {
            StringBuilder sbWhere = new StringBuilder();//条件
            sbWhere.AppendFormat("enable_flag='{0}'", (int)DataSources.EnumEnableFlag.USING);//查询标记未删除的数据
            string dataSource = CommonCtrl.IsNullToString(cboDataSource.SelectedValue);
            if (dataSource.Length > 0)
            {
                sbWhere.AppendFormat(" and data_source='{0}'", dataSource);
            }
            if (cboBrand.SelectedValue != null && cboBrand.SelectedValue.ToString().Length > 0)
            {
                sbWhere.AppendFormat(" and v_brand='{0}'", cboBrand.SelectedValue);
            }
            if (cboType.SelectedValue != null && cboType.SelectedValue.ToString().Length > 0)
            {
                sbWhere.AppendFormat(" and vm_type='{0}'", cboType.SelectedValue);
            }
            if (cboStatus.SelectedValue != null && cboStatus.SelectedValue.ToString().Length > 0)
            {
                sbWhere.AppendFormat(" and status='{0}'", cboStatus.SelectedValue);
            }
            if (txtCode.Caption.Length > 0)
            {
                sbWhere.AppendFormat(" and vm_code like '%{0}%'", txtCode.Caption);
            }
            if (txtName.Caption.Length > 0)
            {
                sbWhere.AppendFormat(" and vm_name like '%{0}%'", txtName.Caption);
            }
            if (txtCreateUser.Caption.Length > 0)
            {
                sbWhere.AppendFormat(" and create_by_name like '%{0}%'", txtCreateUser.Caption);
            }
            sbWhere.AppendFormat(" and create_time between {0} and {1}", Common.LocalDateTimeToUtcLong(DateTime.Parse(diCreate.StartDate).Date),
               Common.LocalDateTimeToUtcLong(DateTime.Parse(diCreate.EndDate).Date.AddDays(1).AddMilliseconds(-1)));
            int recordCount;//总数量
            DataTable dt = DBHelper.GetTableByPage("查询车型", "v_vehicle_models", "*", sbWhere.ToString(), "", "order by create_time desc", page.PageIndex, page.PageSize, out recordCount);
            //dgvVehicleModels.DataSource = dt;
            dgvVehicleModels.RowCount = 0;
            foreach (DataRow dr in dt.Rows)
            {
                DataGridViewRow dgvr = dgvVehicleModels.Rows[dgvVehicleModels.Rows.Add()];
                dgvr.Cells["colDataSource"].Value = DataSources.GetDescription(typeof(SYSModel.DataSources.EnumDataSources), dr["data_source"]);
                dgvr.Cells[colDataSource.Name].Tag = dr["data_source"];
                dgvr.Cells["colBrand"].Value = dr["v_brand_name"];
                dgvr.Cells["colType"].Value = dr["vm_type_name"];
                dgvr.Cells["colCode"].Value = dr["vm_code"];
                dgvr.Cells["colName"].Value = dr["vm_name"];
                dgvr.Cells["colCreateUser"].Value = dr["create_by_name"];
                if (dr["create_time"] != null && dr["create_time"] != DBNull.Value)
                {
                    dgvr.Cells["colCreateDate"].Value = Common.UtcLongToLocalDateTime(Convert.ToInt64(dr["create_time"]));
                }
                dgvr.Cells["colStatus"].Value = DataSources.GetDescription(typeof(DataSources.EnumStatus), dr["status"]);
                dgvr.Cells[colStatus.Name].Tag = dr["status"];
                dgvr.Cells["colRemark"].Value = dr["remark"];
                dgvr.Cells["colVM_ID"].Value = dr["vm_id"];
            }
            page.RecordCount = recordCount;
            page.SetBtnState();
        }
        /// <summary>
        /// 绑定数据后定位到指定车型
        /// </summary>
        /// <param name="vmID">车型ID</param>
        public void BindData(string vmID)
        {
            BindData();
            foreach (DataGridViewRow dgvr in dgvVehicleModels.Rows)
            {
                if (CommonCtrl.IsNullToString(dgvr.Cells[colVM_ID.Name].Value) == vmID)
                {
                    dgvVehicleModels.CurrentCell = dgvr.Cells[colName.Name];
                    break;
                }
            }
        }

        // 新增数据
        private void AddData()
        {
            UCVehicleModelsAddOrEdit add = new UCVehicleModelsAddOrEdit(WindowStatus.Add, null, this);
            base.addUserControl(add, "车型档案-新增", "UCVehicleModelsAdd", this.Tag.ToString(), this.Name);
        }

        // 编辑数据
        private void EditData(WindowStatus status)
        {
            if (status != WindowStatus.Edit && status != WindowStatus.Copy)
            {
                return;
            }
            string title = "编辑";
            string menuId = "UCContactsEdit";
            if (status == WindowStatus.Copy)
            {
                title = "复制";
                menuId = "UCContactsCopy";
            }
            if (dgvVehicleModels.CurrentRow == null)
            {
                MessageBoxEx.Show(string.Format("请选择要{0}的数据!", title));
                return;
            }
            string vm_id = VM_ID;
            if (string.IsNullOrEmpty(vm_id))
            {
                return;
            }
            UCVehicleModelsAddOrEdit add = new UCVehicleModelsAddOrEdit(status, vm_id, this);
            base.addUserControl(add, string.Format("车型档案-{0}", title), menuId + vm_id, this.Tag.ToString(), this.Name);
        }

        // 预览数据
        private void ViewData()
        {
            if (dgvVehicleModels.CurrentRow == null)
            {
                MessageBoxEx.Show("请选择要预览的数据!");
                return;
            }
            string vm_id = VM_ID;
            if (string.IsNullOrEmpty(vm_id))
            {
                return;
            }
            UCVehicleModelsView view = new UCVehicleModelsView(vm_id);
            base.addUserControl(view, "车型档案-预览", "UCVehicleModelsView" + vm_id, this.Tag.ToString(), this.Name);
        }

        // 删除数据
        private void DeleteData()
        {
            dgvVehicleModels.EndEdit();
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dgvr in dgvVehicleModels.Rows)
            {
                object isCheck = dgvr.Cells["colCheck"].Value;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dgvr.Cells["colVM_ID"].Value.ToString());
                }
            }
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择要删除的数据!");
                return;
            }
            if (MessageBoxEx.Show("是否要删除数据?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                return;
            }
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("enable_flag", ((int)DataSources.EnumEnableFlag.DELETED).ToString());//标记删除
            if (DBHelper.BatchUpdateDataByIn("批量删除车型", "tb_vehicle_models", dic, "vm_id", listField.ToArray()))
            {
                MessageBoxEx.Show("删除成功！");
                BindData();
            }
            else
            {
                MessageBoxEx.Show("删除失败！");
            }
        }

        //绑定查询上的控件
        private void BindSearch()
        {
            //CommonCtrl.BindComboBoxByDictionarr(cboDataSource, "sys_data_source", true);//绑定数据源
            cboDataSource.DataSource = DataSources.EnumToList(typeof(SYSModel.DataSources.EnumDataSources), true);
            cboDataSource.ValueMember = "Value";
            cboDataSource.DisplayMember = "Text";
            cboStatus.DataSource = DataSources.EnumToList(typeof(DataSources.EnumStatus), true);//绑定状态
            cboStatus.ValueMember = "Value";
            cboStatus.DisplayMember = "Text";
            CommonCtrl.BindComboBoxByDictionarr(cboBrand, "sys_vehicle_brand", true);//绑定车型品牌
            CommonCtrl.BindComboBoxByDictionarr(cboType, "sys_vehicle_model_category", true);//绑定车型类型
            //CommonCtrl.BindComboBoxByDictionarr(cboStatus, "sys_data_status", true);//绑定状态
        }

        /// <summary>
        /// 设置选择项后状态
        /// </summary>
        void SetSelectedStatus()
        {
            listIDs.Clear();
            listStart.Clear();
            listStop.Clear();
            //已选择状态列表
            List<string> listFiles = new List<string>();
            foreach (DataGridViewRow dgvr in dgvVehicleModels.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells[colCheck.Name].EditedFormattedValue))
                {
                    listFiles.Add(dgvr.Cells[colDataSource.Name].Tag.ToString());//数据来源
                    string vm_id = dgvr.Cells[colVM_ID.Name].Value.ToString();
                    listIDs.Add(vm_id);//数据ID
                    if (dgvr.Cells[colStatus.Name].Tag == null)
                    {
                        continue;
                    }
                    enumStatus = (DataSources.EnumStatus)Convert.ToInt32(dgvr.Cells[colStatus.Name].Tag);//状态
                    if (enumStatus == DataSources.EnumStatus.Start)
                    {
                        listStart.Add(vm_id);
                    }
                    else if (enumStatus == DataSources.EnumStatus.Stop)
                    {
                        listStop.Add(vm_id);
                    }
                }
            }
            #region 设置启用/停用
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
            #endregion
            //宇通
            string dataSource = ((int)DataSources.EnumDataSources.YUTONG).ToString();
            if (listFiles.Count == 1 && !listFiles.Contains(dataSource))
            {
                base.btnEdit.Enabled = true;
                tsmiEdit.Enabled = true;
                base.btnCopy.Enabled = true;
                tsmiCopy.Enabled = true;
                base.btnView.Enabled = true;
                tmsiView.Enabled = true;
            }
            else
            {
                base.btnEdit.Enabled = false;
                tsmiEdit.Enabled = false;
                base.btnCopy.Enabled = false;
                tsmiCopy.Enabled = false;
                if (listFiles.Count == 1)
                {
                    base.btnView.Enabled = true;
                    tmsiView.Enabled = true;
                }
                else
                {
                    base.btnView.Enabled = false;
                    tmsiView.Enabled = false;
                }
            }

            //如果选择包含宇通来源，则不能删除
            if (listFiles.Count == 0 || listFiles.Contains(dataSource))
            {
                btnDelete.Enabled = false;
                tsmiDelete.Enabled = false;
            }
            else
            {
                btnDelete.Enabled = true;
                tsmiDelete.Enabled = true;
            }
        }
        #endregion

        #region 事件
        //清除
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearSearch(pnlSearch);
            diCreate.StartDate = DateTime.Now.AddYears(-1).ToString(diCreate.customFormat);
            diCreate.EndDate = DateTime.Now.ToString(diCreate.customFormat);
        }
        //查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        //双击打开预览
        private void dgvVehicleModels_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            ViewData();
        }

        // 换页
        private void page_PageIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        //菜单查看
        private void tmsiView_Click(object sender, EventArgs e)
        {
            ViewData();
        }

        //菜单编辑
        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            EditData(WindowStatus.Edit);
        }

        //菜单复制
        private void tsmiCopy_Click(object sender, EventArgs e)
        {
            EditData(WindowStatus.Copy);
        }

        //菜单删除
        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            DeleteData();
        }
        //选择复选框
        private void dgvVehicleModels_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvVehicleModels.CurrentCell == null)
            {
                return;
            }
            if (dgvVehicleModels.CurrentCell.OwningColumn.Name == colCheck.Name)
            {
                SetSelectedStatus();
            }
        }

        //鼠标单击，选择当前行
        private void dgvVehicleModels_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == colCheck.Index)
            {
                return;
            }
            //清空已选择框
            foreach (DataGridViewRow dgvr in dgvVehicleModels.Rows)
            {
                object check = dgvr.Cells[colCheck.Name].EditedFormattedValue;
                if (check != null && (bool)check)
                {
                    dgvr.Cells[colCheck.Name].Value = false;
                }
            }
            if ((bool)dgvVehicleModels.CurrentRow.Cells[colCheck.Name].EditedFormattedValue)
            {
                dgvVehicleModels.CurrentRow.Cells[colCheck.Name].Value = false;
            }
            else
            {
                dgvVehicleModels.CurrentRow.Cells[colCheck.Name].Value = true;
            }
            SetSelectedStatus();
        }
        //全选
        void dgvVehicleModels_HeadCheckChanged()
        {
            SetSelectedStatus();
        }
        #endregion
        
    }
}
