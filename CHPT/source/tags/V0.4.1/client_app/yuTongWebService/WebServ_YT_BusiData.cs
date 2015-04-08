using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using HXC_FuncUtility;
using Utility.Security;
using SYSModel;
using System.Data;
using BLL;
using Utility.Common;
using Model;

namespace yuTongWebService
{
    public class WebServ_YT_BusiData
    {

        /// <summary> 配件销售开单
        /// </summary>
        /// <param name="updateTime">最后更新时间</param>
        /// <returns>返回同步配件信息条数，如为-1，同步失败</returns>
        public static bool UpLoadPartSale(string jsonStr)
        {
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return true;
            }
            Utility.Log.Log.writeLineToLog("【配件销售开单】" + jsonStr, "接口");
            #region 测试数据
            //partsale pSale = new partsale();
            //pSale.amount = "120";
            //pSale.cust_name = "add";
            //pSale.cust_phone = "123123";
            //pSale.license_plate = "豫G99300";
            //pSale.remark = "dfgdfsdf";
            //pSale.sale_date = "2014-10-10  14:46:44";
            //pSale.turner = "14H197P-0013";
            //SUPartSale.partDetail[] pArry = new SUPartSale.partDetail[1];
            //SUPartSale.partDetail p0 = new SUPartSale.partDetail();
            //p0.amount = "12";
            //p0.business_count = "1";
            //p0.business_price = "12";
            //p0.car_parts_code = "2100-00233";
            //p0.parts_remark = "asdfasdf";
            //p0.remark = "asdfasf";
            //p0.wh_code = "CK00000202";
            //pArry[0] = p0;
            //pSale.partDetails = pArry;
            #endregion
            ServicePointManager.ServerCertificateValidationCallback = WebServUtil.ValidateServerCertificate;
            SUPartSale.clientInfo clientInfo = new SUPartSale.clientInfo();
            clientInfo.clientID = GlobalStaticObj_YT.ClientID;
            clientInfo.serviceID = "partSaleSU";
            SUPartSale.partSale pSale = Newtonsoft.Json.JsonConvert.DeserializeObject<SUPartSale.partSale>(jsonStr);
            SUPartSale.partSaleSUService serv = new SUPartSale.partSaleSUService();
            string stationCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_YT.SAPCode, GlobalStaticObj_YT.KeySecurity_YT);
            string dateStr = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss"), GlobalStaticObj_YT.KeySecurity_YT);
            string requestType = Secret.Encrypt3DES_UTF8("CREATE", GlobalStaticObj_YT.KeySecurity_YT);
            clientInfo = WebServUtil.EncModel<SUPartSale.clientInfo>(clientInfo);
            //先取出配件实体集合加密
            SUPartSale.partDetail[] partDetails = WebServUtil.EncList<SUPartSale.partDetail>(pSale.partDetails);
            pSale = WebServUtil.EncModel<SUPartSale.partSale>(pSale);//加密销售单实体
            pSale.partDetails = partDetails;//将加密后的配件实体集合赋值给加密后的销售单实体
            SUPartSale.Result result = serv.partSaleSU(stationCode, dateStr, requestType, pSale, clientInfo);
            string state = Secret.Decrypt3DES_UTF8(result.state, GlobalStaticObj_YT.KeySecurity_YT);
            if (state == "F")
            {
                string errMsg = Secret.Decrypt3DES_UTF8(result.errorMsg, GlobalStaticObj_YT.KeySecurity_YT);
                Utility.Log.Log.writeLineToLog("【配件销售开单】" + errMsg, "接口");
                return false;
            }
            return true;
        }

        /// <summary> 14.三包服务单状态查询-CRM
        /// </summary>
        /// <param name="billNum">CRM服务单号</param>
        /// <returns>-1为失败，0未成功</returns>
        public static bool LoadOrderStatus(string billNum)
        {
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return true;
            }
            Utility.Log.Log.writeLineToLog("【三包服务单状态查询】" + billNum, "接口");
            //billNum = "BD00000167";
            ServicePointManager.ServerCertificateValidationCallback = WebServUtil.ValidateServerCertificate;
            QueryOrderStatus.clientInfo clientInfo = new QueryOrderStatus.clientInfo();
            clientInfo.clientID = GlobalStaticObj_YT.ClientID;
            clientInfo.serviceID = "serviceOrderStatusQuery";
            QueryOrderStatus.serviceOrderStatusQueryService serv = new QueryOrderStatus.serviceOrderStatusQueryService();
            billNum = Secret.Encrypt3DES_UTF8(billNum, GlobalStaticObj_YT.KeySecurity_YT);//加密字段
            string stationCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_YT.SAPCode, GlobalStaticObj_YT.KeySecurity_YT);
            string dateStr = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss"), GlobalStaticObj_YT.KeySecurity_YT);
            string requestType = Secret.Encrypt3DES_UTF8("QUERY", GlobalStaticObj_YT.KeySecurity_YT);
            clientInfo = WebServUtil.EncModel<QueryOrderStatus.clientInfo>(clientInfo);
            QueryOrderStatus.Result result = serv.serviceOrderStatusQuery(stationCode, dateStr, requestType, billNum, clientInfo);
            string state = Secret.Decrypt3DES_UTF8(result.state, GlobalStaticObj_YT.KeySecurity_YT);
            if (state == "F")
            {
                string errMsg = Secret.Decrypt3DES_UTF8(result.errorMsg, GlobalStaticObj_YT.KeySecurity_YT);
                Utility.Log.Log.writeLineToLog("【三包服务单状态查询】" + errMsg, "接口");
                return false;
            }
            result = WebServUtil.DesModel(result);//解密
            string res = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            billNum = Secret.Decrypt3DES_UTF8(billNum, GlobalStaticObj_YT.KeySecurity_YT);
            //删除云平台缓存,防止重复调用
            FactoryTemp.DeleteFactory(billNum, DataSources.EnumBillType.ServiceOrder, DataSources.EnumOperateObj.State);
            if (string.IsNullOrEmpty(result.crm_service_bill_num))
            {
                return true;
            }
            #region 添加三包服务单审核记录
            Utility.Log.Log.writeLineToLog("【三包服务单状态查询】返回数据\r\n" + res, "接口");
            string tg_id = DBHelper.GetSingleValue("根据CRM服务单号获取三包服务单ID", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, string.Format("select tg_id from tb_maintain_three_guaranty where service_no_yt='{0}'", result.crm_service_bill_num));
            List<SysSQLString> listSql = new List<SysSQLString>();
            SysSQLString sqlApprove = new SysSQLString();
            sqlApprove.cmdType = CommandType.Text;
            sqlApprove.Param = new Dictionary<string, string>();
            sqlApprove.Param.Add("approve_id", Guid.NewGuid().ToString());
            string review_status = WebServUtil.GetLocalDicID("review_status_name", result.review_status_name);
            sqlApprove.Param.Add("approve_state", review_status);
            sqlApprove.Param.Add("approve_name", result.review_name);
            sqlApprove.Param.Add("approve_time", result.review_date.Length == 0 ? null : Common.LocalDateTimeToUtcLong(DateTime.Parse(result.review_date)).ToString());
            sqlApprove.Param.Add("approve_idea", result.review_suggestion);
            sqlApprove.Param.Add("tg_id", tg_id);
            sqlApprove.sqlString = @"insert into tb_maintain_three_material_approve (approve_id,approve_state,approve_name,approve_time,approve_idea,tg_id)
                            values (@approve_id,@approve_state,@approve_name,@approve_time,@approve_idea,@tg_id)";
            listSql.Add(sqlApprove);
            if (result.review_status_name == "100000002")//如果是驳回,三包服务单修改为“审核未通过”
            {
                SysSQLString sqlStatus = new SysSQLString();
                sqlStatus.cmdType = CommandType.Text;
                sqlStatus.Param = new Dictionary<string, string>();
                sqlStatus.Param.Add("info_status", "4CE4693D-D8E9-8847-54FA-8B6C1318545C");
                sqlStatus.Param.Add("tg_id", tg_id);
                sqlStatus.sqlString = "update tb_maintain_three_guaranty set info_status=@info_status where tg_id=@tg_id";
                listSql.Add(sqlStatus);
            }
            SysSQLString sqlYTApprove = new SysSQLString();
            sqlYTApprove.cmdType = CommandType.Text;
            sqlYTApprove.Param = new Dictionary<string, string>();
            sqlYTApprove.sqlString = "update tb_maintain_three_guaranty set approve_status_yt=@approve_status_yt where tg_id=@tg_id";
            sqlYTApprove.Param.Add("approve_status_yt", WebServUtil.GetLocalDicID("service_status", result.service_status));
            sqlYTApprove.Param.Add("tg_id", tg_id);
            listSql.Add(sqlYTApprove);
            bool flag = false;
            try
            {
                flag = DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup("宇通：三包服务单审核", GlobalStaticObj_Server.Instance.MainAccCode, listSql);
                //if (DBHelper.Submit_AddOrEdit("宇通：三包服务单审核", GlobalStaticObj_Server.DbPrefix+GlobalStaticObj_Server.Instance.MainAccCode, "tb_maintain_three_material_approve", null, null, dic))
                //{
                //    flag = true;
                //}
            }
            catch (Exception ex)
            {
                flag = false;
            }
            #endregion
            return flag;
        }

        /// <summary>三包服务单状态查询
        /// </summary>
        /// <param name="billNum">CRM服务单号</param>
        /// <returns>单据状态编码</returns>
        public static string SearchOrderStatus(string billNum)
        {
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return string.Empty;
            }
            Utility.Log.Log.writeLineToLog("【三包服务单状态查询】" + billNum, "接口");
            //billNum = "BD00000167";
            ServicePointManager.ServerCertificateValidationCallback = WebServUtil.ValidateServerCertificate;
            QueryOrderStatus.clientInfo clientInfo = new QueryOrderStatus.clientInfo();
            clientInfo.clientID = GlobalStaticObj_YT.ClientID;
            clientInfo.serviceID = "serviceOrderStatusQuery";
            QueryOrderStatus.serviceOrderStatusQueryService serv = new QueryOrderStatus.serviceOrderStatusQueryService();
            billNum = Secret.Encrypt3DES_UTF8(billNum, GlobalStaticObj_YT.KeySecurity_YT);//加密字段
            string stationCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_YT.SAPCode, GlobalStaticObj_YT.KeySecurity_YT);
            string dateStr = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss"), GlobalStaticObj_YT.KeySecurity_YT);
            string requestType = Secret.Encrypt3DES_UTF8("QUERY", GlobalStaticObj_YT.KeySecurity_YT);
            clientInfo = WebServUtil.EncModel<QueryOrderStatus.clientInfo>(clientInfo);
            QueryOrderStatus.Result result = serv.serviceOrderStatusQuery(stationCode, dateStr, requestType, billNum, clientInfo);
            string state = Secret.Decrypt3DES_UTF8(result.state, GlobalStaticObj_YT.KeySecurity_YT);
            if (state == "F")
            {
                string errMsg = Secret.Decrypt3DES_UTF8(result.errorMsg, GlobalStaticObj_YT.KeySecurity_YT);
                Utility.Log.Log.writeLineToLog("【三包服务单状态查询】" + errMsg, "接口");
                return string.Empty;
            }
            if (string.IsNullOrEmpty(result.service_status))
            {
                return string.Empty;
            }
            return Secret.Decrypt3DES_UTF8(result.service_status, GlobalStaticObj_YT.KeySecurity_YT);
        }

        /// <summary> 15.配件入库单入库
        /// </summary>
        /// <param name="rationSendCode">DSN配送单号,入库状态</param>
        /// <returns>-1为失败,0为成功</returns>
        public static bool UpLoadPartPutStore(string rationSendCode)
        {
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return true;
            }
            Utility.Log.Log.writeLineToLog("【配件入库单入库】" + rationSendCode, "接口");
            //string[] param = rationSendCode.Split(',');
            ////rationSendCode = "30457907";
            ////stateCode = "";
            //if (param.Length != 2)
            //{
            //    return false;
            //}
            //rationSendCode = param[0];
            string stateCode = "";//入库状态
            string requestType = string.IsNullOrEmpty(rationSendCode) ? "CREATE" : "UPDATE";

            ServicePointManager.ServerCertificateValidationCallback = WebServUtil.ValidateServerCertificate;

            SUPartPutStore.clientInfo clientInfo = new SUPartPutStore.clientInfo();
            clientInfo.clientID = GlobalStaticObj_YT.ClientID;
            clientInfo.serviceID = "partPutStore";
            SUPartPutStore.partPutStoreService serv = new SUPartPutStore.partPutStoreService();
            rationSendCode = Secret.Encrypt3DES_UTF8(rationSendCode, GlobalStaticObj_YT.KeySecurity_YT);
            if (!string.IsNullOrEmpty(stateCode))
            {
                stateCode = Secret.Encrypt3DES_UTF8(stateCode, GlobalStaticObj_YT.KeySecurity_YT);//加密字段
            }
            string stationCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_YT.SAPCode, GlobalStaticObj_YT.KeySecurity_YT);
            string dateStr = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss"), GlobalStaticObj_YT.KeySecurity_YT);
            requestType = Secret.Encrypt3DES_UTF8(requestType, GlobalStaticObj_YT.KeySecurity_YT);
            clientInfo = WebServUtil.EncModel<SUPartPutStore.clientInfo>(clientInfo);
            //SUPartPutStore.Result result = serv.partPutStore(stationCode, dateStr, requestType, rationSendCode, stateCode, clientInfo);
            SUPartPutStore.Result result = serv.partPutStore(stateCode, dateStr, requestType, rationSendCode, clientInfo);
            string state = Secret.Decrypt3DES_UTF8(result.state, GlobalStaticObj_YT.KeySecurity_YT);
            if (state == "F")
            {
                string errMsg = Secret.Decrypt3DES_UTF8(result.errorMsg, GlobalStaticObj_YT.KeySecurity_YT);
                Utility.Log.Log.writeLineToLog("【配件入库单入库】" + errMsg, "接口");
                return false;
            }
            result = WebServUtil.DesModel(result);
            SUPartPutStore.Detail[] details = WebServUtil.DesList(result.Details);
            //记录返回数据
            Utility.Log.Log.writeLineToLog("【配件入库单入库】返回\r\n" + Newtonsoft.Json.JsonConvert.SerializeObject(result), "接口");
            if (string.IsNullOrEmpty(result.dsn_adjustable_parts))
            {
                return false;
            }
            //解密返回数据
            string ration_send_code = result.ration_send_code;//配送单号
            string dsn_adjustable_parts = result.dsn_adjustable_parts;//配件需求单号
            #region 自动生成采购开单
            List<SysSQLString> listSql = new List<SysSQLString>();
            string billingID = Guid.NewGuid().ToString();//采购开单ID
            string nowDate = Common.LocalDateTimeToUtcLong(DateTime.Now).ToString();//当前时间
            //采购开单住主表
            SysSQLString billingSql = new SysSQLString();
            billingSql.cmdType = CommandType.Text;
            billingSql.Param = new Dictionary<string, string>();
            CodingRule cr = new CodingRule(DataSources.EnumProjectType.PurchaseOpenOrder);
            string orderNum = cr.AddNewNo();
            //更新最新采购开单编号
            SysSQLString sqlCode = new SysSQLString();
            sqlCode.cmdType = CommandType.Text;
            sqlCode.Param = new Dictionary<string, string>();
            sqlCode.Param.Add("bill_code_rule_id", cr.ruleID);
            sqlCode.Param.Add("last_bill_no", orderNum);
            sqlCode.sqlString = "update sys_bill_code_rule set last_bill_no=@last_bill_no where bill_code_rule_id=@bill_code_rule_id";
            listSql.Add(sqlCode);

            billingSql.Param.Add("order_num", orderNum);
            billingSql.Param.Add("order_date", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());
            billingSql.Param.Add("crm_bill_id", dsn_adjustable_parts);
            billingSql.Param.Add("purchase_billing_id", billingID);
            billingSql.Param.Add("ration_send_code", ration_send_code);//配送单号
            billingSql.Param.Add("order_status", ((int)DataSources.EnumAuditStatus.SUBMIT).ToString());
            billingSql.Param.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT));
            billingSql.Param.Add("remark", string.Format("由单号为【{0}】的宇通配送单字段生成，收到货后请审核", ration_send_code));
            billingSql.Param.Add("payment_date", Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime.AddMonths(-1)).ToString());//付款日期为当前日期后一个月
            billingSql.sqlString = string.Format(@"insert into tb_parts_purchase_billing(purchase_billing_id,order_num,order_date,order_status,order_status_name,
order_type,order_type_name,sup_id,sup_code,sup_name,receipt_type,receipt_type_name,receipt_no,
contacts,contacts_tel,fax,trans_way,trans_way_name,payment_date,payment_limit,this_payment,sup_arrears,
balance_unit,balance_way,balance_way_name,whythe_discount,balance_account,balance_account_name,
delivery_man,check_number,delivery_address,balance_money,org_id,org_name,handle,handle_name,operators,
operator_name,create_by,create_name,create_time,enable_flag,is_lock,is_occupy,ration_send_code,remark)
select @purchase_billing_id,@order_num,@order_date,@order_status,@order_status_name,'1','采购收货',
'00000000000000000000000000000000' sup_id,'GYS-20140101-00000' sup_code,'宇通客车' sup_name,null receipt_type,null receipt_type_name,null receipt_no,
consignee contacts,consignee_tel contacts_tel,null fax,req_delivery trans_way,req_delivery_name trans_way_name,
@payment_date payment_date,null payment_limit,null this_payment,null sup_arrears,'宇通客车' balance_unit,null balance_way,
null balance_way_name,null whythe_discount,null balance_account,null balance_account_name,null delivery_man,
null check_number,delivery_address,0 balance_money,org_id,org_name,handle,handle_name,operators,
operator_name,create_by,create_name,{0},enable_flag,0 is_lock,0 is_occupy,@ration_send_code,@remark
 from tb_parts_purchase_order_2
 where crm_bill_id=@crm_bill_id", nowDate);
            listSql.Add(billingSql);
            //采购开单明细
            foreach (SUPartPutStore.Detail detail in details)
            {
                int num = (int)Convert.ToSingle(detail.send_count);
                SysSQLString detailSql = new SysSQLString();
                detailSql.cmdType = CommandType.Text;
                detailSql.Param = new Dictionary<string, string>();
                detailSql.Param.Add("crm_bill_id", dsn_adjustable_parts);
                detailSql.Param.Add("purchase_billing_id", billingID);
                detailSql.Param.Add("id", Guid.NewGuid().ToString());
                detailSql.Param.Add("parts_code", detail.car_parts_code);
                detailSql.Param.Add("num", num.ToString());
                detailSql.Param.Add("unit", detail.unit);
                detailSql.sqlString = string.Format(@"insert into tb_parts_purchase_billing_p(id,purchase_billing_id,num,wh_id,wh_code,wh_name,parts_code,
parts_name,parts_type_id,parts_type_name,parts_brand,parts_brand_name,unit_id,unit_name,parts_barcode,car_factory_code,drawing_num,model,business_counts,original_price,discount,business_price,
tax_rate,tax,payment,valorem_together,is_gift,relation_order,return_bus_count,storage_count,remark,
assisted_count,create_by,create_time,create_name,arrival_date,make_date)
select @id,@purchase_billing_id,num,'' wh_id,'' wh_code,'' wh_name,parts_code,parts_name,parts_type_id,parts_type_name,parts_brand,parts_brand_name,
unit_id,@unit unit_name,parts_barcode,car_factory_code,drawing_num,model,@num business_counts,price original_price,100 discount,price business_price,
null tax_rate,null tax,@num*price payment,@num*price+0 valorem_together,'0' is_gift,
b.order_num relation_order,null retum_bus_count,null storage_count,null remark,0 assisted_count,
a.create_by,{0},a.create_name,0 arrival_date,0 make_date
from tb_parts_purchase_order_p_2 a 
inner join tb_parts_purchase_order_2 b on a.purchase_order_yt_id=b.purchase_order_yt_id
where b.crm_bill_id=@crm_bill_id and a.car_factory_code=@parts_code", nowDate);
                listSql.Add(detailSql);
            }
            //采购开单总金额
            SysSQLString sqlAllmoney = new SysSQLString();
            sqlAllmoney.cmdType = CommandType.Text;
            sqlAllmoney.Param = new Dictionary<string, string>();
            sqlAllmoney.Param.Add("purchase_billing_id", billingID);
            sqlAllmoney.sqlString = @"update a set allmoney=b.valorem_together from tb_parts_purchase_billing
