using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using HXCPcClient.CommonClass;
using HXCPcClient.UCForm.RepairBusiness.RepairQuery;

namespace HXCPcClient.UCForm.RepairBusiness.RepairProgress
{
    /// <summary>
    /// 维修管理-维修进度查询
    /// Author：JC
    /// AddTime：2014.11.1
    /// </summary>
    public partial class UCRepairProgressManager : UCBase
    {
        #region 属性设置
        /// <summary>
        /// 维修状态-维修中
        /// </summary>
        string strOneStatusKey = "WXZ";
        /// <summary>
        ///  维修状态-等待中
        /// </summary>
        string strTwoStatusKey = "DDZ";
        /// <summary>
        ///  维修状态-完工
        /// </summary>
        string strThreeStatusKey = "WG";
        #endregion
        public UCRepairProgressManager()
        {
            InitializeComponent();
            SetTopbuttonShow();
            CommonFuncCall.BindComBoxDataSource(cobStation, "sys_station_information", "全部");//工位           
            UIAssistants.SetButtonStyle4QueryAndClear(btnQuery, btnClear);  //设置查询按钮和清除按钮样式
        }

        #region 顶部button按钮显示设置
        /// <summary>
        /// 顶部button按钮显示设置
        /// </summary>
        private void SetTopbuttonShow()
        {
            base.btnAdd.Visible = false;
            base.btnCopy.Visible = false;           
            base.btnEdit.Visible = false;
            base.btnDelete.Visible = false;
            base.btnStatus.Visible = false;
            base.btnVerify.Visible = false;
            base.btnSave.Visible = false;
            base.btnSubmit.Visible = false;
            base.btnImport.Visible = false;
            base.btnExport.Visible = false;        
            base.btnActivation.Visible = false;
            base.btnSync.Visible = false;
            base.btnCommit.Visible = false;
            base.btnCancel.Visible = false;
            base.btnConfirm.Visible = false;
           
        }
        #endregion

        #region 车牌号选择器事件
        /// <summary>
        /// 车牌号选择器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCarNO_ChooserClick(object sender, EventArgs e)
        {
            frmVehicleGrade frmVehicle = new frmVehicleGrade();
            DialogResult result = frmVehicle.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtCarNO.Text = frmVehicle.strLicensePlate;               
            }
        }
        #endregion

        #region 维修工选择器-人员选择器
        private void txtRepairWorker_ChooserClick(object sender, EventArgs e)
        {
            frmPersonnelSelector frmSelector = new frmPersonnelSelector();
            frmSelector.isMoreCheck = true;
            DialogResult result = frmSelector.ShowDialog();
            if (result == DialogResult.OK)
            {      
                txtRepairWorker.Text = frmSelector.strPersonName.Contains(",")? frmSelector.strPersonName.Substring(0,frmSelector.strPersonName.Length-1):frmSelector.strPersonName;
                txtRepairWorker.Tag = frmSelector.strUserId.Substring(0,frmSelector.strUserId.Length-1);
            }
        }
        #endregion

        #region 窗体Load事件-初始化查询进度面板
        private void UCRepairProgressManager_Load(object sender, EventArgs e)
        {
            palData.AutoScroll = true;
            RepairProgress.ExtDataGrid.ColMinWidth = 115;
            RepairProgress.ExtDataGrid.RowMinHeight = 80;
            RepairProgress.ExtDataGrid.Title = "维修工"; 
             Dictionary<string, string> dicOneColumns = new Dictionary<string, string>();

             string sqlWhere = " parent_id in (select dic_id from sys_dictionaries where dic_code='sys_station_information' and enable_flag=1) ";
             DataTable dt_dic = DBHelper.GetTable("查询字典码表信息", "sys_dictionaries", "dic_id,dic_name", sqlWhere, "", " order by create_time ");
             if (dt_dic != null && dt_dic.Rows.Count > 0)
             {
                 foreach (DataRow dr in dt_dic.Rows)
                 {
                     dicOneColumns.Add(dr["dic_id"].ToString(), dr["dic_name"].ToString());                     
                 }
             }
            Dictionary<string, string> dicTwoColumns = new Dictionary<string, string>();
            dicTwoColumns.Add(strOneStatusKey,"等待中");
            dicTwoColumns.Add(strTwoStatusKey, "维修中");
            dicTwoColumns.Add(strThreeStatusKey, "完工");
            RepairProgress.ExtDataGrid.BuildGridColumns(dicOneColumns, dicTwoColumns);//生成表头
           
            RepairProgress.ExtDataGrid.CellClickEvenHandle += CellClickEvenHandle;

        }
        #endregion

