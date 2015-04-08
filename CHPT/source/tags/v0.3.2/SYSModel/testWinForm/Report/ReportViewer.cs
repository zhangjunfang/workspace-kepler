using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using Microsoft.Reporting.WinForms;

namespace testWinForm.Report
{
    public class ReportViewer : Microsoft.Reporting.WinForms.ReportViewer
    {
         private string _rcfile;
        private ReportConfig _rpc;
        private object _datasource;
        private MemoryStream m_rdl;
 
        public ReportViewer() :base()
        {
 
        }
 
        /// <summary>
        /// 配置文件
        /// </summary>
        [DefaultValue("")]
        public string Filename
        {
            get { return _rcfile; }
            set
            {
                _rcfile = value;
                _rpc = new ReportConfig(_rcfile);
            }
        }
 
        /// <summary>
        /// 报表配置
        /// </summary>
        public ReportConfig ReportConfig
        {
            get { return _rpc; }
        }
 
        /// <summary>
        /// 数据源
        /// </summary>
        public object DataSource
        {
            get { return _datasource; }
            set { _datasource = value; }
        }
 
        /// <summary>
        /// 显示报表
        /// </summary>
        public void ShowReport()
        {
            if (m_rdl != null)
                m_rdl.Dispose();
            m_rdl = GenerateRdl();
 
            Reset();
            LocalReport.LoadReportDefinition(m_rdl);
            LocalReport.DataSources.Add(new ReportDataSource("FaibLists", _datasource));
            RefreshReport();
        }
 
        /// <summary>
        /// 生成Rdl流
        /// </summary>
        /// <returns></returns>
        private MemoryStream GenerateRdl()
        {
            MemoryStream ms = new MemoryStream();
            RdlGenerator gen = new RdlGenerator();
            gen.ReportConfig = _rpc;
            gen.WriteXml(ms);
            ms.Position = 0;
            return ms;
        }
    }
}
