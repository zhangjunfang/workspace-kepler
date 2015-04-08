using System;
using System.Windows.Forms;
using System.Threading;
using CloudPlatSocket.Protocol;
using CloudPlatSocket;
using HXC_FuncUtility;

namespace testWinForm
{
    public partial class frmSocketTest : Form
    {
        #region --成员变量
        //接收数
        private int recCount = 0;
        //发送数
        private int sendCount = 0;
        //接收数
        private int recFileCount = 0;
        //发送数
        private int sendFileCount = 0;
        private bool startFlag=true;
        private delegate void ThreadWork(string msg);
        #endregion

        #region --构造函数
        public frmSocketTest()
        {
            InitializeComponent();            
        }
        #endregion

        #region --启动、继续服务
        private void btnLink_Click(object sender, EventArgs e)
        {
            if (this.tbIp.Text.Trim().Length == 0
                || this.tbIp.Text.Trim().Length == 0)
            {
                MessageBox.Show("IP and Port is required!");
                return;
            }
            int port = 0;

            if (!int.TryParse(this.tbPort.Text.Trim(), out port))
            {
                MessageBox.Show("Port must be number!");
                return;
            }

            ServiceAgent.SetParas(this.tbIp.Text.Trim(), port);

            if (int.TryParse(this.tbFilePort.Text.Trim(), out port))
            {
                FileAgent.SetParas(this.tbIp.Text.Trim(), port);
            }

            GlobalStaticObj_Server.Instance.StationID = this.tbSendMsg.Text.Split('$')[0];
            if (ServiceAgent.ServiceTest() || FileAgent.ServiceTest())
            {
                if (this.startFlag)
                {
                    this.startFlag = false;
                    //AutoTask.StartTest(this.dateTimePicker1.Value.Ticks);
                }
                else
                {
                    AutoTask.Continue();
                }

                this.btnLink.Text = "已连接";
                this.btnLink.Enabled = false;
                this.btnClose.Enabled = true;
            }
        }
        #endregion

        #region --暂停服务
        private void btnClose_Click(object sender, EventArgs e)
        {
            AutoTask.Stop();          

            this.btnLink.Enabled = true;
            this.btnClose.Enabled = false;
            this.btnLink.Text = "连接";
        }
        #endregion

        #region --手动点击发送
        private void btnSend_Click(object sender, EventArgs e)
        {
            string msg = this.tbSendMsg.Text.Trim();
            if (msg.Length == 0)
            {
                return;
            }
            AutoTask.SetLogin();
            this.SendMessage(msg);         
           
            //ThreadPool.QueueUserWorkItem(new WaitCallback(this.SendMessage), msg);            
        }
        #endregion

        #region --发送消息
        private void SendMessage(object state)
        {
            string[] arrays = state.ToString().Split('$');
            if (arrays.Length != 8)
            {
                return;
            }
            LoginProtocol lp = new LoginProtocol();
            lp.StationId = arrays[0];
            lp.SerialNumber = arrays[1];
            lp.MessageId = arrays[2];
            lp.SubMessageId = arrays[3];
            lp.TimeSpan = arrays[4];

            lp.UserId = arrays[5];
            lp.Password = arrays[6];
            lp.PermissionCode = arrays[7];          

            ServiceAgent.AddSendQueue(lp);
            FileAgent.AddSendQueue(lp);           
        }
        #endregion

        #region --发送/接收回调
        //接收
        private void ThreadSend(string msg)
        {
            if (this.tbSend.InvokeRequired)//等待异步 
            {
                ThreadWork fc = new ThreadWork(ThreadSend);
                // this.Invoke(fc);//通过代理调用刷新方法 
                this.Invoke(fc, msg);
            }
            else
            {
                sendCount++;
                this.tbSend.Text += DateTime.Now.ToString("[HH:mm:ss:fff] ") + sendCount + "    " + msg + "\r\n";
            }
        }
        //发送
        private void ThreadReceive(string msg)
        {
            if (this.tbReceiveMsg.InvokeRequired)//等待异步 
            {
                ThreadWork fc = new ThreadWork(ThreadReceive);
                // this.Invoke(fc);//通过代理调用刷新方法 
                this.Invoke(fc, msg);
            }
            else
            {
                recCount++;
                this.tbReceiveMsg.Text += DateTime.Now.ToString("[HH:mm:ss:fff] ") + recCount + "    " + msg + "\r\n";
            }
        }
        #endregion

