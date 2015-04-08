using System.Collections.Generic;
using SYSModel;
using Newtonsoft.Json;
using System;
using System.Data;
using HXC_FuncUtility;
namespace HuiXiuCheWcfService
{
    public class CommonBaseFuncCall
    {
        public static RespFunStruct ComFunCall(ReqeFunStruct reqeObj)
        {
            RespFunStruct resp = new RespFunStruct();
            bool result = false;
            int exeCount = 0;
            UserIDOP userID = reqeObj.userIDOP;
            switch (reqeObj.FunName)
            {
                //根据主键值，单表添加或者更新，给定主键为Update,缺失为Insert操作                    
                case ComFunCallEnum.Submit_AddOrEdit:
                    Submit_AddOrEdit add = JsonConvert.DeserializeObject<Submit_AddOrEdit>(reqeObj.FunObject.ToString());
                    try
                    {
                        string connString = LocalVariable.GetConnString(GlobalStaticObj_Server.Instance.CurrAccDbName, ConfigConst.ConnectionStringWrite);
                        result = BLL.DbCommon.Instance.Submit_AddOrEdit(connString, add.TableName, add.pkName, add.pkVal, (Dictionary<string, string>)add.DicParam, userID);
                        resp.IsSuccess = "1";
                        resp.ReturnObject = result.ToString();
                    }
                    catch (Exception ex)
                    {
                        resp.IsSuccess = "0";
                        resp.Msg = "Submit_AddOrEdit error:" + ex.Message;
                        ex.Data.Add("UserCustomDefinitionMsg", "准确点错误描述：ComFunCallEnum.Submit_AddOrEdit;Submit_AddOrEdit对象JSON字串描述：" + reqeObj.FunObject.ToString());
                        throw ex;
                    }
                    break;
                //根据主键值，单表删除单记录
                case ComFunCallEnum.DeleteDataByID:
                    DeleteDataByID del = JsonConvert.DeserializeObject<DeleteDataByID>(reqeObj.FunObject.ToString());
                    try
                    {
                        string connString = LocalVariable.GetConnString(GlobalStaticObj_Server.Instance.CurrAccDbName, ConfigConst.ConnectionStringWrite);
                        result = BLL.DbCommon.Instance.DeleteDataByID(connString, del.TableName, del.pkName, del.pkVal, userID);
                        resp.IsSuccess = "1";
                        resp.ReturnObject = result.ToString();
                    }
                    catch (Exception ex)
                    {
                        resp.IsSuccess = "0";
                        resp.Msg = "DeleteDataByID  error:" + ex.Message;
                        ex.Data.Add("UserCustomDefinitionMsg", "准确点错误描述：ComFunCallEnum.DeleteDataByID;DeleteDataByID对象JSON字串描述：" + reqeObj.FunObject.ToString());
                        throw ex;
                    }
                    break;
                //根据主键值，单表删除多记录，In操作
                case ComFunCallEnum.BatchDeleteDataByIn:
                    BatchDeleteDataByIn delBat = JsonConvert.DeserializeObject<BatchDeleteDataByIn>(reqeObj.FunObject.ToString());
                    try
                    {
                        string connString = LocalVariable.GetConnString(GlobalStaticObj_Server.Instance.CurrAccDbName, ConfigConst.ConnectionStringWrite);
                        exeCount = BLL.DbCommon.Instance.BatchDeleteDataByIn(connString, delBat.TableName, delBat.pkName, delBat.pkValues, userID);
                        resp.IsSuccess = "1";
                        resp.ReturnObject = exeCount.ToString();
                    }
                    catch (Exception ex)
                    {
                        resp.IsSuccess = "0";
                        resp.Msg = "BatchDeleteDataByIn  error:" + ex.Message;
                        ex.Data.Add("UserCustomDefinitionMsg", "准确点错误描述：ComFunCallEnum.BatchDeleteDataByIn;BatchDeleteDataByIn对象JSON字串描述：" + reqeObj.FunObject.ToString());
                        throw ex;
                    }
                    break;
                //根据Where条件，单表删除多记录
                case ComFunCallEnum.BatchDeleteDataByWhere:
                    BatchDeleteDataByWhere delWhere = JsonConvert.DeserializeObject<BatchDeleteDataByWhere>(reqeObj.FunObject.ToString());
                    try
                    {
                        string connString = LocalVariable.GetConnString(GlobalStaticObj_Server.Instance.CurrAccDbName, ConfigConst.ConnectionStringWrite);
                        exeCount = BLL.DbCommon.Instance.BatchDeleteDataByWhere(connString, delWhere.TableName, delWhere.whereString, userID);
                        resp.IsSuccess = "1";
                        resp.ReturnObject = exeCount.ToString();
                    }
                    catch (Exception ex)
                    {
                        resp.IsSuccess = "0";
                        resp.Msg = "BatchDeleteDataByWhere  error:" + ex.Message;
                        ex.Data.Add("UserCustomDefinitionMsg", "准确点错误描述：ComFunCallEnum.BatchDeleteDataByWhere;BatchDeleteDataByWhere对象JSON字串描述：" + reqeObj.FunObject.ToString());
                        throw ex;
                    }
                    break;
                //根据主键值，单表更新多记录，In操作
                case ComFunCallEnum.BatchUpdateDataByIn:
                    BatchUpdateDataByIn updaByIn = JsonConvert.DeserializeObject<BatchUpdateDataByIn>(reqeObj.FunObject.ToString());
                    try
                    {
                        string connString = LocalVariable.GetConnString(GlobalStaticObj_Server.Instance.CurrAccDbName, ConfigConst.ConnectionStringWrite);
                        exeCount = BLL.DbCommon.Instance.BatchUpdateDataByIn(connString, updaByIn.TableName, (Dictionary<string, string>)updaByIn.DicParam, updaByIn.pkName, updaByIn.pkValues, userID);
                        resp.IsSuccess = "1";
                        resp.ReturnObject = exeCount.ToString();
                    }
                    catch (Exception ex)
                    {
                        resp.IsSuccess = "0";
                        resp.Msg = "BatchUpdateDataByIn  error:" + ex.Message;
                        ex.Data.Add("UserCustomDefinitionMsg", "准确点错误描述：ComFunCallEnum.BatchUpdateDataByIn;BatchUpdateDataByIn对象JSON字串描述：" + reqeObj.FunObject.ToString());
                        throw ex;
                    }
                    break;
                // 根据Where条件，单表更新多记录
                case ComFunCallEnum.BatchUpdateDataByWhere:
                    BatchUpdateDataByWhere updaByWhere = JsonConvert.DeserializeObject<BatchUpdateDataByWhere>(reqeObj.FunObject.ToString());
                    try
                    {
                        string connString = LocalVariable.GetConnString(GlobalStaticObj_Server.Instance.CurrAccDbName, ConfigConst.ConnectionStringWrite);
                        exeCount = BLL.DbCommon.Instance.BatchUpdateDataByWhere(connString, updaByWhere.TableName, (Dictionary<string, string>)updaByWhere.DicParam, updaByWhere.whereString, userID);
                        resp.IsSuccess = "1";
                        resp.ReturnObject = exeCount.ToString();
                    }
                    catch (Exception ex)
                    {
                        resp.IsSuccess = "0";
                        resp.Msg = "BatchUpdateDataByWhere  error:" + ex.Message;
                        ex.Data.Add("UserCustomDefinitionMsg", "准确点错误描述：ComFunCallEnum.BatchUpdateDataByWhere;BatchUpdateDataByWhere对象JSON字串描述：" + reqeObj.FunObject.ToString());
                        throw ex;
                    }
                    break;
                // 批处理更新操作，普通型
                case ComFunCallEnum.BatchUpdateDataMulti:
                    BatchUpdateDataMulti updaMulti = JsonConvert.DeserializeObject<BatchUpdateDataMulti>(reqeObj.FunObject.ToString());
                    try
                    {
                        string connString = LocalVariable.GetConnString(GlobalStaticObj_Server.Instance.CurrAccDbName, ConfigConst.ConnectionStringWrite);
                        exeCount = BLL.DbCommon.Instance.BatchUpdateDataMulti(connString, updaMulti.batUpdateList, userID);
                        resp.IsSuccess = "1";
                        resp.ReturnObject = exeCount.ToString();
                    }
                    catch (Exception ex)
                    {
                        resp.IsSuccess = "0";
                        resp.Msg = "BatchUpdateDataMulti  error:" + ex.Message;
                        ex.Data.Add("UserCustomDefinitionMsg", "准确点错误描述：ComFunCallEnum.BatchUpdateDataMulti;BatchUpdateDataMulti对象JSON字串描述：" + reqeObj.FunObject.ToString());
                        throw ex;
                    }
                    break;
                // 批处理更新操作，事务型
                case ComFunCallEnum.BatchUpdateDataMultiByTrans:
                    BatchUpdateDataMultiByTrans updaMultiByTrans = JsonConvert.DeserializeObject<BatchUpdateDataMultiByTrans>(reqeObj.FunObject.ToString());
                    try
                    {
                        string connString = LocalVariable.GetConnString(GlobalStaticObj_Server.Instance.CurrAccDbName, ConfigConst.ConnectionStringWrite);
                        exeCount = BLL.DbCommon.Instance.BatchUpdateDataMultiByTrans(connString, updaMultiByTrans.batUpdateList, userID);
                        resp.IsSuccess = "1";
                        resp.ReturnObject = exeCount.ToString();
                    }
                    catch (Exception ex)
                    {
                        resp.IsSuccess = "0";
                        resp.Msg = "BatchUpdateDataMultiByTrans  error:" + ex.Message;
                        ex.Data.Add("UserCustomDefinitionMsg", "准确点错误描述：ComFunCallEnum.BatchUpdateDataMultiByTrans;BatchUpdateDataMultiByTrans对象JSON字串描述：" + reqeObj.FunObject.ToString());
                        throw ex;
                    }
                    break;
                //一次执行(一次连接，多次执行多条语句，多参数多次添加)多条语句，返回bool;参数为SQLObj.
                case ComFunCallEnum.BatchExeSQLMultiByTrans:
                    BatchExeSQLMultiByTrans BatchExeSQLMultiByTrans = JsonConvert.DeserializeObject<BatchExeSQLMultiByTrans>(reqeObj.FunObject.ToString());
                    try
                    {
                        string connString = LocalVariable.GetConnString(GlobalStaticObj_Server.Instance.CurrAccDbName, ConfigConst.ConnectionStringWrite);
                        result = BLL.DbCommon.Instance.BatchExeSQLMultiByTrans(connString, BatchExeSQLMultiByTrans.batSQLObjList, userID);
                        resp.IsSuccess = "1";
                        resp.ReturnObject = result.ToString();
                    }
                    catch (Exception ex)
                    {
                        resp.IsSuccess = "0";
                        resp.Msg = "BatchExeSQLMultiByTrans  error:" + ex.Message;
                        ex.Data.Add("UserCustomDefinitionMsg", "准确点错误描述：ComFunCallEnum.BatchExeSQLMultiByTrans;BatchExeSQLMultiByTrans对象JSON字串描述：" + reqeObj.FunObject.ToString());
                        throw ex;
                    }
                    break;
                //一次执行(一次连接，多次执行多条语句，多参数多次添加)多条语句，返回bool;参数为SQLString.
                case ComFunCallEnum.BatchExeSQLStringMultiByTrans:
                    BatchExeSQLStringMultiByTrans BatchExeSQLStringMultiByTrans = JsonConvert.DeserializeObject<BatchExeSQLStringMultiByTrans>(reqeObj.FunObject.ToString());
                    try
                    {
                        string connString = LocalVariable.GetConnString(GlobalStaticObj_Server.Instance.CurrAccDbName, ConfigConst.ConnectionStringWrite);
                        result = BLL.DbCommon.Instance.BatchExeSQLMultiByTrans(connString, BatchExeSQLStringMultiByTrans.batSQLStringList, userID);
                        resp.IsSuccess = "1";
                        resp.ReturnObject = result.ToString();
                    }
                    catch (Exception ex)
                    {
                        resp.IsSuccess = "0";
                        resp.Msg = "BatchExeSQLMultiByTrans  error:" + ex.Message;
                        ex.Data.Add("UserCustomDefinitionMsg", "准确点错误描述：ComFunCallEnum.BatchExeSQLStringMultiByTrans;BatchExeSQLStringMultiByTrans对象JSON字串描述：" + reqeObj.FunObject.ToString());
                        throw ex;
                    }
                    break;
                //一次执行(一次连接，一次执行多条语句，多参数一次添加)多条语句，返回bool
                case ComFunCallEnum.ExecuteNonQueryReturnBoolByTrans:
                    ExecuteNonQueryReturnBoolByTrans ExeSQLMultiByTrans = JsonConvert.DeserializeObject<ExecuteNonQueryReturnBoolByTrans>(reqeObj.FunObject.ToString());
                    try
                    {
                        string connString = LocalVariable.GetConnString(GlobalStaticObj_Server.Instance.CurrAccDbName, ConfigConst.ConnectionStringWrite);
                        result = BLL.DbCommon.Instance.ExecuteNonQueryReturnBoolByTrans(connString, ExeSQLMultiByTrans.sqlObj.cmdType, ExeSQLMultiByTrans.sqlObj.sqlString, ExeSQLMultiByTrans.sqlObj.Param, userID);
                        resp.IsSuccess = "1";
                        resp.ReturnObject = result.ToString();
                    }
                    catch (Exception ex)
                    {
                        resp.IsSuccess = "0";
                        resp.Msg = "ExecuteNonQueryReturnBoolByTrans  error:" + ex.Message;
                        ex.Data.Add("UserCustomDefinitionMsg", "准确点错误描述：ComFunCallEnum.ExecuteNonQueryReturnBoolByTrans;ExecuteNonQueryReturnBoolByTrans对象JSON字串描述：" + reqeObj.FunObject.ToString());
                        throw ex;
                    }
                    break;
                //一次执行(一次连接，多次执行多条语句，没有参数)多条语句带事务(无备份操作记录功能,无SQL日志操作记录功能)，返回带事务的bool执行结果
                case ComFunCallEnum.BatchExeSQLStrMultiByTransNoLogNoBackup:
                    BatchExeSQLStringMultiByTrans BatchExeSQLStringMultiByTransNoLog = JsonConvert.DeserializeObject<BatchExeSQLStringMultiByTrans>(reqeObj.FunObject.ToString());
                    try
                    {
                        string connString = LocalVariable.GetConnString(GlobalStaticObj_Server.Instance.CurrAccDbName, ConfigConst.ConnectionStringWrite);
                        result = BLL.DbCommon.Instance.BatchExeSQLStrMultiByTransNoLogNoBackup(connString, BatchExeSQLStringMultiByTransNoLog.batSQLStringList, userID);
                        resp.IsSuccess = "1";
                        resp.ReturnObject = result.ToString();
                    }
                    catch (Exception ex)
                    {
                        resp.IsSuccess = "0";
                        resp.Msg = "BatchExeSQLStrMultiByTransNoLogNoBackup  error:" + ex.Message;
                        ex.Data.Add("UserCustomDefinitionMsg", "准确点错误描述：ComFunCallEnum.BatchExeSQLStrMultiByTransNoLogNoBackup;BatchExeSQLStringMultiByTrans对象JSON字串描述：" + reqeObj.FunObject.ToString());
                        throw ex;
                    }
                    break;
                //一次执行(一次连接，一次执行多条语句)多条语句(不带事务)，返回bool。
                case ComFunCallEnum.ExecuteNonQueryReturnBoolNoTrans:
                    ExecuteNonQueryReturnBoolByTrans ExeSQLReturnBoolNoTrans = JsonConvert.DeserializeObject<ExecuteNonQueryReturnBoolByTrans>(reqeObj.FunObject.ToString());
                    try
                    {
                        string connString = LocalVariable.GetConnString(GlobalStaticObj_Server.Instance.CurrAccDbName, ConfigConst.ConnectionStringWrite);
                        result = BLL.DbCommon.Instance.ExecuteNonQueryReturnBoolNoTrans(connString, ExeSQLReturnBoolNoTrans.sqlObj.cmdType, ExeSQLReturnBoolNoTrans.sqlObj.sqlString, ExeSQLReturnBoolNoTrans.sqlObj.Param, userID);
                        resp.IsSuccess = "1";
                        resp.ReturnObject = result.ToString();
                    }
                    catch (Exception ex)
                    {
                        resp.IsSuccess = "0";
                        resp.Msg = "ExecuteNonQueryReturnBoolNoTrans  error:" + ex.Message;
                        ex.Data.Add("UserCustomDefinitionMsg", "准确点错误描述：ComFunCallEnum.ExecuteNonQueryReturnBoolNoTrans;ExecuteNonQueryReturnBoolNoTrans对象JSON字串描述：" + reqeObj.FunObject.ToString());
                        throw ex;
                    }
                    break;
                //一次执行(一次连接，一次执行多条语句)多条语句(不带事务)，返回影响行数Int。
                case ComFunCallEnum.ExecuteNonQueryReturnIntNoTrans:
                    ExecuteNonQueryReturnIntNoTrans ExeSQLReturnIntNoTrans = JsonConvert.DeserializeObject<ExecuteNonQueryReturnIntNoTrans>(reqeObj.FunObject.ToString());
                    try
                    {
                        string connString = LocalVariable.GetConnString(GlobalStaticObj_Server.Instance.CurrAccDbName, ConfigConst.ConnectionStringWrite);
                        exeCount = BLL.DbCommon.Instance.ExecuteNonQueryReturnIntNoTrans(connString, ExeSQLReturnIntNoTrans.sqlObj.cmdType, ExeSQLReturnIntNoTrans.sqlObj.sqlString, ExeSQLReturnIntNoTrans.sqlObj.Param, userID);
                        resp.IsSuccess = "1";
                        resp.ReturnObject = exeCount.ToString();
                    }
                    catch (Exception ex)
                    {
                        resp.IsSuccess = "0";
                        resp.Msg = "ExecuteNonQueryReturnIntNoTrans  error:" + ex.Message;
                        ex.Data.Add("UserCustomDefinitionMsg", "准确点错误描述：ComFunCallEnum.ExecuteNonQueryReturnIntNoTrans;ExecuteNonQueryReturnIntNoTrans对象JSON字串描述：" + reqeObj.FunObject.ToString());
                        throw ex;
                    }
                    break;
                //一次执行(一次连接，一次执行多条语句)多条语句(不带事务)，返回Object。
                case ComFunCallEnum.ExecuteNonQueryReturnObjectNoTrans:
                    ExecuteNonQueryReturnDataSetNoTrans ExeSQLReturnObjNoTrans = JsonConvert.DeserializeObject<ExecuteNonQueryReturnDataSetNoTrans>(reqeObj.FunObject.ToString());
                    Object obj;
                    try
                    {
                        string connString = LocalVariable.GetConnString(GlobalStaticObj_Server.Instance.CurrAccDbName, ConfigConst.ConnectionStringWrite);
                        obj = BLL.DbCommon.Instance.ExecuteNonQueryReturnObjectNoTrans(connString, ExeSQLReturnObjNoTrans.sqlObj.cmdType, ExeSQLReturnObjNoTrans.sqlObj.sqlString, ExeSQLReturnObjNoTrans.sqlObj.Param, userID);
                        resp.IsSuccess = "1";
                        resp.ReturnObject = obj == null ? "" : obj.ToString();
                    }
                    catch (Exception ex)
                    {
                        resp.IsSuccess = "0";
                        resp.Msg = "ExecuteNonQueryReturnObjectNoTrans  error:" + ex.Message;
                        ex.Data.Add("UserCustomDefinitionMsg", "准确点错误描述：ComFunCallEnum.ExecuteNonQueryReturnObjectNoTrans;ExecuteNonQueryReturnObjectNoTrans对象JSON字串描述：" + reqeObj.FunObject.ToString());
                        throw ex;
                    }
                    break;
                //一次执行(一次连接，一次执行多条语句)多条语句(不带事务)，返回DataSet。
                case ComFunCallEnum.ExecuteNonQueryReturnDataSetNoTrans:
                    ExecuteNonQueryReturnDataSetNoTrans ExeSQLReturnDSNoTrans = JsonConvert.DeserializeObject<ExecuteNonQueryReturnDataSetNoTrans>(reqeObj.FunObject.ToString());
                    DataSet ds = new DataSet();
                    try
                    {
                        string connString = LocalVariable.GetConnString(GlobalStaticObj_Server.Instance.CurrAccDbName, ConfigConst.ConnectionStringReadonly);
                        ds = BLL.DbCommon.Instance.ExecuteNonQueryReturnDataSetNoTrans(connString, ExeSQLReturnDSNoTrans.sqlObj.cmdType, ExeSQLReturnDSNoTrans.sqlObj.sqlString, ExeSQLReturnDSNoTrans.sqlObj.Param, userID);
                        resp.IsSuccess = "1";
                        resp.ReturnObject = JsonConvert.SerializeObject(ds);
                    }
                    catch (Exception ex)
                    {
                        resp.IsSuccess = "0";
                        resp.Msg = "ExecuteNonQueryReturnDataSetNoTrans  error:" + ex.Message;
                        ex.Data.Add("UserCustomDefinitionMsg", "准确点错误描述：ComFunCallEnum.ExecuteNonQueryReturnDataSetNoTrans;ExecuteNonQueryReturnDataSetNoTrans对象JSON字串描述：" + reqeObj.FunObject.ToString());
                        throw ex;
                    }
                    break;
                //根据查询条件，查单表，重点是Where条件，拼接SQL
                case ComFunCallEnum.SelOPSingleTable:
                    SelOPSingleTable selOpSingTable = JsonConvert.DeserializeObject<SelOPSingleTable>(reqeObj.FunObject.ToString());
                    DataSet ds1 = new DataSet();
                    try
                    {
                        string connString = LocalVariable.GetConnString(GlobalStaticObj_Server.Instance.CurrAccDbName, ConfigConst.ConnectionStringReadonly);
                        ds1 = BLL.DbCommon.Instance.SelOPSingleTable(connString, selOpSingTable.Fields, selOpSingTable.TableName, selOpSingTable.whereString, selOpSingTable.AdditionalConditions, userID);
                        //var jSetting = new JsonSerializerSettings();
                        //jSetting.NullValueHandling = NullValueHandling.Include;
                        //jSetting.DefaultValueHandling = DefaultValueHandling.Include;
                        //jSetting.MissingMemberHandling = MissingMemberHandling.Error;
                        resp.IsSuccess = "1";
                        resp.ReturnObject = JsonConvert.SerializeObject(ds1);
                    }
                    catch (Exception ex)
                    {
                        resp.IsSuccess = "0";
                        resp.Msg = "SelOPSingleTable error:" + ex.Message;
                        ex.Data.Add("UserCustomDefinitionMsg", "准确点错误描述：ComFunCallEnum.SelOPSingleTable;SelOPSingleTable对象JSON字串描述：" + reqeObj.FunObject.ToString());
                        throw ex;
                    }
                    break;
                //根据查询条件，查单表，查前几条，重点是Where条件，拼接SQL
                case ComFunCallEnum.SelTopOPSingleTable:
                    SelTopOPSingleTable selTopOpSingTable = JsonConvert.DeserializeObject<SelTopOPSingleTable>(reqeObj.FunObject.ToString());
                    DataSet ds2 = new DataSet();
                    try
                    {
                        string connString = LocalVariable.GetConnString(GlobalStaticObj_Server.Instance.CurrAccDbName, ConfigConst.ConnectionStringReadonly);
                        ds2 = BLL.DbCommon.Instance.SelTopOPSingleTable(connString, selTopOpSingTable.TopNum, selTopOpSingTable.Fields, selTopOpSingTable.TableName, selTopOpSingTable.whereString, selTopOpSingTable.AdditionalConditions, userID);
                        resp.IsSuccess = "1";
                        resp.ReturnObject = JsonConvert.SerializeObject(ds2);
                    }
                    catch (Exception ex)
                    {
                        resp.IsSuccess = "0";
                        resp.Msg = "SelTopOPSingleTable  error:" + ex.Message;
                        ex.Data.Add("UserCustomDefinitionMsg", "准确点错误描述：ComFunCallEnum.SelTopOPSingleTable;SelTopOPSingleTable对象JSON字串描述：" + reqeObj.FunObject.ToString());
                        throw ex;
                    }
                    break;
                //根据通用的分页存储过程，分页查询GetListPageStoreProcedure(page, pageSize, tableSource, whereValue, OrderExpression, Fields, ref Counts);   
                case ComFunCallEnum.SelOPByStoreProcePage:
                    SelOPByStoreProcePage selOpBySPPage = JsonConvert.DeserializeObject<SelOPByStoreProcePage>(reqeObj.FunObject.ToString());
                    DataSet ds3 = new DataSet();
                    try
                    {
                        string connString = LocalVariable.GetConnString(GlobalStaticObj_Server.Instance.CurrAccDbName, ConfigConst.ConnectionStringReadonly);
                        ds3 = BLL.DbCommon.Instance.GetListPageStoreProcedure(connString, selOpBySPPage.PageIndex, selOpBySPPage.PageSize, selOpBySPPage.TableSource, selOpBySPPage.whereString, selOpBySPPage.OrderExpression, selOpBySPPage.Fields, userID, ref exeCount);
                        resp.IsSuccess = "1";
                        resp.ReturnObject = JsonConvert.SerializeObject(ds3);
                        resp.RecordCount = exeCount;
                    }
                    catch (Exception ex)
                    {
                        resp.IsSuccess = "0";
                        resp.Msg = "SelOPByStoreProcePage  error:" + ex.Message;
                        ex.Data.Add("UserCustomDefinitionMsg", "准确点错误描述：ComFunCallEnum.SelOPByStoreProcePage;SelOPByStoreProcePage对象JSON字串描述：" + reqeObj.FunObject.ToString());
                        throw ex;
                    }
                    break;
                /// <summary>
                /// SQLBulk方式操作带事务，无日志记录，无操作备份
                /// </summary>
                /// <param name="connStr">数据库连接串</param>
                /// <param name="tableName">表名</param>
                /// <param name="listRow">DataRowList集合</param>
                /// <returns>SQLBulk批处理方式操作带事务，执行结果。全部成功，返回Bool True; 否则，返回False。</returns>
                case ComFunCallEnum.SqlBulkByTransNoLogNoBackUp:
                    SqlBulkByTransNoLogNoBackUp SqlBulkNoLogNoBackUp = JsonConvert.DeserializeObject<SqlBulkByTransNoLogNoBackUp>(reqeObj.FunObject.ToString());
                    try
                    {
                        string connString = LocalVariable.GetConnString(GlobalStaticObj_Server.Instance.CurrAccDbName, ConfigConst.ConnectionStringWrite);
                        result = BLL.DbCommon.Instance.SqlBulkByTransNoLogNoBackUp(connString, SqlBulkNoLogNoBackUp.tableName, SqlBulkNoLogNoBackUp.listRow);
                        resp.IsSuccess = "1";
                        resp.ReturnObject = result.ToString();
                    }
                    catch (Exception ex)
                    {
                        resp.IsSuccess = "0";
                        resp.Msg = "SqlBulkByTransNoLogNoBackUp  error:" + ex.Message;
                        ex.Data.Add("UserCustomDefinitionMsg", "准确点错误描述：ComFunCallEnum.SqlBulkByTransNoLogNoBackUp;SqlBulkByTransNoLogNoBackUp对象JSON字串描述：" + reqeObj.FunObject.ToString());
                        throw ex;
                    }
                    break;

            }
            exeCount = 0;
            return resp;
        }
    }
}