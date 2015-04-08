using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HXC_FuncUtility;
using System.Windows.Forms;
using BLL;
using SYSModel;
using Utility.Common;
using System.Collections;
using Utility.Security;
using Utility.Net;

namespace HXCServerWinForm.CommonClass
{
    public class CommonUtility
    {
        /// <summary> 验证软件是否过期
        /// </summary>
        /// <returns></returns>
        public static long GetSoftValidate()
        {
            Hashtable ht = new Hashtable();
            ht.Add("signId", Secret.StringToBase64String(GlobalStaticObj_Server.Instance.StationID));
            //GlobalStaticObj_Server.Instance.SoftRegUrl = "http://192.168.35.108:8080/sspapp/operation/auth/";
            string resultStr = HttpRequest.DoGet(GlobalStaticObj_Server.Instance.SoftRegUrl + "getValidate.do", ht);
            if (!string.IsNullOrEmpty(resultStr))
            {
                string jsonStr = Secret.Base64StringToString(resultStr);
                ResultInfo resultInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultInfo>(jsonStr);
                if (resultInfo.isSuccess != 1)
                {
                    GlobalStaticObj_Server.GlobalLogService.WriteLog("软件有效期", resultInfo.errMsg);
                    return 0;
                }
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("protocol_expires_time", resultInfo.validate);
                DBHelper.Submit_AddOrEdit("更新软件有效期", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tb_signing_info", "sign_id", GlobalStaticObj_Server.Instance.StationID, dic);
                return Convert.ToInt64(resultInfo.validate);
                
            }
            return 0;
        }
        public class ResultInfo
        {
            /// <summary>0或1(是否成功)
            /// </summary>
            private int _isSuccess;

            public int isSuccess
            {
                get { return _isSuccess; }
                set { _isSuccess = value; }
            }

            /// <summary>错误原因
            /// </summary>
            private string _errMsg;
            public string errMsg
            {
                get { return _errMsg; }
                set { _errMsg = value; }
            }

            /// <summary>软件有效期
            /// </summary>
            public string _validate;
            public string validate
            {
                get { return _validate; }
                set { _validate = value; }
            }

        }


        /// <summary>  获取新项目编码
        /// </summary>
        /// <param name="opName">操作名称</param>
        /// <param name="startStr">单据前缀</param>
        /// <param name="tableName">表名</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public static string GetNewNo(SYSModel.DataSources.EnumProjectType enumProjectType)
        {
            string opName = "获取" + SYSModel.DataSources.GetDescription(enumProjectType) + "的编码生成规则";
            string ruleID = Convert.ToInt16(enumProjectType).ToString();
            string where = string.Format("bill_code_rule_id='{0}'", ruleID);
            DataTable dt = DBHelper.GetTable("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_bill_code_rule", "*", where, "", "");
            string billCode = dt.Rows[0]["bill_code"].ToString();
            string codeMethod = dt.Rows[0]["code_method"].ToString();
            string delimiter = dt.Rows[0]["delimiter"].ToString();
            string startNum = dt.Rows[0]["start_num"].ToString();
            string lastBillNo = dt.Rows[0]["last_bill_no"].ToString();

            string newBillNo = string.Empty;
            string newPrefix = GetPrefix(billCode, codeMethod, delimiter);
            if (string.IsNullOrEmpty(lastBillNo))
            {
                newBillNo = newPrefix + startNum;
            }
            else
            {
                string oldPrefix = lastBillNo.Substring(0, newPrefix.Length);
                if (newPrefix != oldPrefix)
                {
                    newBillNo = newPrefix + startNum;
                }
                else
                {
                    string newNum = (Convert.ToInt32(lastBillNo.Replace(oldPrefix, "")) + 1).ToString().PadLeft(startNum.Length, '0');
                    newBillNo = newPrefix + newNum;
                }
            }
            Dictionary<string, string> dicValue = new Dictionary<string, string>();
            dicValue.Add("last_bill_no", newBillNo);
            bool flag = DBHelper.Submit_AddOrEdit("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_bill_code_rule", "bill_code_rule_id", ruleID, dicValue);
            if (!flag)
            {
                throw new Exception("获取新项目编码失败");
            }
            return newBillNo;
        }
        /// <summary> 加载自动备份计划信息
        /// </summary>
        public static void LoadAutoBackUpPlanInfo()
        {
            DataTable dtBackupPlan = DBHelper.GetTable("缓存自动备份计划信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "v_sys_auto_backup_set", "*", "", "", "order by setbook_code");
            foreach (DataRow row in dtBackupPlan.Rows)
            {
                string setbook_code = row["setbook_code"].ToString();
                if (!GlobalStaticObj_Server.Instance.DicBackupPlan.ContainsKey(setbook_code))
                {
                    string[] intArr = new string[4];
                    intArr[0] = row["auto_backup_type"].ToString();
                    intArr[1] = row["auto_backup_interval"].ToString();
                    intArr[2] = row["auto_backup_starttime"].ToString();
                    intArr[3] = "";
                    GlobalStaticObj_Server.Instance.DicBackupPlan.Add(setbook_code, intArr);
                }
            }
        }

        /// <summary> 获取前缀
        /// </summary>
        /// <param name="billCode">项目编码</param>
        /// <param name="codeMethod">编码方式</param>
        /// <param name="delimiter">分隔符</param>
        /// <returns>返回前缀</returns>
        public static string GetPrefix(string billCode, string codeMethod, string delimiter)
        {
            if (!string.IsNullOrEmpty(codeMethod))
            {
                SYSModel.DataSources.EnumBillCodeMethod enumBillCodeMethod = (SYSModel.DataSources.EnumBillCodeMethod)Convert.ToInt16(codeMethod);
                codeMethod = GetStrByCodeMethod(enumBillCodeMethod);
            }
            if (!string.IsNullOrEmpty(delimiter) && !delimiter.Equals("0"))
            {
                SYSModel.DataSources.EnumDelimiter enumDelimiter = (SYSModel.DataSources.EnumDelimiter)Convert.ToInt16(delimiter);
                delimiter = SYSModel.DataSources.GetDescription(enumDelimiter, true);
            }
            else
            {
                delimiter = "";
            }
            return billCode + delimiter + codeMethod + delimiter;
        }

        /// <summary> 获取编码方式
        /// </summary>
        /// <param name="enumBillCodeMethod">枚举：编码方式</param>
        /// <returns>返回编码方式串</returns>
        private static string GetStrByCodeMethod(SYSModel.DataSources.EnumBillCodeMethod enumBillCodeMethod)
        {
            if (enumBillCodeMethod == SYSModel.DataSources.EnumBillCodeMethod.NONE)
            {
                return "";
            }
            if (enumBillCodeMethod == SYSModel.DataSources.EnumBillCodeMethod.MMDDHH)
            {
                return GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("MMddhh");
            }
            if (enumBillCodeMethod == SYSModel.DataSources.EnumBillCodeMethod.YYYYMMDD)
            {
                return GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyyMMdd");
            }
            return "";
        }

        /// <summary> 设置编辑行编辑单元格的背景色
        /// </summary>
        /// <param name="row">编辑行</param>
        /// <param name="fields">编辑字段</param>
        public static void SetDgvEditCellBgColor(DataGridViewRow row, string[] fields, bool isEdit)
        {
            DataGridViewCellStyle cell = new DataGridViewCellStyle();
            if (isEdit)
            {
                cell.BackColor = GlobalStaticObj.RowEditBgColor;
                cell.SelectionBackColor = GlobalStaticObj.RowEditSelectBgColor;
            }
            else
            {
                cell.BackColor = GlobalStaticObj.RowBgColor;
                cell.SelectionBackColor = GlobalStaticObj.RowSelectBgColor;
            }

            foreach (string str in fields)
            {
                row.Cells[str].Style = cell;
            }
            row.DataGridView.CurrentCell = row.Cells[fields[0]];
        }

        /// <summary> 设置编辑行背景色
        /// </summary>
        /// <param name="row">编辑行</param>
        /// <param name="fields">编辑字段</param>
        public static void SetDgvEditCellBgColor(DataGridViewRow row, bool isEdit)
        {
            DataGridViewCellStyle cell = new DataGridViewCellStyle();
            if (isEdit)
            {
                cell.BackColor = GlobalStaticObj.RowEditBgColor;
                cell.SelectionBackColor = GlobalStaticObj.RowEditSelectBgColor;
            }
            else
            {
                cell.BackColor = GlobalStaticObj.RowBgColor;
                cell.SelectionBackColor = GlobalStaticObj.RowSelectBgColor;
            }
            for (int i = 0; i < row.DataGridView.Columns.Count; i++)
            {
                row.Cells[i].Style = cell;
            }
            row.DataGridView.CurrentCell = row.Cells[0];
        }

        /// <summary> 备份数据库
        /// </summary>
        /// <param name="accCode">帐套编码</param>
        /// <param name="bak_filename">备份文件名称带扩展名</param>
        /// <returns></returns>
        public static string BackupDb(string accCode, string bak_filename)
        {
            string bak_id = Guid.NewGuid().ToString();
            #region 参数
            Dictionary<string, string> dicFields = new Dictionary<string, string>();
            dicFields.Add("bak_id", bak_id);
            dicFields.Add("bak_acccode", accCode);
            dicFields.Add("bak_filename", bak_filename);
            dicFields.Add("is_success", Convert.ToInt32(DataSources.EnumYesNo.Yes).ToString());
            dicFields.Add("bak_method", Convert.ToInt32(DataSources.EnumBackupMethod.ManualBackup).ToString());
            dicFields.Add("bak_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString());
            dicFields.Add("bak_by", GlobalStaticObj_Server.Instance.UserID);
            #endregion
            bool flag = DBHelper.Submit_AddOrEdit("新建备份记录", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_backup_record", "", "", dicFields);
            if (!flag)
            {
                return "新增备份记录失败";
            }
            #region 备份数据库
            string sqlStr = string.Format("BACKUP DATABASE {0} TO DISK = '{1}' WITH COMPRESSION ", GlobalStaticObj_Server.DbPrefix + accCode, GlobalStaticObj_Server.Instance.DbServerBackDir + bak_filename);
            try
            {
                DBHelper.ExtNonQuery("备份数据库", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, sqlStr, CommandType.Text, null);
            }
            catch (Exception ex)
            {
                #region 提交备份失败结果
                dicFields = new Dictionary<string, string>();
                dicFields.Add("is_success", Convert.ToInt32(DataSources.EnumYesNo.NO).ToString());
                dicFields.Add("bak_failmsg", ex.Message);
                flag = DBHelper.Submit_AddOrEdit("提交备份结果", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_backup_record", "bak_id", bak_id, dicFields);
                #endregion
                GlobalStaticObj_Server.GlobalLogService.WriteLog("账套备份", ex);
                return "备份失败" + ex.Message;
            }
            #endregion
            return "";
        }

        /// </summary>还原数据库
        /// <param name="accCode">帐套编码</param>
        /// <param name="bak_filename">备份文件名称带扩展名</param>
        /// <returns></returns>
        public static string RestoreDb(string accCode, string bak_filename)
        {
            string bak_id = Guid.NewGuid().ToString();
            #region 参数
            Dictionary<string, string> dicFields = new Dictionary<string, string>();
            dicFields.Add("res_id", bak_id);
            dicFields.Add("res_acccode", accCode);
            dicFields.Add("res_filename", bak_filename);
            dicFields.Add("is_success", Convert.ToInt32(DataSources.EnumYesNo.Yes).ToString());
            dicFields.Add("res_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString());
            dicFields.Add("res_by", GlobalStaticObj_Server.Instance.UserID);
            #endregion
            bool flag = DBHelper.Submit_AddOrEdit("新建还原记录", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_restore_record", "", "", dicFields);
            if (!flag)
            {
                return "新增还原记录失败";
            }
            #region 还原数据库
            string sqlStr = string.Format("exec sp_killspid '{0}'; BACKUP DATABASE {0} TO DISK = '{1}' ", GlobalStaticObj_Server.DbPrefix + accCode, GlobalStaticObj_Server.Instance.DbServerBackDir + bak_filename);
            try
            {
                DBHelper.ExtNonQuery("还原数据库", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, sqlStr, CommandType.Text, null);
            }
            catch (Exception ex)
            {
                #region 提交备份失败结果
                dicFields = new Dictionary<string, string>();
                dicFields.Add("is_success", Convert.ToInt32(DataSources.EnumYesNo.NO).ToString());
                dicFields.Add("res_failmsg", ex.Message);
                flag = DBHelper.Submit_AddOrEdit("提交备还原份结果", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_restore_record", "res_id", bak_id, dicFields);
                #endregion
                GlobalStaticObj_Server.GlobalLogService.WriteLog("账套还原", ex);
                return "还原失败" + ex.Message;
            }
            #endregion
            return "";
        }
    }
}
