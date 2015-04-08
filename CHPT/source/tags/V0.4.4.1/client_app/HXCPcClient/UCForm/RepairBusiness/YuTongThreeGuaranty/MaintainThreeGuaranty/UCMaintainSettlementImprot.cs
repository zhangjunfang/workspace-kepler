using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using SYSModel;

namespace HXCPcClient.UCForm.RepairBusiness.YuTongThreeGuaranty.MaintainThreeGuaranty
{
    /// <summary>
    /// UCMaintainSettlementImprot
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2015/1/15 14:35:55</datetime>
    ///         <comment>create</comment>
    ///     </version>
    /// </versioning>
    public partial class UCMaintainSettlementImprot : FormEx
    {
        #region Constructor -- 构造函数
        public UCMaintainSettlementImprot()
        {
            InitializeComponent();

            InitEvent();

            InitProperty();

            pnl_search_QueryClick(null, null);
        }
        #endregion

        #region Field -- 字段
        private const String _maintainIdName = "maintain_id";
        private String _selectedMaintainId = string.Empty;
        #endregion

        #region Property -- 属性

        #endregion

        #region Method -- 方法
        private void InitProperty()
        {
            StartPosition = FormStartPosition.CenterScreen;
        }
        private void InitEvent()
        {
            pnl_search.QueryClick += pnl_search_QueryClick;
            dgv_primary.SelectionChanged += dgv_primary_SelectionChanged;
        }
        private List<string> GetCheckRows()
        {
            return (from DataGridViewRow dr in dgv_primary.Rows let isCheck = dr.Cells[drchk_check.Name].EditedFormattedValue where isCheck != null && (bool)isCheck select dr.Cells[drtxt_maintain_id.Name].Value.ToString()).ToList();
        }
        #endregion

        #region Event -- 事件
        private void dgv_primary_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_primary.SelectedRows.Count < 1)
            {
                _selectedMaintainId = String.Empty;
                return;
            }
            _selectedMaintainId = CommonCtrl.IsNullToString(dgv_primary.SelectedRows[0].Cells[drtxt_maintain_id.Name].Value);
        }
        private void pnl_search_QueryClick(object sender, EventArgs e)
        {
            var strWhere = pnl_search.QueryString;
            strWhere += string.Format(" and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING)) + "' and (info_status='" + Convert.ToInt32(DataSources.EnumAuditStatus.Balance) + "' or info_status='" + +Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT) + "')";
            dgv_primary.DataSource = OrderRelation.GetIsUsePreOrder("tb_maintain_info", "maintain_id", "tb_maintain_three_guaranty", strWhere);
        }
        #endregion
    }
}