        #region --发送/接收回调
        //接收
        private void ThreadSendFile(string msg)
        {
            if (this.tbSend.InvokeRequired)//等待异步 
            {
                ThreadWork fc = new ThreadWork(ThreadSendFile);
                // this.Invoke(fc);//通过代理调用刷新方法 
                this.Invoke(fc, msg);
            }
            else
            {
                sendFileCount++;
                this.tbFileSend.Text += DateTime.Now.ToString("[HH:mm:ss:fff] ") + sendCount + "    " + msg + "\r\n";
            }
        }
        //发送
        private void ThreadReceiveFile(string msg)
        {
            if (this.tbReceiveMsg.InvokeRequired)//等待异步 
            {
                ThreadWork fc = new ThreadWork(ThreadReceiveFile);
                // this.Invoke(fc);//通过代理调用刷新方法 
                this.Invoke(fc, msg);
            }
            else
            {
                recFileCount++;
                this.tbFileReceive.Text += DateTime.Now.ToString("[HH:mm:ss:fff] ") + recCount + "    " + msg + "\r\n";
            }
        }
        #endregion

        #region --窗体初始化
        private void frmSocketTest_Load(object sender, EventArgs e)
        {
            this.labelTicks.Text = this.dateTimePicker1.Value.Ticks.ToString();

            ConfigConst.ConfigPath = "testWinForm.exe.config";

            //string conn = LocalVariable.GetConnString("HXC", ConfigConst.ConnectionStringWrite);
           
            LocalVariable.Init();

            this.labelTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
         
            this.tbIp.Text = "119.57.151.34";
            this.tbPort.Text = "19000";
            this.tbFilePort.Text = "19999";
            this.tbSendMsg.Text = "111$21$L$L1$1414665362571$testxh$test1234$654321";
            ServiceAgent.ReceiveComplated += new ServiceAgent.ReceiveComplate(this.ThreadReceive);
            ServiceAgent.SendComplated += new ServiceAgent.SendComplate(this.ThreadSend);

            FileAgent.ReceiveComplated += new FileAgent.ReceiveComplate(this.ThreadReceiveFile);
            FileAgent.SendComplated += new FileAgent.SendComplate(this.ThreadSendFile);
        }
        #endregion

        #region --发送消息
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.labelTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");            
            //btnSend_Click(null, null);
            this.labelCount.Text = this.sendCount + "/" + this.recCount + "条";
            this.lableFileCount.Text = this.sendFileCount + "/" + this.recFileCount + "条";
        }
        #endregion       

        //附件上传
        private void button1_Click(object sender, EventArgs e)
        {
            AutoTask.fileFlag = true;
            AutoTask.uploadFlag = false;
            AutoTask.annouceFlag = false;

            this.button1.Enabled = false;
            this.button2.Enabled = true;          
        }
        //数据上传
        private void button2_Click(object sender, EventArgs e)
        {
            AutoTask.fileFlag = false;
            AutoTask.uploadFlag = true;
            AutoTask.annouceFlag = false;

            this.button1.Enabled = true;
            this.button2.Enabled = false;           
        }
        //公告
        private void button3_Click(object sender, EventArgs e)
        {
            AutoTask.fileFlag = false;
            AutoTask.uploadFlag = false;
            AutoTask.annouceFlag = true;

            this.button1.Enabled = true;
            this.button2.Enabled = true;            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.labelTicks.Text = this.dateTimePicker1.Value.Ticks.ToString();
        }
    }
}
