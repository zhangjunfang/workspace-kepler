using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using SYSModel;
using System.Data;
using HXCPcClient.Report;
using System.Drawing.Printing;
using ServiceStationClient.ComponentUI;
using Utility.Common;

namespace HXCPcClient
{
    /// <summary>
    /// 业务打印模块
    /// </summary>
    public class BusinessPrint
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
        /// 业务打印
        /// </summary>
        /// <param name="dgv">要打印的DataGridView</param>
        /// <param name="printObject">打印的对象</param>
        /// <param name="printTitle">打印的标题</param>
        /// <param name="paperSize">打印页面默认大小，null为A4</param>
        /// <param name="listNotPrint">不打印的列名</param>
        public BusinessPrint(DataGridView dgv, string printObject,string printTitle,PaperSize paperSize,List<string> listNotPrint)
        {
            this.printObject = printObject;
            this.printTitle = printTitle;
            this.paperSize = paperSize;
            #region 判断是否有当前用户、当前报表的设置，如果没有则创建
            if (!DBHelper.IsExist("", "tb_report_set", string.Format("set_object='{0}' and set_user='{1}'", printObject, GlobalStaticObj.UserID)))
            {
                List<SysSQLString> listSql = new List<SysSQLString>();
                foreach (DataGridViewColumn dgvc in dgv.Columns)
                {
                    if (dgvc.Visible && !string.IsNullOrEmpty(dgvc.DataPropertyName))
                    {
                        SysSQLString sql = new SysSQLString();
                        sql.cmdType = CommandType.Text;
                        sql.sqlString = @"insert INTO tb_report_set (set_id,set_num,set_object,set_user,set_name,set_data_name,set_width,is_show,is_print,create_time) 
                                            values (@set_id,@set_num,@set_object,@set_user,@set_name,@set_data_name,@set_width,@is_show,@is_print,@create_time)";
                        sql.Param = new Dictionary<string, string>();
                        sql.Param.Add("set_id", Guid.NewGuid().ToString());
                        sql.Param.Add("set_num", dgvc.Index.ToString());
                        sql.Param.Add("set_object", printObject);
                        sql.Param.Add("set_user", GlobalStaticObj.UserID);
                        sql.Param.Add("set_name", dgvc.HeaderText);
                        sql.Param.Add("set_data_name", dgvc.DataPropertyName);
                        sql.Param.Add("set_width", dgvc.Width.ToString());
                        sql.Param.Add("is_show", "1");
                        if (listNotPrint.Contains(dgvc.Name))
                        {
                            sql.Param.Add("is_print", "0");
                        }
                        else
                        {
                            sql.Param.Add("is_print", "1");
                        }
                        sql.Param.Add("create_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj.CurrentDateTime).ToString());
                        listSql.Add(sql);
                    }
                }
                if (listSql.Count > 0)
                {
                    DBHelper.BatchExeSQLStringMultiByTrans("", listSql);
                }
            }
            #endregion
        }
        /// <summary>
        /// 生成报表
        /// </summary>
        /// <param name="dt">报表数据</param>
        /// <returns></returns>
        public FastReport.Report GetReport(DataTable dt)
        {
            FastReportEx reportEx = new FastReportEx();
            reportEx.styleObject = printObject;
            reportEx.styleTitle = printTitle;
            reportEx.ReportPaperSize = paperSize;
            reportEx.dt = dt;
            return reportEx.DefaultReport();
        }

        /// <summary>
        /// 预览
        /// </summary>
        /// <param name="dt"></param>
        public void Preview(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBoxEx.ShowWarning("没有可预览数据！");
                return;
            }
            FastReport.Report report = GetReport(dt);
            report.Save("tb_parts.frx");
            if (report != null)
            {
                report.Prepare();
                report.ShowPrepared();
                report.Dispose();
            }
            else
            {
                MessageBoxEx.ShowWarning("没有预览数据！");
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="dt"></param>
        public void Print(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBoxEx.ShowWarning("没有可打印的数据");
                return;
            }
            FastReport.Report report = GetReport(dt);
            if (report != null)
            {
                report.Prepare();
                report.Print();
                report.Dispose();
            }
            else
            {
                MessageBoxEx.ShowWarning("没有可打印的数据");
            }
        }
    }
}
