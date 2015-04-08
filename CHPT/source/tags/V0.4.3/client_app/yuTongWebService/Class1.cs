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

namespace yuTongWebService
{
   public class WebServ_YT_BasicData1
    {
        /// <summary> 车型信息同步
        /// </summary>
        /// <returns>返回同步车型条数，如为-1，同步失败</returns>
       public static int LoadBusModel()
        {
            ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;
            QueryBusModel.clientInfo clientInfo = new QueryBusModel.clientInfo();
            clientInfo.clientID = GlobalStaticObj_Servier.Instance.ClientID;
            clientInfo.serviceID = "busModelQuery";
            QueryBusModel.busModelQueryService serv = new QueryBusModel.busModelQueryService();
            QueryBusModel.Result result = serv.busModelQuery(GlobalStaticObj_Servier.Instance.ClientID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "QUERY", clientInfo);
            if (result.state == "F")
            {
                return -1;
            }
            QueryBusModel.busModel[] busModelArr = result.Details;
            if (busModelArr.Length == 0)
            {
                return 0;
            }

            string nowTicks = Common.LocalDateTimeToUtcLong(DateTime.Now).ToString();
            List<SysSQLString> list = new List<SysSQLString>();
            foreach (QueryBusModel.busModel item in busModelArr)
            {
                SysSQLString sysSQLString = new SysSQLString();
                sysSQLString.cmdType = CommandType.Text;
                sysSQLString.Param = new Dictionary<string, string>();
                StringBuilder strSql = new StringBuilder();
                bool isContactExist = DBHelper.IsExist("判断车型信息是否存在", "tb_vehicle_models", "vm_code='" + item.vm_code + "'");
                if (isContactExist)
                {
                    #region 更新语句
                    strSql.Append(" update tb_vehicle_models set ");
                    strSql.Append(" out_price = @out_price , ");
                    strSql.Append(" out_special_price = @out_special_price , ");
                    strSql.Append(" data_sources = @data_sources , ");
                    strSql.Append(" vm_class = @vm_class , ");
                    strSql.Append(" v_sale_type = @v_sale_type , ");
                    strSql.Append(" report_price = @report_price , ");
                    strSql.Append(" repair_price = @repair_price , ");
                    strSql.Append(" begin_date = @begin_date , ");
                    strSql.Append(" end_date = @end_date , ");
                    strSql.Append(" status = @status , ");
                    strSql.Append(" update_by = @update_by , ");
                    strSql.Append(" update_time = @update_time ");
                    strSql.Append(" where vm_code=@vm_code;  ");
                    #endregion                  
                }
                else
                {
                    #region 插入语句
                    strSql.Append(" insert into tb_vehicle_models(");
                    strSql.Append("vm_id,vm_code,out_price,out_special_price,data_sources,vm_class,v_sale_type,report_price,repair_price,begin_date,end_date,status,enable_flag,create_by,create_time,update_by,update_time");
                    strSql.Append(") values (");
                    strSql.Append("@vm_id,@vm_code,@out_price,@out_special_price,@data_sources,@vm_class,@v_sale_type,@report_price,@repair_price,@begin_date,@end_date,@status,@enable_flag,@create_by,@create_time,@update_by,@update_time");
                    strSql.Append("); ");
                    #endregion
                    sysSQLString.Param.Add("vm_id", Guid.NewGuid().ToString());
                    sysSQLString.Param.Add("create_by", GlobalStaticObj_Servier.Instance.UserID);
                    sysSQLString.Param.Add("create_time", nowTicks);
                }
                #region
                sysSQLString.sqlString = strSql.ToString();
                sysSQLString.Param.Add("vm_code", item.vm_code);
                sysSQLString.Param.Add("out_price", item.out_price);
                sysSQLString.Param.Add("out_special_price", item.out_special_price);
                sysSQLString.Param.Add("vm_class", item.vm_type);
                sysSQLString.Param.Add("v_sale_type", item.v_sale_type);
                sysSQLString.Param.Add("report_price", item.report_price);
                sysSQLString.Param.Add("repair_price", item.repair_price);
                sysSQLString.Param.Add("begin_date", item.begin_date);
                sysSQLString.Param.Add("end_date", item.end_date);
                sysSQLString.Param.Add("status", item.status.ToString());
                sysSQLString.Param.Add("enable_flag", ((int)DataSources.EnumEnableFlag.USING).ToString());
                sysSQLString.Param.Add("data_sources", ((int)DataSources.EnumDataSources.YUTONG).ToString());
                sysSQLString.Param.Add("update_by", GlobalStaticObj_Servier.Instance.UserID);
                sysSQLString.Param.Add("update_time", nowTicks);
                #endregion
                list.Add(sysSQLString);
            }
            bool flag = DBHelper.BatchExeSQLStringMultiByTrans("宇通：同步车型", list);
            if (!flag)
            {
                return -1;
            }
            return busModelArr.Length;
        }


