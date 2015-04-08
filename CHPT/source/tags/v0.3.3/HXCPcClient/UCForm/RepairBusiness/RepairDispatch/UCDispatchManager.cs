using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using HXCPcClient.CommonClass;
using SYSModel;
using Utility.Common;
using HXCPcClient.Chooser;

namespace HXCPcClient.UCForm.RepairBusiness.RepairDispatch
{
    /// <summary>
    /// 维修管理-维修调度管理
    /// Author：JC
    /// AddTime：2014.10.17，修改时间：2014.11.12
    public partial class UCDispatchManager : UCDispatchBase
    {
        #region 属性设置
        /// <summary>
        /// enable_flag 1未删除
        /// </summary>
        private string strWhere = string.Empty;
        /// <summary>
        /// 单据ID值
        /// </summary>
        string strReId = string.Empty;
        /// <summary>
        /// 质检窗体
        /// </summary>
        UCInspection Inspection;
        /// <summary>
        /// 项目工时总费用
        /// </summary>
        decimal decHMoney = 0;
        /// <summary>
        /// 用料配件总费用
        /// </summary>
        decimal decPMoney = 0;
        /// <summary>
        /// 其他项目总费用
        /// </summary>
        decimal decOMoney = 0;
        List<string> listIDs = new List<string>();//已选择ID
        #endregion

        #region 初始化窗体
        public UCDispatchManager()
        {
            InitializeComponent();
            CommonCtrl.BindComboBoxByDictionarr(cobPayType, "sys_repair_pay_methods", true);//绑定维修付费方式  
            BindOrderStatus();
            SetTopbuttonShow();
            UIAssistants.SetButtonStyle4QueryAndClear(btnQuery, btnClear);  //设置查询按钮和清除按钮样式
            base.ViewEvent += new ClickHandler(UCDispatchManager_ViewEvent);
            base.Delete += new ClickHandler(UCDispatchManager_Delete);
            base.QCEvent += new ClickHandler(UCDispatchManager_QCEvent);
            base.BalanceEvent += new ClickHandler(UCDispatchManager_BalanceEvent);
        }
        #endregion

