using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using Newtonsoft.Json;
using ServiceStationClient.ComponentUI;
using ServiceStationClient.ComponentUI.TextBox;
using SYSModel;
using Utility.Common;
using System.Threading;

namespace HXCPcClient.UCForm
{
    ///*************************************************************************//
    /// System:  
    /// FileName:     UIAssistant         
    /// Author:       Kord
    /// Date:         2014/10/24 14:57:38
    /// Machine Name: KORD-PC
    ///***************************************************************************//
    /// <summary>
    /// 	此类中包含界面设计所需的部分常用公共方法
    /// </summary>
    ///***************************************************************************//
    public static class UIAssistants
    {
        #region Constructor -- 构造函数
        #endregion

        #region Field -- 字段

        #endregion

        #region Property -- 属性
        public static DataTable DbColumnsInfo { get; private set; }
        /// <summary>
        /// 是否启用三包服务单审核
        /// </summary>
        public static Boolean ThreeServiceAudit
        {
            get { return true; }
        }
        /// <summary>
        /// 宇通外服人员信息
        /// </summary>
        private static DataTable _ytContacts = null;
        #endregion

        #region Method -- 方法
        /// <summary>
        /// 设置查询和清除按钮的样式,此方法会修改一下属性值(BackgroundImage,NormalImage,MoveImage,DownImage,Anchor,Size,Caption)
        /// </summary>
        /// <param name="btnQuery">查询按钮</param>
        /// <param name="btnClear">清除按钮</param>
        public static void SetButtonStyle4QueryAndClear(ButtonEx btnQuery, ButtonEx btnClear)
        {
            SetButtonStyle4QueryAndClear(btnQuery, btnClear, false);
        }
        public static void SetButtonStyle4QueryAndClear(UCBase ucBase, ButtonEx btnQuery, ButtonEx btnClear, Boolean autoSize = true)
        {
            var query = new ButtonEx {Caption = btnQuery.Caption};
            query.Click += delegate {
                btnQuery.PerformClick();
            };
            var clear = new ButtonEx {Caption = btnClear.Caption};
            clear.Click += delegate
            {
                btnClear.PerformClick();
            };
            ucBase.AddToolStripItem(clear, 0);
            ucBase.AddToolStripItem(query, 0);
            SetButtonStyle4QueryAndClear(btnQuery, btnClear, false);
        }
        public static void SetButtonStyle4QueryAndClear(ButtonEx btnQuery, ButtonEx btnClear, Boolean autoSize)
        {
            try
            {
                btnQuery.BackgroundImage = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage("Query_E", false);
                btnClear.BackgroundImage = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage("Clear_E", false);
                btnQuery.NormalImage = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage("Query_E", false);
                btnClear.NormalImage = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage("Clear_E", false);
                btnQuery.MoveImage = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage("Query_L", false);
                btnClear.MoveImage = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage("Clear_L", false);
                btnQuery.DownImage = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage("Query_L", false);
                btnClear.DownImage = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage("Clear_L", false);
                if(autoSize)
                    btnQuery.Anchor = btnClear.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right);
                btnQuery.Size = btnClear.Size = new Size(80, 24);
                btnQuery.Caption = btnClear.Caption = "";
            }
            catch (Exception ex)
            {
                //Write log
            }
        }

        //public static Boolean ValidationControlValue(Control ctrl, ErrorProvider erp, String )
        //{
        //    if (ctrl == null) return false;


        //    if (erp != null)
        //    {
        //        //erp.SetError(ctrl, );
        //    }
        //    return true;
        //}

