using System;
using System.Drawing;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;

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
            this.tbServerPort.InnerTextBox.BackColor = Color.White;
            this.tbFilePort.InnerTextBox.BackColor = Color.White;
            this.tbFileServerIp.InnerTextBox.BackColor = Color.White;
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
            this.Hide();
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

            bool dataFlag = false;
            bool fileFlag = false;
            if (ConfigManager.SaveWcfConfig(ConfigConst.WcfData,
                        this.tbServerIp.Caption.Trim() + ":" + this.tbServerPort.Caption.Trim()))
            {
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

            if (dataFlag && fileFlag)
            {
                if (this.RefreshDataStart != null)
                {
                    this.RefreshDataStart();
                }
                FormMessgeBox.ShowMsg(this, "数据、文件通讯服务连接测试成功！", this.panelTop.BackColor);
                this.Close();
            }
            else
            {
                if (dataFlag)
                {
                    FormMessgeBox.ShowMsg(this, "数据通讯服务连接测试成功,文件通讯服务连接测试失败！", this.panelTop.BackColor);
                    if (this.RefreshDataStart != null)
                    {
                        this.RefreshDataStart();
                    }
                }
                else if (fileFlag)
                {
                    FormMessgeBox.ShowMsg(this, "数据通讯服务连接测试失败,文件通讯服务连接测试成功！", this.panelTop.BackColor);
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
        
    }
}