a inner join 
(select purchase_billing_id,sum(valorem_together) valorem_together from tb_parts_purchase_billing_p group by purchase_billing_id)
b  on a.purchase_billing_id=b.purchase_billing_id
where a.purchase_billing_id=@purchase_billing_id";
            listSql.Add(sqlAllmoney);
            #endregion
            #region 配送单
            string distribution_id = Guid.NewGuid().ToString();//配送单ID

            SysSQLString sqlDistribution = new SysSQLString();
            sqlDistribution.cmdType = CommandType.Text;
            sqlDistribution.Param = new Dictionary<string, string>();
            sqlDistribution.Param.Add("distribution_id", distribution_id);
            sqlDistribution.Param.Add("ration_send_code", result.ration_send_code);
            sqlDistribution.Param.Add("dsn_adjustable_parts", result.dsn_adjustable_parts);
            sqlDistribution.Param.Add("distribution_status", "1");
            sqlDistribution.sqlString = @"insert into tb_distribution (distribution_id,ration_send_code,dsn_adjustable_parts,distribution_status)
                                        values (@distribution_id,@ration_send_code,@dsn_adjustable_parts,@distribution_status)";
            listSql.Add(sqlDistribution);
            foreach (SUPartPutStore.Detail detail in details)
            {
                SysSQLString sqlDetail = new SysSQLString();
                sqlDetail.cmdType = CommandType.Text;
                sqlDetail.Param = new Dictionary<string, string>();
                sqlDetail.Param.Add("distribution_parts_id", Guid.NewGuid().ToString());
                sqlDetail.Param.Add("distribution_id", distribution_id);
                sqlDetail.Param.Add("car_parts_code", detail.car_parts_code);
                sqlDetail.Param.Add("unit", detail.unit);
                sqlDetail.Param.Add("send_count", detail.send_count);
                sqlDetail.sqlString = @"insert into tb_distribution_parts (distribution_parts_id,distribution_id,car_parts_code,unit,send_count)
                                    values (@distribution_parts_id,@distribution_id,@car_parts_code,@unit,@send_count)";
                listSql.Add(sqlDetail);
            }
            #endregion
            if (DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup("生成采购单", GlobalStaticObj_Server.Instance.MainAccCode, listSql))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary> 16.配件入库单查询
        /// </summary>
        /// <param name="ration_send_code">配送单号,入库状态</param>
        /// <returns>-1调用接口失败，0成功</returns>
        public static bool LoadPartInStore(string ration_send_code)
        {
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return true;
            }
            Utility.Log.Log.writeLineToLog("【配件入库单查询】" + ration_send_code, "接口");
            //string[] parts = ration_send_code.Split(',');
            //if (parts.Length != 2)
            //{
            //    return false;
            //}
            //ration_send_code = parts[0];
            //string state_code = parts[1];
            string state_code = "100000001";
            ServicePointManager.ServerCertificateValidationCallback = WebServUtil.ValidateServerCertificate;

            QueryPartInStore.clientInfo clientInfo = new QueryPartInStore.clientInfo();
            clientInfo.clientID = GlobalStaticObj_YT.ClientID;
            clientInfo.serviceID = "partInStoreQuery";
            QueryPartInStore.partInStoreQueryService serv = new QueryPartInStore.partInStoreQueryService();
            ration_send_code = Secret.Encrypt3DES_UTF8(ration_send_code, GlobalStaticObj_YT.KeySecurity_YT);//加密字段
            state_code = Secret.Encrypt3DES_UTF8(state_code, GlobalStaticObj_YT.KeySecurity_YT);
            string stationCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_YT.SAPCode, GlobalStaticObj_YT.KeySecurity_YT);
            //string stationCode = Secret.Encrypt3DES_UTF8("0000152877", GlobalStaticObj_YT.KeySecurity_YT);
            string dateStr = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss"), GlobalStaticObj_YT.KeySecurity_YT);
            string requestType = Secret.Encrypt3DES_UTF8("UPDATE", GlobalStaticObj_YT.KeySecurity_YT);
            clientInfo = WebServUtil.EncModel<QueryPartInStore.clientInfo>(clientInfo);
            QueryPartInStore.Result result = serv.partInStoreQuery(stationCode, dateStr, requestType, ration_send_code, state_code, clientInfo);
            string state = Secret.Decrypt3DES_UTF8(result.state, GlobalStaticObj_YT.KeySecurity_YT);
            if (state == "F")
            {
                string mess = Secret.Decrypt3DES_UTF8(result.errorMsg, GlobalStaticObj_YT.KeySecurity_YT);
                Utility.Log.Log.writeLineToLog("【配件入库单查询】" + mess, "接口");
                return false;
            }
            result = WebServUtil.DesModel(result);
            //更新配送单状态为已收货
            ration_send_code = Secret.Decrypt3DES_UTF8(ration_send_code, GlobalStaticObj_YT.KeySecurity_YT);
            string sql = string.Format("update tb_distribution set distribution_status='2' where ration_send_code='{0}'", ration_send_code);
            DBHelper.ExtNonQuery("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, sql, CommandType.Text, null);
            //删除云平台缓存,防止重复调用
            FactoryTemp.DeleteFactory(ration_send_code, DataSources.EnumBillType.PartPurChase, DataSources.EnumOperateObj.Data);
            //记录返回数据
            Utility.Log.Log.writeLineToLog("【配件入库单查询】返回\r\n" + Newtonsoft.Json.JsonConvert.SerializeObject(result), "接口");
            return true;
        }

        /// <summary> 17. 配件采购单状态查询
        /// </summary>
        /// <param name="crm_bill_id">采购订单号</param>
        /// <returns></returns>
        public static bool LoadPartPurchaseStauts(string crm_bill_id)
        {
            Utility.Log.Log.writeLineToLog(string.Format("【配件采购单状态查询】{0},{1},{2}", crm_bill_id, GlobalStaticObj_YT.SAPCode, GlobalStaticObj_YT.ClientID), "接口");
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(yuTongWebService.GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return true;
            }
            //crm_bill_id = "PJCG201410160004";
            ServicePointManager.ServerCertificateValidationCallback = WebServUtil.ValidateServerCertificate;

            QueryPartPurchaseStauts.clientInfo clientInfo = new QueryPartPurchaseStauts.clientInfo();
            clientInfo.clientID = GlobalStaticObj_YT.ClientID;
            clientInfo.serviceID = "partPurchaseStautsQuery";
            QueryPartPurchaseStauts.partPurchaseStautsQueryService serv = new QueryPartPurchaseStauts.partPurchaseStautsQueryService();
            crm_bill_id = Secret.Encrypt3DES_UTF8(crm_bill_id, GlobalStaticObj_YT.KeySecurity_YT);//加密字段
            string stationCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_YT.SAPCode, GlobalStaticObj_YT.KeySecurity_YT);
            //string stationCode = Secret.Encrypt3DES_UTF8("0000152877", GlobalStaticObj_YT.KeySecurity_YT);
            string dateStr = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss"), GlobalStaticObj_YT.KeySecurity_YT);
            string requestType = Secret.Encrypt3DES_UTF8("QUERY", GlobalStaticObj_YT.KeySecurity_YT);
            clientInfo = WebServUtil.EncModel<QueryPartPurchaseStauts.clientInfo>(clientInfo);
            QueryPartPurchaseStauts.Result result = serv.partPurchaseStautsQuery(stationCode, dateStr, requestType, crm_bill_id, clientInfo);
            string state = Secret.Decrypt3DES_UTF8(result.state, GlobalStaticObj_YT.KeySecurity_YT);
            if (state == "F")
            {
                string errMsg = Secret.Decrypt3DES_UTF8(result.errorMsg, GlobalStaticObj_YT.KeySecurity_YT);
                Utility.Log.Log.writeLineToLog("【配件采购单状态查询】" + errMsg, "接口");
                return false;
            }
            result = WebServUtil.DesModel(result);
            Utility.Log.Log.writeLineToLog("【配件采购单状态查询】\r\n" + Newtonsoft.Json.JsonConvert.SerializeObject(result), "接口");
            crm_bill_id = Secret.Decrypt3DES_UTF8(crm_bill_id, GlobalStaticObj_YT.KeySecurity_YT);// 
            //删除云平台缓存,防止重复调用
            FactoryTemp.DeleteFactory(crm_bill_id, DataSources.EnumBillType.PartPurChase, DataSources.EnumOperateObj.State);
            if (string.IsNullOrEmpty(result.approval_record))
            {
                return false;
            }
            bool flag = false;
            #region 更新宇通采购单审核
            try
            {
                //Dictionary<string, string> dic = new Dictionary<string, string>();
                string order_status = "order_status_yt_" + result.order_status_yt;
                //dic.Add("order_status_yt", order_status);//服务单状态
                //dic.Add("order_status_yt_name", WebServUtil.GetYTDicName(order_status));
                //dic.Add("approval_record", result.approval_record);//审批记录
                List<SysSQLString> listSql = new List<SysSQLString>();
                SysSQLString sqlYT = new SysSQLString();
                sqlYT.cmdType = CommandType.Text;
                sqlYT.Param = new Dictionary<string, string>();
                sqlYT.Param.Add("order_status_yt", order_status);//服务单状态
                sqlYT.Param.Add("order_status_yt_name", WebServUtil.GetYTDicName(order_status));
                sqlYT.Param.Add("approval_record", result.approval_record);//审批记录
                sqlYT.Param.Add("crm_bill_id", result.crm_bill_id);
                sqlYT.sqlString = "update tb_parts_purchase_order_2 set order_status_yt=@order_status_yt,order_status_yt_name=@order_status_yt_name,approval_record=@approval_record where crm_bill_id=@crm_bill_id";
                listSql.Add(sqlYT);
                //如果宇通状态为“审核未通过”，则修改本地状态
                if (result.order_status_yt == "100000003")
                {
                    SysSQLString sqlStatus = new SysSQLString();
                    sqlStatus.cmdType = CommandType.Text;
                    sqlStatus.Param = new Dictionary<string, string>();
                    sqlStatus.Param.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString());
                    sqlStatus.Param.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.NOTAUDIT, true));
                    sqlStatus.Param.Add("crm_bill_id", result.crm_bill_id);
                    sqlStatus.sqlString = "update tb_parts_purchase_order_2 set order_status=@order_status,order_status_name=@order_status_name where crm_bill_id=@crm_bill_id";
                    listSql.Add(sqlStatus);
                }
                flag = DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup("宇通：宇通采购单审核", GlobalStaticObj_Server.Instance.MainAccCode, listSql);
                //if (DBHelper.Submit_AddOrEdit("宇通：宇通采购单审核", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_parts_purchase_order_2", "crm_bill_id", Secret.Decrypt3DES_UTF8(result.crm_bill_id, GlobalStaticObj_YT.KeySecurity_YT), dic))
                //{
                //    flag = true;
                //}
            }
            catch (Exception ex)
            {
                flag = false;
            }
            #endregion
            return flag;
        }

        /// <summary> 18. 配件采购单--创建/更新
        /// </summary>
        /// <param name="partsPurchase">宇通采购订单</param>
        /// <returns></returns>
        public static bool UpLoadPartPurchase(string jsonStr)
        {
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return true;
            }
            #region 测试数据
            //partsPurchase = new Model.tb_parts_purchase_order_2();
            //partsPurchase.crm_bill_id = "";
            //partsPurchase.order_status_yt = "100000001";
            //partsPurchase.dsn_adjustable_parts = "";
            //partsPurchase.application_name = "0000152877";
            //partsPurchase.application_code = "0000152877";
            //partsPurchase.order_type = "100000000";
            //partsPurchase.emergency_level = "100000001";
            //partsPurchase.price_type = "";
            //partsPurchase.cust_name = "张三";
            //partsPurchase.cust_type = "";
            //partsPurchase.center_library_name = "宇通中心站";
            //partsPurchase.create_time = Common.LocalDateTimeToUtcLong(DateTime.Parse("2014-11-19"));
            //partsPurchase.req_delivery_time = Common.LocalDateTimeToUtcLong(DateTime.Parse("2014-11-20"));
            //partsPurchase.req_delivery_name = "100000001";
            //partsPurchase.consignee = "李鹏";
            //partsPurchase.consignee_tel = "13523584001";
            //partsPurchase.delivery_address = "地址";
            //partsPurchase.remark = "收货";
            //Model.tb_parts_purchase_order_p_2 detailPart = new Model.tb_parts_purchase_order_p_2();
            //detailPart.id = "9398a3d3-c48b-4987-abcd-aee2614ed795";
            //detailPart.car_factory_code = "8212-00129";
            //detailPart.application_count = 3;
            //detailPart.price = 4;
            //detailPart.parts_explain = "";
            //partsPurchase.listDetails = new List<Model.tb_parts_purchase_order_p_2>();
            //partsPurchase.listDetails.Add(detailPart);
            #endregion
            tb_parts_purchase_order_2 partsPurchase = Newtonsoft.Json.JsonConvert.DeserializeObject<tb_parts_purchase_order_2>(jsonStr);
            if (partsPurchase == null)
            {
                return false;
            }

            SUPartPurchase.partPurchase purchase = new SUPartPurchase.partPurchase();
            string requestType = string.IsNullOrEmpty(partsPurchase.crm_bill_id) ? "CREATE" : "UPDATE";
            purchase.crm_bill_id = partsPurchase.crm_bill_id;//?CRM单据编号--ID
            //purchase.order_status = WebServUtil.GetYTDicCode("order_status", partsPurchase.order_status_yt);//订单状态--宇通单据状态
            purchase.order_status = "100000001";//未提交
            purchase.dsn_adjustable_parts = partsPurchase.dsn_adjustable_parts;//?调件单号--宇通单号
            purchase.application_name = GlobalStaticObj_YT.SAPCode;//申请单位名称
            purchase.application_code = GlobalStaticObj_YT.SAPCode;//申请单位编码
            //purchase.application_code = "0000152877";
            //purchase.application_name = "0000152877";
            //purchase.order_type = WebServUtil.GetYTDicCode("order_type", partsPurchase.order_type);//申请类型--订单类型
            purchase.order_type = partsPurchase.order_type.Replace("order_type_", "");
            purchase.emergency_level = WebServUtil.GetYTDicCode("emergency_level", partsPurchase.emergency_level);//紧急程度
            purchase.price_type = partsPurchase.price_type;//价格类型
            purchase.cust_name = partsPurchase.cust_name;//客户名称
            purchase.cust_type = partsPurchase.cust_type;//客户单位
            purchase.center_library = partsPurchase.center_library_name;//中心库站
            
                DateTime dtApply = Common.UtcLongToLocalDateTime(partsPurchase.create_time);
                purchase.apply_date_time = dtApply.ToString("yyyy-MM-dd HH:mm:ss");//申请时间
            
                DateTime dtDeliver = Common.UtcLongToLocalDateTime(Convert.ToInt64(partsPurchase.req_delivery_time));
                purchase.arrival_date_time = dtDeliver.ToString("yyyy-MM-dd HH:mm:ss");//期望到货时间
           
            //purchase.req_delivery_name = WebServUtil.GetYTDicCode("req_delivery_name", partsPurchase.req_delivery);//要求发货方式
            purchase.req_delivery_name = "100000001";//默认用“公路”
            //purchase.consignee = partsPurchase.consignee;//收货人
            purchase.consignee = partsPurchase.consignee_code;//收货人ID
            purchase.consignee_tel = partsPurchase.consignee_tel;//收货人电话
            purchase.consignee_add = partsPurchase.delivery_address;//收货人地址
            purchase.remark = partsPurchase.remark;//备注
            if (partsPurchase.listDetails != null && partsPurchase.listDetails.Count > 0)
            {
                int detailCount = partsPurchase.listDetails.Count;
                purchase.PartDetails = new SUPartPurchase.PartDetail[detailCount];
                for (int i = 0; i < detailCount; i++)
                {
                    yuTongWebService.SUPartPurchase.PartDetail detail = new SUPartPurchase.PartDetail();
                    detail.parts_id = partsPurchase.listDetails[i].id;//ID
                    detail.car_parts_code = partsPurchase.listDetails[i].car_factory_code;//配件编号
                    detail.application_count = partsPurchase.listDetails[i].application_count.ToString();//申请数量
                    detail.pice = partsPurchase.listDetails[i].price.ToString();//单价
                    detail.parts_explain = partsPurchase.listDetails[i].parts_explain;//配件说明
                    purchase.PartDetails[i] = detail;
                }
            }
            else
            {
                purchase.PartDetails = new SUPartPurchase.PartDetail[0];
            }
            Utility.Log.Log.writeLineToLog("【配件采购单--创建/更新】\r\n" + Newtonsoft.Json.JsonConvert.SerializeObject(purchase), "接口");
            purchase.PartDetails = WebServUtil.EncList(purchase.PartDetails);
            purchase = WebServUtil.EncModel(purchase);//加密数据
            ServicePointManager.ServerCertificateValidationCallback = WebServUtil.ValidateServerCertificate;
            SUPartPurchase.clientInfo clientInfo = new SUPartPurchase.clientInfo();
            clientInfo.clientID = GlobalStaticObj_YT.ClientID;
            clientInfo.serviceID = "partPurchaseSU";
            SUPartPurchase.partPurchaseSUService serv = new SUPartPurchase.partPurchaseSUService();
            //string stationCode = Secret.Encrypt3DES_UTF8("0000152877", GlobalStaticObj_YT.KeySecurity_YT);
            string stationCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_YT.SAPCode, GlobalStaticObj_YT.KeySecurity_YT);
            string dateStr = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss"), GlobalStaticObj_YT.KeySecurity_YT);
            requestType = Secret.Encrypt3DES_UTF8(requestType, GlobalStaticObj_YT.KeySecurity_YT);
            clientInfo = WebServUtil.EncModel<SUPartPurchase.clientInfo>(clientInfo);
            SUPartPurchase.Result result = serv.partPurchaseSU(stationCode, dateStr, requestType, purchase, clientInfo);
            string state = Secret.Decrypt3DES_UTF8(result.state, GlobalStaticObj_YT.KeySecurity_YT);
            //判断调用是否成功
            if (state == "F")
            {
                string errMsg = Secret.Decrypt3DES_UTF8(result.errorMsg, GlobalStaticObj_YT.KeySecurity_YT);
                Utility.Log.Log.writeLineToLog("【配件采购单--创建/更新】" + errMsg, "接口");
                return false;
            }
            #region 更新宇通采购单
            bool flag = false;
            try
            {
                result = WebServUtil.DesModel(result);
                Utility.Log.Log.writeLineToLog("【配件采购单--创建/更新】返回数据：\r\n" + Newtonsoft.Json.JsonConvert.SerializeObject(result), "接口");
                if (!string.IsNullOrEmpty(partsPurchase.purchase_order_yt_id))
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("dsn_adjustable_parts", result.dsn_adjustable_parts);
                    dic.Add("crm_bill_id", result.crm_bill_id);
                    if (DBHelper.Submit_AddOrEdit("宇通：宇通采购单上传", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_parts_purchase_order_2", "purchase_order_yt_id", partsPurchase.purchase_order_yt_id, dic))
                    {
                        flag = true;
                    }
                }
                else
                {
                    flag = false;
                }
            }
            catch (Exception ex)
            {
                flag = false;
            }
            #endregion
            return flag;
        }

        /// <summary> 三包服务单创建/更新
        /// </summary>
        /// <param name="contactModel">三包服务单实体</param>
        /// <returns>返回错误信息，如果不为空，则操作失败</returns>
        public static bool UpLoadServiceOrder(string jsonStr)
        {
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return true;
            }
            Model.serviceorder model = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.serviceorder>(jsonStr);//取出tg_id
            jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            string tg_id = model.tg_id;
            //jsonStr = System.IO.File.ReadAllText("tmp\\serviceorder.txt", Encoding.UTF8);
            SUServiceOrder.serviceOrder orderModel = Newtonsoft.Json.JsonConvert.DeserializeObject<SUServiceOrder.serviceOrder>(jsonStr);
            orderModel.sap_code = GlobalStaticObj_YT.SAPCode;
            YuTongDic ytDic = new YuTongDic();
            if (orderModel.FilesDetails == null)
            {
                orderModel.FilesDetails = new SUServiceOrder.Files[0];
                model.FilesDetails = new Files[0];
            }
            if (orderModel.ChangePartsDetails == null)
            {
                orderModel.ChangePartsDetails = new SUServiceOrder.ChangePartsDetail[0];
            }
            else
            {
                foreach (SUServiceOrder.ChangePartsDetail detail in orderModel.ChangePartsDetails)
                {
                    detail.parts_source = ytDic.GetYTDicCode("new_parts_source_yt", detail.parts_source);//新件来源
                }
            }
            if (orderModel.RepairItemsDetails == null)
            {
                orderModel.RepairItemsDetails = new SUServiceOrder.RepairItems[0];
            }

            orderModel.bill_type_yt = ytDic.GetYTDicCode("bill_type_yt", orderModel.bill_type_yt);//单据类型
            orderModel.vehicle_use = ytDic.GetYTDicCode("custom_property_yt", orderModel.vehicle_use);//用户性质（车辆用途）
            orderModel.travel_lookup_code = ytDic.GetYTDicCode("traffic_mode_yt", orderModel.travel_lookup_code);//交通方式
            orderModel.fault_cause = ytDic.GetYTDicCode("cause_fault_yt", orderModel.fault_cause);//故障原因
            orderModel.policy_cost_type = ytDic.GetYTDicCode("cost_type_care_policy_yt", orderModel.policy_cost_type);//费用类型（政策照顾）
            orderModel.fault_duty_corp = ytDic.GetYTDicCode("fault_company_yt", orderModel.fault_duty_corp);//故障责任单位
            orderModel.part_guarantee_period = ytDic.GetYTDicCode("parts_warranty_agreement_yt", orderModel.part_guarantee_period);//配件协议包修期
            orderModel.luxury_cost_type = ytDic.GetYTDicCode("luxury_cost_type", orderModel.luxury_cost_type);//费用类型（高档车）
            orderModel.sap_code = GlobalStaticObj_YT.SAPCode;


            string requestType = string.IsNullOrEmpty(orderModel.crm_service_bill_code) ? "CREATE" : "UPDATE";
            ServicePointManager.ServerCertificateValidationCallback = WebServUtil.ValidateServerCertificate;
            SUServiceOrder.clientInfo clientInfo = new SUServiceOrder.clientInfo();
            clientInfo.clientID = GlobalStaticObj_YT.ClientID;
            clientInfo.serviceID = "serviceOrderSU";
            SUServiceOrder.serviceOrderSUService serv = new SUServiceOrder.serviceOrderSUService();
            string stationCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_YT.SAPCode, GlobalStaticObj_YT.KeySecurity_YT);
            //string name = Secret.Encrypt3DES_UTF8("张三", GlobalStaticObj_YT.KeySecurity_YT);
            //string name1 = Secret.Decrypt3DES_UTF8(name, GlobalStaticObj_YT.KeySecurity_YT);
            string dateStr = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss"), GlobalStaticObj_YT.KeySecurity_YT);
            requestType = Secret.Encrypt3DES_UTF8(requestType, GlobalStaticObj_YT.KeySecurity_YT);
            clientInfo = WebServUtil.EncModel<SUServiceOrder.clientInfo>(clientInfo);
            orderModel = WebServUtil.EncModel<SUServiceOrder.serviceOrder>(orderModel);
            orderModel.ChangePartsDetails = WebServUtil.EncList<SUServiceOrder.ChangePartsDetail>(orderModel.ChangePartsDetails);
            orderModel.RepairItemsDetails = WebServUtil.EncList<SUServiceOrder.RepairItems>(orderModel.RepairItemsDetails);
            orderModel.FilesDetails = WebServUtil.EncList<SUServiceOrder.Files>(orderModel.FilesDetails);

            SUServiceOrder.Result result = serv.serviceOrderSU(stationCode, dateStr, requestType, orderModel, clientInfo);
            #region 记录日志
            orderModel = WebServUtil.DesModel(orderModel);
            orderModel.ChangePartsDetails = WebServUtil.DesList(orderModel.ChangePartsDetails);
            orderModel.RepairItemsDetails = WebServUtil.DesList(orderModel.RepairItemsDetails);
            orderModel.FilesDetails = WebServUtil.DesList(orderModel.FilesDetails);
            foreach (SUServiceOrder.Files file in orderModel.FilesDetails)
            {
                if (!string.IsNullOrEmpty(file.Doc))
                {
                    file.Doc = ".....";
                }
            }
            Utility.Log.Log.writeLineToLog("【三包服务单创建/更新】\r\n" + Newtonsoft.Json.JsonConvert.SerializeObject(orderModel), "接口");
            #endregion
            string state = Secret.Decrypt3DES_UTF8(result.state, GlobalStaticObj_YT.KeySecurity_YT);
            if (state == "F")
            {
                string errMsg = Secret.Decrypt3DES_UTF8(result.errorMsg, GlobalStaticObj_YT.KeySecurity_YT);
                Utility.Log.Log.writeLineToLog("【三包服务单-创建/更新】" + errMsg, "接口");
                //return "宇通接口错误:" + errMsg;
                return false;
            }
            else
            {
                result = WebServUtil.DesModel(result);
                Utility.Log.Log.writeLineToLog("【三包服务单-创建/更新】返回数据\r\n" + Newtonsoft.Json.JsonConvert.SerializeObject(result), "接口");
                Dictionary<string, string> dicFields = new Dictionary<string, string>();
                dicFields.Add("service_no_yt", result.crm_service_bill_code);
                dicFields.Add("series_num_yt", result.dsn_service_bill_code);
                bool flag = DBHelper.Submit_AddOrEdit("更新三包服务单:宇通service_no_yt,series_num_yt", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_maintain_three_guaranty", "tg_id", tg_id, dicFields);
                //return flag ? "" : "DB错误:更新三包服务单失败:宇通宇通crm_service_bill_code,dsn_service_bill_code";
                return flag;
            }
        }

        /// <summary> 宇通中心站-DSN 库存查询
        /// </summary>
        /// <returns></returns>
        public static bool LoadStoreCenter(string carPartsCode)
        {
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return true;
            }
            //carPartsCode = "8111-00250";
            Utility.Log.Log.writeLineToLog("【【宇通中心站-DSN库存查询】" + carPartsCode, "接口");
            ServicePointManager.ServerCertificateValidationCallback = WebServUtil.ValidateServerCertificate;
            QueryPartStoreCenter.clientInfo clientInfo = new QueryPartStoreCenter.clientInfo();
            clientInfo.clientID = GlobalStaticObj_YT.ClientID;
            clientInfo.serviceID = "partStoreCenterQuery";
            QueryPartStoreCenter.partStoreCenterQueryService serv = new QueryPartStoreCenter.partStoreCenterQueryService();
            string stationCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_YT.SAPCode, GlobalStaticObj_YT.KeySecurity_YT);
            string dateStr = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss"), GlobalStaticObj_YT.KeySecurity_YT);
            string requestType = Secret.Encrypt3DES_UTF8("QUERY", GlobalStaticObj_YT.KeySecurity_YT);
            carPartsCode = Secret.Encrypt3DES_UTF8(carPartsCode, GlobalStaticObj_YT.KeySecurity_YT);
            clientInfo = WebServUtil.EncModel<QueryPartStoreCenter.clientInfo>(clientInfo);
            QueryPartStoreCenter.Result result = serv.partStoreCenterQuery(stationCode, dateStr, requestType, carPartsCode, clientInfo);
            string state = Secret.Decrypt3DES_UTF8(result.state, GlobalStaticObj_YT.KeySecurity_YT);
            if (state == "F")
            {
                string errMsg = Secret.Decrypt3DES_UTF8(result.errorMsg, GlobalStaticObj_YT.KeySecurity_YT);
                Utility.Log.Log.writeLineToLog("【宇通中心站-DSN库存查询】" + errMsg, "接口");
                return false;
            }
            QueryPartStoreCenter.Detail[] detailArr = result.Details;
            if (detailArr.Length == 0)
            {
                return false;
            }
            detailArr = WebServUtil.DesList<QueryPartStoreCenter.Detail>(detailArr);
            string nowTicks = Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString();
            List<SysSQLString> list = new List<SysSQLString>();
            SysSQLString sysSQLString = new SysSQLString();
            sysSQLString.cmdType = CommandType.Text;
            sysSQLString.Param = new Dictionary<string, string>();
            StringBuilder strSql = new StringBuilder();
            //bool isServiceStationExist = DBHelper.IsExist("判断服务站信息是否存在", "tb_company", "com_code='" + result.station.com_code + "'");
            //if (isServiceStationExist)
            //{
            //    #region 更新语句
            //    strSql.Append("update tb_company set ");
            //    strSql.Append(" parent_id= @parent_id , ");
            //    strSql.Append(" com_code= @com_code , ")MO;
            //    strSql.Append(" com_name= @com_name , ");
            //    strSql.Append(" com_short_name= @com_short_name , ");
            //    strSql.Append(" province= @province , ");            
            //    #endregion
            //}
            //else
            //{
            //    #region 插入语句
            //    strSql.Append("insert into tb_company(");
            //    strSql.Append("com_id,parent_id,com_code,com_name");
            //    strSql.Append(") values (");
            //    strSql.Append("@com_id,@parent_id,) ");
            //    #endregion

            //    sysSQLString.Param.Add("com_id", Guid.NewGuid().ToString());
            //    sysSQLString.Param.Add("create_by", GlobalStaticObj_Server.Instance.UserID);
            //    sysSQLString.Param.Add("create_time", nowTicks);
            //}
            //#region  参数项 47
            //sysSQLString.Param.Add("parent_id", result.station.parent);
            //sysSQLString.Param.Add("com_code", result.station.com_code);            

            //string dtApplyTime = "";
            //if (!String.IsNullOrEmpty(result.station.apply_time))
            //{
            //    dtApplyTime = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(result.station.apply_time)).ToString();
            //}
            //sysSQLString.Param.Add("apply_time", dtApplyTime);

            //string dtApprovedTime = "";
            //if (!String.IsNullOrEmpty(result.station.apply_time))
            //{
            //    dtApprovedTime = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(result.station.approved_time)).ToString();
            //}
            //sysSQLString.Param.Add("approved_time", dtApprovedTime);
            //sysSQLString.Param.Add("com_level", result.station.com_level);
            //sysSQLString.Param.Add("workhours_price", result.station.workhours_price);
            //sysSQLString.Param.Add("winter_subsidy", result.station.winter_subsidy);
            //#endregion
            //sysSQLString.sqlString = strSql.ToString();
            //list.Add(sysSQLString);

            //bool flag = DBHelper.BatchExeSQLStringMultiByTrans("宇通：同步服务站信息", list);
            //if (!flag)
            //{
            //    return -1;
            //}
            return true;
        }

        /// <summary> 维修结算单查询
        /// </summary>
        /// <returns>返回标识：1：成功 0：无数数据 -1：失败</returns>
        public static bool QuerySettleAccounts(string settlementNo)
        {
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return true;
            }
            Utility.Log.Log.writeLineToLog("【维修结算单查询】" + settlementNo, "接口");
            //settlementNo = "80030601";
            ServicePointManager.ServerCertificateValidationCallback = WebServUtil.ValidateServerCertificate;
            QueryServiceSettleAccounts.clientInfo clientInfo = new QueryServiceSettleAccounts.clientInfo();
            clientInfo.clientID = GlobalStaticObj_YT.ClientID;
            clientInfo.serviceID = "serviceSettleAccountsQuery";
            QueryServiceSettleAccounts.serviceSettleAccountsQueryService serv = new QueryServiceSettleAccounts.serviceSettleAccountsQueryService();
            string stationCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_YT.SAPCode, GlobalStaticObj_YT.KeySecurity_YT);
            string dateStr = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss"), GlobalStaticObj_YT.KeySecurity_YT);
            string requestType = Secret.Encrypt3DES_UTF8("QUERY", GlobalStaticObj_YT.KeySecurity_YT);
            settlementNo = Secret.Encrypt3DES_UTF8(settlementNo, GlobalStaticObj_YT.KeySecurity_YT);
            clientInfo = WebServUtil.EncModel<QueryServiceSettleAccounts.clientInfo>(clientInfo);
            QueryServiceSettleAccounts.Result result = serv.serviceSettleAccountsQuery(stationCode, dateStr, requestType, settlementNo, clientInfo);
            string state = Secret.Decrypt3DES_UTF8(result.state, GlobalStaticObj_YT.KeySecurity_YT);
            if (state == "F")
            {
                string errMsg = Secret.Decrypt3DES_UTF8(result.errorMsg, GlobalStaticObj_YT.KeySecurity_YT);
                Utility.Log.Log.writeLineToLog("【维修结算单查询】" + errMsg, "接口");
                return false;
            }
            QueryServiceSettleAccounts.ResultServiceDetail[] servArr = result.ServiceDetails;
            QueryServiceSettleAccounts.ResultOldPartDetail[] oldPartArr = result.OldPartDetails;
            QueryServiceSettleAccounts.ResultOtherFeeDetail[] otherFeeArr = result.OtherFeeDetails;
            result = WebServUtil.DesModel<QueryServiceSettleAccounts.Result>(result);

            servArr = WebServUtil.DesList<QueryServiceSettleAccounts.ResultServiceDetail>(servArr);
            oldPartArr = WebServUtil.DesList<QueryServiceSettleAccounts.ResultOldPartDetail>(oldPartArr);
            otherFeeArr = WebServUtil.DesList<QueryServiceSettleAccounts.ResultOtherFeeDetail>(otherFeeArr);
            //记录返回数据
            Utility.Log.Log.writeLineToLog("【维修结算单查询】返回数据\r\n" + Newtonsoft.Json.JsonConvert.SerializeObject(result), "接口");
            //删除云平台缓存,防止重复调用
            FactoryTemp.DeleteFactory(Secret.Decrypt3DES_UTF8(settlementNo, GlobalStaticObj_YT.KeySecurity_YT), DataSources.EnumBillType.ServiceSettle, DataSources.EnumOperateObj.Data);
            if (string.IsNullOrEmpty(result.settlement_no))
            {
                return false;
            }
            string nowTicks = Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString();
            List<SysSQLString> list = new List<SysSQLString>();
            SysSQLString sysSQLString = new SysSQLString();
            sysSQLString.cmdType = CommandType.Text;
            sysSQLString.Param = new Dictionary<string, string>();
            StringBuilder strSql = new StringBuilder();

            #region 三包结算单
            string st_id = DBHelper.GetSingleValue("获取三包结算单id", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_maintain_three_guaranty_settlement", "st_id", "settlement_no='" + result.settlement_no + "'", "");
            if (!string.IsNullOrEmpty(st_id))
            {
                #region 更新语句
                strSql.Append("update tb_maintain_three_guaranty_settlement set ");
                strSql.Append("cust_id='00000000000000000000000000000000',");//宇通客户
                strSql.Append(" service_station_code= @service_station_code , ");
                strSql.Append(" service_station_name= @service_station_name , ");
                strSql.Append(" bill_sum_cost= @bill_sum_cost , ");
                strSql.Append(" sum_cost= @sum_cost , ");
                strSql.Append(" oldpart_sum_cost= @oldpart_sum_cost , ");
                strSql.Append(" other_sum_cost= @other_sum_cost , ");
                strSql.Append(" settlement_date= @settlement_date , ");
                strSql.Append("settlement_cycle_start=@settlement_cycle_start,");
                strSql.Append("settlement_cycle_end=@settlement_cycle_end,");
                strSql.Append(" info_status= @info_status , ");
                strSql.Append(" update_time= @update_time , ");
                strSql.Append(" enable_flag= @enable_flag , ");
                strSql.Append(" update_by= @update_by , ");
                strSql.Append(" update_name= @update_name  ");
                strSql.Append(" where settlement_no=@settlement_no  ");
                #endregion
            }
            else
            {
                #region 插入语句
                strSql.Append("insert into tb_maintain_three_guaranty_settlement(");
                strSql.Append("st_id,cust_id,settlement_no,station_settlement_no,service_station_code,service_station_name,bill_sum_cost,sum_cost,oldpart_sum_cost,other_sum_cost,settlement_date,settlement_cycle_start,settlement_cycle_end,info_status,create_time,update_time,enable_flag,create_by,update_by,create_name,update_name");
                strSql.Append(") values (");
                strSql.Append("@st_id,'00000000000000000000000000000000',@settlement_no,@station_settlement_no,@service_station_code,@service_station_name,@bill_sum_cost,@sum_cost,@oldpart_sum_cost,@other_sum_cost,@settlement_date,@settlement_cycle_start,@settlement_cycle_end,@info_status,@create_time,@update_time,@enable_flag,@create_by,@update_by,@create_name,@update_name) ");
                #endregion
                st_id = Guid.NewGuid().ToString();
                sysSQLString.Param.Add("st_id", st_id);
                sysSQLString.Param.Add("create_by", GlobalStaticObj_Server.Instance.UserID);
                sysSQLString.Param.Add("create_name", GlobalStaticObj_Server.Instance.UserName);
                sysSQLString.Param.Add("create_time", nowTicks);
                CodingRule cr = new CodingRule(DataSources.EnumProjectType.ThreeGuarantyServiceSettlement);
                string station_settlement_no = cr.AddNewNo();//结算单号
                sysSQLString.Param.Add("station_settlement_no", station_settlement_no);
                #region 更新编码
                SysSQLString sqlRule = new SysSQLString();
                sqlRule.cmdType = CommandType.Text;
                sqlRule.Param = new Dictionary<string, string>();
                sqlRule.Param.Add("bill_code_rule_id", cr.ruleID);
                sqlRule.Param.Add("last_bill_no", station_settlement_no);
                sqlRule.sqlString = "update sys_bill_code_rule set last_bill_no=@last_bill_no where bill_code_rule_id=@bill_code_rule_id";
                list.Add(sqlRule);
                #endregion
            }
            #region  参数项 9
            sysSQLString.Param.Add("settlement_no", result.settlement_no);
            sysSQLString.Param.Add("service_station_code", result.service_station_code);
            sysSQLString.Param.Add("service_station_name", result.service_station_name);
            sysSQLString.Param.Add("bill_sum_cost", result.bill_sum_cost);
            sysSQLString.Param.Add("sum_cost", result.sum_cost);
            sysSQLString.Param.Add("oldpart_sum_cost", result.oldpart_sum_cost);
            sysSQLString.Param.Add("other_sum_cost", result.other_sum_cost);
            sysSQLString.Param.Add("settlement_date", result.settlement_date);
            sysSQLString.Param.Add("info_status", "D1FEDED3-23ED-EBB1-40EA-B613AD2F505A");//单据状态为“未确认”
            //结算周期
            if (result.settlement_cycle.Trim().Length > 0)
            {
                string[] sp = new string[] { "--" };
                string[] cycles = result.settlement_cycle.Trim().Split(sp, StringSplitOptions.None);
                if (cycles.Length == 2)
                {

                    sysSQLString.Param.Add("settlement_cycle_start", Common.LocalDateTimeToUtcLong(DateTime.Parse(cycles[0])).ToString());
                    sysSQLString.Param.Add("settlement_cycle_end", Common.LocalDateTimeToUtcLong(DateTime.Parse(cycles[1])).ToString());
                }
                else
                {
                    sysSQLString.Param.Add("settlement_cycle_start", null);
                    sysSQLString.Param.Add("settlement_cycle_end", null);
                }
            }
            sysSQLString.Param.Add("update_time", nowTicks);
            sysSQLString.Param.Add("enable_flag", ((int)DataSources.EnumEnableFlag.USING).ToString());
            sysSQLString.Param.Add("update_by", GlobalStaticObj_Server.Instance.UserID);
            sysSQLString.Param.Add("update_name", GlobalStaticObj_Server.Instance.UserName);
            #endregion
            sysSQLString.sqlString = strSql.ToString();
            list.Add(sysSQLString);
            #endregion

            #region 维修服务单
            foreach (QueryServiceSettleAccounts.ResultServiceDetail item in servArr)
            {
                sysSQLString = new SysSQLString();
                sysSQLString.cmdType = CommandType.Text;
                sysSQLString.Param = new Dictionary<string, string>();
                strSql = new StringBuilder();
                bool isOldExist = DBHelper.IsExist("判断维修服务单是否存在", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_maintain_three_guaranty_settlement_ser", "service_no='" + item.service_no + "'");
                if (isOldExist)
                {
                    #region 更新语句
                    strSql.Append("update tb_maintain_three_guaranty_settlement_ser set ");
                    strSql.Append(" submit_time= @submit_time , ");
                    strSql.Append(" st_id= @st_id , ");
                    strSql.Append(" depot_no= @depot_no , ");
                    strSql.Append(" receipt_type= @receipt_type , ");
                    strSql.Append(" repairer_name= @repairer_name , ");
                    strSql.Append(" approval_date= @approval_date , ");//审批通过日期
                    strSql.Append(" other_item_sum_money= @other_item_sum_money , ");
                    strSql.Append(" man_hour_sum_money= @man_hour_sum_money , ");
                    strSql.Append(" fitting_sum_money= @fitting_sum_money , ");
                    strSql.Append(" travel_cost= @travel_cost , ");
                    strSql.Append(" service_sum_cost= @service_sum_cost , ");
                    strSql.Append(" enable_flag= @enable_flag ");
                    strSql.Append(" where service_no=@service_no  ");
                    #endregion
                }
                else
                {
                    #region 插入语句

                    strSql.Append("insert into tb_maintain_three_guaranty_settlement_ser(");
                    strSql.Append("ser_id,service_no,st_id,submit_time,depot_no,receipt_type,repairer_name,approval_date,other_item_sum_money,man_hour_sum_money,fitting_sum_money,travel_cost,service_sum_cost,enable_flag");
                    strSql.Append(") values (");
                    strSql.Append("@ser_id,@service_no,@st_id,@submit_time,@depot_no,@receipt_type,@repairer_name,@approval_date,@other_item_sum_money,@man_hour_sum_money,@fitting_sum_money,@travel_cost,@service_sum_cost,@enable_flag) ");
                    #endregion
                    sysSQLString.Param.Add("ser_id", Guid.NewGuid().ToString());
                }
                #region 参数项 11
                sysSQLString.Param.Add("service_no", item.service_no);
                sysSQLString.Param.Add("st_id", st_id);
                string submit_time = "";
                //提交日期为空处理
                if (!String.IsNullOrEmpty(item.submit_time))
                {
                    submit_time = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(item.submit_time)).ToString();
                }
                sysSQLString.Param.Add("submit_time", submit_time);
                sysSQLString.Param.Add("depot_no", item.depot_no);
                sysSQLString.Param.Add("receipt_type", item.receipt_type);
                sysSQLString.Param.Add("repairer_name", item.repairer_name);
                string approval_date = "";
                //审批通过时间为空处理
                if (!String.IsNullOrEmpty(item.approval_date))
                {
                    approval_date = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(item.approval_date)).ToString();
                }
                sysSQLString.Param.Add("approval_date", approval_date);
                sysSQLString.Param.Add("other_item_sum_money", item.other_item_sum_money);
                sysSQLString.Param.Add("man_hour_sum_money", item.man_hour_sum_money);
                sysSQLString.Param.Add("fitting_sum_money", item.fitting_sum_money);
                sysSQLString.Param.Add("travel_cost", item.travel_cost);
                sysSQLString.Param.Add("service_sum_cost", item.service_sum_cost);

                sysSQLString.Param.Add("enable_flag", ((int)DataSources.EnumEnableFlag.USING).ToString());
                #endregion
                sysSQLString.sqlString = strSql.ToString();
                list.Add(sysSQLString);
            }
            #endregion

            #region 旧件结算单
            foreach (QueryServiceSettleAccounts.ResultOldPartDetail item in oldPartArr)
            {
                sysSQLString = new SysSQLString();
                sysSQLString.cmdType = CommandType.Text;
                sysSQLString.Param = new Dictionary<string, string>();
                strSql = new StringBuilder();
                bool isOldExist = DBHelper.IsExist("判断旧件结算单是否存在", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_maintain_three_guaranty_settlement_old", "recycle_no='" + item.recycle_no + "'");
                if (isOldExist)
                {
                    #region 更新语句
                    strSql.Append("update tb_maintain_three_guaranty_settlement_old set ");
                    strSql.Append(" service_station_code= @service_station_code , ");
                    strSql.Append(" service_station_name= @service_station_name , ");
                    strSql.Append(" sum_cost= @sum_cost , ");
                    strSql.Append(" st_id= @st_id , ");
                    strSql.Append(" enable_flag= @enable_flag ");
                    strSql.Append(" where recycle_no=@recycle_no  ");
                    #endregion
                }
                else
                {
                    #region 插入语句
                    strSql.Append("insert into tb_maintain_three_guaranty_settlement_old(");
                    strSql.Append("old_id,recycle_no,service_station_code,service_station_name,sum_cost,st_id,enable_flag");
                    strSql.Append(") values (");
                    strSql.Append("@old_id,@recycle_no,@service_station_code,@service_station_name,@sum_cost,@st_id,@enable_flag) ");
                    #endregion
                    sysSQLString.Param.Add("old_id", Guid.NewGuid().ToString());
                }
                #region 参数项 4
                sysSQLString.Param.Add("recycle_no", item.recycle_no);
                sysSQLString.Param.Add("service_station_code", item.service_station_code);
                sysSQLString.Param.Add("service_station_name", item.service_station_name);
                sysSQLString.Param.Add("sum_cost", item.sum_cost);
                sysSQLString.Param.Add("st_id", st_id);
                sysSQLString.Param.Add("enable_flag", ((int)DataSources.EnumEnableFlag.USING).ToString());
                #endregion
                sysSQLString.sqlString = strSql.ToString();
                list.Add(sysSQLString);
            }
            #endregion

            #region 其他费用结算单
            foreach (QueryServiceSettleAccounts.ResultOtherFeeDetail item in otherFeeArr)
            {
                sysSQLString = new SysSQLString();
                sysSQLString.cmdType = CommandType.Text;
                sysSQLString.Param = new Dictionary<string, string>();
                strSql = new StringBuilder();
                bool isOldExist = DBHelper.IsExist("判断其他费用结算单是否存在", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_maintain_three_guaranty_settlement_oth", "other_cost_no='" + item.other_cost_no + "'");
                if (isOldExist)
                {
                    #region 更新语句
                    strSql.Append("update tb_maintain_three_guaranty_settlement_oth set ");
                    strSql.Append(" cost_name= @cost_name , ");
                    strSql.Append(" cost_type= @cost_type , ");
                    strSql.Append(" tally_time= @tally_time , ");
                    strSql.Append(" cost_explain= @cost_explain , ");
                    strSql.Append(" sum_cost= @sum_cost , ");
                    strSql.Append(" st_id= @st_id , ");
                    strSql.Append(" enable_flag= @enable_flag  ");
                    strSql.Append(" where other_cost_no=@other_cost_no  ");
                    #endregion
                }
                else
                {
                    #region 插入语句
                    strSql.Append("insert into tb_maintain_three_guaranty_settlement_oth(");
                    strSql.Append("cost_id,other_cost_no,cost_name,cost_type,tally_time,cost_explain,sum_cost,st_id,enable_flag");
                    strSql.Append(") values (");
                    strSql.Append("@cost_id,@other_cost_no,@cost_name,@cost_type,@tally_time,@cost_explain,@sum_cost,@st_id,@enable_flag) ");
                    #endregion
                    sysSQLString.Param.Add("cost_id", Guid.NewGuid().ToString());
                }
                #region 参数项 6
                sysSQLString.Param.Add("other_cost_no", item.other_cost_no);
                sysSQLString.Param.Add("cost_name", item.cost_name);
                sysSQLString.Param.Add("cost_type", item.cost_type);
                string tally_time = "";
                //提交日期为空处理
                if (!String.IsNullOrEmpty(item.tally_time))
                {
                    tally_time = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(item.tally_time)).ToString();
                }
                sysSQLString.Param.Add("tally_time", tally_time);
                sysSQLString.Param.Add("cost_explain", item.cost_explain);
                sysSQLString.Param.Add("sum_cost", item.sum_cost);
                sysSQLString.Param.Add("st_id", st_id);
                sysSQLString.Param.Add("enable_flag", ((int)DataSources.EnumEnableFlag.USING).ToString());
                #endregion
                sysSQLString.sqlString = strSql.ToString();
                list.Add(sysSQLString);
            }
            #endregion

            bool flag = DBHelper.BatchExeSQLStringMultiByTrans("宇通：同步三包服务单信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, list);
            if (!flag)
            {
                return false;
            }
            
            return true;
        }

        /// <summary> 维修单创建
        /// </summary>
        /// <param name="contactModel">维修单实体</param>
        /// <returns>返回true or false</returns>
        public static bool UpLoadRepairBill(string jsonStr)
        {
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return true;
            }
            //jsonStr = System.IO.File.ReadAllText("tmp\\repailbill.txt", Encoding.UTF8);
            Utility.Log.Log.writeLineToLog("【维修单创建】" + jsonStr, "接口");
            SURepairBill.repairBill repailModel = Newtonsoft.Json.JsonConvert.DeserializeObject<SURepairBill.repairBill>(jsonStr);
            //jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(repailModel);
            if (repailModel.RepairmaterialDetails == null)
            {
                repailModel.RepairmaterialDetails = new SURepairBill.RepairmaterialDetail[0];
            }
            if (repailModel.RepairProjectDetails == null)
            {
                repailModel.RepairProjectDetails = new SURepairBill.RepairProjectDetail[0];
            }
            string requestType = "CREATE";
            ServicePointManager.ServerCertificateValidationCallback = WebServUtil.ValidateServerCertificate;
            SURepairBill.clientInfo clientInfo = new SURepairBill.clientInfo();
            clientInfo.clientID = GlobalStaticObj_YT.ClientID;
            clientInfo.serviceID = "repairBillSU";
            SURepairBill.repairBillSUService serv = new SURepairBill.repairBillSUService();
            string stationCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_YT.SAPCode, GlobalStaticObj_YT.KeySecurity_YT);
            string dateStr = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss"), GlobalStaticObj_YT.KeySecurity_YT);
            requestType = Secret.Encrypt3DES_UTF8(requestType, GlobalStaticObj_YT.KeySecurity_YT);
            clientInfo = WebServUtil.EncModel<SURepairBill.clientInfo>(clientInfo);
            repailModel = WebServUtil.EncModel<SURepairBill.repairBill>(repailModel);
            repailModel.RepairmaterialDetails = WebServUtil.EncList<SURepairBill.RepairmaterialDetail>(repailModel.RepairmaterialDetails);
            repailModel.RepairProjectDetails = WebServUtil.EncList<SURepairBill.RepairProjectDetail>(repailModel.RepairProjectDetails);
            SURepairBill.Result result = serv.repairBillSU(stationCode, dateStr, requestType, repailModel, clientInfo);
            string state = Secret.Decrypt3DES_UTF8(result.state, GlobalStaticObj_YT.KeySecurity_YT);
            if (state == "F")
            {
                string errMsg = Secret.Decrypt3DES_UTF8(result.errorMsg, GlobalStaticObj_YT.KeySecurity_YT);
                Utility.Log.Log.writeLineToLog("【维修单创建】" + errMsg, "接口");
                return false;
            }
            return true;
        }

        /// <summary>维修结算单状态查询
        /// </summary>
        /// <param name="settlement_no">结算单号</param>
        /// <returns></returns>
        public static bool LoadServiceSettleStatus(string settlement_no)
        {
            return LoadServiceSettleStatus(settlement_no, "QUERY");
        }
        /// <summary>
        /// 维修结算单状态查询
        /// </summary>
        /// <param name="settlement_no">结算单号</param>
        /// <param name="requestType">类型，QUERY查询，UPDATE确认</param>
        /// <returns></returns>
        public static bool LoadServiceSettleStatus(string settlement_no, string requestType)
        {
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return true;
            }
            //settlement_no = "80030601";
            Utility.Log.Log.writeLineToLog("【维修结算单状态查询】" + settlement_no, "接口");
            ServicePointManager.ServerCertificateValidationCallback = WebServUtil.ValidateServerCertificate;

            QueryServiceSettleStatus.clientInfo clientInfo = new QueryServiceSettleStatus.clientInfo();
            clientInfo.clientID = GlobalStaticObj_YT.ClientID;
            clientInfo.serviceID = "serviceSettleStatusQuery";
            QueryServiceSettleStatus.serviceSettleStatusQueryService serv = new QueryServiceSettleStatus.serviceSettleStatusQueryService();
            settlement_no = Secret.Encrypt3DES_UTF8(settlement_no, GlobalStaticObj_YT.KeySecurity_YT);//加密字段
            string stationCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_YT.SAPCode, GlobalStaticObj_YT.KeySecurity_YT);
            string dateStr = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss"), GlobalStaticObj_YT.KeySecurity_YT);
            //string requestType = Secret.Encrypt3DES_UTF8("QUERY", GlobalStaticObj_YT.KeySecurity_YT);
            requestType = Secret.Encrypt3DES_UTF8(requestType, GlobalStaticObj_YT.KeySecurity_YT);
            clientInfo = WebServUtil.EncModel<QueryServiceSettleStatus.clientInfo>(clientInfo);
            QueryServiceSettleStatus.Result result = serv.serviceSettleStatusQuery(stationCode, dateStr, requestType, settlement_no, clientInfo);
            string state = Secret.Decrypt3DES_UTF8(result.state, GlobalStaticObj_YT.KeySecurity_YT);
            if (state == "F")
            {
                string errMsg = Secret.Decrypt3DES_UTF8(result.errorMsg, GlobalStaticObj_YT.KeySecurity_YT);
                Utility.Log.Log.writeLineToLog("【维修结算单状态查询】" + errMsg, "接口");
                return false;
            }
            result = WebServUtil.DesModel(result);
            Utility.Log.Log.writeLineToLog("【维修结算单状态查询】返回数据\r\n" + Newtonsoft.Json.JsonConvert.SerializeObject(result), "接口");
            settlement_no = Secret.Decrypt3DES_UTF8(settlement_no, GlobalStaticObj_YT.KeySecurity_YT);//加密字段
            //删除云平台缓存,防止重复调用
            FactoryTemp.DeleteFactory(settlement_no, DataSources.EnumBillType.ServiceSettle, DataSources.EnumOperateObj.State);
            if (string.IsNullOrEmpty(result.info_status_yt))
            {
                return false;
            }
            bool flag = false;
            #region 更新维修结算
            //settlement_no = Secret.Decrypt3DES_UTF8(result.settlement_no, GlobalStaticObj_YT.KeySecurity_YT);
            //string info_status = Secret.Decrypt3DES_UTF8(result.info_status_yt, GlobalStaticObj_YT.KeySecurity_YT);
            try
            {
                List<SysSQLString> list = new List<SysSQLString>();
                SysSQLString sql = new SysSQLString();
                sql.cmdType = CommandType.Text;
                sql.Param = new Dictionary<string, string>();
                //如果是确认，则修改结算的的状态为“已确认”
                if (Secret.Decrypt3DES_UTF8(requestType, GlobalStaticObj_YT.KeySecurity_YT) == "UPDATE")
                {
                    sql.sqlString = "update tb_maintain_three_guaranty_settlement set info_status_yt=@info_status_yt,info_status=@info_status where settlement_no=@settlement_no";
                    sql.Param.Add("info_status", "374DACB2-BF59-76A5-52AA-8D751B6831CD");//状态为“已确认”
                }
                else
                {
                    sql.sqlString = "update tb_maintain_three_guaranty_settlement set info_status_yt=@info_status_yt where settlement_no=@settlement_no";
                }
                string status = WebServUtil.GetLocalDicID("settlement_repair_status_yt", result.info_status_yt);//状态ID
                sql.Param.Add("info_status_yt", status);
                sql.Param.Add("settlement_no", result.settlement_no);
                list.Add(sql);
                if (DBHelper.BatchExeSQLStringMultiByTrans("宇通：宇通三包结算审核", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, list))
                {
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                flag = false;
            }
            #endregion
            return flag;
        }

        /// <summary>
        /// 确认维修结算单
        /// </summary>
        /// <param name="settlement_no">维修单号</param>
        /// <returns></returns>
        public static bool UpLoadServiceSettleStatus(string settlement_no)
        {
            return LoadServiceSettleStatus(settlement_no, "UPDATE");
        }

        /// <summary>旧件回收-创建
        /// </summary>
        /// <param name="create_time_start">周期</param>
        /// <returns></returns>
        public static bool UpPartReturnCreate(string create_time)
        {
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return true;
            }
            Utility.Log.Log.writeLineToLog("【旧件回收-新增】" + create_time, "接口");
            //jsonStr = System.IO.File.ReadAllText("tmp\\repailbill.txt", Encoding.UTF8);
            string create_time_start = string.Empty;
            string create_time_end = string.Empty;
            if (string.IsNullOrEmpty(create_time))
            {
                return false;
            }
            string[] times = create_time.Split(',');
            if (times.Length != 2)
            {
                return false;
            }

            create_time_start = times[0];
            create_time_end = times[1];
            string requestType = "CREATE";
            ServicePointManager.ServerCertificateValidationCallback = WebServUtil.ValidateServerCertificate;
            CreatePartReturn.clientInfo clientInfo = new CreatePartReturn.clientInfo();
            clientInfo.clientID = GlobalStaticObj_YT.ClientID;
            clientInfo.serviceID = "partReturnCreate";
            CreatePartReturn.partReturnCreateService serv = new CreatePartReturn.partReturnCreateService();
            string stationCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_YT.SAPCode, GlobalStaticObj_YT.KeySecurity_YT);
            //string stationCode = Secret.Encrypt3DES_UTF8("0000101882", GlobalStaticObj_YT.KeySecurity_YT);
            string dateStr = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss"), GlobalStaticObj_YT.KeySecurity_YT);
            requestType = Secret.Encrypt3DES_UTF8(requestType, GlobalStaticObj_YT.KeySecurity_YT);
            clientInfo = WebServUtil.EncModel<CreatePartReturn.clientInfo>(clientInfo);
            create_time_start = Secret.Encrypt3DES_UTF8(create_time_start, GlobalStaticObj_YT.KeySecurity_YT);
            create_time_end = Secret.Encrypt3DES_UTF8(create_time_end, GlobalStaticObj_YT.KeySecurity_YT);
            CreatePartReturn.Result result = serv.partReturnCreate(stationCode, dateStr, requestType, create_time_start, create_time_end, clientInfo);
            string state = Secret.Decrypt3DES_UTF8(result.state, GlobalStaticObj_YT.KeySecurity_YT);
            if (state == "F")
            {
                string errMsg = Secret.Decrypt3DES_UTF8(result.errorMsg, GlobalStaticObj_YT.KeySecurity_YT);
                Utility.Log.Log.writeLineToLog("【旧件回收-新增】" + errMsg, "接口");
                return false;
            }
            result = WebServUtil.DesModel(result);
            CreatePartReturn.ResultPartDetail[] details = WebServUtil.DesList(result.PartDetails);
            Utility.Log.Log.writeLineToLog("【旧件回收-新增】返回数据\r\n" + Newtonsoft.Json.JsonConvert.SerializeObject(result), "接口");
            string returnID = Guid.NewGuid().ToString();
            //result = Newtonsoft.Json.JsonConvert.DeserializeObject<CreatePartReturn.Result>("{'state':'','errorMsg':'','info_status_yt':'PCM_FIX_CALLBACK_SUBMIT','old_bill_num':'60024216','com_code':'0000152877','create_time_start':'2014-12-01','create_time_end':'2014-12-31','PartDetails':[{'service_no':'71027988','car_parts_code':'3101-00385','parts_remarks':'','change_num':'2.0','send_num':'','process_mode':'CALLBACK_YUTONG','remarks':''},{'service_no':'71027988','car_parts_code':'9301-02759','parts_remarks':'','change_num':'3.0','send_num':'','process_mode':'CALLBACK_YUTONG','remarks':''}]}");
            //details = result.PartDetails;
            List<SysSQLString> listSql = new List<SysSQLString>();
            #region 旧件返厂单
            YuTongDic ytDic = new YuTongDic();
            SysSQLString sqlOld = new SysSQLString();
            sqlOld.cmdType = CommandType.Text;
            sqlOld.sqlString = @"insert into tb_maintain_oldpart_recycle(return_id,create_time_start,create_time_end,receipt_time,remarks,oldpart_receipts_no,service_station_code,info_status_yt)
                               values( @return_id,@create_time_start,@create_time_end,@receipt_time,@remarks,@oldpart_receipts_no,@service_station_code,@info_status_yt)";
            sqlOld.Param = new Dictionary<string, string>();
            sqlOld.Param.Add("return_id", returnID);//返厂单ID
            //sqlOld.Param.Add("receipts_no", Secret.Decrypt3DES_UTF8(result.old_bill_num, GlobalStaticObj_YT.KeySecurity_YT));//返厂单号
            string createStart = result.create_time_start;
            if (!string.IsNullOrEmpty(createStart))
            {
                sqlOld.Param.Add("create_time_start", Common.LocalDateTimeToUtcLong(Convert.ToDateTime(createStart)).ToString());//创建开始时间
            }
            else
            {
                sqlOld.Param.Add("create_time_start", null);//创建开始时间
            }
            string endStart = result.create_time_end;
            if (!string.IsNullOrEmpty(endStart))
            {
                sqlOld.Param.Add("create_time_end", Common.LocalDateTimeToUtcLong(Convert.ToDateTime(endStart)).ToString());//创建结束时间
            }
            else
            {
                sqlOld.Param.Add("create_time_end", null);//创建结束时间
            }
            sqlOld.Param.Add("receipt_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());//单据日期
            sqlOld.Param.Add("remarks", null);//备注
            sqlOld.Param.Add("oldpart_receipts_no", result.old_bill_num);//旧件回收单号
            sqlOld.Param.Add("service_station_code", result.com_code);//服务站编码
            sqlOld.Param.Add("info_status_yt", ytDic.GetLocalDicID("oldpart_recycle_status", result.info_status_yt));//单据状态
            listSql.Add(sqlOld);
            #endregion

            foreach (CreatePartReturn.ResultPartDetail detail in details)
            {
                SysSQLString sqlDetail = new SysSQLString();
                sqlDetail.cmdType = CommandType.Text;
                sqlDetail.sqlString = @"insert into tb_maintain_oldpart_recycle_material_detail (parts_id,service_no,parts_code,parts_name,change_num,send_num,process_mode,remarks,maintain_id)
            values (@parts_id,@service_no,@parts_code,@parts_name,@change_num,@send_num,@process_mode,@remarks,@maintain_id)";
                sqlDetail.Param = new Dictionary<string, string>();
                sqlDetail.Param.Add("parts_id", Guid.NewGuid().ToString());//返厂旧件ID
                sqlDetail.Param.Add("service_no", detail.service_no);//服务单号
                string partsName = DBHelper.GetSingleValue("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, string.Format("select top 1 parts_name from tb_parts where car_parts_code='{0}'", detail.car_parts_code));
                sqlDetail.Param.Add("parts_code", detail.car_parts_code);//配件编码
                sqlDetail.Param.Add("parts_name", partsName);//配件名称
                sqlDetail.Param.Add("change_num", detail.change_num);//更换数量
                sqlDetail.Param.Add("send_num", detail.send_num);//发送数量
                sqlDetail.Param.Add("process_mode", ytDic.GetLocalDicID("set_mode_yt", detail.process_mode));//处理方式
                sqlDetail.Param.Add("remarks", detail.parts_remarks);//备注
                sqlDetail.Param.Add("maintain_id", returnID);
                listSql.Add(sqlDetail);
            }
            if (DBHelper.BatchExeSQLStringMultiByTrans("宇通：创建旧件返厂单", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, listSql))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>旧件回收--状态查询
        /// </summary>
        /// <param name="old_bill_num">旧件返回单号</param>
        /// <returns></returns>
        public static bool LoadPartRetureStatus(string old_bill_num)
        {
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return true;
            }
            //old_bill_num = "60023858";
            Utility.Log.Log.writeLineToLog("【旧件回收-状态查询】" + old_bill_num, "接口");
            ServicePointManager.ServerCertificateValidationCallback = WebServUtil.ValidateServerCertificate;

            QueryPartRetureStatus.clientInfo clientInfo = new QueryPartRetureStatus.clientInfo();
            clientInfo.clientID = GlobalStaticObj_YT.ClientID;
            clientInfo.serviceID = "partRetureStatusQuery";
            QueryPartRetureStatus.partRetureStatusQueryService serv = new QueryPartRetureStatus.partRetureStatusQueryService();
            old_bill_num = Secret.Encrypt3DES_UTF8(old_bill_num, GlobalStaticObj_YT.KeySecurity_YT);//加密字段
            string stationCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_YT.SAPCode, GlobalStaticObj_YT.KeySecurity_YT);
            string dateStr = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss"), GlobalStaticObj_YT.KeySecurity_YT);
            string requestType = Secret.Encrypt3DES_UTF8("QUERY", GlobalStaticObj_YT.KeySecurity_YT);
            clientInfo = WebServUtil.EncModel<QueryPartRetureStatus.clientInfo>(clientInfo);
            QueryPartRetureStatus.Result result = serv.partRetureStatusQuery(stationCode, dateStr, requestType, old_bill_num, clientInfo);
            string state = Secret.Decrypt3DES_UTF8(result.state, GlobalStaticObj_YT.KeySecurity_YT);
            if (state == "F")
            {
                string errMsg = Secret.Decrypt3DES_UTF8(result.errorMsg, GlobalStaticObj_YT.KeySecurity_YT);
                Utility.Log.Log.writeLineToLog("【旧件回收-状态查询】" + errMsg, "接口");
                return false;
            }
            result = WebServUtil.DesModel(result);
            QueryPartRetureStatus.ResultPartDetail[] details = WebServUtil.DesList(result.PartDetails);
            Utility.Log.Log.writeLineToLog("【旧件回收-状态查询】返回数据\r\n" + Newtonsoft.Json.JsonConvert.SerializeObject(result), "接口");
            old_bill_num = Secret.Decrypt3DES_UTF8(old_bill_num, GlobalStaticObj_YT.KeySecurity_YT);
            //删除云平台缓存,防止重复调用
            FactoryTemp.DeleteFactory(old_bill_num, DataSources.EnumBillType.PartReturn, DataSources.EnumOperateObj.State);
            if (string.IsNullOrEmpty(result.crm_old_bill_num))
            {
                return false;
            }
            bool flag = false;
            #region 更新旧件回收状态
            //old_bill_num = Secret.Decrypt3DES_UTF8(result.old_bill_num, GlobalStaticObj_YT.KeySecurity_YT);            
            try
            {
                List<SysSQLString> listSql = new List<SysSQLString>();
                SysSQLString sqlPart = new SysSQLString();
                sqlPart.cmdType = CommandType.Text;
                sqlPart.sqlString = "update tb_maintain_oldpart_recycle set info_status_yt=@info_status_yt,sum_money=@sum_money where oldpart_receipts_no=@oldpart_receipts_no";
                sqlPart.Param = new Dictionary<string, string>();
                sqlPart.Param.Add("info_status_yt", WebServUtil.GetLocalDicID("oldpart_recycle_status", result.info_status_yt));
                sqlPart.Param.Add("sum_money", result.sum_money);
                sqlPart.Param.Add("oldpart_receipts_no", result.crm_old_bill_num);
                listSql.Add(sqlPart);
                foreach (QueryPartRetureStatus.ResultPartDetail detail in details)
                {
                    SysSQLString sqlDetail = new SysSQLString();
                    sqlDetail.cmdType = CommandType.Text;
                    sqlDetail.sqlString = "update tb_maintain_oldpart_recycle_material_detail set receive_num=@receive_num,receive_explain=@receive_explain where parts_id=@parts_id";
                    sqlDetail.Param = new Dictionary<string, string>();
                    sqlDetail.Param.Add("receive_num", detail.receive_num);
                    sqlDetail.Param.Add("receive_explain", detail.receive_explain);
                    sqlDetail.Param.Add("parts_id", detail.parts_id);
                    listSql.Add(sqlDetail);
                }
                if (DBHelper.BatchExeSQLStringMultiByTrans("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, listSql))
                {
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                flag = false;
            }
            #endregion
            return flag;
        }

        /// <summary>旧件回收--更新
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static bool UpPartRetureUpdate(string jsonStr)
        {
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return true;
            }

            UpdatePartReture.partReturn model = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdatePartReture.partReturn>(jsonStr);//取出tg_id
            model.sap_code = GlobalStaticObj_YT.SAPCode;
            if (model.PartDetails == null)
            {
                model.PartDetails = new UpdatePartReture.partReturnPartDetail[0];
            }
            YuTongDic ytDic = new YuTongDic();
            model.info_status_yt = ytDic.GetYTDicCode("oldpart_recycle_status", model.info_status_yt);
            //替换处理方式编码
            foreach (UpdatePartReture.partReturnPartDetail detail in model.PartDetails)
            {
                detail.process_mode = ytDic.GetYTDicCode("set_mode_yt", detail.process_mode);
            }

            Utility.Log.Log.writeLineToLog("【旧件回收-更新】" + Newtonsoft.Json.JsonConvert.SerializeObject(model), "接口");
            #region 测试数据
            //UpdatePartReture.partReturn model = new UpdatePartReture.partReturn();
            //model.crm_old_bill_num = "60024236";
            //model.info_status_yt = "PCM_FIX_CALLBACK_ENTER";
            //model.create_time_start = "2014-12-01";
            //model.create_time_end = "2014-12-31";
            //model.sap_code = GlobalStaticObj_YT.SAPCode;
            //UpdatePartReture.partReturnPartDetail detail = new UpdatePartReture.partReturnPartDetail();
            //detail.parts_id = "e89db41c-a3f3-4826-ab65-ff6ebf2bc0ff";
            //detail.service_no = "71027151";
            //detail.car_parts_code = "1703-01920";
            //detail.parts_remark = "";
            //detail.change_num = "0.10";
            //detail.send_num = "0.10";
            //detail.process_mode = "CALLBACK_YUTONG";
            //detail.remark = "";
            //model.PartDetails = new UpdatePartReture.partReturnPartDetail[1];
            //model.PartDetails[0] = detail;
            #endregion
            string requestType = "UPDATE";
            ServicePointManager.ServerCertificateValidationCallback = WebServUtil.ValidateServerCertificate;
            UpdatePartReture.clientInfo clientInfo = new UpdatePartReture.clientInfo();
            clientInfo.clientID = GlobalStaticObj_YT.ClientID;
            clientInfo.serviceID = "partRetureUpdate";
            UpdatePartReture.partRetureUpdateService serv = new UpdatePartReture.partRetureUpdateService();
            string stationCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_YT.SAPCode, GlobalStaticObj_YT.KeySecurity_YT);
            string dateStr = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss"), GlobalStaticObj_YT.KeySecurity_YT);
            requestType = Secret.Encrypt3DES_UTF8(requestType, GlobalStaticObj_YT.KeySecurity_YT);
            clientInfo = WebServUtil.EncModel<UpdatePartReture.clientInfo>(clientInfo);
            model = WebServUtil.EncModel<UpdatePartReture.partReturn>(model);
            model.PartDetails = WebServUtil.EncList<UpdatePartReture.partReturnPartDetail>(model.PartDetails);

            UpdatePartReture.Result result = serv.partRetureUpdate(stationCode, dateStr, requestType, model, clientInfo);
            string state = Secret.Decrypt3DES_UTF8(result.state, GlobalStaticObj_YT.KeySecurity_YT);
            if (state == "F")
            {
                string errMsg = Secret.Decrypt3DES_UTF8(result.errorMsg, GlobalStaticObj_YT.KeySecurity_YT);
                Utility.Log.Log.writeLineToLog("【旧件回收-更新】" + errMsg, "接口");
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
