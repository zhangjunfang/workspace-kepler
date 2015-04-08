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
using ServiceStationClient.ComponentUI;

namespace HXCPcClient.UCForm.DataManage.WareHouse
{
    public partial class UCWareHouseView : UCBase
    {
        #region 字段属性
        tb_warehouse model;

        UCWareHouseManager uc;
        #endregion

        #region 构造和载入函数

        public UCWareHouseView(string wareHouseId, UCWareHouseManager ParentFrm)
        {
            InitializeComponent();
            LoadInfo(wareHouseId);
            GetAllCargoSpace(wareHouseId);
            ucAttr.TableName = "tb_warehouse";
            ucAttr.TableNameKeyValue = wareHouseId;
            ucAttr.BindAttachment();
            ucAttr.ReadOnly = true;
            ucAttr.Size = tabPage2.Size;
            uc = ParentFrm;
            SubscribeEvent();
        }

        private void UCWareHouseView_Load(object sender, EventArgs e)
        {
            //base.SetBtnStatus(WindowStatus.View);
            SetDataViewBtn();
            btnSync.Enabled = false;
            SetBtnStatus();
        }

        #endregion

        #region 事件订阅处理
        private void SubscribeEvent()
        {
            EditEvent += new ClickHandler(UCWareHouseView_EditEvent);
            DeleteEvent += new ClickHandler(UCWareHouseView_DeleteEvent);
            StatusEvent += new ClickHandler(UCWareHouseView_StatusEvent);
        }

        void UCWareHouseView_StatusEvent(object sender, EventArgs e)
        {
            SetStatus();
        }

        void UCWareHouseView_DeleteEvent(object sender, EventArgs e)
        {
            Delete();
        }

        void UCWareHouseView_EditEvent(object sender, EventArgs e)
        {
            Edit();
        }

        private void SetStatus()
        {
            if (!MessageBoxEx.ShowQuestion(string.Format("确定要{0}吗？", btnStatus.Caption)))
            {
                if (model.status == ((int)DataSources.EnumStatus.Start).ToString())
                {
                    btnStatus.Caption = "启用";

                }
                else
                {
                    btnStatus.Caption = "停用";
                }
                return;
            }

            if (StatusSql())
            {
                MessageBoxEx.Show(btnStatus.Caption + "成功！");
                uc.BindgvWareHouseList();
                deleteMenuByTag(this.Tag.ToString(), uc.Name);
            }
            else
            {
                MessageBoxEx.ShowError(btnStatus.Caption + "失败！");
                if (model.status == ((int)DataSources.EnumStatus.Start).ToString())
                {
                    btnStatus.Caption = "启用";

                }
                else
                {
                    btnStatus.Caption = "停用";
                }

            }
        }

        private bool StatusSql()
        {
            Dictionary<string, string> status = new Dictionary<string, string>();
            if (model.status == ((int)DataSources.EnumStatus.Start).ToString())
            {
                status.Add("status", ((int)DataSources.EnumStatus.Stop).ToString());
            }
            else
            {
                status.Add("status", ((int)DataSources.EnumStatus.Start).ToString());
            }
            return DBHelper.Submit_AddOrEdit(btnStatus.Caption + "仓库", "tb_warehouse", "wh_id", model.wh_id, status);
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void Delete()
        {
            if (MessageBoxEx.Show("是否要删除当前数据？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Dictionary<string, string> updateField = new Dictionary<string, string>();
                updateField.Add("enable_flag", "0");
                if (DBHelper.Submit_AddOrEdit("删除供仓库", "tb_warehouse", "wh_id", model.wh_id, updateField))
                {
                    MessageBoxEx.Show("删除成功！");
                    uc.BindgvWareHouseList();
                    deleteMenuByTag(this.Tag.ToString(), uc.Name);
                }
                else
                {
                    MessageBoxEx.Show("删除失败！");
                }
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        private void Edit()
        {
            UCWareHouseAddOrEdit editFrm = new UCWareHouseAddOrEdit(WindowStatus.Edit, model.wh_id, uc);
            deleteMenuByTag(this.Tag.ToString(), uc.Name);
            base.addUserControl(editFrm, "仓库档案-编辑", "UCWareHouseEdit" + model.wh_id, this.Tag.ToString(), this.Name);
        }

     

        #endregion

        #region 按钮状态

        /// <summary>
        /// 设置按钮状态
        /// </summary>
        private void SetBtnStatus()
        {
            if (model.status == ((int)DataSources.EnumStatus.Start).ToString())
            {
                btnStatus.Caption = "停用";
            }
            else
            {
                btnStatus.Caption = "启用";
            }
        }

        #endregion

        #region 数据绑定
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
                    model = new tb_warehouse();
                    CommonFuncCall.SetModlByDataTable(model, dt);
                    CommonFuncCall.SetShowControlValue(this, model, "View");
                    lblcom_id.Text = dt.Rows[0]["com_name"].ToString();

                    DataSources.EnumDataSources enumDataSources = (DataSources.EnumDataSources)Convert.ToInt16(model.data_source);
                    lbldata_source.Text = DataSources.GetDescription(enumDataSources, true);

                    if (!string.IsNullOrEmpty(model.county))
                    {
                        lblwh_address.Text = CommonFuncCall.GetAddress(model.county) + " " + lblwh_address.Text;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(model.city))
                        {
                            lblwh_address.Text = CommonFuncCall.GetAddress(model.city) + " " + lblwh_address.Text;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(model.province))
                            {
                                lblwh_address.Text = CommonFuncCall.GetAddress(model.province) + " " + lblwh_address.Text;
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
        #endregion

        #region 附件大小
        private void tabPage2_SizeChanged(object sender, EventArgs e)
        {
            ucAttr.Size = tabPage2.Size;
        }
        #endregion

    }
}
