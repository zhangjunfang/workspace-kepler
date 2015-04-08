using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Utility.Net
{
    /// <summary>
    /// Creator:yangtianshuai
    /// CreateTime:2014.11.4
    /// Function:tcp listener
    /// </summary>
    public class MessageListener
    {
        #region 成员变量
        // Fields
        private bool isListen;
        private int port;
        private TcpListener tcpListener;
        // Events
        public event MessageListenerDelegate MessageListenerEvent;
        // Nested Types
        public delegate void MessageListenerDelegate(TcpClient tcpClient);
        #endregion

        #region 属性
        public bool IsListen
        {
            get { return isListen; }
        }
        #endregion

        #region 构造函数
        // Methods
        public MessageListener(int _port)
        {
            this.port = _port;
        }
        #endregion

        #region 成员方法
        public void Listen()
        {
            try
            {
                this.tcpListener = new TcpListener(IPAddress.Any, this.port);
                this.tcpListener.Start();
                this.isListen = true;
                while (this.isListen)
                {
                    TcpClient state = this.tcpListener.AcceptTcpClient();                   
                    state.NoDelay = true;
                    ThreadPool.SetMaxThreads(100, 100);
                    ThreadPool.SetMinThreads(20, 20);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(this.NotifyMessage), state);
                }
            }
            catch (Exception)
            {
                this.Stop();
                //MessageBox.Show(exception.Message);
            }
        }

        private void NotifyMessage(object o)
        {
            TcpClient tcpClient = (TcpClient)o;
            if (this.MessageListenerEvent != null)
            {
                this.MessageListenerEvent(tcpClient);
            }
        }

        public void Stop()
        {
            this.isListen = false;
            if (this.tcpListener != null)
            {
                try
                {
                    this.tcpListener.Stop();
                }
                catch (Exception)
                {
                    //MessageBox.Show(exception.Message);
                }
            }
        }
        #endregion
    }
}