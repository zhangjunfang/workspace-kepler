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
    public partial class UCMaintainWorkhourImport : FormEx
    {
        #region Constructor -- 构造函数
        public UCMaintainWorkhourImport()
        {
            InitializeComponent();

            InitEvent();

            InitProperty();
        }
        #endregion

        #region Field -- 字段
        private String _maintainId = string.Empty;
        #endregion

        #region Property -- 属性
        /// <summary>
        /// id
        /// </summary>
        public string strWhours_id = string.Empty;
        /// <summary>
        /// 项目编号
        /// </summary>
        public string strProjectNum = string.Empty;
        /// <summary>
        /// 项目名称
        /// </summary>
        public string strProjectName = string.Empty;
        /// <summary>
        /// 维修项目类别
        /// </summary>
        public string strRepairType = string.Empty;
        /// <summary>
        /// 工时数量
        /// </summary>
        public string strWhoursNum = string.Empty;
        /// <summary>
        /// 工时调整
        /// </summary>
        public string strWhoursChange = string.Empty;
        /// <summary>
        /// 工时单价
        /// </summary>
        public string strQuotaPrice = string.Empty;
        /// <summary>
        /// 工时类型
        /// </summary>
        public string strWhoursType = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string strRemark = string.Empty;
        /// <summary>
        /// 数据来源
        /// </summary>
        public string strData_source = string.Empty;
        /// <summary>
        /// 维修单ID
        /// </summary>
        public String MaintainId
        {
            get { return _maintainId; }
            set
            {
                _maintainId = value;
                BindData();
            }
        }
        /// <summary>
        /// 维修项目ID
        /// </summary>
        public String ItemId { get; set; }
        #endregion


        #region Method -- 方法
        private void InitProperty()
        {
            StartPosition = FormStartPosition.CenterScreen;
        }
        private void InitEvent()
        {
            dgv_primary.CellDoubleClick += delegate(object sender, DataGridViewCellEventArgs e)
            {
                if (e.RowIndex < 0) return;
                ItemId = CommonCtrl.IsNullToString(dgv_primary.Rows[e.RowIndex].Cells[item_id.Name].Value);
                strWhours_id = CommonCtrl.IsNullToString(dgv_primary.Rows[e.RowIndex].Cells[whours_id.Name].Value);
                strRepairType = CommonCtrl.IsNullToString(dgv_primary.Rows[e.RowIndex].Cells[item_type.Name].Value);
                strProjectNum = CommonCtrl.IsNullToString(dgv_primary.Rows[e.RowIndex].Cells[item_no.Name].Value);
                strProjectName = CommonCtrl.IsNullToString(dgv_primary.Rows[e.RowIndex].Cells[item_name.Name].Value);
                strRemark = CommonCtrl.IsNullToString(dgv_primary.Rows[e.RowIndex].Cells[remarks.Name].Value);
                strQuotaPrice = CommonCtrl.IsNullToString(dgv_primary.Rows[e.RowIndex].Cells[man_hour_norm_unitprice.Name].Value);
                strWhoursNum = CommonCtrl.IsNullToString(dgv_primary.Rows[e.RowIndex].Cells[man_hour_quantity.Name].Value);
                strWhoursType = CommonCtrl.IsNullToString(dgv_primary.Rows[e.RowIndex].Cells[man_hour_type.Name].Value);
                strData_source = CommonCtrl.IsNullToString(dgv_primary.Rows[e.RowIndex].Cells[data_source.Name].Value);
                DialogResult = DialogResult.OK;
                Close();
            };
            #region 数据表格单元格数据转换
            dgv_primary.CellFormatting += (sender, args) => ConvertDataGridColumnsData(args);
            #endregion
        }
        private void ConvertDataGridColumnsData(DataGridViewCellFormattingEventArgs args) //转换数据列数据
        {
            
        }

        private void BindData()
        {
            #region 维修项目数据
            var sqlString = string.Format("select mi.* from tb_maintain_item mi left join tr_order_relation re on mi.item_id = re.pre_item_order_id where (re.post_order_id is null or re.pre_item_order_name != 'tb_maintain_item') and mi.maintain_id = '{0}' and three_warranty = '1' and mi.enable_flag = '1'", _maintainId);
            //维修项目数据   
            var sqlObj = new SQLObj(); 
            sqlObj.Param = new Dictionary<string, ParamObj>();
            sqlObj.sqlString = sqlString;
            var ds = DBHelper.GetDataSet("获取维修项目信息", sqlObj);
            if (ds != null && ds.Tables.Count > 0)
            {
                var dpt = ds.Tables[0];
                if (dpt.Rows.Count > 0)
                {
                    if (dpt.Rows.Count >= dgv_primary.Rows.Count)
                    {
                        dgv_primary.Rows.Add(dpt.Rows.Count);
                    }
                    for (int i = 0; i < dpt.Rows.Count; i++)
                    {
                        DataRow dpr = dpt.Rows[i];
                        dgv_primary.Rows[i].Cells["item_id"].Value = CommonCtrl.IsNullToString(dpr["item_id"]);
                        dgv_primary.Rows[i].Cells["three_warranty"].Value = CommonCtrl.IsNullToString(dpr["three_warranty"]) == "1" ? "是" : "否";
                        dgv_primary.Rows[i].Cells["man_hour_type"].Value = CommonCtrl.IsNullToString(dpr["man_hour_type"]);
                        dgv_primary.Rows[i].Cells["item_no"].Value = CommonCtrl.IsNullToString(dpr["item_no"]);
                        dgv_primary.Rows[i].Cells["item_name"].Value = CommonCtrl.IsNullToString(dpr["item_name"]);
                        dgv_primary.Rows[i].Cells["item_type"].Value = CommonCtrl.IsNullToString(dpr["item_type"]);
                        dgv_primary.Rows[i].Cells["man_hour_quantity"].Value = CommonCtrl.IsNullToString(dpr["man_hour_quantity"]);
                        dgv_primary.Rows[i].Cells["man_hour_norm_unitprice"].ReadOnly = CommonCtrl.IsNullToString(dpr["man_hour_type"]) == "工时" ? true : false;
                        dgv_primary.Rows[i].Cells["man_hour_norm_unitprice"].Value = CommonCtrl.IsNullToString(dpr["man_hour_norm_unitprice"]);
                        dgv_primary.Rows[i].Cells["remarks"].Value = CommonCtrl.IsNullToString(dpr["remarks"]);
                        dgv_primary.Rows[i].Cells["sum_money"].Value = CommonCtrl.IsNullToString(dpr["sum_money"]);
                        dgv_primary.Rows[i].Cells["whours_id"].Value = CommonCtrl.IsNullToString(dpr["whours_id"]);
                        dgv_primary.Rows[i].Cells["data_source"].Value = CommonCtrl.IsNullToString(dpr["data_source"]);
                        dgv_primary.Rows[i].Cells["three_warranty"].ReadOnly = CommonCtrl.IsNullToString(dpr["data_source"]) == Convert.ToInt32(DataSources.EnumDataSources.SELFBUILD).ToString();
                    }
                }
            }
            #endregion
        }
        #endregion

        #region Event -- 事件
        #endregion
    }
}


