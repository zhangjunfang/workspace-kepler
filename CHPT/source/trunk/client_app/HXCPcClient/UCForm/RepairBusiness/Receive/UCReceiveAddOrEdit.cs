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
using Utility.Common;
using ServiceStationClient.ComponentUI;
using SYSModel;
using ServiceStationClient.ComponentUI.TextBox;

namespace HXCPcClient.UCForm.RepairBusiness.Receive
{
    /// <summary>
    /// 维修管理-维修接待添加修改
    /// Author：JC
    /// AddTime：2014.10.08
    /// </summary>
    public partial class UCReceiveAddOrEdit : UCBase
    {
        #region 属性设置
        /// <summary>
        /// 父窗体
        /// </summary>
        public UCReceiveManager uc;
        /// <summary>
        /// 窗体状态
        /// </summary>
        public WindowStatus wStatus;
        /// <summary>
        /// 预约单ID
        /// </summary>
        public string strId = string.Empty;
        /// <summary>
        /// 操作名称
        /// </summary>
        string opName = string.Empty;
        /// <summary>
        /// 单据来源目前1为预约单2为返修单
        /// </summary>
        public string strOrdersSouse = string.Empty;
        /// <summary>
        /// 前置单据Id值
        /// </summary>
        public string strBefore_orderId = string.Empty;
        Dictionary<string, string> dicFileds;
        /// <summary>
        /// 单据状态
        /// </summary>
        string strStatus = string.Empty;
        /// <summary>
        /// 用于判断用料是否重复添加
        /// </summary>
        List<string> listMater = new List<string>();
        /// <summary>
        ///  用于判断项目是否重复添加
        /// </summary>
        List<string> listProject = new List<string>();
        /// <summary>
        /// 是否自动关闭
        /// </summary>
        bool isAutoClose = false;
        /// <summary>
        /// 会员参数设置Id
        /// </summary>
        string strSetInfoid = string.Empty;
        #endregion

        #region 初始化窗体
        public UCReceiveAddOrEdit()
        {
            InitializeComponent();
            GetRepairNo();
            initializeData();
            SetDgvAnchor();
            CommonFuncCall.BindDepartment(cboOrgId, GlobalStaticObj.CurrUserCom_Id, "请选择");
            SetTopbuttonShow();
            BindRepairMeal();
            txtAdvisor.Text = HXCPcClient.GlobalStaticObj.UserName;//默认服务顾问为当前用户
            base.CancelEvent += new ClickHandler(UCReceiveAddOrEdit_CancelEvent);
            base.SaveEvent += new ClickHandler(UCReceiveAddOrEdit_SaveEvent);
            base.SubmitEvent += new ClickHandler(UCReceiveAddOrEdit_SubmitEvent);
            base.ImportEvent += new ClickHandler(UCReceiveAddOrEdit_ImportEvent);
            SetQuick();
        }
        #endregion

        #region 设置速查功能
        /// <summary>
        /// 设置速查功能
        /// </summary>
        private void SetQuick()
        {

            //设置车牌号速查
            txtCarNO.SetBindTable("v_vehicle", "license_plate");
            txtCarNO.GetDataSourced += new ServiceStationClient.ComponentUI.TextBox.TextChooser.GetDataSourceHandler(tc_GetDataSourced);
            txtCarNO.DataBacked += new TextChooser.DataBackHandler(txtCarNO_DataBacked);
            //设置客户编码速查
            txtCustomNO.SetBindTable("tb_customer", "cust_code");
            txtCustomNO.GetDataSourced += new ServiceStationClient.ComponentUI.TextBox.TextChooser.GetDataSourceHandler(tc_GetDataSourced);
            txtCustomNO.DataBacked += new TextChooser.DataBackHandler(txtCustomNO_DataBacked);

        }
        /// <summary>
        /// 客户编码速查连带信息
        /// </summary>
        /// <param name="dr"></param>
        void txtCustomNO_DataBacked(DataRow dr)
        {
            try
            {
                txtCustomNO.Text = CommonCtrl.IsNullToString(dr["cust_code"]);
                txtCustomNO.Tag = CommonCtrl.IsNullToString(dr["cust_id"]);
                txtCustomName.Caption = CommonCtrl.IsNullToString(dr["cust_name"]);
                GetMemberInfo(CommonCtrl.IsNullToString(dr["cust_id"]));  
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
        }
        /// <summary>
        /// 车牌号速查连带信息
        /// </summary>
        /// <param name="dr"></param>
        void txtCarNO_DataBacked(DataRow dr)
        {
            try
            {
                txtVIN.Caption = CommonCtrl.IsNullToString(dr["vin"]);
                txtEngineNo.Caption = CommonCtrl.IsNullToString(dr["engine_num"]);
                txtCarType.Text = GetVmodel(CommonCtrl.IsNullToString(dr["v_model"]));
                txtCarType.Tag = CommonCtrl.IsNullToString(dr["v_model"]);
                cobCarBrand.SelectedValue = CommonCtrl.IsNullToString(dr["v_brand"]);
                //cobColor.SelectedValue = CommonCtrl.IsNullToString(dr["v_color"]);
                txtturner.Caption = CommonCtrl.IsNullToString(dr["turner"]);
                txtCustomNO.Text = CommonCtrl.IsNullToString(dr["cust_code"]);
                txtCustomNO.Tag = CommonCtrl.IsNullToString(dr["cust_id"]);
                txtCustomName.Caption = CommonCtrl.IsNullToString(dr["cust_name"]);
                txtContact.Caption = CommonCtrl.IsNullToString(dr["cont_name"]);
                txtContactPhone.Caption = CommonCtrl.IsNullToString(dr["phone"]);
                GetMemberInfo(CommonCtrl.IsNullToString(dr["cust_id"])); 
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
        }
        /// <summary>
        /// 设置速查
        /// </summary>
        /// <param name="sqlString"></param>
        void tc_GetDataSourced(TextChooser tc, string sqlString)
        {
            try
            {
                if (sqlString.Contains("v_vehicle"))
                {
                    string fileds = string.Format(" license_plate,vin,engine_num,v_model,v_brand,v_color,turner,cust_code,cust_id,cust_name,cont_name,{0} phone ", EncryptByDB.GetDesFieldValue("cont_phone"));
                    sqlString = sqlString.Replace("*", fileds);
                }
                DataTable dvt = CommonFuncCall.GetDataSource(sqlString);
                tc.SetDataSource(dvt);
                if (dvt != null)
                {
                    tc.Search();
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
        }

        #endregion

        #region 绑定维修套餐
        /// <summary>
        /// 绑定维修套餐
        /// </summary>
        private void BindRepairMeal()
        {
            try
            {
                string strWhere = "enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "' and valid_until>='" + Common.LocalDateTimeToUtcLong(DateTime.Now) + "' and period_validity <='" + Common.LocalDateTimeToUtcLong(DateTime.Now) + "' ";
                DataTable dt = DBHelper.GetTable("", "sys_b_set_repair_package_set", "repair_package_set_id,package_name", strWhere, "", "order by repair_package_set_id desc");
                List<ListItem> list = new List<ListItem>();
                list.Add(new ListItem("", "全部"));
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new ListItem(dr["repair_package_set_id"], dr["package_name"].ToString()));
                }
                cobCombo.DataSource = list;
                cobCombo.ValueMember = "Value";
                cobCombo.DisplayMember = "Text";
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
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
            base.btnEdit.Visible = false;
            base.btnDelete.Visible = false;
            base.btnStatus.Visible = false;
            base.btnVerify.Visible = false;
            base.btnSync.Visible = false;
            base.btnConfirm.Visible = false;
            base.btnActivation.Visible = false;
            base.btnExport.Visible = false;
        }
        #endregion

        #region 导入功能
        void UCReceiveAddOrEdit_ImportEvent(object sender, EventArgs e)
        {
            int x = Cursor.Position.X;
            int y = Cursor.Position.Y;
            ShowContextMenuStrip(x, y);
        }

        /// <summary> 点击导入按钮，显示导入方式的下拉选项
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        private void ShowContextMenuStrip(int X, int Y)
        {
            contextMenuM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            contextMenuM.Show();
            contextMenuM.Location = new System.Drawing.Point(X, Y);
        }
        #endregion

        #region 提交事件
        void UCReceiveAddOrEdit_SubmitEvent(object sender, EventArgs e)
        {
            SaveAndSubmit("提交", DataSources.EnumAuditStatus.SUBMIT);
        }
        #endregion

        #region 保存事件
        void UCReceiveAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            SaveAndSubmit("保存", DataSources.EnumAuditStatus.DRAFT);
        }
        #endregion

        #region 验证必填项
        private Boolean CheckControlValue()
        {
            try
            {
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(txtCarNO.Text.Trim()))
                {
                    Validator.SetError(errorProvider1, txtCarNO, "车牌号不能为空!");
                    return false;
                }
                if (string.IsNullOrEmpty(txtCustomNO.Text.Trim()))
                {
                    Validator.SetError(errorProvider1, txtCustomNO, "客户编码不能为空!");
                    return false;
                }
                if (string.IsNullOrEmpty(txtCustomName.Caption.Trim()))
                {
                    Validator.SetError(errorProvider1, txtCustomName, "客户名称不能为空!");
                    return false;
                }
                if (string.IsNullOrEmpty(txtDriver.Caption.Trim()))
                {
                    Validator.SetError(errorProvider1, txtDriver, "报修人不能为空!");
                    return false;
                }
                if (string.IsNullOrEmpty(txtDriverPhone.Caption.Trim()))//报修人手机
                {
                    Validator.SetError(errorProvider1, txtDriverPhone, "报修人手机不能为空!");
                    return false;
                }
                if (!string.IsNullOrEmpty(txtMl.Caption.Trim()))
                {
                    if (Convert.ToDecimal(txtMl.Caption.Trim()) > 100 || Convert.ToDecimal(txtMl.Caption.Trim()) < 1)
                    {
                        Validator.SetError(errorProvider1, txtMl, "进场油量仅能输入1-100的数字!");
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(txtAdvisor.Text.Trim()))
                {
                    Validator.SetError(errorProvider1, txtAdvisor, "服务顾问不能为空!");
                    return false;
                }
                if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(dtpSuTime.Value)))
                {
                    Validator.SetError(errorProvider1, dtpSuTime, "预计完工时间不能为空!");
                    return false;
                }
                if (!ucAttr.CheckAttachment())
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
                return false;
            }
            return true;
        }
        #endregion

