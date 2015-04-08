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
            errorProvider1.Clear();
            if (!this.ValidData())
            {
                return;
            }
            bool flag = false;

            string tempStr = this.tbServerPort.Caption.Trim();
            //if (tempStr != GlobalStaticObj_Server.Instance.ServerPort.ToString())
            //{
                flag = ConfigManager.SaveWcfConfig(ConfigConst.WcfData,
                    GlobalStaticObj_Server.Instance.ServerIp + ":" + tempStr, ConfigConst.ConfigPath);
            //}

            tempStr = this.tbFilePort.Caption.Trim();
            //if (tempStr != GlobalStaticObj_Server.Instance.FilePort.ToString())
            //{
                flag = ConfigManager.SaveWcfConfig(ConfigConst.WcfFile,
                    GlobalStaticObj_Server.Instance.FilePort + ":" + tempStr, ConfigConst.ConfigPath);
            //}           

            tempStr = this.tbSavePath.Caption.Trim();
            //if (tempStr != GlobalStaticObj_Server.Instance.FilePath.ToString())
            //{
                Hashtable ht = new Hashtable();
                ht.Add(ConfigConst.SavePath, tempStr);
                flag = ConfigManager.SaveConfig(ht, ConfigConst.ConfigPath);
            //}

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
                MessageBoxEx.Show("保存成功！");        
                this.Close();
            }
            else
            {
                MessageBoxEx.Show("保存失败！");
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
            errorProvider1.Clear();           
            if (string.IsNullOrEmpty(this.tbFilePort.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(errorProvider1, this.tbFilePort, "文件传输端口不能为空!");
                return false;
            }
            if (string.IsNullOrEmpty(this.tbServerPort.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(errorProvider1, this.tbServerPort, "数据传输端口不能为空!");
                return false;
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
