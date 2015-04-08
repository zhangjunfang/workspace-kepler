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
    public partial class UCMaintainMatrialDetailImport : FormEx
    {
        #region Constructor -- 构造函数
        public UCMaintainMatrialDetailImport()
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
        /// 配件id
        /// </summary>
        public string PartsID = string.Empty;
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
            dgvMaterials.CellDoubleClick += delegate(object sender, DataGridViewCellEventArgs e)
            {
                if (e.RowIndex < 0) return;
                ItemId = CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells[material_id.Name].Value);
                PartsID = CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells[parts_id.Name].Value);
                DialogResult = DialogResult.OK;
                Close();
            };
            #region 数据表格单元格数据转换
            dgvMaterials.CellFormatting += (sender, args) => ConvertDataGridColumnsData(args);
            #endregion
        }
        private void ConvertDataGridColumnsData(DataGridViewCellFormattingEventArgs args) //转换数据列数据
        {
            
        }

        private void BindData()
        {
            #region 维修项目数据
            var sqlString = string.Format("select mi.* from tb_maintain_material_detail mi left join tr_order_relation re on mi.material_id = re.pre_item_order_id where (re.post_order_id is null or re.pre_item_order_name != 'tb_maintain_material_detail') and mi.maintain_id = '{0}' and three_warranty = '1' and mi.enable_flag = '1'", _maintainId);
            //维修用料信息   
            var sqlObj = new SQLObj(); 
            sqlObj.Param = new Dictionary<string, ParamObj>();
            sqlObj.sqlString = sqlString;
            var ds = DBHelper.GetDataSet("获取维修用料信息", sqlObj);
            if (ds != null && ds.Tables.Count > 0)
            {
                var dmt = ds.Tables[0];
                if (dmt.Rows.Count > 0)
                {
                    if (dmt.Rows.Count > 0)
                    {

                        if (dmt.Rows.Count >= dgvMaterials.Rows.Count)
                        {
                            dgvMaterials.Rows.Add(dmt.Rows.Count - dgvMaterials.Rows.Count + 2);
                        }
                        else if ((dgvMaterials.Rows.Count - dmt.Rows.Count) == 1)
                        {
                            dgvMaterials.Rows.Add(1);
                        }
                        for (int i = 0; i < dmt.Rows.Count; i++)
                        {
                            DataRow dmr = dmt.Rows[i];
                            dgvMaterials.Rows[i].Cells["material_id"].Value = CommonCtrl.IsNullToString(dmr["material_id"]);
                            dgvMaterials.Rows[i].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dmr["parts_code"]);
                            dgvMaterials.Rows[i].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dmr["parts_name"]);
                            dgvMaterials.Rows[i].Cells["norms"].Value = CommonCtrl.IsNullToString(dmr["norms"]);
                            dgvMaterials.Rows[i].Cells["unit"].Value = CommonCtrl.IsNullToString(dmr["unit"]);
                            dgvMaterials.Rows[i].Cells["quantity"].Value = CommonCtrl.IsNullToString(dmr["quantity"]);
                            dgvMaterials.Rows[i].Cells["unit_price"].Value = CommonCtrl.IsNullToString(dmr["unit_price"]);
                            dgvMaterials.Rows[i].Cells["Msum_money"].Value = CommonCtrl.IsNullToString(dmr["sum_money"]);
                            dgvMaterials.Rows[i].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dmr["drawn_no"]);
                            dgvMaterials.Rows[i].Cells["vehicle_model"].Value = CommonCtrl.IsNullToString(dmr["vehicle_model"]);
                            dgvMaterials.Rows[i].Cells["Mthree_warranty"].Value = CommonCtrl.IsNullToString(dmr["three_warranty"]) == "1" ? "是" : "否";
                            dgvMaterials.Rows[i].Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dmr["remarks"]);
                            dgvMaterials.Rows[i].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dmr["whether_imported"]) == "1" ? "是" : "否";
                            dgvMaterials.Rows[i].Cells["parts_id"].Value = CommonCtrl.IsNullToString(dmr["parts_id"]);
                            dgvMaterials.Rows[i].Cells["Mdata_source"].Value = CommonCtrl.IsNullToString(dmr["data_source"]);
                            dgvMaterials.Rows[i].Cells["Mthree_warranty"].ReadOnly = CommonCtrl.IsNullToString(dmr["data_source"]) == Convert.ToInt32(DataSources.EnumDataSources.SELFBUILD).ToString() ? true : false;
                        }
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