        #region 保存、提交事件
        private void SaveAndSubmit(string strMessage, DataSources.EnumAuditStatus status)
        {
            try
            {
                List<SQLObj> listSql = new List<SQLObj>();
                string currCom_id = string.Empty;//当前信息编号
                string keyName = string.Empty;
                string keyValue = string.Empty;
                if (!CheckControlValue()) return;
                if (MessageBoxEx.Show("确认要" + strMessage + "吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
                opName = "新增维修接待单";
                #region 表字段赋值
                dicFileds = new Dictionary<string, string>();
                dicFileds.Add("reception_time", Common.LocalDateTimeToUtcLong(dtpReTime.Value).ToString());//接待日期
                dicFileds.Add("vehicle_no", txtCarNO.Text.Trim());//车牌号
                dicFileds.Add("vehicle_vin", txtVIN.Caption.Trim());//VIN
                dicFileds.Add("engine_no", txtEngineNo.Caption.Trim());//发动机号
                dicFileds.Add("vehicle_model", CommonCtrl.IsNullToString(txtCarType.Tag));//车型
                dicFileds.Add("vehicle_brand", cobCarBrand.SelectedValue != null ? cobCarBrand.SelectedValue.ToString() : "");//车辆品牌
                dicFileds.Add("driver_name", txtDriver.Caption.Trim());//保修人               
                dicFileds.Add("driver_mobile", txtDriverPhone.Caption.Trim());
                dicFileds.Add("turner", txtturner.Caption.Trim());//车厂编码
                dicFileds.Add("customer_code", txtCustomNO.Text.Trim());//客户编码
                dicFileds.Add("customer_name", txtCustomName.Caption.Trim());//客户名称 
                if (!string.IsNullOrEmpty(txtCustomNO.Text.Trim()))
                {
                    dicFileds.Add("customer_id", txtCustomNO.Tag.ToString());//客户关联id
                }
                dicFileds.Add("linkman", txtContact.Caption.Trim());//联系人               
                dicFileds.Add("link_man_mobile", txtContactPhone.Caption.Trim());
                dicFileds.Add("maintain_payment", cobPayType.SelectedValue != null ? cobPayType.SelectedValue.ToString() : "");//维修付费方式
                dicFileds.Add("maintain_type", cobRepType.SelectedValue != null ? cobRepType.SelectedValue.ToString() : "");//维修类别
                dicFileds.Add("maintain_man", txtAdvisor.Text.Trim());//服务顾问
                if (!string.IsNullOrEmpty(txtMl.Caption.Trim()))
                {
                    dicFileds.Add("oil_into_factory", txtMl.Caption.Trim());//进场油量
                }
                if (!string.IsNullOrEmpty(txtMil.Caption.Trim()))
                {
                    dicFileds.Add("travel_mileage", txtMil.Caption.Trim());//行驶里程
                }
                dicFileds.Add("completion_time", Common.LocalDateTimeToUtcLong(dtpSuTime.Value).ToString());//预计完工时间
                dicFileds.Add("set_meal", cobCombo.SelectedValue != null ? cobCombo.SelectedValue.ToString() : "");//维修套餐

                dicFileds.Add("fault_describe", txtDesc.Caption.Trim());//故障描述
                dicFileds.Add("remark", txtRemark.Caption.Trim());//备注

                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobYHandle.SelectedValue)))
                {
                    dicFileds.Add("responsible_opid", cobYHandle.SelectedValue.ToString());//经办人id  
                    dicFileds.Add("responsible_name", cobYHandle.SelectedText);//经办人
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cboOrgId.SelectedValue)))
                {
                    dicFileds.Add("org_id", cboOrgId.SelectedValue.ToString());//部门
                }
                dicFileds.Add("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString());//信息状态（1|激活；2|作废；0|删除） 
                dicFileds.Add("dispatch_status", Convert.ToInt32(DataSources.EnumDispatchStatus.NotStartWork).ToString());//调度状态
                dicFileds.Add("orders_source", !string.IsNullOrEmpty(strOrdersSouse)?strOrdersSouse:"0");
                dicFileds.Add("import_status", "0");
                #endregion
                if (wStatus == WindowStatus.Add || wStatus == WindowStatus.Copy)
                {
                    currCom_id = Guid.NewGuid().ToString();
                    strId = currCom_id;
                    dicFileds.Add("maintain_id", currCom_id);//新ID
                    dicFileds.Add("create_by", HXCPcClient.GlobalStaticObj.UserID);//创建人id（制单人） 
                    dicFileds.Add("create_name", HXCPcClient.GlobalStaticObj.UserName);//创建人
                    dicFileds.Add("create_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString());//创建时间
                }
                else if (wStatus == WindowStatus.Edit)
                {
                    keyName = "maintain_id";
                    keyValue = strId;
                    currCom_id = strId;
                    opName = "更新维修接待单";
                    dicFileds.Add("update_by", HXCPcClient.GlobalStaticObj.UserID);//最后修改人id 
                    dicFileds.Add("update_name", HXCPcClient.GlobalStaticObj.UserName);//修改人姓名
                    dicFileds.Add("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString());//修改时间
                }
                SaveProjectData(listSql, strId);
                SaveMaterialsData(listSql, strId);
                SaveOtherData(listSql, strId);
                ucAttr.TableName = "tb_maintain_info";
                ucAttr.TableNameKeyValue = strId;
                listSql.AddRange(ucAttr.AttachmentSql);
                if (status == DataSources.EnumAuditStatus.SUBMIT)//提交操作时生成单号并禁用前置单据
                {
                    dicFileds.Add("maintain_no", labReserveNoS.Text.Trim());//维修单号
                    dicFileds.Add("info_status", Convert.ToInt32(status).ToString());//单据状态0为草稿
                    if (!string.IsNullOrEmpty(strBefore_orderId))
                    {
                        dicFileds.Add("before_orderId", strBefore_orderId);//前置单据id
                        UpdateMaintainInfo(listSql, strBefore_orderId, labReserveNoS.Text.Trim(), "2");
                    }
                }
                else if (status == DataSources.EnumAuditStatus.DRAFT)
                {
                    if (!string.IsNullOrEmpty(strStatus) && strStatus == Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString())
                    {
                        dicFileds.Add("maintain_no", labReserveNoS.Text.Trim());//维修单号
                        dicFileds.Add("info_status", Convert.ToInt32(status).ToString());//单据状态0为草稿
                    }
                    else
                    {
                        dicFileds.Add("info_status", Convert.ToInt32(status).ToString());//单据状态0为草稿
                    }
                    if (!string.IsNullOrEmpty(strBefore_orderId))
                    {
                        dicFileds.Add("before_orderId", strBefore_orderId);//前置单据id
                        opName = "更新前置单据导";
                        UpdateMaintainInfo(listSql, strBefore_orderId, "", "0");
                    }
                }
                bool bln = DBHelper.Submit_AddOrEdit(opName, "tb_maintain_info", keyName, keyValue, dicFileds);
                if (bln && DBHelper.BatchExeSQLMultiByTrans(opName, listSql))
                {
                    MessageBoxEx.Show("" + strMessage + "成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    uc.BindPageData();
                    isAutoClose = true;
                    deleteMenuByTag(this.Tag.ToString(), this.uc.Name);
                }
                else
                {
                    MessageBoxEx.Show("" + strMessage + "失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
                MessageBoxEx.Show("" + strMessage + "失败！" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 维修项目信息保存
        private void SaveProjectData(List<SQLObj> listSql, string partID)
        {
            try
            {
                foreach (DataGridViewRow dgvr in dgvproject.Rows)
                {
                    string strPname = CommonCtrl.IsNullToString(dgvr.Cells["item_name"].Value);
                    if (strPname.Length > 0)
                    {
                        SQLObj obj = new SQLObj();
                        obj.cmdType = CommandType.Text;
                        Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                        dicParam.Add("maintain_id", new ParamObj("maintain_id", partID, SysDbType.VarChar, 40));
                        dicParam.Add("item_no", new ParamObj("item_no", dgvr.Cells["item_no"].Value, SysDbType.VarChar, 40));
                        dicParam.Add("item_type", new ParamObj("item_type", dgvr.Cells["item_type"].Value, SysDbType.VarChar, 40));
                        dicParam.Add("item_name", new ParamObj("item_name", dgvr.Cells["item_name"].Value, SysDbType.VarChar, 40));
                        dicParam.Add("man_hour_type", new ParamObj("man_hour_type", dgvr.Cells["man_hour_type"].Value, SysDbType.VarChar, 40));
                        string strHourQuantity = CommonCtrl.IsNullToString(dgvr.Cells["man_hour_quantity"].Value);
                        if (!string.IsNullOrEmpty(strHourQuantity))
                        {
                            dicParam.Add("man_hour_quantity", new ParamObj("man_hour_quantity", strHourQuantity, SysDbType.Decimal, 15));
                        }
                        else
                        {
                            dicParam.Add("man_hour_quantity", new ParamObj("man_hour_quantity", null, SysDbType.Decimal, 15));
                        }
                        dicParam.Add("man_hour_norm_unitprice", new ParamObj("man_hour_norm_unitprice", dgvr.Cells["man_hour_norm_unitprice"].Value, SysDbType.Decimal, 15));
                        dicParam.Add("member_discount", new ParamObj("member_discount", dgvr.Cells["member_discount"].Value, SysDbType.Decimal, 5));
                        dicParam.Add("member_price", new ParamObj("member_price", dgvr.Cells["member_price"].Value, SysDbType.Decimal, 15));
                        dicParam.Add("member_sum_money", new ParamObj("member_sum_money", dgvr.Cells["member_sum_money"].Value, SysDbType.Decimal, 15));
                        dicParam.Add("sum_money_goods", new ParamObj("sum_money_goods", dgvr.Cells["sum_money_goods"].Value, SysDbType.Decimal, 15));
                        dicParam.Add("whours_id", new ParamObj("whours_id", dgvr.Cells["whours_id"].Value, SysDbType.VarChar, 40));
                        dicParam.Add("data_source", new ParamObj("data_source", dgvr.Cells["data_source"].Value, SysDbType.VarChar, 5));                        
                        string strIsThree = CommonCtrl.IsNullToString(dgvr.Cells["three_warranty"].Value);
                        if (!string.IsNullOrEmpty(strIsThree))
                        {
                            dicParam.Add("three_warranty", new ParamObj("three_warranty", strIsThree == "是" ? "1" : "0", SysDbType.VarChar, 2));
                        }
                        else
                        {
                            dicParam.Add("three_warranty", new ParamObj("three_warranty", null, SysDbType.VarChar, 2));
                        }
                        dicParam.Add("remarks", new ParamObj("remarks", dgvr.Cells["remarks"].Value, SysDbType.VarChar, 200));
                        dicParam.Add("enable_flag", new ParamObj("enable_flag", "1", SysDbType.VarChar, 1));
                        string strPID = CommonCtrl.IsNullToString(dgvr.Cells["item_id"].Value);
                        if (strPID.Length == 0)
                        {
                            opName = "新增接待单维修项目";
                            strPID = Guid.NewGuid().ToString();
                            dicParam.Add("item_id", new ParamObj("item_id", strPID, SysDbType.VarChar, 40));
                            obj.sqlString = "insert into [tb_maintain_item] (item_id,maintain_id,item_no,item_type,item_name,man_hour_type,man_hour_quantity,man_hour_norm_unitprice,member_discount,member_price,member_sum_money,sum_money_goods,three_warranty,remarks,enable_flag,whours_id,data_source) ";
                            obj.sqlString += " values (@item_id,@maintain_id,@item_no,@item_type,@item_name,@man_hour_type,@man_hour_quantity,@man_hour_norm_unitprice,@member_discount,@member_price,@member_sum_money,@sum_money_goods,@three_warranty,@remarks,@enable_flag,@whours_id,@data_source);";
                        }
                        else
                        {
                            dicParam.Add("item_id", new ParamObj("item_id", dgvr.Cells["item_id"].Value, SysDbType.VarChar, 40));
                            opName = "更新接待单维修项目";
                            obj.sqlString = "update tb_maintain_item set item_no=@item_no,item_type=@item_type,item_name=@item_name,man_hour_type=@man_hour_type,man_hour_quantity=@man_hour_quantity,man_hour_norm_unitprice=@man_hour_norm_unitprice,member_discount=@member_discount,member_price=@member_price,";
                            obj.sqlString += " member_sum_money=@member_sum_money,sum_money_goods=@sum_money_goods,three_warranty=@three_warranty,remarks=@remarks,enable_flag=@enable_flag,whours_id=@whours_id,data_source=@data_source where item_id=@item_id";
                        }
                        obj.Param = dicParam;
                        listSql.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
        }
        #endregion

        #region 维修用料信息保存
        private void SaveMaterialsData(List<SQLObj> listSql, string partID)
        {
            try
            {
                foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
                {
                    string strPname = CommonCtrl.IsNullToString(dgvr.Cells["parts_name"].Value);
                    string strPCmoney = CommonCtrl.IsNullToString(dgvr.Cells["sum_money"].Value);
                    if (strPname.Length > 0 && strPCmoney.Length > 0)
                    {
                        SQLObj obj = new SQLObj();
                        obj.cmdType = CommandType.Text;
                        Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                        dicParam.Add("maintain_id", new ParamObj("maintain_id", partID, SysDbType.VarChar, 40));
                        dicParam.Add("parts_code", new ParamObj("parts_code", dgvr.Cells["parts_code"].Value, SysDbType.VarChar, 40));
                        dicParam.Add("parts_name", new ParamObj("parts_name", dgvr.Cells["parts_name"].Value, SysDbType.VarChar, 40));
                        dicParam.Add("norms", new ParamObj("norms", dgvr.Cells["norms"].Value, SysDbType.VarChar, 40));
                        dicParam.Add("unit", new ParamObj("unit", dgvr.Cells["unit"].Value, SysDbType.VarChar, 20));
                        string strIsimport = CommonCtrl.IsNullToString(dgvr.Cells["whether_imported"].Value);
                        if (!string.IsNullOrEmpty(strIsimport))
                        {
                            dicParam.Add("whether_imported", new ParamObj("whether_imported", strIsimport == "是" ? "1" : "0", SysDbType.VarChar, 1));
                        }
                        else
                        {
                            dicParam.Add("whether_imported", new ParamObj("whether_imported", null, SysDbType.VarChar, 1));
                        }
                        dicParam.Add("quantity", new ParamObj("quantity", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["quantity"].Value)) ? dgvr.Cells["quantity"].Value : null, SysDbType.Decimal, 15));
                        dicParam.Add("unit_price", new ParamObj("unit_price", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["unit_price"].Value)) ? dgvr.Cells["unit_price"].Value : null, SysDbType.Decimal, 15));

                        dicParam.Add("member_discount", new ParamObj("member_discount", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["Mmember_discount"].Value)) ? dgvr.Cells["Mmember_discount"].Value : null, SysDbType.Decimal, 15));
                        dicParam.Add("member_price", new ParamObj("member_price", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["Mmember_price"].Value)) ? dgvr.Cells["Mmember_price"].Value : null, SysDbType.Decimal, 15));

                        dicParam.Add("sum_money", new ParamObj("sum_money", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["sum_money"].Value)) ? dgvr.Cells["sum_money"].Value : null, SysDbType.Decimal, 15));
                        dicParam.Add("drawn_no", new ParamObj("drawn_no", dgvr.Cells["drawn_no"].Value, SysDbType.VarChar, 40));
                        dicParam.Add("vehicle_brand", new ParamObj("vehicle_brand", dgvr.Cells["vehicle_brand"].Value, SysDbType.VarChar, 2000));
                        string strIsThree = CommonCtrl.IsNullToString(dgvr.Cells["Mthree_warranty"].Value);
                        if (!string.IsNullOrEmpty(strIsThree))
                        {
                            dicParam.Add("three_warranty", new ParamObj("three_warranty", strIsThree == "是" ? "1" : "0", SysDbType.VarChar, 2));
                        }
                        else
                        {
                            dicParam.Add("three_warranty", new ParamObj("three_warranty", null, SysDbType.VarChar, 2));
                        }
                        dicParam.Add("remarks", new ParamObj("remarks", dgvr.Cells["Mremarks"].Value, SysDbType.VarChar, 200));
                        dicParam.Add("enable_flag", new ParamObj("enable_flag", "1", SysDbType.VarChar, 1));
                        dicParam.Add("parts_id", new ParamObj("parts_id", dgvr.Cells["parts_id"].Value, SysDbType.VarChar, 40));
                        dicParam.Add("data_source", new ParamObj("data_source", dgvr.Cells["Mdata_source"].Value, SysDbType.VarChar, 5));                        
                        string strPID = CommonCtrl.IsNullToString(dgvr.Cells["material_id"].Value);
                        if (strPID.Length == 0)
                        {
                            opName = "新增接待单维修用料";
                            strPID = Guid.NewGuid().ToString();
                            dicParam.Add("material_id", new ParamObj("material_id", strPID, SysDbType.VarChar, 40));
                            obj.sqlString = "insert into [tb_maintain_material_detail] (material_id,parts_code,parts_name,norms,unit,whether_imported,quantity,unit_price,member_discount,member_price,sum_money,drawn_no,vehicle_brand,three_warranty,remarks,enable_flag,maintain_id,parts_id,data_source) ";
                            obj.sqlString += " values (@material_id,@parts_code,@parts_name,@norms,@unit,@whether_imported,@quantity,@unit_price,@member_discount,@member_price,@sum_money,@drawn_no,@vehicle_brand,@three_warranty,@remarks,@enable_flag,@maintain_id,@parts_id,@data_source);";
                        }
                        else
                        {
                            dicParam.Add("material_id", new ParamObj("material_id", dgvr.Cells["material_id"].Value, SysDbType.VarChar, 40));
                            opName = "更新接待单维修用料";
                            obj.sqlString = "update tb_maintain_material_detail set parts_code=@parts_code,parts_name=@parts_name,norms=@norms,unit=@unit,whether_imported=@whether_imported,quantity=@quantity,unit_price=@unit_price,member_discount=@member_discount,member_price=@member_price,sum_money=@sum_money,";
                            obj.sqlString += "drawn_no=@drawn_no,vehicle_brand=@vehicle_brand, three_warranty=@three_warranty,remarks=@remarks,enable_flag=@enable_flag,parts_id=@parts_id,data_source=@data_source where material_id=@material_id";
                        }
                        obj.Param = dicParam;
                        listSql.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
        }
        #endregion

        #region 其他收费项目信息保存
        //其他收费项目信息保存sql语句
        private void SaveOtherData(List<SQLObj> listSql, string partID)
        {
            try
            {
                foreach (DataGridViewRow dgvr in dgvOther.Rows)
                {
                    string strCotype = CommonCtrl.IsNullToString(dgvr.Cells["cost_types"].Value);
                    if (strCotype.Length > 0)
                    {
                        SQLObj obj = new SQLObj();
                        obj.cmdType = CommandType.Text;
                        Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                        dicParam.Add("maintain_id", new ParamObj("maintain_id", partID, SysDbType.VarChar, 40));
                        dicParam.Add("cost_types", new ParamObj("cost_types", dgvr.Cells["cost_types"].Value, SysDbType.VarChar, 40));
                        if (CommonCtrl.IsNullToString(dgvr.Cells["Osum_money"].Value).Length > 0)
                        {
                            dicParam.Add("sum_money", new ParamObj("sum_money", dgvr.Cells["Osum_money"].Value, SysDbType.Decimal, 18));
                        }
                        else
                        {
                            dicParam.Add("sum_money", new ParamObj("sum_money", null, SysDbType.Decimal, 18));
                        }
                        dicParam.Add("remarks", new ParamObj("remarks", dgvr.Cells["Oremarks"].Value, SysDbType.VarChar, 200));
                        dicParam.Add("enable_flag", new ParamObj("enable_flag", "1", SysDbType.Char, 10));
                        string strTollID = CommonCtrl.IsNullToString(dgvr.Cells["toll_id"].Value);
                        if (strTollID.Length == 0)
                        {
                            opName = "新增接待单其他项目收费";
                            strTollID = Guid.NewGuid().ToString();
                            dicParam.Add("toll_id", new ParamObj("toll_id", strTollID, SysDbType.VarChar, 40));
                            obj.sqlString = "insert into [tb_maintain_other_toll] (toll_id,cost_types,sum_money,remarks,maintain_id,enable_flag) values (@toll_id,@cost_types,@sum_money,@remarks,@maintain_id,@enable_flag);";
                        }
                        else
                        {
                            dicParam.Add("toll_id", new ParamObj("toll_id", dgvr.Cells["toll_id"].Value, SysDbType.VarChar, 40));
                            opName = "更新接待单其他项目收费";
                            obj.sqlString = "update tb_maintain_other_toll set cost_types=@cost_types,sum_money=@sum_money,remarks=@remarks where toll_id=@toll_id";
                        }
                        obj.Param = dicParam;
                        listSql.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
        }
        #endregion

        #region 取消事件
        void UCReceiveAddOrEdit_CancelEvent(object sender, EventArgs e)
        {
            try
            {
                if (MessageBoxEx.Show("确认要取消吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
                if (!string.IsNullOrEmpty(strBefore_orderId))
                {
                    List<SQLObj> listSql = new List<SQLObj>();
                    UpdateMaintainInfo(listSql, strBefore_orderId, "", "0");
                    DBHelper.BatchExeSQLMultiByTrans("更新前置单据状体", listSql);
                }
                isAutoClose = true;
                deleteMenuByTag(this.Tag.ToString(), uc.Name);
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
        }
        #endregion

        #region 设置底部DataGridView的Anchor属性
        /// <summary>
        /// 设置底部DataGridView的Anchor属性
        /// </summary>
        private void SetDgvAnchor()
        {
            //dgvAccess.Dock = DockStyle.Fill;
            //dgvAccess.Columns["ACheck"].HeaderText = "选择";           
            //dgvAccess.Rows.Add(4);

            #region 其他收费项目
            dgvOther.Dock = DockStyle.Fill;
            //dgvOther.Columns["OCheck"].HeaderText = "选择";
            dgvOther.ReadOnly = false;
            dgvOther.Rows.Add(3);
            dgvOther.AllowUserToAddRows = true;
            dgvOther.Columns["Osum_money"].ReadOnly = true;
            dgvOther.Columns["Oremarks"].ReadOnly = true;
            ControlsConfig.NumberLimitdgv(dgvOther, new List<string>() { "Osum_money" });
            DataTable dot = CommonCtrl.GetDictByCode("sys_other_expense_type", true);
            cost_types.DataSource = dot;
            cost_types.ValueMember = "dic_id";
            cost_types.DisplayMember = "dic_name";
            #endregion

            #region 维修项目信息设置
            dgvproject.Dock = DockStyle.Fill;
            //dgvproject.Columns["colCheck"].HeaderText = "选择";
            dgvproject.ReadOnly = false;
            dgvproject.Rows.Add(3);
            dgvproject.AllowUserToAddRows = true;
            dgvproject.Columns["item_no"].ReadOnly = true;
            dgvproject.Columns["item_type"].ReadOnly = true;
            dgvproject.Columns["item_name"].ReadOnly = true;
            dgvproject.Columns["man_hour_norm_unitprice"].ReadOnly = true;
            dgvproject.Columns["sum_money_goods"].ReadOnly = true;
            dgvproject.Columns["member_discount"].ReadOnly = true;
            dgvproject.Columns["member_price"].ReadOnly = true;
            dgvproject.Columns["member_sum_money"].ReadOnly = true;
            dgvproject.Columns["man_hour_type"].ReadOnly = true;
            //dgvproject.Columns["man_hour_quantity"].ReadOnly = true;
            ControlsConfig.NumberLimitdgv(dgvproject, new List<string>() { "man_hour_quantity", "man_hour_norm_unitprice" });
            #endregion

            #region 维修用料设置
            dgvMaterials.Dock = DockStyle.Fill;
            //dgvMaterials.Columns["MCheck"].HeaderText = "选择";
            dgvMaterials.ReadOnly = false;
            dgvMaterials.Rows.Add(3);
            dgvMaterials.AllowUserToAddRows = true;
            dgvMaterials.Columns["parts_code"].ReadOnly = true;
            dgvMaterials.Columns["parts_name"].ReadOnly = true;
            dgvMaterials.Columns["norms"].ReadOnly = true;
            dgvMaterials.Columns["unit"].ReadOnly = true;
            dgvMaterials.Columns["whether_imported"].ReadOnly = true;
            dgvMaterials.Columns["sum_money"].ReadOnly = true;
            dgvMaterials.Columns["drawn_no"].ReadOnly = true;
            dgvMaterials.Columns["vehicle_brand"].ReadOnly = true;
            dgvMaterials.Columns["Mmember_discount"].ReadOnly = true;
            dgvMaterials.Columns["Mmember_price"].ReadOnly = true;

            ControlsConfig.NumberLimitdgv(dgvMaterials, new List<string>() { "quantity", "unit_price" });
            #endregion
        }
        #endregion

        #region 窗体Load事件
        private void UCReceiveAddOrEdit_Load(object sender, EventArgs e)
        {
            dtpReTime.Value = DateTime.Now;
            dtpSuTime.Value = DateTime.Now;
            ControlsConfig.TextToDecimal(txtMl, false);
            ControlsConfig.TextToDecimal(txtMil, false);
            ControlsConfig.TextForPhone(txtContactPhone);
            ControlsConfig.TextForPhone(txtDriverPhone);
            //base.SetBtnStatus(wStatus);
            if (wStatus == WindowStatus.Edit || wStatus == WindowStatus.Copy)
            {
                BindData();
            }
            else if (wStatus == WindowStatus.Add)
            {
                labReserveNoS.Visible = false;
            }
        }
        #endregion

        #region 根据维修接待单ID获取信息，复制和编辑用
        /// <summary>
        /// 根据维修接待单ID获取信息，复制和编辑用
        /// </summary>
        private void BindData()
        {
            try
            {
                #region 基础信息
                string strWhere = string.Format("maintain_id='{0}'", strId);
                DataTable dt = DBHelper.GetTable("查询维修接待单", "tb_maintain_info", "*", strWhere, "", "");
                if (dt == null || dt.Rows.Count == 0)
                {
                    return;
                }
                DataRow dr = dt.Rows[0];
                strBefore_orderId = CommonCtrl.IsNullToString(dr["before_orderId"]);
                strOrdersSouse = CommonCtrl.IsNullToString(dr["orders_source"]);
                long Rticks = Convert.ToInt64(CommonCtrl.IsNullToString(dr["reception_time"]));
                dtpReTime.Value = Common.UtcLongToLocalDateTime(Rticks);//接待日期 
                if (wStatus == WindowStatus.Edit)
                {
                    txtCarNO.Text = CommonCtrl.IsNullToString(dr["vehicle_no"]);//车牌号
                    txtVIN.Caption = CommonCtrl.IsNullToString(dr["vehicle_vin"]);//VIN
                    txtEngineNo.Caption = CommonCtrl.IsNullToString(dr["engine_no"]);//发动机号
                    txtCarType.Text = GetVmodel(CommonCtrl.IsNullToString(dr["vehicle_model"]));//车型   
                    txtCarType.Tag = CommonCtrl.IsNullToString(dr["vehicle_model"]);
                    cobCarBrand.SelectedValue = CommonCtrl.IsNullToString(dr["vehicle_brand"]);//车辆品牌
                    txtDriver.Caption = CommonCtrl.IsNullToString(dr["driver_name"]);//司机
                    txtDriverPhone.Caption = CommonCtrl.IsNullToString(dr["driver_mobile"]);//司机手机
                    txtturner.Caption = CommonCtrl.IsNullToString(dr["turner"]);//车厂编码 
                }
                txtCustomNO.Text = CommonCtrl.IsNullToString(dr["customer_code"]);//客户编码
                txtCustomName.Caption = CommonCtrl.IsNullToString(dr["customer_name"]);//客户名称  
                txtCustomNO.Tag = CommonCtrl.IsNullToString(dr["customer_id"]);//客户关联id
                GetMemberInfo(txtCustomNO.Tag.ToString()); 
                txtContact.Caption = CommonCtrl.IsNullToString(dr["linkman"]);//联系人
                txtContactPhone.Caption = CommonCtrl.IsNullToString(dr["link_man_mobile"]);//联系人手机
                cobPayType.SelectedValue = CommonCtrl.IsNullToString(dr["maintain_payment"]);//维修付费方式
                cobRepType.SelectedValue = CommonCtrl.IsNullToString(dr["maintain_type"]);//维修类别
                txtAdvisor.Text = CommonCtrl.IsNullToString(dr["maintain_man"]);//服务顾问
                txtMl.Caption = CommonCtrl.IsNullToString(dr["oil_into_factory"]);//进场油量
                txtMil.Caption = CommonCtrl.IsNullToString(dr["travel_mileage"]);//行驶里程
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["completion_time"])))
                {
                    long ticks = Convert.ToInt64(CommonCtrl.IsNullToString(dr["completion_time"]));
                    dtpSuTime.Value = Common.UtcLongToLocalDateTime(ticks); //预计完工时间      
                }
                else
                {
                    dtpSuTime.Value = DateTime.Now;
                }
                txtDesc.Caption = CommonCtrl.IsNullToString(dr["fault_describe"]);//故障描述
                txtRemark.Caption = CommonCtrl.IsNullToString(dr["remark"]);//备注
                //cboOrgId.SelectedValue = CommonCtrl.IsNullToString(dr["responsible_name"]);//经办人
                cboOrgId.SelectedValue = CommonCtrl.IsNullToString(dr["org_id"]);//部门
                cobYHandle.SelectedValue = CommonCtrl.IsNullToString(dr["responsible_opid"]);//经办人id 
                if (wStatus == WindowStatus.Edit)
                {
                    //labReserveNoS.Text = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["maintain_no"])) ? CommonCtrl.IsNullToString(dr["maintain_no"]) : labReserveNoS.Text;
                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["maintain_no"])))
                    {
                        labReserveNoS.Text = CommonCtrl.IsNullToString(dr["maintain_no"]);//接待单号
                    }
                    else
                    {
                        labReserveNoS.Visible = false;
                    }
                    strStatus = CommonCtrl.IsNullToString(dr["info_status"]);//单据状态
                    if (strStatus == Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString())
                    {
                        //已提交状态屏蔽提交按钮
                        base.btnSubmit.Enabled = false;
                    }
                    else if (strStatus == Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString())
                    {
                        //已审核时屏蔽提交、审核按钮
                        base.btnSubmit.Enabled = false;
                        base.btnVerify.Enabled = false;
                    }
                    //else if (strStatus == Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString())
                    //{
                    //    //审核没通过时屏蔽审核按钮
                    //    base.btnVerify.Enabled = false;
                    //}
                }
                else
                {
                    labReserveNoS.Visible = false;
                }
                #endregion

                #region 底部datagridview数据
                BindPData(strId);
                BindMData(strId);
                BindOData(strId);
                BindAData("tb_maintain_info", strId);
                #endregion
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
        }
        #endregion

        #region 底部datagridview数据信息绑定

        #region 维修项目数据
        private void BindPData(string strOrderId, bool isImport = true)
        {
            try
            {
                //维修项目数据                
                DataTable dpt = DBHelper.GetTable("维修项目数据", "tb_maintain_item", "*", string.Format(" maintain_id='{0}' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", strOrderId), "", "");
                if (dpt.Rows.Count > 0)
                {
                    if (dpt.Rows.Count >= dgvproject.Rows.Count)
                    {
                        dgvproject.Rows.Add(dpt.Rows.Count - dgvproject.Rows.Count + 2);
                    }
                    else if ((dgvproject.Rows.Count - dpt.Rows.Count) == 1)
                    {
                        dgvproject.Rows.Add(1);
                    }
                    for (int i = 0; i < dpt.Rows.Count; i++)
                    {
                        DataRow dpr = dpt.Rows[i];
                        if (isImport)
                        {
                            dgvproject.Rows[i].Cells["item_id"].Value = CommonCtrl.IsNullToString(dpr["item_id"]);
                        }
                        dgvproject.Rows[i].Cells["three_warranty"].Value = CommonCtrl.IsNullToString(dpr["three_warranty"]) == "1" ? "是" : "否";
                        dgvproject.Rows[i].Cells["man_hour_type"].Value = CommonCtrl.IsNullToString(dpr["man_hour_type"]);
                        dgvproject.Rows[i].Cells["item_no"].Value = CommonCtrl.IsNullToString(dpr["item_no"]);
                        dgvproject.Rows[i].Cells["item_name"].Value = CommonCtrl.IsNullToString(dpr["item_name"]);
                        dgvproject.Rows[i].Cells["item_type"].Value = CommonCtrl.IsNullToString(dpr["item_type"]);
                        dgvproject.Rows[i].Cells["man_hour_quantity"].Value = CommonCtrl.IsNullToString(dpr["man_hour_quantity"]);
                        dgvproject.Rows[i].Cells["man_hour_norm_unitprice"].ReadOnly = CommonCtrl.IsNullToString(dpr["man_hour_type"]) == "工时" ? true : false;
                        dgvproject.Rows[i].Cells["man_hour_norm_unitprice"].Value = CommonCtrl.IsNullToString(dpr["man_hour_norm_unitprice"]);
                        dgvproject.Rows[i].Cells["remarks"].Value = CommonCtrl.IsNullToString(dpr["remarks"]);
                        string strPzk = string.Empty;
                        if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["member_discount"])))
                        {
                            dgvproject.Rows[i].Cells["member_discount"].Value = CommonCtrl.IsNullToString(dpr["member_discount"]);
                        }
                        else
                        {
                            strPzk = !string.IsNullOrEmpty(txtMemberPZk.Caption) ? txtMemberPZk.Caption : "100";
                            #region 验证是否存在特殊项目
                            string strPdic = DBHelper.GetSingleValue("获取特殊项目折扣", "tb_CustomerSer_member_setInfo_projrct", "service_project_discount", "setInfo_id='" + strSetInfoid + "' and service_project_id='" + CommonCtrl.IsNullToString(dpr["whours_id"]) + "' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", "");
                            if (!string.IsNullOrEmpty(strPdic))
                            {
                                strPzk = strPdic;
                            }
                            #endregion
                            //会员折扣
                            dgvproject.Rows[i].Cells["member_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk));
                        }
                        if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["member_price"])))
                        {
                            dgvproject.Rows[i].Cells["member_price"].Value = CommonCtrl.IsNullToString(dpr["member_price"]);
                        }
                        else
                        {
                            //会员工时费
                            dgvproject.Rows[i].Cells["member_price"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["man_hour_norm_unitprice"])) ? CommonCtrl.IsNullToString(dpr["man_hour_norm_unitprice"]) : "0") * (Convert.ToDecimal(strPzk) / 100));
                        }

