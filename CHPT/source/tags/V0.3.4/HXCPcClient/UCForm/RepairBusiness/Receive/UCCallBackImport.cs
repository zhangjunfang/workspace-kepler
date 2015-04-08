using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using ServiceStationClient.ComponentUI;
using HXCPcClient.CommonClass;
using Utility.Common;
using SYSModel;

namespace HXCPcClient.UCForm.RepairBusiness.Receive
{
    /// <summary>
    /// 维修管理—维修接待（返修单导入）
    /// Author：JC
    /// AddTime：2014.10.9
    /// </summary>
    public partial class UCCallBackImport : FormChooser
    {
        #region 属性设置
        /// <summary>
        /// enable_flag 1未删除
        /// </summary>
        private string strWhere = string.Empty;
        /// <summary>
        /// 返修单单Id
        /// </summary>
        string strBackRepairId = string.Empty;
        /// <summary>
        /// 新维修单Id
        /// </summary>
        string strId = string.Empty;
        /// <summary>
        /// 父窗体
        /// </summary>
        public UCReceiveAddOrEdit uc;
        #endregion

        #region 初始化
        public UCCallBackImport()
        {
            InitializeComponent();
            this.AcceptButton = btnSubmit;
            dgvRData.ReadOnly = false;
            //dgvRData.Columns["colCheck"].HeaderText = "选择";
            dgvRData.Columns["repair_no"].ReadOnly = true;
            dgvRData.Columns["reception_time"].ReadOnly = true;
            dgvRData.Columns["customer_name"].ReadOnly = true;
            dgvRData.Columns["driver_mobile"].ReadOnly = true;      
            dgvRData.Columns["driver_name"].ReadOnly = true;
            dgvRData.Columns["vehicle_no"].ReadOnly = true;
            dtpReserveSTime.Value = DateTime.Now.AddMonths(-1);
            dtpReserveETime.Value = DateTime.Now;   
        }
        #endregion

        #region 清除事件
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCarNO.Caption = string.Empty;
            txtCustomName.Caption = string.Empty;
            txtDriverPhone.Caption = string.Empty;
            txtCarNO.Focus();
            dtpReserveSTime.Value = DateTime.Now.AddMonths(-1);
            dtpReserveETime.Value = DateTime.Now;
        }
        #endregion

