using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using Utility.Common;
using ServiceStationClient.ComponentUI;
using SYSModel;
using HXCPcClient.Chooser;
using ServiceStationClient.ComponentUI.TextBox;

namespace HXCPcClient.UCForm.RepairBusiness.RepairQuery
{
    /// <summary>
    /// 维修管理-维修单查询管理
    /// Author：JC
    /// AddTime：2014.10.18
    /// </summary>
    public partial class RepairQueryManager : UCBase
    {
        #region 属性设置
        private DataTable dt;
        /// <summary>
        /// 维修单Id
        /// </summary>
        string strRepairId = string.Empty;
        #endregion

        #region --构造函数
        public RepairQueryManager()
        {
            this.dt = null;
            InitializeComponent();
            this.InitUC();
            SetQuick();
        }
        public RepairQueryManager(DataTable dt)
        {
            InitializeComponent();
            this.InitUC();
            this.dt = dt;

        }
        private void InitUC()
        {
            this.BindOrderStatus();

            this.SetDgvAnchor();

            CommonCtrl.CmbBindDeptment(this.cboOrgId, string.Empty);

            CommonCtrl.CmbBindUser(this.cobPHandle, string.Empty);
            CommonCtrl.CmbBindUser(this.cboHandle, string.Empty);
            this.SetUCStyle();
            base.ViewEvent -= new ClickHandler(RepairQueryManager_ViewEvent);
            base.ViewEvent += new ClickHandler(RepairQueryManager_ViewEvent);
        }
        #endregion

        #region 设置速查功能
        /// <summary>
        /// 设置速查功能
        /// </summary>
        private void SetQuick()
        {

            //按维修单查询设置车牌号速查
            txtCarNO.SetBindTable("tb_vehicle", "license_plate");
            txtCarNO.GetDataSourced += new ServiceStationClient.ComponentUI.TextBox.TextChooser.GetDataSourceHandler(tc_GetDataSourced);
            //按维修单查询设置客户编码速查
            txtCustomNO.SetBindTable("tb_customer", "cust_code");
            txtCustomNO.GetDataSourced += new ServiceStationClient.ComponentUI.TextBox.TextChooser.GetDataSourceHandler(tc_GetDataSourced);
            txtCustomNO.DataBacked += new TextChooser.DataBackHandler(txtCustomNO_DataBacked);
            //按维修单查询设置车型速查
            txtCarType.SetBindTable("tb_vehicle_models", "vm_name");
            txtCarType.GetDataSourced += new ServiceStationClient.ComponentUI.TextBox.TextChooser.GetDataSourceHandler(tc_GetDataSourced);
            //按维修项目查询设置车牌号速查
            txtPCarCode.SetBindTable("tb_vehicle", "license_plate");
            txtPCarCode.GetDataSourced += new ServiceStationClient.ComponentUI.TextBox.TextChooser.GetDataSourceHandler(tc_GetDataSourced);
            //按维修项目查询设置客户编码速查
            txtPCustomCode.SetBindTable("tb_customer", "cust_code");
            txtPCustomCode.GetDataSourced += new ServiceStationClient.ComponentUI.TextBox.TextChooser.GetDataSourceHandler(tc_GetDataSourced);
            txtPCustomCode.DataBacked += new TextChooser.DataBackHandler(txtPCustomCode_DataBacked);
            //按维修项目查询设置项目编码
            txtProjectCode.SetBindTable("v_workhours_users", "project_num");
            txtProjectCode.GetDataSourced += new ServiceStationClient.ComponentUI.TextBox.TextChooser.GetDataSourceHandler(tc_GetDataSourced);
            //按维修用料查询设置车牌号速查
            txtVehicle_no.SetBindTable("tb_vehicle", "license_plate");
            txtVehicle_no.GetDataSourced += new ServiceStationClient.ComponentUI.TextBox.TextChooser.GetDataSourceHandler(tc_GetDataSourced);
            //按维修用料查询设置客户编码速查
            txtPartCustomCode.SetBindTable("tb_customer", "cust_code");
             txtPartCustomCode.GetDataSourced += new ServiceStationClient.ComponentUI.TextBox.TextChooser.GetDataSourceHandler(tc_GetDataSourced);
            //按维修用料查询设置配件编码速查
             txtPartCode.SetBindTable("tb_parts", "ser_parts_code");
             txtPartCode.GetDataSourced += new ServiceStationClient.ComponentUI.TextBox.TextChooser.GetDataSourceHandler(tc_GetDataSourced);   
            //按维修用料查询设置领料人速查
             txtLName.SetBindTable("v_user", "user_name");
             txtLName.GetDataSourced += new ServiceStationClient.ComponentUI.TextBox.TextChooser.GetDataSourceHandler(tc_GetDataSourced);   
        }
      
        /// <summary>
        /// 按维修项目查询设置客户编码速查
        /// </summary>
        /// <param name="dr"></param>
        void txtPCustomCode_DataBacked(DataRow dr)
        {
            txtPCustomCode.Text = CommonCtrl.IsNullToString(dr["cust_code"]);
            txtPCustomCode.Tag = CommonCtrl.IsNullToString(dr["cust_id"]);
            txtPCustomName.Caption = CommonCtrl.IsNullToString(dr["cust_name"]);
        }
        /// <summary>
        /// 客户编码速查连带信息
        /// </summary>
        /// <param name="dr"></param>
        void txtCustomNO_DataBacked(DataRow dr)
        {
            txtCustomNO.Text = CommonCtrl.IsNullToString(dr["cust_code"]);
            txtCustomNO.Tag = CommonCtrl.IsNullToString(dr["cust_id"]);
            txtCustomName.Caption = CommonCtrl.IsNullToString(dr["cust_name"]);
        }
        /// <summary>
        /// 设置速查
        /// </summary>
        /// <param name="sqlString"></param>
        void tc_GetDataSourced(TextChooser tc, string sqlString)
        {
             DataTable dvt = CommonFuncCall.GetDataSource(sqlString);
             tc.SetDataSource(dvt);
            if (dvt != null)
            {
                tc.Search();
            }
        }     
        #endregion       

