using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;
namespace SYSModel
{
    public struct LoginInput
    {
        /// <summary>
        /// 用户登录ID
        /// </summary>
        public string Login_Id;
        /// <summary> 帐套编码
        /// </summary>
        public string acccode;
        /// <summary>
        /// 用户名
        /// </summary>
        public string username;
        /// <summary>
        /// 密码
        /// </summary>
        public string pwd;
        /// <summary>
        /// 计算机名称
        /// </summary>
        public string ComputerName;
        /// <summary>
        /// MAC地址
        /// </summary>
        public string MAC;
    }
    /// <summary>
    /// 请求方法调用结构
    /// </summary>
    public struct ReqeFunStruct
    {
        /// <summary>
        /// 子系统名称
        /// </summary>
        public SubSysName SubSysName;
        /// <summary>
        /// 方法名称
        /// </summary>
        public ComFunCallEnum FunName;
        /// <summary> 帐套代码 
        /// </summary>
        public string AccCode;
        /// <summary>
        /// 方法参数
        /// </summary>
        //public string FunParams;
        /// <summary>
        /// 方法调用对象
        /// </summary>
        public Object FunObject;
        /// <summary>
        /// 用户操作对象类
        /// </summary>
        public UserIDOP userIDOP;
        /// <summary>
        /// PC客户端CookieStr字符串
        /// </summary>
        public string PCClientCookieStr;
    }
    /// <summary>
    /// 请求方法调用结构
    /// </summary>
    public struct ReqeFunStruct_WebServ
    {
        /// <summary> 帐套代码
        /// </summary>
        public string AccCode;
        /// <summary> WebServ方法名称
        /// </summary>
        public EnumWebServFunName FunName;
        /// <summary> 方法参数
        /// </summary>
        public Object FunObject;
        /// <summary>
        /// 用户操作对象类
        /// </summary>
        public UserIDOP userIDOP;
        /// <summary>
        /// PC客户端CookieStr字符串
        /// </summary>
        public string PCClientCookieStr;
    }

    /// <summary>
    /// 用户操作结构
    /// </summary>
    public struct UserIDOP
    {
        /// <summary> 
        /// 登录用户的ID
        /// </summary>
        public string UserID;
        /// <summary>
        /// 当前发生的操作名称
        /// </summary>
        public string OPName;
    }
    /// <summary>
    /// 方法调用返回结构
    /// </summary>
    public struct RespFunStruct
    {
        /// <summary>
        /// 是否成功（1成功 0失败）
        /// </summary>
        public string IsSuccess;
        /// <summary>
        /// 返回的业务方法调用的数据JSON串(需解析转换成相应的对象)
        /// </summary>
        public string ReturnObject;
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg;
        /// <summary>
        /// 总记录条数
        /// </summary>
        public int RecordCount;
        /// <summary>
        /// 服务器端ID
        /// </summary>
        public string ServerID;
        /// <summary>
        /// 服务器端CookieStr字符串
        /// </summary>
        public string WCFServerCookieStr;

        public override string ToString()   //add by kord
        {
            return String.Format("IsSuccess:{0};\tMsg:{1};\tRecordCount:{2}",IsSuccess, Msg, RecordCount);
        }
    }
    /// <summary>
    /// 用户操作日志对象类
    /// </summary>
    public class UserOPLog
    {
        /// <summary>
        /// 用户操作
        /// </summary>
        public UserIDOP userOP { get; set; }
        /// <summary>
        /// 操作执行的SQL字符串
        /// </summary>
        public string sqlStr { get; set; }
        /// <summary>
        /// 操作执行的参数JSON格式字符串
        /// </summary>
        public string sqlParams { get; set; }
        /// <summary>
        /// 执行时刻
        /// </summary>
        public long timeTicks { get; set; }
        /// <summary>
        /// SQL操作执行的结果，True or False
        /// </summary>
        public bool exeResult { get; set; }
    }
    /// <summary>
    /// 用户操作文件对象类(上传/下载的日志记录)
    /// </summary>
    public class UserFileOPLog
    {
        /// <summary>
        /// 用户操作
        /// </summary>
        public UserIDOP userOP { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 开始执行时刻
        /// </summary>
        public long sTimeTicks { get; set; }
        /// <summary>
        /// 结束时刻
        /// </summary>
        public long eTimeTicks { get; set; }
        /// <summary>
        /// 操作文件执行的结果，True or False
        /// </summary>
        public bool exeResult { get; set; }
    }

