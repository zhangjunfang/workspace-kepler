using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web;
using System.Data;

namespace HXCCommon.DotNetUI
{
   public class ControlGetValue
    {
        /// <summary>
        /// 返回CheckBoxList所选值的字符串(以$分割连接)
        /// </summary>
        /// <param name="CheckBoxList"></param>
       public static string GetCheckBoxListValue(CheckBoxList checkList)
       {
            string checkStr = string.Empty;
            for (int i = 0; i < checkList.Items.Count; i++)
            {
                if (checkList.Items[i].Selected == true)
                {
                    checkStr += checkList.Items[i].Value + "$";
                }
            }
            return checkStr.TrimEnd('$');
       }
    }
}
