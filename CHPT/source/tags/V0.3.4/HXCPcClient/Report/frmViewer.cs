using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using FastReport;
using System.IO;
using HXCPcClient.CommonClass;
using FastReport.Utils;
using FastReport.Format;
using System.Drawing.Printing;

namespace HXCPcClient.Report
{
    public partial class frmViewer : FormEx
    {
        #region 属性
        string styleObject = string.Empty;//报表对象
        string styleTitle = string.Empty;//默认报表标题
        DataTable dt = new DataTable();//报表数据
        public Dictionary<string, List<string>> dicSpanRows = new Dictionary<string, List<string>>();//合并列
        FastReportEx reportEx = new FastReportEx();
        PaperSize paperSize = null;
        /// <summary>
        /// 报表大小
        /// </summary>
        public PaperSize ReportPaperSize
        {
            get
            {
                return paperSize;
            }
            set
            {
                paperSize = value;
                reportEx.ReportPaperSize = value;
            }
        }
        #endregion
        /// <summary>
        /// 报表预览
        /// </summary>
        /// <param name="styleObject">报表对象</param>
        /// <param name="dt">报表数据</param>
        /// <param name="styleTitle">默认报表标题</param>
        public frmViewer(string styleObject, DataTable dt, string styleTitle)
        {
            InitializeComponent();
            this.styleObject = styleObject;
            this.styleTitle = styleTitle;
            this.dt = dt;
            pnlContainer.BackColor = ColorTranslator.FromHtml("#eff8ff");
        }
        public frmViewer()
        {
            InitializeComponent();
        }
        //加载
        private void frmViewer_Load(object sender, EventArgs e)
        {
            previewReport.ToolBar.Items[1].Visible = false;
            previewReport.ToolBar.Items[8].Visible = false;
            if (string.IsNullOrEmpty(styleObject))
            {
                return;
            }
            BindStyle();

        }
        /// <summary>
        /// 绑定样式，每个报表必须有一个默认报表
        /// </summary>
        void BindStyle()
        {
            DataTable dtStyle = DBHelper.GetTable("", "tb_print_style", "style_url,style_name", string.Format("style_object='{0}'", styleObject), "", "");
            //样式内容为空，怎手动创建列
            if (dtStyle == null || dtStyle.Rows.Count == 0)
            {
                if (dtStyle == null)
                {
                    dtStyle = new DataTable();
                }
                dtStyle.Columns.Add("style_url");
                dtStyle.Columns.Add("style_name");
            }
            //添加默认样式
            DataRow dr = dtStyle.NewRow();
            dr["style_url"] = styleObject;
            dr["style_name"] = "默认样式";
            dtStyle.Rows.InsertAt(dr, 0);
            cboPrintStyle.ValueMember = "style_url";
            cboPrintStyle.DisplayMember = "style_name";
            cboPrintStyle.DataSource = dtStyle;
        }
        //选择预览样式
        private void cboPrintStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            string printStyle = CommonCtrl.IsNullToString(cboPrintStyle.SelectedValue);
            FastReport.Report rep;
            //下载打印样式
            string fileName = FileOperation.DownLoadFileByFile(cboPrintStyle.SelectedValue.ToString(), "Report");
            //如果没找到，则用默认样式
            if (string.IsNullOrEmpty(fileName))
            {
                reportEx.styleObject = styleObject;
                reportEx.dt = dt;
                reportEx.dicSpanRows = dicSpanRows;
                reportEx.styleTitle = styleTitle;
                rep = reportEx.DefaultReport();
                if (rep == null)
                {
                    return;
                }
            }
            else
            {
                rep = new FastReport.Report();
                rep.Load(fileName);
                if (dt != null && dt.Rows.Count > 0)
                {
                    rep.RegisterData(dt, styleObject);
                }
                if (paperSize != null && rep.Pages.Count > 0)
                {
                    ReportPage page = (ReportPage)rep.Pages[0];
                    page.PaperWidth = paperSize.Width;
                    page.PaperHeight = paperSize.Height;
                }
            }
            rep.Preview = previewReport;
            rep.Prepare();
            rep.ShowPrepared();
        }

    }
}
