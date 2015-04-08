using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;

namespace HXCPcClient.UCForm.CustomerService.Feedback
{
    ///*************************************************************************//
    /// System:       HuiXuCheSYS
    /// FileName:     UCFeedbackManager         
    /// Author:       Kord
    /// Date:         2014/10/22 13:40:36
    /// Machine Name: KORD-PC
    ///***************************************************************************//
    /// Function: 
    /// 	客户服务-信息反馈
    ///***************************************************************************//
    public partial class UCFeedbackManager : UCBase
    {
        #region Constructor -- 构造函数
        public UCFeedbackManager()
        {
            InitializeComponent();

            UIAssistants.SetButtonStyle4QueryAndClear(btnQuery, btnClear, true);  //设置查询按钮和清除按钮样式
            DataGridViewEx.SetDataGridViewStyle(dgvQData, drtxt_status);   //设置数据表格样式,并将最后一列填充其余空白

            Init();

            Load += (sender, args) => UIAssistants.SetDataGridViewCheckColumn(dgvQData, drchk_check);
        }
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
            CommonCtrl.BindComboBoxByDictionarr(cboCBType, "sys_callback_type", true);    //绑定反馈类型
            CommonCtrl.BindComboBoxByDictionarr(cboCBMode, "sys_callback_mode", true);    //绑定反馈方式
            #endregion

            #region 注册功能按钮事件
            #region 清除查询条件
            btnClear.Click += delegate 
            {
                txtCBTitle.Caption = String.Empty;
                cboCBType.SelectedValue = String.Empty;
                cboCBMode.SelectedValue = String.Empty;
                dtpCBSTime.Value = DateTime.Now.AddMonths(-1);
                dtpCBETime.Value = DateTime.Now;
                txtCustomerName.Text = String.Empty;
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

            #region 选择反馈人
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
            dgvQData.CellDoubleClick += delegate
            {
                if (dgvQData.SelectedRows.Count > 0)
                {
                    var uc = new UCFeedbackAddOrEdit();
                    uc.OpType = WindowStatus.View;
                    uc.UCFeedbakcManager = this;
                    var selectedData = dgvQData.SelectedRows[0];
                    uc.FeedbackId = selectedData.Cells["drtxt_Feedback_id"].Value.ToString();
                    var callbackInfo = DBHelper.GetTable("查询反馈信息", "tb_CustomerSer_Feedback", "*",
                        "Feedback_id = '" + uc.FeedbackId + "'", "",
                        "");
                    if (callbackInfo != null && callbackInfo.DefaultView.Count != 0)
                    {
                        uc.CustId = callbackInfo.DefaultView[0]["corp_id"].ToString();
                        uc.ContId = callbackInfo.DefaultView[0]["Feedback_by"].ToString();
                        uc.HandleId = callbackInfo.DefaultView[0]["handle_name"].ToString();
                    }
                    uc.addUserControl(uc, "信息反馈-详细", "UCFeedbackView", Tag.ToString(), Name);
                }
            };
            #endregion

            #region 数据表格数据格式化
            dgvQData.CellFormatting += delegate(object sender, DataGridViewCellFormattingEventArgs args)
            {
                UIAssistants.DgvCellDataConvert2Table(dgvQData, args, "Feedback_by", "tb_contacts", "cont_id", "cont_name");
                UIAssistants.DgvCellDataConvert2Table(dgvQData, args, "handle_name", "sys_user", "user_id", "user_name");
                UIAssistants.DgvCellDataConvert2Table(dgvQData, args, "corp_id", "tb_customer", "cust_id", "cust_name");
                UIAssistants.DgvCellDataConvert2Datetime(dgvQData, args, "Feedback_time");
                UIAssistants.DgvCellDataConvert2DicData(dgvQData, args, "Feedback_type");    //反馈类型
                UIAssistants.DgvCellDataConvert2DicData(dgvQData, args, "Feedback_mode");    //反馈形式
            };
            #endregion

            #region 新增数据
            AddEvent += delegate
            {
                var uc = new UCFeedbackAddOrEdit();
                uc.UCFeedbakcManager = this;
                uc.OpType = WindowStatus.Add;
                uc.addUserControl(uc, "信息反馈-新增", "UCFeedbackAdd", Tag.ToString(), Name);
            };
            #endregion

            #region 编辑数据
            EditEvent += delegate
            {
                if (dgvQData.SelectedRows.Count > 0)
                {
                    var uc = new UCFeedbackAddOrEdit();
                    uc.OpType = WindowStatus.Edit;
                    uc.UCFeedbakcManager = this;
                    var selectedData = dgvQData.SelectedRows[0];
                    uc.FeedbackId = selectedData.Cells["drtxt_Feedback_id"].Value.ToString();
                    var callbackInfo = DBHelper.GetTable("查询反馈信息", "tb_CustomerSer_Feedback", "*",
                        "Feedback_id = '" + uc.FeedbackId + "'", "",
                        "");
                    if (callbackInfo != null && callbackInfo.DefaultView.Count != 0)
                    {
                        uc.CustId = callbackInfo.DefaultView[0]["corp_id"].ToString();
                        uc.ContId = callbackInfo.DefaultView[0]["Feedback_by"].ToString();
                        uc.HandleId = callbackInfo.DefaultView[0]["handle_name"].ToString();
                    }
                    uc.addUserControl(uc, "信息反馈-编辑", "UCFeedbackEdit", Tag.ToString(), Name);
                }
                BindPageData("1=1");
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
                    var listField = (from DataGridViewRow selectedRow in dgvQData.SelectedRows select selectedRow.Cells["drtxt_Feedback_id"].Value.ToString()).ToList();

                    var result = MessageBoxEx.Show("此操作将永久删除选定信息。\n若要继续，请单击“确定”。", "您确认要删除此条信息吗？", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);
                    if (result == DialogResult.OK)
                    {
                        var flag = DBHelper.DeleteDataByID("删除反馈信息", "tb_CustomerSer_Feedback", "Feedback_id", listField[0]);
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
                BindPageData("1=1");
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
                    MessageBoxEx.Show("反馈结束时间小于反馈开始时间");
                    dtpCBETime.Focus();
                    return;
                }
                where = "'1' = '1'";

                if (!string.IsNullOrEmpty(txtCBTitle.Caption.Trim()))  //反馈标题
                {
                    where += string.Format(" and title like '%{0}%'", txtCBTitle.Caption.Trim());
                }
                if (cboCBType.SelectedValue != null && !string.IsNullOrEmpty(cboCBType.SelectedValue.ToString()))  //反馈类型
                {
                    where += string.Format(" and Feedback_type like '%{0}%'", cboCBType.SelectedValue);
                }
                if (cboCBType.SelectedValue != null && !string.IsNullOrEmpty(cboCBMode.SelectedValue.ToString()))  //反馈形式
                {
                    where += string.Format(" and Feedback_mode like '%{0}%'", cboCBMode.SelectedValue);
                }
                if (!string.IsNullOrEmpty(txtCustomerName.Text))    //客户名称
                {
                    where += string.Format(" and  corp_id like '%{0}%'", txtCustomerName.Tag);
                }
                //反馈人
                if (!string.IsNullOrEmpty(txtCBPerson.Text))
                {
                    where += string.Format(" and Feedback_by like '%{0}%'", txtCBPerson.Tag);
                }

                where += " and create_time>=" + startTicks;
                where += " and create_time<" + endTicks;
            }
            int recordCount;
            var dt = DBHelper.GetTableByPage("分页查询信息回访记录", "tb_CustomerSer_Feedback", "*", where, "", "status desc,Feedback_time desc", pageQ.PageIndex, pageQ.PageSize, out recordCount);
            dgvQData.DataSource = dt;
            pageQ.RecordCount = recordCount;
        }

