using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using Utility.Common;
using System.Threading;

namespace HXCPcClient.UCForm.SysManage.AcountManage
{
    /// <summary>
    /// 维修业务参数
    /// 孙明生
    /// 修改人：杨天帅
    /// </summary>
    public partial class UCRepairBPara : UCBase
    {
        #region --成员变量
        /// <summary>
        /// 维修业务参数ID
        /// </summary>
        string strR_param_id = string.Empty;
        #endregion

        #region --构造函数
        public UCRepairBPara()
        {
            InitializeComponent();            
        }
        #endregion

        #region --窗体初始化
        private void UCRepairBPara_Load(object sender, EventArgs e)
        {
            base.RoleButtonStstus(this.Name);//角色按钮权限-是否隐藏   
            this.btnDelete.Visible = false;
            this.btnStatus.Visible = false;
            this.btnSave.Visible = true;
            this.btnCancel.Visible = true;

            foreach (Control ctl in this.panelEx1.Controls)
            {
                if (ctl is CheckBox)
                {
                    ctl.Enabled = false;
                }
            }
            this.tbWorkHour.ReadOnly = true;

            this.uiHandler -= new UiHandler(this.InitData);
            this.uiHandler += new UiHandler(this.InitData);

            base.EditEvent += new ClickHandler(UCRepairBPara_EditEvent);
            base.SaveEvent += new ClickHandler(UCRepairBPara_SaveEvent);
            base.CancelEvent += new ClickHandler(UC_CancelEvent);

            ThreadPool.QueueUserWorkItem(new WaitCallback(this._InitData));
        }
        #endregion

        #region --显示数据
        private void _InitData(object obj)
        {
            DataTable dt = DBHelper.GetTable("查询维修业务参数", GlobalStaticObj.CommAccCode, "sys_repair_param", "*", "book_id='" + GlobalStaticObj.CurrAccID + "'", "", "");

            this.Invoke(this.uiHandler, dt);
        }
        private void InitData(object obj)
        {
            DataTable dt = obj as DataTable;
            
            if (dt != null && dt.Rows.Count > 0)
            {
                strR_param_id = dt.Rows[0]["r_param_id"].ToString();
                chkappointment_audit.Checked = dt.Rows[0]["appointment_audit"].ToString() == "1" ? true : false;
                chkr_reception_audit.Checked = dt.Rows[0]["r_reception_audit"].ToString() == "1" ? true : false;
                chkr_schedul_quality_ins.Checked = dt.Rows[0]["r_schedul_quality_ins"].ToString() == "1" ? true : false;
                chkr_settlement_audit.Checked = dt.Rows[0]["r_settlement_audit"].ToString() == "1" ? true : false;
                chkrescue_audit.Checked = dt.Rows[0]["rescue_audit"].ToString() == "1" ? true : false;
                chkr_return_audit.Checked = dt.Rows[0]["r_return_audit"].ToString() == "1" ? true : false;
                chkrequisition_audit.Checked = dt.Rows[0]["requisition_audit"].ToString() == "1" ? true : false;
                chkmaterial_return_audit.Checked = dt.Rows[0]["material_return_audit"].ToString() == "1" ? true : false;
                chksingle_editors_same_person.Checked = dt.Rows[0]["single_editors_same_person"].ToString() == "1" ? true : false;
                chksingle_audit_same_person.Checked = dt.Rows[0]["single_audit_same_person"].ToString() == "1" ? true : false;
                chksingle_disabled_same_person.Checked = dt.Rows[0]["single_disabled_same_person"].ToString() == "1" ? true : false;
                chksingle_delete_same_person.Checked = dt.Rows[0]["single_delete_same_person"].ToString() == "1" ? true : false;
                chkrepair_reception_import_pre.Checked = dt.Rows[0]["repair_reception_import_pre"].ToString() == "1" ? true : false;
                chkrepair_return_import_pre.Checked = dt.Rows[0]["repair_return_import_pre"].ToString() == "1" ? true : false;
                chkrequisition_import_pre.Checked = dt.Rows[0]["requisition_import_pre"].ToString() == "1" ? true : false;
                chkmaterial_return_import_pre.Checked = dt.Rows[0]["material_return_import_pre"].ToString() == "1" ? true : false;
                chkthree_service_import_pre_yt.Checked = dt.Rows[0]["three_service_import_pre_yt"].ToString() == "1" ? true : false;
                chkold_pieces_storage_import_pre.Checked = dt.Rows[0]["old_pieces_storage_import_pre"].ToString() == "1" ? true : false;
                chkallow_material_larger_parts_r.Checked = dt.Rows[0]["allow_material_larger_parts_r"].ToString() == "1" ? true : false;
                chkrequisition_auto_outbound.Checked = dt.Rows[0]["requisition_auto_outbound"].ToString() == "1" ? true : false;
                if (dt.Columns.Contains("time_standards"))
                {
                    //工时标准
                    this.tbWorkHour.Caption = dt.Rows[0]["time_standards"].ToString();
                }
                else
                {
                    this.tbWorkHour.Caption = "0";
                }
            }
        }
        #endregion

