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
    /// <summary>
    /// Control 控件绑定帮助类
    /// </summary>
    public class ControlBindHelper
    {
        /// <summary>
        /// 绑定GridView DataTable
        /// </summary>
        /// <param name="Columns">数据</param>
        public static void BindGridViewList(DataTable table, GridView grid)
        {
            if (table == null || table.Rows.Count == 0)
            {
                table = table.Clone();
                table.Rows.Add(table.NewRow());
                grid.DataSource = table;
                grid.DataBind();
                int columnCount = table.Columns.Count;
                grid.Rows[0].Cells.Clear();
                grid.Rows[0].Cells.Add(new TableCell());
                grid.Rows[0].Cells[0].ColumnSpan = columnCount;
                grid.Rows[0].Cells[0].Text = "没有找到您要的相关数据";
                grid.Rows[0].Cells[0].Style.Add("text-align", "center");
            }
            else
            {
                grid.DataSource = table;
                grid.DataBind();
            }
        }
        /// <summary>
        /// 绑定IList:GridView 
        /// </summary>
        /// <param name="Columns">数据</param>
        public static void BindGridViewList(IList list, GridView grid)
        {
            grid.DataSource = list;
            grid.DataBind();
        }
        /// <summary>
        /// 绑定DataTable:Repeater控件
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="dropdownlists">控件名称</param>
        public static void BindRepeaterList(DataTable dt, Repeater repeater)
        {
            repeater.DataSource = dt;
            repeater.DataBind();
        }
        /// <summary>
        /// 绑定IList:Repeater控件
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="dropdownlists">控件名称</param>
        public static void BindRepeaterList(IList list, Repeater repeater)
        {
            repeater.DataSource = list;
            repeater.DataBind();
        }
        /// <summary>
        /// 绑定IList<T>:Repeater控件
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="dropdownlists">控件名称</param>
        public static void BindRepeaterList<T>(IList<T> list, Repeater repeater)
        {
            repeater.DataSource = list;
            repeater.DataBind();
        }
        /// <summary>
        /// 绑定DataTable:DropDownList下拉列表框
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="dropdownlists">控件名称</param>
        /// <param name="_Name">绑定字段名称</param>
        /// <param name="_ID">绑定字段主键</param>
        /// <param name="_Memo">默认显示值</param>
        public static void BindDropDownList(DataTable dt, DropDownList dropdownlists, string _Name, string _ID, string _Memo)
        {
            dropdownlists.DataSource = dt;
            dropdownlists.DataTextField = _Name;
            dropdownlists.DataValueField = _ID;
            dropdownlists.DataBind();
            if (!string.IsNullOrEmpty(_Memo.Trim()))
            {
                dropdownlists.Items.Insert(0, new ListItem(_Memo, ""));
            }
        }

        /// <summary>
        /// 绑定IList:DropDownList下拉列表框
        /// </summary>
        /// <param name="list">IList</param>
        /// <param name="dropdownlists">控件名称</param>
        /// <param name="_Name">绑定字段名称</param>
        /// <param name="_ID">绑定字段主键</param>
        /// <param name="_Memo">默认显示值</param>
        public static void BindDropDownList(IList list, DropDownList dropdownlists, string _Name, string _ID, string _Memo)
        {
            dropdownlists.DataSource = list;
            dropdownlists.DataTextField = _Name;
            dropdownlists.DataValueField = _ID;
            dropdownlists.DataBind();
            if (!string.IsNullOrEmpty(_Memo.Trim()))
            {
                dropdownlists.Items.Insert(0, new ListItem(_Memo, ""));
            }
        }

        /// <summary>
        /// 绑定IList<T>:DropDownList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dl"></param>
        /// <param name="list"></param>
        /// <param name="value"></param>
        /// <param name="name"></param>
        public static void BindDropDownList<T>(IList<T> list, DropDownList dropdownlists, string _Name, string _ID, string _Memo)
        {
            dropdownlists.DataSource = list;
            dropdownlists.DataTextField = _Name;
            dropdownlists.DataValueField = _ID;
            dropdownlists.DataBind();
            if (!string.IsNullOrEmpty(_Memo.Trim()))
            {
                dropdownlists.Items.Insert(0, new ListItem(_Memo, ""));
            }
        }

        /// <summary>
        /// 绑定DataTable:HtmlSelect下拉列表框 
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="dropdownlists">控件名称</param>
        /// <param name="_Name">绑定字段名称</param>
        /// <param name="_ID">绑定字段主键</param>
        /// <param name="_Memo">默认显示值</param>
        public static void BindHtmlSelect(DataTable dt, HtmlSelect select, string _Name, string _ID, string _Memo)
        {
            select.DataSource = dt;
            select.DataTextField = _Name;
            select.DataValueField = _ID;
            select.DataBind();
            if (!string.IsNullOrEmpty(_Memo.Trim()))
            {
                select.Items.Insert(0, new ListItem(_Memo, ""));
            }
        }
        /// <summary>
        /// 绑定IList:HtmlSelect下拉列表框 
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="dropdownlists">控件名称</param>
        /// <param name="_Name">绑定字段名称</param>
        /// <param name="_ID">绑定字段主键</param>
        /// <param name="_Memo">默认显示值</param>
        public static void BindHtmlSelect(IList list, HtmlSelect select, string _Name, string _ID, string _Memo)
        {
            select.DataSource = list;
            select.DataTextField = _Name;
            select.DataValueField = _ID;
            select.DataBind();
            if (!string.IsNullOrEmpty(_Memo.Trim()))
            {
                select.Items.Insert(0, new ListItem(_Memo, ""));
            }
        }
        /// <summary>
        /// 绑定IList<T>:HtmlSelect下拉列表框 
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="dropdownlists">控件名称</param>
        /// <param name="_Name">绑定字段名称</param>
        /// <param name="_ID">绑定字段主键</param>
        /// <param name="_Memo">默认显示值</param>
        public static void BindHtmlSelect<T>(IList<T> list, HtmlSelect select, string _Name, string _ID, string _Memo)
        {
            select.DataSource = list;
            select.DataTextField = _Name;
            select.DataValueField = _ID;
            select.DataBind();
            if (!string.IsNullOrEmpty(_Memo.Trim()))
            {
                select.Items.Insert(0, new ListItem(_Memo, ""));
            }
        }
        /// <summary>
        /// 绑定DataTable:RadioButtonList单选框
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="rbllist">控件名称</param>
        /// <param name="_Name">绑定字段名称</param>
        /// <param name="_ID">绑定字段主键</param>        
        public static void BindRadioButtonList(DataTable dt, RadioButtonList rbllist, string _Name, string _ID)
        {
            rbllist.DataSource = dt;
            rbllist.DataTextField = _Name;
            rbllist.DataValueField = _ID;
            rbllist.DataBind();
        }
        /// <summary>
        /// 绑定DataTable:CheckBoxList复选框
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="rbllist">控件名称</param>
        /// <param name="_Name">绑定字段名称</param>
        /// <param name="_ID">绑定字段主键</param>        
        public static void BindCheckBoxList(DataTable dt, CheckBoxList checkList, string _Name, string _ID)
        {
            checkList.DataSource = dt;
            checkList.DataTextField = _Name;
            checkList.DataValueField = _ID;
            checkList.DataBind();
        }

        /// <summary>
        /// 获取aspx页面 服务器控件值，返回哈希表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetWebControls(Control page)
        {
            Dictionary<string, string> ht = new Dictionary<string, string>();
            int size = HttpContext.Current.Request.Params.Count;
            for (int i = 0; i < size; i++)
            {
                string id = HttpContext.Current.Request.Params.GetKey(i);
                Control control = page.FindControl(id);
                if (control == null) continue;
                control = page.FindControl(id);
                if (control is HtmlInputText)
                {
                    HtmlInputText txt = (HtmlInputText)control;
                    ht[txt.ID] = txt.Value.Trim();
                }
                if (control is TextBox)
                {
                    TextBox txt = (TextBox)control;
                    ht[txt.ID] = txt.Text.Trim();
                }
                if (control is HtmlSelect)
                {
                    HtmlSelect txt = (HtmlSelect)control;
                    ht[txt.ID] = txt.Value.Trim();
                }
                if (control is HtmlInputHidden)
                {
                    HtmlInputHidden txt = (HtmlInputHidden)control;
                    ht[txt.ID] = txt.Value.Trim();
                }
                if (control is HtmlInputPassword)
                {
                    HtmlInputPassword txt = (HtmlInputPassword)control;
                    ht[txt.ID] = txt.Value.Trim();
                }
                if (control is HtmlInputCheckBox)
                {
                    HtmlInputCheckBox chk = (HtmlInputCheckBox)control;
                    ht[chk.ID] = (chk.Checked == true ? 1 : 0).ToString();
                }
                if (control is HtmlTextArea)
                {
                    HtmlTextArea area = (HtmlTextArea)control;
                    ht[area.ID] = area.Value.Trim();
                }
            }
            return ht;
        }
        /// <summary>
        /// 创建哈希表给服务器控件赋值
        /// </summary>
        /// <param name="page"></param>
        /// <param name="ht"></param>
        public static void SetWebControls(Control page, Dictionary<string, string> ht)
        {
            if (ht.Count != 0)
            {
                int size = ht.Keys.Count;
                foreach (string key in ht.Keys)
                {
                    object val = ht[key];
                    Control control = page.FindControl(key);
                    if (control == null) continue;
                    if (control is HtmlInputText)
                    {
                        HtmlInputText txt = (HtmlInputText)control;
                        txt.Value = val.ToString();
                    }
                    if (control is TextBox)
                    {
                        TextBox txt = (TextBox)control;
                        txt.Text = val.ToString();
                    }
                    if (control is DropDownList)
                    {
                        DropDownList txt = (DropDownList)control;
                        txt.SelectedValue = val.ToString();
                    }
                    if (control is HtmlSelect)
                    {
                        HtmlSelect txt = (HtmlSelect)control;
                        txt.Value = val.ToString();
                    }
                    if (control is HtmlInputHidden)
                    {
                        HtmlInputHidden txt = (HtmlInputHidden)control;
                        txt.Value = val.ToString();
                    }
                    if (control is HtmlInputPassword)
                    {
                        HtmlInputPassword txt = (HtmlInputPassword)control;
                        txt.Value = val.ToString();
                    }
                    if (control is Label)
                    {
                        Label txt = (Label)control;
                        txt.Text = val.ToString();
                    }
                    if (control is HtmlTextArea)
                    {
                        HtmlTextArea area = (HtmlTextArea)control;
                        area.Value = val.ToString();
                    }
                    if (control is HtmlInputCheckBox)
                    {
                        HtmlInputCheckBox area = (HtmlInputCheckBox)control;
                        area.Checked = (val.ToString()=="1"?true:false);
                    }
                    if (control is CheckBoxList)
                    {
                        CheckBoxList checkList = (CheckBoxList)control;
                        string checkStr = val.ToString();
                        for (int i = 0; i < checkList.Items.Count; i++)
                        {
                            if (checkStr.Contains(checkList.Items[i].Value))
                            {
                                checkList.Items[i].Selected = true;
                            }
                        }
                    }
                }
            }
        }
        // <summary>
        /// 返回控件属性
        /// </summary>
        /// <param name="Control_Type">控件类型</param>
        /// <param name="Property_Name">属性名称</param>
        /// <param name="Control_ID">控件ID</param>
        /// <param name="Control_Style">控件样式</param>
        /// <param name="Control_Length">控件长度</param>
        /// <param name="Control_Validator">验证码</param>
        /// <param name="j">判断显示几列，</param>
        /// <param name="Colspan">合并</param>
        /// <param name="DataSource">数据源，值 | 符号分割</param>
        /// <param name="Event">事件</param>
        /// <param name="Maxlength">最大长度</param>
        /// <returns></returns>
        public static string GetControlProperty(string Control_Type, string Property_Name, string Control_ID, string Control_Style, string Control_Length, string Control_Validator, int j, string Colspan, string DataSource, string Event, string Maxlength)
        {
            StringBuilder property = new StringBuilder();
            if (Colspan == "")
            {
                if (j == 0)
                {
                    property.Append("<tr>");
                    property.Append("<th>" + Property_Name + "</th>");
                    property.Append("<td>");
                    property.Append(GetControl_Type(Control_Type, Control_ID, Control_Style, Control_Length, Control_Validator, DataSource, Event, Maxlength));
                    property.Append("</td>");
                }
                else if (j == 1)
                {
                    property.Append("<th>" + Property_Name + "</th>");
                    property.Append("<td>");
                    property.Append(GetControl_Type(Control_Type, Control_ID, Control_Style, Control_Length, Control_Validator, DataSource, Event, Maxlength));
                    property.Append("</td>");
                    property.Append("</tr>");
                }
                else
                {
                    property.Append("<tr>");
                    property.Append("<th>" + Property_Name + "</th>");
                    property.Append("<td>");
                    property.Append(GetControl_Type(Control_Type, Control_ID, Control_Style, Control_Length, Control_Validator, DataSource, Event, Maxlength));
                    property.Append("</td>");
                    property.Append("</tr>");
                }
            }
            else
            {
                property.Append("<tr>");
                property.Append("<th>" + Property_Name + "</th>");
                property.Append("<td " + Colspan + ">");
                property.Append(GetControl_Type(Control_Type, Control_ID, Control_Style, Control_Length, Control_Validator, DataSource, Event, Maxlength));
                property.Append("</td>");
                property.Append("</tr>");
            }
            return property.ToString();
        }
        /// <summary>
        /// 返回控件类型
        /// </summary>
        /// <param name="Control_Type">类型</param>
        /// <param name="Control_ID">控件ID</param>
        /// <param name="Control_Style">控件样式</param>
        /// <param name="Control_Length">控件长度</param>
        /// <param name="Control_Validator">验证码</param>
        /// <param name="DataSource">数据源</param>
        /// <param name="Event">事件</param>
        /// <param name="Maxlength">最大长度</param>
        /// <returns></returns>
        public static string GetControl_Type(string Control_Type, string Control_ID, string Control_Style, string Control_Length, string Control_Validator, string DataSource, string Event, string Maxlength)
        {
            StringBuilder str_Control_Type = new StringBuilder();
            string strMaxlength = "";
            if (Maxlength != "")
            {
                strMaxlength = "maxlength=" + Maxlength + "";
            }
            switch (Control_Type)
            {
                case "1"://1 - 文本框
                    str_Control_Type.Append("<input id=\"" + Control_ID + "\" " + Event + " " + strMaxlength + " type=\"text\" class=\"" + Control_Style + "\" style=\"width: " + Control_Length + "\" " + Control_Validator + "/>");
                    return str_Control_Type.ToString();
                case "2"://2 - 下拉框
                    str_Control_Type.Append("<select id=\"" + Control_ID + "\" " + Event + " class=\"" + Control_Style + "\" style=\"width: " + Control_Length + "\" " + Control_Validator + "/>");
                    if (DataSource != "")
                    {
                        string[] strSource = DataSource.Split('|');
                        foreach (var item in strSource)
                        {
                            str_Control_Type.Append("<option value=" + item + ">" + item + "</option>");
                        }
                    }
                    str_Control_Type.Append("</select>");
                    return str_Control_Type.ToString();
                case "3"://3 - 日期框
                    str_Control_Type.Append("<input id=\"" + Control_ID + "\" " + Event + " " + strMaxlength + " type=\"text\" class=\"" + Control_Style + "\" style=\"width: " + Control_Length + "\" " + Control_Validator + "/>");
                    return str_Control_Type.ToString();
                case "4"://4 - 标签
                    str_Control_Type.Append("<lable id=\"" + Control_ID + "\"/>");
                    return str_Control_Type.ToString();
                default:
                    return "内部错误";
            }
        }
    }
}
