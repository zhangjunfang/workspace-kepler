using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BLL;
using HXC_FuncUtility;

namespace yuTongWebService
{
    /// <summary>
    /// 编码规则
    /// </summary>
    public class CodingRule
    {
        string newPrefix = string.Empty;//获取前缀
        string lastBillNo = string.Empty;//最后编码
        string startNum = string.Empty;
        public string ruleID = string.Empty;//编码ID

        public CodingRule(SYSModel.DataSources.EnumProjectType enumProjectType)
        {
            string opName = "获取" + SYSModel.DataSources.GetDescription(enumProjectType) + "的编码生成规则";
            ruleID = Convert.ToInt16(enumProjectType).ToString();
            string where = string.Format("bill_code_rule_id='{0}'", ruleID);
            DataTable dt = DBHelper.GetTable("",GlobalStaticObj_Server.DbPrefix+ GlobalStaticObj_Server.Instance.MainAccCode, "sys_bill_code_rule", "*", where, "", "");
            string billCode = dt.Rows[0]["bill_code"].ToString();
            string codeMethod = dt.Rows[0]["code_method"].ToString();
            string delimiter = dt.Rows[0]["delimiter"].ToString();
            startNum = dt.Rows[0]["start_num"].ToString();
            lastBillNo = dt.Rows[0]["last_bill_no"].ToString();

            newPrefix = GetPrefix(billCode, codeMethod, delimiter);
        }

        /// <summary>
        /// 获取新的编码
        /// </summary>
        /// <returns></returns>
        public string AddNewNo()
        {
            string newBillNo = string.Empty;
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
            lastBillNo = newBillNo;
            return newBillNo;
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


    }
}
