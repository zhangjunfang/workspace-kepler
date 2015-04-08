using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using SYSModel;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using Utility.Common;
using System.Globalization;
using System.Collections;

namespace HXCPcClient.UCForm.DataManage.VehicleFiles
{
    /// <summary>
    /// 数据管理-车辆档案管理
    /// Author：JC
    /// AddTime：2014.12.02
    /// </summary>
    public partial class UCVehicleManage : UCBase
    {
        #region 属性设置
        /// <summary>
        /// 查询条件
        /// </summary>
        string strWhere = string.Empty;
        /// <summary>
        /// 车辆档案Id
        /// </summary>
        string strVId = string.Empty;
        /// <summary>
        /// 启用停用状态
        /// </summary>
        DataSources.EnumStatus _eStatus;
        /// <summary>
        /// 列表中选中的是启用的 id集合
        /// </summary>
        ArrayList _qiList = new ArrayList();
        /// <summary>
        /// 列表中选中的是停用的 id集合
        /// </summary>
        ArrayList _tiList = new ArrayList();
        List<string> listIDs = new List<string>();//已选择ID
        #endregion

        #region 初始化窗体
        public UCVehicleManage()
        {
            InitializeComponent();
            SetTopbuttonShow();
            base.DeleteEvent += new ClickHandler(UCVehicleManage_DeleteEvent);
            base.EditEvent += new ClickHandler(UCVehicleManage_EditEvent);
            base.CopyEvent += new ClickHandler(UCVehicleManage_CopyEvent);
            base.AddEvent += new ClickHandler(UCVehicleManage_AddEvent);
            base.StatusEvent += new ClickHandler(UCVehicleManage_StatusEvent);
           
        }
        #endregion

        #region 顶部button显示设置
        /// <summary>
        ///  顶部button显示设置
        /// </summary>
        private void SetTopbuttonShow()
        {
            base.btnSave.Visible = false;
            base.btnCancel.Visible = false;
            base.btnSync.Visible = false;
            base.btnVerify.Visible = false;
            base.btnConfirm.Visible = false;
            base.btnSubmit.Visible = false;
            base.btnActivation.Visible = false;
        }
        #endregion

