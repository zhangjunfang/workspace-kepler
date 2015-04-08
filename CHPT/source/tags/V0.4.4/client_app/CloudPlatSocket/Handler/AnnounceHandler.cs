using System.Collections.Generic;
using CloudPlatSocket.Protocol;
using System.Data;
using BLL;
using HXC_FuncUtility;
using SYSModel;
using Utility.Log;
using System.Threading;

namespace CloudPlatSocket.Handler
{
    /// <summary>
    /// 公告信息
    /// 创建时间：2014.11.12
    /// 修改时间：2014.11.12
    /// </summary>
    public class AnnounceHandler
    {
        #region --成员变量
        /// <summary>
        /// 公告子消息ID
        /// </summary>
        private static string SubMessageId = "S1_1";
        /// <summary>
        /// 公告表
        /// </summary>
        private static string TableName = "sys_announcement";
        #endregion

        #region --构造函数
        public AnnounceHandler()
        {
            
        }
        #endregion

        #region --私有方法
        /// <summary> 获取上传协议 
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="dbName">帐套</param>
        /// <returns></returns>
        private static List<AnnounceProtocol> GetUploadDataProtocol(DataTable dt,string dbName)
        {
            List<AnnounceProtocol> protocols = new List<AnnounceProtocol>();
            if (dt != null)
            {
                AnnounceProtocol protocol = null;
                foreach (DataRow dr in dt.Rows)
                {
                    protocol = GetProtocol(dr,dbName);
                    if (protocol != null)
                    {
                        protocols.Add(protocol);
                    }
                }
            }
            return protocols;
        }
        /// <summary> 获取上传协议 
        /// </summary>
        /// <param name="dr">数据行</param>
        /// <param name="dbName">帐套</param>
        /// <returns></returns>
        private static AnnounceProtocol GetProtocol(DataRow dr, string dbName)
        {
            AnnounceProtocol protocol = new AnnounceProtocol();
            protocol.StationId = GlobalStaticObj_Server.Instance.StationID;
            //根据表名得到子消息ID
            protocol.SubMessageId = SubMessageId;
            protocol.TimeSpan = TimeHelper.GetTimeInMillis();
            //Json对象
            string json = JsonHelper.DataTableToJson(dr, protocol.StationId, dbName, true);
            json = BaseCodeHelper.EnCode(json);
            protocol.Json = json;
            return protocol;
        }
        /// <summary> 操作数据
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="operation">操作类别</param>
        /// <param name="dbName">帐套</param>
        private static void Operation(DataTable dt,string operation,string dbName)
        {
            if (dt == null)
            {
                return;
            }
            foreach (DataRow dr in dt.Rows)
            {
                AnnounceProtocol protocol = GetProtocol(dr,dbName);
                if (protocol != null)
                {
                    protocol.Operation = operation;
                    ServiceAgent.AddSendQueue(protocol);
                }
            }
        }
        /// <summary> 接收保存数据 
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private static void SaveData(object state)
        {
            if (state is AnnounceProtocol)
            {
                AnnounceProtocol protocol = state as AnnounceProtocol;
                DataTable dt = JsonHelper.JsonToDataTable(protocol.Json, TableName,true);
                if (dt == null || dt.Rows.Count == 0)
                {
                    return;
                }
                bool result = false;
                string id = "announcement_id";
                Dictionary<string, string> dicFileds = new Dictionary<string, string>();
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.ColumnName != id)
                    {
                        dicFileds.Add(dc.ColumnName, dt.Rows[0][dc.ColumnName].ToString());
                    }
                }
                if (protocol.Operation.Equals(DataSources.EnumOperationType.Add.ToString("d")))
                {
                    if (dt.Columns.Contains(id))
                    {
                        dicFileds.Add(id, dt.Rows[0][id].ToString());
                    }
                    foreach (string db in AutoTask.GetDatabaseList())
                    {
                        //添加数据
                        result = DBHelper.Submit_AddOrEdit("添加云平台公告信息", db, dt.TableName, "", "", dicFileds);
                    }
                }
                else if (protocol.Operation.Equals(DataSources.EnumOperationType.Update.ToString("d")))
                {
                    if (dt.Columns.Contains(id))
                    {
                        foreach (string db in AutoTask.GetDatabaseList())
                        {
                            //修改数据
                            result = DBHelper.Submit_AddOrEdit("修改云平台公告信息", db, dt.TableName, id, dt.Rows[0][id].ToString(), dicFileds);
                        }
                    }
                }
                ResultProtocol rp = new ResultProtocol();
                rp.StationId = protocol.StationId;
                rp.SerialNumber = protocol.SerialNumber;
                rp.MessageId = protocol.MessageId;
                rp.SubMessageId = protocol.SubMessageId;
                rp.TimeSpan = protocol.TimeSpan;
                rp.SerialNumberLock = true;
                if (result)
                {
                    rp.Result = DataSources.EnumResultType.Success.ToString("d");
                }
                else
                {
                    rp.Result = DataSources.EnumResultType.Fail.ToString("d");
                }
                ServiceAgent.AddSendQueue(rp);
            }
        }
        #endregion       

        #region --公用方法
        /// <summary> 上传数据 
        /// </summary>
        /// <param name="dbName">帐套信息</param>
        /// <param name="time">下次上传数据时间</param>
        public static void UpLoadData(string dbName,string time)
        {
            #region --添加数据
            Operation(
                DataHelper.GetAddDataFromHXC(TableName, dbName,
                TimeHelper.GetTimeInMillis(GlobalStaticObj_Server.Instance.LastUploadTime),
                TimeHelper.GetTimeInMillis(time)),
                DataSources.EnumOperationType.Add.ToString("d"), dbName);
            #endregion 
           
            Thread.Sleep(100);

            #region --修改数据
            Operation(
                DataHelper.GetUpdateDataFormHXC(TableName, dbName,
                TimeHelper.GetTimeInMillis(GlobalStaticObj_Server.Instance.LastUploadTime),
                TimeHelper.GetTimeInMillis(time)),
                DataSources.EnumOperationType.Update.ToString("d"), dbName);
            #endregion  

            Thread.Sleep(100);

            //#region --删除数据
            //Operation(DataHelper.GetUpdateDataFormHXC(TableName, dbName),
            //     DataSources.EnumOperationType.Delete.ToString("d"), dbName);
            //#endregion
        }        
        /// <summary> 处理收到的公告信息 
        /// </summary>
        /// <param name="protocol"></param>
        public static void Deal(AnnounceProtocol protocol)
        {
            //保存至数据库，并返回成功/失败信息
            ThreadPool.QueueUserWorkItem(new WaitCallback(SaveData), protocol);
        }
        /// <summary> 写入错误日志 
        /// </summary>        
        /// <param name="protocol">上传数据协议</param>
        public static void WriteErrorLog(UploadDataProtocol protocol)
        {
            string msg = string.Empty;

            msg += "表名：" + TableName + "\r\n";
            msg += "标识：" + protocol.StationId + protocol.SerialNumber + protocol.TimeSpan + "\r\n";
            msg += "时间：" + TimeHelper.MillisToTime(protocol.TimeSpan) + "\r\n";
            msg += "服务站ID：" + protocol.StationId + "\r\n";
            msg += "内容：" + ProtocolTranslator.SerilizeMessage(protocol);
            //写错误日志
            Log.writeCloudLog(msg);
        }
        #endregion
    }
}