        #region 基础数据设置
        private void SetUCStyle()
        {
            palPBottom.Height = 275;           
            UIAssistants.SetButtonStyle4QueryAndClear(btnQuery, btnClear);  //设置查询按钮和清除按钮样式
            UIAssistants.SetButtonStyle4QueryAndClear(btnPSelect, btnPClear);  //设置查询按钮和清除按钮样式
            UIAssistants.SetButtonStyle4QueryAndClear(buttonEx3, btnLClear);  //设置查询按钮和清除按钮样式
            CommonCtrl.BindComboBoxByDictionarr(cobPayType, "sys_repair_pay_methods", true);//绑定维修付费方式  
            CommonCtrl.BindComboBoxByDictionarr(cobRepType, "sys_repair_category", true);//按维修单查询-绑定维修类别  
            CommonCtrl.BindComboBoxByDictionarr(cobPReType, "sys_repair_category", true);//按维修项目查询-绑定维修类别 
            CommonCtrl.BindComboBoxByDictionarr(cobPartRepairType, "sys_repair_category", true);//按维修用料查询-绑定维修类别  
            //CommonFuncCall.BindComBoxDataSource(cobBalanceType, "sys_closing_way", "全部"); //按维修单查询-结算方式
            //CommonFuncCall.BindComBoxDataSource(cobPBalanceType, "sys_closing_way", "全部");//按维修项目查询-结算方式
            //CommonFuncCall.BindComBoxDataSource(cobPartBlanceType, "sys_closing_way", "全部");//按维修用料查询-结算方式
            BindBalanceWay(cobBalanceType);
            BindBalanceWay(cobPBalanceType);
            BindBalanceWay(cobPartBlanceType);
            BindJBPerson(cboHandle);
            BindJBPerson(cobPHandle);
        }
        #endregion

