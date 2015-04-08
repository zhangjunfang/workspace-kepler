
using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using HXCPcClient.CommonClass;
using Kord.LogService;
using ServiceStationClient.ComponentUI;
using Utility.Log;

namespace HXCPcClient.UCForm.RepairBusiness.YuTongThreeGuaranty.MaintainThreeGuaranty
{
    ///*************************************************************************//
    /// System:       HuiXuCheSYS
    /// FileName:     UCMaintainThreeGuarantyQuery         
    /// Author:       Kord
    /// Date:         2014/10/29 10:59:16
    /// Machine Name: KORD
    ///***************************************************************************//
    /// Function: 
    /// 	维修服务-宇通三包-三包服务单查询
    ///***************************************************************************//
    public partial class UCMaintainThreeGuarantyQuery : UCBase
    {
        #region Constructor -- 构造函数
        public UCMaintainThreeGuarantyQuery()
        {
            try
            {
                InitializeComponent();

                UIAssistants.SetButtonStyle4QueryAndClear(btn_query, btn_clear, true);  //设置查询按钮和清除按钮样式
                DataGridViewEx.SetDataGridViewStyle(dgv_table, drtxt_remarks);   //设置数据表格样式,并将最后一列填充其余空白

                Init();

                Load += (sender, args) => UIAssistants.SetDataGridViewCheckColumn(dgv_table, drchk_check);

            }
            catch (Exception ex)
            {
                _loggingService.WriteLog(ex);
            }
        }
        #endregion

        #region Field -- 字段
        private String _where = String.Empty;
        public const String TableName = "tb_maintain_three_guaranty";
        #endregion

        #region Property -- 属性

        private LoggingService _loggingService = Log.CreateLogService("三包服务单查询", @"维修业务\宇通三包\三包服务单查询");
        #endregion

