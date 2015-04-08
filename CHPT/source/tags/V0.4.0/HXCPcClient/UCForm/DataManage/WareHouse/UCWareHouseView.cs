using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using Model;
using SYSModel;

namespace HXCPcClient.UCForm.DataManage.WareHouse
{
    public partial class UCWareHouseView : UCBase
    {
        public UCWareHouseView(string wareHouseId)
        {
            InitializeComponent();
            LoadInfo(wareHouseId);
            GetAllCargoSpace(wareHouseId);
            ucAttr.TableName = "tb_warehouse";
            ucAttr.TableNameKeyValue = wareHouseId;
            ucAttr.BindAttachment();
            ucAttr.ReadOnly = true;
            ucAttr.Size = tabPage2.Size;
        }
        /// <summary>
        /// 加载仓库档案信息
        /// </summary>
        private void LoadInfo(string wareHouseId)
        {
            if (!string.IsNullOrEmpty(wareHouseId))
            {
                //1.加载仓库档案主信息
                DataTable dt = DBHelper.GetTable("查看一条仓库档案信息", "v_warehouse_companyname_username", "*", " wh_id='" + wareHouseId + "'", "", "");
                if (dt != null && dt.Rows.Count > 0)
                {
                    tb_warehouse tb_warehouse_Model = new tb_warehouse();
                    CommonFuncCall.SetModlByDataTable(tb_warehouse_Model, dt);
                    CommonFuncCall.SetShowControlValue(this, tb_warehouse_Model, "View");
                    lblcom_id.Text = dt.Rows[0]["com_name"].ToString();

                    DataSources.EnumDataSources enumDataSources = (DataSources.EnumDataSources)Convert.ToInt16(tb_warehouse_Model.data_source);
                    lbldata_source.Text = DataSources.GetDescription(enumDataSources, true);

                    if (!string.IsNullOrEmpty(tb_warehouse_Model.county))
                    {
                        lblwh_address.Text = CommonFuncCall.GetAddress(tb_warehouse_Model.county) + " " + lblwh_address.Text;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(tb_warehouse_Model.city))
                        {
                            lblwh_address.Text = CommonFuncCall.GetAddress(tb_warehouse_Model.city) + " " + lblwh_address.Text;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(tb_warehouse_Model.province))
                            {
                                lblwh_address.Text = CommonFuncCall.GetAddress(tb_warehouse_Model.province) + " " + lblwh_address.Text;
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 获取该仓库所关联的所有仓库库位信息
        /// </summary>
        /// <param name="wh_id"></param>
        /// <returns></returns>
        private void GetAllCargoSpace(string wh_id)
        {
            string conId = string.Empty;
            DataTable dt_cargo_space = DBHelper.GetTable("查询关联的仓库库位信息", "tb_cargo_space", "*", " wh_id='" + wh_id + "' and enable_flag=1", "", "");
            if (dt_cargo_space != null && dt_cargo_space.Rows.Count > 0)
            {
                foreach (DataRow dr in dt_cargo_space.Rows)
                {
                    DataGridViewRow dgvr = gvWareHouseList.Rows[gvWareHouseList.Rows.Add()];
                    dgvr.Cells["colcs_code"].Value = dr["cs_code"];
                    dgvr.Cells["colcs_name"].Value = dr["cs_name"];
                    dgvr.Cells["colstock_u_limit"].Value = dr["stock_u_limit"];
                    dgvr.Cells["colstock_l_limit"].Value = dr["stock_l_limit"];
                    dgvr.Cells["colremark"].Value = dr["remark"];
                }
            }
        }

        private void tabPage2_SizeChanged(object sender, EventArgs e)
        {
            ucAttr.Size = tabPage2.Size;
        }

        private void UCWareHouseView_Load(object sender, EventArgs e)
        {
            //base.SetBtnStatus(WindowStatus.View);
            SetDataBtnView();
            btnSync.Enabled = false;

        }
    }
}
