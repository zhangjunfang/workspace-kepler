using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using SYSModel;
using Utility.Common;

namespace HXCPcClient.UCForm.RepairBusiness.YuTongThreeGuaranty
{
    ///*************************************************************************//
    /// System:       HuiXuCheSYS
    /// FileName:     UCFormQueryTemplate         
    /// Author:       Kord(baoqiang.guo@outlook.com)
    /// Date:         2014/11/17 11:39:19
    /// Machine Name: KORD
    ///***************************************************************************//
    /// Function: 
    /// 	客户端查询窗体模板
    ///***************************************************************************//
    public partial class UCFormQueryTemplate : UCBase
    {
        #region Constructor -- 构造函数
        public UCFormQueryTemplate()
        {
            InitializeComponent();

            Init(); //初始化方法

            Load += (sender, args) => UIAssistants.SetDataGridViewCheckColumn(dgv_table, drchk_check);
        }
        #endregion

        #region Field -- 字段
        public const String TableName = "tb_maintain_three_guaranty_settlement";
        #endregion

        #region Property -- 属性

        #endregion

        #region Method -- 方法

        /// <summary>
        /// 初始化方法
        /// </summary>
        private void Init()
        {
            SetFuncationButtonVisible();
            InitQueryControlDataSource();
            FuncationRegiste();
        }
        /// <summary>
        /// 设置功能按钮可见性
        /// </summary>
        private void SetFuncationButtonVisible()
        {
            var btnCols = new ObservableCollection<ButtonEx_sms>()
            {
                btnExport,btnView,btnPrint,btnPrint,btnSet
            };
            UIAssistants.SetUCBaseFuncationVisible(this, btnCols);
        }
        /// <summary>
        /// 初始化查询控件数据项
        /// </summary>
        private void InitQueryControlDataSource()
        {
            DataSources.BindComBoxDataEnum(cbo_combobox, typeof(DataSources.EnumYesNo), true);  //枚举类型绑定
            CommonCtrl.BindComboBoxByDictionarr(cbo_combobox, "settlement_repair_status_yt", true);   //数据字典绑定
            txt_chooser.ChooserClick += delegate  //选择器
            {
                var custChooser = new FormChooser();
                var result = custChooser.ShowDialog();
                if (result != DialogResult.OK) return;
                //对查询控件进行赋值
            };
        }
        /// <summary>
        /// 转换数据列数据
        /// </summary>
        /// <param name="args"></param>
        private void ConvertDataGridColumnsData(DataGridViewCellFormattingEventArgs args)
        {
            UIAssistants.DgvCellDataConvert2DicData(dgv_table, args, "info_status_yt"); //数据字典
            UIAssistants.DgvCellDataConvert2Enum(dgv_table, args, "data_source", typeof(DataSources.EnumDataSources));  //枚举类型
            UIAssistants.DgvCellDataConvert2Datetime(dgv_table, args, "create_time");   //时间类型
        }
        /// <summary>
        /// 事件注册
        /// </summary>
        private void FuncationRegiste()
        {
            #region 数据表格
            //单元格数据转换
            dgv_table.CellFormatting += (sender, args) => ConvertDataGridColumnsData(args);
            //数据行双击事件
            dgv_table.CellDoubleClick += delegate(object sender, DataGridViewCellEventArgs e)
            {
                if (e.RowIndex < 0) return;
                var id = dgv_table.Rows[e.RowIndex].Cells["drtxt_id"].Value.ToString();
                //var ucCustomerView = new UCCustomerViews
                //{
                //    Id = id,
                //    UCCustomerManager = this
                //};
                addUserControl(null, "", "", Tag.ToString(), Name);
            };
            #endregion

            #region 数据翻页
            pageQ.PageIndexChanged += delegate
            {
                BindPageData();
            };
            #endregion

            #region 清除按钮事件
            btn_clear.Click += delegate
            {
                UIAssistants.ClearQueryControlValue(pnl_query);
            };
            #endregion

            #region 查询按钮
            btn_query.Click += (sender, args) => BindPageData();
            #endregion

            #region 添加按钮事件
            AddEvent += delegate
            {
                //var uc = new UCMaintainThreeGuarantyEdit { UCForm = this, windowStatus = WindowStatus.Add };
                //uc.addUserControl(uc, "三包服务单-新增", "UCMaintainThreeGuarantyAdd", Tag.ToString(), Name);
            };
            #endregion

            #region 复制按钮事件
            CopyEvent += delegate
            {
                var id = "";
                var dataView = UIAssistants.GetDataGridCheckRows(dgv_table, drchk_check, drtxt_id, TableName);
                if (dataView == null) return;
                foreach (DataRowView rowView in dataView)
                {
                    id = rowView["id"].ToString();
                    break;
                }
                //var uc = new UCMaintainThreeGuarantyEdit { UCForm = this, TgId = tgid, windowStatus = WindowStatus.Copy };
                //uc.addUserControl(uc, "三包服务单-复制", "UCMaintainThreeGuarantyCopy", Tag.ToString(), Name);
            };
            #endregion

            #region 编辑按钮事件
            EditEvent += delegate
            {
                var id = "";
                var dataView = UIAssistants.GetDataGridCheckRows(dgv_table, drchk_check, drtxt_id, TableName);
                if (dataView == null) return;
                foreach (DataRowView rowView in dataView)
                {
                    id = rowView["id"].ToString();
                    break;
                }
                //var uc = new UCMaintainThreeGuarantyEdit { UCForm = this, TgId = tgid, windowStatus = WindowStatus.Copy };
                //uc.addUserControl(uc, "三包服务单-编辑", "UCMaintainThreeGuarantyCopy", Tag.ToString(), Name);
            };
            #endregion

            #region 删除按钮事件
            DeleteEvent += delegate
            {
                var selectedRows = UIAssistants.GetDataGridCheckRows(dgv_table, drchk_check, drtxt_id);
                if (selectedRows.Count == 0)
                {
                    MessageBoxEx.Show("请选择删除记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBoxEx.Show("确认要删除吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
                var flag = false;
                //var comField = new Dictionary<string, string> { { "enable_flag", "0" } };
                //flag = DBHelper.BatchUpdateDataByIn("删除三包服务单", TableName, comField, "tg_id", selectedRows.ToArray());
                if (flag)
                {
                    BindPageData();
                    if (dgv_table.Rows.Count > 0)
                    {
                        dgv_table.CurrentCell = dgv_table.Rows[0].Cells[0];
                    }
                    MessageBoxEx.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBoxEx.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            #endregion

            #region 根据选择的数据判断功能按钮的显示状态
            dgv_table.CellMouseUp += delegate
            {
                var dataView = UIAssistants.GetDataGridCheckRows(dgv_table, drchk_check, drtxt_id, TableName);
                var listField = UIAssistants.GetDataGridCheckRows(dgv_table, drchk_check, drtxt_id);
                if (dataView == null || listField.Count == 0)
                {
                    btnCopy.Enabled = false;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                }
                else
                {
                    btnCopy.Enabled = true;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                }
            };
            #endregion
        }
        /// <summary> 
        /// 绑定数据
        /// </summary>
        public void BindPageData()
        {
            try
            {
                var where = new StringBuilder(" enable_flag='1'");  //enable_flag 1 未删除
                if (!String.IsNullOrEmpty(txt_textbox.Caption.Trim()))  //文本框
                {
                    where.Append(String.Format(" and column like '%{0}%'", txt_textbox.Caption.Trim()));
                }
                if (!String.IsNullOrEmpty(txt_chooser.Text.Trim()))  //选择器
                {
                    where.Append(String.Format(" and column like '%{0}%'", txt_chooser.Text.Trim()));
                }
                if (cbo_combobox.SelectedValue != null && cbo_combobox.SelectedValue.ToString() != "")  //下拉框
                {
                    where.Append(String.Format(" and column = '{0}'", cbo_combobox.SelectedValue));
                }
                if (!String.IsNullOrEmpty(dtp_datetime.Value))  //日期控件
                {
                    where.Append(String.Format(" and column = '{0}'", Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dtp_datetime.Value))));
                }
                if (!String.IsNullOrEmpty(dtp_date_cycle_s.Value))  //时间周期控件 -- 开始
                {
                    where.Append(String.Format(" and column >= '{0}'", Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dtp_date_cycle_s.Value))));
                }
                if (!String.IsNullOrEmpty(dtp_date_cycle_e.Value))  //时间周期控件 -- 结束
                {
                    where.Append(String.Format(" and column <= '{0}'", Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dtp_date_cycle_e.Value))));
                }

                Int32 recordCount;

                var dt = DBHelper.GetTableByPage("分页查询数据", TableName, "*", where.ToString(), "", "", pageQ.PageIndex, pageQ.PageSize, out recordCount);
                dgv_table.DataSource = dt;
                pageQ.RecordCount = recordCount;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Event -- 事件

        #endregion
    }
}
