using System;
using System.Data;
using System.Windows.Forms;
using Newtonsoft.Json;
using SYSModel;
using ServiceStationClient.ComponentUI;
using System.Reflection;
using System.Collections.Generic;
using SYSModel;
using Utility.Common;
using System.Collections;
using BLL;
using HXC_FuncUtility;
namespace HXCServerWinForm.CommonClass
{
    public class CommonFuncCall
    {
        /// <summary>
        /// 从字典码表中获取信息 字典码表编码的子集合 
        /// </summary>
        /// <param name="pDic_codeList">父级编码集合</param>
        /// <returns>DataTable</returns>
        public static DataTable GetDictionariesByPDic_codes(ArrayList pDic_codeList)//(string pDic_codes)//"'"+code1+"','"+
        {
            string dic_code_in = "";
            foreach (string str in pDic_codeList)
            {
                dic_code_in += "'" + str + "',";
            }
            if (!string.IsNullOrEmpty(dic_code_in))
                dic_code_in = dic_code_in.Substring(0, dic_code_in.Length - 1);
            string sqlWhere = " parent_id in (select dic_id from sys_dictionaries where dic_code in (" + dic_code_in + ") and enable_flag=1) ";
            return DBHelper.GetTable("查询字典码表信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_dictionaries", "dic_id,dic_name", sqlWhere, "", " order by create_time ");
        }

        /// <summary>
        /// 创建人：唐春奎
        /// 用于三级联动绑定省份数据的方法
        /// </summary>
        /// <param name="ControlName"></param>
        /// <param name="typename"></param>
        public static void BindProviceComBox(ComboBoxEx ControlName, string typename)
        {
            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem("", typename));
            string sqlWhere = " AREA_LEVEL=0 ";
            DataTable dt_Provice = DBHelper.GetTable("查询区域表省份信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_area", "AREA_CODE,AREA_NAME", sqlWhere, "", " order by AREA_CODE ");
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
        /// <summary>
        /// 创建人：唐春奎
        /// 用于三级联动绑定城市数据的方法
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
                DataTable dt_City = DBHelper.GetTable("查询区域表城市信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_area", "AREA_CODE,AREA_NAME", sqlWhere, "", " order by AREA_CODE ");
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
        /// <summary>
        /// 创建人：唐春奎
        /// 用于三级联动绑定乡镇数据的方法
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
                DataTable dt_Country = DBHelper.GetTable("查询区域表乡镇信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_area", "AREA_CODE,AREA_NAME", sqlWhere, "", " order by AREA_CODE ");
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

        /// <summary>
        /// 创建人：唐春奎
        /// 从字典码表中获取信息，绑定ComboBox下拉框
        /// </summary>
        /// <param name="ControlName"></param>
        /// <param name="dic_code"></param>
        /// <param name="typename"></param>
        public static void BindComBoxDataSource(ComboBoxEx ControlName, string dic_code, string typename)
        {
            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem("", typename));
            string sqlWhere = " parent_id in (select dic_id from sys_dictionaries where dic_code='" + dic_code + "' and enable_flag=1) ";
            DataTable dt_dic = DBHelper.GetTable("查询字典码表信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_dictionaries", "dic_id,dic_name", sqlWhere, "", " order by create_time ");
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

