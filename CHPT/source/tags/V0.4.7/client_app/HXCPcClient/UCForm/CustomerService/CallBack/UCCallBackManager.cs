using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClientBLL;
using HXCPcClient.Chooser;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using Utility.Common;

namespace HXCPcClient.UCForm.CustomerService.CallBack
{
    ///*************************************************************************//
    /// System:       HuiXuCheSYS
    /// FileName:     CallBackManager         
    /// Author:       Kord
    /// Date:         2014/10/22 13:40:36
    /// Machine Name: KORD-PC
    ///***************************************************************************//
    /// Function: 
    /// 	客户服务-客户回访
    ///***************************************************************************//
    public partial class UCCallBackManager : UCBase
    {
        #region Constructor -- 构造函数
        public UCCallBackManager()
        {
            InitializeComponent();

            UIAssistants.SetButtonStyle4QueryAndClear(btnQuery, btnClear, true);  //设置查询按钮和清除按钮样式
            DataGridViewEx.SetDataGridViewStyle(dgvQData, drtxt_status);   //设置数据表格样式,并将最后一列填充其余空白

            Init();

            Load += (sender, args) => UIAssistants.SetDataGridViewCheckColumn(dgvQData, drchk_check);

            SetContentMenuScrip(dgvQData);
        }
        #endregion

        #region Field -- 字段

        #endregion

        #region Property -- 属性

        #endregion

        #region Method -- 方法
        //初始化
        private void Init()
        {
            dgvQData.MultiSelect = false;    //不允许多选操作
            SetUCBaseFuncationVisible();
            #region 初始化时间控件值
            dtpCBSTime.Value = DateTime.Now.AddMonths(-1);
            dtpCBETime.Value = DateTime.Now.AddDays(+1);
            #endregion

            #region 初始化下拉框数据绑定
            CommonCtrl.BindComboBoxByDictionarr(cboCBType, "sys_callback_type", true);    //绑定回访类型
            CommonCtrl.BindComboBoxByDictionarr(cboCBMode, "sys_callback_mode", true);    //绑定回访方式
            #endregion

            #region 注册功能按钮事件
            #region 清除查询条件
            btnClear.Click += delegate 
            {
                txtCBTitle.Caption = String.Empty;
                cboCBType.SelectedValue = String.Empty;
                cboCBMode.SelectedValue = String.Empty;
                dtpCBSTime.Value = DateTime.Now.AddMonths(-1);
                dtpCBETime.Value = DateTime.Now.AddDays(1);
                txtCustomerName.Text = String.Empty;
                txtCustomerName.Tag = String.Empty;
                txtCBPerson.Text = String.Empty;
            };
            #endregion

            #region 查询
            btnQuery.Click += delegate
            {
                BindPageData();
            };
            #endregion

            #region 选择客户信息
            txtCustomerName.ChooserClick += delegate 
            {
                var frmCustomer = new frmCustomerInfo();
                var result = frmCustomer.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtCustomerName.Text = frmCustomer.strCustomerName;
                    txtCustomerName.Tag = frmCustomer.strCustomerId;
                }
            };
            #endregion
             
            #region 选择回访人
            txtCBPerson.ChooserClick += delegate
            {
                var frmContacts= new frmContacts();
                var result = frmContacts.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtCBPerson.Text = frmContacts.contName;
                    txtCBPerson.Tag = frmContacts.contID;
                }
            };
            #endregion

            #region 数据翻页
            pageQ.PageIndexChanged += delegate
            {
                BindPageData();
            };
            #endregion

