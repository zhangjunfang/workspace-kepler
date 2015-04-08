using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXC_FuncUtility;

namespace HXCServerWinForm.UCForm
{
    /// <summary>
    /// Creater:yangtianshuai
    /// CreateTime:2014.10.29
    /// </summary>
    public partial class UCServiceInfo : UserControl
    {
        
        #region --构造函数
        public UCServiceInfo()
        {
            InitializeComponent();            
        }
        #endregion

        #region --操作按钮
        //服务器设置
        private void btnSet_Click(object sender, EventArgs e)
        {
            frmServiceSet serviceSet = new frmServiceSet();
            serviceSet.callbackHandler += new frmServiceSet.CallBackHandler(this.Init);
            serviceSet.ShowDialog();
        }
        //操作记录
        private void btnRecord_Click(object sender, EventArgs e)
        {

        }        
        #endregion       

        #region --初始化窗体
        private void UCServiceInfo_Load(object sender, EventArgs e)
        {
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
