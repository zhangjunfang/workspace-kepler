using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using System.Text.RegularExpressions;

namespace HXCPcClient.CommonClass
{

    /// <summary>
    /// 名    称: ControlsConfig
    /// 功能描述：实现系统所有控件的配置
    /// 作    者：JC
    /// 创建日期：2014-10-11
    /// 备    注：
    /// </summary>    
   public class ControlsConfig
   {
       #region 限制DataGridView控件某个单元格只能输入小数点和数字
       /// <summary>
        ///限制控件的输入必须为数字和小数点
        ///暂时只限制了DataGridView
        /// </summary>
        /// <param name="control">控件的name</param>
        /// <param name="colnameList">DataGridView需要传的参数，要限制的列名</param>
        /// <param name="b_minus">是否允许输入负号，默认不允许</param>
       public static void NumberLimitdgv(Control control, List<string> colnameList = null, bool b_minus = false)
       {
           //DataGridView
           if (control is DataGridView)
           {
               DataGridView dgv = (DataGridView)control;
               dgv.EditingControlShowing += delegate(object sender, DataGridViewEditingControlShowingEventArgs e)
               {
                   try
                   {
                       DataGridViewTextBoxEditingControl cellEdit = (DataGridViewTextBoxEditingControl)e.Control;
                       cellEdit.KeyPress += delegate(object o, KeyPressEventArgs e1)
                       {
                           DataGridViewTextBoxEditingControl text = (DataGridViewTextBoxEditingControl)o;
                           if (colnameList.Contains(dgv.Columns[dgv.CurrentCell.ColumnIndex].Name))
                           {
                               if ((Convert.ToInt32(e1.KeyChar) < 48 || Convert.ToInt32(e1.KeyChar) > 57) && Convert.ToInt32(e1.KeyChar) != 46 && Convert.ToInt32(e1.KeyChar) != 8 && Convert.ToInt32(e1.KeyChar) != 13)
                               {
                                   //如果允许输入负号，就判断负号
                                   if (b_minus && Convert.ToInt32(e1.KeyChar) == 45 && text.Text.Length == 0)
                                       return;
                                   e1.Handled = true;  // 输入非法就屏蔽
                               }
                               else
                               {

                                   if (b_minus)
                                   {
                                       if (text.Text.ToString().IndexOf("-") == -1)
                                       {
                                           if ((Convert.ToInt32(e1.KeyChar) == 46) && (text.Text.Length == 0 || text.Text.ToString().IndexOf(".") != -1))
                                           {
                                               e1.Handled = true;
                                           }
                                       }
                                       else
                                       {
                                           if ((Convert.ToInt32(e1.KeyChar) == 46) && (text.Text.Length <= 1 || text.Text.ToString().IndexOf(".") != -1))
                                           {
                                               e1.Handled = true;
                                           }
                                       }
                                   }
                                   else
                                   {
                                       if ((Convert.ToInt32(e1.KeyChar) == 46) && text.Text.Contains("."))
                                       {
                                           e1.Handled = true;
                                       }
                                   }
                               }
                           }

                       };
                   }
                   catch
                   { }

               };
           }         

       }
        #endregion

       #region DataGridView 合计功能

       /// <summary>
       /// 名称:
       /// 功能: 合计功能DataGridView       
       ///  备注：
       ///  注意：1.请在Load事件中调用  2.合计位置偏下，在下方按钮下，请调节窗体中数据列表DataGridView 的位置
       ///  3. 即时更新合计数据  由于窗体间的更新样式不同  请自行在CellEndEdit事件中添加  并调用该方法
       ///</summary>
       /// <param name="Information1"> 数据列表DataGridView</param>
       /// <param name="form"> 所在容器/窗体(this)</param>
       /// <param name="str">要合计的数据列列名数组</param>
       public static void DatagGridViewTotalConfig(DataGridView Information1, string[] str, bool Iscrool = true)
       {
           new DataGridViewTotal(Information1, str, Iscrool);
           Information1.MultiSelect = false;
       }
       #endregion

