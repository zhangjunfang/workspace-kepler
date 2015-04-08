using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Data;
using System.IO;

namespace HXCPcClient
{
    /// <summary>
    /// 业务明细打印模块
    /// </summary>
    public class BusinessDetailPrint
    {
        /// <summary>
        /// 报表标题
        /// </summary>
        string printTitle = string.Empty;
        /// <summary>
        /// 报表对象
        /// </summary>
        string printObject = string.Empty;
        /// <summary>
        /// 报表名称
        /// </summary>
        string reportFileName = string.Empty;

        /// <summary>
        /// 报表标题
        /// </summary>
        public string PrintTitle
        {
            get { return printTitle; }
            set
            {
                printTitle = value;
                reportFileName = string.Format("BusinessPrint\\{0}.frx", value);
            }
        }

        /// <summary>
        /// 业务明细打印
        /// </summary>
        /// <param name="printTitle">打印的标题</param>
        public BusinessDetailPrint(string printTitle)
        {
            this.printTitle = printTitle;
            reportFileName = string.Format("BusinessPrint\\{0}.frx", printTitle);
        }

        /// <summary>
        /// 生成明细报表
        /// </summary>
        /// <param name="dt">打印数据</param>
        /// <returns></returns>
        public FastReport.Report GetDetailReport(Dictionary<string, DataTable> dic)
        {
            if (!Directory.Exists("BusinessPrint"))
            {
                Directory.CreateDirectory("BusinessPrint");
            }
            
            FastReport.Report report = new FastReport.Report();
            foreach (string key in dic.Keys)
            {
                report.RegisterData(dic[key], key);
            }
            if (File.Exists(reportFileName))
            {
                report.Load(reportFileName);
            }
            report.DoublePass = true;//双通道,只有这样才能显示总页数
            return report;
        }

        /// <summary>
        /// 明细设计
        /// </summary>
        /// <param name="dic">打印数据</param>
        public void DetailDesigner(Dictionary<string, DataTable> dic)
        {
            if (dic == null)
            {
                return;
            }
            FastReport.Report report = GetDetailReport(dic);
            report.DoublePass = true;
            report.Design();
            report.Dispose();
            dic = null;
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="dic">打印数据</param>
        public void DetailPreview(Dictionary<string, DataTable> dic)
        {
            if (dic == null)
            {
                return;
            }
            FastReport.Report report = GetDetailReport(dic);
            if (report == null)
            {
                return;
            }
            report.Prepare();
            report.Show();
            report.Dispose();
            dic = null;
        }

        /// <summary>
        /// 明细打印
        /// </summary>
        /// <param name="dic">打印数据</param>
        public void DetailPrint(Dictionary<string, DataTable> dic)
        {
            if (dic == null)
            {
                return;
            }
            FastReport.Report report = GetDetailReport(dic);
            if (report == null)
            {
                return;
            }
            report.Prepare();
            report.Print();
            report.Dispose();
            dic = null;
        }
    }
}
