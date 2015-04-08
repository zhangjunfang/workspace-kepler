using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ServiceStationClient.ComponentUI.MessageBox
{
    public partial class ProgressBarMsg : Form
    {
        public static string ProgressValue = string.Empty;//进度百分比
        public static int StepMaxValue = 0;//进度条显示最大值
        public static int CurrentValue = 0;//当前进度值

        public ProgressBarMsg()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 启动线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressBarMsg_Load(object sender, EventArgs e)
        {
            Thread ProBarThread = new Thread(DoData);
            ProBarThread.IsBackground = true;//指示该线程为后台线程
            ProBarThread.Start(StepMaxValue);//启动线程
        }
        private delegate void DoDataDelegate(object StepValue);
        /// <summary>  
        /// 进行循环  
        /// </summary>  
        /// <param name="StepValue">进度值</param>  
        private void DoData(object StepValue)
        {
            try
            {
                if (ExcelProgBar.InvokeRequired)
                {
                    DoDataDelegate DDelgt = DoData;
                    ExcelProgBar.Invoke(DDelgt, StepValue);
                }
                else
                {
                    ExcelProgBar.Maximum = (int)StepValue;//获取当前最大值

                    for (int i = 0; i <(int)StepValue; i++)
                    {
                        lblPercent.Text = ProgressValue;//获取进度百分比
                        ExcelProgBar.Value = CurrentValue;//获取当前进度
                        Application.DoEvents();
                    }
                    MessageBoxEx.Show("导出成功！");
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }  
  
        //private void ExcelProgBar_BindingContextChanged(object sender, EventArgs e)
        //{
            
                
        //        ExcelProgBar.Maximum = StepMaxValue;//获取进度条显示最大值
        //        ExcelProgBar.Step = StepValue;//获取进度值
        //        ExcelProgBar.PerformStep();//进度条显示位置
        //        ExcelProgBar.Value += ExcelProgBar.Step;
          

        //}


    }
}
