
namespace SYSModel
{
    /// <summary>
    /// 系统数据类型
    /// </summary>
    public enum SysDbType { VarChar, NVarChar, DateTime, Date, Int, BigInt, Double, Float, SmallInt, TinyInt, Long, Decimal, numerical, VarCharMax, NVarCharMax, Char, NChar, Blob, Bool, Image, Binary }
    /// <summary>
    /// 参数方向
    /// </summary>
    public enum ParamDirection { InPut, OutPut, InputOutput, ReturnValue }
    /// <summary>
    /// 通用基础方法调用枚举
    /// </summary>
    public enum ComFunCallEnum
    {
        /// <summary> 
        /// 根据主键值，单表添加或者更新，给定主键为Update,缺失为Insert操作  
        /// </summary>
        Submit_AddOrEdit,
        /// <summary>
        /// 根据主键值，单表删除单记录
        /// </summary>
        DeleteDataByID,
        /// <summary>
        /// 根据主键值，单表删除多记录，In操作
        /// </summary>
        BatchDeleteDataByIn,
        /// <summary>
        /// 根据Where条件，单表删除多记录
        /// </summary>
        BatchDeleteDataByWhere,
        /// <summary>
        /// 根据主键值，单表更新多记录，In操作
        /// </summary>
        BatchUpdateDataByIn,
        /// <summary>
        /// 根据Where条件，单表更新多记录
        /// </summary>
        BatchUpdateDataByWhere,
        /// <summary>
        /// 批处理更新操作，普通型
        /// </summary>
        BatchUpdateDataMulti,
        /// <summary>
        /// 批处理更新操作，事务型
        /// </summary>
        BatchUpdateDataMultiByTrans,
        /// <summary>
        /// 一次执行(一次连接，多次执行多条语句，多参数多次添加)多条语句，返回bool;;参数为SQLObj.
        /// </summary>
        BatchExeSQLMultiByTrans,
         /// <summary>
        /// 一次执行(一次连接，多次执行多条语句，多参数多次添加)多条语句(不用SQL)，返回bool;参数为SQLString.
        /// </summary>
        BatchExeSQLStringMultiByTrans,
        /// <summary>
        /// 一次执行(一次连接，一次执行多条语句，多参数一次添加)多条语句，返回bool
        /// </summary>
        ExecuteNonQueryReturnBoolByTrans,
        /// <summary>
        /// 一次执行(一次连接，一次执行多条语句)多条语句(不带事务)，返回bool。
        /// </summary>
        ExecuteNonQueryReturnBoolNoTrans,
        /// <summary>
        /// 一次执行(一次连接，一次执行多条语句)多条语句(带事务，不带日志，不带操作记录备份)，返回bool。
        /// </summary>
        BatchExeSQLStrMultiByTransNoLogNoBackup,
        /// <summary>
        /// 一次执行(一次连接，一次执行多条语句)多条语句(不带事务)，返回影响行数Int。
        /// </summary>
        ExecuteNonQueryReturnIntNoTrans,
        /// <summary>
        /// 一次执行(一次连接，一次执行多条语句)多条语句(不带事务)，返回Object。
        /// </summary>
        ExecuteNonQueryReturnObjectNoTrans,
        /// <summary>
        /// 一次执行(一次连接，一次执行多条语句)多条语句(不带事务)，返回DataSet。
        /// </summary>
        ExecuteNonQueryReturnDataSetNoTrans,
         /// <summary>
        /// SQLBulk方式操作带事务，无日志记录，无操作备份
        /// </summary>
        SqlBulkByTransNoLogNoBackUp,
        /// <summary>
        /// 根据查询条件，查单表，重点是Where条件，拼接SQL
        /// </summary>
        SelOPSingleTable,
        /// <summary>
        /// 根据查询条件，查单表，重点是Where条件，拼接SQL,无日志
        /// </summary>
        SelOPSingleTableNoLog,
        /// <summary>
        /// 根据查询条件，查单表，查前几条，重点是Where条件，拼接SQL
        /// </summary>
        SelTopOPSingleTable,
        /// <summary>
        ///根据通用的分页存储过程，分页查询GetListPageStoreProcedure(page, pageSize, tableSource, whereValue, OrderExpression, Fields, ref Counts); 
        /// </summary>   
        SelOPByStoreProcePage,
        /// <summary> WebServ方法调用
        /// </summary>
        WebServHandle,
        /// <summary> 
        /// 记录菜单功能日志
        /// </summary>
        LogFunctionCall
    }   