            #region 数据表数据选择
            dgvQData.CellDoubleClick += delegate(object sender, DataGridViewCellEventArgs args)
            {
                if (args.ColumnIndex <= 0) return;
                var uc = new UCCallBackAddOrEdit {windowStatus = WindowStatus.View, UCCallBackManager = this};
                var selectedData = dgvQData.SelectedRows[0];
                uc.CallBackId = selectedData.Cells["drtxt_Callback_id"].Value.ToString();
                var callbackInfo = DBHelper.GetTable("查询回访信息", "tb_CustomerSer_Callback", "*",
                    "Callback_id = '" + uc.CallBackId + "'", "",
                    "");
                if (callbackInfo != null && callbackInfo.DefaultView.Count != 0)
                {
                    uc.CustId = callbackInfo.DefaultView[0]["Callback_corp"].ToString();
                    uc.ContId = callbackInfo.DefaultView[0]["Callback_by"].ToString();
                    uc.HandleId = callbackInfo.DefaultView[0]["handle_name"].ToString();
                }
                uc.addUserControl(uc, "客户回访-预览", "UCCallBackView", Tag.ToString(), Name);
            };
            #endregion

            #region 数据表格数据格式化
            dgvQData.CellFormatting += delegate(object sender, DataGridViewCellFormattingEventArgs args)
            {
                UIAssistants.DgvCellDataConvert2Table(dgvQData, args, "Callback_by", "tb_contacts", "cont_id", "cont_name");
                UIAssistants.DgvCellDataConvert2Table(dgvQData, args, "handle_name", "sys_user", "user_id", "user_name");
                UIAssistants.DgvCellDataConvert2Table(dgvQData, args, "Callback_corp", "tb_customer", "cust_id", "cust_name");
                UIAssistants.DgvCellDataConvert2Datetime(dgvQData, args, "Callback_time");
                UIAssistants.DgvCellDataConvert2DicData(dgvQData, args, "Callback_type");    //访问类型
                UIAssistants.DgvCellDataConvert2DicData(dgvQData, args, "Callback_mode");    //访问形式
                UIAssistants.DgvCellDataConvert2DicData(dgvQData, args, "status");    //状态

            };
            #endregion

            #region 新增数据
            AddEvent += delegate
            {
                var uc = new UCCallBackAddOrEdit();
                uc.UCCallBackManager = this;
                uc.windowStatus = WindowStatus.Add;
                uc.addUserControl(uc, "客户回访-新增", "UCCallBackAdd", Tag.ToString(), Name);
            };
            #endregion

            #region 编辑数据
            EditEvent += delegate
            {
                if (dgvQData.SelectedRows.Count > 0)
                {
                    var uc = new UCCallBackAddOrEdit();
                    uc.windowStatus = WindowStatus.Edit;
                    uc.UCCallBackManager = this;
                    var dataView = GetSelectedRowData();
                    var listField = GetCheckRows();
                    if (dataView != null && listField.Count > 0)
                    {
                        var selectedData = dataView[0];
                        uc.CallBackId = CommonCtrl.IsNullToString(selectedData["Callback_id"]);
                        var callbackInfo = DBHelper.GetTable("查询回访信息", "tb_CustomerSer_Callback", "*",
                            "Callback_id = '" + uc.CallBackId + "'", "",
                            "");
                        if (callbackInfo != null && callbackInfo.DefaultView.Count != 0)
                        {
                            uc.CustId = callbackInfo.DefaultView[0]["Callback_corp"].ToString();
                            uc.ContId = callbackInfo.DefaultView[0]["Callback_by"].ToString();
                            uc.HandleId = callbackInfo.DefaultView[0]["handle_name"].ToString();
                        }
                        uc.addUserControl(uc, "客户回访-编辑", "UCCallBackEdit", Tag.ToString(), Name);
                    }
                }
            };
            #endregion