        #region --按钮操作

        #region 编辑
        void UCRepairBPara_EditEvent(object sender, EventArgs e)
        {
            foreach (Control ctl in this.panelEx1.Controls)
            {
                if (ctl is CheckBox)
                {
                    ctl.Enabled = true;
                }
            }
            this.tbWorkHour.ReadOnly = false;
        }
        #endregion

        /// <summary> 取消
        /// </summary>
        void UC_CancelEvent(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(this._InitData));
        }

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        void UCRepairBPara_SaveEvent(object sender, EventArgs e)
        {
            if (!chkappointment_audit.Enabled)
            {
                return;
            }

            decimal time_standards = 0;
            if (this.tbWorkHour.Caption.Trim().Length > 0)
            {
                if (!decimal.TryParse(this.tbWorkHour.Caption.Trim(), out time_standards))
                {
                    time_standards = 0;
                }
            }

            string keyName = string.Empty;
            string keyValue = string.Empty;
            string opName = "新增维修业务参数";

            Dictionary<string, string> Fileds = new Dictionary<string, string>();
            Fileds.Add("appointment_audit", chkappointment_audit.Checked == true ? "1" : "0");
            Fileds.Add("r_reception_audit", chkr_reception_audit.Checked == true ? "1" : "0");
            Fileds.Add("r_schedul_quality_ins", chkr_schedul_quality_ins.Checked == true ? "1" : "0");
            Fileds.Add("r_settlement_audit", chkr_settlement_audit.Checked == true ? "1" : "0");

            Fileds.Add("rescue_audit", chkrescue_audit.Checked == true ? "1" : "0");
            Fileds.Add("r_return_audit", chkr_return_audit.Checked == true ? "1" : "0");
            Fileds.Add("requisition_audit", chkrequisition_audit.Checked == true ? "1" : "0");
            Fileds.Add("material_return_audit", chkmaterial_return_audit.Checked == true ? "1" : "0");
            Fileds.Add("single_editors_same_person", chksingle_editors_same_person.Checked == true ? "1" : "0");
            Fileds.Add("single_audit_same_person", chksingle_audit_same_person.Checked == true ? "1" : "0");
            Fileds.Add("single_disabled_same_person", chksingle_disabled_same_person.Checked == true ? "1" : "0");
            Fileds.Add("single_delete_same_person", chksingle_delete_same_person.Checked == true ? "1" : "0");
            Fileds.Add("repair_reception_import_pre", chkrepair_reception_import_pre.Checked == true ? "1" : "0");
            Fileds.Add("repair_return_import_pre", chkrepair_return_import_pre.Checked == true ? "1" : "0");
            Fileds.Add("requisition_import_pre", chkrequisition_import_pre.Checked == true ? "1" : "0");
            Fileds.Add("material_return_import_pre", chkmaterial_return_import_pre.Checked == true ? "1" : "0");
            Fileds.Add("three_service_import_pre_yt", chkthree_service_import_pre_yt.Checked == true ? "1" : "0");
            Fileds.Add("old_pieces_storage_import_pre", chkold_pieces_storage_import_pre.Checked == true ? "1" : "0");
            Fileds.Add("allow_material_larger_parts_r", chkallow_material_larger_parts_r.Checked == true ? "1" : "0");
            Fileds.Add("requisition_auto_outbound", chkrequisition_auto_outbound.Checked == true ? "1" : "0");
            //工时标准
            Fileds.Add("time_standards", time_standards.ToString());

            string nowUtcTicks = Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString();
            Fileds.Add("update_by", HXCPcClient.GlobalStaticObj.UserID);
            Fileds.Add("update_time", nowUtcTicks);
            if (this.strR_param_id.Length == 0)
            {
                Fileds.Add("book_id", GlobalStaticObj.CurrAccID);
                Fileds.Add("r_param_id", Guid.NewGuid().ToString());//新ID                   
                Fileds.Add("create_by", HXCPcClient.GlobalStaticObj.UserID);
                Fileds.Add("create_time", nowUtcTicks);
            }
            else
            {
                keyName = "r_param_id";
                keyValue = this.strR_param_id;
                opName = "更新维修业务参数";
            }
            bool flag = DBHelper.Submit_AddOrEdit(opName, GlobalStaticObj.CommAccCode, "sys_repair_param", keyName, keyValue, Fileds);
            if (flag)
            {
                MessageBoxEx.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                foreach (Control ctl in this.panelEx1.Controls)
                {
                    if (ctl is CheckBox)
                    {
                        ctl.Enabled = false;
                    }
                }
                this.tbWorkHour.ReadOnly = true;
            }
            else
            {
                MessageBoxEx.Show("保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #endregion
    }
}
