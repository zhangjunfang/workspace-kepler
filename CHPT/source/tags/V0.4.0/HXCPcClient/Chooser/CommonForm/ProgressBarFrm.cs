using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace HXCPcClient.Chooser.CommonForm
{
    public partial class ProgressBarFrm : Form
    {
        public  int MaxNum = 0;
        public static int CurrentValue = 0;
        private Thread ProgressThread = null;//声明一个执行委托的线程
        public ProgressBarFrm()
        {
            InitializeComponent();

        }
        private delegate void DoDataDelegate(object MaxValue);//传
        private void ProgressBarFrm_Load(object sender, EventArgs e)
        {
           
            ProgressThread = new Thread(DoDataProgress);//创建一个执行委托的线程
            ProgressThread.IsBackground = true;
            ProgressThread.Start(MaxNum);//启动线程
        }
        /// <summary>
        /// 循环显示导入进度
        /// </summary>
        /// <param name="StepValue"></param>
        private void DoDataProgress(object MaxValue)
        {
            if (ProgBar.InvokeRequired)
            {
                DoDataDelegate DoDelgt = DoDataProgress;
                ProgBar.Invoke(DoDelgt, MaxValue);
            }
            else
            {
                ProgBar.Maximum = (int)MaxValue;
                for (int i = 0; i < (int)MaxValue; i++)
                {
                    int CurPercent=0;
                    ProgBar.Value = i;
                    CurPercent = i * 100 / (int)MaxValue;
                    lblPercent.Text = CurPercent.ToString() + "%";
                    Thread.Sleep(200);
                    Application.DoEvents();
                }
                lblPercent.Text = "100%";
                ProgressThread.Abort();//终止线程
                this.Close();
            }
        }
    }
}