        /// <summary>
        /// 根据数据行获取数据字段数据列类型
        /// </summary>
        /// <param name="row">数据行</param>
        /// <returns></returns>
        private static SysDbType GetColumnType(DataRow row)
        {
            if (row != null)
            {
                foreach (var name in Enum.GetNames(typeof(SysDbType)))
                {
                    if (String.Equals(row["DATA_TYPE"].ToString(), name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return (SysDbType)Enum.Parse(typeof(SysDbType), name);
                    }
                }
            }
            return default(SysDbType);
        }
        /// <summary>
        /// 从数据库中获取ParamObj相关信息并设置其属性值
        /// </summary>
        public static void SetParamObjInfoByDB(String tableName, Dictionary<string, ParamObj> paramObjs)
        {
            if (DbColumnsInfo == null) return;
            foreach (var paramObj in paramObjs)
            {
                if (paramObj.Value != null && !String.IsNullOrEmpty(paramObj.Value.name))
                {
                    foreach (DataRow row in DbColumnsInfo.Rows)
                    {
                        try
                        {
                            if (row["TABLE_NAME"].ToString() == tableName && row["COLUMN_NAME"].ToString() == paramObj.Value.name)
                            {
                                paramObj.Value.type = GetColumnType(row);
                                paramObj.Value.size = Convert.ToInt32(row["CHARACTER_MAXIMUM_LENGTH"].ToString());
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
            }
        }
        /// <summary>
        /// 清空查询面板中条件控件的值
        /// </summary>
        /// <param name="pnlQuery"></param>
        public static void ClearQueryControlValue(PanelEx pnlQuery)
        {
            if (pnlQuery != null)
            {
                foreach (var control in pnlQuery.Controls)
                {
                    if (control.GetType() == typeof(TextBoxEx))    //文本框
                    {
                        var textBoxEx = control as TextBoxEx;
                        if (textBoxEx != null) textBoxEx.Caption = String.Empty;
                    }
                    else if (control.GetType() == typeof(TextChooser)) //选择器
                    {
                        var txtChooser = control as TextChooser;
                        if (txtChooser == null) continue;
                        txtChooser.Text = String.Empty;
                        txtChooser.Tag = String.Empty;
                    }
                    else if (control.GetType() == typeof(ComboBoxEx))   //下拉框
                    {
                        var comboBoxEx = control as ComboBoxEx;
                        if (comboBoxEx == null) continue;
                        if (comboBoxEx.Items.Count > 0) comboBoxEx.SelectedIndex = 0;
                        else comboBoxEx.SelectedIndex = -1;
                    }
                    else if (control.GetType() == typeof(DateTimePickerEx_sms)) //日期选择器
                    {
                        var dateTimePicker = control as DateTimePickerEx_sms;
                        if (dateTimePicker != null) dateTimePicker.Value = String.Empty;
                    }
                }
            }
        }
        /// <summary>
        /// 获取数据表格选中的数据行标识集合
        /// </summary>
        /// <param name="dgvTable">数据表格</param>
        /// <param name="checkColumn">复选框列</param>
        /// <param name="idColumn">标识列</param>
        /// <returns>标识集合</returns>
        public static List<string> GetDataGridCheckRows(DataGridViewEx dgvTable, DataGridViewCheckBoxColumn checkColumn, DataGridViewTextBoxColumn idColumn)
        {
            if (dgvTable == null || checkColumn == null || idColumn == null) return null;
            return (from DataGridViewRow dr in dgvTable.Rows let isCheck = dr.Cells[checkColumn.Name].EditedFormattedValue where isCheck != null && (bool)isCheck select dr.Cells[idColumn.Name].Value.ToString()).ToList();
        }
        /// <summary>
        /// 获取数据表格选中的数据行
        /// </summary>
        /// <param name="dgvTable">数据表格</param>
        /// <param name="checkColumn">复选框列</param>
        /// <param name="idColumn">标识列</param>
        /// <param name="tableName">数据源表名</param>
        /// <returns>数据视图</returns>
        public static DataView GetDataGridCheckRows(DataGridViewEx dgvTable, DataGridViewCheckBoxColumn checkColumn, DataGridViewTextBoxColumn idColumn, String tableName)
        {
            var selectedRows = (from DataGridViewRow dr in dgvTable.Rows let isCheck = dr.Cells[checkColumn.Name].EditedFormattedValue where isCheck != null && (bool)isCheck select dr.Cells[idColumn.Name].Value.ToString()).ToList();
            if (selectedRows.Count > 0)
            {
                var whereSqlSb = new StringBuilder();
                whereSqlSb.Append(" 1=1");
                foreach (var selectedRow in selectedRows)
                {
                    whereSqlSb.Append(" or ");
                    whereSqlSb.Append(idColumn.DataPropertyName);
                    whereSqlSb.Append(" ='");
                    whereSqlSb.Append(selectedRow);
                    whereSqlSb.Append("'");
                }
                var whereSql = whereSqlSb.ToString().Replace(" 1=1 or ", "");
                var data = DBHelper.GetTable("根据标识获取数据表信息", tableName, "*", whereSql, "", "");
                if (data != null && data.DefaultView != null)
                {
                    return data.DefaultView;
                }
            }
            return null;
        }

        /// <summary>
        /// 将外服务站用户数据绑定到指定列表控件,数据源为一个DataTable
        /// </summary>
        /// <remarks>
        ///     宇通联系人CRM ID:cont_crm_guid
        ///           联系人名称:cont_name
        ///           联系人电话:cont_phone
        /// </remarks>
        /// <param name="cbo">列表控件</param>
        /// <param name="isShowFrist"></param>
        /// <param name="typename"></param>
        public static void BindingServiceStationUser(ComboBoxEx cbo, Boolean isShowFrist, String typename)
        {
            if (_ytContacts == null || _ytContacts.DefaultView.Count < 1)
            {
                var listItem = DBHelper.WebServHandlerByObj("获取宇通外服人员", EnumWebServFunName.SearchContact, "");
                _ytContacts = JsonConvert.DeserializeObject<DataTable>(listItem);
            }
            DataTable newDt;
            if (_ytContacts != null)
            {
                newDt = _ytContacts.Copy();
            }
            else
            {
                newDt = new DataTable();
                newDt.Columns.Add(new DataColumn("cont_crm_guid", typeof (String)));
                newDt.Columns.Add(new DataColumn("cont_name", typeof(String)));
                newDt.Columns.Add(new DataColumn("cont_phone", typeof(String)));

            }
            if (isShowFrist)
            {
                var dr = newDt.NewRow();
                dr["cont_crm_guid"] = "";
                dr["cont_name"] = typename;
                dr["cont_phone"] = "";
                newDt.Rows.InsertAt(dr, 0);
            }
            cbo.DataSource = newDt;
            cbo.ValueMember = "cont_crm_guid";
            cbo.DisplayMember = "cont_name";

            cbo.SelectedIndex = 0;
        }

       

        #region 数据库数据转换
        /// <summary>
        /// 根据码表ID获取其对应的名称
        /// </summary>
        /// <param name="id">码表ID值</param>
        public static String GetDicName(String id)
        {
            try
            {
                return LocalCache.GetDictNameById(id);
            }
            catch (Exception ex)
            {
                GlobalStaticObj.GlobalLogService.WriteLog(ex);
                return String.Empty;
            }
        }
        #endregion

        #region DataGridView中单元格数据转换
        /// <summary>
        /// 将数据表格中的数据(码表ID)转换为数据码表中的名称
        /// </summary>
        /// <param name="sender">数据表格</param>
        /// <param name="args">事件数据</param>
        /// <param name="dpName">数据列明</param>
        public static void DgvCellDataConvert2DicData(DataGridView sender, DataGridViewCellFormattingEventArgs args, String dpName)
        {
            var fieldNmae = sender.Columns[args.ColumnIndex].DataPropertyName;
            if (!fieldNmae.Equals(dpName)) return;
            if (String.IsNullOrEmpty(CommonCtrl.IsNullToString(args.Value)))
            {
                return;
            }           
            args.Value = GetDicName(args.Value.ToString());
        }
        /// <summary>
        /// 将数据表格中的数据转换为相对应的日期
        /// </summary>
        /// <param name="sender">数据表格</param>
        /// <param name="args">事件数据</param>
        /// <param name="dpName">数据列明</param>
        public static void DgvCellDataConvert2Datetime(DataGridView sender, DataGridViewCellFormattingEventArgs args, String dpName)
        {
            var fieldNmae = sender.Columns[args.ColumnIndex].DataPropertyName;
            if (!fieldNmae.Equals(dpName)) return;
            if (String.IsNullOrEmpty(CommonCtrl.IsNullToString(args.Value)))
            {
                return;
            }
            var ticks = Convert.ToInt64(args.Value);
            args.Value = Common.UtcLongToLocalDateTime(ticks);
        }
        /// <summary>
        /// 将数据表格中的数据转换为相对应的数据库表中的列数据
        /// </summary>
        /// <param name="sender">数据表格</param>
        /// <param name="args">事件数据</param>
        /// <param name="dpName">数据列明</param>
        /// <param name="tableName">数据表明</param>
        /// <param name="pkColName">主键列名</param>
        /// <param name="valueColName">数据值列名</param>
        public static void DgvCellDataConvert2Table(DataGridView sender, DataGridViewCellFormattingEventArgs args, String dpName, String tableName, String pkColName, String valueColName)
        {
            var fieldNmae = sender.Columns[args.ColumnIndex].DataPropertyName;
            if (!fieldNmae.Equals(dpName)) return;
            if (String.IsNullOrEmpty(CommonCtrl.IsNullToString(args.Value)))
            {
                return;
            }
            var dbValue = DBHelper.GetSingleValue("获取数据表值", tableName, valueColName, pkColName + "='" + args.Value + "'", "");
            args.Value = dbValue;
        }

        /// <summary>
        /// 将数据表格中的数据转换为相对应的枚举类型
        /// </summary>
        /// <param name="sender">数据表格</param>
        /// <param name="args">事件数据</param>
        /// <param name="dpName">数据列明</param>
        /// <param name="enumType">枚举类型</param>
        public static void DgvCellDataConvert2Enum(DataGridView sender, DataGridViewCellFormattingEventArgs args, String dpName, Type enumType)
        {
            var fieldNmae = sender.Columns[args.ColumnIndex].DataPropertyName;
            if (!fieldNmae.Equals(dpName) || enumType == null) return;
            if (String.IsNullOrEmpty(CommonCtrl.IsNullToString(args.Value)))
            {
                return;
            }
            var enumValue = Enum.Parse(enumType, args.Value.ToString(), true) as Enum;
            if (enumValue != null)
            {
                args.Value = enumValue.GetDescription(true);
            }
        }
        /// <summary>
        /// 将数据表格中的数据转换为相对应的数据库表中的列数据
        /// </summary>
        /// <param name="sender">数据表格</param>
        /// <param name="args">事件数据</param>
        /// <param name="dpName">数据列明</param>
        /// <param name="tableName">数据表明</param>
        /// <param name="pkColName">主键列名</param>
        /// <param name="valueColName1">数据值列名1</param>
        /// <param name="valueColName2">数据值列名2</param>
        public static void DgvCellData2Connect(DataGridView sender, DataGridViewCellFormattingEventArgs args, String dpName, String tableName, String pkColName, String valueColName1, String valueColName2)
        {
            var fieldNmae = sender.Columns[args.ColumnIndex].DataPropertyName;
            if (!fieldNmae.Equals(dpName)) return;
            if (String.IsNullOrEmpty(CommonCtrl.IsNullToString(args.Value)))
            {
                return;
            }
            var dbValue1 = Convert.ToInt64(DBHelper.GetSingleValue("获取数据表值", tableName, valueColName1, pkColName + "='" + args.Value + "'", ""));
            var dbValue2 = Convert.ToInt64(DBHelper.GetSingleValue("获取数据表值", tableName, valueColName2, pkColName + "='" + args.Value + "'", ""));
            args.Value = Common.UtcLongToLocalDateTime(dbValue1) + " -- " + Common.UtcLongToLocalDateTime(dbValue2);
        }

        #endregion

        #region UCBase 功能按钮可见性设置
        public static void SetUCBaseFuncationVisible(UCBase ucBase, ObservableCollection<ButtonEx_sms> cols)
        {
            foreach (Control ctrl in ucBase.tlp_command.Controls)
            {
                ctrl.Visible = false;
                var ctrl1 = ctrl;
                foreach (var btn in cols.Where(btn => Equals(btn, ctrl1)))
                {
                    btn.Visible = true;
                }
            }
        }
        #endregion

        #region 设置DataGridView复选框列为可勾选
        /// <summary>
        /// 设置DataGridView复选框列为可勾选
        /// </summary>
        /// <param name="dgvTable">数据表控件</param>
        /// <param name="drchkCheck">复选框列</param>
        public static void SetDataGridViewCheckColumn(DataGridViewEx dgvTable, DataGridViewCheckBoxColumn drchkCheck)
        {
            dgvTable.ReadOnly = false;
            foreach (DataGridViewColumn dgvc in dgvTable.Columns)
            {
                if (dgvc == drchkCheck)
                {
                    drchkCheck.ReadOnly = false;
                    continue;
                }
                dgvc.ReadOnly = true;
            }
        }
        #endregion
        #endregion

        #region Event -- 事件

        #endregion

        public class ExtDataFieldAssistants
        {
            #region Constructor -- 构造函数
            static ExtDataFieldAssistants()
            {
                DbColumnsInfo = DBHelper.GetTable("查询数据库列信息", "v_tables_info", "*", "null", "", "");
            }

            #endregion

            #region Field -- 字段

            #endregion

            #region Method -- 方法

            #endregion

            #region Event -- 事件

            #endregion
        }
    }
}
