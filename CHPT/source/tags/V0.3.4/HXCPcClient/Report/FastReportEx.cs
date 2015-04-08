using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HXCPcClient.CommonClass;
using FastReport;
using FastReport.Utils;
using System.Drawing;
using System.Drawing.Printing;

namespace HXCPcClient.Report
{
    /// <summary>
    /// 报表扩展，主要用来生成默认数据
    /// </summary>
    public class FastReportEx
    {
        #region 属性
        /// <summary>
        /// 报表数据
        /// </summary>
        public DataTable dt = new DataTable();
        /// <summary>
        /// 报表标题
        /// </summary>
        public string styleTitle = string.Empty;
        /// <summary>
        /// 报表对象
        /// </summary>
        public string styleObject = string.Empty;
        //合并列
        public Dictionary<string, List<string>> dicSpanRows = new Dictionary<string, List<string>>();
        //合并列范围
        Dictionary<string, RectangleF> dicSpanWidth = new Dictionary<string, RectangleF>();
        PaperSize paperSize = null;
        #endregion

        public PaperSize ReportPaperSize
        {
            get
            {
                return paperSize;
            }
            set
            {
                paperSize = value;
            }
        }
        /// <summary>
        /// 生成默认报表
        /// </summary>
        /// <returns></returns>
        public FastReport.Report DefaultReport()
        {

            FastReport.Report report = new FastReport.Report();
            //判断有报表数据，则注册数据
            if (dt != null && dt.Rows.Count > 0)
            {
                report.RegisterData(dt, styleObject);
            }
            else
            {
                return null;
            }
            //查询报表设置
            DataTable dtReportSet = DBHelper.GetTable("", "tb_report_set", "*", string.Format("set_object='{0}' and set_user='{1}'", styleObject, GlobalStaticObj.UserID), "", "order by set_num");
            if (dtReportSet == null || dtReportSet.Rows.Count == 0)
            {
                return null;
            }
            // enable the "Employees" table to use it in the report
            report.GetDataSource(styleObject).Enabled = true;

            // add report page
            ReportPage page = new ReportPage();
            if (paperSize != null)
            {
                page.PaperWidth = paperSize.Width;
                page.PaperHeight = paperSize.Height;
            }
            report.Pages.Add(page);
            // always give names to objects you create. You can use CreateUniqueName method to do this;
            // call it after the object is added to a report.
            page.CreateUniqueName();

            // create title band
            page.ReportTitle = new ReportTitleBand();
            // native FastReport unit is screen pixel, use conversion 
            page.ReportTitle.Height = Units.Centimeters * 1;
            page.ReportTitle.CreateUniqueName();

            // create title text
            TextObject titleText = new TextObject();
            titleText.Parent = page.ReportTitle;
            titleText.CreateUniqueName();
            //titleText.Bounds = new RectangleF(Units.Centimeters * 5, 0, Units.Centimeters * 10, Units.Centimeters * 1);
            titleText.Bounds = new RectangleF(0, 0, Units.Millimeters * page.PaperWidth, Units.Centimeters * 1);
            titleText.Font = new Font("Arial", 14, FontStyle.Bold);
            titleText.Text = styleTitle;
            titleText.HorzAlign = HorzAlign.Center;
            titleText.VertAlign = VertAlign.Center;
            //内容行高
            float rowHeight = Units.Centimeters * 1F;
            //标题行高，如果有合并列，怎行高价高
            float rowHeaderHeight = rowHeight;
            if (dicSpanRows != null && dicSpanRows.Count > 0)
            {
                rowHeaderHeight = Units.Centimeters * 1.5f;
            }
            page.PageHeader = new PageHeaderBand();
            page.PageHeader.Height = rowHeaderHeight;
            page.PageHeader.CreateUniqueName();
            #region 生成报表内容
            // create data band
            DataBand dataBand = new DataBand();
            page.Bands.Add(dataBand);
            dataBand.CreateUniqueName();
            dataBand.DataSource = report.GetDataSource(styleObject);
            dataBand.Height = rowHeight;
            float x = 0F;//x坐标

            foreach (DataRow dr in dtReportSet.Rows)
            {
                //判断是否要打印
                if (CommonCtrl.IsNullToString(dr["is_print"]) != "1")
                {
                    continue;
                }
                //列宽
                float columnWidth = Units.Centimeters * 2;
                //标题
                string headerName = string.Empty;
                //获取设置的列宽和标题
                float.TryParse(dr["set_width"].ToString(), out columnWidth);
                headerName = dr["set_name"].ToString();
                //生成标题
                TextObject txtHeader = new TextObject();
                txtHeader.Parent = page.PageHeader;
                txtHeader.CreateUniqueName();
                txtHeader.Text = headerName;
                txtHeader.HorzAlign = HorzAlign.Center;
                txtHeader.VertAlign = VertAlign.Center;
                //生成标题竖线
                LineObject lineHeaderVer = new LineObject();
                lineHeaderVer.Parent = page.PageHeader;
                //生成标题横线
                LineObject lineHeaderHor = new LineObject();
                lineHeaderHor.Parent = page.PageHeader;
                lineHeaderHor.Bounds = new RectangleF(x, 0, columnWidth, 1f);

                //数据源列名称
                string dataName = dr["set_data_name"].ToString();
                string spanName = IsContainDataColumn(dataName);
                //判断列是否是合并列
                if (spanName.Length > 0)
                {
                    #region 合并列标题
                    txtHeader.Bounds = new RectangleF(x, rowHeaderHeight / 2, columnWidth, rowHeaderHeight / 2);
                    lineHeaderVer.Bounds = new RectangleF(x, rowHeaderHeight / 2, 1F, rowHeaderHeight / 2);
                    //生成合并列的短竖线
                    LineObject lineHeaderHorSpan = new LineObject();
                    lineHeaderHorSpan.Parent = page.PageHeader;
                    lineHeaderHorSpan.Bounds = new RectangleF(x, rowHeaderHeight / 2, columnWidth, 1F);
                    //判断是否包含合并列头
                    if (dicSpanWidth.ContainsKey(spanName))
                    {
                        //增加合并列头的列宽
                        RectangleF rf = dicSpanWidth[spanName];
                        rf.Width += columnWidth;
                        dicSpanWidth[spanName] = rf;
                    }
                    else
                    {
                        //记录合并列头
                        RectangleF rf = new RectangleF();
                        rf.X = x;
                        rf.Y = 0;
                        rf.Width = columnWidth;
                        rf.Height = rowHeaderHeight / 2;
                        dicSpanWidth.Add(spanName, rf);
                    }
                    #endregion
                }
                else
                {
                    lineHeaderVer.Bounds = new RectangleF(x, 0, 1F, rowHeaderHeight);
                    txtHeader.Bounds = new RectangleF(x, 0, columnWidth, rowHeaderHeight);
                }
                // create two text objects with employee's name and birth date
                //生成内容
                TextObject empNameText = new TextObject();
                empNameText.Parent = dataBand;
                empNameText.CreateUniqueName();
                empNameText.Bounds = new RectangleF(x, 0, columnWidth, rowHeight);
                DataColumn dc = dt.Columns[dataName];
                empNameText.Text = string.Format("[{0}.{1}]", styleObject, dataName);
                //empNameText.HideZeros = true;
                if (dc != null && dc.DataType != typeof(string))
                {
                    empNameText.HorzAlign = HorzAlign.Right;
                }
                else
                {
                    empNameText.HorzAlign = HorzAlign.Center;
                }
                empNameText.VertAlign = VertAlign.Center;
                //生成内容列的竖线
                LineObject lineVertical = new LineObject();
                lineVertical.Parent = dataBand;
                lineVertical.Bounds = new RectangleF(x, 0, 1F, rowHeight);
                //生成内容列顶部的横线
                LineObject lineHorizontal = new LineObject();
                lineHorizontal.Parent = dataBand;
                lineHorizontal.Bounds = new RectangleF(x, 0, columnWidth, 1f);
                //生成内荣列底部的横线
                LineObject lineHorizontalD = new LineObject();
                lineHorizontalD.Parent = dataBand;
                lineHorizontalD.Bounds = new RectangleF(x, rowHeight, columnWidth, 1f);

                //x坐标增加当前列
                x += columnWidth;
            }

            //生成标题右边的竖线
            LineObject lineHeaderRightVer = new LineObject();
            lineHeaderRightVer.Parent = page.PageHeader;
            lineHeaderRightVer.Bounds = new RectangleF(x, 0, 1f, rowHeight);
            //生成内容右边的竖线
            LineObject lineRightVer = new LineObject();
            lineRightVer.Parent = dataBand;
            lineRightVer.Bounds = new RectangleF(x, 0, 1f, rowHeight);
            //生成合并列头
            foreach (string span in dicSpanWidth.Keys)
            {
                //合并列头
                TextObject txtSpan = new TextObject();
                txtSpan.Parent = page.PageHeader;
                txtSpan.CreateUniqueName();
                txtSpan.Bounds = dicSpanWidth[span];
                txtSpan.Text = span;
                txtSpan.HorzAlign = HorzAlign.Center;
                txtSpan.VertAlign = VertAlign.Center;
                //合并列头的竖线
                LineObject lineHeaderVer = new LineObject();
                lineHeaderVer.Parent = page.PageHeader;
                lineHeaderVer.Bounds = new RectangleF(txtSpan.Bounds.X, txtSpan.Bounds.Y, 1f, txtSpan.Bounds.Height);
            }
            #endregion
            return report;

        }

