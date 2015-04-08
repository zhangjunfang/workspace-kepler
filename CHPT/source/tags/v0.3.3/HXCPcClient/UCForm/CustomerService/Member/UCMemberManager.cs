using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using SYSModel;
using Utility.Common;

namespace HXCPcClient.UCForm.CustomerService.Member
{
    ///*************************************************************************//
    /// System:       HuiXuCheSYS
    /// FileName:     MemberManager         
    /// Author:       Kord
    /// Date:         2014/10/22 13:40:36
    /// Machine Name: KORD-PC
    ///***************************************************************************//
    /// Function: 
    /// 	客户服务-会员管理
    ///***************************************************************************//
    public partial class UCMemberManager : UCBase
    {
        #region Constructor -- 构造函数
        public UCMemberManager()
        {
            InitializeComponent();  //系统自动初始化界面方法
            
            UIAssistants.SetButtonStyle4QueryAndClear(btnQuery, btnClear,true);  //设置查询按钮和清除按钮样式
            DataGridViewEx.SetDataGridViewStyle(dgvQData, drtxt_remark);   //设置数据表格样式,并将最后一列填充其余空白

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
            dtp_create_time_s.Value = DateTime.Now.AddMonths(-1);
            dtp_create_time_e.Value = DateTime.Now.AddDays(+1);
            #endregion

            #region 初始化下拉框数据绑定
            CommonCtrl.BindComboBoxByDictionarr(cbo_member_grade, "sys_member_grade", true);    //绑定会员等级
            DataSources.BindComBoxDataEnum(cbo_status, typeof(DataSources.EnumStatus), true);//绑定会员状态
            #endregion

            #region 注册功能按钮事件
            #region 清除查询条件
            btnClear.Click += delegate 
            {
                txt_vip_code.Caption = String.Empty;
                cbo_member_grade.SelectedValue = String.Empty;
                cbo_status.SelectedValue = String.Empty;
                dtp_create_time_s.Value = DateTime.Now.AddMonths(-1);
                dtp_create_time_e.Value = DateTime.Now.AddDays(1);
                txt_cust_name.Text = String.Empty;
                txt_legal_person.Caption = String.Empty;
            };
            #endregion

            #region 查询
            btnQuery.Click += delegate
            {
                BindPageData();
            };
            #endregion

            #region 选择客户信息
            txt_cust_name.ChooserClick += delegate 
            {
                var frmCustomer = new frmCustomerInfo();
                var result = frmCustomer.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txt_cust_name.Text = frmCustomer.strCustomerName;
                    txt_cust_name.Tag = frmCustomer.strCustomerId;
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
            dgvQData.CellDoubleClick += delegate
            {
                if (dgvQData.SelectedRows.Count > 0)
                {
                    var uc = new UCMemberViewDetail();
                    uc.OpType = WindowStatus.View;
                    uc.UCMemberManager = this;
                    var selectedData = dgvQData.SelectedRows[0];
                    uc.VipId = selectedData.Cells["drtxt_vip_id"].Value.ToString();
                    var callbackInfo = DBHelper.GetTable("查询会员信息", "v_member", "*",
                        "vip_id = '" + uc.VipId + "'", "",
                        "");
                    if (callbackInfo != null && callbackInfo.DefaultView.Count != 0)
                    {
                        uc.CustId = callbackInfo.DefaultView[0]["corp_id"].ToString();
                    }
                    uc.addUserControl(uc, "会员管理-详细信息", "UCMemberView", Tag.ToString(), Name);
                }
            };
            #endregion

            #region 数据表格数据格式化
            dgvQData.CellFormatting += delegate(object sender, DataGridViewCellFormattingEventArgs args)
            {
                UIAssistants.DgvCellDataConvert2Datetime(dgvQData, args, "create_time");
                UIAssistants.DgvCellDataConvert2Datetime(dgvQData, args, "validity_time");
                UIAssistants.DgvCellDataConvert2DicData(dgvQData, args, "member_grade");
                UIAssistants.DgvCellDataConvert2Enum(dgvQData, args, "status", typeof(DataSources.EnumStatus));    //状态
            };
            #endregion

            #region 新增数据
            AddEvent += delegate
            {
                var uc = new UCMemberAddOrEdit();
                uc.UCMemberManager = this;
                uc.OpType = WindowStatus.Add;
                uc.addUserControl(uc, "会员管理-新增", "UCMemberAdd", Tag.ToString(), Name);
            };
            #endregion

            #region 编辑数据
            EditEvent += delegate
            {
                if (dgvQData.SelectedRows.Count > 0)
                {
                    var uc = new UCMemberAddOrEdit();
                    uc.OpType = WindowStatus.Edit;
                    uc.UCMemberManager = this;
                    var selectedData = dgvQData.SelectedRows[0];
                    uc.VipId = selectedData.Cells["drtxt_vip_id"].Value.ToString();
                    var callbackInfo = DBHelper.GetTable("查询会员信息", "v_member", "*",
                        "vip_id = '" + uc.VipId + "'", "",
                        "");
                    if (callbackInfo != null && callbackInfo.DefaultView.Count != 0)
                    {
                        uc.VipId = callbackInfo.DefaultView[0]["vip_id"].ToString();
                        uc.CustId = callbackInfo.DefaultView[0]["cust_id"].ToString();
                        uc.SelectedRow = callbackInfo.DefaultView[0];
                    }
                    uc.addUserControl(uc, "会员管理-编辑", "UCMemberEdit", Tag.ToString(), Name);
                }
            };
            #endregion

            #region 删除数据
            btnDelete.Click += delegate
            {
                try
                {
                    if (dgvQData.SelectedRows.Count == 0)
                    {
                        MessageBoxEx.Show("请选择删除记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    var listField = (from DataGridViewRow selectedRow in dgvQData.SelectedRows select selectedRow.Cells["drtxt_vip_id"].Value.ToString()).ToList();

                    var result = MessageBoxEx.Show("此操作将永久删除选定信息。\n若要继续，请单击“确定”。", "您确认要删除此条信息吗？", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);
                    if (result == DialogResult.OK)
                    {
                        var flag = DBHelper.DeleteDataByID("删除客户回访信息", "tb_CustomerSer_Member", "vip_id", listField[0]);
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
                catch (Exception ex)
                {
                    MessageBoxEx.Show("删除失败！" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            #endregion
            #endregion

            #region 预览
            ViewEvent += delegate
            {
                if (dgvQData.SelectedRows.Count > 0)
                {
                    var uc = new UCMemberViewDetail();
                    uc.OpType = WindowStatus.View;
                    uc.UCMemberManager = this;
                    var selectedData = dgvQData.SelectedRows[0];
                    uc.VipId = selectedData.Cells["drtxt_vip_id"].Value.ToString();
                    var callbackInfo = DBHelper.GetTable("查询会员信息", "v_member", "*",
                        "vip_id = '" + uc.VipId + "'", "",
                        "");
                    if (callbackInfo != null && callbackInfo.DefaultView.Count != 0)
                    {
                        uc.CustId = callbackInfo.DefaultView[0]["corp_id"].ToString();
                    }
                    uc.addUserControl(uc, "会员管理-详细信息", "UCMemberView", Tag.ToString(), Name);
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
                            flag = DBHelper.Submit_AddOrEdit("启用/停用会员状态", "v_member", "vip_id",
                                CommonCtrl.IsNullToString(view[drtxt_vip_id.DataPropertyName]),
                                new Dictionary<string, string>
                                {
                                    {
                                        "status",
                                        status == "1" ? "0" : "1"
                                    },
                                    {
                                        "update_by", GlobalStaticObj.UserID
                                    },
                                    {
                                        "update_time",
                                        Common.LocalDateTimeToUtcLong(GlobalStaticObj.CurrentDateTime).ToString(CultureInfo.InvariantCulture)
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
                btnView.Enabled = false;
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
                        btnView.Enabled = true;
                    }
                    #endregion

                    #region 启用/停用
                    var qy = 0;
                    var jy = 0;
                    foreach (DataRowView rowView in dataView)   //启用/停用
                    {
                        if (rowView["status"].ToString() == "1")
                        {
                            qy++;
                        }
                        else if (rowView["status"].ToString() == "0")
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
                        btnStatus.Caption = "停用";
                        btnStatus.Width = 60;
                    }
                    else
                    {
                        btnStatus.Enabled = false;
                        btnStatus.Caption = "启用/停用";
                        btnStatus.Width = 90;
                    }
                    #endregion
                }
            };
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
                var startTicks = dtp_create_time_s.Value.Date.ToUniversalTime().Ticks;
                var endTicks = dtp_create_time_e.Value.Date.ToUniversalTime().Ticks;
                if (endTicks < startTicks)
                {
                    MessageBoxEx.Show("反馈结束时间小于反馈开始时间");
                    dtp_create_time_e.Focus();
                    return;
                }
                where = "'1' = '1'";

                if (!string.IsNullOrEmpty(txt_vip_code.Caption.Trim()))  //会员编号
                {
                    where += string.Format(" and vip_code like '%{0}%'", txt_vip_code.Caption.Trim());
                }
                if (cbo_member_grade.SelectedValue != null && !string.IsNullOrEmpty(cbo_member_grade.SelectedValue.ToString()))  //会员等级
                {
                    where += string.Format(" and member_grade like '%{0}%'", cbo_member_grade.SelectedValue);
                }
                if (!string.IsNullOrEmpty(txt_cust_name.Text.Trim()))  //会员名称
                {
                    where += string.Format(" and cust_name like '%{0}%'", txt_cust_name.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txt_legal_person.Caption))    //联系人
                {
                    where += string.Format(" and  legal_person like '%{0}%'", txt_legal_person.Caption);
                }
                if (cbo_status.SelectedValue != null && !string.IsNullOrEmpty(cbo_status.SelectedValue.ToString()))  //会员等级
                {
                    where += string.Format(" and status like '%{0}%'", cbo_status.SelectedValue);
                }
                where += " and create_time>=" + startTicks;
                where += " and create_time<" + endTicks;
            }
            int recordCount;
            var dt = DBHelper.GetTableByPage("分页查询会员信息", "v_member", "*", where, "", "update_time desc", pageQ.PageIndex, pageQ.PageSize, out recordCount);
            dgvQData.DataSource = dt;
            pageQ.RecordCount = recordCount;
            SetUCBaseFuncationVisible();
        }
        private List<string> GetCheckRows()
        {
            return (from DataGridViewRow dr in dgvQData.Rows let isCheck = dr.Cells[drchk_check.Name].EditedFormattedValue where isCheck != null && (bool)isCheck select dr.Cells[drtxt_vip_id.Name].Value.ToString()).ToList();
        }
        private DataView GetSelectedRowData()
        {
            var selectedRows = (from DataGridViewRow dr in dgvQData.Rows let isCheck = dr.Cells[drchk_check.Name].EditedFormattedValue where isCheck != null && (bool)isCheck select dr.Cells[drtxt_vip_id.Name].Value.ToString()).ToList();
            if (selectedRows.Count > 0)
            {
                var whereSqlSb = new StringBuilder();
                whereSqlSb.Append(" 1=1");
                foreach (var selectedRow in selectedRows)
                {
                    whereSqlSb.Append(" or vip_id ='");
                    whereSqlSb.Append(selectedRow);
                    whereSqlSb.Append("'");
                }
                var whereSql = whereSqlSb.ToString().Replace(" 1=1 or ", "");
                var data = DBHelper.GetTable("根据ID获取会员信息", "v_member", "*", whereSql, "", "");
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
            UIAssistants.SetUCBaseFuncationVisible(this,
                new ObservableCollection<ButtonEx_sms>
                {
                    btnAdd,
                    btnEdit,
                    btnDelete,
                    btnStatus,
                    btnExport,
                    btnSet,
                    btnView,
                    btnPrint
                });
            btnView.Enabled = btnEdit.Enabled = btnDelete.Enabled = btnStatus.Enabled = false;
            #endregion
        }
        #endregion

        #region Event -- 事件
        #endregion
    }
}
