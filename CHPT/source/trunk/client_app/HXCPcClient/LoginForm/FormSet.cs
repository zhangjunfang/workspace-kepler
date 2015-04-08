using System;
using System.Drawing;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using System.Net.NetworkInformation;
using System.Configuration;
using System.ComponentModel;

namespace HXCPcClient.LoginForms
{
    public partial class FormSet : Form
    {
        #region --回调更新事件
        public delegate void RefreshData();
        public RefreshData RefreshDataStart;
        #endregion

        #region --构造函数
        public FormSet()
        {
            InitializeComponent();
            this.tbServerIp.InnerTextBox.BackColor = Color.White;
            tbServerIp.InnerTextBox.Top = 0;
            this.tbServerPort.InnerTextBox.BackColor = Color.White;
            tbServerPort.InnerTextBox.Top = 0;
            this.tbFilePort.InnerTextBox.BackColor = Color.White;
            tbFilePort.InnerTextBox.Top = 0;
            this.tbFileServerIp.InnerTextBox.BackColor = Color.White;
            tbFileServerIp.InnerTextBox.Top = 0;
        }
        #endregion

        #region --关闭按钮
        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbClose_MouseEnter(object sender, EventArgs e)
        {
            this.pbClose.BackgroundImage = Properties.Resources.close_d;
        }

        private void pbClose_MouseLeave(object sender, EventArgs e)
        {
            this.pbClose.BackgroundImage = Properties.Resources.close_n;
        }
        #endregion

        #region --最小化
        private void pbMin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbMin_MouseEnter(object sender, EventArgs e)
        {
            this.pbMin.BackgroundImage = Properties.Resources.small_d;
        }

        private void pbMin_MouseLeave(object sender, EventArgs e)
        {
            this.pbMin.BackgroundImage = Properties.Resources.small_n;
        }
        #endregion

