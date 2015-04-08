using System.Collections.Generic;
using System.Data;
using System.Collections;
using SYSModel;
using HXC_FuncUtility;
using CloudPlatSocket.Protocol;
using System.Threading;
using Utility.Log;
using CloudPlatSocket.Model;

namespace CloudPlatSocket.Handler
{
    /// <summary>
    /// 附件处理类
    /// </summary>
    public class FileHandler
    {
        #region --成员变量
        /// <summary> 【表名-子消息ID】散列表
        /// </summary>
        public static Hashtable htTable = new Hashtable();
        #endregion

        #region --构造函数
        public FileHandler()
        {
            InitHashtable();                    
        }
        #endregion

        #region --初始化散列表
        /// <summary>
        /// 初始化散列表
        /// </summary>
        private static void InitHashtable()
        {
            if (htTable.Count > 0)
            {
                return;
            }
            //附件信息报
            htTable.Add(AttachmentModel._table,
                new ProtocolValue() { MessageId = "F1", PreTableName = "", Key = "", PreKey = "" });
            //宇通三包附件信息
            htTable.Add(ThreeMaintainModel._table,
                new ProtocolValue() { MessageId = "F2", PreTableName = "tb_maintain_three_guaranty", Key = "tg_id", PreKey = "tg_id" });
            //维修管理 附件信息
            htTable.Add(MaintainModel._table,
                new ProtocolValue() { MessageId = "F3", PreTableName = "tb_maintain_info", Key = "accessory_id", PreKey = "maintain_id" });
        }
        #endregion

        #region --私有成员
        /// <summary> 获取上传协议 
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="dbName">帐套</param>
        /// <returns></returns>
        private static List<FileProtocol> GetProtocol(DataTable dt,string dbName)
        {
            List<FileProtocol> fps = new List<FileProtocol>();
            if (dt != null)
            {
                FileProtocol fp = null;
                foreach (DataRow dr in dt.Rows)
                {
                    fp = GetProtocol(dr,dbName);
                    if (fp != null)
                    {
                        fps.Add(fp);
                    }
                }
            }
            return fps;
        }
        /// <summary> 获取上传协议 
        /// </summary>
        /// <param name="dr">数据行</param>
        /// <param name="dbName">帐套</param>
        /// <returns></returns>
        private static FileProtocol GetProtocol(DataRow dr, string dbName)
        {
            if (!htTable.ContainsKey(dr.Table.TableName))
            {
                return null;
            }
            FileProtocol fp = new FileProtocol();
            fp.StationId = GlobalStaticObj_Server.Instance.StationID;
            //根据表名得到子消息ID
            if (htTable[dr.Table.TableName] is ProtocolValue)
            {
                fp.SubMessageId = (htTable[dr.Table.TableName] as ProtocolValue).MessageId;
            }
            else
            {
                fp.SubMessageId = htTable[dr.Table.TableName].ToString();
            }           
            fp.TimeSpan = TimeHelper.GetTimeInMillis();            
            //Json对象
            string json = JsonHelper.DataTableToJson(dr, fp.StationId, dbName,true);
            json = BaseCodeHelper.EnCode(json);
            fp.Json = json;
            return fp;
        }
        /// <summary> 操作数据
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="operation">操作类别</param>
        /// <param name="dbName">帐套</param>
        private static void Operation(DataTable dt, string operation, string dbName)
        {
            if (dt == null)
            {
                return;
            }
            string path = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                FileProtocol protocol = GetProtocol(dr, dbName);
                if (protocol != null)
                {
                    protocol.Operation = operation;
                    if (operation == DataSources.EnumOperationType.Add.ToString("d")
                        || operation == DataSources.EnumOperationType.Update.ToString("d"))
                    {
                        FileModel file = FileModel.CreateModel(dr);
                        protocol.FileId = file.Id;
                        protocol.FileType = file.FileType;

                        path = GlobalStaticObj_Server.Instance.FilePath + "\\" + file.Path;
                        //上传文件信息     
                        byte[] files = FileHelper.GetFileInByte(path);
                        if (files == null)
                        {
                            continue;
                        }
                        protocol.File = ProtocolTranslator.ByteToHex(files);
                    }                    
                    FileAgent.AddSendQueue(protocol);
                }
            }
        }
        #endregion

        #region --公用方法
        /// <summary> 上传数据 
        /// </summary>
        /// <param name="dbName">帐套信息</param>
        /// <param name="time">下次上传数据时间</param>
        public static void UpLoadFile(string dbName,string time)
        {
            InitHashtable();
            string tableName = string.Empty;
            foreach (DictionaryEntry de in htTable)
            {
                tableName = de.Key.ToString();
                ProtocolValue pv = null;
                if (de.Value is ProtocolValue)
                {
                    pv = de.Value as ProtocolValue;
                }

                #region --删除数据
                Operation(
                    DataHelper.GetDeleteData(tableName, pv, dbName,
                    TimeHelper.GetTimeInMillis(GlobalStaticObj_Server.Instance.LastUploadTime),
                    TimeHelper.GetTimeInMillis(time)),
                    DataSources.EnumOperationType.Delete.ToString("d"), dbName);
                #endregion

                Thread.Sleep(100);

                #region --修改数据
                Operation(
                    DataHelper.GetUpdateData(tableName, pv, dbName,
                    TimeHelper.GetTimeInMillis(GlobalStaticObj_Server.Instance.LastUploadTime),
                    TimeHelper.GetTimeInMillis(time)),
                    DataSources.EnumOperationType.Update.ToString("d"), dbName);
                #endregion

                Thread.Sleep(100);

                #region --添加数据
                Operation(
                    DataHelper.GetAddData(tableName, pv, dbName,
                    TimeHelper.GetTimeInMillis(GlobalStaticObj_Server.Instance.LastUploadTime),
                    TimeHelper.GetTimeInMillis(time)),
                    DataSources.EnumOperationType.Add.ToString("d"), dbName);        
                #endregion

                Thread.Sleep(100);
            }
        }
        /// <summary> 上传数据 
        /// </summary>
        /// <param name="dbName">帐套信息</param>
        /// <param name="time">下次上传数据时间</param>
        public static void UpLoadFileTest(string dbName, string time)
        {
            InitHashtable();

            string tableName = string.Empty;
            foreach (DictionaryEntry de in htTable)
            {
                tableName = de.Key.ToString();
                ProtocolValue pv = null;
                if (de.Value is ProtocolValue)
                {
                    pv = de.Value as ProtocolValue;
                }     

                #region --添加数据
                Operation(
                    DataHelper.GetDataTest(tableName, dbName,
                    TimeHelper.GetTimeInMillis(GlobalStaticObj_Server.Instance.LastUploadTime),
                    TimeHelper.GetTimeInMillis(time)),
                    DataSources.EnumOperationType.Add.ToString("d"), dbName);
                #endregion

                Thread.Sleep(100);
            }

        }
        /// <summary> 写入错误日志 
        /// </summary>        
        /// <param name="protocol">上传文件协议</param>
        public static void WriteErrorLog(FileProtocol protocol)
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
        /// <summary> 附件数据处理 
        /// </summary>
        /// <param name="protocol"></param>
        public static void Deal(FileProtocol protocol)
        {
            
        }
        #endregion
    }
}