        private List<string> GetCheckRows()
        {
            return (from DataGridViewRow dr in dgvQData.Rows let isCheck = dr.Cells[drchk_check.Name].EditedFormattedValue where isCheck != null && (bool)isCheck select dr.Cells[drtxt_Feedback_id.Name].Value.ToString()).ToList();
        }
        private DataView GetSelectedRowData()
        {
            var selectedRows = (from DataGridViewRow dr in dgvQData.Rows let isCheck = dr.Cells[drchk_check.Name].EditedFormattedValue where isCheck != null && (bool)isCheck select dr.Cells[drtxt_Feedback_id.Name].Value.ToString()).ToList();
            if (selectedRows.Count > 0)
            {
                var whereSqlSb = new StringBuilder();
                whereSqlSb.Append(" 1=1");
                foreach (var selectedRow in selectedRows)
                {
                    whereSqlSb.Append(" or Feedback_id ='");
                    whereSqlSb.Append(selectedRow);
                    whereSqlSb.Append("'");
                }
                var whereSql = whereSqlSb.ToString().Replace(" 1=1 or ", "");
                var data = DBHelper.GetTable("根据ID获取客户反馈信息", "tb_CustomerSer_Feedback", "*", whereSql, "", "");
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
            UIAssistants.SetUCBaseFuncationVisible(this, new ObservableCollection<ButtonEx_sms>()
            {
                btnAdd, btnEdit, btnDelete, btnExport,btnSet,btnView,btnPrint
            });
            btnEdit.Enabled = btnDelete.Enabled = btnStatus.Enabled = false;
            #endregion
        }
        #endregion

        #region Event -- 事件
        #endregion
    }
}
