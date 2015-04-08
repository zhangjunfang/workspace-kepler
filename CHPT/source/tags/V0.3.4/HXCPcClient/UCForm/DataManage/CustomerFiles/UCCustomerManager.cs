using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using SYSModel;
using Utility.Common;

namespace HXCPcClient.UCForm.DataManage.CustomerFiles
{
    ///*************************************************************************//
    /// System:       HuiXuCheSYS
    /// FileName:     UCCustomerManager         
    /// Author:       Kord
    /// Date:         2014/11/12 17:21:37
    /// Machine Name: KORD
    ///***************************************************************************//
    /// Function: 
    /// 	基础数据 -- 客户档案 -- 管理
    ///***************************************************************************//
    public partial class UCCustomerManager : UCBase
    {
        #region Constructor -- 构造函数
        public UCCustomerManager()
        {
            InitializeComponent();

            UIAssistants.SetButtonStyle4QueryAndClear(btn_query, btn_clear, true);  //设置查询按钮和清除按钮样式
            DataGridViewEx.SetDataGridViewStyle(dgv_table, create_time);   //设置数据表格样式,并将最后一列填充其余空白
            Load += (sender, args) => UIAssistants.SetDataGridViewCheckColumn(dgv_table, colCheck);

            Init(); //初始化方法

            SetContentMenuScrip(dgv_table);

        }
        #endregion

        #region Field -- 字段
        /// <summary>
        /// 存字典码表本页用到数据
        /// </summary>
        DataTable _dtdic;
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
        public const String TableName = "tb_customer";
        #endregion

        #region Property -- 属性

        #endregion