        #region 车牌号单击进入预览窗体
        /// <summary>
       /// 
       /// </summary>
       /// <param name="strKey">维修工Id</param>
        private void CellClickEvenHandle(string strKey)
        {
            RepairQueryView Qview = new RepairQueryView(strKey);
            base.addUserControl(Qview, "维修单-预览", "Qview" + strKey, this.Tag.ToString(), this.Name);
        }
        #endregion

        #region 查询事件
        private void btnQuery_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dicRowsName = new Dictionary<string, string>();
            string strWorkersName = txtRepairWorker.Text.Trim();
            string strWorkersTag =CommonCtrl.IsNullToString(txtRepairWorker.Tag);
            string[] ArryWorkers = strWorkersName.Split(',');
            string[] ArryWorkersTag = strWorkersTag.Split(',');
            string strSelectName = string.Empty;//人员名称
            for (int i = 0; i < ArryWorkers.Length; i++)
            {
                dicRowsName.Add(ArryWorkersTag[i], ArryWorkers[i]);
                strSelectName += "'" + ArryWorkers[i] + "'" + ",";
            }
            RepairProgress.ExtDataGrid.BuildGridRows(dicRowsName);//生成行
            string strTables = "tb_maintain_info a right join tb_maintain_dispatch_worker b on b.maintain_id=a.maintain_id";
            string strFiles = "a.maintain_id,a.dispatch_status,a.vehicle_no,b.dispatch_id,b.dispatch_name";
            string strWhere = " a.dispatch_status in ('0','1','2','3','4') and b.dispatch_name in (" + strSelectName.Substring(0, strSelectName.Length - 1) + ") ";
            if (!string.IsNullOrEmpty(txtCarNO.Text.Trim()))
            {
                strWhere += " and a.vehicle_no like '%"+txtCarNO.Text.Trim()+"%'";
            }
            if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobStation.SelectedValue)))
            {
                strWhere += " and b.team_name like '%" + cobStation.SelectedValue.ToString() + "%'";
            }
            DataTable dt = DBHelper.GetTable("查询维修单单", strTables, strFiles, strWhere, "", "order by b.dispatch_id desc");
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            for (int i = 0; i < ArryWorkers.Length; i++)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    DataRow dr = dt.Rows[j];
                    if (ArryWorkers[i] == CommonCtrl.IsNullToString(dr["dispatch_name"]))
                    {
                        string status = CommonCtrl.IsNullToString(dr["dispatch_status"]);
                        string strCStatus = string.Empty;
                        switch (status)
                        {
                            case "0":
                                strCStatus = strOneStatusKey;
                                break;
                            case "1":
                                strCStatus = strOneStatusKey;
                                break;
                            case "2":
                                strCStatus = strOneStatusKey;
                                break;
                            case "3":
                                strCStatus = strTwoStatusKey;
                                break;
                            case "4":
                                strCStatus = strThreeStatusKey;
                                break;
                        }
                        RepairProgress.ExtDataGrid.AddCellContent(ArryWorkersTag[i], cobStation.SelectedValue.ToString(), strCStatus, CommonCtrl.IsNullToString(dr["maintain_id"]), CommonCtrl.IsNullToString(dr["vehicle_no"]));

                    }
                }

            }
        }
        #endregion

        #region 清除事件
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCarNO.Text = string.Empty;
            txtCarNO.Tag = string.Empty;
            txtRepairWorker.Text = string.Empty;
            txtRepairWorker.Tag = string.Empty;
            cobStation.SelectedValue = string.Empty;

        }
        #endregion
    }
}