        #region 绑定结算方式
        /// <summary>
        /// 绑定结算方式
        /// </summary>
        private void BindBalanceWay(ComboBoxEx comEx)
        {
            DataTable dt = DBHelper.GetTable("", "tb_balance_way", "balance_way_id,balance_way_name,default_account", "", "", "");
            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem("", "全部"));
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new ListItem(dr["balance_way_id"], dr["balance_way_name"].ToString()));
            }
            comEx.DataSource = list;
            comEx.ValueMember = "Value";
            comEx.DisplayMember = "Text";
        }
        #endregion

        #region 绑定经办人
        private void BindJBPerson(ComboBoxEx comEx)
        {
            DataTable dt = DBHelper.GetTable("", "sys_user", "user_id,user_name", "", "", "");
            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem("", "全部"));
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new ListItem(dr["user_id"], dr["user_name"].ToString()));
            }
            comEx.DataSource = list;
            comEx.ValueMember = "Value";
            comEx.DisplayMember = "Text";
        }
        #endregion

        #region 顶部button显示设置
        /// <summary>
        ///  顶部button显示设置
        /// </summary>
        private void SetTopbuttonShow()
        {
            base.btnAdd.Visible = false;
            base.btnCopy.Visible = false;
            base.btnDelete.Visible = false;
            base.btnEdit.Visible = false;
            base.btnSave.Visible = false;
            base.btnCancel.Visible = false;
            base.btnImport.Visible = false;
            base.btnStatus.Visible = false;
            base.btnSubmit.Visible = false;
            base.btnVerify.Visible = false;
            base.btnSync.Visible = false;
            base.btnConfirm.Visible = false;
            base.btnActivation.Visible = false;
        }
        #endregion

        #region 预览事件

        void RepairQueryManager_ViewEvent(object sender, EventArgs e)
        {
            // DataView();
        }

        #region 根据选型卡切换判断是哪个选选项卡而执行预览
        /// <summary>
        /// 根据选型卡切换判断是哪个选选项卡而执行预览
        /// </summary>
        private void DataView()
        {
            string strTabTag = tcQuery.SelectedIndex.ToString();//0为按维修单查询，1为按维修项目查询，2按维修用料查询
            if (strTabTag == "0")
            {
                if (!IsCheck(dgvQData, strTabTag))
                {
                    return;
                }
            }
            else if (strTabTag == "1")
            {
                if (!IsCheck(dgvProjectData, strTabTag))
                {
                    return;
                }
            }
            else if (strTabTag == "2")
            {
                if (!IsCheck(dgvYlData, strTabTag))
                {
                    return;
                }
            }
            RepairQueryView Qview = new RepairQueryView(strRepairId);
            base.addUserControl(Qview, "维修调度单-预览", "RepairQueryView" + strRepairId, this.Tag.ToString(), this.Name);
        }
        #endregion
        /// <summary>
        /// 验证是否选择预览数据
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="strTabtag"></param>
        /// <returns></returns>
        private bool IsCheck(DataGridView dgv, string strTabtag)
        {
            string strColCheck = string.Empty;//选择框的Name值
            string strIdName = string.Empty;//datagridview的Id-name值
            bool isOk = false;
            strColCheck = strTabtag == "0" ? "colCheck" : strTabtag == "1" ? "PcolCheck" : strTabtag == "2" ? "YLcolCheck" : "";
            strIdName = strTabtag == "0" ? "maintain_id" : strTabtag == "1" ? "Pmaintain_id" : strTabtag == "2" ? "Ymaintain_id" : "";
            if (!string.IsNullOrEmpty(strColCheck) && !string.IsNullOrEmpty(strIdName))
            {

                List<string> listField = new List<string>();
                foreach (DataGridViewRow dr in dgv.Rows)
                {
                    object isCheck = dr.Cells[strColCheck].EditedFormattedValue;
                    if (isCheck != null && (bool)isCheck)
                    {
                        listField.Add(dr.Cells[strIdName].Value.ToString());
                        strRepairId = dr.Cells[strIdName].Value.ToString();
                    }
                }
                if (listField.Count == 0)
                {
                    MessageBoxEx.Show("请选择预览记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (listField.Count > 1)
                {
                    MessageBoxEx.Show(" 一次仅能预览1条记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (listField.Count == 1)
                {
                    isOk = true;
                }
            }
            return isOk;
        }
        #endregion

        #region  按维修单查询功能方法

        #region 按维修单查询清空事件
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtAdvisor.Caption = string.Empty;
            cboHandle.SelectedValue = string.Empty;
            txtCarNO.Text = string.Empty;
            txtCarType.Text = string.Empty;
            txtCustomName.Caption = string.Empty;
            txtCustomNO.Text = string.Empty;
            cboOrgId.SelectedValue = string.Empty;
            txtDriver.Caption = string.Empty;
            txtDriverPhone.Caption = string.Empty;
            txtEngineNo.Caption = string.Empty;
            txtOrder.Caption = string.Empty;
            txtVIN.Caption = string.Empty;
            cobBalanceType.SelectedValue = string.Empty;
            cobOrderStatus.SelectedValue = string.Empty;
            cobPayType.SelectedValue = string.Empty;
            cobRepType.SelectedValue = string.Empty;
            dtpBalanceSTime.Value = DateTime.Now.AddMonths(-1);
            dtpBalanceETime.Value = DateTime.Now;
            dtpReserveSTime.Value = DateTime.Now.AddMonths(-1);
            dtpReserveETime.Value = DateTime.Now;

        }
        #endregion

        #region 按维修单查询事件
        private void btnQuery_Click(object sender, EventArgs e)
        {
            _BindPageData();
        }
        #endregion

        #region 按维修单查询分页查询绑定数据
        /// <summary>
        /// 分页查询绑定数据
        /// </summary>
        public void _BindPageData()
        {
            try
            {
                #region 事件选择判断
                if (dtpReserveSTime.Value > dtpReserveETime.Value)
                {
                    MessageBoxEx.Show("接待日期,开始时间不能大于结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dtpBalanceSTime.Value > dtpBalanceETime.Value)
                {
                    MessageBoxEx.Show("结算日期,开始时间不能大于结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion
                string strTable = "tb_maintain_info a left join tb_maintain_settlement_info b on a.maintain_id=b.maintain_id";
                string strFiles = @" a.maintain_no,a.reception_time,a.vehicle_no,a.vehicle_model,a.customer_name,a.customer_code,a.dispatch_status
                                    ,a.driver_name,a.driver_mobile,a.maintain_man,a.responsible_name,a.vehicle_vin,a.engine_no,b.man_hour_sum
                                    ,b.fitting_sum,b.other_item_sum,b.privilege_cost,b.should_sum,b.received_sum,b.debt_cost,a.travel_mileage
                                    ,a.oil_into_factory,a.fault_describe,a.maintain_type,a.maintain_payment,b.payment_terms,a.remark,a.maintain_id,a.orders_source,a.info_status";
                string strWhere = string.Format(" a.enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'");//enable_flag 1未删除
                if (!string.IsNullOrEmpty(txtCarNO.Text.Trim()))//车牌号
                {
                    strWhere += string.Format(" and  a.vehicle_no like '%{0}%'", txtCarNO.Text.Trim());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtCarType.Tag)))//车型
                {
                    strWhere += string.Format(" and  a.vehicle_model like '%{0}%'", txtCarType.Tag);
                }
                if (!string.IsNullOrEmpty(txtDriver.Caption))//司机
                {
                    strWhere += string.Format(" and  a.driver_name like '%{0}%'", txtDriver.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtDriverPhone.Caption.Trim()))//司机手机
                {
                    strWhere += string.Format(" and  a.driver_mobile like '%{0}%'", txtDriverPhone.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtCustomNO.Text.Trim()))//客户编码
                {
                    strWhere += string.Format(" and  a.customer_code like '%{0}%'", txtCustomNO.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtCustomName.Caption.Trim()))//客户名称
                {
                    strWhere += string.Format(" and  a.customer_name like '%{0}%'", txtCustomName.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtAdvisor.Caption.Trim()))//服务顾问
                {
                    strWhere += string.Format(" and  a.maintain_man like '%{0}%'", txtAdvisor.Caption.Trim());
                }
                //if (!string.IsNullOrEmpty(cobZJY.SelectedValue.ToString()))//质检员
                //{
                //    strWhere += string.Format(" and  a.maintain_man like '%{0}%'", cobZJY.SelectedValue.ToString());
                //} 
                if (!string.IsNullOrEmpty(txtEngineNo.Caption.Trim()))//发动机号
                {
                    strWhere += string.Format(" and  a.engine_no like '%{0}%'", txtEngineNo.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtVIN.Caption.Trim()))//VIN 
                {
                    strWhere += string.Format(" and  a.vehicle_vin like '%{0}%'", txtVIN.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cboHandle.SelectedValue)))//经办人 
                {
                    strWhere += string.Format(" and  a.responsible_opid like '%{0}%'", cboHandle.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobOrderStatus.SelectedValue.ToString())))//单据状体（具体状态目前不详）
                {
                    strWhere += string.Format(" and a.dispatch_status = '{0}'", cobOrderStatus.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(txtOrder.Caption.Trim()))//维修单号
                {
                    strWhere += string.Format(" and  a.maintain_no like '%{0}%'", txtOrder.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(cobPayType.SelectedValue.ToString()))//维修付费方式
                {
                    strWhere += string.Format(" and  a.maintain_payment like '%{0}%'", cobPayType.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobRepType.SelectedValue)))//维修类别
                {
                    strWhere += string.Format(" and  a.maintain_type like '%{0}%'", cobRepType.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(cobBalanceType.SelectedValue.ToString()))//结算方式
                {
                    strWhere += string.Format(" and  b.payment_terms like '%{0}%'", cobBalanceType.SelectedValue.ToString());
                }
                strWhere += string.Format(" and (a.reception_time BETWEEN {0} and {1}", Common.LocalDateTimeToUtcLong(dtpReserveSTime.Value.Date), Common.LocalDateTimeToUtcLong(dtpReserveETime.Value.Date.AddDays(1).AddMilliseconds(-1)));//接待日期
                strWhere += string.Format(" or b.create_time BETWEEN {0} and {1})", Common.LocalDateTimeToUtcLong(dtpReserveSTime.Value.Date), Common.LocalDateTimeToUtcLong(dtpReserveETime.Value.Date.AddDays(1).AddMilliseconds(-1)));//结算日期

                int recordCount;
                DataTable dt = DBHelper.GetTableByPage("分页查询维修单管理", strTable, strFiles, strWhere, "", " order by a.create_time desc", pageQ.PageIndex, pageQ.PageSize, out recordCount);
                dgvQData.DataSource = dt;
                pageQ.RecordCount = recordCount;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pageQ_PageIndexChanged(object sender, EventArgs e)
        {
            _BindPageData();
        }

        #endregion

        #region  按维修单查询车牌号选择器事件
        /// <summary>
        ///  按维修单查询车牌号选择器事件
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
                txtVIN.Caption = frmVehicle.strVIN;
                txtEngineNo.Caption = frmVehicle.strEngineNum;
                txtCarType.Text = GetDicName(frmVehicle.strModel);
                txtCarType.Tag = frmVehicle.strModel;
            }
        }
        #endregion

        #region  按维修单查询车型选择器事件
        /// <summary>
        ///  按维修单查询车型选择器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCarType_ChooserClick(object sender, EventArgs e)
        {
            frmVehicleModels frmModels = new frmVehicleModels();
            DialogResult result = frmModels.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtCarType.Text = frmModels.VMName;
                txtCarType.Tag = frmModels.VMID;
            }
        }
        #endregion

        #region  按维修单查询客户编码选择器事件
        /// <summary>
        ///  按维修单查询客户编码选择器事件
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

        #region 按维修单查询双击进入详情窗体
        private void dgvQData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DoubleClickCell(dgvQData, "maintain_id");
        }
        #endregion

        #endregion

        #region 绑定单据状态
        /// 绑定单据状态
        /// </summary>
        private void BindOrderStatus()
        {
            //按维修项目查询中
            cobPOrderType.DataSource = DataSources.EnumToList(typeof(DataSources.EnumDispatchStatus), true);
            cobPOrderType.ValueMember = "Value";
            cobPOrderType.DisplayMember = "Text";
            //按维修用料查询中
            cobStatus.DataSource = DataSources.EnumToList(typeof(DataSources.EnumDispatchStatus), true);
            cobStatus.ValueMember = "Value";
            cobStatus.DisplayMember = "Text";
            //按维修单查询中
            cobOrderStatus.DataSource = DataSources.EnumToList(typeof(DataSources.EnumDispatchStatus), true);
            cobOrderStatus.ValueMember = "Value";
            cobOrderStatus.DisplayMember = "Text";
        }
        #endregion

        #region datagridview属性设置
        private void SetDgvAnchor()
        {
            #region 按维修单查询
            palQBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                       | System.Windows.Forms.AnchorStyles.Left)
                       | System.Windows.Forms.AnchorStyles.Right)));
            dgvQData.Dock = DockStyle.Fill;
            //dgvQData.Columns["colCheck"].HeaderText = "选择";
            dgvQData.ReadOnly = false;
            foreach (DataGridViewColumn dgvc in dgvQData.Columns)
            {
                if (dgvc == colCheck)
                {
                    //dgvc.HeaderText = "选择";
                    continue;
                }
                dgvc.ReadOnly = true;
            }
            #endregion

            #region 按维修项目查询
            palPBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dgvProjectData.Dock = DockStyle.Fill;
            //dgvProjectData.Columns["PcolCheck"].HeaderText = "选择";
            dgvProjectData.ReadOnly = false;
            foreach (DataGridViewColumn dgvc in dgvProjectData.Columns)
            {
                if (dgvc == PcolCheck)
                {
                    //dgvc.HeaderText = "选择";
                    continue;
                }
                dgvc.ReadOnly = true;
            }
            #endregion

            #region 按维修用料查询
            palYbottom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                       | System.Windows.Forms.AnchorStyles.Left)
                       | System.Windows.Forms.AnchorStyles.Right)));
            dgvYlData.Dock = DockStyle.Fill;
            //dgvYlData.Columns["YLcolCheck"].HeaderText = "选择";
            dgvYlData.ReadOnly = false;
            foreach (DataGridViewColumn dgvc in dgvYlData.Columns)
            {
                if (dgvc == YLcolCheck)
                {
                    //dgvc.HeaderText = "选择";
                    continue;
                }
                dgvc.ReadOnly = true;
            }
            #endregion

        }
        #endregion       

        #region 窗体Load事件
        private void RepairQueryManager_Load(object sender, EventArgs e)
        {
            if (this.dt != null)
            {
                DataTable _dt = DBHelper.GetTableByPage(this.dt, pageQ.PageIndex, pageQ.PageSize);
                this.dgvQData.DataSource = _dt;
                pageQ.RecordCount = _dt.Rows.Count;
                return;
            }
            this.SetTopbuttonShow();
            #region 按维修单查询
            dtpBalanceSTime.Value = DateTime.Now.AddMonths(-1);
            dtpBalanceETime.Value = DateTime.Now;
            dtpReserveSTime.Value = DateTime.Now.AddMonths(-1);
            dtpReserveETime.Value = DateTime.Now;
            _BindPageData();//按维修单查询
            #endregion

            #region 按维修项目查询
            dtpPReserveSTime.Value = DateTime.Now.AddMonths(-1);
            dtpPReserveETime.Value = DateTime.Now;
            dtpPBalanceSTime.Value = DateTime.Now.AddMonths(-1);
            dtpPBalanceETime.Value = DateTime.Now;
            BindProjectPageData();//按维修项目查询
            #endregion
        }
        #endregion

        #region 按维修项目查询功能方法

        #region 按维修项目查询-清空事件
        private void btnPClear_Click(object sender, EventArgs e)
        {
            txtProjectCode.Text = string.Empty;
            txtProjectCode.Tag = string.Empty;
            txtProjectName.Caption = string.Empty;
            txtPRepairType.Caption = string.Empty;
            txtPartCustomCode.Text = string.Empty;
            txtPartCustomCode.Tag = string.Empty;
            txtPCustomName.Caption = string.Empty;
            txtPartCode.Text = string.Empty;
            txtPartCode.Tag = string.Empty;
            cobPReType.SelectedValue = string.Empty;
            txtPRepairCode.Caption = string.Empty;
            txtPRepairName.Caption = string.Empty;
            cobPHandle.SelectedValue = string.Empty;
            cobPBalanceType.SelectedValue = string.Empty;
            dtpPReserveSTime.Value = DateTime.Now.AddMonths(-1);
            dtpPReserveETime.Value = DateTime.Now;
            dtpPBalanceSTime.Value = DateTime.Now.AddMonths(-1);
            dtpPBalanceETime.Value = DateTime.Now;
            txtPCarCode.Text = string.Empty;
            txtPCarCode.Tag = string.Empty;
            txtPCustomCode.Text = string.Empty;
            txtPCustomCode.Tag = string.Empty;
            cobPOrderType.SelectedValue = string.Empty;
            
        }
        #endregion

        #region 按维修项目查询按钮事件
        private void btnPSelect_Click(object sender, EventArgs e)
        {
            BindProjectPageData();
        }
        #endregion

        #region 按维修项目查询-绑定分页查询
        public void BindProjectPageData()
        {
            try
            {
                #region 事件选择判断
                if (dtpPReserveSTime.Value > dtpPReserveETime.Value)
                {
                    MessageBoxEx.Show("接待日期,开始时间不能大于结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dtpPBalanceSTime.Value > dtpPBalanceETime.Value)
                {
                    MessageBoxEx.Show("结算日期,开始时间不能大于结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion
                string strTable = @"tb_maintain_info a
                                    left join tb_maintain_item b on a.maintain_id=b.maintain_id 
                                    right join tb_maintain_settlement_info c  on a.maintain_id=c.maintain_id ";
                string strFiles = @" a.maintain_no,a.reception_time,c.create_time,a.vehicle_no,a.customer_code,a.customer_name
                                    ,a.enable_flag,b.item_no,b.item_type,b.item_name,b.man_hour_type,b.man_hour_quantity
                                    ,b.sum_money_goods,b.three_warranty,a.dispatch_status,a.org_name,a.responsible_name
                                    ,c.payment_terms,a.maintain_payment,a.maintain_type,a.remark,a.maintain_id,a.orders_source,a.info_status ";
                string strWhere = string.Format("a.enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'");//enable_flag 1未删除
                if (!string.IsNullOrEmpty(txtProjectCode.Text.Trim()))//项目编码
                {
                    strWhere += string.Format(" and  b.item_no like '%{0}%'", txtProjectCode.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtProjectName.Caption.Trim()))//项目名称
                {
                    strWhere += string.Format(" and  b.item_name like '%{0}%'", txtProjectName.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtPRepairType.Caption)))//项目类别
                {
                    strWhere += string.Format(" and  b.item_type like '%{0}%'", txtPRepairType.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtPCustomCode.Text.Trim()))//客户编码
                {
                    strWhere += string.Format(" and  a.customer_code like '%{0}%'", txtPCustomCode.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtPCustomName.Caption.Trim()))//客户名称
                {
                    strWhere += string.Format(" and  a.customer_name like '%{0}%'", txtPCustomName.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtPCarCode.Text.Trim()))//车牌号
                {
                    strWhere += string.Format(" and  a.vehicle_no like '%{0}%'", txtPCarCode.Text.Trim());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobPReType.SelectedValue)))//维修类别
                {
                    strWhere += string.Format(" and  a.maintain_payment like '%{0}%'", cobPReType.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(txtPRepairCode.Caption.Trim()))//维修单号
                {
                    strWhere += string.Format(" and  a.maintain_no like '%{0}%'", txtPRepairCode.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobPOrderType.SelectedValue)))//维修单状态
                {
                    if (cobPOrderType.SelectedValue.ToString() != "全部")
                    {
                        strWhere += string.Format(" and a.dispatch_status = '{0}'", cobPOrderType.SelectedValue.ToString());
                    }
                }
                //if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtPRepairName.Caption.Trim())))//维修人员
                //{
                //    strWhere += string.Format(" and  a.vehicle_model like '%{0}%'", txtPRepairName.Caption.Trim());
                //}
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobPHandle.SelectedValue)))//经办人 
                {
                    strWhere += string.Format(" and  a.responsible_opid like '%{0}%'", cobPHandle.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobPBalanceType.SelectedValue)))//结算方式
                {
                    strWhere += string.Format(" and  c.payment_terms like '%{0}%'", cobPBalanceType.SelectedValue.ToString());
                }

                strWhere += string.Format(" and (a.reception_time BETWEEN {0} and {1}", Common.LocalDateTimeToUtcLong(dtpPReserveSTime.Value.Date), Common.LocalDateTimeToUtcLong(dtpPReserveETime.Value.Date.AddDays(1).AddMilliseconds(-1)));//接待日期
                strWhere += string.Format(" or c.create_time BETWEEN {0} and {1})", Common.LocalDateTimeToUtcLong(dtpPBalanceSTime.Value.Date), Common.LocalDateTimeToUtcLong(dtpPBalanceETime.Value.Date.AddDays(1).AddMilliseconds(-1)));//结算日期

                int recordCount;
                DataTable dt = DBHelper.GetTableByPage("分页查询维修单管理", strTable, strFiles, strWhere, "", " order by a.create_time desc", pageP.PageIndex, pageP.PageSize, out recordCount);
                dgvProjectData.DataSource = dt;
                pageP.RecordCount = recordCount;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void pageP_PageIndexChanged(object sender, EventArgs e)
        {
            BindProjectPageData();
        }
        #endregion

        #region 按维修项目查询-项目编码选择器事件
        private void txtProjectCode_ChooserClick(object sender, EventArgs e)
        {
            frmWorkHours frmHours = new frmWorkHours();
            DialogResult result = frmHours.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtProjectCode.Text = frmHours.strProjectNum;
                txtProjectName.Caption = frmHours.strProjectName;
                txtPRepairType.Caption = GetDicName(frmHours.strRepairType);
            }
        }
        #endregion

        #region 按维修项目查询-客户编码选择器事件
        /// <summary>
        /// 按维修项目查询-客户编码选择器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPCustomCode_ChooserClick(object sender, EventArgs e)
        {
            frmCustomerInfo frmCInfo = new frmCustomerInfo();
            DialogResult result = frmCInfo.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtPCustomCode.Text = frmCInfo.strCustomerNo;
                txtPCustomCode.Tag = frmCInfo.strCustomerId;
                txtPCustomName.Caption = frmCInfo.strCustomerName;

            }
        }
        #endregion

        #region  按维修项目查询-车牌号选择器
        /// <summary>
        /// 车牌号选择器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPCarCode_ChooserClick(object sender, EventArgs e)
        {
            frmVehicleGrade frmVehicle = new frmVehicleGrade();
            DialogResult result = frmVehicle.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtPCarCode.Text = frmVehicle.strLicensePlate;
            }

        }
        #endregion

        #region 按维修项目查询-双击单元格进入预览
        private void dgvProjectData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DoubleClickCell(dgvProjectData, "Pmaintain_id");
        }
        #endregion
        #endregion

        #region 按维修维修用料查询功能方法

        #region 按维修维修用料查询-绑定分页查询
        public void BindYLPageData()
        {
            try
            {
                #region 事件选择判断
                if (dtpPartReserveSTime.Value > dtpPartReserveETime.Value)
                {
                    MessageBoxEx.Show("接待日期,开始时间不能大于结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dtpPartBalanceSTime.Value > dtpPartBalanceETime.Value)
                {
                    MessageBoxEx.Show("结算日期,开始时间不能大于结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion
                string strTable = @"tb_maintain_info a
                                    left join tb_maintain_material_detail b on a.maintain_id=b.maintain_id 
                                    right join tb_maintain_settlement_info c  on a.maintain_id=c.maintain_id ";
                string strFiles = @" a.maintain_no,a.reception_time,c.create_time,a.customer_name,a.linkman,b.parts_code,b.parts_name
                                     ,b.norms,b.unit,b.quantity,b.sum_money,b.three_warranty,a.dispatch_status,a.org_name,a.customer_code,a.vehicle_no
                                     ,a.responsible_name,c.payment_terms,a.maintain_payment,a.maintain_type,a.remark,a.maintain_id,a.orders_source,a.info_status";
                string strWhere = string.Format(" a.enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'");//enable_flag 1未删除
                if (!string.IsNullOrEmpty(txtPartCode.Text.Trim()))//配件编码
                {
                    strWhere += string.Format(" and  b.parts_code like '%{0}%'", txtPartCode.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtPartType.Caption.Trim()))//配件类别
                {
                    strWhere += string.Format(" and  b.parts_type like '%{0}%'", txtPartType.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtPartName.Caption.Trim()))//配件名称
                {
                    strWhere += string.Format(" and  b.parts_name like '%{0}%'", txtPartName.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtPartCustomCode.Text.Trim()))//客户编码
                {
                    strWhere += string.Format(" and  a.customer_code like '%{0}%'", txtPartCustomCode.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtVehicle_no.Text.Trim()))//车牌号
                {
                    strWhere += string.Format(" and  a.vehicle_no like '%{0}%'", txtVehicle_no.Text.Trim());
                }
                //if (!string.IsNullOrEmpty(txtLName.Text.Trim()))//领料人
                //{
                //    strWhere += string.Format(" and  a.vehicle_no like '%{0}%'", txtLName.Text.Trim());
                //}
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobStatus.SelectedValue)))//维修单状态
                {
                    strWhere += string.Format(" and a.dispatch_status = '{0}'", cobStatus.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(txtPartRepairCode.Caption.Trim()))//维修单号
                {
                    strWhere += string.Format(" and  a.maintain_no like '%{0}%'", txtPartRepairCode.Caption.Trim());
                }

                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobPartRepairType.SelectedValue)))//维修类别
                {
                    strWhere += string.Format(" and  a.maintain_payment like '%{0}%'", cobPartRepairType.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobPartBlanceType.SelectedValue)))//结算方式
                {
                    strWhere += string.Format(" and  c.payment_terms like '%{0}%'", cobPartBlanceType.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cboOrgId.SelectedValue)))//部门 
                {
                    strWhere += string.Format(" and  a.org_name like '%{0}%'", cboOrgId.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobYHandle.SelectedValue)))//经办人 
                {
                    strWhere += string.Format(" and  a.responsible_opid like '%{0}%'", cobYHandle.SelectedValue.ToString());
                }
                strWhere += string.Format(" and (a.reception_time BETWEEN {0} and {1}", Common.LocalDateTimeToUtcLong(dtpPReserveSTime.Value.Date), Common.LocalDateTimeToUtcLong(dtpPReserveETime.Value.Date.AddDays(1).AddMilliseconds(-1)));//接待日期
                strWhere += string.Format(" or c.create_time BETWEEN {0} and {1})", Common.LocalDateTimeToUtcLong(dtpPBalanceSTime.Value.Date), Common.LocalDateTimeToUtcLong(dtpPBalanceETime.Value.Date.AddDays(1).AddMilliseconds(-1)));//结算日期

                int recordCount;
                DataTable dt = DBHelper.GetTableByPage("分页查询维修单管理", strTable, strFiles, strWhere, "", " order by a.create_time desc", pageL.PageIndex, pageL.PageSize, out recordCount);
                dgvYlData.DataSource = dt;
                pageL.RecordCount = recordCount;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void pageL_PageIndexChanged(object sender, EventArgs e)
        {
            BindYLPageData();
        }
        #endregion

        #region 按维修维修用料查询-配件编码选择器
        private void txtPartCode_ChooserClick(object sender, EventArgs e)
        {
            frmParts frmPart = new frmParts();
            DialogResult result = frmPart.ShowDialog();
            if (result == DialogResult.OK)
            {
                string strPId = frmPart.PartsID;
                DataTable dpt = DBHelper.GetTable("", "v_parts", "*", " parts_id='" + strPId + "'", "", "");
                if (dpt.Rows.Count > 0)
                {
                    DataRow dpr = dpt.Rows[0];
                    txtPartCode.Text = CommonCtrl.IsNullToString(dpr["ser_parts_code"]);
                    txtPartType.Caption = GetDicName(CommonCtrl.IsNullToString(dpr["parts_type"]));
                    txtPartName.Caption = CommonCtrl.IsNullToString(dpr["parts_name"]);
                }
            }
        }
        #endregion

        #region 按维修维修用料查询--客户编码选择器事件
        /// <summary>
        /// 按维修项目查询-客户编码选择器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPartCustomCode_ChooserClick(object sender, EventArgs e)
        {
            frmCustomerInfo frmCInfo = new frmCustomerInfo();
            DialogResult result = frmCInfo.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtPartCustomCode.Text = frmCInfo.strCustomerNo;
                txtPartCustomCode.Tag = frmCInfo.strCustomerId;

            }
        }
        #endregion

        #region  按维修用料查询-车牌号选择器
        /// <summary>
        /// 车牌号选择器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtVehicle_no_ChooserClick(object sender, EventArgs e)
        {
            frmVehicleGrade frmVehicle = new frmVehicleGrade();
            DialogResult result = frmVehicle.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtVehicle_no.Text = frmVehicle.strLicensePlate;
            }

        }
        #endregion

        #region  按维修用料查询-领料人选择器
        private void txtLName_ChooserClick(object sender, EventArgs e)
        {
            frmPersonnelSelector frmPInfo = new frmPersonnelSelector();
            DialogResult result = frmPInfo.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtLName.Text = frmPInfo.strPersonName;
                txtLName.Tag = frmPInfo.strUserId;
            }
        }
        #endregion

        #region  按维修用料查询-经办人选择器
        private void txtPartAtnn_ChooserClick(object sender, EventArgs e)
        {

        }
        #endregion

        #region 按维修用料查询按钮事件
        private void buttonEx3_Click(object sender, EventArgs e)
        {
            BindYLPageData();
        }
        #endregion

        #region 按维修用料查询-清空事件
        private void btnLClear_Click(object sender, EventArgs e)
        {
            txtPartCode.Text = string.Empty;
            txtPartType.Caption = string.Empty;
            txtPartName.Caption = string.Empty;
            txtPartCustomCode.Text = string.Empty;
            txtVehicle_no.Text = string.Empty;
            txtLName.Text = string.Empty;
            cobStatus.SelectedValue = string.Empty;
            txtPartRepairCode.Caption = string.Empty;
            cobPartRepairType.SelectedValue = string.Empty;
            cobPartBlanceType.SelectedValue = string.Empty;
            cboOrgId.SelectedValue = string.Empty;
            cobYHandle.SelectedValue = string.Empty;
            dtpPartReserveSTime.Value = DateTime.Now.AddMonths(-1);
            dtpPartReserveETime.Value = DateTime.Now;
            dtpPartBalanceSTime.Value = DateTime.Now.AddMonths(-1);
            dtpPartBalanceETime.Value = DateTime.Now;
        }
        #endregion

        #region 按维修用料查询-双击单元格进入预览
        private void dgvYlData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DoubleClickCell(dgvYlData, "Ymaintain_id");
        }
        #endregion
        #endregion

        #region 双击datagridview单元格进入浏览窗体
        /// <summary>
        /// 双击datagridview单元格进入浏览窗体
        /// </summary>
        /// <param name="dgvView">双击的datagridview</param>
        /// <param name="strIdName">datagridview中数据Id的name</param>
        private void DoubleClickCell(DataGridView dgvView, string strIdName)
        {
            if (dgvView.CurrentRow == null)
            {
                return;
            }
            strRepairId = dgvView.CurrentRow.Cells[strIdName].Value.ToString();
            RepairQueryView view = new RepairQueryView(strRepairId);
            base.addUserControl(view, "维修单查询-预览", "RepairQueryView" + strRepairId, this.Tag.ToString(), this.Name);
        }
        #endregion


        #region 按维修用料查询-绑定部门
        /// <summary>
        /// 按维修用料查询-根据部门的选择绑定经办人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboOrgId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboOrgId.SelectedValue == null)
            {
                return;
            }
            CommonCtrl.CmbBindUser(this.cobYHandle, cboOrgId.SelectedValue.ToString());
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

        #region 按维修单查询重写时间、状体、车型等
        private void dgvQData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
            string fieldNmae = dgvQData.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("reception_time") || fieldNmae.Equals("create_time"))
            {
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(e.Value)))
                {
                    Int64 ticks = Convert.ToInt64(e.Value);
                    e.Value = Common.UtcLongToLocalDateTime(ticks);
                }
            }
            if (fieldNmae.Equals("dispatch_status"))
            {
                if (CommonCtrl.IsNullToString(dgvQData.Rows[e.RowIndex].Cells["orders_source"].Value) == "3")
                {
                    e.Value = "维修单结算" + DataSources.GetDescription(typeof(DataSources.EnumAuditStatus), int.Parse(CommonCtrl.IsNullToString(dgvQData.Rows[e.RowIndex].Cells["info_status"].Value)));
                }
                else
                {
                    if (CommonCtrl.IsNullToString(dgvQData.Rows[e.RowIndex].Cells["info_status"].Value) != Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString())
                    {
                        e.Value = "接待" + DataSources.GetDescription(typeof(DataSources.EnumAuditStatus), int.Parse(CommonCtrl.IsNullToString(dgvQData.Rows[e.RowIndex].Cells["info_status"].Value)));
                    }
                    else
                    {
                        e.Value = DataSources.GetDescription(typeof(DataSources.EnumDispatchStatus), int.Parse(e.Value.ToString()));
                    }
                }

            }
            if (fieldNmae.Equals("maintain_type") || fieldNmae.Equals("maintain_payment"))
            {
                e.Value = GetDicName(e.Value.ToString());
            }
            if (fieldNmae.Equals("responsible_name"))
            {
                e.Value = GetUserSetName(e.Value.ToString());
            }
            if (fieldNmae.Equals("payment_terms"))
            {
                e.Value = GetBlanceName(e.Value.ToString());
            }
        }
        #endregion

        #region 按维修项目查询重写时间、状态等
        private void dgvProjectData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
            string fieldNmae = dgvProjectData.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("reception_time") || fieldNmae.Equals("create_time"))
            {
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(e.Value)))
                {
                    Int64 ticks = Convert.ToInt64(e.Value);
                    e.Value = Common.UtcLongToLocalDateTime(ticks);
                }
            }
            if (fieldNmae.Equals("dispatch_status"))
            {
                if (CommonCtrl.IsNullToString(dgvProjectData.Rows[e.RowIndex].Cells["Porders_source"].Value) == "3")
                {
                    e.Value = "维修单结算" + DataSources.GetDescription(typeof(DataSources.EnumAuditStatus), int.Parse(CommonCtrl.IsNullToString(dgvProjectData.Rows[e.RowIndex].Cells["Pinfo_status"].Value)));
                }
                else
                {
                    if (CommonCtrl.IsNullToString(dgvProjectData.Rows[e.RowIndex].Cells["Pinfo_status"].Value) != Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString())
                    {
                        e.Value = "接待" + DataSources.GetDescription(typeof(DataSources.EnumAuditStatus), int.Parse(CommonCtrl.IsNullToString(dgvProjectData.Rows[e.RowIndex].Cells["Pinfo_status"].Value)));
                    }
                    else
                    {
                        e.Value = DataSources.GetDescription(typeof(DataSources.EnumDispatchStatus), int.Parse(e.Value.ToString()));
                    }
                }

            }
            if (fieldNmae.Equals("maintain_type") || fieldNmae.Equals("maintain_payment") || fieldNmae.Equals("item_type"))
            {
                e.Value = GetDicName(e.Value.ToString());
            }
            if (fieldNmae.Equals("org_name"))
            {
                e.Value = GetDepartmentName(e.Value.ToString());
            }
            if (fieldNmae.Equals("responsible_opid"))
            {
                e.Value = GetUserSetName(e.Value.ToString());
            }
            if (fieldNmae.Equals("payment_terms"))
            {
                e.Value = GetBlanceName(e.Value.ToString());
            }
        }
        #endregion

        #region 获取部门名称
        private string GetDepartmentName(string strDId)
        {
            return DBHelper.GetSingleValue("获得部门名称", "tb_organization", "org_name", "org_id='" + strDId + "'", "");
        }
        #endregion

        #region 获得人员名称
        private string GetUserSetName(string strPid)
        {
            return DBHelper.GetSingleValue("获得人员名称", "tb_customer", "cust_name", "cust_id='" + strPid + "'", "");
        }
        #endregion

        #region 按维修用料查询重写时间、状体等
        private void dgvYlData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
            string fieldNmae = dgvYlData.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("reception_time") || fieldNmae.Equals("create_time"))
            {
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(e.Value)))
                {
                    Int64 ticks = Convert.ToInt64(e.Value);
                    e.Value = Common.UtcLongToLocalDateTime(ticks);
                }
            }
            if (fieldNmae.Equals("dispatch_status"))
            {
                if (CommonCtrl.IsNullToString(dgvYlData.Rows[e.RowIndex].Cells["Yorders_source"].Value) == "3")
                {
                    e.Value = "维修单结算" + DataSources.GetDescription(typeof(DataSources.EnumAuditStatus), int.Parse(CommonCtrl.IsNullToString(dgvYlData.Rows[e.RowIndex].Cells["Yinfo_status"].Value)));
                }
                else
                {
                    if (CommonCtrl.IsNullToString(dgvYlData.Rows[e.RowIndex].Cells["Yinfo_status"].Value) != Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString())
                    {
                        e.Value = "接待" + DataSources.GetDescription(typeof(DataSources.EnumAuditStatus), int.Parse(CommonCtrl.IsNullToString(dgvYlData.Rows[e.RowIndex].Cells["Yinfo_status"].Value)));
                    }
                    else
                    {
                        e.Value = DataSources.GetDescription(typeof(DataSources.EnumDispatchStatus), int.Parse(e.Value.ToString()));
                    }
                }

            }
            if (fieldNmae.Equals("maintain_type") || fieldNmae.Equals("maintain_payment") || fieldNmae.Equals("item_type"))
            {
                e.Value = GetDicName(e.Value.ToString());
            }
            if (fieldNmae.Equals("org_name"))
            {
                e.Value = GetDepartmentName(e.Value.ToString());
            }
            if (fieldNmae.Equals("responsible_opid"))
            {
                e.Value = GetUserSetName(e.Value.ToString());
            }
            if (fieldNmae.Equals("payment_terms"))
            {
                e.Value = GetBlanceName(e.Value.ToString());
            }
        }
        #endregion

        #region 获得结算方式名称
        private string GetBlanceName(string strPid)
        {
            return DBHelper.GetSingleValue("获得结算方式名称", "tb_balance_way", "balance_way_name", "balance_way_id='" + strPid + "'", "");
        }
        #endregion

        #region 单击时选中行前的复选框     
      
        private void dgvQData_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
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
            foreach (DataGridViewRow dgvr in dgvQData.Rows)
            {
                object check = dgvr.Cells[colCheck.Name].EditedFormattedValue;
                if (check != null && (bool)check)
                {
                    dgvr.Cells[colCheck.Name].Value = false;
                }
            }
            //选择当前行
            dgvQData.Rows[e.RowIndex].Cells[colCheck.Name].Value = true;
        }

        private void dgvProjectData_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
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
            foreach (DataGridViewRow dgvr in dgvProjectData.Rows)
            {
                object check = dgvr.Cells[PcolCheck.Name].EditedFormattedValue;
                if (check != null && (bool)check)
                {
                    dgvr.Cells[PcolCheck.Name].Value = false;
                }
            }
            //选择当前行
            dgvProjectData.Rows[e.RowIndex].Cells[PcolCheck.Name].Value = true;
        }

        private void dgvYlData_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
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
            foreach (DataGridViewRow dgvr in dgvYlData.Rows)
            {
                object check = dgvr.Cells[YLcolCheck.Name].EditedFormattedValue;
                if (check != null && (bool)check)
                {
                    dgvr.Cells[YLcolCheck.Name].Value = false;
                }
            }
            //选择当前行
            dgvYlData.Rows[e.RowIndex].Cells[YLcolCheck.Name].Value = true;
        }
        #endregion

    }
}