        /// <summary>
        /// 查询指定列的合并列头
        /// </summary>
        /// <param name="dataName">列名</param>
        /// <returns>合并列头</returns>
        string IsContainDataColumn(string dataName)
        {
            string spanName = string.Empty;
            foreach (string span in dicSpanRows.Keys)
            {
                if (dicSpanRows[span].Contains(dataName))
                {
                    spanName = span;
                    break;
                }
            }
            return spanName;
        }

        /// <summary>
        /// 默认打印样式
        /// </summary>
        public void LoadDefaultStyle(FastReport.Design.StandardDesigner.DesignerControl designerReport)
        {
            FastReport.Report rep = DefaultReport();
            if (rep == null)
            {
                return;
            }
            designerReport.Report = rep;
            designerReport.RefreshLayout();
        }

        /// <summary>
        /// 加载打印样式
        /// </summary>
        /// <param name="fileName"></param>
        public void LoadPrintStyle(string fileName, FastReport.Design.StandardDesigner.DesignerControl designerReport)
        {
            FastReport.Report rep = new FastReport.Report();
            rep.Load(fileName);
            if (dt != null && dt.Rows.Count > 0)
            {
                rep.RegisterData(dt, styleObject);
            }
            if (paperSize != null && rep.Pages.Count>0)
            {
                ReportPage page = (ReportPage)rep.Pages[0];
                page.PaperWidth = paperSize.Width;
                page.PaperHeight = paperSize.Height;
            }
            //designerReport.Report = rep;
            //designerReport.RefreshLayout();
            SetReport(rep, designerReport);
        }

        private void SetReport(FastReport.Report rep, FastReport.Design.StandardDesigner.DesignerControl ctr)
        {
            Action<FastReport.Report> setReportAction = rep0 => { ctr.Report = rep0; ctr.RefreshLayout(); };//Action<T>本身就是delegate类型，省掉了delegate的定义
            if (ctr.InvokeRequired)
            {
                ctr.Invoke(setReportAction, rep);
            }
            else
            {
                setReportAction(rep);
            }
        }

    }
}