       /// <summary> 配件信息同步
       /// </summary>
       /// <param name="updateTime">最后更新时间</param>
       /// <returns>返回同步配件信息条数，如为-1，同步失败</returns>
       public static int LoadPart(string updateTime)
       {
           ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;

           QueryPart.clientInfo clientInfo = new QueryPart.clientInfo();
           clientInfo.clientID = GlobalStaticObj_Servier.Instance.ClientID;
           clientInfo.serviceID = "partQuery";
           QueryPart.partQueryService serv = new QueryPart.partQueryService();
           string dtNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
           QueryPart.Result result = serv.partQuery(GlobalStaticObj_Servier.Instance.ClientID, dtNow, "QUERY", updateTime, clientInfo);
           if (result.state == "F")
           {
               return -1;
           }
           QueryPart.part[] partArr = result.Details;
           if (partArr.Length == 0)
           {
               return 0;
           }

           string nowTicks = Common.LocalDateTimeToUtcLong(DateTime.Now).ToString();
           List<SysSQLString> list = new List<SysSQLString>();
           foreach (QueryPart.part item in partArr)
           {
               SysSQLString sysSQLString = new SysSQLString();
               sysSQLString.cmdType = CommandType.Text;
               sysSQLString.Param = new Dictionary<string, string>();
               StringBuilder strSql = new StringBuilder();
               bool isContactExist = DBHelper.IsExist("判断配件信息是否存在", "tb_parts", "car_parts_code='" + item.car_parts_code+ "'");
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
                   strSql.Append(" replaced = @replaced , ");
                   strSql.Append(" base_unit_code = @base_unit_code , ");
                   strSql.Append(" base_unit_name = @base_unit_name , ");
                   strSql.Append(" sales_unit_quantity = @sales_unit_quantity , ");
                   strSql.Append(" base_unit_quantity = @base_unit_quantity , ");
                   strSql.Append(" update_time = @update_time ");
                   strSql.Append(" where car_parts_code=@car_parts_code;  ");
                   #endregion               
               }
               else
               {
                   #region 插入语句
                   strSql.Append(" insert into tb_parts(");
                   strSql.Append("parts_id,parts_name,sales_unit_code,data_source,sales_unit_name,model,retail,price3a,price2a,status,enable_flag,replaced,base_unit_code,base_unit_name,sales_unit_quantity,base_unit_quantity,create_by,create_time,update_by,update_time");
                   strSql.Append(") values (");
                   strSql.Append("@parts_id,@parts_name,@sales_unit_code,@data_source,@sales_unit_name,@model,@retail,@price3a,@price2a,@status,@enable_flag,@replaced,@base_unit_code,@base_unit_name,@sales_unit_quantity,@base_unit_quantity,@create_by,@create_time,@update_by,@update_time");
                   strSql.Append(");  ");
                   #endregion
                   sysSQLString.Param.Add("parts_id", Guid.NewGuid().ToString());
                   sysSQLString.Param.Add("create_by", GlobalStaticObj_Servier.Instance.UserID);
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
               sysSQLString.Param.Add("price3a", item.price3a);
               sysSQLString.Param.Add("price2a", item.price2a);
               //sysSQLString.Param.Add("replaced", item.partReplace);替代关系处理
               sysSQLString.Param.Add("base_unit_code", item.basic_unit_code);
               sysSQLString.Param.Add("base_unit_name", item.basic_unit_name);
               sysSQLString.Param.Add("sales_unit_quantity", item.unit_name_quantity);
               sysSQLString.Param.Add("base_unit_quantity", item.basic_unit_quantity);
               sysSQLString.Param.Add("enable_flag", ((int)DataSources.EnumEnableFlag.USING).ToString());
               sysSQLString.Param.Add("data_source", ((int)DataSources.EnumDataSources.YUTONG).ToString());
               sysSQLString.Param.Add("update_by", GlobalStaticObj_Servier.Instance.ClientID);
               sysSQLString.Param.Add("update_time", nowTicks);
               #endregion
               list.Add(sysSQLString);

                foreach (QueryPart.replaceDetail item0 in item.partReplace)
                {
                  SysSQLString sysSQLString0 = new SysSQLString();
                  sysSQLString0.cmdType = CommandType.Text;
                  sysSQLString0.Param = new Dictionary<string, string>();
                  StringBuilder strSql0 = new StringBuilder();
                  bool isContactExist0 = DBHelper.IsExist("判断配件替代信息是否存在", "tb_parts_replace", "repl_parts_code='" + item0.repl_parts_code+ "'");
                  if (isContactExist0)
                  {
                      #region 更新语句
                      strSql0.Append(" update tb_parts_replace set ");
                      strSql0.Append(" repl_parts_status = @repl_parts_status , ");
                      strSql0.Append(" repl_remark = @repl_remark , ");
                      strSql0.Append(" change = @change ");                 
                      strSql0.Append(" update_time = @update_time ");
                      strSql0.Append(" where repl_parts_code=@repl_parts_code;  ");
                      #endregion                    
                  }
                  else
                  {
                      #region 插入语句
                      strSql0.Append(" insert into tb_parts_replace(");
                      strSql0.Append("replace_id,repl_parts_status,repl_remark,change,create_by,create_time,update_by,update_time");
                      strSql0.Append(") values (");
                      strSql0.Append("@replace_id,@repl_parts_status,@repl_remark,@change,@create_by,@create_time,@update_by,@update_time");
                      strSql0.Append("); ");
                      #endregion
                      sysSQLString0.Param.Add("replace_id", Guid.NewGuid().ToString());
                      sysSQLString0.Param.Add("create_by", GlobalStaticObj_Servier.Instance.UserID);
                      sysSQLString0.Param.Add("create_time", nowTicks);
                  }
                  #region 
                  sysSQLString0.sqlString = strSql0.ToString();
                  sysSQLString0.Param.Add("repl_parts_status", item0.repl_parts_status);
                  sysSQLString0.Param.Add("repl_remark", item0.repl_remark);
                  sysSQLString0.Param.Add("change", item0.change);
                  sysSQLString0.Param.Add("repl_parts_code", item0.repl_parts_code);
                  sysSQLString0.Param.Add("update_by", GlobalStaticObj_Servier.Instance.ClientID);
                  sysSQLString0.Param.Add("update_time", nowTicks);
                  #endregion
                  list.Add(sysSQLString0);
               }
           }
           bool flag = DBHelper.BatchExeSQLStringMultiByTrans("宇通：同步车型", list);
           if (!flag)
           {
               return -1;
           }
           return partArr.Length;
       }