        #region 查询事件
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                #region 事件选择判断
                if (dtpReserveSTime.Value > dtpReserveETime.Value)
                {
                    MessageBoxEx.Show("接待时间,开始时间不能大于结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion
                string strFiles = " repair_no,reception_time,customer_name,driver_mobile,driver_name,vehicle_no,repair_id";
                strWhere = string.Format("enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "' and document_status='" + Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString() + "' and import_status='" + Convert.ToInt32(DataSources.EnumImportStaus.OPEN).ToString() + "'");//enable_flag 1未删除
                
                if (!string.IsNullOrEmpty(txtCarNO.Text.Trim()))//车牌号
                {
                    strWhere += string.Format(" and  vehicle_no like '%{0}%'", txtCarNO.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtCustomName.Caption.Trim()))//客户名称
                {
                    strWhere += string.Format(" and  customer_name like '%{0}%'", txtCustomName.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtDriverPhone.Caption.Trim()))//司机手机
                {
                    strWhere += string.Format(" and  driver_mobile like '%{0}%'", txtDriverPhone.Caption.Trim());
                }
                strWhere += string.Format(" and reception_time BETWEEN {0} and {1}", Common.LocalDateTimeToUtcLong(dtpReserveSTime.Value.Date), Common.LocalDateTimeToUtcLong(dtpReserveETime.Value.Date.AddDays(1).AddMilliseconds(-1)));//接待日期

                DataTable dt = DBHelper.GetTable("返修单导入信息列表", "tb_maintain_back_repair", strFiles, strWhere, "", "order by repair_id desc");
                dgvRData.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region 导入事件
        private void btnImport_Click(object sender, EventArgs e)
        {
            List<SQLObj> listSql = new List<SQLObj>();
            List<string> listField = new List<string>();
            string opName = "预约单导入到接待单";
            #region 获取信息Id值
            string strRId = string.Empty;
            foreach (DataGridViewRow dr in dgvRData.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {                   
                    strRId += dr.Cells["repair_id"].Value.ToString();
                    listField.Add(dr.Cells["repair_id"].Value.ToString());
                }
            }
            #endregion
            if (listField.Count <= 0)
            {
                MessageBoxEx.Show("请选择需要导入的数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            if (listField.Count > 1)
            {
                MessageBoxEx.Show("一次仅能导入1条数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            uc.strBefore_orderId = strRId;  //导入单据的Id值
            uc.strOrdersSouse = "2";
            uc.BindCallBackData();
            this.Close();
            //strRId = strRId.Substring(0, strRId.Length - 1);
            //ImportBackRepairData(listSql, strRId);
            //ImportPData(listSql, strId, strBackRepairId);
            //ImportMData(listSql, strId, strBackRepairId);
            //ImportOData(listSql, strId, strBackRepairId);
            //ImportAData(listSql, strId, strBackRepairId);
            //if (DBHelper.BatchExeSQLMultiByTrans(opName, listSql))
            //{
            //    MessageBoxEx.Show("导入成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
            //}
        }
        #endregion

        #region 返修单信息导入
        private void ImportBackRepairData(List<SQLObj> listSql, string strRId)
        {
            string strWOrderNo = CommonUtility.GetNewNo(SYSModel.DataSources.EnumProjectType.Repair);
            strId = string.Empty; ;
            DataTable dt = DBHelper.GetTable("查询返修单", "tb_maintain_back_repair", "*", " repair_id in (" + strRId + ")", "", "");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SQLObj obj = new SQLObj();
                obj.cmdType = CommandType.Text;
                Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                #region 基本信息
                DataRow row = dt.Rows[i];
                strBackRepairId = CommonCtrl.IsNullToString(row["reserv_id"]);
                dicParam.Add("maintain_no", new ParamObj("maintain_no", strWOrderNo, SysDbType.VarChar, 40));//维修单号
                dicParam.Add("reception_time", new ParamObj("reception_time", DateTime.UtcNow.Ticks, SysDbType.BigInt));//接待日期
                dicParam.Add("vehicle_no", new ParamObj("vehicle_no", CommonCtrl.IsNullToString(row["vehicle_no"]), SysDbType.VarChar, 40));//车牌号
                dicParam.Add("vehicle_vin", new ParamObj("vehicle_vin", CommonCtrl.IsNullToString(row["vehicle_vin"]), SysDbType.VarChar, 40));//VIN
                dicParam.Add("engine_no", new ParamObj("engine_no", CommonCtrl.IsNullToString(row["engine_type"]), SysDbType.VarChar, 40));//发动机号
                dicParam.Add("vehicle_model", new ParamObj("vehicle_model", CommonCtrl.IsNullToString(row["vehicle_model"]), SysDbType.VarChar, 40));//车型
                dicParam.Add("vehicle_brand", new ParamObj("vehicle_brand", CommonCtrl.IsNullToString(row["vehicle_brand"]), SysDbType.VarChar, 40));//车辆品牌
                dicParam.Add("driver_name", new ParamObj("driver_name", CommonCtrl.IsNullToString(row["driver_name"]), SysDbType.VarChar, 20));//司机                
                dicParam.Add("driver_mobile", new ParamObj("driver_mobile", CommonCtrl.IsNullToString(row["driver_mobile"]), SysDbType.VarChar, 15));//司机手机               
                dicParam.Add("vehicle_color", new ParamObj("vehicle_color", CommonCtrl.IsNullToString(row["vehicle_color"]), SysDbType.VarChar, 40));//颜色
                dicParam.Add("customer_code", new ParamObj("customer_code", CommonCtrl.IsNullToString(row["customer_code"]), SysDbType.VarChar, 40));//客户编码
                dicParam.Add("customer_name", new ParamObj("customer_name", CommonCtrl.IsNullToString(row["customer_name"]), SysDbType.VarChar, 40));//客户名称 
                dicParam.Add("customer_id", new ParamObj("customer_id", CommonCtrl.IsNullToString(row["customer_id"]), SysDbType.VarChar, 40));//客户关联id
                dicParam.Add("linkman", new ParamObj("linkman", CommonCtrl.IsNullToString(row["linkman"]), SysDbType.VarChar, 20));//联系人
                dicParam.Add("link_man_mobile", new ParamObj("link_man_mobile", CommonCtrl.IsNullToString(row["link_man_mobile"]), SysDbType.VarChar, 15));//联系人手机
                dicParam.Add("fault_describe", new ParamObj("fault_describe", CommonCtrl.IsNullToString(row["repair_describe"]), SysDbType.VarChar, 40));//故障描述
                dicParam.Add("info_status", new ParamObj("info_status", CommonCtrl.IsNullToString(row["document_status"]), SysDbType.VarChar, 40));//单据状态415A3ADF-96C5-495C-8C98-478AA489E0A9表示接待单
                dicParam.Add("enable_flag", new ParamObj("enable_flag", CommonCtrl.IsNullToString(row["enable_flag"]), SysDbType.VarChar, 1));//信息状态（1|激活；2|作废；0|删除） 
                strId = Guid.NewGuid().ToString();
                dicParam.Add("maintain_id", new ParamObj("maintain_id", strId, SysDbType.VarChar, 40));//新ID
                dicParam.Add("create_opid", new ParamObj("create_opid", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//创建人id（制单人） 
                dicParam.Add("create_name", new ParamObj("create_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//创建人
                dicParam.Add("create_time", new ParamObj("create_time", DateTime.UtcNow.Ticks, SysDbType.BigInt));//创建时间
                obj.sqlString = @"insert into [tb_maintain_info] (maintain_no,reception_time,vehicle_no,vehicle_vin,engine_no,vehicle_model,vehicle_brand 
                                ,driver_name,driver_mobile,vehicle_color,customer_name,customer_id,linkman,link_man_mobile,fault_describe,info_status
                                ,enable_flag,maintain_id,create_opid,create_name,create_time) 
                                values (@maintain_no,@reception_time,@vehicle_no,@vehicle_vin,@engine_no,@vehicle_model,@vehicle_brand 
                                ,@driver_name,@driver_mobile,@vehicle_color,@customer_name,@customer_id,@linkman,@link_man_mobile,@fault_describe,@info_status
                                ,@enable_flag,@maintain_id,@create_opid,@create_name,@create_time);";
                obj.Param = dicParam;
                listSql.Add(obj);
                #endregion
            }
        }
        #endregion

        #region 维修项目信息导入
        private void ImportPData(List<SQLObj> listSql, string strId, string strReservId)
        {
            #region 维修项目信息
            DataTable dpt = DBHelper.GetTable("维修项目数据", "tb_maintain_item", "*", string.Format(" maintain_id ='{0}'", strReservId), "", "");
            for (int j = 0; j < dpt.Rows.Count; j++)
            {
                SQLObj obj = new SQLObj();
                obj.cmdType = CommandType.Text;
                Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                DataRow prow = dpt.Rows[j];
                dicParam.Add("maintain_id", new ParamObj("maintain_id", strId, SysDbType.VarChar, 40));
                dicParam.Add("item_no", new ParamObj("item_no", CommonCtrl.IsNullToString(prow["item_no"]), SysDbType.VarChar, 40));
                dicParam.Add("item_type", new ParamObj("item_type", CommonCtrl.IsNullToString(prow["item_type"]), SysDbType.VarChar, 40));
                dicParam.Add("item_name", new ParamObj("item_name", CommonCtrl.IsNullToString(prow["item_name"]), SysDbType.VarChar, 40));
                dicParam.Add("man_hour_type", new ParamObj("man_hour_type", CommonCtrl.IsNullToString(prow["man_hour_type"]), SysDbType.VarChar, 40));
                dicParam.Add("man_hour_quantity", new ParamObj("man_hour_quantity", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(prow["man_hour_quantity"])) ? CommonCtrl.IsNullToString(prow["man_hour_quantity"]) : null, SysDbType.Decimal, 15));
                dicParam.Add("man_hour_norm_unitprice", new ParamObj("man_hour_norm_unitprice", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(prow["man_hour_norm_unitprice"])) ? CommonCtrl.IsNullToString(prow["man_hour_norm_unitprice"]) : null, SysDbType.Decimal, 15));
                dicParam.Add("member_discount", new ParamObj("member_discount", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(prow["member_discount"])) ? CommonCtrl.IsNullToString(prow["member_discount"]) : null, SysDbType.Decimal, 5));
                dicParam.Add("member_price", new ParamObj("member_price", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(prow["member_price"])) ? CommonCtrl.IsNullToString(prow["member_price"]) : null, SysDbType.Decimal, 15));
                dicParam.Add("member_sum_money", new ParamObj("member_sum_money", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(prow["member_sum_money"])) ? CommonCtrl.IsNullToString(prow["member_sum_money"]) : null, SysDbType.Decimal, 15));
                dicParam.Add("sum_money_goods", new ParamObj("sum_money_goods", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(prow["sum_money_goods"])) ? CommonCtrl.IsNullToString(prow["sum_money_goods"]) : null, SysDbType.Decimal, 15));
                dicParam.Add("three_warranty", new ParamObj("three_warranty", CommonCtrl.IsNullToString(prow["three_warranty"]), SysDbType.VarChar, 2));
                dicParam.Add("remarks", new ParamObj("remarks", CommonCtrl.IsNullToString(prow["remarks"]), SysDbType.VarChar, 200));
                dicParam.Add("enable_flag", new ParamObj("enable_flag", "1", SysDbType.VarChar, 1));
                dicParam.Add("item_id", new ParamObj("item_id", Guid.NewGuid().ToString(), SysDbType.VarChar, 40));
                obj.sqlString = @"insert into [tb_maintain_item] (maintain_id,item_no,item_type,item_name,man_hour_type,man_hour_quantity,man_hour_norm_unitprice,member_discount
                                                  ,member_price,member_sum_money,sum_money_goods,three_warranty,remarks,enable_flag,item_id)
                                                values (@maintain_id,@item_no,@item_type,@item_name,@man_hour_type,@man_hour_quantity,@man_hour_norm_unitprice,@member_discount
                                                  ,@member_price,@member_sum_money,@sum_money_goods,@three_warranty,@remarks,@enable_flag,@item_id
                                                        );";
                obj.Param = dicParam;
                listSql.Add(obj);
            }
            #endregion
        }
        #endregion

        #region 维修用料信息
        private void ImportMData(List<SQLObj> listSql, string strId, string strReservId)
        {

            #region 维修用料信息
            DataTable dmt = DBHelper.GetTable("维修用料数据", "tb_maintain_material_detail", "*", string.Format(" maintain_id ='{0}'", strReservId), "", "");
            for (int K = 0; K < dmt.Rows.Count; K++)
            {
                SQLObj obj = new SQLObj();
                obj.cmdType = CommandType.Text;
                Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                DataRow mrow = dmt.Rows[K];
                dicParam.Add("maintain_id", new ParamObj("maintain_id", strId, SysDbType.VarChar, 40));
                dicParam.Add("parts_code", new ParamObj("parts_code", CommonCtrl.IsNullToString(mrow["parts_code"]), SysDbType.VarChar, 40));
                dicParam.Add("parts_name", new ParamObj("parts_name", CommonCtrl.IsNullToString(mrow["parts_name"]), SysDbType.VarChar, 40));
                dicParam.Add("norms", new ParamObj("norms", CommonCtrl.IsNullToString(mrow["norms"]), SysDbType.VarChar, 40));
                dicParam.Add("unit", new ParamObj("unit", CommonCtrl.IsNullToString(mrow["unit"]), SysDbType.VarChar, 20));
                dicParam.Add("whether_imported", new ParamObj("whether_imported", CommonCtrl.IsNullToString(mrow["whether_imported"]), SysDbType.VarChar, 1));
                dicParam.Add("quantity", new ParamObj("quantity", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(mrow["quantity"])) ? mrow["quantity"] : null, SysDbType.Decimal, 15));
                dicParam.Add("unit_price", new ParamObj("unit_price", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(mrow["unit_price"])) ? mrow["unit_price"] : null, SysDbType.Decimal, 15));
                dicParam.Add("member_discount", new ParamObj("member_discount", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(mrow["member_discount"])) ? mrow["member_discount"] : null, SysDbType.Decimal, 15));
                dicParam.Add("member_price", new ParamObj("member_price", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(mrow["member_price"])) ? mrow["member_price"] : null, SysDbType.Decimal, 15));
                dicParam.Add("sum_money", new ParamObj("sum_money", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(mrow["sum_money"])) ? mrow["sum_money"] : null, SysDbType.Decimal, 15));
                dicParam.Add("drawn_no", new ParamObj("drawn_no", CommonCtrl.IsNullToString(mrow["drawn_no"]), SysDbType.VarChar, 40));
                dicParam.Add("vehicle_brand", new ParamObj("vehicle_brand", CommonCtrl.IsNullToString(mrow["vehicle_brand"]), SysDbType.VarChar, 40));
                dicParam.Add("three_warranty", new ParamObj("three_warranty", CommonCtrl.IsNullToString(mrow["three_warranty"]), SysDbType.VarChar, 2));
                dicParam.Add("remarks", new ParamObj("remarks", CommonCtrl.IsNullToString(mrow["remarks"]), SysDbType.VarChar, 200));
                dicParam.Add("enable_flag", new ParamObj("enable_flag", "1", SysDbType.VarChar, 1));
                dicParam.Add("material_id", new ParamObj("material_id", Guid.NewGuid().ToString(), SysDbType.VarChar, 40));
                obj.sqlString = @"insert into [tb_maintain_material_detail] (maintain_id,parts_code,parts_name,norms,unit,whether_imported,quantity,unit_price,sum_money
                                                    ,drawn_no,vehicle_brand,three_warranty,remarks,enable_flag,material_id) 
                                                values (@maintain_id,@parts_code,@parts_name,@norms,@unit,@whether_imported,@quantity,@unit_price,@sum_money
                                                    ,@drawn_no,@vehicle_brand,@three_warranty,@remarks,@enable_flag,@material_id
                                                       );";
                obj.Param = dicParam;
                listSql.Add(obj);
            }
            #endregion
        }
        #endregion

        #region 其他项目收费信息
        private void ImportOData(List<SQLObj> listSql, string strId, string strReservId)
        {

            #region 其他项目收费信息
            DataTable dot = DBHelper.GetTable("其他项目收费数据", "tb_maintain_other_toll", "*", string.Format(" maintain_id ='{0}'", strReservId), "", "");
            for (int l = 0; l < dot.Rows.Count; l++)
            {
                SQLObj obj = new SQLObj();
                obj.cmdType = CommandType.Text;
                Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                DataRow orow = dot.Rows[l];
                dicParam.Add("maintain_id", new ParamObj("maintain_id", strId, SysDbType.VarChar, 40));
                dicParam.Add("cost_types", new ParamObj("cost_types", CommonCtrl.IsNullToString(orow["cost_types"]), SysDbType.VarChar, 40));
                dicParam.Add("sum_money", new ParamObj("sum_money", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(orow["sum_money"])) ? CommonCtrl.IsNullToString(orow["sum_money"]) : null, SysDbType.Decimal, 18));
                dicParam.Add("remarks", new ParamObj("remarks", CommonCtrl.IsNullToString(orow["remarks"]), SysDbType.VarChar, 200));
                dicParam.Add("enable_flag", new ParamObj("enable_flag", "1", SysDbType.Char, 10));
                dicParam.Add("toll_id", new ParamObj("toll_id", Guid.NewGuid().ToString(), SysDbType.VarChar, 40));
                obj.sqlString = @"insert into [tb_maintain_other_toll] (maintain_id,cost_types,sum_money,remarks,enable_flag,toll_id) 
                                                  values (@maintain_id,@cost_types,@sum_money,@remarks,@enable_flag,@toll_id);";
                obj.Param = dicParam;
                listSql.Add(obj);
            }
            #endregion
        }
        #endregion

        #region 附件信息
        private void ImportAData(List<SQLObj> listSql, string strId, string strReservId)
        {

            #region 附件信息
            StringBuilder sbWhere = new StringBuilder();
            sbWhere.AppendFormat("relation_object='{0}'", "tb_maintain_reservation");
            sbWhere.AppendFormat(" and relation_object_id='{0}'", strReservId);
            sbWhere.AppendFormat(" and enable_flag='{0}'", (int)DataSources.EnumEnableFlag.USING);
            sbWhere.Append(" and is_main='0'");
            DataTable dat = DBHelper.GetTable("绑定附件", "attachment_info", "*", sbWhere.ToString(), "", "");
            for (int m = 0; m < dat.Rows.Count; m++)
            {
                SQLObj obj = new SQLObj();
                obj.cmdType = CommandType.Text;
                Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                DataRow drow = dat.Rows[m];
                dicParam.Add("att_name", new ParamObj("att_name", CommonCtrl.IsNullToString(drow["att_name"]), SysDbType.NVarChar, 15));
                dicParam.Add("att_type", new ParamObj("att_type", CommonCtrl.IsNullToString(drow["att_type"]), SysDbType.NVarChar, 15));
                dicParam.Add("att_path", new ParamObj("att_path", CommonCtrl.IsNullToString(drow["att_path"]), SysDbType.VarChar, 40));
                dicParam.Add("remark", new ParamObj("remark", CommonCtrl.IsNullToString(drow["remark"]), SysDbType.NVarChar, 300));
                dicParam.Add("att_id", new ParamObj("att_id", Guid.NewGuid().ToString(), SysDbType.VarChar, 40));
                dicParam.Add("is_main", new ParamObj("is_main", (int)DataSources.EnumYesNo.NO, SysDbType.VarChar, 5));
                dicParam.Add("relation_object", new ParamObj("relation_object", "tb_maintain_info", SysDbType.NVarChar, 30));
                dicParam.Add("relation_object_id", new ParamObj("relation_object_id", strId, SysDbType.VarChar, 40));
                dicParam.Add("enable_flag", new ParamObj("enable_flag", (int)DataSources.EnumEnableFlag.USING, SysDbType.VarChar, 5));
                dicParam.Add("create_by", new ParamObj("create_by", GlobalStaticObj.UserID, SysDbType.NVarChar, 40));
                dicParam.Add("create_time", new ParamObj("create_time", DateTime.UtcNow.Ticks, SysDbType.BigInt));
                obj.sqlString = @"insert into [attachment_info] (att_name,att_type,att_path,remark,att_id,is_main,relation_object,relation_object_id,enable_flag,create_by,create_time)
                                        values (@att_name,@att_type,@att_path,@remark,@att_id,@is_main,@relation_object,@relation_object_id,@enable_flag,@create_by,@create_time);";
                obj.Param = dicParam;
                listSql.Add(obj);
            }

            #endregion
        }
        #endregion      

        #region 格式化时间
        private void dgvRData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
            string fieldNmae = dgvRData.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("reception_time"))
            {
                long ticks = (long)e.Value;
                e.Value = Common.UtcLongToLocalDateTime(ticks);
            }
        }
        #endregion

        private void UCCallBackImport_Load(object sender, EventArgs e)
        {
            btnSubmit_Click(null,null);
        }

        #region 双击事件
        private void dgvRData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                uc.strBefore_orderId = dgvRData.Rows[e.RowIndex].Cells["repair_id"].Value.ToString();  //导入单据的Id值
                uc.strOrdersSouse = "2";
                uc.BindReserveData();
                this.Close();
            }
        }
        #endregion

        #region 选择行
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
        }
        #endregion
    }
}
