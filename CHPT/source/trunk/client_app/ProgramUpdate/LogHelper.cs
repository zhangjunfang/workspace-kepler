using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProgramUpdate
{
    public class LogHelper
    {
        #region 写日志
        /// <summary> 写日志
        /// </summary>
        /// <param name="InfoType">日志登记</param>
        /// <param name="sLog">日志信息</param>
        /// <param name="LogType">日志类型</param>
        public static void WriteLog(int InfoType, string sLog, int LogType)
        {
            try
            {
                string strLogFile = "Logs";
                if (LogType == 2)
                {
                    strLogFile = "UpLogs";
                }
                string strFileLogs = Application.StartupPath + @"\" + strLogFile;
                if (!Directory.Exists(strFileLogs))
                {
                    Directory.CreateDirectory(strFileLogs);
                }
                string strInfoType = "警告";
                if (InfoType == 2)
                {
                    strInfoType = "故障";
                }
                if (InfoType == 3)
                {
                    strInfoType = "系统";
                }
                if (InfoType == 4)
                {
                    strInfoType = "异常";
                }
                string LosPathName = strFileLogs + @"\" + strInfoType + "-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
                string strLogs = ((Environment.NewLine + DateTime.Now.ToString() + Environment.NewLine + "------------------------------------------------------------") + Environment.NewLine) + sLog + Environment.NewLine;
                File.AppendAllText(LosPathName, strLogs);
            }
            catch
            {
                MessageBox.Show("日志记录异常");
            }
        }
        #endregion
    }
}
