using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Data;

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
        PaperSize paperSize = null;

        /// <summary>
        /// 业务明细打印
        /// </summary>
        /// <param name="printTitle">打印的标题</param>
        /// <param name="paperSize">打印页面默认大小，null为A4</param>
        public BusinessDetailPrint(string printTitle, PaperSize paperSize)
        {
            this.printTitle = printTitle;
            this.paperSize = paperSize;
        }

        /// <summary>
        /// 明细设计
        /// </summary>
        /// <param name="dic"></param>
        public void DetailDesigner(Dictionary<string, DataTable> dic)
        {
            if (dic == null)
            {
                return;
            }
            FastReport.Report report = new FastReport.Report();
            foreach (string key in dic.Keys)
            {
                report.RegisterData(dic[key], key);
            }
            //report.Show();
            report.Design();
            report.Dispose();
            dic = null;
        }
    }
}
