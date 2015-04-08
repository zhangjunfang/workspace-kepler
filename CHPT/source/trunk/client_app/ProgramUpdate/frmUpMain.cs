using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.IO;
using System.Xml;
using System.Collections;
using System.Web;
using System.Security.Cryptography;
using ICSharpCode.SharpZipLib.Zip;
using System.Diagnostics;
using ICSharpCode.SharpZipLib.Core;
using ProgramUpdate;

namespace ProgramUpdate
{
    public partial class frmUpMain : Form
    {
        /// <summary> 启动程序名
        /// </summary>
        public string appName = string.Empty;

        public frmUpMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 要升级到的版本号
        /// </summary>
        string serverVersion = "";

        public delegate void InvokeForm(object obj);

        private void showBarText(object obj)
        {
            hstprogressbar1.Caption = obj.ToString() + "   ";
        }

        private void CloseMain(string str)
        {
            this.Close();
        }

        private void SetBarMax(object obj)
        {
            hstprogressbar1.Maximum = Convert.ToInt32(obj);
        }

        private void SetBarValue(object obj)
        {
            hstprogressbar1.Value = (int)obj;
        }

        private string updateFileUrl = string.Empty;
        #region 窗体加载事件（获取要升级的版本号）
        /// <summary>
        /// 窗体加载事件（获取要升级的版本号）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUpMain_Load(object sender, EventArgs e)
        {

            #region 1、读取版本号
            if (System.IO.File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "\\upTemp\\uPversion.ini"))//查看主程序生成的升级版本号文件是否存在
            {
                try
                {
                    //如果存在，把版本号读取出来
                    StreamReader srConfig = new StreamReader(System.AppDomain.CurrentDomain.BaseDirectory + "\\upTemp\\uPversion.ini");
                    serverVersion = srConfig.ReadLine();
                    srConfig.Close();

                    string result = DESDeCode(serverVersion);
                    string[] resultArr = result.Split('|');
                    updateFileUrl = resultArr[2];
                    serverVersion = resultArr[0];
                    appName = resultArr[3];
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(4, "frmUpMain_Load：" + ex.Message, 2);
                    return;
                }
            }
            else
            {
                MessageBox.Show("主程序生成的版本号文件不存在!请手动启动主程序!", "升级失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogHelper.WriteLog(4, "主程序生成的版本号文件不存在!", 2);
                this.Close();
            }
            #endregion

            //MessageBox.Show("升级前请关闭CS客户端。");
            frmUpDateInfo formUpDateInfo = new frmUpDateInfo();
            formUpDateInfo.UpdateFileUrl = updateFileUrl;
            formUpDateInfo.ShowDialog();
            #region 3、开启解压线程
            System.Threading.Thread thisThread = new System.Threading.Thread(new System.Threading.ThreadStart(UnZipFile));
            thisThread.IsBackground = true;
            thisThread.Start();
            #endregion
        }
        #endregion

