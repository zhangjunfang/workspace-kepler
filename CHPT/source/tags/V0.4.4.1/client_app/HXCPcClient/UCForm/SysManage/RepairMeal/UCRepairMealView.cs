using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using Utility.Common;
using SYSModel;
using ServiceStationClient.ComponentUI;

namespace HXCPcClient.UCForm.SysManage.RepairMeal
{
    /// <summary>
    /// 系统管理—维修套餐设置详情
    /// Author：JC
    /// AddTime：2014.12.01
    /// </summary>
    public partial class UCRepairMealView : UCBase
    {
        #region 属性设置
        /// <summary>
        /// 单据Id值
        /// </summary>
        string strMId = string.Empty;
        /// <summary>
        /// 父窗体
        /// </summary>
        public UCRepairMealManage uc;       
        /// <summary>
        /// 单据状态
        /// </summary>
        string strStatus = string.Empty;
        #endregion

        #region 初始化窗体
        public UCRepairMealView(string strId)
        {
            InitializeComponent();
            strMId = strId;
            SetTopbuttonShow();
            SetDgvAnchor();
            base.EditEvent += new ClickHandler(UCRepairMealView_EditEvent);
            base.DeleteEvent += new ClickHandler(UCRepairMealView_DeleteEvent);
            base.StatusEvent += new ClickHandler(UCRepairMealView_StatusEvent);
            base.AddEvent += new ClickHandler(UCRepairMealView_AddEvent);           
        }
        #endregion

        #region 顶部button显示设置
        /// <summary>
        ///  顶部button显示设置
        /// </summary>
        private void SetTopbuttonShow()
        {       
            base.btnImport.Visible = false;
            base.btnSync.Visible = false;
            base.btnConfirm.Visible = false;
            base.btnActivation.Visible = false;
            base.btnBalance.Visible = false;
            base.btnSubmit.Visible = false;         
            base.btnVerify.Visible = false;
            base.btnSet.Visible = false;
            base.btnPrint.Visible = false;
            base.btnRevoke.Visible = false;
            base.btnExport.Visible = false;
            base.btnSave.Visible = false;
            base.btnView.Visible = false;
            base.btnCancel.Visible = false;
        }
        #endregion
      
        #region 新增事件
        void UCRepairMealView_AddEvent(object sender, EventArgs e)
        {
            UCRepairMealAddOrEdit Add = new UCRepairMealAddOrEdit();
            Add.windowStatus = WindowStatus.Add;
            base.addUserControl(Add, "维修套餐设置-新增", "UCRepairMealAddOrEdit" + Add.strId, this.Tag.ToString(), this.Name);
            deleteMenuByTag(this.Tag.ToString(), "UCRepairMealView");
        }
        #endregion

