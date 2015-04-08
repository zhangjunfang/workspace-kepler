using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using ServiceStationClient.ComponentUI.TextBox;
using HXCPcClient.CommonClass;
using SYSModel;
using Utility.Common;
using FastReport.Export.Pdf;
using System.Drawing.Printing;
using System.Collections.ObjectModel;

namespace HXCPcClient.UCForm.BusinessAnalysis
{
    /// <summary>
    /// 报表基类
    /// </summary>
    public partial class UCReport : UCBase
    {
        #region 属性
        string styleObject = string.Empty;//报表对象
        string styleTitle = string.Empty;//默认报表标题
        protected DataTable dt = new DataTable();//报表数据
        //合并列
        protected Dictionary<string, List<string>> dicSpanRows = new Dictionary<string, List<string>>();
        /// <summary>
        /// 负数需要显示成红色的列名
        /// </summary>
        protected List<string> listNegative;
        /// <summary>
        /// 页面大小
        /// </summary>
        protected PaperSize paperSize = null;
        /// <summary>
        /// 金额样式
        /// </summary>
        protected DataGridViewCellStyle styleMoney;
        #endregion
        #region 事件
        //打印
        void UCReport_PrintEvent(object sender, EventArgs e)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBoxEx.Show("请查询出要打印的数据！");
                return;
            }
            string fileName = GetDefaultStyleFile();
            // create report instance
            FastReport.Report report = new FastReport.Report();
            if (!string.IsNullOrEmpty(fileName))
            {
                report.Load(fileName);
                report.RegisterData(dt, styleObject);
            }
            else
            {
                Report.FastReportEx reportEx = new Report.FastReportEx();
                reportEx.dicSpanRows = dicSpanRows;
                reportEx.dt = dt;
                reportEx.styleObject = styleObject;
                reportEx.styleTitle = styleTitle;
                report = reportEx.DefaultReport();
            }
            if (paperSize != null && report.Pages.Count > 0)
            {
                FastReport.ReportPage page = (FastReport.ReportPage)report.Pages[0];
                page.PaperHeight = paperSize.Height;
                page.PaperWidth = paperSize.Width;
            }
            report.Prepare();
            report.Print();
            report.Dispose();
        }
        //导出
        void UCReport_ExportEvent(object sender, EventArgs e)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBoxEx.Show("请查询出要导出的数据！");
                return;
            }
            string fileName = GetDefaultStyleFile();
            // create report instance
            FastReport.Report report = new FastReport.Report();
            if (!string.IsNullOrEmpty(fileName))
            {
                report.Load(fileName);
                report.RegisterData(dt, styleObject);
            }
            else
            {

                Report.FastReportEx reportEx = new Report.FastReportEx();
                reportEx.dicSpanRows = dicSpanRows;
                reportEx.dt = dt;
                reportEx.styleObject = styleObject;
                reportEx.styleTitle = styleTitle;
                report = reportEx.DefaultReport();
            }
            if (report == null)
            {
                return;
            }
            report.Prepare();
            report.FileName = styleTitle;
            //PDFExport export = new PDFExport();
            //export.Name = "name";
            //export.Title = styleTitle;
            //export.Export(report,styleTitle+".pdf");
            FastReport.Export.Csv.CSVExport csvExport = new FastReport.Export.Csv.CSVExport();
            csvExport.Export(report);
            //report.Export(export, styleTitle + ".pdf");
            report.Dispose();
        }
        //报表设置
        void UCReport_EditEvent(object sender, EventArgs e)
        {
            //报表设置
            frmReportSet frmSet = new frmReportSet();
            frmSet.setObject = styleObject;
            if (frmSet.ShowDialog() == DialogResult.OK)
            {
                SetReportSet();
            }
        }
        //报表预览
        void UCReport_ViewEvent(object sender, EventArgs e)
        {
            //预览
            Report.frmViewer frmView = new Report.frmViewer(styleObject, dt, styleTitle);
            frmView.dicSpanRows = dicSpanRows;
            frmView.ReportPaperSize = paperSize;
            frmView.ShowDialog();
        }
        //报表设计器
        void UCReport_SetEvent(object sender, EventArgs e)
        {
            //编辑
            Report.frmReportDesigner frmDesigner = new Report.frmReportDesigner(styleObject, styleTitle, dt, dicSpanRows);
            frmDesigner.ReportPaperSize = paperSize;
            frmDesigner.ShowDialog();
        }
        //清空查询
        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control ctr in pnlSearch.Controls)
            {
                if (ctr is TextBoxEx)
                {
                    TextBoxEx txt = (TextBoxEx)ctr;
                    txt.Caption = string.Empty;
                }
                else if (ctr is ComboBoxEx)
                {
                    ComboBoxEx cbo = (ComboBoxEx)ctr;
                    if (cbo.Items.Count > 0)
                    {
                        cbo.SelectedIndex = 0;
                    }
                    else
                    {
                        cbo.SelectedItem = null;
                    }
                }
                else if (ctr is TextChooser)
                {
                    TextChooser txtc = (TextChooser)ctr;
                    txtc.Text = string.Empty;
                    txtc.Tag = null;
                }
                else if (ctr is CheckBox)
                {
                    CheckBox cb = (CheckBox)ctr;
                    cb.Checked = false;
                }
                else if (ctr is DateInterval)
                {
                    DateInterval di = (DateInterval)ctr;
                    di.StartDate = DateTime.Now.ToString("yyyy-MM-01");
                    di.EndDate = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
        }
        #endregion
        /// <summary>
        /// 报表基类
        /// </summary>
        /// <param name="styleObject">报表对象</param>
        /// <param name="styleTitle">报表默认标题</param>
        public UCReport(string styleObject, string styleTitle)
        {
            InitializeComponent();
            this.styleObject = styleObject;
            this.styleTitle = styleTitle;
            styleMoney = new DataGridViewCellStyle();
            styleMoney.Alignment = DataGridViewContentAlignment.MiddleRight;
            styleMoney.Format = "N2";
            btnEdit.Caption = "设置";
            btnSet.Caption = "设计";
            this.SetEvent += new ClickHandler(UCReport_SetEvent);
            this.ViewEvent += new ClickHandler(UCReport_ViewEvent);
            this.EditEvent += new ClickHandler(UCReport_EditEvent);
            this.ExportEvent += new ClickHandler(UCReport_ExportEvent);
            this.PrintEvent += new ClickHandler(UCReport_PrintEvent);
            #region 设置
            //设置页面按钮可见性
            //var btnCols = new ObservableCollection<ButtonEx_sms>
            //    {
            //        btnExport,btnView,btnPrint,btnSet,btnEdit
            //    };
            //UIAssistants.SetUCBaseFuncationVisible(this, btnCols);
            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);  //设置查询按钮和清除按钮样式
            #endregion
        }
        public UCReport()
        {
            InitializeComponent();
        }
        //报表加载
        private void UCReport_Load(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(styleObject))
            {
                return;
            }
            foreach (Control ctr in pnlReport.Controls)
            {
                if (ctr is DataGridView)
                {
                    DataGridView dgv = (DataGridView)ctr;
                    #region 判断是否有当前用户、当前报表的设置，如果没有则创建
                    if (!DBHelper.IsExist("", "tb_report_set", string.Format("set_object='{0}' and set_user='{1}'", styleObject, GlobalStaticObj.UserID)))
                    {
                        List<SysSQLString> listSql = new List<SysSQLString>();
                        foreach (DataGridViewColumn dgvc in dgv.Columns)
                        {
                            if (dgvc.Visible)
                            {
                                SysSQLString sql = new SysSQLString();
                                sql.cmdType = CommandType.Text;
                                sql.sqlString = @"insert INTO tb_report_set (set_id,set_num,set_object,set_user,set_name,set_data_name,set_width,is_show,is_print) 
                                            values (@set_id,@set_num,@set_object,@set_user,@set_name,@set_data_name,@set_width,@is_show,@is_print)";
                                sql.Param = new Dictionary<string, string>();
                                sql.Param.Add("set_id", Guid.NewGuid().ToString());
                                sql.Param.Add("set_num", dgvc.Index.ToString());
                                sql.Param.Add("set_object", styleObject);
                                sql.Param.Add("set_user", GlobalStaticObj.UserID);
                                sql.Param.Add("set_name", dgvc.HeaderText);
                                sql.Param.Add("set_data_name", dgvc.DataPropertyName);
                                sql.Param.Add("set_width", dgvc.Width.ToString());
                                sql.Param.Add("is_show", "1");
                                sql.Param.Add("is_print", "1");
                                listSql.Add(sql);
                            }
                        }
                        if (listSql.Count > 0)
                        {
                            DBHelper.BatchExeSQLStringMultiByTrans("", listSql);
                        }
                    }
                    #endregion
                    dgv.CellFormatting += new DataGridViewCellFormattingEventHandler(dgv_CellFormatting);
                    break;
                }
            }
            SetReportSet();
        }
        #region 方法
        /// <summary>
        /// 获取默认打印样式文件
        /// </summary>
        /// <returns></returns>
        string GetDefaultStyleFile()
        {
            //判断是否有样式对象或数据
            //if (string.IsNullOrEmpty(styleObject) || dt == null || dt.Rows.Count == 0)
            //{
            //    return null;
            //}
            ////获取样式对象的打印样式列表
            //DataTable dtStyle = DBHelper.GetTable("", "tb_print_style", "style_url,style_name", string.Format("style_object='{0}'", styleObject), "", "");
            //if (dtStyle == null || dtStyle.Rows.Count == 0)
            //{
            //    return null;
            //}
            ////默认样式url
            //string defaultStyle = CommonCtrl.IsNullToString(dtStyle.Rows[0]["style_url"]);
            //if (defaultStyle.Length == 0)
            //{
            //    return null;
            //}
            //下载默认样式并得到本地样式路径
            string fileName = FileOperation.DownLoadFileByFile(styleObject, "Report");
            return fileName;
        }

        /// <summary>
        /// 报表设置
        /// </summary>
        /// <returns></returns>
        protected void SetReportSet()
        {
            //查报表的DataGridView
            foreach (Control ctr in pnlReport.Controls)
            {
                if (ctr is DataGridViewReport)
                {
                    DataGridViewReport dgv = (DataGridViewReport)ctr;
                    //查询用户对报表的设置
                    DataTable dt = DBHelper.GetTable("", "tb_report_set", "*", string.Format("set_object='{0}' and set_user='{1}'", styleObject, GlobalStaticObj.UserID), "", "");
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        return;
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        int columnIndex = Convert.ToInt32(dr["set_num"]);
                        dgv.Columns[columnIndex].Visible = CommonCtrl.IsNullToString(dr["is_show"]) == "1";//控制是否显示
                        dgv.Columns[columnIndex].Width = Convert.ToInt32(dr["set_width"]);//列宽
                    }
                    //设置列不能排序
                    foreach (DataGridViewColumn dgvc in dgv.Columns)
                    {
                        dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// 添加合并列
        /// </summary>
        /// <param name="spanHeader">合并列头</param>
        /// <param name="listSpanColumns">要合并的列</param>
        protected void AddSpanRows(string spanHeader, List<string> listSpanColumns)
        {
            dicSpanRows.Add(spanHeader, listSpanColumns);
        }

        //设置单元格样式，未负数的显示红色，并且不显示负号
        void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0 || listNegative == null || listNegative.Count == 0)
            {
                return;
            }
            DataGridView dgv = (DataGridView)sender;
            DataGridViewCell dgvc = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (listNegative.Contains(dgvc.OwningColumn.DataPropertyName))
            {
                string strNum = CommonCtrl.IsNullToString(e.Value);
                if (strNum.Length == 0)
                {
                    return;
                }
                decimal num = Convert.ToDecimal(strNum);
                //单元格值为负数
                if (num < 0)
                {
                    e.CellStyle.ForeColor = Color.Red;//字体为红色
                    e.Value = num * -1;//不显示负号
                }
            }
        }
        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns></returns>
        protected string GetWhere()
        {
            StringBuilder sbWhere = new StringBuilder();
            sbWhere.Append("1=1");
            foreach (Control ctr in pnlSearch.Controls)
            {
                if (ctr.Tag == null)
                {
                    continue;
                }
                if (ctr.Tag.ToString().Length == 0)
                {
                    continue;
                }
                if (ctr is TextBoxEx)
                {
                    TextBoxEx txt = (TextBoxEx)ctr;
                    string strTxt = txt.Caption.Trim();
                    if (strTxt.Length > 0)
                    {
                        sbWhere.AppendFormat(" and {0} like '%{1}%'", txt.Tag, strTxt);
                    }
                }
                else if (ctr is ComboBoxEx)
                {
                    ComboBoxEx cbo = (ComboBoxEx)ctr;
                    string strCbo = CommonCtrl.IsNullToString(cbo.SelectedValue);
                    if (strCbo.Length > 0)
                    {
                        sbWhere.AppendFormat(" and {0}='{1}'", cbo.Tag, strCbo);
                    }
                }
                else if (ctr is TextChooser)
                {
                    TextChooser txtc = (TextChooser)ctr;
                    string strTxtc = txtc.Text.Trim();
                    if (strTxtc.Length > 0)
                    {
                        sbWhere.AppendFormat(" and {0} like '%{1}%'", txtc.Tag, strTxtc);
                    }
                }
                else if (ctr is DateInterval)
                {
                    DateInterval di = (DateInterval)ctr;
                    if (!string.IsNullOrEmpty(di.StartDate) && !string.IsNullOrEmpty(di.EndDate))
                    {
                        sbWhere.AppendFormat(" and {0} between {1} and {2}", di.Tag,
                            Common.LocalDateTimeToUtcLong(Convert.ToDateTime(di.StartDate).Date),
                   Common.LocalDateTimeToUtcLong(Convert.ToDateTime(di.EndDate).Date.AddDays(1).AddMilliseconds(-1)));
                    }
                }
            }
            return sbWhere.ToString();
        }
        #endregion
    }
}
