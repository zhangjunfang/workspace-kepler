using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using HXCPcClient.CommonClass;

namespace HXCPcClient.Report
{
    public partial class frmReportStyle : FormEx
    {
        /// <summary>
        /// 样式名称
        /// </summary>
        public string styleName = string.Empty;
        string styleObject = string.Empty;//报表对象
        /// <summary>
        /// 用于让用户输入新增的报表样式名称
        /// </summary>
        /// <param name="styleObject">报表对象</param>
        public frmReportStyle(string styleObject)
        {
            InitializeComponent();
            this.styleObject = styleObject;
            pnlContainer.BackColor = ColorTranslator.FromHtml("#eff8ff");
        }
        //关闭
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //确定
        private void btnOK_Click(object sender, EventArgs e)
        {
            styleName = txtStyleName.Caption.Trim();
            if (styleName.Length == 0)
            {
                MessageBoxEx.Show("请输入样式名称！");
                txtStyleName.Focus();
                return;
            }
            //不能是默认样式
            if (styleName == "默认样式")
            {
                MessageBoxEx.Show("不能是默认样式！");
                txtStyleName.Focus();
                return;
            }
            //样式名称不能重复
            if(DBHelper.IsExist("","tb_print_style",string.Format("style_object='{0}' and style_name='{1}'",styleObject,styleName)))
            {
                MessageBoxEx.Show("样式名称已存在！");
                txtStyleName.Focus();
                return;
            }
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