        #region 启用停用事件
        void UCVehicleManage_StatusEvent(object sender, EventArgs e)
        {
            var listSql = new List<SQLObj>();
            const string opName = "修改车辆档案状态";
            var strStatus = Convert.ToInt16(_eStatus).ToString(CultureInfo.InvariantCulture);
            string msg;
            if (_eStatus == DataSources.EnumStatus.Start)//启用
            {
                StatusSql(listSql, _tiList, strStatus);//停用的ti_list改为启用
                msg = "启用";
            }
            else//停用
            {
                StatusSql(listSql, _qiList, strStatus);//启用的qi_list 改为停用
                msg = "停用";
            }
            if (MessageBoxEx.Show("确认要" + msg + "吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            if (DBHelper.BatchExeSQLMultiByTrans(opName, listSql))
            {
                btnStatus.Enabled = false;
                BindPageData();
                MessageBoxEx.Show(msg + "成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBoxEx.Show(msg + "失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 更新启停用状态
        private void StatusSql(ICollection<SQLObj> listSql, ICollection idList, string status)
        {
            if (idList.Count <= 0) return;
            foreach (string id in idList)
            {
                var sqlObj = new SQLObj { cmdType = CommandType.Text };
                var dicParam = new Dictionary<string, ParamObj>
                {
                    {"v_id", new ParamObj("v_id", id, SysDbType.VarChar, 40)},
                    {"status", new ParamObj("status", status, SysDbType.VarChar, 40)},
                    {"update_by", new ParamObj("update_by", GlobalStaticObj.UserID, SysDbType.NVarChar, 40)},
                    {
                        "update_time",
                        new ParamObj("update_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj.CurrentDateTime),
                            SysDbType.BigInt)
                    }
                };//参数
                sqlObj.sqlString = @"update tb_vehicle set status=@status,update_by=@update_by,update_time=@update_time where v_id=@v_id;";
                sqlObj.Param = dicParam;
                listSql.Add(sqlObj);
            }
        }
        #endregion

        #region  新增事件
        void UCVehicleManage_AddEvent(object sender, EventArgs e)
        {
            UCVehicleAddOrEdit Add = new UCVehicleAddOrEdit();
            Add.uc = this;
            Add.wStatus = WindowStatus.Add;
            base.addUserControl(Add, "车辆档案-新增", "UCVehicleAddOrEdit", this.Tag.ToString(), this.Name);
        }
        #endregion

        #region 复制事件
        void UCVehicleManage_CopyEvent(object sender, EventArgs e)
        {
            if (!IsCheck("复制"))
            {
                return;
            }
            UCVehicleAddOrEdit Edit = new UCVehicleAddOrEdit();
            Edit.uc = this;
            Edit.wStatus = WindowStatus.Copy;
            Edit.strId = strVId;  //车辆档案ID
            base.addUserControl(Edit, "车辆档案-复制", "UCVehicleAddOrEdit" + Edit.strId, this.Tag.ToString(), this.Name);
        }
        #endregion

        #region 编辑事件
        void UCVehicleManage_EditEvent(object sender, EventArgs e)
        {
            if (!IsCheck("编辑"))
            {
                return;
            }
            UCVehicleAddOrEdit Edit = new UCVehicleAddOrEdit();
            Edit.uc = this;
            Edit.wStatus = WindowStatus.Edit;
            Edit.strId = strVId;  //车辆档案ID
            base.addUserControl(Edit, "车辆档案-编辑", "UCVehicleAddOrEdit" + Edit.strId, this.Tag.ToString(), this.Name);
        }
        #endregion

        #region 删除事件
        void UCVehicleManage_DeleteEvent(object sender, EventArgs e)
        {
            try
            {
                List<string> listField = new List<string>();
                foreach (DataGridViewRow dr in dgvData.Rows)
                {
                    object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                    if (isCheck != null && (bool)isCheck)
                    {
                        listField.Add(dr.Cells["v_id"].Value.ToString());
                    }
                }
                if (listField.Count == 0)
                {
                    MessageBoxEx.Show("请选择删除记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBoxEx.Show("确认要删除吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
                Dictionary<string, string> comField = new Dictionary<string, string>();
                comField.Add("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.DELETED).ToString());
                comField.Add("update_by", HXCPcClient.GlobalStaticObj.UserID);//修改人Id            
                comField.Add("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString());//修改时间  
                bool flag = DBHelper.BatchUpdateDataByIn("批量删车辆档案", "tb_vehicle", comField, "v_id", listField.ToArray());
                if (flag)
                {
                    BindPageData();
                    if (dgvData.Rows.Count > 0)
                    {
                        dgvData.CurrentCell = dgvData.Rows[0].Cells[0];
                    }
                    MessageBoxEx.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBoxEx.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("删除失败！" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region  编辑、预览数据验证
        /// <summary>
        /// 编辑、复制数据验证
        /// </summary>
        /// <param name="strMessage"></param>
        /// <returns></returns>
        private bool IsCheck(string strMessage)
        {
            bool isOk = false;
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in dgvData.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells["v_id"].Value.ToString());
                    strVId = dr.Cells["v_id"].Value.ToString();
                }
            }
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择" + strMessage + "记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (listField.Count > 1)
            {
                MessageBoxEx.Show(" 一次仅能" + strMessage + "1条记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (listField.Count == 1)
            {
                isOk = true;
            }
            return isOk;
        }
        #endregion

        #region 窗体Load事件
        private void UCVehicleManage_Load(object sender, EventArgs e)
        {
            #region 基础数据加载
            CommonCtrl.BindComboBoxByDictionarr(cobCarBrand, "sys_vehicle_brand", true);//绑定车型品牌
            cobDataSources.DataSource = DataSources.EnumToList(typeof(DataSources.EnumDataSources), true);//数据来源
            cobDataSources.ValueMember = "Value";
            cobDataSources.DisplayMember = "Text";
            cobStatus.DataSource = DataSources.EnumToList(typeof(DataSources.EnumStatus), true);//绑定状态 启用 停用
            cobStatus.ValueMember = "Value";
            cobStatus.DisplayMember = "Text";
            #endregion          
            dtpCSTime.Value = DateTime.Now.AddMonths(-1);
            dtpCETime.Value = DateTime.Now;           
            dgvData.ReadOnly = false;
            foreach (DataGridViewColumn dgvc in dgvData.Columns)
            {
                if (dgvc == colCheck)
                {
                    continue;
                }
                dgvc.ReadOnly = true;
            }
            BindPageData();
        }
        #endregion       

        #region 清除功能
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCarNO.Text = string.Empty;
            txtCarNO.Tag = string.Empty;
            txtCarType.Text = string.Empty;
            txtCarType.Tag = string.Empty;
            txtCustomNO.Text = string.Empty;
            txtCustomNO.Tag = string.Empty;
            txtVIN.Caption = string.Empty;
            txtYardCode.Caption = string.Empty;
            cobCarBrand.SelectedValue = string.Empty;            
            cobDataSources.SelectedValue = string.Empty;
            cobStatus.SelectedValue = string.Empty;           
            dtpCSTime.Value = DateTime.Now.AddMonths(-1);
            dtpCETime.Value = DateTime.Now;
        }
        #endregion

        #region 车牌号选择器
        private void txtCarNO_ChooserClick(object sender, EventArgs e)
        {
            frmVehicleGrade frmVehicle = new frmVehicleGrade();
            DialogResult result = frmVehicle.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtCarNO.Text = frmVehicle.strLicensePlate;
                txtVIN.Caption = frmVehicle.strVIN; 
                cobCarBrand.SelectedValue = frmVehicle.strBrand;
            }
        }
        #endregion

        #region 车型选择器
        private void txtCarType_ChooserClick(object sender, EventArgs e)
        {
            frmVehicleModels frmModels = new frmVehicleModels();
            DialogResult result = frmModels.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtCarType.Text = frmModels.VMName;
                txtCarType.Tag = frmModels.VMID;
            }
        }
        #endregion

        #region 所属客户选择器
        private void txtCustomNO_ChooserClick(object sender, EventArgs e)
        {
            frmCustomerInfo frmCInfo = new frmCustomerInfo();
            DialogResult result = frmCInfo.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtCustomNO.Text = frmCInfo.strCustomerName;
                txtCustomNO.Tag = frmCInfo.strCustomerId;
               
            }
        }
        #endregion

        #region 查询功能
        private void btnQuery_Click(object sender, EventArgs e)
        {
            BindPageData();
        }
        #endregion

        #region 分页查询绑定数据
        /// <summary>
        /// 分页查询绑定数据
        /// </summary>
        public void BindPageData()
        {
            try
            {
                #region 事件选择判断               
                if (dtpCSTime.Value > dtpCETime.Value)
                {
                    MessageBoxEx.Show("创建时间,开始时间不能大于结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion
                strWhere = string.Format(" a.enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "' ");//enable_flag 1未删除
                if (!string.IsNullOrEmpty(txtCarNO.Text.Trim()))//车牌号
                {
                    strWhere += string.Format(" and  a.license_plate like '%{0}%'", txtCarNO.Text.Trim());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobCarBrand.SelectedValue)))//车辆品牌
                {
                    strWhere += string.Format(" and  a.v_brand like '%{0}%'", cobCarBrand.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobDataSources.SelectedValue)))//数据来源
                {
                    strWhere += string.Format(" and  a.data_source = '{0}'", cobDataSources.SelectedValue.ToString());
                }               
                if (!string.IsNullOrEmpty(txtVIN.Caption.Trim()))//VIN
                {
                    strWhere += string.Format(" and  a.vin like '%{0}%'", txtVIN.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtCarType.Text)))//车型
                {
                    strWhere += string.Format(" and  a.v_model = '{0}'", txtCarType.Text.Trim());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtCustomNO.Tag)))//所属客户
                {
                    strWhere += string.Format(" and  a.cust_id like '%{0}%'", txtCustomNO.Tag.ToString());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dtpCSTime.Value)) && !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dtpCETime.Value)))//创建时间
                {
                    strWhere += string.Format(" and  a.create_time BETWEEN {0} and {1}", Common.LocalDateTimeToUtcLong(dtpCSTime.Value.Date), Common.LocalDateTimeToUtcLong(dtpCETime.Value.Date.AddDays(1).AddMilliseconds(-1)));//出厂日期
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtYardCode.Caption.Trim())))//车场编号
                {
                    strWhere += string.Format(" and  a.car_factory_num like '%{0}%'", txtYardCode.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobStatus.SelectedValue)))//状态
                {
                    strWhere += string.Format(" and a.status = '{0}'", cobStatus.SelectedValue.ToString());
                }
                string strFileds = @"a.license_plate,a.vin,a.v_brand,d.vm_name as v_model ,a.carbuy_date,a.car_factory_num,b.cust_name,a.data_source,
                c.user_name,a.create_time, case a.status when '1'  then '启用'  when '0' then  '停用' else '' end  as status,a.remark,a.v_id";
                string strTables = " tb_vehicle a left join tb_customer b on a.cust_id=b.cust_id left join sys_user c on a.create_by=c.user_id left join tb_vehicle_models d on a.v_model=d.vm_id";
                int recordCount;
                DataTable dt = DBHelper.GetTableByPage("分页查询车辆档案", strTables, strFileds, strWhere, "", " order by a.create_time desc", page.PageIndex, page.PageSize, out recordCount);
                dgvData.DataSource = dt;
                page.RecordCount = recordCount;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void page_PageIndexChanged(object sender, EventArgs e)
        {
            BindPageData();
        }

