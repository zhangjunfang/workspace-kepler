using LogService;
using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Utility.Log;

namespace AutoUpdate
{
    public partial class frmDownload : Form
    {
       

        #region - 属性 -

        /// <summary>
        /// 无标题栏后，恢复任务栏属性
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                cp.Style = WS_CLIPCHILDREN | WS_MINIMIZEBOX | WS_SYSMENU;
                cp.ClassStyle = CS_DBLCLKS;

                return cp;
            }
        }

        #endregion

        #region 无标题栏后，恢复任务栏属性所用变量
        const int WS_CLIPCHILDREN = 0x2000000;
        const int WS_MINIMIZEBOX = 0x20000;
        const int WS_MAXIMIZEBOX = 0x10000;
        const int WS_SYSMENU = 0x80000;
        const int CS_DBLCLKS = 0x8;
        #endregion

        #region 窗体边框阴影效果变量申明

        const int CS_DropSHADOW = 0x20000;
        const int GCL_STYLE = (-26);
        //声明Win32 API
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern int GetClassLong(IntPtr hwnd, int nIndex);

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmDownload()
        {
            InitializeComponent();

            #region API函数加载，实现窗体边框阴影效果

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.ResizeRedraw |
                    ControlStyles.AllPaintingInWmPaint, true);
            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW); //API函数加载，实现窗体边框阴影效果

            #endregion
        }

        #endregion

        #region 变量

        public delegate void InvokeForm(object obj);

        /// <summary>
        /// 要升级到的版本号
        /// </summary>
        string serverVersion = "";

        /// <summary>
        /// 升级文件大小
        /// </summary>
        int serverSize = 0;


        string strUrl = string.Empty;


        #endregion

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
            try
            {
                hstprogressbar1.Maximum = Convert.ToInt32(obj);
            }
            catch (Exception ex)
            {
              CommonUtil.GlobalLogService.WriteLog("frmDownload.SetBarMax", ex.Message);
            }
        }

        private void SetBarValue(object obj)
        {
            try
            {
                hstprogressbar1.Value = (int)obj;
            }
            catch (Exception ex)
            {
               CommonUtil.GlobalLogService.WriteLog("frmDownload.SetBarValue", ex.Message);
            }
        }


        /// <summary>
        /// 窗体加载完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDownload_Load(object sender, EventArgs e)
        {
            #region 1、读取版本号
            if (System.IO.File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "\\upTemp\\uPversion.ini"))//查看主程序生成的升级版本号文件是否存在
            {
                try
                {
                    //如果存在，把版本号读取出来
                    StreamReader srConfig = new StreamReader(System.AppDomain.CurrentDomain.BaseDirectory + "\\upTemp\\uPversion.ini");
                    string strUpdateInfo = srConfig.ReadLine();
                    srConfig.Close();

                    //strUpdateInfo = new MonitorClient.BLL.clsDEC().DESDeCode(strUpdateInfo);//解密版本号
                    string[] arrUpdateInfo = strUpdateInfo.Split('|');

                    serverVersion = arrUpdateInfo[0];

                    serverSize = Convert.ToInt32(arrUpdateInfo[1]);

                    strUrl = arrUpdateInfo[2];
                    //throw new Exception();

                    #region 2、开启下载升级包线程
                    System.Threading.Thread UpdataThread = new System.Threading.Thread(new System.Threading.ThreadStart(StartDownload));
                    UpdataThread.IsBackground = true;
                    UpdataThread.Start();
                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "升级失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   CommonUtil.GlobalLogService.WriteLog("frmDownload.frmDownload_Load", ex.Message);
                    this.Close();

                }
            }
            else
            {
                MessageBox.Show("主程序生成的版本号文件不存在!", "升级失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
               CommonUtil.GlobalLogService.WriteLog("frmDownload.frmDownload_Load", "主程序生成的版本号文件不存在");
                this.Close();
            }
            #endregion


        }

        #region 下载升级文件

        /// <summary>
        /// 下载升级文件
        /// </summary>
        private void StartDownload()
        {
            try
            {
                this.Invoke(new InvokeForm(showBarText), "正在准备下载升级文件!");
                //clsLog.WriteLog(3, "正在准备下载升级文件!", 2);

                //string DownUrl = "http://192.168.100.195/build/KCPT/Release-records/cs/2.6.1/2.6.1_20130604_20/Product/update(10).zip";//MonitorClient.Model.clsSysConfig._actionUrl + "clientMonitor/downloadClientFile.action?versions=" + serverVersion + ".zip";//升级zip下载地址
                string thisLocalityUrl = System.AppDomain.CurrentDomain.BaseDirectory + "upTemp\\" + serverVersion.Replace(".", "") + ".zip";//本地存放Zip地址

                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(strUrl);
                //httpRequest.CookieContainer = MonitorClient.Model.clsSysConfig._actionCookie;
                HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                int _TOTALSIZE = 0;//当前下载的文件大小
                this.Invoke(new InvokeForm(SetBarMax), serverSize);//设置进度条最大值

                System.IO.Stream dataStream = httpResponse.GetResponseStream();
                byte[] buffer = new byte[8192];
                FileStream fs = new FileStream(thisLocalityUrl, FileMode.Create, FileAccess.Write);
                int size = 0;

                do
                {
                    size = dataStream.Read(buffer, 0, buffer.Length);//开始下载文件，一包文件的大小
                    _TOTALSIZE += size;//目前已下载大小
                    this.Invoke(new InvokeForm(SetBarValue), _TOTALSIZE);//目前已下载大小,设置进度条当前值

                    if (size > 0)
                        fs.Write(buffer, 0, size);//写入文件
                } while (size > 0);

                fs.Dispose();
                fs.Close();//关闭文件流
                httpResponse.Close();//下载完成 关闭对象

                this.Invoke(new InvokeForm(showBarText), "开始下载升级文件!");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "升级失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
               CommonUtil.GlobalLogService.WriteLog("frmDownload.StartDownload", ex.Message);
                this.Invoke(new InvokeForm(CloseThis), (object)"");
            }

            this.Invoke(new InvokeForm(ShowUpDataExe), (object)"");
        }
        #endregion

        private void CloseThis(object obj)
        {
            this.Close();
        }

        private void ShowUpDataExe(object obj)
        {
            System.IO.File.Copy(System.AppDomain.CurrentDomain.BaseDirectory + "ProgramUpdate.exe", System.AppDomain.CurrentDomain.BaseDirectory + "this_ProgramUpdate.exe");
            System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + "this_ProgramUpdate.exe");
            try
            {
                Application.ExitThread();
                Application.Exit();
            }
            catch (Exception ex)
            {
               CommonUtil.GlobalLogService.WriteLog("frmDownload.ShowUpDataExe", ex.Message);
            }
        }
    }
}
