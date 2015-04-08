using System.Collections.Generic;
using CloudPlatSocket.Protocol;
using SYSModel;
using System.Data;
using HXC_FuncUtility;
using BLL;
using System.Collections;
using Utility.Log;
using System.Threading;

namespace CloudPlatSocket.Handler
{
    /// <summary>
    /// 控制类
    /// </summary>
    public class ContolHandler
    {
        #region --成员变量
        /// <summary>
        /// 在线用户控制子消息ID
        /// </summary>
        private static string SubMessageId1 = "CD1";
        /// <summary>
        /// 云备份控制子消息ID
        /// </summary>
        private static string SubMessageId2 = "CD2";

        /// <summary> 【表名-子消息ID】散列表
        /// </summary>
        private static Hashtable htTable = new Hashtable();
        #endregion

        #region --构造函数
        public ContolHandler()
        {
            htTable.Add("sys_user", "T1");
        }
        #endregion

        #region --私有方法
        /// <summary> 获取在线客户端数据表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="dbName">帐套</param>
        /// <returns></returns>
        private static DataTable GetData(string tableName,string dbName)
        {
            string where = string.Format("is_online = '{0}'",DataSources.EnumYesNo.Yes);
            DataTable dt = DBHelper.GetTable("获取新增上传表[" + tableName + "]数据", dbName, tableName, "*", where, "", "");
            return dt;
        }
        /// <summary> 获取在线客户端 
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="dbName">帐套</param>
        /// <returns></returns>
        private static List<ClientProtocol> GetProtocol(DataTable dt,string dbName)
        {
            List<ClientProtocol> cps = new List<ClientProtocol>();
            if (dt != null)
            {
                ClientProtocol cp = null;
                foreach (DataRow dr in dt.Rows)
                {
                    cp = GetProtocol(dr, dbName);
                    if (cp != null)
                    {
                        cps.Add(cp);
                    }
                }
            }
            return cps;
        }
        /// <summary> 获取在线客户端 
        /// </summary>
        /// <param name="dr">数据行</param>
        /// <param name="dbName">帐套</param>
        /// <returns></returns>
        private static ClientProtocol GetProtocol(DataRow dr,string dbName)
        {
            if (!htTable.ContainsValue(dr.Table.TableName))
            {
                return null;
            }
            ClientProtocol cp = new ClientProtocol();
            cp.StationId = GlobalStaticObj_Server.Instance.StationID;            
            //根据表名得到子消息ID
            cp.SubMessageId = htTable[dr.Table.TableName].ToString();
            cp.TimeSpan = TimeHelper.GetTimeInMillis();
            //Json对象
            string json = JsonHelper.DataTableToJson(dr, cp.StationId, dbName,true);
            json = BaseCodeHelper.EnCode(json);
            cp.Json = json;
            return cp;
        }
        /// <summary> 处理收到的控制信息 
        /// </summary>
        /// <param name="state"></param>
        private static void SaveData(object state)
        {
            if (state is ControlProtocol)
            {
                ControlProtocol protocol = state as ControlProtocol;
                bool result = false;
                //保存至数据库，并返回成功/失败信息
                Dictionary<string, string> dicFileds = new Dictionary<string, string>();
                if (protocol.ControlType.Equals(DataSources.EnumControlType.UnAvailble.ToString("d")))
                {
                    dicFileds.Add("status", DataSources.EnumStatus.Stop.ToString("d"));
                }
                else if (protocol.ControlType.Equals(DataSources.EnumControlType.Availble.ToString("d")))
                {
                    dicFileds.Add("status", DataSources.EnumStatus.Start.ToString("d"));
                }
                //协议里包含帐套信息
                result = DBHelper.Submit_AddOrEdit("修改用户信息", GlobalStaticObj_Server.Instance.CurrAccDbName, "sys_user", "user_id", "", dicFileds);

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
        public static void UpLoadData(string dbName)
        {
            DataTable dt;
            ClientProtocol protocol;
            foreach (DictionaryEntry de in htTable)
            {
                dt = GetData(de.Key.ToString(),dbName);
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        protocol = GetProtocol(dr,dbName);
                        if (protocol != null)
                        {                            
                            ServiceAgent.AddSendQueue(protocol);
                        }
                    }
                }
            }
        }       
        /// <summary> 修改控制 
        /// </summary>
        /// <param name="subId">子消息ID</param>
        /// <param name="controlType">控制类别</param>
        /// <param name="protocol">协议</param>
        public static void Deal(ControlProtocol protocol)
        {           
            //保存至数据库，并返回成功/失败信息
            ThreadPool.QueueUserWorkItem(new WaitCallback(SaveData), protocol);           
        }
        /// <summary> 写入错误日志 
        /// </summary>        
        /// <param name="protocol">上传数据协议</param>
        public static void WriteErrorLog(ClientProtocol protocol)
        {
            string msg = string.Empty;
            if (htTable.ContainsKey(protocol.SubMessageId))
            {
                string tableName = string.Empty;
                //表名：
                foreach (DictionaryEntry de in htTable)
                {
                    if (de.Value.ToString() == protocol.SubMessageId)
                    {
                        tableName = de.Key.ToString();
                        break;
                    }
                }
                msg += "表名：" + tableName + "\r\n";
            }
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