       /// <summary> 产品改进号信息同步
       /// </summary>
       /// <param name="updateTime">最后更新时间</param>
       /// <returns>返回产品改进号条数，如为-1，同步失败</returns>
       public static int LoadProdImprovement(string updateTime)
       {
           ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;

           QueryProdImprovement.clientInfo clientInfo = new QueryProdImprovement.clientInfo();
           clientInfo.clientID = GlobalStaticObj_Servier.Instance.ClientID;
           clientInfo.serviceID = "prodImprovementQuery";
           QueryProdImprovement.prodImprovementQueryService serv = new  QueryProdImprovement.prodImprovementQueryService();
           QueryProdImprovement.Result result = serv.prodImprovementQuery(GlobalStaticObj_Servier.Instance.ClientID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "QUERY", updateTime, clientInfo);
           if (result.state == "F")
           {
               return -1;
           }
           QueryProdImprovement.prodImprovement[] ProdArr = result.Details;
           if (ProdArr.Length == 0)
           {
               return 0;
           }

           string nowTicks = Common.LocalDateTimeToUtcLong(DateTime.Now).ToString();
           List<SysSQLString> list = new List<SysSQLString>();
           foreach (QueryProdImprovement.prodImprovement item in ProdArr)
           {
               SysSQLString sysSQLString = new SysSQLString();
               sysSQLString.cmdType = CommandType.Text;
               sysSQLString.Param = new Dictionary<string, string>();
               StringBuilder strSql = new StringBuilder();
               bool isContactExist = DBHelper.IsExist("判断产品改进号是否存在", "tb_product_no", "service_code='" + item.service_code+ "'");
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
                   sysSQLString.Param.Add("create_by", GlobalStaticObj_Servier.Instance.ClientID);
                   sysSQLString.Param.Add("create_time", nowTicks);
               }
               #region 参数项 9
               sysSQLString.sqlString = strSql.ToString();
               sysSQLString.Param.Add("activities", item.activities);
               sysSQLString.Param.Add("service_code", item.service_code);
               sysSQLString.Param.Add("service_type", item.service_type);
               sysSQLString.Param.Add("sart_date", Common.LocalDateTimeToUtcLong(DateTime.Parse(string.IsNullOrEmpty(item.sart_date) ? DateTime.MinValue.ToString() : item.sart_date.ToString())).ToString());
               sysSQLString.Param.Add("begin_date",  Common.LocalDateTimeToUtcLong(DateTime.Parse(item.begin_date)).ToString());
               sysSQLString.Param.Add("end_date", Common.LocalDateTimeToUtcLong(DateTime.Parse(item.end_date)).ToString());
               sysSQLString.Param.Add("service_memo", item.service_memo);
               sysSQLString.Param.Add("update_by", GlobalStaticObj_Servier.Instance.ClientID);
               sysSQLString.Param.Add("update_time", nowTicks);
               #endregion
               list.Add(sysSQLString);

               foreach (QueryProdImprovement.BusDetail item0 in item.BusDetails)
                {
                  SysSQLString sysSQLString0 = new SysSQLString();
                  sysSQLString0.cmdType = CommandType.Text;
                  sysSQLString0.Param = new Dictionary<string, string>();
                  StringBuilder strSql0 = new StringBuilder();
                  bool isContactExist0 = DBHelper.IsExist("判断产品改进号车辆信息是否存在", "tb_product_no_vehicle", "vehicle_code='" + item0.vehicle_code + "'");
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
                      sysSQLString0.Param.Add("create_by", GlobalStaticObj_Servier.Instance.ClientID);
                      sysSQLString0.Param.Add("create_time", nowTicks);
                  }
                  #region 
                  sysSQLString0.sqlString = strSql0.ToString();
                  sysSQLString0.Param.Add("account_code", item0.account_code);
                  sysSQLString0.Param.Add("server_station", item0.server_station);            
                  sysSQLString0.Param.Add("update_by", GlobalStaticObj_Servier.Instance.ClientID);
                  sysSQLString0.Param.Add("update_time", nowTicks);
                  #endregion
                  list.Add(sysSQLString0);
               }

