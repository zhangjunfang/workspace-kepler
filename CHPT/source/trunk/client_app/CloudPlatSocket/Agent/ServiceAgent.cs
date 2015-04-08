using System;
using System.Windows.Forms;
using Utility.Log;
using Utility.Net;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using HXC_FuncUtility;
using CloudPlatSocket.Handler;

namespace CloudPlatSocket
{
    /// <summary>
    /// Creator:yangtianshuai
    /// CreateTime:2014.11.5
    /// Function:Service Agent
    /// UpdateTime:2014.11.5
    /// </summary>
    public class ServiceAgent
    {
        #region --成员变量
        /// <summary>
        /// 发送消息队列
        /// </summary>
        private static Queue<MessageProtocol> protocols = new Queue<MessageProtocol>();
        /// <summary>
        /// 已发送消息队列
        /// </summary>
        private static Hashtable htProtocols = new Hashtable();
        /// <summary>
        /// 
        /// </summary>
        private static object mylock = new object();
        /// <summary>
        /// 接收完成事件
        /// </summary>
        /// <param name="message"></param>
        public delegate void ReceiveComplate(string message);
        /// <summary>
        /// 接收完成
        /// </summary>
        public static event ReceiveComplate ReceiveComplated;
        /// <summary>
        /// 发送完成事件
        /// </summary>
        /// <param name="message"></param>
        public delegate void SendComplate(string message);
        /// <summary>
        /// 接收完成
        /// </summary>
        public static event SendComplate SendComplated;
        /// <summary>
        /// 通讯客户端
        /// </summary>
        private static MessageClient mc;
        /// <summary>
        /// 服务端IP
        /// </summary>
        private static string _ip = "119.57.151.34";
        /// <summary>
        /// 服务端端口
        /// </summary>
        private static int _port = 19000;
        /// <summary>
        /// 发送线程
        /// </summary>
        private static Thread threadSend;
        /// <summary>
        /// 接收线程
        /// </summary>
        private static Thread threadReceive;
        /// <summary>
        /// 流水号
        /// </summary>
        public static int serialNumber = 0;
        #endregion       

        #region --获取流水号
        /// <summary>
        /// 获取流水号
        /// </summary>
        /// <returns></returns>
        public int GetSerialNumber()
        {
            return serialNumber;
        }
        #endregion

        #region --启动\停止发收线程
        public static void StartSendAndReceiveThread()
        {           
            //发送线程开启
            if (threadSend == null)
            {
                threadSend = new Thread(new ThreadStart(_SendThread));
                threadSend.IsBackground = true;
                threadSend.Start();
            }
            else if (threadSend.ThreadState == ThreadState.Suspended)
            {
                threadSend.Resume();
            }

            //接收线程开启
            if (threadReceive == null)
            {
                threadReceive = new Thread(new ThreadStart(_ReceiveThread));
                threadReceive.IsBackground = true;
                threadReceive.Start();
            }
        }
        /// <summary>
        /// 暂停接收线程
        /// </summary>
        public static void SupendSendThread()
        {
            if (threadSend == null && threadSend.ThreadState == ThreadState.Running)
            {
                threadSend.Suspend();
            }         
        }
        public static void ResumeSendThread()
        {          
            if (threadSend == null && threadSend.ThreadState == ThreadState.Suspended)
            {
                threadSend.Resume();
            } 
        }   
        #endregion

        #region --参数设置
        public static void SetParas(string ip, int port)
        {
            _ip = ip;
            _port = port;
        }
        #endregion

        #region --析构函数
        ~ServiceAgent()
        {
            ServiceTestClose();
        }
        #endregion

        #region --服务器连接测试
        /// <summary>
        /// 当前服务器连接是否正常
        /// </summary>
        /// <returns></returns>
        public static bool ServiceTest()
        {
            if (mc == null)
            {
                mc = new MessageClient(GlobalStaticObj_Server.Instance.CloundServerIp, GlobalStaticObj_Server.Instance.CloundServerPort);
            }
            if (mc.Connect())
            {
                return true;
            }
            return false;
        }       
        /// <summary>
        /// 关闭测试连接
        /// </summary>
        /// <returns></returns>
        public static bool ServiceTestClose()
        {
            if (mc == null)
            {
                return false;
            }
            if (mc.Connect())
            {
                mc.DisConnect();
                return true;
            }
            return false;
        }
        #endregion

