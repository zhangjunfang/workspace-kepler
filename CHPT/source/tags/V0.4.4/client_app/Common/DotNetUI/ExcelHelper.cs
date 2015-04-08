using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using HXCCommon.DotNetCode;
using System.Web;

namespace HXCCommon.DotNetUI
{
    /// <summary>
    /// 导出Excel帮助类
    /// </summary>
    public class ExcelHelper
    {
        /// <summary>
        /// 创建系统异常日志
        /// </summary>
        protected static LogHelper Logger = new LogHelper("ExcelHelper");
        /// <summary>
        /// Excel导出数据
        /// </summary>
        /// <param name="data">数据源</param>
        /// <param name="fileName"></param>
        public static void ExportExcel(DataTable data, string fileName)
        {
            try
            {
                if (data != null && data.Rows.Count > 0)
                {
                    System.Web.HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                    System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                    System.Web.HttpContext.Current.Response.Charset = "Utf-8";
                    System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName + ".xls", System.Text.Encoding.UTF8));

                    System.Text.StringBuilder sbHtml = new System.Text.StringBuilder();
                    sbHtml.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
                    sbHtml.AppendLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");

                    //写出列名
                    sbHtml.AppendLine("<tr style=\"background-color: #FFE88C;font-weight: bold; white-space: nowrap;\">");

                    foreach (System.Data.DataColumn column in data.Columns)
                    {
                        sbHtml.AppendLine("<td>" + column.ColumnName + "</td>");
                    }
                    sbHtml.AppendLine("</tr>");

                    //写数据
                    foreach (System.Data.DataRow row in data.Rows)
                    {
                        sbHtml.Append("<tr>");

                        foreach (System.Data.DataColumn column in data.Columns)
                        {
                            sbHtml.Append("<td>").Append(row[column].ToString()).Append("</td>");
                        }
                        sbHtml.AppendLine("</tr>");
                    }
                    sbHtml.AppendLine("</table>");
                    System.Web.HttpContext.Current.Response.Write(sbHtml.ToString());
                    System.Web.HttpContext.Current.Response.End();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("-----------Excel导出数据异常-----------\r\n" + ex.ToString() + "\r\n");
            }
        }
        /// <summary>
        /// Excel检查版本
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static string ConnectionString(string fileName)
        {
            bool isExcel2003 = fileName.EndsWith(".xls");
            string connectionString = string.Format(
                isExcel2003
                    ? "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;"
                    : "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES\"",
                fileName);
            return connectionString;
        }
        /// <summary>
        /// Excel导入数据源
        /// </summary>
        /// <param name="sheet">sheet</param>
        /// <param name="filename">文件路径</param>
        /// <returns></returns>
        public static DataTable ExcelToDataSet(string sheet, string filename)
        {
            try
            {
                DataSet ds;
                OleDbConnection myConn = new OleDbConnection(ConnectionString(filename));
                string strCom = " SELECT * FROM [" + sheet + "$]";
                myConn.Open();
                OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, myConn);
                ds = new DataSet();
                myCommand.Fill(ds);
                myConn.Close();
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                Logger.WriteLog("-----------Excel导入数据异常-----------\r\n" + ex.ToString() + "\r\n");
                return null;
            }
        }
    }
}
