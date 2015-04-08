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
using SYSModel;
using Utility.Common;
using ServiceStationClient.ComponentUI;

namespace HXCPcClient.UCForm.RepairBusiness.RepairCallback
{
    /// <summary>
    /// 维修管理-维修返修单新增编辑
    /// Author：JC
    /// AddTime：2014.10.24
    /// </summary>
    public partial class UCRepairCallbackAddOrEdit : UCBase
    {
        #region 属性设置
        /// <summary>
        /// 父窗体
        /// </summary>
        public UCRepairCallbackManager uc;
        /// <summary>
        /// 窗体状态
        /// </summary>
        public WindowStatus wStatus;
        /// <summary>
        /// 维修返修单ID
        /// </summary>
        public string strId = string.Empty;
        /// <summary>
        /// 操作名称
        /// </summary>
        string opName = string.Empty;
        /// <summary>
        /// 附件操作Id值
        /// </summary>
        string strValue = string.Empty;
        /// <summary>
        /// 会员工时折扣
        /// </summary>
        string strMemberPZk = string.Empty;
        /// <summary>
        /// 会员用料折扣
        /// </summary>
        string strMemberLZk = string.Empty;
        /// <summary>
        /// 前置单据Id
        /// </summary>
       public string strBefore_orderId = string.Empty;
        /// <summary>
        /// 单据状态
        /// </summary>
       string strStatus = string.Empty;
        #endregion

        #region 初始化窗体
        public UCRepairCallbackAddOrEdit()
        {
            InitializeComponent();
            initializeData();
            BindDepartment();
            GetRepairNo();
            SetDgvAnchor();
            SetTopbuttonShow();
            base.CancelEvent += new ClickHandler(UCRepairCallbackAddOrEdit_CancelEvent);
            base.SaveEvent += new ClickHandler(UCRepairCallbackAddOrEdit_SaveEvent);
            base.SubmitEvent += new ClickHandler(UCRepairCallbackAddOrEdit_SubmitEvent);
            base.ImportEvent += new ClickHandler(UCRepairCallbackAddOrEdit_ImportEvent);
        }
        #endregion

        #region 顶部button显示设置
        /// <summary>
        ///  顶部button显示设置
        /// </summary>
        private void SetTopbuttonShow()
        {
            base.btnAdd.Visible = false;
            base.btnEdit.Visible = false;
            base.btnCopy.Visible = false;
            base.btnDelete.Visible = false;
            base.btnStatus.Visible = false;
            base.btnVerify.Visible = false;                  
            base.btnView.Visible = false;
            base.btnSync.Visible = false;
            base.btnConfirm.Visible = false;
            base.btnActivation.Visible = false;
        }
        #endregion

        #region 导入事件
        void UCRepairCallbackAddOrEdit_ImportEvent(object sender, EventArgs e)
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
        void UCRepairCallbackAddOrEdit_SubmitEvent(object sender, EventArgs e)
        {
            opName = "提交维修返修单信息";
            SaveOrSubmitMethod("提交", DataSources.EnumAuditStatus.SUBMIT);          
        }
        #endregion

        #region 保存事件
        void UCRepairCallbackAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            opName = "保存维修返修单信息";
            SaveOrSubmitMethod("保存", DataSources.EnumAuditStatus.DRAFT);
        }
        #endregion