                        if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["member_sum_money"])))
                        {
                            dgvproject.Rows[i].Cells["member_sum_money"].Value = CommonCtrl.IsNullToString(dpr["member_sum_money"]);
                        }
                        else
                        {
                            if (CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value) != "0")
                            {
                                //折扣额
                                dgvproject.Rows[i].Cells["member_sum_money"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["man_hour_norm_unitprice"])) ? CommonCtrl.IsNullToString(dpr["man_hour_norm_unitprice"]) : "0") - Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value) : "0"));
                            }
                            else
                            {
                                dgvproject.Rows[i].Cells["member_sum_money"].Value = "0";
                            }
                        }
                        dgvproject.Rows[i].Cells["sum_money_goods"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["man_hour_quantity"])) ? CommonCtrl.IsNullToString(dpr["man_hour_quantity"]) : "0") * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value) : "0"));
                        dgvproject.Rows[i].Cells["whours_id"].Value = CommonCtrl.IsNullToString(dpr["whours_id"]);
                        dgvproject.Rows[i].Cells["data_source"].Value = CommonCtrl.IsNullToString(dpr["data_source"]);
                        dgvproject.Rows[i].Cells["three_warranty"].ReadOnly = CommonCtrl.IsNullToString(dpr["data_source"]) == Convert.ToInt32(DataSources.EnumDataSources.SELFBUILD).ToString() ? true : false;
                        listProject.Add(CommonCtrl.IsNullToString(dpr["item_no"]));
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
        }
        #endregion

        #region 维修用料数据
        private void BindMData(string strOrderId, bool isImport = true)
        {
            try
            {
                //维修用料数据
                DataTable dmt = DBHelper.GetTable("维修用料数据", "tb_maintain_material_detail", "*", string.Format(" maintain_id='{0}' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", strOrderId), "", "");
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
                        if (isImport)
                        {
                            dgvMaterials.Rows[i].Cells["material_id"].Value = CommonCtrl.IsNullToString(dmr["material_id"]);
                        }
                        dgvMaterials.Rows[i].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dmr["parts_code"]);
                        dgvMaterials.Rows[i].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dmr["parts_name"]);
                        dgvMaterials.Rows[i].Cells["norms"].Value = CommonCtrl.IsNullToString(dmr["norms"]);
                        dgvMaterials.Rows[i].Cells["unit"].Value = CommonCtrl.IsNullToString(dmr["unit"]);
                        dgvMaterials.Rows[i].Cells["quantity"].Value = CommonCtrl.IsNullToString(dmr["quantity"]);
                        dgvMaterials.Rows[i].Cells["unit_price"].Value = CommonCtrl.IsNullToString(dmr["unit_price"]);
                        if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dmr["member_discount"])))
                        {
                            dgvMaterials.Rows[i].Cells["Mmember_discount"].Value = CommonCtrl.IsNullToString(dmr["member_discount"]);
                        }
                        else
                        {
                            string strPzk = !string.IsNullOrEmpty(txtMemberLZk.Caption) ? txtMemberLZk.Caption : "100";
                            #region 验证是否存在特殊配件
                            string strMdic = DBHelper.GetSingleValue("获取特殊配件折扣", "tb_CustomerSer_member_setInfo_parts", "parts_discount", "setInfo_id='" + strSetInfoid + "' and parts_id='" + CommonCtrl.IsNullToString(dmr["parts_id"]) + "' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", "");
                            if (!string.IsNullOrEmpty(strMdic))
                            {
                                strPzk = strMdic;
                            }
                            #endregion
                            //会员折扣
                            dgvMaterials.Rows[i].Cells["Mmember_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk));
                        }
                        if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dmr["member_price"])))
                        {
                            dgvMaterials.Rows[i].Cells["Mmember_price"].Value = CommonCtrl.IsNullToString(dmr["member_price"]);
                        }
                        else
                        {
                            string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["quantity"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["quantity"].Value) : "0";
                            string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["unit_price"].Value)) ? dgvMaterials.Rows[i].Cells["unit_price"].Value.ToString() : "0";
                            //会员单价
                            dgvMaterials.Rows[i].Cells["Mmember_price"].Value = Convert.ToString(Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["Mmember_discount"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["Mmember_discount"].Value) : "0") / 100);
                        }
                        if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dmr["sum_money"])))
                        {
                            dgvMaterials.Rows[i].Cells["sum_money"].Value = CommonCtrl.IsNullToString(dmr["sum_money"]);
                        }
                        else
                        {
                            string strNums = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["quantity"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["quantity"].Value) : "0";
                            string strMprice = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["Mmember_price"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["Mmember_price"].Value) : "0";
                            dgvMaterials.Rows[i].Cells["sum_money"].Value = Convert.ToString(Convert.ToDecimal(strNums) * Convert.ToDecimal(strMprice));
                        }
                        dgvMaterials.Rows[i].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dmr["drawn_no"]);
                        dgvMaterials.Rows[i].Cells["vehicle_brand"].Value = CommonCtrl.IsNullToString(dmr["vehicle_brand"]);
                        dgvMaterials.Rows[i].Cells["Mthree_warranty"].Value = CommonCtrl.IsNullToString(dmr["three_warranty"]) == "1" ? "是" : "否";
                        dgvMaterials.Rows[i].Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dmr["remarks"]);
                        dgvMaterials.Rows[i].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dmr["whether_imported"]) == "1" ? "是" : "否";
                        dgvMaterials.Rows[i].Cells["parts_id"].Value = CommonCtrl.IsNullToString(dmr["parts_id"]);
                        dgvMaterials.Rows[i].Cells["Mdata_source"].Value = CommonCtrl.IsNullToString(dmr["data_source"]);
                        dgvMaterials.Rows[i].Cells["Mthree_warranty"].ReadOnly = CommonCtrl.IsNullToString(dmr["data_source"]) == Convert.ToInt32(DataSources.EnumDataSources.SELFBUILD).ToString() ? true : false;
                        listMater.Add(CommonCtrl.IsNullToString(dmr["parts_code"]));
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
        }
        #endregion

        #region 其他项目收费数据
        private void BindOData(string strOrderId, bool isImport = true)
        {
            try
            {
                ////其他项目收费数据
                DataTable dot = DBHelper.GetTable("其他项目收费数据", "tb_maintain_other_toll", "*", string.Format(" maintain_id='{0}'", strOrderId), "", "");
                if (dot.Rows.Count > 0)
                {
                    if (dot.Rows.Count >= dgvOther.Rows.Count)
                    {
                        dgvOther.Rows.Add(dot.Rows.Count - dgvOther.Rows.Count + 1);
                    }
                    for (int i = 0; i < dot.Rows.Count; i++)
                    {
                        DataRow dor = dot.Rows[i];
                        if (isImport)
                        {
                            dgvOther.Rows[i].Cells["toll_id"].Value = CommonCtrl.IsNullToString(dor["toll_id"]);
                        }
                        dgvOther.Rows[i].Cells["Osum_money"].Value = CommonCtrl.IsNullToString(dor["sum_money"]);
                        dgvOther.Rows[i].Cells["Oremarks"].Value = CommonCtrl.IsNullToString(dor["remarks"]);
                        dgvOther.Rows[i].Cells["cost_types"].Value = CommonCtrl.IsNullToString(dor["cost_types"]);
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
        }
        #endregion

        #region 附件信息数据
        private void BindAData(string strTableName, string strOrderId)
        {
            try
            {
                //附件信息数据
                ucAttr.TableName = strTableName;
                ucAttr.TableNameKeyValue = strOrderId;
                ucAttr.BindAttachment();
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
        }
        #endregion

        #endregion

        #region 初始化车辆品牌、车厂编码、维修付费方式、维修类别信息
        /// <summary>
        /// 初始化车型、车辆品牌、车厂编码、维修付费方式、维修类别信息
        /// </summary>
        private void initializeData()
        {
            try
            {
                CommonCtrl.BindComboBoxByDictionarr(cobCarBrand, "sys_vehicle_brand", true);//绑定车型品牌
                //CommonCtrl.BindComboBoxByDictionarr(cobColor, "sys_vehicle_color", true);//绑定车厂编码
                CommonCtrl.BindComboBoxByDictionarr(cobPayType, "sys_repair_pay_methods", true);//绑定维修付费方式
                CommonCtrl.BindComboBoxByDictionarr(cobRepType, "sys_repair_category", true);//绑定维修类别            
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
        }
        #endregion

        #region 生成维修单号&创建人姓名
        /// <summary>
        /// 生成预约单号&创建人姓名
        /// </summary>
        private void GetRepairNo()
        {
            labReserveNoS.Text = CommonUtility.GetNewNo(SYSModel.DataSources.EnumProjectType.Repair);
            labCreatePersonS.Text = HXCPcClient.GlobalStaticObj.UserName;
        }
        #endregion

        #region 获取车型名称
        private string GetVmodel(string strVId)
        {
            return DBHelper.GetSingleValue("获得车型名称", "tb_vehicle_models", "vm_name", "vm_id='" + strVId + "'", "");
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
            try
            {
                frmVehicleGrade frmVehicle = new frmVehicleGrade();
                DialogResult result = frmVehicle.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtCarNO.Text = frmVehicle.strLicensePlate;
                    txtVIN.Caption = frmVehicle.strVIN;
                    txtEngineNo.Caption = frmVehicle.strEngineNum;
                    txtCarType.Text = GetVmodel(frmVehicle.strModel);
                    txtCarType.Tag = frmVehicle.strModel;
                    cobCarBrand.SelectedValue = frmVehicle.strBrand;
                    //cobColor.SelectedValue = frmVehicle.strColor;
                    txtturner.Caption = frmVehicle.Turner;
                    txtCustomNO.Text = frmVehicle.strCustCode;
                    txtCustomNO.Tag = frmVehicle.strCustId;
                    txtCustomName.Caption = frmVehicle.strCustName;
                    txtContact.Caption = frmVehicle.strContactName;
                    txtContactPhone.Caption = frmVehicle.strContactPhone;
                    GetMemberInfo(frmVehicle.strCustId);  
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

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
            try
            {
                frmVehicleModels frmModels = new frmVehicleModels();
                DialogResult result = frmModels.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtCarType.Text = frmModels.VMName;
                    txtCarType.Tag = frmModels.VMID;
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
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
            try
            {
                frmCustomerInfo frmCInfo = new frmCustomerInfo();
                DialogResult result = frmCInfo.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtCustomNO.Text = frmCInfo.strCustomerNo;
                    txtCustomNO.Tag = frmCInfo.strCustomerId;
                    txtCustomName.Caption = frmCInfo.strCustomerName;
                    txtContact.Caption = frmCInfo.strLegalPerson;
                    GetMemberInfo(frmCInfo.strCustomerId);                  
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
        }
        #endregion

        #region 获取客户会员信息
        /// <summary>
        /// 获取客户会员信息
        /// </summary>
        /// <param name="strCid">客户Id</param>
        private void GetMemberInfo(string strCid)
        {
            MemberInfo mInfo = new MemberInfo();
            CommonFuncCall.GetMemberDiscount(mInfo,strCid);
            txtMemberNo.Caption = mInfo.strMemberNo;
            txtMemberGrade.Caption = mInfo.strMemberGrade;
            txtMemberPZk.Caption = mInfo.strMemberPZk;
            txtMemberLZk.Caption = mInfo.strMemberLZk;
            strSetInfoid = mInfo.strSetInfoId;
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

        #region 底部估计信息
        #region 计算其他收费项目估价
        private void dgvOther_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvOther.Rows[e.RowIndex].Cells["cost_types"].Value)))
                {
                    dgvOther.Rows[e.RowIndex].Cells["Osum_money"].ReadOnly = false;
                    dgvOther.Rows[e.RowIndex].Cells["Oremarks"].ReadOnly = false;
                    if (e.ColumnIndex == 2)
                    {
                        ControlsConfig.SetCellsValue(dgvOther, e.RowIndex, "Osum_money");
                    }
                    dgvOther.Rows[e.RowIndex].Cells["Osum_money"].Value = ControlsConfig.SetNewValue(dgvOther.Rows[e.RowIndex].Cells["Osum_money"].Value, 2);
                }
                else
                {                   
                    dgvOther.Rows[e.RowIndex].Cells["Osum_money"].Value = "";
                    dgvOther.Rows[e.RowIndex].Cells["Osum_money"].ReadOnly = true;
                }
                decimal dcMoney = 0;
                for (int i = 0; i < dgvOther.Rows.Count; i++)
                {
                    string strMoney = CommonCtrl.IsNullToString(dgvOther.Rows[i].Cells["Osum_money"].Value);

                    if (!string.IsNullOrEmpty(strMoney))
                    {
                        dcMoney += Convert.ToDecimal(strMoney);
                    }                   
                }
                txtOAssess.Caption = dcMoney.ToString();
                txtTotal.Caption = (Convert.ToDecimal(txtPAssess.Caption = !string.IsNullOrEmpty(txtPAssess.Caption) ? txtPAssess.Caption : "0") + Convert.ToDecimal(txtLAssess.Caption = !string.IsNullOrEmpty(txtLAssess.Caption) ? txtLAssess.Caption : "0") + Convert.ToDecimal(txtOAssess.Caption)).ToString();
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
        }

        #endregion
        #endregion

        #region 预约单导入,返修单导入
        /// <summary>
        /// 预约单导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(strBefore_orderId))
                {
                    List<SQLObj> listSql = new List<SQLObj>();
                    UpdateMaintainInfo(listSql, strBefore_orderId, "", "0");
                    DBHelper.BatchExeSQLMultiByTrans("更新前置单据状体", listSql);
                }
                UCReserveImport ReserveImport = new UCReserveImport();
                ReserveImport.uc = this;
                ReserveImport.ShowDialog();
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }

        }
        /// <summary>
        /// 返修单导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(strBefore_orderId))
                {
                    List<SQLObj> listSql = new List<SQLObj>();
                    UpdateMaintainInfo(listSql, strBefore_orderId, "", "0");
                    DBHelper.BatchExeSQLMultiByTrans("更新前置单据状体", listSql);
                }
                UCCallBackImport CallBackImport = new UCCallBackImport();
                CallBackImport.uc = this;
                CallBackImport.ShowDialog();
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
        }
        #endregion

        #region 绑定返修单信息
        public void BindCallBackData()
        {
            try
            {
                #region 基本信息
                DataTable dt = DBHelper.GetTable("查询返修单", "tb_maintain_back_repair", "*", " repair_id ='" + strBefore_orderId + "'", "", "");
                if (dt == null || dt.Rows.Count == 0)
                {
                    return;
                }
                DataRow dr = dt.Rows[0];
                strOrdersSouse = "2";
                dtpReTime.Value = DateTime.Now;//接待日期          
                txtCarNO.Text = CommonCtrl.IsNullToString(dr["vehicle_no"]);//车牌号
                txtVIN.Caption = CommonCtrl.IsNullToString(dr["vehicle_vin"]);//VIN
                txtEngineNo.Caption = CommonCtrl.IsNullToString(dr["engine_type"]);//发动机号
                txtCarType.Text = GetVmodel(CommonCtrl.IsNullToString(dr["vehicle_model"]));//车型  
                txtCarType.Tag = CommonCtrl.IsNullToString(dr["vehicle_model"]);
                cobCarBrand.SelectedValue = CommonCtrl.IsNullToString(dr["vehicle_brand"]);//车辆品牌
                txtDriver.Caption = CommonCtrl.IsNullToString(dr["driver_name"]);//司机
                txtDriverPhone.Caption = CommonCtrl.IsNullToString(dr["driver_mobile"]);//司机手机
                txtturner.Caption = CommonCtrl.IsNullToString(dr["turner"]);//车厂编码           
                txtCustomNO.Text = CommonCtrl.IsNullToString(dr["customer_code"]);//客户编码
                txtCustomName.Caption = CommonCtrl.IsNullToString(dr["customer_name"]);//客户名称  
                txtCustomNO.Tag = CommonCtrl.IsNullToString(dr["customer_id"]);//客户关联id
                GetMemberInfo(CommonCtrl.IsNullToString(dr["customer_id"]));
                txtContact.Caption = CommonCtrl.IsNullToString(dr["linkman"]);//联系人
                txtContactPhone.Caption = CommonCtrl.IsNullToString(dr["linkman_mobile"]);//联系人手机
                txtDesc.Caption = CommonCtrl.IsNullToString(dr["repair_describe"]);//故障描述
                txtMil.Caption = CommonCtrl.IsNullToString(dr["mileage"]);//行驶里程
                #endregion

                #region 底部datagridview数据
                BindPData(strBefore_orderId, false);
                BindMData(strBefore_orderId, false);
                BindOData(strBefore_orderId, false);
                BindAData("tb_maintain_back_repair", strBefore_orderId);
                List<SQLObj> listSql = new List<SQLObj>();
                UpdateMaintainInfo(listSql, strBefore_orderId, "", "1");
                DBHelper.BatchExeSQLMultiByTrans("更新前置单据状体", listSql);
                #endregion
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
        }
        #endregion

        #region 绑定预约单信息
        public void BindReserveData()
        {
            try
            {
                #region 基本信息
                DataTable dt = DBHelper.GetTable("查询预约单", "tb_maintain_reservation", "*", " reserv_id ='" + strBefore_orderId + "'", "", "");
                if (dt == null || dt.Rows.Count == 0)
                {
                    return;
                }
                DataRow dr = dt.Rows[0];
                strOrdersSouse = "1";
                dtpReTime.Value = DateTime.Now;//接待日期          
                txtCarNO.Text = CommonCtrl.IsNullToString(dr["vehicle_no"]);//车牌号
                txtVIN.Caption = CommonCtrl.IsNullToString(dr["vehicle_vin"]);//VIN
                txtEngineNo.Caption = CommonCtrl.IsNullToString(dr["engine_type"]);//发动机号
                txtCarType.Text = GetVmodel(CommonCtrl.IsNullToString(dr["vehicle_model"]));//车型     
                txtCarType.Tag = CommonCtrl.IsNullToString(dr["vehicle_model"]);
                cobCarBrand.SelectedValue = CommonCtrl.IsNullToString(dr["vehicle_brand"]);//车辆品牌
                txtDriver.Caption = CommonCtrl.IsNullToString(dr["linkman"]);//预约人
                txtDriverPhone.Caption = CommonCtrl.IsNullToString(dr["link_man_mobile"]);//预约人手机
                txtturner.Caption = CommonCtrl.IsNullToString(dr["turner"]);//车厂编码           
                txtCustomNO.Text = CommonCtrl.IsNullToString(dr["customer_code"]);//客户编码
                txtCustomName.Caption = CommonCtrl.IsNullToString(dr["customer_name"]);//客户名称  
                txtCustomNO.Tag = CommonCtrl.IsNullToString(dr["customer_id"]);//客户关联id
                GetMemberInfo(CommonCtrl.IsNullToString(dr["customer_id"]));
                txtContact.Caption = CommonCtrl.IsNullToString(dr["linkman"]);//联系人
                txtContactPhone.Caption = CommonCtrl.IsNullToString(dr["link_man_mobile"]);//联系人手机
                cobPayType.SelectedValue = CommonCtrl.IsNullToString(dr["maintain_payment"]);//维修付费方式
                cobRepType.SelectedValue = CommonCtrl.IsNullToString(dr["maintain_type"]);//维修类别
                txtDesc.Caption = CommonCtrl.IsNullToString(dr["fault_describe"]);//故障描述
                txtRemark.Caption = CommonCtrl.IsNullToString(dr["remark"]);//备注

                #endregion

                #region 底部datagridview数据
                BindPData(strBefore_orderId, false);
                BindMData(strBefore_orderId, false);
                BindOData(strBefore_orderId, false);
                BindAData("tb_maintain_reservation", strBefore_orderId);
                List<SQLObj> listSql = new List<SQLObj>();
                UpdateMaintainInfo(listSql, strBefore_orderId, "", "1");
                DBHelper.BatchExeSQLMultiByTrans("更新前置单据状体", listSql);
                #endregion
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
        }
        #endregion

        #region 导入后更新预约单-在预约单中添加维修单号
        /// <summary>
        /// 导入后更新预约单-在预约单中添加维修单号
        /// </summary>
        /// <param name="strReservId">预约单Id</param>
        /// <param name="strMaintainNo">维修单号</param>
        /// <param name="status">操作状体，0保存开放、1导入占用、2提交审核锁定</param>
        private void UpdateMaintainInfo(List<SQLObj> listSql, string strReservId, string strMaintainNo, string status)
        {
            try
            {
                SQLObj obj = new SQLObj();
                obj.cmdType = CommandType.Text;
                Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                Dictionary<string, string> BefordicFileds = new Dictionary<string, string>();
                if (strOrdersSouse == "1")//预约单
                {
                    dicParam.Add("reserv_id", new ParamObj("reserv_id", strReservId, SysDbType.VarChar, 40));
                    if (status == "0")
                    {
                        //保存时，前置单据被释放               
                        dicParam.Add("import_status", new ParamObj("import_status", "0", SysDbType.VarChar, 40));//开放
                        obj.sqlString = "update tb_maintain_reservation set import_status=@import_status where reserv_id=@reserv_id";
                        BefordicFileds.Add("before_orderId", strBefore_orderId);//前置信息单号
                        BefordicFileds.Add("orders_source", "1");//前置信息类型
                    }
                    else if (status == "1")
                    {
                        //导入时，前置单据被占用 
                        dicParam.Add("import_status", new ParamObj("import_status", "1", SysDbType.VarChar, 40));//占用
                        obj.sqlString = "update tb_maintain_reservation set import_status=@import_status where reserv_id=@reserv_id";
                    }
                    else if (status == "2")
                    {

                        //审核提交时，前置单据被锁定并添加维修单号
                        dicParam.Add("maintain_no", new ParamObj("maintain_no", strMaintainNo, SysDbType.VarChar, 40));
                        dicParam.Add("import_status", new ParamObj("import_status", "2", SysDbType.VarChar, 40));//锁定
                        obj.sqlString = "update tb_maintain_reservation set maintain_no=@maintain_no,import_status=@import_status where reserv_id=@reserv_id";
                        BefordicFileds.Add("before_orderId", strBefore_orderId);//前置信息单号
                        BefordicFileds.Add("orders_source", "1");//前置信息类型
                    }
                }
                else if (strOrdersSouse == "2")//返修单
                {
                    dicParam.Add("repair_id", new ParamObj("repair_id", strReservId, SysDbType.VarChar, 40));
                    if (status == "0")
                    {
                        //保存时，前置单据被释放               
                        dicParam.Add("import_status", new ParamObj("import_status", "0", SysDbType.VarChar, 40));//开放
                        obj.sqlString = "update tb_maintain_back_repair set import_status=@import_status where repair_id=@repair_id";
                        BefordicFileds.Add("before_orderId", strBefore_orderId);//前置信息单号
                        BefordicFileds.Add("orders_source", "2");//前置信息类型
                    }
                    else if (status == "1")
                    {
                        //导入时，前置单据被占用 
                        dicParam.Add("import_status", new ParamObj("import_status", "1", SysDbType.VarChar, 40));//占用
                        obj.sqlString = "update tb_maintain_back_repair set import_status=@import_status where repair_id=@repair_id";
                    }
                    else if (status == "2")
                    {

                        //审核提交时，前置单据被锁定并添加维修单号
                        dicParam.Add("maintain_no", new ParamObj("maintain_no", strMaintainNo, SysDbType.VarChar, 40));
                        dicParam.Add("import_status", new ParamObj("import_status", "2", SysDbType.VarChar, 40));//锁定
                        obj.sqlString = "update tb_maintain_back_repair set maintain_no=@maintain_no,import_status=@import_status where repair_id=@repair_id";
                        BefordicFileds.Add("before_orderId", strBefore_orderId);//前置信息单号
                        BefordicFileds.Add("orders_source", "2");//前置信息类型
                    }
                }
                obj.Param = dicParam;
                listSql.Add(obj);
                //DBHelper.Submit_AddOrEdit(opName, "tb_maintain_info", "", "", BefordicFileds);
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
        }
        #endregion

        #region 根据部门的选择绑定经办人
        /// <summary>
        /// 根据部门的选择绑定经办人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboOrgId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboOrgId.SelectedValue == null)
                {
                    return;
                }
                CommonFuncCall.BindHandle(cobYHandle, cboOrgId.SelectedValue.ToString(), "请选择");
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
        }
        #endregion

        #region 维修项目信息读取
        private void dgvproject_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int intProws = e.RowIndex;//当前行的索引
                frmWorkHours frmHours = new frmWorkHours();
                DialogResult result = frmHours.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (listProject.Contains(frmHours.strProjectNum) && frmHours.strWhoursType != "1")
                    {
                        MessageBoxEx.Show("此维修项目已存在，请选择其他维修项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    for (int i = 0; i <= intProws; i++)
                    {
                        if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["item_no"].Value)))
                        {
                            dgvproject.Rows[i].Cells["item_no"].Value = frmHours.strProjectNum;
                            dgvproject.Rows[i].Cells["item_type"].Value = GetDicName(frmHours.strRepairType);
                            dgvproject.Rows[i].Cells["item_name"].Value = frmHours.strProjectName;
                            dgvproject.Rows[i].Cells["man_hour_norm_unitprice"].Value = frmHours.strQuotaPrice;
                            dgvproject.Rows[i].Cells["remarks"].Value = frmHours.strRemark;
                            dgvproject.Rows[i].Cells["man_hour_type"].Value = frmHours.strWhoursType == "1" ? "工时" : "定额";
                            dgvproject.Rows[i].Cells["man_hour_norm_unitprice"].ReadOnly = CommonCtrl.IsNullToString(frmHours.strWhoursType) == "1" ? true : false;
                            string strPzk = !string.IsNullOrEmpty(txtMemberPZk.Caption) ? txtMemberPZk.Caption : "100";
                            #region 验证是否存在特殊项目
                            string strPdic = DBHelper.GetSingleValue("获取特殊项目折扣", "tb_CustomerSer_member_setInfo_projrct", "service_project_discount", "setInfo_id='" + strSetInfoid + "' and service_project_id='" + frmHours.strWhours_id + "' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", "");
                            if (!string.IsNullOrEmpty(strPdic))
                           {
                               strPzk = strPdic;
                           }
                            #endregion
                            //会员折扣
                            dgvproject.Rows[i].Cells["member_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk));
                            //会员工时费
                            dgvproject.Rows[i].Cells["member_price"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strQuotaPrice) ? frmHours.strQuotaPrice : "0") * (Convert.ToDecimal(strPzk) / 100));
                            if (CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value) != "0")
                            {
                                //折扣额
                                dgvproject.Rows[i].Cells["member_sum_money"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strQuotaPrice) ? frmHours.strQuotaPrice : "0") - Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value) : "0"));
                            }
                            else
                            {
                                dgvproject.Rows[i].Cells["member_sum_money"].Value = "0";
                            }
                            dgvproject.Rows[i].Cells["whours_id"].Value = frmHours.strWhours_id;
                            dgvproject.Rows[i].Cells["man_hour_quantity"].Value = frmHours.strWhoursNum;
                            dgvproject.Rows[i].Cells["sum_money_goods"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strWhoursNum) ? frmHours.strWhoursNum : "0") * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value) : "0"));
                            dgvproject.Rows[i].Cells["three_warranty"].Value = "否";
                            dgvproject.Rows[i].Cells["data_source"].Value = frmHours.strData_source;
                            dgvproject.Rows[i].Cells["three_warranty"].ReadOnly = frmHours.strData_source == Convert.ToInt32(DataSources.EnumDataSources.SELFBUILD).ToString() ? true : false;
                            dgvproject.Rows.Add(1);
                            break;
                        }
                    }
                }
                foreach (DataGridViewRow dgvr in dgvproject.Rows)
                {
                    string strPCode = CommonCtrl.IsNullToString(dgvr.Cells["item_no"].Value);
                    if (!string.IsNullOrEmpty(strPCode) && !listProject.Contains(strPCode))
                    {
                        listProject.Add(strPCode);
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
        }

        /// <summary>
        /// 工时数量发生改变时货款也跟着改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>       
        private void dgvproject_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                //是否三包值
                string strIsThree = CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["three_warranty"].Value);
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["item_name"].Value)))
                {
                    ControlsConfig.SetCellsValue(dgvproject, e.RowIndex, "man_hour_quantity,man_hour_norm_unitprice");
                    //工时数量
                    string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_quantity"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_quantity"].Value) : "0";
                    //原工时单价
                    string strUnitprice = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value) : "0";

                    if (e.ColumnIndex == 6 || e.ColumnIndex == 7)
                    {
                        //会员工时费
                        dgvproject.Rows[e.RowIndex].Cells["member_price"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["member_discount"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["member_discount"].Value) : "0") * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value) : "0") / 100);
                        string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["member_price"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["member_price"].Value) : "0";
                        if (CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["member_price"].Value) != "0")
                        {
                            //折扣额
                            dgvproject.Rows[e.RowIndex].Cells["member_sum_money"].Value = Convert.ToString(Convert.ToDecimal(strUnitprice) - Convert.ToDecimal(strUMoney));
                            //货款
                            dgvproject.Rows[e.RowIndex].Cells["sum_money_goods"].Value = Convert.ToString(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney));
                        }
                        else
                        {
                            dgvproject.Rows[e.RowIndex].Cells["sum_money_goods"].Value = Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value) : "0");
                        }

                    }
                    if (e.ColumnIndex == 4)
                    {
                        //if (CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_type"].Value) == "工时")
                        //{
                        //    //工时时设置数量和金额均可修改
                        //    //dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].ReadOnly = strIsThree == "否" ? false : true;
                        //    dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].ReadOnly = true;
                        //}
                        dgvproject.Rows[e.RowIndex].Cells["man_hour_quantity"].ReadOnly = strIsThree == "否" ? false : true;
                        if (strIsThree == "是")
                        {
                            string strOId = CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["whours_id"].Value);
                            if (!string.IsNullOrEmpty(strOId))
                            {
                                DataTable dot = DBHelper.GetTable("", "v_workhours_users", "*", "whours_id='" + strOId + "'", "", "");
                                if (dot.Rows.Count > 0)
                                {
                                    DataRow dpr = dot.Rows[0];
                                    dgvproject.Rows[e.RowIndex].Cells["man_hour_quantity"].Value = CommonCtrl.IsNullToString(dpr["whours_num_a"]);
                                    string strONum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["whours_num_a"])) ? CommonCtrl.IsNullToString(dpr["whours_num_a"]) : "0";
                                    dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value = CommonCtrl.IsNullToString(dpr["quota_price"]);
                                    dgvproject.Rows[e.RowIndex].Cells["sum_money_goods"].Value = Convert.ToString(Convert.ToDecimal(strONum) * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["quota_price"])) ? CommonCtrl.IsNullToString(dpr["quota_price"]) : "0"));
                                }
                            }
                        }
                    }
                    dgvproject.Rows[e.RowIndex].Cells["man_hour_quantity"].Value = ControlsConfig.SetNewValue(dgvproject.Rows[e.RowIndex].Cells["man_hour_quantity"].Value, 1);
                    dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value = ControlsConfig.SetNewValue(dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value, 2);
                    dgvproject.Rows[e.RowIndex].Cells["sum_money_goods"].Value = ControlsConfig.SetNewValue(dgvproject.Rows[e.RowIndex].Cells["sum_money_goods"].Value, 2);
                    dgvproject.Rows[e.RowIndex].Cells["member_price"].Value = ControlsConfig.SetNewValue(dgvproject.Rows[e.RowIndex].Cells["member_price"].Value, 2);
                    dgvproject.Rows[e.RowIndex].Cells["member_sum_money"].Value = ControlsConfig.SetNewValue(dgvproject.Rows[e.RowIndex].Cells["member_sum_money"].Value, 2);
                }
                decimal dcMoney = 0;
                for (int i = 0; i < dgvproject.Rows.Count; i++)
                {
                    string strMoney = CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["sum_money_goods"].Value);
                    if (!string.IsNullOrEmpty(strMoney))
                    {
                        dcMoney += Convert.ToDecimal(strMoney);
                    }
                }
                txtPAssess.Caption = dcMoney.ToString();
                txtTotal.Caption = (Convert.ToDecimal(txtLAssess.Caption = !string.IsNullOrEmpty(txtLAssess.Caption) ? txtLAssess.Caption : "0") + Convert.ToDecimal(txtOAssess.Caption = !string.IsNullOrEmpty(txtOAssess.Caption) ? txtOAssess.Caption : "0") + Convert.ToDecimal(txtPAssess.Caption)).ToString();
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
        }

        #endregion

        #region 维修用料信息读取
        private void dgvMaterials_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int intMrows = e.RowIndex;//当前行的索引
                frmParts frmPart = new frmParts();
                DialogResult result = frmPart.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string strPId = frmPart.PartsID;
                    DataTable dpt = DBHelper.GetTable("", "v_parts_chooser", "*", " parts_id='" + strPId + "'", "", "");
                    if (dpt.Rows.Count > 0)
                    {
                        DataRow dpr = dpt.Rows[0];
                        if (listMater.Contains(CommonCtrl.IsNullToString(dpr["ser_parts_code"])))
                        {
                            if (MessageBoxEx.Show("已添加此用料,是否继续添加?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                            {
                                return;
                            }
                        }
                        for (int i = 0; i <= intMrows; i++)
                        {
                            if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["parts_code"].Value)))
                            {
                                dgvMaterials.Rows[i].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dpr["ser_parts_code"]);
                                dgvMaterials.Rows[i].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dpr["parts_name"]);
                                dgvMaterials.Rows[i].Cells["norms"].Value = CommonCtrl.IsNullToString(dpr["model"]);
                                dgvMaterials.Rows[i].Cells["unit"].Value = CommonCtrl.IsNullToString(dpr["unit"]);
                                dgvMaterials.Rows[i].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dpr["is_import"]) == "1" ? "是" : "否";
                                dgvMaterials.Rows[i].Cells["quantity"].Value = "1.00";
                                dgvMaterials.Rows[i].Cells["unit_price"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["ref_out_price"])) ? CommonCtrl.IsNullToString(dpr["ref_out_price"]) : "0";
                                string strPzk = !string.IsNullOrEmpty(txtMemberLZk.Caption) ? txtMemberLZk.Caption : "100";
                                #region 验证是否存在特殊配件
                                string strMdic = DBHelper.GetSingleValue("获取特殊配件折扣", "tb_CustomerSer_member_setInfo_parts", "parts_discount", "setInfo_id='" + strSetInfoid + "' and parts_id='" + strPId + "' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", "");
                                if (!string.IsNullOrEmpty(strMdic))
                                {
                                    strPzk = strMdic;
                                }
                                #endregion
                                //会员折扣
                                dgvMaterials.Rows[i].Cells["Mmember_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk));
                                string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["quantity"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["quantity"].Value) : "0";
                                string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["unit_price"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["unit_price"].Value) : "0";
                                //会员单价
                                dgvMaterials.Rows[i].Cells["Mmember_price"].Value = Convert.ToString(Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * (Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["Mmember_discount"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["Mmember_discount"].Value) : "0") / 100));
                                dgvMaterials.Rows[i].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dpr["drawing_num"]);
                                dgvMaterials.Rows[i].Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dpr["remark"]);
                                dgvMaterials.Rows[i].Cells["vehicle_brand"].Value = CommonFuncCall.GetCarTypeForMa((CommonCtrl.IsNullToString(dpr["ser_parts_code"])));
                                dgvMaterials.Rows[i].Cells["parts_id"].Value = frmPart.PartsID;
                                dgvMaterials.Rows[i].Cells["Mthree_warranty"].Value = "否";
                                dgvMaterials.Rows[i].Cells["sum_money"].Value = Convert.ToString(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["Mmember_discount"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["Mmember_discount"].Value) : "0") / 100);
                                dgvMaterials.Rows[i].Cells["Mdata_source"].Value = CommonCtrl.IsNullToString(dpr["data_source"]);
                                dgvMaterials.Rows[i].Cells["Mthree_warranty"].ReadOnly = CommonCtrl.IsNullToString(dpr["data_source"]) == Convert.ToInt32(DataSources.EnumDataSources.SELFBUILD).ToString() ? true : false;
                                dgvMaterials.Rows.Add(1);
                                break;
                            }
                        }
                    }
                }
                foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
                {
                    string strPCode = CommonCtrl.IsNullToString(dgvr.Cells["parts_code"].Value);
                    if (!string.IsNullOrEmpty(strPCode) && !listMater.Contains(strPCode))
                    {
                        listMater.Add(strPCode);
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
        }

        #region 计算维修用料估计
        /// <summary>
        /// 数量、原始单价发生改变时金额也跟着改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvMaterials_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            string strIsThree = CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mthree_warranty"].Value);
            if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["parts_name"].Value)))
            {
                if (e.ColumnIndex == 7 || e.ColumnIndex == 8)
                {
                    //if (strIsThree == "否")
                    //{
                    ControlsConfig.SetCellsValue(dgvMaterials, e.RowIndex, "quantity,unit_price");
                    string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value)) ? dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value.ToString() : "0";
                    string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value)) ? dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value.ToString() : "0";
                    string strzk = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value)) ? dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value.ToString() : "0";
                    dgvMaterials.Rows[e.RowIndex].Cells["Mmember_price"].Value = Convert.ToString(Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(strzk.Length > 0 ? strzk : "0") / 100);
                    if (CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_price"].Value) != "0")
                    {
                        strzk = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value) : "0";
                        dgvMaterials.Rows[e.RowIndex].Cells["sum_money"].Value = Convert.ToString(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(strzk) / 100);
                    }
                    else
                    {
                        dgvMaterials.Rows[e.RowIndex].Cells["sum_money"].Value = Convert.ToString(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney));
                    }
                    //}
                }
                if (e.ColumnIndex == 3)
                {
                    if (strIsThree == "否")
                    {
                        dgvMaterials.Rows[e.RowIndex].Cells["quantity"].ReadOnly = false;
                        dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].ReadOnly = false;
                    }
                    else if (strIsThree == "是")
                    {
                        dgvMaterials.Columns["quantity"].ReadOnly = false;
                        dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].ReadOnly = true;
                        string strMId = CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["parts_id"].Value);
                        DataTable dpt = DBHelper.GetTable("", "v_parts_chooser", "*", " parts_id='" + strMId + "'", "", "");
                        if (dpt.Rows.Count > 0)
                        {
                            DataRow dpr = dpt.Rows[0];
                            dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value = CommonCtrl.IsNullToString(dpr["ref_out_price"]);
                            string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value) : "0";
                            string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value) : "0";
                            dgvMaterials.Rows[e.RowIndex].Cells["sum_money"].Value = Convert.ToString(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney));
                        }
                    }
                }
                dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value = ControlsConfig.SetNewValue(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value, 2);
                dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value = ControlsConfig.SetNewValue(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value, 2);
                dgvMaterials.Rows[e.RowIndex].Cells["sum_money"].Value = ControlsConfig.SetNewValue(dgvMaterials.Rows[e.RowIndex].Cells["sum_money"].Value, 2);
                dgvMaterials.Rows[e.RowIndex].Cells["Mmember_price"].Value = ControlsConfig.SetNewValue(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_price"].Value, 2);
            }
            decimal dcMoney = 0;
            for (int i = 0; i < dgvMaterials.Rows.Count; i++)
            {
                string strMoney = CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["sum_money"].Value);

                if (!string.IsNullOrEmpty(strMoney))
                {
                    dcMoney += Convert.ToDecimal(strMoney);
                }
            }
            txtLAssess.Caption = dcMoney.ToString();
            txtTotal.Caption = (Convert.ToDecimal(txtPAssess.Caption = !string.IsNullOrEmpty(txtPAssess.Caption) ? txtPAssess.Caption : "0") + Convert.ToDecimal(txtOAssess.Caption = !string.IsNullOrEmpty(txtOAssess.Caption) ? txtOAssess.Caption : "0") + Convert.ToDecimal(txtLAssess.Caption)).ToString();
        }
        #endregion

        #endregion

        #region 服务顾问选择器（人员选择器）
        private void txtAdvisor_ChooserClick(object sender, EventArgs e)
        {
            try
            {
                frmPersonnelSelector perSelector = new frmPersonnelSelector();
                DialogResult result = perSelector.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtAdvisor.Text = perSelector.strPersonName;
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
        }
        #endregion

        #region 右键添加维修项目
        private void addProject_Click(object sender, EventArgs e)
        {
            try
            {
                int intProws = dgvproject.CurrentRow.Index;//当前行的索引
                frmWorkHours frmHours = new frmWorkHours();
                DialogResult result = frmHours.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (listProject.Contains(frmHours.strProjectNum) && frmHours.strWhoursType != "1")
                    {
                        MessageBoxEx.Show("此维修项目已存在，请选择其他维修项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    for (int i = 0; i <= intProws; i++)
                    {
                        if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["item_no"].Value)))
                        {
                            dgvproject.Rows[i].Cells["item_no"].Value = frmHours.strProjectNum;
                            dgvproject.Rows[i].Cells["item_type"].Value = GetDicName(frmHours.strRepairType);
                            dgvproject.Rows[i].Cells["item_name"].Value = frmHours.strProjectName;
                            dgvproject.Rows[i].Cells["man_hour_norm_unitprice"].Value = frmHours.strQuotaPrice;
                            dgvproject.Rows[i].Cells["remarks"].Value = frmHours.strRemark;
                            dgvproject.Rows[i].Cells["man_hour_type"].Value = frmHours.strWhoursType == "1" ? "工时" : "定额";
                            dgvproject.Rows[i].Cells["man_hour_norm_unitprice"].ReadOnly = CommonCtrl.IsNullToString(frmHours.strWhoursType) == "1" ? true : false;
                            string strPzk = !string.IsNullOrEmpty(txtMemberPZk.Caption) ? txtMemberPZk.Caption : "100";
                            #region 验证是否存在特殊项目
                            string strPdic = DBHelper.GetSingleValue("获取特殊项目折扣", "tb_CustomerSer_member_setInfo_projrct", "service_project_discount", "setInfo_id='" + strSetInfoid + "' and service_project_id='" + frmHours.strWhours_id + "' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", "");
                            if (!string.IsNullOrEmpty(strPdic))
                            {
                                strPzk = strPdic;
                            }
                            #endregion
                            //会员折扣
                            dgvproject.Rows[i].Cells["member_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk));
                            //会员工时费
                            dgvproject.Rows[i].Cells["member_price"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strQuotaPrice) ? frmHours.strQuotaPrice : "0") * (Convert.ToDecimal(strPzk) / 100));
                            if (CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value) != "0")
                            {
                                //折扣额
                                dgvproject.Rows[i].Cells["member_sum_money"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strQuotaPrice) ? frmHours.strQuotaPrice : "0") - Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value) : "0"));
                            }
                            else
                            {
                                dgvproject.Rows[i].Cells["member_sum_money"].Value = "0";
                            }
                            dgvproject.Rows[i].Cells["whours_id"].Value = frmHours.strWhours_id;
                            dgvproject.Rows[i].Cells["man_hour_quantity"].Value = frmHours.strWhoursNum;
                            dgvproject.Rows[i].Cells["sum_money_goods"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strWhoursNum) ? frmHours.strWhoursNum : "0") * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["member_price"].Value) : "0"));
                            dgvproject.Rows[i].Cells["three_warranty"].Value = "否";
                            dgvproject.Rows[i].Cells["data_source"].Value = frmHours.strData_source;
                            dgvproject.Rows[i].Cells["three_warranty"].ReadOnly = frmHours.strData_source == Convert.ToInt32(DataSources.EnumDataSources.SELFBUILD).ToString() ? true : false;
                            dgvproject.Rows.Add(1);
                            break;
                        }
                    }
                }
                foreach (DataGridViewRow dgvr in dgvproject.Rows)
                {
                    string strPCode = CommonCtrl.IsNullToString(dgvr.Cells["item_no"].Value);
                    if (!string.IsNullOrEmpty(strPCode) && !listProject.Contains(strPCode))
                    {
                        listProject.Add(strPCode);
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
        }
        #endregion

        #region 右键删除维修项目
        private void deleteProject_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvproject.RowCount; i++)
                {
                    DataGridViewRow dgvr = dgvproject.Rows[i];
                    object isCheck = dgvr.Cells["colCheck"].Value;
                    //将选中的删除
                    if (isCheck != null && (bool)isCheck)
                    {
                        string strItemId = CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["item_id"].Value);
                        string strItemNo = CommonCtrl.IsNullToString(dgvproject.Rows[i].Cells["item_no"].Value);
                        if (!string.IsNullOrEmpty(strItemNo))
                        {
                            if (!string.IsNullOrEmpty(strItemId))
                            {
                                List<string> listField = new List<string>();
                                Dictionary<string, string> comField = new Dictionary<string, string>();
                                listField.Add(strItemId);
                                comField.Add("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.DELETED).ToString());
                                DBHelper.BatchUpdateDataByIn("删除维修项目信息", "tb_maintain_item", comField, "item_id", listField.ToArray());
                                dgvproject.Rows.RemoveAt(i--);
                            }
                            else
                            {
                                dgvproject.Rows.RemoveAt(i--);
                            }
                            listProject.Remove(strItemNo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
        }
        #endregion

        #region 右键添加用料
        private void addParts_Click(object sender, EventArgs e)
        {
            try
            {
                int intMrows = dgvMaterials.CurrentRow.Index;//当前行的索引
                frmParts frmPart = new frmParts();
                DialogResult result = frmPart.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string strPId = frmPart.PartsID;
                    DataTable dpt = DBHelper.GetTable("", "v_parts_chooser", "*", " parts_id='" + strPId + "'", "", "");
                    if (dpt.Rows.Count > 0)
                    {
                        DataRow dpr = dpt.Rows[0];
                        if (listMater.Contains(CommonCtrl.IsNullToString(dpr["ser_parts_code"])))
                        {
                            if (MessageBoxEx.Show("已添加此用料,是否继续添加?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                            {
                                return;
                            }
                        }
                        for (int i = 0; i <= intMrows; i++)
                        {
                            if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["parts_code"].Value)))
                            {
                                dgvMaterials.Rows[i].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dpr["ser_parts_code"]);
                                dgvMaterials.Rows[i].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dpr["parts_name"]);
                                dgvMaterials.Rows[i].Cells["norms"].Value = CommonCtrl.IsNullToString(dpr["model"]);
                                dgvMaterials.Rows[i].Cells["unit"].Value = CommonCtrl.IsNullToString(dpr["unit"]);
                                dgvMaterials.Rows[i].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dpr["is_import"]) == "1" ? "是" : "否";
                                dgvMaterials.Rows[i].Cells["quantity"].Value = "1.00";
                                dgvMaterials.Rows[i].Cells["unit_price"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["ref_out_price"])) ? CommonCtrl.IsNullToString(dpr["ref_out_price"]) : "0";
                                string strPzk = !string.IsNullOrEmpty(txtMemberLZk.Caption) ? txtMemberLZk.Caption : "100";
                                #region 验证是否存在特殊配件
                                string strMdic = DBHelper.GetSingleValue("获取特殊配件折扣", "tb_CustomerSer_member_setInfo_parts", "parts_discount", "setInfo_id='" + strSetInfoid + "' and parts_id='" + strPId + "' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", "");
                                if (!string.IsNullOrEmpty(strMdic))
                                {
                                    strPzk = strMdic;
                                }
                                #endregion
                                //会员折扣
                                dgvMaterials.Rows[i].Cells["Mmember_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk));
                                string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["quantity"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["quantity"].Value) : "0";
                                string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["unit_price"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["unit_price"].Value) : "0";
                                //会员单价
                                dgvMaterials.Rows[i].Cells["Mmember_price"].Value = Convert.ToString(Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * (Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["Mmember_discount"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["Mmember_discount"].Value) : "0") / 100));
                                dgvMaterials.Rows[i].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dpr["drawing_num"]);
                                dgvMaterials.Rows[i].Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dpr["remark"]);
                                dgvMaterials.Rows[i].Cells["vehicle_brand"].Value = CommonFuncCall.GetCarTypeForMa((CommonCtrl.IsNullToString(dpr["ser_parts_code"])));
                                dgvMaterials.Rows[i].Cells["parts_id"].Value = frmPart.PartsID;
                                dgvMaterials.Rows[i].Cells["Mthree_warranty"].Value = "否";
                                dgvMaterials.Rows[i].Cells["sum_money"].Value = Convert.ToString(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["Mmember_discount"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["Mmember_discount"].Value) : "0") / 100);
                                dgvMaterials.Rows[i].Cells["Mdata_source"].Value = CommonCtrl.IsNullToString(dpr["data_source"]);
                                dgvMaterials.Rows[i].Cells["Mthree_warranty"].ReadOnly = CommonCtrl.IsNullToString(dpr["data_source"]) == Convert.ToInt32(DataSources.EnumDataSources.SELFBUILD).ToString() ? true : false;
                                dgvMaterials.Rows.Add(1);
                                break;
                            }
                        }
                    }
                }
                foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
                {
                    string strPCode = CommonCtrl.IsNullToString(dgvr.Cells["parts_code"].Value);
                    if (!string.IsNullOrEmpty(strPCode) && !listMater.Contains(strPCode))
                    {
                        listMater.Add(strPCode);
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
        }
        #endregion

        #region 右键删除用料
        private void deleteParts_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvMaterials.RowCount; i++)
                {
                    DataGridViewRow dgvr = dgvMaterials.Rows[i];
                    object isCheck = dgvr.Cells["Mcheck"].Value;
                    //将选中的删除
                    if (isCheck != null && (bool)isCheck)
                    {
                        string strItemId = CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["material_id"].Value);
                        string strPartCode = CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["parts_code"].Value);
                        if (!string.IsNullOrEmpty(strItemId))
                        {
                            List<string> listField = new List<string>();
                            Dictionary<string, string> comField = new Dictionary<string, string>();
                            listField.Add(strItemId);
                            comField.Add("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.DELETED).ToString());
                            DBHelper.BatchUpdateDataByIn("删除维修用料", "tb_maintain_material_detail", comField, "material_id", listField.ToArray());
                            dgvMaterials.Rows.RemoveAt(i--);
                        }
                        else
                        {
                            dgvMaterials.Rows.RemoveAt(i--);
                        }
                        listMater.Remove(strPartCode);
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);

            }
        }
        #endregion     

        #region 点击页签上的叉号提示是否关闭当前窗体
        /// <summary>
        /// 点击页签上的叉号提示是否关闭当前窗体
        /// </summary>
        /// <returns></returns>
        public override bool CloseMenu()
        {
            if (isAutoClose)
            {
                return true;
            }
            if (MessageBoxEx.Show("确定要关闭当前窗体吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                return false;
            }
            else
            {
                if (!string.IsNullOrEmpty(strBefore_orderId))
                {
                    List<SQLObj> listSql = new List<SQLObj>();
                    UpdateMaintainInfo(listSql, strBefore_orderId, "", "0");
                    DBHelper.BatchExeSQLMultiByTrans("更新前置单据状体", listSql);
                }
            }
            return true;
        }
        #endregion

        #region 进场油量仅能输入1-100的数字 bug(11473 )
        private void txtMl_UserControlValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMl.Caption.Trim()))
            {
                if (Convert.ToDecimal(txtMl.Caption.Trim()) > 100 || Convert.ToDecimal(txtMl.Caption.Trim()) < 1)
                {
                    errorProvider1.Clear();
                    Validator.SetError(errorProvider1, txtMl, "进场油量仅能输入1-100的数字!");
                    return;
                }
                else
                {
                    errorProvider1.Clear();
                }
            }
        }
        #endregion

        #region 右键编辑维修项目
        private void editProject_Click(object sender, EventArgs e)
        {
            try
            {
                int intCurrentIndex = dgvproject.CurrentRow.Index;
                if (intCurrentIndex >= 0)
                {
                    string strItemNo = CommonCtrl.IsNullToString(dgvproject.Rows[intCurrentIndex].Cells["item_no"].Value);
                    if (!string.IsNullOrEmpty(strItemNo))
                    {
                        frmWorkHours frmHours = new frmWorkHours();
                        DialogResult result = frmHours.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            if (listProject.Contains(frmHours.strProjectNum) && frmHours.strWhoursType != "1")
                            {
                                MessageBoxEx.Show("此维修项目已存在，请选择其他维修项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            dgvproject.Rows[intCurrentIndex].Cells["item_no"].Value = frmHours.strProjectNum;
                            dgvproject.Rows[intCurrentIndex].Cells["item_type"].Value = GetDicName(frmHours.strRepairType);
                            dgvproject.Rows[intCurrentIndex].Cells["item_name"].Value = frmHours.strProjectName;
                            dgvproject.Rows[intCurrentIndex].Cells["man_hour_norm_unitprice"].Value = frmHours.strQuotaPrice;
                            dgvproject.Rows[intCurrentIndex].Cells["remarks"].Value = frmHours.strRemark;
                            dgvproject.Rows[intCurrentIndex].Cells["man_hour_type"].Value = frmHours.strWhoursType == "1" ? "工时" : "定额";
                            dgvproject.Rows[intCurrentIndex].Cells["man_hour_norm_unitprice"].ReadOnly = CommonCtrl.IsNullToString(frmHours.strWhoursType) == "1" ? true : false;
                            string strPzk = !string.IsNullOrEmpty(txtMemberPZk.Caption) ? txtMemberPZk.Caption : "100";
                            #region 验证是否存在特殊项目
                            string strPdic = DBHelper.GetSingleValue("获取特殊项目折扣", "tb_CustomerSer_member_setInfo_projrct", "service_project_discount", "setInfo_id='" + strSetInfoid + "' and service_project_id='" + frmHours.strWhours_id + "' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", "");
                            if (!string.IsNullOrEmpty(strPdic))
                            {
                                strPzk = strPdic;
                            }
                            #endregion
                            //会员折扣
                            dgvproject.Rows[intCurrentIndex].Cells["member_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk));
                            //会员工时费
                            dgvproject.Rows[intCurrentIndex].Cells["member_price"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strQuotaPrice) ? frmHours.strQuotaPrice : "0") * (Convert.ToDecimal(strPzk) / 100));
                            if (CommonCtrl.IsNullToString(dgvproject.Rows[intCurrentIndex].Cells["member_price"].Value) != "0")
                            {
                                //折扣额
                                dgvproject.Rows[intCurrentIndex].Cells["member_sum_money"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strQuotaPrice) ? frmHours.strQuotaPrice : "0") - Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[intCurrentIndex].Cells["member_price"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[intCurrentIndex].Cells["member_price"].Value) : "0"));
                            }
                            else
                            {
                                dgvproject.Rows[intCurrentIndex].Cells["member_sum_money"].Value = "0";
                            }
                            dgvproject.Rows[intCurrentIndex].Cells["whours_id"].Value = frmHours.strWhours_id;
                            dgvproject.Rows[intCurrentIndex].Cells["man_hour_quantity"].Value = frmHours.strWhoursNum;
                            dgvproject.Rows[intCurrentIndex].Cells["sum_money_goods"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(frmHours.strWhoursNum) ? frmHours.strWhoursNum : "0") * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[intCurrentIndex].Cells["member_price"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[intCurrentIndex].Cells["member_price"].Value) : "0"));
                            dgvproject.Rows[intCurrentIndex].Cells["three_warranty"].Value = "否";
                            dgvproject.Rows[intCurrentIndex].Cells["data_source"].Value = frmHours.strData_source;
                            dgvproject.Rows[intCurrentIndex].Cells["three_warranty"].ReadOnly = frmHours.strData_source == Convert.ToInt32(DataSources.EnumDataSources.SELFBUILD).ToString() ? true : false;
                            listProject.Remove(strItemNo);
                            foreach (DataGridViewRow dgvr in dgvproject.Rows)
                            {
                                string strPCode = CommonCtrl.IsNullToString(dgvr.Cells["item_no"].Value);
                                if (!string.IsNullOrEmpty(strPCode) && !listProject.Contains(strPCode))
                                {
                                    listProject.Add(strPCode);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }

        }
        #endregion

        #region 右键编辑维修用料(配件)
        private void editParts_Click(object sender, EventArgs e)
        {
            try
            {
                int intMrows = dgvMaterials.CurrentRow.Index;
                if (intMrows >= 0)
                {
                    string strPcode = CommonCtrl.IsNullToString(dgvMaterials.Rows[intMrows].Cells["parts_code"].Value);
                    if (!string.IsNullOrEmpty(strPcode))
                    {
                        frmParts frmPart = new frmParts();
                        DialogResult result = frmPart.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            string strPId = frmPart.PartsID;
                            DataTable dpt = DBHelper.GetTable("", "v_parts_chooser", "*", " parts_id='" + strPId + "'", "", "");
                            if (dpt.Rows.Count > 0)
                            {
                                DataRow dpr = dpt.Rows[0];
                                if (listMater.Contains(CommonCtrl.IsNullToString(dpr["ser_parts_code"])))
                                {
                                    if (MessageBoxEx.Show("已添加此用料,是否继续添加?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                                    {
                                        return;
                                    }
                                }
                                dgvMaterials.Rows[intMrows].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dpr["ser_parts_code"]);
                                dgvMaterials.Rows[intMrows].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dpr["parts_name"]);
                                dgvMaterials.Rows[intMrows].Cells["norms"].Value = CommonCtrl.IsNullToString(dpr["model"]);
                                dgvMaterials.Rows[intMrows].Cells["unit"].Value = CommonCtrl.IsNullToString(dpr["unit"]);
                                dgvMaterials.Rows[intMrows].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dpr["is_import"]) == "1" ? "是" : "否";
                                dgvMaterials.Rows[intMrows].Cells["quantity"].Value = "1.00";
                                dgvMaterials.Rows[intMrows].Cells["unit_price"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["ref_out_price"])) ? CommonCtrl.IsNullToString(dpr["ref_out_price"]) : "0";
                                string strPzk = !string.IsNullOrEmpty(txtMemberLZk.Caption) ? txtMemberLZk.Caption : "100";
                                #region 验证是否存在特殊配件
                                string strMdic = DBHelper.GetSingleValue("获取特殊配件折扣", "tb_CustomerSer_member_setInfo_parts", "parts_discount", "setInfo_id='" + strSetInfoid + "' and parts_id='" + strPId + "' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", "");
                                if (!string.IsNullOrEmpty(strMdic))
                                {
                                    strPzk = strMdic;
                                }
                                #endregion
                                //会员折扣
                                dgvMaterials.Rows[intMrows].Cells["Mmember_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk));
                                string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[intMrows].Cells["quantity"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[intMrows].Cells["quantity"].Value) : "0";
                                string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[intMrows].Cells["unit_price"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[intMrows].Cells["unit_price"].Value) : "0";
                                //会员单价
                                dgvMaterials.Rows[intMrows].Cells["Mmember_price"].Value = Convert.ToString(Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * (Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[intMrows].Cells["Mmember_discount"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[intMrows].Cells["Mmember_discount"].Value) : "0") / 100));
                                dgvMaterials.Rows[intMrows].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dpr["drawing_num"]);
                                dgvMaterials.Rows[intMrows].Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dpr["remark"]);
                                dgvMaterials.Rows[intMrows].Cells["vehicle_brand"].Value = CommonFuncCall.GetCarTypeForMa((CommonCtrl.IsNullToString(dpr["ser_parts_code"])));
                                dgvMaterials.Rows[intMrows].Cells["parts_id"].Value = frmPart.PartsID;
                                dgvMaterials.Rows[intMrows].Cells["Mthree_warranty"].Value = "否";
                                dgvMaterials.Rows[intMrows].Cells["sum_money"].Value = Convert.ToString(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[intMrows].Cells["Mmember_discount"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[intMrows].Cells["Mmember_discount"].Value) : "0") / 100);
                                dgvMaterials.Rows[intMrows].Cells["Mdata_source"].Value = CommonCtrl.IsNullToString(dpr["data_source"]);
                                dgvMaterials.Rows[intMrows].Cells["Mthree_warranty"].ReadOnly = CommonCtrl.IsNullToString(dpr["data_source"]) == Convert.ToInt32(DataSources.EnumDataSources.SELFBUILD).ToString() ? true : false;
                                listMater.Remove(strPcode);
                                foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
                                {
                                    string strPCode = CommonCtrl.IsNullToString(dgvr.Cells["parts_code"].Value);
                                    if (!string.IsNullOrEmpty(strPCode) && !listMater.Contains(strPCode))
                                    {
                                        listMater.Add(strPCode);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion
    }
}