    /// <summary> WebService接口方法
    /// </summary>
    public enum EnumWebServFunName
    {
        /// <summary> 上传联系人
        /// 参数联系人实体：tb_contacts_ex
        /// </summary>
        UpLoadCcontact = 0,
        /// <summary> 上传车辆客户
        /// 参数客户实体实体：tb_customer
        /// </summary>
        UpLoadCustomer,
        /// <summary>上传服务站库存
        /// 参数库存键值集合：Dictionary<string, int>
        /// </summary>
        UpLoadSercicePartStock,
        /// <summary>配件销售开单
        /// 参数单据实体：partSale
        /// </summary>
        UpLoadPartSale,
        /// <summary>上传三包服务单
        /// 参数三包服务单单据实体：serviceorder
        /// </summary>
        UpLoadServiceOrder,
        /// <summary>上传维修单
        /// 参数维修单单单据实体：repairbill
        /// </summary>
        UpLoadRepairBill,
        /// <summary>
        /// 配件采购单--创建/更新
        /// 参数宇通采购单据实体：partsPurchase
        /// </summary>
        UpLoadPartPurchase,
        /// <summary>
        /// 配件入库单入库
        /// 参数[DSN配送单号,入库状态]:rationSendCode
        /// </summary>
        UpLoadPartPutStore,
        /// <summary>
        /// 三包服务单状态查询-CRM
        /// 参数CRM服务单号:billNum
        /// </summary>
        LoadOrderStatus,
        /// <summary>
        /// 配件入库单查询
        /// 参数[配送单号,入库状态]:ration_send_code
        /// </summary>
        LoadPartInStore,
        /// <summary>
        /// 配件采购单状态查询
        /// 参数采购订单号：crm_bill_id
        /// </summary>
        LoadPartPurchaseStauts,
        /// <summary>
        /// 宇通中心站-DSN 库存查询
        /// </summary>
        LoadStoreCenter,
        /// <summary>
        ///维修结算单查询
        ///参数：结算单号
        /// </summary>
        QuerySettleAccounts,
        /// <summary>
        /// 维修结算单状态查询
        /// 参数结算单号：settlement_no
        /// </summary>
        LoadServiceSettleStatus,
        /// <summary>
        /// 旧件回收-创建
        /// 参数：周期（开始周期，结束周期以“,”分开）
        /// </summary>
        UpPartReturnCreate,
        /// <summary>
        /// 旧件回收--状态查询
        /// 参数旧件返回单号：old_bill_num
        /// </summary>
        LoadPartRetureStatus,
        /// <summary>
        /// 旧件回收--更新
        /// 参数旧件回收实体：partReturn
        /// </summary>
        UpPartRetureUpdate,
        /// <summary>
        /// 宇通用户
        /// 返回List<ListItem>
        /// </summary>
        SearchContact,
        /// <summary>
        /// 查询三包服务单状态
        /// 参数：CRM服务单号
        /// 返回：单据状态编码
        /// </summary>
        SearchOrderStatus,
        /// <summary>
        /// 确认三包结算单
        /// 参数：settlement_no维修单号
        /// </summary>
        UpLoadServiceSettleStatus

    }

    /// <summary>
    /// 子系统
    /// </summary>
    public enum SubSysName { CommonBaseFuncCall, ExtendBaseFuncCall,WebServ }
}
