using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using Utility.Common;

namespace HXCPcClient.UCForm.RepairBusiness.YuTongThreeGuaranty.MaintainThreeGuarantySettlement
{
    ///*************************************************************************//
    /// System:       HuiXuCheSYS
    /// FileName:     ThreeGuarantySettlement         
    /// Author:       Kord
    /// Date:         2014/10/30 16:35:03
    /// Machine Name: KORD
    ///***************************************************************************//
    /// Function: 
    /// 	ThreeGuarantySettlement
    ///***************************************************************************//
    public partial class UCThreeGuarantySettlementQuery : UCBase
    {
        #region Constructor -- 构造函数
        public UCThreeGuarantySettlementQuery()
        {
            InitializeComponent();

            UIAssistants.SetButtonStyle4QueryAndClear(btnQuery, btnClear, true);  //设置查询按钮和清除按钮样式
            DataGridViewEx.SetDataGridViewStyle(dgvTable, drtxt_remark);   //设置数据表格样式,并将最后一列填充其余空白

            Init();
        }
        #endregion

        #region Field -- 字段
        private String _where = String.Empty;   //查询条件语句
        #endregion

        #region Property -- 属性

        #endregion

        #region Method -- 方法
        private void Init() //初始化方法
        {
            SetFuncationButtonVisible();
            InitQueryControlDataSource();
            dgvTable.CellFormatting += (sender, args) => ConvertDataGridColumnsData(args);
            FuncationRegiste();
        }
        private void SetFuncationButtonVisible()    //设置功能按钮可见性
        {
            var btnCols = new ObservableCollection<ButtonEx_sms>()
            {
                btnExport,btnView,btnPrint,btnSet
            };
            UIAssistants.SetUCBaseFuncationVisible(this,btnCols);
        }
        private void InitQueryControlDataSource()   //初始化查询控件数据项
        {
            CommonCtrl.BindComboBoxByDictionarr(cbo_info_status_yt, "settlement_repair_status_yt", true);         //宇通结算状态(宇通同步数据)
            CommonCtrl.BindComboBoxByDictionarr(cbo_info_status, "sys_station_settlement_status", true);    //绑定服务站计算单状态
            dtp_settlement_cycle_s.Value = DBHelper.GetCurrentTime().AddMonths(-1);
            dtp_settlement_cycle_e.Value = DBHelper.GetCurrentTime().AddDays(1);
        }
        private void ConvertDataGridColumnsData(DataGridViewCellFormattingEventArgs args) //转换数据列数据
        {
            UIAssistants.DgvCellDataConvert2DicData(dgvTable, args, "info_status_yt");    //宇通结算状态转换
            UIAssistants.DgvCellDataConvert2DicData(dgvTable, args, "info_status");    //服务站结算状态转换
            UIAssistants.DgvCellDataConvert2Datetime(dgvTable, args, "create_time");   //创建时间、
            UIAssistants.DgvCellDataConvert2Datetime(dgvTable, args, "settlement_cycle_start");   //创建时间
            UIAssistants.DgvCellDataConvert2Datetime(dgvTable, args, "settlement_cycle_end");   //创建时间
            UIAssistants.DgvCellDataConvert2Datetime(dgvTable, args, "clearing_time"); //清账时间
        }
        private void FuncationRegiste() //功能按钮事件注册
        {
            #region 清除
            btnClear.Click += delegate  //清除查询条件
            {
                try
                {
                    txt_recycle_no.Caption = String.Empty;
                    txt_settlement_no.Caption = String.Empty;
                    cbo_info_status_yt.SelectedIndex = 0;
                    cbo_info_status.SelectedIndex = 0;
                    txt_station_settlement_no.Caption = String.Empty;
                    txt_service_no.Caption = String.Empty;
                    dtp_settlement_cycle_s.Value = DBHelper.GetCurrentTime().AddMonths(-1);
                    dtp_settlement_cycle_e.Value = DBHelper.GetCurrentTime().AddDays(1);
                }
                catch (Exception ex)
                {
                    MessageBoxEx.Show(ex.Message, "错误的操作！");
                }
            };
            #endregion

            #region 查询
            btnQuery.Click += delegate
            {
                try
                {
                    _where = "enable_flag = '1'";

                    if (!string.IsNullOrEmpty(txt_settlement_no.Caption.Trim()))  //宇通结算单号
                    {
                        _where += string.Format(" settlement_no like '%{0}%'", txt_settlement_no.Caption.Trim());
                    }
                    if (cbo_info_status_yt.SelectedValue != null && !string.IsNullOrEmpty(cbo_info_status_yt.SelectedValue.ToString()))  //宇通结算单状态
                    {
                        _where += string.Format(" and info_status_yt like '%{0}%'", cbo_info_status_yt.SelectedValue);
                    }
                    if (!string.IsNullOrEmpty(txt_service_no.Caption))    //宇通旧件回收单号
                    {
                        //从旧件回收结算单中获取结算单号
                        var strWhere = string.Format(" recycle_no='{0}'", txt_service_no.Caption.Trim());
                        var dt = DBHelper.GetTable("查询结算单号", "tb_maintain_three_guaranty_settlement_old", "*", strWhere, "", "");
                        if (dt != null && dt.DefaultView.Count > 0)
                        {
                            var stNo = dt.DefaultView[0]["st_id"].ToString();
                            _where += string.Format(" and st_id like '%{0}%'", stNo);
                        }
                    }
                    if (!string.IsNullOrEmpty(txt_recycle_no.Caption))  //宇通服务单号
                    {
                        //从三包服务单中获取结算单号
                        var strWhere = string.Format(" service_no_yt='{0}'", txt_recycle_no.Caption.Trim());
                        var dt = DBHelper.GetTable("查询结算单号", "tb_maintain_three_guaranty_settlement", "*", strWhere, "", "");
                        if (dt != null && dt.DefaultView.Count > 0)
                        {
                            var stNo = dt.DefaultView[0]["st_id"].ToString();
                            _where += string.Format(" and st_id like '%{0}%'", stNo);
                        }
                    }
                    if (!string.IsNullOrEmpty(txt_station_settlement_no.Caption.Trim()))  //服务站结算单号
                    {
                        _where += string.Format(" station_settlement_no like '%{0}%'", txt_station_settlement_no.Caption.Trim());
                    }
                    if (cbo_info_status.SelectedValue != null && !string.IsNullOrEmpty(cbo_info_status.SelectedValue.ToString()))  //服务站结算单状态
                    {
                        _where += string.Format(" and info_status like '%{0}%'", cbo_info_status.SelectedValue);
                    }

                    //结算周期
                    var startTicks = dtp_settlement_cycle_s.Value.Date.ToUniversalTime().Ticks;
                    var endTicks = dtp_settlement_cycle_s.Value.Date.ToUniversalTime().Ticks;
                    _where += " and settlement_cycle_start >=" + startTicks;
                    _where += " and settlement_cycle_end <=" + endTicks;

                    BindPageData(_where);
                }
                catch (Exception ex)
                {
                    MessageBoxEx.Show(ex.Message, "错误的操作！");
                }
            };
            #endregion

            #region 数据翻页
            pageQ.PageIndexChanged += delegate
            {
                BindPageData(String.IsNullOrEmpty(_where) ? "enable_flag = '1'" : _where);
            };
            #endregion
        }
        /// <summary> 
        /// 绑定数据
        /// </summary>
        public void BindPageData(string where)
        {
            try
            {
                int recordCount;
                var dt = DBHelper.GetTableByPage("分页查询结算单信息", "tb_maintain_three_guaranty_settlement", "*", where, "", "update_time desc", pageQ.PageIndex, pageQ.PageSize, out recordCount);
                dgvTable.DataSource = dt;
                pageQ.RecordCount = recordCount;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "错误的操作！");
            }
        }
        #endregion

        #region Event -- 事件

        #endregion
    }
}