        #region --下拉数据绑定
        /// <summary>
        /// 创建人：杨天帅
        /// 通用下拉数据绑定
        /// </summary>
        /// <param name="cmb">下拉容器</param>
        /// <param name="ht">数据散列集合</param>
        public static void BindComBox(ComboBox cmb, Hashtable ht, bool showFlag)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Value");
            if (showFlag)
            {
                dt.Rows.Add("请选择", "");
            }
            foreach (DictionaryEntry de in ht)
            {
                dt.Rows.Add(de.Key, de.Value);
            }
            cmb.DataSource = dt;
            cmb.DisplayMember = "Name";
            cmb.ValueMember = "Value";
        }
        /// <summary>
        /// 创建人：杨天帅
        /// 通用下拉数据绑定
        /// </summary>
        /// <param name="cmb">下拉容器</param>
        /// <param name="ht">数据散列集合</param>
        /// <param name="defaultValue">默认值</param>
        public static void BindComBox(ComboBox cmb, Hashtable ht, string defaultValue)
        {
            BindComBox(cmb, ht, false);
            cmb.SelectedValue = defaultValue;
        }
        /// <summary>
        /// 数据容器下拉容器列
        /// </summary>
        /// <param name="dgvcc"></param>
        /// <param name="ht"></param>
        public static void BindDataGridComboBox(DataGridViewComboBoxColumn dgvcc, Hashtable ht)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Value");
            foreach (DictionaryEntry de in ht)
            {
                dt.Rows.Add(de.Key, de.Value);
            }
            dgvcc.DataSource = dt;
            dgvcc.DisplayMember = "Name";
            dgvcc.ValueMember = "Value";
        }
        #endregion

        /// <summary>
        /// 获取公司信息并绑定
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="typename"></param>
        public static void BindCompanyComBox(ComboBoxEx cmb, string typename, string com_id)
        {
            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem("", typename));

            string sqlWhere = " enable_flag='1'";
            if (!string.IsNullOrEmpty(com_id))
            {
                sqlWhere += " and com_id!='" + com_id + "'";
            }
            DataTable dt_com = DBHelper.GetTable("查询公司信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tb_company", "com_id,com_name", sqlWhere, "", " order by create_time ");
            if (dt_com != null && dt_com.Rows.Count > 0)
            {
                foreach (DataRow dr in dt_com.Rows)
                {
                    list.Add(new ListItem(dr["com_id"].ToString(), dr["com_name"].ToString()));
                }
            }

            cmb.DisplayMember = "Text";
            cmb.ValueMember = "Value";
            cmb.DataSource = list;
        }

        /// <summary>
        /// 获取公司信息并绑定
        /// </summary>
        /// <param name="cmb"></param>     
        public static void BindCompanyComBox(DataGridViewComboBoxColumn dgcbc)
        {
            List<ListItem> list = new List<ListItem>();

            string sqlWhere = " enable_flag='1'";
            DataTable dt_com = DBHelper.GetTable("查询公司信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tb_company", "com_id,com_name", sqlWhere, "", " order by create_time ");
            if (dt_com != null && dt_com.Rows.Count > 0)
            {
                foreach (DataRow dr in dt_com.Rows)
                {
                    list.Add(new ListItem(dr["com_id"].ToString(), dr["com_name"].ToString()));
                }
            }

            dgcbc.DisplayMember = "Text";
            dgcbc.ValueMember = "Value";
            dgcbc.DataSource = list;
        }

        /// <summary>
        /// 获取公司组织信息并绑定
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="companyId"></param>
        /// <param name="typename"></param>
        public static void BindOrganizeComBox(ComboBoxEx cmb, string companyId, string typename)
        {
            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem("", typename));

            string sqlWhere = string.Format(" enable_flag='1' and com_id='{0}'", companyId);
            DataTable dt_Org = DBHelper.GetTable("查询公司信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tb_organization", "org_id,org_name", sqlWhere, "", " order by create_time ");
            if (dt_Org != null && dt_Org.Rows.Count > 0)
            {
                foreach (DataRow dr in dt_Org.Rows)
                {
                    list.Add(new ListItem(dr["org_id"].ToString(), dr["org_name"].ToString()));
                }
            }

            cmb.DisplayMember = "Text";
            cmb.ValueMember = "Value";
            cmb.DataSource = list;
        }

        public static void LogFunctionCall(string fun_id, string setbook_id)
        {
            UserFunctionOPLog FunctionOP_Add = new UserFunctionOPLog();
            FunctionOP_Add.com_id = HXC_FuncUtility.GlobalStaticObj_Server.Instance.ComID;
            FunctionOP_Add.fun_id = fun_id;
            FunctionOP_Add.setbook_id = setbook_id;
            FunctionOP_Add.userOP = new SYSModel.UserIDOP() { UserID = HXC_FuncUtility.GlobalStaticObj_Server.Instance.UserID, OPName = "菜单功能操作日志" };
            BLL.OPLog.Add(FunctionOP_Add, GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode);
        }


    }
}
