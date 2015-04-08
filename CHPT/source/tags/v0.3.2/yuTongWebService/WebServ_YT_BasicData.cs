using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL;
using SYSModel;
using System.Data;
using HXC_FuncUtility;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Utility.Common;
using Model;
using System.Collections;
using Utility.Net;
using Utility.Security;
using System.IO;
using System.Reflection;

namespace yuTongWebService
{
    /// <summary> 基础数据接口
    /// Create By syn
    /// Create Time 2014-10-11
    /// </summary>
    public class WebServ_YT_BasicData
    {
        public static void TestSecret()
        {
            string cont_phone = WebServUtil.GetEncFieldValue("111");
            string cont_phone1 = WebServUtil.GetDesFieldValue(cont_phone);

        }

        #region 增量同步数据
        /// <summary>故障模式信息同步
        /// </summary>
        /// <returns>返回同步故障模式信息条数，如为-1，同步失败</returns>
        public static string LoadHitchMode()
        {
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return "";
            }
            ServicePointManager.ServerCertificateValidationCallback = WebServUtil.ValidateServerCertificate;
            QueryHitchMode.clientInfo clientInfo = new QueryHitchMode.clientInfo();
            clientInfo.clientID = GlobalStaticObj_YT.ClientID;
            clientInfo.serviceID = "hitchModeQuery";
            QueryHitchMode.hitchModeQueryService serv = new QueryHitchMode.hitchModeQueryService();
            string stationCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_YT.SAPCode, GlobalStaticObj_YT.KeySecurity_YT);
            string dateStr = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss"), GlobalStaticObj_YT.KeySecurity_YT);
            string requestType = Secret.Encrypt3DES_UTF8("QUERY", GlobalStaticObj_YT.KeySecurity_YT);
            clientInfo = WebServUtil.EncModel<QueryHitchMode.clientInfo>(clientInfo);
            DateTime dtStart = GlobalStaticObj_Server.Instance.CurrentDateTime;//开始时间
            string message = string.Empty;//错误消息
            QueryHitchMode.Result result = new QueryHitchMode.Result();
            try
            {
                result = serv.hitchModeQuery(stationCode, dateStr, requestType, clientInfo);
            }
            catch (InvalidOperationException ioe)
            {
                Utility.Log.Log.writeLineToLog(ioe, "接口");
                message = ioe.Message;
                return "故障模式接口调用超时！";
            }
            catch (Exception e)
            {
                Utility.Log.Log.writeLineToLog(e, "接口");
                return "故障模式接口调用出错！";
            }
            string state = Secret.Decrypt3DES_UTF8(result.state, GlobalStaticObj_YT.KeySecurity_YT);
            if (state == "F")
            {
                string errMsg = Secret.Decrypt3DES_UTF8(result.errorMsg, GlobalStaticObj_YT.KeySecurity_YT);
                Utility.Log.Log.writeLineToLog("【故障模式信息同步】" + errMsg, "接口");
                return "故障模式接口调用失败！";
            }
            QueryHitchMode.faultPosition[] faultPositionArr = result.faultPositions;
            QueryHitchMode.faultMode[] faultModeArr = result.faultModes;
            QueryHitchMode.faultRule[] faultRuleArr = result.faultRules;
            //if (faultPositionArr.Length == 0 && faultModeArr.Length == 0 && faultRuleArr.Length == 0)
            //{
            //    return "";
            //}
            faultPositionArr = WebServUtil.DesList<QueryHitchMode.faultPosition>(faultPositionArr);
            faultModeArr = WebServUtil.DesList<QueryHitchMode.faultMode>(faultModeArr);
            faultRuleArr = WebServUtil.DesList<QueryHitchMode.faultRule>(faultRuleArr);
            int updateCount = 0;//更新条数
            bool flag = SaveHitchMode(faultPositionArr, faultModeArr, faultRuleArr, ref updateCount);
            if (!flag)
            {
                return "故障模式更新失败";
            }
            DateTime dtEnd = GlobalStaticObj_Server.Instance.CurrentDateTime;//结束时间
            int totalCount = int.Parse(DBHelper.GetSingleValue("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "select COUNT(1) from tb_fault_class"));
            WebServUtil.WriteInterficeSync(DataSources.EnumInterfaceType.HitchMode, DataSources.EnumExternalSys.YTCRM, totalCount, updateCount, GlobalStaticObj_Server.Instance.CurrentDateTime);
            WebServUtil.WriteInterficeSyncLog(DataSources.EnumInterfaceType.HitchMode, DataSources.EnumExternalSys.YTCRM, DataSources.EnumSyncDirection.DownLoad, "tb_fault_class", dtStart, dtEnd, updateCount, message);
            return "";
        }

        /// <summary> 联系人信息同步
        /// </summary>
        /// <param name="updateTime">最后更新时间</param>
        /// <param name="contType">联系人类别</param>
        /// <returns>返回同步联系人条数，如为-1，同步失败</returns>
        public static string LoadContact(string updateTime, string contType)
        {
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return "";
            }
            ServicePointManager.ServerCertificateValidationCallback = WebServUtil.ValidateServerCertificate;
            QueryContact.clientInfo clientInfo = new QueryContact.clientInfo();
            clientInfo.clientID = GlobalStaticObj_YT.ClientID;
            clientInfo.serviceID = "queryContact";
            QueryContact.portTypeService serv = new QueryContact.portTypeService();
            updateTime = Secret.Encrypt3DES_UTF8(updateTime, GlobalStaticObj_YT.KeySecurity_YT);
            string priovinceCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.ServiceStationProvince, GlobalStaticObj_YT.KeySecurity_YT);
            //string priovinceCode = Secret.Encrypt3DES_UTF8("620000", GlobalStaticObj_YT.KeySecurity_YT);
            string stationCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_YT.SAPCode, GlobalStaticObj_YT.KeySecurity_YT);
            string dateStr = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss"), GlobalStaticObj_YT.KeySecurity_YT);
            string requestType = Secret.Encrypt3DES_UTF8("QUERY", GlobalStaticObj_YT.KeySecurity_YT);
            clientInfo = WebServUtil.EncModel<QueryContact.clientInfo>(clientInfo);
            contType = Secret.Encrypt3DES_UTF8(contType, GlobalStaticObj_YT.KeySecurity_YT);
            DateTime dtStart = GlobalStaticObj_Server.Instance.CurrentDateTime;//开始时间
            string message = string.Empty;//错误信息
            QueryContact.Result result = new QueryContact.Result();
            try
            {
                result = serv.queryContact(updateTime, priovinceCode, contType, stationCode, dateStr, requestType, clientInfo);
            }
            catch (InvalidOperationException ioe)
            {
                message = ioe.Message;
                Utility.Log.Log.writeLineToLog(ioe, "联系人信息同步");
                return "联系人接口调用超时！";
            }
            catch (Exception e)
            {
                message = e.Message;
                Utility.Log.Log.writeLineToLog(e, "联系人信息同步");
                return "联系人接口调用出错";
            }
            string state = Secret.Decrypt3DES_UTF8(result.state, GlobalStaticObj_YT.KeySecurity_YT);
            if (state == "F")
            {
                string errMsg = Secret.Decrypt3DES_UTF8(result.errorMsg, GlobalStaticObj_YT.KeySecurity_YT);
                Utility.Log.Log.writeLineToLog("【联系人信息同步】" + errMsg, "接口");
                return "联系人调用接口失败！";
            }
            QueryContact.contact[] contactArr = result.Details;
            //if (contactArr.Length == 0)
            //{
            //    return "";
            //}
            contactArr = WebServUtil.DesList<QueryContact.contact>(contactArr);
            //bool flag = SaveContact(contactArr);
            bool flag = true;
            int updateCount = 0;
            int totalCount = 0;
            switch (Secret.Decrypt3DES_UTF8(contType, GlobalStaticObj_YT.KeySecurity_YT))
            {
                case "01":
                    flag = SaveContactNotBatch(contactArr, ref updateCount);
                    totalCount = int.Parse(DBHelper.GetSingleValue("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "select COUNT(1) from tb_contacts where data_source='2'"));
                    break;
                case "02":
                    flag = SaveUserNotBatch(contactArr, ref updateCount);
                    totalCount = int.Parse(DBHelper.GetSingleValue("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "select COUNT(1) from sys_user where data_sources='2'"));
                    break;
            }
            if (!flag)
            {
                return "联系人更新失败";
            }
            DateTime dtEnd = GlobalStaticObj_Server.Instance.CurrentDateTime;//结束时间
            WebServUtil.WriteInterficeSync(DataSources.EnumInterfaceType.Contact, DataSources.EnumExternalSys.YTCRM, totalCount, updateCount, GlobalStaticObj_Server.Instance.CurrentDateTime);
            WebServUtil.WriteInterficeSyncLog(DataSources.EnumInterfaceType.Contact, DataSources.EnumExternalSys.YTCRM, DataSources.EnumSyncDirection.DownLoad, "tb_contacts", dtStart, dtEnd, updateCount, message);

            return ""; ;
        }

        /// <summary>
        /// 查询宇通用户
        /// </summary>
        /// <returns></returns>
        public static DataTable SearchContact()
        {
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return null;
            }
            ServicePointManager.ServerCertificateValidationCallback = WebServUtil.ValidateServerCertificate;
            QueryContact.clientInfo clientInfo = new QueryContact.clientInfo();
            clientInfo.clientID = GlobalStaticObj_YT.ClientID;
            clientInfo.serviceID = "queryContact";
            QueryContact.portTypeService serv = new QueryContact.portTypeService();
            string updateTime = Secret.Encrypt3DES_UTF8("1900-01-01", GlobalStaticObj_YT.KeySecurity_YT);//查询所有日期的
            string priovinceCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.ServiceStationProvince, GlobalStaticObj_YT.KeySecurity_YT);
            //string priovinceCode = Secret.Encrypt3DES_UTF8("620000", GlobalStaticObj_YT.KeySecurity_YT);
            string stationCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_YT.SAPCode, GlobalStaticObj_YT.KeySecurity_YT);
            string dateStr = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss"), GlobalStaticObj_YT.KeySecurity_YT);
            string requestType = Secret.Encrypt3DES_UTF8("QUERY", GlobalStaticObj_YT.KeySecurity_YT);
            clientInfo = WebServUtil.EncModel<QueryContact.clientInfo>(clientInfo);
            string contType = Secret.Encrypt3DES_UTF8("03", GlobalStaticObj_YT.KeySecurity_YT);//用户类型
            List<ListItem> listContact = new List<ListItem>();
            DataTable dt = new DataTable();
            dt.Columns.Add("cont_crm_guid");
            dt.Columns.Add("cont_name");
            dt.Columns.Add("cont_phone");
            QueryContact.Result result = new QueryContact.Result();
            string message = string.Empty;//错误消息
            try
            {
                result = serv.queryContact(updateTime, priovinceCode, contType, stationCode, dateStr, requestType, clientInfo);
            }
            catch (InvalidOperationException ioe)
            {
                Utility.Log.Log.writeLineToLog(ioe, "联系人信息同步");
                return null;
            }
            string state = Secret.Decrypt3DES_UTF8(result.state, GlobalStaticObj_YT.KeySecurity_YT);
            if (state == "F")
            {
                string errMsg = Secret.Decrypt3DES_UTF8(result.errorMsg, GlobalStaticObj_YT.KeySecurity_YT);
                Utility.Log.Log.writeLineToLog("【联系人信息同步】" + errMsg, "接口");
                return null;
            }
            QueryContact.contact[] contactArr = WebServUtil.DesList(result.Details);
            if (contactArr.Length == 0)
            {
                return dt;
            }
            foreach (QueryContact.contact cont in contactArr)
            {
                DataRow dr = dt.NewRow();
                dr["cont_crm_guid"] = cont.cont_crm_guid;
                dr["cont_name"] = cont.cont_name;
                dr["cont_phone"] = cont.cont_phone;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        /// <summary> 服务站信息同步
        /// </summary>
        /// <param name="clientID">接入码</param>
        /// <param name="sapCode">服务站代码</param>
        /// <param name="isSave">是否存库</param>
        /// <returns>返回同步服务站信息条数，如为-1，同步失败</returns>
        public static string LoadServiceStation(string clientID, string sapCode, bool isSave)
        {
            ServicePointManager.ServerCertificateValidationCallback = WebServUtil.ValidateServerCertificate;
            QueryServiceStation.clientInfo clientInfo = new QueryServiceStation.clientInfo();
            //clientInfo.clientID = GlobalStaticObj_Server.Instance.ClientID;
            clientInfo.clientID = clientID;
            clientInfo.serviceID = "queryServiceStation";
            QueryServiceStation.portTypeService serv = new QueryServiceStation.portTypeService();
            //string stationCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_YT.SAPCode, GlobalStaticObj_YT.KeySecurity_YT);
            string stationCode = Secret.Encrypt3DES_UTF8(sapCode, GlobalStaticObj_YT.KeySecurity_YT);
            string dateStr = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss"), GlobalStaticObj_YT.KeySecurity_YT);
            string requestType = Secret.Encrypt3DES_UTF8("QUERY", GlobalStaticObj_YT.KeySecurity_YT);
            clientInfo = WebServUtil.EncModel<QueryServiceStation.clientInfo>(clientInfo);
            DateTime dtStart = GlobalStaticObj_Server.Instance.CurrentDateTime;//开始时间
            string message = string.Empty;//错误消息
            QueryServiceStation.Result result = new QueryServiceStation.Result();
            try
            {
                result = serv.queryServiceStation(stationCode, dateStr, requestType, clientInfo);
            }
            catch (InvalidOperationException ioe)
            {
                Utility.Log.Log.writeLineToLog(ioe, "服务站信息同步");
                return "服务站接口调用超时！";
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog(ex, "服务站信息同步");
                return "服务站接口调用出错！";
            }
            string state = Secret.Decrypt3DES_UTF8(result.state, GlobalStaticObj_YT.KeySecurity_YT);
            if (state == "F")
            {
                string errMsg = Secret.Decrypt3DES_UTF8(result.errorMsg, GlobalStaticObj_YT.KeySecurity_YT);
                Utility.Log.Log.writeLineToLog("【服务站信息同步】" + errMsg, "接口");
                return "服务站接口调用失败！";
            }
            QueryServiceStation.station stt = result.station;
            if (string.IsNullOrEmpty(stt.com_code))
            {
                return "";
            }
            if (!isSave)
            {
                //return stt.com_name;
                return "";
            }
            stt = WebServUtil.DesModel<QueryServiceStation.station>(stt);
            string nowTicks = Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString();
            List<SysSQLString> list = new List<SysSQLString>();
            SysSQLString sysSQLString = new SysSQLString();
            sysSQLString.cmdType = CommandType.Text;
            sysSQLString.Param = new Dictionary<string, string>();
            StringBuilder strSql = new StringBuilder();
            bool isServiceStationExist = DBHelper.IsExist("判断服务站信息是否存在", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tb_company", "com_code='" + result.station.com_code + "'");
            if (isServiceStationExist)
            {
                #region 更新语句
                strSql.Append("update tb_company set ");
                strSql.Append(" parent_id= @parent_id , ");
                strSql.Append(" com_code= @com_code , ");
                strSql.Append(" com_name= @com_name , ");
                strSql.Append(" com_short_name= @com_short_name , ");
                strSql.Append(" province= @province , ");
                strSql.Append(" city= @city , ");
                strSql.Append(" county= @county , ");
                strSql.Append(" category= @category , ");
                strSql.Append(" zip_code= @zip_code , ");
                strSql.Append(" com_email= @com_email , ");
                strSql.Append(" com_phone= @com_phone , ");
                strSql.Append(" com_fax= @com_fax , ");
                strSql.Append(" repair_qualification= @repair_qualification , ");
                strSql.Append(" unit_properties= @unit_properties , ");
                strSql.Append(" legal_person= @legal_person , ");
                strSql.Append(" indepen_check= @indepen_check , ");
                strSql.Append(" indepen_legalperson= @indepen_legalperson , ");
                strSql.Append(" financial_indepen= @financial_indepen , ");
                strSql.Append(" tax_qualification= @tax_qualification , ");
                strSql.Append(" tax_num= @tax_num , ");
                strSql.Append(" work_time= @work_time , ");
                strSql.Append(" staff_counts= @staff_counts , ");
                strSql.Append(" ser_staff_counts= @ser_staff_counts , ");
                strSql.Append(" trench_counts= @trench_counts , ");
                strSql.Append(" reception_area= @reception_area , ");
                strSql.Append(" cust_lounge_area= @cust_lounge_area , ");
                strSql.Append(" meeting_room_area= @meeting_room_area , ");
                strSql.Append(" training_room_area= @training_room_area , ");
                strSql.Append(" repair_workshop_area= @repair_workshop_area , ");
                strSql.Append(" big_repaired_area= @big_repaired_area , ");
                strSql.Append(" parts_sales_area= @parts_sales_area , ");
                strSql.Append(" parts_warehouse_area= @parts_warehouse_area , ");
                strSql.Append(" ytparts_warehouse_area= @ytparts_warehouse_area , ");
                strSql.Append(" oldparts_warehouse_area= @oldparts_warehouse_area , ");
                strSql.Append(" repair_instructions= @repair_instructions , ");
                strSql.Append(" enable_flag= @enable_flag , ");
                strSql.Append(" apply_time= @apply_time , ");
                strSql.Append(" approved_time= @approved_time , ");
                strSql.Append(" com_level= @com_level , ");
                strSql.Append(" workhours_price= @workhours_price , ");
                strSql.Append(" winter_subsidy= @winter_subsidy , ");
                strSql.Append(" territory= @territory , ");
                strSql.Append(" street= @street , ");
                strSql.Append(" institution_code= @institution_code , ");
                strSql.Append(" service_phone= @service_phone , ");
                strSql.Append(" star_level= @star_level , ");
                strSql.Append(" sap_code= @sap_code , ");
                strSql.Append(" center_library= @center_library , ");
                strSql.Append(" data_source= @data_source , ");
                strSql.Append(" update_by= @update_by , ");
                strSql.Append(" update_time= @update_time  ");
                strSql.Append(" where com_code=@com_code  ");
                #endregion
            }
            else
            {
                #region 插入语句
                strSql.Append("insert into tb_company(");
                strSql.Append("com_id,parent_id,com_code,com_name,com_short_name,province,city,county,category,zip_code,com_email,com_phone,com_fax,repair_qualification,unit_properties,legal_person,indepen_check,indepen_legalperson,financial_indepen,tax_qualification,tax_num,work_time,staff_counts,ser_staff_counts,trench_counts,reception_area,cust_lounge_area,meeting_room_area,training_room_area,repair_workshop_area,big_repaired_area,parts_sales_area,parts_warehouse_area,ytparts_warehouse_area,oldparts_warehouse_area,repair_instructions,enable_flag,apply_time,approved_time,com_level,workhours_price,winter_subsidy,territory,street,institution_code,service_phone,star_level,sap_code,center_library,data_source,create_by,create_time,update_by,update_time");
                strSql.Append(") values (");
                strSql.Append("@com_id,@parent_id,@com_code,@com_name,@com_short_name,@province,@city,@county,@category,@zip_code,@com_email,@com_phone,@com_fax,@repair_qualification,@unit_properties,@legal_person,@indepen_check,@indepen_legalperson,@financial_indepen,@tax_qualification,@tax_num,@work_time,@staff_counts,@ser_staff_counts,@trench_counts,@reception_area,@cust_lounge_area,@meeting_room_area,@training_room_area,@repair_workshop_area,@big_repaired_area,@parts_sales_area,@parts_warehouse_area,@ytparts_warehouse_area,@oldparts_warehouse_area,@repair_instructions,@enable_flag,@apply_time,@approved_time,@com_level,@workhours_price,@winter_subsidy,@territory,@street,@institution_code,@service_phone,@star_level,@sap_code,@center_library,@data_source,@create_by,@create_time,@update_by,@update_time) ");
                #endregion
                object objServStatId = DBHelper.GetSingleValue("获取服务站id", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tb_signing_info", "sign_id", "", "");
                if (objServStatId == null)
                {
                    return "获取服务站ID失败！";
                }
                sysSQLString.Param.Add("com_id", objServStatId.ToString());
                sysSQLString.Param.Add("create_by", GlobalStaticObj_Server.Instance.UserID);
                sysSQLString.Param.Add("create_time", nowTicks);
            }
            #region  参数项 47
            YuTongDic ytDic = new YuTongDic();
            //sysSQLString.Param.Add("parent_id", result.station.parent);
            sysSQLString.Param.Add("parent_id", "-1");
            sysSQLString.Param.Add("com_code", result.station.com_code);
            sysSQLString.Param.Add("com_name", result.station.com_name);
            sysSQLString.Param.Add("com_short_name", result.station.com_short_name);
            sysSQLString.Param.Add("province", result.station.province);
            sysSQLString.Param.Add("city", result.station.city);
            sysSQLString.Param.Add("county", result.station.county);
            sysSQLString.Param.Add("category", result.station.category);
            sysSQLString.Param.Add("zip_code", result.station.zip_code);
            sysSQLString.Param.Add("com_email", result.station.com_email);
            sysSQLString.Param.Add("com_phone", result.station.com_phone);
            sysSQLString.Param.Add("com_fax", result.station.com_fax);
            sysSQLString.Param.Add("repair_qualification", result.station.repair_qualification);
            sysSQLString.Param.Add("unit_properties", result.station.unit_properties);
            sysSQLString.Param.Add("legal_person", result.station.legal_person);
            sysSQLString.Param.Add("indepen_check", result.station.indepen_check);
            sysSQLString.Param.Add("indepen_legalperson", result.station.indepen_legalperson);
            sysSQLString.Param.Add("financial_indepen", result.station.financial_indepen);
            sysSQLString.Param.Add("tax_qualification", result.station.tax_qualification);
            sysSQLString.Param.Add("tax_num", result.station.tax_num);
            sysSQLString.Param.Add("work_time", result.station.work_time);
            sysSQLString.Param.Add("staff_counts", string.IsNullOrEmpty(result.station.staff_counts) ? null : result.station.staff_counts);
            sysSQLString.Param.Add("ser_staff_counts", string.IsNullOrEmpty(result.station.ser_staff_counts) ? null : result.station.ser_staff_counts);
            sysSQLString.Param.Add("trench_counts", string.IsNullOrEmpty(result.station.trench_counts) ? null : result.station.trench_counts);
            sysSQLString.Param.Add("reception_area", string.IsNullOrEmpty(result.station.reception_area) ? null : result.station.reception_area);
            sysSQLString.Param.Add("cust_lounge_area", string.IsNullOrEmpty(result.station.cust_lounge_area) ? null : result.station.cust_lounge_area);
            sysSQLString.Param.Add("meeting_room_area", string.IsNullOrEmpty(result.station.meeting_room_area) ? null : result.station.meeting_room_area);
            sysSQLString.Param.Add("training_room_area", string.IsNullOrEmpty(result.station.training_room_area) ? null : result.station.training_room_area);
            sysSQLString.Param.Add("repair_workshop_area", string.IsNullOrEmpty(result.station.repair_workshop_area) ? null : result.station.repair_workshop_area);
            sysSQLString.Param.Add("big_repaired_area", string.IsNullOrEmpty(result.station.big_repaired_area) ? null : result.station.big_repaired_area);
            sysSQLString.Param.Add("parts_sales_area", string.IsNullOrEmpty(result.station.parts_sales_area) ? null : result.station.parts_sales_area);
            sysSQLString.Param.Add("parts_warehouse_area", string.IsNullOrEmpty(result.station.parts_warehouse_area) ? null : result.station.parts_warehouse_area);
            sysSQLString.Param.Add("ytparts_warehouse_area", string.IsNullOrEmpty(result.station.ytparts_warehouse_area) ? null : result.station.ytparts_warehouse_area);
            sysSQLString.Param.Add("oldparts_warehouse_area", string.IsNullOrEmpty(result.station.oldparts_warehouse_area) ? null : result.station.oldparts_warehouse_area);
            sysSQLString.Param.Add("repair_instructions", result.station.repair_instructions);
            sysSQLString.Param.Add("enable_flag", ((int)DataSources.EnumEnableFlag.USING).ToString());

            string dtApplyTime = "";
            if (!String.IsNullOrEmpty(result.station.apply_time))
            {
                dtApplyTime = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(result.station.apply_time)).ToString();
            }
            sysSQLString.Param.Add("apply_time", dtApplyTime);

            string dtApprovedTime = "";
            if (!String.IsNullOrEmpty(result.station.apply_time))
            {
                dtApprovedTime = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(result.station.approved_time)).ToString();
            }
            sysSQLString.Param.Add("approved_time", dtApprovedTime);
            sysSQLString.Param.Add("com_level", result.station.com_level);
            sysSQLString.Param.Add("workhours_price", string.IsNullOrEmpty(result.station.workhours_price) ? null : result.station.workhours_price);
            sysSQLString.Param.Add("winter_subsidy", result.station.winter_subsidy);
            sysSQLString.Param.Add("territory", result.station.territory);
            sysSQLString.Param.Add("street", result.station.street);
            sysSQLString.Param.Add("institution_code", result.station.institution_code);
            sysSQLString.Param.Add("service_phone", result.station.service_phone);
            sysSQLString.Param.Add("star_level", result.station.star_level);
            sysSQLString.Param.Add("sap_code", result.station.sap_code);
            if (!string.IsNullOrEmpty(result.station.center_library))
            {
                sysSQLString.Param.Add("center_library", WebServUtil.GetLocalDicID("center_library", result.station.center_library));
            }
            else
            {
                sysSQLString.Param.Add("center_library", WebServUtil.GetLocalDicID("center_library", result.station.sap_code));
            }
            sysSQLString.Param.Add("data_source", ((int)DataSources.EnumDataSources.YUTONG).ToString());
            sysSQLString.Param.Add("update_by", GlobalStaticObj_Server.Instance.UserID);
            sysSQLString.Param.Add("update_time", nowTicks);
            #endregion
            sysSQLString.sqlString = strSql.ToString();
            list.Add(sysSQLString);
            int updateCount = 0;
            bool flag = DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup("宇通：同步服务站信息", GlobalStaticObj_Server.CommAccCode, list);
            if (!flag)
            {
                return "更新服务站信息失败！";
            }
            updateCount = list.Count;
            DateTime dtEnd = GlobalStaticObj_Server.Instance.CurrentDateTime;//结束时间
            int totalCount = int.Parse(DBHelper.GetSingleValue("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "select count(1) from tb_company where data_source='2'"));
            WebServUtil.WriteInterficeSync(DataSources.EnumInterfaceType.ServiceStation, DataSources.EnumExternalSys.YTCRM, totalCount, updateCount, GlobalStaticObj_Server.Instance.CurrentDateTime);
            WebServUtil.WriteInterficeSyncLog(DataSources.EnumInterfaceType.ServiceStation, DataSources.EnumExternalSys.YTCRM, DataSources.EnumSyncDirection.DownLoad, "tb_company", dtStart, dtEnd, updateCount, "");
            return "";
        }

        /// <summary> 车辆信息同步
        /// </summary>
        /// <param name="updateTime">最后更新时间</param>
        /// <returns>返回同步车辆信息条数，如为-1，同步失败</returns>
        public static string LoadBus(string updateTime)
        {
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return "";
            }
            ServicePointManager.ServerCertificateValidationCallback = WebServUtil.ValidateServerCertificate;
            QueryBus.clientInfo clientInfo = new QueryBus.clientInfo();
            clientInfo.clientID = GlobalStaticObj_YT.ClientID;
            clientInfo.serviceID = "busQuery";
            QueryBus.portTypeService serv = new QueryBus.portTypeService();
            updateTime = Secret.Encrypt3DES_UTF8(updateTime, GlobalStaticObj_YT.KeySecurity_YT);
            string stationCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_YT.SAPCode, GlobalStaticObj_YT.KeySecurity_YT);
            string dateStr = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss"), GlobalStaticObj_YT.KeySecurity_YT);
            string requestType = Secret.Encrypt3DES_UTF8("QUERY", GlobalStaticObj_YT.KeySecurity_YT);
            clientInfo = WebServUtil.EncModel<QueryBus.clientInfo>(clientInfo);
            DateTime dtStart = GlobalStaticObj_Server.Instance.CurrentDateTime;
            string message = string.Empty;
            QueryBus.Result result = new QueryBus.Result();
            try
            {
                result = serv.busQuery(updateTime, stationCode, dateStr, requestType, clientInfo);
            }
            catch (InvalidOperationException ioe)
            {
                Utility.Log.Log.writeLineToLog(ioe, "车辆信息同步");
                message = ioe.Message;
                return "车辆信息同步超时！";
            }
            catch (TimeoutException te)
            {
                Utility.Log.Log.writeLineToLog(te, "车辆信息同步");
                message = te.Message;
                return "车辆信息同步出错！";
            }
            string state = Secret.Decrypt3DES_UTF8(result.state, GlobalStaticObj_YT.KeySecurity_YT);
            if (state == "F")
            {
                string errMsg = Secret.Decrypt3DES_UTF8(result.errorMsg, GlobalStaticObj_YT.KeySecurity_YT);
                Utility.Log.Log.writeLineToLog("【车辆信息同步】" + errMsg, "接口");
                return "车辆信息接口调用失败！";
            }
            QueryBus.Detail[] busArr = result.Details;
            //if (busArr.Length == 0)
            //{
            //    return "车辆信息接口没有返回数据";
            //}
            busArr = WebServUtil.DesList<QueryBus.Detail>(busArr);
            //bool flag = SaveBus(busArr);
            int updateCount = 0;
            bool flag = SaveBusNotBatch(busArr, ref updateCount);
            if (!flag)
            {
                return "车辆更新失败";
            }
            DateTime dtEnd = GlobalStaticObj_Server.Instance.CurrentDateTime;//结束时间
            int totalCount = int.Parse(DBHelper.GetSingleValue("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "select count(1) from tb_vehicle where data_source='2'"));
            WebServUtil.WriteInterficeSync(DataSources.EnumInterfaceType.Bus, DataSources.EnumExternalSys.YTCRM, totalCount, updateCount, GlobalStaticObj_Server.Instance.CurrentDateTime);
            WebServUtil.WriteInterficeSyncLog(DataSources.EnumInterfaceType.Bus, DataSources.EnumExternalSys.YTCRM, DataSources.EnumSyncDirection.DownLoad, "tb_vehicle", dtStart, dtEnd, updateCount, message);
            return "";
        }

        /// <summary> 车辆客户信息同步
        /// </summary>
        /// <param name="updateTime">最后更新时间</param>
        /// <returns>返回同步车辆客户信息条数，如为-1，同步失败</returns>
        public static string LoadCustomer(string updateTime)
        {
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return "";
            }
            ServicePointManager.ServerCertificateValidationCallback = WebServUtil.ValidateServerCertificate;
            QueryCustomer.clientInfo clientInfo = new QueryCustomer.clientInfo();
            clientInfo.clientID = GlobalStaticObj_YT.ClientID;
            clientInfo.serviceID = "customerQuery";
            QueryCustomer.customerQueryService serv = new QueryCustomer.customerQueryService();
            updateTime = Secret.Encrypt3DES_UTF8(updateTime, GlobalStaticObj_YT.KeySecurity_YT);
            string priovinceCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.ServiceStationProvince, GlobalStaticObj_YT.KeySecurity_YT);
            //string priovinceCode = Secret.Encrypt3DES_UTF8("620000", GlobalStaticObj_YT.KeySecurity_YT);
            string stationCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_YT.SAPCode, GlobalStaticObj_YT.KeySecurity_YT);
            string dateStr = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss"), GlobalStaticObj_YT.KeySecurity_YT);
            string requestType = Secret.Encrypt3DES_UTF8("QUERY", GlobalStaticObj_YT.KeySecurity_YT);
            clientInfo = WebServUtil.EncModel<QueryCustomer.clientInfo>(clientInfo);
            DateTime dtStart = GlobalStaticObj_Server.Instance.CurrentDateTime;//开始时间
            QueryCustomer.Result result = new QueryCustomer.Result();
            string message = string.Empty;//错误消息
            try
            {
                result = serv.customerQuery(stationCode, dateStr, requestType, priovinceCode, updateTime, clientInfo);
            }
            catch (InvalidOperationException ioe)
            {
                Utility.Log.Log.writeLineToLog(ioe, "车辆客户信息同步");
                message = ioe.Message;
                return "车辆客户信息同步超时！";
            }
            catch (TimeoutException te)
            {
                Utility.Log.Log.writeLineToLog(te, "车辆客户信息同步");
                message = te.Message;
                return "车辆客户信息同步出错！";
            }
            string state = Secret.Decrypt3DES_UTF8(result.state, GlobalStaticObj_YT.KeySecurity_YT);
            if (state == "F")
            {
                string errMsg = Secret.Decrypt3DES_UTF8(result.errorMsg, GlobalStaticObj_YT.KeySecurity_YT);
                Utility.Log.Log.writeLineToLog("【车辆客户信息同步】" + errMsg, "接口");
                return "车辆客户接口调用失败！";
            }
            QueryCustomer.customer[] customerArr = result.Details;
            //if (customerArr.Length == 0)
            //{
            //    return "车辆客户接口没有返回数据";
            //}
            customerArr = WebServUtil.DesList<QueryCustomer.customer>(customerArr);
            //bool flag = SaveCustomer(customerArr);
            int updateCount = 0;
            bool flag = SaveCustomerNotBatch(customerArr, ref updateCount);
            if (!flag)
            {
                return "车辆客户更新失败";
            }
            DateTime dtEnd = GlobalStaticObj_Server.Instance.CurrentDateTime;//结束时间
            int totalCount = int.Parse(DBHelper.GetSingleValue("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "select count(1) from tb_customer where data_source='2'"));
            WebServUtil.WriteInterficeSync(DataSources.EnumInterfaceType.BusCustomer, DataSources.EnumExternalSys.YTCRM, totalCount, updateCount, GlobalStaticObj_Server.Instance.CurrentDateTime);
            WebServUtil.WriteInterficeSyncLog(DataSources.EnumInterfaceType.BusCustomer, DataSources.EnumExternalSys.YTCRM, DataSources.EnumSyncDirection.DownLoad, "tb_customer", dtStart, dtEnd, updateCount, message);
            return "";
        }

        /// <summary> 工时信息同步
        /// </summary>
        /// <param name="updateTime">最后更新时间</param>
        /// <returns>返回同步工时信息条数，如为-1，同步失败</returns>
        public static string LoadRepairProject()
        {

            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return "";
            }
            ServicePointManager.ServerCertificateValidationCallback = WebServUtil.ValidateServerCertificate;
            QueryRepairProject.clientInfo clientInfo = new QueryRepairProject.clientInfo();
            clientInfo.clientID = GlobalStaticObj_YT.ClientID;
            clientInfo.serviceID = "repairProjectQuery";
            QueryRepairProject.repairProjectQueryService serv = new QueryRepairProject.repairProjectQueryService();
            string stationCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_YT.SAPCode, GlobalStaticObj_YT.KeySecurity_YT);
            string dateStr = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss"), GlobalStaticObj_YT.KeySecurity_YT);
            string requestType = Secret.Encrypt3DES_UTF8("QUERY", GlobalStaticObj_YT.KeySecurity_YT);
            clientInfo = WebServUtil.EncModel<QueryRepairProject.clientInfo>(clientInfo);
            DateTime dtStart = GlobalStaticObj_Server.Instance.CurrentDateTime;//开始时间
            QueryRepairProject.Result result = new QueryRepairProject.Result();
            string message = string.Empty;
            try
            {
                result = serv.repairProjectQuery(stationCode, dateStr, requestType, clientInfo);
            }
            catch (InvalidOperationException ioe)
            {
                Utility.Log.Log.writeLineToLog(ioe, "工时信息同步");
                message = ioe.Message;
                return "工时信息同步超时！";
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog(ex, "工时信息同步");
                message = ex.Message;
                return "工时信息同步失败！";
            }
            string state = Secret.Decrypt3DES_UTF8(result.state, GlobalStaticObj_YT.KeySecurity_YT);
            if (state == "F")
            {
                string errMsg = Secret.Decrypt3DES_UTF8(result.errorMsg, GlobalStaticObj_YT.KeySecurity_YT);
                Utility.Log.Log.writeLineToLog("【工时信息同步】" + errMsg, "接口");
                return "工时信息调用失败！";
            }
            QueryRepairProject.project[] projectArr = result.Details;
            //if (projectArr.Length == 0)
            //{
            //    return "工时信息接口没有返回数据";
            //}
            int updateCount = 0;//更新条数
            projectArr = WebServUtil.DesList<QueryRepairProject.project>(projectArr);
            string workhoursPriceSql = string.Format("select workhours_price from tb_company where sap_code='{0}'", GlobalStaticObj_YT.SAPCode);
            string workhoursPrice = DBHelper.GetSingleValue("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, workhoursPriceSql);
            string nowTicks = Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString();
            List<SysSQLString> list = new List<SysSQLString>();
            DataTable dtWorkhours = DBHelper.GetTable("判断车辆工时信息是否存在", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_workhours", "project_num", "", "", "");
            foreach (QueryRepairProject.project item in projectArr)
            {
                SysSQLString sysSQLString = new SysSQLString();
                sysSQLString.cmdType = CommandType.Text;
                sysSQLString.Param = new Dictionary<string, string>();
                StringBuilder strSql = new StringBuilder();
                //bool isContactExist = DBHelper.IsExist("判断车辆工时信息是否存在", "tb_workhours", GlobalStaticObj_Server.DbPrefix+GlobalStaticObj_Server.Instance.MainAccCode, "project_num='" + item.project_code + "'");
                bool isContactExist = false;
                if (dtWorkhours == null || dtWorkhours.Rows.Count == 0)
                {
                    isContactExist = false;
                }
                else
                {
                    DataRow[] drsWorkhours = dtWorkhours.Select("project_num='" + item.project_code + "'");
                    if (drsWorkhours.Count() > 0)
                    {
                        isContactExist = true;
                    }
                }
                if (isContactExist)
                {
                    #region 更新语句
                    strSql.Append("update tb_workhours set ");
                    strSql.Append(" data_source= @data_source , ");
                    strSql.Append(" project_name= @project_name , ");
                    strSql.Append(" project_remark= @project_remark , ");
                    strSql.Append(" whours_change= @whours_change , ");
                    strSql.Append(" whours_type= @whours_type , ");
                    strSql.Append(" whours_num_a= @whours_num_a , ");
                    strSql.Append(" whours_num_b= @whours_num_b , ");
                    strSql.Append(" whours_num_c= @whours_num_c , ");
                    strSql.Append(" quota_price= @quota_price , ");
                    strSql.Append(" status=@status,");
                    strSql.Append(" update_time= @update_time , ");
                    strSql.Append(" update_by= @update_by , ");
                    strSql.Append(" enable_flag= @enable_flag  ");
                    strSql.Append(" where project_num=@project_num  ");
                    #endregion
                }
                else
                {
                    #region 插入语句
                    strSql.Append("insert into tb_workhours(");
                    strSql.Append("whours_id,data_source,project_num,project_name,project_remark,whours_type,whours_change,whours_num_a,whours_num_b,whours_num_c,quota_price,status,create_by,create_time,update_time,update_by,enable_flag");
                    strSql.Append(") values (");
                    strSql.Append("@whours_id,@data_source,@project_num,@project_name,@project_remark,@whours_type,@whours_change,@whours_num_a,@whours_num_b,@whours_num_c,@quota_price,@status,@create_by,@create_time,@update_time,@update_by,@enable_flag) ");

                    #endregion
                    sysSQLString.Param.Add("whours_id", Guid.NewGuid().ToString());
                    sysSQLString.Param.Add("create_by", GlobalStaticObj_Server.Instance.UserID);
                    sysSQLString.Param.Add("create_time", nowTicks);
                }
                #region 参数项 10
                sysSQLString.sqlString = strSql.ToString();
                sysSQLString.Param.Add("project_num", item.project_code);
                sysSQLString.Param.Add("project_name", item.project_name);
                sysSQLString.Param.Add("project_remark", item.project_remark);
                sysSQLString.Param.Add("whours_change", Convert.ToBoolean(item.whours_change) ? "1" : "0");
                sysSQLString.Param.Add("whours_type", Convert.ToBoolean(item.is_quota) ? "2" : "1");
                //sysSQLString.Param.Add("whours_type", item.is_quota);
                sysSQLString.Param.Add("whours_num_a", item.whours_num_a);
                sysSQLString.Param.Add("whours_num_b", item.whours_num_b);
                sysSQLString.Param.Add("whours_num_c", item.whours_num_c);
                if (Convert.ToBoolean(item.is_quota))
                {
                    sysSQLString.Param.Add("quota_price", item.quota_price);
                }
                else
                {
                    sysSQLString.Param.Add("quota_price", workhoursPrice);
                }
                sysSQLString.Param.Add("status", item.status == "0" ? "1" : "0");
                sysSQLString.Param.Add("enable_flag", ((int)DataSources.EnumEnableFlag.USING).ToString());
                sysSQLString.Param.Add("data_source", ((int)DataSources.EnumDataSources.YUTONG).ToString());
                sysSQLString.Param.Add("update_by", GlobalStaticObj_Server.Instance.UserID);
                sysSQLString.Param.Add("update_time", nowTicks);
                #endregion
                sysSQLString.sqlString = strSql.ToString();
                list.Add(sysSQLString);
            }
            if (list.Count > 0)
            {
                bool flag = DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup("宇通：同步工时信息", GlobalStaticObj_Server.Instance.MainAccCode, list);
                if (!flag)
                {
                    return "工时信息更新失败";
                }
            }
            updateCount = list.Count;
            DateTime dtEnd = GlobalStaticObj_Server.Instance.CurrentDateTime;//结束时间
            int totalCount = int.Parse(DBHelper.GetSingleValue("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "select count(1) from tb_workhours where data_source='2'"));
            WebServUtil.WriteInterficeSync(DataSources.EnumInterfaceType.RepairProject, DataSources.EnumExternalSys.YTCRM, totalCount, updateCount, GlobalStaticObj_Server.Instance.CurrentDateTime);
            WebServUtil.WriteInterficeSyncLog(DataSources.EnumInterfaceType.RepairProject, DataSources.EnumExternalSys.YTCRM, DataSources.EnumSyncDirection.DownLoad, "tb_workhours", dtStart, dtEnd, updateCount, "");
            return "";
        }

        /// <summary> 车型信息同步
        /// </summary>
        /// <returns>返回同步车型条数，如为-1，同步失败</returns>
        public static string LoadBusModel()
        {
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return "";
            }
            ServicePointManager.ServerCertificateValidationCallback = WebServUtil.ValidateServerCertificate;
            QueryBusModel.clientInfo clientInfo = new QueryBusModel.clientInfo();
            clientInfo.clientID = GlobalStaticObj_YT.ClientID;
            clientInfo.serviceID = "busModelQuery";
            QueryBusModel.busModelQueryService serv = new QueryBusModel.busModelQueryService();
            string stationCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_YT.SAPCode, GlobalStaticObj_YT.KeySecurity_YT);
            string dateStr = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss"), GlobalStaticObj_YT.KeySecurity_YT);
            string requestType = Secret.Encrypt3DES_UTF8("QUERY", GlobalStaticObj_YT.KeySecurity_YT);
            clientInfo = WebServUtil.EncModel<QueryBusModel.clientInfo>(clientInfo);
            DateTime dtStart = GlobalStaticObj_Server.Instance.CurrentDateTime;//开始时间
            QueryBusModel.Result result = new QueryBusModel.Result();
            string message = string.Empty;
            try
            {
                result = serv.busModelQuery(stationCode, dateStr, requestType, clientInfo);
            }
            catch (InvalidOperationException ioe)
            {
                Utility.Log.Log.writeLineToLog(ioe, "车型信息同步");
                message = ioe.Message;
                return "车型信息同步超时！";
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog(ex, "车型信息同步");
                message = ex.Message;
                return "车型信息同步出错！";
            }
            string state = Secret.Decrypt3DES_UTF8(result.state, GlobalStaticObj_YT.KeySecurity_YT);
            if (state == "F")
            {
                string errMsg = Secret.Decrypt3DES_UTF8(result.errorMsg, GlobalStaticObj_YT.KeySecurity_YT);
                Utility.Log.Log.writeLineToLog("【车型信息同步】" + errMsg, "接口");
                return "车型信息接口调用失败";
            }
            QueryBusModel.busModel[] busModelArr = result.Details;
            //if (busModelArr.Length == 0)
            //{
            //    return "车型信息接口没有返回数据";
            //}
            int updateCount = 0;
            busModelArr = WebServUtil.DesList<QueryBusModel.busModel>(busModelArr);
            long nowTicks = Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime);
            List<SysSQLString> list = new List<SysSQLString>();
            DataTable dtModels = DBHelper.GetTable("判断车型信息是否存在", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_vehicle_models", "vm_id,models_crm_id ", "", "", "");
            YuTongDic ytDic = new YuTongDic();
            DataTable dtTbModels = new DataTable();
            List<DataRow> listTbModels = new List<DataRow>();
            dtTbModels.Columns.Add("vm_id", typeof(string));
            dtTbModels.Columns.Add("models_crm_id", typeof(string));
            dtTbModels.Columns.Add("vm_name", typeof(string));
            dtTbModels.Columns.Add("out_price", typeof(decimal));
            dtTbModels.Columns.Add("out_special_price", typeof(decimal));
            dtTbModels.Columns.Add("data_source", typeof(string));
            dtTbModels.Columns.Add("vm_class", typeof(string));
            dtTbModels.Columns.Add("v_sale_type", typeof(string));
            dtTbModels.Columns.Add("report_price", typeof(decimal));
            dtTbModels.Columns.Add("repair_price", typeof(decimal));
            dtTbModels.Columns.Add("begin_date", typeof(string));
            dtTbModels.Columns.Add("end_date", typeof(string));
            dtTbModels.Columns.Add("status", typeof(string));
            dtTbModels.Columns.Add("enable_flag", typeof(string));
            dtTbModels.Columns.Add("create_by", typeof(string));
            dtTbModels.Columns.Add("create_time", typeof(long));
            //StringBuilder sbDelete = new StringBuilder();//删除ID
            foreach (QueryBusModel.busModel item in busModelArr)
            {
                if (string.IsNullOrEmpty(item.vm_code))
                {
                    continue;
                }
                SysSQLString sysSQLString = new SysSQLString();
                sysSQLString.cmdType = CommandType.Text;
                sysSQLString.Param = new Dictionary<string, string>();
                StringBuilder strSql = new StringBuilder();
                //bool isContactExist = DBHelper.IsExist("判断车型信息是否存在", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_vehicle_models", "vm_code='" + item.vm_code + "'");
                string vm_id = string.Empty;
                DataRow[] drsModels = dtModels.Select("models_crm_id='" + item.vm_code + "'");
                if (drsModels.Count() > 0)
                {
                    vm_id = drsModels[0]["vm_id"].ToString();
                    //sbDelete.AppendFormat("'{0}',", vm_id);
                }
                string vm_type = ytDic.GetLocalDicID("vm_class", item.vm_type);
                if (string.IsNullOrEmpty(vm_id))
                {

                    vm_id = Guid.NewGuid().ToString();
                    DataRow drModel = dtTbModels.NewRow();
                    drModel["vm_id"] = vm_id;
                    drModel["models_crm_id"] = item.vm_code;
                    drModel["vm_name"] = item.remark;
                    if (!string.IsNullOrEmpty(item.out_price))
                    {
                        drModel["out_price"] = Convert.ToDecimal(item.out_price);
                    }
                    if (!string.IsNullOrEmpty(item.out_special_price))
                    {
                        drModel["out_special_price"] = Convert.ToDecimal(item.out_special_price);
                    }
                    drModel["data_source"] = ((int)DataSources.EnumDataSources.YUTONG).ToString();
                    drModel["vm_class"] = vm_type;
                    drModel["v_sale_type"] = item.v_sale_type;
                    if (!string.IsNullOrEmpty(item.report_price))
                    {
                        drModel["report_price"] = Convert.ToDecimal(item.report_price);
                    }
                    if (!string.IsNullOrEmpty(item.repair_price))
                    {
                        drModel["repair_price"] = Convert.ToDecimal(item.repair_price);
                    }
                    drModel["begin_date"] = item.begin_date;
                    drModel["end_date"] = item.end_date;
                    drModel["status"] = item.status == "0" ? "1" : "0";
                    drModel["enable_flag"] = ((int)DataSources.EnumEnableFlag.USING).ToString();
                    drModel["create_by"] = GlobalStaticObj_Server.Instance.UserID;
                    drModel["create_time"] = nowTicks;
                    listTbModels.Add(drModel);
                }
                else
                {
                    strSql.Append(" update tb_vehicle_models set ");
                    //strSql.Append("vm_name=@vm_name,");
                    strSql.AppendFormat("vm_name='{0}',", item.remark.Replace("'", "''"));
                    if (!string.IsNullOrEmpty(item.out_price))
                    {
                        strSql.AppendFormat("out_price={0},", item.out_price);
                    }
                    if (!string.IsNullOrEmpty(item.out_special_price))
                    {
                        strSql.AppendFormat("out_special_price={0},", item.out_special_price);
                    }
                    strSql.AppendFormat("data_source='{0}',", (int)DataSources.EnumDataSources.YUTONG);
                    strSql.AppendFormat("vm_class='{0}',", vm_type);
                    strSql.AppendFormat("v_sale_type='{0}',", item.v_sale_type);
                    if (!string.IsNullOrEmpty(item.report_price))
                    {
                        strSql.AppendFormat("report_price={0},", item.report_price);
                    }
                    if (!string.IsNullOrEmpty(item.repair_price))
                    {
                        strSql.AppendFormat("repair_price={0},", item.repair_price);
                    }
                    strSql.AppendFormat("begin_date='{0}',", item.begin_date);
                    strSql.AppendFormat("end_date='{0}',", item.end_date);
                    strSql.AppendFormat("status='{0}',", item.status == "0" ? "1" : "0");
                    strSql.AppendFormat("update_by='{0}',", GlobalStaticObj_Server.Instance.UserID);
                    strSql.AppendFormat("update_time={0}", nowTicks);
                    strSql.AppendFormat(" where vm_id='{0}'", vm_id);
                    sysSQLString.sqlString = strSql.ToString();
                    //sysSQLString.Param.Add("vm_name", item.remark);
                    list.Add(sysSQLString);
                }

            }
            bool flag = true;
            //if (sbDelete.Length > 0)
            //{
            //    flag = DBHelper.BatchDeleteDataByWhere("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_vehicle_models", " vm_id in (" + sbDelete.ToString().TrimEnd(',') + ")");
            //    if (!flag)
            //    {
            //        return "";
            //    }
            //}
            if (listTbModels.Count > 0)
            {
                flag = DBHelper.SqlBulkByTransNoLogNoBackUp("", GlobalStaticObj_Server.Instance.MainAccCode, "tb_vehicle_models", listTbModels);
                if (!flag)
                {
                    return "车型新增失败";
                }
            }
            updateCount += listTbModels.Count;
            if (list.Count > 0)
            {
                flag = DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup("", GlobalStaticObj_Server.Instance.MainAccCode, list);
                if (!flag)
                {
                    return "车型更新失败";
                }
                updateCount += list.Count;
            }
            DateTime dtEnd = GlobalStaticObj_Server.Instance.CurrentDateTime;//结束时间
            int totalCount = int.Parse(DBHelper.GetSingleValue("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "select count(1) from tb_vehicle_models where data_source='2'"));
            WebServUtil.WriteInterficeSync(DataSources.EnumInterfaceType.RepairProject, DataSources.EnumExternalSys.YTCRM, totalCount, updateCount, GlobalStaticObj_Server.Instance.CurrentDateTime);
            WebServUtil.WriteInterficeSyncLog(DataSources.EnumInterfaceType.RepairProject, DataSources.EnumExternalSys.YTCRM, DataSources.EnumSyncDirection.DownLoad, "tb_vehicle_models", dtStart, dtEnd, updateCount, message);
            return "";
        }

        /// <summary> 配件信息同步
        /// </summary>
        /// <param name="updateTime">最后更新时间</param>
        /// <returns>返回同步配件信息条数，如为-1，同步失败</returns>
        public static string LoadPart(string updateTime)
        {
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return "";
            }
            ServicePointManager.ServerCertificateValidationCallback = WebServUtil.ValidateServerCertificate;
            QueryPart.clientInfo clientInfo = new QueryPart.clientInfo();
            clientInfo.clientID = GlobalStaticObj_YT.ClientID;
            clientInfo.serviceID = "partQuery";
            QueryPart.partQueryService serv = new QueryPart.partQueryService();
            updateTime = Secret.Encrypt3DES_UTF8(updateTime, GlobalStaticObj_YT.KeySecurity_YT);
            string stationCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_YT.SAPCode, GlobalStaticObj_YT.KeySecurity_YT);
            string dateStr = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss"), GlobalStaticObj_YT.KeySecurity_YT);
            string requestType = Secret.Encrypt3DES_UTF8("QUERY", GlobalStaticObj_YT.KeySecurity_YT);
            clientInfo = WebServUtil.EncModel<QueryPart.clientInfo>(clientInfo);
            DateTime dtStart = GlobalStaticObj_Server.Instance.CurrentDateTime;//开始时间
            QueryPart.Result result = new QueryPart.Result();
            string message = string.Empty;//错误消息
            try
            {
                result = serv.partQuery(stationCode, dateStr, requestType, updateTime, clientInfo);
            }
            catch (InvalidOperationException ioe)
            {
                Utility.Log.Log.writeLineToLog(ioe, "配件信息同步");
                message = ioe.Message;
                return "配件信息同步超时！";
            }
            catch (TimeoutException te)
            {
                Utility.Log.Log.writeLineToLog(te, "配件信息同步");
                message = te.Message;
                return "配件信息同步出错！";
            }
            string state = Secret.Decrypt3DES_UTF8(result.state, GlobalStaticObj_YT.KeySecurity_YT);
            if (state == "F")
            {
                string errMsg = Secret.Decrypt3DES_UTF8(result.errorMsg, GlobalStaticObj_YT.KeySecurity_YT);
                Utility.Log.Log.writeLineToLog("【配件信息同步】" + errMsg, "接口");
                return "配件接口调用失败！";
            }
            QueryPart.part[] partArr = result.Details;
            //if (partArr.Length == 0)
            //{
            //    return "配件接口没有返回数据";
            //}
            partArr = WebServUtil.DesList<QueryPart.part>(partArr);
            //bool flag = SavePart(partArr);
            int updateCount = 0;
            bool flag = SavePartNotBatch(partArr, ref updateCount);
            if (!flag)
            {
                return "配件更新失败！";
            }
            DateTime dtEnd = GlobalStaticObj_Server.Instance.CurrentDateTime;//结束时间
            int totalCount = int.Parse(DBHelper.GetSingleValue("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "select count(1) from tb_parts where data_source='2'"));
            WebServUtil.WriteInterficeSync(DataSources.EnumInterfaceType.Part, DataSources.EnumExternalSys.YTCRM, totalCount, updateCount, GlobalStaticObj_Server.Instance.CurrentDateTime);
            WebServUtil.WriteInterficeSyncLog(DataSources.EnumInterfaceType.Part, DataSources.EnumExternalSys.YTCRM, DataSources.EnumSyncDirection.DownLoad, "tb_parts", dtStart, dtEnd, updateCount, message);
            return "";
        }

        /// <summary> 产品改进号信息同步
        /// </summary>
        /// <param name="updateTime">最后更新时间</param>
        /// <returns>返回产品改进号条数，如为-1，同步失败</returns>
        public static string LoadProdImprovement(string updateTime)
        {
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return "";
            }
            ServicePointManager.ServerCertificateValidationCallback = WebServUtil.ValidateServerCertificate;
            QueryProdImprovement.clientInfo clientInfo = new QueryProdImprovement.clientInfo();
            clientInfo.clientID = GlobalStaticObj_YT.ClientID;
            clientInfo.serviceID = "prodImprovementQuery";
            QueryProdImprovement.prodImprovementQueryService serv = new QueryProdImprovement.prodImprovementQueryService();
            updateTime = Secret.Encrypt3DES_UTF8(updateTime, GlobalStaticObj_YT.KeySecurity_YT);
            string stationCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_YT.SAPCode, GlobalStaticObj_YT.KeySecurity_YT);
            string dateStr = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss"), GlobalStaticObj_YT.KeySecurity_YT);
            string requestType = Secret.Encrypt3DES_UTF8("QUERY", GlobalStaticObj_YT.KeySecurity_YT);
            clientInfo = WebServUtil.EncModel<QueryProdImprovement.clientInfo>(clientInfo);
            DateTime dtStart = GlobalStaticObj_Server.Instance.CurrentDateTime;//开始时间
            QueryProdImprovement.Result result = new QueryProdImprovement.Result();
            string message = string.Empty;//错误消息
            try
            {
                result = serv.prodImprovementQuery(stationCode, dateStr, requestType, updateTime, clientInfo);
            }
            catch (InvalidOperationException ioe)
            {
                Utility.Log.Log.writeLineToLog(ioe, "产品改进号信息同步");
                message = ioe.Message;
                return "产品改进号信息同步超时！";
            }
            catch (TimeoutException te)
            {
                Utility.Log.Log.writeLineToLog(te, "产品改进号信息同步");
                message = te.Message;
                return "产品改进号信息同步出错！";
            }
            string state = Secret.Decrypt3DES_UTF8(result.state, GlobalStaticObj_YT.KeySecurity_YT);
            if (state == "F")
            {
                string errMsg = Secret.Decrypt3DES_UTF8(result.errorMsg, GlobalStaticObj_YT.KeySecurity_YT);
                Utility.Log.Log.writeLineToLog("【产品改进号信息同步】" + errMsg, "接口");
                return "产品改进号接口调用失败！";
            }
            QueryProdImprovement.prodImprovement[] ProdArr = result.Details;
            //if (ProdArr.Length == 0)
            //{
            //    return "产品改进号接口没有返回数据";
            //}
            int updateCount = 0;//更新条数
            ProdArr = WebServUtil.DesList<QueryProdImprovement.prodImprovement>(ProdArr);
            string nowTicks = Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString();
            List<SysSQLString> list = new List<SysSQLString>();
            DataTable dtProduct = DBHelper.GetTable("判断产品改进号是否存在", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_product_no", "service_code", "", "", "");
            foreach (QueryProdImprovement.prodImprovement item in ProdArr)
            {
                SysSQLString sysSQLString = new SysSQLString();
                sysSQLString.cmdType = CommandType.Text;
                sysSQLString.Param = new Dictionary<string, string>();
                StringBuilder strSql = new StringBuilder();
                //bool isContactExist = DBHelper.IsExist("判断产品改进号是否存在", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_product_no", "service_code='" + item.service_code + "'");
                bool isContactExist = false;
                DataRow[] drsProcuct = dtProduct.Select("service_code='" + item.service_code + "'");
                if (drsProcuct.Count() > 0)
                {
                    isContactExist = true;
                }
                if (isContactExist)
                {
                    #region 更新语句
                    strSql.Append(" update tb_product_no set ");
                    strSql.Append(" activities = @activities , ");
                    strSql.Append(" service_type = @service_type , ");
                    strSql.Append(" sart_date = @sart_date , ");
                    strSql.Append(" begin_date = @begin_date , ");
                    strSql.Append(" end_date = @end_date , ");
                    strSql.Append(" service_memo = @service_memo , ");
                    strSql.Append(" update_time = @update_time ");
                    strSql.Append(" where service_code=@service_code;  ");
                    #endregion
                }
                else
                {
                    #region 插入语句
                    strSql.Append(" insert into tb_product_no(");
                    strSql.Append("p_no_id,activities,service_type,sart_date,begin_date,end_date,service_memo,service_code,create_by,create_time,update_by,update_time");
                    strSql.Append(") values (");
                    strSql.Append("@p_no_id,@activities,@service_type,@sart_date,@begin_date,@end_date,@service_memo,@service_code,@create_by,@create_time,@update_by,@update_time");
                    strSql.Append(");  ");
                    #endregion
                    sysSQLString.Param.Add("p_no_id", Guid.NewGuid().ToString());
                    sysSQLString.Param.Add("create_by", GlobalStaticObj_Server.Instance.UserID);
                    sysSQLString.Param.Add("create_time", nowTicks);
                }
                #region 参数项 9
                sysSQLString.sqlString = strSql.ToString();
                sysSQLString.Param.Add("activities", item.activities);
                sysSQLString.Param.Add("service_code", item.service_code);
                sysSQLString.Param.Add("service_type", WebServUtil.GetLocalDicID("service_type", item.service_type));
                sysSQLString.Param.Add("sart_date", Common.LocalDateTimeToUtcLong(DateTime.Parse(string.IsNullOrEmpty(item.sart_date) ? DateTime.MinValue.ToString() : item.sart_date.ToString())).ToString());
                sysSQLString.Param.Add("begin_date", Common.LocalDateTimeToUtcLong(DateTime.Parse(item.begin_date)).ToString());
                sysSQLString.Param.Add("end_date", Common.LocalDateTimeToUtcLong(DateTime.Parse(item.end_date)).ToString());
                sysSQLString.Param.Add("service_memo", item.service_memo);
                sysSQLString.Param.Add("update_by", GlobalStaticObj_Server.Instance.UserID);
                sysSQLString.Param.Add("update_time", nowTicks);
                #endregion
                list.Add(sysSQLString);
                item.BusDetails = WebServUtil.DesList(item.BusDetails);
                foreach (QueryProdImprovement.BusDetail item0 in item.BusDetails)
                {
                    SysSQLString sysSQLString0 = new SysSQLString();
                    sysSQLString0.cmdType = CommandType.Text;
                    sysSQLString0.Param = new Dictionary<string, string>();
                    StringBuilder strSql0 = new StringBuilder();
                    bool isContactExist0 = DBHelper.IsExist("判断产品改进号车辆信息是否存在", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_product_no_vehicle", "vehicle_code='" + item0.vehicle_code + "'");
                    if (isContactExist0)
                    {
                        #region 更新语句
                        strSql0.Append(" update tb_product_no_vehicle set ");
                        strSql0.Append(" account_code = @account_code , ");
                        strSql0.Append(" server_station = @server_station , ");
                        strSql0.Append(" update_time = @update_time ");
                        strSql0.Append(" where vehicle_code=@vehicle_code;  ");
                        #endregion
                    }
                    else
                    {
                        #region 插入语句
                        strSql0.Append(" insert into tb_product_no_vehicle(");
                        strSql0.Append("p_no_v_id,account_code,server_station,create_by,create_time,update_by,update_time");
                        strSql0.Append(") values (");
                        strSql0.Append("@p_no_v_id,@account_code,@server_station,@create_by,@create_time,@update_by,@update_time");
                        strSql0.Append("); ");
                        #endregion
                        sysSQLString0.Param.Add("p_no_v_id", Guid.NewGuid().ToString());
                        sysSQLString0.Param.Add("create_by", GlobalStaticObj_Server.Instance.UserID);
                        sysSQLString0.Param.Add("create_time", nowTicks);
                    }
                    #region
                    sysSQLString0.sqlString = strSql0.ToString();
                    sysSQLString0.Param.Add("account_code", item0.account_code);
                    sysSQLString0.Param.Add("server_station", item0.server_station);
                    sysSQLString0.Param.Add("update_by", GlobalStaticObj_Server.Instance.UserID);
                    sysSQLString0.Param.Add("update_time", nowTicks);
                    #endregion
                    list.Add(sysSQLString0);
                }
                item.PartDetails = WebServUtil.DesList(item.PartDetails);
                foreach (QueryProdImprovement.PartDetail item1 in item.PartDetails)
                {
                    SysSQLString sysSQLString1 = new SysSQLString();
                    sysSQLString1.cmdType = CommandType.Text;
                    sysSQLString1.Param = new Dictionary<string, string>();
                    StringBuilder strSql1 = new StringBuilder();
                    bool isContactExist1 = DBHelper.IsExist("判断产品改进号配件信息是否存在", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_product_no_part", "part_code='" + item1.part_code + "'");
                    if (isContactExist1)
                    {
                        #region 更新语句
                        strSql1.Append(" update tb_product_no_part set ");
                        strSql1.Append(" quantity = @quantity , ");
                        strSql1.Append(" uint = @uint , ");
                        strSql1.Append(" update_time = @update_time ");
                        strSql1.Append(" where part_code=@part_code;  ");
                        #endregion
                    }
                    else
                    {
                        #region 插入语句
                        strSql1.Append(" insert into tb_product_no_part(");
                        strSql1.Append("p_no_part_id,part_code,quantity,uint,create_by,create_time,update_by,update_time");
                        strSql1.Append(") values (");
                        strSql1.Append("@p_no_part_id,@part_code,@quantity,@uint,@create_by,@create_time,@update_by,@update_time");
                        strSql1.Append("); ");
                        #endregion
                        sysSQLString1.Param.Add("p_no_part_id", Guid.NewGuid().ToString());
                        sysSQLString1.Param.Add("create_by", GlobalStaticObj_Server.Instance.UserID);
                        sysSQLString1.Param.Add("create_time", nowTicks);
                    }
                    #region
                    sysSQLString1.sqlString = strSql1.ToString();
                    sysSQLString1.Param.Add("part_code", item1.part_code);
                    sysSQLString1.Param.Add("quantity", Convert.ToDecimal(item1.quantity).ToString());
                    sysSQLString1.Param.Add("uint", item1.@uint);
                    sysSQLString1.Param.Add("update_by", GlobalStaticObj_Server.Instance.UserID);
                    sysSQLString1.Param.Add("update_time", nowTicks);
                    #endregion
                    list.Add(sysSQLString1);
                }
            }
            bool flag = true;
            if (list.Count > 0)
            {
                flag = DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup("宇通：产品改进号", GlobalStaticObj_Server.Instance.MainAccCode, list);
            }
            updateCount = list.Count;
            SysConfig sysConfig = new SysConfig();
            sysConfig.UpdateLastTime("ProdImprovement");
            if (!flag)
            {
                return "产品改进号更新失败";
            }
            DateTime dtEnd = GlobalStaticObj_Server.Instance.CurrentDateTime;//结束时间
            int totalCount = int.Parse(DBHelper.GetSingleValue("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "select count(1) from tb_parts where data_source='2'"));
            WebServUtil.WriteInterficeSync(DataSources.EnumInterfaceType.ProdImprovement, DataSources.EnumExternalSys.YTCRM, totalCount, updateCount, GlobalStaticObj_Server.Instance.CurrentDateTime);
            WebServUtil.WriteInterficeSyncLog(DataSources.EnumInterfaceType.ProdImprovement, DataSources.EnumExternalSys.YTCRM, DataSources.EnumSyncDirection.DownLoad, "tb_parts", dtStart, dtEnd, updateCount, message);
            return "";
        }
        #endregion

        #region 上传数据
        /// <summary> 联系人创建更新
        /// </summary>
        /// <param name="contactModel">联系人实体</param>
        /// <returns>返回错误信息，如果不为空，则操作失败</returns>
        public static string UpLoadCcontact(tb_contacts_ex tempContactModel)
        {
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return "";
            }
            //tempContactModel = new tb_contacts_ex();
            //tempContactModel.cont_crm_guid = Guid.NewGuid().ToString();
            //tempContactModel.cont_crm_guid = "64258b13-0b55-e411-b917-005056ad01ea";
            //tempContactModel.cont_name = "张三";
            //tempContactModel.cont_phone = "13525566532";
            //tempContactModel.sex = "1";
            //tempContactModel.cont_post = "100000017";
            //tempContactModel.status = "1";
            //tempContactModel.nation = "100000000";
            //tempContactModel.parent_customer = "KH0000154987";
            //tempContactModel.contact_type = "1";
            //tempContactModel.cont_post_remark = "";
            string requestType = string.IsNullOrEmpty(tempContactModel.cont_crm_guid) ? "CREATE" : "UPDATE";
            string docType = string.Empty;
            SUContact.contact contactServModel = new SUContact.contact();
            contactServModel.crmcont_guid = tempContactModel.cont_crm_guid;
            contactServModel.cont_name = tempContactModel.cont_name;
            contactServModel.sex = tempContactModel.sex;
            contactServModel.cont_post = "";//联系人职务传空
            //转成宇通编码
            contactServModel.nation = WebServUtil.GetYTDicCode("nation", tempContactModel.nation);
            //根据parent_customer去客户表查找客户编码
            contactServModel.parent_customer = tempContactModel.parent_customer;
            contactServModel.cont_phone = tempContactModel.cont_phone;
            contactServModel.cont_post_remark = tempContactModel.cont_post_remark;
            contactServModel.status = tempContactModel.status;
            if (!string.IsNullOrEmpty(tempContactModel.contact_type))
            {
                docType = Convert.ToInt32(tempContactModel.contact_type).ToString("00");
                contactServModel.doc_type = docType;
            }
            else
            {
                contactServModel.doc_type = "";
            }
            Utility.Log.Log.writeLineToLog("【联系人创建更新】\r\n" + Newtonsoft.Json.JsonConvert.SerializeObject(contactServModel), "接口");
            ServicePointManager.ServerCertificateValidationCallback = WebServUtil.ValidateServerCertificate;
            SUContact.clientInfo clientInfo = new SUContact.clientInfo();
            clientInfo.clientID = GlobalStaticObj_YT.ClientID;
            clientInfo.serviceID = "contactSU";
            SUContact.contactSUPortTypeService serv = new SUContact.contactSUPortTypeService();
            string stationCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_YT.SAPCode, GlobalStaticObj_YT.KeySecurity_YT);
            string dateStr = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss"), GlobalStaticObj_YT.KeySecurity_YT);
            requestType = Secret.Encrypt3DES_UTF8(requestType, GlobalStaticObj_YT.KeySecurity_YT);
            clientInfo = WebServUtil.EncModel<SUContact.clientInfo>(clientInfo);
            contactServModel = WebServUtil.EncModel<SUContact.contact>(contactServModel);
            //contactServModel.cont_phone = WebServUtil.GetDesFieldValue(contactServModel.cont_phone);//解密
            DateTime dtStart = GlobalStaticObj_Server.Instance.CurrentDateTime;//开始时间
            SUContact.Result result = new SUContact.Result();
            string message = string.Empty;//错误消息
            try
            {
                result = serv.contactSU(stationCode, dateStr, requestType, contactServModel, clientInfo);
            }
            catch (InvalidOperationException ioe)
            {
                Utility.Log.Log.writeLineToLog(ioe, "联系人");
                message = ioe.Message;
            }
            catch (TimeoutException te)
            {
                Utility.Log.Log.writeLineToLog(te, "联系人");
                message = te.Message;
            }
            string state = Secret.Decrypt3DES_UTF8(result.state, GlobalStaticObj_YT.KeySecurity_YT);
            if (state == "F")
            {
                string errMsg = Secret.Decrypt3DES_UTF8(result.errorMsg, GlobalStaticObj_YT.KeySecurity_YT);
                Utility.Log.Log.writeLineToLog("【联系人创建更新】\r\n" + errMsg, "接口");
                return "宇通接口错误:" + errMsg;
            }
            else
            {
                if (!string.IsNullOrEmpty(tempContactModel.cont_crm_guid))
                {
                    return "";
                }
                string contact_guid = Secret.Decrypt3DES_UTF8(result.contact_guid, GlobalStaticObj_YT.KeySecurity_YT);
                Utility.Log.Log.writeLineToLog("【联系人创建更新】返回\r\n" + contact_guid, "接口");
                List<SysSQLString> listSql = new List<SysSQLString>();
                SysSQLString updateSql = new SysSQLString();
                updateSql.cmdType = CommandType.Text;
                updateSql.Param = new Dictionary<string, string>();
                switch (docType)
                {
                    case "01":
                        updateSql.Param.Add("cont_crm_guid", contact_guid);
                        updateSql.Param.Add("cont_id", tempContactModel.cont_id);
                        updateSql.sqlString = "update tb_contacts set cont_crm_guid=@cont_crm_guid where cont_id=@cont_id";
                        break;
                    case "02":
                        updateSql.Param.Add("cont_crm_guid", contact_guid);
                        updateSql.Param.Add("user_id", tempContactModel.cont_id);
                        updateSql.sqlString = "update sys_user set cont_crm_guid=@cont_crm_guid where user_id=@user_id";
                        break;
                }
                listSql.Add(updateSql);
                bool flag = DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup("更新联系人：宇通cont_crm_guid", GlobalStaticObj_Server.Instance.MainAccCode, listSql);
                DateTime dtEnd = GlobalStaticObj_Server.Instance.CurrentDateTime;//结束时间
                if (flag)
                {
                    WebServUtil.WriteInterficeSyncLog(DataSources.EnumInterfaceType.Contact, DataSources.EnumExternalSys.YTCRM, DataSources.EnumSyncDirection.UpLoad, "tb_contacts", dtStart, dtEnd, 1, message);
                }
                return flag ? "" : "DB错误:更新联系人失败:宇通cont_crm_guid";
            }
        }

        /// <summary> 车辆客户创建更新
        /// </summary>
        /// <param name="contactModel">车辆客户实体</param>
        /// <returns>返回错误信息，如果不为空，则操作失败</returns>
        public static string UpLoadCustomer(tb_customer customerModel)
        {
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return "";
            }
            #region 测试数据
            //customerModel = new tb_customer();
            //customerModel.cust_crm_guid = "f11397b4-d155-e411-b917-005056ad01ea";
            //customerModel.cust_name = "";
            //customerModel.cust_code = "";
            //customerModel.province = "";
            //customerModel.city = "";
            //customerModel.county = "";
            //customerModel.cust_address = "";
            //customerModel.cust_relation = "";
            //customerModel.zip_code = "";
            //customerModel.cust_phone = "";
            //customerModel.cust_fax = "";
            //customerModel.cust_email = "";
            //customerModel.cust_website = "";
            //customerModel.indepen_legalperson = "";
            //customerModel.market_segment = "";
            //customerModel.institution_code = "";
            //customerModel.ent_qualification = "";
            //customerModel.com_constitution = "";
            //customerModel.registered_capital = "";
            //customerModel.business_scope = "";
            //customerModel.credit_rating = "";
            //customerModel.status = "";
            //customerModel.agency = "";
            //customerModel.sap_code = "";
            #endregion

            string requestType = string.IsNullOrEmpty(customerModel.cust_crm_guid) ? "CREATE" : "UPDATE";
            SUCustomer.customer customerServModel = new SUCustomer.customer();
            customerServModel.cust_crm_guid = customerModel.cust_crm_guid;
            customerServModel.cust_name = customerModel.cust_name;
            customerServModel.cust_code = customerModel.cust_code;
            customerServModel.province = customerModel.province;
            customerServModel.city = customerModel.city;
            customerServModel.county = customerModel.county;
            customerServModel.cust_address = customerModel.cust_address;

            customerServModel.cust_relation = "";
            customerServModel.zip_code = customerModel.zip_code;
            customerServModel.cust_phone = customerModel.cust_phone;
            customerServModel.cust_fax = customerModel.cust_fax;
            customerServModel.email = customerModel.cust_email;
            customerServModel.cust_website = customerModel.cust_website;
            customerServModel.indepen_legalperson = customerModel.indepen_legalperson;//独立法人 true false
            customerServModel.market_segment = customerModel.market_segment;
            customerServModel.institution_code = customerModel.institution_code;
            customerServModel.ent_qualification = "";
            customerServModel.com_constitution = "";
            customerServModel.registered_capital = customerModel.registered_capital;
            customerServModel.business_scope = customerModel.business_scope;
            customerServModel.credit_rating = "";
            customerServModel.status = customerModel.status;//可用 0 停用 1
            customerServModel.agency = customerModel.agency;//是否整车经销商 True False
            customerServModel.sap_code = customerModel.sap_code;
            Utility.Log.Log.writeLineToLog("【车辆客户创建更新】\r\n" + Newtonsoft.Json.JsonConvert.SerializeObject(customerServModel), "接口");
            ServicePointManager.ServerCertificateValidationCallback = WebServUtil.ValidateServerCertificate;
            SUCustomer.clientInfo clientInfo = new SUCustomer.clientInfo();
            clientInfo.clientID = GlobalStaticObj_YT.ClientID;
            clientInfo.serviceID = "customerSU";
            SUCustomer.customerSUService serv = new SUCustomer.customerSUService();
            string stationCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_YT.SAPCode, GlobalStaticObj_YT.KeySecurity_YT);
            string dateStr = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss"), GlobalStaticObj_YT.KeySecurity_YT);
            requestType = Secret.Encrypt3DES_UTF8(requestType, GlobalStaticObj_YT.KeySecurity_YT);
            clientInfo = WebServUtil.EncModel<SUCustomer.clientInfo>(clientInfo);
            customerServModel = WebServUtil.EncModel<SUCustomer.customer>(customerServModel);
            DateTime dtStart = GlobalStaticObj_Server.Instance.CurrentDateTime;//开始时间
            SUCustomer.Result result = new SUCustomer.Result();
            string message = string.Empty;//错误消息
            try
            {
                result = serv.customerSU(stationCode, dateStr, requestType, customerServModel, clientInfo);
            }
            catch (InvalidOperationException ioe)
            {
                Utility.Log.Log.writeLineToLog(ioe, "车辆客户");
                message = ioe.Message;
            }
            string state = Secret.Decrypt3DES_UTF8(result.state, GlobalStaticObj_YT.KeySecurity_YT);
            if (state == "F")
            {
                string errMsg = Secret.Decrypt3DES_UTF8(result.errorMsg, GlobalStaticObj_YT.KeySecurity_YT);
                Utility.Log.Log.writeLineToLog("【车辆客户创建更新】\r\n" + errMsg, "接口");
                return "宇通接口错误:" + errMsg;
            }
            else
            {
                Dictionary<string, string> dicFields = new Dictionary<string, string>();
                string cust_crm_guid = Secret.Decrypt3DES_UTF8(result.cust_crm_guid, GlobalStaticObj_YT.KeySecurity_YT);
                Utility.Log.Log.writeLineToLog("【车辆客户创建更新】返回\r\n" + cust_crm_guid, "接口");
                dicFields.Add("cust_crm_guid", cust_crm_guid);
                bool flag = DBHelper.Submit_AddOrEdit("更新车辆客户:宇通cust_crm_guid", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_customer", "cust_id", customerModel.cust_id, dicFields);
                DateTime dtEnd = GlobalStaticObj_Server.Instance.CurrentDateTime;//结束时间
                WebServUtil.WriteInterficeSyncLog(DataSources.EnumInterfaceType.BusCustomer, DataSources.EnumExternalSys.YTCRM, DataSources.EnumSyncDirection.UpLoad, "tb_customer", dtStart, dtEnd, 1, message);
                return flag ? "" : "DB错误:更新车辆客户失败:宇通cust_crm_guid";
            }
        }

        /// <summary>服务站库存信息创建更新
        /// </summary>
        /// <param name="dicServiceStationStock">部件库存键值集合</param>
        /// <returns>返回是否更新成功</returns>
        public static bool UpLoadSercicePartStock(Dictionary<string, int> dicServiceStationStock)
        {
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return true;
            }
            //dicServiceStationStock = new Dictionary<string, int>();
            //dicServiceStationStock.Add("1101-00504", 10);
            SUServicePartStock.partdetail[] partStockArr = new SUServicePartStock.partdetail[dicServiceStationStock.Count];
            int i = 0;
            foreach (string str in dicServiceStationStock.Keys)
            {
                SUServicePartStock.partdetail partDetailModel = new SUServicePartStock.partdetail();
                partDetailModel.car_parts_code = str;
                partDetailModel.parts_counts = dicServiceStationStock[str].ToString();
                partStockArr[i++] = partDetailModel;
            }
            ServicePointManager.ServerCertificateValidationCallback = WebServUtil.ValidateServerCertificate;
            SUServicePartStock.clientInfo clientInfo = new SUServicePartStock.clientInfo();
            clientInfo.clientID = GlobalStaticObj_YT.ClientID;
            clientInfo.serviceID = "sercicePartStockSU";
            SUServicePartStock.sercicePartStockSUService serv = new SUServicePartStock.sercicePartStockSUService();
            string stationCode = Secret.Encrypt3DES_UTF8(GlobalStaticObj_YT.SAPCode, GlobalStaticObj_YT.KeySecurity_YT);
            string dateStr = Secret.Encrypt3DES_UTF8(GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss"), GlobalStaticObj_YT.KeySecurity_YT);
            string requestType = Secret.Encrypt3DES_UTF8("UPDATE", GlobalStaticObj_YT.KeySecurity_YT);
            clientInfo = WebServUtil.EncModel<SUServicePartStock.clientInfo>(clientInfo);
            partStockArr = WebServUtil.EncList<SUServicePartStock.partdetail>(partStockArr);
            SUServicePartStock.Result result = serv.sercicePartStockSU(stationCode, dateStr, requestType, partStockArr, clientInfo);
            string state = Secret.Decrypt3DES_UTF8(result.state, GlobalStaticObj_YT.KeySecurity_YT);
            if (state == "F")
            {
                string errMsg = Secret.Decrypt3DES_UTF8(result.errorMsg, GlobalStaticObj_YT.KeySecurity_YT);
                return false;
            }
            return true;
        }

        #endregion

        #region  初始化接口
        /// <summary> 全量同步联系人信息
        /// </summary>
        /// <returns>返回错误信息</returns>
        public static string InitData_Contact()
        {
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return "";
            }
            string errMsg = "";
            //GlobalStaticObj_Server.Instance.ServiceStationProvince = "330000";
            string serviceType = "Contact";
            string url = "https://isgqas.yutong.com:2222/ISG/downloadFile/downloadFile.do";
            Hashtable ht = new Hashtable();
            ht.Add("method", "downLoadFile");
            ht.Add("provinceCode", GlobalStaticObj_Server.Instance.ServiceStationProvince);
            ht.Add("serviceType", serviceType);
            string tempDir = "tmp";
            string fileName = serviceType + GlobalStaticObj_Server.Instance.ServiceStationProvince;
            string zipFilePath = tempDir + "\\" + fileName + ".zip";
            DateTime dtStart = GlobalStaticObj_Server.Instance.CurrentDateTime;//开始时间
            bool flagDownLoad = HttpRequest.DoGet(url, ht, zipFilePath);
            if (!flagDownLoad)
            {
                return "文件下载失败";
            }
            errMsg = ZipCompress.UnZipFile(zipFilePath, tempDir, GlobalStaticObj_YT.KeySecurity_YT);
            if (!string.IsNullOrEmpty(errMsg))
            {
                return errMsg;
            }
            string jsonFilePath = tempDir + "\\opt\\download\\" + fileName + ".json";
            if (!File.Exists(jsonFilePath))
            {
                return "文件不存在";
            }
            string jsonStr = File.ReadAllText(jsonFilePath, Encoding.UTF8);
            string contactJson = HandleJson(jsonStr, out errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                return errMsg;
            }
            QueryContact.contact[] contactArr = Newtonsoft.Json.JsonConvert.DeserializeObject<QueryContact.contact[]>(contactJson);
            int updateCount = 0;
            bool flagContact = SaveContactNotBatch(contactArr, ref updateCount);
            if (!flagContact)
            {
                return "联系人更新到数据库失败";
            }
            DateTime dtEnd = GlobalStaticObj_Server.Instance.CurrentDateTime;//结束时间
            int totalCount = int.Parse(DBHelper.GetSingleValue("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "select COUNT(1) from tb_contacts where data_source='2'"));
            WebServUtil.WriteInterficeSync(DataSources.EnumInterfaceType.Contact, DataSources.EnumExternalSys.YTCRM, totalCount, updateCount, GlobalStaticObj_Server.Instance.CurrentDateTime);
            WebServUtil.WriteInterficeSyncLog(DataSources.EnumInterfaceType.Contact, DataSources.EnumExternalSys.YTCRM, DataSources.EnumSyncDirection.DownLoad, "tb_contacts", dtStart, dtEnd, updateCount, "");
            return "";
        }

        /// <summary> 全量同步车辆信息
        /// </summary>
        /// <returns>返回错误信息</returns>
        public static string InitData_Bus()
        {
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return "";
            }
            string errMsg = "";
            string serviceType = "BusQuery";
            string url = "https://isgqas.yutong.com:2222/ISG/downloadFile/downloadFile.do";
            Hashtable ht = new Hashtable();
            ht.Add("method", "downLoadFile");
            //ht.Add("provinceCode", GlobalStaticObj_Server.Instance.ServiceStationProvince);
            ht.Add("serviceType", serviceType);
            string tempDir = "tmp";
            string fileName = serviceType;
            string zipFilePath = tempDir + "\\" + fileName + ".zip";
            DateTime dtStart = GlobalStaticObj_Server.Instance.CurrentDateTime;//开始时间
            bool flagDownLoad = HttpRequest.DoGet(url, ht, zipFilePath);
            if (!flagDownLoad)
            {
                return "下载文件失败";
            }
            errMsg = ZipCompress.UnZipFile(zipFilePath, tempDir, GlobalStaticObj_YT.KeySecurity_YT);
            if (!string.IsNullOrEmpty(errMsg))
            {
                return errMsg;
            }
            string jsonFilePath = tempDir + "\\opt\\download\\" + fileName + ".json";
            if (!File.Exists(jsonFilePath))
            {
                return "文件不存在";
            }
            string jsonStr = File.ReadAllText(jsonFilePath, Encoding.UTF8);
            //string busJson = HandleJson(jsonStr, out errMsg);
            //if (!string.IsNullOrEmpty(errMsg))
            //{
            //    return errMsg;
            //}
            //QueryBus.Detail[] busArr = Newtonsoft.Json.JsonConvert.DeserializeObject<QueryBus.Detail[]>(busJson);
            QueryBusModels bus = Newtonsoft.Json.JsonConvert.DeserializeObject<QueryBusModels>(jsonStr);
            if (bus.ReturnStatus != "S")
            {
                return bus.ReturnValue;
            }
            int updateCount = 0;
            bool flagContact = SaveBusNotBatch(bus.busDetails, ref updateCount);
            if (!flagContact)
            {
                return "车辆更新到数据库失败";
            }
            DateTime dtEnd = GlobalStaticObj_Server.Instance.CurrentDateTime;//结束时间
            int totalCount = int.Parse(DBHelper.GetSingleValue("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "select count(1) from tb_vehicle where data_source='2'"));
            WebServUtil.WriteInterficeSync(DataSources.EnumInterfaceType.Bus, DataSources.EnumExternalSys.YTCRM, totalCount, updateCount, GlobalStaticObj_Server.Instance.CurrentDateTime);
            WebServUtil.WriteInterficeSyncLog(DataSources.EnumInterfaceType.Bus, DataSources.EnumExternalSys.YTCRM, DataSources.EnumSyncDirection.DownLoad, "tb_vehicle", dtStart, dtEnd, updateCount, "");
            return "";
        }

        /// <summary> 全量同步车辆客户信息
        /// </summary>
        /// <returns>返回错误信息</returns>
        public static string InitData_Customer()
        {
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return "";
            }
            string errMsg = "";
            string serviceType = "CustomerQuery";
            //GlobalStaticObj_Server.Instance.ServiceStationProvince = "330000";
            string url = "https://isgqas.yutong.com:2222/ISG/downloadFile/downloadFile.do";
            Hashtable ht = new Hashtable();
            ht.Add("method", "downLoadFile");
            ht.Add("provinceCode", GlobalStaticObj_Server.Instance.ServiceStationProvince);
            ht.Add("serviceType", serviceType);
            string tempDir = "tmp";
            string fileName = serviceType + GlobalStaticObj_Server.Instance.ServiceStationProvince;
            string zipFilePath = tempDir + "\\" + fileName + ".zip";
            DateTime dtStart = GlobalStaticObj_Server.Instance.CurrentDateTime;//开始时间
            bool flagDownLoad = HttpRequest.DoGet(url, ht, zipFilePath);
            if (!flagDownLoad)
            {
                return "下载文件失败";
            }
            errMsg = ZipCompress.UnZipFile(zipFilePath, tempDir, GlobalStaticObj_YT.KeySecurity_YT);
            if (!string.IsNullOrEmpty(errMsg))
            {
                return errMsg;
            }
            string jsonFilePath = tempDir + "\\opt\\download\\" + fileName + ".json";
            if (!File.Exists(jsonFilePath))
            {
                return "文件不存在";
            }
            string jsonStr = File.ReadAllText(jsonFilePath, Encoding.UTF8);
            string customerJson = HandleJson(jsonStr, out errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                return errMsg;
            }

            QueryCustomer.customer[] customerArr = Newtonsoft.Json.JsonConvert.DeserializeObject<QueryCustomer.customer[]>(customerJson);
            int updateCount = 0;
            bool flagContact = SaveCustomerNotBatch(customerArr, ref updateCount);
            if (!flagContact)
            {
                return "联系人更新到数据库失败";
            }
            DateTime dtEnd = GlobalStaticObj_Server.Instance.CurrentDateTime;//结束时间
            int totalCount = int.Parse(DBHelper.GetSingleValue("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "select count(1) from tb_customer where data_source='2'"));
            WebServUtil.WriteInterficeSync(DataSources.EnumInterfaceType.BusCustomer, DataSources.EnumExternalSys.YTCRM, totalCount, updateCount, GlobalStaticObj_Server.Instance.CurrentDateTime);
            WebServUtil.WriteInterficeSyncLog(DataSources.EnumInterfaceType.BusCustomer, DataSources.EnumExternalSys.YTCRM, DataSources.EnumSyncDirection.DownLoad, "tb_customer", dtStart, dtEnd, updateCount, "");
            return "";
        }

        /// <summary> 全量同步配件信息
        /// </summary>
        /// <returns>返回错误信息</returns>
        public static string InitData_Part()
        {
            //如果没有接入码或者sap代码,则不调用接口
            if (string.IsNullOrEmpty(GlobalStaticObj_YT.ClientID) ||
                string.IsNullOrEmpty(GlobalStaticObj_YT.SAPCode))
            {
                return "";
            }
            string errMsg = "";
            string serviceType = "Part";
            string url = "https://isgqas.yutong.com:2222/ISG/downloadFile/downloadFile.do";
            Hashtable ht = new Hashtable();
            ht.Add("method", "downLoadFile");
            //ht.Add("provinceCode", GlobalStaticObj_Server.Instance.ServiceStationProvince);
            ht.Add("serviceType", serviceType);
            string tempDir = "tmp";
            string fileName = serviceType;
            string zipFilePath = tempDir + "\\" + fileName + ".zip";
            DateTime dtStart = GlobalStaticObj_Server.Instance.CurrentDateTime;//开始时间
            bool flagDownLoad = HttpRequest.DoGet(url, ht, zipFilePath);
            if (!flagDownLoad)
            {
                return "下载文件失败";
            }
            errMsg = ZipCompress.UnZipFile(zipFilePath, tempDir, GlobalStaticObj_YT.KeySecurity_YT);
            if (!string.IsNullOrEmpty(errMsg))
            {
                return errMsg;
            }
            string jsonFilePath = tempDir + "\\opt\\download\\" + fileName + ".json";
            if (!File.Exists(jsonFilePath))
            {
                return "文件不存在";
            }
            string jsonStr = File.ReadAllText(jsonFilePath, Encoding.UTF8);
            string partJson = HandleJson(jsonStr, out errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                return errMsg;
            }
            QueryPart.part[] partArr = Newtonsoft.Json.JsonConvert.DeserializeObject<QueryPart.part[]>(partJson);
            //partArr = WebServUtil.DesList<QueryPart.part>(partArr);
            int updateCount = 0;
            bool flagContact = SavePartNotBatch(partArr, ref updateCount);
            if (!flagContact)
            {
                return "配件更新到数据库失败";
            }
            DateTime dtEnd = GlobalStaticObj_Server.Instance.CurrentDateTime;//结束时间
            int totalCount = int.Parse(DBHelper.GetSingleValue("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "select count(1) from tb_parts where data_source='2'"));
            WebServUtil.WriteInterficeSync(DataSources.EnumInterfaceType.Part, DataSources.EnumExternalSys.YTCRM, totalCount, updateCount, GlobalStaticObj_Server.Instance.CurrentDateTime);
            WebServUtil.WriteInterficeSyncLog(DataSources.EnumInterfaceType.Part, DataSources.EnumExternalSys.YTCRM, DataSources.EnumSyncDirection.DownLoad, "tb_parts", dtStart, dtEnd, updateCount, "");
            return "";
        }

        /// <summary> 处理json串
        /// </summary>
        /// <param name="jsonStr">json串</param>
        /// <returns>返回数据集合</returns>
        private static string HandleJson(string jsonStr, out string errMsg)
        {
            errMsg = "";
            string headStr = jsonStr.Substring(0, jsonStr.IndexOf(":[{"));
            string status = headStr.Substring(headStr.IndexOf(":") + 2, 1);
            if (status == "S")
            {
                string tempjsonStr = jsonStr.Substring(jsonStr.IndexOf(":[{") + 1);
                //tempjsonStr = tempjsonStr.Substring(0, tempjsonStr.LastIndexOf("}"));
                tempjsonStr = tempjsonStr.Substring(0, tempjsonStr.Length - 3);
                return tempjsonStr;
            }
            else
            {
                int index = headStr.LastIndexOf("\":\"") + 3;
                int len = headStr.LastIndexOf("\",\"") - index;
                errMsg = headStr.Substring(index, len);
                return "";
            }
        }
        #endregion

        #region  数据保存
        /// <summary> 保存故障模式相关
        /// </summary>
        /// <param name="faultPositionArr">故障分类</param>
        /// <param name="faultModeArr">故障模式</param>
        /// <param name="faultRuleArr">部件和故障模式关系</param>
        /// <param name="updateCount">更新条数</param>
        /// <returns>True OR False</returns>
        private static bool SaveHitchMode(QueryHitchMode.faultPosition[] faultPositionArr, QueryHitchMode.faultMode[] faultModeArr, QueryHitchMode.faultRule[] faultRuleArr, ref int updateCount)
        {
            string nowTicks = Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString();
            List<SysSQLString> list = new List<SysSQLString>();
            SysSQLString sysSQLString;
            StringBuilder strSql;
            DataTable dtFaultClass = DBHelper.GetTable("获取故障分类id", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_fault_class", "fault_class_id,system_code", "", "", "");
            //故障分类
            DataTable dtTbFaultClass = new DataTable();
            List<DataRow> listTbFaultClass = new List<DataRow>();
            dtTbFaultClass.Columns.Add("fault_class_id", typeof(string));
            dtTbFaultClass.Columns.Add("system_category", typeof(string));
            dtTbFaultClass.Columns.Add("system_code", typeof(string));
            dtTbFaultClass.Columns.Add("system_name", typeof(string));
            dtTbFaultClass.Columns.Add("system_index_code", typeof(string));
            //故障总成
            DataTable dtTbFaultAssembly = new DataTable();
            List<DataRow> listTbAssembly = new List<DataRow>();
            dtTbFaultAssembly.Columns.Add("fault_assembly_id", typeof(string));
            dtTbFaultAssembly.Columns.Add("assembly_code", typeof(string));
            dtTbFaultAssembly.Columns.Add("assembly_name", typeof(string));
            dtTbFaultAssembly.Columns.Add("assembly_index_code", typeof(string));
            dtTbFaultAssembly.Columns.Add("fault_class_id", typeof(string));
            //故障总成部件
            DataTable dtTbFaultComponent = new DataTable();
            List<DataRow> listTbFaultComponent = new List<DataRow>();
            dtTbFaultComponent.Columns.Add("fault_component_id", typeof(string));
            dtTbFaultComponent.Columns.Add("part_code", typeof(string));
            dtTbFaultComponent.Columns.Add("part_name", typeof(string));
            dtTbFaultComponent.Columns.Add("part_index_code", typeof(string));
            dtTbFaultComponent.Columns.Add("fault_assembly_id", typeof(string));
            DataTable dtFaultAssembly = DBHelper.GetTable("获取故障总成id", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_fault_assembly", "fault_assembly_id,assembly_code", "", "", "");
            DataTable dtFaultComponent = DBHelper.GetTable("判断故障总成部件是否存在", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_fault_component", "part_code", "", "", "");
            foreach (QueryHitchMode.faultPosition item in faultPositionArr)
            {
                //string fault_class_id = DBHelper.GetSingleValue("获取故障分类id", GlobalStaticObj_Server.DbPrefix+GlobalStaticObj_Server.Instance.MainAccCode, "tb_fault_class", "fault_class_id", "system_code='" + item.system_code + "'", "");
                //string fault_assembly_id = DBHelper.GetSingleValue("获取故障总成id", GlobalStaticObj_Server.DbPrefix+GlobalStaticObj_Server.Instance.MainAccCode, "tb_fault_assembly", "fault_assembly_id", "assembly_code='" + item.assembly_code + "'", "");
                string fault_class_id = string.Empty;
                string fault_assembly_id = string.Empty;
                DataRow[] drsClass = dtFaultClass.Select("system_code='" + item.system_code + "'");
                if (drsClass.Count() > 0)
                {
                    fault_class_id = drsClass[0]["fault_class_id"].ToString();
                }
                DataRow[] drsAssembly = dtFaultAssembly.Select("assembly_code='" + item.assembly_code + "'");
                if (drsAssembly.Count() > 0)
                {
                    fault_assembly_id = drsAssembly[0]["fault_assembly_id"].ToString();
                }
                #region 故障分类
                sysSQLString = new SysSQLString();
                sysSQLString.cmdType = CommandType.Text;
                sysSQLString.Param = new Dictionary<string, string>();
                strSql = new StringBuilder();

                if (!string.IsNullOrEmpty(fault_class_id))
                {
                    #region 更新语句
                    strSql.Append("update tb_fault_class set ");
                    strSql.AppendFormat(" system_category= '{0}' , ", item.system_category);
                    strSql.AppendFormat(" system_name= '{0}' , ", item.system_name);
                    strSql.AppendFormat(" system_index_code= '{0}'  ", item.system_index_code);
                    strSql.AppendFormat(" where system_code='{0}'  ", item.system_code);
                    sysSQLString.sqlString = strSql.ToString();
                    //list.Add(sysSQLString);
                    #endregion
                }
                else
                {
                    #region 插入语句
                    fault_class_id = Guid.NewGuid().ToString();
                    DataRow drFaultClass = dtTbFaultClass.NewRow();
                    drFaultClass["fault_class_id"] = fault_class_id;
                    drFaultClass["system_category"] = item.system_category;
                    drFaultClass["system_code"] = item.system_code;
                    drFaultClass["system_name"] = item.system_name;
                    drFaultClass["system_index_code"] = item.system_index_code;
                    listTbFaultClass.Add(drFaultClass);
                    DataRow drClass = dtFaultClass.NewRow();
                    drClass["fault_class_id"] = fault_class_id;
                    drClass["system_code"] = item.system_code;
                    dtFaultClass.Rows.Add(drClass);
                    #endregion

                }
                #endregion

                #region 故障总成
                sysSQLString = new SysSQLString();
                sysSQLString.cmdType = CommandType.Text;
                sysSQLString.Param = new Dictionary<string, string>();
                strSql = new StringBuilder();
                if (!string.IsNullOrEmpty(fault_assembly_id))
                {
                    #region 更新语句
                    strSql.Append("update tb_fault_assembly set ");
                    strSql.AppendFormat(" assembly_name= '{0}' , ", item.assembly_name);
                    strSql.AppendFormat(" assembly_index_code= '{0}' , ", item.assembly_index_code);
                    strSql.AppendFormat(" fault_class_id= '{0}'  ", fault_class_id);
                    strSql.AppendFormat(" where assembly_code='{0}'  ", item.assembly_code);
                    sysSQLString.sqlString = strSql.ToString();
                    //list.Add(sysSQLString);
                    #endregion
                }
                else
                {
                    #region 插入语句
                    fault_assembly_id = Guid.NewGuid().ToString();
                    DataRow drAssembly = dtTbFaultAssembly.NewRow();
                    drAssembly["fault_assembly_id"] = fault_assembly_id;
                    drAssembly["assembly_code"] = item.assembly_code;
                    drAssembly["assembly_name"] = item.assembly_name;
                    drAssembly["assembly_index_code"] = item.assembly_index_code;
                    drAssembly["fault_class_id"] = fault_class_id;
                    listTbAssembly.Add(drAssembly);
                    DataRow drAssemblyCopy = dtFaultAssembly.NewRow();
                    drAssemblyCopy["fault_assembly_id"] = fault_assembly_id;
                    drAssemblyCopy["assembly_code"] = item.assembly_code;
                    dtFaultAssembly.Rows.Add(drAssemblyCopy);
                    #endregion
                }

                #endregion

                #region 故障总成部件
                sysSQLString = new SysSQLString();
                sysSQLString.cmdType = CommandType.Text;
                sysSQLString.Param = new Dictionary<string, string>();
                strSql = new StringBuilder();
                //bool isFaultComponentExist = DBHelper.IsExist("判断故障总成部件是否存在", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_fault_component", "part_code='" + item.part_code + "'");
                bool isFaultComponentExist = false;
                DataRow[] drsComponent = dtFaultComponent.Select("part_code='" + item.part_code + "'");
                if (drsComponent.Count() > 0)
                {
                    isFaultComponentExist = true;
                }
                if (isFaultComponentExist)
                {
                    #region 更新语句
                    strSql.Append("update tb_fault_component set ");
                    strSql.AppendFormat(" part_name= '{0}' , ", item.part_name);
                    strSql.AppendFormat(" part_index_code= '{0}' , ", item.part_index_code);
                    strSql.AppendFormat(" fault_assembly_id= '{0}'  ", fault_assembly_id);
                    strSql.AppendFormat(" where part_code='{0}'  ", item.part_code);
                    sysSQLString.sqlString = strSql.ToString();
                    list.Add(sysSQLString);
                    #endregion
                }
                else
                {
                    #region 插入语句
                    DataRow drModel = dtTbFaultComponent.NewRow();
                    drModel["fault_component_id"] = Guid.NewGuid().ToString();
                    drModel["part_code"] = item.part_code;
                    drModel["part_name"] = item.part_name;
                    drModel["part_index_code"] = item.part_index_code;
                    drModel["fault_assembly_id"] = fault_assembly_id;
                    listTbFaultComponent.Add(drModel);
                    #endregion
                }
                #endregion
            }

            #region 故障模式
            DataTable dtTbFaultModel = new DataTable();
            List<DataRow> listFaultModel = new List<DataRow>();
            dtTbFaultModel.Columns.Add("fault_model_id", typeof(string));
            dtTbFaultModel.Columns.Add("fmea_code", typeof(string));
            dtTbFaultModel.Columns.Add("fmea_name", typeof(string));
            dtTbFaultModel.Columns.Add("fmea_index_code", typeof(string));
            dtTbFaultModel.Columns.Add("remark", typeof(string));
            DataTable dtFaultModel = DBHelper.GetTable("判断故障模式是否存在", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_fault_model", "fmea_code", "", "", "");
            foreach (QueryHitchMode.faultMode item in faultModeArr)
            {
                sysSQLString = new SysSQLString();
                sysSQLString.cmdType = CommandType.Text;
                sysSQLString.Param = new Dictionary<string, string>();
                strSql = new StringBuilder();
                //bool isFaultModeExist = DBHelper.IsExist("判断故障模式是否存在", GlobalStaticObj_Server.DbPrefix+GlobalStaticObj_Server.Instance.MainAccCode, "tb_fault_model", "fmea_code='" + item.fmea_code + "'");
                bool isFaultModeExist = false;
                DataRow[] drsModel = dtFaultModel.Select("fmea_code='" + item.fmea_code + "'");
                if (drsModel.Count() > 0)
                {
                    isFaultModeExist = true;
                }
                if (isFaultModeExist)
                {
                    #region 更新语句
                    strSql.Append("update tb_fault_model set ");
                    strSql.AppendFormat(" fmea_name= '{0}' , ", item.fmea_name);
                    strSql.AppendFormat(" fmea_index_code= '{0}' , ", item.fmea_index_code);
                    strSql.AppendFormat(" remark= '{0}' ", item.category_name);
                    strSql.AppendFormat(" where fmea_code='{0}'  ", item.fmea_code);
                    sysSQLString.sqlString = strSql.ToString();
                    list.Add(sysSQLString);
                    #endregion
                }
                else
                {
                    #region 插入语句
                    DataRow drFaultModel = dtTbFaultModel.NewRow();
                    drFaultModel["fault_model_id"] = Guid.NewGuid().ToString();
                    drFaultModel["fmea_code"] = item.fmea_code;
                    drFaultModel["fmea_name"] = item.fmea_name;
                    drFaultModel["fmea_index_code"] = item.fmea_index_code;
                    drFaultModel["remark"] = item.category_name;
                    listFaultModel.Add(drFaultModel);
                    #endregion
                }

            }
            #endregion

            #region 部件和故障模式关系
            DataTable dtTrComponentModel = new DataTable();
            List<DataRow> listComponentModel = new List<DataRow>();
            dtTrComponentModel.Columns.Add("component_model_id", typeof(string));
            dtTrComponentModel.Columns.Add("rule_part_code", typeof(string));
            dtTrComponentModel.Columns.Add("rule_fmea_code", typeof(string));
            dtTrComponentModel.Columns.Add("limit_condition", typeof(string));
            DataTable dtComponent = DBHelper.GetTable("判断部件和故障模式关系是否存在", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tr_component_model", "rule_part_code,rule_fmea_code", "", "", "");
            foreach (QueryHitchMode.faultRule item in faultRuleArr)
            {
                sysSQLString = new SysSQLString();
                sysSQLString.cmdType = CommandType.Text;
                sysSQLString.Param = new Dictionary<string, string>();
                strSql = new StringBuilder();
                //bool isExist = DBHelper.IsExist("判断部件和故障模式关系是否存在", GlobalStaticObj_Server.DbPrefix+GlobalStaticObj_Server.Instance.MainAccCode, "tr_component_model", "rule_part_code='" + item.rule_part_code + "' and rule_fmea_code='" + item.rule_fmea_code + "'");
                bool isExist = false;
                DataRow[] drsComponent = dtComponent.Select(string.Format("rule_part_code='{0}' and rule_fmea_code='{1}'", item.rule_part_code, item.rule_fmea_code));
                if (drsComponent.Count() > 0)
                {
                    isExist = true;
                }
                if (isExist)
                {
                    #region 更新语句
                    strSql.Append("update tr_component_model set ");
                    strSql.AppendFormat(" limit_condition= '{0}' ", item.limit_condition);
                    strSql.AppendFormat(" where rule_part_code='{0}' and rule_fmea_code= '{1}' ", item.rule_part_code, item.rule_fmea_code);
                    sysSQLString.sqlString = strSql.ToString();
                    list.Add(sysSQLString);
                    #endregion
                }
                else
                {
                    #region 插入语句
                    DataRow drComponentModel = dtTrComponentModel.NewRow();
                    drComponentModel["component_model_id"] = Guid.NewGuid().ToString();
                    drComponentModel["rule_part_code"] = item.rule_part_code;
                    drComponentModel["rule_fmea_code"] = item.rule_fmea_code;
                    drComponentModel["limit_condition"] = item.limit_condition;
                    listComponentModel.Add(drComponentModel);
                    #endregion

                }
            }
            #endregion
            bool flag = true;
            flag = DBHelper.SqlBulkByTransNoLogNoBackUp("", GlobalStaticObj_Server.Instance.MainAccCode, "tb_fault_class", listTbFaultClass);
            if (!flag)
            {
                return flag;
            }
            updateCount += listTbFaultClass.Count;
            flag = DBHelper.SqlBulkByTransNoLogNoBackUp("", GlobalStaticObj_Server.Instance.MainAccCode, "tb_fault_assembly", listTbAssembly);
            if (!flag)
            {
                return flag;
            }
            updateCount += listTbAssembly.Count;
            flag = DBHelper.SqlBulkByTransNoLogNoBackUp("", GlobalStaticObj_Server.Instance.MainAccCode, "tb_fault_component", listTbFaultComponent);
            if (!flag)
            {
                return flag;
            }
            updateCount += listTbFaultComponent.Count;
            flag = DBHelper.SqlBulkByTransNoLogNoBackUp("", GlobalStaticObj_Server.Instance.MainAccCode, "tb_fault_model", listFaultModel);
            if (!flag)
            {
                return flag;
            }
            updateCount += listFaultModel.Count;
            flag = DBHelper.SqlBulkByTransNoLogNoBackUp("", GlobalStaticObj_Server.Instance.MainAccCode, "tr_component_model", listComponentModel);
            if (!flag)
            {
                return flag;
            }
            updateCount += listComponentModel.Count;
            if (list.Count > 0)
            {
                flag = DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup("宇通：同步故障模式信息", GlobalStaticObj_Server.Instance.MainAccCode, list);
                if (!flag)
                {
                    return flag;
                }
                updateCount += list.Count;
            }

            return flag;
        }

        /// <summary> 保存联系人
        /// </summary>
        /// <param name="contactArr">联系人</param>
        /// <returns>True OR False</returns>
        private static bool SaveContact(QueryContact.contact[] contactArr)
        {
            string nowTicks = Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString();
            List<SysSQLString> list = new List<SysSQLString>();
            foreach (QueryContact.contact item in contactArr)
            {
                SysSQLString sysSQLString = new SysSQLString();
                sysSQLString.cmdType = CommandType.Text;
                sysSQLString.Param = new Dictionary<string, string>();
                StringBuilder strSql = new StringBuilder();
                //bool isContactExist = DBHelper.IsExist("判断联系人信息是否存在", "tb_contacts", "cont_crm_guid='" + item.cont_crm_guid + "'");
                bool isContactExist = false;
                if (isContactExist)
                {
                    #region 更新语句
                    strSql.Append("update tb_contacts set ");
                    strSql.Append(" cont_name = @cont_name , ");
                    strSql.Append(" sex = @sex , ");
                    strSql.Append(" nation = @nation , ");
                    strSql.Append(" cont_post = @cont_post , ");
                    strSql.Append(" cont_phone = @cont_phone , ");
                    strSql.Append(" post_remark = @post_remark , ");
                    strSql.Append(" parent_customer = @parent_customer , ");
                    strSql.Append(" status = @status , ");
                    strSql.Append(" enable_flag = @enable_flag , ");
                    strSql.Append(" data_source = @data_source , ");
                    strSql.Append(" update_by = @update_by , ");
                    strSql.Append(" update_time = @update_time  ");
                    strSql.Append(" where cont_crm_guid=@cont_crm_guid  ");
                    #endregion
                }
                else
                {
                    #region 插入语句
                    strSql.Append("insert into tb_contacts(");
                    strSql.Append("cont_id,cont_crm_guid,cont_name,sex,nation,cont_post,cont_phone,post_remark,parent_customer,status,enable_flag,data_source,create_by,create_time,update_by,update_time");
                    strSql.Append(") values (");
                    strSql.Append("@cont_id,@cont_crm_guid,@cont_name,@sex,@nation,@cont_post,@cont_phone,@post_remark,@parent_customer,@status,@enable_flag,@data_source,@create_by,@create_time,@update_by,@update_time");
                    strSql.Append(") ");
                    #endregion
                    sysSQLString.Param.Add("cont_id", Guid.NewGuid().ToString());
                    sysSQLString.Param.Add("create_by", GlobalStaticObj_Server.Instance.UserID);
                    sysSQLString.Param.Add("create_time", nowTicks);
                }
                #region 参数项 9
                sysSQLString.sqlString = strSql.ToString();
                sysSQLString.Param.Add("cont_crm_guid", item.cont_crm_guid);
                sysSQLString.Param.Add("cont_name", item.cont_name);
                sysSQLString.Param.Add("sex", item.sex);
                sysSQLString.Param.Add("nation", WebServUtil.GetLocalDicID("nation", item.nation));
                sysSQLString.Param.Add("cont_post", item.cont_post);
                sysSQLString.Param.Add("cont_phone", WebServUtil.GetEncFieldValue(item.cont_phone));//加密
                sysSQLString.Param.Add("post_remark", item.cont_post_remark);
                sysSQLString.Param.Add("parent_customer", item.parent_customer);
                sysSQLString.Param.Add("status", item.status);
                sysSQLString.Param.Add("enable_flag", ((int)DataSources.EnumEnableFlag.USING).ToString());
                sysSQLString.Param.Add("data_source", ((int)DataSources.EnumDataSources.YUTONG).ToString());
                sysSQLString.Param.Add("update_by", GlobalStaticObj_Server.Instance.UserID);
                sysSQLString.Param.Add("update_time", nowTicks);
                #endregion
                sysSQLString.sqlString = strSql.ToString();
                list.Add(sysSQLString);
            }

            bool flag = DBHelper.BatchExeSQLStringMultiByTrans("宇通：同步联系人", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, list);
            return flag;
        }

        /// <summary> 保存联系人
        /// </summary>
        /// <param name="contactArr">联系人</param>
        /// <param name="updateCount">更新条数</param>
        /// <returns>True OR False</returns>
        private static bool SaveContactNotBatch(QueryContact.contact[] contactArr, ref int updateCount)
        {
            string nowTicks = Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString();
            int contactIndex = 0;//联系人列表索引
            int contactCount = 10000;//每批执行条数
            //contactCount = contactArr.Count();
            int contactSum = contactArr.Count() / contactCount + 1;//执行批数
            //int contactSum = 3;
            bool flag = true;//执行结果
            DateTime startDate = DateTime.Now;
            YuTongDic dic = new YuTongDic();
            YTCustomer ytCustomer = new YTCustomer();
            //联系人
            DataTable dtContacts = DBHelper.GetTable("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_contacts", "cont_id,cont_crm_guid", "", "", "");
            DataTable dtBaseContacts = DBHelper.GetTable("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tr_base_contacts", "cont_id,relation_object_id", "relation_object='tb_customer'", "", "");
            #region 生成表列
            DataTable dt = new DataTable();
            List<DataRow> listTb = new List<DataRow>();
            dt.Columns.Add(new DataColumn("cont_id", typeof(string)));
            dt.Columns.Add(new DataColumn("cont_crm_guid", typeof(string)));
            dt.Columns.Add(new DataColumn("cont_name", typeof(string)));
            dt.Columns.Add(new DataColumn("sex", typeof(string)));
            dt.Columns.Add(new DataColumn("nation", typeof(string)));
            dt.Columns.Add(new DataColumn("cont_post", typeof(string)));
            //dt.Columns.Add(new DataColumn("cont_phone", typeof(string)));
            dt.Columns.Add(new DataColumn("cont_phone_back", typeof(string)));
            dt.Columns.Add(new DataColumn("post_remark", typeof(string)));
            dt.Columns.Add(new DataColumn("parent_customer", typeof(string)));
            dt.Columns.Add(new DataColumn("status", typeof(string)));
            dt.Columns.Add(new DataColumn("enable_flag", typeof(string)));
            dt.Columns.Add(new DataColumn("create_by", typeof(string)));
            dt.Columns.Add(new DataColumn("create_time", typeof(long)));
            dt.Columns.Add(new DataColumn("data_source", typeof(string)));
            dt.Columns.Add("contacts_type", typeof(string));

            DataTable dtTrContacts = new DataTable();
            List<DataRow> listTrContacts = new List<DataRow>();
            dtTrContacts.Columns.Add("id", typeof(string));
            dtTrContacts.Columns.Add("cont_id", typeof(string));
            dtTrContacts.Columns.Add("relation_object", typeof(string));
            dtTrContacts.Columns.Add("relation_object_id", typeof(string));
            #endregion
            //StringBuilder sbMessage=new StringBuilder ();
            for (int i = 0; i <= contactSum; i++)
            {
                dt.Rows.Clear();
                listTb.Clear();
                listTrContacts.Clear();
                List<SysSQLString> list = new List<SysSQLString>();
                for (int y = contactIndex; y < contactCount; y++)
                {
                    int index = i * contactCount + y;
                    if (index >= contactArr.Count())
                    {
                        break;
                    }
                    QueryContact.contact item = contactArr[index];

                    StringBuilder strSql = new StringBuilder();
                    DataRow[] drsCont = dtContacts.Select("cont_crm_guid='" + item.cont_crm_guid + "'");
                    bool isContactExist = false;
                    //bool isContactExist = DBHelper.IsExist("判断联系人信息是否存在", "tb_contacts", "cont_crm_guid='" + item.cont_crm_guid + "'");
                    string nation = dic.GetLocalDicID("nation", item.nation);//民族
                    string cont_post = dic.GetLocalDicID("cont_post", item.cont_post);//职务
                    int cont_type = 0;
                    if (!string.IsNullOrEmpty(item.doc_type))
                    {
                        cont_type = Convert.ToInt32(item.doc_type);
                        DataSources.ContactType contType = (DataSources.ContactType)cont_type;
                        if (contType != DataSources.ContactType.Contact)
                        {
                            continue;
                        }
                    }
                    if (drsCont.Count() > 0)
                    {
                        isContactExist = true;
                    }
                    if (isContactExist)
                    {
                        #region 更新语句
                        SysSQLString sysSQLString = new SysSQLString();
                        sysSQLString.cmdType = CommandType.Text;
                        sysSQLString.Param = new Dictionary<string, string>();
                        strSql.Append("update tb_contacts set ");
                        strSql.AppendFormat(" cont_name = '{0}' , ", item.cont_name);
                        strSql.AppendFormat(" sex = '{0}' , ", item.sex);
                        strSql.AppendFormat(" nation = '{0}' , ", nation);
                        strSql.AppendFormat(" cont_post = '{0}' , ", item.cont_post);
                        //strSql.Append(" cont_phone = @cont_phone , ");
                        strSql.AppendFormat(" cont_phone = {0} , ", WebServUtil.GetEncField(item.cont_phone));
                        strSql.AppendFormat(" post_remark = '{0}' , ", item.cont_post_remark);
                        strSql.AppendFormat(" parent_customer = '{0}' , ", item.parent_customer);
                        strSql.AppendFormat(" contacts_type={0},", cont_type);
                        strSql.AppendFormat(" status = '{0}' , ", item.status == "0" ? "1" : "0");
                        strSql.AppendFormat(" enable_flag = '{0}' , ", (int)DataSources.EnumEnableFlag.USING);
                        strSql.AppendFormat(" data_source = '{0}' , ", (int)DataSources.EnumDataSources.YUTONG);
                        strSql.AppendFormat(" update_by = '{0}' , ", GlobalStaticObj_Server.Instance.UserID);
                        strSql.AppendFormat(" update_time = {0}  ", nowTicks);
                        strSql.AppendFormat(" where cont_crm_guid='{0}'  ", item.cont_crm_guid);
                        sysSQLString.sqlString = strSql.ToString();
                        list.Add(sysSQLString);
                        #endregion
                    }
                    else
                    {
                        #region 插入语句
                        //strSql.Append("insert into tb_contacts(");
                        //strSql.Append("cont_id,cont_crm_guid,cont_name,sex,nation,cont_post,cont_phone,post_remark,parent_customer,status,enable_flag,data_source,create_by,create_time");
                        //strSql.Append(") values (");
                        //strSql.AppendFormat("'{0}',", Guid.NewGuid());
                        //strSql.AppendFormat("'{0}',", item.cont_crm_guid);
                        //strSql.AppendFormat("'{0}',", item.cont_name);
                        //strSql.AppendFormat("'{0}',", item.sex);
                        //strSql.AppendFormat("'{0}',", nation);
                        //strSql.AppendFormat("'{0}',", item.cont_post);
                        //strSql.Append(WebServUtil.GetEncField(item.cont_phone));
                        //strSql.AppendFormat(",'{0}',", item.cont_post_remark);
                        //strSql.AppendFormat("'{0}',", item.parent_customer);
                        //strSql.AppendFormat("'{0}',", item.status);
                        //strSql.AppendFormat("'{0}',", (int)DataSources.EnumEnableFlag.USING);
                        //strSql.AppendFormat("'{0}',", (int)DataSources.EnumDataSources.YUTONG);
                        //strSql.AppendFormat("'{0}',", GlobalStaticObj_Server.Instance.UserID);
                        //strSql.AppendFormat("{0})", nowTicks);
                        //if (item.cont_name.Length > 15)
                        //{
                        //    sbMessage.AppendFormat("[cont_name:{0}]", item.cont_name);
                        //}
                        //if (cont_post!=null && cont_post.Length > 40)
                        //{
                        //    sbMessage.AppendFormat("[cont_post:{0}]", cont_post);
                        //}
                        //if (item.parent_customer.Length > 50)
                        //{
                        //    sbMessage.AppendFormat("[parent_customer:{0}]", item.parent_customer);
                        //}
                        //if (item.cont_post_remark.Length > 300)
                        //{
                        //    sbMessage.AppendFormat("[cont_post_remark:{0}]", item.cont_post_remark);
                        //}
                        DataRow dr = dt.NewRow();
                        string contID = Guid.NewGuid().ToString();
                        dr["cont_id"] = contID;
                        dr["cont_crm_guid"] = item.cont_crm_guid;
                        dr["cont_name"] = item.cont_name;
                        dr["sex"] = item.sex;
                        dr["nation"] = nation;
                        dr["cont_post"] = cont_post;
                        dr["cont_phone_back"] = item.cont_phone;
                        dr["post_remark"] = item.cont_post_remark;
                        dr["parent_customer"] = item.parent_customer;
                        dr["status"] = item.status == "0" ? "1" : "0";
                        dr["contacts_type"] = cont_type;
                        dr["enable_flag"] = ((int)DataSources.EnumEnableFlag.USING).ToString();
                        dr["data_source"] = ((int)DataSources.EnumDataSources.YUTONG).ToString();
                        dr["create_by"] = GlobalStaticObj_Server.Instance.UserID;
                        dr["create_time"] = nowTicks;
                        //dt.Rows.Add(dr);
                        listTb.Add(dr);
                        //关联客户档案
                        string custID = ytCustomer.GetLocalCustID(item.parent_customer);
                        if (!string.IsNullOrEmpty(custID))
                        {
                            DataRow[] drsBase = dtBaseContacts.Select(string.Format("cont_id='{0}' and relation_object_id='{1}'", contID, custID));
                            if (drsBase.Count() == 0)
                            {
                                DataRow drTrContacts = dtTrContacts.NewRow();
                                drTrContacts["id"] = Guid.NewGuid().ToString();
                                drTrContacts["cont_id"] = contID;
                                drTrContacts["relation_object"] = "tb_customer";
                                drTrContacts["relation_object_id"] = custID;
                                listTrContacts.Add(drTrContacts);
                            }
                        }
                        #endregion
                    }
                }

                flag = DBHelper.SqlBulkByTransNoLogNoBackUp("同步联系人", GlobalStaticObj_Server.Instance.MainAccCode, "tb_contacts", listTb);
                if (!flag)
                {
                    break;
                }
                updateCount += listTb.Count;
                flag = DBHelper.SqlBulkByTransNoLogNoBackUp("", GlobalStaticObj_Server.Instance.MainAccCode, "tr_base_contacts", listTrContacts);
                if (!flag)
                {
                    break;
                }
                updateCount += listTrContacts.Count;
                if (list.Count > 0)
                {
                    flag = DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup("", GlobalStaticObj_Server.Instance.MainAccCode, list);
                    if (!flag)
                    {
                        return false;
                    }
                    updateCount += list.Count;
                }
                //flag = true;
                if (!flag)
                {
                    break;
                }
            }
            #region 加密电话
            List<SysSQLString> listUpdatePhone = new List<SysSQLString>();
            SysSQLString sqlPhone = new SysSQLString();
            sqlPhone.cmdType = CommandType.Text;
            sqlPhone.Param = new Dictionary<string, string>();
            string phoneEnc = WebServUtil.GetEncFieldByField("cont_phone_back");
            sqlPhone.sqlString = string.Format("update tb_contacts set cont_phone={0},cont_phone_back=null where cont_phone_back is not null", phoneEnc);
            listUpdatePhone.Add(sqlPhone);
            flag = DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup("", GlobalStaticObj_Server.Instance.MainAccCode, listUpdatePhone);
            #endregion
            SysConfig sysConfig = new SysConfig();
            sysConfig.UpdateLastTime("ContactLastTime");
            DateTime endDate = DateTime.Now;
            TimeSpan span = endDate - startDate;
            return flag;
        }


        /// <summary>保存用户
        /// </summary>
        /// <param name="contactArr">用户</param>
        /// <param name="updateCount">更新条数</param>
        /// <returns>True OR False</returns>
        private static bool SaveUserNotBatch(QueryContact.contact[] contactArr, ref int updateCount)
        {
            string nowTicks = Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString();
            int contactIndex = 0;//联系人列表索引
            int contactCount = 10000;//每批执行条数
            //contactCount = contactArr.Count();
            int contactSum = contactArr.Count() / contactCount + 1;//执行批数
            //int contactSum = 3;
            bool flag = true;//执行结果
            DateTime startDate = DateTime.Now;
            YuTongDic dic = new YuTongDic();
            YTCustomer ytCustomer = new YTCustomer();
            //用户
            DataTable dtContacts = DBHelper.GetTable("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "sys_user", "user_id,cont_crm_guid", "", "", "");
            #region 生成表列
            DataTable dt = new DataTable();
            List<DataRow> listTb = new List<DataRow>();
            dt.Columns.Add(new DataColumn("user_id", typeof(string)));
            dt.Columns.Add(new DataColumn("cont_crm_guid", typeof(string)));
            dt.Columns.Add(new DataColumn("user_name", typeof(string)));
            dt.Columns.Add(new DataColumn("sex", typeof(string)));
            dt.Columns.Add(new DataColumn("nation", typeof(string)));
            dt.Columns.Add(new DataColumn("post", typeof(string)));
            //dt.Columns.Add(new DataColumn("cont_phone", typeof(string)));
            dt.Columns.Add(new DataColumn("user_phone", typeof(string)));
            //dt.Columns.Add(new DataColumn("post_remark", typeof(string)));
            //dt.Columns.Add(new DataColumn("parent_customer", typeof(string)));
            dt.Columns.Add(new DataColumn("status", typeof(string)));
            dt.Columns.Add(new DataColumn("enable_flag", typeof(string)));
            dt.Columns.Add(new DataColumn("create_by", typeof(string)));
            dt.Columns.Add(new DataColumn("create_time", typeof(long)));
            dt.Columns.Add(new DataColumn("data_sources", typeof(string)));
            //dt.Columns.Add("contacts_type", typeof(string));
            #endregion
            for (int i = 0; i <= contactSum; i++)
            {
                dt.Rows.Clear();
                listTb.Clear();
                List<SysSQLString> list = new List<SysSQLString>();
                for (int y = contactIndex; y < contactCount; y++)
                {
                    int index = i * contactCount + y;
                    if (index >= contactArr.Count())
                    {
                        break;
                    }
                    QueryContact.contact item = contactArr[index];

                    StringBuilder strSql = new StringBuilder();
                    DataRow[] drsCont = dtContacts.Select("cont_crm_guid='" + item.cont_crm_guid + "'");
                    bool isContactExist = false;
                    //bool isContactExist = DBHelper.IsExist("判断联系人信息是否存在", "tb_contacts", "cont_crm_guid='" + item.cont_crm_guid + "'");
                    string nation = dic.GetLocalDicID("nation", item.nation);//民族
                    string cont_post = dic.GetLocalDicID("cont_post", item.cont_post);//职务
                    int cont_type = 0;
                    if (!string.IsNullOrEmpty(item.doc_type))
                    {
                        cont_type = Convert.ToInt32(item.doc_type);
                        DataSources.ContactType contType = (DataSources.ContactType)cont_type;
                        if (contType != DataSources.ContactType.Server)
                        {
                            continue;
                        }
                    }
                    if (drsCont.Count() > 0)
                    {
                        isContactExist = true;
                    }
                    if (isContactExist)
                    {
                        #region 更新语句
                        SysSQLString sysSQLString = new SysSQLString();
                        sysSQLString.cmdType = CommandType.Text;
                        sysSQLString.Param = new Dictionary<string, string>();
                        strSql.Append("update sys_user set ");
                        strSql.AppendFormat(" user_name = '{0}' , ", item.cont_name);
                        strSql.AppendFormat(" sex = '{0}' , ", item.sex);
                        strSql.AppendFormat(" nation = '{0}' , ", nation);
                        strSql.AppendFormat(" post = '{0}' , ", item.cont_post);
                        //strSql.Append(" cont_phone = @cont_phone , ");
                        strSql.AppendFormat(" user_phone = {0} , ", item.cont_phone);
                        //strSql.AppendFormat(" post_remark = '{0}' , ", item.cont_post_remark);
                        //strSql.AppendFormat(" parent_customer = '{0}' , ", item.parent_customer);
                        //strSql.AppendFormat(" contacts_type={0},", cont_type);
                        strSql.AppendFormat(" status = '{0}' , ", item.status == "0" ? "1" : "0");
                        strSql.AppendFormat(" enable_flag = '{0}' , ", (int)DataSources.EnumEnableFlag.USING);
                        strSql.AppendFormat(" data_sources = '{0}' , ", (int)DataSources.EnumDataSources.YUTONG);
                        strSql.AppendFormat(" update_by = '{0}' , ", GlobalStaticObj_Server.Instance.UserID);
                        strSql.AppendFormat(" update_time = {0}  ", nowTicks);
                        strSql.AppendFormat(" where cont_crm_guid='{0}'  ", item.cont_crm_guid);
                        sysSQLString.sqlString = strSql.ToString();
                        list.Add(sysSQLString);
                        #endregion
                    }
                    else
                    {
                        #region 插入语句
                        DataRow dr = dt.NewRow();
                        string contID = Guid.NewGuid().ToString();
                        dr["user_id"] = contID;
                        dr["cont_crm_guid"] = item.cont_crm_guid;
                        dr["user_name"] = item.cont_name;
                        dr["sex"] = item.sex;
                        dr["nation"] = nation;
                        dr["post"] = cont_post;
                        dr["user_phone"] = item.cont_phone;
                        //dr["post_remark"] = item.cont_post_remark;
                        //dr["parent_customer"] = item.parent_customer;
                        dr["status"] = item.status == "0" ? "1" : "0";
                        //dr["contacts_type"] = cont_type;
                        dr["enable_flag"] = ((int)DataSources.EnumEnableFlag.USING).ToString();
                        dr["data_sources"] = ((int)DataSources.EnumDataSources.YUTONG).ToString();
                        dr["create_by"] = GlobalStaticObj_Server.Instance.UserID;
                        dr["create_time"] = nowTicks;
                        listTb.Add(dr);
                        #endregion
                    }
                }

                flag = DBHelper.SqlBulkByTransNoLogNoBackUp("同步用户", GlobalStaticObj_Server.Instance.MainAccCode, "sys_user", listTb);
                if (!flag)
                {
                    break;
                }
                updateCount += listTb.Count;
                if (list.Count > 0)
                {
                    flag = DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup("", GlobalStaticObj_Server.Instance.MainAccCode, list);
                    if (!flag)
                    {
                        return flag;
                    }
                    updateCount += list.Count;
                }
                //flag = true;
                if (!flag)
                {
                    break;
                }
            }
            SysConfig sysConfig = new SysConfig();
            sysConfig.UpdateLastTime("UserLastTime");
            DateTime endDate = DateTime.Now;
            TimeSpan span = endDate - startDate;
            return flag;
        }

        /// <summary> 保存车辆信息
        /// </summary>
        /// <param name="busArr">车辆信息</param>
        /// <returns>True OR False</returns>
        private static bool SaveBus(QueryBus.Detail[] busArr)
        {
            string nowTicks = Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString();
            List<SysSQLString> list = new List<SysSQLString>();
            foreach (QueryBus.Detail item in busArr)
            {
                string v_id = DBHelper.GetSingleValue("获取车辆信息车辆ID", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_vehicle", "v_id", "turner='" + item.turner + "'", "");
                SysSQLString sysSQLString = new SysSQLString();
                sysSQLString.cmdType = CommandType.Text;
                sysSQLString.Param = new Dictionary<string, string>();
                StringBuilder strSql = new StringBuilder();
                if (!string.IsNullOrEmpty(v_id))
                {
                    #region 更新语句
                    strSql.Append("update tb_vehicle set ");
                    strSql.Append(" data_source= @data_source , ");
                    strSql.Append(" license_plate= @license_plate , ");
                    strSql.Append(" v_brand= @v_brand , ");
                    strSql.Append(" vin= @vin , ");
                    strSql.Append(" v_model= @v_model , ");
                    strSql.Append(" carbuy_date= @carbuy_date , ");
                    strSql.Append(" engine_num= @engine_num , ");
                    strSql.Append(" turner= @turner , ");
                    strSql.Append(" customer_unit= @customer_unit , ");
                    strSql.Append(" use_unit= @use_unit , ");
                    strSql.Append(" vehicle_use= @vehicle_use , ");
                    strSql.Append(" operating_line= @operating_line , ");
                    strSql.Append(" point_departure= @point_departure , ");
                    strSql.Append(" cont_name= @cont_name , ");
                    strSql.Append(" cont_phone= @cont_phone , ");
                    strSql.Append(" place_arrival= @place_arrival , ");
                    strSql.Append(" warranty_period= @warranty_period , ");
                    strSql.Append(" warranty_mileage= @warranty_mileage , ");
                    strSql.Append(" enable_flag= @enable_flag , ");
                    strSql.Append(" update_time= @update_time , ");
                    strSql.Append(" update_by= @update_by  ");
                    strSql.Append(" where turner=@turner  ");
                    #endregion
                }
                else
                {
                    #region 插入语句
                    strSql.Append("insert into tb_vehicle(");
                    strSql.Append("v_id,data_source,license_plate,v_brand,vin,v_model,carbuy_date,engine_num,turner,customer_unit,use_unit,vehicle_use,operating_line,point_departure,cont_name,cont_phone,place_arrival,warranty_period,warranty_mileage,enable_flag,update_time,create_by,create_time,update_by");
                    strSql.Append(") values (");
                    strSql.Append("@v_id,@data_source,@license_plate,@v_brand,@vin,@v_model,@carbuy_date,@engine_num,@turner,@customer_unit,@use_unit,@vehicle_use,@operating_line,@point_departure,@cont_name,@cont_phone,@place_arrival,@warranty_period,@warranty_mileage,@enable_flag,@update_time,@create_by,@create_time,@update_by) ");
                    #endregion
                    v_id = Guid.NewGuid().ToString();
                    sysSQLString.Param.Add("v_id", v_id);
                    sysSQLString.Param.Add("create_by", GlobalStaticObj_Server.Instance.UserID);
                    sysSQLString.Param.Add("create_time", nowTicks);
                }

                #region  参数项 20
                sysSQLString.Param.Add("data_source", ((int)DataSources.EnumDataSources.YUTONG).ToString());
                sysSQLString.Param.Add("license_plate", item.license_plate);
                sysSQLString.Param.Add("v_brand", item.v_brand);
                sysSQLString.Param.Add("vin", item.vin);
                sysSQLString.Param.Add("v_model", item.v_model);
                string dtCarBuyDate = "";
                //购买日期为空处理
                if (!String.IsNullOrEmpty(item.carbuy_date))
                {
                    dtCarBuyDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(item.carbuy_date)).ToString();
                }
                sysSQLString.Param.Add("carbuy_date", dtCarBuyDate);
                sysSQLString.Param.Add("engine_num", item.engine_num);
                sysSQLString.Param.Add("turner", item.turner);
                sysSQLString.Param.Add("customer_unit", item.customer_unit);
                sysSQLString.Param.Add("use_unit", item.use_unit);
                sysSQLString.Param.Add("vehicle_use", WebServUtil.GetLocalDicID("vehicle_use", item.vehicle_use));
                sysSQLString.Param.Add("operating_line", item.operating_line);
                sysSQLString.Param.Add("point_departure", item.point_departure);
                sysSQLString.Param.Add("cont_name", item.cont_name);
                sysSQLString.Param.Add("cont_phone", item.cont_phone);
                sysSQLString.Param.Add("place_arrival", item.place_arrival);
                sysSQLString.Param.Add("warranty_period", item.warranty_period);
                sysSQLString.Param.Add("warranty_mileage", item.warranty_mileage);

                sysSQLString.Param.Add("enable_flag", ((int)DataSources.EnumEnableFlag.USING).ToString());
                sysSQLString.Param.Add("update_by", GlobalStaticObj_Server.Instance.UserID);
                sysSQLString.Param.Add("update_time", nowTicks);

                #endregion
                sysSQLString.sqlString = strSql.ToString();
                list.Add(sysSQLString);


                #region 车辆司机信息处理 tr_driver_vehicle,tb_driver
                string driver_id = DBHelper.GetSingleValue("获取车辆司机关系表中的司机ID", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tr_driver_vehicle", "driver_id", " v_id='" + v_id + "'", "");
                if (!string.IsNullOrEmpty(driver_id))
                {
                    #region 更新车辆司机信息
                    sysSQLString = new SysSQLString();
                    sysSQLString.cmdType = CommandType.Text;
                    sysSQLString.sqlString = " update tb_driver set driver_name=@driver_name,driver_phone=@driver_phone,update_by=@update_by,update_time=@update_time where driver_id=@driver_id";
                    sysSQLString.Param = new Dictionary<string, string>();
                    sysSQLString.Param.Add("driver_id", driver_id);
                    sysSQLString.Param.Add("driver_name", item.driver_name);
                    sysSQLString.Param.Add("driver_phone", item.driver_phone);
                    sysSQLString.Param.Add("update_by", GlobalStaticObj_Server.Instance.UserID);
                    sysSQLString.Param.Add("update_time", nowTicks);
                    list.Add(sysSQLString);
                    #endregion
                }
                else
                {
                    #region 插入车辆司机信息
                    driver_id = Guid.NewGuid().ToString();
                    sysSQLString = new SysSQLString();
                    sysSQLString.cmdType = CommandType.Text;
                    sysSQLString.sqlString = "insert into tb_driver(driver_id,driver_name,driver_phone,enable_flag,create_by,create_time,update_by,update_time) values(@driver_id,@driver_name,@driver_phone,@enable_flag,@create_by,@create_time,@update_by,@update_time)";
                    sysSQLString.Param = new Dictionary<string, string>();
                    sysSQLString.Param.Add("driver_id", driver_id);
                    sysSQLString.Param.Add("driver_name", item.driver_name);
                    sysSQLString.Param.Add("driver_phone", item.driver_phone);

                    sysSQLString.Param.Add("enable_flag", ((int)DataSources.EnumEnableFlag.USING).ToString());
                    sysSQLString.Param.Add("create_by", GlobalStaticObj_Server.Instance.UserID);
                    sysSQLString.Param.Add("create_time", nowTicks);
                    sysSQLString.Param.Add("update_by", GlobalStaticObj_Server.Instance.UserID);
                    sysSQLString.Param.Add("update_time", nowTicks);

                    list.Add(sysSQLString);
                    #endregion

                    #region 插入车辆司机关系表
                    string dir_v_id = Guid.NewGuid().ToString();
                    sysSQLString = new SysSQLString();
                    sysSQLString.cmdType = CommandType.Text;
                    sysSQLString.sqlString = "insert into tr_driver_vehicle(dir_v_id,v_id,driver_id) values(@dir_v_id,@v_id,@driver_id)";
                    sysSQLString.Param = new Dictionary<string, string>();
                    sysSQLString.Param.Add("dir_v_id", dir_v_id);
                    sysSQLString.Param.Add("v_id", v_id);
                    sysSQLString.Param.Add("driver_id", driver_id);
                    list.Add(sysSQLString);
                    #endregion
                }
                #endregion
            }
            bool flag = DBHelper.BatchExeSQLStringMultiByTrans("宇通：同步车辆信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, list);
            return flag;
        }
        /// <summary>
        /// 非批量保存车辆信息
        /// </summary>
        /// <param name="busArr">车辆信息</param>
        /// <returns></returns>
        private static bool SaveBusNotBatch(QueryBus.Detail[] busArr, ref int updateCount)
        {
            string nowTicks = Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString();
            int busIndex = 0;//列表索引
            int busCount = 10000;//每批执行条数
            //busCount = busArr.Count();
            int busSum = busArr.Count() / busCount + 1;//执行批数
            int busSumCount = busArr.Count();
            //int contactSum = 3;
            bool flag = true;//执行结果
            DateTime startDate = DateTime.Now;
            YuTongDic dic = new YuTongDic();
            //车辆
            DataTable dtVehicle = DBHelper.GetTable("获取车辆信息车辆ID", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_vehicle", "v_id,turner", null, null, null);
            //车辆司机
            DataTable dtDriver = DBHelper.GetTable("获取车辆司机关系表中的司机ID", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tr_driver_vehicle", "driver_id,v_id", null, null, null);
            DataTable dtContacts = DBHelper.GetTable("获取联系人", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_contacts", "cont_id,cont_crm_guid,cont_name", "cont_crm_guid is not null", null, null);
            //品牌
            DataTable dtBrand = DBHelper.GetTable("获取品牌", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "sys_dictionaries a inner join sys_dictionaries b on a.dic_id=b.parent_id", "b.dic_id,b.dic_name", "a.dic_code='sys_vehicle_brand'", "", "");
            //车型
            DataTable dtVehicleModels = DBHelper.GetTable("获取车型信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_vehicle_models", "models_crm_id,vm_id", "isnull(models_crm_id,'')<>''", "", "");
            #region 初始化表列
            ///车辆表
            DataTable dt = new DataTable();
            List<DataRow> listTbVehicle = new List<DataRow>();
            dt.Columns.Add(new DataColumn("v_id", typeof(string)));
            dt.Columns.Add(new DataColumn("data_source", typeof(string)));
            dt.Columns.Add(new DataColumn("license_plate", typeof(string)));
            dt.Columns.Add(new DataColumn("v_brand", typeof(string)));
            dt.Columns.Add(new DataColumn("vin", typeof(string)));
            dt.Columns.Add(new DataColumn("v_model", typeof(string)));
            dt.Columns.Add(new DataColumn("carbuy_date", typeof(long)));
            dt.Columns.Add(new DataColumn("engine_num", typeof(string)));
            dt.Columns.Add(new DataColumn("turner", typeof(string)));
            dt.Columns.Add(new DataColumn("customer_unit", typeof(string)));
            dt.Columns.Add(new DataColumn("use_unit", typeof(string)));
            dt.Columns.Add(new DataColumn("vehicle_use", typeof(string)));
            dt.Columns.Add(new DataColumn("operating_line", typeof(string)));
            dt.Columns.Add(new DataColumn("point_departure", typeof(string)));
            dt.Columns.Add(new DataColumn("cont_name", typeof(string)));
            dt.Columns.Add(new DataColumn("cont_phone", typeof(string)));
            dt.Columns.Add(new DataColumn("place_arrival", typeof(string)));
            dt.Columns.Add(new DataColumn("warranty_period", typeof(int)));
            dt.Columns.Add("status", typeof(string));
            dt.Columns.Add(new DataColumn("warranty_mileage", typeof(decimal)));
            dt.Columns.Add(new DataColumn("enable_flag", typeof(string)));
            dt.Columns.Add(new DataColumn("create_by", typeof(string)));
            dt.Columns.Add(new DataColumn("create_time", typeof(long)));
            ///车辆司机表
            DataTable dtTbDriver = new DataTable();
            List<DataRow> listTbDriver = new List<DataRow>();
            dtTbDriver.Columns.Add(new DataColumn("driver_id", typeof(string)));
            dtTbDriver.Columns.Add(new DataColumn("driver_name", typeof(string)));
            dtTbDriver.Columns.Add(new DataColumn("driver_phone", typeof(string)));
            dtTbDriver.Columns.Add(new DataColumn("enable_flag", typeof(string)));
            dtTbDriver.Columns.Add(new DataColumn("create_by", typeof(string)));
            dtTbDriver.Columns.Add(new DataColumn("create_time", typeof(long)));
            ///车辆司机关联表
            DataTable dtTrDriver = new DataTable();
            List<DataRow> listTrDriver = new List<DataRow>();
            dtTrDriver.Columns.Add(new DataColumn("dir_v_id", typeof(string)));
            dtTrDriver.Columns.Add(new DataColumn("v_id", typeof(string)));
            dtTrDriver.Columns.Add(new DataColumn("driver_id", typeof(string)));
            #endregion
            for (int i = 0; i < busSum; i++)
            {
                List<SysSQLString> list = new List<SysSQLString>();
                dt.Rows.Clear();
                dtTbDriver.Rows.Clear();
                dtTrDriver.Rows.Clear();
                listTbDriver.Clear();
                listTbVehicle.Clear();
                listTrDriver.Clear();
                for (int y = busIndex; y < busCount; y++)
                {
                    int index = i * busCount + y;
                    if (index >= busSumCount)
                    {
                        break;
                    }
                    QueryBus.Detail item = busArr[index];
                    string v_id = null;
                    string dtCarBuyDate = "";
                    //购买日期为空处理
                    if (!String.IsNullOrEmpty(item.carbuy_date))
                    {
                        dtCarBuyDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(item.carbuy_date)).ToString();
                    }
                    string vehicle_use = dic.GetLocalDicID("vehicle_use", item.vehicle_use);
                    string cont_id = string.Empty;//联系人ID转换成本地ID
                    if (!string.IsNullOrEmpty(item.cont_name))
                    {
                        DataRow[] drsContacts = dtContacts.Select(string.Format("cont_crm_guid='{0}'", item.cont_name));
                        if (drsContacts.Count() > 0)
                        {
                            cont_id = drsContacts[0]["cont_id"].ToString();
                        }
                    }
                    DataRow[] drs = dtVehicle.Select("turner='" + item.turner + "'");
                    if (drs.Count() > 0)
                    {
                        v_id = drs[0]["v_id"].ToString();
                    }

                    string brand_id = string.Empty;//品牌ID
                    DataRow[] drsBrand = dtBrand.Select(string.Format("dic_name='{0}'", item.v_brand));
                    if (drsBrand.Count() > 0)
                    {
                        brand_id = drsBrand[0]["dic_id"].ToString();
                    }
                    string models_id = string.Empty;//车型ID
                    DataRow[] drsModels = dtVehicleModels.Select(string.Format("models_crm_id='{0}'", item.v_model));
                    if (drsModels.Count() > 0)
                    {
                        models_id = drsModels[0]["vm_id"].ToString();
                    }
                    #region 验证字段长度
                    //string file = string.Empty;
                    //if (item.license_plate.Length > 30)
                    //{
                    //    file = "license_plate";
                    //}
                    //if (item.v_brand.Length > 40)
                    //{
                    //    file = "v_brand";
                    //}
                    //if (item.v_model.Length > 40)
                    //{
                    //    file = "v_model";
                    //}
                    //if (item.vin.Length > 20)
                    //{
                    //    file = "vin";
                    //}
                    //if (item.engine_num.Length > 30)
                    //{
                    //    file = "engine_num";
                    //}
                    //if (item.turner.Length > 50)
                    //{
                    //    file = "turner";
                    //}
                    //if (item.customer_unit.Length > 50)
                    //{
                    //    file = "customer_unit";
                    //}
                    //if (item.use_unit.Length > 50)
                    //{
                    //    file = "use_unit";
                    //}
                    //if (vehicle_use.Length > 50)
                    //{
                    //    file = "vehicle_use";
                    //}
                    //if (item.operating_line.Length > 50)
                    //{
                    //    file = "operating_line";
                    //}
                    //if (item.point_departure.Length > 100)
                    //{
                    //    file = "point_departure";
                    //}
                    //if (item.cont_phone.Length > 50)
                    //{
                    //    file = "cont_phone";
                    //}
                    //if (item.place_arrival.Length > 100)
                    //{
                    //    file = "place_arrival";
                    //}
                    //if (!string.IsNullOrEmpty(file))
                    //{
                    //    return false;
                    //}
                    #endregion
                    if (!string.IsNullOrEmpty(v_id))
                    {
                        #region 更新语句
                        StringBuilder strSql = new StringBuilder();
                        SysSQLString sysSQLString = new SysSQLString();
                        sysSQLString.cmdType = CommandType.Text;
                        sysSQLString.Param = new Dictionary<string, string>();
                        strSql.Append("update tb_vehicle set ");
                        strSql.AppendFormat(" data_source= '{0}' , ", (int)DataSources.EnumDataSources.YUTONG);
                        strSql.AppendFormat(" license_plate= '{0}' , ", item.license_plate);
                        strSql.AppendFormat(" v_brand= '{0}' , ", brand_id);
                        strSql.AppendFormat(" vin= '{0}' , ", item.vin);
                        strSql.AppendFormat(" v_model= '{0}' , ", models_id);
                        if (!string.IsNullOrEmpty(dtCarBuyDate))
                        {
                            strSql.AppendFormat(" carbuy_date= {0} , ", dtCarBuyDate);
                        }
                        strSql.AppendFormat(" engine_num= '{0}' , ", item.engine_num);
                        strSql.AppendFormat(" turner= '{0}' , ", item.turner);
                        strSql.AppendFormat(" customer_unit= '{0}' , ", item.customer_unit);
                        strSql.AppendFormat(" use_unit= '{0}' , ", item.use_unit);
                        strSql.AppendFormat(" vehicle_use= '{0}' , ", vehicle_use);
                        strSql.AppendFormat(" operating_line= '{0}' , ", item.operating_line);
                        strSql.AppendFormat(" point_departure= '{0}' , ", item.point_departure);
                        strSql.AppendFormat(" cont_name= '{0}' , ", cont_id);
                        strSql.AppendFormat(" cont_phone= '{0}' , ", item.cont_phone);
                        strSql.AppendFormat(" place_arrival= '{0}' , ", item.place_arrival);
                        if (!string.IsNullOrEmpty(item.warranty_period))
                        {
                            strSql.AppendFormat(" warranty_period= '{0}' , ", item.warranty_period);
                        }
                        if (!string.IsNullOrEmpty(item.warranty_mileage))
                        {
                            strSql.AppendFormat(" warranty_mileage= {0} , ", item.warranty_mileage);
                        }
                        strSql.AppendFormat(" status='{0}',", item.status == "0" ? "1" : "0");
                        strSql.AppendFormat(" enable_flag= '{0}' , ", (int)DataSources.EnumEnableFlag.USING);
                        strSql.AppendFormat(" update_time= {0} , ", nowTicks);
                        strSql.AppendFormat(" update_by= '{0}'  ", GlobalStaticObj_Server.Instance.UserID);
                        strSql.AppendFormat(" where turner='{0}'  ", item.turner);
                        sysSQLString.sqlString = strSql.ToString();
                        list.Add(sysSQLString);
                        #endregion
                    }
                    else
                    {
                        #region 插入语句
                        v_id = Guid.NewGuid().ToString();
                        //strSql.Append("insert into tb_vehicle(");
                        //strSql.Append("v_id,data_source,license_plate,v_brand,vin,v_model,carbuy_date,engine_num,turner,customer_unit,use_unit,vehicle_use,operating_line,point_departure,cont_name,cont_phone,place_arrival,warranty_period,warranty_mileage,enable_flag,create_by,create_time");
                        //strSql.Append(") values (");
                        //strSql.AppendFormat("'{0}',", v_id);
                        //strSql.AppendFormat("'{0}',", (int)DataSources.EnumDataSources.YUTONG);
                        //strSql.AppendFormat("'{0}',", item.license_plate);
                        //strSql.AppendFormat("'{0}',", item.v_brand);
                        //strSql.AppendFormat("'{0}',", item.vin);
                        //strSql.AppendFormat("'{0}',", item.v_model);
                        //strSql.AppendFormat("'{0}',", item.carbuy_date);
                        //strSql.AppendFormat("'{0}',", item.engine_num);
                        //strSql.AppendFormat("'{0}',", item.turner);
                        //strSql.AppendFormat("'{0}',", item.customer_unit);
                        //strSql.AppendFormat("'{0}',", item.use_unit);
                        //strSql.AppendFormat("'{0}',", vehicle_use);
                        //strSql.AppendFormat("'{0}',", item.operating_line);
                        //strSql.AppendFormat("'{0}',", item.point_departure);
                        //strSql.AppendFormat("'{0}',", item.cont_name);
                        //strSql.AppendFormat("'{0}',", item.cont_phone);
                        //strSql.AppendFormat("'{0}',", item.place_arrival);
                        //strSql.AppendFormat("'{0}',", item.warranty_period);
                        //strSql.AppendFormat("'{0}',", item.warranty_mileage);
                        //strSql.AppendFormat("'{0}',", (int)DataSources.EnumEnableFlag.USING);
                        //strSql.AppendFormat("'{0}',", GlobalStaticObj_Server.Instance.ClientID);
                        //strSql.AppendFormat("{0})", nowTicks);
                        DataRow dr = dt.NewRow();
                        dr["v_id"] = v_id;
                        dr["data_source"] = ((int)DataSources.EnumDataSources.YUTONG).ToString();
                        dr["license_plate"] = item.license_plate;
                        dr["v_brand"] = brand_id;
                        dr["vin"] = item.vin;
                        dr["v_model"] = models_id;
                        if (!string.IsNullOrEmpty(dtCarBuyDate))
                        {
                            dr["carbuy_date"] = dtCarBuyDate;
                        }
                        dr["engine_num"] = item.engine_num;
                        dr["turner"] = item.turner;
                        dr["customer_unit"] = item.customer_unit;
                        dr["use_unit"] = item.use_unit;
                        dr["vehicle_use"] = vehicle_use;
                        dr["operating_line"] = item.operating_line;
                        dr["point_departure"] = item.point_departure;
                        dr["cont_name"] = cont_id;
                        dr["cont_phone"] = item.cont_phone;
                        dr["place_arrival"] = item.place_arrival;
                        if (!string.IsNullOrEmpty(item.warranty_period))
                        {
                            dr["warranty_period"] = item.warranty_period;
                        }
                        if (!string.IsNullOrEmpty(item.warranty_mileage))
                        {
                            dr["warranty_mileage"] = item.warranty_mileage;
                        }
                        dr["status"] = item.status == "0" ? "1" : "0";
                        dr["enable_flag"] = ((int)DataSources.EnumEnableFlag.USING).ToString();
                        dr["create_by"] = GlobalStaticObj_Server.Instance.UserID;
                        dr["create_time"] = nowTicks;
                        //dt.Rows.Add(dr);
                        listTbVehicle.Add(dr);
                        #endregion
                    }
                    #region 车辆司机信息处理 tr_driver_vehicle,tb_driver
                    string driver_id = null;
                    string driver_name = string.Empty;
                    DataRow[] drsDriver = dtDriver.Select("v_id='" + v_id + "'");
                    if (drsDriver.Count() > 0)
                    {
                        driver_id = drsDriver[0]["driver_id"].ToString();
                    }
                    DataRow[] drsDriverName = dtContacts.Select(string.Format("cont_crm_guid='{0}'", item.driver_name));
                    if (drsDriverName.Count() > 0)
                    {
                        driver_name = drsDriverName[0]["cont_name"].ToString();
                    }
                    if (!string.IsNullOrEmpty(driver_id))
                    {
                        #region 更新车辆司机信息
                        SysSQLString sysSQLString = new SysSQLString();
                        sysSQLString.cmdType = CommandType.Text;
                        //if (item.driver_name.Length > 15)
                        //{
                        //    file = "driver_name";
                        //}
                        //if (item.driver_phone.Length > 15)
                        //{
                        //    file = "driver_phone";
                        //}
                        sysSQLString.sqlString = string.Format(" update tb_driver set driver_name='{0}',driver_phone='{1}',update_by='{2}',update_time={3} where driver_id='{4}'",
                            driver_name, item.driver_phone, GlobalStaticObj_Server.Instance.UserID, nowTicks, driver_id);
                        sysSQLString.Param = new Dictionary<string, string>();
                        list.Add(sysSQLString);
                        #endregion
                    }
                    else
                    {
                        #region 插入车辆司机信息
                        driver_id = Guid.NewGuid().ToString();
                        //                        sysSQLString = new SysSQLString();
                        //                        sysSQLString.cmdType = CommandType.Text;
                        //                        sysSQLString.sqlString = string.Format(@"insert into tb_driver(driver_id,driver_name,driver_phone,enable_flag,create_by,create_time) 
                        //                                values('{0}','{1}','{2}','{3}','{4}',{5})",
                        //                                driver_id, item.driver_name, item.driver_phone, (int)DataSources.EnumEnableFlag.USING, GlobalStaticObj_Server.Instance.ClientID, nowTicks);
                        //                        sysSQLString.Param = new Dictionary<string, string>();

                        //list.Add(sysSQLString);
                        DataRow drDriver = dtTbDriver.NewRow();
                        drDriver["driver_id"] = driver_id;
                        drDriver["driver_name"] = driver_name;
                        drDriver["driver_phone"] = item.driver_phone;
                        drDriver["enable_flag"] = ((int)DataSources.EnumEnableFlag.USING).ToString();
                        drDriver["create_by"] = GlobalStaticObj_Server.Instance.UserID;
                        drDriver["create_time"] = nowTicks;
                        //dtTbDriver.Rows.Add(drDriver);
                        listTbDriver.Add(drDriver);
                        #endregion

                        #region 插入车辆司机关系表
                        string dir_v_id = Guid.NewGuid().ToString();
                        //sysSQLString = new SysSQLString();
                        //sysSQLString.cmdType = CommandType.Text;
                        //sysSQLString.sqlString = string.Format("insert into tr_driver_vehicle(dir_v_id,v_id,driver_id) values('{0}','{1}','{2}')",
                        //    dir_v_id, v_id, driver_id);
                        //sysSQLString.Param = new Dictionary<string, string>();
                        //list.Add(sysSQLString);
                        DataRow drTr = dtTrDriver.NewRow();
                        drTr["dir_v_id"] = dir_v_id;
                        drTr["v_id"] = v_id;
                        drTr["driver_id"] = driver_id;
                        //dtTrDriver.Rows.Add(drTr);
                        listTrDriver.Add(drTr);
                        #endregion
                    }
                    #endregion
                }
                //flag = DBHelper.BatchExeSQLStringMultiByTrans("宇通：同步车辆信息", list);
                //flag = DBHelper.BatchExeSQLStringMultiByTrans("tb_vehicle", listTbVehicle);
                flag = DBHelper.SqlBulkByTransNoLogNoBackUp("同步车辆信息", GlobalStaticObj_Server.Instance.MainAccCode, "tb_vehicle", listTbVehicle);
                if (!flag)
                {
                    break;
                }
                updateCount += listTbVehicle.Count;
                //flag = DBHelper.BatchExeSQLStringMultiByTrans("tb_driver", listTbDriver);
                flag = DBHelper.SqlBulkByTransNoLogNoBackUp("同步司机", GlobalStaticObj_Server.Instance.MainAccCode, "tb_driver", listTbDriver);
                if (!flag)
                {
                    break;
                }
                updateCount += listTbDriver.Count;
                //flag = DBHelper.BatchExeSQLStringMultiByTrans("tr_driver_vehicle", listTrDriver);
                flag = DBHelper.SqlBulkByTransNoLogNoBackUp("", GlobalStaticObj_Server.Instance.MainAccCode, "tr_driver_vehicle", listTrDriver);
                if (!flag)
                {
                    break;
                }
                updateCount += listTrDriver.Count;
                if (list.Count > 0)
                {
                    flag = DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup("", GlobalStaticObj_Server.Instance.MainAccCode, list);
                    if (!flag)
                    {
                        break;
                    }
                    updateCount += list.Count;
                }
            }
            SysConfig sysConfig = new SysConfig();
            sysConfig.UpdateLastTime("BusLastTime");
            DateTime endDate = DateTime.Now;
            TimeSpan span = endDate - startDate;
            return flag;
        }

        /// <summary> 保存车辆客户信息
        /// </summary>
        /// <param name="customerArr">车辆客户信息</param>
        /// <returns>True OR False</returns>
        private static bool SaveCustomer(QueryCustomer.customer[] customerArr)
        {
            string nowTicks = Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString();
            List<SysSQLString> list = new List<SysSQLString>();
            foreach (QueryCustomer.customer item in customerArr)
            {
                SysSQLString sysSQLString = new SysSQLString();
                sysSQLString.cmdType = CommandType.Text;
                sysSQLString.Param = new Dictionary<string, string>();
                StringBuilder strSql = new StringBuilder();
                bool isContactExist = DBHelper.IsExist("判断车辆客户信息是否存在", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_customer", "cust_code='" + item.cust_code + "'");
                if (isContactExist)
                {
                    #region 更新语句
                    strSql.Append("update tb_customer set ");
                    strSql.Append(" cust_crm_guid= @cust_crm_guid , ");
                    strSql.Append(" cust_code= @cust_code , ");
                    strSql.Append(" cust_name= @cust_name , ");
                    strSql.Append(" province= @province , ");
                    strSql.Append(" city= @city , ");
                    strSql.Append(" county= @county , ");
                    strSql.Append(" cust_address= @cust_address , ");
                    strSql.Append(" zip_code= @zip_code , ");
                    strSql.Append(" cust_phone= @cust_phone , ");
                    strSql.Append(" cust_fax= @cust_fax , ");
                    strSql.Append(" cust_email= @cust_email , ");
                    strSql.Append(" cust_website= @cust_website , ");
                    strSql.Append(" credit_rating= @credit_rating , ");
                    strSql.Append(" enable_flag= @enable_flag , ");
                    strSql.Append(" data_source= @data_source , ");
                    strSql.Append(" cust_relation= @cust_relation , ");
                    strSql.Append(" indepen_legalperson= @indepen_legalperson , ");
                    strSql.Append(" market_segment= @market_segment , ");
                    strSql.Append(" institution_code= @institution_code , ");
                    strSql.Append(" com_constitution= @com_constitution , ");
                    strSql.Append(" registered_capital= @registered_capital , ");
                    strSql.Append(" agency= @agency , ");
                    strSql.Append(" status= @status , ");
                    strSql.Append(" sap_code= @sap_code , ");
                    strSql.Append(" business_scope= @business_scope , ");
                    strSql.Append(" ent_qualification= @ent_qualification , ");
                    strSql.Append(" update_by= @update_by , ");
                    strSql.Append(" update_time= @update_time  ");
                    strSql.Append(" where cust_code=@cust_code  ");
                    #endregion
                }
                else
                {
                    #region 插入语句
                    strSql.Append("insert into tb_customer(");
                    strSql.Append("cust_crm_guid,cust_id,cust_code,cust_name,province,city,county,cust_address,zip_code,cust_phone,cust_fax,cust_email,cust_website,credit_rating,enable_flag,data_source,cust_relation,indepen_legalperson,market_segment,institution_code,com_constitution,registered_capital,agency,status,sap_code,business_scope,ent_qualification,create_by,create_time,update_by,update_time");
                    strSql.Append(") values (");
                    strSql.Append("@cust_crm_guid,@cust_id,@cust_code,@cust_name,@province,@city,@county,@cust_address,@zip_code,@cust_phone,@cust_fax,@cust_email,@cust_website,@credit_rating,@enable_flag,@data_source,@cust_relation,@indepen_legalperson,@market_segment,@institution_code,@com_constitution,@registered_capital,@agency,@status,@sap_code,@business_scope,@ent_qualification,@create_by,@create_time,@update_by,@update_time) ");

                    #endregion
                    sysSQLString.Param.Add("cust_id", Guid.NewGuid().ToString());
                    sysSQLString.Param.Add("create_by", GlobalStaticObj_Server.Instance.UserID);
                    sysSQLString.Param.Add("create_time", nowTicks);
                }
                #region 参数项 24
                sysSQLString.sqlString = strSql.ToString();
                sysSQLString.Param.Add("cust_crm_guid", item.cust_crm_guid);
                sysSQLString.Param.Add("cust_code", item.cust_code);
                sysSQLString.Param.Add("cust_name", item.cust_name);
                sysSQLString.Param.Add("province", item.province);
                sysSQLString.Param.Add("city", item.city);
                sysSQLString.Param.Add("county", item.county);
                sysSQLString.Param.Add("cust_address", item.cust_address);
                sysSQLString.Param.Add("zip_code", item.zip_code);
                sysSQLString.Param.Add("cust_phone", item.cust_phone);
                sysSQLString.Param.Add("cust_fax", item.cust_fax);
                sysSQLString.Param.Add("cust_email", item.email);
                sysSQLString.Param.Add("cust_website", item.cust_website);
                sysSQLString.Param.Add("credit_rating", WebServUtil.GetLocalDicID("credit_rating", item.credit_rating));
                sysSQLString.Param.Add("cust_relation", WebServUtil.GetLocalDicID("cust_relation", item.cust_relation));
                sysSQLString.Param.Add("indepen_legalperson", item.indepen_legalperson);//独立法人 true false
                sysSQLString.Param.Add("market_segment", item.market_segment);
                sysSQLString.Param.Add("institution_code", item.institution_code);
                sysSQLString.Param.Add("com_constitution", WebServUtil.GetLocalDicID("com_constitution", item.com_constitution));
                sysSQLString.Param.Add("registered_capital", item.registered_capital);
                sysSQLString.Param.Add("agency", item.agency);//是否整车经销商 True False
                sysSQLString.Param.Add("status", item.status);//可用 0 停用 1
                sysSQLString.Param.Add("sap_code", item.sap_code);
                sysSQLString.Param.Add("business_scope", item.business_scope);
                sysSQLString.Param.Add("ent_qualification", WebServUtil.GetLocalDicID("ent_qualification", item.ent_qualification));
                sysSQLString.Param.Add("enable_flag", ((int)DataSources.EnumEnableFlag.USING).ToString());
                sysSQLString.Param.Add("data_source", ((int)DataSources.EnumDataSources.YUTONG).ToString());
                sysSQLString.Param.Add("update_by", GlobalStaticObj_Server.Instance.UserID);
                sysSQLString.Param.Add("update_time", nowTicks);
                #endregion
                sysSQLString.sqlString = strSql.ToString();
                list.Add(sysSQLString);
            }
            bool flag = DBHelper.BatchExeSQLStringMultiByTrans("宇通：同步车辆客户信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, list);
            return flag;
        }

        /// <summary>
        /// 非批量保存车辆客户信息
        /// </summary>
        /// <param name="customerArr">车辆客户信息</param>
        /// <param name="updateCount">更新条数</param>
        /// <returns></returns>
        private static bool SaveCustomerNotBatch(QueryCustomer.customer[] customerArr, ref int updateCount)
        {
            string nowTicks = Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString();
            int customerIndex = 0;//列表索引
            int customerCount = 10000;//每批执行条数
            //customerCount = customerArr.Count();
            int customerSum = customerArr.Count() / customerCount + 1;//执行批数
            //int contactSum = 3;
            bool flag = true;//执行结果
            DateTime startDate = DateTime.Now;
            YuTongDic dic = new YuTongDic();
            //客户
            DataTable dtCustomer = DBHelper.GetTable("获取车辆客户ID", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_customer", "cust_id,cust_code", null, null, null);
            #region 生成表列
            DataTable dt = new DataTable();
            List<DataRow> listTbCustomer = new List<DataRow>();
            dt.Columns.Add(new DataColumn("cust_crm_guid", typeof(string)));
            dt.Columns.Add(new DataColumn("cust_id", typeof(string)));
            dt.Columns.Add(new DataColumn("cust_code", typeof(string)));
            dt.Columns.Add(new DataColumn("cust_name", typeof(string)));
            dt.Columns.Add(new DataColumn("province", typeof(string)));
            dt.Columns.Add(new DataColumn("city", typeof(string)));
            dt.Columns.Add(new DataColumn("county", typeof(string)));
            dt.Columns.Add(new DataColumn("cust_address", typeof(string)));
            dt.Columns.Add(new DataColumn("zip_code", typeof(string)));
            dt.Columns.Add(new DataColumn("cust_phone", typeof(string)));
            dt.Columns.Add(new DataColumn("cust_fax", typeof(string)));
            dt.Columns.Add(new DataColumn("cust_email", typeof(string)));
            dt.Columns.Add(new DataColumn("cust_website", typeof(string)));
            dt.Columns.Add(new DataColumn("credit_rating", typeof(string)));
            dt.Columns.Add(new DataColumn("enable_flag", typeof(string)));
            dt.Columns.Add(new DataColumn("data_source", typeof(string)));
            dt.Columns.Add(new DataColumn("cust_relation", typeof(string)));
            dt.Columns.Add(new DataColumn("indepen_legalperson", typeof(string)));
            dt.Columns.Add(new DataColumn("market_segment", typeof(string)));
            dt.Columns.Add(new DataColumn("institution_code", typeof(string)));
            dt.Columns.Add(new DataColumn("com_constitution", typeof(string)));
            dt.Columns.Add(new DataColumn("registered_capital", typeof(string)));
            dt.Columns.Add(new DataColumn("agency", typeof(string)));
            dt.Columns.Add(new DataColumn("status", typeof(string)));
            dt.Columns.Add(new DataColumn("sap_code", typeof(string)));
            dt.Columns.Add(new DataColumn("business_scope", typeof(string)));
            dt.Columns.Add(new DataColumn("ent_qualification", typeof(string)));
            dt.Columns.Add(new DataColumn("create_by", typeof(string)));
            dt.Columns.Add(new DataColumn("create_time", typeof(long)));
            #endregion
            for (int i = 0; i < customerSum; i++)
            {
                List<SysSQLString> list = new List<SysSQLString>();
                dt.Rows.Clear();
                listTbCustomer.Clear();
                for (int y = customerIndex; y < customerCount; y++)
                {
                    int index = i * customerCount + y;
                    if (index >= customerArr.Count())
                    {
                        break;
                    }
                    QueryCustomer.customer item = customerArr[index];
                    //bool isContactExist = DBHelper.IsExist("判断车辆客户信息是否存在", "tb_customer", "cust_code='" + item.cust_code + "'");
                    string custID = null;
                    if (dtCustomer != null && dtCustomer.Rows.Count > 0)
                    {
                        DataRow[] drs = dtCustomer.Select("cust_code='" + item.cust_code + "'");
                        if (drs.Count() > 0)
                        {
                            custID = drs[0]["cust_id"].ToString();
                        }
                    }
                    string credit_rating = dic.GetLocalDicID("credit_rating", item.credit_rating);
                    string cust_relation = dic.GetLocalDicID("cust_relation", item.cust_relation);
                    string com_constitution = dic.GetLocalDicID("com_constitution", item.com_constitution);
                    string ent_qualification = dic.GetLocalDicID("ent_qualification", item.ent_qualification);
                    string market_segment = dic.GetLocalDicID("market_segment", item.market_segment);
                    if (!string.IsNullOrEmpty(custID))
                    {
                        #region 更新语句
                        SysSQLString sysSQLString = new SysSQLString();
                        sysSQLString.cmdType = CommandType.Text;
                        sysSQLString.Param = new Dictionary<string, string>();
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update tb_customer set ");
                        strSql.AppendFormat(" cust_crm_guid= '{0}' , ", item.cust_crm_guid);
                        strSql.AppendFormat(" cust_code= '{0}' , ", item.cust_code);
                        strSql.AppendFormat(" cust_name= '{0}' , ", item.cust_name);
                        strSql.AppendFormat(" province= '{0}' , ", item.province);
                        strSql.AppendFormat(" city= '{0}' , ", item.city);
                        strSql.AppendFormat(" county= '{0}' , ", item.county);
                        strSql.AppendFormat(" cust_address= '{0}' , ", item.cust_address);
                        strSql.AppendFormat(" zip_code= '{0}' , ", item.zip_code);
                        strSql.AppendFormat(" cust_phone= '{0}' , ", item.cust_phone);
                        strSql.AppendFormat(" cust_fax= '{0}' , ", item.cust_fax);
                        strSql.AppendFormat(" cust_email= '{0}' , ", item.email);
                        strSql.AppendFormat(" cust_website= '{0}' , ", item.cust_website);
                        strSql.AppendFormat(" credit_rating= '{0}' , ", credit_rating);
                        strSql.AppendFormat(" enable_flag= '{0}' , ", (int)DataSources.EnumEnableFlag.USING);
                        strSql.AppendFormat(" data_source= '{0}' , ", (int)DataSources.EnumDataSources.YUTONG);
                        strSql.AppendFormat(" cust_relation= '{0}' , ", cust_relation);
                        strSql.AppendFormat(" indepen_legalperson= '{0}' , ", item.indepen_legalperson);
                        strSql.AppendFormat(" market_segment= '{0}' , ", market_segment);
                        strSql.AppendFormat(" institution_code= '{0}' , ", item.institution_code);
                        strSql.AppendFormat(" com_constitution= '{0}' , ", com_constitution);
                        strSql.AppendFormat(" registered_capital= '{0}' , ", item.registered_capital);
                        strSql.AppendFormat(" agency= '{0}' , ", item.agency);
                        strSql.AppendFormat(" status= '{0}' , ", item.status == "0" ? "1" : "0");
                        strSql.AppendFormat(" sap_code= '{0}' , ", item.sap_code);
                        strSql.AppendFormat(" business_scope= '{0}' , ", item.business_scope);
                        strSql.AppendFormat(" ent_qualification= '{0}' , ", ent_qualification);
                        strSql.AppendFormat(" update_by= '{0}' , ", GlobalStaticObj_Server.Instance.UserID);
                        strSql.AppendFormat(" update_time={0}  ", nowTicks);
                        strSql.AppendFormat(" where cust_id='{0}'  ", custID);
                        sysSQLString.sqlString = strSql.ToString();
                        list.Add(sysSQLString);
                        #endregion
                    }
                    else
                    {
                        #region 插入语句
                        #region 验证字段长度
                        //string file = string.Empty;
                        //if (item.zip_code.Length > 5)
                        //{
                        //    file = "zip_code";
                        //}
                        //if (item.cust_crm_guid.Length > 40)
                        //{
                        //    file = "cust_crm_guid";
                        //}
                        //if (item.cust_code.Length > 30)
                        //{
                        //    file = "cust_code";
                        //}
                        //if (item.cust_name.Length > 100)
                        //{
                        //    file = "cust_name";
                        //}
                        //if (item.province.Length > 40)
                        //{
                        //    file = "province";
                        //}
                        //if (item.city.Length > 40)
                        //{
                        //    file = "city";
                        //}
                        //if (item.county.Length > 40)
                        //{
                        //    file = "county";
                        //}
                        //if (item.cust_address.Length > 100)
                        //{
                        //    file = "cust_address";
                        //}
                        //if (item.cust_phone.Length > 15)
                        //{
                        //    file = "cust_phone";
                        //}
                        //if (item.cust_fax.Length > 15)
                        //{
                        //    file = "cust_fax";
                        //}
                        //if (item.email.Length > 30)
                        //{
                        //    file = "email";
                        //}
                        //if (item.cust_website.Length > 100)
                        //{
                        //    file = "cust_website";
                        //}
                        //if (credit_rating.Length > 40)
                        //{
                        //    file = "credit_rating";
                        //}
                        //if (item.indepen_legalperson.Length > 5)
                        //{
                        //    file = "indepen_legalperson";
                        //}
                        //if (cust_relation.Length > 40)
                        //{
                        //    file = "cust_relation";
                        //}
                        //if (market_segment.Length > 40)
                        //{
                        //    file = "market_segment";
                        //}
                        //if (item.institution_code.Length > 50)
                        //{
                        //    file = "institution_code";
                        //}
                        //if (com_constitution.Length > 40)
                        //{
                        //    file = "com_constitution";
                        //}
                        //if (item.registered_capital.Length > 20)
                        //{
                        //    file = "registered_capital";
                        //}
                        //if (item.agency.Length > 30)
                        //{
                        //    file = "agency";
                        //}
                        //if (item.sap_code.Length > 50)
                        //{
                        //    file = "sap_code";
                        //}
                        //if (item.business_scope.Length > 500)
                        //{
                        //    file = "business_scope";
                        //}
                        //if (ent_qualification.Length > 40)
                        //{
                        //    file = "ent_qualification";
                        //}
                        //if (!string.IsNullOrEmpty(file))
                        //{
                        //    file = "";
                        //}
                        #endregion
                        DataRow dr = dt.NewRow();
                        dr["cust_crm_guid"] = item.cust_crm_guid;
                        dr["cust_id"] = Guid.NewGuid().ToString();
                        dr["cust_code"] = item.cust_code;
                        dr["cust_name"] = item.cust_name;
                        dr["province"] = item.province;
                        dr["city"] = item.city;
                        dr["county"] = item.county;
                        dr["cust_address"] = item.cust_address;
                        dr["zip_code"] = item.zip_code;
                        dr["cust_phone"] = item.cust_phone;
                        dr["cust_fax"] = item.cust_fax;
                        dr["cust_email"] = item.email;
                        dr["cust_website"] = item.cust_website;
                        dr["credit_rating"] = credit_rating;
                        dr["enable_flag"] = ((int)DataSources.EnumEnableFlag.USING).ToString();
                        dr["data_source"] = ((int)DataSources.EnumDataSources.YUTONG).ToString();
                        dr["cust_relation"] = cust_relation;
                        dr["indepen_legalperson"] = item.indepen_legalperson;
                        dr["market_segment"] = market_segment;
                        dr["institution_code"] = item.institution_code;
                        dr["com_constitution"] = com_constitution;
                        dr["registered_capital"] = item.registered_capital;
                        dr["agency"] = item.agency;
                        dr["status"] = item.status == "0" ? "1" : "0";
                        dr["sap_code"] = item.sap_code;
                        dr["business_scope"] = item.business_scope;
                        dr["ent_qualification"] = ent_qualification;
                        dr["create_by"] = GlobalStaticObj_Server.Instance.UserID;
                        dr["create_time"] = nowTicks;
                        //dt.Rows.Add(dr);
                        listTbCustomer.Add(dr);
                        #endregion
                    }
                }
                //flag = DBHelper.BatchExeSQLMultiByTrans("宇通：同步车辆客户信息", list);
                //flag = DBHelper.BatchExeSQLStringMultiByTrans("tb_customer", listTbCustomer);
                flag = DBHelper.SqlBulkByTransNoLogNoBackUp("同步车辆客户信息", GlobalStaticObj_Server.Instance.MainAccCode, "tb_customer", listTbCustomer);
                if (!flag)
                {
                    break;
                }
                updateCount += listTbCustomer.Count;
                if (list.Count > 0)
                {
                    flag = DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup("", GlobalStaticObj_Server.Instance.MainAccCode, list);
                    if (!flag)
                    {
                        break;
                    }
                    updateCount += list.Count;
                }
            }
            SysConfig sysConfig = new SysConfig();
            sysConfig.UpdateLastTime("CustomerLastTime");
            DateTime endDate = DateTime.Now;
            TimeSpan span = endDate - startDate;
            return flag;
        }

        /// <summary> 保存配件信息
        /// </summary>
        /// <param name="partArr">配件信息</param>
        /// <returns>True OR False</returns>
        private static bool SavePart(QueryPart.part[] partArr)
        {
            string nowTicks = Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString();
            List<SysSQLString> list = new List<SysSQLString>();
            foreach (QueryPart.part item in partArr)
            {
                #region 配件信息
                SysSQLString sysSQLString = new SysSQLString();
                sysSQLString.cmdType = CommandType.Text;
                sysSQLString.Param = new Dictionary<string, string>();
                StringBuilder strSql = new StringBuilder();
                bool isContactExist = DBHelper.IsExist("判断配件信息是否存在", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_parts", "car_parts_code='" + item.car_parts_code + "'");
                if (isContactExist)
                {
                    #region 更新语句
                    strSql.Append(" update tb_parts set ");
                    strSql.Append(" parts_name = @parts_name , ");
                    strSql.Append(" sales_unit_code = @sales_unit_code , ");
                    strSql.Append(" sales_unit_name = @sales_unit_name , ");
                    strSql.Append(" model = @model , ");
                    strSql.Append(" status = @status , ");
                    strSql.Append(" retail = @retail , ");
                    strSql.Append(" price3a = @price3a , ");
                    strSql.Append(" price2a = @price2a , ");
                    strSql.Append(" base_unit_code = @base_unit_code , ");
                    strSql.Append(" base_unit_name = @base_unit_name , ");
                    strSql.Append(" sales_unit_quantity = @sales_unit_quantity , ");
                    strSql.Append(" base_unit_quantity = @base_unit_quantity , ");
                    strSql.Append(" enable_flag = @enable_flag ");
                    strSql.Append(" data_source = @data_source ");
                    strSql.Append(" update_time = @update_time ");
                    strSql.Append(" update_by = @update_by ");
                    strSql.Append(" where car_parts_code=@car_parts_code;  ");
                    #endregion
                }
                else
                {
                    #region 插入语句
                    strSql.Append(" insert into tb_parts(");
                    strSql.Append("parts_id,car_parts_code,parts_name,sales_unit_code,data_source,sales_unit_name,model,retail,price3a,price2a,status,enable_flag,base_unit_code,base_unit_name,sales_unit_quantity,base_unit_quantity,create_by,create_time,update_by,update_time");
                    strSql.Append(") values (");
                    strSql.Append("@parts_id,@car_parts_code,@parts_name,@sales_unit_code,@data_source,@sales_unit_name,@model,@retail,@price3a,@price2a,@status,@enable_flag,@base_unit_code,@base_unit_name,@sales_unit_quantity,@base_unit_quantity,@create_by,@create_time,@update_by,@update_time");
                    strSql.Append(");  ");
                    #endregion
                    sysSQLString.Param.Add("parts_id", Guid.NewGuid().ToString());
                    sysSQLString.Param.Add("create_by", GlobalStaticObj_Server.Instance.UserID);
                    sysSQLString.Param.Add("create_time", nowTicks);
                }
                #region 参数项 9
                sysSQLString.sqlString = strSql.ToString();
                sysSQLString.Param.Add("parts_name", item.parts_name);
                sysSQLString.Param.Add("sales_unit_code", item.unit_code);
                sysSQLString.Param.Add("sales_unit_name", item.unit_name);
                sysSQLString.Param.Add("model", item.model);
                sysSQLString.Param.Add("status", item.status);
                sysSQLString.Param.Add("retail", item.retail);
                sysSQLString.Param.Add("price3a", WebServUtil.GetEncFieldValue(item.price3a));//加密
                sysSQLString.Param.Add("price2a", WebServUtil.GetEncFieldValue(item.price2a));//加密
                sysSQLString.Param.Add("base_unit_code", item.basic_unit_code);
                sysSQLString.Param.Add("base_unit_name", item.basic_unit_name);
                sysSQLString.Param.Add("sales_unit_quantity", item.unit_name_quantity);
                sysSQLString.Param.Add("base_unit_quantity", item.basic_unit_quantity);
                sysSQLString.Param.Add("enable_flag", ((int)DataSources.EnumEnableFlag.USING).ToString());
                sysSQLString.Param.Add("data_source", ((int)DataSources.EnumDataSources.YUTONG).ToString());
                sysSQLString.Param.Add("update_by", GlobalStaticObj_Server.Instance.UserID);
                sysSQLString.Param.Add("update_time", nowTicks);
                sysSQLString.Param.Add("car_parts_code", item.car_parts_code);
                #endregion
                list.Add(sysSQLString);
                #endregion
            }
            bool flag = DBHelper.BatchExeSQLStringMultiByTrans("宇通：同步车型", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, list);
            if (!flag)
            {
                return false;
            }
            list = new List<SysSQLString>();
            foreach (QueryPart.part item in partArr)
            {
                string car_parts_code = item.car_parts_code;

                #region 替代配件
                foreach (QueryPart.replaceDetail itemReplace in item.partReplace)
                {
                    SysSQLString sysSQLString0 = new SysSQLString();
                    sysSQLString0.cmdType = CommandType.Text;
                    sysSQLString0.Param = new Dictionary<string, string>();
                    StringBuilder strSql0 = new StringBuilder();
                    string partid = DBHelper.GetSingleValue("获取配件id", "tb_parts", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "parts_id", "car_parts_code='" + item.car_parts_code + "'", "");
                    string replacepartid = DBHelper.GetSingleValue("获取配件id", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_parts", "parts_id", "car_parts_code='" + itemReplace.repl_parts_code + "'", "");
                    bool isExist = DBHelper.IsExist("判断配件替代信息是否存在", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_parts_replace", "parts_id='" + partid + "' and repl_id='" + replacepartid + "'");
                    if (isExist)
                    {
                        #region 更新语句
                        strSql0.Append(" update tb_parts_replace set ");
                        strSql0.Append(" repl_parts_code = @repl_parts_code , ");
                        strSql0.Append(" repl_parts_status = @repl_parts_status , ");
                        strSql0.Append(" repl_remark = @repl_remark , ");
                        strSql0.Append(" change = @change ");
                        strSql0.Append(" update_time = @update_time ");
                        strSql0.Append(" update_by = @update_by ");
                        strSql0.Append(" where parts_id='" + partid + "' and repl_id='" + replacepartid + "'");
                        #endregion
                    }
                    else
                    {
                        #region 插入语句
                        strSql0.Append(" insert into tb_parts_replace(");
                        strSql0.Append("replace_id,parts_id,repl_id,repl_parts_code,repl_parts_status,repl_remark,change,create_by,create_time,update_by,update_time");
                        strSql0.Append(") values (");
                        strSql0.Append("@replace_id,@parts_id,@repl_id,@repl_parts_code,@repl_parts_status,@repl_remark,@change,@create_by,@create_time,@update_by,@update_time");
                        strSql0.Append("); ");
                        #endregion
                        sysSQLString0.Param.Add("replace_id", Guid.NewGuid().ToString());
                        sysSQLString0.Param.Add("create_by", GlobalStaticObj_Server.Instance.UserID);
                        sysSQLString0.Param.Add("create_time", nowTicks);
                    }
                    #region
                    sysSQLString0.sqlString = strSql0.ToString();
                    sysSQLString0.Param.Add("parts_id", partid);
                    sysSQLString0.Param.Add("repl_id", replacepartid);
                    sysSQLString0.Param.Add("repl_parts_code", itemReplace.repl_parts_code);
                    sysSQLString0.Param.Add("repl_parts_status", itemReplace.repl_parts_status);
                    sysSQLString0.Param.Add("repl_remark", itemReplace.repl_remark);
                    sysSQLString0.Param.Add("change", itemReplace.change);
                    sysSQLString0.Param.Add("update_by", GlobalStaticObj_Server.Instance.UserID);
                    sysSQLString0.Param.Add("update_time", nowTicks);
                    #endregion
                    list.Add(sysSQLString0);
                }
                #endregion
            }
            flag = DBHelper.BatchExeSQLStringMultiByTrans("宇通：同步车型", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, list);
            return flag;
        }

        /// <summary>
        /// 非批量保存配件信息
        /// </summary>
        /// <param name="partArr">配件信息</param>
        /// <param name="updateCount">更新条数</param>
        /// <returns></returns>
        private static bool SavePartNotBatch(QueryPart.part[] partArr, ref int updateCount)
        {
            long nowTicks = Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime);
            int partIndex = 0;//列表索引
            int partCount = 10000;//每批执行条数
            int partSum = partArr.Count() / partCount + 1;//执行批数
            //partSum = 0;
            bool flag = true;//执行结果
            DateTime startDate = DateTime.Now;
            YuTongDic dic = new YuTongDic();
            //配件
            DataTable dtParts = DBHelper.GetTable("配件信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_parts", "car_parts_code,parts_id",
                string.Format("data_source='{0}'", (int)DataSources.EnumDataSources.YUTONG), null, null);
            Dictionary<string, string> dicParts = new Dictionary<string, string>();
            foreach (DataRow dr in dtParts.Rows)
            {
                dicParts.Add(dr["car_parts_code"].ToString(), dr["parts_id"].ToString());
            }
            //配件替代信息
            DataTable dtReplace = DBHelper.GetTable("配件替代信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_parts_replace", "parts_id,repl_id", null, null, null);
            //Dictionary<string, string> dicReplace = new Dictionary<string, string>();
            List<string> listReplace = new List<string>();
            foreach (DataRow dr in dtReplace.Rows)
            {
                //dicReplace.Add(dr["repl_id"].ToString(), dr["parts_id"].ToString());
                listReplace.Add(string.Format("{0},{1}", dr["repl_id"], dr["parts_id"]));
            }
            CodingRule comm = new CodingRule(DataSources.EnumProjectType.Parts);
            string partCode = string.Empty;//配件编码
            #region 生成表列
            //配件表
            List<DataRow> listTbParts = new List<DataRow>();
            DataTable dtTbParts = new DataTable();
            dtTbParts.Columns.Add(new DataColumn("parts_id", typeof(string)));
            dtTbParts.Columns.Add(new DataColumn("ser_parts_code", typeof(string)));
            dtTbParts.Columns.Add(new DataColumn("car_parts_code", typeof(string)));
            dtTbParts.Columns.Add(new DataColumn("parts_name", typeof(string)));
            dtTbParts.Columns.Add(new DataColumn("sales_unit_code", typeof(string)));
            dtTbParts.Columns.Add(new DataColumn("data_source", typeof(string)));
            dtTbParts.Columns.Add(new DataColumn("default_unit", typeof(string)));
            dtTbParts.Columns.Add(new DataColumn("sales_unit_name", typeof(string)));
            dtTbParts.Columns.Add(new DataColumn("model", typeof(string)));
            dtTbParts.Columns.Add(new DataColumn("retail", typeof(decimal)));
            dtTbParts.Columns.Add(new DataColumn("price3a_back", typeof(string)));
            dtTbParts.Columns.Add(new DataColumn("price2a_back", typeof(string)));
            dtTbParts.Columns.Add(new DataColumn("status", typeof(string)));
            dtTbParts.Columns.Add(new DataColumn("enable_flag", typeof(string)));
            dtTbParts.Columns.Add("parts_type", typeof(string));
            dtTbParts.Columns.Add(new DataColumn("base_unit_code", typeof(string)));
            dtTbParts.Columns.Add(new DataColumn("base_unit_name", typeof(string)));
            dtTbParts.Columns.Add(new DataColumn("sales_unit_quantity", typeof(int)));
            dtTbParts.Columns.Add(new DataColumn("base_unit_quantity", typeof(int)));
            dtTbParts.Columns.Add(new DataColumn("create_by", typeof(string)));
            dtTbParts.Columns.Add(new DataColumn("create_time", typeof(long)));
            //配件价格信息
            DataTable dtTbPartsPrice = new DataTable();
            List<DataRow> listTbPrice = new List<DataRow>();
            dtTbPartsPrice.Columns.Add(new DataColumn("pp_id", typeof(string)));
            dtTbPartsPrice.Columns.Add(new DataColumn("parts_id", typeof(string)));
            dtTbPartsPrice.Columns.Add(new DataColumn("unit", typeof(string)));
            dtTbPartsPrice.Columns.Add(new DataColumn("out_price_one", typeof(decimal)));
            dtTbPartsPrice.Columns.Add(new DataColumn("out_price_two", typeof(decimal)));
            dtTbPartsPrice.Columns.Add(new DataColumn("out_price_three", typeof(decimal)));
            dtTbPartsPrice.Columns.Add(new DataColumn("enable_flag", typeof(string)));
            dtTbPartsPrice.Columns.Add(new DataColumn("create_by", typeof(string)));
            dtTbPartsPrice.Columns.Add(new DataColumn("create_time", typeof(long)));
            //替换配件表
            DataTable dtTbReplace = new DataTable();
            List<DataRow> listTbReplace = new List<DataRow>();
            dtTbReplace.Columns.Add(new DataColumn("replace_id", typeof(string)));
            dtTbReplace.Columns.Add(new DataColumn("parts_id", typeof(string)));
            dtTbReplace.Columns.Add(new DataColumn("repl_id", typeof(string)));
            dtTbReplace.Columns.Add(new DataColumn("repl_parts_code", typeof(string)));
            dtTbReplace.Columns.Add(new DataColumn("repl_parts_status", typeof(string)));
            dtTbReplace.Columns.Add(new DataColumn("repl_remark", typeof(string)));
            dtTbReplace.Columns.Add(new DataColumn("change", typeof(string)));
            dtTbReplace.Columns.Add(new DataColumn("create_by", typeof(string)));
            dtTbReplace.Columns.Add(new DataColumn("create_time", typeof(long)));
            //单位设置
            DataTable dtTbPartsSetup = new DataTable();
            List<DataRow> listTbSetup = new List<DataRow>();
            dtTbPartsSetup.Columns.Add(new DataColumn("set_id", typeof(string)));
            dtTbPartsSetup.Columns.Add(new DataColumn("parts_id", typeof(string)));
            dtTbPartsSetup.Columns.Add(new DataColumn("stock_unit", typeof(string)));
            dtTbPartsSetup.Columns.Add(new DataColumn("purchase_unit", typeof(string)));
            dtTbPartsSetup.Columns.Add(new DataColumn("sale_unit", typeof(string)));
            dtTbPartsSetup.Columns.Add(new DataColumn("stock_purchase", typeof(string)));
            dtTbPartsSetup.Columns.Add(new DataColumn("purchase_sale", typeof(string)));
            dtTbPartsSetup.Columns.Add(new DataColumn("create_by", typeof(string)));
            dtTbPartsSetup.Columns.Add(new DataColumn("create_time", typeof(long)));
            dtTbPartsSetup.Columns.Add(new DataColumn("enable_flag", typeof(string)));
            #endregion
            for (int i = 0; i < partSum; i++)
            {
                List<SysSQLString> list = new List<SysSQLString>();
                dtTbParts.Rows.Clear();
                dtTbReplace.Rows.Clear();
                listTbParts.Clear();
                listTbReplace.Clear();
                listTbPrice.Clear();
                listTbSetup.Clear();
                for (int y = partIndex; y < partCount; y++)
                {
                    int index = i * partCount + y;
                    if (index >= partArr.Count())
                    {
                        break;
                    }
                    #region 配件信息
                    QueryPart.part item = partArr[index];

                    string price3a = WebServUtil.GetEncField(item.price3a);//加密
                    string price2a = WebServUtil.GetEncField(item.price2a);//加密
                    string partType = dic.GetLocalDicID("part_type", item.part_type);//配件类型
                    string partsID = null;
                    bool isAdd = false;//是否新增
                    if (dicParts.ContainsKey(item.car_parts_code))
                    {
                        partsID = dicParts[item.car_parts_code];
                    }
                    if (!string.IsNullOrEmpty(partsID))
                    {
                        #region 更新语句
                        SysSQLString sysSQLString = new SysSQLString();
                        sysSQLString.cmdType = CommandType.Text;
                        sysSQLString.Param = new Dictionary<string, string>();
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append(" update tb_parts set ");
                        strSql.AppendFormat(" parts_name = '{0}' , ", item.parts_name);
                        strSql.AppendFormat(" sales_unit_code = '{0}' , ", item.unit_code);
                        strSql.AppendFormat(" sales_unit_name = '{0}' , ", item.unit_name);
                        strSql.AppendFormat(" model = '{0}' , ", item.model);
                        strSql.AppendFormat(" status = '{0}' , ", item.status == "0" ? "1" : "0");
                        strSql.AppendFormat(" retail = '{0}' , ", item.retail);
                        strSql.AppendFormat(" price3a = {0} , ", price3a);
                        strSql.AppendFormat(" price2a = {0} , ", price2a);
                        strSql.AppendFormat(" parts_type='{0}',", partType);
                        strSql.AppendFormat(" base_unit_code = '{0}' , ", item.basic_unit_code);
                        strSql.AppendFormat(" base_unit_name = '{0}' , ", item.basic_unit_name);
                        if (!string.IsNullOrEmpty(item.unit_name_quantity))
                        {
                            strSql.AppendFormat(" sales_unit_quantity = {0} , ", item.unit_name_quantity);
                        }
                        if (!string.IsNullOrEmpty(item.basic_unit_quantity))
                        {
                            strSql.AppendFormat(" base_unit_quantity = {0} , ", item.basic_unit_quantity);
                        }
                        strSql.AppendFormat(" enable_flag = '{0}' ,", (int)DataSources.EnumEnableFlag.USING);
                        strSql.AppendFormat(" data_source = '{0}' ,", (int)DataSources.EnumDataSources.YUTONG);
                        strSql.AppendFormat(" update_time = {0} ,", nowTicks);
                        strSql.AppendFormat(" update_by = '{0}' ", GlobalStaticObj_Server.Instance.UserID);
                        strSql.AppendFormat(" where car_parts_code='{0}';  ", item.car_parts_code);
                        sysSQLString.sqlString = strSql.ToString();
                        list.Add(sysSQLString);
                        #endregion
                    }
                    else
                    {
                        isAdd = true;
                        #region 插入语句
                        partsID = Guid.NewGuid().ToString();
                        //strSql.Append(" insert into tb_parts(");
                        //strSql.Append("parts_id,car_parts_code,parts_name,sales_unit_code,data_source,sales_unit_name,model,retail,price3a,price2a,status,enable_flag,base_unit_code,base_unit_name,sales_unit_quantity,base_unit_quantity,create_by,create_time");
                        //strSql.Append(") values (");
                        //strSql.AppendFormat("'{0}',", partsID);
                        //strSql.AppendFormat("'{0}',", item.car_parts_code);
                        //strSql.AppendFormat("'{0}',", item.parts_name);
                        //strSql.AppendFormat("'{0}',", item.unit_code);
                        //strSql.AppendFormat("'{0}',", (int)DataSources.EnumDataSources.YUTONG);
                        //strSql.AppendFormat("'{0}',", item.unit_name);
                        //strSql.AppendFormat("'{0}',", item.model);
                        //strSql.AppendFormat("'{0}',", item.retail);
                        //strSql.AppendFormat("{0},", price3a);
                        //strSql.AppendFormat("{0},", price2a);
                        //strSql.AppendFormat("'{0}',", item.status);
                        //strSql.AppendFormat("'{0}',", (int)DataSources.EnumEnableFlag.USING);
                        //strSql.AppendFormat("'{0}',", item.basic_unit_code);
                        //strSql.AppendFormat("'{0}',", item.basic_unit_name);
                        //strSql.AppendFormat("'{0}',", item.unit_name_quantity);
                        //strSql.AppendFormat("'{0}',", item.basic_unit_quantity);
                        //strSql.AppendFormat("'{0}',", GlobalStaticObj_Server.Instance.ClientID);
                        //strSql.AppendFormat("{0})", nowTicks);
                        //DataRow dr = dtParts.NewRow();
                        //dr["parts_id"] = partsID;
                        //dr["car_parts_code"] = item.car_parts_code;
                        //dtParts.Rows.Add(dr);
                        dicParts.Add(item.car_parts_code, partsID);
                        partCode = comm.AddNewNo();
                        DataRow drParts = dtTbParts.NewRow();
                        drParts["parts_id"] = partsID;
                        drParts["ser_parts_code"] = partCode;
                        drParts["car_parts_code"] = item.car_parts_code;
                        drParts["parts_name"] = item.parts_name;
                        drParts["sales_unit_code"] = item.unit_code;
                        drParts["data_source"] = ((int)DataSources.EnumDataSources.YUTONG).ToString();
                        drParts["default_unit"] = item.unit_name;
                        drParts["sales_unit_name"] = item.unit_name;
                        drParts["model"] = item.model;
                        if (!string.IsNullOrEmpty(item.retail))
                        {
                            drParts["retail"] = Convert.ToDecimal(item.retail);
                        }
                        drParts["price3a_back"] = item.price3a;
                        drParts["price2a_back"] = item.price2a;
                        drParts["parts_type"] = partType;
                        drParts["status"] = item.status == "0" ? "1" : "0";
                        drParts["enable_flag"] = ((int)DataSources.EnumEnableFlag.USING).ToString();
                        drParts["base_unit_code"] = item.basic_unit_code;
                        drParts["base_unit_name"] = item.basic_unit_name;
                        if (!string.IsNullOrEmpty(item.unit_name_quantity))
                        {
                            drParts["sales_unit_quantity"] = (int)Convert.ToDecimal(item.unit_name_quantity);
                        }
                        if (!string.IsNullOrEmpty(item.basic_unit_quantity))
                        {
                            drParts["base_unit_quantity"] = (int)Convert.ToDecimal(item.basic_unit_quantity);
                        }
                        drParts["create_by"] = GlobalStaticObj_Server.Instance.UserID;
                        drParts["create_time"] = nowTicks;
                        listTbParts.Add(drParts);
                        //dtTbParts.Rows.Add(drParts);

                        #endregion
                    }

                    #endregion

                    #region 替代配件
                    foreach (QueryPart.replaceDetail itemReplace in item.partReplace)
                    {
                        if (!dicParts.ContainsKey(item.car_parts_code) || dicParts.ContainsKey(itemReplace.repl_parts_code)
                            || string.IsNullOrEmpty(itemReplace.repl_parts_code))
                        {
                            continue;
                        }
                        string partid = dicParts[item.car_parts_code];
                        string replacepartid = dicParts[itemReplace.repl_parts_code];
                        if (listReplace.Contains(string.Format("{0},{1}", replacepartid, partid)))
                        {
                            #region 更新语句
                            SysSQLString sysSQLString0 = new SysSQLString();
                            sysSQLString0.cmdType = CommandType.Text;
                            sysSQLString0.Param = new Dictionary<string, string>();
                            StringBuilder strSql0 = new StringBuilder();
                            strSql0.Append(" update tb_parts_replace set ");
                            strSql0.AppendFormat(" repl_parts_code = '{0}' , ", itemReplace.repl_parts_code);
                            strSql0.AppendFormat(" repl_parts_status = '{0}' , ", itemReplace.repl_parts_status);
                            strSql0.AppendFormat(" repl_remark = '{0}' , ", itemReplace.repl_remark);
                            strSql0.AppendFormat(" change = '{0}' ", itemReplace.change);
                            strSql0.AppendFormat(" update_time = {0} ", nowTicks);
                            strSql0.AppendFormat(" update_by = '{0}' ", GlobalStaticObj_Server.Instance.UserID);
                            strSql0.AppendFormat(" where parts_id='{0}' and repl_id='{1}'", partid, replacepartid);
                            list.Add(sysSQLString0);
                            #endregion
                        }
                        else
                        {
                            #region 插入语句
                            replacepartid = Guid.NewGuid().ToString();
                            //strSql0.Append(" insert into tb_parts_replace(");
                            //strSql0.Append("replace_id,parts_id,repl_id,repl_parts_code,repl_parts_status,repl_remark,change,create_by,create_time");
                            //strSql0.Append(") values (");
                            //strSql0.AppendFormat("'{0}',", replacepartid);
                            //strSql0.AppendFormat("'{0}',", partid);
                            //strSql0.AppendFormat("'{0}',", Guid.NewGuid());
                            //strSql0.AppendFormat("'{0}',", itemReplace.repl_parts_code);
                            //strSql0.AppendFormat("'{0}',", itemReplace.repl_parts_status);
                            //strSql0.AppendFormat("'{0}',", itemReplace.repl_remark);
                            //strSql0.AppendFormat("'{0}',", itemReplace.change);
                            //strSql0.AppendFormat("'{0}',", GlobalStaticObj_Server.Instance.ClientID);
                            //strSql0.AppendFormat("{0})", nowTicks);
                            //DataRow dr = dtReplace.NewRow();
                            //dr["parts_id"] = partid;
                            //dr["repl_id"] = replacepartid;
                            //dtReplace.Rows.Add(dr);
                            listReplace.Add(string.Format("{0},{1}", replacepartid, partid));

                            DataRow drReplace = dtTbReplace.NewRow();
                            drReplace["replace_id"] = replacepartid;
                            drReplace["parts_id"] = partid;
                            drReplace["repl_id"] = Guid.NewGuid().ToString();
                            drReplace["repl_parts_code"] = itemReplace.repl_parts_code;
                            drReplace["repl_parts_status"] = itemReplace.repl_parts_status;
                            drReplace["repl_remark"] = itemReplace.repl_remark;
                            drReplace["change"] = itemReplace.change;
                            drReplace["create_by"] = GlobalStaticObj_Server.Instance.UserID;
                            drReplace["create_time"] = nowTicks;
                            //dtTbReplace.Rows.Add(drReplace);
                            listTbReplace.Add(drReplace);
                            #endregion
                        }
                    }
                    #endregion

                    #region 配件价格信息
                    if (!isAdd)
                    {
                        DataRow drPrice = dtTbPartsPrice.NewRow();
                        drPrice["pp_id"] = Guid.NewGuid().ToString();
                        drPrice["parts_id"] = partsID;
                        drPrice["unit"] = item.unit_name;
                        drPrice["is_stock"] = "0";
                        drPrice["is_stock"] = "0";
                        drPrice["is_sale"] = "1";
                        if (!string.IsNullOrEmpty(item.retail))
                        {
                            drPrice["ref_out_price"] = Convert.ToDecimal(item.retail);//参考售价
                        }
                        if (!string.IsNullOrEmpty(item.price2a))
                        {
                            drPrice["out_price_two"] = Convert.ToDecimal(item.price2a);
                        }
                        if (!string.IsNullOrEmpty(item.price3a))
                        {
                            drPrice["out_price_three"] = Convert.ToDecimal(item.price3a);
                        }
                        drPrice["sort_index"] = "0";
                        drPrice["enable_flag"] = ((int)DataSources.EnumEnableFlag.USING).ToString();
                        drPrice["create_by"] = GlobalStaticObj_Server.Instance.UserID;
                        drPrice["create_time"] = nowTicks;
                        listTbPrice.Add(drPrice);
                    }
                    #endregion
                }

                //flag = DBHelper.BatchExeSQLStringMultiByTrans("宇通：同步配件", list);
                //flag = DBHelper.BatchExeSQLStringMultiByTrans("tb_parts", listTbParts);

                flag = DBHelper.SqlBulkByTransNoLogNoBackUp("同步配件", GlobalStaticObj_Server.Instance.MainAccCode, "tb_parts", listTbParts);
                if (!flag)
                {
                    break;
                }
                #region 更新配件编码
                List<SysSQLString> listUpCode = new List<SysSQLString>();
                SysSQLString sqlCode = new SysSQLString();
                sqlCode.cmdType = CommandType.Text;
                sqlCode.Param = new Dictionary<string, string>();
                sqlCode.Param.Add("bill_code_rule_id", comm.ruleID);
                sqlCode.Param.Add("last_bill_no", partCode);
                sqlCode.sqlString = "update sys_bill_code_rule set last_bill_no=@last_bill_no where bill_code_rule_id=@bill_code_rule_id";
                listUpCode.Add(sqlCode);
                flag = DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup("", GlobalStaticObj_Server.Instance.MainAccCode, listUpCode);
                if (!flag)
                {
                    break;
                }
                #endregion
                updateCount += listTbParts.Count;
                //flag = DBHelper.BatchExeSQLStringMultiByTrans("tb_parts_replace", listTbReplace);
                flag = DBHelper.SqlBulkByTransNoLogNoBackUp("同步替换配件", GlobalStaticObj_Server.Instance.MainAccCode, "tb_parts_replace", listTbReplace);
                if (!flag)
                {
                    break;
                }
                updateCount += listTbReplace.Count;
                flag = DBHelper.SqlBulkByTransNoLogNoBackUp("同步价格信息", GlobalStaticObj_Server.Instance.MainAccCode, "tb_parts_price", listTbPrice);
                if (!flag)
                {
                    break;
                }
                updateCount += listTbPrice.Count;
                flag = DBHelper.SqlBulkByTransNoLogNoBackUp("同步单位设置", GlobalStaticObj_Server.Instance.MainAccCode, "tb_parts_setup", listTbSetup);
                if (list.Count > 0)
                {
                    flag = DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup("更新配件信息", GlobalStaticObj_Server.Instance.MainAccCode, list);
                    if (!flag)
                    {
                        break;
                    }
                    updateCount += list.Count;
                }

            }
            #region 加密价格
            List<SysSQLString> listUp = new List<SysSQLString>();
            SysSQLString sqlPrice = new SysSQLString();
            sqlPrice.cmdType = CommandType.Text;
            string price3aEnc = WebServUtil.GetEncFieldByField("price3a_back");
            string price2aEnc = WebServUtil.GetEncFieldByField("price2a_back");
            sqlPrice.sqlString = string.Format("update tb_parts set price3a={0},price2a={1},price2a_back=null,price3a_back=null where price3a_back is not null or price2a_back is not null",
                price2aEnc, price2aEnc);
            sqlPrice.Param = new Dictionary<string, string>();
            listUp.Add(sqlPrice);
            DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup("", GlobalStaticObj_Server.Instance.MainAccCode, listUp);
            #endregion

            SysConfig sysConfig = new SysConfig();
            sysConfig.UpdateLastTime("PartLastTime");
            DateTime endDate = DateTime.Now;
            TimeSpan span = endDate - startDate;

            return flag;
        }
        #endregion
    }
}
