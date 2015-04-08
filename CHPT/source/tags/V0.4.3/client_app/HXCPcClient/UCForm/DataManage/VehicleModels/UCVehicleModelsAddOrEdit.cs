using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using SYSModel;
using Utility.Common;
using System.Collections.ObjectModel;

namespace HXCPcClient.UCForm.DataManage.VehicleModels
{
    public partial class UCVehicleModelsAddOrEdit : UCBase
    {
        private string vm_id = string.Empty;//车型ID
        private WindowStatus status;
        UCVehicleModelsManage uc;
        public UCVehicleModelsAddOrEdit(WindowStatus status, string vm_id, UCVehicleModelsManage uc)
        {
            InitializeComponent();
            this.vm_id = vm_id;
            this.status = status;
            this.SaveEvent += new ClickHandler(UCVehicleModelsAddOrEdit_SaveEvent);
            CancelEvent += new ClickHandler(UCVehicleModelsAddOrEdit_CancelEvent);
            this.uc = uc;
        }
        //取消事件
        void UCVehicleModelsAddOrEdit_CancelEvent(object sender, EventArgs e)
        {
            deleteMenuByTag(this.Tag.ToString(), this.uc.Name);
        }
        //保存事件
        void UCVehicleModelsAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            Action save = Save;
            this.BeginInvoke(save);
        }
        //保存
        private void Save()
        {
            #region 验证输入的数据
            if (cboBrand.SelectedValue == null)
            {
                MessageBoxEx.Show("车辆品牌不能为空!");
                cboBrand.Focus();
                return;
            }
            if (cboType.SelectedValue == null)
            {
                MessageBoxEx.Show("车辆类型不能为空!");
                cboType.Focus();
                return;
            }
            if (txtCode.Caption.Length == 0)
            {
                MessageBoxEx.Show("车型编码不能为空!");
                txtCode.Focus();
                return;
            }
            if (txtName.Caption.Length == 0)
            {
                MessageBoxEx.Show("车型名称不能为空!");
                txtName.Focus();
                return;
            }
            if (MessageBoxEx.Show("确认要保存吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            #endregion
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("v_brand", cboBrand.SelectedValue.ToString());
            dic.Add("vm_type", cboType.SelectedValue.ToString());
            dic.Add("vm_code", txtCode.Caption);
            dic.Add("vm_name", txtName.Caption);
            dic.Add("remark", txtRemark.Caption);
            //判断窗体状态
            if (status == WindowStatus.Add || status == WindowStatus.Copy)
            {
                dic.Add("data_source", ((int)DataSources.EnumDataSources.SELFBUILD).ToString());
                dic.Add("vm_id", Guid.NewGuid().ToString());
                dic.Add("enable_flag", ((int)DataSources.EnumEnableFlag.USING).ToString());
                dic.Add("status", ((int)DataSources.EnumStatus.Start).ToString());
                dic.Add("create_by", GlobalStaticObj.UserID);
                dic.Add("create_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());
                if (DBHelper.Submit_AddOrEdit("车型操作", "tb_vehicle_models", "", "", dic))
                {
                    MessageBoxEx.Show("保存成功!");
                    uc.BindData(vm_id);
                    deleteMenuByTag(this.Tag.ToString(), this.uc.Name);
                }
                else
                {
                    MessageBoxEx.Show("保存失败!");
                }
            }
            else if (status == WindowStatus.Edit)
            {
                dic.Add("update_by", GlobalStaticObj.UserID);
                dic.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());
                if (DBHelper.Submit_AddOrEdit("车型操作", "tb_vehicle_models", "vm_id", vm_id, dic))
                {
                    MessageBoxEx.Show("修改成功!");
                    uc.BindData(vm_id);
                    deleteMenuByTag(this.Tag.ToString(), this.uc.Name);
                }
                else
                {
                    MessageBoxEx.Show("修改失败!");
                }
            }
        }
        //加载
        private void UCVehicleModelsAddOrEdit_Load(object sender, EventArgs e)
        {
            var btnCols = new ObservableCollection<ButtonEx_sms>()
            {
                btnSave, btnImport, btnExport, btnSet, btnView, btnPrint,btnCancel
            };
            UIAssistants.SetUCBaseFuncationVisible(this, btnCols);
            BindControls();
            //如果是编辑或者复制,则先绑定数据
            if ((status == WindowStatus.Edit || status == WindowStatus.Copy) && vm_id.Length > 0)
            {
                BindData();
            }
            if (status == WindowStatus.Add || status == WindowStatus.Copy)
            {
                txtCode.Caption = CommonUtility.GetNewNo(DataSources.EnumProjectType.VehicleModels);
            }
            SetBtnStatus(status);
        }

        /// <summary>
        /// 根据ID绑定原数据
        /// </summary>
        private void BindData()
        {
            string strWhere = string.Format("vm_id='{0}'", vm_id);
            DataTable dt = DBHelper.GetTable("查询车型", "tb_vehicle_models", "vm_id,vm_code,vm_name,v_brand,vm_type,data_source,remark", strWhere, "", "");
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            DataRow dr = dt.Rows[0];
            cboBrand.SelectedValue = dr["v_brand"];
            cboDataSource.SelectedValue = dr["data_source"];
            cboType.SelectedValue = dr["vm_type"];
            txtCode.Caption = dr["vm_code"].ToString();
            txtName.Caption = dr["vm_name"].ToString();
            txtRemark.Caption = dr["remark"].ToString();
        }

        /// <summary>
        /// 关闭判断
        /// </summary>
        /// <returns>是否关闭</returns>
        public override bool CloseMenu()
        {
            //return base.CloseMenu();
            return MessageBoxEx.ShowQuestion("是否关闭?");

        }

        //绑定控件
        private void BindControls()
        {
            //CommonCtrl.BindComboBoxByDictionarr(cboDataSource, "sys_data_source", false);//绑定数据源
            CommonCtrl.BindComboBoxByDictionarr(cboBrand, "sys_vehicle_brand", false);//绑定车型品牌
            CommonCtrl.BindComboBoxByDictionarr(cboType, "sys_vehicle_model_category", false);//绑定车型类型
        }

    }
}