        #region 保存、提交方法
        /// <summary>
        /// 保存、提交方法
        /// </summary>
        /// <param name="strMessage">提示信息</param>
        /// <param name="Estatus">单据操作状态</param>
        private void SaveOrSubmitMethod(string strMessage, DataSources.EnumAuditStatus Estatus)
        {
            try
            {
                #region 必要的判断
                if (string.IsNullOrEmpty(txtCarNO.Text.Trim()))
                {
                    MessageBoxEx.Show("车牌号不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (string.IsNullOrEmpty(txtCustomNO.Text.Trim()))
                {
                    MessageBoxEx.Show("客户编码不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (string.IsNullOrEmpty(txtCustomName.Caption.Trim()))
                {
                    MessageBoxEx.Show("客户名称不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (!ucAttr.CheckAttachment())
                {
                    return;
                }
                if (!string.IsNullOrEmpty(txtContactPhone.Caption.Trim()))//联系人手机
                {
                    if (!Validator.IsMobile(txtContactPhone.Caption.Trim()))
                    {
                        MessageBoxEx.Show("联系人手机号码格式错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (string.IsNullOrEmpty(txtDriver.Caption.Trim()))
                {
                    MessageBoxEx.Show("报修人不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (!string.IsNullOrEmpty(txtDriverPhone.Caption.Trim()))//报修人手机
                {
                    if (!Validator.IsMobile(txtDriverPhone.Caption.Trim()))
                    {
                        MessageBoxEx.Show("报修人手机号码格式错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    MessageBoxEx.Show("报修人手机不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion
                if (MessageBoxEx.Show("确认要" + strMessage + "吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
                List<SQLObj> listSql = new List<SQLObj>();
                SaveOrderInfo(listSql, Estatus);              
                SaveProjectData(listSql, strId);
                SaveMaterialsData(listSql, strId);
                SaveOtherData(listSql, strId);
                ucAttr.TableNameKeyValue = strValue;
                listSql.AddRange(ucAttr.AttachmentSql);
                if (DBHelper.BatchExeSQLMultiByTrans(opName, listSql))
                {
                    MessageBoxEx.Show("" + strMessage + "成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    uc.BindPageData();
                    deleteMenuByTag(this.Tag.ToString(), this.uc.Name);
                }
                else
                {
                    MessageBoxEx.Show("" + strMessage + "失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("" + strMessage + "失败！" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 维修返修单基本信息保存
        /// <summary>
        /// 维修返修单基本信息保存
        /// </summary>
        /// <param name="listSql">sql语句集合</param>
        /// <param name="status">操作方式</param>
        private void SaveOrderInfo(List<SQLObj> listSql, DataSources.EnumAuditStatus status)
        {
            SQLObj obj = new SQLObj();
            obj.cmdType = CommandType.Text;
            Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
            #region  基本信息
            dicParam.Add("reception_time", new ParamObj("reception_time", Common.LocalDateTimeToUtcLong(dtpReceptionTime.Value).ToString(), SysDbType.BigInt));//接待时间
            dicParam.Add("vehicle_no", new ParamObj("vehicle_no", txtCarNO.Text.Trim(), SysDbType.VarChar, 20));//车牌号
            dicParam.Add("vehicle_vin", new ParamObj("vehicle_vin", txtVIN.Caption.Trim(), SysDbType.VarChar, 40));//VIN
            dicParam.Add("engine_type", new ParamObj("engine_type", txtEngineNo.Caption.Trim(), SysDbType.VarChar, 40));//发动机号
            dicParam.Add("vehicle_model", new ParamObj("vehicle_model", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtCarType.Tag)) ? txtCarType.Tag : null, SysDbType.VarChar, 40));//车型
            dicParam.Add("vehicle_brand", new ParamObj("vehicle_brand", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobCarBrand.SelectedValue)) ? cobCarBrand.SelectedValue.ToString() : null, SysDbType.VarChar, 40));//车辆品牌
            dicParam.Add("driver_name", new ParamObj("driver_name", txtDriver.Caption.Trim(), SysDbType.VarChar, 20));//司机
            if (!string.IsNullOrEmpty(txtDriverPhone.Caption.Trim()))//司机手机
            {
                dicParam.Add("driver_mobile", new ParamObj("driver_mobile", txtDriverPhone.Caption.Trim(), SysDbType.VarChar, 15));//司机手机
            }
            else
            {
                dicParam.Add("driver_mobile", new ParamObj("driver_mobile", null, SysDbType.VarChar, 15));//司机手机
            }
            dicParam.Add("vehicle_color", new ParamObj("vehicle_color", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobColor.SelectedValue)) ? cobColor.SelectedValue.ToString() : null, SysDbType.VarChar, 40));//颜色
            dicParam.Add("customer_code", new ParamObj("customer_code", txtCustomNO.Text, SysDbType.VarChar, 40));//客户编码
            dicParam.Add("customer_name", new ParamObj("customer_name", txtCarNO.Text.Trim(), SysDbType.VarChar, 20));//客户名称
            dicParam.Add("linkman", new ParamObj("linkman", txtContact.Caption.Trim(), SysDbType.VarChar, 20));//联系人
            if (!string.IsNullOrEmpty(txtDriverPhone.Caption.Trim()))//联系人手机
            {
                dicParam.Add("linkman_mobile", new ParamObj("linkman_mobile", txtContactPhone.Caption.Trim(), SysDbType.VarChar, 15));//联系人手机 
            }
            else
            {
                dicParam.Add("linkman_mobile", new ParamObj("linkman_mobile", null, SysDbType.VarChar, 15));//联系人手机 
            }
            //经办人id
            dicParam.Add("responsible_opid", new ParamObj("responsible_opid", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobYHandle.SelectedValue)) ? cobYHandle.SelectedValue.ToString() : null, SysDbType.VarChar, 40));
            //经办人
            dicParam.Add("responsible_name", new ParamObj("responsible_name", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobYHandle.SelectedValue)) ? cobYHandle.SelectedText : null, SysDbType.VarChar, 40));
            //部门
            dicParam.Add("org_name", new ParamObj("org_name", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cboOrgId.SelectedValue)) ? cboOrgId.SelectedValue : null, SysDbType.VarChar, 40));
            dicParam.Add("enable_flag", new ParamObj("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString(), SysDbType.VarChar, 1));//信息状态（1|激活；2|作废；0|删除）
           
            dicParam.Add("repairer_name", new ParamObj("repairer_name", txtBlamePerson.Text.Trim(), SysDbType.VarChar, 40));//返修负责人
            dicParam.Add("repairer_id", new ParamObj("repairer_id", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtBlamePerson.Tag)) ? txtBlamePerson.Tag : null, SysDbType.VarChar, 40));//返修负责人Id
            dicParam.Add("mileage", new ParamObj("mileage", !string.IsNullOrEmpty(txtMil.Caption.Trim()) ? txtMil.Caption.Trim() : null, SysDbType.Decimal, 15));//行驶里程
           
            dicParam.Add("repair_describe", new ParamObj("repair_describe", txtRepairDesc.Caption.Trim(), SysDbType.VarChar, 200));//返修原因描述
            dicParam.Add("dispose_opinion", new ParamObj("dispose_opinion", txtOpinion.Caption.Trim(), SysDbType.VarChar, 200));//故障查询及处理意见
            dicParam.Add("dispose_result", new ParamObj("dispose_result", txtResult.Caption.Trim(), SysDbType.VarChar, 200));//处理结果
            if (status == DataSources.EnumAuditStatus.SUBMIT)//提交操作时生成单号
            {               
                dicParam.Add("repair_no", new ParamObj("repair_no", labReserveNoS.Text.Trim(), SysDbType.VarChar, 40));//单据编号 
                //单据状态
                dicParam.Add("document_status", new ParamObj("document_status", Convert.ToInt32(status).ToString(), SysDbType.VarChar, 40));
                if (!string.IsNullOrEmpty(strBefore_orderId))
                {
                    UpdateMaintainInfo(listSql, strBefore_orderId, labReserveInfo.Text.Trim(), "2");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(strStatus) && strStatus == Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString())
                {
                    dicParam.Add("repair_no", new ParamObj("repair_no", labReserveNoS.Text.Trim(), SysDbType.VarChar, 40));//单据编号 
                    //单据状态
                    dicParam.Add("document_status", new ParamObj("document_status", Convert.ToInt32(status).ToString(), SysDbType.VarChar, 40));
                }
                else
                {
                    //单据状态
                    dicParam.Add("document_status", new ParamObj("document_status", Convert.ToInt32(status).ToString(), SysDbType.VarChar, 40));
                    dicParam.Add("repair_no", new ParamObj("repair_no", null, SysDbType.VarChar, 40));//单据编号
                }  
                if (!string.IsNullOrEmpty(strBefore_orderId))
                {
                    opName = "更新前置单据导";
                    UpdateMaintainInfo(listSql, strBefore_orderId, "", "0");
                }
            }
            
            #endregion
            if (wStatus == WindowStatus.Add || wStatus == WindowStatus.Copy)
            {
                strId = Guid.NewGuid().ToString();
                dicParam.Add("repair_id", new ParamObj("repair_id", strId, SysDbType.VarChar, 40));//Id
                dicParam.Add("create_by", new ParamObj("create_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//创建人id（制单人） 
                dicParam.Add("create_name", new ParamObj("create_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//创建人
                dicParam.Add("create_time", new ParamObj("create_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));
                obj.sqlString = @"insert into tb_maintain_back_repair (reception_time,vehicle_no,vehicle_vin,engine_type,vehicle_model,vehicle_brand,driver_name,driver_mobile
                    ,vehicle_color,customer_code,customer_name,linkman,linkman_mobile,responsible_opid,responsible_name,org_name,enable_flag,document_status,repairer_name
                    ,repairer_id,mileage,repair_describe,dispose_opinion,dispose_result,repair_id,create_by,create_name,create_time,repair_no)
                 values (@reception_time,@vehicle_no,@vehicle_vin,@engine_type,@vehicle_model,@vehicle_brand,@driver_name,@driver_mobile
                    ,@vehicle_color,@customer_code,@customer_name,@linkman,@linkman_mobile,@responsible_opid,@responsible_name,@org_name,@enable_flag,@document_status,@repairer_name
                    ,@repairer_id,@mileage,@repair_describe,@dispose_opinion,@dispose_result,@repair_id,@create_by,@create_name,@create_time,@repair_no);";
            }
            else if (wStatus == WindowStatus.Edit)
            {
                dicParam.Add("repair_id", new ParamObj("repair_id", strId, SysDbType.VarChar, 40));//Id
                dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
                dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
                dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                obj.sqlString = @"update tb_maintain_back_repair set repair_no=@repair_no,reception_time=@reception_time,vehicle_no=@vehicle_no,vehicle_vin=@vehicle_vin,engine_type=@engine_type,vehicle_model=@vehicle_model,vehicle_brand=@vehicle_brand,driver_name=@driver_name,driver_mobile=@driver_mobile
                    ,vehicle_color=@vehicle_color,customer_code=@customer_code,customer_name=@customer_name,linkman=@linkman,linkman_mobile=@linkman_mobile,responsible_opid=@responsible_opid,responsible_name=@responsible_name,org_name=@org_name,enable_flag=@enable_flag,document_status=@document_status,repairer_name=@repairer_name
                    ,repairer_id=@repairer_id,mileage=@mileage,repair_describe=@repair_describe,dispose_opinion=@dispose_opinion,dispose_result=@dispose_result,update_by=@update_by,update_name=@update_name,update_time=@update_time where repair_id=@repair_id";
            }
            obj.Param = dicParam;
            listSql.Add(obj);
        }
        #endregion

        #region 维修项目信息保存
        private void SaveProjectData(List<SQLObj> listSql, string partID)
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
                        obj.sqlString = "insert into [tb_maintain_item] (item_id,maintain_id,item_no,item_type,item_name,man_hour_type,man_hour_quantity,man_hour_norm_unitprice,member_discount,member_price,member_sum_money,sum_money_goods,three_warranty,remarks,enable_flag) ";
                        obj.sqlString += " values (@item_id,@maintain_id,@item_no,@item_type,@item_name,@man_hour_type,@man_hour_quantity,@man_hour_norm_unitprice,@member_discount,@member_price,@member_sum_money,@sum_money_goods,@three_warranty,@remarks,@enable_flag);";
                    }
                    else
                    {
                        dicParam.Add("item_id", new ParamObj("item_id", dgvr.Cells["item_id"].Value, SysDbType.VarChar, 40));
                        opName = "更新接待单维修项目";
                        obj.sqlString = "update tb_maintain_item set item_no=@item_no,item_type=@item_type,item_name=@item_name,man_hour_type=@man_hour_type,man_hour_quantity=@man_hour_quantity,man_hour_norm_unitprice=@man_hour_norm_unitprice,member_discount=@member_discount,member_price=@member_price,";
                        obj.sqlString += " member_sum_money=@member_sum_money,sum_money_goods=@sum_money_goods,three_warranty=@three_warranty,remarks=@remarks,enable_flag=@enable_flag where item_id=@item_id";
                    }
                    obj.Param = dicParam;
                    listSql.Add(obj);
                }
            }
        }
        #endregion

        #region 维修用料信息保存
        private void SaveMaterialsData(List<SQLObj> listSql, string partID)
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
                    dicParam.Add("vehicle_brand", new ParamObj("vehicle_brand", dgvr.Cells["vehicle_brand"].Value, SysDbType.VarChar, 40));
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
                    string strPID = CommonCtrl.IsNullToString(dgvr.Cells["material_id"].Value);
                    if (strPID.Length == 0)
                    {
                        opName = "新增接待单维修用料";
                        strPID = Guid.NewGuid().ToString();
                        dicParam.Add("material_id", new ParamObj("material_id", strPID, SysDbType.VarChar, 40));
                        obj.sqlString = "insert into [tb_maintain_material_detail] (material_id,parts_code,parts_name,norms,unit,whether_imported,quantity,unit_price,sum_money,drawn_no,vehicle_brand,three_warranty,remarks,enable_flag,maintain_id) ";
                        obj.sqlString += " values (@material_id,@parts_code,@parts_name,@norms,@unit,@whether_imported,@quantity,@unit_price,@sum_money,@drawn_no,@vehicle_brand,@three_warranty,@remarks,@enable_flag,@maintain_id);";
                    }
                    else
                    {
                        dicParam.Add("material_id", new ParamObj("material_id", dgvr.Cells["material_id"].Value, SysDbType.VarChar, 40));
                        opName = "更新接待单维修用料";
                        obj.sqlString = "update tb_maintain_material_detail set parts_code=@parts_code,parts_name=@parts_name,norms=@norms,unit=@unit,whether_imported=@whether_imported,quantity=@quantity,unit_price=@unit_price,sum_money=@sum_money,";
                        obj.sqlString += "drawn_no=@drawn_no,vehicle_brand=@vehicle_brand, three_warranty=@three_warranty,remarks=@remarks,enable_flag=@enable_flag where material_id=@material_id";
                    }
                    obj.Param = dicParam;
                    listSql.Add(obj);
                }
            }
        }
        #endregion

        #region 其他收费项目信息保存
        //其他收费项目信息保存sql语句
        private void SaveOtherData(List<SQLObj> listSql, string partID)
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
        #endregion      

        #region 取消事件
        void UCRepairCallbackAddOrEdit_CancelEvent(object sender, EventArgs e)
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
            deleteMenuByTag(this.Tag.ToString(), uc.Name);
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
                txtVIN.Caption = frmVehicle.strVIN;
                txtEngineNo.Caption = frmVehicle.strEngineNum;
                txtCarType.Text = GetDicName(frmVehicle.strModel);
                txtCarType.Tag = frmVehicle.strModel;
                cobCarBrand.SelectedValue = frmVehicle.strBrand;
                cobColor.SelectedValue = frmVehicle.strColor;
                txtCustomNO.Text = frmVehicle.strCustCode;
                txtCustomNO.Tag = frmVehicle.strCustId;
                txtCustomName.Caption = frmVehicle.strCustName;
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
                txtCarType.Text = frmModels.VMName;
                txtCarType.Tag = frmModels.VMID;
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
            frmCustomerInfo frmCInfo = new frmCustomerInfo();
            DialogResult result = frmCInfo.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtCustomNO.Text = frmCInfo.strCustomerNo;
                txtCustomNO.Tag = frmCInfo.strCustomerId;
                txtCustomName.Caption = frmCInfo.strCustomerName;
                txtContact.Caption = frmCInfo.strLegalPerson;
                #region 会员信息
                DataTable dct = DBHelper.GetTable("获取会员信息", "tb_customer", "member_number,member_class,accessories_discount,workhours_discount", " is_member='1' and cust_id='" + txtCustomNO.Tag + "'", "", "");
                if (dct.Rows.Count > 0)
                {
                    DataRow dcr = dct.Rows[0];                   
                    strMemberPZk = CommonCtrl.IsNullToString(dcr["workhours_discount"]);
                    strMemberLZk = CommonCtrl.IsNullToString(dcr["accessories_discount"]);
                }                
                #endregion
            }
        }
        #endregion

        #region 返修负责人选择器
        /// <summary>
        /// 返修负责人选择器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBlamePerson_ChooserClick(object sender, EventArgs e)
        {
            frmPersonnelSelector frmPInfo = new frmPersonnelSelector();
            DialogResult result = frmPInfo.ShowDialog();
            if (result == DialogResult.OK)
            {               
                txtBlamePerson.Text = frmPInfo.strPersonName;
                txtBlamePerson.Tag = frmPInfo.strUserId;
            }
        }
        #endregion

        #region 初始化车辆品牌、颜色信息
        /// <summary>
        /// 初始化车型、车辆品牌、颜色、维修付费方式、维修类别信息
        /// </summary>
        private void initializeData()
        {
            CommonCtrl.BindComboBoxByDictionarr(cobCarBrand, "sys_vehicle_brand", true);//绑定车型品牌
            CommonCtrl.BindComboBoxByDictionarr(cobColor, "sys_vehicle_color", true);//绑定颜色                    
        }
        #endregion

        #region 窗体Load事件
        private void UCRepairCallbackAddOrEdit_Load(object sender, EventArgs e)
        {
            dtpReceptionTime.Value = DateTime.Now;
            ControlsConfig.TextToDecimal(txtMil);
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

        #region 根据维修单ID获取信息，复制和编辑用
        /// <summary>
        /// 根据维修单ID获取信息，复制和编辑用
        /// </summary>
        private void BindData()
        {
            #region 基础信息
            string strWhere = string.Format("repair_id='{0}'", strId);
            strValue = strId;
            DataTable dt = DBHelper.GetTable("查询维修返修单", "tb_maintain_back_repair", "*", strWhere, "", "");
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            DataRow dr = dt.Rows[0];

            #region 车辆信息
            string strCtime = CommonCtrl.IsNullToString(dr["reception_time"]);
            if (!string.IsNullOrEmpty(strCtime))
            {
                long Rticks = Convert.ToInt64(strCtime);
                dtpReceptionTime.Value = Common.UtcLongToLocalDateTime(Rticks);//返修接待时间
            }
            if (wStatus == WindowStatus.Edit)
            {
                txtCarNO.Text = CommonCtrl.IsNullToString(dr["vehicle_no"]);//车牌号
                txtVIN.Caption = CommonCtrl.IsNullToString(dr["vehicle_vin"]);//VIN
                txtEngineNo.Caption = CommonCtrl.IsNullToString(dr["engine_type"]);//发动机号
                txtCarType.Text = GetDicName(CommonCtrl.IsNullToString(dr["vehicle_model"]));//车型          
                cobCarBrand.SelectedValue = CommonCtrl.IsNullToString(dr["vehicle_brand"]);//车辆品牌
                txtDriver.Caption = CommonCtrl.IsNullToString(dr["driver_name"]);//司机
                txtDriverPhone.Caption = CommonCtrl.IsNullToString(dr["driver_mobile"]);//司机手机
                cobColor.SelectedValue = CommonCtrl.IsNullToString(dr["vehicle_color"]);//颜色   
            }
            txtMil.Caption = CommonCtrl.IsNullToString(dr["mileage"]);//进站里程    

            #endregion

            #region 客户信息
            txtCustomNO.Text = CommonCtrl.IsNullToString(dr["customer_code"]);//客户编码
            txtCustomName.Caption = CommonCtrl.IsNullToString(dr["customer_name"]);//客户名称  
            txtCustomNO.Tag = CommonCtrl.IsNullToString(dr["customer_id"]);//客户关联id           
            DataTable dct = DBHelper.GetTable("获取会员信息", "tb_customer", "member_number,member_class,accessories_discount,workhours_discount", " is_member='1' and cust_id='" + txtCustomNO.Tag + "'", "", "");
            if (dct.Rows.Count > 0)
            {
                DataRow dcr = dct.Rows[0];
                strMemberPZk = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dcr["workhours_discount"])) ? (Convert.ToInt32(CommonCtrl.IsNullToString(dcr["workhours_discount"])) * 10).ToString() + "%" : "";
                strMemberLZk = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dcr["accessories_discount"])) ? (Convert.ToInt32(CommonCtrl.IsNullToString(dcr["accessories_discount"])) * 10).ToString() + "%" : "";
            }
            txtContact.Caption = CommonCtrl.IsNullToString(dr["linkman"]);//联系人
            txtContactPhone.Caption = CommonCtrl.IsNullToString(dr["linkman_mobile"]);//联系人手机
            #endregion

            #region 返修信息
            txtBlamePerson.Text = CommonCtrl.IsNullToString(dr["repairer_name"]);//返修负责人
            txtBlamePerson.Tag = CommonCtrl.IsNullToString(dr["repairer_id"]);//返修负责人Id
            txtMil.Caption = CommonCtrl.IsNullToString(dr["mileage"]);//进站里程             
            txtRepairDesc.Caption = CommonCtrl.IsNullToString(dr["repair_describe"]);//返修原因描述
            txtOpinion.Caption = CommonCtrl.IsNullToString(dr["dispose_opinion"]);//处理意见
            txtResult.Caption = CommonCtrl.IsNullToString(dr["dispose_result"]);//处理结果
            #endregion

            #region 顶部按钮设置
            if (wStatus == WindowStatus.Edit)
            {
                //labReserveNoS.Text = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["repair_no"])) ? CommonCtrl.IsNullToString(dr["repair_no"]) : labReserveNoS.Text;//返修单号
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["repair_no"])))
                {
                    labReserveNoS.Text = CommonCtrl.IsNullToString(dr["repair_no"]);//返修单号
                }
                else
                {
                    labReserveNoS.Visible = false;
                }
                strStatus = CommonCtrl.IsNullToString(dr["document_status"]);//单据状态
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
                else if (strStatus == Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString())
                {
                    //审核没通过时屏蔽审核按钮
                    base.btnVerify.Enabled = false;
                }
            }
            else
            {
                labReserveNoS.Visible = false;
            }
            #endregion