        #region --发送并接收消息
        /// <summary>
        /// 向服务端发送消息，并接收消息
        /// </summary>
        /// <param name="protocol">协议对象</param>
        /// <returns></returns>
        public static MessageProtocol SendAndReceiveMessage(MessageProtocol protocol)
        {
            if (mc == null)
            {
                mc = new MessageClient(_ip, _port);
            }

            MessageProtocol _protocol = null;
            try
            {
                if (!protocol.SerialNumberLock)
                {
                    protocol.SerialNumber = serialNumber.ToString();
                    //流水号递增
                    serialNumber++;
                    if (serialNumber > 1000000)
                    {
                        serialNumber = 0;
                    }
                }
                if (mc.Connect())
                {
                    //发送命令                    
                    string message = ProtocolTranslator.SerilizeMessage(protocol);
                    mc.Send(message);
                    Log.writeCloudLog("【数据通讯-发送消息】：" + message);
                    Thread.Sleep(100);
                    string str = mc.Receive(ProtocolTranslator.StartFlag, ProtocolTranslator.EndFlag);                   
                    _protocol = ProtocolTranslator.DeserilizeMessage(str);
                }               
            }
            catch (Exception ex)
            {
                Log.writeLineToLog(ex.Message, "云支撑平台文件通讯故障");               
            }
            return _protocol;
        }
        #endregion     

        #region --发送消息
        /// <summary>
        /// 向服务端发送消息
        /// </summary>
        /// <param name="protocol">协议对象</param>
        /// <returns></returns>
        private static bool SendMessage(MessageProtocol protocol)
        {
            if (mc == null)
            {
                mc = new MessageClient(_ip, _port);
            }
            try
            {
                if (!protocol.SerialNumberLock)
                {
                    protocol.SerialNumber = serialNumber.ToString();
                    //流水号递增
                    serialNumber++;
                    if (serialNumber > 1000000)
                    {
                        serialNumber = 0;
                    }
                }
                if (mc.Connect())
                {                   
                    //发送命令                    
                    string message = ProtocolTranslator.SerilizeMessage(protocol);
                    string key = protocol.StationId + protocol.SerialNumber + protocol.TimeSpan;
                    if (!htProtocols.ContainsKey(key))
                    {
                        htProtocols.Add(key, protocol);
                    }
                    if (SendComplated != null)
                    {
                        //显示消息
                        SendComplated(message);
                    }
                    mc.Send(message);
                    Log.writeCloudLog("【数据通讯-发送消息】：" + message);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.writeCloudLog("云通讯服务：" + ex.Message);
            }
            return false;
        }
        #endregion

        #region --添加消息队列
        /// <summary>
        /// 向发送队列添加消息
        /// </summary>
        /// <param name="protocol"></param>
        public static void AddSendQueue(MessageProtocol protocol)
        {
            if (protocol == null)
            {
                return;
            }
            lock (mylock)
            {
                if (ServiceTest())
                {                   
                    protocols.Enqueue(protocol);
                }
                else
                {
                    GlobalStaticObj_Server.Instance.ServerLink = false;
                }
            }
        }
        #endregion

        #region --接收消息
        private static MessageProtocol ReceiveMessage()
        {
            if (mc == null)
            {
                return null;
            }
            //接收数据
            string str = mc.Receive(ProtocolTranslator.StartFlag, ProtocolTranslator.EndFlag);           
            MessageProtocol _protocol = null;
            if (str.Length > 0)
            {
                _protocol = ProtocolTranslator.DeserilizeMessage(str);
            }
            return _protocol;
        }
        #endregion      

        #region --发送线程
        /// <summary>
        /// 发送线程
        /// </summary>
        /// <param name="state"></param>
        public static void _SendThread()
        {
            while (true)
            {
                if (protocols.Count > 0)
                {
                    //Log.writeLog("发送数据");
                    SendMessage(protocols.Dequeue());
                }
                else
                {
                    Thread.Sleep(100);
                }

                Thread.Sleep(20);
            }
        }
        #endregion

        #region --接收线程
        /// <summary> 接收线程
        /// </summary>
        /// <param name="state"></param>
        public static void _ReceiveThread()
        {
            MessageProtocol protocol;
            bool flag = false;
            string key = string.Empty;
            while (true)
            {
                protocol = ReceiveMessage();
                if (protocol != null)
                {
                    //状态设置，设置当前时间
                    GlobalStaticObj_Server.Instance.ServerLink = true;
                    //组建唯一标识
                    key = protocol.StationId + protocol.SerialNumber + protocol.TimeSpan;
                    if (htProtocols.ContainsKey(key))
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                    if (protocol.IsSuccess())
                    {
                        if (ReceiveComplated != null)
                        {
                            //显示消息
                            ReceiveComplated(ProtocolTranslator.SerilizeMessage(protocol.GetRealProtocol()));
                        }
                        //具体操作是否有效
                        protocol.Do(ref flag);                        
                        if (flag)
                        {                            
                            htProtocols.Remove(key);
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        //重新发送数据
                        MessageProtocol mp = htProtocols[key] as MessageProtocol;
                        mp.sendCount++;
                        if (mp.SendCount > 3)
                        {
                            if (htProtocols.ContainsKey(key))
                            {
                                htProtocols.Remove(key);
                            }
                            if (mp.SendCount == 3)
                            {
                                //写错误日志
                                mp.ErrorLog();
                            }
                        }
                        else
                        {
                            //重复发送三次
                            mp.SerialNumberLock = true;
                            AddSendQueue(mp);
                        }
                    }
                }
                Thread.Sleep(50);
            }
        }
        #endregion
    }
}