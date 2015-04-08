using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using System.Collections;
using HXC_FuncUtility;

namespace HXCServerWinForm.UCForm
{
    /// <summary>
    /// Creater:yangtianshuai
    /// Time:2014.10.29
    /// Function:Set Service Config
    /// LastUpdateTime:2014.10.29
    /// </summary>
    public partial class frmServiceSet : FormEx
    {
        #region --UI交互
        public delegate void CallBackHandler();
        public CallBackHandler callbackHandler;
        #endregion

        #region --构造函数
        public frmServiceSet()
        {
            InitializeComponent();
        }
        #endregion

        #region --操作按钮
        //确认
        private void btnYes_Click(object sender, EventArgs e)
        {
            try
            {
                errProvider.Clear();
                if (!this.ValidData())
                {
                    return;
                }
                bool flag = false;

                string tempStr = this.tbServerPort.Caption.Trim();
                flag = ConfigManager.SaveWcfConfig(ConfigConst.WcfData,
                    GlobalStaticObj_Server.Instance.ServerIp + ":" + tempStr, ConfigConst.ConfigPath);


                tempStr = this.tbFilePort.Caption.Trim();
                flag = ConfigManager.SaveWcfConfig(ConfigConst.WcfFile,
                    GlobalStaticObj_Server.Instance.ServerIp + ":" + tempStr, ConfigConst.ConfigPath);


                tempStr = this.tbSavePath.Caption.Trim();
                Hashtable ht = new Hashtable();
                ht.Add(ConfigConst.SavePath, tempStr);
                flag = ConfigManager.SaveConfig(ht, ConfigConst.ConfigPath);

                if (flag)
                {
                    int tempInt = 0;
                    if (int.TryParse(this.tbServerPort.Caption.Trim(), out tempInt))
                    {
                        GlobalStaticObj_Server.Instance.ServerPort = tempInt;
                    }
                    if (int.TryParse(this.tbFilePort.Caption.Trim(), out tempInt))
                    {
                        GlobalStaticObj_Server.Instance.FilePort = tempInt;
                    }
                    GlobalStaticObj_Server.Instance.FilePath = this.tbSavePath.Caption.Trim();
                    if (this.callbackHandler != null)
                    {
                        this.callbackHandler();
                    }
                    MessageBoxEx.Show("保存成功，重启软件后生效！");
                    this.Close();
                }
                else
                {
                    MessageBoxEx.Show("保存失败！");
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("服务器设置", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }
        //取消
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //浏览路径
        private void btnBrowser_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = false;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                this.tbSavePath.Caption = folderBrowserDialog.SelectedPath;
            }
        }
        #endregion

        #region --数据验证
        private bool ValidData()
        {
            errProvider.Clear();
            int port = 0;
            if (string.IsNullOrEmpty(this.tbServerPort.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(errProvider, this.tbServerPort, "请录入端口号!");
                return false;
            }
            else
            {
                if (!int.TryParse(tbServerPort.Caption.Trim(), out port))
                {
                    Utility.Common.Validator.SetError(errProvider, this.tbServerPort, "请录入数值!");
                    return false;
                }
                if (port > 65535)
                {
                    Utility.Common.Validator.SetError(errProvider, this.tbServerPort, "端口号不能大于65535!");
                    return false;
                }
            }

            if (string.IsNullOrEmpty(this.tbFilePort.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(errProvider, this.tbFilePort, "请录入端口号!");
                return false;
            }
            else
            {
                if (!int.TryParse(tbFilePort.Caption.Trim(), out port))
                {
                    Utility.Common.Validator.SetError(errProvider, this.tbFilePort, "请录入数值!");
                    return false;
                }
                if (port > 65535)
                {
                    Utility.Common.Validator.SetError(errProvider, this.tbFilePort, "端口号不能大于65535!");
                    return false;
                }
                if (tbFilePort.Caption.Trim()==tbServerPort.Caption.Trim())
                {
                     Utility.Common.Validator.SetError(errProvider, this.tbFilePort, "两个端口号不能一样!");
                    return false;
                }
            } 
            return true;
        }
        #endregion

        #region --窗体初始化
        private void frmServiceSet_Load(object sender, EventArgs e)
        {
            this.tbServerIp.Caption = GlobalStaticObj_Server.Instance.ServerIp.ToString();
            this.tbServerPort.Caption = GlobalStaticObj_Server.Instance.ServerPort.ToString();
            this.tbFilePort.Caption = GlobalStaticObj_Server.Instance.FilePort.ToString();
            this.tbSavePath.Caption = GlobalStaticObj_Server.Instance.FilePath;
        }
        #endregion
    }
}
