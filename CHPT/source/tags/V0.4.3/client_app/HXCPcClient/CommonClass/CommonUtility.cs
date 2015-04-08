using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace HXCPcClient.CommonClass
{
    public class CommonUtility
    {
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
            DataTable dt = DBHelper.GetTable("", "sys_bill_code_rule", "*", where, "", "");
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
            bool flag = DBHelper.Submit_AddOrEdit("获取新单号", "sys_bill_code_rule", "bill_code_rule_id", ruleID, dicValue);
            if (!flag)
            {
                throw new Exception("获取新项目编码失败");
            }
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
            if (string.IsNullOrEmpty(codeMethod))
            {
                 return billCode + delimiter;
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
                return GlobalStaticObj.CurrentDateTime.ToString("MMddhh");
            }
            if (enumBillCodeMethod == SYSModel.DataSources.EnumBillCodeMethod.YYYYMMDD)
            {
                return GlobalStaticObj.CurrentDateTime.ToString("yyyyMMdd");
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
        /// <summary>
        /// 小数点右侧补零
        /// </summary>
        /// <param name="value">strDecimal</param>
        /// <param name="zeroCount">小数位数</param>
        /// <returns></returns>
        public static string DecimalRightZeroFill(string value, int zeroCount)
        {
            if (!value.Contains("."))
            {
                value +=  ".";
                for (int i = 0; i < zeroCount; i++)
                {
                    value += "0";
                } 
            }
            else
            {
                string[] arr = value.Split('.');
                string right = arr[1];
                if (right.Length < zeroCount)
                {
                    for (int i = right.Length ; i < zeroCount; i++)
                    {
                        right += "0";
                    }
                    value = arr[0] + "." + right;
                }
            }
            return value;
        }
    }
}
