using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ServiceStationClient.ComponentUI.TextBox;

namespace HXCPcClient.CommonClass
{
    public class Choosefrm
    {
        public delegate void GreetingDelegate(DataRow dr);
        public static GreetingDelegate delDataBack;

        #region --供应商编码选择器获取数据
        /// <summary> 供应商编码速查选择器
        /// </summary>
        /// <param name="ContorlName">控件ID</param>
        /// <param name="delDataBack">回调函数</param>
        public static void SupperCodeChoose(TextChooser ContorlName, GreetingDelegate delDataBack)
        {
            string table = "tb_supplier";
            string displayColumn = "sup_code";
            string valueColumn = "sup_full_name";
            //设置表和显示字段
            ContorlName.SetBindTable(table, displayColumn, valueColumn);
            ContorlName.GetDataSourced += new TextChooser.GetDataSourceHandler(Sup_CodeBindData);
            if (delDataBack != null)
            {
                ContorlName.DataBacked -= new TextChooser.DataBackHandler(delDataBack);
                ContorlName.DataBacked += new TextChooser.DataBackHandler(delDataBack);
            }
        }
        /// <summary> 绑定供应商编码到选择器
        /// </summary>
        /// <param name="tc"></param>
        /// <param name="sqlString"></param>
        private static void Sup_CodeBindData(TextChooser tc, string sqlString)
        {
            DataTable dt = CommonFuncCall.GetDataSource(sqlString);
            tc.SetDataSource(dt);
            if (dt != null)
            {
                tc.Search();
            }
        }
        #endregion

        #region --供应商名称选择器获取数据
        /// <summary> 供应商名称速查选择器
        /// </summary>
        /// <param name="ContorlName">控件ID</param>
        /// <param name="delDataBack">回调函数</param>
        public static void SupperNameChoose(TextChooser ContorlName, GreetingDelegate delDataBack)
        {
            string table = "tb_supplier";
            string valueColumn = "sup_full_name";
            //设置表和显示字段
            ContorlName.SetBindTable(table, valueColumn);
            ContorlName.GetDataSourced += new TextChooser.GetDataSourceHandler(Sup_NameBindData);
            if (delDataBack != null)
            {
                ContorlName.DataBacked -= new TextChooser.DataBackHandler(delDataBack);
                ContorlName.DataBacked += new TextChooser.DataBackHandler(delDataBack);
            }
        }
        /// <summary> 绑定供应商名称到选择器
        /// </summary>
        /// <param name="tc"></param>
        /// <param name="sqlString"></param>
        private static void Sup_NameBindData(TextChooser tc, string sqlString)
        {
            DataTable dt = CommonFuncCall.GetDataSource(sqlString);
            tc.SetDataSource(dt);
            if (dt != null)
            {
                tc.Search();
            }
        }
        #endregion

        #region --客户编码选择器获取数据
        /// <summary> 客户编码速查选择器
        /// </summary>
        /// <param name="ContorlName">控件ID</param>
        /// <param name="delDataBack">回调函数</param>
        public static void CusCodeChoose(TextChooser ContorlName, GreetingDelegate delDataBack)
        {
            string table = "tb_customer";
            string displayColumn = "cust_code";
            string valueColumn = "cust_name";
            //设置表和显示字段
            ContorlName.SetBindTable(table, displayColumn, valueColumn);
            ContorlName.GetDataSourced += new TextChooser.GetDataSourceHandler(Cus_CodeBindData);
            if (delDataBack != null)
            {
                ContorlName.DataBacked -= new TextChooser.DataBackHandler(delDataBack);
                ContorlName.DataBacked += new TextChooser.DataBackHandler(delDataBack);
            }
        }
        /// <summary> 绑定客户编码到选择器
        /// </summary>
        /// <param name="tc"></param>
        /// <param name="sqlString"></param>
        private static void Cus_CodeBindData(TextChooser tc, string sqlString)
        {
            DataTable dt = CommonFuncCall.GetDataSource(sqlString);
            tc.SetDataSource(dt);
            if (dt != null)
            {
                tc.Search();
            }
        }
        #endregion

        #region --客户名称选择器获取数据
        /// <summary> 客户名称速查选择器
        /// </summary>
        /// <param name="ContorlName">控件ID</param>
        /// <param name="delDataBack">回调函数</param>
        public static void CusNameChoose(TextChooser ContorlName, GreetingDelegate delDataBack)
        {
            string table = "tb_customer";
            string valueColumn = "cust_name";
            //设置表和显示字段
            ContorlName.SetBindTable(table, valueColumn);
            ContorlName.GetDataSourced += new TextChooser.GetDataSourceHandler(Cus_NameBindData);
            if (delDataBack != null)
            {
                ContorlName.DataBacked -= new TextChooser.DataBackHandler(delDataBack);
                ContorlName.DataBacked += new TextChooser.DataBackHandler(delDataBack);
            }
        }
        /// <summary> 绑定客户名称到选择器
        /// </summary>
        /// <param name="tc"></param>
        /// <param name="sqlString"></param>
        private static void Cus_NameBindData(TextChooser tc, string sqlString)
        {
            DataTable dt = CommonFuncCall.GetDataSource(sqlString);
            tc.SetDataSource(dt);
            if (dt != null)
            {
                tc.Search();
            }
        }
        #endregion

