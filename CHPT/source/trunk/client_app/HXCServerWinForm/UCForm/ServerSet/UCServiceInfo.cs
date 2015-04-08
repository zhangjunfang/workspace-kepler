using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXC_FuncUtility;
using ServiceStationClient.ComponentUI;

namespace HXCServerWinForm.UCForm
{
    /// <summary>
    /// Creater:yangtianshuai
    /// CreateTime:2014.10.29
    /// </summary>
    public partial class UCServiceInfo : UCBase
    {

        #region --构造函数
        public UCServiceInfo()
        {
            InitializeComponent();
            this.SetEvent += UCServiceInfo_SetEvent;
            this.OpRecordEvent += UCServiceInfo_OpRecordEvent;
        }       
        #endregion

        #region --操作按钮
        //服务器设置
        void UCServiceInfo_SetEvent(object sender, EventArgs e)
        {
            try
            {
                frmServiceSet serviceSet = new frmServiceSet();
                serviceSet.callbackHandler += new frmServiceSet.CallBackHandler(this.Init);
                serviceSet.ShowDialog();
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("服务器设置", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }
        //操作记录
        void UCServiceInfo_OpRecordEvent(object sender, EventArgs e)
        {
        }
        #endregion

        #region --初始化窗体
        private void UCServiceInfo_Load(object sender, EventArgs e)
        {
            base.SetOpButtonVisible(this.Name);//按钮权限-是否隐藏
            this.Init();
        }
        #endregion

        private void Init()
        {
            this.labelSeverIp.Text = GlobalStaticObj_Server.Instance.ServerIp;
            this.labelServerPort.Text = GlobalStaticObj_Server.Instance.ServerPort.ToString();
            this.labelFilePort.Text = GlobalStaticObj_Server.Instance.FilePort.ToString();
            this.labelPath.Text = GlobalStaticObj_Server.Instance.FilePath.ToString();
        }
    }
}