        #endregion

        #region 重写时间
        private void dgvData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
            string fieldNmae = dgvData.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("carbuy_date") || fieldNmae.Equals("create_time"))
            {
                long ticks = (long)e.Value;
                e.Value = Common.UtcLongToLocalDateTime(ticks);
            }
            if (fieldNmae.Equals("data_source"))
            {
                e.Value = DataSources.GetDescription(typeof(DataSources.EnumDataSources), int.Parse(e.Value.ToString()));
            }
            if (fieldNmae.Equals("v_brand"))
            {
                e.Value = GetDicName(e.Value.ToString());
            }
            //if (fieldNmae.Equals("v_model"))
            //{
            //    e.Value = GetVmodel(e.Value.ToString());
            //}

        }
        #endregion 

        #region 获取车型名称
        private string GetVmodel(string strVId)
        {
            return DBHelper.GetSingleValue("获得车型名称", "tb_vehicle_models", "vm_name", "vm_id='" + strVId + "'", "");
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

        #region 单击单元格内容设置启停用状体
        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.CurrentCell == null)
            {
                return;
            }
            _qiList.Clear();
            _tiList.Clear();
            _eStatus = new DataSources.EnumStatus();
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                if (!Convert.ToBoolean(row.Cells[0].EditedFormattedValue)) continue;
                if (row.Cells["status"].EditedFormattedValue.ToString() == DataSources.EnumStatus.Start.GetDescription()) //表格中是启用
                {
                    _qiList.Add(row.Cells["v_id"].EditedFormattedValue.ToString());
                }
                else//表格中是停用
                {
                    _tiList.Add(row.Cells["v_id"].EditedFormattedValue.ToString());
                }
            }
            BtnStatus(_qiList, _tiList);
            //点击选择框
            if (dgvData.CurrentCell.OwningColumn.Name == colCheck.Name)
            {
                SetSelectedStatus();
            }
        }
        #endregion

        #region 启用按钮状态设置
        /// <summary>
        /// 启用按钮状态设置
        /// </summary>
        /// <param name="qiList">列表中选中的是启用的 id集合</param>
        /// <param name="tiList">列表中选中的是停用的 id集合</param>
        private void BtnStatus(ICollection qiList, ICollection tiList)
        {
            if (qiList.Count > 0 && tiList.Count > 0)
            {
                btnStatus.Enabled = false;
                btnStatus.Caption = "启用";
                tsmiStart.Enabled = false;
                tsmiStart.Text = "启用";
            }
            else if (qiList.Count > 0 && tiList.Count == 0)
            {
                btnStatus.Enabled = true;
                btnStatus.Caption = "停用";
                tsmiStart.Enabled = true;
                tsmiStart.Text = "停用";
                _eStatus = DataSources.EnumStatus.Stop;
            }
            else if (qiList.Count == 0 && tiList.Count > 0)
            {
                btnStatus.Enabled = true;
                btnStatus.Caption = "启用";
                tsmiStart.Enabled = true;
                tsmiStart.Text = "启用";
                _eStatus = DataSources.EnumStatus.Start;
            }
            else if (qiList.Count == 0 && tiList.Count == 0)
            {
                btnStatus.Enabled = false;
                btnStatus.Caption = "启用";
                tsmiStart.Enabled = false;
                tsmiStart.Text = "启用";
            }
        }
        #endregion

        #region 全选设置启停用按钮状态
        private void dgvData_HeadCheckChanged()
        {
            _qiList.Clear();
            _tiList.Clear();
            _eStatus = new DataSources.EnumStatus();
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value))
                {
                    if (row.Cells["status"].Value.ToString() == DataSources.EnumStatus.Start.GetDescription())//表格中是启用
                    {
                        _qiList.Add(row.Cells["v_id"].Value.ToString());
                    }
                    else//表格中是停用
                    {
                        _tiList.Add(row.Cells["v_id"].Value.ToString());
                    }
                }
            }
            BtnStatus(_qiList, _tiList);
            SetSelectedStatus();
        }
        #endregion

        #region 双击进入详情窗体
        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.CurrentRow == null)
            {
                return;
            }
            strVId = dgvData.CurrentRow.Cells["v_id"].Value.ToString();
            ViewData("1");
        }
        #endregion

        #region 预览数据
        /// <summary>
        /// 预览数据
        /// </summary>
        /// <param name="strType">操作类型，0为预览，1为双击单元格</param>
        private void ViewData(string strType = "0")
        {
            if (strType == "0")
            {
                if (!IsCheck("预览"))
                {
                    return;
                }
            }
            UCVehicleView view = new UCVehicleView(strVId);
            view.uc = this;
            base.addUserControl(view, "车辆档案-预览", "UCVehicleView" + strVId, this.Tag.ToString(), this.Name);
        }
        #endregion

        #region 根据选择的数据判断编辑、复制的显示状态
        private void dgvData_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
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
            foreach (DataGridViewRow dgvr in dgvData.Rows)
            {
                object check = dgvr.Cells[colCheck.Name].EditedFormattedValue;
                if (check != null && (bool)check)
                {
                    dgvr.Cells[colCheck.Name].Value = false;
                }
            }
            //选择当前行
            dgvData.Rows[e.RowIndex].Cells[colCheck.Name].Value = true;
            SetSelectedStatus();
        }
         #endregion

        #region 选择，设置工具栏状态
        /// <summary>
        /// 选择，设置工具栏状态
        /// </summary>
        private void SetSelectedStatus()
        {
            listIDs.Clear();
            //已选择状态列表
            List<string> listFiles = new List<string>();
            List<string> listdata_source = new List<string>();//数据来源
            
            foreach (DataGridViewRow dgvr in dgvData.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells[colCheck.Name].EditedFormattedValue))
                {
                    listFiles.Add(dgvr.Cells[status.Name].Value.ToString());
                    listIDs.Add(dgvr.Cells[v_id.Name].Value.ToString());
                    listdata_source.Add(dgvr.Cells[data_source.Name].Value.ToString());
                    
                }
            }

            string strSource = Convert.ToInt32(DataSources.EnumDataSources.YUTONG).ToString();
            //复制按钮，只选择一个并且不是作废，可以复制
            if (listFiles.Count == 1)
            {
                btnCopy.Enabled = true;
                tsmiCopy.Enabled = true;
            }
            else
            {
                btnCopy.Enabled = false;
                tsmiCopy.Enabled = false;
            }
            //编辑按钮，只选择一个可以编辑
            if (listFiles.Count == 1)
            {
                btnEdit.Enabled = true;
                tsmiEdit.Enabled = true;
            }
            else
            {
                btnEdit.Enabled = false;
                tsmiEdit.Enabled = false;
            }
            if (listdata_source.Contains(strSource))
            {
                btnEdit.Enabled = false;
                tsmiEdit.Enabled = false;
                btnDelete.Enabled = false;
                tsmiDelete.Enabled = false;
            }
            else
            {
                btnEdit.Enabled = true;
                tsmiEdit.Enabled = true;
                btnDelete.Enabled = true;
                tsmiDelete.Enabled = true;
            }
        }
        #endregion

        #region 右键功能
        /// <summary>
        /// 右键查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiSearch_Click(object sender, EventArgs e)
        {
            BindPageData();
        }
        /// <summary>
        /// 右键清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiClear_Click(object sender, EventArgs e)
        {
            btnClear_Click(null,null);
        }
        /// <summary>
        /// 右键新建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            UCVehicleManage_AddEvent(null,null);
        }
        /// <summary>
        /// 右键编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            UCVehicleManage_EditEvent(null,null);
        }
        /// <summary>
        /// 右键复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiCopy_Click(object sender, EventArgs e)
        {
            UCVehicleManage_EditEvent(null,null);
        }
        /// <summary>
        /// 右键删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            UCVehicleManage_DeleteEvent(null,null);
        }
        /// <summary>
        /// 右键启停用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiStart_Click(object sender, EventArgs e)
        {
            UCVehicleManage_StatusEvent(null,null);
        }
        #endregion
    }
}