            #endregion

            #region 底部datagridview数据
            BindPData(strId);
            BindMData(strId);
            BindOData(strId);
            BindAData("tb_maintain_back_repair", strId);
            #endregion
        }
        #endregion

        #region 底部datagridview数据信息绑定

        #region 维修项目数据
        private void BindPData(string strOrderId)
        {

            //维修项目数据                
            DataTable dpt = DBHelper.GetTable("维修项目数据", "tb_maintain_item", "*", string.Format(" maintain_id='{0}'", strOrderId), "", "");
            if (dpt.Rows.Count > 0)
            {
                if (dpt.Rows.Count > dgvproject.Rows.Count)
                {
                    dgvproject.Rows.Add(dpt.Rows.Count - dgvproject.Rows.Count + 1);
                }
                for (int i = 0; i < dpt.Rows.Count; i++)
                {
                    DataRow dpr = dpt.Rows[i];

                    if (wStatus == WindowStatus.Edit)
                    {
                        dgvproject.Rows[i].Cells["item_id"].Value = CommonCtrl.IsNullToString(dpr["item_id"]);
                    }
                    dgvproject.Rows[i].Cells["three_warranty"].Value = CommonCtrl.IsNullToString(dpr["three_warranty"]) == "1" ? "是" : "否";
                    dgvproject.Rows[i].Cells["man_hour_type"].Value = CommonCtrl.IsNullToString(dpr["man_hour_type"]);
                    dgvproject.Rows[i].Cells["item_no"].Value = CommonCtrl.IsNullToString(dpr["item_no"]);
                    dgvproject.Rows[i].Cells["item_name"].Value = CommonCtrl.IsNullToString(dpr["item_name"]);
                    dgvproject.Rows[i].Cells["item_type"].Value = CommonCtrl.IsNullToString(dpr["item_type"]);
                    dgvproject.Rows[i].Cells["man_hour_quantity"].Value = CommonCtrl.IsNullToString(dpr["man_hour_quantity"]);
                    dgvproject.Rows[i].Cells["man_hour_norm_unitprice"].Value = CommonCtrl.IsNullToString(dpr["man_hour_norm_unitprice"]);
                    dgvproject.Rows[i].Cells["remarks"].Value = CommonCtrl.IsNullToString(dpr["remarks"]);
                    dgvproject.Rows[i].Cells["sum_money_goods"].Value = CommonCtrl.IsNullToString(dpr["sum_money_goods"]);
                    dgvproject.Rows[i].Cells["member_discount"].Value = CommonCtrl.IsNullToString(dpr["member_discount"]);
                    dgvproject.Rows[i].Cells["member_price"].Value = CommonCtrl.IsNullToString(dpr["member_price"]);
                    dgvproject.Rows[i].Cells["member_sum_money"].Value = CommonCtrl.IsNullToString(dpr["member_sum_money"]);

                }
            }
        }
        #endregion

        #region 维修用料数据
        private void BindMData(string strOrderId)
        {

            //维修用料数据
            DataTable dmt = DBHelper.GetTable("维修用料数据", "tb_maintain_material_detail", "*", string.Format(" maintain_id='{0}'", strOrderId), "", "");
            if (dmt.Rows.Count > 0)
            {
                if (dmt.Rows.Count > dgvMaterials.Rows.Count)
                {
                    dgvMaterials.Rows.Add(dmt.Rows.Count - dgvMaterials.Rows.Count + 1);
                }
                for (int i = 0; i < dmt.Rows.Count; i++)
                {
                    DataRow dmr = dmt.Rows[i];
                    if (wStatus == WindowStatus.Edit)
                    {
                        dgvMaterials.Rows[i].Cells["material_id"].Value = CommonCtrl.IsNullToString(dmr["material_id"]);
                    }
                    dgvMaterials.Rows[i].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dmr["parts_code"]);
                    dgvMaterials.Rows[i].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dmr["parts_name"]);
                    dgvMaterials.Rows[i].Cells["norms"].Value = CommonCtrl.IsNullToString(dmr["norms"]);
                    dgvMaterials.Rows[i].Cells["unit"].Value = CommonCtrl.IsNullToString(dmr["unit"]);
                    dgvMaterials.Rows[i].Cells["quantity"].Value = CommonCtrl.IsNullToString(dmr["quantity"]);
                    dgvMaterials.Rows[i].Cells["unit_price"].Value = CommonCtrl.IsNullToString(dmr["unit_price"]);
                    dgvMaterials.Rows[i].Cells["Mmember_discount"].Value = CommonCtrl.IsNullToString(dmr["member_discount"]);
                    dgvMaterials.Rows[i].Cells["Mmember_price"].Value = CommonCtrl.IsNullToString(dmr["member_price"]);
                    dgvMaterials.Rows[i].Cells["sum_money"].Value = CommonCtrl.IsNullToString(dmr["sum_money"]);
                    dgvMaterials.Rows[i].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dmr["drawn_no"]);
                    dgvMaterials.Rows[i].Cells["vehicle_brand"].Value = CommonCtrl.IsNullToString(dmr["vehicle_brand"]);
                    dgvMaterials.Rows[i].Cells["Mthree_warranty"].Value = CommonCtrl.IsNullToString(dmr["three_warranty"]) == "1" ? "是" : "否";
                    dgvMaterials.Rows[i].Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dmr["remarks"]);
                    dgvMaterials.Rows[i].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dmr["whether_imported"]) == "1" ? "是" : "否";
                    string strPzk = !string.IsNullOrEmpty(strMemberLZk) ? strMemberLZk : "10";
                    //会员折扣
                    dgvMaterials.Rows[i].Cells["Mmember_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk) * 10);
                    string strNum = dgvMaterials.Rows[i].Cells["quantity"].Value.ToString();
                    string strUMoney = dgvMaterials.Rows[i].Cells["unit_price"].Value != null ? dgvMaterials.Rows[i].Cells["unit_price"].Value.ToString() : "0";
                    //会员单价
                    dgvMaterials.Rows[i].Cells["Mmember_price"].Value = Convert.ToString(Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(dgvMaterials.Rows[i].Cells["Mmember_discount"].Value) / 10);
                }
            }
        }
        #endregion

        #region 其他项目收费数据
        private void BindOData(string strOrderId)
        {

            ////其他项目收费数据
            DataTable dot = DBHelper.GetTable("其他项目收费数据", "tb_maintain_other_toll", "*", string.Format(" maintain_id='{0}'", strOrderId), "", "");
            if (dot.Rows.Count > 0)
            {
                if (dot.Rows.Count > dgvOther.Rows.Count)
                {
                    dgvOther.Rows.Add(dot.Rows.Count - dgvOther.Rows.Count + 1);
                }
                for (int i = 0; i < dot.Rows.Count; i++)
                {
                    DataRow dor = dot.Rows[i];
                    if (wStatus == WindowStatus.Edit)
                    {
                        dgvOther.Rows[i].Cells["toll_id"].Value = CommonCtrl.IsNullToString(dor["toll_id"]);
                    }
                    dgvOther.Rows[i].Cells["Osum_money"].Value = CommonCtrl.IsNullToString(dor["sum_money"]);
                    dgvOther.Rows[i].Cells["Oremarks"].Value = CommonCtrl.IsNullToString(dor["remarks"]);
                    dgvOther.Rows[i].Cells["cost_types"].Value = CommonCtrl.IsNullToString(dor["cost_types"]);
                }
            }
        }
        #endregion

        #region 附件信息数据
        private void BindAData(string strTableName, string strOrderId)
        {
            //附件信息数据
            ucAttr.TableName = strTableName;
            ucAttr.TableNameKeyValue = strOrderId;
            ucAttr.BindAttachment();
        }
        #endregion

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

        #region 绑定部门
        /// <summary>
        /// 绑定部门
        /// </summary>
        private void BindDepartment()
        {
            DataTable dt = DBHelper.GetTable("", "tb_organization", "org_id,org_name", "", "", "");
            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem("", "全部"));
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new ListItem(dr["org_id"], dr["org_name"].ToString()));
            }
            cboOrgId.DataSource = list;
            cboOrgId.ValueMember = "Value";
            cboOrgId.DisplayMember = "Text";
        }
        #endregion

        #region 绑定经办人
        /// <summary>
        /// 绑定经办人
        /// </summary>
        /// <param name="strDepartId">部门Id</param>
        private void BindHandle(ComboBox cobox, string strDepartId = null)
        {
            string strWhere = !string.IsNullOrEmpty(strDepartId) ? string.Format("org_id='{0}'", strDepartId) : "";
            DataTable dt = DBHelper.GetTable("", "sys_user", "user_id,user_name", strWhere, "", "");
            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem("", "全部"));
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new ListItem(dr["user_id"], dr["user_name"].ToString()));
            }
            cobox.DataSource = list;
            cobox.ValueMember = "Value";
            cobox.DisplayMember = "Text";
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
            if (cboOrgId.SelectedValue == null)
            {
                return;
            }
            BindHandle(cobYHandle, cboOrgId.SelectedValue.ToString());
        }
        #endregion

        #region 生成维修单号
        /// <summary>
        /// 生成预约单号&创建人姓名
        /// </summary>
        private void GetRepairNo()
        {
            labReserveNoS.Text = CommonUtility.GetNewNo(SYSModel.DataSources.EnumProjectType.RepairCallback);
            labCreatePersonS.Text = HXCPcClient.GlobalStaticObj.UserName;
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
            ControlsConfig.NumberLimitdgv(dgvproject, new List<string>() { "Osum_money" });
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
            //dgvproject.Columns["man_hour_norm_unitprice"].ReadOnly = true;
            dgvproject.Columns["sum_money_goods"].ReadOnly = true;
            dgvproject.Columns["member_discount"].ReadOnly = true;
            dgvproject.Columns["member_price"].ReadOnly = true;
            dgvproject.Columns["member_sum_money"].ReadOnly = true;
            dgvproject.Columns["man_hour_type"].ReadOnly = true;
            //dgvproject.Columns["man_hour_quantity"].ReadOnly = true;
            ControlsConfig.NumberLimitdgv(dgvproject, new List<string>() { "man_hour_quantity" });
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
            ControlsConfig.NumberLimitdgv(dgvMaterials, new List<string>() { "quantity" });
            #endregion
        }
        #endregion

        #region 维修返修单据导入功能
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(strBefore_orderId))
            {
                List<SQLObj> listSql = new List<SQLObj>();
                UpdateMaintainInfo(listSql, strBefore_orderId, "", "0");
                DBHelper.BatchExeSQLMultiByTrans("更新前置单据状体", listSql);
            }
            UCRepairCallbackImport CallbackImport = new UCRepairCallbackImport();
            CallbackImport.uc = this;
            CallbackImport.ShowDialog();
        }
        #endregion

        #region 维修返修导入-根据前置单据绑定维修信息
        /// <summary>
        /// 维修返修导入-根据前置单据绑定维修信息
        /// </summary>
        public void BindSetmentData()
        {

            #region 基本信息
            DataTable dt = DBHelper.GetTable("查询维修单单", "tb_maintain_info", "*", " maintain_id ='" + strBefore_orderId + "'", "", "");
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            DataRow dr = dt.Rows[0];                
            txtCarNO.Text = CommonCtrl.IsNullToString(dr["vehicle_no"]);//车牌号
            txtVIN.Caption = CommonCtrl.IsNullToString(dr["vehicle_vin"]);//VIN
            txtEngineNo.Caption = CommonCtrl.IsNullToString(dr["engine_no"]);//发动机号
            txtCarType.Text = CommonCtrl.IsNullToString(dr["vehicle_model"]);//车型          
            cobCarBrand.SelectedValue = CommonCtrl.IsNullToString(dr["vehicle_brand"]);//车辆品牌
            txtDriver.Caption = CommonCtrl.IsNullToString(dr["driver_name"]);//司机
            txtDriverPhone.Caption = CommonCtrl.IsNullToString(dr["driver_mobile"]);//司机手机
            cobColor.SelectedValue = CommonCtrl.IsNullToString(dr["vehicle_color"]);//颜色           
            txtCustomNO.Text = CommonCtrl.IsNullToString(dr["customer_code"]);//客户编码
            txtCustomName.Caption = CommonCtrl.IsNullToString(dr["customer_name"]);//客户名称  
            txtCustomNO.Tag = CommonCtrl.IsNullToString(dr["customer_id"]);//客户关联id
            #region 会员信息
            DataTable dct = DBHelper.GetTable("获取会员信息", "tb_customer", "member_number,member_class,accessories_discount,workhours_discount", " is_member='1' and cust_id='" + txtCustomNO.Tag + "'", "", "");
            if (dct.Rows.Count > 0)
            {
                DataRow dcr = dct.Rows[0];
                strMemberPZk = CommonCtrl.IsNullToString(dcr["workhours_discount"]);
                strMemberLZk = CommonCtrl.IsNullToString(dcr["accessories_discount"]);
            }
            else
            {
                strMemberPZk = "0";
                strMemberLZk = "0";                
            }
            #endregion
            txtContact.Caption = CommonCtrl.IsNullToString(dr["linkman"]);//联系人
            txtContactPhone.Caption = CommonCtrl.IsNullToString(dr["link_man_mobile"]);//联系人手机  
            #endregion

            #region 底部datagridview数据
            BindPData(strBefore_orderId);
            BindMData(strBefore_orderId);
            BindOData(strBefore_orderId);
            BindAData("tb_maintain_reservation", strBefore_orderId);
            List<SQLObj> listSql = new List<SQLObj>();
            UpdateMaintainInfo(listSql, strBefore_orderId, "", "1");
            DBHelper.BatchExeSQLMultiByTrans("更新前置单据状体", listSql);
            #endregion
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

            SQLObj obj = new SQLObj();
            obj.cmdType = CommandType.Text;
            Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();

            dicParam.Add("maintain_id", new ParamObj("maintain_id", strReservId, SysDbType.VarChar, 40));
            if (status == "0")
            {
                //保存时，前置单据被释放               
                dicParam.Add("Import_status", new ParamObj("Import_status", "0", SysDbType.VarChar, 40));//开放
                obj.sqlString = "update tb_maintain_info set Import_status=@Import_status where maintain_id=@maintain_id";
               
            }
            else if (status == "1")
            {
                //导入时，前置单据被占用 
                dicParam.Add("Import_status", new ParamObj("Import_status", "1", SysDbType.VarChar, 40));//占用
                obj.sqlString = "update tb_maintain_info set Import_status=@Import_status where maintain_id=@maintain_id";
            }
            else if (status == "2")
            {
                //审核提交时，前置单据被锁定
                dicParam.Add("Import_status", new ParamObj("Import_status", "2", SysDbType.VarChar, 40));//锁定
                obj.sqlString = "update tb_maintain_info set Import_status=@Import_status where maintain_id=@maintain_id";
               
            }
            obj.Param = dicParam;
            listSql.Add(obj);
        }
        #endregion

        #region 维修项目信息读取
        private void dgvproject_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmWorkHours frmHours = new frmWorkHours();
            DialogResult result = frmHours.ShowDialog();
            if (result == DialogResult.OK)
            {
                dgvproject.Rows[e.RowIndex].Cells["item_no"].Value = frmHours.strProjectNum;
                dgvproject.Rows[e.RowIndex].Cells["item_type"].Value = GetDicName(frmHours.strRepairType);
                dgvproject.Rows[e.RowIndex].Cells["item_name"].Value = frmHours.strProjectName;
                dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value = frmHours.strQuotaPrice;
                dgvproject.Rows[e.RowIndex].Cells["remarks"].Value = frmHours.strRemark;
                dgvproject.Rows[e.RowIndex].Cells["man_hour_type"].Value = frmHours.strWhoursType == "1" ? "工时" : "定额";
                string strPzk = !string.IsNullOrEmpty(strMemberPZk) ? strMemberPZk : "10";
                //工时单价
                dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value = frmHours.strQuotaPrice;
                //会员折扣
                dgvproject.Rows[e.RowIndex].Cells["member_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk) * 10);
                //会员工时费
                dgvproject.Rows[e.RowIndex].Cells["member_price"].Value = Convert.ToString(Convert.ToDecimal(frmHours.strQuotaPrice) * (Convert.ToDecimal(strPzk) / 10));
                if (dgvproject.Rows[e.RowIndex].Cells["member_price"].Value.ToString() != "0")
                {
                    //折扣额
                    dgvproject.Rows[e.RowIndex].Cells["member_sum_money"].Value = Convert.ToString(Convert.ToDecimal(frmHours.strQuotaPrice) - Convert.ToDecimal(dgvproject.Rows[e.RowIndex].Cells["member_price"].Value));
                }
                else
                {
                    dgvproject.Rows[e.RowIndex].Cells["member_sum_money"].Value = "0";
                }
                dgvproject.Rows[e.RowIndex].Cells["OldItem_id"].Value = frmHours.strWhours_id;
                dgvproject.Rows[e.RowIndex].Cells["man_hour_quantity"].Value = frmHours.strWhoursNum;
                dgvproject.Rows[e.RowIndex].Cells["sum_money_goods"].Value = Convert.ToString(Convert.ToDecimal(frmHours.strWhoursNum) * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["member_price"].Value)) ? CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["member_price"].Value) : "0"));
                dgvproject.Rows.Add(1);
            }
        }
        /// <summary>
        /// 工时数量发生改变时货款也跟着改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>      
        private void dgvproject_CellValueChanged(object sender, DataGridViewCellEventArgs e)
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

                if (e.ColumnIndex == 5 || e.ColumnIndex == 6)
                {
                    //会员工时费
                    dgvproject.Rows[e.RowIndex].Cells["member_price"].Value = Convert.ToString(Convert.ToDecimal(dgvproject.Rows[e.RowIndex].Cells["member_discount"].Value) * Convert.ToDecimal(dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value) / 100);
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
                        dgvproject.Rows[e.RowIndex].Cells["sum_money_goods"].Value = Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value);
                    }
                }
                if (e.ColumnIndex == 11)
                {
                    if (CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_type"].Value) == "工时")
                    {
                        //工时时设置数量和金额均可修改
                        dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].ReadOnly = strIsThree == "否" ? false : true;
                    }
                    dgvproject.Rows[e.RowIndex].Cells["man_hour_quantity"].ReadOnly = strIsThree == "否" ? false : true;
                    if (strIsThree == "是")
                    {
                        string strOId = CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["OldItem_id"].Value);
                        if (!string.IsNullOrEmpty(strOId))
                        {
                            DataTable dot = DBHelper.GetTable("", "v_workhours_users", "*", "whours_id='" + strOId + "'", "", "");
                            if (dot.Rows.Count > 0)
                            {
                                DataRow dpr = dot.Rows[0];
                                dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value = CommonCtrl.IsNullToString(dpr["quota_price"]);
                                dgvproject.Rows[e.RowIndex].Cells["sum_money_goods"].Value = Convert.ToString(Convert.ToDecimal(strNum) * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["quota_price"])) ? CommonCtrl.IsNullToString(dpr["quota_price"]) : "0"));
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 维修用料信息读取
        private void dgvMaterials_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
                    dgvMaterials.Rows[e.RowIndex].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dpr["ser_parts_code"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dpr["parts_name"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["norms"].Value = CommonCtrl.IsNullToString(dpr["model"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["unit"].Value = CommonCtrl.IsNullToString(dpr["default_unit_name"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dpr["is_import"]) == "1" ? "是" : "否";
                    dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value = "1";
                    dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value = CommonCtrl.IsNullToString(dpr["highest_out_price"]);
                    string strPzk = !string.IsNullOrEmpty(strMemberLZk) ? strMemberLZk : "10";
                    //会员折扣
                    dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value = Convert.ToString(Convert.ToDecimal(strPzk) * 10);
                    string strNum = dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value.ToString();
                    string strUMoney = dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value != null ? dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value.ToString() : "0";
                    //会员单价
                    dgvMaterials.Rows[e.RowIndex].Cells["Mmember_price"].Value = Convert.ToString(Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value) / 10);
                    dgvMaterials.Rows[e.RowIndex].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dpr["drawing_num"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dpr["remark"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["vehicle_brand"].Value = CommonCtrl.IsNullToString(dpr["v_brand_name"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["M_Id"].Value = frmPart.PartsID;
                    dgvMaterials.Rows.Add(1);
                }
            }
        }
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
                if (e.ColumnIndex == 6 || e.ColumnIndex == 7)
                {
                    //if (strIsThree == "否")
                    //{
                    ControlsConfig.SetCellsValue(dgvMaterials, e.RowIndex, "quantity,unit_price");
                    string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value) : "0";
                    string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value) : "0";
                    string strzk = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value) : "0";
                    dgvMaterials.Rows[e.RowIndex].Cells["Mmember_price"].Value = Convert.ToString(Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * Convert.ToDecimal(strzk.Length > 0 ? strzk : "0") / 100);
                    if (CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_price"].Value) != "0")
                    {
                        strzk = CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mmember_discount"].Value);
                        string strSmoney = Convert.ToString(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney) * (Convert.ToDecimal(!string.IsNullOrEmpty(strzk)?strzk:"0") / 100));
                        dgvMaterials.Rows[e.RowIndex].Cells["sum_money"].Value = strSmoney;
                    }
                    else
                    {
                        string strMoney = Convert.ToString(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney));
                        if (strMoney == "0.0")
                        {
                            strMoney = "0";
                        }
                        dgvMaterials.Rows[e.RowIndex].Cells["sum_money"].Value = strMoney;
                    }
                    //}
                }
                if (e.ColumnIndex == 13)
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
                        string strMId = CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["M_Id"].Value);
                        DataTable dpt = DBHelper.GetTable("", "v_parts", "*", " parts_id='" + strMId + "'", "", "");
                        if (dpt.Rows.Count > 0)
                        {
                            DataRow dpr = dpt.Rows[0];
                            dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value = CommonCtrl.IsNullToString(dpr["highest_out_price"]);
                            string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value) : "0";
                            string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value) : "0";
                            dgvMaterials.Rows[e.RowIndex].Cells["sum_money"].Value = Convert.ToString(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney));
                        }
                    }
                }
            }
        }
        #endregion

        #region 其他收费项目数据设置
        private void dgvOther_CellValueChanged(object sender, DataGridViewCellEventArgs e)
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
            }
        }
        #endregion
    }
}
