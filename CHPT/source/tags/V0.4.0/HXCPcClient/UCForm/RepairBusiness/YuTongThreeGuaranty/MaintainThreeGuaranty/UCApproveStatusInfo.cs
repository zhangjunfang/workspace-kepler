using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;

namespace HXCPcClient.UCForm.RepairBusiness.YuTongThreeGuaranty.MaintainThreeGuaranty
{
    ///*************************************************************************//
    /// System:       HuiXuCheSYS
    /// FileName:     UCApproveStatusInfo         
    /// Author:       Kord
    /// Date:         2014/11/5 19:04:08
    /// Machine Name: KORD
    ///***************************************************************************//
    /// Function: 
    /// 	UCApproveStatusInfo
    ///***************************************************************************//
    public partial class UCApproveStatusInfo : FormEx
    {
        #region Constructor -- 构造函数
        /// <summary>
        /// 服务单审核详情
        /// </summary>
        /// <param name="tgId">服务单id</param>
        public UCApproveStatusInfo(String tgId)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
            _tgId = tgId;
            
            DataGridViewEx.SetDataGridViewStyle(dgvTable, drtxt_remark);
            Name = String.Format("{0}号服务单审核详情", _tgId);

            Load += (sender, args) => BindPageData();

            dgvTable.CellFormatting += (sender, args) => ConvertDataGridColumnsData(args);
        }
        #endregion

        #region Field -- 字段
        private readonly String _tgId = String.Empty;
        #endregion

        #region Property -- 属性

        #endregion

        #region Method -- 方法
        /// <summary> 
        /// 绑定数据
        /// </summary>
        public void BindPageData()
        {
            try
            {
                var dt = DBHelper.GetTable("查询服务单审核详情", "tb_maintain_three_material_approve", "*", "tg_id = '" + _tgId + "'", "", "order by approve_time desc");
                dgvTable.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "错误的操作！");
            }
        }
        private void ConvertDataGridColumnsData(DataGridViewCellFormattingEventArgs args) //转换数据列数据
        {
            UIAssistants.DgvCellDataConvert2DicData(dgvTable, args, "approve_state");   //单据状态
            UIAssistants.DgvCellDataConvert2Datetime(dgvTable, args, "approve_time"); //审批通过时间
        }
        #endregion


        #region Event -- 事件

        #endregion
    }
}


