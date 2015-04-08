using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using ServiceStationClient.ComponentUI;
namespace  Utility.Common
{
    public partial class ProgressBarMsg : Form
    {
        public int MaxNum = 0;//最大记录行
        private Thread ProgressThread = null;//创建线程引用
        //public static int PercentMsg = 0;//进度百分比
        public ProgressBarMsg()
        {
            InitializeComponent();

        }
        private void ProgressBarMsg_Load(object sender, EventArgs e)
        {
            ProgressThread = new Thread(DoData);//创建执行委托的线程
            ProgressThread.IsBackground = true;
            ProgressThread.Start(MaxNum);
        }
        private delegate void DoDataDelegate(object StepValue);//创建委托
       /// <summary>
       /// 进度循环
       /// </summary>
       /// <param name="StepValue"></param>
        private void DoData(object StepValue)
        {
            try
            {
                if (ExcelProgBar.InvokeRequired)
                {
                    DoDataDelegate doDelgt = DoData;
                    ExcelProgBar.Invoke(doDelgt, StepValue);
                }
                else
                {
                    //循环执行当前进度
                    ExcelProgBar.Maximum = (int)StepValue;
                    for (int i = 0; i < (int)StepValue;i++ )
                    {
                        ExcelProgBar.Value = i;
                        int PercMsg = i * 100 / (int)StepValue;
                        lblPercent.Text = PercMsg.ToString()+"%";
                        Thread.Sleep(100);
                        Application.DoEvents();
                    }
                    //ProgressThread.Abort();//关闭线程
                    //Environment.Exit(0);
                    this.Close();               

                }

           
            
            
            }catch(Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

    }
}
