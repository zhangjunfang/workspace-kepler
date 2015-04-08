using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using SYSModel;
using Utility.Common;
using ServiceStationClient.ComponentUI;

namespace HXCPcClient.UCForm.DataManage.VehicleModels
{
    public partial class UCVehicleModelsView : UCBase
    {

        #region 字段属性

        //车型ID
        string vm_id;

        //车型信息
        DataRow vehInfo;

        UCVehicleModelsManage uc;

        #endregion

        #region 构造载入方法

        public UCVehicleModelsView(string vm_id, UCVehicleModelsManage parentFrm)
        {
            InitializeComponent();
            this.vm_id = vm_id;
            SubscribeEvent();
            this.uc = parentFrm;
        }

        //加载
        private void UCVehicleModelsView_Load(object sender, EventArgs e)
        {
            //SetBtnStatus(WindowStatus.View);
            DataTable dt = DBHelper.GetTable("预览车型", "v_vehicle_models", "*", string.Format(" vm_id='{0}'", vm_id), "", "");
            //如果查不到车型，全部为空
            if (dt == null || dt.Rows.Count == 0)
            {
                lblBrand.Text = string.Empty;
                lblCode.Text = string.Empty;
                lblCreateDate.Text = string.Empty;
                lblCreateUser.Text = string.Empty;
                lblName.Text = string.Empty;
                lblRemark.Text = string.Empty;
                lblStatus.Text = string.Empty;
                lblType.Text = string.Empty;
                lblUpdateDate.Text = string.Empty;
                lblUpdateUser.Text = string.Empty;
            }
            else
            {
                vehInfo = dt.Rows[0];
                lblBrand.Text = vehInfo["v_brand_name"].ToString();
                lblCode.Text = vehInfo["vm_code"].ToString();
                if (vehInfo["create_time"] != null && vehInfo["create_time"] != DBNull.Value)
                {
                    DateTime createDT = Common.UtcLongToLocalDateTime(Convert.ToInt64(vehInfo["create_time"]));
                    lblCreateDate.Text = createDT.ToString();
                }
                else
                {
                    lblCreateDate.Text = "";
                }
                lblCreateUser.Text = vehInfo["create_by_name"].ToString();
                lblName.Text = vehInfo["vm_name"].ToString();
                lblRemark.Text = vehInfo["remark"].ToString();
                lblStatus.Text = DataSources.GetDescription(typeof(DataSources.EnumStatus), vehInfo["status"]);
                lblType.Text = vehInfo["vm_type_name"].ToString();
                if (vehInfo["update_time"] != null && vehInfo["update_time"] != DBNull.Value)
                {
                    lblUpdateDate.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(vehInfo["update_time"])).ToString();
                }
                else
                {
                    lblUpdateDate.Text = "";
                }
                lblUpdateUser.Text = vehInfo["update_by_name"].ToString();
            }
            SetDataViewBtn();
            SetBtnStatus();
        }
        #endregion

        #region 事件订阅处理

        private void SubscribeEvent()
        {
            EditEvent += new ClickHandler(UCVehicleModelsView_EditEvent);
            DeleteEvent += new ClickHandler(UCVehicleModelsView_DeleteEvent);
            StatusEvent += new ClickHandler(UCVehicleModelsView_StatusEvent);
        }

        void UCVehicleModelsView_StatusEvent(object sender, EventArgs e)
        {
            SetStatus();
        }

        void UCVehicleModelsView_EditEvent(object sender, EventArgs e)
        {
            Edit();
        }

        void UCVehicleModelsView_DeleteEvent(object sender, EventArgs e)
        {
            Delete();
        }

        private void SetStatus()
        {
            if (!MessageBoxEx.ShowQuestion(string.Format("确定要{0}吗？", btnStatus.Caption)))
            {
                if (vehInfo["status"].ToString() == ((int)DataSources.EnumStatus.Start).ToString())
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
                uc.BindData(vm_id);
                deleteMenuByTag(this.Tag.ToString(), uc.Name);
            }
            else
            {
                MessageBoxEx.ShowError(btnStatus.Caption + "失败！");
                if (vehInfo["status"].ToString() == ((int)DataSources.EnumStatus.Start).ToString())
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
            if (vehInfo["status"].ToString() == ((int)DataSources.EnumStatus.Start).ToString())
            {
                status.Add("status", ((int)DataSources.EnumStatus.Stop).ToString());
            }
            else
            {
                status.Add("status", ((int)DataSources.EnumStatus.Start).ToString());
            }
            return DBHelper.Submit_AddOrEdit(btnStatus.Caption + "配件", "tb_vehicle_models", "vm_id", vehInfo["vm_id"].ToString(), status);
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
                if (DBHelper.Submit_AddOrEdit("删除车型档案", "tb_vehicle_models", "vm_id", vm_id, updateField))
                {
                    MessageBoxEx.Show("删除成功！");
                    uc.BindData();
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
            UCVehicleModelsAddOrEdit partsEdit = new UCVehicleModelsAddOrEdit(WindowStatus.Edit, vm_id, uc);
            deleteMenuByTag(this.Tag.ToString(), uc.Name);
            base.addUserControl(partsEdit, "车型档案-编辑", "UCVehicleModelsEdit" + vm_id, this.Tag.ToString(), this.Name);
        }

        #endregion

        #region 按钮状态

        /// <summary>
        /// 设置按钮状态
        /// </summary>
        private void SetBtnStatus()
        {
            if (vehInfo["data_source"].ToString() != ((int)DataSources.EnumDataSources.SELFBUILD).ToString())
            {
                btnStatus.Enabled = false;
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
                return;
            }
            if (vehInfo["status"].ToString() == ((int)DataSources.EnumStatus.Start).ToString())
            {
                btnStatus.Caption = "停用";
            }
            else
            {
                btnStatus.Caption = "启用";
            }
        }

        #endregion

    }
}
