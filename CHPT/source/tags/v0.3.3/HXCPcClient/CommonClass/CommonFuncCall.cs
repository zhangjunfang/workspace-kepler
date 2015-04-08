using System;
using System.Data;
using System.Windows.Forms;
using Newtonsoft.Json;
using SYSModel;
using ServiceStationClient.ComponentUI;
using System.Reflection;
using HXCPcClient.CommonClass;
using System.Collections.Generic;
using Utility.Common;
using System.Collections;
using System.IO;
using System.Data.OleDb;
using ServiceStationClient.ComponentUI.MessageBox;
using System.Text;

namespace HXCPcClient
{
   public delegate bool FunctionReturnBoolDelegateHandler(string opName, ReqeFunStruct request0, out RespFunStruct result); 
   public delegate DataTable GetTableByPageDelegateHandler(string opName, string tableName, string fileds, string where, string groupBy,string orderBy, int PageIndex, int PageSize, out int recordCount);
   public class CommonFuncCall
   {
       public static bool TryCallWCFFunc(string OPNameStr, ReqeFunStruct requestObj, out RespFunStruct responseObj)
       {
           UserIDOP userOP = new UserIDOP() { UserID = GlobalStaticObj.UserID, OPName = OPNameStr };
           requestObj.userIDOP = userOP;
           requestObj.PCClientCookieStr = GlobalStaticObj.CookieStr;
           RespFunStruct returnObj = new RespFunStruct();
           string requestStr = HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(requestObj));
           if (!WCFClientProxy.TestPCClientProxy())
           {
               returnObj.IsSuccess = "0";
               returnObj.ReturnObject = "未能建立同服务器连接";
               responseObj = returnObj;
               MessageBox.Show("未能建立同服务器连接！");
               return false;
           }
           try
           {
               string ResultStr = GlobalStaticObj.proxy.JsonOperate(requestStr);
               string Str = HXCCommon.DotNetEncrypt.DESEncrypt.Decrypt(ResultStr);
               responseObj = JsonConvert.DeserializeObject<RespFunStruct>(Str);
               return true;
           }
           catch (Exception ex)
           {
               returnObj.IsSuccess = "0";
               returnObj.ReturnObject = "发生错误";
               returnObj.Msg = ex.Message;
               responseObj = returnObj;
               MessageBox.Show("发生错误！");
               return false;
           }

       }
       /// <summary> 创建人:唐春奎 根据控件给对应实体类对象赋值
       /// </summary>
       /// <param name="p_Control"></param>
       /// <param name="p_ModelObject"></param>
       public static void SetModelObjectValue(Control p_Control, object p_ModelObject)
       {
           try
           {
               if (p_ModelObject != null)
               {
                   foreach (PropertyInfo info in p_ModelObject.GetType().GetProperties())
                   {
                       ComboBox list;
                       DateTime time;
                       string str5;
                       string str = "txt" + info.Name;
                       string str2 = "";
                       Control control = FindControl(p_Control.Controls, str);
                       if (control == null)
                       {
                           str = "lbl" + info.Name;
                           control = FindControl(p_Control.Controls, str);
                       }
                       if (control == null)
                       {
                           str = "ddl" + info.Name;
                           control = FindControl(p_Control.Controls, str);
                       }
                       if (control == null)
                       {
                           str = "ddt" + info.Name;
                           control = FindControl(p_Control.Controls, str);
                       }
                       if (control == null)
                       {
                           goto Label_01D8;
                       }
                       string name = control.GetType().Name;
                       if (name != null)
                       {
                           ////old
                           //if (!(name == "TextBoxEx"))
                           //{
                           //    if (name == "ComboBoxEx")
                           //    {
                           //        goto Label_00B7;
                           //    }
                           //}
                           //else
                           //{
                           //    TextBoxEx box = (TextBoxEx)control;
                           //    str2 = box.Caption.Trim();
                           //}
                           if (name == "TextBoxEx")
                           {
                               TextBoxEx box = (TextBoxEx)control;
                               str2 = box.Caption.Trim();
                           }
                           else if (name == "Label")
                           {
                               Label box = (Label)control;
                               str2 = box.Text.Trim();
                           }
                           else if (name == "ComboBoxEx")
                           {
                               goto Label_00B7;
                           }
                           else if (name == "DateTimePickerEx")
                           {
                               DateTimePickerEx date = (DateTimePickerEx)control;
                               str2 = Common.LocalDateTimeToUtcLong(date.Value).ToString();
                           }
                           else if (name == "DateTimePickerEx_sms")
                           {
                               DateTimePickerEx_sms date = (DateTimePickerEx_sms)control;
                               if (date != null && !string.IsNullOrEmpty(date.Value))
                               {
                                   str2 = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(date.Value)).ToString();
                               }
                           }
                       }
                       goto Label_00CD;
                   Label_00B7:
                       list = (ComboBoxEx)control;
                   if (list.Items.Count > 0)
                   {
                       str2 = list.SelectedValue.ToString().Trim();
                   }
                   Label_00CD:
                       time = DateTime.Now;
                       string str4 = info.PropertyType.Name;
                       if (str4 == null)
                       {
                           goto Label_01CF;
                       }
                       if (!(str4 == "String"))
                       {
                           if (str4 == "Int32")
                           {
                               goto Label_0145;
                           }
                           if (str4 == "Int64")
                           {
                               goto Label_0146;
                           }
                           if (str4 == "DateTime")
                           {
                               goto Label_015A;
                           }
                           if (str4 == "Boolean")
                           {
                               goto Label_0180;
                           }
                           if (str4 == "Nullable`1")
                           {
                               goto Label_0195;
                           }
                           if (str4 == "Decimal")
                           {
                               goto Label_0194;
                           }
                           goto Label_01CF;
                       }
                       info.SetValue(p_ModelObject, Convert.ToString(str2), null);
                       goto Label_01D8;
                   Label_0145:
                       if (!string.IsNullOrEmpty(str2))
                       {
                           info.SetValue(p_ModelObject, Convert.ToInt32(str2), null);
                       }
                       goto Label_01D8;
                   Label_0146:
                       if (!string.IsNullOrEmpty(str2))
                       {
                           info.SetValue(p_ModelObject, Convert.ToInt64(str2), null);
                       }
                       goto Label_01D8;
                   Label_015A:
                       if (!string.IsNullOrEmpty(str2))
                       {
                           info.SetValue(p_ModelObject, DateTime.TryParse(str2, out time) ? Convert.ToDateTime(str2) : DateTime.Now, null);
                       }
                       goto Label_01D8;
                   Label_0180:
                       if (!string.IsNullOrEmpty(str2))
                       {
                           info.SetValue(p_ModelObject, Convert.ToBoolean(str2), null);
                       }
                       goto Label_01D8;
                   Label_0195:
                       if (((str5 = info.PropertyType.GetGenericArguments()[0].Name) != null) && (str5 == "Int32"))
                       {
                           if (!string.IsNullOrEmpty(str2))
                           {
                               info.SetValue(p_ModelObject, Convert.ToInt32(str2), null);
                           }
                       }
                       goto Label_01D8;
                   Label_0194:
                       if (!string.IsNullOrEmpty(str2))
                       {
                           info.SetValue(p_ModelObject, Convert.ToDecimal(str2), null);
                       }
                       goto Label_01D8;
                   Label_01CF:
                       info.SetValue(p_ModelObject, str2, null);
                   Label_01D8: ;
                   }
               }
           }
           catch (Exception ex)
           { }
       }
       /// <summary> 创建人:唐春奎 根据实体类对象给对应控件赋值
       /// </summary>
       /// <param name="p_Control"></param>
       /// <param name="p_ModelObject"></param>
       /// <param name="p_Mode"></param>
       public static void SetShowControlValue(Control p_Control, object p_ModelObject, string p_Mode)
       {
           try
           {
               if (p_ModelObject != null)
               {
                   foreach (PropertyInfo info in p_ModelObject.GetType().GetProperties())
                   {
                       Control control;
                       object obj2;
                       Label label;
                       TextBoxEx box;
                       ComboBoxEx list;
                       string str = "";
                       if (p_Mode == "View")
                       {
                           str = "lbl" + info.Name;
                           control = FindControl(p_Control.Controls, str);
                       }
                       else
                       {
                           str = "txt" + info.Name;
                           control = FindControl(p_Control.Controls, str);
                           if (control == null)
                           {
                               str = "lbl" + info.Name;
                               control = FindControl(p_Control.Controls, str);
                           }
                           if (control == null)
                           {
                               str = "ddl" + info.Name;
                               control = FindControl(p_Control.Controls, str);
                           }
                           if (control == null)
                           {
                               str = "ddt" + info.Name;
                               control = FindControl(p_Control.Controls, str);
                           }
                       }
                       if (control != null)
                       {
                           string str2 = string.Empty;
                           obj2 = info.GetValue(p_ModelObject, null);
                           str2 = control.GetType().Name;
                           if ((obj2 != null) && (str2 != null))
                           {
                               if (str2 == "Label")
                               {
                                   goto Label_0139;
                               }
                               if (str2 == "TextBoxEx")
                               {
                                   goto Label_0154;
                               }
                               if (str2 == "ComboBoxEx")
                               {
                                   goto Label_01D9;
                               }
                               if (str2 == "DateTimePickerEx")
                               {
                                   DateTimePickerEx date = (DateTimePickerEx)control;
                                   long ticks = (long)obj2;
                                   if (ticks > 0)
                                       date.Value = Common.UtcLongToLocalDateTime(ticks);
                               }
                               if (str2 == "DateTimePickerEx_sms")
                               {
                                   DateTimePickerEx_sms date = (DateTimePickerEx_sms)control;
                                   long ticks = (long)obj2;
                                   if (ticks > 0)
                                       date.Value = Common.UtcLongToLocalDateTime(ticks).ToString();
                               }
                           }
                       }
                       goto Label_020D;
                   Label_0139:
                       label = (Label)control;
                       label.Text = Convert.ToString(obj2);
                       goto Label_020D;
                   Label_0154:
                       box = (TextBoxEx)control;
                       try
                       {
                           if (info.PropertyType.Name.Equals("DateTime") || (info.PropertyType.Name.Equals("Nullable`1") && info.PropertyType.GetGenericArguments()[0].Name.Equals("DateTime")))
                           {
                               box.Caption = Convert.ToDateTime(obj2).ToString("yyyy-MM-dd HH:mm:ss");
                           }
                           else
                           {
                               box.Caption = Convert.ToString(obj2);
                           }
                       }
                       catch (Exception)
                       {
                       }
                       goto Label_020D;
                   Label_01D9:
                       list = (ComboBoxEx)control;
                       if (info.GetValue(p_ModelObject, null).ToString().Trim() != "0")
                       {
                           list.SelectedValue = Convert.ToString(obj2);
                       }
                   Label_020D: ;
                   }
               }
           }
           catch (Exception ex)
           { }
       }
       /// <summary> 创建人:唐春奎 根据控件ID，在控件集合中获取相应控件的对象
       /// </summary>
       /// <param name="p_Controls"></param>
       /// <param name="p_ControlID"></param>
       /// <returns></returns>
       public static Control FindControl(Control.ControlCollection p_Controls, string p_ControlID)
       {
           try
           {
               foreach (Control control in p_Controls)
               {
                   if (!string.IsNullOrEmpty(control.Name) && control.Name.Equals(p_ControlID))
                   {
                       return control;
                   }
                   if ((control.Controls != null) && (control.Controls.Count > 0))
                   {
                       Control control2 = FindControl(control.Controls, p_ControlID);
                       if (control2 != null)
                       {
                           return control2;
                       }
                   }
               }
               return null;
           }
           catch (Exception ex)
           { throw ex; }
       }
       /// <summary> 创建人:唐春奎 根据字段类型和名称，将DataTable中的值赋值给Model对应的字段
       /// </summary>
       /// <param name="p_ModelObject"></param>
       /// <param name="dt"></param>
       public static void SetModlByDataTable(object p_ModelObject, DataTable dt)
       {
           try
           {
               if (p_ModelObject != null && dt.Rows.Count > 0)
               {
                   DateTime time;
                   string str5;
                   foreach (PropertyInfo info in p_ModelObject.GetType().GetProperties())
                   {
                       string controlName = info.Name;
                       string fileType = info.PropertyType.Name;
                       if (dt.Columns[controlName] == null)
                       {
                           continue;
                       }
                       if (fileType == null)
                       {
                           goto Label_01CF;
                       }
                       if (!(fileType == "String"))
                       {
                           if (fileType == "Int32")
                           {
                               goto Label_0145;
                           }
                           if (fileType == "Int64")
                           {
                               goto Label_0146;
                           }
                           if (fileType == "DateTime")
                           {
                               goto Label_015A;
                           }
                           if (fileType == "Boolean")
                           {
                               goto Label_0180;
                           }
                           if (fileType == "Nullable`1")
                           {
                               goto Label_0195;
                           }
                           if (fileType == "Decimal")
                           {
                               goto Label_0194;
                           }
                           goto Label_01CF;
                          
                       }
                       info.SetValue(p_ModelObject, Convert.ToString(dt.Rows[0][controlName].ToString()), null);
                       goto Label_01D8;
                   Label_0145:
                       if (!string.IsNullOrEmpty(dt.Rows[0][controlName].ToString()))
                       {
                           info.SetValue(p_ModelObject, Convert.ToInt32(dt.Rows[0][controlName].ToString()), null);
                       }
                       goto Label_01D8;
                   Label_0146:
                       if (!string.IsNullOrEmpty(dt.Rows[0][controlName].ToString()))
                       {
                           info.SetValue(p_ModelObject, Convert.ToInt64(dt.Rows[0][controlName].ToString()), null);
                       }
                       goto Label_01D8;
                   Label_015A:
                       if (!string.IsNullOrEmpty(dt.Rows[0][controlName].ToString()))
                       {
                           info.SetValue(p_ModelObject, DateTime.TryParse(dt.Rows[0][controlName].ToString(), out time) ? Convert.ToDateTime(dt.Rows[0][controlName].ToString()) : DateTime.Now, null);
                       }
                       goto Label_01D8;
                   Label_0180:
                       if (!string.IsNullOrEmpty(dt.Rows[0][controlName].ToString()))
                       {
                           info.SetValue(p_ModelObject, Convert.ToBoolean(dt.Rows[0][controlName].ToString()), null);
                       }
                       goto Label_01D8;
                   Label_0195:
                       if (((str5 = info.PropertyType.GetGenericArguments()[0].Name) != null) && (str5 == "Int32"))
                       {
                           if (!string.IsNullOrEmpty(dt.Rows[0][controlName].ToString()))
                           {
                               info.SetValue(p_ModelObject, Convert.ToInt32(dt.Rows[0][controlName].ToString()), null);
                           }
                       }
                       goto Label_01D8;
                   Label_0194:
                       if (!string.IsNullOrEmpty(dt.Rows[0][controlName].ToString()))
                       {
                           info.SetValue(p_ModelObject, Convert.ToDecimal(dt.Rows[0][controlName].ToString()), null);
                       }
                       goto Label_01D8;
                   Label_01CF:
                       info.SetValue(p_ModelObject, dt.Rows[0][controlName].ToString(), null);
                   Label_01D8: ;
                   }
               }
           }
           catch (Exception ex)
           { throw ex; }
       }
       /// <summary> 创建人:唐春奎 根据字段类型和名称，将DataTable中的值赋值给Model对应的字段
       /// </summary>
       /// <param name="p_ModelObject"></param>
       /// <param name="dt"></param>
       public static void SetModlByDataTable(object p_ModelObject, DataTable dt,int rowsindex)
       {
           try
           {
               if (p_ModelObject != null && dt.Rows.Count > 0)
               {
                   DateTime time;
                   string str5;
                   foreach (PropertyInfo info in p_ModelObject.GetType().GetProperties())
                   {
                       string controlName = info.Name;
                       string fileType = info.PropertyType.Name;

                       if (fileType == null)
                       {
                           goto Label_01CF;
                       }
                       if (!(fileType == "String"))
                       {
                           if (fileType == "Int32")
                           {
                               goto Label_0145;
                           }
                           if (fileType == "Int64")
                           {
                               goto Label_0146;
                           }
                           if (fileType == "DateTime")
                           {
                               goto Label_015A;
                           }
                           if (fileType == "Boolean")
                           {
                               goto Label_0180;
                           }
                           if (fileType == "Nullable`1")
                           {
                               goto Label_0195;
                           }
                           if (fileType == "Decimal")
                           {
                               goto Label_0194;
                           }
                           goto Label_01CF;

                       }
                       info.SetValue(p_ModelObject, Convert.ToString(dt.Rows[rowsindex][controlName].ToString()), null);
                       goto Label_01D8;
                   Label_0145:
                       if (!string.IsNullOrEmpty(dt.Rows[rowsindex][controlName].ToString()))
                       {
                           info.SetValue(p_ModelObject, Convert.ToInt32(dt.Rows[rowsindex][controlName].ToString()), null);
                       }
                       goto Label_01D8;
                   Label_0146:
                       if (!string.IsNullOrEmpty(dt.Rows[rowsindex][controlName].ToString()))
                       {
                           info.SetValue(p_ModelObject, Convert.ToInt64(dt.Rows[rowsindex][controlName].ToString()), null);
                       }
                       goto Label_01D8;
                   Label_015A:
                       if (!string.IsNullOrEmpty(dt.Rows[rowsindex][controlName].ToString()))
                       {
                           info.SetValue(p_ModelObject, DateTime.TryParse(dt.Rows[rowsindex][controlName].ToString(), out time) ? Convert.ToDateTime(dt.Rows[rowsindex][controlName].ToString()) : DateTime.Now, null);
                       }
                       goto Label_01D8;
                   Label_0180:
                       if (!string.IsNullOrEmpty(dt.Rows[rowsindex][controlName].ToString()))
                       {
                           info.SetValue(p_ModelObject, Convert.ToBoolean(dt.Rows[rowsindex][controlName].ToString()), null);
                       }
                       goto Label_01D8;
                   Label_0195:
                       if (((str5 = info.PropertyType.GetGenericArguments()[rowsindex].Name) != null) && (str5 == "Int32"))
                       {
                           if (!string.IsNullOrEmpty(dt.Rows[rowsindex][controlName].ToString()))
                           {
                               info.SetValue(p_ModelObject, Convert.ToInt32(dt.Rows[rowsindex][controlName].ToString()), null);
                           }
                       }
                       goto Label_01D8;
                   Label_0194:
                       if (!string.IsNullOrEmpty(dt.Rows[rowsindex][controlName].ToString()))
                       {
                           info.SetValue(p_ModelObject, Convert.ToDecimal(dt.Rows[rowsindex][controlName].ToString()), null);
                       }
                       goto Label_01D8;
                   Label_01CF:
                       info.SetValue(p_ModelObject, dt.Rows[rowsindex][controlName].ToString(), null);
                   Label_01D8: ;
                   }
               }
           }
           catch (Exception ex)
           { throw ex; }
       }
       /// <summary> 创建人：唐春奎 从字典码表中获取信息，绑定ComboBox下拉框
       /// </summary>
       /// <param name="ControlName"></param>
       /// <param name="dic_code"></param>
       /// <param name="typename"></param>
       public static void BindComBoxDataSource(ComboBoxEx ControlName, string dic_code,string typename)
       {
           DataTable dt_dic = null;
           if (LocalCache.DtDict == null)
           {
               LocalCache.Update(CacheList.Dict);
           }

           dt_dic = LocalCache.DtDict.Clone();

           if (LocalCache.DtDict != null
           && LocalCache.DtDict.Rows.Count > 0)
           {
               DataRow[] drs = LocalCache.DtDict.Select("dic_code = '" + dic_code + "'");
               if (drs.Length > 0)
               {
                   drs = LocalCache.DtDict.Select("parent_id = '" + drs[0]["dic_id"] + "'");
                   foreach (DataRow dr in drs)
                   {
                       dt_dic.ImportRow(dr);
                   }
               }
           }           

           List<ListItem> list = new List<ListItem>();
           list.Add(new ListItem("", typename));          
           if (dt_dic != null && dt_dic.Rows.Count > 0)
           {
               foreach (DataRow dr in dt_dic.Rows)
               {
                   list.Add(new ListItem(dr["dic_id"], dr["dic_name"].ToString()));
               }
           }
           ControlName.DisplayMember = "Text";
           ControlName.ValueMember = "Value";
           ControlName.DataSource = list;   
       }
       /// <summary> 从字典码表中获取信息 字典码表编码的子集合 
       /// </summary>
       /// <param name="pDic_codeList">父级编码集合</param>
       /// <returns>DataTable</returns>
       public static DataTable GetDictionariesByPDic_codes(ArrayList pDic_codeList)
       {
           string select = "";
           foreach (string str in pDic_codeList)
           {
               if (select.Length > 0)
               {
                   select += " or ";
               }
               select += "dic_code = '" + str + "'";
           }          

           DataTable dt_dic = null;
           if (LocalCache.DtDict == null)
           {
               LocalCache.Update(CacheList.Dict);
           }

           dt_dic = LocalCache.DtDict.Clone();

           if (LocalCache.DtDict != null
           && LocalCache.DtDict.Rows.Count > 0)
           {

               DataRow[] drs = LocalCache.DtDict.Select(select);
               if (drs.Length > 0)
               {
                   drs = LocalCache.DtDict.Select("parent_id = '" + drs[0]["dic_id"] + "'");
                   foreach (DataRow dr in drs)
                   {
                       dt_dic.ImportRow(dr);
                   }
               }
           }
           return dt_dic;          
       }
       /// <summary> 检索码表Table中是否有dic_code这条记录 有即返回名称
       /// </summary>
       /// <param name="dt">码表Table</param>
       /// <param name="dic_id">码表id</param>
       /// <returns>码表名称</returns>
       public static string RetrievalDic_name(DataTable dt, string dic_id)
       {
           string dic_name = "";
           if (dt != null && dt.Rows.Count > 0)
           {
               foreach (DataRow dr in dt.Rows)
               {
                   if (dr["dic_id"].ToString() == dic_id)
                   {
                       dic_name = dr["dic_name"].ToString();
                       break;
                   }
               }
           }
           return dic_name;
       }
       /// <summary> 根据传入父级类型的dic_code,获取父级下的子集信息
       /// </summary>
       /// <param name="parentDic_code"></param>
       /// <returns></returns>
       public static DataTable BindDicDataSource(string parentDic_code)
       {
           string sqlWhere = " parent_id in (select dic_id from sys_dictionaries where dic_code='" + parentDic_code + "' and enable_flag=1) ";
           DataTable dt_dic = DBHelper.GetTable("查询字典码表信息", "sys_dictionaries", "dic_id,dic_name", sqlWhere, "", " order by create_time ");
           return dt_dic;
       }

       /// <summary> 创建人：唐春奎 用于三级联动绑定省份数据的方法
       /// </summary>
       /// <param name="ControlName"></param>
       /// <param name="typename"></param>
       public static void BindProviceComBox(ComboBoxEx ControlName, string typename)
       {
           List<ListItem> list = new List<ListItem>();
           list.Add(new ListItem("", typename));
           string sqlWhere = " AREA_LEVEL=0 ";
           DataTable dt_Provice = DBHelper.GetTable("查询区域表省份信息", "sys_area", "AREA_CODE,AREA_NAME", sqlWhere, "", " order by AREA_CODE ");
           if (dt_Provice != null && dt_Provice.Rows.Count > 0)
           {
               foreach (DataRow dr in dt_Provice.Rows)
               {
                   list.Add(new ListItem(dr["AREA_CODE"].ToString(), dr["AREA_NAME"].ToString()));
               }
           }
           ControlName.DisplayMember = "Text";
           ControlName.ValueMember = "Value";
           ControlName.DataSource = list;
          
       }
       /// <summary> 创建人：唐春奎 用于三级联动绑定城市数据的方法
       /// </summary>
       /// <param name="ControlName"></param>
       /// <param name="ProviceID"></param>
       /// <param name="typename"></param>
       public static void BindCityComBox(ComboBoxEx ControlName, string ProviceID, string typename)
       {
           List<ListItem> list = new List<ListItem>();
           list.Add(new ListItem("", typename));
           if (!string.IsNullOrEmpty(ProviceID))
           {
               string sqlWhere = " PARENT_CODE=" + ProviceID + "";
               DataTable dt_City = DBHelper.GetTable("查询区域表城市信息", "sys_area", "AREA_CODE,AREA_NAME", sqlWhere, "", " order by AREA_CODE ");
               if (dt_City != null && dt_City.Rows.Count > 0)
               {
                   foreach (DataRow dr in dt_City.Rows)
                   {
                       list.Add(new ListItem(dr["AREA_CODE"].ToString(), dr["AREA_NAME"].ToString()));
                   }
               }
           }
           ControlName.DisplayMember = "Text";
           ControlName.ValueMember = "Value";
           ControlName.DataSource = list;     
       }
       /// <summary> 创建人：唐春奎 用于三级联动绑定乡镇数据的方法
       /// </summary>
       /// <param name="ControlName"></param>
       /// <param name="CityID"></param>
       /// <param name="typename"></param>
       public static void BindCountryComBox(ComboBoxEx ControlName, string CityID, string typename)
       {
           List<ListItem> list = new List<ListItem>();
           list.Add(new ListItem("", typename));
           if (!string.IsNullOrEmpty(CityID))
           {
               string sqlWhere = " PARENT_CODE=" + CityID + "";
               DataTable dt_Country = DBHelper.GetTable("查询区域表乡镇信息", "sys_area", "AREA_CODE,AREA_NAME", sqlWhere, "", " order by AREA_CODE ");
               if (dt_Country != null && dt_Country.Rows.Count > 0)
               {
                   foreach (DataRow dr in dt_Country.Rows)
                   {
                       list.Add(new ListItem(dr["AREA_CODE"].ToString(), dr["AREA_NAME"].ToString()));
                   }
               }
           }
           ControlName.DisplayMember = "Text";
           ControlName.ValueMember = "Value";
           ControlName.DataSource = list;
       }
       /// <summary> 创建人：唐春奎 根据ID获取地址全部信息
       /// </summary>
       /// <param name="AddressID"></param>
       /// <returns></returns>
       public static string GetAddress(string AddressID)
       {
           string AddressName = string.Empty;
           if (!string.IsNullOrEmpty(AddressID))
           {
               string sqlWhere = " AREA_CODE=" + AddressID + "";
               DataTable dt_Country = DBHelper.GetTable("查询区域表地址信息", "sys_area", "All_Name", sqlWhere, "", " order by AREA_CODE ");
               if (dt_Country != null && dt_Country.Rows.Count > 0)
               {
                   AddressName = dt_Country.Rows[0]["All_Name"].ToString();
               }
           }
           return AddressName;
       }
       /// <summary> 创建人：唐春奎 获取字典码表中几个类型的子集集合
       /// </summary>
       /// <returns></returns>
       public static DataTable GetDataTable()
       {
         //sys_supplier_category:供应商类别
         //sys_price_type:价格类型
         //sys_enterprise_property:企业\单位性质
         //sys_enterprise_credit_class:企业信用等级
         //sys_repair_project_category:维修项目类别
           string dic_codes = "'sys_supplier_category','sys_price_type','sys_enterprise_property','sys_enterprise_credit_class','sys_repair_project_category'";
           string sqlWhere = " parent_id in (select dic_id from sys_dictionaries where dic_code in (" + dic_codes + ") and enable_flag=1) ";
           DataTable dt_dic = DBHelper.GetTable("查询字典码表信息", "sys_dictionaries", "dic_id,dic_name", sqlWhere, "", " order by create_time ");
           return dt_dic;
       }
       /// <summary> 创建人：唐春奎 从字典码表中，根据编号得到名称的方法
       /// </summary>
       /// <returns></returns>
       public static string GetBillNameByBillCode(DataTable dt, string billCode)
       {
           string billName = string.Empty;
           if (dt != null && dt.Rows.Count > 0)
           {
               for (int i = 0; i < dt.Rows.Count; i++)
               {
                   if (dt.Rows[i]["dic_id"].ToString() == billCode)
                   {
                       billName = dt.Rows[i]["dic_name"].ToString();
                       break;
                   }
               }
           }
           return billName;
       }
       /// <summary> 创建人：唐春奎
       /// 绑定公司
       /// </summary>
       public static void BindCompany(ComboBoxEx ControlName, string typename)
       {
           DataTable dt = DBHelper.GetTable("", "tb_company", "com_id,com_name", " enable_flag=1 and data_source is null or  data_source !='2' ", "", "");
           List<ListItem> list = new List<ListItem>();
           list.Add(new ListItem("", typename));
           foreach (DataRow dr in dt.Rows)
           {
               list.Add(new ListItem(dr["com_id"], dr["com_name"].ToString()));
           }
           ControlName.DataSource = list;
           ControlName.ValueMember = "Value";
           ControlName.DisplayMember = "Text";
       }
       /// <summary> 创建人：唐春奎 绑定部门
       /// </summary>
       public static void BindDepartment(ComboBoxEx ControlName, string com_id, string typename)
       {
           List<ListItem> list = new List<ListItem>();
           list.Add(new ListItem("", typename));
           if (!string.IsNullOrEmpty(com_id))
           {
               DataTable dt = DBHelper.GetTable("", "tb_organization", "org_id,org_name", string.Format("com_id='{0}'", com_id), "", "");
               foreach (DataRow dr in dt.Rows)
               {
                   list.Add(new ListItem(dr["org_id"], dr["org_name"].ToString()));
               }
           }
           ControlName.DataSource = list;
           ControlName.ValueMember = "Value";
           ControlName.DisplayMember = "Text";
       }
       /// <summary> 创建人：唐春奎 绑定经办人
       /// </summary>
       /// <param name="orgID"></param>
       public static void BindHandle(ComboBoxEx ControlName, string orgID, string typename)
       {
           List<ListItem> list = new List<ListItem>();
           list.Add(new ListItem("", typename));
           if (!string.IsNullOrEmpty(orgID))
           {
               DataTable dt = DBHelper.GetTable("", "sys_user", "user_id,user_name", string.Format("org_id='{0}'", orgID), "", "");
               foreach (DataRow dr in dt.Rows)
               {
                   list.Add(new ListItem(dr["user_id"], dr["user_name"].ToString()));
               }
           }
           ControlName.DataSource = list;
           ControlName.ValueMember = "Value";
           ControlName.DisplayMember = "Text";
       }
       /// <summary> 创建人：唐春奎 绑定单据状态
       /// </summary>
       /// <param name="ControlName">控件名称</param>
       /// <param name="typename">true:全部, false:请选择</param>
       public static void BindOrderStatus(ComboBoxEx ControlName, bool typename)
       {
           ControlName.DataSource = DataSources.EnumToList(typeof(DataSources.EnumAuditStatus), typename);
           ControlName.ValueMember = "Value";
           ControlName.DisplayMember = "Text";
       }
       /// <summary> 创建人：唐春奎 绑定采购计划单查询中计划完成状态
       /// </summary>
       /// <param name="ControlName">控件名称</param>
       /// <param name="typename">true:全部, false:请选择</param>
       public static void BindPurchasePlanFinishStatus(ComboBoxEx ControlName, bool typename)
       {
           ControlName.DataSource = DataSources.EnumToList(typeof(DataSources.PurchasePlanFinishStatus), typename);
           ControlName.ValueMember = "Value";
           ControlName.DisplayMember = "Text";
       }
       /// <summary> 创建人：唐春奎 绑定采购开单类型
       /// </summary>
       /// <param name="ControlName">控件名称</param>
       /// <param name="typename">true:全部, false:请选择</param>
       public static void BindPurchaseOrderType(ComboBoxEx ControlName,bool isShowFrist, string typename)
       {
           ControlName.DataSource = DataSources.EnumToListByValueString(typeof(DataSources.EnumPurchaseOrderType), isShowFrist, typename);
           ControlName.ValueMember = "Value";
           ControlName.DisplayMember = "Text";
       }
       /// <summary> 创建人：唐春奎 绑定销售开单类型
       /// </summary>
       /// <param name="ControlName">控件名称</param>
       /// <param name="typename">true:全部, false:请选择</param>
       public static void BindSaleOrderType(ComboBoxEx ControlName, bool isShowFrist, string typename)
       {
           ControlName.DataSource = DataSources.EnumToListByValueString(typeof(DataSources.EnumSaleOrderType), isShowFrist, typename);
           ControlName.ValueMember = "Value";
           ControlName.DisplayMember = "Text";
       }
       /// <summary> 绑定结算方式
       /// </summary>
       public static void BindBalanceWay(ComboBoxEx ControlName, string typename)
       {
           List<ListItem> list = new List<ListItem>();
           list.Add(new ListItem("", typename));
           DataTable dt = DBHelper.GetTable("", "tb_balance_way", "balance_way_id,balance_way_name", " enable_flag='1' ", "", "");
           if (dt != null && dt.Rows.Count > 0)
           {
               foreach (DataRow dr in dt.Rows)
               {
                   list.Add(new ListItem(dr["balance_way_id"], dr["balance_way_name"].ToString()));
               }
           }
           ControlName.DataSource = list;
           ControlName.ValueMember = "Value";
           ControlName.DisplayMember = "Text";
       }
       /// <summary> 绑定结算方式 
       /// </summary>
       /// <param name="cbo"></param>
       /// <param name="typename"></param>
       public static void BindBalanceWayByItem(ComboBox cbo, string typename)
       {
           DataTable dt = DBHelper.GetTable("", "tb_balance_way", "balance_way_id,balance_way_name,default_account", " enable_flag='1' ", "", "");
           if (!string.IsNullOrEmpty(typename))
           {
               DataRow dr = dt.NewRow();
               dr["balance_way_id"] = "";
               dr["balance_way_name"] = typename;
               dr["default_account"] = "";
               dt.Rows.InsertAt(dr, 0);
           }
           cbo.DataSource = dt;
           cbo.ValueMember = "balance_way_id";
           cbo.DisplayMember = "balance_way_name";
       }
       /// <summary> 创建人：唐春奎  获取结算账户信息 绑定采购开单
       /// </summary>
       /// <param name="ControlName">控件名称</param>
       /// <param name="typename">true:全部, false:请选择</param>
       public static void BindAccount(ComboBoxEx ControlName,string whereStr, string typename)
       {
           string sql_where = " enable_flag=1 ";
           List<ListItem> list = new List<ListItem>();
           list.Add(new ListItem("", typename));
           if (!string.IsNullOrEmpty(whereStr))
           {
               sql_where += " and cashier_account='" + whereStr + "'";
               DataTable dt = DBHelper.GetTable("", "tb_cashier_account", "cashier_account,account_name", sql_where, "", "");
               foreach (DataRow dr in dt.Rows)
               {
                   list.Add(new ListItem(dr["cashier_account"], dr["account_name"].ToString()));
               }
           }
           ControlName.DataSource = list;
           ControlName.ValueMember = "Value";
           ControlName.DisplayMember = "Text";
       }

       /// <summary>
       /// 获取结算账户信息
       /// </summary>
       /// <param name="controlName">控件名称</param>
       /// <param name="typename"></param>
       public static void BindAccount(ComboBoxEx controlName, string typename)
       {
           string strWhere = string.Format("enable_flag='{0}' and status='{1}'", (int)DataSources.EnumEnableFlag.USING, (int)DataSources.EnumStatus.Start);
           DataTable dt = DBHelper.GetTable("", "tb_cashier_account", "cashier_account,account_name", strWhere, "", "");
           if (dt == null || dt.Rows.Count == 0)
           {
               dt = new DataTable();
               dt.Columns.Add("cashier_account");
               dt.Columns.Add("account_name");
           }
           if (!string.IsNullOrEmpty(typename))
           {
               DataRow dr1 = dt.NewRow();
               dr1["cashier_account"] = "";
               dr1["account_name"] = typename;
               dt.Rows.InsertAt(dr1, 0);
           }
           controlName.DataSource = dt;
           controlName.ValueMember = "cashier_account";
           controlName.DisplayMember = "account_name";
       }

       #region 绑定DataGridview中单位下拉框的方法
       /// <summary>
       /// 创建人：唐春奎
       /// 绑定DataGridview中单位下拉框的方法
       /// </summary>
       /// <param name="colUnit"></param>
       public static void BindUnit(DataGridViewComboBoxColumn colUnit)
       {
           colUnit.DataSource = UnitData.Copy();
           colUnit.ValueMember = "dic_id";
           colUnit.DisplayMember = "dic_name";
       }
       static DataTable dtUnit = null;
       private static DataTable UnitData
       {
           get
           {
               if (dtUnit == null)
               {
                   dtUnit = CommonCtrl.GetDictByCode("sys_parts_unit_classifier_definition", false);
               }
               return dtUnit;
           }
       } 
       #endregion

       /// <summary> 创建人：唐春奎 GridView中的ComboBox绑定仓库
       /// </summary>
       /// <param name="colWarHouse"></param>
       public static void BindWarehouse(DataGridViewComboBoxColumn colWarHouse)
       {
           List<ListItem> list = new List<ListItem>();
           list.Add(new ListItem("", "请选择"));

           //公司ID
           string com_id = GlobalStaticObj.CurrUserCom_Id;
           string where_str = " enable_flag='1' and com_id='" + com_id + "'";
           DataTable dt = DBHelper.GetTable("查询仓库名称", "tb_warehouse", "wh_id,wh_name", where_str, "", "");
           if (dt != null && dt.Rows.Count > 0)
           {
               foreach (DataRow dr in dt.Rows)
               {
                   list.Add(new ListItem(dr["wh_id"], dr["wh_name"].ToString()));
               }
           }
           colWarHouse.DataSource = list;
           colWarHouse.ValueMember = "Value";
           colWarHouse.DisplayMember = "Text";
       }
       /// <summary> 创建人：唐春奎 ComboBox绑定仓库信息
       /// </summary>
       /// <param name="cbo"></param>
       /// <param name="typename"></param>
       public static void BindWarehouse(ComboBox ControlName, string typename)
       {
           List<ListItem> list = new List<ListItem>();
           if (!string.IsNullOrEmpty(typename))
           {
               list.Add(new ListItem("",typename));
           }
           //公司ID
           string com_id = GlobalStaticObj.CurrUserCom_Id;
           string where_str = " enable_flag='1' and com_id='" + com_id + "'";
           DataTable dt = DBHelper.GetTable("查询仓库名称", "tb_warehouse", "wh_id,wh_name", where_str, "", "");
           if (dt != null && dt.Rows.Count > 0)
           {
               foreach (DataRow dr in dt.Rows)
               {
                   list.Add(new ListItem(dr["wh_id"], dr["wh_name"].ToString()));
               }
           }
           ControlName.DataSource = list;
           ControlName.ValueMember = "Value";
           ControlName.DisplayMember = "Text";
       }

       /// <summary> 创建人：唐春奎 绑定 采购订单查询 完成状态
       /// </summary>
       /// <param name="ControlName"></param>
       /// <param name="typename"></param>
       public static void BindFinishStatus(ComboBoxEx ControlName, bool typename)
       {
           ControlName.DataSource = DataSources.EnumToList(typeof(DataSources.PurchaseOrderFinishStatus), typename);
           ControlName.ValueMember = "Value";
           ControlName.DisplayMember = "Text";
       }
       /// <summary> 创建人：唐春奎 绑定 采购订单查询 开单状态
       /// </summary>
       /// <param name="ControlName"></param>
       /// <param name="typename"></param>
       public static void BindBillStatus(ComboBoxEx ControlName, bool typename)
       {
           ControlName.DataSource = DataSources.EnumToList(typeof(DataSources.PurchaseOrderBillStatus), typename);
           ControlName.ValueMember = "Value";
           ControlName.DisplayMember = "Text";
       }
       /// <summary> 创建人：唐春奎 绑定配件信息 是否是赠品 
       /// </summary>
       /// <param name="ControlName"></param>
       /// <param name="typename"></param>
       public static void BindIs_Gift(ComboBoxEx ControlName, bool typename)
       {
           ControlName.DataSource = DataSources.EnumToList(typeof(DataSources.IsGift), typename);
           ControlName.ValueMember = "Value";
           ControlName.DisplayMember = "Text";
       }

       /// <summary> 创建人：唐春奎 根据配件编号，获取配件车型信息
       /// </summary>
       /// <param name="parts_code"></param>
       /// <returns></returns>
       public static DataTable GetCarType(string parts_code)
       {
           string WhereStr=string.Empty;
           if (!string.IsNullOrEmpty(parts_code))
           {
               WhereStr = " ser_parts_code in (" + parts_code + ")";
           }
           string TableName = string.Format(@"
                                            (
                                              select a.parts_id,b.vm_name,c.ser_parts_code from tb_parts_for_vehicle a 
                                              left join tb_vehicle_models b on a.vm_id=b.vm_id 
                                              left join tb_parts c on a.parts_id=c.parts_id
                                            ) tb_cartype");
           DataTable dt_CarType = DBHelper.GetTable("配件车型信息", TableName, "*", WhereStr, "", "");
           return dt_CarType;
       }
       /// <summary> 创建人：唐春奎 根据配件编号，获取配件类型信息
       /// </summary>
       /// <param name="parts_code"></param>
       /// <returns></returns>
       public static DataTable GetPartsType(string parts_code)
       {
           string WhereStr = string.Empty;
           if (!string.IsNullOrEmpty(parts_code))
           {
               WhereStr = " ser_parts_code in (" + parts_code + ")";
           }
           string TableName = string.Format(@"
                                            (
                                              select a.ser_parts_code,b.* from tb_parts a left join 
                                                (select dic_id,dic_name from sys_dictionaries) b 
                                                on a.parts_type=b.dic_id where b.dic_id is not null or b.dic_id!=''
                                            ) tb_partstype");
           DataTable dt_PartsType = DBHelper.GetTable("获取配件类型信息", TableName, "*", WhereStr, "", "");
           return dt_PartsType;
       }
       /// <summary> 创建人：唐春奎 获取收货人信息,绑定用户表中，宇通的人员信息
       /// </summary>
       /// <param name="ControlName">控件ID</param>
       /// <param name="isShowFrist">是否需要加载显示首项</param>
       /// <param name="typename">首项需要显示的文字</param>
       public static void BindUser(ComboBoxEx ControlName, bool isShowFrist, string typename)
       {
           List<ListItem> list = new List<ListItem>();
           if (isShowFrist)
           {
               list.Add(new ListItem("", typename));
           }
           //公司ID
           string com_id = GlobalStaticObj.CurrUserCom_Id;
           DataTable dt = DBHelper.GetTable("查询收货人信息", "sys_user", "cont_crm_guid,user_name", "LEN(cont_crm_guid)>0", "", "");
           if (dt != null && dt.Rows.Count > 0)
           {
               foreach (DataRow dr in dt.Rows)
               {
                   list.Add(new ListItem(dr["cont_crm_guid"], dr["user_name"].ToString()));
               }
           }
           ControlName.DataSource = list;
           ControlName.ValueMember = "Value";
           ControlName.DisplayMember = "Text";
       }

       /// <summary> 创建人：唐春奎 绑定结算情况
       /// </summary>
       /// <param name="ControlName">控件名称</param>
       /// <param name="typename">全部, 请选择</param>
       public static void BindBalanceStatus(ComboBoxEx ControlName, bool isShowFrist, string typename)
       {
           ControlName.DataSource = DataSources.EnumToListByValueString(typeof(DataSources.EnumBalanceStatus), isShowFrist, typename);
           ControlName.ValueMember = "Value";
           ControlName.DisplayMember = "Text";
       }

       /// <summary> 创建人：唐春奎 绑定入库状态
       /// </summary>
       /// <param name="ControlName">控件名称</param>
       /// <param name="typename">全部, 请选择</param>
       public static void BindIntoStockStatus(ComboBoxEx ControlName, bool isShowFrist, string typename)
       {
           ControlName.DataSource = DataSources.EnumToListByValueString(typeof(DataSources.EnumIntoStockStatus), isShowFrist, typename);
           ControlName.ValueMember = "Value";
           ControlName.DisplayMember = "Text";
       }
       /// <summary> 创建人：唐春奎 绑定出库状态
       /// </summary>
       /// <param name="ControlName">控件名称</param>
       /// <param name="typename">全部, 请选择</param>
       public static void BindOutStockStatus(ComboBoxEx ControlName, bool isShowFrist, string typename)
       {
           ControlName.DataSource = DataSources.EnumToListByValueString(typeof(DataSources.EnumOutStockStatus), isShowFrist, typename);
           ControlName.ValueMember = "Value";
           ControlName.DisplayMember = "Text";
       }
       /// <summary> 创建人：唐春奎 设置列表中某些列可以编辑，其他的列都不可以编辑
       /// </summary>
       /// <param name="ControleName"></param>
       /// <param name="CanEditColumnsName">可以编辑的列的集合数组</param>
       public static void SetColumnReadOnly(DataGridView ControleName, string[] CanEditColumnsName)
       {
           if (ControleName != null)
           {
               ControleName.ReadOnly = false;
               foreach (DataGridViewColumn DataGridViewColumn in ControleName.Columns)
               {
                   if (CanEditColumnsName.Length > 0)
                   {
                       if (!((IList)CanEditColumnsName).Contains(DataGridViewColumn.Name))
                       {
                           DataGridViewColumn.ReadOnly = true;
                       }
                   }
               }
           }
       }


       #region 宇通采购单界面需要的绑定信息
       /// <summary> 创建人：唐春奎 绑定宇通采购订单类型
       /// </summary>
       /// <param name="ControlName">控件名称</param>
       /// <param name="isShowFrist">是否显示首项</param>
       /// <param name="typename">true:全部, false:请选择</param>
       public static void BindYTPurchaseOrderType(ComboBoxEx ControlName, bool isShowFrist, string typename)
       {
           ////绑定枚举获取订单类型
           //ControlName.DataSource = DataSources.EnumToListByValueString(typeof(DataSources.YTOrderType), isShowFrist, typename);
           //ControlName.ValueMember = "Value";
           //ControlName.DisplayMember = "Text";

           //绑定码表获取订单类型
           List<ListItem> list = new List<ListItem>();
           if (isShowFrist)
           {
               list.Add(new ListItem("", typename));
           }
           string sqlWhere = " parent_id ='064A1E51-84F2-4742-8BA5-5FD1F4A2C6B3' and enable_flag='1' ";
           DataTable dt_dic = DBHelper.GetTable("查询字典码表信息", "sys_dictionaries", "dic_code,dic_name", sqlWhere, "", " order by dic_code ");
           foreach (DataRow dr in dt_dic.Rows)
           {
               list.Add(new ListItem(dr["dic_code"], dr["dic_name"].ToString()));
           }
           ControlName.DataSource = list;
           ControlName.ValueMember = "Value";
           ControlName.DisplayMember = "Text";
       }
       /// <summary> 创建人：唐春奎 绑定宇通采购紧急程度
       /// </summary>
       /// <param name="ControlName"></param>
       /// <param name="typename"></param>
       public static void BindYTEmergencyLevel(ComboBoxEx ControlName, bool isShowFrist, string typename)
       {
           //ControlName.DataSource = DataSources.EnumToListByValueString(typeof(DataSources.YTEmergency_Level), isShowFrist, typename);
           //ControlName.ValueMember = "Value";
           //ControlName.DisplayMember = "Text";
       }
       /// <summary> 创建人：唐春奎 绑定宇通采购调拨类型
       /// </summary>
       /// <param name="ControlName"></param>
       /// <param name="typename"></param>
       public static void BindYTAllotType(ComboBoxEx ControlName, bool isShowFrist, string typename)
       {
           ControlName.DataSource = DataSources.EnumToListByValueString(typeof(DataSources.YTAllot_Type), isShowFrist, typename);
           ControlName.ValueMember = "Value";
           ControlName.DisplayMember = "Text";
       }
       /// <summary> 创建人：唐春奎 绑定宇通采购要求发货方式
       /// </summary>
       /// <param name="ControlName"></param>
       /// <param name="typename"></param>
       public static void BindYTReqdelivery(ComboBoxEx ControlName, bool isShowFrist, string typename)
       {
           //ControlName.DataSource = DataSources.EnumToListByValueString(typeof(DataSources.YTReq_delivery), isShowFrist, typename);
           //ControlName.ValueMember = "Value";
           //ControlName.DisplayMember = "Text";
       }
       /// <summary> 创建人：唐春奎 绑定宇通采购活动类型
       /// </summary>
       /// <param name="ControlName"></param>
       /// <param name="typename"></param>
       public static void BindYTActivityType(ComboBoxEx ControlName, bool isShowFrist, string typename)
       {
           ControlName.DataSource = DataSources.EnumToListByValueString(typeof(DataSources.YTActivityType),isShowFrist, typename);
           ControlName.ValueMember = "Value";
           ControlName.DisplayMember = "Text";
       }
       /// <summary> 创建人：唐春奎
       /// 绑定宇通采购订单中中心站/库的信息
       /// </summary>
       public static void BindYTCenterStation(ComboBoxEx ControlName, string typename)
       {
           try
           {
               //DataTable dt = DBHelper.GetTable("", "tb_company", "com_id,center_library", " enable_flag=1 and data_source = 2 ", "", "");
               //List<ListItem> list = new List<ListItem>();
               //list.Add(new ListItem("", typename));
               //foreach (DataRow dr in dt.Rows)
               //{
               //    list.Add(new ListItem(dr["com_id"], dr["center_library"].ToString()));
               //}
               //ControlName.DataSource = list;
               //ControlName.ValueMember = "Value";
               //ControlName.DisplayMember = "Text";

               List<ListItem> list = new List<ListItem>();
               list.Add(new ListItem("", typename));
               DataTable dt_company = DBHelper.GetTable("", GlobalStaticObj.CommAccCode, "tb_company", "center_library", " enable_flag=1 and data_source = 2 ", "", "");
               if (dt_company != null && dt_company.Rows.Count > 0)
               {
                   DataTable dt = DBHelper.GetTable("", "sys_dictionaries", "dic_id,dic_name", " dic_id = '" + dt_company.Rows[0]["center_library"] + "' ", "", "");
                   foreach (DataRow dr in dt.Rows)
                   {
                       list.Add(new ListItem(dr["dic_id"], dr["dic_name"].ToString()));
                   }
               }
               ControlName.DataSource = list;
               ControlName.ValueMember = "Value";
               ControlName.DisplayMember = "Text";
           }
           catch (Exception ex)
           { }
       }
       /// <summary> 创建人：唐春奎
       /// 绑定宇通采购订单产品改进号通知的信息
       /// </summary>
       public static void BindYTProduct_No(ComboBoxEx ControlName, string typename)
       {
           try
           {
               DataTable dt = DBHelper.GetTable("", "tb_product_no", "p_no_id,activities", "", "", "");
               List<ListItem> list = new List<ListItem>();
               list.Add(new ListItem("", typename));
               foreach (DataRow dr in dt.Rows)
               {
                   list.Add(new ListItem(dr["p_no_id"], dr["activities"].ToString()));
               }
               ControlName.DataSource = list;
               ControlName.ValueMember = "Value";
               ControlName.DisplayMember = "Text";
           }
           catch (Exception ex)
           { }
       }
       #endregion


       /// <summary>
       /// 创建人：赵学营
       /// 绑定库存管理单类型
       /// </summary>
       /// <param name="ControlName"></param>
       /// <param name="IsShowFirst"></param>
       /// <param name="TypeName"></param>
       public static void BindAllocationBillType(ComboBoxEx ControlName, bool IsShowFirst, string TypeName)
       {
           ControlName.DataSource = DataSources.EnumToListByValueString(typeof(DataSources.EnumAllocationBillType), IsShowFirst, TypeName);
           ControlName.ValueMember = "Value";
           ControlName.DisplayMember = "Text";
       }
       /// <summary>
       /// 创建人：赵学营
       /// 绑定库存管理开单类型
       /// </summary>
       /// <param name="ControlName"></param>
       /// <param name="IsShowFirst"></param>
       /// <param name="TypeName"></param>
       public static void BindAllocationBillingType(ComboBoxEx ControlName, bool IsShowFirst, string TypeName)
       {
           ControlName.DataSource = DataSources.EnumToListByValueString(typeof(DataSources.EnumAllocationBillingType), IsShowFirst, TypeName);
           ControlName.ValueMember = "Value";
           ControlName.DisplayMember = "Text";
           
       }
       /// <summary>
       /// 创建人：赵学营
       /// 绑定入库单操作的开单类型
       /// </summary>
       /// <param name="CtlName"></param>
       /// <param name="IsShowFirst"></param>
       /// <param name="TypeName"></param>
       public static void BindInStockBillingType(ComboBoxEx CtlName,bool IsShowFirst,string TypeName)
       {
           CtlName.DataSource = DataSources.EnumToListByValueString(typeof(DataSources.EnumInStockBillingType), IsShowFirst, TypeName);
           CtlName.ValueMember = "Value";
           CtlName.DisplayMember = "Text";
       }
       /// <summary>
       /// 创建人：赵学营
       /// 根据实体类模型将表中数据填充到实体类属性
       /// </summary>
       /// <param name="ObjEntity">实体类对像</param>
       /// <param name="dt">用于填充的数据表</param>
       public static void FillEntityByTable(object ObjEntity,DataTable dt)
       {
           try
           {
               //获取实体类的所有公共属性集
               PropertyInfo[] FieldsArray = ObjEntity.GetType().GetProperties();
             
                   for (int i = 0; i< FieldsArray.Length; i++)
                   {//循环给实体对像属性赋值
                       int FieldType = IsCurrentField(dt, FieldsArray[i].Name, FieldsArray[i].PropertyType);//字段类型标志
                       if (FieldType ==(int) DataSources.EnumFieldType.StrType)
                       {
                           FieldsArray[i].SetValue(ObjEntity, dt.Rows[0][FieldsArray[i].Name].ToString(), null);
                       }
                       else if (FieldType == (int)DataSources.EnumFieldType.LongType)
                       {
                           FieldsArray[i].SetValue(ObjEntity,Convert.ToInt64( dt.Rows[0][FieldsArray[i].Name].ToString()), null);
                       }
                   }

           }catch(Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }


       /// <summary>
       /// 创建人：赵学营
       /// 根据实体类属性名给对应控件赋值
       /// </summary>
       /// <param name="Ctl"></param>
       /// <param name="ObjEntity"></param>
       public static void FillControlsByEntity(Control Ctl,object ObjEntity)
       {
           try
           {
               const string TextBx = "txt";//文本框控件名称前缀
               const string DateTimePick = "DTPick";//日期控件名称前缀
               const string Combox = "Comb";//下拉框控件名称前缀
               const string labl = "lbl";//标签控件名称前缀
               PropertyInfo[] FieldsArray = ObjEntity.GetType().GetProperties();
               for (int i = 0; i < FieldsArray.Length;i++ )
               { 
                     
                       object ObjField = FieldsArray[i].GetValue(ObjEntity, null);//循环返回实体类对像属性值
                       //从主控件中递归查询所有子控件
                       Control TxtSubCtl = FindControl(Ctl.Controls, TextBx+FieldsArray[i].Name);
                       Control DTPickSubCtl = FindControl(Ctl.Controls, DateTimePick + FieldsArray[i].Name);
                       Control ComBSubCtl = FindControl(Ctl.Controls, Combox + FieldsArray[i].Name);
                       Control LablSubCtl = FindControl(Ctl.Controls, labl + FieldsArray[i].Name);
                       if (TxtSubCtl!=null)
                       {
                           TextBoxEx txtMsg = (TextBoxEx)TxtSubCtl;
                           txtMsg.Text = (string)ObjField;
                       }
                       else if (DTPickSubCtl!=null)
                       {
                           DateTimePickerEx_sms DateMsg = (DateTimePickerEx_sms)DTPickSubCtl;
                           DateTime OrdDate = Common.UtcLongToLocalDateTime((long)ObjField);
                           DateMsg.Value =OrdDate.ToLongDateString();
                       }
                       else if (ComBSubCtl!=null)
                       {
                           ComboBoxEx ComBoxMsg = (ComboBoxEx)ComBSubCtl;
                           if (ComBoxMsg.Name == "Comborder_type_name")
                           {
                               CommonFuncCall.BindAllocationBillType(ComBoxMsg, true, "请选择");
                               ComBoxMsg.Text = (string)ObjField;
                           }
                           else if (ComBoxMsg.Name == "Combbilling_type_name")
                           {
                               CommonFuncCall.BindInStockBillingType(ComBoxMsg, true, "请选择");
                               ComBoxMsg.Text = (string)ObjField; 
                           }
                           else if (ComBoxMsg.Name == "Combwh_name")
                           {
                               CommonFuncCall.BindWarehouse(ComBoxMsg,"请选择");
                               ComBoxMsg.Text = (string)ObjField; 
                           }
                       }
                       else if (LablSubCtl!=null)
                       {
                           Label LablMsg = (Label)LablSubCtl;
                           if (FieldsArray[i].Name.Contains("time"))
                           {
                               DateTime LblTime = Common.UtcLongToLocalDateTime((long)ObjField);
                               LablMsg.Text = LblTime.ToLongDateString();
                           }
                           else
                           {
                               LablMsg.Text = (string)ObjField;
                           }
                       }
                  
               }
           }catch(Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }


       /// <summary>
       /// 创建人：赵学营
       /// 根据控件输入值给对应实体类属性名赋值
       /// </summary>
       /// <param name="Ctl"></param>
       /// <param name="ObjEntity"></param>
       public static void FillEntityByControls(Control Ctl, object ObjEntity)
       {
           try
           {
                       const string TextBx = "txt";//文本框控件名称前缀
                       const string DateTimePick = "DTPick";//日期控件名称前缀
                       const string Combox = "Comb";//下拉框控件名称前缀
                       const string labl = "lbl";//标签控件名称前缀
                       PropertyInfo[] FieldsArray = ObjEntity.GetType().GetProperties();//实体类型属性数组
                       for (int i = 0; i < FieldsArray.Length; i++)
                       {

                               //从主控件中递归查询所有子控件
                               Control TxtSubCtl = FindControl(Ctl.Controls, TextBx + FieldsArray[i].Name);
                               Control DTPickSubCtl = FindControl(Ctl.Controls, DateTimePick + FieldsArray[i].Name);
                               Control ComBSubCtl = FindControl(Ctl.Controls, Combox + FieldsArray[i].Name);
                               Control LablSubCtl = FindControl(Ctl.Controls, labl + FieldsArray[i].Name);
                               if (TxtSubCtl != null)
                               {
                                   TextBoxEx txtMsg = (TextBoxEx)TxtSubCtl;
                                   if(!string.IsNullOrEmpty(txtMsg.Caption.ToString()))
                                   {
                                     FieldsArray[i].SetValue(ObjEntity,txtMsg.Caption.ToString(),null);//设置属性值
                                   }
 
                               }
                               else if (DTPickSubCtl != null)
                               {
                                   DateTimePickerEx_sms DateMsg = (DateTimePickerEx_sms)DTPickSubCtl;

                                   if (!string.IsNullOrEmpty(DateMsg.Value.ToString()))
                                   {
                                       DateTime DateTransfer = Convert.ToDateTime(DateMsg.Value.ToString());
                                       FieldsArray[i].SetValue(ObjEntity, Common.LocalDateTimeToUtcLong(DateTransfer), null);
                                   }
                               }
                               else if (ComBSubCtl != null)
                               {
                                   ComboBoxEx ComBoxMsg = (ComboBoxEx)ComBSubCtl;
                                   if (!string.IsNullOrEmpty(ComBoxMsg.Text.ToString()))
                                   {
                                       FieldsArray[i].SetValue(ObjEntity, ComBoxMsg.Text.ToString(), null);
                                   }
                               }
                               else if (LablSubCtl != null)
                               {
                                   Label LablMsg = (Label)LablSubCtl;
                                   if (!string.IsNullOrEmpty(LablMsg.Text.ToString()) && LablMsg.Text.ToString()!=".")
                                   {
                                       if (FieldsArray[i].Name.Contains("time"))
                                       {
                                           DateTime DateTransfer = Convert.ToDateTime(LablMsg.Text.ToString());
                                           FieldsArray[i].SetValue(ObjEntity, Common.LocalDateTimeToUtcLong(DateTransfer), null);
                                       }
                                       else
                                       {
                                           FieldsArray[i].SetValue(ObjEntity, LablMsg.Text.ToString(), null);
                                      
                                       }
                                   }
                               }

                           }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
   }

       /// <summary>
       /// 创建人：赵学营
       /// 绑定调拨单操作的类型
       /// </summary>
       /// <param name="CtlName"></param>
       /// <param name="IsShowFirst"></param>
       /// <param name="TypeName"></param>
       public static void BindAllotBillType(ComboBoxEx CtlName, bool IsShowFirst, string TypeName)
       {
           CtlName.DataSource = DataSources.EnumToListByValueString(typeof(DataSources.EnumAllotType), IsShowFirst, TypeName);
           CtlName.ValueMember = "Value";
           CtlName.DisplayMember = "Text";
       }
       /// <summary>
       /// 读取Excel表格中配件信息
       /// </summary>
       /// <returns></returns>
       public static DataTable ImportExcelFile(string FilesPathName)
       {
           try
           {
               string ExcelFilePath = string.Empty;//存储Excel文件路径
               string ExcelConnStr = string.Empty;//读取Excel表格字符串
               DataTable ExcelTable = null;//获取Excel表格中的数据
               string SheetName = string.Empty;//Excel中表名
               string ExcelSql = string.Empty;//查询Excel表格

                ExcelFilePath = FilesPathName;//获取选定文件的路径名
                if (Path.GetExtension(ExcelFilePath) != ".xlsx" && Path.GetExtension(ExcelFilePath) != ".xls")
                {
                    MessageBoxEx.Show("请您输入Excel格式的文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if (ExcelFilePath == string.Empty)
                {
                    MessageBoxEx.Show("请您选择要导入的配件信息文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    //HDR=YES代表Excel第一行是标题不作为数据使用，IMEX三种模式：0代表写入模式，1代表读取模式，2代表链接模式（支持同时读取和写入）
                    ExcelConnStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ExcelFilePath + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;'";
                    OleDbConnection DbCon = new OleDbConnection(ExcelConnStr);//打开数据源连接
                    DbCon.Open();
                    ExcelTable = DbCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "table" });//获取Excel表格中的数据
                    SheetName = ExcelTable.Rows[0]["table_name"].ToString();//获取Excel表名称
                    ExcelSql = "select * from [" + SheetName + "]";
                    OleDbDataAdapter DbAdapter = new OleDbDataAdapter(ExcelSql, DbCon);//执行数据查询
                    DataSet ds = new DataSet();
                    DbAdapter.Fill(ds, SheetName);//填充数据集
                    DbCon.Close();//关闭数据源连接
                    ExcelTable = ds.Tables[0];

                }
            

               return ExcelTable;

           }
           catch (Exception ex)
           {
               MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
               return null;
           }
       }

       /// <summary>
       /// 创建人：赵学营
       /// 绑定单据出入库状态
       /// </summary>
       /// <param name="CtlName"></param>
       /// <param name="IsShowFirst"></param>
       /// <param name="TypeName"></param>
       public static void BindBillInOutStatus(ComboBoxEx CtlName, bool IsShowFirst, string TypeName)
       {
           CtlName.DataSource = DataSources.EnumToListByValueString(typeof(DataSources.EnumBillInOutStatus), IsShowFirst, TypeName);
           CtlName.ValueMember = "Value";
           CtlName.DisplayMember = "Text";
       }

       /// <summary>
       /// 创建人：赵学营
       /// 绑定出入库类型
       /// </summary>
       /// <param name="ControlName">控件</param>
       /// <param name="IsShowFirst">默认值</param>
       /// <param name="TypeName">显示值</param>
       public static void BindInOutType(ComboBoxEx ControlName, bool IsShowFirst, string TypeName)
       {
           ControlName.DataSource = DataSources.EnumToListByValueString(typeof(DataSources.EnumOtherInoutType), IsShowFirst, TypeName);
           ControlName.ValueMember = "Value";
           ControlName.DisplayMember = "Text";
       }

       /// <summary>
       /// 创建人：赵学营
       /// 绑定其它收发货出入库类型
       /// </summary>
       /// <param name="ControlName">控件</param>
       /// <param name="IsShowFirst">默认值</param>
       /// <param name="TypeName">显示值</param>
       public static void BindInOutBillType(ComboBoxEx ControlName, bool IsShowFirst, string TypeName)
       {
           ControlName.DataSource = DataSources.EnumToListByValueString(typeof(DataSources.EnumOtherInoutBillType), IsShowFirst, TypeName);
           ControlName.ValueMember = "Value";
           ControlName.DisplayMember = "Text";
       }
       /// <summary>
       /// 导出Excel表格文件
       /// </summary>
 
       public static void ExportExcelFile(DataTable ExcelTable)
       {
           try
           {
               string SaveExcelName = string.Empty;//保存的Excel文件名称
               SaveFileDialog SFDialog = new SaveFileDialog();
               SFDialog.DefaultExt = "xlsx";
               SFDialog.Filter = "Excel文件(*.xlsx;*.xls)|*.xlsx;*.xls";
               SFDialog.ShowDialog();
               SaveExcelName = SFDialog.FileName;//获取保存的Excel文件名称
               if (SaveExcelName.IndexOf(":") < 0) return ;
               Microsoft.Office.Interop.Excel.Application XlsApp = new Microsoft.Office.Interop.Excel.Application();//创建Excel应用程序
               object missing = System.Reflection.Missing.Value;
               if (XlsApp == null)
               {
                   MessageBoxEx.Show("无法创建Excel表格文件，您的电脑可能未安装Excel软件！","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                   return ;
               }
               else
               {

                   Microsoft.Office.Interop.Excel.Workbooks WkBks = XlsApp.Workbooks;//获取工作簿对像
                   Microsoft.Office.Interop.Excel.Workbook WkBk = WkBks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);//添加Excel表格模板
                   Microsoft.Office.Interop.Excel.Worksheet WkSheet = (Microsoft.Office.Interop.Excel.Worksheet)WkBk.Worksheets[1];//获取工作表格1;
                   Microsoft.Office.Interop.Excel.Range Ran;//声明Excel表格
                   int TotalCount = ExcelTable.Rows.Count;
                   int rowRead = 0;//读取行数
                   float PercentRead = 0;//导入百分比
                   //写入字段名
                   for (int i = 0; i < ExcelTable.Columns.Count;i++ )
                   {
                       WkSheet.Cells[1, i + 1] = ExcelTable.Columns[i].ColumnName.ToString();//获取表列名称
                       Ran=(Microsoft.Office.Interop.Excel.Range)WkSheet.Cells[1,i+1];//列名称写入单元格
                       Ran.Interior.ColorIndex = 15;
                       Ran.Font.Bold = true;//标题加粗

                   }
                   ProgressBarMsg ProgBarMsg = new ProgressBarMsg();
                   ProgressBarMsg.StepMaxValue = TotalCount;//获取进度条最大范围
                   ProgBarMsg.ShowDialog();//显示进度条
                   string ProgressPercent = string.Empty;//进度百分比
                   //写字段值
                   for (int j = 0; j < ExcelTable.Rows.Count; j++)
                   {
                       for (int k = 0; k < ExcelTable.Columns.Count; k++)
                       {
                           WkSheet.Cells[j + 2, k + 1] = ExcelTable.Rows[j][k].ToString();//写表格值
                       }
                       rowRead++;
                       PercentRead = ((float)rowRead * 100) / TotalCount;//导入进度百分比
                       ProgressPercent = PercentRead.ToString("0.00") + "%";
                       ProgressBarMsg.ProgressValue = ProgressPercent;//获取进度百分比
                       ProgressBarMsg.CurrentValue = rowRead;//获取当前进度值
                       Application.DoEvents();//处理当前在消息队列中所有windows消息
                   }            

                   WkSheet.SaveAs(SaveExcelName, missing, missing, missing, missing, missing, missing, missing,missing);
                   Ran = WkSheet.get_Range(WkSheet.Cells[2, 1],WkSheet.Cells[ExcelTable.Rows.Count + 2,ExcelTable.Columns.Count]);//给工作表指定区域
                   //设置Excel表格边框样式
                   Ran.BorderAround2(missing,Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin,
                   Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,missing,missing);
                   Ran.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic;//设置区域边框颜色
                   Ran.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;//连续边框
                   Ran.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;//边框浓度

                   if (ExcelTable.Columns.Count > 1)
                   {//设置垂直表格颜色索引
                     Ran.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].ColorIndex=Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic;
                   }
                   //关闭表格对像，并退出应用程序域
                   WkBk.Close(missing,missing,missing);
                   XlsApp.Quit();
                   ProgBarMsg.Close();//关闭进度条 

               }

               
               
           }catch(Exception ex)
           {
               MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);

               
           }
       }

       /// <summary>
       /// 创建人：赵学营
       /// 绑定入库单操作的开单类型
       /// </summary>
       /// <param name="CtlName"></param>
       /// <param name="IsShowFirst"></param>
       /// <param name="TypeName"></param>
       public static void BindOutStockBillingType(ComboBoxEx CtlName, bool IsShowFirst, string TypeName)
       {
           CtlName.DataSource = DataSources.EnumToListByValueString(typeof(DataSources.EnumOutStockBillingType), IsShowFirst, TypeName);
           CtlName.ValueMember = "Value";
           CtlName.DisplayMember = "Text";
       }

       /// <summary>
       /// 判断字段类型
       /// </summary>
       /// <param name="dt"></param>
       /// <param name="FieldName"></param>
       /// <param name="FieldType"></param>
       /// <returns></returns>
       public static int IsCurrentField(DataTable dt ,string FieldName,Type FieldType )
       {
           try
           {
                int TypeFieldFlag =0;
               for (int i = 0; i < dt.Columns.Count; i++)
               {
                   DataColumn Dc = dt.Columns[i];
                   if (Dc.ColumnName == FieldName && FieldType.Equals(typeof(string)))
                   {

                       TypeFieldFlag = (int)DataSources.EnumFieldType.StrType;
                   }
                   else if (Dc.ColumnName == FieldName && FieldType.Equals(typeof(Int64))) 
                   {
                       TypeFieldFlag = (int)DataSources.EnumFieldType.LongType;
                   }
               }
               return TypeFieldFlag;
           }
           catch (Exception ex)
           {           
               throw new Exception(ex.Message);
               
           }
       }
       /// <summary>
       /// 统计所有功能模块的业务单据配件库存数量
       /// </summary>
       /// <param name="OrderID">已审核业务单据ID</param>
       /// <param name="OrderDate">已审核业务单据日期</param>
       /// <param name="OrderDate">已审核业务单据主键ID字段名称</param>
       /// <param name="OrderDate">已审核业务配件数量统计字段名称</param>
       /// <param name="PartTableName">已审核业务单据配件表名</param>
       public static void StatisticStock(Dictionary<string,long> OrderIDDateValueDic,string BillIDName,string PartCountName,string PartTableName)
       {
           try
           {
              const string StockID = "stock_id";//库存统计表主键ID
              const string StatisticDate="statistic_date";//统计日期
              const string PartWhID = "wh_id";
              const string PartWhName = "wh_name";
              const string PartCode="parts_code"; 
              const string PartName="parts_name"; 
              const string PartBarCode="parts_barcode";
              const string PartDrawNum="drawing_num";
              const string PartUnitName="unit_name";
              const string PartStatisticCount="statistic_count";//统计数量
              const string PartStatisticType = "statistic_Type";//统计类型
              const string PartMakeDate="make_date";//生产日期
              //const string PartStationID="ser_station_id";//服务站ID
              //const string PartBookID="set_book_id";//账套ID
              const string PaperCount = "账面库存";
              const string ActualCount = "实际库存";
              const string OccupyCount = "占用库存";
              DataTable BillPartsTable = null;//存放要统计的配件信息
              DataTable StockStatisTable = null;//获取当前已统计的配件数量
              
              List<SysSQLString> SqlStr = new List<SysSQLString>();//存放要执行的sql语句
              string OpName = "查询" + PartTableName + "配件信息";//查询日志
              string StatisticLogMsg = "更新最新的配件库存统计记录";
              string StockInoutPartTable = "tb_parts_stock_inout_p";//出入库配件表
              string YuTongThreeGuarantyPartTable = "tb_maintain_three_guaranty_material_detail";//宇通三包维修用料表
              StringBuilder sbPartField = new StringBuilder();
              sbPartField.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", BillIDName, PartWhID, PartWhName, PartCode,
              PartName, PartBarCode, PartDrawNum, PartUnitName, PartCountName, PartMakeDate);//设置配件表字段
            //获取已审核单据的配件信息
            foreach (KeyValuePair<string, long> KvPair in OrderIDDateValueDic)
            {
                  BillPartsTable = DBHelper.GetTable(OpName, PartTableName, sbPartField.ToString(), BillIDName + "='" + KvPair.Key.ToString() + "'", "", "");//获取要统计的配件信息

                 //插入统计数据
                StringBuilder sbStockInsertStr = new StringBuilder();
                sbStockInsertStr.AppendFormat("insert into tb_parts_stock_p ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11})" +
                " values(@{0},@{1},@{2},@{3},@{4},@{5},@{6},@{7},@{8},@{9},@{10},@{11})",
                StockID,StatisticDate, PartWhID, PartWhName, PartCode, PartName, PartBarCode, PartDrawNum, PartUnitName,//8
                PartStatisticCount, PartStatisticType,PartMakeDate);//11//设置库存统计表字段
                //更新统计数据
                StringBuilder sbStockUpdateStr = new StringBuilder();
                sbStockUpdateStr.AppendFormat("update tb_parts_stock_p set {0}=@{0} where {1}=@{1} and {2}=@{2} and {3}=@{3}",
                PartStatisticCount, StatisticDate, PartStatisticType, PartCode);
                //向库存统计表中更新数据
                for (int i = 0; i < BillPartsTable.Rows.Count; i++)
                {
                    SysSQLString StrSqlParts = new SysSQLString();//创建存储要执行的sql语句的实体类
                    StrSqlParts.cmdType = CommandType.Text;//指定sql语句执行格式
                    Dictionary<string, string> DicPartParam = new Dictionary<string, string>();//字段参数值集合
                  //查询统计库存表中是否存在该配件已统计的记录行项
                    StockStatisTable = DBHelper.GetTable("查询库存统计表", "tb_parts_stock_p", "statistic_date,parts_code,statistic_count", "statistic_date=" +
                    KvPair.Value + " and parts_code='" + BillPartsTable.Rows[i][PartCode].ToString() + "'", "", "");
                    if (StockStatisTable.Rows.Count == 0)//统计实际库存
                    {

                        DicPartParam.Add(StockID, Guid.NewGuid().ToString());
                        DicPartParam.Add(StatisticDate, KvPair.Value.ToString());
                        DicPartParam.Add(PartWhID, BillPartsTable.Rows[i][PartWhID].ToString() == string.Empty ? "" : BillPartsTable.Rows[i][PartWhID].ToString());
                        DicPartParam.Add(PartWhName, BillPartsTable.Rows[i][PartWhName].ToString() == string.Empty ? "" : BillPartsTable.Rows[i][PartWhName].ToString());
                        DicPartParam.Add(PartCode, BillPartsTable.Rows[i][PartCode].ToString() == string.Empty ? "" : BillPartsTable.Rows[i][PartCode].ToString());
                        DicPartParam.Add(PartName, BillPartsTable.Rows[i][PartName].ToString() == string.Empty ? "" : BillPartsTable.Rows[i][PartName].ToString());
                        DicPartParam.Add(PartBarCode, BillPartsTable.Rows[i][PartBarCode].ToString() == string.Empty ? "" : BillPartsTable.Rows[i][PartBarCode].ToString());
                        DicPartParam.Add(PartDrawNum, BillPartsTable.Rows[i][PartDrawNum].ToString() == string.Empty ? "" : BillPartsTable.Rows[i][PartDrawNum].ToString());
                        DicPartParam.Add(PartUnitName, BillPartsTable.Rows[i][PartUnitName].ToString() == string.Empty ? "" : BillPartsTable.Rows[i][PartUnitName].ToString());
                        DicPartParam.Add(PartStatisticCount, BillPartsTable.Rows[i][PartCountName].ToString() == string.Empty ? "" : BillPartsTable.Rows[i][PartCountName].ToString());//统计数量
                        //统计类型
                        if (PartTableName == StockInoutPartTable)
                        {
                            DicPartParam.Add(PartStatisticType, ActualCount); //实际库存
                        }
                        else if (PartTableName == YuTongThreeGuarantyPartTable)
                        {
                            DicPartParam.Add(PartStatisticType, OccupyCount); //占用库存
                        }
                        else
                        {
                            DicPartParam.Add(PartStatisticType, PaperCount); //账面库存
                        }
                        DicPartParam.Add(PartMakeDate, BillPartsTable.Rows[i][PartMakeDate].ToString() == string.Empty ? "" : BillPartsTable.Rows[i][PartMakeDate].ToString());
                        //DicPartParam.Add(PartStationID, BillPartsTable.Rows[i][PartStationID].ToString() == string.Empty ? "" : BillPartsTable.Rows[i][PartStationID].ToString());
                        //DicPartParam.Add(PartBookID, BillPartsTable.Rows[i][PartBookID].ToString() == string.Empty ? "" : BillPartsTable.Rows[i][PartBookID].ToString());
                        StrSqlParts.Param = DicPartParam;//获取参数值
                        StrSqlParts.sqlString = sbStockInsertStr.ToString();//获取执行的sql语句

                    }
                    else if (StockStatisTable.Rows.Count != 0)
                    {
                        int OldStatisticCount = Convert.ToInt32(StockStatisTable.Rows[0][PartStatisticCount].ToString());//原来统计的配件数量
                        int NewStatisticCount = Convert.ToInt32(BillPartsTable.Rows[i][PartCountName].ToString());//新添加的配件数量
                        int StatisticCount=OldStatisticCount+NewStatisticCount;//获取当前最新配件统计数量
                        DicPartParam.Add(PartStatisticCount, StatisticCount.ToString());//统计数量
                        DicPartParam.Add(StatisticDate, KvPair.Value.ToString());//统计日期
                        DicPartParam.Add(PartCode, BillPartsTable.Rows[i][PartCode].ToString());//配件编码
                        //统计类型
                        if (PartTableName == StockInoutPartTable)
                        {
                            DicPartParam.Add(PartStatisticType, ActualCount); //实际库存
                        }
                        else if (PartTableName == YuTongThreeGuarantyPartTable)
                        {
                            DicPartParam.Add(PartStatisticType, OccupyCount); //占用库存
                        }
                        else
                        {
                            DicPartParam.Add(PartStatisticType, PaperCount); //账面库存
                        }
                        StrSqlParts.Param = DicPartParam;//获取参数值
                        StrSqlParts.sqlString = sbStockUpdateStr.ToString();//获取执行的sql语句
                    }
                
                    SqlStr.Add(StrSqlParts);//完成sql语句拼装
                }
                DBHelper.BatchExeSQLStringMultiByTrans(StatisticLogMsg, SqlStr);//执行sql语句操作
           }
        }
           catch (Exception ex)
           {
               MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
               
           }
       }

       /// <summary> 
       /// ComboBox绑定导入Excel表头信息
       /// </summary>
       /// <param name="cbo"></param>
       /// <param name="typename"></param>
       public static void BindExcelHeadText( DataGridViewComboBoxCell ControlName, DataTable XlsTable, string typename)
       {
           List<ListItem> list = new List<ListItem>();
           if (!string.IsNullOrEmpty(typename))
           {
               list.Add(new ListItem("", typename));
           }
           if (XlsTable.Rows.Count>0)
           {
               for (int i = 0; i < XlsTable.Columns.Count;i++ )
               {
                   list.Add(new ListItem(i.ToString(), XlsTable.Columns[i].ColumnName.ToString()));//存放combox显示的名值对
               }
           }
           ControlName.DataSource = list;
           ControlName.ValueMember = "Value";
           ControlName.DisplayMember = "Text";
       }


 }




   /// <summary> 创建人：唐春奎 用于在采购订单添加/编辑界面 导入配件信息 存放采购计划单号和配件编号的实体类
   /// </summary>
   public class PartsInfoClassByPurchasePlan
   {
       public string planID;
       public string parts_code;
   }
   /// <summary> 创建人：唐春奎 用于在采购开单添加/编辑界面 导入配件信息 存放采购订单号和配件编号的实体类
   /// </summary>
   public class PartsInfoClassByPurchaseOrder
   {
       public string orderID;
       public string parts_code;
   }
   /// <summary> 创建人：唐春奎 用于在采购开单添加/编辑界面 导入配件信息 存放采购开单号和配件编号的实体类
   /// </summary>
   public class PartsInfoClassByPurchaseBill
   {
       public string billID;
       public string parts_code;
   }

   /// <summary> 创建人：唐春奎 用于在销售订单添加/编辑界面 导入配件信息 存放销售计划单号和配件编号的实体类
   /// </summary>
   public class PartsInfoClassBySalePlan
   {
       public string sale_plan_id;
       public string parts_code;
   }
   /// <summary> 创建人：唐春奎 用于在销售开单添加/编辑界面 导入配件信息 存放销售订单号和配件编号的实体类
   /// </summary>
   public class PartsInfoClassBySaleOrder
   {
       public string sale_order_id;
       public string parts_code;
   }
   /// <summary> 创建人：唐春奎 用于在销售开单添加/编辑界面 导入配件信息 存放销售开单号和配件编号的实体类
   /// </summary>
   public class PartsInfoClassBySaleBill
   {
       public string sale_billing_id;
       public string parts_code;
   }


}