        #region --确认按钮
        private void panelYes_Click(object sender, EventArgs e)
        {
            this.errorProvider.Clear();

            if (string.IsNullOrEmpty(this.tbServerIp.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(this.errorProvider, this.tbServerIp, "数据服务器Ip不能为空!");
                return;
            }

            if (string.IsNullOrEmpty(this.tbServerPort.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(this.errorProvider, this.tbServerPort, "数据服务端口不能为空!");
                return;
            }
            int dataPort = 0;
            if (!int.TryParse(tbServerPort.Caption.Trim(), out dataPort))
            {
                FormMessgeBox.ShowMsg(this, "数据服务端口只能录入数字！", this.panelTop.BackColor);
                return;
            }
            if (!InetetTest(this.tbServerIp.Caption.Trim(), dataPort, 100))
            {
                FormMessgeBox.ShowMsg(this, "数据服务器地址无法连接！", this.panelTop.BackColor);
                return;
            }
            if (string.IsNullOrEmpty(this.tbFileServerIp.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(this.errorProvider, this.tbFileServerIp, "文件服务器Ip不能为空!");
                return;
            }

            if (string.IsNullOrEmpty(this.tbFilePort.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(this.errorProvider, this.tbFilePort, "文件服务端口不能为空!");
                return;
            }

            int filePort = 0;
            if (!int.TryParse(tbFilePort.Caption.Trim(), out filePort))
            {
                FormMessgeBox.ShowMsg(this, "文件服务端口只能录入数字！", this.panelTop.BackColor);
                return;
            }
            if (!InetetTest(this.tbFileServerIp.Caption.Trim(), filePort, 100))
            {
                FormMessgeBox.ShowMsg(this, "文件服务器地址无法连接！", this.panelTop.BackColor);
                return;
            }



            BackgroundWorker Worker = new BackgroundWorker();
            Worker.WorkerReportsProgress = false;
            Worker.WorkerSupportsCancellation = true;
            Worker.DoWork += Worker_DoWork;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;


            Worker.RunWorkerAsync();
            

            
        }

        void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            bool dataFlag = false;
            bool fileFlag = false;
            if (ConfigManager.SaveWcfConfig(ConfigConst.WcfData,
                        this.tbServerIp.Caption.Trim() + ":" + this.tbServerPort.Caption.Trim()))
            {
                ConfigurationManager.RefreshSection("system.serviceModel/behaviors");
                ConfigurationManager.RefreshSection("system.serviceModel/bindings");
                ConfigurationManager.RefreshSection("system.serviceModel/client");
                ConfigurationManager.RefreshSection("system.serviceModel/services");

                GlobalStaticObj.channelFactory = null;

                if (WCFClientProxy.TestDataProxy())
                {

                    dataFlag = true;
                    GlobalStaticObj.DataServerIp = this.tbServerIp.Caption.Trim();
                    GlobalStaticObj.DataPort = int.Parse(this.tbServerPort.Caption.Trim());
                }
                else
                {
                    this.tbServerIp.Caption = GlobalStaticObj.DataServerIp;
                    this.tbServerPort.Caption = GlobalStaticObj.DataPort.ToString();
                    ConfigManager.SaveWcfConfig(ConfigConst.WcfData,
                        this.tbServerIp.Caption.Trim() + ":" + this.tbServerPort.Caption.Trim());
                }
            }

            if (ConfigManager.SaveWcfConfig(ConfigConst.WcfFile,
                        this.tbFileServerIp.Caption.Trim() + ":" + this.tbFilePort.Caption.Trim()))
            {
                ConfigurationManager.RefreshSection("system.serviceModel/behaviors");
                ConfigurationManager.RefreshSection("system.serviceModel/bindings");
                ConfigurationManager.RefreshSection("system.serviceModel/client");
                ConfigurationManager.RefreshSection("system.serviceModel/services");

                GlobalStaticObj.channelFactoryFile = null;

                if (WCFClientProxy.TestFileProxy())
                {
                    fileFlag = true;
                    GlobalStaticObj.FileServerIp = this.tbFileServerIp.Caption.Trim();
                    GlobalStaticObj.FilePort = int.Parse(this.tbFilePort.Caption.Trim());
                }
                else
                {
                    this.tbFileServerIp.Caption = GlobalStaticObj.FileServerIp;
                    this.tbFilePort.Caption = GlobalStaticObj.FilePort.ToString();
                    //恢复原来的连接
                    ConfigManager.SaveWcfConfig(ConfigConst.WcfFile,
                        this.tbFileServerIp.Caption.Trim() + ":" + this.tbFilePort.Caption.Trim());
                }
            }

            Tuple<bool, bool> E = new Tuple<bool, bool>(dataFlag, fileFlag);
            e.Result = E;
        }

        void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Tuple<bool, bool> E = e.Result as Tuple<bool, bool>;
            bool dataFlag = E.Item1;
            bool fileFlag = E.Item2;

            if (dataFlag && fileFlag)
            {
                FormMessgeBox.ShowMsg(this, "数据、文件通讯服务连接测试成功！", this.panelTop.BackColor);
                this.Close();
            }
            else
            {
                if (dataFlag)
                {
                    FormMessgeBox.ShowMsg(this, "数据通讯服务连接测试成功，\r\n文件通讯服务连接测试失败！", this.panelTop.BackColor);
                }
                else if (fileFlag)
                {
                    FormMessgeBox.ShowMsg(this, "数据通讯服务连接测试失败,\r\n文件通讯服务连接测试成功！", this.panelTop.BackColor);
                }
                else
                {
                    FormMessgeBox.ShowMsg(this, "数据、文件通讯服务连接测试失败！", this.panelTop.BackColor);
                }
            }
        }

        private void panelYes_MouseEnter(object sender, EventArgs e)
        {
            this.panelYes.BackColor = Color.FromArgb(23, 146, 219);
        }

        private void panelYes_MouseLeave(object sender, EventArgs e)
        {
            this.panelYes.BackColor = Color.FromArgb(20, 129, 194);
        }
        #endregion

        #region --取消按钮
        private void panelCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelCancel_MouseEnter(object sender, EventArgs e)
        {

        }

        private void panelCancel_MouseLeave(object sender, EventArgs e)
        {

        }
        #endregion

        #region --窗体初始化
        private void FormSet_Load(object sender, EventArgs e)
        {
            this.tbServerIp.Caption = GlobalStaticObj.DataServerIp;
            this.tbServerPort.Caption = GlobalStaticObj.DataPort.ToString();

            this.tbFileServerIp.Caption = GlobalStaticObj.FileServerIp;
            this.tbFilePort.Caption = GlobalStaticObj.FilePort.ToString();
        }
        #endregion

        /// <summary>
        /// 网络测试
        /// </summary>
        /// <param name="remoteIP">远程服务器IP</param>
        /// <param name="time">超时时间</param>
        /// <returns></returns>
        public static bool InetetTest(string remoteIP, int port, int time)
        {
            try
            {
                System.Net.Sockets.TcpClient tcpClient = new System.Net.Sockets.TcpClient();
                tcpClient.SendTimeout = time;
                tcpClient.ReceiveTimeout = time;
                tcpClient.Connect(remoteIP, port);            
                return true;
            }
            catch (Exception ex)
            {
                GlobalStaticObj.GlobalLogService.WriteLog("登录测试连接", ex);
                return false;
            }
        }

        #region --关闭窗体事件
        private void FormSet_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.RefreshDataStart != null)
            {
                this.RefreshDataStart();
            }
        }
        #endregion

        #region --快捷键
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                this.panelYes_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
    }
}