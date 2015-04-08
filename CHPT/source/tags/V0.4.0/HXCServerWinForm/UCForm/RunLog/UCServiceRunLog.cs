using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using HXC_FuncUtility;
namespace HXCServerWinForm.UCForm.RunLog
{
    public partial class UCServiceRunLog : UCBase
    {
        public UCServiceRunLog()
        {
            InitializeComponent();
        }

        private void SetTextBoxValue(string value, TextBox ctr)
        {
            //Action<string> setValueAction = text => ctr.AppendText(value);//Action<T>本身就是delegate类型，省掉了delegate的定义
            //if (ctr.InvokeRequired)
            //{
            //    ctr.Invoke(setValueAction, value);
            //}
            //else
            //{
            //    setValueAction(value);
            //}
        }

        void LoadServiceLog()
        {
            //EventLog myLog = new EventLog();
            //myLog.Log = "Application";
            //foreach (EventLogEntry entry in myLog.Entries)
            //{
            //    //EventLogEntryType枚举包括：
            //    //Error 错误事件。
            //    //FailureAudit 失败审核事件。
            //    //Information 信息事件。
            //    //SuccessAudit 成功审核事件。
            //    //Warning 警告事件。
            //    if (entry.Source == GlobalStaticObj_Server.DataServiceName || entry.Source == GlobalStaticObj_Server.FileServiceName || entry.Source == GlobalStaticObj_Server.FileTransferServiceName || entry.Source == GlobalStaticObj_Server.SessionServiceName)
            //    {
            //        if (entry.EntryType == EventLogEntryType.Error || entry.EntryType == EventLogEntryType.Warning || entry.EntryType == EventLogEntryType.Information)
            //        {
            //            //result.Append("<font color='red'>" + log);
            //            //result.Append(entry.EntryType.ToString() + "</font>");
            //            //result.Append("<font color='blue'>(" + entry.TimeWritten.ToString() + ")</font>：");
            //            //result.Append(entry.Message + "<br /><br />");
            //            //txtServiceLog.AppendText("HXCSessionService" + " 发生时间：" + entry.TimeWritten.ToString() + "\r\n");
            //            //txtServiceLog.AppendText(entry.Message + "\r\n");
            //            SetTextBoxValue(entry.Source + " 发生时间：" + entry.TimeWritten.ToString() + "\r\n", txtServiceLog);
            //            SetTextBoxValue(entry.Message + "\r\n", txtServiceLog);
            //        }
            //    }
            //}
        }

        private void UCServiceRunLog_Load(object sender, EventArgs e)
        {
            //Action loadEventLog = LoadServiceLog;
            //loadEventLog.BeginInvoke(null, null);
            //string path = Application.StartupPath + "\\Log\\";           
            //string logName = "Service.log";
            //string logpath = path + logName;
            //if (File.Exists(logpath))
            //{
            //    string sc = string.Empty;
            //    //StreamReader sr = new StreamReader(logpath,System.Text.Encoding.GetEncoding("gb2312"));
            //    StreamReader sr = new StreamReader(logpath, System.Text.Encoding.GetEncoding("gb2312"));
            //    while ((sc = sr.ReadLine()) != null)
            //    {                  
            //        txtServiceLog.AppendText(sc);
            //    }
            //    sr.Close();
            //    sr.Dispose();
            //}
            //Windows日志有："Application"应用程序, "Security"安全, "System"系统
            //string[] logs = new string[] { "Application", "System" };

            //StringBuilder result = new StringBuilder();

            //foreach (string log in logs)
            //{
            //  EventLog myLog = new EventLog();
            //  myLog.Log = "Application";
            //  //myLog.MachineName = "rondi-agt0qf9op";
            //  foreach (EventLogEntry entry in myLog.Entries)
            //  {
            //      //EventLogEntryType枚举包括：
            //      //Error 错误事件。
            //      //FailureAudit 失败审核事件。
            //      //Information 信息事件。
            //      //SuccessAudit 成功审核事件。
            //      //Warning 警告事件。
            //      if (entry.Source == "HXCSessionService" || entry.Source == "HXCFileTransferService" || entry.Source == "HXCDataService" || entry.Source == "HXCFileService")
            //      {
            //          if (entry.EntryType == EventLogEntryType.Error || entry.EntryType == EventLogEntryType.Warning || entry.EntryType == EventLogEntryType.Information)
            //          {
            //              //result.Append("<font color='red'>" + log);
            //              //result.Append(entry.EntryType.ToString() + "</font>");
            //              //result.Append("<font color='blue'>(" + entry.TimeWritten.ToString() + ")</font>：");
            //              //result.Append(entry.Message + "<br /><br />");
            //              txtServiceLog.AppendText("HXCSessionService" + " 发生时间：" + entry.TimeWritten.ToString() + "\r\n");
            //              txtServiceLog.AppendText(entry.Message + "\r\n");
            //          }
            //      }              
            //}
            //}
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }
    }
}