        #region --配件编码选择器获取数据
        /// <summary> 配件编码速查选择器
        /// </summary>
        /// <param name="ContorlName">控件ID</param>
        /// <param name="delDataBack">回调函数</param>
        public static void PartsCodeChoose(TextChooser ContorlName, GreetingDelegate delDataBack)
        {
            string table = "tb_parts";
            string displayColumn = "ser_parts_code";
            string valueColumn = "parts_name";
            //设置表和显示字段
            ContorlName.SetBindTable(table, displayColumn, valueColumn);
            ContorlName.GetDataSourced += new TextChooser.GetDataSourceHandler(Parts_CodeBindData);
            if (delDataBack != null)
            {
                ContorlName.DataBacked -= new TextChooser.DataBackHandler(delDataBack);
                ContorlName.DataBacked += new TextChooser.DataBackHandler(delDataBack);
            }
        }
        /// <summary> 绑定配件编码到选择器
        /// </summary>
        /// <param name="tc"></param>
        /// <param name="sqlString"></param>
        private static void Parts_CodeBindData(TextChooser tc, string sqlString)
        {
            DataTable dt = CommonFuncCall.GetDataSource(sqlString);
            tc.SetDataSource(dt);
            if (dt != null)
            {
                tc.Search();
            }
        }
        #endregion

        #region --配件名称选择器获取数据
        /// <summary> 配件名称速查选择器
        /// </summary>
        /// <param name="ContorlName">控件ID</param>
        /// <param name="delDataBack">回调函数</param>
        public static void PartsNameChoose(TextChooser ContorlName, GreetingDelegate delDataBack)
        {
            string table = "tb_parts";
            string valueColumn = "parts_name";
            //设置表和显示字段
            ContorlName.SetBindTable(table, valueColumn);
            ContorlName.GetDataSourced += new TextChooser.GetDataSourceHandler(Parts_NameBindData);
            if (delDataBack != null)
            {
                ContorlName.DataBacked -= new TextChooser.DataBackHandler(delDataBack);
                ContorlName.DataBacked += new TextChooser.DataBackHandler(delDataBack);
            }
        }
        /// <summary> 绑定配件名称到选择器
        /// </summary>
        /// <param name="tc"></param>
        /// <param name="sqlString"></param>
        private static void Parts_NameBindData(TextChooser tc, string sqlString)
        {
            DataTable dt = CommonFuncCall.GetDataSource(sqlString);
            tc.SetDataSource(dt);
            if (dt != null)
            {
                tc.Search();
            }
        }
        #endregion

        #region --配件类型名称选择器获取数据
        /// <summary> 配件类型名称速查选择器
        /// </summary>
        /// <param name="ContorlName">控件ID</param>
        /// <param name="delDataBack">回调函数</param>
        public static void PartsTypeNameChoose(TextChooser ContorlName, GreetingDelegate delDataBack)
        {
            string table = "sys_dictionaries";
            string valueColumn = "dic_name";
            //设置表和显示字段
            ContorlName.SetBindTable(table, valueColumn);
            ContorlName.GetDataSourced += new TextChooser.GetDataSourceHandler(PartsType_NameBindData);
            if (delDataBack != null)
            {
                ContorlName.DataBacked -= new TextChooser.DataBackHandler(delDataBack);
                ContorlName.DataBacked += new TextChooser.DataBackHandler(delDataBack);
            }
        }
        /// <summary> 绑定配件类型名称到选择器
        /// </summary>
        /// <param name="tc"></param>
        /// <param name="sqlString"></param>
        private static void PartsType_NameBindData(TextChooser tc, string sqlString)
        {
            sqlString = string.Format(@"select dic_name from sys_dictionaries where dic_id not in 
                                      (select parent_id from sys_dictionaries) and dic_name like '%{0}%'", tc.Text);
            DataTable dt = CommonFuncCall.GetDataSource(sqlString);
            tc.SetDataSource(dt);
            if (dt != null)
            {
                tc.Search();
            }
        }
        #endregion

        #region --配件车型名称选择器获取数据
        /// <summary> 配件车型名称速查选择器
        /// </summary>
        /// <param name="ContorlName">控件ID</param>
        /// <param name="delDataBack">回调函数</param>
        public static void PartsCarModelNameChoose(TextChooser ContorlName, GreetingDelegate delDataBack)
        {
            string table = "tb_vehicle_models";
            string valueColumn = "vm_name";
            //设置表和显示字段
            ContorlName.SetBindTable(table, valueColumn);
            ContorlName.GetDataSourced += new TextChooser.GetDataSourceHandler(PartsCarModel_NameBindData);
            if (delDataBack != null)
            {
                ContorlName.DataBacked -= new TextChooser.DataBackHandler(delDataBack);
                ContorlName.DataBacked += new TextChooser.DataBackHandler(delDataBack);
            }
        }
        /// <summary> 绑定配件车型名称到选择器
        /// </summary>
        /// <param name="tc"></param>
        /// <param name="sqlString"></param>
        private static void PartsCarModel_NameBindData(TextChooser tc, string sqlString)
        {
            DataTable dt = CommonFuncCall.GetDataSource(sqlString);
            tc.SetDataSource(dt);
            if (dt != null)
            {
                tc.Search();
            }
        }
        #endregion
    }
}
