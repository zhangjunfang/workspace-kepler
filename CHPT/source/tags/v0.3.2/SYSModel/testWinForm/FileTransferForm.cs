using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace testWinForm
{
    public partial class FileTransferForm : Form
    {
        public FileTransferForm()
        {
            InitializeComponent();
        }

        private void btnPicTransfer_Click(object sender, EventArgs e)
        {
            string FilePath = Application.StartupPath + "\\UpLoadFiles\\";
            #region 图片
            string PicPath = FilePath + "慧修车windows服务.txt";
            HXCFileTransferCache_Client.FileTransferClient.ClientReadFile(PicPath, Guid.NewGuid().ToString("N"), "\\testPic\\001\\");
            #endregion
        }

        private void FileTransferForm_Load(object sender, EventArgs e)
        {
            HXCFileTransferCache_Client.FileTransferClient.OnUpLoadFile += new HXCFileTransferCache_Client.UpLoadFileHandler<HXCFileTransferCache_Client.UpLoadFileEventArgs>(Client_OnUpLoadFile);
        }

        private void Client_OnUpLoadFile(object sender, HXCFileTransferCache_Client.UpLoadFileEventArgs e)
        {
            //txtMsg.AppendText(e.Guid + e.SendFileObj.FileName + e.SendFileObj.FileSize + e.SendFileObj.ReadSize + e.SendFileObj.SendSize + e.SendFileObj.Status.ToString());
        SetTextBoxValue("GUID:"+e.Guid + e.SendFileObj.FileName + e.SendFileObj.FileSize + e.SendFileObj.ReadSize + e.SendFileObj.SendSize + e.SendFileObj.Status.ToString(),txtMsg);
        }

        private void SetTextBoxValue(string value, TextBox ctr)
        {
            Action<string> setValueAction = text => ctr.AppendText(text);//Action<T>本身就是delegate类型，省掉了delegate的定义
            if (ctr.InvokeRequired)
            {
                ctr.Invoke(setValueAction, value);
            }
            else
            {
                setValueAction(value);
            }
        }
    }
}
