using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Utility.Net
{
    /// <summary>
    /// Creator:yangtianshuai
    /// CreateTime:2014.11.3
    /// Function:UDP Client
    /// UpdateTime:2014.11.3
    /// </summary>
    public class MessageClient
    {
        #region 成员变量
        // Fields
        private NetworkStream networkStream;
        private string serverIP;
        private int serverPort;
        private TcpClient tcpClient;
        private int timeOut;
        #endregion

        #region 构造函数
        // Methods
        public MessageClient(string _serverIP, int _serverPort)
        {
            if (_serverIP.Length == 0)
            {
                return;
            }
            this.serverIP = _serverIP;
            this.serverPort = _serverPort;
            timeOut = 30000;
            this.tcpClient = new TcpClient();
            //this.tcpClient.ReceiveTimeout = this.timeOut;
            this.tcpClient.SendTimeout = this.timeOut;
        }
        public MessageClient(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
            timeOut = 30000;
            //this.tcpClient.ReceiveTimeout = this.timeOut;
            this.tcpClient.SendTimeout = this.timeOut;
        }
        #endregion

        #region 连接/断开
        public bool Connect()
        {
            if (this.tcpClient == null)
            {
                return false;
            }
            try
            {
                if (this.tcpClient.Connected)
                {
                    return true;
                }

                this.tcpClient.Connect(IPAddress.Parse(this.serverIP), this.serverPort);
                return true;
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000);
                //写入日志
                Log.Log.writeCloudLog(ex.Message);
                return false;
            }
        }      
       
        public void DisConnect()
        {
            if (this.networkStream != null)
            {
                this.networkStream.Close();
                this.networkStream.Dispose();
            }
            if (this.tcpClient != null)
            {
                this.tcpClient.Close();
                this.tcpClient = null;
            }
        }
        #endregion

        #region 发送/接收
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool Send(string message)
        {
            if (!this.Connect())
            {
                return false;
            }

            if (this.networkStream == null)
            {
                this.networkStream = this.tcpClient.GetStream();
            }            
            byte[] data = Encoding.UTF8.GetBytes(message);           
            //发送数据
            this.networkStream.Write(data, 0, data.Length);
            this.networkStream.Flush();
            return true;
        }
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public string Receive(char start, char end)
        {
            if (!this.Connect())
            {
                return string.Empty;
            }

            if (this.networkStream == null)
            {
                this.networkStream = this.tcpClient.GetStream();
            }
            byte[] read = new byte[1];
            //转义处理           
            byte[] data = new byte[100];
            int len = 0;
            bool receiveFlag = false;
            try
            {
                if (!this.networkStream.DataAvailable)
                {
                    return string.Empty;
                }
                this.networkStream.Read(read, 0, 1);
                char ch = (char)read[0];
                while (ch != end)
                {
                    if (ch == start)
                    {
                        receiveFlag = true;
                    }
                    else
                    {
                        if (receiveFlag)
                        {
                            if (len > data.Length - 1)
                            {
                                //数据扩容，规则加倍
                                byte[] dataTemp = new byte[data.Length];
                                data.CopyTo(dataTemp, 0);
                                data = new byte[2 * data.Length];
                                dataTemp.CopyTo(data, 0);
                            }
                            data[len] = read[0];
                            len++;
                        }

                    }
                    if (this.networkStream.DataAvailable)
                    {
                        this.networkStream.Read(read, 0, 1);
                        ch = (char)read[0];
                    }
                    else
                    {
                        Thread.Sleep(5);
                    }
                }
                if (len > 0)
                {
                    //取出实际数据大小
                    byte[] realData = new byte[len];
                    for (int i = 0; i < len; i++)
                    {
                        realData[i] = data[i];
                    }
                    return Encoding.UTF8.GetString(realData);
                }
                Log.Log.writeCloudLog("解析错误：未发现开始符");
                return string.Empty;
            }
            catch (Exception ex)
            {
                //写入日志
                Log.Log.writeLog(ex.Message);
            }
            return string.Empty;
        }
        #endregion 
    }
}