    /// <summary>
    /// 用户操作菜单功能类
    /// </summary>
    public class UserFunctionOPLog
    {
        /// <summary>
        /// 用户操作
        /// </summary>
        public UserIDOP userOP { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string fun_id { get; set; }       
        /// <summary>
        /// 执行时刻
        /// </summary>
        public long access_time { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        public string com_id { get; set; }      
    }

    /// <summary>
    /// UpdateSQL操作对象
    /// </summary>
    public class UpdateSQLObj : Submit_AddOrEdit
    {
        /// <summary>
        /// where条件
        /// </summary>
        public string whereString { get; set; }
    }
    /// <summary>
    /// 一组事务执行的SQL语句
    /// </summary>
    public class BatchExeSQLMultiByTrans
    {
        public IList<SQLObj> batSQLObjList { get; set; }
    }
    /// <summary>
    /// 一组事务执行的SQL语句
    /// </summary>
    public class BatchExeSQLStringMultiByTrans
    {
        public IList<SysSQLString> batSQLStringList { get; set; }
    }
    /// <summary>
    /// 返回布尔带事务的SQL执行操作
    /// </summary>
    public class ExecuteNonQueryReturnBoolByTrans
    {
        /// <summary>
        /// 只读操作标记
        /// </summary>
        public bool ReadOnlyFlag { get; set; }
        /// <summary>
        /// SQL对象
        /// </summary>
        public SQLObj sqlObj { get; set; }
    }
    /// <summary>
    /// 返回布尔无事务的SQL执行操作
    /// </summary>
    public class ExecuteNonQueryReturnBoolNoTrans : ExecuteNonQueryReturnBoolByTrans
    {

    }
    /// <summary>
    /// 返回Int无事务的SQL执行操作
    /// </summary>
    public class ExecuteNonQueryReturnIntNoTrans : ExecuteNonQueryReturnBoolByTrans
    {

    }
    /// <summary>
    /// 返回DataSet无事务的SQL执行操作
    /// </summary>
    public class ExecuteNonQueryReturnDataSetNoTrans : ExecuteNonQueryReturnBoolByTrans
    {

    }
    /// <summary>
    /// 返回对象无事务的SQL执行操作
    /// </summary>
    public class ExecuteNonQueryReturnObjectNoTrans : ExecuteNonQueryReturnBoolByTrans
    {

    }


    #region 单表类增删改，操作（写）库
    /// <summary>
    /// 根据主键值，单表添加或者更新，给定主键为Update,缺失为Insert操作
    /// </summary>
    public class Submit_AddOrEdit
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 字段(键)名称
        /// </summary>
        public string pkName { get; set; }
        /// <summary>
        /// 字段(键)值
        /// </summary>
        public string pkVal { get; set; }
        /// <summary>
        /// 字段参数字典对象
        /// </summary>
        public Dictionary<string, string> DicParam { get; set; }
    }
    /// <summary>
    /// 根据主键值，单表删除单记录
    /// </summary>
    public class DeleteDataByID
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 字段(键)名称
        /// </summary>
        public string pkName { get; set; }
        /// <summary>
        /// 字段(键)值
        /// </summary>
        public string pkVal { get; set; }
    }
    /// <summary>
    /// 根据主键值，单表删除多记录，In操作
    /// </summary>
    public class BatchDeleteDataByIn
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 字段(键)名称
        /// </summary>
        public string pkName { get; set; }
        /// <summary>
        /// In参数里的参数值数组
        /// </summary>
        public object[] pkValues { get; set; }
    }
    /// <summary>
    /// 根据Where条件，单表删除多记录
    /// </summary>
    public class BatchDeleteDataByWhere
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// where条件
        /// </summary>
        public string whereString { get; set; }
    }
    /// <summary>
    /// 根据主键值，单表更新多记录，In操作
    /// </summary>
    public class BatchUpdateDataByIn
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 字段(键)名称
        /// </summary>
        public string pkName { get; set; }
        /// <summary>
        /// In参数里的参数值数组
        /// </summary>
        public object[] pkValues { get; set; }
        public Dictionary<string, string> DicParam { get; set; }
    }
    //根据Where条件，单表更新多记录
    public class BatchUpdateDataByWhere
    {
        public string TableName { get; set; }
        public Dictionary<string, string> DicParam { get; set; }
        public string whereString { get; set; }
    }
    //批处理更新操作，分为事务型和普通型
    public class BatchUpdateDataMulti
    {
        public IList<UpdateSQLObj> batUpdateList { get; set; }
    }
    public class BatchUpdateDataMultiByTrans : BatchUpdateDataMulti
    {

    }

    #endregion

    #region 单表类查操作，操作（读）库
    //根据查询条件，查单表，重点是Where条件，拼接SQL
    public class SelOPSingleTable
    {
        public List<string> Fields { get; set; }
        public string whereString { get; set; }
        public string TableName { get; set; }
        //附加条件 GROUP BY expression [, ...] ] [ HAVING condition [, ...] ]  [ ORDER BY expression [ ASC | DESC | USING operator ] [, ...] ]
        //这些附加条件拼接好传进去，切记自带关键字。
        public string AdditionalConditions { get; set; }
    }
    //根据查询条件，查单表，查前几条，重点是Where条件，拼接SQL
    public class SelTopOPSingleTable : SelOPSingleTable
    {
        public int TopNum { get; set; }
    }
    //根据通用的分页存储过程，分页查询
    public class SelOPByStoreProcePage
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Fields { get; set; }
        public string whereString { get; set; }
        public string TableSource { get; set; }
        public string WhereValue { get; set; }
        public string OrderExpression { get; set; }
    }
    //根据通用的分页存储过程，分页查询
    public class SqlBulkByTransNoLogNoBackUp
    {
        public string tableName { get; set; }
        public List<DataRow> listRow { get; set; }
    }
    #endregion
}
