using System;
using System.Collections.Generic;
using System.Text;
using LogService;
using SYSModel;
using System.Data;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.ServiceModel;
using Utility.Log;
using Utility.Common;

namespace HXCPcClient.CommonClass
{
    public class DBHelper
    {
        #region 特有方法
        /// <summary> 获取使用中的帐套信息
        /// </summary>
        /// <param name="errMsg">错误信息</param>
        /// <returns>返回DataTable</returns>
        public static DataTable GetAccDataTable(out string errMsg)
        {
            DataTable dt = new DataTable();
            errMsg = "";
            if (!WCFClientProxy.TestDataProxy())
            {
                errMsg = "未能建立同服务器连接";
            }
            else
            {
                string accRtnStr = GlobalStaticObj.proxy.GetAccList();
                RespFunStruct rtnFun = JsonConvert.DeserializeObject<RespFunStruct>(accRtnStr);
                if (rtnFun.IsSuccess == "0")
                {
                    errMsg = rtnFun.Msg;
                }
                else
                {
                    dt = JsonConvert.DeserializeObject<DataTable>(rtnFun.ReturnObject);
                }
            }
            return dt;
        }

        /// <summary> 登陆验证
        /// </summary>
        /// <param name="uName">用户名</param>
        /// <param name="uPwd">密码</param>
        /// <returns>返回错误信息</returns>
        public static string LoginInput(string uName, string uPwd, string MAC, string ComputerName, string Login_Id)
        {
            if (!WCFClientProxy.TestDataProxy())
            {
                return "未能建立同服务器连接";
            }
            LoginInput loginO = new LoginInput();
            loginO.username = uName;
            loginO.pwd = uPwd;
            loginO.ComputerName = ComputerName;
            loginO.MAC = MAC;
            loginO.Login_Id = Login_Id;
            loginO.acccode = GlobalStaticObj.CurrAccCode;
            string loginStr = HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(loginO));
            loginStr = GlobalStaticObj.proxy.LoginIn(loginStr);
            string dsStr = HXCCommon.DotNetEncrypt.DESEncrypt.Decrypt(loginStr);
            RespFunStruct respO = JsonConvert.DeserializeObject<RespFunStruct>(dsStr);
            if (respO.IsSuccess == "0")
            {
                return respO.Msg;
            }
            HXCPcClient.GlobalStaticObj.gLoginDataSet = JsonConvert.DeserializeObject<DataSet>(respO.ReturnObject);
            return string.Empty;
        }

