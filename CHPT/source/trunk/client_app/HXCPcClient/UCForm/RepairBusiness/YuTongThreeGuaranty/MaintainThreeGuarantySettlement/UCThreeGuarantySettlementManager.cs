using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using SYSModel;
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
    /// 	维修业务--宇通三包--三包结算单管理
    ///***************************************************************************//
    public partial class UCThreeGuarantySettlementManager : UCBase
    {
        #region Constructor -- 构造函数
        public UCThreeGuarantySettlementManager()
        {
            InitializeComponent();

            SetContentMenuScrip(dgvTable);

            UIAssistants.SetButtonStyle4QueryAndClear(this, btnQuery, btnClear);  //设置查询按钮和清除按钮样式
            DataGridViewEx.SetDataGridViewStyle(dgvTable, drtxt_settlement_cycle_end);   //设置数据表格样式,并将最后一列填充其余空白

            Init();

            Load += (sender, args) => UIAssistants.SetDataGridViewCheckColumn(dgvTable, drchk_check);
            var dt = DateTime.Now;

        }
        #endregion

        #region Field -- 字段
        private const String TableName = "tb_maintain_three_guaranty_settlement";
        
        #endregion

        #region Property -- 属性

        #endregion

        #region Method -- 方法
        private void Init() //初始化方法
        {
            #region 数据表数据选择
            dgvTable.CellDoubleClick += delegate(object sender, DataGridViewCellEventArgs args)
            {
                if (args.RowIndex < 0) return;
                var stId = dgvTable.Rows[args.RowIndex].Cells["drtxt_st_id"].Value.ToString();
                if (String.IsNullOrEmpty(stId))
                {
                    MessageBoxEx.Show("无效的结算单信息", "操作提示");
                    return;
                }
                var uc = new UCThreeGuarantySettlementView
                {
                    SettlementId = stId
                };
                uc.addUserControl(uc, "三包结算单-浏览", "UCThreeGuarantySettlementView", Tag.ToString(), Name);
            };
            #endregion

            SetFuncationButtonVisible();
            InitQueryControlDataSource();
            dgvTable.CellFormatting += (sender, args) => ConvertDataGridColumnsData(args);
            FuncationRegiste();
        }
        private void SetFuncationButtonVisible()    //设置功能按钮可见性
        {
            var btnCols = new ObservableCollection<ButtonEx_sms>()
            {
                btnSync,btnConfirm,btnExport,btnView,btnPrint,btnPrint,btnSet
            };
            UIAssistants.SetUCBaseFuncationVisible(this,btnCols);
        }
        private void InitQueryControlDataSource()   //初始化查询控件数据项
        {
            dtp_settlement_cycle_start.Value = DBHelper.GetCurrentTime().AddMonths(-1);
            dtp_settlement_cycle_end.Value = DBHelper.GetCurrentTime().AddDays(1);
            CommonCtrl.BindComboBoxByDictionarr(cbo_yt_info_status, "settlement_repair_status_yt", true);         //宇通结算状态(宇通同步数据)
            CommonCtrl.BindComboBoxByDictionarr(cbo_station_info_status, "sys_station_settlement_status", true);    //绑定服务站计算单状态
        }
        private void ConvertDataGridColumnsData(DataGridViewCellFormattingEventArgs args) //转换数据列数据
        {
            UIAssistants.DgvCellDataConvert2DicData(dgvTable, args, "info_status_yt");      //宇通结算单状态转换(数据来自宇通)
            UIAssistants.DgvCellDataConvert2DicData(dgvTable, args, "info_status");      //服务站结算状态转换
            UIAssistants.DgvCellDataConvert2Datetime(dgvTable, args, "create_time");        //创建时间
            UIAssistants.DgvCellDataConvert2Datetime(dgvTable, args, "settlement_cycle_start");        //结算周期开始时间
            UIAssistants.DgvCellDataConvert2Datetime(dgvTable, args, "settlement_cycle_end");        //结算周期开始时间
        }
        private void FuncationRegiste() //功能按钮事件注册
        {
            #region 同步
            btnSync.Click += delegate
            {
                if (!GlobalStaticObj.IsDefaultAcc)
                {
                    MessageBoxEx.ShowWarning("不是主账套信息,不允许进行操作!");
                    return;
                }
                var selectCount = 0;
                foreach (DataGridViewRow dr in dgvTable.Rows)
                {
                    selectCount++;
                    var isCheck = dr.Cells["drchk_check"].EditedFormattedValue;
                    if (isCheck != null && (bool)isCheck)
                    {
                        var stId = dr.Cells["drtxt_settlement_no"].Value.ToString();
                        var flag = DBHelper.WebServHandler("三包结算单同步", EnumWebServFunName.LoadServiceSettleStatus, stId);
                        if (String.IsNullOrEmpty(flag))
                        {
                            MessageBoxEx.Show("结算单同步成功!", "操作提示");
                            BindPageData("enable_flag = '1'");
                        }
                        else
                        {
                            MessageBoxEx.Show("结算单同步失败!", "操作提示");
                        }
                    }
                }
                if (selectCount == 0)
                {
                    MessageBoxEx.Show("请选择需要同步的结算单信息!", "操作提示");
                }
            };
            #endregion

            #region 确认
            btnConfirm.Click += delegate
            {
                var selectCount = 0;
                foreach (DataGridViewRow dr in dgvTable.Rows)
                {
                    selectCount ++;
                    var isCheck = dr.Cells["drchk_check"].EditedFormattedValue;
                    if (isCheck != null && (bool)isCheck)
                    {
                        var stId = dr.Cells["drtxt_settlement_no"].Value.ToString();
                        var flag = DBHelper.WebServHandler("三包结算单确认", EnumWebServFunName.UpLoadServiceSettleStatus, stId);
                        if (String.IsNullOrEmpty(flag))
                        {
                            MessageBoxEx.Show("三包结算单确认成功!", "操作提示");
                            BindPageData();
                        }
                        else
                        {
                            MessageBoxEx.Show("三包结算单确认失败!", "操作提示");
                        }
                    }
                }
                if (selectCount == 0)
                {
                    MessageBoxEx.Show("请选择需要确认的结算单信息!", "操作提示");
                }
            };
            #endregion

            #region 清除
            btnClear.Click += delegate  //清除查询条件
            {
                try
                {
                    cbo_yt_info_status.SelectedIndex = 0;
                    cbo_station_info_status.SelectedIndex = 0;
                    dtp_settlement_cycle_start.Value = DBHelper.GetCurrentTime().AddMonths(-1);
                    dtp_settlement_cycle_end.Value = DBHelper.GetCurrentTime().AddDays(1);
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
                BindPageData(); 
            };
            #endregion

            #region 数据翻页
            pageQ.PageIndexChanged += delegate
            {
                BindPageData(String.IsNullOrEmpty("") ? "enable_flag = '1'" : "");
            };
            #endregion

            #region 根据选择的数据判断功能按钮的显示状态
            dgvTable.CellMouseUp += delegate
            {
                var dataView = GetSelectedRowData();
                var listField = GetCheckRows();
                if (dataView == null || listField.Count == 0)
                {
                    btnSync.Enabled = false;
                    btnConfirm.Enabled = false;
                }
                else
                {
                    btnSync.Enabled = true;

                    btnConfirm.Enabled = dataView[0]["info_status"].ToString() == DbDic2Enum.SYS_STATION_SETTLEMENT_STATUS_UNCONFIRM;
                }
            };
            #endregion
        }
        private List<string> GetCheckRows()
        {
            return (from DataGridViewRow dr in dgvTable.Rows let isCheck = dr.Cells["drchk_check"].EditedFormattedValue where isCheck != null && (bool)isCheck select dr.Cells["drtxt_st_id"].Value.ToString()).ToList();
        }
        private DataView GetSelectedRowData()
        {
            var selectedRows = (from DataGridViewRow dr in dgvTable.Rows let isCheck = dr.Cells["drchk_check"].EditedFormattedValue where isCheck != null && (bool)isCheck select dr.Cells["drtxt_st_id"].Value.ToString()).ToList();
            if (selectedRows.Count > 0)
            {
                var whereSqlSb = new StringBuilder();
                whereSqlSb.Append(" 1=1");
                foreach (var selectedRow in selectedRows)
                {
                    whereSqlSb.Append(" or st_id ='");
                    whereSqlSb.Append(selectedRow);
                    whereSqlSb.Append("'");
                }
                var whereSql = whereSqlSb.ToString().Replace(" 1=1 or ", "");
                var data = DBHelper.GetTable("根据ID获取三包结算单信息", "tb_maintain_three_guaranty_settlement", "*", whereSql, "", "");
                if (data != null && data.DefaultView != null)
                {
                    return data.DefaultView;
                }
            }
            return null;
        }
        /// <summary> 
        /// 绑定数据
        /// </summary>
        public void BindPageData(string where = "")
        {
            try
            {
                if(String.IsNullOrEmpty(where))
                {
                    where = "enable_flag = '1'";

                    if (cbo_yt_info_status.SelectedValue != null && !string.IsNullOrEmpty(cbo_yt_info_status.SelectedValue.ToString()))  //宇通结算单状态
                    {
                        where += string.Format(" and info_status_yt like '%{0}%'", cbo_yt_info_status.SelectedValue);
                    }
                    if (cbo_station_info_status.SelectedValue != null && !string.IsNullOrEmpty(cbo_station_info_status.SelectedValue.ToString()))  //服务站结算单状态
                    {
                        where += string.Format(" and info_status like '%{0}%'", cbo_station_info_status.SelectedValue);
                    }

                    var startTicks = Common.LocalDateTimeToUtcLong(dtp_settlement_cycle_start.Value);
                    var endTicks = Common.LocalDateTimeToUtcLong(dtp_settlement_cycle_end.Value);
                    where += String.Format(" and settlement_cycle_start >= {0}", startTicks);    //2014-09-01
                    where += String.Format(" and settlement_cycle_end <= {0}", endTicks);    //2014-09-01
                }
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