        #region Method -- 方法
        private void Init() //初始化方法
        {
            SetFuncationButtonVisible();
            InitQueryControlDataSource();
            FuncationRegiste();
        }
        private void SetFuncationButtonVisible()    //设置功能按钮可见性
        {
            var btnCols = new ObservableCollection<ButtonEx_sms>
            {
                btnAdd, btnCopy, btnEdit, btnDelete, btnStatus, btnSubmit, btnSave, btnCancel, btnExport, btnImport, btnPrint
            };
            UIAssistants.SetUCBaseFuncationVisible(this, btnCols);
            btnStatus.Enabled = false;
        }
        private void InitQueryControlDataSource()   //初始化查询控件数据项
        {
            DataSources.BindComBoxDataEnum(cbo_is_member, typeof(DataSources.EnumYesNo), true); //是否会员
            CommonFuncCall.BindProviceComBox(cboprovince, "省"); //省份
            CommonCtrl.BindComboBoxByDictionarr(cbo_cust_type, "sys_customer_category", true);//客户类别
            DataSources.BindComBoxDataEnum(cbo_status, typeof(DataSources.EnumStatus), true);//数据状态
            DataSources.BindComBoxDataEnum(cbo_data_source, typeof(DataSources.EnumDataSources), true);//数据来源
        }
        private void ConvertDataGridColumnsData(DataGridViewCellFormattingEventArgs args) //转换数据列数据
        {
            UIAssistants.DgvCellDataConvert2Datetime(dgv_table, args, "create_time");
            UIAssistants.DgvCellDataConvert2Enum(dgv_table, args, "is_member", typeof(DataSources.EnumYesNo));
            UIAssistants.DgvCellDataConvert2Enum(dgv_table, args, "status", typeof(DataSources.EnumStatus));
            UIAssistants.DgvCellDataConvert2Enum(dgv_table, args, "data_source", typeof(DataSources.EnumDataSources));
            UIAssistants.DgvCellDataConvert2DicData(dgv_table, args, "enterprise_nature");
            UIAssistants.DgvCellDataConvert2DicData(dgv_table, args, "cust_type");
        }
        private void FuncationRegiste() //事件注册
        {
            #region 数据表数据选择
            //单元格数据转换
            dgv_table.CellFormatting += (sender, args) => ConvertDataGridColumnsData(args);
            //数据行双击事件
            dgv_table.CellDoubleClick += delegate(object sender, DataGridViewCellEventArgs e)
            {
                if (e.RowIndex < 0) return;
                var value = dgv_table.Rows[e.RowIndex].Cells[cust_id.Name].Value;
                if (value == null) return;
                var id = value.ToString();
                var ucCustomerView = new UCCustomerViews
                {
                    Id = id,
                    UCCustomerManager = this
                };
                addUserControl(ucCustomerView, "客户详细信息", "UCCustomerViews" + id, Tag.ToString(), Name);
            };
            #endregion

            #region 数据翻页
            pageQ.PageIndexChanged += delegate
            {
                BindPageData();
            };
            #endregion

            #region 清除按钮事件
            btn_clear.Click += delegate
            {
                UIAssistants.ClearQueryControlValue(pnl_query);
            };
            #endregion

            #region 查询
            btn_query.Click += (sender, args) =>
            {
                pageQ.PageIndex = 1;
                pageQ.RecordCount = 0;
                BindPageData();
            };
            #endregion

            #region 新增
            btnAdd.Click += delegate
            {
                var ucCustomerAddOrEdit = new UCCustomerAddOrEdit {UCCustomerManager = this, wStatus = WindowStatus.Add};
                addUserControl(ucCustomerAddOrEdit, "客户信息新增", "UCCustomerAddOrEdit-Add", Tag.ToString(), Name);
            };
            #endregion

            #region 复制
            btnCopy.Click += delegate
            {
                var id = "";
                var dataView = UIAssistants.GetDataGridCheckRows(dgv_table, colCheck, cust_id, TableName);
                if (dataView == null) return;
                foreach (DataRowView rowView in dataView)
                {
                    id = rowView["cust_id"].ToString();
                    break;
                }
                var ucCustomerAddOrEdit = new UCCustomerAddOrEdit
                {
                    UCCustomerManager = this,
                    wStatus = WindowStatus.Copy,
                    Id = id
                };
                addUserControl(ucCustomerAddOrEdit, "客户信息拷贝", "UCCustomerAddOrEdit-Copy" + ucCustomerAddOrEdit.Id, Tag.ToString(), Name);
            };
            #endregion

            #region 编辑
            btnEdit.Click += delegate
            {
                var id = "";
                var dataView = UIAssistants.GetDataGridCheckRows(dgv_table, colCheck, cust_id, TableName);
                if (dataView == null) return;
                foreach (DataRowView rowView in dataView)
                {
                    id = rowView["cust_id"].ToString();
                    break;
                }
                var ucCustomerAddOrEdit = new UCCustomerAddOrEdit
                {
                    UCCustomerManager = this,
                    wStatus = WindowStatus.Edit,
                    Id = id
                };
                addUserControl(ucCustomerAddOrEdit, "客户信息编辑", "UCCustomerAddOrEdit-Edit" + ucCustomerAddOrEdit.Id, Tag.ToString(), Name);
            };
            #endregion

            #region 删除
            btnDelete.Click += delegate
            {
                try
                {
                    var selectedRows = UIAssistants.GetDataGridCheckRows(dgv_table, colCheck, cust_id);
                    if (selectedRows.Count == 0)
                    {
                        MessageBoxEx.Show("请选择删除记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (MessageBoxEx.Show("确认要删除吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    {
                        return;
                    }
                    //var comField = new Dictionary<string, string> { { "enable_flag", "0" } };
                    //flag = DBHelper.BatchUpdateDataByIn("删除三包服务单", TableName, comField, "tg_id", selectedRows.ToArray());
                    var comField = new Dictionary<string, string> { { "enable_flag", "0" } };
                    var flag = DBHelper.BatchUpdateDataByIn("批量删除客户档案", "tb_customer", comField, "cust_id", selectedRows.ToArray());
                    if (flag)
                    {
                        BindPageData();
                        if (dgv_table.Rows.Count > 0)
                        {
                            dgv_table.CurrentCell = dgv_table.Rows[0].Cells[0];
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
            };
            #endregion

            #region 省市区联动
            cboprovince.SelectedIndexChanged += delegate
            {
                if (!string.IsNullOrEmpty(cboprovince.SelectedValue.ToString()))
                {
                    CommonFuncCall.BindCityComBox(cbocity, cboprovince.SelectedValue.ToString(), "市");
                    CommonFuncCall.BindCountryComBox(cbocounty, cbocity.SelectedValue.ToString(), "区/县");
                }
                else
                {
                    CommonFuncCall.BindCityComBox(cbocity, "", "市");
                    CommonFuncCall.BindCountryComBox(cbocounty, "", "区/县");
                }
            };
            cbocity.SelectedIndexChanged += delegate
            {
                if (!string.IsNullOrEmpty(cbocity.SelectedValue.ToString()))
                {
                    CommonFuncCall.BindCountryComBox(cbocounty, cbocity.SelectedValue.ToString(), "区/县");
                }
                else
                {
                    CommonFuncCall.BindCountryComBox(cbocounty, "", "区/县");
                }
            };
            #endregion

            #region 启用停用事件
            StatusEvent += delegate
            {
                
                var listSql = new List<SQLObj>();
                const string opName = "修改客户档案状态";
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
            };
            #endregion

            #region 数据表格事件
            dgv_table.CellContentClick += delegate(object sender, DataGridViewCellEventArgs e)
            {
                if (e.ColumnIndex != 0) return;
                _qiList.Clear();
                _tiList.Clear();
                _eStatus = new DataSources.EnumStatus();
                foreach (DataGridViewRow row in dgv_table.Rows)
                {
                    if (!Convert.ToBoolean(row.Cells[0].EditedFormattedValue)) continue;
                    if (row.Cells["status"].EditedFormattedValue.ToString() == DataSources.EnumStatus.Start.GetDescription()) //表格中是启用
                    {
                        _qiList.Add(row.Cells["cust_id"].EditedFormattedValue.ToString());
                    }
                    else//表格中是停用
                    {
                        _tiList.Add(row.Cells["cust_id"].EditedFormattedValue.ToString());
                    }
                }
                BtnStatus(_qiList, _tiList);
            };
            dgv_table.HeadCheckChanged += delegate
            {
                _qiList.Clear();
                _tiList.Clear();
                _eStatus = new DataSources.EnumStatus();
                foreach (DataGridViewRow row in dgv_table.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value))
                    {
                        if (row.Cells["status"].Value.ToString() == DataSources.EnumStatus.Start.GetDescription())//表格中是启用
                        {
                            _qiList.Add(row.Cells["cust_id"].Value.ToString());
                        }
                        else//表格中是停用
                        {
                            _tiList.Add(row.Cells["cust_id"].Value.ToString());
                        }
                    }
                }
                BtnStatus(_qiList, _tiList);
            };
            #endregion

            #region 根据选择的数据判断功能按钮的显示状态
            dgv_table.CellMouseUp += delegate
            {
                var dataView = UIAssistants.GetDataGridCheckRows(dgv_table, colCheck, cust_id, TableName);
                var listField = UIAssistants.GetDataGridCheckRows(dgv_table, colCheck, cust_id);
                if (dataView == null || listField.Count == 0)
                {
                    btnCopy.Enabled = false;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                }
                else
                {
                    btnCopy.Enabled = true;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                }
            };
            #endregion
        }
        private void StatusSql(ICollection<SQLObj> listSql, ICollection idList, string status)
        {
            if (idList.Count <= 0) return;
            foreach (string id in idList)
            {
                var sqlObj = new SQLObj {cmdType = CommandType.Text};
                var dicParam = new Dictionary<string, ParamObj>
                {
                    {"cust_id", new ParamObj("cust_id", id, SysDbType.VarChar, 40)},
                    {"status", new ParamObj("status", status, SysDbType.VarChar, 40)},
                    {"update_by", new ParamObj("update_by", GlobalStaticObj.UserID, SysDbType.NVarChar, 40)},
                    {
                        "update_time",
                        new ParamObj("update_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj.CurrentDateTime),
                            SysDbType.BigInt)
                    }
                };//参数
                sqlObj.sqlString = @"update [tb_customer] set status=@status,update_by=@update_by,update_time=@update_time where cust_id=@cust_id;";
                sqlObj.Param = dicParam;
                listSql.Add(sqlObj);
            }
        }
        /// <summary>
        /// 启用按钮状态设置
        /// </summary>
        /// <param name="qiList">列表中选中的是启用的 id集合</param>
        /// <param name="tiList">列表中选中的是停用的 id集合</param>
        private void BtnStatus(ICollection qiList, ICollection tiList)
        {
            //if (qiList == null) throw new ArgumentNullException("qiList");
            //if (tiList == null) throw new ArgumentNullException("tiList");
            if (qiList.Count > 0 && tiList.Count > 0)
            {
                btnStatus.Enabled = false;
                btnStatus.Caption = "启用";
            }
            else if (qiList.Count > 0 && tiList.Count == 0)
            {
                btnStatus.Enabled = true;
                btnStatus.Caption = "停用";
                _eStatus = DataSources.EnumStatus.Stop;
            }
            else if (qiList.Count == 0 && tiList.Count > 0)
            {
                btnStatus.Enabled = true;
                btnStatus.Caption = "启用";
                _eStatus = DataSources.EnumStatus.Start;
            }
            else if (qiList.Count == 0 && tiList.Count == 0)
            {
                btnStatus.Enabled = false;
                btnStatus.Caption = "启用";
            }
        }

        /// <summary> 
        /// 绑定数据
        /// </summary>
        public void BindPageData()
        {
            try
            {
                var where = " c.enable_flag='1' ";  //enable_flag 1未删除
                if (!string.IsNullOrEmpty(txt_cust_code.Caption.Trim()))//客户编码
                {
                    where += string.Format(" and  c.cust_code like '%{0}%'", txt_cust_code.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txt_cust_name.Caption.Trim()))//客户名称
                {
                    where += string.Format(" and  c.cust_name like '%{0}%'", txt_cust_name.Caption.Trim());
                }
                if (cbo_cust_type.SelectedValue != null && cbo_cust_type.SelectedValue.ToString() != "")
                {
                    where += string.Format(" and  c.cust_type = '{0}'", cbo_cust_type.SelectedValue);
                }
                if (cbo_is_member.SelectedValue != null && cbo_is_member.SelectedValue.ToString() != "")
                {
                    where += string.Format(" and  c.is_member = '{0}'", cbo_is_member.SelectedValue);
                }
                if (cboprovince.SelectedValue != null && cboprovince.SelectedValue.ToString() != "")
                {
                    where += string.Format(" and  c.province = '{0}'", cboprovince.SelectedValue);
                }
                if (cbocity.SelectedValue != null && cbocity.SelectedValue.ToString() != "")
                {
                    where += string.Format(" and  c.city = '{0}'", cbocity.SelectedValue);
                }
                if (cbocounty.SelectedValue != null && cbocounty.SelectedValue.ToString() != "")
                {
                    where += string.Format(" and  c.county = '{0}'", cbocounty.SelectedValue);
                }
                if (!string.IsNullOrEmpty(txtcust_address.Caption.Trim()))
                {
                    where += string.Format(" and  c.cust_address like '%{0}%'", txtcust_address.Caption.Trim());
                }
                if (dtp_create_time_s.Value.Trim() != "" && Validator.IsDateTime(dtp_create_time_s.Value))
                {
                    var startTicks = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dtp_create_time_s.Value).Date);
                    where += " and c.create_time>=" + startTicks;
                }
                if (dtp_create_time_e.Value.Trim() != "" && Validator.IsDateTime(dtp_create_time_e.Value))
                {
                    var endTicks = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dtp_create_time_e.Value).Date.AddDays(1));
                    where += " and c.create_time<" + endTicks;
                }
                if (cbo_status.SelectedValue != null && cbo_status.SelectedValue.ToString() != "")
                {
                    where += string.Format(" and  c.status = '{0}'", cbo_status.SelectedValue);
                }
                if (cbo_data_source.SelectedValue != null && cbo_data_source.SelectedValue.ToString() != "")
                {
                    where += string.Format(" and  c.data_source = '{0}'", cbo_data_source.SelectedValue);
                }
                if (!string.IsNullOrEmpty(txt_yt_customer_manager.Caption.Trim()))
                {
                    where += string.Format(" and  c.yt_customer_manager like '%{0}%'", txt_yt_customer_manager.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txt_yt_sap_code.Caption.Trim()))
                {
                    where += string.Format(" and  c.yt_sap_code like '%{0}%'", txt_yt_sap_code.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(uccont_name.ResultWords.Trim().Replace("请输入名称、手机或电话", "")))
                {
                    where += string.Format(" and  c.cust_id in {0}",
                        " ( select distinct b.relation_object_id from tb_contacts c,tr_base_contacts b where c.cont_id=b.cont_id and b.relation_object='客户档案' and c.cont_name='" + uccont_name.ResultWords.Trim().Replace("请输入名称、手机或电话", "") + "' ) "
                        );
                }


                var al = new ArrayList {"sys_enterprise_property", "sys_data_source", "sys_customer_category"};
                _dtdic = CommonFuncCall.GetDictionariesByPDic_codes(al);

                int recordCount;
                var dt = DBHelper.GetTableByPage("分页查询客户档案", "tb_customer c left join sys_user u on c.create_by=u.user_id", "c.*,u.user_name", where, "", "c.create_time", pageQ.PageIndex, pageQ.PageSize, out recordCount);
                dgv_table.DataSource = dt;
                pageQ.RecordCount = recordCount;
                pageQ.SetBtnState();
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Event -- 事件

        #endregion
    }
}