        #region 启用停用事件
        void UCRepairMealView_StatusEvent(object sender, EventArgs e)
        {
            List<SQLObj> listSql = new List<SQLObj>();
            SQLObj obj = new SQLObj();
            obj.cmdType = CommandType.Text;
            Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
            string strMsg = string.Empty;
            if (strStatus == Convert.ToInt32(DataSources.EnumStatus.Start).ToString())
            {
                strMsg = "停用";
                dicParam.Add("status", new ParamObj("status", DataSources.EnumStatus.Stop, SysDbType.VarChar, 40));//状态
            }
            else if (strStatus == Convert.ToInt32(DataSources.EnumStatus.Stop).ToString())
            {
                strMsg = "启用";
                dicParam.Add("status", new ParamObj("status", DataSources.EnumStatus.Start, SysDbType.VarChar, 40));//状态
            }
            if (MessageBoxEx.Show("确认要" + strMsg + "吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            dicParam.Add("repair_package_set_id", new ParamObj("repair_package_set_id", strMId, SysDbType.VarChar, 40));//维修套餐Id 
            dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id          
            dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
            obj.sqlString = "update sys_b_set_repair_package_set set status=@status,update_by=@update_by,update_time=@update_time where repair_package_set_id=@repair_package_set_id";
            obj.Param = dicParam;
            listSql.Add(obj);
            if (DBHelper.BatchExeSQLMultiByTrans("更新单据状态为启停用", listSql))
            {
                MessageBoxEx.Show("" + strMsg + "成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                uc.BindPageData();
                deleteMenuByTag(this.Tag.ToString(), this.uc.Name);
            }
        }
        #endregion

        #region 删除事件
        void UCRepairMealView_DeleteEvent(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确认要删除吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            List<SQLObj> listSql = new List<SQLObj>();
            List<string> listField = new List<string>();
            listField.Add(strMId);
            Dictionary<string, string> comField = new Dictionary<string, string>();
            comField.Add("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.DELETED).ToString());
            comField.Add("update_by", HXCPcClient.GlobalStaticObj.UserID);//修改人Id
            comField.Add("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString());//修改时间  
            bool flag = DBHelper.BatchUpdateDataByIn("删除维修设置", "sys_b_set_repair_package_set", comField, "repair_package_set_id", listField.ToArray());
            if (flag)
            {
                MessageBoxEx.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                uc.BindPageData();
                deleteMenuByTag(this.Tag.ToString(), "UCRepairMealView");
            }
            else
            {
                MessageBoxEx.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 编辑事件
        void UCRepairMealView_EditEvent(object sender, EventArgs e)
        {
            UCRepairMealAddOrEdit Edit = new UCRepairMealAddOrEdit();
            UCRepairMealManage manage = new UCRepairMealManage();
            Edit.uc = manage;
            Edit.windowStatus = WindowStatus.Edit;
            Edit.strId = strMId;  //编辑维修套餐的Id值
            base.addUserControl(Edit, "维修套餐设置-编辑", "Edit" + Edit.strId, this.Tag.ToString(), this.Name);
            deleteMenuByTag(this.Tag.ToString(), "UCRepairMealView");
        }
        #endregion

        #region 根据维修套餐设置Id获取相应的详细信息
        /// <summary>
        /// 根据维修套餐Id获取相应的详细信息
        /// </summary>
        /// <param name="strRId">维修套餐Id值</param>
        private void GetReservData(string strRId)
        {
            #region 基本信息
            DataTable dt = DBHelper.GetTable("维修套餐设置预览", "sys_b_set_repair_package_set ", "*", string.Format(" repair_package_set_id='{0}'", strRId), "", "");
            if (dt.Rows.Count > 0)
            {
                #region 维修表信息
                DataRow dr = dt.Rows[0];
                labMealCodeS.Text = CommonCtrl.IsNullToString(dr["package_code"]);//套餐编码
                labMealNameS.Text = CommonCtrl.IsNullToString(dr["package_name"]);//套餐名称                
                labRemarkS.Text = CommonCtrl.IsNullToString(dr["remarks"]);//备注               
                string strSTime = CommonCtrl.IsNullToString(dr["period_validity"]); //有效期起
                string strETime = CommonCtrl.IsNullToString(dr["valid_until"]); //有效期止
                if (!string.IsNullOrEmpty(strSTime)&&!string.IsNullOrEmpty(strETime))
                {
                    labSTimeS.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(strSTime)).ToString("yyyy-MM-dd") + " 至 " + Common.UtcLongToLocalDateTime(Convert.ToInt64(strETime)).ToString("yyyy-MM-dd");
                }
                else
                {
                    labSTimeS.Text = string.Empty;
                }
                strStatus = CommonCtrl.IsNullToString(dr["status"]);//状态
                if (strStatus == Convert.ToInt32(DataSources.EnumStatus.Start).ToString())
                {
                    base.btnStatus.Caption = "停用";
                }
                else if (strStatus == Convert.ToInt32(DataSources.EnumStatus.Stop).ToString())
                {
                    base.btnStatus.Caption = "启用";
                }

                #endregion

                #region 底部datagridview数据

                #region 维修项目数据
                DataTable dpt = DBHelper.GetTable("维修项目数据", "sys_b_set_repair_package_set_project", "*", string.Format(" repair_package_set_id='{0}' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", strRId), "", ""); ;
                if (dpt.Rows.Count > 0)
                {
                    if (dpt.Rows.Count > dgvproject.Rows.Count)
                    {
                        dgvproject.Rows.Add(dpt.Rows.Count - dgvproject.Rows.Count + 1);
                    }
                    for (int i = 0; i < dpt.Rows.Count; i++)
                    {
                        DataRow dpr = dpt.Rows[i];
                        dgvproject.Rows[i].Cells["repair_package_set_project_id"].Value = CommonCtrl.IsNullToString(dpr["repair_package_set_project_id"]);
                        dgvproject.Rows[i].Cells["project_num"].Value = CommonCtrl.IsNullToString(dpr["project_num"]);
                        dgvproject.Rows[i].Cells["project_name"].Value = CommonCtrl.IsNullToString(dpr["project_name"]);
                        dgvproject.Rows[i].Cells["repair_type"].Value = CommonCtrl.IsNullToString(dpr["repair_type"]);
                        dgvproject.Rows[i].Cells["whours_type"].Value = CommonCtrl.IsNullToString(dpr["whours_type"]);
                        dgvproject.Rows[i].Cells["whours_counts"].Value = CommonCtrl.IsNullToString(dpr["whours_counts"]);
                        dgvproject.Rows[i].Cells["whours_price"].Value = CommonCtrl.IsNullToString(dpr["whours_price"]);
                        dgvproject.Rows[i].Cells["tax_amount"].Value = CommonCtrl.IsNullToString(dpr["tax_amount"]);
                        dgvproject.Rows[i].Cells["remark"].Value = CommonCtrl.IsNullToString(dpr["remark"]);
                    }
                   
                }
                #endregion

                #region 维修用料数据
                DataTable dmt = DBHelper.GetTable("维修用料数据", "sys_b_set_repair_package_set_materials", "*", string.Format(" repair_package_set_id='{0}'and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", strRId), "", "");
                if (dmt.Rows.Count > 0)
                {

                    if (dmt.Rows.Count > dgvMaterials.Rows.Count)
                    {
                        dgvMaterials.Rows.Add(dmt.Rows.Count - dgvMaterials.Rows.Count + 1);
                    }
                    for (int i = 0; i < dmt.Rows.Count; i++)
                    {
                        DataRow dmr = dmt.Rows[i];
                        dgvMaterials.Rows[i].Cells["repair_package_set_materials_id"].Value = CommonCtrl.IsNullToString(dmr["repair_package_set_materials_id"]);
                        dgvMaterials.Rows[i].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dmr["parts_code"]);
                        dgvMaterials.Rows[i].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dmr["parts_name"]);
                        dgvMaterials.Rows[i].Cells["unit"].Value = CommonCtrl.IsNullToString(dmr["unit"]);
                        dgvMaterials.Rows[i].Cells["number"].Value = CommonCtrl.IsNullToString(dmr["number"]);
                        dgvMaterials.Rows[i].Cells["original_price"].Value = CommonCtrl.IsNullToString(dmr["original_price"]);
                        dgvMaterials.Rows[i].Cells["preferential_price"].Value = CommonCtrl.IsNullToString(dmr["preferential_price"]);
                        dgvMaterials.Rows[i].Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dmr["remarks"]);
                    }
                }
                #endregion

                #region 其他项目收费数据               
                DataTable dot = DBHelper.GetTable("其他项目收费数据", "sys_b_set_repair_package_set_other", "*", string.Format(" repair_package_set_id='{0}'", strRId), "", "");
                if (dot.Rows.Count > 0)
                {
                    if (dot.Rows.Count > dgvOther.Rows.Count)
                    {
                        dgvOther.Rows.Add(dot.Rows.Count - dgvOther.Rows.Count + 1);
                    }
                    for (int i = 0; i < dot.Rows.Count; i++)
                    {
                        DataRow dor = dot.Rows[i];
                        dgvOther.Rows[i].Cells["repair_package_set_other_id"].Value = CommonCtrl.IsNullToString(dor["repair_package_set_other_id"]);                       
                        dgvOther.Rows[i].Cells["other_expense_type"].Value = GetDicName(CommonCtrl.IsNullToString(dor["other_expense_type"]));
                        dgvOther.Rows[i].Cells["other_expense_amount"].Value = CommonCtrl.IsNullToString(dor["other_expense_amount"]);
                        dgvOther.Rows[i].Cells["Oremark"].Value = CommonCtrl.IsNullToString(dor["remark"]);
                    }                   
                }

                #endregion

                #region 附件信息数据
                //附件信息数据
                ucAttr.TableName = "sys_b_set_repair_package_set";
                ucAttr.TableNameKeyValue = strRId;
                ucAttr.BindAttachment();
                #endregion
                #endregion
            }
            #endregion
        }
        #endregion

        #region 根据码表ID获取其对应的名称
        /// <summary>
        /// 根据码表ID获取其对应的名称
        /// </summary>
        /// <param name="strId">码表Id值</param>
        private string GetDicName(string strId)
        {
            return DBHelper.GetSingleValue("获取码表值", "sys_dictionaries", "dic_name", "dic_id='" + strId + "'", "");
        }
        #endregion

        #region 设置底部DataGridView的Anchor属性
        /// <summary>
        /// 设置底部DataGridView的Anchor属性
        /// </summary>
        private void SetDgvAnchor()
        {
            dgvOther.Dock = DockStyle.Fill;
            dgvproject.Dock = DockStyle.Fill;
            dgvMaterials.Dock = DockStyle.Fill;
        }
        #endregion

        #region 窗体Load事件
        private void UCRepairMealView_Load(object sender, EventArgs e)
        {
            GetReservData(strMId);
            ucAttr.ReadOnly = true;
        }
        #endregion


    }
}