               foreach (QueryProdImprovement.PartDetail item1 in item.PartDetails)
               {
                   SysSQLString sysSQLString1 = new SysSQLString();
                   sysSQLString1.cmdType = CommandType.Text;
                   sysSQLString1.Param = new Dictionary<string, string>();
                   StringBuilder strSql1 = new StringBuilder();
                   bool isContactExist1 = DBHelper.IsExist("判断产品改进号配件信息是否存在", "tb_product_no_part", "part_code='" + item1.part_code + "'");
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
                       sysSQLString1.Param.Add("create_by", GlobalStaticObj_Servier.Instance.ClientID);
                       sysSQLString1.Param.Add("create_time", nowTicks);
                   }
                   #region
                   sysSQLString1.sqlString = strSql1.ToString();
                   sysSQLString1.Param.Add("part_code", item1.part_code);
                   sysSQLString1.Param.Add("quantity", item1.quantity);
                   sysSQLString1.Param.Add("uint", item1.@uint);
                   sysSQLString1.Param.Add("update_by", GlobalStaticObj_Servier.Instance.ClientID);
                   sysSQLString1.Param.Add("update_time", nowTicks);
                   #endregion
                   list.Add(sysSQLString1);
               }
           }
           bool flag = DBHelper.BatchExeSQLStringMultiByTrans("宇通：同步车型", list);
           if (!flag)
           {
               return -1;
           }
           return ProdArr.Length;
       }


       /// <summary> 配件销售开单
       /// </summary>
       /// <param name="updateTime">最后更新时间</param>
       /// <returns>返回同步配件信息条数，如为-1，同步失败</returns>
       public static bool UpLoadPartSales()
       {
           ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;
           SUPartSale.clientInfo clientInfo = new SUPartSale.clientInfo();
           clientInfo.clientID = GlobalStaticObj_Servier.Instance.ClientID;
           clientInfo.serviceID = "partSaleSU";
           SUPartSale.partSale  pSale = new SUPartSale.partSale();
           pSale.amount = "120";
           pSale.cust_name = "add";
           pSale.cust_phone = "123123";
           pSale.license_plate = "豫G99300";
           pSale.remark = "dfgdfsdf";
           pSale.sale_date = "2014-10-10  14:46:44";
           pSale.turner = "14H197P-0013";
           SUPartSale.partDetail[] pArry = new SUPartSale.partDetail[1];
           SUPartSale.partDetail p0 = new SUPartSale.partDetail();
           p0.amount="12";
           p0.business_count = "1";
           p0.business_price = "12";
           p0.car_parts_code = "2100-00233";
           p0.parts_remark = "asdfasdf";
           p0.remark = "asdfasf";
           p0.wh_code = "CK00000202";          
           pArry[0] = p0;    
           pSale.partDetails = pArry;
           SUPartSale.partSaleSUService serv = new SUPartSale.partSaleSUService();
          int time= serv.Timeout;
           SUPartSale.Result result = serv.partSaleSU(GlobalStaticObj_Servier.Instance.ClientID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "CREATE", pSale, clientInfo);
           if (result.state == "S")
           {
               return true;
           }
           return false;          
       }


        /// <summary> 证书验证
        /// </summary>
        /// <returns></returns>
        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

    }
}