        #region 试结算事件
        void UCDispatchManager_BalanceEvent(object sender, EventArgs e)
        {
            string strMId = string.Empty;
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in dgvRData.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells["maintain_id"].Value.ToString());
                    strMId = dr.Cells["maintain_id"].Value.ToString();
                }
            }
            if (listField.Count <= 0)
            {
                MessageBoxEx.Show("请选择需要实结算的记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (listField.Count > 1)
            {
                MessageBoxEx.Show("一次仅能实结算1条记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            GetHMoney(strMId);
            GetPMoney(strMId);
            GetOMoney(strMId);
            UCTrialSettlement settle = new UCTrialSettlement();
            settle.strHMoney = decHMoney.ToString();
            settle.strPMoney = decPMoney.ToString();
            settle.strOMoney = decOMoney.ToString();
            settle.ShowDialog();
        }
        #endregion

        #region 获取项目工时总费用
        /// <summary>
        /// 获取项目工时总费用
        /// </summary>
        /// <param name="strRId">单据Id</param>
        private void GetHMoney(string strRId)
        {
            decHMoney = 0;
            DataTable dpt = DBHelper.GetTable("维修项目数据", "tb_maintain_item", "*", string.Format(" maintain_id ='{0}'", strRId), "", "order by maintain_id desc"); ;
            if (dpt.Rows.Count > 0)
            {
                for (int i = 0; i < dpt.Rows.Count; i++)
                {
                    DataRow dpr=dpt.Rows[i];
                    decHMoney += Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["sum_money_goods"]))?dpr["sum_money_goods"]:"0");
                }
            }
           
        }
        #endregion

        #region 获取用料配件总费用
        /// <summary>
        /// 获取用料配件总费用
        /// </summary>
        /// <param name="strRId">单据Id</param>
        private void GetPMoney(string strRId)
        {
            decPMoney = 0;
             DataTable dmt = DBHelper.GetTable("维修用料数据", "tb_maintain_material_detail", "*", string.Format(" maintain_id='{0}'", strRId), "", "");
             if (dmt.Rows.Count > 0)
             {
                 for (int i = 0; i < dmt.Rows.Count; i++)
                 {
                     DataRow dmr = dmt.Rows[i];
                     decPMoney += Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dmr["sum_money"])) ? dmr["sum_money"] : "0");
                 }
             }
        }
        #endregion

        #region 获取其他项目总费用
        /// <summary>
        /// 获取其他项目总费用
        /// </summary>
        /// <param name="strRId">单据Id</param>
        private void GetOMoney(string strRId)
        {
            decOMoney = 0;
              DataTable dot = DBHelper.GetTable("其他项目收费数据", "tb_maintain_other_toll", "*", string.Format(" maintain_id='{0}'", strRId), "", "");
              if (dot.Rows.Count > 0)
              {
                  for (int i = 0; i < dot.Rows.Count; i++)
                  {
                      DataRow dor = dot.Rows[i];
                      decOMoney += Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dor["sum_money"])) ? dor["sum_money"] : "0");
                  }
              }
        }
        #endregion

        #region 质检事件
        void UCDispatchManager_QCEvent(object sender, EventArgs e)
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in dgvRData.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells["maintain_id"].Value.ToString());
                }
            }
            if (listField.Count <= 0)
            {
                MessageBoxEx.Show("请选择需要质检的记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBoxEx.Show("确认要质检吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            Inspection = new UCInspection();
            if (Inspection.ShowDialog() == DialogResult.OK)
            {
                List<SQLObj> listSql = new List<SQLObj>();
                string strReceId = string.Empty;//单据Id值
                foreach (DataGridViewRow dr in dgvRData.Rows)
                {
                    object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                    if (isCheck != null && (bool)isCheck)
                    {
                        strReceId += dr.Cells["maintain_id"].Value.ToString() + ",";                        
                    }
                }
                UpdateRepairOrderInfo(listSql, strReceId,Inspection.DStatus,Inspection.Content);
                SaveProjectData(listSql, strReceId, Inspection.PStatus);
                if (DBHelper.BatchExeSQLMultiByTrans("更新单据调度状态为质检", listSql))
                {
                    MessageBoxEx.Show("质检成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindPageData();
                }
            }
        }
        #endregion

        #region 质检-更新单据状态
        /// <summary>
        /// 质检-更新单据状态
        /// </summary>
        /// <param name="listSql">SQLObj list</param>
        /// <param name="strRId">单据Id字符串</param>
        /// <param name="DStatus">调度状态枚举</param>
        /// <param name="DStatus">质检意见</param>
        private void UpdateRepairOrderInfo(List<SQLObj> listSql, string strRId, DataSources.EnumDispatchStatus DStatus, string strContent)
        {           
            strRId = strRId.Substring(0,strRId.Length-1);
            string[] Array = strRId.Split(',');
            for (int i = 0; i < Array.Length; i++)
            {
                SQLObj obj = new SQLObj();
                obj.cmdType = CommandType.Text;
                Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                dicParam.Add("maintain_id", new ParamObj("maintain_id", Array[i], SysDbType.VarChar, 40));//单据ID
                dicParam.Add("dispatch_status", new ParamObj("dispatch_status", Convert.ToInt32(DStatus).ToString(), SysDbType.VarChar, 1));//调度状态
                dicParam.Add("Verify_advice", new ParamObj("Verify_advice", strContent, SysDbType.VarChar, 200));//质检意见
                dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
                dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
                dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                obj.sqlString = "update tb_maintain_info set dispatch_status=@dispatch_status,Verify_advice=@Verify_advice,update_by=@update_by,update_name=@update_name,update_time=@update_time where maintain_id=@maintain_id";
                obj.Param = dicParam;
                listSql.Add(obj);                
            }
           
        }
        #endregion

        #region 质检-更新项目维修进度
        /// <summary>
        /// 质检-更新项目维修进度
        /// </summary>
        /// <param name="listSql">SQLObj list</param>
        /// <param name="strRId">单据Id字符串</param>
        /// <param name="PStatus">项目调度维修进度枚举</param>
        private void SaveProjectData(List<SQLObj> listSql, string strRId, DataSources.EnumProjectDisStatus PStatus)
        {
           
            strRId = strRId.Substring(0,strRId.Length-1);
            string[] pArray = strRId.Split(',');
            string strNRId = string.Empty;
            for (int i = 0; i < pArray.Length; i++)
            {
                strNRId += "'" + pArray[i] + "'" + ",";
            }
            strNRId = strNRId.Substring(0, strNRId.Length - 1);
            DataTable dpt = DBHelper.GetTable("维修项目数据", "tb_maintain_item", "*", string.Format(" maintain_id in ({0})", strNRId), "", "order by maintain_id desc"); ;
             if (dpt.Rows.Count > 0)
             {
                 for (int i = 0; i < dpt.Rows.Count; i++)
                 {
                     SQLObj obj = new SQLObj();
                     obj.cmdType = CommandType.Text;
                     Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                     DataRow dpr = dpt.Rows[i];
                     dicParam.Add("item_id", new ParamObj("item_id", dpr["item_id"], SysDbType.VarChar, 40));
                     dicParam.Add("repair_progress", new ParamObj("repair_progress", Convert.ToInt32(PStatus), SysDbType.VarChar, 40));//维修进度
                     obj.sqlString = @"update tb_maintain_item set repair_progress=@repair_progress where item_id=@item_id";
                     obj.Param = dicParam;
                     listSql.Add(obj);
                 }
             }
        }
        #endregion

        #region 驳回事件
        void UCDispatchManager_Delete(object sender, EventArgs e)
        {
            try
            {               
                List<string> listField = new List<string>();
                foreach (DataGridViewRow dr in dgvRData.Rows)
                {
                    object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                    if (isCheck != null && (bool)isCheck)
                    {
                        listField.Add(dr.Cells["maintain_id"].Value.ToString());                       
                    }
                }
                if (listField.Count == 0)
                {
                    MessageBoxEx.Show("请选择驳回记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBoxEx.Show("确认要驳回吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
                Dictionary<string, string> comField = new Dictionary<string, string>();
                comField.Add("info_status", Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString());
                comField.Add("dispatch_status", Convert.ToInt32(DataSources.EnumDispatchStatus.NotWork).ToString());//调度状态
                comField.Add("update_by", HXCPcClient.GlobalStaticObj.UserID);//修改人Id
                comField.Add("update_name", HXCPcClient.GlobalStaticObj.UserName);//修改人姓名
                comField.Add("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString());//修改时间  
                bool flag = DBHelper.BatchUpdateDataByIn("批量驳回维修调度单", "tb_maintain_info", comField, "maintain_id", listField.ToArray());
                if (flag)
                {
                    BindPageData();
                    if (dgvRData.Rows.Count > 0)
                    {
                        dgvRData.CurrentCell = dgvRData.Rows[0].Cells[0];
                    }
                    MessageBoxEx.Show("驳回成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindPageData();
                }
                else
                {
                    MessageBoxEx.Show("驳回失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("驳回失败！" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 顶部button按钮显示设置
        /// <summary>
        /// 顶部button按钮显示设置
        /// </summary>
        private void SetTopbuttonShow()
        {
            base.btnSave.Visible = false;//保存
            base.btnDtalo.Visible = false;//派工          
            base.btnAffirm.Visible = false;//确认派工
            base.btnSatart.Visible = false;//开工
            base.btnStop.Visible = false;//停工          
            base.btnCancel.Visible = false;//取消
            base.btnComplete.Visible = false;//完工
            base.btnDelete.Caption = "驳回";
        }
        #endregion

        #region 预览事件
        void UCDispatchManager_ViewEvent(object sender, EventArgs e)
        {
            //if (!IsCheck("预览"))
            //{
            //    return;
            //}
            //ViewData();
        }
        #endregion

        #region 进入预览窗体的方法
        /// <summary>
        /// 预览数据
        /// </summary>
        private void ViewData()
        {           
            UCDispatchDetails Details = new UCDispatchDetails(strReId);
            Details.uc = this;
            base.addUserControl(Details, "维修调度单-预览", "UCDispatchDetails" + strReId, this.Tag.ToString(), this.Name);
        }      
        #endregion

        #region 检测数据是否选中
        private bool IsCheck(string strMessage)
        {
            bool isOk = false;
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in dgvRData.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells["maintain_id"].Value.ToString());
                    strReId = dr.Cells["maintain_id"].Value.ToString();
                }
            }
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择" + strMessage + "记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (listField.Count > 1)
            {
                MessageBoxEx.Show(" 一次仅能" + strMessage + "1条记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (listField.Count == 1)
            {
                isOk = true;
            }
            return isOk;
        }
        #endregion

        #region 查询功能
        private void btnQuery_Click(object sender, EventArgs e)
        {
            BindPageData();
        }
        #endregion

        #region 分页查询绑定数据
        /// <summary>
        /// 分页查询绑定数据
        /// </summary>
        public void BindPageData()
        {
            try
            {
                #region 事件选择判断
                if (dtpReserveSTime.Value > dtpReserveETime.Value)
                {
                    MessageBoxEx.Show("接待日期,开始时间不能大于结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion               
                strWhere = string.Format(" enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "' and info_status='" + Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString() + "'");//enable_flag 1未删除,info_status='2'已审核
                if (!string.IsNullOrEmpty(txtCarNO.Text.Trim()))//车牌号
                {
                    strWhere += string.Format(" and  vehicle_no like '%{0}%'", txtCarNO.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtEngineNo.Caption.Trim()))//发动机号
                {
                    strWhere += string.Format(" and  engine_no like '%{0}%'", txtEngineNo.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtCustomNO.Text.Trim()))//客户编码
                {
                    strWhere += string.Format(" and  customer_code like '%{0}%'", txtCustomNO.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtCustomName.Caption.Trim()))//客户名称
                {
                    strWhere += string.Format(" and  customer_name like '%{0}%'", txtCustomName.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtOrder.Caption.Trim()))//维修单号
                {
                    strWhere += string.Format(" and  maintain_no like '%{0}%'", txtOrder.Caption.Trim());
                }
                
                if (!string.IsNullOrEmpty(cobPayType.SelectedValue.ToString()))//维修付费方式
                {
                    strWhere += string.Format(" and  maintain_payment like '%{0}%'", cobPayType.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(cobOrderStatus.SelectedValue.ToString()))//单据状体（具体状态目前不详）
                {
                    strWhere += string.Format(" and dispatch_status = '{0}'", cobOrderStatus.SelectedValue.ToString());
                }
                strWhere += string.Format(" and reception_time BETWEEN {0} and {1}", Common.LocalDateTimeToUtcLong(dtpReserveSTime.Value.Date), Common.LocalDateTimeToUtcLong(dtpReserveETime.Value.Date.AddDays(1).AddMilliseconds(-1)));//接待日期
                int recordCount;
                DataTable dt = DBHelper.GetTableByPage("分页查询维修接待单管理", "tb_maintain_info", "*", strWhere, "", " order by reception_time desc", page.PageIndex, page.PageSize, out recordCount);
                dgvRData.DataSource = dt;
                page.RecordCount = recordCount;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void page_PageIndexChanged(object sender, EventArgs e)
        {
            BindPageData();
        }

        #endregion

        #region 绑定单据状态
        /// 绑定单据状态
        /// </summary>
        private void BindOrderStatus()
        {
            cobOrderStatus.DataSource = DataSources.EnumToList(typeof(DataSources.EnumDispatchStatus), true);
            cobOrderStatus.ValueMember = "Value";
            cobOrderStatus.DisplayMember = "Text";
        }
        #endregion

        #region 重写时间
        private void dgvRData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
            string fieldNmae = dgvRData.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("reception_time"))
            {
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(e.Value)))
                {
                    long ticks = (long)e.Value;
                    e.Value = Common.UtcLongToLocalDateTime(ticks);
                }
            }
            if (fieldNmae.Equals("dispatch_status"))
            {
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(e.Value)))
                {
                    e.Value = DataSources.GetDescription(typeof(DataSources.EnumDispatchStatus), int.Parse(e.Value.ToString()));
                }
            }
            if (fieldNmae.Equals("vehicle_brand") || fieldNmae.Equals("maintain_payment"))
            {
                e.Value = GetDicName(e.Value.ToString());
            }
            if (fieldNmae.Equals("vehicle_model"))
            {
                e.Value = GetVehicleModels(e.Value.ToString());
            }
            
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

        #region 清除功能
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCarNO.Text = string.Empty;
            txtEngineNo.Caption = string.Empty;
            txtCustomNO.Text = string.Empty;
            txtCustomName.Caption = string.Empty;
            txtOrder.Caption = string.Empty;
            cobPayType.SelectedValue = string.Empty;
            cobOrderStatus.SelectedValue = string.Empty;
            txtCarType.Text = string.Empty;
            dtpReserveSTime.Value = DateTime.Now.AddMonths(-1);
            dtpReserveETime.Value = DateTime.Now;  
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
                txtEngineNo.Caption = frmVehicle.strEngineNum;
                txtCarType.Text = frmVehicle.strModel;               
            }
        }
        #endregion

        #region 车型选择器事件
        /// <summary>
        /// 车型选择器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCarType_ChooserClick(object sender, EventArgs e)
        {
            frmVehicleModels frmModels = new frmVehicleModels();
            DialogResult result = frmModels.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtCarType.Text = GetDicName(frmModels.VMID);
            }
        }
        #endregion

        #region 根据车型编号获取车型名称
        private string GetVehicleModels(string strMId)
        {
            return DBHelper.GetSingleValue("获取车型名称", "tb_vehicle_models", "vm_name", "vm_id='" + strMId + "'", "");
        }
        #endregion

        #region 客户编码选择器事件
        /// <summary>
        /// 客户编码选择器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCustomNO_ChooserClick(object sender, EventArgs e)
        {
            frmCustomerInfo frmCInfo = new frmCustomerInfo();
            DialogResult result = frmCInfo.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtCustomNO.Text = frmCInfo.strCustomerNo;
                txtCustomNO.Tag = frmCInfo.strCustomerId;
                txtCustomName.Caption = frmCInfo.strCustomerName;
            }
        }
        #endregion       

        #region 窗体Load事件
        private void UCDispatchManager_Load(object sender, EventArgs e)
        {
            dtpReserveSTime.Value = DateTime.Now.AddMonths(-1);
            dtpReserveETime.Value = DateTime.Now;          
            dgvRData.ReadOnly = false;
            foreach (DataGridViewColumn dgvc in dgvRData.Columns)
            {
                if (dgvc == colCheck)
                {
                    //dgvc.HeaderText = "选择";
                    continue;
                }
                dgvc.ReadOnly = true;
            }
            BindPageData();
        }
        #endregion

        #region 双击单元格进入详情页面
        private void dgvRData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRData.CurrentRow == null)
            {
                return;
            }
            strReId =dgvRData.CurrentRow.Cells["maintain_id"].Value.ToString();
            ViewData();
        }
        #endregion      

        #region 全选事件
        private void dgvRData_HeadCheckChanged()
        {
            SetSelectedStatus();

        }
        #endregion

        #region 选择，设置工具栏状态
        /// <summary>
        /// 选择，设置工具栏状态
        /// </summary>
        private void SetSelectedStatus()
        {
            listIDs.Clear();
            //已选择状态列表
            List<string> listFiles = new List<string>();
            foreach (DataGridViewRow dgvr in dgvRData.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells[colCheck.Name].EditedFormattedValue))
                {
                    listFiles.Add(dgvr.Cells[dispatch_status.Name].Value.ToString());
                    listIDs.Add(dgvr.Cells[maintain_id.Name].Value.ToString());
                }
            }

            //已完工
            string FinishWork = ((int)DataSources.EnumDispatchStatus.FinishWork).ToString();
            //质检通过
            string HasPassed = ((int)DataSources.EnumDispatchStatus.HasPassed).ToString();
            //已质检未通过
            string NotPassed = ((int)DataSources.EnumDispatchStatus.NotPassed).ToString();
            //已派工未开工
            string NotStartWork = ((int)DataSources.EnumDispatchStatus.NotStartWork).ToString();
            //未派工
            string NotWork = ((int)DataSources.EnumDispatchStatus.NotWork).ToString();
            //已开工
            string StartWork = ((int)DataSources.EnumDispatchStatus.StartWork).ToString();
            //已停工
            string StopWork = ((int)DataSources.EnumDispatchStatus.StopWork).ToString();          
            base.btnBalance.Enabled = listIDs.Count > 1 ? false : true;
            tsmiBalance.Enabled = listIDs.Count > 1 ? false : true;
            base.btnQC.Enabled = listFiles.Contains(HasPassed) || listFiles.Contains(NotPassed) || listFiles.Contains(NotStartWork) || listFiles.Contains(NotWork) || listFiles.Contains(StartWork) || listFiles.Contains(StopWork) ? false : true;
            tsmiQC.Enabled = listFiles.Contains(HasPassed) || listFiles.Contains(NotPassed) || listFiles.Contains(NotStartWork) || listFiles.Contains(NotWork) || listFiles.Contains(StartWork) || listFiles.Contains(StopWork) ? false : true;
            base.btnDelete.Enabled = listFiles.Contains(FinishWork) || listFiles.Contains(HasPassed) ? false : true;
            tsmidelete.Enabled = listFiles.Contains(FinishWork) || listFiles.Contains(HasPassed) ? false : true;

        }
        #endregion

        #region 单击行时选中或取消选中
        private void dgvRData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRData.CurrentCell == null)
            {
                return;
            }
            //点击选择框
            if (dgvRData.CurrentCell.OwningColumn.Name == colCheck.Name)
            {
                SetSelectedStatus();
            }
        }
        #endregion

        #region 在单元格的任意位置单击鼠标时发生
        private void dgvRData_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == colCheck.Index)
            {
                return;
            }
            //清空已选择框
            foreach (DataGridViewRow dgvr in dgvRData.Rows)
            {
                object check = dgvr.Cells[colCheck.Name].EditedFormattedValue;
                if (check != null && (bool)check)
                {
                    dgvr.Cells[colCheck.Name].Value = false;
                }
            }
            //选择当前行
            dgvRData.Rows[e.RowIndex].Cells[colCheck.Name].Value = true;
            SetSelectedStatus();
        }
        #endregion


        #region 右键功能
        /// <summary>
        /// 右键查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiSearch_Click(object sender, EventArgs e)
        {
            BindPageData();
        }
        /// <summary>
        /// 右键清除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiClear_Click(object sender, EventArgs e)
        {
            btnClear_Click(null, null);
        }
       
      
       /// <summary>
       /// 试结算
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void tsmiBalance_Click(object sender, EventArgs e)
        {
            UCDispatchManager_BalanceEvent(null,null);
        }
        /// <summary>
        /// 驳回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmidelete_Click(object sender, EventArgs e)
        {
            UCDispatchManager_Delete(null,null);
        }
        /// <summary>
        /// 质检
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiQC_Click(object sender, EventArgs e)
        {
            UCDispatchManager_QCEvent(null,null);
        }
        #endregion

    }
}
