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
using ServiceStationClient.ComponentUI.TextBox;

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
        /// <summary>
        /// 结算单ID
        /// </summary>
        string strBlanceId = string.Empty;
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
            txtCarNO.SetBindTable("tb_vehicle", "license_plate", "license_plate");
            txtCarNO.GetDataSourced += new ServiceStationClient.ComponentUI.TextBox.TextChooser.GetDataSourceHandler(tc_GetDataSourced);
            //设置客户编码速查
            txtCustomNO.SetBindTable("tb_customer", "cust_code", "cust_code");
            txtCustomNO.GetDataSourced += new ServiceStationClient.ComponentUI.TextBox.TextChooser.GetDataSourceHandler(tc_GetDataSourced);
            txtCustomNO.DataBacked += new TextChooser.DataBackHandler(txtCustomNO_DataBacked);
            //按维修单查询设置车型速查
            txtCarType.SetBindTable("tb_vehicle_models", "vm_name");
            txtCarType.GetDataSourced += new ServiceStationClient.ComponentUI.TextBox.TextChooser.GetDataSourceHandler(tc_GetDataSourced);
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
            //if (MessageBoxEx.Show("确认要质检吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            //{
            //    return;
            //}
            Inspection = new UCInspection();
            if (Inspection.ShowDialog() == DialogResult.OK)
            {
                string strMsg = string.Empty;               
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
                UpdateRepairOrderInfo(listSql, strReceId, Inspection.DStatus, Inspection.Content);
                SaveProjectData(listSql, strReceId, Inspection.PStatus);
                if (DBHelper.BatchExeSQLMultiByTrans("更新单据调度状态为质检", listSql))
                {
                    if (Inspection.DStatus == DataSources.EnumDispatchStatus.NotPassed)
                    {
                        strMsg = "质检未通过！";                      
                    }
                    else if (Inspection.DStatus == DataSources.EnumDispatchStatus.HasPassed)
                    {
                        strMsg = "质检成功！";
                        #region 自动生成领料单（草稿状态）
                        List<SQLObj> listblanceSql = new List<SQLObj>();
                        SaveOrderInfo(listblanceSql, strReceId);                       
                        DBHelper.BatchExeSQLMultiByTrans("", listblanceSql);
                        #endregion
                    }
                    MessageBoxEx.Show(strMsg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindPageData();
                }
            }
        }
        #endregion

        #region 自动生成结算单信息

        #region 维修结算单基本信息保存
        private void SaveOrderInfo(List<SQLObj> listSql,string strRId)
        {
            try
            {
                strRId = strRId.Substring(0, strRId.Length - 1);
                string[] pArray = strRId.Split(',');
                string strNRId = string.Empty;
                for (int i = 0; i < pArray.Length; i++)
                {
                    strNRId += "'" + pArray[i] + "'" + ",";
                }
                strNRId = strNRId.Substring(0, strNRId.Length - 1);
                string[] cArray = strNRId.Split(',');
                for (int j = 0; j < cArray.Length; j++)
                {
                    if (string.IsNullOrEmpty(DBHelper.GetSingleValue("查询单据是否结算", "tb_maintain_settlement_info", "settlement_id", "maintain_id=" + cArray[j] + "", "")))
                    {
                        strBlanceId=cArray[j];
                        strBlanceId = strBlanceId.Replace("'","");
                        SQLObj obj = new SQLObj();
                        obj.cmdType = CommandType.Text;
                        Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                        #region 基本信息
                        //单据来源,0结算单、1预约单、2返修单、3结算单，默认0
                        dicParam.Add("orders_source", new ParamObj("orders_source", "3", SysDbType.VarChar, 1));
                        //单据状态(草稿)
                        dicParam.Add("info_status", new ParamObj("info_status", Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString(), SysDbType.VarChar, 40));
                        dicParam.Add("before_orderId", new ParamObj("before_orderId", strBlanceId, SysDbType.VarChar, 40));
                        #endregion
                        dicParam.Add("maintain_id", new ParamObj("maintain_id", strBlanceId, SysDbType.VarChar, 40));//Id
                        dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
                        dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
                        dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                        obj.sqlString = @"update tb_maintain_info set orders_source=@orders_source,info_status=@info_status,before_orderId=@before_orderId
                        ,update_by=@update_by,update_name=@update_name,update_time=@update_time  where maintain_id=@maintain_id";
                        obj.Param = dicParam;
                        listSql.Add(obj);
                        SaveBalanceInfo(listSql, strBlanceId);
                    }
                }
               
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 维修结算单结算信息保存
        /// <summary>
        /// 维修结算单结算信息保存
        /// </summary>
        /// <param name="strRid">跟维修结算单基本信息关联的Id</param>
        private void SaveBalanceInfo(List<SQLObj> listSql,string strRid)
        {
            try
            {
                SQLObj obj = new SQLObj();
                obj.cmdType = CommandType.Text;
                Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                #region 基本信息
                decimal dcHmoney = 0;//工时货款
                DataTable dpt = DBHelper.GetTable("维修项目数据", "tb_maintain_item", "*", string.Format(" maintain_id ='{0}' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", strRid), "", "order by maintain_id desc");
                if (dpt.Rows.Count > 0)
                {
                    for (int i = 0; i < dpt.Rows.Count; i++)
                    {
                        DataRow dr = dpt.Rows[i];
                        if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["sum_money_goods"])))
                        {
                            dcHmoney += Convert.ToDecimal(dr["sum_money_goods"]);
                        }
                    }
                }             

                dicParam.Add("man_hour_sum_money", new ParamObj("man_hour_sum_money", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dcHmoney)) ? dcHmoney.ToString() : null, SysDbType.Decimal, 15));//工时货款
                dicParam.Add("man_hour_sum", new ParamObj("man_hour_sum", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dcHmoney)) ? dcHmoney.ToString() : null, SysDbType.Decimal, 15));//工时税价合计
                decimal dcPmoney = 0;//配件货款
                DataTable dmt = DBHelper.GetTable("维修用料数据", "tb_maintain_material_detail", "*", string.Format(" maintain_id='{0}' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", strRid), "", "");
                if (dmt.Rows.Count > 0)
                {
                    for (int j = 0; j < dmt.Rows.Count; j++)
                    {
                        DataRow dmr = dmt.Rows[j];
                        if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dmr["sum_money"])))
                        {
                            dcPmoney += Convert.ToDecimal(dmr["sum_money"]);
                        }
                    }
                }
                dicParam.Add("fitting_sum_money", new ParamObj("fitting_sum_money", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dcPmoney)) ? dcPmoney.ToString() : null, SysDbType.Decimal, 15));//配件货款
                dicParam.Add("fitting_sum", new ParamObj("fitting_sum", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dcPmoney)) ? dcPmoney.ToString() : null, SysDbType.Decimal, 15));//配件价税合计
                decimal dcOmoney = 0;//其他项目费用
                DataTable dot = DBHelper.GetTable("其他项目收费数据", "tb_maintain_other_toll", "*", string.Format(" maintain_id='{0}'", strRid), "", "");
                 if (dot.Rows.Count > 0)
                 {
                     for (int k = 0; k < dot.Rows.Count; k++)
                     {
                         DataRow dor = dot.Rows[k];
                         if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dor["sum_money"])))
                         {
                             dcOmoney += Convert.ToDecimal(dor["sum_money"]);
                         }
                     }
                 }
                dicParam.Add("other_item_sum_money", new ParamObj("other_item_sum_money", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dcOmoney)) ? dcOmoney.ToString() : null, SysDbType.Decimal, 15));//其他项目费用
                dicParam.Add("other_item_sum", new ParamObj("other_item_sum", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dcOmoney)) ? dcOmoney.ToString() : null, SysDbType.Decimal, 15));//其他项目税价合计
                dicParam.Add("should_sum", new ParamObj("should_sum", Convert.ToString((dcHmoney + dcPmoney + dcOmoney)), SysDbType.Decimal, 15));//应收总额位            
                dicParam.Add("maintain_id", new ParamObj("maintain_id", strRid, SysDbType.VarChar, 40));//关联维修结算单主数据Id
                dicParam.Add("privilege_cost", new ParamObj("privilege_cost", "0", SysDbType.Decimal, 15));//优惠费用
                dicParam.Add("received_sum", new ParamObj("received_sum", Convert.ToString((dcHmoney + dcPmoney + dcOmoney)), SysDbType.Decimal, 15));//实收总额
                #endregion
                dicParam.Add("settlement_id", new ParamObj("settlement_id", Guid.NewGuid().ToString(), SysDbType.VarChar, 40));//Id
                dicParam.Add("create_by", new ParamObj("create_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//创建人id（制单人） 
                dicParam.Add("create_name", new ParamObj("create_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//创建人
                dicParam.Add("create_time", new ParamObj("create_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));
                obj.sqlString = @"insert into tb_maintain_settlement_info (man_hour_sum_money,man_hour_sum,fitting_sum_money,fitting_sum,other_item_sum_money,other_item_sum,should_sum
                ,maintain_id,privilege_cost,received_sum,settlement_id,create_by,create_name,create_time)
                values (@man_hour_sum_money,@man_hour_sum,@fitting_sum_money,@fitting_sum,@other_item_sum_money,@other_item_sum,@should_sum
                ,@maintain_id,@privilege_cost,@received_sum,@settlement_id,@create_by,@create_name,@create_time);";

                obj.Param = dicParam;
                listSql.Add(obj);
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

//        #region 维修项目信息保存
//        private void SaveProjectData(List<SQLObj> listSql,string strRid)
//        {
//            try
//            {
//                 DataTable dpt = DBHelper.GetTable("维修项目数据", "tb_maintain_item", "*", string.Format(" maintain_id ={0} and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", strRid), "", "order by maintain_id desc");
//                 if (dpt.Rows.Count > 0)
//                 {
//                     for (int i = 0; i < dpt.Rows.Count; i++)
//                     {
//                         DataRow dr = dpt.Rows[i];
//                         SQLObj obj = new SQLObj();
//                         obj.cmdType = CommandType.Text;
//                         Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
//                         dicParam.Add("maintain_id", new ParamObj("maintain_id", strRid, SysDbType.VarChar, 40));
//                         dicParam.Add("item_no", new ParamObj("item_no", dr["item_no"], SysDbType.VarChar, 40));
//                         dicParam.Add("item_type", new ParamObj("item_type", dr["item_type"], SysDbType.VarChar, 40));
//                         dicParam.Add("item_name", new ParamObj("item_name", dr["item_name"], SysDbType.VarChar, 40));
//                         dicParam.Add("man_hour_type", new ParamObj("man_hour_type", dr["man_hour_type"], SysDbType.VarChar, 40));
//                         dicParam.Add("man_hour_quantity", new ParamObj("man_hour_quantity", CommonCtrl.IsNullToString(dr["man_hour_quantity"]), SysDbType.Decimal, 15));
//                         dicParam.Add("man_hour_norm_unitprice", new ParamObj("man_hour_norm_unitprice", dr["man_hour_norm_unitprice"], SysDbType.Decimal, 15));
//                         dicParam.Add("member_discount", new ParamObj("member_discount", dr["member_discount"], SysDbType.Decimal, 5));
//                         dicParam.Add("member_price", new ParamObj("member_price", dr["member_price"], SysDbType.Decimal, 15));
//                         dicParam.Add("member_sum_money", new ParamObj("member_sum_money", dr["member_sum_money"], SysDbType.Decimal, 15));
//                         dicParam.Add("sum_money_goods", new ParamObj("sum_money_goods", dr["sum_money_goods"], SysDbType.Decimal, 15));
//                         dicParam.Add("three_warranty", new ParamObj("three_warranty", CommonCtrl.IsNullToString(dr["three_warranty"]), SysDbType.VarChar, 2));
//                         dicParam.Add("remarks", new ParamObj("remarks", dr["remarks"], SysDbType.VarChar, 200));
//                         dicParam.Add("enable_flag", new ParamObj("enable_flag", "1", SysDbType.VarChar, 1));
//                         dicParam.Add("whours_id", new ParamObj("whours_id", dr["whours_id"], SysDbType.VarChar, 40));
//                         dicParam.Add("item_id", new ParamObj("item_id", Guid.NewGuid().ToString(), SysDbType.VarChar, 40));
//                         obj.sqlString = "insert into [tb_maintain_item] (item_id,maintain_id,item_no,item_type,item_name,man_hour_type,man_hour_quantity,man_hour_norm_unitprice,member_discount,member_price,member_sum_money,sum_money_goods,three_warranty,remarks,enable_flag,whours_id) ";
//                         obj.sqlString += " values (@item_id,@maintain_id,@item_no,@item_type,@item_name,@man_hour_type,@man_hour_quantity,@man_hour_norm_unitprice,@member_discount,@member_price,@member_sum_money,@sum_money_goods,@three_warranty,@remarks,@enable_flag,@whours_id);";

//                     }
//                 }
              
//            }
//            catch (Exception ex)
//            {
//                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
//            }
//        }
//        #endregion

//        #region 维修用料信息保存
//        private void SaveMaterialsData(List<SQLObj> listSql,string strRid)
//        {
//            try
//            {
//                 DataTable dmt = DBHelper.GetTable("维修用料数据", "tb_maintain_material_detail", "*", string.Format(" maintain_id={0} and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", strRid), "", "");
//                 if (dmt.Rows.Count > 0)
//                 {
//                     for (int j = 0; j < dmt.Rows.Count; j++)
//                     {
//                         DataRow dmr = dmt.Rows[j];
//                         SQLObj obj = new SQLObj();
//                         obj.cmdType = CommandType.Text;
//                         Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
//                         dicParam.Add("maintain_id", new ParamObj("maintain_id", strRid, SysDbType.VarChar, 40));
//                         dicParam.Add("parts_code", new ParamObj("parts_code", CommonCtrl.IsNullToString(dmr["parts_code"]), SysDbType.VarChar, 40));
//                         dicParam.Add("parts_name", new ParamObj("parts_name", CommonCtrl.IsNullToString(dmr["parts_name"]), SysDbType.VarChar, 40));
//                         dicParam.Add("norms", new ParamObj("norms", CommonCtrl.IsNullToString(dmr["norms"]), SysDbType.VarChar, 40));
//                         dicParam.Add("unit", new ParamObj("unit", CommonCtrl.IsNullToString(dmr["unit"]), SysDbType.VarChar, 20));
//                         dicParam.Add("whether_imported", new ParamObj("whether_imported", CommonCtrl.IsNullToString(dmr["whether_imported"]), SysDbType.VarChar, 1));
//                         dicParam.Add("quantity", new ParamObj("quantity", CommonCtrl.IsNullToString(dmr["quantity"]), SysDbType.Decimal, 15));
//                         dicParam.Add("unit_price", new ParamObj("unit_price", CommonCtrl.IsNullToString(dmr["unit_price"]), SysDbType.Decimal, 15));
//                         dicParam.Add("member_discount", new ParamObj("member_discount", CommonCtrl.IsNullToString(dmr["Mmember_discount"]), SysDbType.Decimal, 15));
//                         dicParam.Add("member_price", new ParamObj("member_price", CommonCtrl.IsNullToString(dmr["Mmember_price"]), SysDbType.Decimal, 15));
//                         dicParam.Add("sum_money", new ParamObj("sum_money", CommonCtrl.IsNullToString(dmr["sum_money"]), SysDbType.Decimal, 15));
//                         dicParam.Add("drawn_no", new ParamObj("drawn_no", dmr["drawn_no"], SysDbType.VarChar, 40));
//                         dicParam.Add("vehicle_brand", new ParamObj("vehicle_brand", dmr["vehicle_brand"], SysDbType.VarChar, 200));
//                         dicParam.Add("three_warranty", new ParamObj("three_warranty", CommonCtrl.IsNullToString(dmr["Mthree_warranty"]), SysDbType.VarChar, 2));
//                         dicParam.Add("remarks", new ParamObj("remarks", dmr["Mremarks"], SysDbType.VarChar, 200));
//                         dicParam.Add("enable_flag", new ParamObj("enable_flag", "1", SysDbType.VarChar, 1));
//                         dicParam.Add("parts_id", new ParamObj("parts_id", dmr["parts_id"], SysDbType.VarChar, 40));
//                         dicParam.Add("material_id", new ParamObj("material_id", Guid.NewGuid().ToString(), SysDbType.VarChar, 40));
//                         obj.sqlString = "insert into [tb_maintain_material_detail] (material_id,parts_code,parts_name,norms,unit,whether_imported,quantity,unit_price,sum_money,drawn_no,vehicle_brand,three_warranty,remarks,enable_flag,maintain_id,parts_id) ";
//                         obj.sqlString += " values (@material_id,@parts_code,@parts_name,@norms,@unit,@whether_imported,@quantity,@unit_price,@sum_money,@drawn_no,@vehicle_brand,@three_warranty,@remarks,@enable_flag,@maintain_id,@parts_id);";

//                     }
//                 }
                
//            }
//            catch (Exception ex)
//            {
//                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
//            }
//        }
//        #endregion

//        #region 其他收费项目信息保存
//        //其他收费项目信息保存sql语句
//        private void SaveOtherData(List<SQLObj> listSql,string strRid)
//        {
//            try
//            {
//                DataTable dot = DBHelper.GetTable("其他项目收费数据", "tb_maintain_other_toll", "*", string.Format(" maintain_id={0} and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "' ", strRid), "", "");
//                if (dot.Rows.Count > 0)
//                {
//                    for (int k = 0; k < dot.Rows.Count; k++)
//                    {
//                        DataRow dor = dot.Rows[k];
//                        SQLObj obj = new SQLObj();
//                        obj.cmdType = CommandType.Text;
//                        Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
//                        dicParam.Add("maintain_id", new ParamObj("maintain_id", strRid, SysDbType.VarChar, 40));
//                        dicParam.Add("cost_types", new ParamObj("cost_types", CommonCtrl.IsNullToString(dor["cost_types"]), SysDbType.VarChar, 40));
//                        dicParam.Add("sum_money", new ParamObj("sum_money", CommonCtrl.IsNullToString(dor["Osum_money"]), SysDbType.Decimal, 18));
//                        dicParam.Add("remarks", new ParamObj("remarks", CommonCtrl.IsNullToString(dor["Oremarks"]), SysDbType.VarChar, 200));
//                        dicParam.Add("enable_flag", new ParamObj("enable_flag", "1", SysDbType.Char, 10));
//                        dicParam.Add("toll_id", new ParamObj("toll_id", Guid.NewGuid().ToString(), SysDbType.VarChar, 40));
//                        obj.sqlString = "insert into [tb_maintain_other_toll] (toll_id,cost_types,sum_money,remarks,maintain_id,enable_flag) values (@toll_id,@cost_types,@sum_money,@remarks,@maintain_id,@enable_flag);";

//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
//            }
//        }
//        #endregion

//        #region 附件信息保存
//        private void SaveAttachment(List<SQLObj> listSql, string strRid)
//        {

//            DataTable dat = DBHelper.GetTable("附件数据信息", "attachment_info", "*", string.Format(" relation_object_id={0} and relation_object='tb_maintain_info' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "' ", strRid), "", "");
//            if (dat.Rows.Count > 0)
//            {
//                for (int k = 0; k < dat.Rows.Count; k++)
//                {
//                    DataRow dar = dat.Rows[k];
//                    SQLObj obj = new SQLObj();
//                    obj.cmdType = CommandType.Text;
//                    Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
//                    dicParam.Add("att_name", new ParamObj("att_name", CommonCtrl.IsNullToString(dar["att_name"]), SysDbType.VarChar, 15));
//                    dicParam.Add("att_type", new ParamObj("att_type", CommonCtrl.IsNullToString(dar["att_type"]), SysDbType.VarChar, 15));
//                    dicParam.Add("att_path", new ParamObj("att_path", CommonCtrl.IsNullToString(dar["att_path"]), SysDbType.VarChar, 300));
//                    dicParam.Add("att_filename", new ParamObj("att_filename", CommonCtrl.IsNullToString(dar["att_filename"]), SysDbType.VarChar, 50));
//                    dicParam.Add("remark", new ParamObj("remark", CommonCtrl.IsNullToString(dar["remark"]), SysDbType.VarChar, 300));
//                    dicParam.Add("is_main", new ParamObj("is_main", CommonCtrl.IsNullToString(dar["is_main"]), SysDbType.VarChar, 5));
//                    dicParam.Add("relation_object", new ParamObj("relation_object", CommonCtrl.IsNullToString(dar["relation_object"]), SysDbType.VarChar, 30));
//                    dicParam.Add("relation_object_id", new ParamObj("relation_object_id", CommonCtrl.IsNullToString(dar["relation_object_id"]), SysDbType.VarChar, 40));
//                    dicParam.Add("enable_flag", new ParamObj("enable_flag", "1", SysDbType.VarChar, 5));
//                    dicParam.Add("create_by", new ParamObj("create_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//创建人id（制单人）                 
//                    dicParam.Add("create_time", new ParamObj("create_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));
//                    obj.sqlString = @"insert into [attachment_info] ([att_id],[att_name],[att_type],[att_path],[att_filename],[remark],[relation_object],[relation_object_id],[enable_flag],[create_by],[create_time],[is_main])
//                            values (@att_id,@att_name,@att_type,@att_path,@att_filename,@remark,@relation_object,@relation_object_id,@enable_flag,@create_by,@create_time,@is_main);";
//                }
//            }
//        }
    
//        #endregion
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
                comField.Add("dispatch_status", Convert.ToInt32(DataSources.EnumDispatchStatus.NotStartWork).ToString());//调度状态
                comField.Add("update_by", HXCPcClient.GlobalStaticObj.UserID);//修改人Id
                comField.Add("update_name", HXCPcClient.GlobalStaticObj.UserName);//修改人姓名
                comField.Add("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString());//修改时间  
                bool flag = DBHelper.BatchUpdateDataByIn("批量驳回维修调度单", "tb_maintain_info", comField, "maintain_id", listField.ToArray());
                if (flag)
                {                    
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
                strWhere = string.Format(" enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "' and (info_status='" + Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString() + "' or info_status='" + Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString() + "')");//enable_flag 1未删除,info_status='2'已审核
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
                txtCarType.Text = frmModels.VMName;
                txtCarType.Tag = frmModels.VMID;
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
            //未开工
            string NotStartWork = ((int)DataSources.EnumDispatchStatus.NotStartWork).ToString();           
            //已开工
            string StartWork = ((int)DataSources.EnumDispatchStatus.StartWork).ToString();
            //已停工
            string StopWork = ((int)DataSources.EnumDispatchStatus.StopWork).ToString();          
            base.btnBalance.Enabled = listIDs.Count > 1 ? false : true;
            tsmiBalance.Enabled = listIDs.Count > 1 ? false : true;
            base.btnQC.Enabled = listFiles.Contains(HasPassed) || listFiles.Contains(NotPassed) || listFiles.Contains(NotStartWork)  || listFiles.Contains(StartWork) || listFiles.Contains(StopWork) ? false : true;
            tsmiQC.Enabled = listFiles.Contains(HasPassed) || listFiles.Contains(NotPassed) || listFiles.Contains(NotStartWork) || listFiles.Contains(StartWork) || listFiles.Contains(StopWork) ? false : true;
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