        /// <summary> 系统登出
        /// </summary>
        /// <param name="opName">操作名称</param>
        /// <param name="uid">用户id</param>
        /// <returns>返回错误信息</returns>
        public static string LoginOutput(string opName, string uid)
        {
            if (!WCFClientProxy.TestDataProxy())
            {
                return "未能建立同服务器连接";
            }
            ReqeFunStruct UserO = new ReqeFunStruct();
            /* 此处注意填入实际的当前用户ID */
            UserO.userIDOP = new UserIDOP() { OPName = opName, UserID = uid };
            UserO.AccCode = GlobalStaticObj.CurrAccCode;
            string Str = HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(UserO));
            GlobalStaticObj.proxy.LoginOut(Str);
            if (GlobalStaticObj.channelFactory.State == CommunicationState.Opened)
            {
                GlobalStaticObj.proxy = null;
                GlobalStaticObj.channelFactory.Close();
                GlobalStaticObj.channelFactory = null;
            }
            return "";
        }

        /// <summary>
        /// 获取在线用户
        /// </summary>
        /// <returns></returns>
        public static int GetOnline()
        {
            if (!WCFClientProxy.TestDataProxy())
            {
                return -1;
            }
            string code = HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(GlobalStaticObj.CurrAccCode);

            int count = GlobalStaticObj.proxy.GetOnLineUserCount(code);

            return count;
        }
        #endregion

        #region JsonOperate 契约  add by kord -- 2015.01.05
        public static LoggingService JsonOperateLogService = Log.CreateLogService("WCF", "WCF");
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static RespFunStruct JsonOperate(String str)
        {
            try
            {
                //JsonOperateLogService.WriteLog(1, HXCCommon.DotNetEncrypt.DESEncrypt.Decrypt(str));
                if (!WCFClientProxy.TestDataProxy())
                {
                    throw new Exception("未能建立同服务器连接！");
                }
                var ResultStr = GlobalStaticObj.proxy.JsonOperate(str);
                var decryptStr = HXCCommon.DotNetEncrypt.DESEncrypt.Decrypt(ResultStr);
                var resultStr = JsonConvert.DeserializeObject<RespFunStruct>(decryptStr);
                //JsonOperateLogService.WriteLog(1, resultStr.ToString());
                return resultStr;
            }
            catch (Exception ex)
            {
                JsonOperateLogService.WriteLog(ex);
                return new RespFunStruct {IsSuccess = "0", Msg = ex.Message, ReturnObject = String.Empty};
            }
        }
        #endregion

        #region 公用方法
        /// <summary> 判断记录是否存在
        /// </summary>
        /// <param name="opName">表名</param>
        /// <param name="tableName">表名</param>
        /// <param name="where">条件</param>
        /// <returns>返回boolen</returns>
        public static bool IsExist(string opName, string tableName, string where)
        {
            DataTable dt = GetTable(opName, tableName, "1", where, "", "");
            return dt.Rows.Count > 0;

        }

        /// <summary> 获取系统服务时间
        /// </summary>
        /// <returns>返回Value</returns>
        public static DateTime GetCurrentTime()
        {
            DateTime dtCurr = DateTime.Now;
            string timeStr = GetSingleValue("获取服务时间", "", "SYSDATETIME()", "", "");
            if (!string.IsNullOrEmpty(timeStr))
            {
                dtCurr = Convert.ToDateTime(timeStr);
            }
            return dtCurr;
        }

        /// <summary> 查询单个字段的值(一般求最大，最小值)
        /// <param name="opName">操作名</param>
        /// <param name="tableName">表名</param>
        /// <param name="field">单个查询字段</param>
        /// <param name="orderBy">排序字段，多个以“,”分割</param>
        /// </summary>
        /// <returns>返回Value</returns>
        public static string GetSingleValue(string opName, string tableName, string field, string where, string orderBy)
        {
            try
            {
                ReqeFunStruct reqeFunStruct = new ReqeFunStruct();
                reqeFunStruct.SubSysName = SubSysName.CommonBaseFuncCall;
                reqeFunStruct.FunName = ComFunCallEnum.ExecuteNonQueryReturnObjectNoTrans;
                reqeFunStruct.AccCode = GlobalStaticObj.CurrAccCode;
                SQLObj sqlObj = new SQLObj();
                sqlObj.cmdType = CommandType.Text;
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.Append("select " + field);
                if (!string.IsNullOrEmpty(tableName))
                {
                    strBuilder.Append(" from  " + tableName);
                    if (!string.IsNullOrEmpty(where))
                    {
                        strBuilder.Append(" where  " + where);
                    }
                    if (!string.IsNullOrEmpty(orderBy))
                    {
                        strBuilder.Append(" order by  " + orderBy);
                    }
                }
                sqlObj.sqlString = strBuilder.ToString();
                sqlObj.Param = new Dictionary<string, ParamObj>();
                ExecuteNonQueryReturnDataSetNoTrans executeNonQueryReturnDataSetNoTrans = new ExecuteNonQueryReturnDataSetNoTrans();
                executeNonQueryReturnDataSetNoTrans.ReadOnlyFlag = true;
                executeNonQueryReturnDataSetNoTrans.sqlObj = sqlObj;
                reqeFunStruct.FunObject = executeNonQueryReturnDataSetNoTrans;
                UserIDOP userOp = new UserIDOP() { UserID = GlobalStaticObj.UserID, OPName = opName };
                reqeFunStruct.userIDOP = userOp;
                reqeFunStruct.PCClientCookieStr = GlobalStaticObj.CookieStr;
                var addStr = HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(reqeFunStruct));
                var respO = JsonOperate(addStr);
                if (respO.IsSuccess == "0")
                {
                    return "";
                }
                return respO.ReturnObject;
            }
            catch (Exception ex)
            {
                JsonOperateLogService.WriteLog(ex);
                return null;
            }
        }

        /// <summary> 新增或更新
        /// </summary>
        /// /// <param name="opName">操作名</param>
        /// <param name="tableName">表名</param>
        /// <param name="keyName">主键名称</param>
        /// <param name="keyValue">主键值,如果有值，表示更新记录</param>
        /// <param name="dicFileds">字段集合</param>
        /// <returns>返回执行是否成功</returns>
        public static bool Submit_AddOrEdit(string opName, string tableName, string keyName, string keyValue, Dictionary<string, string> dicFileds)
        {
            try
            {
                ReqeFunStruct reqeFunStruct = new ReqeFunStruct();
                reqeFunStruct.SubSysName = SubSysName.CommonBaseFuncCall;
                reqeFunStruct.FunName = ComFunCallEnum.Submit_AddOrEdit;
                reqeFunStruct.AccCode = GlobalStaticObj.CurrAccCode;
                Submit_AddOrEdit submit_AddOrEdit = new Submit_AddOrEdit();
                submit_AddOrEdit.TableName = string.Format("{0}", tableName);
                submit_AddOrEdit.DicParam = dicFileds;
                submit_AddOrEdit.pkName = keyName;//默认第一个
                submit_AddOrEdit.pkVal = keyValue;
                reqeFunStruct.FunObject = submit_AddOrEdit;
                UserIDOP userOp = new UserIDOP() { UserID = GlobalStaticObj.UserID, OPName = opName };
                reqeFunStruct.userIDOP = userOp;
                reqeFunStruct.PCClientCookieStr = GlobalStaticObj.CookieStr;
                string addStr = HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(reqeFunStruct));
                var respO = JsonOperate(addStr);
                if (respO.IsSuccess == "0")
                {
                    return false;
                }
                return !String.IsNullOrEmpty(respO.ReturnObject) && bool.Parse(respO.ReturnObject);
            }
            catch (Exception ex)
            {
                JsonOperateLogService.WriteLog(ex);
                return false;
            }
        }
        /// <summary> 删除 BYID
        /// </summary>
        /// <param name="opName">操作名</param>
        /// <param name="tableName">表名</param>
        /// <param name="keyName">键名称</param>
        /// <param name="keyValue">键值</param>
        /// <returns></returns>
        public static bool DeleteDataByID(string opName, string tableName, string keyName, string keyValue)
        {
            try
            {
                ReqeFunStruct reqeFunStruct = new ReqeFunStruct();
                reqeFunStruct.SubSysName = SubSysName.CommonBaseFuncCall;
                reqeFunStruct.FunName = ComFunCallEnum.DeleteDataByID;
                reqeFunStruct.AccCode = GlobalStaticObj.CurrAccCode;
                DeleteDataByID DeleteDataByID = new DeleteDataByID();
                DeleteDataByID.TableName = string.Format("{0}", tableName);
                DeleteDataByID.pkName = keyName;//默认第一个
                DeleteDataByID.pkVal = keyValue;
                reqeFunStruct.FunObject = DeleteDataByID;
                UserIDOP userOp = new UserIDOP() { UserID = GlobalStaticObj.UserID, OPName = opName };
                reqeFunStruct.userIDOP = userOp;
                reqeFunStruct.PCClientCookieStr = GlobalStaticObj.CookieStr;
                string addStr = HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(reqeFunStruct));
                var respO = JsonOperate(addStr);
                if (respO.IsSuccess == "0")
                {
                    return false;
                }
                return !String.IsNullOrEmpty(respO.ReturnObject) && bool.Parse(respO.ReturnObject);
            }
            catch (Exception ex)
            {
                JsonOperateLogService.WriteLog(ex);
                return false;
            }
        }

        /// <summary> 根据查询条件，查单表，重点是Where条件，拼接SQL
        /// <param name="opName">操作名</param>
        /// <param name="tableName">表名</param>
        /// <param name="fileds">查询字段</param>
        /// <param name="where">查询条件，不包含where</param>
        /// <param name="groupBy">分组字段，多个以“,”分割</param>
        /// <param name="orderBy">排序字段+排序方式，多个以“,”分割</param>
        /// </summary>
        /// <returns>返回结果集</returns>
        public static DataTable GetTable(string opName, string tableName, string fileds, string where, string groupBy,
            string orderBy)
        {
            try
            {
                ReqeFunStruct reqeFunStruct = new ReqeFunStruct();
                reqeFunStruct.SubSysName = SubSysName.CommonBaseFuncCall;
                reqeFunStruct.FunName = ComFunCallEnum.SelOPSingleTable;
                reqeFunStruct.AccCode = GlobalStaticObj.CurrAccCode;
                SelOPSingleTable selOPSingleTable = new SelOPSingleTable();
                selOPSingleTable.TableName = string.Format("{0}", tableName);
                selOPSingleTable.whereString = where;
                string[] fieldArr = Regex.Split(fileds, ",", RegexOptions.IgnorePatternWhitespace);
                List<string> listField = new List<string>();
                foreach (string str in fieldArr)
                {
                    listField.Add(str);
                }
                selOPSingleTable.Fields = listField;
                selOPSingleTable.AdditionalConditions = orderBy;
                reqeFunStruct.FunObject = selOPSingleTable;
                reqeFunStruct.userIDOP.UserID = GlobalStaticObj.UserID;
                reqeFunStruct.userIDOP.OPName = opName;
                reqeFunStruct.PCClientCookieStr = GlobalStaticObj.CookieStr;

                string addStr =
                    HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(reqeFunStruct));
                var respO = JsonOperate(addStr);
                if (respO.IsSuccess == "0")
                {
                    //JsonOperateLogService.WriteLog(respO.Msg);
                    return null;
                }
                DataSet ds = JsonConvert.DeserializeObject<DataSet>(respO.ReturnObject);
                if (ds == null || ds.Tables.Count == 0)
                {
                    throw new Exception("查询异常:数据表不存在！");
                }
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                JsonOperateLogService.WriteLog(ex);
                return null;
            }
        }

        /// <summary>查询表
        /// </summary>
        /// <param name="opName">操作名</param>
        /// <param name="obj">操作sql对象</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string opName, SQLObj obj)
        {
            try
            {
                ReqeFunStruct reqeFunStruct = new ReqeFunStruct();
                reqeFunStruct.SubSysName = SubSysName.CommonBaseFuncCall;
                reqeFunStruct.FunName = ComFunCallEnum.ExecuteNonQueryReturnDataSetNoTrans;
                reqeFunStruct.AccCode = GlobalStaticObj.CurrAccCode;
                ExecuteNonQueryReturnDataSetNoTrans ExeSQLReturnDSNoTrans = new ExecuteNonQueryReturnDataSetNoTrans();
                ExeSQLReturnDSNoTrans.ReadOnlyFlag = true;
                ExeSQLReturnDSNoTrans.sqlObj = obj;
                reqeFunStruct.FunObject = ExeSQLReturnDSNoTrans;
                reqeFunStruct.userIDOP.UserID = GlobalStaticObj.UserID;
                reqeFunStruct.userIDOP.OPName = opName;
                reqeFunStruct.PCClientCookieStr = GlobalStaticObj.CookieStr;

                var addStr = HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(reqeFunStruct));
                var respO = JsonOperate(addStr);
                if (respO.IsSuccess == "0")
                {
                    return null;
                }
                var ds = JsonConvert.DeserializeObject<DataSet>(respO.ReturnObject);
                return ds;
            }
            catch (Exception ex)
            {
                JsonOperateLogService.WriteLog(ex);
                return null;
            }
        }

        /// <summary> 根据通用的分页存储过程，分页查询
        /// </summary>
        /// <param name="opName">操作名</param>
        /// <param name="tableName">表名</param>
        /// <param name="fileds">查询字段集合，以“,”分割</param>
        /// <param name="where">查询条件，不包含where</param>
        /// <param name="groupBy">分组字段，多个以“,”分割</param>
        /// <param name="orderBy">排序字段+排序方式，多个以“,”分割</param>
        /// <param name="pageIndex">查询第几页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="recordCount">返回总记录数</param>
        /// <returns>返回结果集</returns>
        public static DataTable GetTableByPage(string opName, string tableName, string fileds, string where, string groupBy, string orderBy, int pageIndex, int pageSize, out int recordCount)
        {
            try
            {
                DataSet ds = new DataSet();
                recordCount = 0;
                ReqeFunStruct reqeFunStruct = new ReqeFunStruct();
                reqeFunStruct.SubSysName = SubSysName.CommonBaseFuncCall;
                reqeFunStruct.FunName = ComFunCallEnum.SelOPByStoreProcePage;
                reqeFunStruct.AccCode = GlobalStaticObj.CurrAccCode;
                SelOPByStoreProcePage procePageInfo = new SelOPByStoreProcePage();
                procePageInfo.TableSource = string.Format("{0}", tableName);
                procePageInfo.PageIndex = pageIndex;
                procePageInfo.PageSize = pageSize;
                procePageInfo.Fields = fileds;
                //检查是否包含order by 关键字
                if (orderBy.Replace(" ", "").ToLower().IndexOf("orderby") < 0)
                {
                    orderBy = "order by " + orderBy;
                }
                procePageInfo.OrderExpression = orderBy;
                procePageInfo.whereString = where;
                reqeFunStruct.FunObject = procePageInfo;

                UserIDOP userOp = new UserIDOP() { UserID = GlobalStaticObj.UserID, OPName = opName };
                reqeFunStruct.userIDOP = userOp;

                //reqeFunStruct.userIDOP.UserID = GlobalStaticObj.UserID;
                //reqeFunStruct.userIDOP.OPName = opName;
                reqeFunStruct.PCClientCookieStr = GlobalStaticObj.CookieStr;

                var addStr = HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(reqeFunStruct));
                var respO = JsonOperate(addStr);
                if (respO.IsSuccess == "0")
                {
                    throw new Exception(respO.Msg);
                }
                if (respO.ReturnObject == null)
                {
                    throw new Exception("返回数据异常:返回数据对象为空！");
                }
                else
                {
                    ds = JsonConvert.DeserializeObject<DataSet>(respO.ReturnObject);
                    if (ds == null || ds.Tables.Count == 0)
                    {
                        throw new Exception("查询异常:数据表不存在！");
                    }
                    recordCount = respO.RecordCount;
                }
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                JsonOperateLogService.WriteLog(ex);
                recordCount = 0;
                return null;
            }
        }
        public static DataTable GetTableByPage(DataTable _dt, int pageIndex, int pageSize)
        {
            if (_dt == null || _dt.Rows.Count == 0)
            {
                return _dt;
            }
            DataTable dt = _dt.Clone();
            int startIndex = 0;
            int endIndex = 0;
            startIndex = (pageIndex - 1) * pageSize;
            if (startIndex >= _dt.Rows.Count)
            {
                pageIndex--;
                startIndex = (pageIndex - 1) * pageSize;
            }
            endIndex = startIndex;
            endIndex += pageSize + 1;
            if (endIndex > _dt.Rows.Count)
            {
                endIndex = _dt.Rows.Count;
            }
            for (int i = startIndex; i < _dt.Rows.Count; i++)
            {
                dt.ImportRow(_dt.Rows[i]);
            }
            return dt;
        }

        /// <summary> 根据主键值，单表删除多记录，In操作
        /// </summary>
        /// <param name="opName">操作名</param>
        /// <param name="sqlObjList">sql对象</param>
        /// <returns></returns>
        public static bool BatchUpdateDataByIn(string opName, string tableName, Dictionary<string, string> dicParam, string pkName, string[] pkValues)
        {
            try
            {
                ReqeFunStruct reqeFunStruct = new ReqeFunStruct();
                reqeFunStruct.SubSysName = SubSysName.CommonBaseFuncCall;
                reqeFunStruct.FunName = ComFunCallEnum.BatchUpdateDataByIn;
                reqeFunStruct.AccCode = GlobalStaticObj.CurrAccCode;
                reqeFunStruct.userIDOP.UserID = GlobalStaticObj.UserID;
                reqeFunStruct.userIDOP.OPName = opName;
                reqeFunStruct.PCClientCookieStr = GlobalStaticObj.CookieStr;

                BatchUpdateDataByIn batchUpdateDataByIn = new BatchUpdateDataByIn();
                batchUpdateDataByIn.TableName = tableName;
                batchUpdateDataByIn.DicParam = dicParam;
                batchUpdateDataByIn.pkName = pkName;
                batchUpdateDataByIn.pkValues = pkValues;
                reqeFunStruct.FunObject = batchUpdateDataByIn;

                var addStr = HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(reqeFunStruct));
                var respO = JsonOperate(addStr);
                return respO.IsSuccess == "1";
            }
            catch (Exception ex)
            {
                JsonOperateLogService.WriteLog(ex);
                return false;
            }
        }

        /// <summary> 事务批量执行
        /// </summary>
        /// <param name="opName">操作名</param>
        /// <param name="sqlObjList">批量sql</param>
        /// <returns></returns>
        public static bool BatchExeSQLMultiByTrans(string opName, IList<SQLObj> sqlObjList)
        {
            try
            {
                ReqeFunStruct reqeFunStruct = new ReqeFunStruct();
                reqeFunStruct.SubSysName = SubSysName.CommonBaseFuncCall;
                reqeFunStruct.FunName = ComFunCallEnum.BatchExeSQLMultiByTrans;
                reqeFunStruct.AccCode = GlobalStaticObj.CurrAccCode;
                reqeFunStruct.userIDOP.UserID = GlobalStaticObj.UserID;
                reqeFunStruct.userIDOP.OPName = opName;
                reqeFunStruct.PCClientCookieStr = GlobalStaticObj.CookieStr;

                BatchExeSQLMultiByTrans batchExeSQLMultiByTrans = new BatchExeSQLMultiByTrans();
                batchExeSQLMultiByTrans.batSQLObjList = sqlObjList;
                reqeFunStruct.FunObject = batchExeSQLMultiByTrans;

                var addStr = HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(reqeFunStruct));
                var respO = JsonOperate(addStr);
                return respO.IsSuccess == "1";
            }
            catch (Exception ex)
            {
                JsonOperateLogService.WriteLog(ex);
                return false;
            }
        }
        /// <summary> 事务批量执行（List<string>）
        /// 创建人：唐春奎
        /// </summary>
        /// <param name="opName">操作名</param>
        /// <param name="sqlStringList">批量sql</param>
        /// <returns></returns>
        public static bool BatchExeSQLStringMultiByTrans(string opName, IList<SysSQLString> sqlStringList)
        {
            try
            {
                ReqeFunStruct reqeFunStruct = new ReqeFunStruct();
                reqeFunStruct.SubSysName = SubSysName.CommonBaseFuncCall;
                reqeFunStruct.FunName = ComFunCallEnum.BatchExeSQLStringMultiByTrans;
                reqeFunStruct.AccCode = GlobalStaticObj.CurrAccCode;
                reqeFunStruct.userIDOP.UserID = GlobalStaticObj.UserID;
                reqeFunStruct.userIDOP.OPName = opName;
                reqeFunStruct.PCClientCookieStr = GlobalStaticObj.CookieStr;

                BatchExeSQLStringMultiByTrans batchExeSQLStringByTrans = new BatchExeSQLStringMultiByTrans();
                batchExeSQLStringByTrans.batSQLStringList = sqlStringList;
                reqeFunStruct.FunObject = batchExeSQLStringByTrans;

                var addStr = HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(reqeFunStruct));
                var respO = JsonOperate(addStr);
                return respO.ReturnObject == "True";
            }
            catch (Exception ex)
            {
                JsonOperateLogService.WriteLog(ex);
                return false;
            }
        }
        /// <summary> 根据条件删除指定表中的数据
        /// 创建人：唐春奎
        /// </summary>
        /// <param name="opName">操作名</param>
        /// <param name="tableName">表名</param>
        /// <param name="strWhere">执行条件，不包含where</param>
        /// <returns></returns>
        public static bool BatchDeleteDataByWhere(string opName, string tableName, string strWhere)
        {
            try
            {
                ReqeFunStruct reqeFunStruct = new ReqeFunStruct();
                reqeFunStruct.SubSysName = SubSysName.CommonBaseFuncCall;
                reqeFunStruct.FunName = ComFunCallEnum.BatchDeleteDataByWhere;
                reqeFunStruct.AccCode = GlobalStaticObj.CurrAccCode;
                reqeFunStruct.userIDOP.UserID = GlobalStaticObj.UserID;
                reqeFunStruct.userIDOP.OPName = opName;
                reqeFunStruct.PCClientCookieStr = GlobalStaticObj.CookieStr;

                BatchDeleteDataByWhere batchDeleteDataByWhere = new BatchDeleteDataByWhere();
                batchDeleteDataByWhere.TableName = tableName;
                batchDeleteDataByWhere.whereString = strWhere;
                reqeFunStruct.FunObject = batchDeleteDataByWhere;

                var deleteStr = HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(reqeFunStruct));
                var respO = JsonOperate(deleteStr);
                return respO.IsSuccess == "1";
                //return respO.ReturnObject == "True";
            }
            catch (Exception ex)
            {
                JsonOperateLogService.WriteLog(ex);
                return false;
            }
        }

        #region WebService 契约  add by kord -- 2015.01.05
        public static LoggingService WebServiceLogService = Log.CreateLogService("WebService", "WebService");

        /// <summary> 调用webService方法
        /// </summary>
        /// <param name="funcName">调用webService方法的枚举</param>
        /// <param name="funcObject">调用Webservice传递对象，参考枚举描述</param>
        /// <returns>返回错信息,如果错误信息为空则为执行成功</returns>
        public static RespFunStruct WebServHandlerByFun(string opName, SYSModel.EnumWebServFunName enumFuncName, object webServFuncObj)
        {
            ReqeFunStruct_WebServ reqeFunStruct = new ReqeFunStruct_WebServ();
            reqeFunStruct.FunName = enumFuncName;
            reqeFunStruct.AccCode = GlobalStaticObj.CurrAccCode;
            reqeFunStruct.userIDOP.UserID = GlobalStaticObj.UserID;
            reqeFunStruct.userIDOP.OPName = opName;
            reqeFunStruct.PCClientCookieStr = GlobalStaticObj.CookieStr;
            reqeFunStruct.FunObject = webServFuncObj;

            string jsonStr = HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(reqeFunStruct));
            if (!WCFClientProxy.TestDataProxy())
            {
                throw new Exception("未能建立同服务器连接！");
            }
            RespFunStruct resp = new RespFunStruct();
            try
            {
                string resultStr = GlobalStaticObj.proxy.HandleWebServ(jsonStr);
                if (string.IsNullOrEmpty(resultStr))
                {
                    throw new Exception("调用webservice失败！");
                }
                string Str = HXCCommon.DotNetEncrypt.DESEncrypt.Decrypt(resultStr);
                resp = JsonConvert.DeserializeObject<RespFunStruct>(Str);
                return resp;
            }
            catch (TimeoutException te)
            {
                WebServiceLogService.WriteLog(te);
                resp.IsSuccess = "0";
                resp.Msg = "操作超时";
            }
            catch (Exception e)
            {
                WebServiceLogService.WriteLog(e);
                resp.IsSuccess = "0";
                resp.Msg = "调用webservice出错！";
            }
            return resp;
        }

        /// <summary> 调用webService方法
        /// </summary>
        /// <param name="opName">操作名</param>
        /// <param name="enumFuncName">调用webService方法的枚举</param>
        /// <param name="webServFuncObj">调用Webservice传递对象，参考枚举描述</param>
        /// <returns>调用服务的消息,如果为空,则调用成功</returns>
        public static string WebServHandler(string opName, SYSModel.EnumWebServFunName enumFuncName, object webServFuncObj)
        {
            RespFunStruct resp = WebServHandlerByFun(opName, enumFuncName, webServFuncObj);
            if (resp.IsSuccess == "0")
            {
                return resp.Msg;
            }
            return "";
        }

        /// <summary>调用webService方法
        /// </summary>
        /// <param name="opName">操作名</param>
        /// <param name="enumFuncName">调用webService方法的枚举</param>
        /// <param name="webServFuncObj">调用Webservice传递对象，参考枚举描述</param>
        /// <returns>调用服务返回的对象,如果为空,怎调用失败</returns>
        public static string WebServHandlerByObj(string opName, SYSModel.EnumWebServFunName enumFuncName, object webServFuncObj)
        {
            RespFunStruct resp = WebServHandlerByFun(opName, enumFuncName, webServFuncObj);
            if (resp.IsSuccess == "0")
            {
                return null;
            }
            return resp.ReturnObject;
        }

        public static void LogFunctionCall(string fun_id, string setbook_id)
        {
            ReqeFunStruct reqeFunStruct = new ReqeFunStruct();
            reqeFunStruct.SubSysName = SubSysName.ExtendBaseFuncCall;
            reqeFunStruct.FunName = ComFunCallEnum.LogFunctionCall;
            reqeFunStruct.AccCode = GlobalStaticObj.CurrAccCode;
            UserFunctionOPLog FunctionOP_Add = new UserFunctionOPLog();
            FunctionOP_Add.com_id = GlobalStaticObj.CurrUserCom_Id;
            FunctionOP_Add.fun_id = fun_id;           
            FunctionOP_Add.access_time = Common.LocalDateTimeToUtcLong(GlobalStaticObj.CurrentDateTime);
            FunctionOP_Add.userOP = new SYSModel.UserIDOP() { UserID = GlobalStaticObj.UserID, OPName = "菜单功能操作日志" };
            reqeFunStruct.FunObject = FunctionOP_Add;
            UserIDOP userOp = new SYSModel.UserIDOP() { UserID = GlobalStaticObj.UserID, OPName = "菜单功能操作日志" };
            reqeFunStruct.userIDOP = userOp;
            reqeFunStruct.PCClientCookieStr = GlobalStaticObj.CookieStr;
            string addStr = HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(reqeFunStruct));
            if (!WCFClientProxy.TestDataProxy())
            {
                throw new Exception("未能建立同服务器连接！");
            }
            GlobalStaticObj.proxy.JsonOperate(addStr);           
        }
        #endregion

        /// <summary> 事务批量执行，不执行备份
        /// </summary>
        /// <param name="opName">操作名</param>
        /// <param name="sqlStringList">批量sql</param>
        /// <returns></returns>
        public static bool BatchExeSQLStrMultiByTransNoLogNoBackup(string opName, IList<SysSQLString> sqlStringList)
        {
            try
            {
                ReqeFunStruct reqeFunStruct = new ReqeFunStruct();
                reqeFunStruct.SubSysName = SubSysName.CommonBaseFuncCall;
                reqeFunStruct.FunName = ComFunCallEnum.BatchExeSQLStrMultiByTransNoLogNoBackup;
                reqeFunStruct.AccCode = GlobalStaticObj.CurrAccCode;
                reqeFunStruct.userIDOP.UserID = GlobalStaticObj.UserID;
                reqeFunStruct.userIDOP.OPName = opName;
                reqeFunStruct.PCClientCookieStr = GlobalStaticObj.CookieStr;

                BatchExeSQLStringMultiByTrans batchExeSQLStringByTrans = new BatchExeSQLStringMultiByTrans();
                batchExeSQLStringByTrans.batSQLStringList = sqlStringList;
                reqeFunStruct.FunObject = batchExeSQLStringByTrans;

                string addStr = HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(reqeFunStruct));
                if (!WCFClientProxy.TestDataProxy())
                {
                    throw new Exception("未能建立同服务器连接！");
                }
                string ResultStr = string.Empty;
                try
                {
                    ResultStr = GlobalStaticObj.proxy.JsonOperate(addStr);
                }
                catch (Exception e)
                {
                    return false;
                }

                string Str = HXCCommon.DotNetEncrypt.DESEncrypt.Decrypt(ResultStr);
                RespFunStruct respO = JsonConvert.DeserializeObject<RespFunStruct>(Str);
                return respO.ReturnObject == "True";
            }
            catch (Exception ex)
            {
                JsonOperateLogService.WriteLog(ex);
                return false;
            }
        }
        #endregion

        #region 跨库调用实现,需传入帐套编码
        /// <summary> 判断记录是否存在
        /// </summary>
        /// <param name="opName">操作名称</param>
        /// <param name="accCode">帐套代码</param>
        /// <param name="tableName">表名</param>
        /// <param name="where">条件</param>
        /// <returns>返回boolen</returns>
        public static bool IsExist(string opName, string accCode, string tableName, string where)
        {
            DataTable dt = GetTable(opName, accCode, tableName, "1", where, "", "");
            return dt.Rows.Count > 0;

        }

        /// <summary> 查询单个字段的值(一般求最大，最小值)
        /// <param name="opName">操作名</param>
        /// <param name="accCode">帐套代码</param>
        /// <param name="tableName">表名</param>
        /// <param name="field">单个查询字段</param>
        /// <param name="orderBy">排序字段，多个以“,”分割</param>
        /// </summary>
        /// <returns>返回Value</returns>
        public static string GetSingleValue(string opName, string accCode, string tableName, string field, string where, string orderBy)
        {
            ReqeFunStruct reqeFunStruct = new ReqeFunStruct();
            reqeFunStruct.SubSysName = SubSysName.CommonBaseFuncCall;
            reqeFunStruct.FunName = ComFunCallEnum.ExecuteNonQueryReturnObjectNoTrans;
            reqeFunStruct.AccCode = accCode;
            SQLObj sqlObj = new SQLObj();
            sqlObj.cmdType = CommandType.Text;
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("select " + field);
            if (!string.IsNullOrEmpty(tableName))
            {
                strBuilder.Append(" from  " + tableName);
                if (!string.IsNullOrEmpty(where))
                {
                    strBuilder.Append(" where  " + where);
                }
                if (!string.IsNullOrEmpty(orderBy))
                {
                    strBuilder.Append(" order by  " + orderBy);
                }
            }
            sqlObj.sqlString = strBuilder.ToString();
            sqlObj.Param = new Dictionary<string, ParamObj>();
            ExecuteNonQueryReturnDataSetNoTrans executeNonQueryReturnDataSetNoTrans = new ExecuteNonQueryReturnDataSetNoTrans();
            executeNonQueryReturnDataSetNoTrans.ReadOnlyFlag = true;
            executeNonQueryReturnDataSetNoTrans.sqlObj = sqlObj;
            reqeFunStruct.FunObject = executeNonQueryReturnDataSetNoTrans;
            UserIDOP userOp = new UserIDOP() { UserID = GlobalStaticObj.UserID, OPName = opName };
            reqeFunStruct.userIDOP = userOp;
            reqeFunStruct.PCClientCookieStr = GlobalStaticObj.CookieStr;
            var addStr = HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(reqeFunStruct));
            var respO = JsonOperate(addStr);
            if (respO.IsSuccess == "0")
            {
                return "";
            }
            return respO.ReturnObject;
        }

        /// <summary> 新增或更新
        /// </summary>
        /// <param name="opName">操作名</param>
        /// <param name="accCode">帐套代码</param>
        /// <param name="tableName">表名</param>
        /// <param name="keyName">主键名称</param>
        /// <param name="keyValue">主键值,如果有值，表示更新记录</param>
        /// <param name="dicFileds">字段集合</param>
        /// <returns>返回执行是否成功</returns>
        public static bool Submit_AddOrEdit(string opName, string accCode, string tableName, string keyName, string keyValue, Dictionary<string, string> dicFileds)
        {
            ReqeFunStruct reqeFunStruct = new ReqeFunStruct();
            reqeFunStruct.SubSysName = SubSysName.CommonBaseFuncCall;
            reqeFunStruct.FunName = ComFunCallEnum.Submit_AddOrEdit;
            reqeFunStruct.AccCode = accCode;
            Submit_AddOrEdit submit_AddOrEdit = new Submit_AddOrEdit();
            submit_AddOrEdit.TableName = string.Format("{0}", tableName);
            submit_AddOrEdit.DicParam = dicFileds;
            submit_AddOrEdit.pkName = keyName;//默认第一个
            submit_AddOrEdit.pkVal = keyValue;
            reqeFunStruct.FunObject = submit_AddOrEdit;
            UserIDOP userOp = new UserIDOP() { UserID = GlobalStaticObj.UserID, OPName = opName };
            reqeFunStruct.userIDOP = userOp;
            reqeFunStruct.PCClientCookieStr = GlobalStaticObj.CookieStr;
            var addStr = HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(reqeFunStruct));
            var respO = JsonOperate(addStr);
            if (respO.IsSuccess == "0")
            {
                return false;
            }
            return bool.Parse(respO.ReturnObject);
        }
        /// <summary> 删除 BYID
        /// </summary>
        /// <param name="opName">操作名</param>
        /// <param name="accCode">帐套代码</param>
        /// <param name="tableName">表名</param>
        /// <param name="keyName">键名称</param>
        /// <param name="keyValue">键值</param>
        /// <returns></returns>
        public static bool DeleteDataByID(string opName, string accCode, string tableName, string keyName, string keyValue)
        {
            ReqeFunStruct reqeFunStruct = new ReqeFunStruct();
            reqeFunStruct.SubSysName = SubSysName.CommonBaseFuncCall;
            reqeFunStruct.FunName = ComFunCallEnum.DeleteDataByID;
            reqeFunStruct.AccCode = accCode;
            DeleteDataByID DeleteDataByID = new DeleteDataByID();
            DeleteDataByID.TableName = string.Format("{0}", tableName);
            DeleteDataByID.pkName = keyName;//默认第一个
            DeleteDataByID.pkVal = keyValue;
            reqeFunStruct.FunObject = DeleteDataByID;
            UserIDOP userOp = new UserIDOP() { UserID = GlobalStaticObj.UserID, OPName = opName };
            reqeFunStruct.userIDOP = userOp;
            reqeFunStruct.PCClientCookieStr = GlobalStaticObj.CookieStr;
            var addStr = HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(reqeFunStruct));
            var respO = JsonOperate(addStr);
            if (respO.IsSuccess == "0")
            {
                return false;
            }
            return bool.Parse(respO.ReturnObject);
        }
        /// <summary> 根据查询条件，查单表，重点是Where条件，拼接SQL
        /// <param name="opName">操作名</param>
        /// <param name="accCode">帐套代码</param>
        /// <param name="tableName">表名</param>
        /// <param name="fileds">查询字段</param>
        /// <param name="where">查询条件，不包含where</param>
        /// <param name="groupBy">分组字段，多个以“,”分割</param>
        /// <param name="orderBy">排序字段+排序方式，多个以“,”分割</param>
        /// </summary>
        /// <returns>返回结果集</returns>
        public static DataTable GetTable(string opName, string accCode, string tableName, string fileds, string where, string groupBy, string orderBy)
        {
            try
            {
                ReqeFunStruct reqeFunStruct = new ReqeFunStruct();
                reqeFunStruct.SubSysName = SubSysName.CommonBaseFuncCall;
                reqeFunStruct.FunName = ComFunCallEnum.SelOPSingleTable;
                reqeFunStruct.AccCode = accCode;
                SelOPSingleTable selOPSingleTable = new SelOPSingleTable();
                selOPSingleTable.TableName = string.Format("{0}", tableName);
                selOPSingleTable.whereString = where;
                string[] fieldArr = Regex.Split(fileds, ",", RegexOptions.IgnorePatternWhitespace);
                List<string> listField = new List<string>();
                foreach (string str in fieldArr)
                {
                    listField.Add(str);
                }
                selOPSingleTable.Fields = listField;
                selOPSingleTable.AdditionalConditions = orderBy;
                reqeFunStruct.FunObject = selOPSingleTable;
                reqeFunStruct.userIDOP.UserID = GlobalStaticObj.UserID;
                reqeFunStruct.userIDOP.OPName = opName;
                reqeFunStruct.PCClientCookieStr = GlobalStaticObj.CookieStr;

                var addStr = HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(reqeFunStruct));
                var respO = JsonOperate(addStr);
                if (respO.IsSuccess == "0")
                {
                    //JsonOperateLogService.WriteLog(respO.Msg);
                    return null;
                }
                DataSet ds = JsonConvert.DeserializeObject<DataSet>(respO.ReturnObject);
                if (ds == null || ds.Tables.Count == 0)
                {
                    throw new Exception("查询异常:数据表不存在！");
                }
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                JsonOperateLogService.WriteLog(ex);
                return null;
            }
        }

        /// <summary> 查询表
        /// </summary>
        /// <param name="opName">操作名</param>
        /// <param name="accCode">帐套代码</param>
        /// <param name="obj">操作sql对象</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string opName, string accCode, SQLObj obj)
        {
            ReqeFunStruct reqeFunStruct = new ReqeFunStruct();
            reqeFunStruct.SubSysName = SubSysName.CommonBaseFuncCall;
            reqeFunStruct.FunName = ComFunCallEnum.ExecuteNonQueryReturnDataSetNoTrans;
            reqeFunStruct.AccCode = accCode;
            ExecuteNonQueryReturnDataSetNoTrans ExeSQLReturnDSNoTrans = new ExecuteNonQueryReturnDataSetNoTrans();
            ExeSQLReturnDSNoTrans.ReadOnlyFlag = true;
            ExeSQLReturnDSNoTrans.sqlObj = obj;
            reqeFunStruct.FunObject = ExeSQLReturnDSNoTrans;
            reqeFunStruct.userIDOP.UserID = GlobalStaticObj.UserID;
            reqeFunStruct.userIDOP.OPName = opName;
            reqeFunStruct.PCClientCookieStr = GlobalStaticObj.CookieStr;

            var addStr = HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(reqeFunStruct));
            var respO = JsonOperate(addStr);
            if (respO.IsSuccess == "0")
            {
                return null;
            }
            DataSet ds = JsonConvert.DeserializeObject<DataSet>(respO.ReturnObject);
            return ds;
        }

        /// <summary> 根据通用的分页存储过程，分页查询
        /// </summary>
        /// <param name="opName">操作名</param>
        /// <param name="accCode">帐套代码</param>
        /// <param name="tableName">表名</param>
        /// <param name="fileds">查询字段集合，以“,”分割</param>
        /// <param name="where">查询条件，不包含where</param>
        /// <param name="groupBy">分组字段，多个以“,”分割</param>
        /// <param name="orderBy">排序字段+排序方式，多个以“,”分割</param>
        /// <param name="pageIndex">查询第几页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="recordCount">返回总记录数</param>
        /// <returns>返回结果集</returns>
        public static DataTable GetTableByPage(string opName, string accCode, string tableName, string fileds, string where, string groupBy, string orderBy, int pageIndex, int pageSize, out int recordCount)
        {
            try
            {               
                recordCount = 0;
                ReqeFunStruct reqeFunStruct = new ReqeFunStruct();
                reqeFunStruct.SubSysName = SubSysName.CommonBaseFuncCall;
                reqeFunStruct.FunName = ComFunCallEnum.SelOPByStoreProcePage;
                reqeFunStruct.AccCode = accCode;
                SelOPByStoreProcePage procePageInfo = new SelOPByStoreProcePage();
                procePageInfo.TableSource = string.Format("{0}", tableName);
                procePageInfo.PageIndex = pageIndex;
                procePageInfo.PageSize = pageSize;
                procePageInfo.Fields = fileds;
                //检查是否包含order by 关键字
                if (orderBy.Replace(" ", "").ToLower().IndexOf("orderby") < 0)
                {
                    orderBy = "order by " + orderBy;
                }
                procePageInfo.OrderExpression = orderBy;
                procePageInfo.whereString = where;
                reqeFunStruct.FunObject = procePageInfo;

                UserIDOP userOp = new UserIDOP() { UserID = GlobalStaticObj.UserID, OPName = opName };
                reqeFunStruct.userIDOP = userOp;

                //reqeFunStruct.userIDOP.UserID = GlobalStaticObj.UserID;
                //reqeFunStruct.userIDOP.OPName = opName;
                reqeFunStruct.PCClientCookieStr = GlobalStaticObj.CookieStr;

                var addStr = HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(reqeFunStruct));
                var respO = JsonOperate(addStr);
                if (respO.IsSuccess == "0")
                {
                    throw new Exception(respO.Msg);
                }
                if (respO.ReturnObject == null)
                {
                    throw new Exception("返回数据异常:返回数据对象为空！");
                }
                else
                {
                    DataSet ds = JsonConvert.DeserializeObject<DataSet>(respO.ReturnObject);
                    if (ds == null || ds.Tables.Count == 0)
                    {
                        throw new Exception("查询异常:数据表不存在！");
                    }
                    return ds.Tables[0];
                }               
            }
            catch (Exception ex)
            {
                JsonOperateLogService.WriteLog(ex);
                recordCount = 0;
                return null;
            }
        }

        /// <summary> 根据主键值，单表删除多记录，In操作
        /// </summary>
        /// <param name="opName">操作名</param>
        /// <param name="accCode">帐套代码</param>
        /// <param name="sqlObjList">sql对象</param>
        /// <returns></returns>
        public static bool BatchUpdateDataByIn(string opName, string accCode, string tableName, Dictionary<string, string> dicParam, string pkName, string[] pkValues)
        {
            ReqeFunStruct reqeFunStruct = new ReqeFunStruct();
            reqeFunStruct.SubSysName = SubSysName.CommonBaseFuncCall;
            reqeFunStruct.FunName = ComFunCallEnum.BatchUpdateDataByIn;
            reqeFunStruct.AccCode = accCode;
            reqeFunStruct.userIDOP.UserID = GlobalStaticObj.UserID;
            reqeFunStruct.userIDOP.OPName = opName;
            reqeFunStruct.PCClientCookieStr = GlobalStaticObj.CookieStr;

            BatchUpdateDataByIn batchUpdateDataByIn = new BatchUpdateDataByIn();
            batchUpdateDataByIn.TableName = tableName;
            batchUpdateDataByIn.DicParam = dicParam;
            batchUpdateDataByIn.pkName = pkName;
            batchUpdateDataByIn.pkValues = pkValues;
            reqeFunStruct.FunObject = batchUpdateDataByIn;

            var addStr = HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(reqeFunStruct));
            var respO = JsonOperate(addStr);
            return respO.IsSuccess == "1";
        }

        /// <summary> 事务批量执行
        /// </summary>
        /// <param name="opName">操作名</param>
        /// <param name="accCode">帐套代码</param>
        /// <param name="sqlObjList">批量sql</param>
        /// <returns></returns>
        public static bool BatchExeSQLMultiByTrans(string opName, string accCode, IList<SQLObj> sqlObjList)
        {
            ReqeFunStruct reqeFunStruct = new ReqeFunStruct();
            reqeFunStruct.SubSysName = SubSysName.CommonBaseFuncCall;
            reqeFunStruct.FunName = ComFunCallEnum.BatchExeSQLMultiByTrans;
            reqeFunStruct.AccCode = accCode;
            reqeFunStruct.userIDOP.UserID = GlobalStaticObj.UserID;
            reqeFunStruct.userIDOP.OPName = opName;
            reqeFunStruct.PCClientCookieStr = GlobalStaticObj.CookieStr;

            BatchExeSQLMultiByTrans batchExeSQLMultiByTrans = new BatchExeSQLMultiByTrans();
            batchExeSQLMultiByTrans.batSQLObjList = sqlObjList;
            reqeFunStruct.FunObject = batchExeSQLMultiByTrans;

            var addStr = HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(reqeFunStruct));
            var respO = JsonOperate(addStr); ;
            return respO.IsSuccess == "1";
        }
        /// <summary> 事务批量执行（List<string>）
        /// 创建人：唐春奎
        /// </summary>
        /// <param name="opName">操作名</param>
        /// <param name="accCode">帐套代码</param>
        /// <param name="sqlStringList">批量sql</param>
        /// <returns></returns>
        public static bool BatchExeSQLStringMultiByTrans(string opName, string accCode, IList<SysSQLString> sqlStringList)
        {
            try
            {
                ReqeFunStruct reqeFunStruct = new ReqeFunStruct();
                reqeFunStruct.SubSysName = SubSysName.CommonBaseFuncCall;
                reqeFunStruct.FunName = ComFunCallEnum.BatchExeSQLStringMultiByTrans;
                reqeFunStruct.AccCode = accCode;
                reqeFunStruct.userIDOP.UserID = GlobalStaticObj.UserID;
                reqeFunStruct.userIDOP.OPName = opName;
                reqeFunStruct.PCClientCookieStr = GlobalStaticObj.CookieStr;

                BatchExeSQLStringMultiByTrans batchExeSQLStringByTrans = new BatchExeSQLStringMultiByTrans();
                batchExeSQLStringByTrans.batSQLStringList = sqlStringList;
                reqeFunStruct.FunObject = batchExeSQLStringByTrans;

                var addStr = HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(reqeFunStruct));
                var respO = JsonOperate(addStr);
                return respO.ReturnObject == "True";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary> 根据条件删除指定表中的数据
        /// 创建人：唐春奎
        /// </summary>
        /// <param name="opName">操作名</param>
        /// <param name="accCode">帐套代码</param>
        /// <param name="tableName">表名</param>
        /// <param name="strWhere">执行条件，不包含where</param>
        /// <returns></returns>
        public static bool BatchDeleteDataByWhere(string opName, string accCode, string tableName, string strWhere)
        {
            ReqeFunStruct reqeFunStruct = new ReqeFunStruct();
            reqeFunStruct.SubSysName = SubSysName.CommonBaseFuncCall;
            reqeFunStruct.FunName = ComFunCallEnum.BatchDeleteDataByWhere;
            reqeFunStruct.AccCode = accCode;
            reqeFunStruct.userIDOP.UserID = GlobalStaticObj.UserID;
            reqeFunStruct.userIDOP.OPName = opName;
            reqeFunStruct.PCClientCookieStr = GlobalStaticObj.CookieStr;

            BatchDeleteDataByWhere batchDeleteDataByWhere = new BatchDeleteDataByWhere();
            batchDeleteDataByWhere.TableName = tableName;
            batchDeleteDataByWhere.whereString = strWhere;
            reqeFunStruct.FunObject = batchDeleteDataByWhere;

            var deleteStr = HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(reqeFunStruct));
            var respO = JsonOperate(deleteStr);
            return respO.IsSuccess == "1";
            //return respO.ReturnObject == "True";
        }
        #endregion
    }
}