        #region Method -- 方法
        private void Init() //初始化
        {
            SetFuncationButtonVisible();
            InitQueryControlDataSource();
            FuncationRegiste();
        }
        private void SetFuncationButtonVisible() //设置功能按钮可见性
        {
            try
            {
                var btnCols = new ObservableCollection<ButtonEx_sms>
            {
                btnExport, btnView, btnPrint, btnSet
            };
                btnCopy.Enabled = btnEdit.Enabled = btnDelete.Enabled = btnActivation.Enabled = btnVerify.Enabled = btnSave.Enabled = btnCommit.Enabled = btnRevoke.Enabled = false;
                UIAssistants.SetUCBaseFuncationVisible(this, btnCols);
            }
            catch (Exception ex)
            {
                _loggingService.WriteLog(ex);
            }
        }
        private void InitQueryControlDataSource()   //初始化查询控件数据项
        {
            txt_vehicle_no.ChooserClick += delegate //车牌号
            {
                try
                {
                    var vechicleChooser = new frmVehicleGrade();
                    var result = vechicleChooser.ShowDialog();
                    if (result != DialogResult.OK) return;
                    txt_vehicle_no.Text = vechicleChooser.strLicensePlate;
                }
                catch (Exception ex)
                {
                    _loggingService.WriteLog(ex);
                }
            };
            txt_customer_code.ChooserClick += delegate  //客户编码(同时会更新客户名称)
            {
                try
                {
                    var custChooser = new frmCustomerInfo();
                    var result = custChooser.ShowDialog();
                    if (result != DialogResult.OK) return;
                    txt_customer_code.Text = custChooser.strCustomerNo;
                    txt_customer_name.Caption = custChooser.strCustomerName;
                }
                catch (Exception ex)
                {
                    _loggingService.WriteLog(ex);
                }
            };
            CommonCtrl.CmbBindDict(cbo_bill_type_yt, "bill_type_yt", true);    //单据类型
            CommonCtrl.CmbBindDict(cbo_approve_status_yt, "service_single_status_yt", true);  //宇通单据状态
        }
        private void FuncationRegiste()  //注册控件相关事件
        {
            #region 窗体加载事件
            Load += delegate
            {
                dgv_table.ReadOnly = false;
                foreach (DataGridViewColumn dgvc in dgv_table.Columns)
                {
                    if (dgvc == drchk_check)
                    {
                        continue;
                    }
                    dgvc.ReadOnly = true;
                }
            };
            #endregion

            #region 查询按钮事件
            btn_query.Click += delegate
            {
                try
                {
                    _where = String.Format("enable_flag = '1' and info_status = '{0}'", DbDic2Enum.SYS_SERVICE_INFO_STATUS_SHTG);
                    if (dtp_repairs_time_s.Value.AddYears(1) < dtp_repairs_time_e.Value)
                    {
                        MessageBoxEx.Show("报修日期选择范围最大为1年!", "操作提示");
                        return;
                    }
                    if (dtp_approval_date_s.Value.AddYears(1) < dtp_approval_date_e.Value)
                    {
                        MessageBoxEx.Show("审批通过时间选择范围最大为1年!", "操作提示");
                        return;
                    }
                    var startTicks4Repairs = dtp_repairs_time_s.Value.Date.ToUniversalTime().Ticks;
                    var endTicks4Repairs = dtp_repairs_time_e.Value.Date.ToUniversalTime().Ticks;
                    _where += " and repairs_time >= " + startTicks4Repairs;
                    _where += " and repairs_time <= " + endTicks4Repairs;

                    //var startTicks4Approve = dtp_approval_date_s.Value.Date.ToUniversalTime().Ticks;
                    //var endTicks4Approve = dtp_approval_date_e.Value.Date.ToUniversalTime().Ticks;
                    //_where += " and approval_date >= " + startTicks4Approve;
                    //_where += " and approval_date <= " + endTicks4Approve;

                    if (txt_service_no.Caption != null && !string.IsNullOrEmpty(txt_service_no.Caption))
                    {
                        _where += string.Format(" and service_no like '%{0}%'", txt_service_no.Caption);
                    }
                    if (txt_service_no_yt.Caption != null && !string.IsNullOrEmpty(txt_service_no_yt.Caption))
                    {
                        _where += string.Format(" and service_no_yt like '%{0}%'", txt_service_no_yt.Caption);
                    }
                    if (txt_vehicle_no.Text != null && !string.IsNullOrEmpty(txt_vehicle_no.Text))
                    {
                        _where += string.Format(" and vehicle_no like '%{0}%'", txt_vehicle_no.Text);
                    }
                    if (txt_depot_no.Caption != null && !string.IsNullOrEmpty(txt_depot_no.Caption))
                    {
                        _where += string.Format(" and depot_no like '%{0}%'", txt_depot_no.Caption);
                    }
                    if (txt_customer_code.Text != null && !string.IsNullOrEmpty(txt_customer_code.Text))
                    {
                        _where += string.Format(" and customer_code like '%{0}%'", txt_customer_code.Text);
                    }
                    if (txt_customer_name.Caption != null && !string.IsNullOrEmpty(txt_customer_name.Caption))
                    {
                        _where += string.Format(" and customer_name like '%{0}%'", txt_customer_name.Caption);
                    }
                    if (cbo_bill_type_yt.SelectedValue != null && !string.IsNullOrEmpty(cbo_bill_type_yt.SelectedValue.ToString()))
                    {
                        _where += string.Format(" and receipt_type like '%{0}%'", CommonCtrl.IsNullToString(cbo_bill_type_yt.SelectedValue));
                    }
                    if (cbo_approve_status_yt.SelectedValue != null && !string.IsNullOrEmpty(cbo_approve_status_yt.SelectedValue.ToString()))
                    {
                        _where += string.Format(" and approve_status_yt like '%{0}%'", CommonCtrl.IsNullToString(cbo_approve_status_yt.SelectedValue));
                    }
                    _loggingService.WriteLog(1, _where);
                    BindPageData(_where);
                }
                catch (Exception ex)
                {
                    _loggingService.WriteLog(ex);
                    MessageBoxEx.Show(ex.Message, "错误的操作！");
                }
            };
            #endregion

            #region 清除按钮事件
            btn_clear.Click += delegate
            {
                try
                {
                    txt_service_no.Caption = txt_service_no_yt.Caption = String.Empty;
                    txt_vehicle_no.Text = String.Empty;
                    txt_depot_no.Caption = String.Empty;
                    txt_customer_code.Text = String.Empty;
                    txt_customer_name.Caption = String.Empty;
                    cbo_bill_type_yt.SelectedIndex = 0;
                    cbo_approve_status_yt.SelectedIndex = 0;
                    dtp_repairs_time_s.Value = DateTime.Now.AddMonths(-1);
                    dtp_repairs_time_e.Value = DateTime.Now.AddDays(1);
                    dtp_approval_date_s.Value = DateTime.Now.AddMonths(-1);
                    dtp_approval_date_e.Value = DateTime.Now.AddDays(1);
                }
                catch (Exception ex)
                {
                    _loggingService.WriteLog(ex);
                }
            };
            #endregion

            #region 数据翻页
            pageQ.PageIndexChanged += delegate
            {
                BindPageData(String.IsNullOrEmpty(_where) ? _where = String.Format("enable_flag = '1' and info_status = '{0}'", DbDic2Enum.SYS_SERVICE_INFO_STATUS_SHTG) : _where);
            };
            #endregion


            #region 数据表格单元格数据转换
            dgv_table.CellFormatting += (sender, args) => ConvertDataGridColumnsData(args);
            #endregion

        }
        private void ConvertDataGridColumnsData(DataGridViewCellFormattingEventArgs args) //转换数据列数据
        {
            try
            {
                UIAssistants.DgvCellDataConvert2DicData(dgv_table, args, "info_status");   //单据状态
                UIAssistants.DgvCellDataConvert2DicData(dgv_table, args, "service_single_status_yt"); //宇通同步状态
                UIAssistants.DgvCellDataConvert2Datetime(dgv_table, args, "approve_time"); //审批通过时间
                UIAssistants.DgvCellDataConvert2Datetime(dgv_table, args, "repairs_time"); //报修时间
                UIAssistants.DgvCellDataConvert2DicData(dgv_table, args, "receipt_type");   //单据类型
                UIAssistants.DgvCellDataConvert2DicData(dgv_table, args, "approve_state");   //单据类型
            }
            catch (Exception ex)
            {
                _loggingService.WriteLog(ex);
            }
        }
        /// <summary> 
        /// 绑定数据
        /// </summary>
        public void BindPageData(string where)
        {
            try
            {
                int recordCount;
                var dt = DBHelper.GetTableByPage("分页查询三包服务单信息", "v_maintain_three_guaranty_approve", "*", where, "", "repairs_time desc", pageQ.PageIndex, pageQ.PageSize, out recordCount);
                dgv_table.DataSource = dt;
                pageQ.RecordCount = recordCount;
            }
            catch (Exception ex)
            {
                _loggingService.WriteLog(ex);
                MessageBoxEx.Show(ex.Message, "错误的操作！");
            }
        }
        #endregion

        #region Event -- 事件

        #endregion
    }
}