       #region TextBoxEx中只能输入数字和小数点
       /// <summary>
       /// TextBoxEx中只能输入数字和小数点
       /// </summary>
       /// <param name="txt"></param>
       public static void TextToDecimal(TextBoxEx txt,bool isint=true)
       {
          
           //键盘按下事件KeyPress
           txt.KeyPress += delegate(object sender, KeyPressEventArgs e)
           {
               if (isint)
               {
                   if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 46)
                   {
                       e.Handled = true;
                   }

                   if (e.KeyChar == 46 && txt.Caption.Contains("."))
                   {
                       e.Handled = true;
                   }
               }
               else
               {
                   if ((e.KeyChar >= '0' && e.KeyChar <= '9') || (byte)(e.KeyChar) == 8)//8就是回格，backspace(删除).
                   {

                   }
                   else
                   {
                       e.Handled = true;
                   }
               }

           };
           //textbox失去焦点事件leave
           txt.Leave += delegate(object sender, EventArgs e)
           {
               string Money = txt.Caption.Trim();
               if (isint)
               {                  
                   Regex regex = new Regex(@"^([1-9][0-9]*|0)(\.[0-9]+)?$");
                   if (Money == "")
                   {
                       txt.Caption = "0.00";
                   }
                   if (!regex.Match(Money).Success)
                   {
                       txt.Caption = "0.00";
                   }
                   else
                   {
                       if (Money.IndexOf(".") == 0)
                       {
                           txt.Caption = "0.00";
                       }
                       if (txt.Caption != "0.00")
                       {
                           txt.Caption = Math.Round(Convert.ToDecimal(Money), 2).ToString("0.00");
                       }
                   }
               }
               else
               {
                    Regex regex = new Regex(@"^([1-9][0-9]*|0)(\.[0-9]+)?$");
                   if (Money == "")
                   {
                       txt.Caption = "0";
                   }
                   if (!regex.Match(Money).Success)
                   {
                       txt.Caption = "0";
                   }
                   else
                   {
                       txt.Caption = Math.Round(Convert.ToDecimal(Money), 0).ToString("0");
                   }
               }
           };
       }
       #endregion

       #region datagridview单元格中如果第一个数输入的是小数点则该单元格的值为0.**
       /// <summary>
       /// datagridview单元格中如果第一个数输入的是小数点则该单元格的值为0.**
       /// </summary>
       /// <param name="dgv">DataGridViewEx</param>
       /// <param name="intIndex">行值</param>
       /// <param name="strCellName">单元格的Name字符串例如："aa,bb,cc,dd"</param>
       public static void SetCellsValue(DataGridViewEx dgv, int intIndex, string strCellName)
       {
           string[] ArrayCells = strCellName.Split(',');
           if (ArrayCells.Length > 0)
           {
               for (int i = 0; i < ArrayCells.Length; i++)
               {
                   if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgv.Rows[intIndex].Cells[ArrayCells[i]].Value)))
                   {

                       if (CommonCtrl.IsNullToString(dgv.Rows[intIndex].Cells[ArrayCells[i]].Value).Substring(0, 1) == ".")
                       {
                           dgv.Rows[intIndex].Cells[ArrayCells[i]].Value = "0" + dgv.Rows[intIndex].Cells[ArrayCells[i]].Value;

                       }
                   }
               }
           }
       }
       #endregion

       #region  把数据转换为保留小数点后两位或者一位的字符串格式
       /// <summary>
       /// 把数据转换为保留小数点后两位或者一位的字符串格式
       /// </summary>
       /// <param name="strData">要转换的数据</param>
       /// <param name="inttype">类型，1为数量显示2为金额显示方式</param>
       /// <returns>返回新的数据格式</returns>
       public static string SetNewValue(object strData,int inttype)
       {
           string strNewData = string.Empty;
           if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(strData)))
           {
               strNewData = inttype == 1 ? Math.Round(Convert.ToDecimal(strData), 1).ToString("0.0") : Math.Round(Convert.ToDecimal(strData),2).ToString("0.00");
           }
           else
           {
               strNewData =inttype==1? "0.0":"0.00";
           }
           return strNewData;

       }
       #endregion 

   }
}