        #region 解压升级zip文件
        /// <summary>
        /// 解压升级zip文件
        /// </summary>
        private void UnZipFile()
        {
            try
            {
                string zipFilePath = serverVersion.Replace(".", "");//升级包zip文件名称
                zipFilePath = System.AppDomain.CurrentDomain.BaseDirectory + "upTemp\\" + zipFilePath + ".zip";
                if (!File.Exists(zipFilePath))
                {
                    MessageBox.Show("无法找到升级文件!", "升级失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LogHelper.WriteLog(4, "UnZipFile  升级失败!无法找到升级文件!", 2);
                    this.Invoke(new InvokeForm(CloseThis), (object)"");
                }

                ArrayList fileArr = new ArrayList();

                using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipFilePath)))
                {
                    this.Invoke(new InvokeForm(SetBarValue), 0);//设置进度条当前值
                    //this.Invoke(new InvokeForm(showBarText), "正在解压升级文件!");
                    this.Invoke(new InvokeForm(showBarText), "正在准备解压升级文件!");
                    //this.Invoke(new InvokeForm(SetBarMax), s.Length);//设置进度条最大值

                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        string directoryName = Path.GetDirectoryName(theEntry.Name);
                        string fileName = Path.GetFileName(theEntry.Name);

                        fileArr.Add(fileName);

                        if ( fileName == "hstwintoolbox.dll" || fileName == "Newtonsoft.Json.dll" || fileName == "ICSharpCode.SharpZipLib.dll")
                        {
                            //fileName == "ProgramUpdate.exe" ||
                            continue;
                        }

                        if (directoryName.Length > 0)
                        {
                            Directory.CreateDirectory(directoryName);
                        }

                        if (fileName != String.Empty)
                        {
                            using (FileStream streamWriter = File.Create(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + theEntry.Name))
                            {
                                int size = 2048;
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    size = s.Read(data, 0, data.Length);
                                    if (size > 0)
                                    {
                                        streamWriter.Write(data, 0, size);
                                        //this.Invoke(new InvokeForm(SetBarValue), thisValue);//设置进度条当前值
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                this.Invoke(new InvokeForm(SetBarMax), fileArr.Count);//设置进度条最大值

                for (int i = 1; i < fileArr.Count; i++)
                {
                    this.Invoke(new InvokeForm(showBarText), "正在解压 " + fileArr[i].ToString());
                    this.Invoke(new InvokeForm(SetBarValue), i + 1);//设置进度条当前值
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "升级失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogHelper.WriteLog(4, "UnZipFile " + ex.Message, 2);
                this.Invoke(new InvokeForm(CloseThis), (object)"");
            }

            DelExe();
        }
        #endregion

        #region 删除升级文件
        /// <summary>
        /// 删除升级文件
        /// </summary>
        /// <returns></returns>
        private void DelExe()
        {
            try
            {
                Directory.Delete(System.AppDomain.CurrentDomain.BaseDirectory + "upTemp", true);
            }
            catch (Exception ex)
            {
                this.Invoke(new InvokeForm(showBarText), ex.Message);
                LogHelper.WriteLog(4, "删除升级文件失败!" + ex.Message, 2);
            }

            StartExe();
        }
        #endregion

        #region 启动主程序
        /// <summary>
        /// 启动主程序
        /// </summary>
        private void StartExe()
        {
            try
            {
                this.Invoke(new InvokeForm(showBarText), "升级完成!正在启动主程序!");
                LogHelper.WriteLog(3, "升级完成!正在启动主程序!", 2);

                //System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + "ctfo.exe");
                this.Invoke(new InvokeForm(ShowMainExe), (object)"");

                //this.Close();
            }
            catch (Exception ex)
            {
                this.Invoke(new InvokeForm(showBarText), "启动主程序失败!请联系管理员!");
                LogHelper.WriteLog(4, "启动主程序失败!" + ex.Message, 2);
                this.Invoke(new InvokeForm(CloseThis), (object)"");
            }
        }
        #endregion

        #region DESDeCode DES解密

        /// <summary>
        /// 对字符串进行解密操作
        /// </summary>
        /// <param name="pToDecrypt">要解密的加密字符串</param>
        /// <returns></returns>
        private string DESDeCode(string pToDecrypt)
        {
            return DESDeCode(pToDecrypt, "82368236");
        }

        /// <summary>
        /// 对字符串进行解密操作
        /// </summary>
        /// <param name="pToDecrypt">要解密的加密字符串</param>
        /// <param name="sKey">解密密钥</param>
        /// <returns></returns>
        private string DESDeCode(string pToDecrypt, string sKey)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
                for (int x = 0; x < pToDecrypt.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }

                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                StringBuilder ret = new StringBuilder();

                return System.Text.Encoding.UTF8.GetString(ms.ToArray());
            }
            catch
            {
                return pToDecrypt;
            }
        }
        #endregion

        private void CloseThis(object obj)
        {
            this.Close();
        }

        private void ShowMainExe(object obj)
        {
            //启动主程序
            System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + appName);
            this.Close();
        }
    }
}