            #region 删除数据
            DeleteEvent += delegate
            {
                try
                {
                    if (dgvQData.SelectedRows.Count == 0)
                    {
                        MessageBoxEx.Show("请选择删除记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    var dataView = GetSelectedRowData();
                    var listField = GetCheckRows();
                    if (dataView != null && listField.Count > 0)
                    {
                        var selectedData = dataView[0];
                        var callBackId = CommonCtrl.IsNullToString(selectedData["Callback_id"]);
                        var result = MessageBoxEx.Show("此操作将永久删除选定信息。\n若要继续，请单击“确定”。", "您确认要删除此条信息吗？", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);
                        if (result == DialogResult.OK)
                        {
                            var flag = DBHelper.BatchDeleteDataByWhere("删除客户回访信息", "tb_CustomerSer_Callback", String.Format("Callback_id = '{0}'", callBackId));
                            if (flag)
                            {
                                BindPageData();
                                MessageBoxEx.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                BindPageData();
                            }
                            else
                            {
                                MessageBoxEx.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxEx.Show("删除失败！" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            #endregion

            #region 启用/禁用
            StatusEvent += delegate
            {
                if (dgvQData.SelectedRows.Count > 0)
                {
                    var dataView = GetSelectedRowData();
                    var listField = GetCheckRows();
                    if (listField.Count >= 1)
                    {
                        if (MessageBoxEx.Show("确认要" + btnStatus.Caption + "吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                        {
                            return;
                        }
                        var flag = false;
                        foreach (DataRowView view in dataView)
                        {
                            var status = CommonCtrl.IsNullToString(view[drtxt_status.DataPropertyName]);
                            flag = DBHelper.Submit_AddOrEdit("启用/停用客户回访记录", "tb_CustomerSer_Callback", "Callback_id",
                                CommonCtrl.IsNullToString(view[drtxt_Callback_id.DataPropertyName]),
                                new Dictionary<string, string>
                                {
                                    {
                                        "status",
                                        status == DbDic2Enum.SYS_DATA_STATUS_QI
                                            ? DbDic2Enum.SYS_DATA_STATUS_JIN
                                            : DbDic2Enum.SYS_DATA_STATUS_QI
                                    },
                                    {
                                        "update_by", GlobalStaticObj.UserID
                                    },
                                    {
                                        "update_time",
                                        Common.LocalDateTimeToUtcLong(GlobalStaticObj.CurrentDateTime).ToString()
                                    }
                                });
                        }
                        MessageBoxEx.Show(btnStatus.Caption + (flag ? "成功！" : "失败！"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BindPageData();
                    }
                }
            };
            #endregion

            #region 根据选择的数据判断功能按钮的显示状态
            dgvQData.CellMouseUp += delegate
            {
                var dataView = GetSelectedRowData();
                var listField = GetCheckRows();
                btnStatus.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                if (dataView == null || listField.Count == 0)
                {

                }
                else
                {
                    #region 编辑
                    if (listField.Count == 1)
                    {
                        btnEdit.Enabled = true;
                        btnDelete.Enabled = true;
                    }
                    #endregion

                    #region 启用/禁用
                    var qy = 0;
                    var jy = 0;
                    foreach (DataRowView rowView in dataView)   //启用/禁用
                    {
                        if (rowView["status"].ToString() == DbDic2Enum.SYS_DATA_STATUS_QI)
                        {
                            qy++;
                        }
                        else if (rowView["status"].ToString() == DbDic2Enum.SYS_DATA_STATUS_JIN)
                        {
                            jy++;
                        }
                    }
                    if (qy == 0 && jy != 0)
                    {
                        btnStatus.Enabled = true;
                        btnStatus.Caption = "启用";
                        btnStatus.Width = 60;
                    }
                    else if (qy != 0 && jy == 0)
                    {
                        btnStatus.Enabled = true;
                        btnStatus.Caption = "禁用";
                        btnStatus.Width = 60;
                    }
                    else
                    {
                        btnStatus.Enabled = false;
                        btnStatus.Caption = "启用/禁用";
                        btnStatus.Width = 90;
                    }
                    #endregion
                }
            };
            #endregion

            #endregion
            BindPageData();
        }
        /// <summary> 绑定数据
        /// </summary>
        public void BindPageData(string where = "")
        {
            if (String.IsNullOrEmpty(where))
            {
                //回访时间
                var startTicks = dtpCBSTime.Value.Date.ToUniversalTime().Ticks;
                var endTicks = dtpCBETime.Value.Date.ToUniversalTime().Ticks;
                if (endTicks < startTicks)
                {
                    MessageBoxEx.Show("回访结束时间小于回访开始时间");
                    dtpCBETime.Focus();
                    return;
                }
                where = "'1' = '1'";

                if (!string.IsNullOrEmpty(txtCBTitle.Caption.Trim()))  //回访标题
                {
                    where += string.Format(" and title like '%{0}%'", txtCBTitle.Caption.Trim());
                }
                if (cboCBType.SelectedValue != null && !string.IsNullOrEmpty(cboCBType.SelectedValue.ToString()))  //回访类型
                {
                    where += string.Format(" and Callback_type like '%{0}%'", cboCBType.SelectedValue);
                }
                if (cboCBType.SelectedValue != null && !string.IsNullOrEmpty(cboCBMode.SelectedValue.ToString()))  //回访形式
                {
                    where += string.Format(" and Callback_mode like '%{0}%'", cboCBMode.SelectedValue);
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtCustomerName.Tag)))    //客户名称
                {
                    where += string.Format(" and  Callback_corp like '%{0}%'", txtCustomerName.Tag);
                }

                //回访人
                if (!string.IsNullOrEmpty(txtCBPerson.Text))
                {
                    where += string.Format(" and  Callback_by like '%{0}%'", CommonCtrl.IsNullToString(txtCBPerson.Tag));
                }
                where += " and create_time>=" + startTicks;
                where += " and create_time<" + endTicks;
            }
            int recordCount;
            var dt = DBHelper.GetTableByPage("分页查询客户回访记录", "tb_CustomerSer_Callback", "*", where, "", "status desc,Callback_time desc", pageQ.PageIndex, pageQ.PageSize, out recordCount);
            dgvQData.DataSource = dt;
            pageQ.RecordCount = recordCount;
            SetUCBaseFuncationVisible();
        }
        private List<string> GetCheckRows()
        {
            return (from DataGridViewRow dr in dgvQData.Rows let isCheck = dr.Cells[drchk_check.Name].EditedFormattedValue where isCheck != null && (bool)isCheck select dr.Cells[drtxt_Callback_id.Name].Value.ToString()).ToList();
        }
        private DataView GetSelectedRowData()
        {
            var selectedRows = (from DataGridViewRow dr in dgvQData.Rows let isCheck = dr.Cells[drchk_check.Name].EditedFormattedValue where isCheck != null && (bool)isCheck select dr.Cells[drtxt_Callback_id.Name].Value.ToString()).ToList();
            if (selectedRows.Count > 0)
            {
                var whereSqlSb = new StringBuilder();
                whereSqlSb.Append(" 1=1");
                foreach (var selectedRow in selectedRows)
                {
                    whereSqlSb.Append(" or Callback_id ='");
                    whereSqlSb.Append(selectedRow);
                    whereSqlSb.Append("'");
                }
                var whereSql = whereSqlSb.ToString().Replace(" 1=1 or ", "");
                var data = DBHelper.GetTable("根据ID获取客户回访信息", "tb_CustomerSer_Callback", "*", whereSql, "", "");
                if (data != null && data.DefaultView != null)
                {
                    return data.DefaultView;
                }
            }
            return null;
        }
        private void SetUCBaseFuncationVisible()
        {
            #region 设置功能按钮可见性
            UIAssistants.SetUCBaseFuncationVisible(this, new ObservableCollection<ButtonEx_sms>
            {
                btnAdd, btnEdit, btnDelete, btnStatus, btnView, btnExport, btnSet, btnPrint
            });
            btnEdit.Enabled = btnDelete.Enabled = btnStatus.Enabled = false;
            #endregion
        }
        #endregion

        #region Event -- 事件
        #endregion
    }
}
